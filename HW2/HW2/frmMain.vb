Imports System.IO
Public Class frmMain
    Private strFileName As String
    Private dblTotalInvValue As Double
    Private intTotalCount As Integer
    Private arrCategories As ArrayList
    'Private Stats As frmStats
#Region "Column Contstants"
    'contansts to maanage the listView columns
    Private Const EMP_ID As Integer = 0
    Private Const LAST_NAME As Integer = 1
    Private Const FIRST_NAME As Integer = 2
    Private Const JOB_CODE As Integer = 3
    Private Const DEPTARTMENT As Integer = 4
    Private Const EVALUATION As Integer = 5
    Private Const RATING As Integer = 6
    Private Const BASE_SALARY As Integer = 7
    Private Const BONUS_PERCENT As Integer = 8
    Private Const BONUS_AMT As Integer = 9
    Private Const TOTAL_PAID As Integer = 10
#End Region
    Private Sub OpenFile()
        Dim intResult As Integer
        ofdOpen.InitialDirectory = Application.StartupPath
        ofdOpen.Filter = "All files(*.*) |*.*| Text files (*.txt)| *.txt"
        ofdOpen.FilterIndex = 2
        intResult = ofdOpen.ShowDialog
        If intResult = DialogResult.Cancel Then 'user cancelled the open
            Exit Sub
        End If
        strFileName = ofdOpen.FileName
        Try
            ReadinputFile(strFileName)
        Catch exNotFound As FileNotFoundException
            'put error handling code here
            'message box for example then exit
        Catch exIOError As IOException
            'put error handling code here

        Catch ex As Exception 'anything else that might go wrong
            'put error handling code here
        End Try
    End Sub
    Private Sub SaveFile()
        Dim intResult As Integer
        sfdSave.InitialDirectory = Application.StartupPath
        sfdSave.Filter = "All files (*.*)| *.*| Text files (*.txt)|*.txt"
        sfdSave.FilterIndex = 2
        intResult = sfdSave.ShowDialog
        If intResult = DialogResult.Cancel Then
            Exit Sub
        End If
        strFileName = sfdSave.FileName
        Try
            writeOutputFile(strFileName)
        Catch exNotFound As FileNotFoundException
            'put error handling code here
        Catch exIoError As IOException

            'put error handling code here
        Catch exOther As Exception ' anything else that might go wrong 
            'put error handling code here
        End Try
    End Sub
    Private Sub writeOutputFile(strName As String)
        Dim fileOut As StreamWriter
        Dim strLineOut As String = ""
        Dim i As Integer
        Dim j As Integer
        Try
            fileOut = New StreamWriter(strName)
            'build the field names as the first line in the output file by reading the column names
            For i = 0 To lvwTaxData.Columns.Count - 1
                If i < lvwTaxData.Columns.Count - 1 Then ' not the last column
                    strLineOut &= lvwTaxData.Columns(i).Text
                End If
            Next
            'write out the heading to the output file 
            fileOut.WriteLine(strLineOut)
            'build each data line with commas separtating teh feils 
            'by looping through the rows adn columns of the listview
            For i = 0 To lvwTaxData.Items.Count - 1
                strLineOut = lvwTaxData.Items(i).Text ' Write the first dulmn items
                For j = 1 To lvwTaxData.Items(i).SubItems.Count - 1
                    strLineOut &= "," & lvwTaxData.Items(i).SubItems(j).Text
                Next
                fileOut.WriteLine(strLineOut)
            Next
            fileOut.Close()
            fileOut.Dispose()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub ReadInputFile(strIn As String)
        Dim fileIn As StreamReader
        Dim strLineIn As String
        Dim strFields() As String 'string array to hold the field brom a record in the file
        Dim i As Integer
        lvwTaxData.Items.Clear()
        Try
            fileIn = New StreamReader(strIn)
            If Not fileIn.EndOfStream Then 'get the first line adn use it to build the column headings
                strLineIn = fileIn.ReadLine 'will read each line 
                strFields = strLineIn.Split(",") ' comma is the delimiter and what is used to spearte each value in the line
                For i = 0 To strFields.Length - 1 'build column headings
                    lvwTaxData.Columns.Add(strFields(i)) 'anthing that is ith will become headings in the listview
                Next

                ' do some formatting of the columns 
                With lvwTaxData ' with sets a temp naming concept(shortcut)
                    .Columns(EMP_ID).Width = 80
                    .Columns(LAST_NAME).Width = 100
                    .Columns(FIRST_NAME).Width = 100
                    .Columns(JOB_CODE).Width = 50
                    .Columns(DEPTARTMENT).Width = 30
                    .Columns(EVALUATION).Width = 80
                    .Columns(RATING).Width = 80
                    .Columns(BASE_SALARY).Width = 150
                    .Columns(BONUS_PERCENT).Width = 80
                    .Columns(BONUS_AMT).Width = 150

                    .Columns(TOTAL_PAID).TextAlign = HorizontalAlignment.Right

                End With
            End If
            'now get data for each row
            While Not fileIn.EndOfStream
                strLineIn = fileIn.ReadLine
                strFields = strLineIn.Split(",")
                'set up the first column value in a new listview item (the row)
                Dim lviRow As New ListViewItem(strFields(0))
                'now add the rest of the column values as subitems to that listview item 
                For i = 1 To strFields.Length - 1
                    Dim lsiCal As New ListViewItem.ListViewSubItem
                    If i <> BASE_SALARY Or BONUS_AMT Or TOTAL_PAID Then
                        lsiCal.Text = strFields(i)
                    Else ' it is the money value so format it 
                        lsiCal.Text = FormatCurrency(strFields(i), 0)
                    End If
                    lviRow.SubItems.Add(lsiCal) ' add column to the row 
                Next
                'now add the completed row to the listview
                lvwTaxData.Items.Add(lviRow)
                'UpdateStatistics(lviRow)
            End While
            fileIn.Close()
            fileIn.Dispose()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub btnLoad_Click(sender As Object, e As EventArgs) Handles btnLoad.Click
        OpenFile()
    End Sub

    Private Sub munOpen_Click(sender As Object, e As EventArgs) Handles munOpen.Click
        OpenFile()
    End Sub
End Class
