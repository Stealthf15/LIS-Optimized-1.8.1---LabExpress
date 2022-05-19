<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSystemConfig
    Inherits DevExpress.XtraEditors.XtraForm

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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSystemConfig))
        Me.cboType = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.label1 = New DevExpress.XtraEditors.LabelControl()
        Me.btnClose = New DevExpress.XtraEditors.SimpleButton()
        Me.btnSave = New DevExpress.XtraEditors.SimpleButton()
        Me.XtraTabControl1 = New DevExpress.XtraTab.XtraTabControl()
        Me.XtraTabPage1 = New DevExpress.XtraTab.XtraTabPage()
        Me.cboAuthenticate = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.LabelControl4 = New DevExpress.XtraEditors.LabelControl()
        Me.cboLISType = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.LabelControl3 = New DevExpress.XtraEditors.LabelControl()
        Me.cboPrinteName = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.chkBarcode = New System.Windows.Forms.CheckBox()
        Me.XtraTabPage3 = New DevExpress.XtraTab.XtraTabPage()
        Me.LabelControl17 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl16 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl11 = New DevExpress.XtraEditors.LabelControl()
        Me.txtPaperHeight = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl12 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl13 = New DevExpress.XtraEditors.LabelControl()
        Me.txtPaperWidth = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl10 = New DevExpress.XtraEditors.LabelControl()
        Me.txtBCHeight = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl8 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl9 = New DevExpress.XtraEditors.LabelControl()
        Me.txtBCWidth = New DevExpress.XtraEditors.TextEdit()
        Me.XtraTabPage2 = New DevExpress.XtraTab.XtraTabPage()
        Me.chkHL7Write = New System.Windows.Forms.CheckBox()
        Me.chkHL7Read = New System.Windows.Forms.CheckBox()
        Me.LabelControl7 = New DevExpress.XtraEditors.LabelControl()
        Me.txtHL7Destination = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl6 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl5 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.txtHL7Location = New DevExpress.XtraEditors.TextEdit()
        Me.XtraTabPage4 = New DevExpress.XtraTab.XtraTabPage()
        Me.LabelControl22 = New DevExpress.XtraEditors.LabelControl()
        Me.txtSQLDBName = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl20 = New DevExpress.XtraEditors.LabelControl()
        Me.txtSQLPWD = New DevExpress.XtraEditors.TextEdit()
        Me.chkEnableSQL = New System.Windows.Forms.CheckBox()
        Me.LabelControl18 = New DevExpress.XtraEditors.LabelControl()
        Me.txtSQLUID = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl19 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl21 = New DevExpress.XtraEditors.LabelControl()
        Me.txtSQLServerName = New DevExpress.XtraEditors.TextEdit()
        Me.XtraTabPage6 = New DevExpress.XtraTab.XtraTabPage()
        Me.chkPDF = New System.Windows.Forms.CheckBox()
        Me.LabelControl14 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl15 = New DevExpress.XtraEditors.LabelControl()
        Me.txtPDF = New DevExpress.XtraEditors.TextEdit()
        CType(Me.cboType.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.XtraTabControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.XtraTabControl1.SuspendLayout()
        Me.XtraTabPage1.SuspendLayout()
        CType(Me.cboAuthenticate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboLISType.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboPrinteName.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.XtraTabPage3.SuspendLayout()
        CType(Me.txtPaperHeight.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtPaperWidth.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtBCHeight.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtBCWidth.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.XtraTabPage2.SuspendLayout()
        CType(Me.txtHL7Destination.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtHL7Location.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.XtraTabPage4.SuspendLayout()
        CType(Me.txtSQLDBName.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtSQLPWD.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtSQLUID.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtSQLServerName.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.XtraTabPage6.SuspendLayout()
        CType(Me.txtPDF.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cboType
        '
        Me.cboType.EditValue = ""
        Me.cboType.Location = New System.Drawing.Point(147, 74)
        Me.cboType.Name = "cboType"
        Me.cboType.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI", 9.75!)
        Me.cboType.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(27, Byte), Integer), CType(CType(41, Byte), Integer), CType(CType(62, Byte), Integer))
        Me.cboType.Properties.Appearance.Options.UseFont = True
        Me.cboType.Properties.Appearance.Options.UseForeColor = True
        Me.cboType.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cboType.Properties.Items.AddRange(New Object() {"Default", "Per Account"})
        Me.cboType.Properties.LookAndFeel.SkinName = "Visual Studio 2013 Blue"
        Me.cboType.Properties.LookAndFeel.UseDefaultLookAndFeel = False
        Me.cboType.Size = New System.Drawing.Size(316, 24)
        Me.cboType.TabIndex = 0
        '
        'label1
        '
        Me.label1.Appearance.Font = New System.Drawing.Font("Segoe UI", 9.75!)
        Me.label1.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(27, Byte), Integer), CType(CType(41, Byte), Integer), CType(CType(62, Byte), Integer))
        Me.label1.Appearance.Options.UseFont = True
        Me.label1.Appearance.Options.UseForeColor = True
        Me.label1.Location = New System.Drawing.Point(19, 80)
        Me.label1.LookAndFeel.SkinName = "Visual Studio 2013 Blue"
        Me.label1.LookAndFeel.UseDefaultLookAndFeel = False
        Me.label1.Name = "label1"
        Me.label1.Size = New System.Drawing.Size(113, 17)
        Me.label1.TabIndex = 80
        Me.label1.Text = "MedTech Selection:"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.AppearanceHovered.BackColor = System.Drawing.Color.Gray
        Me.btnClose.AppearanceHovered.BackColor2 = System.Drawing.Color.Gray
        Me.btnClose.AppearanceHovered.BorderColor = System.Drawing.Color.Gray
        Me.btnClose.AppearanceHovered.Options.UseBackColor = True
        Me.btnClose.AppearanceHovered.Options.UseBorderColor = True
        Me.btnClose.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnClose.ImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.[True]
        Me.btnClose.ImageOptions.Image = CType(resources.GetObject("btnClose.ImageOptions.Image"), System.Drawing.Image)
        Me.btnClose.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter
        Me.btnClose.Location = New System.Drawing.Point(401, 220)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(108, 27)
        Me.btnClose.TabIndex = 102
        Me.btnClose.Text = "&Close"
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSave.AppearanceHovered.BackColor = System.Drawing.Color.Gray
        Me.btnSave.AppearanceHovered.BackColor2 = System.Drawing.Color.Gray
        Me.btnSave.AppearanceHovered.BorderColor = System.Drawing.Color.Gray
        Me.btnSave.AppearanceHovered.Options.UseBackColor = True
        Me.btnSave.AppearanceHovered.Options.UseBorderColor = True
        Me.btnSave.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnSave.ImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.[True]
        Me.btnSave.ImageOptions.Image = CType(resources.GetObject("btnSave.ImageOptions.Image"), System.Drawing.Image)
        Me.btnSave.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter
        Me.btnSave.Location = New System.Drawing.Point(287, 220)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(108, 27)
        Me.btnSave.TabIndex = 101
        Me.btnSave.Text = "&Save"
        '
        'XtraTabControl1
        '
        Me.XtraTabControl1.AppearancePage.Header.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold)
        Me.XtraTabControl1.AppearancePage.Header.Options.UseFont = True
        Me.XtraTabControl1.Location = New System.Drawing.Point(14, 13)
        Me.XtraTabControl1.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Office2003
        Me.XtraTabControl1.Name = "XtraTabControl1"
        Me.XtraTabControl1.SelectedTabPage = Me.XtraTabPage1
        Me.XtraTabControl1.Size = New System.Drawing.Size(495, 198)
        Me.XtraTabControl1.TabIndex = 120
        Me.XtraTabControl1.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.XtraTabPage1, Me.XtraTabPage3, Me.XtraTabPage2, Me.XtraTabPage4, Me.XtraTabPage6})
        '
        'XtraTabPage1
        '
        Me.XtraTabPage1.Controls.Add(Me.cboAuthenticate)
        Me.XtraTabPage1.Controls.Add(Me.LabelControl4)
        Me.XtraTabPage1.Controls.Add(Me.cboLISType)
        Me.XtraTabPage1.Controls.Add(Me.LabelControl3)
        Me.XtraTabPage1.Controls.Add(Me.cboPrinteName)
        Me.XtraTabPage1.Controls.Add(Me.LabelControl2)
        Me.XtraTabPage1.Controls.Add(Me.chkBarcode)
        Me.XtraTabPage1.Controls.Add(Me.cboType)
        Me.XtraTabPage1.Controls.Add(Me.label1)
        Me.XtraTabPage1.ImageOptions.Image = CType(resources.GetObject("XtraTabPage1.ImageOptions.Image"), System.Drawing.Image)
        Me.XtraTabPage1.Name = "XtraTabPage1"
        Me.XtraTabPage1.Size = New System.Drawing.Size(493, 166)
        Me.XtraTabPage1.Text = "General Configuration"
        '
        'cboAuthenticate
        '
        Me.cboAuthenticate.EditValue = "False"
        Me.cboAuthenticate.Location = New System.Drawing.Point(147, 44)
        Me.cboAuthenticate.Name = "cboAuthenticate"
        Me.cboAuthenticate.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI", 9.75!)
        Me.cboAuthenticate.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(27, Byte), Integer), CType(CType(41, Byte), Integer), CType(CType(62, Byte), Integer))
        Me.cboAuthenticate.Properties.Appearance.Options.UseFont = True
        Me.cboAuthenticate.Properties.Appearance.Options.UseForeColor = True
        Me.cboAuthenticate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cboAuthenticate.Properties.Items.AddRange(New Object() {"False", "True"})
        Me.cboAuthenticate.Properties.LookAndFeel.SkinName = "Visual Studio 2013 Blue"
        Me.cboAuthenticate.Properties.LookAndFeel.UseDefaultLookAndFeel = False
        Me.cboAuthenticate.Size = New System.Drawing.Size(316, 24)
        Me.cboAuthenticate.TabIndex = 105
        '
        'LabelControl4
        '
        Me.LabelControl4.Appearance.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.LabelControl4.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(27, Byte), Integer), CType(CType(41, Byte), Integer), CType(CType(62, Byte), Integer))
        Me.LabelControl4.Appearance.Options.UseFont = True
        Me.LabelControl4.Appearance.Options.UseForeColor = True
        Me.LabelControl4.Location = New System.Drawing.Point(19, 48)
        Me.LabelControl4.LookAndFeel.SkinName = "Visual Studio 2013 Blue"
        Me.LabelControl4.LookAndFeel.UseDefaultLookAndFeel = False
        Me.LabelControl4.Name = "LabelControl4"
        Me.LabelControl4.Size = New System.Drawing.Size(119, 17)
        Me.LabelControl4.TabIndex = 106
        Me.LabelControl4.Text = "Authenicate Release:"
        '
        'cboLISType
        '
        Me.cboLISType.EditValue = "Lite"
        Me.cboLISType.Location = New System.Drawing.Point(147, 14)
        Me.cboLISType.Name = "cboLISType"
        Me.cboLISType.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI", 9.75!)
        Me.cboLISType.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(27, Byte), Integer), CType(CType(41, Byte), Integer), CType(CType(62, Byte), Integer))
        Me.cboLISType.Properties.Appearance.Options.UseFont = True
        Me.cboLISType.Properties.Appearance.Options.UseForeColor = True
        Me.cboLISType.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cboLISType.Properties.Items.AddRange(New Object() {"Lite", "Professional"})
        Me.cboLISType.Properties.LookAndFeel.SkinName = "Visual Studio 2013 Blue"
        Me.cboLISType.Properties.LookAndFeel.UseDefaultLookAndFeel = False
        Me.cboLISType.Size = New System.Drawing.Size(316, 24)
        Me.cboLISType.TabIndex = 103
        '
        'LabelControl3
        '
        Me.LabelControl3.Appearance.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.LabelControl3.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(27, Byte), Integer), CType(CType(41, Byte), Integer), CType(CType(62, Byte), Integer))
        Me.LabelControl3.Appearance.Options.UseFont = True
        Me.LabelControl3.Appearance.Options.UseForeColor = True
        Me.LabelControl3.Location = New System.Drawing.Point(19, 18)
        Me.LabelControl3.LookAndFeel.SkinName = "Visual Studio 2013 Blue"
        Me.LabelControl3.LookAndFeel.UseDefaultLookAndFeel = False
        Me.LabelControl3.Name = "LabelControl3"
        Me.LabelControl3.Size = New System.Drawing.Size(51, 17)
        Me.LabelControl3.TabIndex = 104
        Me.LabelControl3.Text = "LIS Type:"
        '
        'cboPrinteName
        '
        Me.cboPrinteName.EditValue = ""
        Me.cboPrinteName.Location = New System.Drawing.Point(147, 127)
        Me.cboPrinteName.Name = "cboPrinteName"
        Me.cboPrinteName.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI", 9.75!)
        Me.cboPrinteName.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(27, Byte), Integer), CType(CType(41, Byte), Integer), CType(CType(62, Byte), Integer))
        Me.cboPrinteName.Properties.Appearance.Options.UseFont = True
        Me.cboPrinteName.Properties.Appearance.Options.UseForeColor = True
        Me.cboPrinteName.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cboPrinteName.Properties.Items.AddRange(New Object() {"Default", "Per Account"})
        Me.cboPrinteName.Properties.LookAndFeel.SkinName = "Visual Studio 2013 Blue"
        Me.cboPrinteName.Properties.LookAndFeel.UseDefaultLookAndFeel = False
        Me.cboPrinteName.Size = New System.Drawing.Size(316, 24)
        Me.cboPrinteName.TabIndex = 101
        '
        'LabelControl2
        '
        Me.LabelControl2.Appearance.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.LabelControl2.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(27, Byte), Integer), CType(CType(41, Byte), Integer), CType(CType(62, Byte), Integer))
        Me.LabelControl2.Appearance.Options.UseFont = True
        Me.LabelControl2.Appearance.Options.UseForeColor = True
        Me.LabelControl2.Location = New System.Drawing.Point(19, 131)
        Me.LabelControl2.LookAndFeel.SkinName = "Visual Studio 2013 Blue"
        Me.LabelControl2.LookAndFeel.UseDefaultLookAndFeel = False
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Size = New System.Drawing.Size(80, 17)
        Me.LabelControl2.TabIndex = 102
        Me.LabelControl2.Text = "Printer Name:"
        '
        'chkBarcode
        '
        Me.chkBarcode.AutoSize = True
        Me.chkBarcode.Location = New System.Drawing.Point(147, 104)
        Me.chkBarcode.Name = "chkBarcode"
        Me.chkBarcode.Size = New System.Drawing.Size(137, 17)
        Me.chkBarcode.TabIndex = 98
        Me.chkBarcode.Text = "Enable Print Barcode?"
        Me.chkBarcode.UseVisualStyleBackColor = True
        '
        'XtraTabPage3
        '
        Me.XtraTabPage3.Controls.Add(Me.LabelControl17)
        Me.XtraTabPage3.Controls.Add(Me.LabelControl16)
        Me.XtraTabPage3.Controls.Add(Me.LabelControl11)
        Me.XtraTabPage3.Controls.Add(Me.txtPaperHeight)
        Me.XtraTabPage3.Controls.Add(Me.LabelControl12)
        Me.XtraTabPage3.Controls.Add(Me.LabelControl13)
        Me.XtraTabPage3.Controls.Add(Me.txtPaperWidth)
        Me.XtraTabPage3.Controls.Add(Me.LabelControl10)
        Me.XtraTabPage3.Controls.Add(Me.txtBCHeight)
        Me.XtraTabPage3.Controls.Add(Me.LabelControl8)
        Me.XtraTabPage3.Controls.Add(Me.LabelControl9)
        Me.XtraTabPage3.Controls.Add(Me.txtBCWidth)
        Me.XtraTabPage3.ImageOptions.SvgImage = CType(resources.GetObject("XtraTabPage3.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.XtraTabPage3.ImageOptions.SvgImageSize = New System.Drawing.Size(16, 16)
        Me.XtraTabPage3.Name = "XtraTabPage3"
        Me.XtraTabPage3.Size = New System.Drawing.Size(493, 166)
        Me.XtraTabPage3.Text = "Page Setup"
        '
        'LabelControl17
        '
        Me.LabelControl17.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(27, Byte), Integer), CType(CType(41, Byte), Integer), CType(CType(62, Byte), Integer))
        Me.LabelControl17.Appearance.Options.UseForeColor = True
        Me.LabelControl17.Location = New System.Drawing.Point(337, 112)
        Me.LabelControl17.LookAndFeel.SkinName = "Visual Studio 2013 Blue"
        Me.LabelControl17.LookAndFeel.UseDefaultLookAndFeel = False
        Me.LabelControl17.Name = "LabelControl17"
        Me.LabelControl17.Size = New System.Drawing.Size(97, 13)
        Me.LabelControl17.TabIndex = 106
        Me.LabelControl17.Text = "set size inches (8.5)"
        '
        'LabelControl16
        '
        Me.LabelControl16.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(27, Byte), Integer), CType(CType(41, Byte), Integer), CType(CType(62, Byte), Integer))
        Me.LabelControl16.Appearance.Options.UseForeColor = True
        Me.LabelControl16.Location = New System.Drawing.Point(337, 40)
        Me.LabelControl16.LookAndFeel.SkinName = "Visual Studio 2013 Blue"
        Me.LabelControl16.LookAndFeel.UseDefaultLookAndFeel = False
        Me.LabelControl16.Name = "LabelControl16"
        Me.LabelControl16.Size = New System.Drawing.Size(94, 13)
        Me.LabelControl16.TabIndex = 105
        Me.LabelControl16.Text = "set size in cm (200)"
        '
        'LabelControl11
        '
        Me.LabelControl11.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(27, Byte), Integer), CType(CType(41, Byte), Integer), CType(CType(62, Byte), Integer))
        Me.LabelControl11.Appearance.Options.UseForeColor = True
        Me.LabelControl11.Location = New System.Drawing.Point(57, 141)
        Me.LabelControl11.LookAndFeel.SkinName = "Visual Studio 2013 Blue"
        Me.LabelControl11.LookAndFeel.UseDefaultLookAndFeel = False
        Me.LabelControl11.Name = "LabelControl11"
        Me.LabelControl11.Size = New System.Drawing.Size(38, 13)
        Me.LabelControl11.TabIndex = 104
        Me.LabelControl11.Text = "Height:"
        '
        'txtPaperHeight
        '
        Me.txtPaperHeight.EditValue = ""
        Me.txtPaperHeight.Location = New System.Drawing.Point(146, 135)
        Me.txtPaperHeight.Name = "txtPaperHeight"
        Me.txtPaperHeight.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(27, Byte), Integer), CType(CType(41, Byte), Integer), CType(CType(62, Byte), Integer))
        Me.txtPaperHeight.Properties.Appearance.Options.UseForeColor = True
        Me.txtPaperHeight.Properties.LookAndFeel.SkinName = "Visual Studio 2013 Blue"
        Me.txtPaperHeight.Properties.LookAndFeel.UseDefaultLookAndFeel = False
        Me.txtPaperHeight.Size = New System.Drawing.Size(185, 20)
        Me.txtPaperHeight.TabIndex = 103
        '
        'LabelControl12
        '
        Me.LabelControl12.Appearance.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.LabelControl12.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(27, Byte), Integer), CType(CType(41, Byte), Integer), CType(CType(62, Byte), Integer))
        Me.LabelControl12.Appearance.Options.UseFont = True
        Me.LabelControl12.Appearance.Options.UseForeColor = True
        Me.LabelControl12.Location = New System.Drawing.Point(29, 86)
        Me.LabelControl12.LookAndFeel.SkinName = "Visual Studio 2013 Blue"
        Me.LabelControl12.LookAndFeel.UseDefaultLookAndFeel = False
        Me.LabelControl12.Name = "LabelControl12"
        Me.LabelControl12.Size = New System.Drawing.Size(63, 17)
        Me.LabelControl12.TabIndex = 102
        Me.LabelControl12.Text = "Paper Size"
        '
        'LabelControl13
        '
        Me.LabelControl13.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(27, Byte), Integer), CType(CType(41, Byte), Integer), CType(CType(62, Byte), Integer))
        Me.LabelControl13.Appearance.Options.UseForeColor = True
        Me.LabelControl13.Location = New System.Drawing.Point(57, 115)
        Me.LabelControl13.LookAndFeel.SkinName = "Visual Studio 2013 Blue"
        Me.LabelControl13.LookAndFeel.UseDefaultLookAndFeel = False
        Me.LabelControl13.Name = "LabelControl13"
        Me.LabelControl13.Size = New System.Drawing.Size(35, 13)
        Me.LabelControl13.TabIndex = 101
        Me.LabelControl13.Text = "Width:"
        '
        'txtPaperWidth
        '
        Me.txtPaperWidth.EditValue = ""
        Me.txtPaperWidth.Location = New System.Drawing.Point(146, 109)
        Me.txtPaperWidth.Name = "txtPaperWidth"
        Me.txtPaperWidth.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(27, Byte), Integer), CType(CType(41, Byte), Integer), CType(CType(62, Byte), Integer))
        Me.txtPaperWidth.Properties.Appearance.Options.UseForeColor = True
        Me.txtPaperWidth.Properties.LookAndFeel.SkinName = "Visual Studio 2013 Blue"
        Me.txtPaperWidth.Properties.LookAndFeel.UseDefaultLookAndFeel = False
        Me.txtPaperWidth.Size = New System.Drawing.Size(185, 20)
        Me.txtPaperWidth.TabIndex = 100
        '
        'LabelControl10
        '
        Me.LabelControl10.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(27, Byte), Integer), CType(CType(41, Byte), Integer), CType(CType(62, Byte), Integer))
        Me.LabelControl10.Appearance.Options.UseForeColor = True
        Me.LabelControl10.Location = New System.Drawing.Point(57, 66)
        Me.LabelControl10.LookAndFeel.SkinName = "Visual Studio 2013 Blue"
        Me.LabelControl10.LookAndFeel.UseDefaultLookAndFeel = False
        Me.LabelControl10.Name = "LabelControl10"
        Me.LabelControl10.Size = New System.Drawing.Size(38, 13)
        Me.LabelControl10.TabIndex = 99
        Me.LabelControl10.Text = "Height:"
        '
        'txtBCHeight
        '
        Me.txtBCHeight.EditValue = ""
        Me.txtBCHeight.Location = New System.Drawing.Point(146, 60)
        Me.txtBCHeight.Name = "txtBCHeight"
        Me.txtBCHeight.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(27, Byte), Integer), CType(CType(41, Byte), Integer), CType(CType(62, Byte), Integer))
        Me.txtBCHeight.Properties.Appearance.Options.UseForeColor = True
        Me.txtBCHeight.Properties.LookAndFeel.SkinName = "Visual Studio 2013 Blue"
        Me.txtBCHeight.Properties.LookAndFeel.UseDefaultLookAndFeel = False
        Me.txtBCHeight.Size = New System.Drawing.Size(185, 20)
        Me.txtBCHeight.TabIndex = 98
        '
        'LabelControl8
        '
        Me.LabelControl8.Appearance.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.LabelControl8.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(27, Byte), Integer), CType(CType(41, Byte), Integer), CType(CType(62, Byte), Integer))
        Me.LabelControl8.Appearance.Options.UseFont = True
        Me.LabelControl8.Appearance.Options.UseForeColor = True
        Me.LabelControl8.Location = New System.Drawing.Point(29, 11)
        Me.LabelControl8.LookAndFeel.SkinName = "Visual Studio 2013 Blue"
        Me.LabelControl8.LookAndFeel.UseDefaultLookAndFeel = False
        Me.LabelControl8.Name = "LabelControl8"
        Me.LabelControl8.Size = New System.Drawing.Size(126, 17)
        Me.LabelControl8.TabIndex = 97
        Me.LabelControl8.Text = "Barcode Sticker Size:"
        '
        'LabelControl9
        '
        Me.LabelControl9.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(27, Byte), Integer), CType(CType(41, Byte), Integer), CType(CType(62, Byte), Integer))
        Me.LabelControl9.Appearance.Options.UseForeColor = True
        Me.LabelControl9.Location = New System.Drawing.Point(57, 40)
        Me.LabelControl9.LookAndFeel.SkinName = "Visual Studio 2013 Blue"
        Me.LabelControl9.LookAndFeel.UseDefaultLookAndFeel = False
        Me.LabelControl9.Name = "LabelControl9"
        Me.LabelControl9.Size = New System.Drawing.Size(35, 13)
        Me.LabelControl9.TabIndex = 96
        Me.LabelControl9.Text = "Width:"
        '
        'txtBCWidth
        '
        Me.txtBCWidth.EditValue = ""
        Me.txtBCWidth.Location = New System.Drawing.Point(146, 34)
        Me.txtBCWidth.Name = "txtBCWidth"
        Me.txtBCWidth.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(27, Byte), Integer), CType(CType(41, Byte), Integer), CType(CType(62, Byte), Integer))
        Me.txtBCWidth.Properties.Appearance.Options.UseForeColor = True
        Me.txtBCWidth.Properties.LookAndFeel.SkinName = "Visual Studio 2013 Blue"
        Me.txtBCWidth.Properties.LookAndFeel.UseDefaultLookAndFeel = False
        Me.txtBCWidth.Size = New System.Drawing.Size(185, 20)
        Me.txtBCWidth.TabIndex = 95
        '
        'XtraTabPage2
        '
        Me.XtraTabPage2.Controls.Add(Me.chkHL7Write)
        Me.XtraTabPage2.Controls.Add(Me.chkHL7Read)
        Me.XtraTabPage2.Controls.Add(Me.LabelControl7)
        Me.XtraTabPage2.Controls.Add(Me.txtHL7Destination)
        Me.XtraTabPage2.Controls.Add(Me.LabelControl6)
        Me.XtraTabPage2.Controls.Add(Me.LabelControl5)
        Me.XtraTabPage2.Controls.Add(Me.LabelControl1)
        Me.XtraTabPage2.Controls.Add(Me.txtHL7Location)
        Me.XtraTabPage2.ImageOptions.Image = CType(resources.GetObject("XtraTabPage2.ImageOptions.Image"), System.Drawing.Image)
        Me.XtraTabPage2.Name = "XtraTabPage2"
        Me.XtraTabPage2.Size = New System.Drawing.Size(493, 166)
        Me.XtraTabPage2.Text = "HL7 File"
        '
        'chkHL7Write
        '
        Me.chkHL7Write.AutoSize = True
        Me.chkHL7Write.Location = New System.Drawing.Point(178, 134)
        Me.chkHL7Write.Name = "chkHL7Write"
        Me.chkHL7Write.Size = New System.Drawing.Size(119, 17)
        Me.chkHL7Write.TabIndex = 98
        Me.chkHL7Write.Text = "Enable Write HL7?"
        Me.chkHL7Write.UseVisualStyleBackColor = True
        '
        'chkHL7Read
        '
        Me.chkHL7Read.AutoSize = True
        Me.chkHL7Read.Location = New System.Drawing.Point(30, 134)
        Me.chkHL7Read.Name = "chkHL7Read"
        Me.chkHL7Read.Size = New System.Drawing.Size(117, 17)
        Me.chkHL7Read.TabIndex = 97
        Me.chkHL7Read.Text = "Enable Read HL7?"
        Me.chkHL7Read.UseVisualStyleBackColor = True
        '
        'LabelControl7
        '
        Me.LabelControl7.Appearance.Font = New System.Drawing.Font("Segoe UI", 9.75!)
        Me.LabelControl7.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(27, Byte), Integer), CType(CType(41, Byte), Integer), CType(CType(62, Byte), Integer))
        Me.LabelControl7.Appearance.Options.UseFont = True
        Me.LabelControl7.Appearance.Options.UseForeColor = True
        Me.LabelControl7.Location = New System.Drawing.Point(58, 100)
        Me.LabelControl7.LookAndFeel.SkinName = "Visual Studio 2013 Blue"
        Me.LabelControl7.LookAndFeel.UseDefaultLookAndFeel = False
        Me.LabelControl7.Name = "LabelControl7"
        Me.LabelControl7.Size = New System.Drawing.Size(48, 17)
        Me.LabelControl7.TabIndex = 96
        Me.LabelControl7.Text = "HL7 File:"
        '
        'txtHL7Destination
        '
        Me.txtHL7Destination.EditValue = ""
        Me.txtHL7Destination.Location = New System.Drawing.Point(147, 94)
        Me.txtHL7Destination.Name = "txtHL7Destination"
        Me.txtHL7Destination.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI", 9.75!)
        Me.txtHL7Destination.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(27, Byte), Integer), CType(CType(41, Byte), Integer), CType(CType(62, Byte), Integer))
        Me.txtHL7Destination.Properties.Appearance.Options.UseFont = True
        Me.txtHL7Destination.Properties.Appearance.Options.UseForeColor = True
        Me.txtHL7Destination.Properties.LookAndFeel.SkinName = "Visual Studio 2013 Blue"
        Me.txtHL7Destination.Properties.LookAndFeel.UseDefaultLookAndFeel = False
        Me.txtHL7Destination.Size = New System.Drawing.Size(316, 24)
        Me.txtHL7Destination.TabIndex = 95
        '
        'LabelControl6
        '
        Me.LabelControl6.Appearance.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.LabelControl6.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(27, Byte), Integer), CType(CType(41, Byte), Integer), CType(CType(62, Byte), Integer))
        Me.LabelControl6.Appearance.Options.UseFont = True
        Me.LabelControl6.Appearance.Options.UseForeColor = True
        Me.LabelControl6.Location = New System.Drawing.Point(30, 15)
        Me.LabelControl6.LookAndFeel.SkinName = "Visual Studio 2013 Blue"
        Me.LabelControl6.LookAndFeel.UseDefaultLookAndFeel = False
        Me.LabelControl6.Name = "LabelControl6"
        Me.LabelControl6.Size = New System.Drawing.Size(111, 17)
        Me.LabelControl6.TabIndex = 94
        Me.LabelControl6.Text = "HL7 File Location:"
        '
        'LabelControl5
        '
        Me.LabelControl5.Appearance.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.LabelControl5.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(27, Byte), Integer), CType(CType(41, Byte), Integer), CType(CType(62, Byte), Integer))
        Me.LabelControl5.Appearance.Options.UseFont = True
        Me.LabelControl5.Appearance.Options.UseForeColor = True
        Me.LabelControl5.Location = New System.Drawing.Point(30, 72)
        Me.LabelControl5.LookAndFeel.SkinName = "Visual Studio 2013 Blue"
        Me.LabelControl5.LookAndFeel.UseDefaultLookAndFeel = False
        Me.LabelControl5.Name = "LabelControl5"
        Me.LabelControl5.Size = New System.Drawing.Size(130, 17)
        Me.LabelControl5.TabIndex = 93
        Me.LabelControl5.Text = "HL7 File Destination:"
        '
        'LabelControl1
        '
        Me.LabelControl1.Appearance.Font = New System.Drawing.Font("Segoe UI", 9.75!)
        Me.LabelControl1.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(27, Byte), Integer), CType(CType(41, Byte), Integer), CType(CType(62, Byte), Integer))
        Me.LabelControl1.Appearance.Options.UseFont = True
        Me.LabelControl1.Appearance.Options.UseForeColor = True
        Me.LabelControl1.Location = New System.Drawing.Point(58, 44)
        Me.LabelControl1.LookAndFeel.SkinName = "Visual Studio 2013 Blue"
        Me.LabelControl1.LookAndFeel.UseDefaultLookAndFeel = False
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(48, 17)
        Me.LabelControl1.TabIndex = 86
        Me.LabelControl1.Text = "HL7 File:"
        '
        'txtHL7Location
        '
        Me.txtHL7Location.EditValue = ""
        Me.txtHL7Location.Location = New System.Drawing.Point(147, 38)
        Me.txtHL7Location.Name = "txtHL7Location"
        Me.txtHL7Location.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI", 9.75!)
        Me.txtHL7Location.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(27, Byte), Integer), CType(CType(41, Byte), Integer), CType(CType(62, Byte), Integer))
        Me.txtHL7Location.Properties.Appearance.Options.UseFont = True
        Me.txtHL7Location.Properties.Appearance.Options.UseForeColor = True
        Me.txtHL7Location.Properties.LookAndFeel.SkinName = "Visual Studio 2013 Blue"
        Me.txtHL7Location.Properties.LookAndFeel.UseDefaultLookAndFeel = False
        Me.txtHL7Location.Size = New System.Drawing.Size(316, 24)
        Me.txtHL7Location.TabIndex = 85
        '
        'XtraTabPage4
        '
        Me.XtraTabPage4.Controls.Add(Me.LabelControl22)
        Me.XtraTabPage4.Controls.Add(Me.txtSQLDBName)
        Me.XtraTabPage4.Controls.Add(Me.LabelControl20)
        Me.XtraTabPage4.Controls.Add(Me.txtSQLPWD)
        Me.XtraTabPage4.Controls.Add(Me.chkEnableSQL)
        Me.XtraTabPage4.Controls.Add(Me.LabelControl18)
        Me.XtraTabPage4.Controls.Add(Me.txtSQLUID)
        Me.XtraTabPage4.Controls.Add(Me.LabelControl19)
        Me.XtraTabPage4.Controls.Add(Me.LabelControl21)
        Me.XtraTabPage4.Controls.Add(Me.txtSQLServerName)
        Me.XtraTabPage4.ImageOptions.SvgImage = CType(resources.GetObject("XtraTabPage4.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.XtraTabPage4.ImageOptions.SvgImageSize = New System.Drawing.Size(16, 16)
        Me.XtraTabPage4.Name = "XtraTabPage4"
        Me.XtraTabPage4.Size = New System.Drawing.Size(493, 166)
        Me.XtraTabPage4.Text = "SQL"
        '
        'LabelControl22
        '
        Me.LabelControl22.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(27, Byte), Integer), CType(CType(41, Byte), Integer), CType(CType(62, Byte), Integer))
        Me.LabelControl22.Appearance.Options.UseForeColor = True
        Me.LabelControl22.Location = New System.Drawing.Point(58, 117)
        Me.LabelControl22.LookAndFeel.SkinName = "Visual Studio 2013 Blue"
        Me.LabelControl22.LookAndFeel.UseDefaultLookAndFeel = False
        Me.LabelControl22.Name = "LabelControl22"
        Me.LabelControl22.Size = New System.Drawing.Size(83, 13)
        Me.LabelControl22.TabIndex = 110
        Me.LabelControl22.Text = "Database Name:"
        '
        'txtSQLDBName
        '
        Me.txtSQLDBName.EditValue = ""
        Me.txtSQLDBName.Location = New System.Drawing.Point(147, 111)
        Me.txtSQLDBName.Name = "txtSQLDBName"
        Me.txtSQLDBName.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(27, Byte), Integer), CType(CType(41, Byte), Integer), CType(CType(62, Byte), Integer))
        Me.txtSQLDBName.Properties.Appearance.Options.UseForeColor = True
        Me.txtSQLDBName.Properties.LookAndFeel.SkinName = "Visual Studio 2013 Blue"
        Me.txtSQLDBName.Properties.LookAndFeel.UseDefaultLookAndFeel = False
        Me.txtSQLDBName.Size = New System.Drawing.Size(294, 20)
        Me.txtSQLDBName.TabIndex = 109
        '
        'LabelControl20
        '
        Me.LabelControl20.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(27, Byte), Integer), CType(CType(41, Byte), Integer), CType(CType(62, Byte), Integer))
        Me.LabelControl20.Appearance.Options.UseForeColor = True
        Me.LabelControl20.Location = New System.Drawing.Point(58, 91)
        Me.LabelControl20.LookAndFeel.SkinName = "Visual Studio 2013 Blue"
        Me.LabelControl20.LookAndFeel.UseDefaultLookAndFeel = False
        Me.LabelControl20.Name = "LabelControl20"
        Me.LabelControl20.Size = New System.Drawing.Size(52, 13)
        Me.LabelControl20.TabIndex = 108
        Me.LabelControl20.Text = "Password:"
        '
        'txtSQLPWD
        '
        Me.txtSQLPWD.EditValue = ""
        Me.txtSQLPWD.Location = New System.Drawing.Point(147, 85)
        Me.txtSQLPWD.Name = "txtSQLPWD"
        Me.txtSQLPWD.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(27, Byte), Integer), CType(CType(41, Byte), Integer), CType(CType(62, Byte), Integer))
        Me.txtSQLPWD.Properties.Appearance.Options.UseForeColor = True
        Me.txtSQLPWD.Properties.LookAndFeel.SkinName = "Visual Studio 2013 Blue"
        Me.txtSQLPWD.Properties.LookAndFeel.UseDefaultLookAndFeel = False
        Me.txtSQLPWD.Properties.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtSQLPWD.Size = New System.Drawing.Size(294, 20)
        Me.txtSQLPWD.TabIndex = 107
        '
        'chkEnableSQL
        '
        Me.chkEnableSQL.AutoSize = True
        Me.chkEnableSQL.Location = New System.Drawing.Point(30, 141)
        Me.chkEnableSQL.Name = "chkEnableSQL"
        Me.chkEnableSQL.Size = New System.Drawing.Size(169, 17)
        Me.chkEnableSQL.TabIndex = 105
        Me.chkEnableSQL.Text = "Enable iHOMIS Integration?"
        Me.chkEnableSQL.UseVisualStyleBackColor = True
        '
        'LabelControl18
        '
        Me.LabelControl18.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(27, Byte), Integer), CType(CType(41, Byte), Integer), CType(CType(62, Byte), Integer))
        Me.LabelControl18.Appearance.Options.UseForeColor = True
        Me.LabelControl18.Location = New System.Drawing.Point(58, 65)
        Me.LabelControl18.LookAndFeel.SkinName = "Visual Studio 2013 Blue"
        Me.LabelControl18.LookAndFeel.UseDefaultLookAndFeel = False
        Me.LabelControl18.Name = "LabelControl18"
        Me.LabelControl18.Size = New System.Drawing.Size(54, 13)
        Me.LabelControl18.TabIndex = 104
        Me.LabelControl18.Text = "Username:"
        '
        'txtSQLUID
        '
        Me.txtSQLUID.EditValue = ""
        Me.txtSQLUID.Location = New System.Drawing.Point(147, 59)
        Me.txtSQLUID.Name = "txtSQLUID"
        Me.txtSQLUID.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(27, Byte), Integer), CType(CType(41, Byte), Integer), CType(CType(62, Byte), Integer))
        Me.txtSQLUID.Properties.Appearance.Options.UseForeColor = True
        Me.txtSQLUID.Properties.LookAndFeel.SkinName = "Visual Studio 2013 Blue"
        Me.txtSQLUID.Properties.LookAndFeel.UseDefaultLookAndFeel = False
        Me.txtSQLUID.Size = New System.Drawing.Size(294, 20)
        Me.txtSQLUID.TabIndex = 103
        '
        'LabelControl19
        '
        Me.LabelControl19.Appearance.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl19.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(27, Byte), Integer), CType(CType(41, Byte), Integer), CType(CType(62, Byte), Integer))
        Me.LabelControl19.Appearance.Options.UseFont = True
        Me.LabelControl19.Appearance.Options.UseForeColor = True
        Me.LabelControl19.Location = New System.Drawing.Point(30, 12)
        Me.LabelControl19.LookAndFeel.SkinName = "Visual Studio 2013 Blue"
        Me.LabelControl19.LookAndFeel.UseDefaultLookAndFeel = False
        Me.LabelControl19.Name = "LabelControl19"
        Me.LabelControl19.Size = New System.Drawing.Size(123, 13)
        Me.LabelControl19.TabIndex = 102
        Me.LabelControl19.Text = "iHOMIS DB Connection:"
        '
        'LabelControl21
        '
        Me.LabelControl21.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(27, Byte), Integer), CType(CType(41, Byte), Integer), CType(CType(62, Byte), Integer))
        Me.LabelControl21.Appearance.Options.UseForeColor = True
        Me.LabelControl21.Location = New System.Drawing.Point(58, 39)
        Me.LabelControl21.LookAndFeel.SkinName = "Visual Studio 2013 Blue"
        Me.LabelControl21.LookAndFeel.UseDefaultLookAndFeel = False
        Me.LabelControl21.Name = "LabelControl21"
        Me.LabelControl21.Size = New System.Drawing.Size(66, 13)
        Me.LabelControl21.TabIndex = 100
        Me.LabelControl21.Text = "Server Name:"
        '
        'txtSQLServerName
        '
        Me.txtSQLServerName.EditValue = ""
        Me.txtSQLServerName.Location = New System.Drawing.Point(147, 33)
        Me.txtSQLServerName.Name = "txtSQLServerName"
        Me.txtSQLServerName.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(27, Byte), Integer), CType(CType(41, Byte), Integer), CType(CType(62, Byte), Integer))
        Me.txtSQLServerName.Properties.Appearance.Options.UseForeColor = True
        Me.txtSQLServerName.Properties.LookAndFeel.SkinName = "Visual Studio 2013 Blue"
        Me.txtSQLServerName.Properties.LookAndFeel.UseDefaultLookAndFeel = False
        Me.txtSQLServerName.Size = New System.Drawing.Size(294, 20)
        Me.txtSQLServerName.TabIndex = 99
        '
        'XtraTabPage6
        '
        Me.XtraTabPage6.Controls.Add(Me.chkPDF)
        Me.XtraTabPage6.Controls.Add(Me.LabelControl14)
        Me.XtraTabPage6.Controls.Add(Me.LabelControl15)
        Me.XtraTabPage6.Controls.Add(Me.txtPDF)
        Me.XtraTabPage6.ImageOptions.Image = CType(resources.GetObject("XtraTabPage6.ImageOptions.Image"), System.Drawing.Image)
        Me.XtraTabPage6.Name = "XtraTabPage6"
        Me.XtraTabPage6.Size = New System.Drawing.Size(493, 166)
        Me.XtraTabPage6.Text = "PDF"
        '
        'chkPDF
        '
        Me.chkPDF.AutoSize = True
        Me.chkPDF.Location = New System.Drawing.Point(25, 142)
        Me.chkPDF.Name = "chkPDF"
        Me.chkPDF.Size = New System.Drawing.Size(126, 17)
        Me.chkPDF.TabIndex = 102
        Me.chkPDF.Text = "Save Result as PDF?"
        Me.chkPDF.UseVisualStyleBackColor = True
        '
        'LabelControl14
        '
        Me.LabelControl14.Appearance.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold)
        Me.LabelControl14.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(27, Byte), Integer), CType(CType(41, Byte), Integer), CType(CType(62, Byte), Integer))
        Me.LabelControl14.Appearance.Options.UseFont = True
        Me.LabelControl14.Appearance.Options.UseForeColor = True
        Me.LabelControl14.Location = New System.Drawing.Point(25, 31)
        Me.LabelControl14.LookAndFeel.SkinName = "Visual Studio 2013 Blue"
        Me.LabelControl14.LookAndFeel.UseDefaultLookAndFeel = False
        Me.LabelControl14.Name = "LabelControl14"
        Me.LabelControl14.Size = New System.Drawing.Size(112, 17)
        Me.LabelControl14.TabIndex = 101
        Me.LabelControl14.Text = "PDF File Location:"
        '
        'LabelControl15
        '
        Me.LabelControl15.Appearance.Font = New System.Drawing.Font("Segoe UI", 9.75!)
        Me.LabelControl15.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(27, Byte), Integer), CType(CType(41, Byte), Integer), CType(CType(62, Byte), Integer))
        Me.LabelControl15.Appearance.Options.UseFont = True
        Me.LabelControl15.Appearance.Options.UseForeColor = True
        Me.LabelControl15.Location = New System.Drawing.Point(53, 60)
        Me.LabelControl15.LookAndFeel.SkinName = "Visual Studio 2013 Blue"
        Me.LabelControl15.LookAndFeel.UseDefaultLookAndFeel = False
        Me.LabelControl15.Name = "LabelControl15"
        Me.LabelControl15.Size = New System.Drawing.Size(48, 17)
        Me.LabelControl15.TabIndex = 100
        Me.LabelControl15.Text = "PDF File:"
        '
        'txtPDF
        '
        Me.txtPDF.EditValue = ""
        Me.txtPDF.Location = New System.Drawing.Point(142, 54)
        Me.txtPDF.Name = "txtPDF"
        Me.txtPDF.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI", 9.75!)
        Me.txtPDF.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(27, Byte), Integer), CType(CType(41, Byte), Integer), CType(CType(62, Byte), Integer))
        Me.txtPDF.Properties.Appearance.Options.UseFont = True
        Me.txtPDF.Properties.Appearance.Options.UseForeColor = True
        Me.txtPDF.Properties.LookAndFeel.SkinName = "Visual Studio 2013 Blue"
        Me.txtPDF.Properties.LookAndFeel.UseDefaultLookAndFeel = False
        Me.txtPDF.Size = New System.Drawing.Size(316, 24)
        Me.txtPDF.TabIndex = 99
        '
        'frmSystemConfig
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(523, 258)
        Me.Controls.Add(Me.XtraTabControl1)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.btnSave)
        Me.FormBorderEffect = DevExpress.XtraEditors.FormBorderEffect.Glow
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.name = "frmSystemConfig"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "System Configuration"
        CType(Me.cboType.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.XtraTabControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.XtraTabControl1.ResumeLayout(False)
        Me.XtraTabPage1.ResumeLayout(False)
        Me.XtraTabPage1.PerformLayout()
        CType(Me.cboAuthenticate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboLISType.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboPrinteName.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.XtraTabPage3.ResumeLayout(False)
        Me.XtraTabPage3.PerformLayout()
        CType(Me.txtPaperHeight.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtPaperWidth.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtBCHeight.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtBCWidth.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.XtraTabPage2.ResumeLayout(False)
        Me.XtraTabPage2.PerformLayout()
        CType(Me.txtHL7Destination.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtHL7Location.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.XtraTabPage4.ResumeLayout(False)
        Me.XtraTabPage4.PerformLayout()
        CType(Me.txtSQLDBName.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtSQLPWD.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtSQLUID.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtSQLServerName.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.XtraTabPage6.ResumeLayout(False)
        Me.XtraTabPage6.PerformLayout()
        CType(Me.txtPDF.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents label1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents cboType As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents btnClose As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnSave As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents XtraTabControl1 As DevExpress.XtraTab.XtraTabControl
    Friend WithEvents XtraTabPage1 As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents XtraTabPage2 As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents LabelControl7 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents txtHL7Destination As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl6 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl5 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents txtHL7Location As DevExpress.XtraEditors.TextEdit
    Friend WithEvents chkHL7Write As System.Windows.Forms.CheckBox
    Friend WithEvents chkHL7Read As System.Windows.Forms.CheckBox
    Friend WithEvents chkBarcode As System.Windows.Forms.CheckBox
    Friend WithEvents XtraTabPage6 As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents chkPDF As System.Windows.Forms.CheckBox
    Friend WithEvents LabelControl14 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl15 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents txtPDF As DevExpress.XtraEditors.TextEdit
    Friend WithEvents cboPrinteName As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents cboLISType As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents LabelControl3 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents cboAuthenticate As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents LabelControl4 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents XtraTabPage3 As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents LabelControl17 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl16 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl11 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents txtPaperHeight As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl12 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl13 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents txtPaperWidth As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl10 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents txtBCHeight As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl8 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl9 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents txtBCWidth As DevExpress.XtraEditors.TextEdit
    Friend WithEvents XtraTabPage4 As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents LabelControl22 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents txtSQLDBName As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl20 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents txtSQLPWD As DevExpress.XtraEditors.TextEdit
    Friend WithEvents chkEnableSQL As CheckBox
    Friend WithEvents LabelControl18 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents txtSQLUID As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl19 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl21 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents txtSQLServerName As DevExpress.XtraEditors.TextEdit
End Class
