Imports MySql.Data.MySqlClient
Public Class frmHemaChangeDateTime

    Public MainID As String
    Public Section As String
    Public SubSection As String

    Private Sub SimpleButton1_Click(sender As Object, e As EventArgs) Handles btnChange.Click

        If txtReason.Text = "" Then

            MessageBox.Show("Enter Reason for Date and Time Change.", "Required!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

        Else
            rs.Parameters.Clear()
            rs.Parameters.AddWithValue("@mainID", MainID)
            rs.Parameters.AddWithValue("@Section", Section)
            rs.Parameters.AddWithValue("@SubSection", SubSection)
            rs.Parameters.AddWithValue("@remarks", txtReason.Text)

            'MessageBox.Show(MainID & " " & Section & " " & SubSection)

            rs.Parameters.Add("@specimen_extracted_date_time", MySql.Data.MySqlClient.MySqlDbType.DateTime).Value = Format(dtChange.Value, "yyyy-MM-dd HH:mm:ss") 'version 1.6.0.0-beta
            rs.Parameters.AddWithValue("@specimen_extracted_date_time_remarks", dtChange.Value)

            UpdateRecordwthoutMSG("UPDATE `specimen_tracking` SET " _
                    & "`extracted` = @specimen_extracted_date_time" _
                    & " WHERE sample_id = @mainID AND section = @Section AND sub_section = @Subsection"
                    )

            UpdateRecordwthoutMSG("UPDATE `patient_remarks` SET " _
                    & "`remarks` = CONCAT(@remarks, CHAR(13, 10) , 'Changed Extracted Date and Time: ', @specimen_extracted_date_time_remarks)" _
                    & " WHERE sample_id = @mainID AND section = @Section AND sub_section = @Subsection"
                    )

            frmHemaNew.txtRemarks.Text = txtReason.Text & vbNewLine & "Changed Extracted Date and Time: " & dtChange.Value

            ActivityLogs(frmHemaNew.txtSampleID.Text,
                             frmHemaNew.txtPatientID.Text,
                              frmHemaNew.txtName.Text,
                             CurrUser,
                             "Changed Hamatology Extraction Date",
                             frmHemaNew.cboRequest.Text,
                             Section,
                             SubSection)

            MessageBox.Show("Extracted Date and Time has been updated.", "", MessageBoxButtons.OK, MessageBoxIcon.Information)

            Me.Close()
        End If
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    Private Sub frmHemaChangeDateTime_Closed(sender As Object, e As EventArgs) Handles Me.Closed
        Me.Dispose()
    End Sub
End Class