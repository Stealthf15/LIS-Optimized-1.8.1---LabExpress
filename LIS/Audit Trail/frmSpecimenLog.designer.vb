﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmSpecimenLog
    Inherits DevExpress.XtraEditors.XtraForm

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSpecimenLog))
        Dim Code128Generator1 As DevExpress.XtraPrinting.BarCode.Code128Generator = New DevExpress.XtraPrinting.BarCode.Code128Generator()
        Me.pnlBackground = New DevExpress.XtraEditors.PanelControl()
        Me.XTabPatient = New DevExpress.XtraTab.XtraTabControl()
        Me.XtraTabPage1 = New DevExpress.XtraTab.XtraTabPage()
        Me.Panel = New DevExpress.XtraEditors.PanelControl()
        Me.btnSearch = New DevExpress.XtraEditors.SimpleButton()
        Me.dtFrom1 = New System.Windows.Forms.DateTimePicker()
        Me.dtTo1 = New System.Windows.Forms.DateTimePicker()
        Me.LabelControl3 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl5 = New DevExpress.XtraEditors.LabelControl()
        Me.cboTestType = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.BarManager = New DevExpress.XtraBars.BarManager(Me.components)
        Me.Bar = New DevExpress.XtraBars.Bar()
        Me.btnRefresh = New DevExpress.XtraBars.BarLargeButtonItem()
        Me.btnClose = New DevExpress.XtraBars.BarLargeButtonItem()
        Me.BarAndDockingController = New DevExpress.XtraBars.BarAndDockingController(Me.components)
        Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl()
        Me.BarButtonItem1 = New DevExpress.XtraBars.BarButtonItem()
        Me.BarButtonItem2 = New DevExpress.XtraBars.BarButtonItem()
        Me.BarButtonItem3 = New DevExpress.XtraBars.BarButtonItem()
        Me.BarButtonItem4 = New DevExpress.XtraBars.BarButtonItem()
        Me.BarButtonItem5 = New DevExpress.XtraBars.BarButtonItem()
        Me.BarButtonItem6 = New DevExpress.XtraBars.BarButtonItem()
        Me.BarButtonItem7 = New DevExpress.XtraBars.BarButtonItem()
        Me.gList = New DevExpress.XtraEditors.GroupControl()
        Me.dtList = New DevExpress.XtraGrid.GridControl()
        Me.GridView = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.bcCode = New DevExpress.XtraEditors.BarCodeControl()
        Me.picCode = New System.Windows.Forms.PictureBox()
        Me.txtTest = New DevExpress.XtraEditors.TextEdit()
        Me.ImageList = New System.Windows.Forms.ImageList(Me.components)
        Me.PrintPreviewDialog = New System.Windows.Forms.PrintPreviewDialog()
        Me.PrintDocument = New System.Drawing.Printing.PrintDocument()
        Me.ToastNotificationsManager = New DevExpress.XtraBars.ToastNotifications.ToastNotificationsManager(Me.components)
        CType(Me.pnlBackground, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlBackground.SuspendLayout()
        CType(Me.XTabPatient, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.XTabPatient.SuspendLayout()
        Me.XtraTabPage1.SuspendLayout()
        CType(Me.Panel, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel.SuspendLayout()
        CType(Me.cboTestType.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BarManager, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BarAndDockingController, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gList.SuspendLayout()
        CType(Me.dtList, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTest.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ToastNotificationsManager, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pnlBackground
        '
        Me.pnlBackground.Appearance.BackColor = System.Drawing.Color.White
        Me.pnlBackground.Appearance.Options.UseBackColor = True
        Me.pnlBackground.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.pnlBackground.Controls.Add(Me.XTabPatient)
        Me.pnlBackground.Controls.Add(Me.barDockControlLeft)
        Me.pnlBackground.Controls.Add(Me.barDockControlRight)
        Me.pnlBackground.Controls.Add(Me.barDockControlBottom)
        Me.pnlBackground.Controls.Add(Me.barDockControlTop)
        Me.pnlBackground.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlBackground.Location = New System.Drawing.Point(0, 0)
        Me.pnlBackground.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.pnlBackground.Name = "pnlBackground"
        Me.pnlBackground.Size = New System.Drawing.Size(1386, 758)
        Me.pnlBackground.TabIndex = 6
        '
        'XTabPatient
        '
        Me.XTabPatient.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.XTabPatient.AppearancePage.Header.BackColor = System.Drawing.Color.FromArgb(CType(CType(16, Byte), Integer), CType(CType(110, Byte), Integer), CType(CType(190, Byte), Integer))
        Me.XTabPatient.AppearancePage.Header.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(16, Byte), Integer), CType(CType(110, Byte), Integer), CType(CType(190, Byte), Integer))
        Me.XTabPatient.AppearancePage.Header.BorderColor = System.Drawing.Color.FromArgb(CType(CType(16, Byte), Integer), CType(CType(110, Byte), Integer), CType(CType(190, Byte), Integer))
        Me.XTabPatient.AppearancePage.Header.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold)
        Me.XTabPatient.AppearancePage.Header.Options.UseBackColor = True
        Me.XTabPatient.AppearancePage.Header.Options.UseBorderColor = True
        Me.XTabPatient.AppearancePage.Header.Options.UseFont = True
        Me.XTabPatient.AppearancePage.HeaderActive.BackColor = System.Drawing.Color.FromArgb(CType(CType(16, Byte), Integer), CType(CType(110, Byte), Integer), CType(CType(190, Byte), Integer))
        Me.XTabPatient.AppearancePage.HeaderActive.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(16, Byte), Integer), CType(CType(110, Byte), Integer), CType(CType(190, Byte), Integer))
        Me.XTabPatient.AppearancePage.HeaderActive.BorderColor = System.Drawing.Color.FromArgb(CType(CType(16, Byte), Integer), CType(CType(110, Byte), Integer), CType(CType(190, Byte), Integer))
        Me.XTabPatient.AppearancePage.HeaderActive.Options.UseBackColor = True
        Me.XTabPatient.AppearancePage.HeaderActive.Options.UseBorderColor = True
        Me.XTabPatient.AppearancePage.HeaderHotTracked.BackColor = System.Drawing.Color.FromArgb(CType(CType(16, Byte), Integer), CType(CType(110, Byte), Integer), CType(CType(190, Byte), Integer))
        Me.XTabPatient.AppearancePage.HeaderHotTracked.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(16, Byte), Integer), CType(CType(110, Byte), Integer), CType(CType(190, Byte), Integer))
        Me.XTabPatient.AppearancePage.HeaderHotTracked.BorderColor = System.Drawing.Color.FromArgb(CType(CType(16, Byte), Integer), CType(CType(110, Byte), Integer), CType(CType(190, Byte), Integer))
        Me.XTabPatient.AppearancePage.HeaderHotTracked.Options.UseBackColor = True
        Me.XTabPatient.AppearancePage.HeaderHotTracked.Options.UseBorderColor = True
        Me.XTabPatient.HeaderAutoFill = DevExpress.Utils.DefaultBoolean.[True]
        Me.XTabPatient.Location = New System.Drawing.Point(11, 42)
        Me.XTabPatient.Name = "XTabPatient"
        Me.XTabPatient.SelectedTabPage = Me.XtraTabPage1
        Me.XTabPatient.Size = New System.Drawing.Size(1363, 705)
        Me.XTabPatient.TabIndex = 36
        Me.XTabPatient.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.XtraTabPage1})
        '
        'XtraTabPage1
        '
        Me.XtraTabPage1.Controls.Add(Me.Panel)
        Me.XtraTabPage1.Controls.Add(Me.gList)
        Me.XtraTabPage1.ImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.[True]
        Me.XtraTabPage1.ImageOptions.SvgImage = CType(resources.GetObject("XtraTabPage1.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.XtraTabPage1.ImageOptions.SvgImageSize = New System.Drawing.Size(16, 16)
        Me.XtraTabPage1.Name = "XtraTabPage1"
        Me.XtraTabPage1.Size = New System.Drawing.Size(1361, 673)
        Me.XtraTabPage1.Text = "Logs"
        '
        'Panel
        '
        Me.Panel.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel.Controls.Add(Me.btnSearch)
        Me.Panel.Controls.Add(Me.dtFrom1)
        Me.Panel.Controls.Add(Me.dtTo1)
        Me.Panel.Controls.Add(Me.LabelControl3)
        Me.Panel.Controls.Add(Me.LabelControl5)
        Me.Panel.Controls.Add(Me.cboTestType)
        Me.Panel.Location = New System.Drawing.Point(4, 0)
        Me.Panel.Name = "Panel"
        Me.Panel.Size = New System.Drawing.Size(1353, 33)
        Me.Panel.TabIndex = 144
        '
        'btnSearch
        '
        Me.btnSearch.ImageOptions.Image = CType(resources.GetObject("btnSearch.ImageOptions.Image"), System.Drawing.Image)
        Me.btnSearch.Location = New System.Drawing.Point(292, 4)
        Me.btnSearch.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(63, 24)
        Me.btnSearch.TabIndex = 146
        Me.btnSearch.Text = "&Apply"
        Me.btnSearch.Visible = False
        '
        'dtFrom1
        '
        Me.dtFrom1.CalendarForeColor = System.Drawing.Color.FromArgb(CType(CType(27, Byte), Integer), CType(CType(41, Byte), Integer), CType(CType(62, Byte), Integer))
        Me.dtFrom1.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtFrom1.Location = New System.Drawing.Point(87, 5)
        Me.dtFrom1.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.dtFrom1.Name = "dtFrom1"
        Me.dtFrom1.Size = New System.Drawing.Size(100, 22)
        Me.dtFrom1.TabIndex = 145
        '
        'dtTo1
        '
        Me.dtTo1.CalendarFont = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtTo1.CalendarForeColor = System.Drawing.Color.FromArgb(CType(CType(27, Byte), Integer), CType(CType(41, Byte), Integer), CType(CType(62, Byte), Integer))
        Me.dtTo1.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtTo1.Location = New System.Drawing.Point(191, 5)
        Me.dtTo1.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.dtTo1.Name = "dtTo1"
        Me.dtTo1.Size = New System.Drawing.Size(94, 22)
        Me.dtTo1.TabIndex = 144
        '
        'LabelControl3
        '
        Me.LabelControl3.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(27, Byte), Integer), CType(CType(41, Byte), Integer), CType(CType(62, Byte), Integer))
        Me.LabelControl3.Appearance.Options.UseForeColor = True
        Me.LabelControl3.Location = New System.Drawing.Point(8, 8)
        Me.LabelControl3.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.LabelControl3.Name = "LabelControl3"
        Me.LabelControl3.Size = New System.Drawing.Size(63, 13)
        Me.LabelControl3.TabIndex = 143
        Me.LabelControl3.Text = "Date Range:"
        '
        'LabelControl5
        '
        Me.LabelControl5.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(27, Byte), Integer), CType(CType(41, Byte), Integer), CType(CType(62, Byte), Integer))
        Me.LabelControl5.Appearance.Options.UseForeColor = True
        Me.LabelControl5.Location = New System.Drawing.Point(8, 65)
        Me.LabelControl5.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.LabelControl5.Name = "LabelControl5"
        Me.LabelControl5.Size = New System.Drawing.Size(41, 13)
        Me.LabelControl5.TabIndex = 7
        Me.LabelControl5.Text = "Section:"
        '
        'cboTestType
        '
        Me.cboTestType.Location = New System.Drawing.Point(73, 62)
        Me.cboTestType.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.cboTestType.MenuManager = Me.BarManager
        Me.cboTestType.Name = "cboTestType"
        Me.cboTestType.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.cboTestType.Size = New System.Drawing.Size(268, 20)
        Me.cboTestType.TabIndex = 8
        '
        'BarManager
        '
        Me.BarManager.AllowCustomization = False
        Me.BarManager.AllowMoveBarOnToolbar = False
        Me.BarManager.AllowQuickCustomization = False
        Me.BarManager.AllowShowToolbarsPopup = False
        Me.BarManager.Bars.AddRange(New DevExpress.XtraBars.Bar() {Me.Bar})
        Me.BarManager.Controller = Me.BarAndDockingController
        Me.BarManager.DockControls.Add(Me.barDockControlTop)
        Me.BarManager.DockControls.Add(Me.barDockControlBottom)
        Me.BarManager.DockControls.Add(Me.barDockControlLeft)
        Me.BarManager.DockControls.Add(Me.barDockControlRight)
        Me.BarManager.DockWindowTabFont = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BarManager.Form = Me.pnlBackground
        Me.BarManager.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.BarButtonItem1, Me.BarButtonItem2, Me.BarButtonItem3, Me.BarButtonItem4, Me.BarButtonItem5, Me.BarButtonItem6, Me.btnRefresh, Me.BarButtonItem7, Me.btnClose})
        Me.BarManager.MainMenu = Me.Bar
        Me.BarManager.MaxItemId = 27
        '
        'Bar
        '
        Me.Bar.BarName = "Main menu"
        Me.Bar.DockCol = 0
        Me.Bar.DockRow = 0
        Me.Bar.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
        Me.Bar.FloatLocation = New System.Drawing.Point(74, 177)
        Me.Bar.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.btnRefresh, True), New DevExpress.XtraBars.LinkPersistInfo(Me.btnClose)})
        Me.Bar.OptionsBar.AllowQuickCustomization = False
        Me.Bar.OptionsBar.MultiLine = True
        Me.Bar.OptionsBar.UseWholeRow = True
        Me.Bar.Text = "Main menu"
        '
        'btnRefresh
        '
        Me.btnRefresh.Caption = "Refresh"
        Me.btnRefresh.CaptionAlignment = DevExpress.XtraBars.BarItemCaptionAlignment.Right
        Me.btnRefresh.Id = 10
        Me.btnRefresh.ImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.[True]
        Me.btnRefresh.ImageOptions.SvgImage = CType(resources.GetObject("btnRefresh.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.btnRefresh.ImageOptions.SvgImageSize = New System.Drawing.Size(24, 24)
        Me.btnRefresh.Name = "btnRefresh"
        '
        'btnClose
        '
        Me.btnClose.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right
        Me.btnClose.Caption = "&Close"
        Me.btnClose.CaptionAlignment = DevExpress.XtraBars.BarItemCaptionAlignment.Right
        Me.btnClose.Id = 23
        Me.btnClose.ImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.[True]
        Me.btnClose.ImageOptions.SvgImage = CType(resources.GetObject("btnClose.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.btnClose.ImageOptions.SvgImageSize = New System.Drawing.Size(24, 24)
        Me.btnClose.Name = "btnClose"
        '
        'BarAndDockingController
        '
        Me.BarAndDockingController.AppearancesBar.MainMenuAppearance.Normal.ForeColor = System.Drawing.Color.White
        Me.BarAndDockingController.AppearancesBar.MainMenuAppearance.Normal.Options.UseForeColor = True
        Me.BarAndDockingController.PropertiesBar.AllowLinkLighting = False
        '
        'barDockControlTop
        '
        Me.barDockControlTop.CausesValidation = False
        Me.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.barDockControlTop.Location = New System.Drawing.Point(0, 0)
        Me.barDockControlTop.Manager = Me.BarManager
        Me.barDockControlTop.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.barDockControlTop.Size = New System.Drawing.Size(1386, 36)
        '
        'barDockControlBottom
        '
        Me.barDockControlBottom.CausesValidation = False
        Me.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.barDockControlBottom.Location = New System.Drawing.Point(0, 758)
        Me.barDockControlBottom.Manager = Me.BarManager
        Me.barDockControlBottom.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.barDockControlBottom.Size = New System.Drawing.Size(1386, 0)
        '
        'barDockControlLeft
        '
        Me.barDockControlLeft.CausesValidation = False
        Me.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.barDockControlLeft.Location = New System.Drawing.Point(0, 36)
        Me.barDockControlLeft.Manager = Me.BarManager
        Me.barDockControlLeft.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.barDockControlLeft.Size = New System.Drawing.Size(0, 722)
        '
        'barDockControlRight
        '
        Me.barDockControlRight.CausesValidation = False
        Me.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right
        Me.barDockControlRight.Location = New System.Drawing.Point(1386, 36)
        Me.barDockControlRight.Manager = Me.BarManager
        Me.barDockControlRight.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.barDockControlRight.Size = New System.Drawing.Size(0, 722)
        '
        'BarButtonItem1
        '
        Me.BarButtonItem1.Caption = "View"
        Me.BarButtonItem1.Id = 0
        Me.BarButtonItem1.ImageOptions.Image = CType(resources.GetObject("BarButtonItem1.ImageOptions.Image"), System.Drawing.Image)
        Me.BarButtonItem1.ImageOptions.LargeImage = CType(resources.GetObject("BarButtonItem1.ImageOptions.LargeImage"), System.Drawing.Image)
        Me.BarButtonItem1.Name = "BarButtonItem1"
        '
        'BarButtonItem2
        '
        Me.BarButtonItem2.Caption = "Test Order"
        Me.BarButtonItem2.Id = 1
        Me.BarButtonItem2.ImageOptions.Image = CType(resources.GetObject("BarButtonItem2.ImageOptions.Image"), System.Drawing.Image)
        Me.BarButtonItem2.ImageOptions.LargeImage = CType(resources.GetObject("BarButtonItem2.ImageOptions.LargeImage"), System.Drawing.Image)
        Me.BarButtonItem2.Name = "BarButtonItem2"
        '
        'BarButtonItem3
        '
        Me.BarButtonItem3.Caption = "Patient Information"
        Me.BarButtonItem3.Id = 2
        Me.BarButtonItem3.Name = "BarButtonItem3"
        '
        'BarButtonItem4
        '
        Me.BarButtonItem4.Caption = "Delete"
        Me.BarButtonItem4.Id = 3
        Me.BarButtonItem4.Name = "BarButtonItem4"
        '
        'BarButtonItem5
        '
        Me.BarButtonItem5.Caption = "Refresh"
        Me.BarButtonItem5.Id = 4
        Me.BarButtonItem5.Name = "BarButtonItem5"
        '
        'BarButtonItem6
        '
        Me.BarButtonItem6.Caption = "Close"
        Me.BarButtonItem6.Id = 5
        Me.BarButtonItem6.ImageOptions.Image = CType(resources.GetObject("BarButtonItem6.ImageOptions.Image"), System.Drawing.Image)
        Me.BarButtonItem6.ImageOptions.LargeImage = CType(resources.GetObject("BarButtonItem6.ImageOptions.LargeImage"), System.Drawing.Image)
        Me.BarButtonItem6.Name = "BarButtonItem6"
        '
        'BarButtonItem7
        '
        Me.BarButtonItem7.Caption = "BarButtonItem7"
        Me.BarButtonItem7.Id = 21
        Me.BarButtonItem7.Name = "BarButtonItem7"
        '
        'gList
        '
        Me.gList.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.gList.AppearanceCaption.BackColor = System.Drawing.Color.FromArgb(CType(CType(16, Byte), Integer), CType(CType(110, Byte), Integer), CType(CType(190, Byte), Integer))
        Me.gList.AppearanceCaption.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(16, Byte), Integer), CType(CType(110, Byte), Integer), CType(CType(190, Byte), Integer))
        Me.gList.AppearanceCaption.BorderColor = System.Drawing.Color.FromArgb(CType(CType(16, Byte), Integer), CType(CType(110, Byte), Integer), CType(CType(190, Byte), Integer))
        Me.gList.AppearanceCaption.ForeColor = System.Drawing.Color.White
        Me.gList.AppearanceCaption.Options.UseBackColor = True
        Me.gList.AppearanceCaption.Options.UseBorderColor = True
        Me.gList.AppearanceCaption.Options.UseForeColor = True
        Me.gList.CaptionImageOptions.AllowGlyphSkinning = True
        Me.gList.CaptionImageOptions.SvgImage = CType(resources.GetObject("gList.CaptionImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.gList.CaptionImageOptions.SvgImageSize = New System.Drawing.Size(16, 16)
        Me.gList.Controls.Add(Me.dtList)
        Me.gList.Controls.Add(Me.bcCode)
        Me.gList.Controls.Add(Me.picCode)
        Me.gList.Controls.Add(Me.txtTest)
        Me.gList.Location = New System.Drawing.Point(4, 39)
        Me.gList.Name = "gList"
        Me.gList.Size = New System.Drawing.Size(1353, 631)
        Me.gList.TabIndex = 145
        Me.gList.Text = "List"
        '
        'dtList
        '
        Me.dtList.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dtList.Location = New System.Drawing.Point(2, 27)
        Me.dtList.MainView = Me.GridView
        Me.dtList.MenuManager = Me.BarManager
        Me.dtList.Name = "dtList"
        Me.dtList.Size = New System.Drawing.Size(1349, 602)
        Me.dtList.TabIndex = 144
        Me.dtList.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridView})
        '
        'GridView
        '
        Me.GridView.GridControl = Me.dtList
        Me.GridView.Name = "GridView"
        Me.GridView.OptionsBehavior.AutoExpandAllGroups = True
        Me.GridView.OptionsCustomization.AllowColumnResizing = False
        Me.GridView.OptionsCustomization.AllowGroup = False
        Me.GridView.OptionsCustomization.AllowQuickHideColumns = False
        Me.GridView.OptionsSelection.CheckBoxSelectorColumnWidth = 30
        Me.GridView.OptionsView.ShowGroupPanel = False
        Me.GridView.OptionsView.ShowVerticalLines = DevExpress.Utils.DefaultBoolean.[False]
        '
        'bcCode
        '
        Me.bcCode.AutoModule = True
        Me.bcCode.BackColor = System.Drawing.Color.White
        Me.bcCode.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.bcCode.ForeColor = System.Drawing.Color.Black
        Me.bcCode.HorizontalAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.bcCode.HorizontalTextAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.bcCode.Location = New System.Drawing.Point(902, 178)
        Me.bcCode.Margin = New System.Windows.Forms.Padding(2)
        Me.bcCode.Name = "bcCode"
        Me.bcCode.Padding = New System.Windows.Forms.Padding(8, 2, 8, 0)
        Me.bcCode.ShowText = False
        Me.bcCode.Size = New System.Drawing.Size(343, 92)
        Me.bcCode.Symbology = Code128Generator1
        Me.bcCode.TabIndex = 141
        '
        'picCode
        '
        Me.picCode.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.picCode.BackColor = System.Drawing.Color.White
        Me.picCode.Location = New System.Drawing.Point(997, 87)
        Me.picCode.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.picCode.Name = "picCode"
        Me.picCode.Size = New System.Drawing.Size(340, 72)
        Me.picCode.TabIndex = 140
        Me.picCode.TabStop = False
        Me.picCode.Visible = False
        '
        'txtTest
        '
        Me.txtTest.Enabled = False
        Me.txtTest.Location = New System.Drawing.Point(938, 320)
        Me.txtTest.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.txtTest.Name = "txtTest"
        Me.txtTest.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI", 9.75!)
        Me.txtTest.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(27, Byte), Integer), CType(CType(41, Byte), Integer), CType(CType(62, Byte), Integer))
        Me.txtTest.Properties.Appearance.Options.UseFont = True
        Me.txtTest.Properties.Appearance.Options.UseForeColor = True
        Me.txtTest.Properties.LookAndFeel.SkinName = "Visual Studio 2013 Blue"
        Me.txtTest.Properties.LookAndFeel.UseDefaultLookAndFeel = False
        Me.txtTest.Size = New System.Drawing.Size(250, 24)
        Me.txtTest.TabIndex = 143
        '
        'ImageList
        '
        Me.ImageList.ImageStream = CType(resources.GetObject("ImageList.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageList.Images.SetKeyName(0, "cart.ico")
        '
        'PrintPreviewDialog
        '
        Me.PrintPreviewDialog.AutoScrollMargin = New System.Drawing.Size(0, 0)
        Me.PrintPreviewDialog.AutoScrollMinSize = New System.Drawing.Size(0, 0)
        Me.PrintPreviewDialog.ClientSize = New System.Drawing.Size(400, 300)
        Me.PrintPreviewDialog.Enabled = True
        Me.PrintPreviewDialog.Icon = CType(resources.GetObject("PrintPreviewDialog.Icon"), System.Drawing.Icon)
        Me.PrintPreviewDialog.Name = "PrintPreviewDialog"
        Me.PrintPreviewDialog.Visible = False
        '
        'ToastNotificationsManager
        '
        Me.ToastNotificationsManager.ApplicationId = "328bac00-b99b-4119-b021-c7720c4cd88e"
        Me.ToastNotificationsManager.ApplicationName = "SBSI LIS"
        '
        'frmSpecimenLog
        '
        Me.AcceptButton = Me.btnSearch
        Me.Appearance.BackColor = System.Drawing.SystemColors.Control
        Me.Appearance.Options.UseBackColor = True
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.ClientSize = New System.Drawing.Size(1386, 758)
        Me.ControlBox = False
        Me.Controls.Add(Me.pnlBackground)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Name = "frmSpecimenLog"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Specimen Logs"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.pnlBackground, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlBackground.ResumeLayout(False)
        Me.pnlBackground.PerformLayout()
        CType(Me.XTabPatient, System.ComponentModel.ISupportInitialize).EndInit()
        Me.XTabPatient.ResumeLayout(False)
        Me.XtraTabPage1.ResumeLayout(False)
        CType(Me.Panel, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel.ResumeLayout(False)
        Me.Panel.PerformLayout()
        CType(Me.cboTestType.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BarManager, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BarAndDockingController, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gList.ResumeLayout(False)
        CType(Me.dtList, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTest.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ToastNotificationsManager, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents pnlBackground As DevExpress.XtraEditors.PanelControl
    Friend WithEvents BarManager As DevExpress.XtraBars.BarManager
    Friend WithEvents Bar As DevExpress.XtraBars.Bar
    Friend WithEvents BarButtonItem1 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents BarButtonItem2 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents BarButtonItem3 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents BarButtonItem4 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents BarButtonItem5 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents BarButtonItem6 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
    Friend WithEvents btnRefresh As DevExpress.XtraBars.BarLargeButtonItem
    Friend WithEvents BarAndDockingController As DevExpress.XtraBars.BarAndDockingController
    Friend WithEvents ImageList As System.Windows.Forms.ImageList
    Friend WithEvents cboTestType As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents LabelControl5 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents BarButtonItem7 As DevExpress.XtraBars.BarButtonItem
    Friend WithEvents XTabPatient As DevExpress.XtraTab.XtraTabControl
    Friend WithEvents XtraTabPage1 As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents btnClose As DevExpress.XtraBars.BarLargeButtonItem
    Friend WithEvents bcCode As DevExpress.XtraEditors.BarCodeControl
    Friend WithEvents picCode As System.Windows.Forms.PictureBox
    Friend WithEvents PrintPreviewDialog As System.Windows.Forms.PrintPreviewDialog
    Friend WithEvents PrintDocument As System.Drawing.Printing.PrintDocument
    Friend WithEvents txtTest As DevExpress.XtraEditors.TextEdit
    Friend WithEvents gList As DevExpress.XtraEditors.GroupControl
    Friend WithEvents Panel As DevExpress.XtraEditors.PanelControl
    Friend WithEvents btnSearch As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents dtFrom1 As DateTimePicker
    Friend WithEvents dtTo1 As DateTimePicker
    Friend WithEvents LabelControl3 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents dtList As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridView As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents ToastNotificationsManager As DevExpress.XtraBars.ToastNotifications.ToastNotificationsManager
End Class
