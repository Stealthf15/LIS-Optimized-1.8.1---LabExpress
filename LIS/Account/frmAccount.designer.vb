﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAccount
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmAccount))
        Me.ImageList = New System.Windows.Forms.ImageList(Me.components)
        Me.BarManager = New DevExpress.XtraBars.BarManager(Me.components)
        Me.Bar2 = New DevExpress.XtraBars.Bar()
        Me.btnAdd = New DevExpress.XtraBars.BarLargeButtonItem()
        Me.BarLargeButtonItem1 = New DevExpress.XtraBars.BarLargeButtonItem()
        Me.btnDelete = New DevExpress.XtraBars.BarLargeButtonItem()
        Me.btnRefresh = New DevExpress.XtraBars.BarLargeButtonItem()
        Me.btnClose = New DevExpress.XtraBars.BarLargeButtonItem()
        Me.BarAndDockingController = New DevExpress.XtraBars.BarAndDockingController(Me.components)
        Me.barDockControlTop = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlBottom = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlLeft = New DevExpress.XtraBars.BarDockControl()
        Me.barDockControlRight = New DevExpress.XtraBars.BarDockControl()
        Me.btnEdit = New DevExpress.XtraBars.BarLargeButtonItem()
        Me.GroupControl2 = New DevExpress.XtraEditors.GroupControl()
        Me.dtList = New DevExpress.XtraGrid.GridControl()
        Me.GridView = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.btnEnable = New DevExpress.XtraBars.BarLargeButtonItem()
        CType(Me.BarManager, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BarAndDockingController, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl2.SuspendLayout()
        CType(Me.dtList, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ImageList
        '
        Me.ImageList.ImageStream = CType(resources.GetObject("ImageList.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageList.Images.SetKeyName(0, "Folder.ico")
        '
        'BarManager
        '
        Me.BarManager.AllowCustomization = False
        Me.BarManager.AllowQuickCustomization = False
        Me.BarManager.Bars.AddRange(New DevExpress.XtraBars.Bar() {Me.Bar2})
        Me.BarManager.Controller = Me.BarAndDockingController
        Me.BarManager.DockControls.Add(Me.barDockControlTop)
        Me.BarManager.DockControls.Add(Me.barDockControlBottom)
        Me.BarManager.DockControls.Add(Me.barDockControlLeft)
        Me.BarManager.DockControls.Add(Me.barDockControlRight)
        Me.BarManager.Form = Me
        Me.BarManager.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.btnAdd, Me.btnDelete, Me.btnRefresh, Me.btnClose, Me.btnEdit, Me.BarLargeButtonItem1, Me.btnEnable})
        Me.BarManager.MainMenu = Me.Bar2
        Me.BarManager.MaxItemId = 7
        '
        'Bar2
        '
        Me.Bar2.BarName = "Main menu"
        Me.Bar2.DockCol = 0
        Me.Bar2.DockRow = 0
        Me.Bar2.DockStyle = DevExpress.XtraBars.BarDockStyle.Top
        Me.Bar2.LinksPersistInfo.AddRange(New DevExpress.XtraBars.LinkPersistInfo() {New DevExpress.XtraBars.LinkPersistInfo(Me.btnAdd), New DevExpress.XtraBars.LinkPersistInfo(Me.BarLargeButtonItem1), New DevExpress.XtraBars.LinkPersistInfo(Me.btnEnable), New DevExpress.XtraBars.LinkPersistInfo(Me.btnDelete), New DevExpress.XtraBars.LinkPersistInfo(Me.btnRefresh), New DevExpress.XtraBars.LinkPersistInfo(Me.btnClose)})
        Me.Bar2.OptionsBar.MultiLine = True
        Me.Bar2.OptionsBar.UseWholeRow = True
        Me.Bar2.Text = "Main menu"
        '
        'btnAdd
        '
        Me.btnAdd.Caption = "Add"
        Me.btnAdd.CaptionAlignment = DevExpress.XtraBars.BarItemCaptionAlignment.Right
        Me.btnAdd.Id = 0
        Me.btnAdd.ImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.[True]
        Me.btnAdd.ImageOptions.SvgImage = CType(resources.GetObject("btnAdd.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.btnAdd.ImageOptions.SvgImageSize = New System.Drawing.Size(24, 24)
        Me.btnAdd.Name = "btnAdd"
        '
        'BarLargeButtonItem1
        '
        Me.BarLargeButtonItem1.Caption = "Edit"
        Me.BarLargeButtonItem1.CaptionAlignment = DevExpress.XtraBars.BarItemCaptionAlignment.Right
        Me.BarLargeButtonItem1.Id = 5
        Me.BarLargeButtonItem1.ImageOptions.SvgImage = CType(resources.GetObject("BarLargeButtonItem1.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.BarLargeButtonItem1.ImageOptions.SvgImageSize = New System.Drawing.Size(24, 24)
        Me.BarLargeButtonItem1.Name = "BarLargeButtonItem1"
        '
        'btnDelete
        '
        Me.btnDelete.Caption = "Disable"
        Me.btnDelete.CaptionAlignment = DevExpress.XtraBars.BarItemCaptionAlignment.Right
        Me.btnDelete.Id = 1
        Me.btnDelete.ImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.[True]
        Me.btnDelete.ImageOptions.SvgImage = CType(resources.GetObject("btnDelete.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.btnDelete.ImageOptions.SvgImageSize = New System.Drawing.Size(24, 24)
        Me.btnDelete.Name = "btnDelete"
        '
        'btnRefresh
        '
        Me.btnRefresh.Caption = "Refresh"
        Me.btnRefresh.CaptionAlignment = DevExpress.XtraBars.BarItemCaptionAlignment.Right
        Me.btnRefresh.Id = 2
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
        Me.btnClose.Id = 3
        Me.btnClose.ImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.[True]
        Me.btnClose.ImageOptions.SvgImage = CType(resources.GetObject("btnClose.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.btnClose.ImageOptions.SvgImageSize = New System.Drawing.Size(24, 24)
        Me.btnClose.Name = "btnClose"
        '
        'BarAndDockingController
        '
        Me.BarAndDockingController.AppearancesBar.MainMenuAppearance.Normal.ForeColor = System.Drawing.Color.White
        Me.BarAndDockingController.AppearancesBar.MainMenuAppearance.Normal.Options.UseForeColor = True
        Me.BarAndDockingController.LookAndFeel.SkinName = "VS2010"
        Me.BarAndDockingController.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Office2003
        Me.BarAndDockingController.PropertiesBar.AllowLinkLighting = False
        '
        'barDockControlTop
        '
        Me.barDockControlTop.CausesValidation = False
        Me.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.barDockControlTop.Location = New System.Drawing.Point(0, 0)
        Me.barDockControlTop.Manager = Me.BarManager
        Me.barDockControlTop.Size = New System.Drawing.Size(888, 36)
        '
        'barDockControlBottom
        '
        Me.barDockControlBottom.CausesValidation = False
        Me.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.barDockControlBottom.Location = New System.Drawing.Point(0, 494)
        Me.barDockControlBottom.Manager = Me.BarManager
        Me.barDockControlBottom.Size = New System.Drawing.Size(888, 0)
        '
        'barDockControlLeft
        '
        Me.barDockControlLeft.CausesValidation = False
        Me.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.barDockControlLeft.Location = New System.Drawing.Point(0, 36)
        Me.barDockControlLeft.Manager = Me.BarManager
        Me.barDockControlLeft.Size = New System.Drawing.Size(0, 458)
        '
        'barDockControlRight
        '
        Me.barDockControlRight.CausesValidation = False
        Me.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right
        Me.barDockControlRight.Location = New System.Drawing.Point(888, 36)
        Me.barDockControlRight.Manager = Me.BarManager
        Me.barDockControlRight.Size = New System.Drawing.Size(0, 458)
        '
        'btnEdit
        '
        Me.btnEdit.Caption = "Edit"
        Me.btnEdit.CaptionAlignment = DevExpress.XtraBars.BarItemCaptionAlignment.Right
        Me.btnEdit.Id = 4
        Me.btnEdit.ImageOptions.SvgImage = CType(resources.GetObject("btnEdit.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.btnEdit.Name = "btnEdit"
        '
        'GroupControl2
        '
        Me.GroupControl2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupControl2.AppearanceCaption.BackColor = System.Drawing.Color.FromArgb(CType(CType(16, Byte), Integer), CType(CType(110, Byte), Integer), CType(CType(190, Byte), Integer))
        Me.GroupControl2.AppearanceCaption.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(16, Byte), Integer), CType(CType(110, Byte), Integer), CType(CType(190, Byte), Integer))
        Me.GroupControl2.AppearanceCaption.BorderColor = System.Drawing.Color.FromArgb(CType(CType(16, Byte), Integer), CType(CType(110, Byte), Integer), CType(CType(190, Byte), Integer))
        Me.GroupControl2.AppearanceCaption.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupControl2.AppearanceCaption.Options.UseBackColor = True
        Me.GroupControl2.AppearanceCaption.Options.UseBorderColor = True
        Me.GroupControl2.AppearanceCaption.Options.UseFont = True
        Me.GroupControl2.Controls.Add(Me.dtList)
        Me.GroupControl2.Location = New System.Drawing.Point(12, 42)
        Me.GroupControl2.Name = "GroupControl2"
        Me.GroupControl2.Size = New System.Drawing.Size(864, 440)
        Me.GroupControl2.TabIndex = 181
        Me.GroupControl2.Text = "List"
        '
        'dtList
        '
        Me.dtList.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dtList.Location = New System.Drawing.Point(2, 27)
        Me.dtList.MainView = Me.GridView
        Me.dtList.MenuManager = Me.BarManager
        Me.dtList.Name = "dtList"
        Me.dtList.Size = New System.Drawing.Size(860, 411)
        Me.dtList.TabIndex = 146
        Me.dtList.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridView})
        '
        'GridView
        '
        Me.GridView.Appearance.FocusedRow.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold)
        Me.GridView.Appearance.FocusedRow.Options.UseFont = True
        Me.GridView.GridControl = Me.dtList
        Me.GridView.Name = "GridView"
        Me.GridView.OptionsBehavior.AutoExpandAllGroups = True
        Me.GridView.OptionsCustomization.AllowGroup = False
        Me.GridView.OptionsCustomization.AllowQuickHideColumns = False
        Me.GridView.OptionsSelection.CheckBoxSelectorColumnWidth = 30
        Me.GridView.OptionsView.ShowGroupPanel = False
        Me.GridView.OptionsView.ShowVerticalLines = DevExpress.Utils.DefaultBoolean.[False]
        '
        'btnEnable
        '
        Me.btnEnable.Caption = "Enable"
        Me.btnEnable.CaptionAlignment = DevExpress.XtraBars.BarItemCaptionAlignment.Right
        Me.btnEnable.Id = 6
        Me.btnEnable.ImageOptions.SvgImage = CType(resources.GetObject("btnEnable.ImageOptions.SvgImage"), DevExpress.Utils.Svg.SvgImage)
        Me.btnEnable.ImageOptions.SvgImageSize = New System.Drawing.Size(24, 24)
        Me.btnEnable.Name = "btnEnable"
        '
        'frmAccount
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(888, 494)
        Me.Controls.Add(Me.GroupControl2)
        Me.Controls.Add(Me.barDockControlLeft)
        Me.Controls.Add(Me.barDockControlRight)
        Me.Controls.Add(Me.barDockControlBottom)
        Me.Controls.Add(Me.barDockControlTop)
        Me.FormBorderEffect = DevExpress.XtraEditors.FormBorderEffect.Glow
        Me.Name = "frmAccount"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Account Management"
        CType(Me.BarManager, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BarAndDockingController, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl2.ResumeLayout(False)
        CType(Me.dtList, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents BarManager As DevExpress.XtraBars.BarManager
    Friend WithEvents Bar2 As DevExpress.XtraBars.Bar
    Friend WithEvents BarAndDockingController As DevExpress.XtraBars.BarAndDockingController
    Friend WithEvents barDockControlTop As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlBottom As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlLeft As DevExpress.XtraBars.BarDockControl
    Friend WithEvents barDockControlRight As DevExpress.XtraBars.BarDockControl
    Friend WithEvents btnAdd As DevExpress.XtraBars.BarLargeButtonItem
    Friend WithEvents btnDelete As DevExpress.XtraBars.BarLargeButtonItem
    Friend WithEvents btnRefresh As DevExpress.XtraBars.BarLargeButtonItem
    Friend WithEvents btnClose As DevExpress.XtraBars.BarLargeButtonItem
    Friend WithEvents ImageList As System.Windows.Forms.ImageList
    Friend WithEvents BarLargeButtonItem1 As DevExpress.XtraBars.BarLargeButtonItem
    Friend WithEvents btnEdit As DevExpress.XtraBars.BarLargeButtonItem
    Friend WithEvents GroupControl2 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents dtList As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridView As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents btnEnable As DevExpress.XtraBars.BarLargeButtonItem
End Class
