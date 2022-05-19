Imports System.IO
Imports System.IO.Ports
Imports System.Threading
Imports System.Xml
Imports System.Text.RegularExpressions
Imports System.Drawing.Printing
Imports DevExpress.XtraPrinting.BarCode
Imports DevExpress.XtraGrid.Views.Grid

Public Class frmSeroWorklist

    Public mainID As String = ""
    Public Section As String = ""
    Public SubSection As String = ""
    Public Patient_ID As String = ""
    Public Validation As String = ""
    Dim arrImage() As Byte
    Dim bDate As String
    Dim age As String
    Dim list_Test As String
    Dim patientGender As String

    Dim Specimen As String = ""
    Dim SID As String = ""
    Dim PID As String = ""
    Dim PName As String = ""
    Dim dateAge As String = ""
    Dim gender As String = ""
    Dim Test As String = ""

    Dim Selected As String = ""

    'For Worklist
    Dim SequenceNo, Status, SampleID, PatientID, PatientName, PatientDoB, PatientAge, PatientSex, ResultDate, ResultTime As String

    'For Result
    Dim ResultName, ResultValue, ResultFlag, ResultOrder, ResultCode, ResultUnit, ResultUnitConv, ResultID, ResultGroup As String

    Private PrintDocType As String = "Barcode"
    Private StrPrinterName As String = My.Settings.BCPrinterName

    Public Sub LoadRecords()
        Dim getStatus As String = ""

        Connect()
        rs.Connection = conn
        rs.CommandType = CommandType.Text
        rs.CommandText = "SELECT `usertype` FROM `user_account` WHERE `email` = '" & CurrEmail & "'"
        reader = rs.ExecuteReader
        reader.Read()
        If reader.HasRows Then
            If reader(0) = "Doctor" Then
                getStatus = "`tmpWorklist`.`status` = 'For Verification'"
                Disconnect()
            ElseIf reader(0) = "Administrator" Then
                getStatus = "`tmpWorklist`.`status` = 'Result Received' OR `tmpWorklist`.`status` = 'Verified' OR `tmpWorklist`.`status` = 'Processing' OR `tmpWorklist`.`status` = 'For Verification'"
                Disconnect()
            Else
                getStatus = "`tmpWorklist`.`status` = 'Result Received' OR `tmpWorklist`.`status` = 'Verified' OR `tmpWorklist`.`status` = 'Processing'"
                Disconnect()
            End If

        End If
        Disconnect()

        Try
            GridView.Columns.Clear()
            GridView.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
            GridView.Appearance.HeaderPanel.FontStyleDelta = FontStyle.Bold

            'cboLocation.Text = "All" 'CurrDept
            If (txtSearch.Text = "") Then

                If (cboLocation.Text = "All") Then

                    Dim SQL As String = "SELECT
                        `tmpWorklist`.`id` AS SequenceNo,
                        `tmpWorklist`.`status` AS Status,
                        `tmpWorklist`.`sample_id` AS SampleID,
                        `tmpWorklist`.`patient_id` AS PatientID,
                        `additional_info`.`cs_no` AS ChargeSlip,
                        `tmpWorklist`.`patient_name` AS PatientName, 
                        `tmpWorklist`.`test` AS Request,
                        `tmpWorklist`.`bdate` AS DateOfBirth,
                        `tmpWorklist`.`sex` AS Sex,
                        `tmpWorklist`.`age` AS Age,
                        `tmpWorklist`.`dept` AS RoomWard,
                        `tmpWorklist`.`physician` AS Physician,
                        `tmpWorklist`.`medtech` AS PerformedBy,
                        `tmpWorklist`.`verified_by` AS VerifiedBy,
                        DATE_FORMAT(`tmpWorklist`.`date`, '%m/%d/%Y') AS DateRequested,
                        `tmpWorklist`.`time` AS TimeRequested,
                        `tmpWorklist`.`testtype` AS Section,
                        `tmpWorklist`.`sub_section` AS SubSection,
                        DATE_FORMAT(`specimen_tracking`.`processing`, '%m/%d/%Y %r') AS DateReceived, 
                        `tmpWorklist`.`main_id` AS RefID,
                        `tmpWorklist`.`patient_type` AS PatientType,
                        `patient_remarks`.`diagnosis` AS ClinicalImpression,
                                    `patient_info`.`address` AS Address
                        FROM `tmpWorklist` 
                        LEFT JOIN `specimen_tracking` ON
	                        `specimen_tracking`.`sample_id` = `tmpWorklist`.`main_id`
                        LEFT JOIN `additional_info` ON
	                        `additional_info`.`sample_id` = `tmpWorklist`.`main_id`
                        LEFT JOIN `patient_remarks` ON `patient_remarks`.`sample_id` = `tmpworklist`.`sample_id`
                        LEFT JOIN `patient_info` ON `patient_info`.`patient_id` = `tmpworklist`.`patient_id`
                        WHERE (" & getStatus & ")
                        AND (`specimen_tracking`.`extracted` <> '' OR `specimen_tracking`.`extracted` IS NOT NULL)
                        AND (`tmpWorklist`.`testtype` = `specimen_tracking`.`section`)
                        AND (`tmpWorklist`.`sub_section` = `specimen_tracking`.`sub_section`)
                        AND (`tmpworklist`.`testtype` = `additional_info`.`section`)
                        AND (`tmpworklist`.`sub_section` = `additional_info`.`sub_section`)
                        AND (`tmpworklist`.`testtype` = `patient_remarks`.`section`)
                        AND (`tmpworklist`.`sub_section` = `patient_remarks`.`sub_section`)
                        AND (`tmpWorklist`.`testtype` = 'ImmunoSero')
                        ORDER BY `tmpWorklist`.`status`, `specimen_tracking`.`processing` DESC "

                    Dim command As New MySql.Data.MySqlClient.MySqlCommand(SQL, conn)

                    command.Parameters.Clear()

                    Dim adapter As New MySql.Data.MySqlClient.MySqlDataAdapter(command)

                    Dim myTable As DataTable = New DataTable
                    adapter.Fill(myTable)

                    dtList.DataSource = myTable

                    Gridview_Column_Width()
                Else

                    Dim SQL As String = "SELECT
                        `tmpWorklist`.`id` AS SequenceNo,
                        `tmpWorklist`.`status` AS Status,
                        `tmpWorklist`.`sample_id` AS SampleID,
                        `tmpWorklist`.`patient_id` AS PatientID,
                        `additional_info`.`cs_no` AS ChargeSlip,
                        `tmpWorklist`.`patient_name` AS PatientName, 
                        `tmpWorklist`.`test` AS Request,
                        `tmpWorklist`.`bdate` AS DateOfBirth,
                        `tmpWorklist`.`sex` AS Sex,
                        `tmpWorklist`.`age` AS Age,
                        `tmpWorklist`.`dept` AS RoomWard,
                        `tmpWorklist`.`physician` AS Physician,
                        `tmpWorklist`.`medtech` AS PerformedBy,
                        `tmpWorklist`.`verified_by` AS VerifiedBy,
                        DATE_FORMAT(`tmpWorklist`.`date`, '%m/%d/%Y') AS DateRequested,
                        `tmpWorklist`.`time` AS TimeRequested,
                        `tmpWorklist`.`testtype` AS Section,
                        `tmpWorklist`.`sub_section` AS SubSection,
                        DATE_FORMAT(`specimen_tracking`.`processing`, '%m/%d/%Y %r') AS DateReceived, 
                        `tmpWorklist`.`main_id` AS RefID,
                        `tmpWorklist`.`patient_type` AS PatientType,
                        `patient_remarks`.`diagnosis` AS ClinicalImpression,
                                    `patient_info`.`address` AS Address
                        FROM `tmpWorklist` 
                        LEFT JOIN `specimen_tracking` ON
	                        `specimen_tracking`.`sample_id` = `tmpWorklist`.`main_id`
                        LEFT JOIN `additional_info` ON
	                        `additional_info`.`sample_id` = `tmpWorklist`.`main_id`
                        LEFT JOIN `patient_remarks` ON `patient_remarks`.`sample_id` = `tmpworklist`.`sample_id`
                        LEFT JOIN `patient_info` ON `patient_info`.`patient_id` = `tmpworklist`.`patient_id`
                        WHERE (" & getStatus & ")
                        AND (`specimen_tracking`.`extracted` <> '' OR `specimen_tracking`.`extracted` IS NOT NULL)
                        AND (`tmpWorklist`.`testtype` = `specimen_tracking`.`section`)
                        AND (`tmpWorklist`.`sub_section` = `specimen_tracking`.`sub_section`)
                        AND (`tmpworklist`.`testtype` = `additional_info`.`section`)
                        AND (`tmpworklist`.`sub_section` = `additional_info`.`sub_section`)
                        AND (`tmpworklist`.`testtype` = `patient_remarks`.`section`)
                        AND (`tmpworklist`.`sub_section` = `patient_remarks`.`sub_section`)
                        AND (`tmpWorklist`.`testtype` = 'ImmunoSero')
                        AND (`tmpWorklist`.`location` = @Location)
                        ORDER BY `tmpWorklist`.`status`, `specimen_tracking`.`processing` DESC"

                    Dim command As New MySql.Data.MySqlClient.MySqlCommand(SQL, conn)

                    command.Parameters.Clear()
                    command.Parameters.AddWithValue("@Location", cboLocation.Text)

                    Dim adapter As New MySql.Data.MySqlClient.MySqlDataAdapter(command)

                    Dim myTable As DataTable = New DataTable
                    adapter.Fill(myTable)

                    dtList.DataSource = myTable

                    Gridview_Column_Width()

                End If

            Else

                If (cboLocation.Text = "All") Then

                    Dim SQL As String = "SELECT * FROM (SELECT
                        `tmpWorklist`.`id` AS SequenceNo,
                        `tmpWorklist`.`status` AS Status,
                        `tmpWorklist`.`sample_id` AS SampleID,
                        `tmpWorklist`.`patient_id` AS PatientID,
                        `additional_info`.`cs_no` AS ChargeSlip,
                        `tmpWorklist`.`patient_name` AS PatientName, 
                        `tmpWorklist`.`test` AS Request,
                        `tmpWorklist`.`bdate` AS DateOfBirth,
                        `tmpWorklist`.`sex` AS Sex,
                        `tmpWorklist`.`age` AS Age,
                        `tmpWorklist`.`dept` AS RoomWard,
                        `tmpWorklist`.`physician` AS Physician,
                        `tmpWorklist`.`medtech` AS PerformedBy,
                        `tmpWorklist`.`verified_by` AS VerifiedBy,
                        DATE_FORMAT(`tmpWorklist`.`date`, '%m/%d/%Y') AS DateRequested,
                        `tmpWorklist`.`time` AS TimeRequested,
                        `tmpWorklist`.`testtype` AS Section,
                        `tmpWorklist`.`sub_section` AS SubSection,
                        DATE_FORMAT(`specimen_tracking`.`processing`, '%m/%d/%Y %r') AS DateReceived, 
                        `tmpWorklist`.`main_id` AS RefID,
                        `tmpWorklist`.`patient_type` AS PatientType,
                        `patient_remarks`.`diagnosis` AS ClinicalImpression,
                                    `patient_info`.`address` AS Address
                        FROM `tmpWorklist` 
                        LEFT JOIN `specimen_tracking` ON
	                        `specimen_tracking`.`sample_id` = `tmpWorklist`.`main_id`
                        LEFT JOIN `additional_info` ON
	                        `additional_info`.`sample_id` = `tmpWorklist`.`main_id`
                        LEFT JOIN `patient_remarks` ON `patient_remarks`.`sample_id` = `tmpworklist`.`sample_id`
                        LEFT JOIN `patient_info` ON `patient_info`.`patient_id` = `tmpworklist`.`patient_id`
                        WHERE (" & getStatus & ")
                        AND (`specimen_tracking`.`extracted` <> '' OR `specimen_tracking`.`extracted` IS NOT NULL)
                        AND (`tmpWorklist`.`testtype` = `specimen_tracking`.`section`)
                        AND (`tmpWorklist`.`sub_section` = `specimen_tracking`.`sub_section`)
                        AND (`tmpworklist`.`testtype` = `additional_info`.`section`)
                        AND (`tmpworklist`.`sub_section` = `additional_info`.`sub_section`)
                        AND (`tmpworklist`.`testtype` = `patient_remarks`.`section`)
                        AND (`tmpworklist`.`sub_section` = `patient_remarks`.`sub_section`)
                        AND (`tmpWorklist`.`testtype` = 'ImmunoSero')
                       ) AS result WHERE SampleID LIKE '" & txtSearch.Text & "%' OR PatientID LIKE '" & txtSearch.Text & "%' OR PatientName LIKE '" & txtSearch.Text & "%' OR Physician LIKE '" & txtSearch.Text & "%'
                       ORDER BY `result`.`Status`, `result`.`DateReceived` DESC"

                    Dim command As New MySql.Data.MySqlClient.MySqlCommand(SQL, conn)

                    command.Parameters.Clear()

                    Dim adapter As New MySql.Data.MySqlClient.MySqlDataAdapter(command)

                    Dim myTable As DataTable = New DataTable
                    adapter.Fill(myTable)

                    dtList.DataSource = myTable

                    Gridview_Column_Width()

                Else

                    Dim SQL As String = "SELECT * FROM (SELECT
                        `tmpWorklist`.`id` AS SequenceNo,
                        `tmpWorklist`.`status` AS Status,
                        `tmpWorklist`.`sample_id` AS SampleID,
                        `tmpWorklist`.`patient_id` AS PatientID,
                        `additional_info`.`cs_no` AS ChargeSlip,
                        `tmpWorklist`.`patient_name` AS PatientName, 
                        `tmpWorklist`.`test` AS Request,
                        `tmpWorklist`.`bdate` AS DateOfBirth,
                        `tmpWorklist`.`sex` AS Sex,
                        `tmpWorklist`.`age` AS Age,
                        `tmpWorklist`.`dept` AS RoomWard,
                        `tmpWorklist`.`physician` AS Physician,
                        `tmpWorklist`.`medtech` AS PerformedBy,
                        `tmpWorklist`.`verified_by` AS VerifiedBy,
                        DATE_FORMAT(`tmpWorklist`.`date`, '%m/%d/%Y') AS DateRequested,
                        `tmpWorklist`.`time` AS TimeRequested,
                        `tmpWorklist`.`testtype` AS Section,
                        `tmpWorklist`.`sub_section` AS SubSection,
                        DATE_FORMAT(`specimen_tracking`.`processing`, '%m/%d/%Y %r') AS DateReceived, 
                        `tmpWorklist`.`main_id` AS RefID,
                        `tmpWorklist`.`patient_type` AS PatientType,
                        `patient_remarks`.`diagnosis` AS ClinicalImpression,
                        `patient_info`.`address` AS Address
                        FROM `tmpWorklist` 
                        LEFT JOIN `specimen_tracking` ON
	                        `specimen_tracking`.`sample_id` = `tmpWorklist`.`main_id`
                        LEFT JOIN `additional_info` ON
	                        `additional_info`.`sample_id` = `tmpWorklist`.`main_id`
                        LEFT JOIN `patient_remarks` ON `patient_remarks`.`sample_id` = `tmpworklist`.`sample_id`
                        LEFT JOIN `patient_info` ON `patient_info`.`patient_id` = `tmpworklist`.`patient_id`
                        WHERE (" & getStatus & ")
                        AND (`specimen_tracking`.`extracted` <> '' OR `specimen_tracking`.`extracted` IS NOT NULL)
                        AND (`tmpWorklist`.`testtype` = `specimen_tracking`.`section`)
                        AND (`tmpWorklist`.`sub_section` = `specimen_tracking`.`sub_section`)
                        AND (`tmpworklist`.`testtype` = `additional_info`.`section`)
                        AND (`tmpworklist`.`sub_section` = `additional_info`.`sub_section`)
                        AND (`tmpworklist`.`testtype` = `patient_remarks`.`section`)
                        AND (`tmpworklist`.`sub_section` = `patient_remarks`.`sub_section`)
                        AND (`tmpWorklist`.`testtype` = 'ImmunoSero')
                        AND (`tmpWorklist`.`location` = @Location)
                        ) AS result WHERE SampleID LIKE '" & txtSearch.Text & "%' OR PatientID LIKE '" & txtSearch.Text & "%' OR PatientName LIKE '" & txtSearch.Text & "%' OR Physician LIKE '" & txtSearch.Text & "%
                        ORDER BY `result`.`Status`, `result`.`DateReceived` DESC"

                    Dim command As New MySql.Data.MySqlClient.MySqlCommand(SQL, conn)

                    command.Parameters.Clear()
                    command.Parameters.AddWithValue("@Location", cboLocation.Text)

                    Dim adapter As New MySql.Data.MySqlClient.MySqlDataAdapter(command)

                    Dim myTable As DataTable = New DataTable
                    adapter.Fill(myTable)

                    dtList.DataSource = myTable

                    Gridview_Column_Width()

                End If

            End If

            GridView.Columns("SequenceNo").Visible = False
            GridView.Columns("PerformedBy").Visible = False
            GridView.Columns("VerifiedBy").Visible = False
            GridView.Columns("PatientType").Visible = False
            GridView.Columns("RefID").Visible = False
            'GridView.Columns("Section").Visible = False
            'GridView.Columns("SubSection").Visible = False
            GridView.Columns("Age").Visible = False
            GridView.Columns("ChargeSlip").Visible = False
            GridView.Columns("Address").Visible = False
            GridView.Columns("Section").Visible = False
            GridView.Columns("SubSection").Visible = False

            ' Make the grid read-only. 
            GridView.OptionsBehavior.Editable = False
            ' Prevent the focused cell from being highlighted. 
            GridView.OptionsSelection.EnableAppearanceFocusedCell = True
            ' Draw a dotted focus rectangle around the entire row. 
            GridView.FocusRectStyle = DrawFocusRectStyle.RowFullFocus

            GridView.OptionsSelection.MultiSelect = True
            GridView.OptionsSelection.MultiSelectMode = GridMultiSelectMode.RowSelect

            GridView.OptionsView.ColumnAutoWidth = False

            'GridView.ScrollStyle = ScrollStyleFlags.None

            GridView.Columns("SequenceNo").Width = 50
            GridView.Columns("PatientName").Width = 200
            GridView.Columns("Physician").Width = 200
            GridView.Columns("Request").Width = 200
            GridView.Columns("RoomWard").Width = 260
            GridView.Columns("Sex").Width = 60
            GridView.Columns("DateReceived").Width = 135
            GridView.Columns("ClinicalImpression").Width = 135
            GridView.Columns("Status").Width = 100
            GridView.Columns("TimeRequested").Width = 110
            GridView.Columns("DateRequested").Width = 100
            'GridView.Columns("DateReleased").Width = 135
            GridView.Columns("PerformedBy").Width = 200
            'GridView.Columns("ReleasedBy").Width = 200

        Catch ex As Exception
            MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub GridView_RowCellStyle(sender As Object, e As RowCellStyleEventArgs) Handles GridView.RowCellStyle
        Dim view As GridView = TryCast(sender, GridView)
        If (e.Column.FieldName = "SequenceNo") Or (e.Column.FieldName = "Status") Then
            If view.GetRowCellValue(e.RowHandle, "Status") = "Processing" Then
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
            ElseIf view.GetRowCellValue(e.RowHandle, "Status") = "Verified" Then
                e.Appearance.BackColor = Color.DarkCyan
                e.Appearance.BackColor2 = Color.DarkCyan
                e.Appearance.ForeColor = Color.White
            ElseIf view.GetRowCellValue(e.RowHandle, "Status") = "For Verification" Then
                e.Appearance.BackColor = Color.Tan
                e.Appearance.BackColor2 = Color.Tan
                e.Appearance.ForeColor = Color.Black
            Else
                e.Appearance.BackColor = Color.Gray
                e.Appearance.BackColor2 = Color.Gray
                e.Appearance.ForeColor = Color.White
            End If
        End If
    End Sub

    Public Sub LoadRecordsCompleted()
        Try
            GridCompleted.Columns.Clear()
            GridCompleted.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
            GridCompleted.Appearance.HeaderPanel.FontStyleDelta = FontStyle.Bold

            'cboLocation1.Text = "All" 'CurrDept
            'dtFrom1.Text = Date.Now
            'dtTo1.Text = Date.Now

            If (cboLocation1.Text = "All") Then

                Dim SQL As String = "SELECT
                        `order`.`id` AS SequenceNo,
                        `order`.`status` AS Status,
                        `order`.`sample_id` AS SampleID,
                        `order`.`patient_id` AS PatientID,
                        `additional_info`.`cs_no` AS ChargeSlip,
                        `order`.`patient_name` AS PatientName, 
                        `order`.`test` AS Request,
                        `order`.`bdate` AS DateOfBirth,
                        `order`.`sex` AS Sex,
                        `order`.`age` AS Age,
                        `order`.`dept` AS RoomWard,
                        `order`.`physician` AS Physician,
                        `order`.`medtech` AS PerformedBy,
                        `order`.`verified_by` AS VerifiedBy,
                        DATE_FORMAT(`order`.`date`, '%m/%d/%Y') AS DateRequested,
                        `order`.`time` AS TimeRequested,
                        `order`.`testtype` AS Section,
                        `order`.`sub_section` AS SubSection,
                        DATE_FORMAT(`specimen_tracking`.`extracted`, '%m/%d/%Y %r') AS DateReceived, 
                        DATE_FORMAT(`order`.`dt_released`, '%m/%d/%Y %r') AS DateReleased,
                        `order`.`main_id` AS RefID,
                        `order`.`patient_type` AS PatientType
                        FROM `order` 
                        LEFT JOIN `specimen_tracking` ON
	                        `specimen_tracking`.`sample_id` = `order`.`main_id`
                        LEFT JOIN `additional_info` ON
	                        `additional_info`.`sample_id` = `order`.`main_id`
                        WHERE (`order`.`status` = 'Printed' OR `order`.`status` = 'Validated' OR `order`.`status` = 'Released') 
                        AND (`order`.`testtype` = `specimen_tracking`.`section`)
                        AND (`order`.`sub_section` = `specimen_tracking`.`sub_section`)
                        AND (`order`.`testtype` = 'Immuno')
                        AND (DATE(DATE_FORMAT(`order`.`dt_released`, '%Y-%m-%d')) BETWEEN @Date1 AND @Date2)
                        ORDER BY `order`.`main_id` DESC"

                Dim command As New MySql.Data.MySqlClient.MySqlCommand(SQL, conn)

                command.Parameters.Clear()
                command.Parameters.Add("@Date1", MySql.Data.MySqlClient.MySqlDbType.DateTime).Value = Format(dtFrom1.Value, "yyyy-MM-dd")
                command.Parameters.Add("@Date2", MySql.Data.MySqlClient.MySqlDbType.DateTime).Value = Format(dtTo1.Value, "yyyy-MM-dd")

                Dim adapter As New MySql.Data.MySqlClient.MySqlDataAdapter(command)

                Dim myTable As DataTable = New DataTable
                adapter.Fill(myTable)

                dtCompleted.DataSource = myTable

            Else
                Try
                    GridCompleted.Columns.Clear()
                    GridCompleted.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                    GridCompleted.Appearance.HeaderPanel.FontStyleDelta = FontStyle.Bold

                    Dim SQL As String = "SELECT
                        `order`.`id` AS SequenceNo,
                        `order`.`status` AS Status,
                        `order`.`sample_id` AS SampleID,
                        `order`.`patient_id` AS PatientID,
                        `additional_info`.`cs_no` AS ChargeSlip,
                        `order`.`patient_name` AS PatientName, 
                        `order`.`test` AS Request,
                        `order`.`bdate` AS DateOfBirth,
                        `order`.`sex` AS Sex,
                        `order`.`age` AS Age,
                        `order`.`dept` AS RoomWard,
                        `order`.`physician` AS Physician,
                        `order`.`medtech` AS PerformedBy,
                        `order`.`verified_by` AS VerifiedBy,
                        DATE_FORMAT(`order`.`date`, '%m/%d/%Y') AS DateRequested,
                        `order`.`time` AS TimeRequested,
                        `order`.`testtype` AS Section,
                        `order`.`sub_section` AS SubSection,
                        DATE_FORMAT(`specimen_tracking`.`extracted`, '%m/%d/%Y %r') AS DateReceived, 
                        DATE_FORMAT(`order`.`dt_released`, '%m/%d/%Y %r') AS DateReleased,
                        `order`.`main_id` AS RefID,
                        `order`.`patient_type` AS PatientType
                        FROM `order` 
                        LEFT JOIN `specimen_tracking` ON
	                        `specimen_tracking`.`sample_id` = `order`.`main_id`
                        LEFT JOIN `additional_info` ON
	                        `additional_info`.`sample_id` = `order`.`main_id`
                        WHERE (`order`.`status` = 'Printed' OR `order`.`status` = 'Validated' OR `order`.`status` = 'Released') 
                        AND (`order`.`testtype` = `specimen_tracking`.`section`)
                        AND (`order`.`sub_section` = `specimen_tracking`.`sub_section`)
                        AND (`order`.`testtype` = 'Immuno')
                        AND (`order`.`location` = @Location)
                        AND (DATE(DATE_FORMAT(`order`.`dt_released`, '%Y-%m-%d')) BETWEEN @Date1 AND @Date2)
                        ORDER BY `order`.`main_id` DESC"

                    Dim command As New MySql.Data.MySqlClient.MySqlCommand(SQL, conn)

                    command.Parameters.Clear()
                    command.Parameters.Add("@Date1", MySql.Data.MySqlClient.MySqlDbType.DateTime).Value = Format(dtFrom1.Value, "yyyy-MM-dd")
                    command.Parameters.Add("@Date2", MySql.Data.MySqlClient.MySqlDbType.DateTime).Value = Format(dtTo1.Value, "yyyy-MM-dd")
                    command.Parameters.AddWithValue("@Location", cboLocation1.Text)

                    Dim adapter As New MySql.Data.MySqlClient.MySqlDataAdapter(command)

                    Dim myTable As DataTable = New DataTable
                    adapter.Fill(myTable)

                    dtCompleted.DataSource = myTable

                Catch ex As Exception
                    MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            End If

            'GridCompleted.Columns("PerformedBy").Visible = False
            'GridCompleted.Columns("ReleasedBy").Visible = False
            GridCompleted.Columns("PatientType").Visible = False
            GridCompleted.Columns("RefID").Visible = False
            GridCompleted.Columns("Section").Visible = False
            GridCompleted.Columns("SubSection").Visible = False
            GridCompleted.Columns("ChargeSlip").Visible = False
            'GridCompleted.Columns("Age").Visible = False

            ' Make the grid read-only. 
            GridCompleted.OptionsBehavior.Editable = False
            ' Prevent the focused cell from being highlighted. 
            GridCompleted.OptionsSelection.EnableAppearanceFocusedCell = False
            ' Draw a dotted focus rectangle around the entire row. 
            GridCompleted.FocusRectStyle = DrawFocusRectStyle.RowFullFocus

            GridCompleted.OptionsSelection.MultiSelect = True
            GridCompleted.OptionsSelection.MultiSelectMode = GridMultiSelectMode.RowSelect

        Catch ex As Exception
            MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub GridComplete_RowCellStyle(sender As Object, e As RowCellStyleEventArgs) Handles GridCompleted.RowCellStyle
        Dim view As GridView = TryCast(sender, GridView)
        If (e.Column.FieldName = "SequenceNo") Or (e.Column.FieldName = "Status") Then
            If view.GetRowCellValue(e.RowHandle, "Status") = "Processing" Then
                e.Appearance.BackColor = Color.ForestGreen
                e.Appearance.BackColor2 = Color.ForestGreen
                e.Appearance.ForeColor = Color.White
            ElseIf view.GetRowCellValue(e.RowHandle, "Status") = "Released" Then
                e.Appearance.BackColor = Color.ForestGreen
                e.Appearance.BackColor2 = Color.ForestGreen
                e.Appearance.ForeColor = Color.White
            ElseIf view.GetRowCellValue(e.RowHandle, "Status") = "Validated" Then
                e.Appearance.BackColor = Color.ForestGreen
                e.Appearance.BackColor2 = Color.ForestGreen
                e.Appearance.ForeColor = Color.White
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

    Private Sub frmNewOrder_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Me.Dispose()
    End Sub

    Private Sub frmNewOrder_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.cboLocation.Properties.Items.Clear()
        Me.cboLocation1.Properties.Items.Clear()
        dtFrom1.Text = "01/01/2000"

        Connect()
        rs.Connection = conn
        rs.CommandType = CommandType.Text
        rs.CommandText = "SELECT `location` FROM `location` ORDER BY `id`"
        reader = rs.ExecuteReader
        While reader.Read
            Me.cboLocation.Properties.Items.Add(reader(0))
            Me.cboLocation1.Properties.Items.Add(reader(0))
        End While
        Disconnect()

        'Load Location Automatically
        cboLocation.Text = CurrDept
        cboLocation1.Text = CurrDept

        'LoadRecords()
        'LoadRecordsCompleted()

        lblCountQueue.Text = "Record Count: " & GridView.RowCount

        DisablePermission()
    End Sub

    Public Sub DisablePermission()

        Connect()
        rs.Connection = conn
        rs.CommandType = CommandType.Text
        rs.CommandText = "SELECT * FROM `user_permission` WHERE `user_id` = '" & CurrEmail & "'"
        reader = rs.ExecuteReader
        While reader.Read()

            If reader(2).ToString = "Re-Print Barcode" Then
                If reader(3).ToString = 0 Then
                    Me.btnBarcode.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                ElseIf reader(3).ToString = 1 Then
                    Me.btnBarcode.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                End If
            End If

            If reader(2).ToString = "Print" Then
                If reader(3).ToString = 0 Then
                    Me.btnPrint.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                ElseIf reader(3).ToString = 1 Then
                    Me.btnPrint.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                End If
            End If

            If reader(2).ToString = "View Result" Then
                If reader(3).ToString = 0 Then
                    Me.btnView.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                ElseIf reader(3).ToString = 1 Then
                    Me.btnView.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                End If
            End If

            If reader(2).ToString = "Delete" Then
                If reader(3).ToString = 0 Then
                    Me.btnDelete.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                ElseIf reader(3).ToString = 1 Then
                    Me.btnDelete.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                End If
            End If

            If reader(2).ToString = "Reject Order" Then
                If reader(3).ToString = 0 Then
                    Me.btnReject.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                ElseIf reader(3).ToString = 1 Then
                    Me.btnReject.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                End If
            End If
        End While
        Disconnect()

    End Sub

    Private Sub btnView_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnView.ItemClick
        If XTab.SelectedTabPageIndex = 0 Then
            DisplayResult()
        ElseIf XTab.SelectedTabPageIndex = 1 Then
            DisplayResultCompleted()
        End If
    End Sub

    Private Sub GridCompleted_DoubleClick(sender As Object, e As EventArgs) Handles GridCompleted.DoubleClick
        DisplayResultCompleted()
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.ItemClick
        LoadRecords()
        LoadRecordsCompleted()
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.ItemClick
        Me.Close()
        Me.Dispose()
    End Sub

    Private Sub chkRefresh_CheckedChanged(sender As Object, e As EventArgs) Handles chkRefresh.CheckedChanged
        If chkRefresh.Checked = True Then
            refreshWorklist.Enabled = True
        ElseIf chkRefresh.Checked = False Then
            refreshWorklist.Enabled = False
        End If
    End Sub

    Private Sub GridView_DoubleClick(sender As Object, e As MouseEventArgs) Handles dtList.MouseDoubleClick
        DisplayResult()
    End Sub

    Private Sub refreshWorklist_Tick(sender As Object, e As EventArgs) Handles refreshWorklist.Tick
        LoadRecords()
        LoadRecordsCompleted()
    End Sub

    Private Sub cboLimit_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboLocation.SelectedIndexChanged, txtSearch.TextChanged
        LoadRecords()
    End Sub

    'Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    LoadRecordsCompleted()
    'End Sub

    'Private Sub cboLimit1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboLocation1.SelectedIndexChanged
    '    Try
    '        LoadRecordsCompleted()
    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    End Try
    'End Sub

    Private Sub cboSection_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboLocation1.SelectedIndexChanged, dtTo1.TextChanged, dtFrom1.TextChanged, txtSearch1.TextChanged
        Try
            GridCompleted.Columns.Clear()
            GridCompleted.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
            GridCompleted.Appearance.HeaderPanel.FontStyleDelta = FontStyle.Bold

            If (cboLocation1.Text = "All") Then

                Dim SQL As String = "SELECT * FROM (SELECT
                        `order`.`id` AS SequenceNo,
                        `order`.`status` AS Status,
                        `order`.`sample_id` AS SampleID,
                        `order`.`patient_id` AS PatientID,
                        `order`.`patient_name` AS PatientName, 
                        `order`.`test` AS Request,
                        `order`.`bdate` AS DateOfBirth,
                        `order`.`sex` AS Sex,
                        `order`.`age` AS Age,
                        `order`.`dept` AS RoomWard,
                        `order`.`physician` AS Physician,
                        `order`.`medtech` AS PerformedBy,
                        `order`.`verified_by` AS ReleasedBy,
                        DATE_FORMAT(`order`.`date`, '%m/%d/%Y') AS DateReceived,
                        `order`.`time` AS TimeReceived,
                        `order`.`testtype` AS Section,
                        `order`.`sub_section` AS SubSection,
                        DATE_FORMAT(`specimen_tracking`.`extracted`, '%m/%d/%Y %r') AS DateCheckedIn, 
                        DATE_FORMAT(`order`.`dt_released`, '%m/%d/%Y %r') AS DateReleased,
                        `order`.`main_id` AS RefID,
                        `order`.`patient_type` AS PatientType
                        FROM `order` 
                        LEFT JOIN `specimen_tracking` ON
	                        `specimen_tracking`.`sample_id` = `order`.`main_id`
                        WHERE (`order`.`status` = 'Printed' OR `order`.`status` = 'Validated' OR `order`.`status` = 'Released') 
                        AND (`order`.`testtype` = `specimen_tracking`.`section`)
                        AND (`order`.`sub_section` = `specimen_tracking`.`sub_section`)
                        AND (`order`.`testtype` = 'Immuno')
                        AND (DATE(DATE_FORMAT(`order`.`dt_released`, '%Y-%m-%d')) BETWEEN @Date1 AND @Date2)
                        ORDER BY `order`.`main_id` DESC) AS result WHERE SampleID LIKE '" & txtSearch1.Text & "%' OR PatientID LIKE '" & txtSearch1.Text & "%' OR PatientName LIKE '" & txtSearch1.Text & "%' OR Physician LIKE '" & txtSearch1.Text & "%'"

                Dim command As New MySql.Data.MySqlClient.MySqlCommand(SQL, conn)

                command.Parameters.Clear()
                command.Parameters.Add("@Date1", MySql.Data.MySqlClient.MySqlDbType.DateTime).Value = Format(dtFrom1.Value, "yyyy-MM-dd")
                command.Parameters.Add("@Date2", MySql.Data.MySqlClient.MySqlDbType.DateTime).Value = Format(dtTo1.Value, "yyyy-MM-dd")

                Dim adapter As New MySql.Data.MySqlClient.MySqlDataAdapter(command)

                Dim myTable As DataTable = New DataTable
                adapter.Fill(myTable)

                dtCompleted.DataSource = myTable

            Else

                Try
                    Dim SQL As String = "SELECT * FROM (SELECT
                        `order`.`id` AS SequenceNo,
                        `order`.`status` AS Status,
                        `order`.`sample_id` AS SampleID,
                        `order`.`patient_id` AS PatientID,
                        `order`.`patient_name` AS PatientName, 
                        `order`.`test` AS Request,
                        `order`.`bdate` AS DateOfBirth,
                        `order`.`sex` AS Sex,
                        `order`.`age` AS Age,
                        `order`.`dept` AS RoomWard,
                        `order`.`physician` AS Physician,
                        `order`.`medtech` AS PerformedBy,
                        `order`.`verified_by` AS ReleasedBy,
                        DATE_FORMAT(`order`.`date`, '%m/%d/%Y') AS DateReceived,
                        `order`.`time` AS TimeReceived,
                        `order`.`testtype` AS Section,
                        `order`.`sub_section` AS SubSection,
                        DATE_FORMAT(`specimen_tracking`.`extracted`, '%m/%d/%Y %r') AS DateCheckedIn, 
                        DATE_FORMAT(`order`.`dt_released`, '%m/%d/%Y %r') AS DateReleased,
                        `order`.`main_id` AS RefID,
                        `order`.`patient_type` AS PatientType
                        FROM `order` 
                        LEFT JOIN `specimen_tracking` ON
	                        `specimen_tracking`.`sample_id` = `order`.`main_id`
                        WHERE (`order`.`status` = 'Printed' OR `order`.`status` = 'Validated' OR `order`.`status` = 'Released') 
                        AND (`order`.`testtype` = `specimen_tracking`.`section`)
                        AND (`order`.`sub_section` = `specimen_tracking`.`sub_section`)
                        AND (`order`.`testtype` = 'Immuno')
                        AND (`order`.`location` = @Location)
                        AND (DATE(DATE_FORMAT(`order`.`dt_released`, '%Y-%m-%d')) BETWEEN @Date1 AND @Date2)
                        ORDER BY `order`.`main_id` DESC) AS result WHERE SampleID LIKE '" & txtSearch1.Text & "%' OR PatientID LIKE '" & txtSearch1.Text & "%' OR PatientName LIKE '" & txtSearch1.Text & "%' OR Physician LIKE '" & txtSearch1.Text & "%'"

                    Dim command As New MySql.Data.MySqlClient.MySqlCommand(SQL, conn)

                    command.Parameters.Clear()
                    command.Parameters.Add("@Date1", MySql.Data.MySqlClient.MySqlDbType.DateTime).Value = Format(dtFrom1.Value, "yyyy-MM-dd")
                    command.Parameters.Add("@Date2", MySql.Data.MySqlClient.MySqlDbType.DateTime).Value = Format(dtTo1.Value, "yyyy-MM-dd")
                    command.Parameters.AddWithValue("@Location", cboLocation1.Text)

                    Dim adapter As New MySql.Data.MySqlClient.MySqlDataAdapter(command)

                    Dim myTable As DataTable = New DataTable
                    adapter.Fill(myTable)

                    dtCompleted.DataSource = myTable


                Catch ex As Exception
                    MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            End If

            'GridCompleted.Columns("PerformedBy").Visible = False
            'GridCompleted.Columns("ReleasedBy").Visible = False
            GridCompleted.Columns("PatientType").Visible = False
            GridCompleted.Columns("RefID").Visible = False
            GridCompleted.Columns("Section").Visible = False
            GridCompleted.Columns("SubSection").Visible = False
            'GridCompleted.Columns("Age").Visible = False

            ' Make the grid read-only. 
            GridCompleted.OptionsBehavior.Editable = False
            ' Prevent the focused cell from being highlighted. 
            GridCompleted.OptionsSelection.EnableAppearanceFocusedCell = False
            ' Draw a dotted focus rectangle around the entire row. 
            GridCompleted.FocusRectStyle = DrawFocusRectStyle.RowFullFocus

            GridCompleted.OptionsSelection.MultiSelect = True
            GridCompleted.OptionsSelection.MultiSelectMode = GridMultiSelectMode.RowSelect

        Catch ex As Exception
            MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    'Private Sub GridView_DoubleClick(sender As Object, e As EventArgs) Handles btnView.ItemClick
    '    If XTab.SelectedTabPageIndex = 0 Then
    '        DisplayResult()
    '    ElseIf XTab.SelectedTabPageIndex = 1 Then
    '        DisplayResultCompleted()
    '    End If
    'End Sub

    Private Sub XTab_SelectedPageChanged(ByVal sender As Object, ByVal e As DevExpress.XtraTab.TabPageChangedEventArgs) Handles XTab.SelectedPageChanged
        If XTab.SelectedTabPageIndex = 0 Then
            btnDelete.Enabled = True
            btnView.Enabled = True
            btnRefresh.Enabled = True
            btnBarcode.Enabled = True
            btnPrint.Enabled = False
            lblCountQueue.Text = "Record Count: " & GridView.RowCount
            LoadRecords()
        ElseIf XTab.SelectedTabPageIndex = 1 Then
            btnView.Enabled = True
            btnRefresh.Enabled = True
            btnDelete.Enabled = False
            btnBarcode.Enabled = False
            btnPrint.Enabled = True
            lblCountQueue.Text = "Record Count: " & GridCompleted.RowCount
            LoadRecordsCompleted()
        End If
    End Sub

    Private Sub tmLoadNew_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmLoadNew.Tick
        LoadNewResult()
    End Sub

    Private Sub LoadNewResult()
        Try
            Connect()
            rs.Connection = conn
            rs.CommandType = CommandType.Text
            rs.CommandText = "SELECT `status` FROM `tmpresultstatus` WHERE `status` = 'New Result'"
            reader = rs.ExecuteReader
            reader.Read()
            If reader.HasRows Then
                Disconnect()
                LoadRecords()
                UpdateResultStatus()
            End If
            Disconnect()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub

    Private Sub UpdateResultStatus()
        Connect()
        rs.Connection = conn
        rs.CommandType = CommandType.Text
        rs.CommandText = "UPDATE `tmpresultstatus` SET `status` = 'No Result'"
        rs.ExecuteNonQuery()
        Disconnect()
    End Sub

    Private Sub btnPrint_ItemClick(ByVal sender As Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnPrint.ItemClick
        Try
            If XTab.SelectedTabPageIndex = 0 Then

            ElseIf XTab.SelectedTabPageIndex = 1 Then
                Using myRDLCPrinter As New RDLCPrinterPrintReleased(GridCompleted.GetFocusedRowCellValue(GridCompleted.Columns("RefID")),
                                                                    GridCompleted.GetFocusedRowCellValue(GridCompleted.Columns("Section")),
                                                                    GridCompleted.GetFocusedRowCellValue(GridCompleted.Columns("SubSection")),
                                                                    "", My.Settings.DefaultPrinter)
                    If My.Settings.SaveAsPDF Then
                        Dim byteViewer As Byte() = myRDLCPrinter.LocalReport.Render("PDF")
                        Dim saveFileDialog1 As New SaveFileDialog()
                        saveFileDialog1.Filter = "*PDF files (*.pdf)|*.pdf"
                        saveFileDialog1.FilterIndex = 2
                        saveFileDialog1.RestoreDirectory = True
                        Dim newFile As New FileStream(My.Settings.PDFLocation & GridCompleted.GetFocusedRowCellValue(GridCompleted.Columns("SampleID")) &
                                                      "_" & GridCompleted.GetFocusedRowCellValue(GridCompleted.Columns("PatientName")) & ".pdf", FileMode.Create)
                        newFile.Write(byteViewer, 0, byteViewer.Length)
                        newFile.Close()

                        myRDLCPrinter.Print(1)
                    Else
                        myRDLCPrinter.Print(1)
                    End If
                End Using
                'Activity Logs
                ActivityLogs(GridView.GetFocusedRowCellValue(GridCompleted.Columns("SampleID")).ToString,
                             GridCompleted.GetFocusedRowCellValue(GridCompleted.Columns("PatientID")).ToString,
                             GridCompleted.GetFocusedRowCellValue(GridCompleted.Columns("PatientName")).ToString,
                             CurrUser,
                             "Print Result",
                             GridCompleted.GetFocusedRowCellValue(GridCompleted.Columns("Request")).ToString,
                             GridCompleted.GetFocusedRowCellValue(GridCompleted.Columns("Section")).ToString,
                             GridCompleted.GetFocusedRowCellValue(GridCompleted.Columns("SubSection")).ToString)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try
    End Sub

    Private Sub btnDelete_ItemClick(ByVal sender As Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnDelete.ItemClick
        If MessageBox.Show("Are you sure you want to reject " & GridView.GetFocusedRowCellValue(GridView.Columns("PatientName")) & " order?", "Confirm Reject", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = vbYes Then
            frmRejectOrder.ID = GridView.GetFocusedRowCellValue(GridView.Columns("SequenceNo")).ToString
            frmRejectOrder.sID = GridView.GetFocusedRowCellValue(GridView.Columns("SampleID")).ToString
            frmRejectOrder.pID = GridView.GetFocusedRowCellValue(GridView.Columns("PatientID")).ToString
            frmRejectOrder.pName = GridView.GetFocusedRowCellValue(GridView.Columns("PatientName")).ToString
            frmRejectOrder.pTest = GridView.GetFocusedRowCellValue(GridView.Columns("Request")).ToString
            frmRejectOrder.pSection = GridView.GetFocusedRowCellValue(GridView.Columns("Section")).ToString
            frmRejectOrder.pSubSection = GridView.GetFocusedRowCellValue(GridView.Columns("SubSection")).ToString
            frmRejectOrder.ShowDialog()
        End If
        LoadRecords()

        ActivityLogs(GridView.GetFocusedRowCellValue(GridView.Columns("SampleID")).ToString,
                     GridView.GetFocusedRowCellValue(GridView.Columns("PatientID")).ToString,
                     GridView.GetFocusedRowCellValue(GridView.Columns("PatientName")).ToString,
                     CurrUser,
                     "Rejected",
                     GridView.GetFocusedRowCellValue(GridView.Columns("Request")).ToString,
                     GridView.GetFocusedRowCellValue(GridView.Columns("Section")).ToString,
                     GridView.GetFocusedRowCellValue(GridView.Columns("SubSection")).ToString)

    End Sub

    Private Sub btnReject_ItemClick(ByVal sender As Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnReject.ItemClick
        If MessageBox.Show("Are you sure you want to cancel " & GridView.GetFocusedRowCellValue(GridView.Columns("PatientName")) & " order?", "Confirm Cancel", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = vbYes Then
            frmCancelOR.ID = GridView.GetFocusedRowCellValue(GridView.Columns("SequenceNo")).ToString
            frmCancelOR.sID = GridView.GetFocusedRowCellValue(GridView.Columns("SampleID")).ToString
            frmCancelOR.pID = GridView.GetFocusedRowCellValue(GridView.Columns("PatientID")).ToString
            frmCancelOR.pName = GridView.GetFocusedRowCellValue(GridView.Columns("PatientName")).ToString
            frmCancelOR.pTest = GridView.GetFocusedRowCellValue(GridView.Columns("Request")).ToString
            frmCancelOR.pSection = GridView.GetFocusedRowCellValue(GridView.Columns("Section")).ToString
            frmCancelOR.pSubSection = GridView.GetFocusedRowCellValue(GridView.Columns("SubSection")).ToString
            frmCancelOR.ShowDialog()
        End If
        LoadRecords()

        ActivityLogs(GridView.GetFocusedRowCellValue(GridView.Columns("SampleID")).ToString,
                     GridView.GetFocusedRowCellValue(GridView.Columns("PatientID")).ToString,
                     GridView.GetFocusedRowCellValue(GridView.Columns("PatientName")).ToString,
                     CurrUser,
                     "Cancelled",
                     GridView.GetFocusedRowCellValue(GridView.Columns("Request")).ToString,
                     GridView.GetFocusedRowCellValue(GridView.Columns("Section")).ToString,
                     GridView.GetFocusedRowCellValue(GridView.Columns("SubSection")).ToString)

    End Sub

    Private Sub btnBarcode_ItemClick(ByVal sender As Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnBarcode.ItemClick
        PrintBarcode(GridView.GetFocusedRowCellValue(GridView.Columns("Request")).ToString,
                            GridView.GetFocusedRowCellValue(GridView.Columns("SampleID")).ToString,
                            GridView.GetFocusedRowCellValue(GridView.Columns("PatientID")).ToString,
                            GridView.GetFocusedRowCellValue(GridView.Columns("PatientName")).ToString,
                            GridView.GetFocusedRowCellValue(GridView.Columns("DateOfBirth")).ToString,
                            GridView.GetFocusedRowCellValue(GridView.Columns("Sex")).ToString,
                            GridView.GetFocusedRowCellValue(GridView.Columns("Section")).ToString,
                            GridView.GetFocusedRowCellValue(GridView.Columns("SubSection")),
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
    End Sub

    Private Sub frm_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        'MainFOrm.aceFecal.Appearance.Normal.BackColor = Color.FromArgb(6, 31, 71)
        MainFOrm.aceSerology.Appearance.Normal.BackColor = Color.FromArgb(16, 110, 190)
        MainFOrm.aceSerology.Appearance.Normal.ForeColor = Color.FromArgb(255, 255, 255)
    End Sub

    Private Sub frm_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        MainFOrm.lblTitle.Text = ""
        MainFOrm.aceSerology.Appearance.Normal.BackColor = Color.FromArgb(240, 240, 240)
        MainFOrm.aceSerology.Appearance.Normal.ForeColor = Color.FromArgb(27, 41, 62)
        Me.Dispose()
    End Sub

#Region "DisplayResults"
    '############################################-----For On-Queue Orders-----############################################
    Private Sub DisplayResult()
        'Try
        'On Error Resume Next
        frmSeroNew.cboPathologist.Properties.Items.Clear()
        frmSeroNew.cboMedTech.Properties.Items.Clear()
        frmSeroNew.cboVerify.Properties.Items.Clear()

        '###########################---Load Basic Patient Details---######################################################
        frmSeroNew.status = GridView.GetFocusedRowCellValue(GridView.Columns("Status")).ToString
        frmSeroNew.txtChargeSlip.Text = GridView.GetFocusedRowCellValue(GridView.Columns("ChargeSlip")).ToString
        frmSeroNew.mainID = GridView.GetFocusedRowCellValue(GridView.Columns("RefID")).ToString
        frmSeroNew.Section = GridView.GetFocusedRowCellValue(GridView.Columns("Section")).ToString
        frmSeroNew.SubSection = GridView.GetFocusedRowCellValue(GridView.Columns("SubSection")).ToString
        frmSeroNew.PatientID = GridView.GetFocusedRowCellValue(GridView.Columns("PatientID")).ToString
        frmSeroNew.txtSampleID.Text = GridView.GetFocusedRowCellValue(GridView.Columns("SampleID")).ToString
        frmSeroNew.txtPatientID.Text = GridView.GetFocusedRowCellValue(GridView.Columns("PatientID")).ToString
        frmSeroNew.txtName.Text = GridView.GetFocusedRowCellValue(GridView.Columns("PatientName")).ToString
        frmSeroNew.cboRequest.Text = GridView.GetFocusedRowCellValue(GridView.Columns("Request")).ToString
        frmSeroNew.dtReceived.Value = Format(CDate(GridView.GetFocusedRowCellValue(GridView.Columns("DateReceived")).ToString), "MM/dd/yyyy")
        frmSeroNew.tmTimeReceived.Text = Format(CDate(GridView.GetFocusedRowCellValue(GridView.Columns("DateReceived")).ToString), "hh:mm:ss tt")
        frmSeroNew.cboSex.Text = GridView.GetFocusedRowCellValue(GridView.Columns("Sex")).ToString
        frmSeroNew.dtBDate.Text = GridView.GetFocusedRowCellValue(GridView.Columns("DateOfBirth")).ToString
        frmSeroNew.cboPatientType.Text = GridView.GetFocusedRowCellValue(GridView.Columns("PatientType")).ToString
        frmSeroNew.cboPhysician.Text = GridView.GetFocusedRowCellValue(GridView.Columns("Physician")).ToString
        frmSeroNew.cboRoom.Text = GridView.GetFocusedRowCellValue(GridView.Columns("RoomWard")).ToString
        frmSeroNew.cboMedTech.Text = GridView.GetFocusedRowCellValue(GridView.Columns("PerformedBy")).ToString
        frmSeroNew.cboVerify.Text = GridView.GetFocusedRowCellValue(GridView.Columns("VerifiedBy")).ToString

        'For Age computation
        Dim Age As String = ""
        Age = GetBDate(GridView.GetFocusedRowCellValue(GridView.Columns("DateOfBirth")))
        frmSeroNew.txtAge.Text = Age.Split(" ").GetValue(0)
        frmSeroNew.txtClass.Text = Age.Split(" ").GetValue(1)
        '######################################----END-----###############################################################

        'Parameters 
        rs.Parameters.Clear()
        rs.Parameters.AddWithValue("@PatientID", GridView.GetFocusedRowCellValue(GridView.Columns("PatientID")).ToString)
        rs.Parameters.AddWithValue("@MainID", GridView.GetFocusedRowCellValue(GridView.Columns("RefID")).ToString)
        rs.Parameters.AddWithValue("@Section", GridView.GetFocusedRowCellValue(GridView.Columns("Section")).ToString)
        rs.Parameters.AddWithValue("@SubSection", GridView.GetFocusedRowCellValue(GridView.Columns("SubSection")).ToString)
        rs.Parameters.AddWithValue("@CurrUser", CurrUser)
        '###########################---Load Address and Contact No.---####################################################
        Connect()
        rs.Connection = conn
        rs.CommandType = CommandType.Text
        rs.CommandText = "SELECT `address`, `contact_no`, `civil_status` FROM `patient_info` WHERE `patient_id` = @PatientID"
        reader = rs.ExecuteReader
        reader.Read()
        If reader.HasRows Then
            frmSeroNew.txtAddress.Text = reader(0).ToString
            frmSeroNew.txtContact.Text = reader(1).ToString
            frmSeroNew.cboCS.Text = reader(2).ToString
        End If
        Disconnect()
        '######################################----END-----###############################################################

        Connect()
        rs.Connection = conn
        rs.CommandType = CommandType.Text
        rs.CommandText = "SELECT `comment` FROM `lab_comment` WHERE `sample_id` = @MainID"
        reader = rs.ExecuteReader
        reader.Read()
        If reader.HasRows Then
            frmSeroNew.txtExtracted.Text = reader(0).ToString
            Disconnect()
        End If
        Disconnect()

        '###########################---Load Additional Info---####################################################
        Connect()
        rs.Connection = conn
            rs.CommandType = CommandType.Text
            rs.CommandText = "SELECT `accession_no`, `or_no`, `cs_no` FROM `additional_info` WHERE `sample_id` = @MainID AND section = @Section AND sub_section = @SubSection"
            reader = rs.ExecuteReader
            reader.Read()
            If reader.HasRows Then
                frmSeroNew.txtAccession.Text = reader(0).ToString
                frmSeroNew.txtORNo.Text = reader(1).ToString
                frmSeroNew.txtChargeSlip.Text = reader(2).ToString
            End If
            Disconnect()
            '######################################----END-----###############################################################

            '###########################---Load remarks and comments---####################################################
            Connect()
            rs.Connection = conn
            rs.CommandType = CommandType.Text
            rs.CommandText = "SELECT `remarks`, `diagnosis` FROM `patient_remarks` WHERE `sample_id` = @MainID AND section = @Section AND sub_section = @SubSection"
            reader = rs.ExecuteReader
            reader.Read()
        If reader.HasRows Then
            frmSeroNew.txtRemarks.Text = reader(0).ToString
            frmSeroNew.txtComment.Text = reader(1).ToString
        End If
        Disconnect()

        Connect()
        rs.Connection = conn
        rs.CommandType = CommandType.Text
        rs.CommandText = "SELECT `viewpathologist`.`name` FROM `viewpathologist` JOIN order_pathologist ON `viewpathologist`.`id`=`order_pathologist`.`pathologist_id` WHERE `sample_id` = @MainID"
        reader = rs.ExecuteReader
        While reader.Read
            frmSeroNew.cboPathologist.Text = reader(0).ToString
        End While
        Disconnect()

        '##############################------To enable necessary buttons-------#######################################################
        If GridView.GetFocusedRowCellValue(GridView.Columns("Status")) = "Validated" Then
            frmSeroNew.btnPrint.Enabled = False
            frmSeroNew.btnValidate.Enabled = True
            frmSeroNew.btnPrintNow.Enabled = True
        End If
        '############################----------End------------##############################################################

        'frmSeroNew.GetBDate()

        'frmSeroNew.cboPathologist.SelectedIndex = 0

        'Activity Logs
        ActivityLogs(GridView.GetFocusedRowCellValue(GridView.Columns("SampleID")),
                                 GridView.GetFocusedRowCellValue(GridView.Columns("PatientID")),
                                 GridView.GetFocusedRowCellValue(GridView.Columns("PatientName")),
                                 CurrUser,
                                 "View Result",
                                 GridView.GetFocusedRowCellValue(GridView.Columns("Request")),
                                 GridView.GetFocusedRowCellValue(GridView.Columns("Section")),
                                 GridView.GetFocusedRowCellValue(GridView.Columns("SubSection")))

            frmSeroNew.ShowDialog()
        'Catch ex As Exception
        '    MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '    Exit Sub
        'End Try
    End Sub
    '############################################--------------END------------############################################

    '############################################-----For Completed Orders-----############################################
    Private Sub DisplayResultCompleted()

        frmSeroOrdered.cboPathologist.Properties.Items.Clear()
        frmSeroOrdered.cboMedTech.Properties.Items.Clear()
        frmSeroOrdered.cboVerify.Properties.Items.Clear()

        '###########################---Load Basic Patient Details---######################################################
        frmSeroOrdered.mainID = GridCompleted.GetFocusedRowCellValue(GridCompleted.Columns("RefID")).ToString
        frmSeroOrdered.Section = GridCompleted.GetFocusedRowCellValue(GridCompleted.Columns("Section")).ToString
        frmSeroOrdered.SubSection = GridCompleted.GetFocusedRowCellValue(GridCompleted.Columns("SubSection")).ToString
        frmSeroOrdered.PatientID = GridCompleted.GetFocusedRowCellValue(GridCompleted.Columns("PatientID")).ToString
        frmSeroOrdered.txtSampleID.Text = GridCompleted.GetFocusedRowCellValue(GridCompleted.Columns("SampleID")).ToString
        frmSeroOrdered.txtPatientID.Text = GridCompleted.GetFocusedRowCellValue(GridCompleted.Columns("PatientID")).ToString
        frmSeroOrdered.txtName.Text = GridCompleted.GetFocusedRowCellValue(GridCompleted.Columns("PatientName")).ToString
        frmSeroOrdered.cboRequest.Text = GridCompleted.GetFocusedRowCellValue(GridCompleted.Columns("Request")).ToString
        frmSeroOrdered.dtReceived.Text = GridCompleted.GetFocusedRowCellValue(GridCompleted.Columns("DateReceived")).ToString
        frmSeroOrdered.tmTimeReceived.Text = Format(CDate(GridCompleted.GetFocusedRowCellValue(GridCompleted.Columns("DateReceived")).ToString), "hh:mm:ss tt")
        frmSeroOrdered.tmTimeReleased.Text = Format(CDate(GridCompleted.GetFocusedRowCellValue(GridCompleted.Columns("DateReleased")).ToString), "MM/dd/yyyy hh:mm:ss tt")
        frmSeroOrdered.cboSex.Text = GridCompleted.GetFocusedRowCellValue(GridCompleted.Columns("Sex")).ToString
        frmSeroOrdered.dtBDate.Text = GridCompleted.GetFocusedRowCellValue(GridCompleted.Columns("DateOfBirth")).ToString
        frmSeroOrdered.cboPatientType.Text = GridCompleted.GetFocusedRowCellValue(GridCompleted.Columns("PatientType")).ToString
        frmSeroOrdered.cboPhysician.Text = GridCompleted.GetFocusedRowCellValue(GridCompleted.Columns("Physician")).ToString
        frmSeroOrdered.cboRoom.Text = GridCompleted.GetFocusedRowCellValue(GridCompleted.Columns("RoomWard")).ToString
        frmSeroOrdered.cboMedTech.Text = GridCompleted.GetFocusedRowCellValue(GridCompleted.Columns("PerformedBy")).ToString
        frmHemaOrdered.cboVerify.Text = GridCompleted.GetFocusedRowCellValue(GridCompleted.Columns("VerifiedBy")).ToString
        frmHemaOrdered.txtChargeSlip.Text = GridCompleted.GetFocusedRowCellValue(GridCompleted.Columns("ChargeSlip")).ToString

        'For Age computation
        Dim Age As String = ""
        Age = GetBDate(GridCompleted.GetFocusedRowCellValue(GridCompleted.Columns("DateOfBirth")).ToString)
        frmSeroOrdered.txtAge.Text = Age.Split(" ").GetValue(0)
        frmSeroOrdered.txtClass.Text = Age.Split(" ").GetValue(1)
        '######################################----END-----###############################################################

        'Parameters 
        rs.Parameters.Clear()
        rs.Parameters.AddWithValue("@PatientID", GridCompleted.GetFocusedRowCellValue(GridCompleted.Columns("PatientID")).ToString)
        rs.Parameters.AddWithValue("@MainID", GridCompleted.GetFocusedRowCellValue(GridCompleted.Columns("RefID")).ToString)
        rs.Parameters.AddWithValue("@Section", GridCompleted.GetFocusedRowCellValue(GridCompleted.Columns("Section")).ToString)
        rs.Parameters.AddWithValue("@SubSection", GridCompleted.GetFocusedRowCellValue(GridCompleted.Columns("SubSection")).ToString)
        rs.Parameters.AddWithValue("@CurrUser", CurrUser)


        '###########################---Load Address and Contact No.---####################################################
        Connect()
        rs.Connection = conn
        rs.CommandType = CommandType.Text
        rs.CommandText = "SELECT `address`, `contact_no`, `civil_status` FROM `patient_info` WHERE `patient_id` = @PatientID"
        reader = rs.ExecuteReader
        reader.Read()
        If reader.HasRows Then
            frmSeroOrdered.txtAddress.Text = reader(0).ToString
            frmSeroOrdered.txtContact.Text = reader(1).ToString
            frmSeroOrdered.cboCS.Text = reader(2).ToString
        End If
        Disconnect()
        '######################################----END-----###############################################################

        '###########################---Load Additional Info---####################################################
        Connect()
        rs.Connection = conn
        rs.CommandType = CommandType.Text
        rs.CommandText = "SELECT `accession_no`, `or_no`, `cs_no` FROM `additional_info` WHERE `sample_id` = @MainID AND section = @Section AND sub_section = @SubSection"
        reader = rs.ExecuteReader
        reader.Read()
        If reader.HasRows Then
            frmSeroOrdered.txtAccession.Text = reader(0).ToString
            frmSeroOrdered.txtORNo.Text = reader(1).ToString
            frmSeroOrdered.txtChargeSlip.Text = reader(2).ToString
        End If
        Disconnect()
        '######################################----END-----###############################################################

        '###########################---Load remarks and comments---####################################################
        Connect()
        rs.Connection = conn
        rs.CommandType = CommandType.Text
        rs.CommandText = "SELECT `remarks`, `diagnosis` FROM `patient_remarks` WHERE `sample_id` = @MainID AND section = @Section AND sub_section = @SubSection"
        reader = rs.ExecuteReader
        reader.Read()
        If reader.HasRows Then
            frmSeroOrdered.txtRemarks.Text = reader(0).ToString
            frmSeroOrdered.txtComment.Text = reader(1).ToString
        End If
        Disconnect()
        '######################################----END-----###############################################################

        Connect()
        rs.Connection = conn
        rs.CommandType = CommandType.Text
        rs.CommandText = "SELECT `viewpathologist`.`name` FROM `viewpathologist` JOIN order_pathologist ON `viewpathologist`.`id`=`order_pathologist`.`pathologist_id` WHERE `sample_id` = @MainID"
        reader = rs.ExecuteReader
        While reader.Read
            frmSeroOrdered.cboPathologist.Text = reader(0).ToString
        End While
        Disconnect()

        'Activity Logs
        ActivityLogs(GridCompleted.GetFocusedRowCellValue(GridCompleted.Columns("SampleID")),
                            GridCompleted.GetFocusedRowCellValue(GridCompleted.Columns("PatientID")),
                            GridCompleted.GetFocusedRowCellValue(GridCompleted.Columns("PatientName")),
                            CurrUser,
                            "View Archived Result",
                            GridCompleted.GetFocusedRowCellValue(GridCompleted.Columns("Request")),
                            GridCompleted.GetFocusedRowCellValue(GridCompleted.Columns("Section")),
                            GridCompleted.GetFocusedRowCellValue(GridCompleted.Columns("SubSection")))
        frmSeroOrdered.ShowDialog()
    End Sub
    '############################################--------------END------------############################################

    Public Sub Gridview_Column_Width()
        GridView.Columns("SequenceNo").Width = 95
        GridView.Columns("Status").Width = 78
        GridView.Columns("SampleID").Width = 75
        GridView.Columns("PatientID").Width = 75
        GridView.Columns("PatientName").Width = 150
        GridView.Columns("Request").Width = 95
        GridView.Columns("DateOfBirth").Width = 95
        GridView.Columns("Sex").Width = 55
        GridView.Columns("RoomWard").Width = 65
        GridView.Columns("Physician").Width = 40
        GridView.Columns("DateRequested").Width = 80
        GridView.Columns("TimeRequested").Width = 85
        GridView.Columns("DateReceived").Width = 85
    End Sub

#End Region

End Class