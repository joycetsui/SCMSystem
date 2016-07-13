
''''''''''''''' Setting up parameters '''''''''''''''
Dim strPath
Dim strSheet

strPath = WScript.Arguments(0)
strSheet = WScript.Arguments(1)

strPath = "D:\Documents\GitHub\cs490-scm\database"
strSheet = cstr(strSheet)

msgbox strpath
Dim intDataRowStart
Dim intDataColumnStart
Dim intServerRowNum
Dim intDatabaseRowNum
Dim intTableRowNum
Dim intColumnRowNum
Dim intSQLQueryRowNum

intDataRowStart = WScript.Arguments(2)
intDataColumnStart = WScript.Arguments(3)
intServerRowNum = WScript.Arguments(4)

'not currently used :/
intDatabaseRowNum = WScript.Arguments(5)
intTableRowNum = WScript.Arguments(6)
intColumnRowNum = WScript.Arguments(7)
intSQLQueryRowNum = WScript.Arguments(8)


intDataRowStart = cint(intDataRowStart)
intDataColumnStart = cint(intDataColumnStart)
intServerRowNum = cint(intServerRowNum)

Dim intColumnCounter
Dim intRowCounter


''''''''''''''' Read info from excel '''''''''''''''

Dim xlApp, xlBook, xlSht

Set xlApp = GetObject(strPath)
'Set xlBook = xlApp.ActiveWorkbook
Set xlSht = xlApp.ActiveSheet

' intNumCols is used to count the number of columns and is used throughout
' indexA and indexB are just counter variables used in various for loops
Dim intNumCols, indexA, indexB
intNumCols = 0

intColumnCounter = intDataColumnStart

Do 
    msgbox (xlSht.Name)
    If xlSht.Cells(intServerRowNum, intColumnCounter).Value = "" Then Exit Do
    intNumCols = intNumCols + 1
    intColumnCounter = intColumnCounter + 1
Loop

If intNumCols = 0 Then WScript.Quit
intNumCols = intNumCols - 1

Dim ArgArray()
Redim ArgArray(intNumCols, 6) 'total args

intColumnCounter = intDataColumnStart

For indexA = 0 to intNumCols
    intRowCounter = intServerRowNum
    For indexB = 0 to 6 ' total args
        ArgArray(indexA, indexB) = xlSht.Cells(intRowCounter, intColumnCounter)
        intRowCounter = intRowCounter + 1
    Next
    intColumnCounter = intColumnCounter + 1
Next


''''''''''''''' save & close active sheet '''''''''''''''
xlApp.Save
xlApp.Close



' deallocate
set xlSht = Nothing
'Set xlBook = Nothing
Set xlApp = Nothing

''''''''''''''' Open new Excel workbook as different user '''''''''''''''
Dim objShell
set objShell = Wscript.CreateObject("WScript.Shell")
objShell.Run "TASKKILL /F /IM excel.exe", vbHide
Msgbox ("Closed?")
objShell.Run "RunAs /user:mfcgd\%username% ""C:\Program Files\Microsoft Office 15\root\office15\EXCEL.EXE""", 1, True

'objShell.Run "cd %HOMEPATH%Users\%username%"
'objShell.Run "echo text-to-be-written-into-the-file  > filename.txt"

Set xlApp = GetObject("", "Excel.Application")
Set xlBook = xlApp.Workbooks.Add()
'Set xlSht = xlBook.Wirj

'xlBook.Sheets.Cells(1,1) = "Hello World"

Dim xlmodule
Set xlmodule = xlBook.VBProject.VBComponents.Add(1)
strCode = "sub test()" & vbCr & _ 
       "   msgbox ""Inside the macro"" " & vbCr & _ 
       "end sub" 

xlmodule.CodeModule.AddFromString strCode

xlApp.Run "test"
''''''''''''''' GetData '''''''''''''''

' Import macro module into workbook
'xlApp.VBE.ActiveVBProject.VBComponents.Import "\\mlisfile03\scratch1\GetDataAzure.bas"

' Run macro
'xlApp.Run "GetDataAzure", ArgArray, intNumCols

' Save output into a variable
msgbox ("2")

Wscript.Quit
''''''''''''''' Copy output '''''''''''''''




