﻿Imports System.IO
Imports System.IO.Ports
Imports System.Threading
Imports System.Xml
Imports System.Text.RegularExpressions

Imports System.Data.SqlClient
Imports System.Globalization
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraEditors.Repository
Imports DevExpress.XtraEditors

Public Class frmHemaOrdered

    Public MedTechID As String = ""
    Public PathologistID As String = ""
    Public VerifyID As String = ""

    Public mainID As String
    Public MainSampleID As String = ""
    Public FinalMainID As String = ""

    Public PatientID As String = ""
    Public RDate As Date

    Public Channel As String = ""
    Public FLAG As String = ""
    Public RANGE As String = ""

    Public Section As String = ""
    Public SubSection As String = ""

    Public DefaultUnit As Integer = 0
    Public DefaultDiffCount As Integer = 0
    Public Validation As String = ""

    Dim Diff As Double

    Private Sub txtResult_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If InStr("1234567890.", e.KeyChar) = 0 And Not Chr(AscW(e.KeyChar)) = vbBack Then
            e.KeyChar = ChrW(0)
        End If
    End Sub

    Private Sub frmResultNewChem_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Me.Dispose()
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.ItemClick
        Me.Close()
    End Sub

    Private Sub txtAge_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtAge.KeyPress
        If InStr("1234567890", e.KeyChar) = 0 And Not Chr(AscW(e.KeyChar)) = vbBack Then
            e.KeyChar = ChrW(0)
        End If
    End Sub

    Private Sub cboMedTech_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboMedTech.SelectedIndexChanged
        Connect()
        rs.Connection = conn
        rs.CommandType = CommandType.Text
        rs.CommandText = "SELECT * FROM `viewMedTEch` WHERE `name` LIKE '" & Me.cboMedTech.Text & "'"
        reader = rs.ExecuteReader
        reader.Read()
        If reader.HasRows Then
            MedTechID = reader(0).ToString
        End If
        Disconnect()
    End Sub

    Private Sub cboPathologist_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboPathologist.SelectedIndexChanged
        Connect()
        rs.Connection = conn
        rs.CommandType = CommandType.Text
        rs.CommandText = "SELECT * FROM `viewPathologist` WHERE `name` LIKE '" & Me.cboPathologist.Text & "'"
        reader = rs.ExecuteReader
        reader.Read()
        If reader.HasRows Then
            PathologistID = reader(0).ToString
        End If
        Disconnect()
    End Sub

    Private Sub btnPreview_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPreview.Click
        frmHemaPrevious.patientID = txtPatientID.Text
        frmHemaPrevious.section = Section
        frmHemaPrevious.SubSection = SubSection
        frmHemaPrevious.ShowDialog()

        ActivityLogs(txtSampleID.Text,
                             txtPatientID.Text,
                              txtName.Text,
                             CurrUser,
                             "Opened Delta Check Form Archive",
                             cboRequest.Text,
                             Section,
                             SubSection)

    End Sub

    Private Sub cboVerify_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboVerify.SelectedIndexChanged
        Connect()
        rs.Connection = conn
        rs.CommandType = CommandType.Text
        rs.CommandText = "SELECT * FROM `viewMedTEch` WHERE `name` LIKE '" & Me.cboVerify.Text & "'"
        reader = rs.ExecuteReader
        reader.Read()
        If reader.HasRows Then
            VerifyID = reader(0).ToString
        End If
        Disconnect()
    End Sub

    Public Sub LoadTest()
        'On Error Resume Next
        Try
            GridView.Columns.Clear()
            GridView.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
            GridView.Appearance.HeaderPanel.FontStyleDelta = FontStyle.Bold

            'GridView.Appearance.OddRow.BackColor = Color.Gainsboro
            'GridView.OptionsView.EnableAppearanceOddRow = True
            'GridView.Appearance.EvenRow.BackColor = Color.White
            'GridView.OptionsView.EnableAppearanceEvenRow = True

            Dim SQL As String = "SELECT `universal_id` AS TestName, `flag` AS Flag, `measurement` AS Result, `reference_range` as ReferenceRange, `units` as Unit,
                `value_conv` AS Conventional, `unit_conv` AS Units, 
                `ref_conv` AS RefRange,  `instrument` AS Instrument, `test_code` AS TestCode, `id` AS ID, 
                `test_group` AS TestGroup, `his_code` AS HISTestCode, `his_mainid` AS HISMainID, `print_status` AS PrintStatus, `accepted_result` AS AcceptedResult FROM `result`
                WHERE `sample_id` = @MainID AND `section` = @Section AND `accepted_result` = 1 ORDER BY `order_no`"

            Dim command As New MySql.Data.MySqlClient.MySqlCommand(SQL, conn)

            command.Parameters.Clear()
            command.Parameters.AddWithValue("@MainID", mainID)
            command.Parameters.AddWithValue("@Section", Section)
            command.Parameters.AddWithValue("@SubSection", SubSection)

            Dim adapter As New MySql.Data.MySqlClient.MySqlDataAdapter(command)

            Dim myTable As DataTable = New DataTable
            adapter.Fill(myTable)

            dtResult.DataSource = myTable

            GridView.Columns("TestCode").Visible = False
            GridView.Columns("ID").Visible = False
            GridView.Columns("HISTestCode").Visible = False
            GridView.Columns("HISMainID").Visible = False
            GridView.Columns("TestGroup").Visible = False
            GridView.Columns("PrintStatus").Visible = False
            GridView.Columns("AcceptedResult").Visible = False
            GridView.Columns("Conventional").Visible = False
            GridView.Columns("Units").Visible = False
            GridView.Columns("RefRange").Visible = False
            'GridView.Columns("Conventional").Visible = False
            'GridView.Columns("Units").Visible = False
            'GridView.Columns("RefRange").Visible = False
            'Version 1.6.0.0-beta
            'Not allow edit on Grid View Columns to prevent it to display on Results Form or cause of error
            GridView.Columns("TestName").OptionsColumn.AllowEdit = False
            GridView.Columns("Flag").OptionsColumn.AllowEdit = False
            GridView.Columns("Unit").OptionsColumn.AllowEdit = False
            GridView.Columns("ReferenceRange").OptionsColumn.AllowEdit = False
            GridView.Columns("Result").OptionsColumn.AllowEdit = False
            GridView.Columns("Conventional").OptionsColumn.AllowEdit = False
            GridView.Columns("Units").OptionsColumn.AllowEdit = False
            GridView.Columns("RefRange").OptionsColumn.AllowEdit = False
            GridView.Columns("Instrument").OptionsColumn.AllowEdit = False

            ' Make the grid read-only. 
            'GridView.OptionsBehavior.Editable = False
            ' Prevent the focused cell from being highlighted. 
            GridView.OptionsSelection.EnableAppearanceFocusedCell = False
            ' Draw a dotted focus rectangle around the entire row. 
            GridView.FocusRectStyle = DrawFocusRectStyle.RowFullFocus

            For x As Integer = 0 To GridView.RowCount - 1 Step 1
                If GridView.GetRowCellValue(x, GridView.Columns("PrintStatus")) Then
                    GridView.SelectRow(x)
                Else

                End If
                'turn no value to 0 no save in db only view
                'If Not IsNumeric(GridView.GetRowCellValue(x, "Result")) Then
                '    GridView.SetRowCellValue(x, "Result", 0)
                'End If
            Next

            'GridView.Columns("TestGroup").Group()
            'GridView.Columns("TestGroup").Caption = " "

            LoadRangeAndValues()
            CreateDropdown()
            FillDropdown()

            DiffCount()
        Finally
            If conn.State = ConnectionState.Open Then
                conn.Close()
            End If
        End Try

    End Sub

    Private test As New Hashtable()

    Private Sub FillDropdown()
        On Error Resume Next
        Dim Btype() As String = {
            ControlChars.Quote & "A" & ControlChars.Quote & "Rh POSITIVE",
            ControlChars.Quote & "B" & ControlChars.Quote & "Rh POSITIVE",
            ControlChars.Quote & "AB" & ControlChars.Quote & "Rh POSITIVE",
            ControlChars.Quote & "O" & ControlChars.Quote & "Rh POSITIVE",
            ControlChars.Quote & "A" & ControlChars.Quote & "Rh NEGATIVE",
            ControlChars.Quote & "B" & ControlChars.Quote & "Rh NEGATIVE",
            ControlChars.Quote & "AB" & ControlChars.Quote & "Rh NEGATIVE",
            ControlChars.Quote & "O" & ControlChars.Quote & "Rh NEGATIVE"
            }
        test.Add("BType", Btype)
    End Sub

    Private Sub GridView_ShownEditor(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridView.ShownEditor
        On Error Resume Next
        Dim view As GridView = TryCast(sender, GridView)
        If view.FocusedColumn.FieldName = "Result" Then
            Dim edit As ComboBoxEdit = CType(view.ActiveEditor, ComboBoxEdit)
            edit.Properties.Items.Clear()

            Dim parameter As Object = view.GetRowCellValue(view.FocusedRowHandle, "TestCode")
            If test(parameter) IsNot Nothing Then
                edit.Properties.Items.AddRange(CType(test(parameter), ICollection))
            End If
        End If
    End Sub

    Private Sub CreateDropdown()
        On Error Resume Next
        dtResult.RepositoryItems.Clear()
        Dim _riEditor As New RepositoryItemComboBox()
        dtResult.RepositoryItems.Add(_riEditor)
        GridView.Columns("Result").ColumnEdit = _riEditor
    End Sub

    Private Sub DiffCount()
        Dim TotalDIFCount As Decimal = 0
        Dim RowValue As Double = 0

        For x As Integer = 0 To Me.GridView.RowCount - 1 Step 1
            If GridView.IsRowSelected(x) Then
                If GridView.GetRowCellValue(x, GridView.Columns("TestGroup")) = "Differential Count" Then
                    If IsNumeric(GridView.GetRowCellValue(x, "Result")) Then
                        TotalDIFCount += GridView.GetRowCellValue(x, "Result")
                    Else
                        GridView.SetRowCellValue(x, "Result", "")
                    End If
                End If
            End If
            '#################################################----TOTAL DIF COUNT----################################################################################

            lblDiffCount.Text = "Total DIF Count: " & TotalDIFCount
            '#################################################----TOTAL DIF COUNT----################################################################################
        Next

        If TotalDIFCount.ToString = 1 Or TotalDIFCount = 100 Then
            lblDiffCount.BackColor = Color.DeepSkyBlue
        Else
            lblDiffCount.BackColor = Color.Crimson
        End If
    End Sub

    Public Sub LoadRangeAndValues()
        Try
            Dim RANGE As String = ""
            Dim RANGE_CONVENTIONAL As String = ""
            Dim FLAG As String = ""
            Dim ConvertionFactor, ConvertionMultiplier, DIffCount As Double

            For x As Integer = 0 To Me.GridView.RowCount - 1 Step 1
                rs.Parameters.Clear()
                rs.Parameters.AddWithValue("@patient_id", txtPatientID.Text)
                rs.Parameters.AddWithValue("@TestCode", GridView.GetRowCellValue(x, GridView.Columns("TestCode")))
                rs.Parameters.AddWithValue("@classification", txtClass.Text)
                rs.Parameters.AddWithValue("@gender", cboSex.Text)
                rs.Parameters.AddWithValue("@age", txtAge.Text)
                rs.Parameters.AddWithValue("@main_id", mainID)
                rs.Parameters.AddWithValue("@instrument", GridView.GetRowCellValue(x, GridView.Columns("Instrument")))
                rs.Parameters.AddWithValue("@section", Section)

                '#################################################----REFERENCE RANGE----################################################################################
                Connect()
                rs.Connection = conn
                rs.CommandText = "SELECT `si_range`, `conv_range`, `low_value`, `high_value`,`age_begin`,`age_end` FROM `reference_range` 
                    WHERE `test_code` = @TestCode AND `classification` = @classification AND `gender` = @gender AND `machine` = @instrument  AND @age between `age_begin` AND `age_end`"
                reader = rs.ExecuteReader
                reader.Read()
                If reader.HasRows Then
                    RANGE = reader("si_range").ToString
                    RANGE_CONVENTIONAL = reader("conv_range").ToString
                    If Not GridView.GetRowCellValue(x, GridView.Columns("Result")) = "" Then
                        If IsNumeric(GridView.GetRowCellValue(x, GridView.Columns("Result"))) Then
                            If CDbl(GridView.GetRowCellValue(x, GridView.Columns("Result"))) < Val(reader(2).ToString) Then
                                FLAG = "L"
                            ElseIf CDbl(GridView.GetRowCellValue(x, GridView.Columns("Result"))) > Val(reader(3).ToString) Then
                                FLAG = "H"
                            Else
                                FLAG = ""
                            End If
                        Else
                            FLAG = ""
                        End If
                        Disconnect()
                        GridView.SetRowCellValue(x, GridView.Columns("ReferenceRange"), RANGE)
                        GridView.SetRowCellValue(x, GridView.Columns("RefRange"), RANGE_CONVENTIONAL)
                        GridView.SetRowCellValue(x, GridView.Columns("Flag"), FLAG)
                    Else
                        Disconnect()
                        GridView.SetRowCellValue(x, GridView.Columns("ReferenceRange"), RANGE)
                        GridView.SetRowCellValue(x, GridView.Columns("RefRange"), RANGE_CONVENTIONAL)
                    End If
                End If
                Disconnect()
                '#################################################----REFERENCE RANGE----################################################################################

                '#################################################----CONVERTION FACTOR----################################################################################
                Connect()
                rs.Connection = conn
                rs.CommandType = CommandType.Text
                rs.CommandText = "SELECT `convertion_factor`, `convertion_multiplier` FROM `conversion_factor` WHERE `test_code` = @TestCode"
                reader = rs.ExecuteReader
                reader.Read()
                If reader.HasRows Then

                    If Not GridView.GetRowCellValue(x, GridView.Columns("Result")) = "" Then
                        If IsNumeric(GridView.GetRowCellValue(x, GridView.Columns("Result"))) Then
                            ConvertionFactor = reader(0).ToString
                            ConvertionMultiplier = reader(1).ToString
                            Disconnect()
                            GridView.SetRowCellValue(x, GridView.Columns("Conventional"), FormatNumber(Val(GridView.GetRowCellValue(x, GridView.Columns("Result"))) / ConvertionFactor, ConvertionMultiplier))
                        Else
                            Disconnect()
                            GridView.SetRowCellValue(x, GridView.Columns("Conventional"), GridView.GetRowCellValue(x, GridView.Columns("Result")))
                        End If
                    Else
                        Disconnect()
                        ConvertionFactor = 0
                    End If

                Else
                    Disconnect()
                    ConvertionFactor = 0
                End If
                Disconnect()
                '#################################################----CONVERTION FACTOR----################################################################################
            Next
        Catch ex As Exception
            Disconnect()
            MessageBox.Show(ex.Message, "Mysql Exception", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub

    Private Sub frmResultsNew_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        LoadMedTechPatho()
        AutoLoadDoctor()
        AutoLoadRoom()

        LoadTest()

        'tmTimeReceived.Enabled = True



        If My.Settings.HL7Write = True Then
            btnResend.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
        Else
            btnResend.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
        End If

        If CurrDept = "WARD" Then
            'If CurrType = "Doctor" Then
            btnResend.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
            btnRetrive.Visible = False
            btnAddTest.Visible = False
        Else
            btnResend.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
            btnRetrive.Visible = True
            btnAddTest.Visible = True
        End If

        tmTimeReleased.Enabled = False
        'LoadSignatories()
        DisablePermission()
    End Sub

    Public Sub DisablePermission()

        Connect()
        rs.Connection = conn
        rs.CommandType = CommandType.Text
        rs.CommandText = "SELECT * FROM `user_permission` WHERE `user_id` = '" & CurrEmail & "'"
        reader = rs.ExecuteReader
        While reader.Read()

            If reader(2).ToString = "Preview" Then
                If reader(3).ToString = 0 Then
                    Me.btnViewPrint.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                ElseIf reader(3).ToString = 1 Then
                    Me.btnViewPrint.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                End If
            End If

            If reader(2).ToString = "Edit" Then
                If reader(3).ToString = 0 Then
                    Me.btnEdit.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                ElseIf reader(3).ToString = 1 Then
                    Me.btnEdit.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                End If
            End If

            If reader(2).ToString = "Save" Then
                If reader(3).ToString = 0 Then
                    Me.btnValidate.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                ElseIf reader(3).ToString = 1 Then
                    Me.btnValidate.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                End If
            End If

            If reader(2).ToString = "Print" Then
                If reader(3).ToString = 0 Then
                    Me.btnPrintNow.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                ElseIf reader(3).ToString = 1 Then
                    Me.btnPrintNow.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                End If
            End If
        End While
        Disconnect()

    End Sub

    Private Sub LoadSignatories()
        '###########################---Load Pathologist---################################################################
        Connect()
        rs.Connection = conn
        rs.CommandText = "SELECT CONCAT(fname, ' ', mname, ' ', lname, ', ', designation) AS `name` FROM `pathologist` ORDER BY `name`"
        reader = rs.ExecuteReader
        While reader.Read
            cboPathologist.Properties.Items.Add(reader(0))
        End While
        Disconnect()
        '######################################----END-----###############################################################

        '###########################---Load Medical Technologist---#######################################################
        If My.Settings.MedTech = 0 Then
            Connect()
            rs.Connection = conn
            rs.CommandType = CommandType.Text
            rs.CommandText = "SELECT CONCAT(fname, ' ', mname, ' ', lname, ', ', designation) AS `name` FROM `medtech` WHERE `verificator` = 0 ORDER BY `name`"
            reader = rs.ExecuteReader
            While reader.Read
                cboMedTech.Properties.Items.Add(reader(0))
            End While
            Disconnect()
        ElseIf My.Settings.MedTech = 1 Then
            Connect()
            rs.Connection = conn
            rs.CommandType = CommandType.Text
            rs.CommandText = "SELECT CONCAT(fname, ' ', mname, ' ', lname, ', ', designation) AS `name` FROM `medtech` WHERE `id` = @CurrUser ORDER BY `name`"
            reader = rs.ExecuteReader
            While reader.Read
                cboMedTech.Properties.Items.Add(reader(0))
            End While
            Disconnect()

            'cboMedTech.SelectedIndex = 0
        End If
        '######################################----END-----###############################################################

        '###########################---Load Med Tech for Verification---##################################################
        Connect()
        rs.Connection = conn
        rs.CommandType = CommandType.Text
        rs.CommandText = "SELECT CONCAT(fname, ' ', mname, ' ', lname, ', ', designation) AS `name` FROM `medtech` WHERE `verificator` = 1 ORDER BY `name`"
        reader = rs.ExecuteReader
        While reader.Read
            cboVerify.Properties.Items.Add(reader(0))
        End While
        Disconnect()
        '######################################----END-----###############################################################

        'cboPathologist.SelectedIndex = 0
    End Sub

    Private Sub GridView_CellValueChanged(sender As Object, e As CellValueChangedEventArgs) Handles GridView.CellValueChanged
        Try
            If e.Column.FieldName = "Result" Then
                'If frmHemaWorklist.lvList.FocusedItem.SubItems(1).Text = "Printed" Then

                'Else
                LoadRangeAndValues()
                'End If
            Else
                DiffCount()
            End If
        Catch
        End Try
    End Sub

    Private Sub btnAddTest_ItemClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnAddTest.Click
        frmHemaAddTest.mainID = mainID
        frmHemaAddTest.patientID = txtPatientID.Text
        frmHemaAddTest.TypeResult = "Old"

        frmHemaAddTest.ShowDialog()

        ActivityLogs(txtSampleID.Text,
                             txtPatientID.Text,
                              txtName.Text,
                             CurrUser,
                             "Opened Add Test Form Archive",
                             cboRequest.Text,
                             Section,
                             SubSection)

    End Sub

    Private Sub txtAge_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboSex.SelectedIndexChanged, cboSex.TextChanged, txtAge.TextChanged, txtClass.SelectedIndexChanged
        'LoadRangeAndValues()\
        LoadRangeAndValues()
    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        frmHemaPatientList.Type = "Old"
        frmHemaPatientList.ShowDialog()
    End Sub

    Private Sub btnDelete_ItemClick(ByVal sender As Object, ByVal e As EventArgs)
        'Try
        '    rs.Parameters.Clear()
        '    rs.Parameters.AddWithValue("@sample_id", mainID)
        '    Dim rows As DataGridViewRow = dtResult.SelectedRows(0)

        '    rs.Parameters.AddWithValue("@id", rows.Cells(11).Value)

        '    DeleteRecordSQL("DELETE FROM `tmpResult` WHERE sample_id = @sample_id AND `id` = @ID")
        '    LoadTest()
        'Catch ex As Exception
        '    MessageBox.Show("No Records Selected!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        'End Try

    End Sub

    Public Sub AutoLoadDoctor()
        Me.cboPhysician.Properties.Items.Clear()
        Connect()
        rs.Connection = conn
        rs.CommandType = CommandType.Text
        rs.CommandText = "SELECT `name` FROM `requestor` ORDER BY `name`"
        reader = rs.ExecuteReader
        While reader.Read
            Me.cboPhysician.Properties.Items.Add(reader(0))
        End While
        Disconnect()
    End Sub

    Public Sub AutoLoadRoom()
        Me.cboRoom.Properties.Items.Clear()
        Connect()
        rs.Connection = conn
        rs.CommandType = CommandType.Text
        rs.CommandText = "SELECT * FROM `department` ORDER BY `dept_name`"
        reader = rs.ExecuteReader
        While reader.Read
            Me.cboRoom.Properties.Items.Add(reader(1).ToString)
        End While
        Disconnect()
    End Sub

    Private Sub dtBDate_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dtBDate.EditValueChanged
        GetBDate()
    End Sub

    Public Sub GetBDate()
        Try
            If dtBDate.DateTime = Nothing Then
                Exit Sub
            End If

            Dim Birthday As Date = dtBDate.DateTime
            Dim endDate As Date = Date.Now

            Dim timeSpan As TimeSpan = endDate.Subtract(Birthday)
            Dim totalDays As Integer = timeSpan.TotalDays
            Dim totalYears As Integer = Math.Truncate(totalDays / 365)
            Dim totalMonths As Integer = Math.Truncate((totalDays Mod 365) / 30)
            Dim remainingDays As Integer = Math.Truncate((totalDays Mod 365) Mod 30)

            If totalDays <= 61 Then
                txtClass.Text = "Day(s)"
                txtAge.Text = totalDays

            ElseIf totalDays >= 62 And totalDays <= 364 Then
                txtClass.Text = "Month(s)"
                txtAge.Text = totalMonths

            ElseIf totalDays >= 365 Then
                txtClass.Text = "Year(s)"
                txtAge.Text = totalYears
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub

    Private Sub btnValidate_ItemClick(ByVal sender As Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnValidate.ItemClick
        Try
            If Me.txtName.Text = "" Or cboMedTech.Text = "" Or txtAge.Text = "" Or cboSex.Text = "" Then
                MessageBox.Show("Please verify the data carefully.", "Verification Failed", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If

            GridView.Columns("Result").OptionsColumn.AllowEdit = False

            MainSampleID = mainID
            'MainSampleID = ID_Randomizer()

            rs.Parameters.Clear()
            rs.Parameters.AddWithValue("@mainID", mainID)
            rs.Parameters.AddWithValue("@MainSampleID", MainSampleID)

            rs.Parameters.AddWithValue("@medtech", cboMedTech.Text)
            rs.Parameters.AddWithValue("@verify", cboVerify.Text)

            rs.Parameters.AddWithValue("@pathologist", PathologistID)
            rs.Parameters.AddWithValue("@medtechid", MedTechID)
            rs.Parameters.AddWithValue("@verifyid", VerifyID)

            rs.Parameters.AddWithValue("@remarks", txtRemarks.Text)
            rs.Parameters.AddWithValue("@lab_comment", txtComment.Text)

            rs.Parameters.AddWithValue("@status", "Printed")

            rs.Parameters.AddWithValue("@id", SampleID)
            rs.Parameters.AddWithValue("@sample_id", txtSampleID.Text)
            rs.Parameters.AddWithValue("@patient_id", txtPatientID.Text)
            rs.Parameters.AddWithValue("@name", txtName.Text)
            rs.Parameters.AddWithValue("@age", txtAge.Text)
            rs.Parameters.AddWithValue("@type", txtClass.Text)
            rs.Parameters.AddWithValue("@bdate", dtBDate.Text)
            rs.Parameters.AddWithValue("@physician", cboPhysician.Text)
            rs.Parameters.AddWithValue("@room", cboRoom.Text)
            rs.Parameters.AddWithValue("@sex", cboSex.Text)
            rs.Parameters.AddWithValue("@CS", cboCS.Text)
            rs.Parameters.AddWithValue("@address", txtAddress.Text)
            rs.Parameters.AddWithValue("@contact", txtContact.Text)
            rs.Parameters.AddWithValue("@test", cboRequest.Text)
            rs.Parameters.AddWithValue("@patient_type", cboPatientType.Text)
            rs.Parameters.Add("@date", MySql.Data.MySqlClient.MySqlDbType.Date).Value = Format(dtReceived.Value, "yyyy-MM-dd")
            rs.Parameters.AddWithValue("@time", tmTimeReceived.Text)
            'rs.Parameters.AddWithValue("@date_release", Format(Now, "yyyy-MM-dd") & " " & Now.ToLongTimeString)
            rs.Parameters.AddWithValue("@accession_no", txtAccession.Text)
            rs.Parameters.AddWithValue("@OR_No", txtORNo.Text)
            rs.Parameters.AddWithValue("@CS_No", txtChargeSlip.Text)
            rs.Parameters.AddWithValue("@Section", Section)
            rs.Parameters.AddWithValue("@SubSection", SubSection)

            Connect()
            rs.Connection = conn
            rs.CommandType = CommandType.Text
            rs.CommandText = "SELECT * FROM patient_info WHERE `patient_id` = @PATIENT_ID"
            reader = rs.ExecuteReader
            reader.Read()
            If reader.HasRows Then
                Disconnect()
                SaveRecordwthoutMSG("UPDATE `patient_info` SET " _
                        & "`patient_id` = @patient_id," _
                        & "`name` = @name," _
                        & "`sex` = @sex," _
                        & "`date_of_birth` = @bdate," _
                        & "`age` = @age," _
                        & "`civil_status` = @CS," _
                        & "`address` = @address," _
                        & "`contact_no` = @contact," _
                        & "`sample_id` = @MainSampleID" _
                        & " WHERE `patient_id` = @patient_id"
                        )
            Else
                Disconnect()
                SaveRecordwthoutMSG("INSERT INTO patient_info (patient_id, name, sex, date_of_birth, age, civil_status, address, contact_no, `date`, `sample_id`) VALUES " _
                        & "(" _
                        & "@patient_id," _
                        & "@name," _
                        & "@sex," _
                        & "@bdate," _
                        & "@age," _
                        & "@CS," _
                        & "@address," _
                        & "@contact," _
                        & "@date," _
                        & "@MainSampleID" _
                        & ")"
                        )
            End If
            Disconnect()

            UpdateRecordwthoutMSG("UPDATE `order` SET " _
                    & "`sample_id` = @sample_id," _
                    & "`patient_id` = @patient_id," _
                    & "`patient_name` = @name," _
                    & "`sex` = @sex," _
                    & "`bdate` = @bdate," _
                    & "`age` = @age," _
                    & "`type` = @type," _
                    & "`physician` = @physician," _
                    & "`dept` = @room," _
                    & "`medtech` = @medtech," _
                    & "`verified_by` = @verify," _
                    & "`test` = @test," _
                    & "`patient_type` = @patient_type," _
                    & "`status` = @status," _
                    & "`main_id` = @MainSampleID" _
                    & " WHERE main_id = @mainID AND testtype = @Section AND sub_section = @SubSection"
                    )

            Connect()
            rs.Connection = conn
            rs.CommandType = CommandType.Text
            rs.CommandText = "SELECT * FROM `order_pathologist` WHERE `sample_id` = @mainID"
            reader = rs.ExecuteReader
            reader.Read()
            If reader.HasRows Then
                Disconnect()
                UpdateRecordwthoutMSG("UPDATE `order_pathologist` SET " _
                        & "`pathologist_id` = @pathologist," _
                        & "`sample_id` = @MainSampleID" _
                        & " WHERE `sample_id` = @mainID"
                        )
            Else
                Disconnect()
                SaveRecordwthoutMSG("INSERT INTO `order_pathologist` (`sample_id`, `pathologist_id`) VALUES " _
                        & "(" _
                        & "@MainSampleID," _
                        & "@pathologist" _
                        & ")"
                        )
            End If
            Disconnect()

            If Not cboMedTech.Text = "" Then
                Connect()
                rs.Connection = conn
                rs.CommandType = CommandType.Text
                rs.CommandText = "SELECT * FROM `order_medtech` WHERE `sample_id` = @mainID"
                reader = rs.ExecuteReader
                reader.Read()
                If reader.HasRows Then
                    Disconnect()
                    UpdateRecordwthoutMSG("UPDATE `order_medtech` SET " _
                            & "`medtech_id` = @medtechid," _
                            & "`sample_id` = @MainSampleID" _
                            & " WHERE `sample_id` = @mainID"
                            )
                Else
                    Disconnect()
                    SaveRecordwthoutMSG("INSERT INTO `order_medtech` (`sample_id`, `medtech_id`) VALUES " _
                            & "(" _
                            & "@MainSampleID," _
                            & "@medtechid" _
                            & ")"
                            )
                End If
                Disconnect()
            End If

            If Not cboVerify.Text = "" Then
                Connect()
                rs.Connection = conn
                rs.CommandType = CommandType.Text
                rs.CommandText = "SELECT * FROM `order_Verified` WHERE `sample_id` = @mainID"
                reader = rs.ExecuteReader
                reader.Read()
                If reader.HasRows Then
                    Disconnect()
                    UpdateRecordwthoutMSG("UPDATE `order_Verified` SET " _
                            & "`medtech_id` = @verifyid," _
                            & "`sample_id` = @MainSampleID" _
                            & " WHERE `sample_id` = @mainID"
                            )
                Else
                    Disconnect()
                    SaveRecordwthoutMSG("INSERT INTO `order_Verified` (`sample_id`, `medtech_id`) VALUES " _
                            & "(" _
                            & "@MainSampleID," _
                            & "@verifyid" _
                            & ")"
                            )
                End If
                Disconnect()
            End If

            If Not txtAccession.Text = "" Or txtORNo.Text = "" Or txtChargeSlip.Text = "" Then
                Connect()
                rs.Connection = conn
                rs.CommandType = CommandType.Text
                rs.CommandText = "SELECT * FROM `additional_info` WHERE `sample_id` = @mainID AND section = @Section AND sub_section = @SubSection"
                reader = rs.ExecuteReader
                reader.Read()
                If reader.HasRows Then
                    Disconnect()
                    UpdateRecordwthoutMSG("UPDATE `additional_info` SET " _
                                & "`accession_no` = @accession_no," _
                                & "`or_no` = @OR_No," _
                                & "`cs_no` = @CS_No," _
                                & "`sample_id` = @MainSampleID" _
                                & " WHERE `sample_id` = @mainID AND section = @Section AND sub_section = @SubSection"
                                )
                Else
                    Disconnect()
                    SaveRecordwthoutMSG("INSERT INTO `additional_info` (`accession_no`, `or_no`, `cs_no`, section, sub_section, `sample_id`) VALUES " _
                                & "(" _
                                & "@accession_no," _
                                & "@OR_No," _
                                & "@CS_No," _
                                & "@Section," _
                                & "@SubSection," _
                                & "@MainSampleID" _
                                & ")"
                                )
                End If
                Disconnect()
            End If

            For x As Integer = 0 To GridView.RowCount - 1 Step 1
                If (GridView.IsRowSelected(x)) Then
                    UpdateRecordwthoutMSG("UPDATE `result` SET " _
                          & "`flag` = '" & GridView.GetRowCellValue(x, "Flag") & "'," _
                          & "`measurement` = '" & GridView.GetRowCellValue(x, "Result") & "'," _
                          & "`reference_range` = '" & GridView.GetRowCellValue(x, "ReferenceRange") & "'," _
                          & "`value_conv` = '" & GridView.GetRowCellValue(x, "Conventional") & "'," _
                          & "`ref_conv` = '" & GridView.GetRowCellValue(x, "RefRange") & "'," _
                          & "`patient_id` = @patient_id," _
                          & "`instrument` = '" & GridView.GetRowCellValue(x, "Instrument") & "'," _
                          & "`print_status` = '1'," _
                          & "`sample_id` = @MainSampleID" _
                          & " WHERE `sample_id` = @mainID AND `test_code` = '" & GridView.GetRowCellValue(x, "TestCode") & "' AND `accepted_result` = '" & GridView.GetRowCellValue(x, "AcceptedResult") & "' AND `id` = '" & GridView.GetRowCellValue(x, "ID") & "'"
                          )
                Else
                    UpdateRecordwthoutMSG("UPDATE `result` SET " _
                          & "`flag` = '" & GridView.GetRowCellValue(x, "Flag") & "'," _
                          & "`measurement` = '" & GridView.GetRowCellValue(x, "Result") & "'," _
                          & "`reference_range` = '" & GridView.GetRowCellValue(x, "ReferenceRange") & "'," _
                          & "`value_conv` = '" & GridView.GetRowCellValue(x, "Conventional") & "'," _
                          & "`ref_conv` = '" & GridView.GetRowCellValue(x, "RefRange") & "'," _
                          & "`patient_id` = @patient_id," _
                          & "`instrument` = '" & GridView.GetRowCellValue(x, "Instrument") & "'," _
                          & "`print_status` = '0'," _
                          & "`sample_id` = @MainSampleID" _
                          & " WHERE `sample_id` = @mainID AND `test_code` = '" & GridView.GetRowCellValue(x, "TestCode") & "' AND `accepted_result` = '" & GridView.GetRowCellValue(x, "AcceptedResult") & "' AND `id` = '" & GridView.GetRowCellValue(x, "ID") & "'"
                          )
                End If
            Next

            Connect()
            rs.Connection = conn
            rs.CommandType = CommandType.Text
            rs.CommandText = "SELECT `sample_id` FROM `patient_remarks` WHERE `sample_id` = @mainID"
            reader = rs.ExecuteReader
            reader.Read()
            If reader.HasRows Then
                Disconnect()
                UpdateRecordwthoutMSG("UPDATE `patient_remarks` SET `remarks` = @remarks, `diagnosis` = @lab_comment WHERE `sample_id` = @mainID AND `section` = @Section AND `sub_section` = @SubSection")
            Else
                Disconnect()
                SaveRecordwthoutMSG("INSERT INTO `patient_remarks` (`remarks`, `diagnosis`, `sample_id`, `section`, `sub_section`) VALUES (@remarks, @lab_comment, @MainSampleID, @Section, @SubSection)")
            End If
            Disconnect()

            'gcAdditional.Enabled = True
            gcPatient.Enabled = False
            gcRemarks.Enabled = False
            gcSignature.Enabled = False
            'gcTest.Enabled = False

            txtRemarks.Enabled = False
            txtComment.Enabled = False

            btnValidate.Enabled = False
            btnEdit.Enabled = True
            btnViewPrint.Enabled = True
            btnPrintNow.Enabled = True

            mainID = MainSampleID
            frmHemaWorklist.LoadRecordsCompleted()

            'CreateHL7File()

            ActivityLogs(txtSampleID.Text,
                             txtPatientID.Text,
                              txtName.Text,
                             CurrUser,
                             "Validated Archive",
                             cboRequest.Text,
                             Section,
                             SubSection)

        Catch ex As Exception
            Disconnect()
            MessageBox.Show(ex.Message, "Mysql Exception", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End Try
    End Sub

    Private Sub btnRetrive_Click(sender As Object, e As EventArgs) Handles btnRetrive.Click
        frmHemaRerun.mainID = mainID
        frmHemaRerun.Section = Section
        frmHemaRerun.SubSection = SubSection
        'frmChemRerun.PatientID = PatientID
        'frmChemRerun.RDate = RDate
        frmHemaRerun.ShowDialog()

        ActivityLogs(txtSampleID.Text,
                             txtPatientID.Text,
                              txtName.Text,
                             CurrUser,
                             "Opened Re-run Form Archive",
                             cboRequest.Text,
                             Section,
                             SubSection)

    End Sub

    Private Sub btnPrintNow_ItemClick(ByVal sender As Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnPrintNow.ItemClick
        Try
            If Me.txtName.Text = "" Or cboMedTech.Text = "" Or txtAge.Text = "" Or cboSex.Text = "" Then
                MessageBox.Show("Please verify the data carefully.", "Verification Failed", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If

            Using myRDLCPrinter As New RDLCPrinterPrintReleased(mainID, Section, SubSection, "", My.Settings.DefaultPrinter)
                If My.Settings.SaveAsPDF Then
                    Dim byteViewer As Byte() = myRDLCPrinter.LocalReport.Render("PDF")
                    Dim saveFileDialog1 As New SaveFileDialog()
                    saveFileDialog1.Filter = "*PDF files (*.pdf)|*.pdf"
                    saveFileDialog1.FilterIndex = 2
                    saveFileDialog1.RestoreDirectory = True
                    Dim newFile As New FileStream(My.Settings.PDFLocation & txtSampleID.Text & ".pdf", FileMode.Create)
                    newFile.Write(byteViewer, 0, byteViewer.Length)
                    newFile.Close()

                    myRDLCPrinter.Print(1)
                Else
                    myRDLCPrinter.Print(1)
                End If
            End Using

            'UpdateWorkSheet()

            With My.Settings
                If .HL7Write = True Then
                    CreateHL7File()
                End If
            End With

            frmHemaWorklist.LoadRecords()
            frmHemaWorklist.LoadRecordsCompleted()

            ActivityLogs(txtSampleID.Text,
                             txtPatientID.Text,
                              txtName.Text,
                             CurrUser,
                             "Reprint Archive",
                             cboRequest.Text,
                             Section,
                             SubSection)

            Me.Close()
            Me.Dispose()
        Catch ex As Exception
            Disconnect()
            MessageBox.Show(ex.Message, "Mysql Exception", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub

    Private Sub btnViewPrint_ItemClick(ByVal sender As Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnViewPrint.ItemClick
        'For Each rows As DataGridViewRow In GridView.rows
        RPTresults.sample_id = mainID
        PrintPreview(mainID, "order", "result", "1", Section, SubSection, RPTresults, RPTresults.ReportViewer1)
        'Next
    End Sub

    Private Sub GridView_RowCellStyle(sender As Object, e As RowCellStyleEventArgs) Handles GridView.RowCellStyle
        If e.Column.FieldName = "TestName" Then
            e.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default
        Else
            e.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        End If

        If GridView.GetRowCellValue(e.RowHandle, "Flag").ToString = "L" Then
            e.Appearance.BackColor = Color.Yellow
            e.Appearance.BackColor2 = Color.Yellow
            e.Appearance.ForeColor = Color.Black
        ElseIf GridView.GetRowCellValue(e.RowHandle, "Flag").ToString = "H" Then
            e.Appearance.BackColor = Color.Crimson
            e.Appearance.BackColor2 = Color.Crimson
            e.Appearance.ForeColor = Color.White
        End If
    End Sub

    Private Sub btnEdit_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnEdit.ItemClick
        gcAdditional.Enabled = True
        'gcTest.Enabled = True
        gcSignature.Enabled = True
        gcPatient.Enabled = True
        btnValidate.Enabled = True
        btnAddTest.Enabled = True
        txtRemarks.Enabled = True
        txtComment.Enabled = True

        btnEdit.Enabled = False
        btnPreview.Enabled = True
        btnViewPrint.Enabled = False

        GridView.Columns("Result").OptionsColumn.AllowEdit = True

        ActivityLogs(txtSampleID.Text,
                             txtPatientID.Text,
                              txtName.Text,
                             CurrUser,
                             "Opened Edit Form",
                             cboRequest.Text,
                             Section,
                             SubSection)
    End Sub

    Private Sub LoadMedTechPatho()
        Connect()
        rs.Connection = conn
        rs.CommandType = CommandType.Text
        rs.CommandText = "SELECT CONCAT(fname, ' ', mname, ' ', lname, ', ', designation) AS `name` FROM `pathologist` ORDER BY `name`"
        reader = rs.ExecuteReader
        While reader.Read
            cboPathologist.Properties.Items.Add(reader(0))
        End While
        Disconnect()

        Connect()
        rs.Connection = conn
        rs.CommandType = CommandType.Text
        rs.CommandText = "SELECT * FROM `viewpathologist` WHERE `name` = '" & Me.cboPathologist.Text & "'"
        reader = rs.ExecuteReader
        reader.Read()
        If reader.HasRows Then
            PathologistID = reader(0).ToString
        End If
        Disconnect()
        'MessageBox.Show(PathologistID)

        Connect()
        rs.Connection = conn
        rs.CommandType = CommandType.Text
        rs.CommandText = "SELECT CONCAT(fname, ' ', mname, ' ', lname, ', ', designation) AS `name` FROM `medtech` WHERE `verificator` = 1 ORDER BY `name`"
        reader = rs.ExecuteReader
        While reader.Read
            cboVerify.Properties.Items.Add(reader(0))
        End While
        Disconnect()
        'cboVerify.SelectedIndex = cboVerify.ToString

        Connect()
        rs.Connection = conn
        rs.CommandType = CommandType.Text
        rs.CommandText = "SELECT * FROM `viewMedTEch` WHERE `name` = '" & Me.cboVerify.Text & "'"
        reader = rs.ExecuteReader
        reader.Read()
        If reader.HasRows Then
            VerifyID = reader(0).ToString
        End If
        Disconnect()

        If My.Settings.MedTech = 0 Then
            Connect()
            rs.Connection = conn
            rs.CommandType = CommandType.Text
            rs.CommandText = "SELECT CONCAT(fname, ' ', mname, ' ', lname, ', ', designation) AS `name` FROM `medtech` WHERE `verificator` = 0 ORDER BY `name`"
            reader = rs.ExecuteReader
            While reader.Read
                cboMedTech.Properties.Items.Add(reader(0))
            End While
            Disconnect()
            Connect()
            rs.Connection = conn
            rs.CommandType = CommandType.Text
            rs.CommandText = "SELECT * FROM `viewMedTEch` WHERE `name` = '" & Me.cboMedTech.Text & "'"
            reader = rs.ExecuteReader
            reader.Read()
            If reader.HasRows Then
                MedTechID = reader(0).ToString
            End If
            Disconnect()
        ElseIf My.Settings.MedTech = 1 Then
            Connect()
            rs.Connection = conn
            rs.CommandType = CommandType.Text
            rs.CommandText = "SELECT CONCAT(fname, ' ', mname, ' ', lname, ', ', designation) AS `name` FROM `medtech` WHERE `id` = '" & CurrEmail & "' ORDER BY `name`"
            reader = rs.ExecuteReader
            While reader.Read
                cboMedTech.Properties.Items.Add(reader(0))
            End While
            Disconnect()
            Connect()
            rs.Connection = conn
            rs.CommandType = CommandType.Text
            rs.CommandText = "SELECT * FROM `viewMedTEch` WHERE `name` = '" & Me.cboMedTech.Text & "'"
            reader = rs.ExecuteReader
            reader.Read()
            If reader.HasRows Then
                MedTechID = reader(0).ToString
            End If
            Disconnect()

            'cboMedTech.SelectedIndex = 0
        End If
        '######################################----END-----###############################################################

        '###########################---Load Med Tech for Verification---##################################################
        Connect()
        rs.Connection = conn
        rs.CommandType = CommandType.Text
        rs.CommandText = "SELECT CONCAT(fname, ' ', mname, ' ', lname, ', ', designation) AS `name` FROM `medtech` WHERE `verificator` = 1 ORDER BY `name`"
        reader = rs.ExecuteReader
        While reader.Read
            cboVerify.Properties.Items.Add(reader(0))
        End While
        Disconnect()
        '######################################----END-----###############################################################
    End Sub

    '######################################### Shift Function #########################################
    Private Function getShift(ByVal shift As TimeSpan)
        Dim readerVar As String

        'Variable that Determine the Shift Next Day
        Dim thrdshft As New TimeSpan(22, 0, 0) '22:00:00
        Dim thrdshft2 As New TimeSpan(7, 0, 0) '07:00:00

        Connect()
        rs.Connection = conn
        rs.CommandType = CommandType.Text
        rs.CommandText = "SELECT * FROM `shift`"
        reader = rs.ExecuteReader
        While reader.Read
            'If Data in database (reader) is greater than equal or less than equal to variable above
            If reader(2) >= thrdshft And reader(3) <= thrdshft2 Then
                If shift > reader(2) Or shift < reader(3) Then
                    readerVar = reader(1).ToString
                    Disconnect()
                    Return readerVar
                    Exit Function
                ElseIf shift < reader(3) Then
                    readerVar = reader(1).ToString
                    Disconnect()
                    Return readerVar
                    Exit Function

                End If
            ElseIf shift > reader(2) Then
                If shift < reader(3) Then
                    readerVar = reader(1).ToString
                    Disconnect()
                    Return readerVar
                    Exit Function
                End If
            End If
        End While
        Disconnect()
        Return shift
    End Function
    '######################################### End of Shift Function #########################################

    '######################################### Re sending HL7 file to HIS #########################################
    Private Sub btnResend_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnResend.ItemClick
        With My.Settings
            If .SQLConnection = True Then
                UpdateResultiHOMIS()
            End If
        End With

        ActivityLogs(txtSampleID.Text,
                             txtPatientID.Text,
                              txtName.Text,
                             CurrUser,
                             "Resend Result To HIS",
                             cboRequest.Text,
                             Section,
                             SubSection)

        'With My.Settings
        '    If .HL7Write = True Then
        '        CreateHL7File()
        '    End If
        'End With
        'CreateHL7File()

    End Sub
    '##################################### End of Re sending HL7 file to HIS #########################################

#Region "HIS Integration Methods"
    'Method for Creating HL7 File

    Private his_test_code As String
    Public Function getHIS_Test(ByVal accesion_no As String, or_no As String, ByVal cs_no As String)
        Dim additional_info_id As String
        Dim his_test As String
        Dim test_string As String

        rs.Parameters.Clear()
        rs.Parameters.AddWithValue("@accession_no", accesion_no)
        rs.Parameters.AddWithValue("@or_no", or_no)
        rs.Parameters.AddWithValue("@ChargeSlip", cs_no)
        rs.Parameters.AddWithValue("@section", Section)
        rs.Parameters.AddWithValue("@sub_section", SubSection)

        Disconnect()
        Connect()
        rs.Connection = conn
        rs.CommandText = "SELECT id FROM additional_info WHERE accession_no = @accession_no AND or_no = @or_no AND cs_no = @ChargeSlip AND section = @section AND sub_section = @sub_section"
        reader = rs.ExecuteReader
        If reader.HasRows Then
            While reader.Read
                test_string = ""
                additional_info_id = reader(0).ToString
                Connect1()
                rs1.Connection = conn1
                rs1.CommandText = "SELECT his_test_code FROM hl7_result_his_code WHERE additional_info_id = '" & additional_info_id & "'"
                reader1 = rs1.ExecuteReader
                If reader1.HasRows Then
                    While reader1.Read
                        his_test = reader1(0).ToString + "^~"
                        test_string &= his_test.TrimEnd
                    End While
                End If
                Disconnect1()
            End While
        End If
        Disconnect()

        Return test_string
    End Function

    Public Sub CreateHL7File()
        'Check If Date of Birth Is nothing then clear text formating
        If dtBDate.Text = "" Then
            'Clear text formating
            PatientBDate_Out = ""
            '--------------------
        Else
            'Create a format for date of birth (yyyyMMdd)
            Dim dDate As DateTime = DateTime.ParseExact(dtBDate.Text, "MM/dd/yyyy", CultureInfo.InvariantCulture)
            Dim reformatted As String = dDate.ToString("yyyyMMdd", CultureInfo.InvariantCulture)
            PatientBDate_Out = reformatted
            '--------------------------------------------
        End If
        '-----------------------------------------------------------
        Dim a As Integer = 0

        Dim HL7Log As FileStream
        Dim HL7Writer As StreamWriter
        Dim HL7File As String = "SBSILIS_" & txtSampleID.Text.Trim & ".HL7"

        Dim pType As String

        If cboPatientType.Text.Equals("") Then
            pType = ""
        Else
            pType = cboPatientType.Text.Substring(0, 1)
        End If

        HL7Log = New FileStream(My.Settings.HL7Destination & "\" & HL7File, FileMode.Create, FileAccess.Write)
        HL7Writer = New StreamWriter(HL7Log)

        his_test_code = getHIS_Test(txtAccession.Text, txtORNo.Text, txtChargeSlip.Text)

        HL7Writer.WriteLine("MSH|^~\&|BIZBOX|HOSPITAL|BIOTECH|HOSPITAL|" & FormatDateRegex() & "||ORU^RO1|1|P|2.3|||")
        HL7Writer.WriteLine("PID|1||" & txtPatientID.Text & "||" & Replace(txtName.Text, ", ", "^") & "||" & PatientBDate_Out & "|" & cboSex.Text.Substring(0, 1))
        HL7Writer.WriteLine("PV1|1|" & pType)
        HL7Writer.WriteLine("ORC|1|" & txtAccession.Text & "|||||||" & FormatDateRegex())
        'HL7Writer.WriteLine("OBR|1|" & txtAccession.Text & "||^|||" & FormatDateRegex())

        For x As Integer = 0 To his_test_code.Split("~").Count - 1 Step 1
            HL7Writer.WriteLine("OBR|" & x + 1 & "|" & txtAccession.Text & "||" & his_test_code.Split("~").GetValue(x).ToString & "|||" & FormatDateRegex())
        Next

        For x As Integer = 0 To GridView.RowCount - 1 Step 1
            'HL7Writer.WriteLine("OBX|" & x + 1 & "|NM|" & GridView.GetRowCellValue(x, "TestCode") & "||" & GridView.GetRowCellValue(x, "SI") & "|" & GridView.GetRowCellValue(x, "Unit") & "|" & GridView.GetRowCellValue(x, "ReferenceRange") & "|N|||F|||" & FormatDateRegex() & "|")
            HL7Writer.WriteLine("OBX|" & x + 1 & "|NM|" & GridView.GetRowCellValue(x, "TestCode") & "||" & GridView.GetRowCellValue(x, "Conventional") & "|||N|||F|||" & FormatDateRegex() & "|")
            a = x + 1
        Next
        'Remarks Section
        HL7Writer.WriteLine("OBX|" & a + 1 & "|NM|Remarks_H||" & txtRemarks.Text & "|||N|||F|||" & FormatDateRegex() & "|")
        HL7Writer.WriteLine("OBX|" & a + 2 & "|NM|Others_H||" & txtComment.Text & "|||N|||F|||" & FormatDateRegex() & "|")
        HL7Writer.Close()
        HL7Log.Close()
    End Sub
    ' End Of HL7

    'Function for update result for iHOMIS
    Public Sub UpdateResultiHOMIS()
        'Try
        If Not GridView.GetRowCellValue(0, GridView.Columns("HISMainID")).ToString = "" Then
            If GridView.RowCount = 1 Then
                ConnectSQL()
                rsSQL.Parameters.Clear()
                rsSQL.Parameters.AddWithValue("@docointkey", GridView.GetRowCellValue(0, GridView.Columns("HISMainID")))
                rsSQL.Connection = connSQL
                rsSQL.CommandType = CommandType.Text
                rsSQL.CommandText = "UPDATE hbldrslt SET " & GridView.GetRowCellValue(0, GridView.Columns("HISTestCode")) & " = '" & GridView.GetRowCellValue(0, GridView.Columns("Result")) & "'" _
                            & " WHERE docointkey = @docointkey"
                rsSQL.ExecuteNonQuery()
                DisconnectSQL()
            Else
                For x As Integer = 0 To GridView.RowCount - 1 Step 1

                    ConnectSQL()
                    rsSQL.Parameters.Clear()
                    rsSQL.Parameters.AddWithValue("@docointkey", GridView.GetRowCellValue(x, GridView.Columns("HISMainID")))
                    rsSQL.Connection = connSQL
                    rsSQL.CommandType = CommandType.Text
                    rsSQL.CommandText = "UPDATE hbldrslt SET " & GridView.GetRowCellValue(x, GridView.Columns("HISTestCode")) & " = '" & GridView.GetRowCellValue(x, GridView.Columns("Result")) & "'" _
                                & " WHERE docointkey = @docointkey"
                    rsSQL.ExecuteNonQuery()
                    DisconnectSQL()
                Next
            End If
        Else
            Exit Sub
        End If
        'Catch ex As Exception
        '    MessageBox.Show(ex.Message)
        '    Exit Sub
        'End Try
    End Sub

    Private Sub GridView_SelectionChanged(sender As Object, e As DevExpress.Data.SelectionChangedEventArgs) Handles GridView.SelectionChanged
        DiffCount()
    End Sub
    'End iHOMIS
#End Region

End Class