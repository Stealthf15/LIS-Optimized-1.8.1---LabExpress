Imports System.IO
Imports System.IO.Ports
Imports System.Threading
Imports System.Xml
Imports System.Text.RegularExpressions
Imports System.Drawing.Printing
Imports DevExpress.XtraPrinting.BarCode
Imports DevExpress.XtraGrid.Views.Grid

Public Class frmMicroWorklist

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

    'For Worklist
    Dim SequenceNo, Status, SampleID, PatientID, PatientName, PatientDoB, PatientAge, PatientSex, Test, ResultDate, ResultTime As String

    'For Result
    Dim ResultName, ResultValue, ResultFlag, ResultOrder, ResultCode, ResultUnit, ResultUnitConv, ResultID, ResultGroup As String

    Private PrintDocType As String = "Barcode"
    Private StrPrinterName As String = My.Settings.BCPrinterName

    Public Sub LoadRecords()
        Try
            GridView.Columns.Clear()
            GridView.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
            GridView.Appearance.HeaderPanel.FontStyleDelta = FontStyle.Bold

            'cboLocation.Text = "All" 'CurrDept

            If (cboLocation.Text = "All") Then

                Dim SQL As String = "SELECT
                        `tmpWorklist`.`id` AS SequenceNo,
                        `tmpWorklist`.`status` AS Status,
                        `tmpWorklist`.`sample_id` AS SampleID,
                        `tmpWorklist`.`patient_id` AS PatientID,
                        `tmpWorklist`.`patient_name` AS PatientName, 
                        `tmpWorklist`.`test` AS Request,
                        `tmpWorklist`.`bdate` AS DateOfBirth,
                        `tmpWorklist`.`sex` AS Sex,
                        `tmpWorklist`.`age` AS Age,
                        `tmpWorklist`.`dept` AS RoomWard,
                        `tmpWorklist`.`physician` AS Physician,
                        `tmpWorklist`.`medtech` AS PerformedBy,
                        `tmpWorklist`.`verified_by` AS ReleasedBy,
                        DATE_FORMAT(`tmpWorklist`.`date`, '%m/%d/%Y') AS DateReceived,
                        `tmpWorklist`.`time` AS TimeReceived,
                        `tmpWorklist`.`testtype` AS Section,
                        `tmpWorklist`.`sub_section` AS SubSection,
                        DATE_FORMAT(`specimen_tracking`.`extracted`, '%m/%d/%Y %r') AS DateCheckedIn, 
                        `tmpWorklist`.`main_id` AS RefID,
                        `tmpWorklist`.`patient_type` AS PatientType
                        FROM `tmpWorklist` 
                        LEFT JOIN `specimen_tracking` ON
	                        `specimen_tracking`.`sample_id` = `tmpWorklist`.`main_id`
                        WHERE (`tmpWorklist`.`status` = 'Result Received' OR `tmpWorklist`.`status` = 'Validated' OR `tmpWorklist`.`status` = 'Processing') 
                        AND (`tmpWorklist`.`testtype` = `specimen_tracking`.`section`)
                        AND (`tmpWorklist`.`sub_section` = `specimen_tracking`.`sub_section`)
                        AND (`tmpWorklist`.`testtype` = 'Microbiology')
                        ORDER BY `tmpWorklist`.`main_id` DESC"

                Dim command As New MySql.Data.MySqlClient.MySqlCommand(SQL, conn)

                command.Parameters.Clear()

                Dim adapter As New MySql.Data.MySqlClient.MySqlDataAdapter(command)

                Dim myTable As DataTable = New DataTable
                adapter.Fill(myTable)

                dtList.DataSource = myTable

            Else

                Dim SQL As String = "SELECT
                        `tmpWorklist`.`id` AS SequenceNo,
                        `tmpWorklist`.`status` AS Status,
                        `tmpWorklist`.`sample_id` AS SampleID,
                        `tmpWorklist`.`patient_id` AS PatientID,
                        `tmpWorklist`.`patient_name` AS PatientName, 
                        `tmpWorklist`.`test` AS Request,
                        `tmpWorklist`.`bdate` AS DateOfBirth,
                        `tmpWorklist`.`sex` AS Sex,
                        `tmpWorklist`.`age` AS Age,
                        `tmpWorklist`.`dept` AS RoomWard,
                        `tmpWorklist`.`physician` AS Physician,
                        `tmpWorklist`.`medtech` AS PerformedBy,
                        `tmpWorklist`.`verified_by` AS ReleasedBy,
                        DATE_FORMAT(`tmpWorklist`.`date`, '%m/%d/%Y') AS DateReceived,
                        `tmpWorklist`.`time` AS TimeReceived,
                        `tmpWorklist`.`testtype` AS Section,
                        `tmpWorklist`.`sub_section` AS SubSection,
                        DATE_FORMAT(`specimen_tracking`.`extracted`, '%m/%d/%Y %r') AS DateCheckedIn, 
                        `tmpWorklist`.`main_id` AS RefID,
                        `tmpWorklist`.`patient_type` AS PatientType
                        FROM `tmpWorklist` 
                        LEFT JOIN `specimen_tracking` ON
	                        `specimen_tracking`.`sample_id` = `tmpWorklist`.`main_id`
                        WHERE (`tmpWorklist`.`status` = 'Result Received' OR `tmpWorklist`.`status` = 'Validated' OR `tmpWorklist`.`status` = 'Processing') 
                        AND (`tmpWorklist`.`testtype` = `specimen_tracking`.`section`)
                        AND (`tmpWorklist`.`sub_section` = `specimen_tracking`.`sub_section`)
                        AND (`tmpWorklist`.`testtype` = 'Microbiology')
                        AND (`tmpWorklist`.`location` = @Location)
                        ORDER BY `tmpWorklist`.`main_id` DESC"

                Dim command As New MySql.Data.MySqlClient.MySqlCommand(SQL, conn)

                command.Parameters.Clear()
                command.Parameters.AddWithValue("@Location", cboLocation.Text)

                Dim adapter As New MySql.Data.MySqlClient.MySqlDataAdapter(command)

                Dim myTable As DataTable = New DataTable
                adapter.Fill(myTable)

                dtList.DataSource = myTable

            End If
            GridView.Columns("PerformedBy").Visible = False
            GridView.Columns("ReleasedBy").Visible = False
            GridView.Columns("PatientType").Visible = False
            GridView.Columns("RefID").Visible = False
            GridView.Columns("Section").Visible = False
            GridView.Columns("SubSection").Visible = False
            GridView.Columns("Age").Visible = False

            ' Make the grid read-only. 
            GridView.OptionsBehavior.Editable = False
            ' Prevent the focused cell from being highlighted. 
            GridView.OptionsSelection.EnableAppearanceFocusedCell = False
            ' Draw a dotted focus rectangle around the entire row. 
            GridView.FocusRectStyle = DrawFocusRectStyle.RowFullFocus

            GridView.OptionsSelection.MultiSelect = True
            GridView.OptionsSelection.MultiSelectMode = GridMultiSelectMode.RowSelect

            'GridView.BestFitColumns()
            'GridView.Columns("PatientName").Width = 500
        Catch ex As Exception
            MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub GridView_RowCellStyle(sender As Object, e As RowCellStyleEventArgs) Handles GridView.RowCellStyle
        Dim view As GridView = TryCast(sender, GridView)
        If (e.Column.FieldName = "ID") Or (e.Column.FieldName = "Status") Then
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
                        AND (`order`.`testtype` = 'Microbiology')
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
                        AND (`order`.`testtype` = 'Microbiology')
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
        If (e.Column.FieldName = "ID") Or (e.Column.FieldName = "Status") Then
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

        LoadRecords()
        LoadRecordsCompleted()
    End Sub

    Private Sub btnView_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnView.ItemClick
        If XTab.SelectedTabPageIndex = 0 Then
            Dim selectedRows() As Integer = GridView.GetSelectedRows()
            For Each rowHandle As Integer In selectedRows
                If rowHandle >= 0 Then
                    'If Not GridView.GetFocusedRowCellValue(GridView.Columns("Status")) = "Processing" Then
                    '    DisplayResult()
                    'End If
                    DisplayResult()
                End If
            Next rowHandle
        ElseIf XTab.SelectedTabPageIndex = 1 Then
            DisplayResultCompleted()
        End If
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.ItemClick
        LoadRecords()
        LoadRecordsCompleted()
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.ItemClick
        Me.Close()
        Me.Dispose()
    End Sub

    Private Sub dtTo_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtTo.ValueChanged, dtFrom.ValueChanged
        LoadRecords()
    End Sub

    Private Sub cboLimit_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboLocation.SelectedIndexChanged, txtSearch.TextChanged
        Try
            GridView.Columns.Clear()
            GridView.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
            GridView.Appearance.HeaderPanel.FontStyleDelta = FontStyle.Bold

            If (txtSearch.Text = "") Then

                If (cboLocation.Text = "All") Then

                    Dim SQL As String = "SELECT
                        `tmpWorklist`.`id` AS SequenceNo,
                        `tmpWorklist`.`status` AS Status,
                        `tmpWorklist`.`sample_id` AS SampleID,
                        `tmpWorklist`.`patient_id` AS PatientID,
                        `tmpWorklist`.`patient_name` AS PatientName, 
                        `tmpWorklist`.`test` AS Request,
                        `tmpWorklist`.`bdate` AS DateOfBirth,
                        `tmpWorklist`.`sex` AS Sex,
                        `tmpWorklist`.`age` AS Age,
                        `tmpWorklist`.`dept` AS RoomWard,
                        `tmpWorklist`.`physician` AS Physician,
                        `tmpWorklist`.`medtech` AS PerformedBy,
                        `tmpWorklist`.`verified_by` AS ReleasedBy,
                        DATE_FORMAT(`tmpWorklist`.`date`, '%m/%d/%Y') AS DateReceived,
                        `tmpWorklist`.`time` AS TimeReceived,
                        `tmpWorklist`.`testtype` AS Section,
                        `tmpWorklist`.`sub_section` AS SubSection,
                        DATE_FORMAT(`specimen_tracking`.`extracted`, '%m/%d/%Y %r') AS DateCheckedIn, 
                        `tmpWorklist`.`main_id` AS RefID,
                        `tmpWorklist`.`patient_type` AS PatientType
                        FROM `tmpWorklist` 
                        LEFT JOIN `specimen_tracking` ON
	                        `specimen_tracking`.`sample_id` = `tmpWorklist`.`main_id`
                        WHERE (`tmpWorklist`.`status` = 'Result Received' OR `tmpWorklist`.`status` = 'Validated' OR `tmpWorklist`.`status` = 'Processing') 
                        AND (`tmpWorklist`.`testtype` = `specimen_tracking`.`section`)
                        AND (`tmpWorklist`.`sub_section` = `specimen_tracking`.`sub_section`)
                        AND (`tmpWorklist`.`testtype` = 'Microbiology')
                        ORDER BY `tmpWorklist`.`main_id` DESC"

                    Dim command As New MySql.Data.MySqlClient.MySqlCommand(SQL, conn)

                    command.Parameters.Clear()

                    Dim adapter As New MySql.Data.MySqlClient.MySqlDataAdapter(command)

                    Dim myTable As DataTable = New DataTable
                    adapter.Fill(myTable)

                    dtList.DataSource = myTable

                Else

                    Dim SQL As String = "SELECT
                        `tmpWorklist`.`id` AS SequenceNo,
                        `tmpWorklist`.`status` AS Status,
                        `tmpWorklist`.`sample_id` AS SampleID,
                        `tmpWorklist`.`patient_id` AS PatientID,
                        `tmpWorklist`.`patient_name` AS PatientName, 
                        `tmpWorklist`.`test` AS Request,
                        `tmpWorklist`.`bdate` AS DateOfBirth,
                        `tmpWorklist`.`sex` AS Sex,
                        `tmpWorklist`.`age` AS Age,
                        `tmpWorklist`.`dept` AS RoomWard,
                        `tmpWorklist`.`physician` AS Physician,
                        `tmpWorklist`.`medtech` AS PerformedBy,
                        `tmpWorklist`.`verified_by` AS ReleasedBy,
                        DATE_FORMAT(`tmpWorklist`.`date`, '%m/%d/%Y') AS DateReceived,
                        `tmpWorklist`.`time` AS TimeReceived,
                        `tmpWorklist`.`testtype` AS Section,
                        `tmpWorklist`.`sub_section` AS SubSection,
                        DATE_FORMAT(`specimen_tracking`.`extracted`, '%m/%d/%Y %r') AS DateCheckedIn, 
                        `tmpWorklist`.`main_id` AS RefID,
                        `tmpWorklist`.`patient_type` AS PatientType
                        FROM `tmpWorklist` 
                        LEFT JOIN `specimen_tracking` ON
	                        `specimen_tracking`.`sample_id` = `tmpWorklist`.`main_id`
                        WHERE (`tmpWorklist`.`status` = 'Result Received' OR `tmpWorklist`.`status` = 'Validated' OR `tmpWorklist`.`status` = 'Processing') 
                        AND (`tmpWorklist`.`testtype` = `specimen_tracking`.`section`)
                        AND (`tmpWorklist`.`sub_section` = `specimen_tracking`.`sub_section`)
                        AND (`tmpWorklist`.`testtype` = 'Microbiology')
                        AND (`tmpWorklist`.`location` = @Location)
                        ORDER BY `tmpWorklist`.`main_id` DESC"

                    Dim command As New MySql.Data.MySqlClient.MySqlCommand(SQL, conn)

                    command.Parameters.Clear()
                    command.Parameters.AddWithValue("@Location", cboLocation.Text)

                    Dim adapter As New MySql.Data.MySqlClient.MySqlDataAdapter(command)

                    Dim myTable As DataTable = New DataTable
                    adapter.Fill(myTable)

                    dtList.DataSource = myTable
                End If

            Else

                If (cboLocation.Text = "All") Then

                    Dim SQL As String = "SELECT * FROM (SELECT
                        `tmpWorklist`.`id` AS SequenceNo,
                        `tmpWorklist`.`status` AS Status,
                        `tmpWorklist`.`sample_id` AS SampleID,
                        `tmpWorklist`.`patient_id` AS PatientID,
                        `tmpWorklist`.`patient_name` AS PatientName, 
                        `tmpWorklist`.`test` AS Request,
                        `tmpWorklist`.`bdate` AS DateOfBirth,
                        `tmpWorklist`.`sex` AS Sex,
                        `tmpWorklist`.`age` AS Age,
                        `tmpWorklist`.`dept` AS RoomWard,
                        `tmpWorklist`.`physician` AS Physician,
                        `tmpWorklist`.`medtech` AS PerformedBy,
                        `tmpWorklist`.`verified_by` AS ReleasedBy,
                        DATE_FORMAT(`tmpWorklist`.`date`, '%m/%d/%Y') AS DateReceived,
                        `tmpWorklist`.`time` AS TimeReceived,
                        `tmpWorklist`.`testtype` AS Section,
                        `tmpWorklist`.`sub_section` AS SubSection,
                        DATE_FORMAT(`specimen_tracking`.`extracted`, '%m/%d/%Y %r') AS DateCheckedIn, 
                        `tmpWorklist`.`main_id` AS RefID,
                        `tmpWorklist`.`patient_type` AS PatientType
                        FROM `tmpWorklist` 
                        LEFT JOIN `specimen_tracking` ON
	                        `specimen_tracking`.`sample_id` = `tmpWorklist`.`main_id`
                        WHERE (`tmpWorklist`.`status` = 'Result Received' OR `tmpWorklist`.`status` = 'Validated' OR `tmpWorklist`.`status` = 'Processing') 
                        AND (`tmpWorklist`.`testtype` = `specimen_tracking`.`section`)
                        AND (`tmpWorklist`.`sub_section` = `specimen_tracking`.`sub_section`)
                        AND (`tmpWorklist`.`testtype` = 'Microbiology')
                        ORDER BY `tmpWorklist`.`main_id` DESC) AS result WHERE SampleID LIKE '" & txtSearch.Text & "%' OR PatientID LIKE '" & txtSearch.Text & "%' OR PatientName LIKE '" & txtSearch.Text & "%' OR Physician LIKE '" & txtSearch.Text & "%'"

                    Dim command As New MySql.Data.MySqlClient.MySqlCommand(SQL, conn)

                    command.Parameters.Clear()

                    Dim adapter As New MySql.Data.MySqlClient.MySqlDataAdapter(command)

                    Dim myTable As DataTable = New DataTable
                    adapter.Fill(myTable)

                    dtList.DataSource = myTable

                Else

                    Dim SQL As String = "SELECT * FROM (SELECT
                        `tmpWorklist`.`id` AS SequenceNo,
                        `tmpWorklist`.`status` AS Status,
                        `tmpWorklist`.`sample_id` AS SampleID,
                        `tmpWorklist`.`patient_id` AS PatientID,
                        `tmpWorklist`.`patient_name` AS PatientName, 
                        `tmpWorklist`.`test` AS Request,
                        `tmpWorklist`.`bdate` AS DateOfBirth,
                        `tmpWorklist`.`sex` AS Sex,
                        `tmpWorklist`.`age` AS Age,
                        `tmpWorklist`.`dept` AS RoomWard,
                        `tmpWorklist`.`physician` AS Physician,
                        `tmpWorklist`.`medtech` AS PerformedBy,
                        `tmpWorklist`.`verified_by` AS ReleasedBy,
                        DATE_FORMAT(`tmpWorklist`.`date`, '%m/%d/%Y') AS DateReceived,
                        `tmpWorklist`.`time` AS TimeReceived,
                        `tmpWorklist`.`testtype` AS Section,
                        `tmpWorklist`.`sub_section` AS SubSection,
                        DATE_FORMAT(`specimen_tracking`.`extracted`, '%m/%d/%Y %r') AS DateCheckedIn, 
                        `tmpWorklist`.`main_id` AS RefID,
                        `tmpWorklist`.`patient_type` AS PatientType
                        FROM `tmpWorklist` 
                        LEFT JOIN `specimen_tracking` ON
	                        `specimen_tracking`.`sample_id` = `tmpWorklist`.`main_id`
                        WHERE (`tmpWorklist`.`status` = 'Result Received' OR `tmpWorklist`.`status` = 'Validated' OR `tmpWorklist`.`status` = 'Processing') 
                        AND (`tmpWorklist`.`testtype` = `specimen_tracking`.`section`)
                        AND (`tmpWorklist`.`sub_section` = `specimen_tracking`.`sub_section`)
                        AND (`tmpWorklist`.`testtype` = 'Microbiology')
                        AND (`tmpWorklist`.`location` = @Location)
                        ORDER BY `tmpWorklist`.`main_id` DESC) AS result WHERE SampleID LIKE '" & txtSearch.Text & "%' OR PatientID LIKE '" & txtSearch.Text & "%' OR PatientName LIKE '" & txtSearch.Text & "%' OR Physician LIKE '" & txtSearch.Text & "%'"

                    Dim command As New MySql.Data.MySqlClient.MySqlCommand(SQL, conn)

                    command.Parameters.Clear()
                    command.Parameters.AddWithValue("@Location", cboLocation.Text)

                    Dim adapter As New MySql.Data.MySqlClient.MySqlDataAdapter(command)

                    Dim myTable As DataTable = New DataTable
                    adapter.Fill(myTable)

                    dtList.DataSource = myTable
                End If

            End If

            GridView.Columns("PerformedBy").Visible = False
            GridView.Columns("ReleasedBy").Visible = False
            GridView.Columns("PatientType").Visible = False
            GridView.Columns("RefID").Visible = False
            GridView.Columns("Section").Visible = False
            GridView.Columns("SubSection").Visible = False
            GridView.Columns("Age").Visible = False

            ' Make the grid read-only. 
            GridView.OptionsBehavior.Editable = False
            ' Prevent the focused cell from being highlighted. 
            GridView.OptionsSelection.EnableAppearanceFocusedCell = False
            ' Draw a dotted focus rectangle around the entire row. 
            GridView.FocusRectStyle = DrawFocusRectStyle.RowFullFocus

            GridView.OptionsSelection.MultiSelect = True
            GridView.OptionsSelection.MultiSelectMode = GridMultiSelectMode.RowSelect
        Catch ex As Exception
            MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

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
                        AND (`order`.`testtype` = 'Microbiology')
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
                        AND (`order`.`testtype` = 'Microbiology')
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

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        LoadRecordsCompleted()
    End Sub

    Private Sub GridView_DoubleClick(sender As Object, e As EventArgs) Handles GridView.DoubleClick
        Dim selectedRows() As Integer = GridView.GetSelectedRows()
        For Each rowHandle As Integer In selectedRows
            If rowHandle >= 0 Then
                'If Not GridView.GetFocusedRowCellValue(GridView.Columns("Status")) = "Processing" Then
                '    DisplayResult()
                'End If
                DisplayResult()
            End If
        Next rowHandle
    End Sub

    Private Sub GridCompleted_DoubleClick(sender As Object, e As EventArgs) Handles GridCompleted.DoubleClick
        DisplayResultCompleted()
    End Sub

    Private Sub XTab_SelectedPageChanged(ByVal sender As Object, ByVal e As DevExpress.XtraTab.TabPageChangedEventArgs) Handles XTab.SelectedPageChanged
        If XTab.SelectedTabPageIndex = 0 Then
            btnDelete.Enabled = True
            btnView.Enabled = True
            btnRefresh.Enabled = True
            btnBarcode.Enabled = True
            btnPrint.Enabled = False
            lblCountQueue.Text = "Record Count: " & GridView.RowCount
        ElseIf XTab.SelectedTabPageIndex = 1 Then
            btnView.Enabled = True
            btnRefresh.Enabled = True
            btnDelete.Enabled = False
            btnBarcode.Enabled = False
            btnPrint.Enabled = True
            lblCountQueue.Text = "Record Count: " & GridCompleted.RowCount
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
            Dim selectedRows() As Integer = GridView.GetSelectedRows()
            For Each rowHandle As Integer In selectedRows
                If rowHandle >= 0 Then
                    If XTab.SelectedTabPageIndex = 0 Then

                    ElseIf XTab.SelectedTabPageIndex = 1 Then
                        Using myRDLCPrinter As New RDLCPrinterPrintReleased(GridCompleted.GetFocusedRowCellValue(GridCompleted.Columns("RefID")), GridCompleted.GetFocusedRowCellValue(GridCompleted.Columns("Section")), GridCompleted.GetFocusedRowCellValue(GridCompleted.Columns("SubSection")), "", My.Settings.DefaultPrinter)
                            If My.Settings.SaveAsPDF Then
                                Dim byteViewer As Byte() = myRDLCPrinter.LocalReport.Render("PDF")
                                Dim saveFileDialog1 As New SaveFileDialog()
                                saveFileDialog1.Filter = "*PDF files (*.pdf)|*.pdf"
                                saveFileDialog1.FilterIndex = 2
                                saveFileDialog1.RestoreDirectory = True
                                Dim newFile As New FileStream(My.Settings.PDFLocation & GridCompleted.GetFocusedRowCellValue(GridCompleted.Columns("SampleID")) & "_" & GridCompleted.GetFocusedRowCellValue(GridCompleted.Columns("PatientName")) & ".pdf", FileMode.Create)
                                newFile.Write(byteViewer, 0, byteViewer.Length)
                                newFile.Close()

                                myRDLCPrinter.Print(1)
                            Else
                                myRDLCPrinter.Print(1)
                            End If
                        End Using
                        'Activity Logs
                        ActivityLogs(GridCompleted.GetFocusedRowCellValue(GridCompleted.Columns("SampleID")),
                                     GridCompleted.GetFocusedRowCellValue(GridCompleted.Columns("PatientID")),
                                     GridCompleted.GetFocusedRowCellValue(GridCompleted.Columns("PatientName")),
                                     CurrUser,
                                     "Print Result",
                                     GridCompleted.GetFocusedRowCellValue(GridCompleted.Columns("Request")),
                                     GridCompleted.GetFocusedRowCellValue(GridCompleted.Columns("Section")),
                                     GridCompleted.GetFocusedRowCellValue(GridCompleted.Columns("SubSection")))
                    End If
                End If
            Next rowHandle
        Catch ex As Exception
            MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try
    End Sub

    Private Sub btnDelete_ItemClick(ByVal sender As Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnDelete.ItemClick
        Dim selectedRows() As Integer = GridView.GetSelectedRows()
        For Each rowHandle As Integer In selectedRows
            If rowHandle >= 0 Then
                If MessageBox.Show("Are you sure you want to reject " & GridView.GetFocusedRowCellValue(GridView.Columns("PatientName")) & " order?", "Confirm Reject", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = vbYes Then
                    frmRejectOrder.ID = GridView.GetFocusedRowCellValue(GridView.Columns("SequenceNo"))
                    frmRejectOrder.sID = GridView.GetFocusedRowCellValue(GridView.Columns("SampleID"))
                    frmRejectOrder.pID = GridView.GetFocusedRowCellValue(GridView.Columns("PatientID"))
                    frmRejectOrder.pName = GridView.GetFocusedRowCellValue(GridView.Columns("PatientName"))
                    frmRejectOrder.pTest = GridView.GetFocusedRowCellValue(GridView.Columns("Request"))
                    frmRejectOrder.pSection = GridView.GetFocusedRowCellValue(GridView.Columns("Section"))
                    frmRejectOrder.pSubSection = GridView.GetFocusedRowCellValue(GridView.Columns("SubSection"))
                    frmRejectOrder.ShowDialog()
                End If
            End If
        Next rowHandle

        LoadRecords()
    End Sub

    Private Sub btnReject_ItemClick(ByVal sender As Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnReject.ItemClick
        Dim selectedRows() As Integer = GridView.GetSelectedRows()
        For Each rowHandle As Integer In selectedRows
            If rowHandle >= 0 Then
                If MessageBox.Show("Are you sure you want to cancel " & GridView.GetFocusedRowCellValue(GridView.Columns("PatientName")) & " order?", "Confirm Cancel", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = vbYes Then
                    frmCancelOR.ID = GridView.GetFocusedRowCellValue(GridView.Columns("SequenceNo"))
                    frmCancelOR.sID = GridView.GetFocusedRowCellValue(GridView.Columns("SampleID"))
                    frmCancelOR.pID = GridView.GetFocusedRowCellValue(GridView.Columns("PatientID"))
                    frmCancelOR.pName = GridView.GetFocusedRowCellValue(GridView.Columns("PatientName"))
                    frmCancelOR.pTest = GridView.GetFocusedRowCellValue(GridView.Columns("Request"))
                    frmCancelOR.pSection = GridView.GetFocusedRowCellValue(GridView.Columns("Section"))
                    frmCancelOR.pSubSection = GridView.GetFocusedRowCellValue(GridView.Columns("SubSection"))
                    frmCancelOR.ShowDialog()
                End If
            End If
        Next rowHandle
        LoadRecords()
    End Sub

    Private Sub btnBarcode_ItemClick(ByVal sender As Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnBarcode.ItemClick
        Dim selectedRows() As Integer = GridView.GetSelectedRows()
        For Each rowHandle As Integer In selectedRows
            If rowHandle >= 0 Then
                PrintBarcode(GridView.GetFocusedRowCellValue(GridView.Columns("Request")),
                            GridView.GetFocusedRowCellValue(GridView.Columns("SampleID")),
                            GridView.GetFocusedRowCellValue(GridView.Columns("PatientID")),
                            GridView.GetFocusedRowCellValue(GridView.Columns("PatientName")),
                            GridView.GetFocusedRowCellValue(GridView.Columns("DateOfBirth")),
                            GridView.GetFocusedRowCellValue(GridView.Columns("Sex")),
                            GridView.GetFocusedRowCellValue(GridView.Columns("Section")),
                            GridView.GetFocusedRowCellValue(GridView.Columns("SubSection")),
                            Split(GridView.GetFocusedRowCellValue(GridView.Columns("RoomWard")), "^")(0),
                            Split(GridView.GetFocusedRowCellValue(GridView.Columns("RoomWard")), "^")(1),
                            CurrUsername)
                'Activity Logs
                ActivityLogs(GridView.GetFocusedRowCellValue(GridView.Columns("SampleID")),
                             GridView.GetFocusedRowCellValue(GridView.Columns("PatientID")),
                             GridView.GetFocusedRowCellValue(GridView.Columns("PatientName")),
                             CurrUser,
                             "Reprint Barcode",
                             GridView.GetFocusedRowCellValue(GridView.Columns("Request")),
                             GridView.GetFocusedRowCellValue(GridView.Columns("Section")),
                             GridView.GetFocusedRowCellValue(GridView.Columns("SubSection")))
            End If
        Next rowHandle
    End Sub

    Private Sub frm_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        'MainFOrm.aceFecal.Appearance.Normal.BackColor = Color.FromArgb(6, 31, 71)
        MainFOrm.aceMicrobiology.Appearance.Normal.BackColor = Color.FromArgb(16, 110, 190)
        MainFOrm.aceMicrobiology.Appearance.Normal.ForeColor = Color.FromArgb(255, 255, 255)
    End Sub

    Private Sub frm_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        MainFOrm.lblTitle.Text = ""
        MainFOrm.aceMicrobiology.Appearance.Normal.BackColor = Color.FromArgb(240, 240, 240)
        MainFOrm.aceMicrobiology.Appearance.Normal.ForeColor = Color.FromArgb(27, 41, 62)
        Me.Dispose()
    End Sub

#Region "DisplayResults"
    '############################################-----For On-Queue Orders-----############################################
    Private Sub DisplayResult()
        'Dim selectedRows() As Integer = GridView.GetSelectedRows()
        'For Each rowHandle As Integer In selectedRows
        '    If rowHandle >= 0 Then
        'On Error Resume Next
        frmMicroNew.cboPathologist.Properties.Items.Clear()
                frmMicroNew.cboMedTech.Properties.Items.Clear()
                frmMicroNew.cboVerify.Properties.Items.Clear()

                '###########################---Load Basic Patient Details---######################################################
                frmMicroNew.mainID = GridView.GetFocusedRowCellValue(GridView.Columns("RefID")).ToString
                frmMicroNew.Section = GridView.GetFocusedRowCellValue(GridView.Columns("Section")).ToString
                frmMicroNew.SubSection = GridView.GetFocusedRowCellValue(GridView.Columns("SubSection")).ToString
                frmMicroNew.PatientID = GridView.GetFocusedRowCellValue(GridView.Columns("PatientID")).ToString
                frmMicroNew.txtSampleID.Text = GridView.GetFocusedRowCellValue(GridView.Columns("SampleID")).ToString
                frmMicroNew.txtPatientID.Text = GridView.GetFocusedRowCellValue(GridView.Columns("PatientID")).ToString
                frmMicroNew.txtName.Text = GridView.GetFocusedRowCellValue(GridView.Columns("PatientName")).ToString
                frmMicroNew.cboRequest.Text = GridView.GetFocusedRowCellValue(GridView.Columns("Request")).ToString
                frmMicroNew.dtReceived.Value = GridView.GetFocusedRowCellValue(GridView.Columns("DateReceived")).ToString
                frmMicroNew.tmTimeReceived.Text = GridView.GetFocusedRowCellValue(GridView.Columns("TimeReceived")).ToString
                frmMicroNew.cboSex.Text = GridView.GetFocusedRowCellValue(GridView.Columns("Sex")).ToString
                frmMicroNew.dtBDate.Text = GridView.GetFocusedRowCellValue(GridView.Columns("DateOfBirth")).ToString
                frmMicroNew.cboPatientType.Text = GridView.GetFocusedRowCellValue(GridView.Columns("PatientType")).ToString
                frmMicroNew.cboPhysician.Text = GridView.GetFocusedRowCellValue(GridView.Columns("Physician")).ToString
                frmMicroNew.cboRoom.Text = GridView.GetFocusedRowCellValue(GridView.Columns("RoomWard")).ToString
                frmMicroNew.cboMedTech.Text = GridView.GetFocusedRowCellValue(GridView.Columns("PerformedBy")).ToString
                frmMicroNew.cboVerify.Text = GridView.GetFocusedRowCellValue(GridView.Columns("ReleasedBy")).ToString
        'frmMicroNew.cboPhysician.Text = GridView.GetFocusedRowCellValue(GridView.Columns("Physician")).ToString

        'For Age computation
        Dim Age As String = ""
                Age = GetBDate(GridView.GetFocusedRowCellValue(GridView.Columns("DateOfBirth")))
                frmMicroNew.txtAge.Text = Age.Split(" ").GetValue(0)
                frmMicroNew.txtClass.Text = Age.Split(" ").GetValue(1)
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
                    frmMicroNew.txtAddress.Text = reader(0).ToString
                    frmMicroNew.txtContact.Text = reader(1).ToString
                    frmMicroNew.cboCS.Text = reader(2).ToString
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
                    frmMicroNew.txtAccession.Text = reader(0).ToString
                    frmMicroNew.txtORNo.Text = reader(1).ToString
                    frmMicroNew.txtChargeSlip.Text = reader(2).ToString
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
                    frmMicroNew.txtRemarks.Text = reader(0).ToString
                    frmMicroNew.txtComment.Text = reader(1).ToString
                End If
                Disconnect()
                '######################################----END-----###############################################################

                Connect()
                rs.Connection = conn
                rs.CommandType = CommandType.Text
                rs.CommandText = "SELECT `viewpathologist`.`name` FROM `viewpathologist` JOIN order_pathologist ON `viewpathologist`.`id`=`order_pathologist`.`pathologist_id` WHERE `sample_id` = @MainID"
                reader = rs.ExecuteReader
                While reader.Read
                    frmMicroNew.cboPathologist.Text = reader(0).ToString
                End While
                Disconnect()

        '##############################------To enable necessary buttons-------#######################################################
        If GridView.GetFocusedRowCellValue(GridView.Columns("Status")) = "Validated" Then
            frmChemNew.btnPrint.Enabled = False
            frmChemNew.btnValidate.Enabled = True
            frmChemNew.btnPrintNow.Enabled = True
        End If
        '############################----------End------------##############################################################

        'frmMicroNew.GetBDate()

        'frmMicroNew.cboPathologist.SelectedIndex = 0

        'Activity Logs
        ActivityLogs(GridView.GetFocusedRowCellValue(GridView.Columns("SampleID")),
                                     GridView.GetFocusedRowCellValue(GridView.Columns("PatientID")),
                                     GridView.GetFocusedRowCellValue(GridView.Columns("PatientName")),
                                     CurrUser,
                                     "View Result",
                                     GridView.GetFocusedRowCellValue(GridView.Columns("Request")),
                                     GridView.GetFocusedRowCellValue(GridView.Columns("Section")),
                                     GridView.GetFocusedRowCellValue(GridView.Columns("SubSection")))

                frmMicroNew.ShowDialog()
        '    End If
        'Next rowHandle
    End Sub
    '############################################--------------END------------############################################

    '############################################-----For Completed Orders-----############################################
    Private Sub DisplayResultCompleted()
        'Dim selectedRows() As Integer = GridCompleted.GetSelectedRows()
        'For Each rowHandle As Integer In selectedRows
        '    If rowHandle >= 0 Then
        frmMicroOrdered.cboPathologist.Properties.Items.Clear()
                frmMicroOrdered.cboMedTech.Properties.Items.Clear()
                frmMicroOrdered.cboVerify.Properties.Items.Clear()

                '###########################---Load Basic Patient Details---######################################################
                frmMicroOrdered.mainID = GridCompleted.GetFocusedRowCellValue(GridCompleted.Columns("RefID")).ToString
                frmMicroOrdered.Section = GridCompleted.GetFocusedRowCellValue(GridCompleted.Columns("Section")).ToString
                frmMicroOrdered.SubSection = GridCompleted.GetFocusedRowCellValue(GridCompleted.Columns("SubSection")).ToString
                frmMicroOrdered.PatientID = GridCompleted.GetFocusedRowCellValue(GridCompleted.Columns("PatientID")).ToString
                frmMicroOrdered.txtSampleID.Text = GridCompleted.GetFocusedRowCellValue(GridCompleted.Columns("SampleID")).ToString
                frmMicroOrdered.txtPatientID.Text = GridCompleted.GetFocusedRowCellValue(GridCompleted.Columns("PatientID")).ToString
                frmMicroOrdered.txtName.Text = GridCompleted.GetFocusedRowCellValue(GridCompleted.Columns("PatientName")).ToString
                frmMicroOrdered.cboRequest.Text = GridCompleted.GetFocusedRowCellValue(GridCompleted.Columns("Request")).ToString
                frmMicroOrdered.dtReceived.Text = GridCompleted.GetFocusedRowCellValue(GridCompleted.Columns("DateReceived")).ToString
                frmMicroOrdered.tmTimeReceived.Text = GridCompleted.GetFocusedRowCellValue(GridCompleted.Columns("TimeReceived")).ToString
                frmMicroOrdered.tmTimeReleased.Text = GridCompleted.GetFocusedRowCellValue(GridCompleted.Columns("DateReleased")).ToString
                frmMicroOrdered.cboSex.Text = GridCompleted.GetFocusedRowCellValue(GridCompleted.Columns("Sex")).ToString
                frmMicroOrdered.dtBDate.Text = GridCompleted.GetFocusedRowCellValue(GridCompleted.Columns("DateOfBirth")).ToString
                frmMicroOrdered.cboPatientType.Text = GridCompleted.GetFocusedRowCellValue(GridCompleted.Columns("PatientType")).ToString
                frmMicroOrdered.cboPhysician.Text = GridCompleted.GetFocusedRowCellValue(GridCompleted.Columns("Physician")).ToString
                frmMicroOrdered.cboRoom.Text = GridCompleted.GetFocusedRowCellValue(GridCompleted.Columns("RoomWard")).ToString
                frmMicroOrdered.cboMedTech.Text = GridCompleted.GetFocusedRowCellValue(GridCompleted.Columns("PerformedBy")).ToString
        frmMicroOrdered.cboVerify.Text = GridCompleted.GetFocusedRowCellValue(GridCompleted.Columns("ReleasedBy")).ToString

        'For Age computation
        Dim Age As String = ""
                Age = GetBDate(GridCompleted.GetFocusedRowCellValue(GridCompleted.Columns("DateOfBirth")).ToString)
                frmMicroOrdered.txtAge.Text = Age.Split(" ").GetValue(0)
                frmMicroOrdered.txtClass.Text = Age.Split(" ").GetValue(1)
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
                    frmMicroOrdered.txtAddress.Text = reader(0).ToString
                    frmMicroOrdered.txtContact.Text = reader(1).ToString
                    frmMicroOrdered.cboCS.Text = reader(2).ToString
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
                    frmMicroOrdered.txtAccession.Text = reader(0).ToString
                    frmMicroOrdered.txtORNo.Text = reader(1).ToString
                    frmMicroOrdered.txtChargeSlip.Text = reader(2).ToString
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
                    frmMicroOrdered.txtRemarks.Text = reader(0).ToString
                    frmMicroOrdered.txtComment.Text = reader(1).ToString
                End If
                Disconnect()
                '######################################----END-----###############################################################

                Connect()
                rs.Connection = conn
                rs.CommandType = CommandType.Text
                rs.CommandText = "SELECT `viewpathologist`.`name` FROM `viewpathologist` JOIN order_pathologist ON `viewpathologist`.`id`=`order_pathologist`.`pathologist_id` WHERE `sample_id` = @MainID"
                reader = rs.ExecuteReader
                While reader.Read
                    frmMicroOrdered.cboPathologist.Text = reader(0).ToString
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
                frmMicroOrdered.ShowDialog()
        '    End If
        'Next rowHandle
    End Sub
    '############################################--------------END------------############################################

#End Region

End Class