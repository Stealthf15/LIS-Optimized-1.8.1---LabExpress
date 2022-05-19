
Public Class frmAuthChangeDate

    Public ChangeDate As String
    Public DeleteUserU As String
    Public DeleteUserS As String
    Public DeleteUserP As String
    Public DeleteUserD As String

    Private Sub btnSave_Click(sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            If Me.txtUsername.Text = "" Or txtPassword.Text = "" Then
                MessageBox.Show("Please fill all required information.", "Empty String", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If

            Dim bytes() As Byte = System.Text.Encoding.UTF8.GetBytes(txtPassword.Text)
            Dim hashOfBytes() As Byte = New System.Security.Cryptography.SHA1Managed().ComputeHash(bytes)
            Dim strHash As String = Convert.ToBase64String(hashOfBytes)
            Dim status As String = ""

            rs.Parameters.Clear()
            rs.Parameters.AddWithValue("@username", txtUsername.Text)
            rs.Parameters.AddWithValue("@password", strHash)

            If ChangeDate = 1 Then
                Connect()
                rs.Connection = conn
                rs.CommandText = "SELECT * FROM `user_account` WHERE `Username` = @username AND `password` = @password AND `usertype` = 'Administrator'"
                reader = rs.ExecuteReader
                reader.Read()
                If reader.HasRows Then
                    Disconnect()
                    Close()
                    txtUsername.Text = ""
                    txtPassword.Text = ""
                    frmHemaChangeDateTime.ShowDialog()

                Else
                    Disconnect()
                    MessageBox.Show("Admin Login Reqired", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                End If

            ElseIf DeleteUserU = 1 Then
                Connect()
                rs.Connection = conn
                rs.CommandText = "SELECT * FROM `user_account` WHERE `Username` = @username AND `password` = @password AND `usertype` = 'Administrator'"
                reader = rs.ExecuteReader
                reader.Read()
                If reader.HasRows Then
                    Disconnect()
                    Close()
                    txtUsername.Text = ""
                    txtPassword.Text = ""
                    frmAccount.AuthDelete = 1
                Else
                    Disconnect()
                    MessageBox.Show("Admin Login Reqired", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                End If

            ElseIf DeleteUserS = 1 Then
                Connect()
                rs.Connection = conn
                rs.CommandText = "SELECT * FROM `user_account` WHERE `Username` = @username AND `password` = @password AND `usertype` = 'Administrator'"
                reader = rs.ExecuteReader
                reader.Read()
                If reader.HasRows Then
                    Disconnect()
                    Close()
                    txtUsername.Text = ""
                    txtPassword.Text = ""
                    frmMedTech.AuthDelete = 1
                Else
                    Disconnect()
                    MessageBox.Show("Admin Login Reqired", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                End If

            ElseIf DeleteUserP = 1 Then
                Connect()
                rs.Connection = conn
                rs.CommandText = "SELECT * FROM `user_account` WHERE `Username` = @username AND `password` = @password AND `usertype` = 'Administrator'"
                reader = rs.ExecuteReader
                reader.Read()
                If reader.HasRows Then
                    Disconnect()
                    Close()
                    txtUsername.Text = ""
                    txtPassword.Text = ""
                    frmPathologist.AuthDelete = 1
                Else
                    Disconnect()
                    MessageBox.Show("Admin Login Reqired", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                End If

            ElseIf DeleteUserD = 1 Then
                Connect()
                rs.Connection = conn
                rs.CommandText = "SELECT * FROM `user_account` WHERE `Username` = @username AND `password` = @password AND `usertype` = 'Administrator'"
                reader = rs.ExecuteReader
                reader.Read()
                If reader.HasRows Then
                    Disconnect()
                    Close()
                    txtUsername.Text = ""
                    txtPassword.Text = ""
                    frmDoctor.AuthDelete = 1
                Else
                    Disconnect()
                    MessageBox.Show("Admin Login Reqired", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                End If
            End If
            Disconnect()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try
    End Sub

End Class