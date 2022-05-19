Public Class frmPatientList

    Public Type As String = ""

    Public Sub LoadRecords()
        Try
            'LoadRecordsOnLV(lvList, "`patient_info`", 8, "patient_id")

            lvList.Items.Clear()

            Dim SQL As String = "SELECT TOP 100 t1.hpercode AS HospitalNo, " _
                          & "t1.patlast + ', ' + t1.patfirst + ' ' + t1.patmiddle AS PatientName, " _
                          & "CONVERT(varchar, t1.patbdate, 101) AS DateOfBirth, " _
                          & "t1.patsex AS Sex, " _
                          & "(t2.patstr + ', ' + t3.bgyname + ', ' + t4.ctyname + ', ' + t5.provname) AS Address" _
                          & " FROM " _
                          & "hperson t1 " _
                          & "LEFT JOIN haddr t2 On t1.hpercode = t2.hpercode " _
                          & "LEFT JOIN hbrgy t3 On t2.brg = t3.bgycode " _
                          & "LEFT JOIN hcity t4 ON t2.ctycode = t4.ctycode " _
                          & "LEFT JOIN hprov t5 ON t2.provcode = t5.provcode " _
                          & "WHERE t1.hpercode LIKE '" & txtSearch.Text & "%' OR t1.patlast + ', ' + t1.patfirst LIKE '" & txtSearch.Text & "%' " _
                          & "ORDER BY t1.hpercode ASC"

            ConnectSQL()
            rsSQL.Connection = connSQL
            rsSQL.CommandType = CommandType.Text
            rsSQL.CommandText = SQL
            readerSQL = rsSQL.ExecuteReader
            While readerSQL.Read
                iItem = New ListViewItem(readerSQL(0).ToString, 0)
                iItem.Checked = False
                For x As Integer = 1 To 4 Step 1
                    iItem.SubItems.Add(readerSQL(x).ToString())
                Next
                lvList.Items.Add(iItem)
            End While
            DisconnectSQL()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub frmPatientList_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Me.Dispose()
    End Sub

    Private Sub frmNewOrder_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        'SearchRecordsOnLV(lvList, "`patient_info`", 8, "`patient_id`", Me.txtSearch.Text & "%' OR `name` LIKE '" & Me.txtSearch.Text, "patient_id")
        LoadRecords()
    End Sub

    Private Sub SimpleButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSelect.ItemClick, lvList.DoubleClick

        If Me.lvList.SelectedItems.Count > 0 Then
            frmPatientOrderAE.txtPatientID.Text = lvList.FocusedItem.SubItems(0).Text
            frmPatientOrderAE.txtName.Text = lvList.FocusedItem.SubItems(1).Text
            frmPatientOrderAE.dtBDate.Text = lvList.FocusedItem.SubItems(2).Text
            frmPatientOrderAE.cboSex.Text = lvList.FocusedItem.SubItems(3).Text
            frmPatientOrderAE.txtAddress.Text = lvList.FocusedItem.SubItems(4).Text

            frmPatientOrderAE.btnDelete.PerformClick()

            Me.Close()
            Me.Dispose()


        End If
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.ItemClick
        Me.Close()
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.ItemClick
        LoadRecords()
    End Sub

    Private Sub txtSearch_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSearch.TextChanged
        'SearchRecordsOnLV(lvList, "`patient_info`", 8, "`patient_id`", Me.txtSearch.Text & "%' OR `name` LIKE '" & Me.txtSearch.Text, "patient_id")

        lvList.Items.Clear()

        Dim SQL As String = "SELECT TOP 100 t1.hpercode AS HospitalNo, " _
                          & "t1.patlast + ', ' + t1.patfirst + ' ' + t1.patmiddle AS PatientName, " _
                          & "CONVERT(varchar, t1.patbdate, 101) AS DateOfBirth, " _
                          & "t1.patsex AS Sex, " _
                          & "(t2.patstr + ', ' + t3.bgyname + ', ' + t4.ctyname + ', ' + t5.provname) AS Address" _
                          & " FROM " _
                          & "hperson t1 " _
                          & "LEFT JOIN haddr t2 On t1.hpercode = t2.hpercode " _
                          & "LEFT JOIN hbrgy t3 On t2.brg = t3.bgycode " _
                          & "LEFT JOIN hcity t4 ON t2.ctycode = t4.ctycode " _
                          & "LEFT JOIN hprov t5 ON t2.provcode = t5.provcode " _
                          & "WHERE t1.hpercode LIKE '" & txtSearch.Text & "%' OR t1.patlast + ', ' + t1.patfirst LIKE '" & txtSearch.Text & "%' " _
                          & "ORDER BY t1.hpercode ASC"

        ConnectSQL()
        rsSQL.Connection = connSQL
        rsSQL.CommandType = CommandType.Text
        rsSQL.CommandText = SQL
        readerSQL = rsSQL.ExecuteReader
        While readerSQL.Read
            iItem = New ListViewItem(readerSQL(0).ToString, 0)
            iItem.Checked = False
            For x As Integer = 1 To 4 Step 1
                iItem.SubItems.Add(readerSQL(x).ToString())
            Next
            lvList.Items.Add(iItem)
        End While
        DisconnectSQL()

    End Sub
End Class