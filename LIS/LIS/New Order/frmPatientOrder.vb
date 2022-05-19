Imports System.Drawing.Printing
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraPrinting.BarCode

Public Class frmPatientOrder

    Dim LastID As Integer
    Dim SampleID As String

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
            GridView.Columns.Clear()
            GridView.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
            GridView.Appearance.HeaderPanel.FontStyleDelta = FontStyle.Bold

            'cboSection.Text = "All"

            'Dim SQL As String = "SELECT 
            '            `id` AS ID, `status` AS `Status`, `sample_id` AS SampleID, `patient_id` AS PatientID, `patient_name`AS PatientName, 
            '            `bdate` AS DateOfBirth, `sex` AS Sex, `test` AS Test, DATE_FORMAT(`date`, '%m/%d/%Y') AS DateReceived, `time` AS TimeReceived, 
            '            `testtype` AS Section, `sub_section` AS SubSection, `main_id` AS RefID 
            '            FROM `tmpWorklist` WHERE (`status` = 'Checked-In') 
            '            AND (`date` BETWEEN @DateFrom and @DateTo) ORDER BY `id` DESC"

            If (cboSection.Text = "All" And txtSearch.Text = "") Then
                Dim SQL As String = "SELECT 
                        `tmpWorklist`.`id` AS SequenceNo, 
                        `tmpWorklist`.`status` AS `Status`, 
                        `tmpWorklist`.`sample_id` AS SampleID, 
                        `tmpWorklist`.`patient_id` AS PatientID, 
                        `additional_info`.`cs_no` AS ChargeSlip,
                        `tmpWorklist`.`patient_name`AS PatientName, 
                        `tmpWorklist`.`test` AS Test,
                        `tmpWorklist`.`dept` AS RoomWard,
                        `tmpWorklist`.`physician` AS Physician,
                        DATE_FORMAT(`tmpWorklist`.`date`, '%m/%d/%Y') AS DateRequested, 
                        `tmpWorklist`.`time` AS TimeRequested, 
                        `tmpWorklist`.`testtype` AS Section, 
                        `tmpWorklist`.`sub_section` AS SubSection, 
                        `tmpWorklist`.`main_id` AS RefID,
                        `tmpWorklist`.`sex` AS Sex, 
                        `tmpWorklist`.`bdate` AS DateOfBirth,
                        `tmpWorklist`.`age` AS Age,
                        `patient_remarks`.`diagnosis` AS ClinicalImpression,
                        `specimen_tracking`.`extracted` AS ExtractedDate
                        FROM `tmpWorklist`
                        LEFT JOIN `additional_info` ON `additional_info`.`sample_id` = `tmpWorklist`.`main_id`
                        LEFT JOIN `patient_remarks` ON `patient_remarks`.`sample_id` = `tmpworklist`.`sample_id`
                        LEFT JOIN `specimen_tracking` ON `specimen_tracking`.`sample_id` = `tmpworklist`.`sample_id`
                        WHERE (`tmpWorklist`.`status` = 'Checked-In' OR `tmpWorklist`.`status` = 'Result Received')
                        AND (`tmpworklist`.`testtype` = `specimen_tracking`.`section`)
                        AND (`tmpworklist`.`sub_section` = `specimen_tracking`.`sub_section`)
                        AND (`tmpworklist`.`testtype` = `additional_info`.`section`)
                        AND (`tmpworklist`.`sub_section` = `additional_info`.`sub_section`)
                        AND (`tmpworklist`.`testtype` = `patient_remarks`.`section`)
                        AND (`tmpworklist`.`sub_section` = `patient_remarks`.`sub_section`)
                        AND (`specimen_tracking`.`extracted` = '' OR `specimen_tracking`.`extracted` IS NULL)
                        AND `tmpWorklist`.`date` BETWEEN @DateFrom and @DateTo ORDER BY `tmpWorklist`.`id` DESC"

                Dim command As New MySql.Data.MySqlClient.MySqlCommand(SQL, conn)

                command.Parameters.Clear()
                command.Parameters.Add("@DateFrom", MySql.Data.MySqlClient.MySqlDbType.DateTime).Value = Format(dtFrom1.Value, "yyyy-MM-dd")
                command.Parameters.Add("@DateTo", MySql.Data.MySqlClient.MySqlDbType.DateTime).Value = Format(dtTo1.Value, "yyyy-MM-dd")

                Dim adapter As New MySql.Data.MySqlClient.MySqlDataAdapter(command)

                Dim myTable As DataTable = New DataTable
                adapter.Fill(myTable)

                dtList.DataSource = myTable

            ElseIf (cboSection.Text <> "All" And txtSearch.Text = "") Then
                Dim SQL As String = "SELECT 
                        `tmpWorklist`.`id` AS SequenceNo, 
                        `tmpWorklist`.`status` AS `Status`, 
                        `tmpWorklist`.`sample_id` AS SampleID, 
                        `tmpWorklist`.`patient_id` AS PatientID, 
                        `additional_info`.`cs_no` AS ChargeSlip,
                        `tmpWorklist`.`patient_name`AS PatientName, 
                        `tmpWorklist`.`test` AS Test,
                        `tmpWorklist`.`dept` AS RoomWard,
                        `tmpWorklist`.`physician` AS Physician,
                        DATE_FORMAT(`tmpWorklist`.`date`, '%m/%d/%Y') AS DateRequested, 
                        `tmpWorklist`.`time` AS TimeRequested, 
                        `tmpWorklist`.`testtype` AS Section, 
                        `tmpWorklist`.`sub_section` AS SubSection, 
                        `tmpWorklist`.`main_id` AS RefID,
                        `tmpWorklist`.`sex` AS Sex, 
                        `tmpWorklist`.`bdate` AS DateOfBirth,
                        `tmpWorklist`.`age` AS Age,
                        `patient_remarks`.`diagnosis` AS ClinicalImpression,
                        `specimen_tracking`.`extracted` AS ExtractedDate
                        FROM `tmpWorklist`
                        LEFT JOIN `additional_info` ON `additional_info`.`sample_id` = `tmpWorklist`.`main_id` 
                        LEFT JOIN `patient_remarks` ON `patient_remarks`.`sample_id` = `tmpworklist`.`sample_id`
                        LEFT JOIN `specimen_tracking` ON `specimen_tracking`.`sample_id` = `tmpworklist`.`sample_id`
                        WHERE (`tmpWorklist`.`status` = 'Checked-In' OR `tmpWorklist`.`status` = 'Result Received')
                        AND (`tmpworklist`.`testtype` = `specimen_tracking`.`section`)
                        AND (`tmpworklist`.`sub_section` = `specimen_tracking`.`sub_section`) 
                        AND (`tmpworklist`.`testtype` = `additional_info`.`section`)
                        AND (`tmpworklist`.`sub_section` = `additional_info`.`sub_section`)
                        AND (`tmpworklist`.`testtype` = `patient_remarks`.`section`)
                        AND (`tmpworklist`.`sub_section` = `patient_remarks`.`sub_section`)
                        AND (`specimen_tracking`.`extracted` = '' OR `specimen_tracking`.`extracted` IS NULL)
                        AND `tmpWorklist`.`testtype` = @Section
                        AND `tmpWorklist`.`date` BETWEEN @DateFrom and @DateTo ORDER BY `tmpWorklist`.`id` DESC"

                Dim command As New MySql.Data.MySqlClient.MySqlCommand(SQL, conn)

                command.Parameters.Clear()
                command.Parameters.AddWithValue("@Section", cboSection.Text)
                command.Parameters.Add("@DateFrom", MySql.Data.MySqlClient.MySqlDbType.DateTime).Value = Format(dtFrom1.Value, "yyyy-MM-dd")
                command.Parameters.Add("@DateTo", MySql.Data.MySqlClient.MySqlDbType.DateTime).Value = Format(dtTo1.Value, "yyyy-MM-dd")

                Dim adapter As New MySql.Data.MySqlClient.MySqlDataAdapter(command)

                Dim myTable As DataTable = New DataTable
                adapter.Fill(myTable)

                dtList.DataSource = myTable

            ElseIf (cboSection.Text = "All" And txtSearch.Text IsNot Nothing) Then
                Dim SQL As String = "SELECT 
                        `tmpWorklist`.`id` AS SequenceNo, 
                        `tmpWorklist`.`status` AS `Status`, 
                        `tmpWorklist`.`sample_id` AS SampleID, 
                        `tmpWorklist`.`patient_id` AS PatientID,
                        `additional_info`.`cs_no` AS ChargeSlip, 
                        `tmpWorklist`.`patient_name`AS PatientName, 
                        `tmpWorklist`.`test` AS Test,
                        `tmpWorklist`.`dept` AS RoomWard,
                        `tmpWorklist`.`physician` AS Physician,
                        DATE_FORMAT(`tmpWorklist`.`date`, '%m/%d/%Y') AS DateRequested, 
                        `tmpWorklist`.`time` AS TimeRequested, 
                        `tmpWorklist`.`testtype` AS Section, 
                        `tmpWorklist`.`sub_section` AS SubSection, 
                        `tmpWorklist`.`main_id` AS RefID,
                        `tmpWorklist`.`sex` AS Sex, 
                        `tmpWorklist`.`bdate` AS DateOfBirth,
                        `tmpWorklist`.`age` AS Age,
                        `patient_remarks`.`diagnosis` AS ClinicalImpression,
                        `specimen_tracking`.`extracted` AS ExtractedDate
                        FROM `tmpWorklist`
                        LEFT JOIN `additional_info` ON `additional_info`.`sample_id` = `tmpWorklist`.`main_id`
                        LEFT JOIN `patient_remarks` ON `patient_remarks`.`sample_id` = `tmpworklist`.`sample_id`
                        LEFT JOIN `specimen_tracking` ON `specimen_tracking`.`sample_id` = `tmpworklist`.`sample_id`
                        WHERE (`tmpWorklist`.`sample_id` LIKE '" & txtSearch.Text & "%' OR `tmpWorklist`.`patient_id` LIKE '" & txtSearch.Text & "%' OR `tmpWorklist`.`patient_name` LIKE '" & txtSearch.Text & "%') 
                        AND (`tmpWorklist`.`status` = 'Checked-In' OR `tmpWorklist`.`status` = 'Result Received') AND (`specimen_tracking`.`extracted` = '' OR `specimen_tracking`.`extracted` IS NULL)
                        AND (`tmpworklist`.`testtype` = `specimen_tracking`.`section`)
                        AND (`tmpworklist`.`sub_section` = `specimen_tracking`.`sub_section`) 
                        AND (`tmpworklist`.`testtype` = `additional_info`.`section`)
                        AND (`tmpworklist`.`sub_section` = `additional_info`.`sub_section`)
                        AND (`tmpworklist`.`testtype` = `patient_remarks`.`section`)
                        AND (`tmpworklist`.`sub_section` = `patient_remarks`.`sub_section`)
                        AND (`tmpWorklist`.`date` BETWEEN @DateFrom and @DateTo) ORDER BY `tmpWorklist`.`id` DESC"

                Dim command As New MySql.Data.MySqlClient.MySqlCommand(SQL, conn)

                command.Parameters.Clear()
                command.Parameters.Add("@DateFrom", MySql.Data.MySqlClient.MySqlDbType.DateTime).Value = Format(dtFrom1.Value, "yyyy-MM-dd")
                command.Parameters.Add("@DateTo", MySql.Data.MySqlClient.MySqlDbType.DateTime).Value = Format(dtTo1.Value, "yyyy-MM-dd")

                Dim adapter As New MySql.Data.MySqlClient.MySqlDataAdapter(command)

                Dim myTable As DataTable = New DataTable
                adapter.Fill(myTable)

                dtList.DataSource = myTable

            ElseIf (cboSection.Text <> "All" And txtSearch.Text IsNot Nothing) Then
                Dim SQL As String = "SELECT 
                         `tmpWorklist`.`id` AS SequenceNo, 
                        `tmpWorklist`.`status` AS `Status`, 
                        `tmpWorklist`.`sample_id` AS SampleID, 
                        `tmpWorklist`.`patient_id` AS PatientID, 
                        `additional_info`.`cs_no` AS ChargeSlip,
                        `tmpWorklist`.`patient_name`AS PatientName, 
                        `tmpWorklist`.`test` AS Test,
                        `tmpWorklist`.`dept` AS RoomWard,
                        `tmpWorklist`.`physician` AS Physician,
                        DATE_FORMAT(`tmpWorklist`.`date`, '%m/%d/%Y') AS DateRequested, 
                        `tmpWorklist`.`time` AS TimeRequested, 
                        `tmpWorklist`.`testtype` AS Section, 
                        `tmpWorklist`.`sub_section` AS SubSection, 
                        `tmpWorklist`.`main_id` AS RefID,
                        `tmpWorklist`.`sex` AS Sex, 
                        `tmpWorklist`.`bdate` AS DateOfBirth,
                        `tmpWorklist`.`age` AS Age,
                        `patient_remarks`.`diagnosis` AS ClinicalImpression,
                        `specimen_tracking`.`extracted` AS ExtractedDate
                        FROM `tmpWorklist`
                        LEFT JOIN `additional_info` ON `additional_info`.`sample_id` = `tmpWorklist`.`main_id` 
                        LEFT JOIN `patient_remarks` ON `patient_remarks`.`sample_id` = `tmpworklist`.`sample_id`
                        LEFT JOIN `specimen_tracking` ON `specimen_tracking`.`sample_id` = `tmpworklist`.`sample_id`
                        WHERE 
                        (`tmpWorklist`.`sample_id` LIKE '" & txtSearch.Text & "%' OR `tmpWorklist`.`patient_id` LIKE '" & txtSearch.Text & "%' OR `tmpWorklist`.`patient_name` LIKE '" & txtSearch.Text & "%')    
                        AND (`tmpWorklist`.`status` = 'Checked-In' OR `tmpWorklist`.`status` = 'Result Received') AND (`specimen_tracking`.`extracted` = '' OR `specimen_tracking`.`extracted` IS NULL)
                        AND (`tmpworklist`.`testtype` = `specimen_tracking`.`section`)
                        AND (`tmpworklist`.`sub_section` = `specimen_tracking`.`sub_section`) 
                        AND (`tmpworklist`.`testtype` = `additional_info`.`section`)
                        AND (`tmpworklist`.`sub_section` = `additional_info`.`sub_section`)
                        AND (`tmpworklist`.`testtype` = `patient_remarks`.`section`)
                        AND (`tmpworklist`.`sub_section` = `patient_remarks`.`sub_section`)
                        AND `tmpWorklist`.`date` BETWEEN @DateFrom and @DateTo ORDER BY `tmpWorklist`.`id` DESC"

                Dim command As New MySql.Data.MySqlClient.MySqlCommand(SQL, conn)

                command.Parameters.Clear()
                command.Parameters.AddWithValue("@Section", cboSection.Text)
                command.Parameters.Add("@DateFrom", MySql.Data.MySqlClient.MySqlDbType.DateTime).Value = Format(dtFrom1.Value, "yyyy-MM-dd")
                command.Parameters.Add("@DateTo", MySql.Data.MySqlClient.MySqlDbType.DateTime).Value = Format(dtTo1.Value, "yyyy-MM-dd")

                Dim adapter As New MySql.Data.MySqlClient.MySqlDataAdapter(command)

                Dim myTable As DataTable = New DataTable
                adapter.Fill(myTable)

                dtList.DataSource = myTable
            End If
            GridView.Columns("RefID").Visible = False
            GridView.Columns("Section").Visible = True
            GridView.Columns("SubSection").Visible = False
            GridView.Columns("ExtractedDate").Visible = False

            ' Make the grid read-only. 
            GridView.OptionsBehavior.Editable = False
            ' Prevent the focused cell from being highlighted. 
            GridView.OptionsSelection.EnableAppearanceFocusedCell = False
            ' Draw a dotted focus rectangle around the entire row. 
            GridView.FocusRectStyle = DrawFocusRectStyle.RowFullFocus

            GridView.OptionsSelection.MultiSelect = False
            GridView.OptionsSelection.MultiSelectMode = GridMultiSelectMode.CheckBoxRowSelect

            GridView.Columns("Age").Visible = False
            GridView.Columns("Physician").Visible = False
            GridView.Columns("Section").Visible = False
            'GridView.Columns("RoomWard").Visible = False
            GridView.Columns("SequenceNo").Visible = False

            GridView.OptionsView.ColumnAutoWidth = False

            GridView.Columns("SequenceNo").Width = 50
            GridView.Columns("PatientName").Width = 200
            GridView.Columns("Physician").Width = 200
            GridView.Columns("Test").Width = 200
            GridView.Columns("RoomWard").Width = 260
            GridView.Columns("Sex").Width = 60
            'GridView.Columns("DateReceived").Width = 90
            'GridView.Columns("Status").Width = 100
            GridView.Columns("TimeRequested").Width = 90
            GridView.Columns("DateRequested").Width = 135

        Catch ex As Exception
            MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub GridView_RowCellStyle(sender As Object, e As RowCellStyleEventArgs) Handles GridView.RowCellStyle
        Dim view As GridView = TryCast(sender, GridView)
        If (e.Column.FieldName = "ID") Or (e.Column.FieldName = "Status") Then
            If view.GetRowCellValue(e.RowHandle, "Status") = "Checked-In" Then
                e.Appearance.BackColor = Color.CornflowerBlue
                e.Appearance.BackColor2 = Color.CornflowerBlue
                e.Appearance.ForeColor = Color.White
            ElseIf view.GetRowCellValue(e.RowHandle, "Status") = "Result Received" Then
                e.Appearance.BackColor = Color.LightGreen
                e.Appearance.BackColor2 = Color.LightGreen
                e.Appearance.ForeColor = Color.Black
            End If
        End If
    End Sub

    Private Sub frmNewOrder_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        dtFrom1.Text = "01/01/2000"
        LoadRecords()

        DisablePermission()
    End Sub

    'permissions
    Public Sub DisablePermission()

        Connect()
        rs.Connection = conn
        rs.CommandType = CommandType.Text
        rs.CommandText = "SELECT * FROM `user_permission` WHERE `user_id` = '" & CurrEmail & "'"
        reader = rs.ExecuteReader
        While reader.Read()

            If reader(2).ToString = "Check-In Order" Then
                If reader(3).ToString = 0 Then
                    Me.btnAdd.Enabled = False
                ElseIf reader(3).ToString = 1 Then
                    Me.btnAdd.Enabled = True
                End If
            End If

            If reader(2).ToString = "Add Order" Then
                If reader(3).ToString = 0 Then
                    Me.btnAdd.Enabled = False
                ElseIf reader(3).ToString = 1 Then
                    Me.btnAdd.Enabled = True
                End If
            End If

            If reader(2).ToString = "Add" Then
                If reader(3).ToString = 0 Then
                    Me.btnAdd.Enabled = False
                ElseIf reader(3).ToString = 1 Then
                    Me.btnAdd.Enabled = True
                End If
            End If

            If reader(2).ToString = "Reject Order" Then
                If reader(3).ToString = 0 Then
                    Me.btnCancel.Enabled = False
                ElseIf reader(3).ToString = 1 Then
                    Me.btnCancel.Enabled = True
                End If
            End If

        End While
        Disconnect()

    End Sub

    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.ItemClick
        If MessageBox.Show("Do you want to check-in the patient?", "Check-In Patient", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
            Dim selectedRows() As Integer = GridView.GetSelectedRows()
            For Each rowHandle As Integer In selectedRows

                If rowHandle >= 0 Then

                    rs.Parameters.Clear()
                        rs.Parameters.AddWithValue("@mainID", GridView.GetRowCellValue(rowHandle, GridView.Columns("RefID")))
                    rs.Parameters.AddWithValue("@SampleID", GridView.GetRowCellValue(rowHandle, GridView.Columns("SampleID")))

                    If GridView.GetRowCellValue(rowHandle, GridView.Columns("Status")) = "Result Received" Then
                        rs.Parameters.AddWithValue("@status", "Result Received")
                    Else
                        rs.Parameters.AddWithValue("@status", "Processing")
                    End If

                    rs.Parameters.AddWithValue("@Section", GridView.GetRowCellValue(rowHandle, GridView.Columns("Section")))
                    rs.Parameters.AddWithValue("@SubSection", GridView.GetRowCellValue(rowHandle, GridView.Columns("SubSection")))
                    rs.Parameters.AddWithValue("@Processing", Now)

                    UpdateRecordwthoutMSG("UPDATE `tmpWorklist` SET " _
                            & "`sample_id` = @SampleID," _
                            & "`main_id` = @mainID," _
                            & "`status` = @status" _
                            & " WHERE main_id = @mainID AND `testtype` = @Section AND `sub_section` = @SubSection"
                            )

                        Connect()
                        rs.Connection = conn
                        rs.CommandType = CommandType.Text
                        rs.CommandText = "SELECT `sample_id` FROM `specimen_tracking` WHERE `sample_id` = @SampleID"
                        reader = rs.ExecuteReader
                        reader.Read()
                        If reader.HasRows Then
                            Disconnect()
                            UpdateRecordwthoutMSG("UPDATE `specimen_tracking` SET " _
                                & "`sample_id` = @SampleID," _
                                & "`extracted` = @Processing," _
                                & "`processing` = @Processing" _
                                & " WHERE sample_id = @mainID AND `section` = @Section AND `sub_section` = @SubSection"
                                )
                        Else
                            Disconnect()
                            SaveRecordwthoutMSG("INSERT INTO `specimen_tracking` (`sample_id`, `extracted`, `processing`, `section`, `sub_section`) VALUES " _
                                & "(" _
                                & " @SampleID," _
                                & "@Processing," _
                                & "@Processing," _
                                & "@Section," _
                                & "@SubSection" _
                                & ")"
                                )
                        End If
                        Disconnect()
                    End If

                'SaveRecordwthoutMSG("INSERT INTO `patient_remarks` (`sample_id`, `section`, `sub_section`) VALUES " _
                '            & "(" _
                '            & " @SampleID," _
                '            & "@Section," _
                '            & "@SubSection" _
                '            & ")"
                '            )
            Next rowHandle
            LoadRecords()
        Else
            Exit Sub
        End If
    End Sub

    Private Sub btnCancel_ItemClick(ByVal sender As Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnCancel.ItemClick
        Dim selectedRows() As Integer = GridView.GetSelectedRows()
        For Each rowHandle As Integer In selectedRows
            If rowHandle >= 0 Then
                If MessageBox.Show("Are you sure you want to reject " & GridView.GetRowCellValue(rowHandle, GridView.Columns("PatientName")) & " order?", "Confirm Reject", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = vbYes Then
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
        MainFOrm.btnPatientOrder.Appearance.Normal.BackColor = Color.FromArgb(16, 110, 190)
        MainFOrm.btnPatientOrder.Appearance.Normal.ForeColor = Color.FromArgb(255, 255, 255)
    End Sub

    Private Sub frm_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        MainFOrm.lblTitle.Text = ""
        MainFOrm.btnPatientOrder.Appearance.Normal.BackColor = Color.FromArgb(240, 240, 240)
        MainFOrm.btnPatientOrder.Appearance.Normal.ForeColor = Color.FromArgb(27, 41, 62)
        Me.Dispose()
    End Sub

    Private Sub frmPatientOrder_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.cboSection.Properties.Items.Clear()

        Connect()
        rs.Connection = conn
        rs.CommandType = CommandType.Text
        rs.CommandText = "SELECT `test_name` FROM `testtype` ORDER BY `id`"
        reader = rs.ExecuteReader
        While reader.Read
            Me.cboSection.Properties.Items.Add(reader(0))
        End While
        Disconnect()

        'Load Location Automatically
        'cboSection.Text = "All" 'CurrDept
        'dtFrom1.Text = Now()
        'dtTo1.Text = Now()
        'cboLocation1.Text = "All" 'CurrDept

        'LoadRecords()
        'LoadRecordsCompleted()
    End Sub

    Private Sub cboSection_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboSection.SelectedIndexChanged, dtFrom1.TextChanged, dtTo1.TextChanged, txtSearch.TextChanged, btnRefresh.ItemClick
        LoadRecords()
    End Sub
End Class