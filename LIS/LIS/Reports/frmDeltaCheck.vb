Imports MySql.Data.MySqlClient

Public Class frmDeltaCheck

    Public Sub AutoLoadTestName()
        cboLimit.Properties.Items.Clear()
        Connect()
        rs.Connection = conn
        rs.CommandText = "SELECT `test_name` FROM `testtype` WHERE `test_name` NOT LIKE 'All' ORDER BY `test_name`"
        reader = rs.ExecuteReader
        While reader.Read
            cboLimit.Properties.Items.Add(reader(0).ToString)
        End While
        Disconnect()

        cboLimit.SelectedIndex = 0
    End Sub

    Private Sub frmWorkListHema_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Me.Dispose()
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.ItemClick
        Me.Close()
    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        If cboLimit.Text = "Hematology" Then
            Connect()
            Dim sql As String
            sql = "SELECT * FROM `hematology` WHERE (`patient_name` = @Type OR `patient_id` = @Type) AND (`date` BETWEEN @d1 AND @d2)"

            Dim dt As DataTable = New DataTable
            Dim command As New MySqlCommand(sql, conn)

            command.Parameters.Add("@d1", MySqlDbType.Date).Value = dtFrom.Value.ToString
            command.Parameters.Add("@d2", MySqlDbType.Date).Value = dtTo.Value.ToString
            command.Parameters.AddWithValue("@Type", cboType.Text)

            Dim adapter As New MySqlDataAdapter(command)
            adapter.Fill(dt)
            dtResult.DataSource = (dt)
            Disconnect()
        ElseIf cboLimit.Text = "Chemistry" Then
            Connect()
            Dim sql As String
            sql = "SELECT * FROM `chemistry` WHERE (`patient_name` = @Type OR `patient_id` = @Type) AND (`date` BETWEEN @d1 AND @d2)"

            Dim dt As DataTable = New DataTable
            Dim command As New MySqlCommand(sql, conn)

            command.Parameters.Add("@d1", MySqlDbType.Date).Value = dtFrom.Value.ToString
            command.Parameters.Add("@d2", MySqlDbType.Date).Value = dtTo.Value.ToString
            command.Parameters.AddWithValue("@Type", cboType.Text)

            Dim adapter As New MySqlDataAdapter(command)
            adapter.Fill(dt)
            dtResult.DataSource = (dt)
            Disconnect()
            'Else
            '    Connect()
            '    Dim sql As String
            '    sql = "SELECT * FROM `worksheetchem` WHERE `date` BETWEEN @d1 AND @d2"

            '    Dim dt As DataTable = New DataTable
            '    Dim command As New MySqlCommand(sql, conn)

            '    command.Parameters.Add("@d1", MySqlDbType.Date).Value = dtFrom.Value.ToString
            '    command.Parameters.Add("@d2", MySqlDbType.Date).Value = dtTo.Value.ToString

            '    Dim adapter As New MySqlDataAdapter(command)
            '    adapter.Fill(dt)
            '    dtResult.DataSource = (dt)
            '    Disconnect()
        End If
    End Sub

    Private Sub frmWorkSheet_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        AutoLoadTestName()
    End Sub
End Class