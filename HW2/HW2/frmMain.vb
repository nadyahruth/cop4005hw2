﻿Imports System.IO
Imports System.Math
Public Class frmMain
    Private strFileName As String
    Private dblTotalInvValue As Double ' total amount 
    Private intTotalCount As Integer ' how many in cat
    Private intTotalEmploy As Integer ' total emply
    Private intPoorRating As Integer ' total empl w/o  poor rating 
    Private arrCategories As ArrayList
    Private Stats As frmStats
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
            ReadInputFile(strFileName)
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
                lvwTaxData.Columns.Add("Ratings") ' adds Rating Header
                lvwTaxData.Columns.Add("Base Salary") ' adds Bonus Salary Header
                lvwTaxData.Columns.Add("Bonus %") ' adds Bonus % Header
                lvwTaxData.Columns.Add("Bonus Amount") ' adds Bonus Amount Headers
                lvwTaxData.Columns.Add("Total Paid") ' adds Total Header


                ' do some formatting of the columns 
                With lvwTaxData ' with sets a temp naming concept(shortcut)
                    .Columns(EMP_ID).Width = 80
                    .Columns(LAST_NAME).Width = 100
                    .Columns(FIRST_NAME).Width = 100
                    .Columns(JOB_CODE).Width = 80
                    .Columns(DEPTARTMENT).Width = 80
                    .Columns(EVALUATION).Width = 90
                    .Columns(BASE_SALARY).Width = 100
                    .Columns(BASE_SALARY).TextAlign = HorizontalAlignment.Right
                    .Columns(BONUS_PERCENT).Width = 80
                    .Columns(BONUS_PERCENT).TextAlign = HorizontalAlignment.Right
                    .Columns(BONUS_AMT).Width = 100
                    .Columns(BONUS_AMT).TextAlign = HorizontalAlignment.Right
                    .Columns(TOTAL_PAID).Width = 100
                    .Columns(TOTAL_PAID).TextAlign = HorizontalAlignment.Right
                End With
            End If
            'now get data for each row
            While Not fileIn.EndOfStream 'as long as there is more data to read
                strLineIn = fileIn.ReadLine ' then read the line 
                strFields = strLineIn.Split(",") ' split the data values by commas
                'set up the first column value in a new listview item (the row)
                Dim lviRow As New ListViewItem(strFields(0)) 'given the value for the first column using strFields (goes under its spot in the array)
                'now add the rest of the column values as subitems to that listview item 
                For i = 1 To strFields.Length - 1 ' loop theough the headers 
                    Dim lsiCal As New ListViewItem.ListViewSubItem ' add a subitem for every column 
                    If i <> BASE_SALARY Or BONUS_AMT Or TOTAL_PAID Then ' if its not money
                        lsiCal.Text = strFields(i) ' just add the text 
                    Else ' it is the money value so format it 
                        lsiCal.Text = FormatCurrency(strFields(i), 0) ' 0 is for the decimal places, and we dont want any
                    End If
                    lviRow.SubItems.Add(lsiCal) ' add column to the row 
                Next
                Dim lsiEval As New ListViewItem.ListViewSubItem 'Evaluation 
                Select Case lviRow.SubItems(EVALUATION).Text
                    Case Is >= 90

                        lsiEval.Text = "Excellent"
                    Case Is >= 80

                        lsiEval.Text = "Good"
                    Case Is >= 70

                        lsiEval.Text = "Fair"
                    Case Is >= 60

                        lsiEval.Text = "Poor"
                End Select
                lviRow.SubItems.Add(lsiEval) 'adding the evaluation to the row
                Dim lsiSal As New ListViewItem.ListViewSubItem 'Salary
                Select Case lviRow.SubItems(JOB_CODE).Text
                    Case "E428"
                        lsiSal.Text = "$42,000"
                    Case "E538"
                        lsiSal.Text = "$39,000"
                    Case "E425"
                        lsiSal.Text = "$38,000"
                    Case "E513"
                        lsiSal.Text = "$56,000"
                    Case "E535"
                        lsiSal.Text = "$57,500"
                    Case "E601"
                        lsiSal.Text = "$67,500"
                End Select
                lviRow.SubItems.Add(lsiSal) ' adding BaseSalary to the row
                Dim lsiBPerc As New ListViewItem.ListViewSubItem
                Select Case lviRow.SubItems(EVALUATION).Text
                    Case Is >= 90
                        lsiBPerc.Text = 5%
                    Case Is >= 80
                        lsiBPerc.Text = 5%
                    Case Is >= 70
                        lsiBPerc.Text = 5%
                    Case Is >= 60
                        lsiBPerc.Text = 0%
                End Select
                lviRow.SubItems.Add(lsiBPerc)
                Dim lsiBAmnt As New ListViewItem.ListViewSubItem
                Select Case lviRow.SubItems(EVALUATION).Text
                    Case Is >= 90
                        lsiBAmnt.Text = FormatCurrency(CInt(lsiSal.Text) * 0.05, 0)
                    Case Is >= 80
                        lsiBAmnt.Text = FormatCurrency(CInt(lsiSal.Text) * 0.05, 0)
                    Case Is >= 70
                        lsiBAmnt.Text = FormatCurrency(CInt(lsiSal.Text) * 0.05, 0)
                    Case Is >= 60
                        lsiBAmnt.Text = FormatCurrency(0, 0)
                End Select
                lviRow.SubItems.Add(lsiBAmnt)
                Dim lsiTot As New ListViewItem.ListViewSubItem
                Select Case lviRow.SubItems(EVALUATION).Text
                    Case Is >= 90
                        lsiTot.Text = FormatCurrency((CInt(lsiSal.Text) * 0.05) + lsiSal.Text, 0)
                    Case Is >= 80
                        lsiTot.Text = FormatCurrency((CInt(lsiSal.Text) * 0.05) + lsiSal.Text, 0)
                    Case Is >= 70
                        lsiTot.Text = FormatCurrency((CInt(lsiSal.Text) * 0.05) + lsiSal.Text, 0)
                    Case Is >= 60
                        lsiTot.Text = FormatCurrency(CInt(lsiSal.Text), 0)
                End Select
                lviRow.SubItems.Add(lsiTot)
                'now add the completed row to the listview
                lvwTaxData.Items.Add(lviRow)
                UpdateStatistics(lviRow)
            End While
            fileIn.Close()
            fileIn.Dispose()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub UpdateStatistics(aRow As ListViewItem)
        Dim blnFountIt As Boolean
        'first check if the new rows category already exist in the array list
        For Each aCat As CCategory In arrCategories
            'based on job code(below)
            If aCat.CatName = aRow.SubItems(JOB_CODE).Text Then ' we already have it do update it  
                'add the amount of bonuses show fro each in the category
                aCat.TotalValue += CDbl(aRow.SubItems(BONUS_AMT).Text)
                'in that column count how many times that job code appears 
                aCat.TotalCount += 1
                aCat.intPoorRating = aRow.SubItems(EVALUATION).Text > 60
                intPoorRating += 1
                blnFountIt = True
                Exit For
            End If
        Next
        If Not blnFountIt Then ' need to create a new CCat Object
            Dim newCat As New CCategory(aRow.SubItems(JOB_CODE).Text, CDbl(aRow.SubItems(BONUS_AMT).Text))
            arrCategories.Add(newCat)
        End If
        'intTotalEmploy += CDbl(aRow.SubItems().Text)
        'intTotalCount += 1
    End Sub

    Private Sub btnLoad_Click(sender As Object, e As EventArgs) Handles btnLoad.Click
        OpenFile()
    End Sub

    Private Sub munOpen_Click(sender As Object, e As EventArgs) Handles munOpen.Click
        OpenFile()
    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Application.Exit()
    End Sub

    Private Sub mnuStatistics_Click(sender As Object, e As EventArgs) Handles mnuStatistics.Click
        ' when the stats button is clicked this will happen
        'Stats.lstCompanyStats.Items.Clear()
        Stats.lstJobCodeStats.Items.Clear()
        With Stats.lstJobCodeStats
            .Items.Add("Total Amount of Emplyees Total= " & CStr(intTotalEmploy))
            .Items.Add("Total inventory count = " & CStr(intTotalCount))
            For Each aCat As CCategory In arrCategories
                .Items.Add(aCat.CatName & ":")
                .Items.Add("  Total Bonuses = " & FormatCurrency(aCat.TotalValue))
                .Items.Add("  Total Employees = " & CStr(aCat.TotalCount))
                .Items.Add("  Number Of Employees With Bonnus = " & CStr(aCat.intPoorRating))
            Next
        End With
        Stats.ShowDialog()
    End Sub

    Private Sub frmMain_Load(sender As Object, e As EventArgs) Handles Me.Load
        arrCategories = New ArrayList
        Stats = New frmStats
    End Sub
End Class
