<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSeroChangeDateTime
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.dtChange = New System.Windows.Forms.DateTimePicker()
        Me.btnCancel = New DevExpress.XtraEditors.SimpleButton()
        Me.btnChange = New DevExpress.XtraEditors.SimpleButton()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.txtReason = New DevExpress.XtraEditors.MemoEdit()
        CType(Me.txtReason.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'dtChange
        '
        Me.dtChange.CalendarFont = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtChange.CustomFormat = "MM/dd/yyyy hh:mm:ss tt"
        Me.dtChange.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtChange.Location = New System.Drawing.Point(2, 31)
        Me.dtChange.Name = "dtChange"
        Me.dtChange.Size = New System.Drawing.Size(213, 20)
        Me.dtChange.TabIndex = 174
        '
        'btnCancel
        '
        Me.btnCancel.Location = New System.Drawing.Point(250, 214)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(75, 23)
        Me.btnCancel.TabIndex = 173
        Me.btnCancel.Text = "Cancel"
        '
        'btnChange
        '
        Me.btnChange.Location = New System.Drawing.Point(121, 214)
        Me.btnChange.Name = "btnChange"
        Me.btnChange.Size = New System.Drawing.Size(123, 23)
        Me.btnChange.TabIndex = 172
        Me.btnChange.Text = "Change Date && Time"
        '
        'LabelControl2
        '
        Me.LabelControl2.Location = New System.Drawing.Point(2, 58)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Size = New System.Drawing.Size(41, 13)
        Me.LabelControl2.TabIndex = 171
        Me.LabelControl2.Text = "Reason:"
        '
        'LabelControl1
        '
        Me.LabelControl1.Location = New System.Drawing.Point(3, 12)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(92, 13)
        Me.LabelControl1.TabIndex = 170
        Me.LabelControl1.Text = "New Date && Time:"
        '
        'txtReason
        '
        Me.txtReason.Location = New System.Drawing.Point(2, 77)
        Me.txtReason.Name = "txtReason"
        Me.txtReason.Size = New System.Drawing.Size(323, 129)
        Me.txtReason.TabIndex = 169
        '
        'frmSeroChangeDateTime
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(331, 244)
        Me.Controls.Add(Me.dtChange)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnChange)
        Me.Controls.Add(Me.LabelControl2)
        Me.Controls.Add(Me.LabelControl1)
        Me.Controls.Add(Me.txtReason)
        Me.Name = "frmSeroChangeDateTime"
        Me.Text = "frmSeroChangeDateTime"
        CType(Me.txtReason.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents dtChange As DateTimePicker
    Friend WithEvents btnCancel As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnChange As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents txtReason As DevExpress.XtraEditors.MemoEdit
End Class
