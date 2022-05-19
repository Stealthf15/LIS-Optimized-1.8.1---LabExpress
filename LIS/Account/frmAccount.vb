Imports DevExpress.XtraGrid.Views.Grid

Public Class frmAccount
    Public AuthDelete As String
    Public Sub LoadRecords()
        Try
            GridView.Columns.Clear()
            GridView.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
            GridView.Appearance.HeaderPanel.FontStyleDelta = FontStyle.Bold

            Dim SQL As String = "SELECT `user_id` AS SequenceNo, 
                                    CASE WHEN `a_status` = 0 THEN 'Disabled' 
                                         WHEN `a_status` = 1 THEN 'Enabled' END AS Status,
                                    `email` AS UserID, 
                                    `name` AS User, 
                                    `username` AS Username, 
                                    `password` AS Password, 
                                    `usertype` AS UserType 
                                FROM `user_account` 
                                ORDER BY UserID asc, `Status`"
            Dim command As New MySql.Data.MySqlClient.MySqlCommand(SQL, conn)
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

        Catch ex As Exception
            MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub frmMachineList_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        LoadRecords()

        Connect()
        rs.Connection = conn
        rs.CommandType = CommandType.Text
        rs.CommandText = "SELECT `usertype` FROM `user_account` WHERE `email` = '" & CurrEmail & "'"
        reader = rs.ExecuteReader
        reader.Read()
        If reader.HasRows Then
            If reader(0).ToString = "Administrator" Then
                Disconnect()
                Me.btnDelete.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
            Else
                Disconnect()
                Me.btnDelete.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
            End If
        End If
        Disconnect()
    End Sub

    Private Sub SimpleButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.ItemClick
        Me.Close()
        Me.Dispose()
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.ItemClick

        Try
            frmAuthChangeDate.DeleteUserU = 1
            frmAuthChangeDate.ShowDialog()
            'MessageBox.Show(GridView.GetFocusedRowCellValue(GridView.Columns("SequenceNo")))
            If AuthDelete = 1 Then
                If (MessageBox.Show("Are you sure you want to disable the selected account?", "Disable Accouunt", MessageBoxButtons.YesNo, MessageBoxIcon.Question)) = DialogResult.Yes Then

                    'DeleteRecord("user_permission", "user_id", GridView.GetFocusedRowCellValue(GridView.Columns("UserID")))

                    'DeleteRecord("user_access", "user_id", GridView.GetFocusedRowCellValue(GridView.Columns("UserID")))

                    UpdateUserRecord("user_account", "a_status", 0, GridView.GetFocusedRowCellValue(GridView.Columns("UserID")))

                    MessageBox.Show("The selected record has been successfully updated.", "Record Update", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    'Else
                    '    Exit Sub
                End If
            End If
            LoadRecords()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnEnable_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEnable.ItemClick

        'Try
        frmAuthChangeDate.DeleteUserU = 1
        frmAuthChangeDate.ShowDialog()
        'MessageBox.Show(GridView.GetFocusedRowCellValue(GridView.Columns("SequenceNo")))
        If AuthDelete = 1 Then
            If (MessageBox.Show("Are you sure you want to enable the selected account?", "Enable Accouunt", MessageBoxButtons.YesNo, MessageBoxIcon.Question)) = DialogResult.Yes Then

                'DeleteRecord("user_permission", "user_id", GridView.GetFocusedRowCellValue(GridView.Columns("UserID")))

                'DeleteRecord("user_access", "user_id", GridView.GetFocusedRowCellValue(GridView.Columns("UserID")))

                UpdateUserRecord("user_account", "a_status", 1, GridView.GetFocusedRowCellValue(GridView.Columns("UserID")))

                MessageBox.Show("The selected record has been successfully updated.", "Record Update", MessageBoxButtons.OK, MessageBoxIcon.Information)
                'Else
                '    Exit Sub
            End If
        End If
        LoadRecords()
        'Catch ex As Exception
        '    MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        'End Try
    End Sub

    Private Sub btnRefresh_ItemClick(ByVal sender As Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnRefresh.ItemClick
        LoadRecords()
    End Sub

    Private Sub btnAdd_ItemClick(ByVal sender As Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnAdd.ItemClick
        frmAccountA.ShowDialog()
    End Sub

    Private Sub BarLargeButtonItem1_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarLargeButtonItem1.ItemClick
        Dim bytes() As Byte = System.Text.Encoding.UTF8.GetBytes(GridView.GetFocusedRowCellValue(GridView.Columns("Password")))
        Dim hashOfBytes() As Byte = New System.Security.Cryptography.SHA1Managed().ComputeHash(bytes)
        Dim strHash As String = Convert.ToBase64String(hashOfBytes)

        frmAccountEdit.ID_NO = GridView.GetFocusedRowCellValue(GridView.Columns("UserID"))

        frmAccountEdit.cboMedTech.Text = GridView.GetFocusedRowCellValue(GridView.Columns("User"))
        frmAccountEdit.txtUsername.Text = GridView.GetFocusedRowCellValue(GridView.Columns("Username"))
        frmAccountEdit.cboUserType.Text = GridView.GetFocusedRowCellValue(GridView.Columns("UserType"))
        frmAccountEdit.Password = GridView.GetFocusedRowCellValue(GridView.Columns("Password"))
        frmAccountEdit.cboLocation.Text = "Laboratory"
        'frmAccountEdit.cboEmailAccess.Text = Me.lvList.FocusedItem.SubItems(1).Text

        frmAccountEdit.ShowDialog()
    End Sub

    Public Sub DisableUser()
        'user_account
        'user_permission
        'user_access
    End Sub

    Private Sub GridView_RowCellStyle(sender As Object, e As RowCellStyleEventArgs) Handles GridView.RowCellStyle
        Dim view As GridView = TryCast(sender, GridView)
        If (e.Column.FieldName = "Status") Then
            If view.GetRowCellValue(e.RowHandle, "Status") = "Disabled" Then
                e.Appearance.BackColor = Color.Gold
                e.Appearance.BackColor2 = Color.Gold
                e.Appearance.ForeColor = Color.Black
            ElseIf view.GetRowCellValue(e.RowHandle, "Status") = "Enabled" Then
                e.Appearance.BackColor = Color.Green
                e.Appearance.BackColor2 = Color.Green
                e.Appearance.ForeColor = Color.White
            End If
        End If
    End Sub
End Class