Imports System.Drawing.Printing
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraPrinting.BarCode
Imports MySql.Data.MySqlClient

Public Class frmFrontDesk

    Dim LastID As Integer
    Dim SampleID As String
    Public getName As String = ""
    Public mainID As String = ""
    Dim arrImage() As Byte
    Dim Mode As String
    Dim BDate As String
    Dim Age As String

    Dim Section As String
    Dim SubSection As String

    Private PrintDocType As String = "Barcode"
    'Private StrPrinterName As String = "EPSON L220 Series"
    Private StrPrinterName As String = My.Settings.BCPrinterName

    Public Sub LoadRecords()
        Try

            If txtSearch.Text = Nothing Then

                GridView.Columns.Clear()
                GridView.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                GridView.Appearance.HeaderPanel.FontStyleDelta = FontStyle.Bold
                'storedproc frontdesk W worklist O order
                Connect()
                rs.Parameters.Clear()
                rs.Parameters.AddWithValue("@DateFromW", Format(CDate(dtFrom1.Value.ToShortDateString), "yyyy-MM-dd"))
                rs.Parameters.AddWithValue("@DateToW", Format(CDate(dtTo1.Value.ToShortDateString), "yyyy-MM-dd"))
                rs.Parameters.AddWithValue("@DateFromO", Format(CDate(dtFrom1.Value.ToShortDateString), "yyyy-MM-dd"))
                rs.Parameters.AddWithValue("@DateToO", Format(CDate(dtTo1.Value.ToShortDateString), "yyyy-MM-dd"))
                Dim sql As String
                sql = "frontdesk"

                Dim dt As DataTable = New DataTable
                rs.Connection = conn
                rs.CommandType = CommandType.StoredProcedure
                rs.CommandText = sql
                Dim adapter As New MySqlDataAdapter(rs)

                adapter.Fill(dt)
                dtList.DataSource = (dt)

            Else
                GridView.Columns.Clear()
                GridView.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                GridView.Appearance.HeaderPanel.FontStyleDelta = FontStyle.Bold

                Dim SQL As String = "SELECT * FROM (SELECT
			                        `tmpworklist`.`id` AS SequenceNo,
			                        `tmpworklist`.`status` AS `Status`,
			                        `tmpworklist`.`sample_id` AS SampleID,
			                        `tmpworklist`.`patient_id` AS PatientID,
									`additional_info`.`cs_no` AS ChargeSlip,
			                        `tmpworklist`.`patient_name` AS PatientName, 
			                        `tmpworklist`.`test` AS Request,
			                        `tmpworklist`.`dept` AS RoomWard,
			                        `tmpworklist`.`physician` AS Physician,
			                        `tmpworklist`.`medtech` AS PerformedBy,
			                        `tmpworklist`.`verified_by` AS ReleasedBy,
			                        DATE_FORMAT(`specimen_tracking`.`received`, '%m/%d/%Y') AS DateRequested,
			                        `tmpworklist`.`time` AS TimeRequested,
			                        `tmpworklist`.`testtype` AS Section,
			                        `tmpworklist`.`sub_section` AS SubSection,
			                        DATE_FORMAT(`specimen_tracking`.`processing`, '%m/%d/%Y %r') AS DateReceived, 
			                        DATE_FORMAT(`specimen_tracking`.`printed`, '%m/%d/%Y %r') AS DateReleased,
			                        `tmpworklist`.`main_id` AS RefID,
			                        `tmpworklist`.`patient_type` AS PatientType,
			                        `tmpworklist`.`bdate` AS DateOfBirth,
			                        `tmpworklist`.`sex` AS Sex,
			                        `tmpworklist`.`age` AS Age,
                                    `patient_info`.`address` AS Address,
                                    `patient_info`.`contact_no` AS ContactNumber,
                                    `patient_remarks`.`diagnosis` AS Diagnosis
			                        FROM `tmpworklist`
			                        LEFT JOIN `specimen_tracking` ON `specimen_tracking`.`sample_id` = `tmpworklist`.`main_id`
			                        AND (`tmpworklist`.`testtype` = `specimen_tracking`.`section`)
			                        AND (`tmpworklist`.`sub_section` = `specimen_tracking`.`sub_section`)
									LEFT JOIN `additional_info` ON `additional_info`.`sample_id` = `tmpworklist`.`sample_id`
                                    LEFT JOIN `patient_info` ON `patient_info`.`patient_id` = `tmpworklist`.`patient_id`
                                    LEFT JOIN `patient_remarks` ON `patient_remarks`.`sample_id` = `tmpworklist`.`sample_id`
			                        WHERE 
                                    (`tmpworklist`.`testtype` = `specimen_tracking`.`section`)
                                    AND (`tmpworklist`.`sub_section` = `specimen_tracking`.`sub_section`) 
                                    AND (`tmpworklist`.`testtype` = `additional_info`.`section`)
                                    AND (`tmpworklist`.`sub_section` = `additional_info`.`sub_section`)
                                    AND (`tmpworklist`.`testtype` = `patient_remarks`.`section`)
                                    AND (`tmpworklist`.`sub_section` = `patient_remarks`.`sub_section`)
                                    AND `tmpworklist`.`date` BETWEEN @DateFromW AND @DateToW
	                        UNION ALL
	                        SELECT 
                                    `order`.`id` AS SequenceNo,
			                        `order`.`status` AS `Status`,
			                        `order`.`sample_id` AS SampleID,
			                        `order`.`patient_id` AS PatientID,
                                    `additional_info`.`cs_no` AS ChargeSlip,
			                        `order`.`patient_name` AS PatientName,
			                        `order`.`test` AS Request,
			                        `order`.`dept` AS RoomWard,
			                        `order`.`physician` AS Physician,
			                        `order`.`medtech` AS PerformedBy,
			                        `order`.`verified_by` AS ReleasedBy,
			                        DATE_FORMAT(`specimen_tracking`.`received`, '%m/%d/%Y') AS DateRequested,
			                        `order`.`time` AS TimeRequested,
			                        `order`.`testtype` AS Section,
			                        `order`.`sub_section` AS SubSection,
			                        DATE_FORMAT(`specimen_tracking`.`processing`, '%m/%d/%Y %r') AS DateReceived, 
			                        DATE_FORMAT(`specimen_tracking`.`printed`, '%m/%d/%Y %r') AS DateReleased,
			                        `order`.`main_id` AS RefID,
			                        `order`.`patient_type` AS PatientType,
			                        `order`.`bdate` AS DateOfBirth,
			                        `order`.`sex` AS Sex,
			                        `order`.`age` AS Age,
                                    `patient_info`.`address` AS Address,
                                    `patient_info`.`contact_no` AS ContactNumber,
                                    `patient_remarks`.`diagnosis` AS Diagnosis
			                        FROM `order`
			                        LEFT JOIN `specimen_tracking` ON `specimen_tracking`.`sample_id` = `order`.`main_id`
			                        AND (`order`.`testtype` = `specimen_tracking`.`section`)
			                        AND (`order`.`sub_section` = `specimen_tracking`.`sub_section`) 
                                    LEFT JOIN additional_info ON `additional_info`.`sample_id` = `order`.`sample_id`
                                    LEFT JOIN `patient_info` ON `patient_info`.`patient_id` = `order`.`patient_id`
                                    LEFT JOIN `patient_remarks` ON `patient_remarks`.`sample_id` = `order`.`sample_id`
			                        WHERE 

                                    (`order`.`testtype` = `specimen_tracking`.`section`)
                                    AND (`order`.`sub_section` = `specimen_tracking`.`sub_section`) 
                                    AND (`order`.`testtype` = `additional_info`.`section`)
                                    AND (`order`.`sub_section` = `additional_info`.`sub_section`)
                                    AND (`order`.`testtype` = `patient_remarks`.`section`)
                                    AND (`order`.`sub_section` = `patient_remarks`.`sub_section`)
                                    AND `order`.`date` BETWEEN @DateFromO AND @DateToO) AS result
                                    
                                    WHERE SampleID LIKE '" & txtSearch.Text & "%' OR PatientID LIKE '" & txtSearch.Text & "%' OR PatientName LIKE '" & txtSearch.Text & "%' OR Physician LIKE '" & txtSearch.Text & "%'"

                Dim rs As New MySql.Data.MySqlClient.MySqlCommand(SQL, conn)

                rs.Parameters.Clear()
                rs.Parameters.AddWithValue("@DateFromW", Format(CDate(dtFrom1.Value.ToShortDateString), "yyyy-MM-dd"))
                rs.Parameters.AddWithValue("@DateToW", Format(CDate(dtTo1.Value.ToShortDateString), "yyyy-MM-dd"))
                rs.Parameters.AddWithValue("@DateFromO", Format(CDate(dtFrom1.Value.ToShortDateString), "yyyy-MM-dd"))
                rs.Parameters.AddWithValue("@DateToO", Format(CDate(dtTo1.Value.ToShortDateString), "yyyy-MM-dd"))

                Dim adapter As New MySql.Data.MySqlClient.MySqlDataAdapter(rs)

                Dim myTable As DataTable = New DataTable
                adapter.Fill(myTable)

                dtList.DataSource = myTable

            End If

            GridView.Columns("RefID").Visible = False
            GridView.Columns("Section").Visible = False
            GridView.Columns("SubSection").Visible = False
            GridView.Columns("PatientType").Visible = False
            GridView.Columns("SequenceNo").Visible = False
            GridView.Columns("Age").Visible = False
            GridView.Columns("ContactNumber").Visible = False
            GridView.Columns("Diagnosis").Visible = False

            ' Make the grid read-only. 
            GridView.OptionsBehavior.Editable = False
            ' Prevent the focused cell from being highlighted. 
            GridView.OptionsSelection.EnableAppearanceFocusedCell = False
            ' Draw a dotted focus rectangle around the entire row. 
            GridView.FocusRectStyle = DrawFocusRectStyle.RowFullFocus

            GridView.OptionsSelection.MultiSelect = False
            GridView.OptionsSelection.MultiSelectMode = GridMultiSelectMode.CheckBoxRowSelect

            GridView.OptionsView.ColumnAutoWidth = False

            GridView.Columns("ChargeSlip").Width = 100
            GridView.Columns("SequenceNo").Width = 50
            GridView.Columns("PatientName").Width = 200
            GridView.Columns("Physician").Width = 200
            GridView.Columns("Request").Width = 200
            GridView.Columns("RoomWard").Width = 260
            GridView.Columns("Sex").Width = 60
            GridView.Columns("DateRequested").Width = 90
            GridView.Columns("Status").Width = 100
            GridView.Columns("TimeRequested").Width = 90
            GridView.Columns("DateReceived").Width = 135
            GridView.Columns("DateReleased").Width = 135
            GridView.Columns("PerformedBy").Width = 200
            GridView.Columns("ReleasedBy").Width = 200

            Disconnect()

            DisableAddPatient()

            HisDiasable()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub GridView_RowCellStyle(sender As Object, e As RowCellStyleEventArgs) Handles GridView.RowCellStyle
        Dim view As GridView = TryCast(sender, GridView)
        If (e.Column.FieldName = "SequenceNo") Or (e.Column.FieldName = "Status") Then
            If view.GetRowCellValue(e.RowHandle, "Status") = "Ordered" Then
                e.Appearance.BackColor = Color.White
                e.Appearance.BackColor2 = Color.White
                e.Appearance.ForeColor = Color.Black
            ElseIf view.GetRowCellValue(e.RowHandle, "Status") = "Warding" Then
                e.Appearance.BackColor = Color.Orange
                e.Appearance.BackColor2 = Color.Orange
                e.Appearance.ForeColor = Color.White
            ElseIf view.GetRowCellValue(e.RowHandle, "Status") = "Rejected" Then
                e.Appearance.BackColor = Color.Crimson
                e.Appearance.BackColor2 = Color.Crimson
                e.Appearance.ForeColor = Color.White
            ElseIf view.GetRowCellValue(e.RowHandle, "Status") = "Cancelled" Then
                e.Appearance.BackColor = Color.Gray
                e.Appearance.BackColor2 = Color.Gray
                e.Appearance.ForeColor = Color.White
            ElseIf view.GetRowCellValue(e.RowHandle, "Status") = "Checked-In" Then
                e.Appearance.BackColor = Color.CornflowerBlue
                e.Appearance.BackColor2 = Color.CornflowerBlue
                e.Appearance.ForeColor = Color.Black
            ElseIf view.GetRowCellValue(e.RowHandle, "Status") = "Processing" Then
                e.Appearance.BackColor = Color.Gold
                e.Appearance.BackColor2 = Color.Gold
                e.Appearance.ForeColor = Color.Black
            ElseIf view.GetRowCellValue(e.RowHandle, "Status") = "Result Received" Then
                e.Appearance.BackColor = Color.LightGreen
                e.Appearance.BackColor2 = Color.LightGreen
                e.Appearance.ForeColor = Color.Black
            ElseIf view.GetRowCellValue(e.RowHandle, "Status") = "Validated" Then
                e.Appearance.BackColor = Color.Green
                e.Appearance.BackColor2 = Color.Green
                e.Appearance.ForeColor = Color.Black
            ElseIf view.GetRowCellValue(e.RowHandle, "Status") = "Printed" Then
                e.Appearance.BackColor = Color.ForestGreen
                e.Appearance.BackColor2 = Color.ForestGreen
                e.Appearance.ForeColor = Color.White
            Else
                e.Appearance.BackColor = Color.Gray
                e.Appearance.BackColor2 = Color.Gray
                e.Appearance.ForeColor = Color.White
            End If
        End If
    End Sub

    Private Sub MainGridView_MouseMove(sender As Object, e As MouseEventArgs) Handles GridView.MouseMove
        Dim view = TryCast(sender, GridView)
        Dim info = view.CalcHitInfo(e.Location)
        If (info.InRowCell) Then
            view.RefreshRowCell(info.RowHandle, info.Column)
        End If
    End Sub

    Private Sub frmNewOrder_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        LoadRecords()
        DisablePermission()

    End Sub

    Private Sub gridChecker(sender As Object, e As EventArgs) Handles dtList.MouseClick, dtList.KeyUp, dtList.KeyDown
        HisDiasable()
    End Sub

    Public Sub HisDiasable()
        Dim selectedRows() As Integer = GridView.GetSelectedRows()
        For Each rowHandle As Integer In selectedRows
            Dim sid As String = GridView.GetRowCellValue(rowHandle, GridView.Columns("SampleID")).ToString
            If rowHandle >= 0 Then
                Connect()
                rs.Connection = conn
                rs.CommandType = CommandType.Text
                rs.CommandText = "SELECT * FROM `tmpresult` WHERE `sample_id` = '" & sid & "' And `his_mainID` IS NOT NULL"
                reader = rs.ExecuteReader
                reader.Read()

                If reader.HasRows Then
                    btnEdit.Enabled = False
                Else
                    btnEdit.Enabled = True
                End If
            End If
            Disconnect()
        Next rowHandle
        'LoadRecords()
    End Sub

    'permissions
    Public Sub DisablePermission()
        Disconnect()
        Connect()
        rs.Connection = conn
        rs.CommandType = CommandType.Text
        rs.CommandText = "SELECT * FROM `user_permission` WHERE `user_id` = '" & CurrEmail & "'"
        reader = rs.ExecuteReader
        While reader.Read()

            'If reader(2).ToString = "Add" Then
            '    If reader(3).ToString = 0 Then
            '        Me.btnAdd.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
            '        'Me.btnAddPatient.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
            '    ElseIf reader(3).ToString = 1 Then
            '        Me.btnAdd.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
            '        'Me.btnAddPatient.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
            '    End If
            'End If

            If reader(2).ToString = "Reject Order" Then
                If reader(3).ToString = 0 Then
                    Me.btnCancel.Enabled = False
                ElseIf reader(3).ToString = 1 Then
                    Me.btnCancel.Enabled = True
                End If
            End If

            If reader(2).ToString = "Delete" Then
                If reader(3).ToString = 0 Then
                    Me.btnDelete.Enabled = False
                ElseIf reader(3).ToString = 1 Then
                    Me.btnDelete.Enabled = True
                End If
            End If

            If reader(2).ToString = "Re-Print Barcode" Then
                If reader(3).ToString = 0 Then
                    Me.btnReprint.Enabled = False
                ElseIf reader(3).ToString = 1 Then
                    Me.btnReprint.Enabled = True
                End If
            End If
        End While
        Disconnect()
    End Sub

    Public Sub DisableAddPatient()
        'if user is admin enable add patient
        Connect()
        rs.Connection = conn
        rs.CommandType = CommandType.Text
        rs.CommandText = "SELECT `usertype`, `dept` FROM `user_account` WHERE `email` = '" & CurrEmail & "'"
        reader = rs.ExecuteReader
        While reader.Read()
            If reader.HasRows Then
                If reader(0).ToString = "Administrator" Or reader(0).ToString = "Medical Technol" Or reader(1).ToString = "WARD" Then
                    Me.btnAddPatientOrder.Enabled = True
                ElseIf reader(0).ToString <> "Administrator" Or reader(0).ToString <> "Medical Technol" Then
                    Me.btnAddPatientOrder.Enabled = False
                End If
            End If
        End While
        Disconnect()
    End Sub

    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.ItemClick
        Dim selectedRows() As Integer = GridView.GetSelectedRows()
        For Each rowHandle As Integer In selectedRows
            If rowHandle >= 0 Then
                rs.Parameters.Clear()
                rs.Parameters.AddWithValue("@mainID", GridView.GetRowCellValue(rowHandle, GridView.Columns("RefID")))
                rs.Parameters.AddWithValue("@SampleID", GridView.GetRowCellValue(rowHandle, GridView.Columns("SampleID")))
                rs.Parameters.AddWithValue("@status", "Processing")
                rs.Parameters.AddWithValue("@Section", GridView.GetRowCellValue(rowHandle, GridView.Columns("Section")))
                rs.Parameters.AddWithValue("@SubSection", GridView.GetRowCellValue(rowHandle, GridView.Columns("SubSection")))
                rs.Parameters.AddWithValue("@Processing", Now)

                UpdateRecordwthoutMSG("UPDATE `tmpWorklist` Set " _
                    & "`sample_id` = @SampleID, " _
                    & "`main_id` = @mainID, " _
                    & "`status` = @status" _
                    & " WHERE main_id = @mainID And `testtype` = @Section And `sub_section` = @SubSection"
                    )

                Connect()
                rs.Connection = conn
                rs.CommandType = CommandType.Text
                rs.CommandText = "Select `sample_id` FROM `specimen_tracking` WHERE `sample_id` = @SampleID"
                reader = rs.ExecuteReader
                reader.Read()
                If reader.HasRows Then
                    Disconnect()
                    UpdateRecordwthoutMSG("UPDATE `specimen_tracking` Set " _
                        & "`sample_id` = @SampleID, " _
                        & "`processing` = @Processing" _
                        & " WHERE sample_id = @mainID And `section` = @Section And `sub_section` = @SubSection"
                        )
                Else
                    Disconnect()
                    SaveRecordwthoutMSG("INSERT INTO `specimen_tracking` (`sample_id`, `processing`, `section`, `sub_section`) VALUES " _
                        & "(" _
                        & " @SampleID, " _
                        & "@Processing, " _
                        & "@Section, " _
                        & "@SubSection" _
                        & ")"
                        )
                End If
                Disconnect()

            End If
        Next rowHandle
        LoadRecords()
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.ItemClick
        LoadRecords()
        'packagename()
    End Sub

    Private Sub RefreshForm()
        Me.Controls.Clear()
        Me.InitializeComponent()
    End Sub

    Private Sub btnCancel_ItemClick(ByVal sender As Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnCancel.ItemClick
        Dim selectedRows() As Integer = GridView.GetSelectedRows()
        For Each rowHandle As Integer In selectedRows
            If rowHandle >= 0 Then
                If MessageBox.Show("Are you sure you want To reject " & GridView.GetRowCellValue(rowHandle, GridView.Columns("PatientName")) & " order?", "Confirm Reject", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = vbYes Then
                    frmCancelOR.ID = GridView.GetRowCellValue(rowHandle, GridView.Columns("SequenceNo"))
                    frmCancelOR.sID = GridView.GetRowCellValue(rowHandle, GridView.Columns("SampleID"))
                    frmCancelOR.pID = GridView.GetRowCellValue(rowHandle, GridView.Columns("PatientID"))
                    frmCancelOR.pName = GridView.GetRowCellValue(rowHandle, GridView.Columns("PatientName"))
                    frmCancelOR.pTest = GridView.GetRowCellValue(rowHandle, GridView.Columns("Request"))
                    frmCancelOR.pSection = GridView.GetRowCellValue(rowHandle, GridView.Columns("Section"))
                    frmCancelOR.pSubSection = GridView.GetRowCellValue(rowHandle, GridView.Columns("SubSection"))
                    frmCancelOR.ShowDialog()
                End If
            End If
        Next rowHandle
        LoadRecords()
    End Sub

    Private Sub btnClose_ItemClick(ByVal sender As Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnClose.ItemClick
        Me.Close()
        Me.Dispose()
    End Sub

    Private Sub frm_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        'MainFOrm.aceFecal.Appearance.Normal.BackColor = Color.FromArgb(6, 31, 71)
        MainFOrm.btnPatient.Appearance.Normal.BackColor = Color.FromArgb(16, 110, 190)
        MainFOrm.btnPatient.Appearance.Normal.ForeColor = Color.FromArgb(255, 255, 255)
    End Sub

    Private Sub frm_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        MainFOrm.lblTitle.Text = ""
        MainFOrm.btnPatient.Appearance.Normal.BackColor = Color.FromArgb(240, 240, 240)
        MainFOrm.btnPatient.Appearance.Normal.ForeColor = Color.FromArgb(27, 41, 62)
        Me.Dispose()
    End Sub

    Private Sub txtSearch_SelectedIndexChanged(sender As Object, e As EventArgs) Handles txtSearch.SelectedIndexChanged, dtFrom1.TextChanged, dtTo1.TextChanged, txtSearch.TextChanged
        LoadRecords()
    End Sub

    Private Sub btnReprint_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnReprint.ItemClick
        Dim selectedRows() As Integer = GridView.GetSelectedRows()
        For Each rowHandle As Integer In selectedRows
            If rowHandle >= 0 Then
                PrintBarcode(GridView.GetFocusedRowCellValue(GridView.Columns("Request")).ToString,
                             GridView.GetFocusedRowCellValue(GridView.Columns("SampleID")).ToString,
                             GridView.GetFocusedRowCellValue(GridView.Columns("PatientID")).ToString,
                             GridView.GetFocusedRowCellValue(GridView.Columns("PatientName")).ToString,
                             GridView.GetFocusedRowCellValue(GridView.Columns("DateOfBirth")).ToString,
                             GridView.GetFocusedRowCellValue(GridView.Columns("Sex")).ToString,
                             GridView.GetFocusedRowCellValue(GridView.Columns("Section")).ToString,
                             GridView.GetFocusedRowCellValue(GridView.Columns("SubSection")).ToString,
                             Split(GridView.GetFocusedRowCellValue(GridView.Columns("RoomWard")).ToString, "^")(0),
                             Split(GridView.GetFocusedRowCellValue(GridView.Columns("RoomWard")).ToString, "^")(1),
                             CurrUsername)
                'Activity Logs
                ActivityLogs(GridView.GetFocusedRowCellValue(GridView.Columns("SampleID")).ToString,
                             GridView.GetFocusedRowCellValue(GridView.Columns("PatientID")).ToString,
                             GridView.GetFocusedRowCellValue(GridView.Columns("PatientName")).ToString,
                             CurrUser,
                             "Reprint Barcode",
                             GridView.GetFocusedRowCellValue(GridView.Columns("Request")).ToString,
                             GridView.GetFocusedRowCellValue(GridView.Columns("Section")).ToString,
                             GridView.GetFocusedRowCellValue(GridView.Columns("SubSection")).ToString)
            End If
        Next rowHandle
    End Sub

    Private Sub btnDelete_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnDelete.ItemClick
        Dim selectedRows() As Integer = GridView.GetSelectedRows()
        For Each rowHandle As Integer In selectedRows
            If rowHandle >= 0 Then
                If MessageBox.Show("Are you sure you want to reject " & GridView.GetRowCellValue(rowHandle, GridView.Columns("PatientName")) & " order?", "Confirm Reject", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = vbYes Then
                    frmRejectOrder.ID = GridView.GetRowCellValue(rowHandle, GridView.Columns("SequenceNo"))
                    frmRejectOrder.sID = GridView.GetRowCellValue(rowHandle, GridView.Columns("SampleID"))
                    frmRejectOrder.pID = GridView.GetRowCellValue(rowHandle, GridView.Columns("PatientID"))
                    frmRejectOrder.pName = GridView.GetRowCellValue(rowHandle, GridView.Columns("PatientName"))
                    frmRejectOrder.pTest = GridView.GetRowCellValue(rowHandle, GridView.Columns("Request"))
                    frmRejectOrder.pSection = GridView.GetRowCellValue(rowHandle, GridView.Columns("Section"))
                    frmRejectOrder.pSubSection = GridView.GetRowCellValue(rowHandle, GridView.Columns("SubSection"))
                    frmRejectOrder.ShowDialog()
                End If
            End If
        Next rowHandle

        LoadRecords()
    End Sub

    Private Sub btnAddPatient_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnAddPatientOrder.ItemClick
        Try
            frmPatientOrderAE.AEstatus = "new"
            frmPatientOrderAE.ShowDialog()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnEdit_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnEdit.ItemClick
        Dim a As Integer = 0
        Dim selectedRows() As Integer = GridView.GetSelectedRows()

        For Each rowHandle As Integer In selectedRows
            If rowHandle >= 0 Then
                If GridView.GetFocusedRowCellValue(GridView.Columns("Status")).ToString = "Checked-In" Then

                    Connect()
                    rs.Connection = conn
                    'rs.CommandText = "SELECT `universal_id`, `test_code`, `section`, `sample_id`, `order_no`, `sub_section`, `his_mainID`, `his_code` FROM `tmpresult` where `sample_id` = '" & GridView.GetFocusedRowCellValue(GridView.Columns("SampleID")) & "'"
                    rs.CommandText = "SELECT `tmpresult`.`universal_id`, `tmpresult`.`test_code`, `tmpresult`.`section`, `tmpresult`.`sample_id`, `tmpresult`.`order_no`, `tmpresult`.`sub_section`, `tmpresult`.`his_mainID`, `tmpresult`.`his_code`, `packages`.`packagename` FROM `tmpresult` LEFT JOIN `packages` ON `packages`.`packagecode` = `tmpresult`.`his_code` where `sample_id` = '" & GridView.GetFocusedRowCellValue(GridView.Columns("SampleID")) & "'"
                    reader = rs.ExecuteReader
                    While reader.Read
                        'Dim iItem As New ListViewItem("", 0)
                        a = a + 1
                        Dim iItem As New ListViewItem((a).ToString, 0)
                        iItem.SubItems.Add(reader(0).ToString)
                        iItem.SubItems.Add(reader(1).ToString)
                        iItem.SubItems.Add(reader(2).ToString)
                        iItem.SubItems.Add(reader(3).ToString)

                        iItem.SubItems.Add(reader(4).ToString)
                        iItem.SubItems.Add(reader(5).ToString)
                        iItem.SubItems.Add(reader(6).ToString)
                        iItem.SubItems.Add(reader(7).ToString)
                        iItem.SubItems.Add(reader(8).ToString)
                        frmPatientOrderAE.lvOrder.Items.Add(iItem)
                    End While
                    Disconnect()

                    frmPatientOrderAE.txtSampleID.Text = GridView.GetFocusedRowCellValue(GridView.Columns("SampleID")).ToString
                    frmPatientOrderAE.txtPatientID.Text = GridView.GetFocusedRowCellValue(GridView.Columns("PatientID")).ToString
                    frmPatientOrderAE.txtName.Text = GridView.GetFocusedRowCellValue(GridView.Columns("PatientName")).ToString
                    frmPatientOrderAE.cboSex.Text = GridView.GetFocusedRowCellValue(GridView.Columns("Sex")).ToString
                    frmPatientOrderAE.dtBDate.Text = GridView.GetFocusedRowCellValue(GridView.Columns("DateOfBirth")).ToString
                    frmPatientOrderAE.txtAge.Text = GridView.GetFocusedRowCellValue(GridView.Columns("Age")).ToString
                    frmPatientOrderAE.dtReceived.Text = GridView.GetFocusedRowCellValue(GridView.Columns("DateRequested")).ToString
                    frmPatientOrderAE.tmTimeReceived.Text = GridView.GetFocusedRowCellValue(GridView.Columns("TimeRequested")).ToString
                    frmPatientOrderAE.cboRequest.Text = GridView.GetFocusedRowCellValue(GridView.Columns("Request")).ToString
                    frmPatientOrderAE.cboPhysician.Text = GridView.GetFocusedRowCellValue(GridView.Columns("Physician")).ToString
                    frmPatientOrderAE.cboRoom.Text = GridView.GetFocusedRowCellValue(GridView.Columns("RoomWard")).ToString
                    frmPatientOrderAE.cboPType.Text = GridView.GetFocusedRowCellValue(GridView.Columns("PatientType")).ToString
                    frmPatientOrderAE.txtAddress.Text = GridView.GetFocusedRowCellValue(GridView.Columns("Address")).ToString
                    'frmPatientOrderAE.cboTestName.Text = GridView.GetFocusedRowCellValue(GridView.Columns("Section")).ToString
                    frmPatientOrderAE.subsec = GridView.GetFocusedRowCellValue(GridView.Columns("SubSection")).ToString
                    frmPatientOrderAE.txtContact.Text = GridView.GetFocusedRowCellValue(GridView.Columns("ContactNumber")).ToString
                    frmPatientOrderAE.txtClinicalImpression.Text = GridView.GetFocusedRowCellValue(GridView.Columns("Diagnosis")).ToString

                    frmPatientOrderAE.AEstatus = "edit"

                    frmPatientOrderAE.ShowDialog()

                Else
                    MessageBox.Show("Can only edit Checked-In Patients", "", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Exit Sub

                End If
            End If
        Next rowHandle
    End Sub

    Private Sub GridView_DoubleClick(sender As Object, e As EventArgs) 'Handles GridView.DoubleClick
        If GridView.GetFocusedRowCellValue(GridView.Columns("Status")).ToString = "Checked-In" Then

            Connect()
            rs.Connection = conn
            rs.CommandText = "SELECT `universal_id`, `test_code`, `section`, `sample_id`, `order_no`, `sub_section` FROM `tmpresult` where `sample_id` = '" & GridView.GetFocusedRowCellValue(GridView.Columns("SampleID")) & "'"
            reader = rs.ExecuteReader
            While reader.Read
                Dim iItem As New ListViewItem("", 0)
                iItem.SubItems.Add(reader(0).ToString)
                iItem.SubItems.Add(reader(1).ToString)
                iItem.SubItems.Add(reader(2).ToString)
                iItem.SubItems.Add(reader(3))

                iItem.SubItems.Add(reader(4))
                iItem.SubItems.Add(reader(5))
                iItem.SubItems.Add("")
                frmPatientOrderAE.lvOrder.Items.Add(iItem)
            End While
            Disconnect()

            frmPatientOrderAE.txtSampleID.Text = GridView.GetFocusedRowCellValue(GridView.Columns("SampleID")).ToString
            frmPatientOrderAE.txtPatientID.Text = GridView.GetFocusedRowCellValue(GridView.Columns("PatientID")).ToString
            frmPatientOrderAE.txtName.Text = GridView.GetFocusedRowCellValue(GridView.Columns("PatientName")).ToString
            frmPatientOrderAE.cboSex.Text = GridView.GetFocusedRowCellValue(GridView.Columns("Sex")).ToString
            frmPatientOrderAE.dtBDate.Text = GridView.GetFocusedRowCellValue(GridView.Columns("DateOfBirth")).ToString
            frmPatientOrderAE.txtAge.Text = GridView.GetFocusedRowCellValue(GridView.Columns("Age")).ToString
            frmPatientOrderAE.dtReceived.Text = GridView.GetFocusedRowCellValue(GridView.Columns("DateRequested")).ToString
            frmPatientOrderAE.tmTimeReceived.Text = GridView.GetFocusedRowCellValue(GridView.Columns("TimeRequested")).ToString
            frmPatientOrderAE.cboRequest.Text = GridView.GetFocusedRowCellValue(GridView.Columns("Request")).ToString
            frmPatientOrderAE.cboPhysician.Text = GridView.GetFocusedRowCellValue(GridView.Columns("Physician")).ToString
            frmPatientOrderAE.cboRoom.Text = GridView.GetFocusedRowCellValue(GridView.Columns("RoomWard")).ToString
            frmPatientOrderAE.cboPType.Text = GridView.GetFocusedRowCellValue(GridView.Columns("PatientType")).ToString
            frmPatientOrderAE.cboTestName.Text = GridView.GetFocusedRowCellValue(GridView.Columns("Section")).ToString
            frmPatientOrderAE.ShowDialog()

        Else
            MessageBox.Show("Can only edit Checked-In Patients", "", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub

        End If
    End Sub

    'Public Sub packagename()
    '    'Connect()
    '    'rs.Connection = conn
    '    ''rs.CommandText = "SELECT DISTINCT * FROM `packages` WHERE `packagecode` = '" & GridView.GetFocusedRowCellValue(GridView.Columns("HISCode")) & "' ORDER BY `packagename`"
    '    'rs.CommandText = "SELECT DISTINCT * FROM `tmpworklist` WHERE `sample_id` = '" & GridView.GetFocusedRowCellValue(GridView.Columns("sampleID")) & "'"
    '    'reader = rs.ExecuteReader
    '    'While reader.Read
    '    '    MessageBox.Show(reader(2).ToString)
    '    'End While
    '    'Disconnect()

    '    Dim selectedRows2() As Integer = GridView.GetSelectedRows()
    '    For Each rowHandle1 As Integer In selectedRows2
    '        If rowHandle1 >= 0 Then
    '            Connect1()

    '            rs1.Parameters.Clear()
    '            rs1.Parameters.AddWithValue("@sampleDD", GridView.GetRowCellValue(rowHandle1, GridView.Columns("SampleID")))

    '            rs1.Connection = conn1
    '            rs1.CommandType = CommandType.Text
    '            rs1.CommandText = "SELECT `sample_id` FROM `tmpworklist` WHERE `sample_id` = @sampleDD"
    '            reader1 = rs1.ExecuteReader
    '            reader1.Read()
    '            If reader1.HasRows Then
    '                Test = reader1(0).ToString

    '            End If
    '            Disconnect1()
    '            'rs3.Parameters.Clear()

    '        End If
    '    Next
    '    Disconnect1()

    '    MessageBox.Show(Test)

    'End Sub

End Class