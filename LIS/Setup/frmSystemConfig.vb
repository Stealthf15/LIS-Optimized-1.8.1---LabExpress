Imports System.Drawing.Printing

Public Class frmSystemConfig

    Public name As String

    Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            With My.Settings
                .MedTech = cboType.SelectedIndex
                .HL7Destination = txtHL7Destination.Text
                .HL7Location = txtHL7Location.Text
                .HL7Read = chkHL7Read.CheckState
                .HL7Write = chkHL7Write.CheckState

                .PDFLocation = txtPDF.Text
                .SaveAsPDF = chkPDF.CheckState

                .PrintBarcode = chkBarcode.CheckState
                .BCPrinterName = cboPrinteName.Text
                .LISType = cboLISType.Text
                .AuthenticateRelease = cboAuthenticate.Text

                'Page Setup
                .BCWidth = txtBCWidth.Text
                .BCHeight = txtBCHeight.Text
                .PaperWidth = txtPaperWidth.Text
                .PaperHeight = txtPaperHeight.Text

                'Save SQL Settings
                .SQLServer = txtSQLServerName.Text
                .SQLUID = txtSQLUID.Text
                .SQLPWD = txtSQLPWD.Text
                .SQLDBName = txtSQLDBName.Text
                .SQLConnection = chkEnableSQL.CheckState
                .MyConnectionStringSQL = "SERVER = " & txtSQLServerName.Text & ";" & "DATABASE = " & txtSQLDBName.Text & "; UID = " & txtSQLUID.Text & "; PWD = " & txtSQLPWD.Text & ";"

                .Save()
                .Reload()
            End With
            Me.Close()
        Catch ex As Exception
            Disconnect()
            MessageBox.Show(ex.Message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub

    Private Sub frmTestTypeAE_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Me.Dispose()
    End Sub

    Private Sub frmSystemConfig_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            For Each prnt As String In PrinterSettings.InstalledPrinters
                cboPrinteName.Properties.Items.Add(prnt)
            Next

            With My.Settings
                cboType.SelectedIndex = .MedTech
                txtHL7Destination.Text = .HL7Destination
                txtHL7Location.Text = .HL7Location
                chkHL7Read.Checked = .HL7Read
                chkHL7Write.Checked = .HL7Write

                chkBarcode.Checked = .PrintBarcode
                cboPrinteName.Text = .BCPrinterName

                txtPDF.Text = .PDFLocation
                chkPDF.Checked = .SaveAsPDF
                cboLISType.Text = .LISType
                cboAuthenticate.Text = .AuthenticateRelease

                'Page Setup
                txtBCWidth.Text = .BCWidth
                txtBCHeight.Text = .BCHeight
                txtPaperWidth.Text = .PaperWidth
                txtPaperHeight.Text = .PaperHeight

                'Save SQL Settings
                txtSQLServerName.Text = .SQLServer
                txtSQLUID.Text = .SQLUID
                txtSQLPWD.Text = .SQLPWD
                txtSQLDBName.Text = .SQLDBName
                chkEnableSQL.Checked = .SQLConnection

            End With
        Catch ex As Exception
            Exit Sub
        End Try
    End Sub
End Class