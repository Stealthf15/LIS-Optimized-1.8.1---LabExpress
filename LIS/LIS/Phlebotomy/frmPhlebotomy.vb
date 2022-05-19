Imports System.Drawing.Printing
Imports DevExpress.Xpo
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraPrinting.BarCode

Public Class frmPhlebotomy

    Public Sub LoadRecords()
        Try
            If txtSearch.Text = Nothing Then

                GridView.Columns.Clear()
                GridView.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                GridView.Appearance.HeaderPanel.FontStyleDelta = FontStyle.Bold

                Dim SQL As String = "SELECT 
                        `id` AS ID, `status` AS `Status`, `sample_id` AS SampleID, `patient_id` AS PatientID, `patient_name`AS PatientName, 
                        `test` AS Request, `bdate` AS DateOfBirth, `sex` AS Sex, DATE_FORMAT(`date`, '%m/%d/%Y') AS DateReceived, `time` AS TimeReceived, 
                        `testtype` AS Section, `sub_section` AS SubSection, `main_id` AS RefID, `location` AS Location 
                        FROM `tmpWorklist` WHERE (`status` = 'Ordered' OR `status` = 'Rejected' OR `status` = 'Cancelled' OR `status` = 'Warding') 
                        AND (`date` BETWEEN @DateFrom and @DateTo) ORDER BY `id` DESC"

                Dim command As New MySql.Data.MySqlClient.MySqlCommand(SQL, conn)

                command.Parameters.Clear()
                command.Parameters.Add("@DateFrom", MySql.Data.MySqlClient.MySqlDbType.DateTime).Value = Format(dtFrom1.Value, "yyyy-MM-dd")
                command.Parameters.Add("@DateTo", MySql.Data.MySqlClient.MySqlDbType.DateTime).Value = Format(dtTo1.Value, "yyyy-MM-dd")

                Dim adapter As New MySql.Data.MySqlClient.MySqlDataAdapter(command)

                Dim myTable As DataTable = New DataTable
                adapter.Fill(myTable)

                dtList.DataSource = myTable

                GridView.Columns("RefID").Visible = False
                GridView.Columns("Section").Visible = False
                GridView.Columns("SubSection").Visible = False

                ' Make the grid read-only. 
                GridView.OptionsBehavior.Editable = False
                ' Prevent the focused cell from being highlighted. 
                GridView.OptionsSelection.EnableAppearanceFocusedCell = False
                ' Draw a dotted focus rectangle around the entire row. 
                GridView.FocusRectStyle = DrawFocusRectStyle.RowFullFocus

                GridView.OptionsSelection.MultiSelect = True
                GridView.OptionsSelection.MultiSelectMode = GridMultiSelectMode.CheckBoxRowSelect

            Else

                GridView.Columns.Clear()
                GridView.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                GridView.Appearance.HeaderPanel.FontStyleDelta = FontStyle.Bold

                Dim SQL As String = "SELECT 
                        `id` AS ID, `status` AS `Status`, `sample_id` AS SampleID, `patient_id` AS PatientID, `patient_name` AS PatientName, 
                        `test` AS Request, `bdate` AS DateOfBirth, `sex` AS Sex, DATE_FORMAT(`date`, '%m/%d/%Y') AS DateReceived, `time` AS TimeReceived, 
                        `testtype` AS Section, `sub_section` AS SubSection, `main_id` AS RefID, `location` AS Location 
                        FROM `tmpWorklist` WHERE `sample_id` LIKE '" & txtSearch.Text & "%' OR `patient_id` LIKE '" & txtSearch.Text & "%' OR `patient_name` LIKE '" & txtSearch.Text & "%' AND (`status` = 'Ordered' OR `status` = 'Rejected' OR `status` = 'Cancelled' OR `status` = 'Warding') 
                        AND (`date` BETWEEN @DateFrom and @DateTo) ORDER BY `id` DESC"

                Dim command As New MySql.Data.MySqlClient.MySqlCommand(SQL, conn)

                command.Parameters.Clear()
                command.Parameters.Add("@DateFrom", MySql.Data.MySqlClient.MySqlDbType.DateTime).Value = Format(dtFrom1.Value, "yyyy-MM-dd")
                command.Parameters.Add("@DateTo", MySql.Data.MySqlClient.MySqlDbType.DateTime).Value = Format(dtTo1.Value, "yyyy-MM-dd")

                Dim adapter As New MySql.Data.MySqlClient.MySqlDataAdapter(command)

                Dim myTable As DataTable = New DataTable
                adapter.Fill(myTable)

                dtList.DataSource = myTable

                GridView.Columns("RefID").Visible = False
                GridView.Columns("Section").Visible = False
                GridView.Columns("SubSection").Visible = False

                ' Make the grid read-only. 
                GridView.OptionsBehavior.Editable = False
                ' Prevent the focused cell from being highlighted. 
                GridView.OptionsSelection.EnableAppearanceFocusedCell = False
                ' Draw a dotted focus rectangle around the entire row. 
                GridView.FocusRectStyle = DrawFocusRectStyle.RowFullFocus

                GridView.OptionsSelection.MultiSelect = True
                GridView.OptionsSelection.MultiSelectMode = GridMultiSelectMode.CheckBoxRowSelect

            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub GridView_RowCellStyle(sender As Object, e As RowCellStyleEventArgs) Handles GridView.RowCellStyle
        Dim view As GridView = TryCast(sender, GridView)
        If (e.Column.FieldName = "ID") Or (e.Column.FieldName = "Status") Then
            If view.GetRowCellValue(e.RowHandle, "Status") = "Warding" Then
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
            End If
        End If
    End Sub

    Public Sub LoadRecordsFilterWard()
        Try
            GridView.Columns.Clear()
            GridView.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
            GridView.Appearance.HeaderPanel.FontStyleDelta = FontStyle.Bold

            Dim SQL As String = "SELECT 
                        `id` AS ID, `status` AS `Status`, `sample_id` AS SampleID, `patient_id` AS PatientID, `patient_name`AS PatientName, 
                        `test` AS Request, `bdate` AS DateOfBirth, `sex` AS Sex, DATE_FORMAT(`date`, '%m/%d/%Y') AS DateReceived, `time` AS TimeReceived, 
                        `testtype` AS Section, `sub_section` AS SubSection, `main_id` AS RefID, `location` AS Location 
                        FROM `tmpWorklist` WHERE (`status` = 'Ordered' OR `status` = 'Rejected' OR `status` = 'Cancelled' OR `status` = 'Warding') 
                        AND (`date` BETWEEN @DateFrom AND @DateTo) AND `dept` = @Search ORDER BY `id` DESC"

            Dim command As New MySql.Data.MySqlClient.MySqlCommand(SQL, conn)

            command.Parameters.Clear()
            command.Parameters.Add("@DateFrom", MySql.Data.MySqlClient.MySqlDbType.DateTime).Value = Format(dtFrom1.Value, "yyyy-MM-dd")
            command.Parameters.Add("@DateTo", MySql.Data.MySqlClient.MySqlDbType.DateTime).Value = Format(dtTo1.Value, "yyyy-MM-dd")
            command.Parameters.AddWithValue("@Search", cboWard.Text)

            Dim adapter As New MySql.Data.MySqlClient.MySqlDataAdapter(command)

            Dim myTable As DataTable = New DataTable
            adapter.Fill(myTable)

            dtList.DataSource = myTable

            ' Make the grid read-only. 
            GridView.OptionsBehavior.Editable = False
            ' Prevent the focused cell from being highlighted. 
            GridView.OptionsSelection.EnableAppearanceFocusedCell = False
            ' Draw a dotted focus rectangle around the entire row. 
            GridView.FocusRectStyle = DrawFocusRectStyle.RowFullFocus

            GridView.OptionsSelection.MultiSelect = True
            GridView.OptionsSelection.MultiSelectMode = GridMultiSelectMode.CheckBoxRowSelect

        Catch ex As Exception
            MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub frmNewOrder_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        dtFrom1.Text = "01/01/2000"
        LoadRecords()
        LoadWard()

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
                    Me.btnAddOrder.Enabled = False
                ElseIf reader(3).ToString = 1 Then
                    Me.btnAdd.Enabled = True
                    Me.btnAddOrder.Enabled = False
                End If
            End If

            If reader(2).ToString = "Print" Then
                If reader(3).ToString = 0 Then
                    Me.btnPrintList.Enabled = False
                ElseIf reader(3).ToString = 1 Then
                    Me.btnPrintList.Enabled = True
                End If
            End If

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

        End While
        Disconnect()

    End Sub

    Private Sub LoadWard()
        Connect()
        rs.Connection = conn
        rs.CommandType = CommandType.Text
        rs.CommandText = "SELECT DISTINCT `dept` FROM tmpworklist"
        reader = rs.ExecuteReader
        While reader.Read
            cboWard.Properties.Items.Add(reader(0).ToString)
        End While
        Disconnect()
    End Sub


    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.ItemClick
        Dim selectedRows() As Integer = GridView.GetSelectedRows()
        For Each rowHandle As Integer In selectedRows
            If rowHandle >= 0 Then
                Dim Result As DialogResult = MessageBox.Show("You're about to Check-In Patient " & GridView.GetRowCellValue(rowHandle, GridView.Columns("PatientName")) & "." & vbCrLf & vbCrLf & "Do you want to continue to print Barcode Sticker " & GridView.GetRowCellValue(rowHandle, GridView.Columns("SampleID")) & "?", "System Message", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)

                If (Result = DialogResult.Yes) Then
                    Try
                        PrintBarcode(GridView.GetRowCellValue(rowHandle, GridView.Columns("Request")),
                                     GridView.GetRowCellValue(rowHandle, GridView.Columns("SampleID")),
                                     GridView.GetRowCellValue(rowHandle, GridView.Columns("PatientID")),
                                     GridView.GetRowCellValue(rowHandle, GridView.Columns("PatientName")),
                                     GridView.GetRowCellValue(rowHandle, GridView.Columns("DateOfBirth")),
                                     GridView.GetRowCellValue(rowHandle, GridView.Columns("Sex")),
                                     GridView.GetRowCellValue(rowHandle, GridView.Columns("Section")),
                                     GridView.GetRowCellValue(rowHandle, GridView.Columns("SubSection")),
                                     GridView.GetFocusedRowCellValue(GridView.Columns("Location")),
                                     GridView.GetFocusedRowCellValue(GridView.Columns("Location")),
                                     CurrUsername)

                    Catch ex As Exception
                        MessageBox.Show("Error in connection on printer. " + ex.Message, "Barcode Printing Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End Try
                    GoTo A
                ElseIf (Result = DialogResult.No) Then
                    GoTo A
                ElseIf (Result = DialogResult.Cancel) Then
                    Exit Sub
                End If

A:

                rs.Parameters.Clear()
                rs.Parameters.AddWithValue("@mainID", GridView.GetRowCellValue(rowHandle, GridView.Columns("RefID")))
                rs.Parameters.AddWithValue("@SampleID", GridView.GetRowCellValue(rowHandle, GridView.Columns("SampleID")))
                rs.Parameters.AddWithValue("@status", "Checked-In")
                rs.Parameters.AddWithValue("@Section", GridView.GetRowCellValue(rowHandle, GridView.Columns("Section")))
                rs.Parameters.AddWithValue("@SubSection", GridView.GetRowCellValue(rowHandle, GridView.Columns("SubSection")))
                rs.Parameters.AddWithValue("@time_checked_in", Now)

                UpdateRecordwthoutMSG("UPDATE `tmpWorklist` SET " _
                    & "`sample_id` = @SampleID," _
                    & "`main_id` = @mainID," _
                    & "`status` = @status" _
                    & " WHERE main_id = @mainID AND `testtype` = @Section AND `sub_section` = @SubSection"
                    )

                UpdateRecordwthoutMSG("UPDATE `additional_info` SET " _
                    & "`sample_id` = @SampleID," _
                    & "`accession_no` = @mainID" _
                    & " WHERE sample_id = @mainID AND `section` = @Section AND `sub_section` = @SubSection"
                    )

                'UpdateRecordwthoutMSG("UPDATE `tmpresult` SET " _
                '    & "`sample_id` = @SampleID" _
                '    & " WHERE sample_id = @mainID AND section = @Section AND sub_section = @SubSection"
                '    )

                'UpdateRecordwthoutMSG("UPDATE `patient_order` SET " _
                '    & "`sample_id` = @SampleID" _
                '    & " WHERE sample_id = @mainID AND section = @Section AND sub_section = @SubSection"
                '    )

                'UpdateRecordwthoutMSG("UPDATE `lis_order` SET " _
                '    & "`sample_id` = @SampleID" _
                '    & " WHERE sample_id = @mainID AND section = @Section AND sub_section = @SubSection"
                '    )

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
                        & "`extracted` = @time_checked_in" _
                        & " WHERE sample_id = @mainID AND `section` = @Section AND `sub_section` = @SubSection"
                        )
                Else
                    Disconnect()
                    SaveRecordwthoutMSG("INSERT INTO `specimen_tracking` (`sample_id`, `extracted`, `section`, `sub_section`) VALUES " _
                        & "(" _
                        & "@SampleID," _
                        & "@time_checked_in," _
                        & "@Section," _
                        & "@SubSection" _
                        & ")"
                        )
                End If
                Disconnect()
                'Log activity
                SpecimenActivity("z_logs_specimen", GridView.GetRowCellValue(rowHandle, GridView.Columns("SampleID")), GridView.GetRowCellValue(rowHandle, GridView.Columns("PatientID")), GridView.GetRowCellValue(rowHandle, GridView.Columns("PatientName")), CurrUser, "Checked-In Specimen", "", GridView.GetRowCellValue(rowHandle, GridView.Columns("Request")), GridView.GetRowCellValue(rowHandle, GridView.Columns("Section")), GridView.GetRowCellValue(rowHandle, GridView.Columns("SubSection")))
            End If
        Next rowHandle
        LoadRecords()

    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.ItemClick
        LoadRecords()
    End Sub

    Private Sub btnClose_ItemClick(ByVal sender As Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnClose.ItemClick
        Me.Close()
        Me.Dispose()
    End Sub

    Private Sub btnWarding_ItemClick(ByVal sender As Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnAddOrder.ItemClick
        Dim selectedRows() As Integer = GridView.GetSelectedRows()
        For Each rowHandle As Integer In selectedRows
            If rowHandle >= 0 Then
                Dim Result As DialogResult = MessageBox.Show("You're about to Check-In Patient " & GridView.GetRowCellValue(rowHandle, GridView.Columns("PatientName")) & "." & vbCrLf & vbCrLf & "Do you want to continue to print Barcode Sticker " & GridView.GetRowCellValue(rowHandle, GridView.Columns("SampleID")) & "?", "System Message", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)

                If (Result = DialogResult.Yes) Then
                    Try
                        'PrintBarcode(GridView.GetRowCellValue(rowHandle, GridView.Columns("Request")),
                        '             GridView.GetRowCellValue(rowHandle, GridView.Columns("SampleID")),
                        '             GridView.GetRowCellValue(rowHandle, GridView.Columns("PatientID")),
                        '             GridView.GetRowCellValue(rowHandle, GridView.Columns("PatientName")),
                        '             GridView.GetRowCellValue(rowHandle, GridView.Columns("DateOfBirth")),
                        '             GridView.GetRowCellValue(rowHandle, GridView.Columns("Sex")),
                        '             GridView.GetRowCellValue(rowHandle, GridView.Columns("Section")),
                        '             GridView.GetRowCellValue(rowHandle, GridView.Columns("SubSection")))
                    Catch ex As Exception
                        MessageBox.Show("Error in connection on printer. " + ex.Message, "Barcode Printing Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End Try
                    GoTo A
                ElseIf (Result = DialogResult.No) Then
                    GoTo A
                ElseIf (Result = DialogResult.Cancel) Then
                    Exit Sub
                End If

A:

                rs.Parameters.Clear()
                rs.Parameters.AddWithValue("@mainID", GridView.GetRowCellValue(rowHandle, GridView.Columns("RefID")))
                rs.Parameters.AddWithValue("@SampleID", GridView.GetRowCellValue(rowHandle, GridView.Columns("SampleID")))
                rs.Parameters.AddWithValue("@status", "Warding")
                rs.Parameters.AddWithValue("@Section", GridView.GetRowCellValue(rowHandle, GridView.Columns("Section")))
                rs.Parameters.AddWithValue("@SubSection", GridView.GetRowCellValue(rowHandle, GridView.Columns("SubSection")))
                rs.Parameters.AddWithValue("@time_warding", Now)

                UpdateRecordwthoutMSG("UPDATE `tmpWorklist` SET " _
                    & "`sample_id` = @SampleID," _
                    & "`main_id` = @mainID," _
                    & "`status` = @status" _
                    & " WHERE main_id = @mainID AND `testtype` = @Section AND `sub_section` = @SubSection"
                    )

                UpdateRecordwthoutMSG("UPDATE `additional_info` SET " _
                    & "`sample_id` = @SampleID," _
                    & "`accession_no` = @mainID" _
                    & " WHERE sample_id = @mainID AND `section` = @Section AND `sub_section` = @SubSection"
                    )

                'UpdateRecordwthoutMSG("UPDATE `tmpresult` SET " _
                '    & "`sample_id` = @SampleID" _
                '    & " WHERE sample_id = @mainID AND section = @Section AND sub_section = @SubSection"
                '    )

                'UpdateRecordwthoutMSG("UPDATE `patient_order` SET " _
                '    & "`sample_id` = @SampleID" _
                '    & " WHERE sample_id = @mainID AND section = @Section AND sub_section = @SubSection"
                '    )

                'UpdateRecordwthoutMSG("UPDATE `lis_order` SET " _
                '    & "`sample_id` = @SampleID" _
                '    & " WHERE sample_id = @mainID AND section = @Section AND sub_section = @SubSection"
                '    )

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
                        & "`warding` = @time_warding" _
                        & " WHERE sample_id = @mainID AND `section` = @Section AND `sub_section` = @SubSection"
                        )
                Else
                    Disconnect()
                    SaveRecordwthoutMSG("INSERT INTO `specimen_tracking` (`sample_id`, `warding`, `section`, `sub_section`) VALUES " _
                        & "(" _
                        & "@SampleID," _
                        & "@time_warding," _
                        & "@Section," _
                        & "@SubSection" _
                        & ")"
                        )
                End If
                Disconnect()
                'Log activity
                SpecimenActivity("z_logs_specimen", GridView.GetRowCellValue(rowHandle, GridView.Columns("SampleID")), GridView.GetRowCellValue(rowHandle, GridView.Columns("PatientID")), GridView.GetRowCellValue(rowHandle, GridView.Columns("PatientName")), CurrUser, "Ward Specimen", "", GridView.GetRowCellValue(rowHandle, GridView.Columns("Request")), GridView.GetRowCellValue(rowHandle, GridView.Columns("Section")), GridView.GetRowCellValue(rowHandle, GridView.Columns("SubSection")))
            End If
        Next rowHandle
        LoadRecords()
    End Sub

    Private Sub frm_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        'MainFOrm.aceFecal.Appearance.Normal.BackColor = Color.FromArgb(6, 31, 71)
        MainFOrm.accPhlebotomy.Appearance.Normal.BackColor = Color.FromArgb(16, 110, 190)
        MainFOrm.accPhlebotomy.Appearance.Normal.ForeColor = Color.FromArgb(255, 255, 255)
    End Sub

    Private Sub frm_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        MainFOrm.lblTitle.Text = ""
        MainFOrm.accPhlebotomy.Appearance.Normal.BackColor = Color.FromArgb(240, 240, 240)
        MainFOrm.accPhlebotomy.Appearance.Normal.ForeColor = Color.FromArgb(27, 41, 62)
        Me.Dispose()
    End Sub

    'Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
    '    LoadRecords()
    'End Sub

    Private Sub cboWard_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboWard.SelectedIndexChanged
        LoadRecordsFilterWard()
    End Sub

    Private Sub btnDelete_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnDelete.ItemClick
        Dim selectedRows() As Integer = GridView.GetSelectedRows()
        For Each rowHandle As Integer In selectedRows
            If rowHandle >= 0 Then
                If MessageBox.Show("Are you sure you want to reject " & GridView.GetRowCellValue(rowHandle, GridView.Columns("PatientName")) & " order?", "Confirm Reject", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = vbYes Then
                    frmRejectOrder.ID = GridView.GetRowCellValue(rowHandle, GridView.Columns("ID"))
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

    Private Sub btnCancel_ItemClick(ByVal sender As Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnCancel.ItemClick
        Dim selectedRows() As Integer = GridView.GetSelectedRows()
        For Each rowHandle As Integer In selectedRows
            If rowHandle >= 0 Then
                If MessageBox.Show("Are you sure you want to cancel " & GridView.GetRowCellValue(rowHandle, GridView.Columns("PatientName")) & " order?", "Confirm Cancel", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = vbYes Then
                    frmCancelOR.ID = GridView.GetRowCellValue(rowHandle, GridView.Columns("ID"))
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

    Private Sub btnSearch_KeyPress(sender As Object, e As KeyPressEventArgs) Handles btnSearch.KeyPress
        If e.KeyChar = Chr(13) Then
            LoadRecordsFilterWard()
        End If
    End Sub

    Private Sub NotifyMe()
        ToastNotificationsManager.ShowNotification(ToastNotificationsManager.Notifications(0))
    End Sub

    Private Sub txtSearch_SelectedIndexChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged, dtFrom1.TextChanged, dtTo1.TextChanged
        LoadRecords()
        'Try
        '    GridView.Columns.Clear()
        '    GridView.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        '    GridView.Appearance.HeaderPanel.FontStyleDelta = FontStyle.Bold

        '    Dim SQL As String = "SELECT 
        '                `id` AS ID, `status` AS `Status`, `sample_id` AS SampleID, `patient_id` AS PatientID, `patient_name`AS PatientName, 
        '                `test` AS Request, `bdate` AS DateOfBirth, `sex` AS Sex, DATE_FORMAT(`date`, '%m/%d/%Y') AS DateReceived, `time` AS TimeReceived, 
        '                `testtype` AS Section, `sub_section` AS SubSection, `main_id` AS RefID 
        '                FROM `tmpWorklist` WHERE (`sample_id` LIKE '" & txtSearch.Text & "%' OR `patient_id` LIKE '" & txtSearch.Text & "%' OR `patient_name` LIKE '" & txtSearch.Text & "%') AND (`status` = 'Ordered' OR `status` = 'Rejected' OR `status` = 'Cancelled' OR `status` = 'Warding') 
        '                AND (`date` BETWEEN @DateFrom and @DateTo) ORDER BY `id` DESC"

        '    Dim command As New MySql.Data.MySqlClient.MySqlCommand(SQL, conn)

        '    command.Parameters.Clear()
        '    command.Parameters.Add("@DateFrom", MySql.Data.MySqlClient.MySqlDbType.DateTime).Value = Format(dtFrom1.Value, "yyyy-MM-dd")
        '    command.Parameters.Add("@DateTo", MySql.Data.MySqlClient.MySqlDbType.DateTime).Value = Format(dtTo1.Value, "yyyy-MM-dd")

        '    Dim adapter As New MySql.Data.MySqlClient.MySqlDataAdapter(command)

        '    Dim myTable As DataTable = New DataTable
        '    adapter.Fill(myTable)

        '    dtList.DataSource = myTable

        '    GridView.Columns("RefID").Visible = False
        '    GridView.Columns("Section").Visible = False
        '    GridView.Columns("SubSection").Visible = False

        '    ' Make the grid read-only. 
        '    GridView.OptionsBehavior.Editable = False
        '    ' Prevent the focused cell from being highlighted. 
        '    GridView.OptionsSelection.EnableAppearanceFocusedCell = False
        '    ' Draw a dotted focus rectangle around the entire row. 
        '    GridView.FocusRectStyle = DrawFocusRectStyle.RowFullFocus

        '    GridView.OptionsSelection.MultiSelect = True
        '    GridView.OptionsSelection.MultiSelectMode = GridMultiSelectMode.CheckBoxRowSelect

        'Catch ex As Exception
        '    MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        'End Try
    End Sub

    Private Sub GridView_RowClick(sender As Object, e As RowClickEventArgs) Handles GridView.RowClick
        LoadRecordsOnLVSQL(lvTest, "SELECT * FROM `patient_order` WHERE `sample_id` = '" & GridView.GetFocusedRowCellValue(GridView.Columns("RefID")) & "' AND testtype = '" & GridView.GetFocusedRowCellValue(GridView.Columns("Section")) & "' AND sub_section = '" & GridView.GetFocusedRowCellValue(GridView.Columns("SubSection")) & "'", 3)
    End Sub

    Private Sub btnPrintList_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnPrintList.ItemClick

    End Sub

    'Private Sub GridView_FocusedRowChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles GridView.FocusedRowChanged
    '    Dim view As GridView = TryCast(sender, GridView)
    '    view.LayoutChanged()
    'End Sub

    'Private Sub GridView_CalcRowHeight(ByVal sender As Object, ByVal e As RowHeightEventArgs) Handles GridView.CalcRowHeight
    '    Dim view As GridView = TryCast(sender, GridView)
    '    If e.RowHandle = view.FocusedRowHandle Then
    '        e.RowHeight = 50
    '    End If
    'End Sub

End Class