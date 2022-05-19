Imports System.IO
Imports System.IO.Ports
Imports System.Threading
Imports System.Xml
Imports System.Text.RegularExpressions

Imports System.Data.SqlClient
Imports System.Globalization
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraEditors.Repository
Imports DevExpress.XtraEditors

Public Class frmHemaNew

    Public MedTechID As String = ""
    Public PathologistID As String = ""
    Public VerifyID As String = ""

    Public mainID As String
    Public MainSampleID As String = ""
    Public FinalMainID As String = ""

    Public PatientID As String = ""
    Public RDate As Date

    Public Channel As String = ""
    Public FLAG As String = ""
    Public RANGE As String = ""

    Public Section As String = ""
    Public SubSection As String = ""

    Public DefaultUnit As Integer = 0
    Public DefaultDiffCount As Integer = 0
    Public Validation As String = ""

    'used in verified to disable edit of result
    Public status As String = ""
    Public resultStatus As String = ""

    Dim Diff As Double

    Private Sub txtResult_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If InStr("1234567890.", e.KeyChar) = 0 And Not Chr(AscW(e.KeyChar)) = vbBack Then
            e.KeyChar = ChrW(0)
        End If
    End Sub

    Private Sub frmResultNewChem_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        frmHemaWorklist.LoadRecords()
        frmHemaWorklist.LoadRecordsCompleted()
        Me.Dispose()
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.ItemClick
        Me.Close()
    End Sub

    Private Sub txtAge_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtAge.KeyPress
        If InStr("1234567890", e.KeyChar) = 0 And Not Chr(AscW(e.KeyChar)) = vbBack Then
            e.KeyChar = ChrW(0)
        End If
    End Sub

    Private Sub cboMedTech_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboVerify.SelectedIndexChanged, cboMedTech.SelectedIndexChanged, cboPathologist.SelectedIndexChanged

        Connect()
        rs.Connection = conn
        rs.CommandType = CommandType.Text
        rs.CommandText = "SELECT * FROM `viewmedtEch` WHERE `name` = '" & Me.cboMedTech.Text & "'"
        reader = rs.ExecuteReader
        reader.Read()
        If reader.HasRows Then
            MedTechID = reader(0).ToString
        End If
        Disconnect()

        Connect()
        rs.Connection = conn
        rs.CommandType = CommandType.Text
        rs.CommandText = "SELECT * FROM `viewpathologist` WHERE `name` = '" & Me.cboPathologist.Text & "'"
        reader = rs.ExecuteReader
        reader.Read()
        If reader.HasRows Then
            PathologistID = reader(0).ToString
        End If
        Disconnect()
    End Sub

    Private Sub btnPreview_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPreview.Click
        frmHemaPrevious.patientID = txtPatientID.Text
        frmHemaPrevious.section = Section
        frmHemaPrevious.SubSection = SubSection

        'frmHemaPrevious.sample_id = txtSampleID.Text
        'frmHemaPrevious.Classification = txtClass.Text
        'frmHemaPrevious.Gender = cboSex.Text
        'frmHemaPrevious.Age = txtAge.Text

        frmHemaPrevious.ShowDialog()
    End Sub

    Public Sub LoadTest()
        'On Error Resume Next
        Try
            GridView.Columns.Clear()
            GridView.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
            GridView.Appearance.HeaderPanel.FontStyleDelta = FontStyle.Bold

            rs.Parameters.Clear()
            rs.Parameters.AddWithValue("@patientID", txtPatientID.Text)
            Dim adapter As New MySql.Data.MySqlClient.MySqlDataAdapter(rs)

            Connect()
            rs.Connection = conn
            rs.CommandType = CommandType.Text
            rs.CommandText = "Select * From `result` Where `Patient_id` = @patientID"
            reader = rs.ExecuteReader
            reader.Read()
            If reader.HasRows Then
                Disconnect()

                Connect()
                rs.Connection = conn
                rs.CommandType = CommandType.Text
                rs.CommandText = "Select tmpresult.`universal_id` AS TestName, 
                                          tmpresult.flag AS Flag,
                                          T1.Result As PreviousResult,
                                          tmpresult.`measurement` AS Result, 
                                          reference_range.`si_range` as ReferenceRange, 
                                          tmpresult.`units` as Unit,
                                          tmpresult.`value_conv` AS Conventional, 
                                          tmpresult.`unit_conv` AS Units, 
                                          reference_range.`conv_range` AS RefRange,  
                                          tmpresult.`instrument` AS Instrument, 
                                          tmpresult.`test_code` AS TestCode, 
                                          tmpresult.`id` AS ID, 
                                          tmpresult.`test_group` AS TestGroup, 
                                          tmpresult.`his_code` AS HISTestCode, 
                                          tmpresult.`his_mainid` AS HISMainID, 
                                          tmpresult.`print_status` AS PrintStatus,
                                          reference_range.`low_value` AS LowValue,
                                          reference_range.`high_value` AS HighValue,
                                          Specimen.`convertion_factor` AS ConversionFactor,
                                          Specimen.`convertion_multiplier` AS ConversionMultiplier,
                                          Specimen.`order_no` AS DisplayNo, 
                                            `tmpresult`.`accepted_result` AS AcceptedResult 
                                 From tmpresult
                                 Left Join(SELECT measurement AS Result, test_code As TestCode, patient_id As PatientID, result.`date` AS DateReleased FROM result WHERE date IN (SELECT MAX(date) FROM result WHERE result.patient_id = '" & txtPatientID.Text & "' And result.section = '" & Section & "' And result.sub_section ='" & SubSection & "' AND `result`.`accepted_result` = 1 GROUP BY test_code) ORDER BY date DESC) AS T1 ON tmpresult.patient_id = T1.PatientID And tmpresult.test_code = T1.TestCode 
                                 Left Join reference_range ON tmpresult.test_code = reference_range.test_code And reference_range.machine = tmpresult.instrument
                                 Left Join specimen ON tmpresult.test_code = specimen.test_code And tmpresult.instrument = specimen.instrument
                                 
                                 WHERE tmpresult.`sample_id` = '" & txtSampleID.Text & "' AND tmpresult.section = '" & Section & "' And tmpresult.sub_section = '" & SubSection & "' AND `tmpresult`.`accepted_result` = 1 And (`reference_range`.`classification` = '" & txtClass.Text & "' And `reference_range`.`gender` = '" & cboSex.Text & "' And ('" & txtAge.Text & "' BETWEEN `reference_range`.`age_begin` And `reference_range`.`age_end`)) 
                                 GROUP BY T1.TestCode, tmpresult.test_code ORDER BY specimen.order_no ASC"

                reader = rs.ExecuteReader
                reader.Read()
                If reader.HasRows Then
                    Disconnect()
                    'MessageBox.Show(1)
                    Dim SQL As String = "Select tmpresult.`universal_id` AS TestName, 
                                          tmpresult.flag AS Flag,
                                          T1.Result As PreviousResult,
                                          tmpresult.`measurement` AS Result, 
                                          reference_range.`si_range` as ReferenceRange, 
                                          tmpresult.`units` as Unit,
                                          tmpresult.`value_conv` AS Conventional, 
                                          tmpresult.`unit_conv` AS Units, 
                                          reference_range.`conv_range` AS RefRange,  
                                          tmpresult.`instrument` AS Instrument, 
                                          tmpresult.`test_code` AS TestCode, 
                                          tmpresult.`id` AS ID, 
                                          tmpresult.`test_group` AS TestGroup, 
                                          tmpresult.`his_code` AS HISTestCode, 
                                          tmpresult.`his_mainid` AS HISMainID, 
                                          tmpresult.`print_status` AS PrintStatus,
                                          reference_range.`low_value` AS LowValue,
                                          reference_range.`high_value` AS HighValue,
                                          Specimen.`convertion_factor` AS ConversionFactor,
                                          Specimen.`convertion_multiplier` AS ConversionMultiplier,
                                          Specimen.`order_no` AS DisplayNo, 
                                            `tmpresult`.`accepted_result` AS AcceptedResult 
                                 From tmpresult
                                 Left Join(SELECT measurement AS Result, test_code As TestCode, patient_id As PatientID, result.`date` AS DateReleased FROM result WHERE date IN (SELECT MAX(date) FROM result WHERE result.patient_id = '" & txtPatientID.Text & "' And result.section = '" & Section & "' And result.sub_section ='" & SubSection & "' AND `result`.`accepted_result` = 1 GROUP BY test_code) ORDER BY date DESC) AS T1 ON tmpresult.patient_id = T1.PatientID And tmpresult.test_code = T1.TestCode 
                                 Left Join reference_range ON tmpresult.test_code = reference_range.test_code And reference_range.machine = tmpresult.instrument
                                 Left Join specimen ON tmpresult.test_code = specimen.test_code And tmpresult.instrument = specimen.instrument
                                 
                                 WHERE tmpresult.`sample_id` = '" & txtSampleID.Text & "' AND tmpresult.section = '" & Section & "' And tmpresult.sub_section = '" & SubSection & "' AND `tmpresult`.`accepted_result` = 1 And (`reference_range`.`classification` = '" & txtClass.Text & "' And `reference_range`.`gender` = '" & cboSex.Text & "' And ('" & txtAge.Text & "' BETWEEN `reference_range`.`age_begin` And `reference_range`.`age_end`)) 
                                 GROUP BY T1.TestCode, tmpresult.test_code ORDER BY specimen.order_no ASC"

                    Dim command As New MySql.Data.MySqlClient.MySqlCommand(SQL, conn)

                    'command.Parameters.Clear()
                    'command.Parameters.AddWithValue("@MainID", txtSampleID.Text)
                    'command.Parameters.AddWithValue("@Section", Section)
                    'command.Parameters.AddWithValue("@SubSection", SubSection)

                    'command.Parameters.AddWithValue("@patientID", txtPatientID.Text)
                    'command.Parameters.AddWithValue("@Classification", txtClass.Text)
                    'command.Parameters.AddWithValue("@Gender", cboSex.Text)
                    'command.Parameters.AddWithValue("@Age", txtAge.Text)

                    Dim adapter1 As New MySql.Data.MySqlClient.MySqlDataAdapter(command)

                    Dim myTable As DataTable = New DataTable
                    adapter1.Fill(myTable)

                    dtResult.DataSource = myTable

                    'End If
                    Disconnect()
                Else

                    Disconnect()
                    'MessageBox.Show(2)
                    Dim SQL As String = "Select tmpresult.`universal_id` AS TestName, 
                                          tmpresult.flag AS Flag,
                                          T1.Result As PreviousResult,
                                          tmpresult.`measurement` AS Result, 
                                          reference_range.`si_range` as ReferenceRange, 
                                          tmpresult.`units` as Unit,
                                          tmpresult.`value_conv` AS Conventional, 
                                          tmpresult.`unit_conv` AS Units, 
                                          reference_range.`conv_range` AS RefRange,  
                                          tmpresult.`instrument` AS Instrument, 
                                          tmpresult.`test_code` AS TestCode, 
                                          tmpresult.`id` AS ID, 
                                          tmpresult.`test_group` AS TestGroup, 
                                          tmpresult.`his_code` AS HISTestCode, 
                                          tmpresult.`his_mainid` AS HISMainID, 
                                          tmpresult.`print_status` AS PrintStatus,
                                          reference_range.`low_value` AS LowValue,
                                          reference_range.`high_value` AS HighValue,
                                          Specimen.`convertion_factor` AS ConversionFactor,
                                          Specimen.`convertion_multiplier` AS ConversionMultiplier,
                                          Specimen.`order_no` AS DisplayNo, 
                                         `tmpresult`.`accepted_result` AS AcceptedResult 
                                 From tmpresult
                                 Left Join(SELECT measurement AS Result, test_code As TestCode, patient_id As PatientID, result.`date` AS DateReleased FROM result WHERE date IN (SELECT MAX(date) FROM result WHERE result.patient_id = '" & txtPatientID.Text & "' And result.section = '" & Section & "' And result.sub_section ='" & SubSection & "' AND `result`.`accepted_result` = 1 GROUP BY test_code) ORDER BY date DESC) AS T1 ON tmpresult.patient_id = T1.PatientID And tmpresult.test_code = T1.TestCode 
                                 Left Join reference_range ON tmpresult.test_code = reference_range.test_code And reference_range.machine = tmpresult.instrument
                                 Left Join specimen ON tmpresult.test_code = specimen.test_code And tmpresult.instrument = specimen.instrument
                                 
                                 WHERE tmpresult.`sample_id` = '" & txtSampleID.Text & "' AND tmpresult.section = '" & Section & "' And tmpresult.sub_section = '" & SubSection & "' AND `tmpresult`.`accepted_result` = 1 And (`reference_range`.`classification` = '" & txtClass.Text & "' And ('" & txtAge.Text & "' BETWEEN `reference_range`.`age_begin` And `reference_range`.`age_end`)) 
                                 GROUP BY T1.TestCode, tmpresult.test_code ORDER BY specimen.order_no ASC"

                    Dim command As New MySql.Data.MySqlClient.MySqlCommand(SQL, conn)

                    Dim adapter1 As New MySql.Data.MySqlClient.MySqlDataAdapter(command)

                    Dim myTable As DataTable = New DataTable
                    adapter1.Fill(myTable)

                    dtResult.DataSource = myTable

                    'End If
                    Disconnect()

                End If
            Else
                Disconnect()

                '       Connect()
                '       rs.Connection = conn
                '       rs.CommandType = CommandType.Text
                '       rs.CommandText = "Select `tmpresult`.`universal_id` As TestName, 
                '                                   `tmpresult`.`flag` As Flag,
                '		`measurement` As Result, 
                '                                   `reference_range`.`si_range` As ReferenceRange, 
                '                                   `tmpresult`.`units` As Unit, 
                '                                   `tmpresult`.`value_conv` AS Conventional, 
                '                                   `tmpresult`.`unit_conv` AS Units,
                '                                   `tmpresult`.`ref_conv` AS RefRange,
                '                                   `tmpresult`.`instrument` AS Instrument,
                '                                   `tmpresult`.`test_code` AS TestCode,
                '                                   `tmpresult`.`id` AS ID, 
                '                                   `tmpresult`.`test_group` AS TestGroup, 
                '                                   `tmpresult`.`his_code` AS HISTestCode, 
                '                                   `tmpresult`.`his_mainid` AS HISMainID, 
                '                                   `tmpresult`.`print_status` AS PrintStatus, 
                '                                   `tmpresult`.`accepted_result` AS AcceptedResult 
                '                           FROM `tmpresult` 
                'LEFT JOIN `reference_range` ON `reference_range`.`test_code` = `tmpresult`.`test_code` AND `reference_range`.`machine` = `tmpresult`.`instrument`
                '                           WHERE tmpresult.`sample_id` = '" & txtSampleID.Text & "' AND tmpresult.section = '" & Section & "' And tmpresult.sub_section = '" & SubSection & "' AND `tmpresult`.`accepted_result` = 1 And (`reference_range`.`classification` = '" & txtClass.Text & "' And `reference_range`.`gender` = '" & cboSex.Text & "' And ('" & txtAge.Text & "' BETWEEN `reference_range`.`age_begin` And `reference_range`.`age_end`)) 
                '                           ORDER BY `tmpresult`.`order_no`"

                '       'WHERE `tmpresult`.`sample_id` = '" & txtSampleID.Text & "' AND `tmpresult`.`section` = '" & Section & "' AND `tmpresult`.`accepted_result` = 1 AND `reference_range`.`gender` = '" & cboSex.Text & "'
                '       'ORDER BY `tmpresult`.`order_no`

                '       reader = rs.ExecuteReader
                '       reader.Read()
                '       If reader.HasRows Then
                '           Disconnect()
                'MessageBox.Show(3)
                'MessageBox.Show(txtSampleID.Text)
                'MessageBox.Show(Section)
                'MessageBox.Show(SubSection)
                'MessageBox.Show(txtClass.Text)
                'MessageBox.Show(cboSex.Text)
                'MessageBox.Show(txtAge.Text)
                Dim SQL As String = "Select `tmpresult`.`universal_id` As TestName, 
                                            `tmpresult`.`flag` As Flag,
											`measurement` As Result, 
                                            `reference_range`.`si_range` As ReferenceRange, 
                                            `tmpresult`.`units` As Unit, 
                                            `tmpresult`.`value_conv` AS Conventional, 
                                            `tmpresult`.`unit_conv` AS Units,
                                            `tmpresult`.`ref_conv` AS RefRange,
                                            `tmpresult`.`instrument` AS Instrument,
                                            `tmpresult`.`test_code` AS TestCode,
                                            `tmpresult`.`id` AS ID, 
                                            `tmpresult`.`test_group` AS TestGroup, 
                                            `tmpresult`.`his_code` AS HISTestCode, 
                                            `tmpresult`.`his_mainid` AS HISMainID, 
                                            `tmpresult`.`print_status` AS PrintStatus, 
                                            `tmpresult`.`accepted_result` AS AcceptedResult 
                                    FROM `tmpresult` 
									LEFT JOIN `reference_range` ON `reference_range`.`test_code` = `tmpresult`.`test_code` AND `reference_range`.`machine` = `tmpresult`.`instrument`
                                    WHERE tmpresult.`sample_id` = '" & txtSampleID.Text & "' AND tmpresult.section = '" & Section & "' And tmpresult.sub_section = '" & SubSection & "' AND `tmpresult`.`accepted_result` = 1 And (`reference_range`.`classification` = '" & txtClass.Text & "' And `reference_range`.`gender` = '" & cboSex.Text & "' And ('" & txtAge.Text & "' BETWEEN `reference_range`.`age_begin` And `reference_range`.`age_end`)) 
                                    ORDER BY `tmpresult`.`order_no`"

                    'WHERE `tmpresult`.`sample_id` = '" & txtSampleID.Text & "' AND `tmpresult`.`section` = '" & Section & "' AND `tmpresult`.`accepted_result` = 1 AND `reference_range`.`gender` = '" & cboSex.Text & "'
                    'ORDER BY `tmpresult`.`order_no`

                    Dim command As New MySql.Data.MySqlClient.MySqlCommand(SQL, conn)

                    'command.Parameters.Clear()
                    'command.Parameters.AddWithValue("@MainID", txtSampleID.Text)
                    'command.Parameters.AddWithValue("@Section", Section)

                    'command.Parameters.AddWithValue("@patientID", txtPatientID.Text)
                    'command.Parameters.AddWithValue("@Classification", txtClass.Text)
                    'command.Parameters.AddWithValue("@Gender", cboSex.Text)
                    'command.Parameters.AddWithValue("@Age", txtAge.Text)

                    Dim adapter2 As New MySql.Data.MySqlClient.MySqlDataAdapter(command)

                    Dim myTable As DataTable = New DataTable
                    adapter2.Fill(myTable)

                    dtResult.DataSource = myTable

                    'End If
                    Disconnect()

                '       Else
                '           Disconnect()
                '           'MessageBox.Show(4)
                '           Dim SQL As String = "Select `tmpresult`.`universal_id` As TestName, 
                '                                   `tmpresult`.`flag` As Flag,
                '		`measurement` As Result, 
                '                                   `reference_range`.`si_range` As ReferenceRange, 
                '                                   `tmpresult`.`units` As Unit, 
                '                                   `tmpresult`.`value_conv` AS Conventional, 
                '                                   `tmpresult`.`unit_conv` AS Units,
                '                                   `tmpresult`.`ref_conv` AS RefRange,
                '                                   `tmpresult`.`instrument` AS Instrument,
                '                                   `tmpresult`.`test_code` AS TestCode,
                '                                   `tmpresult`.`id` AS ID, 
                '                                   `tmpresult`.`test_group` AS TestGroup, 
                '                                   `tmpresult`.`his_code` AS HISTestCode, 
                '                                   `tmpresult`.`his_mainid` AS HISMainID, 
                '                                   `tmpresult`.`print_status` AS PrintStatus, 
                '                                   `tmpresult`.`accepted_result` AS AcceptedResult 
                '                           FROM `tmpresult` 
                'LEFT JOIN `reference_range` ON `reference_range`.`test_code` = `tmpresult`.`test_code` AND `reference_range`.`machine` = `tmpresult`.`instrument`
                '                           WHERE tmpresult.`sample_id` = '" & txtSampleID.Text & "' AND tmpresult.section = '" & Section & "' And tmpresult.sub_section = '" & SubSection & "' AND `tmpresult`.`accepted_result` = 1 And (`reference_range`.`classification` = '" & txtClass.Text & "' And ('" & txtAge.Text & "' BETWEEN `reference_range`.`age_begin` And `reference_range`.`age_end`))
                '                           ORDER BY `tmpresult`.`order_no`"

                '           'WHERE `tmpresult`.`sample_id` = '" & txtSampleID.Text & "' AND `tmpresult`.`section` = '" & Section & "' AND `tmpresult`.`accepted_result` = 1
                '           'ORDER BY `tmpresult`.`order_no`

                '           Dim command As New MySql.Data.MySqlClient.MySqlCommand(SQL, conn)

                'command.Parameters.Clear()
                'command.Parameters.AddWithValue("@MainID", txtSampleID.Text)
                'command.Parameters.AddWithValue("@Section", Section)

                'command.Parameters.AddWithValue("@patientID", txtPatientID.Text)
                'command.Parameters.AddWithValue("@Classification", txtClass.Text)
                'command.Parameters.AddWithValue("@Gender", cboSex.Text)
                'command.Parameters.AddWithValue("@Age", txtAge.Text)

                'Dim adapter2 As New MySql.Data.MySqlClient.MySqlDataAdapter(command)

                '    Dim myTable As DataTable = New DataTable
                '    adapter2.Fill(myTable)

                '    dtResult.DataSource = myTable

                '    'End If
                'Disconnect()
            End If
            'End If

            GridView.Columns("TestCode").Visible = False
            GridView.Columns("ID").Visible = False
            GridView.Columns("HISTestCode").Visible = False
            GridView.Columns("HISMainID").Visible = False
            GridView.Columns("TestGroup").Visible = False
            GridView.Columns("PrintStatus").Visible = False
            'GridView.Columns("AcceptedResult").Visible = False
            GridView.Columns("Conventional").Visible = False
            GridView.Columns("Units").Visible = False
            GridView.Columns("RefRange").Visible = False
            GridView.Columns("AcceptedResult").Visible = False
            'GridView.Columns("Conventional").Visible = False
            'GridView.Columns("Units").Visible = False
            'GridView.Columns("RefRange").Visible = False
            'Version 1.6.0.0-beta
            'Not allow edit on Grid View Columns to prevent it to display on Results Form or cause of error
            GridView.Columns("TestName").OptionsColumn.AllowEdit = False
            GridView.Columns("Flag").OptionsColumn.AllowEdit = False
            GridView.Columns("Unit").OptionsColumn.AllowEdit = False
            GridView.Columns("ReferenceRange").OptionsColumn.AllowEdit = False
            GridView.Columns("Result").OptionsColumn.AllowEdit = True
            GridView.Columns("Units").OptionsColumn.AllowEdit = False
            GridView.Columns("RefRange").OptionsColumn.AllowEdit = False
            GridView.Columns("Instrument").OptionsColumn.AllowEdit = False
            GridView.Columns("Conventional").OptionsColumn.AllowEdit = False

            '-----For Doctor and Medtech Permissiom------
            'Connect()
            'rs.Connection = conn
            'rs.CommandType = CommandType.Text
            'rs.CommandText = "Select `usertype` FROM `user_account` WHERE `email` = '" & CurrEmail & "'"
            'reader = rs.ExecuteReader
            'reader.Read()
            'If reader.HasRows Then
            If CurrType.Contains("Doctor") = True Then
                'Disconnect()
                'cboVerify.Text = CurrUser
                'cboMedTech.Enabled = False
                GroupControl1.Enabled = False
                GroupControl2.Enabled = False
                GridView.Columns("Result").OptionsColumn.AllowEdit = False
                'GroupControl3.Enabled = False
                btnVerify.Visibility = DevExpress.XtraBars.BarItemVisibility.Always

            ElseIf CurrType.Contains("Medical Technol") = True And status = "Verified" Then
                GridView.Columns("Result").OptionsColumn.AllowEdit = False
            Else
                'cboVerify.Enabled = False
            End If
            '    Disconnect()
            'End If
            'Disconnect()
            '-----For Doctor and Medtech Permissiom------

            'If status = "Verified" Then
            '    GridView.Columns("Result").OptionsColumn.AllowEdit = False
            'End If

            ' Make the grid read-only. 
            'GridView.OptionsBehavior.Editable = False
            ' Prevent the focused cell from being highlighted. 
            GridView.OptionsSelection.EnableAppearanceFocusedCell = False
            ' Draw a dotted focus rectangle around the entire row. 
            GridView.FocusRectStyle = DrawFocusRectStyle.RowFullFocus

            For x As Integer = 0 To GridView.RowCount - 1 Step 1
                If GridView.GetRowCellValue(x, GridView.Columns("PrintStatus")) Then
                    GridView.SelectRow(x)
                Else

                End If

                'If Not IsNumeric(GridView.GetRowCellValue(x, "Result")) Then
                '    GridView.SetRowCellValue(x, "Result", 0)
                'End If
            Next

            CreateDropdown()
            FillDropdown()

            'If Validation = "Validated" Then

            'Else
            LoadRangeAndValues()
            'End If
            DiffCount()

        Finally
            If conn.State = ConnectionState.Open Then
                conn.Close()
            End If
        End Try

    End Sub

    Private test As New Hashtable()

    Private Sub FillDropdown()
        On Error Resume Next
        Dim Btype() As String = {
            ControlChars.Quote & "A" & ControlChars.Quote & " Rh(D) POSITIVE",
            ControlChars.Quote & "B" & ControlChars.Quote & " Rh(D) POSITIVE",
            ControlChars.Quote & "AB" & ControlChars.Quote & " Rh(D) POSITIVE",
            ControlChars.Quote & "O" & ControlChars.Quote & " Rh(D) POSITIVE",
            ControlChars.Quote & "A" & ControlChars.Quote & " Rh(D) NEGATIVE",
            ControlChars.Quote & "B" & ControlChars.Quote & " Rh(D) NEGATIVE",
            ControlChars.Quote & "AB" & ControlChars.Quote & " Rh(D) NEGATIVE",
            ControlChars.Quote & "O" & ControlChars.Quote & " Rh(D) NEGATIVE"
            }
        test.Add("BType", Btype)
    End Sub

    Private Sub GridView_ShownEditor(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridView.ShownEditor
        On Error Resume Next
        Dim view As GridView = TryCast(sender, GridView)
        If view.FocusedColumn.FieldName = "Result" Then
            Dim edit As ComboBoxEdit = CType(view.ActiveEditor, ComboBoxEdit)
            edit.Properties.Items.Clear()

            Dim parameter As Object = view.GetRowCellValue(view.FocusedRowHandle, "TestCode")
            If test(parameter) IsNot Nothing Then
                edit.Properties.Items.AddRange(CType(test(parameter), ICollection))
            End If

        End If
    End Sub

    Private Sub CreateDropdown()
        On Error Resume Next
        dtResult.RepositoryItems.Clear()
        Dim _riEditor As New RepositoryItemComboBox()
        dtResult.RepositoryItems.Add(_riEditor)
        GridView.Columns("Result").ColumnEdit = _riEditor

        AddHandler _riEditor.SelectedIndexChanged, Sub(sender1 As Object, e1 As EventArgs)
                                                       'View.SetFocusedRowCellValue(View.FocusedColumn.FieldName = "Conventional", _riEditor /)
                                                   End Sub
    End Sub

    'Private Sub _riEditor_SelectedIndexChange(ByVal sender As Object, ByVal e As System.EventArgs) Handles _riEditor.SelectedIndexChanged
    '    For x As Integer = 0 To Me.GridView.RowCount - 1 Step 1
    '        GridView.SetRowCellValue(_riEditor., GridView.GetRowCellValue(x, GridView.Columns("Result")))
    '    Next
    'End Sub
    Private Sub DiffCount()
        Dim TotalDIFCount As Decimal = 0
        Dim RowValue As Double = 0

        Try
            For x As Integer = 0 To Me.GridView.RowCount - 1 Step 1
                If GridView.IsRowSelected(x) Then
                    If GridView.GetRowCellValue(x, GridView.Columns("TestGroup")) = "Differential Count" Then
                        If GridView.GetRowCellValue(x, "Conventional") Is "" Then
                        Else
                            TotalDIFCount += GridView.GetRowCellValue(x, "Conventional")
                        End If
                    End If
                End If
                '#################################################----TOTAL DIF COUNT----################################################################################


                lblDiffCount.Text = "Total DIF Count:  " & TotalDIFCount

                If TotalDIFCount.ToString = 1 Or TotalDIFCount = 100 Then
                    lblDiffCount.BackColor = Color.DeepSkyBlue
                Else
                    lblDiffCount.BackColor = Color.Crimson
                End If

                If TotalDIFCount.ToString = 1 Or TotalDIFCount = 100 Then
                    lblDiffCount.BackColor = Color.DeepSkyBlue
                Else
                    lblDiffCount.BackColor = Color.Crimson
                End If
                '#################################################----TOTAL DIF COUNT----################################################################################
            Next
        Catch ex As Exception

        End Try

    End Sub

    Public Sub LoadRangeAndValues()
        Try
            Dim RANGE As String = ""
            Dim RANGE_CONVENTIONAL As String = ""
            Dim FLAG As String = ""
            Dim ConvertionFactor, ConvertionMultiplier, DIffCount As Double

            For x As Integer = 0 To Me.GridView.RowCount - 1 Step 1
                rs.Parameters.Clear()
                rs.Parameters.AddWithValue("@patient_id", txtPatientID.Text)
                rs.Parameters.AddWithValue("@TestCode", GridView.GetRowCellValue(x, GridView.Columns("TestCode")))
                rs.Parameters.AddWithValue("@classification", txtClass.Text)
                rs.Parameters.AddWithValue("@gender", cboSex.Text)
                rs.Parameters.AddWithValue("@age", txtAge.Text)
                rs.Parameters.AddWithValue("@main_id", mainID)
                rs.Parameters.AddWithValue("@instrument", GridView.GetRowCellValue(x, GridView.Columns("Instrument")))
                rs.Parameters.AddWithValue("@section", Section)

                ''#################################################----REFERENCE RANGE----################################################################################
                'Connect()
                'rs.Connection = conn
                'rs.CommandType = CommandType.Text
                'rs.CommandText = "SELECT measurement FROM result WHERE test_code = @TestCode AND patient_id = '" & txtPatientID.Text & "' ORDER BY date DESC LIMIT 1"
                'reader = rs.ExecuteReader
                'reader.Read()
                'If reader.HasRows Then
                '    GridView.SetRowCellValue(x, GridView.Columns("PreviousResult"), reader(0).ToString)
                'Else
                '    GridView.SetRowCellValue(x, GridView.Columns("PreviousResult"), "")
                'End If
                'Disconnect()
                ''#################################################----REFERENCE RANGE----################################################################################

                '#################################################----REFERENCE RANGE----################################################################################
                Connect()
                rs.Connection = conn
                'rs.CommandText = "SELECT `si_range`, `conv_range`, `low_value`, `high_value` FROM `reference_range` WHERE `test_code` = @TestCode AND `classification` = @classification AND `gender` = @gender AND `machine` = @instrument AND (`age_begin` <= @age AND `age_end` >= @age)"
                rs.CommandText = "SELECT `si_range`, `conv_range`, `low_value`, `high_value`,`age_begin`,`age_end` FROM `reference_range` 
                    WHERE `test_code` = @TestCode AND `classification` = @classification AND `gender` = @gender AND `machine` = @instrument  AND @age between `age_begin` AND `age_end`"
                reader = rs.ExecuteReader
                reader.Read()
                If reader.HasRows Then
                    RANGE = reader("si_range").ToString
                    RANGE_CONVENTIONAL = reader("conv_range").ToString
                    If Not GridView.GetRowCellValue(x, GridView.Columns("Result")) = "" Then
                        If IsNumeric(GridView.GetRowCellValue(x, GridView.Columns("Result"))) Then
                            If CDbl(GridView.GetRowCellValue(x, GridView.Columns("Result"))) < Val(reader(2).ToString) Then
                                FLAG = "L"
                            ElseIf CDbl(GridView.GetRowCellValue(x, GridView.Columns("Result"))) > Val(reader(3).ToString) Then
                                FLAG = "H"
                            Else
                                FLAG = ""
                            End If
                        Else
                            FLAG = ""
                        End If
                        Disconnect()
                        GridView.SetRowCellValue(x, GridView.Columns("ReferenceRange"), RANGE)
                        GridView.SetRowCellValue(x, GridView.Columns("RefRange"), RANGE_CONVENTIONAL)
                        GridView.SetRowCellValue(x, GridView.Columns("Flag"), FLAG)
                    Else
                        Disconnect()
                        GridView.SetRowCellValue(x, GridView.Columns("ReferenceRange"), RANGE)
                        GridView.SetRowCellValue(x, GridView.Columns("RefRange"), RANGE_CONVENTIONAL)
                    End If
                End If
                Disconnect()
                '#################################################----REFERENCE RANGE----################################################################################

                '#################################################----CONVERTION FACTOR----################################################################################
                Connect()
                rs.Connection = conn
                rs.CommandType = CommandType.Text
                rs.CommandText = "SELECT `convertion_factor`, `convertion_multiplier` FROM `conversion_factor` WHERE `test_code` = @TestCode"
                reader = rs.ExecuteReader
                reader.Read()
                If reader.HasRows Then

                    If Not GridView.GetRowCellValue(x, GridView.Columns("Result")) = "" Then
                        If IsNumeric(GridView.GetRowCellValue(x, GridView.Columns("Result"))) Then
                            ConvertionFactor = reader(0).ToString
                            ConvertionMultiplier = reader(1).ToString
                            Disconnect()
                            GridView.SetRowCellValue(x, GridView.Columns("Conventional"), FormatNumber(Val(GridView.GetRowCellValue(x, GridView.Columns("Result"))) / ConvertionFactor, ConvertionMultiplier))
                        Else
                            Disconnect()
                            'dtResult.Rows(x).Cells(6).Value = ""
                            GridView.SetRowCellValue(x, GridView.Columns("Conventional"), GridView.GetRowCellValue(x, GridView.Columns("Result")))
                        End If
                    Else
                        Disconnect()
                        ConvertionFactor = 0
                    End If

                Else
                    Disconnect()
                    ConvertionFactor = 0
                End If
                Disconnect()
                '#################################################----CONVERTION FACTOR----################################################################################
            Next
        Catch ex As Exception
            Disconnect()
            MessageBox.Show(ex.Message, "Mysql Exception", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub

    Private Sub LoadMedTechPatho()
        Connect()
        rs.Connection = conn
        rs.CommandType = CommandType.Text
        rs.CommandText = "SELECT CONCAT(fname, ' ', mname, ' ', lname, ', ', designation) AS `name` FROM `pathologist` ORDER BY `name`"
        reader = rs.ExecuteReader
        While reader.Read
            cboPathologist.Properties.Items.Add(reader(0))
        End While
        Disconnect()

        'if no pathologist is saved yet it will default to index 0
        If cboPathologist.Text = "" Then
            cboPathologist.SelectedIndex = 0
        End If

        Connect()
        rs.Connection = conn
        rs.CommandType = CommandType.Text
        rs.CommandText = "SELECT * FROM `viewpathologist` WHERE `name` = '" & Me.cboPathologist.Text & "'"
        reader = rs.ExecuteReader
        reader.Read()
        If reader.HasRows Then
            PathologistID = reader(0).ToString
        End If
        Disconnect()

        Connect()
        rs.Connection = conn
        rs.CommandType = CommandType.Text
        rs.CommandText = "SELECT CONCAT(fname, ' ', mname, ' ', lname, ', ', designation) AS `name` FROM `medtech` WHERE `verificator` = 1 AND `id` = '" & CurrEmail & "' ORDER BY `name`"
        reader = rs.ExecuteReader
        While reader.Read
            cboVerify.Properties.Items.Add(reader(0))
        End While
        Disconnect()

        Connect()
        rs.Connection = conn
        rs.CommandType = CommandType.Text
        rs.CommandText = "SELECT * FROM `viewMedTEch` WHERE `name` = '" & Me.cboVerify.Text & "'"
        reader = rs.ExecuteReader
        reader.Read()
        If reader.HasRows Then
            VerifyID = reader(0).ToString
        End If
        Disconnect()

        If My.Settings.MedTech = 0 Then
            Connect()
            rs.Connection = conn
            rs.CommandType = CommandType.Text
            rs.CommandText = "SELECT CONCAT(fname, ' ', mname, ' ', lname, ', ', designation) AS `name` FROM `medtech` WHERE `verificator` = 0 ORDER BY `name`"
            reader = rs.ExecuteReader
            While reader.Read
                cboMedTech.Properties.Items.Add(reader(0))
            End While
            Disconnect()
            Connect()
            rs.Connection = conn
            rs.CommandType = CommandType.Text
            rs.CommandText = "SELECT * FROM `viewMedTEch` WHERE `name` LIKE '" & Me.cboMedTech.Text & "'"
            reader = rs.ExecuteReader
            reader.Read()
            If reader.HasRows Then
                MedTechID = reader(0).ToString
            End If
            Disconnect()
        ElseIf My.Settings.MedTech = 1 Then
            Connect()
            rs.Connection = conn
            rs.CommandType = CommandType.Text
            rs.CommandText = "SELECT CONCAT(fname, ' ', mname, ' ', lname, ', ', designation) AS `name` FROM `medtech` WHERE `id` = '" & CurrEmail & "' ORDER BY `name`"
            reader = rs.ExecuteReader
            While reader.Read
                cboMedTech.Properties.Items.Add(reader(0))
            End While
            Disconnect()
            'cboMedTech.SelectedIndex = 0

            Connect()
            rs.Connection = conn
            rs.CommandType = CommandType.Text
            rs.CommandText = "SELECT * FROM `viewMedTEch` WHERE `name` LIKE '" & Me.cboMedTech.Text & "'"
            reader = rs.ExecuteReader
            reader.Read()
            If reader.HasRows Then
                MedTechID = reader(0).ToString
            End If
            Disconnect()

        End If
        '######################################----END-----###############################################################
    End Sub

    Private Sub frmResultsNew_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        LoadMedTechPatho()

        AutoLoadDoctor()
        AutoLoadRoom()

        LoadTest()
        tmTimeReleased.Enabled = False

        DisablePermission()

        If cboVerify.Text = "" Then
            btnPrint.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
            btnRelease.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
        Else
            btnValidate.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
            btnVerify.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
        End If
        'add to frmchemnew
        If status = "Result Received" Or status = "Processing" Then
            btnReextract.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
        Else
            btnReextract.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
        End If

        getMedDocID()

        'MessageBox.Show(status)

        If cboMedTech.Text = "" And (status = "Processing" Or status = "Result Received") Then
            cboMedTech.SelectedIndex = 0
        End If

        If cboVerify.Text = "" And status = "For Verification" Then
            cboVerify.SelectedIndex = 0
        End If

        If CurrType.Contains("Administrator") = True Then
            'MessageBox.Show(status)
            Disconnect()
            'If status = "Processing" Or status = "Result Received" Then
            '    cboVerify.Text = ""
            '    Disconnect()
            If status = "For Verification" Then
                cboVerify.SelectedIndex = 0
                btnValidate.Enabled = False
                Disconnect()
            End If
        Else
            Disconnect()
        End If

        'Return to Medtech Visibility
        'this is for preparation just incase they need the function
        'Connect()
        'rs.Connection = conn
        'rs.CommandType = CommandType.Text
        'rs.CommandText = "SELECT `usertype` FROM `user_account` WHERE `email` = '" & CurrEmail & "'"
        'reader = rs.ExecuteReader
        'reader.Read()
        'If reader.HasRows Then
        '    If (reader(0) = "Doctor" Or reader(0) = "Administrator") And status = "For Verification" Then
        '        Disconnect()
        '        btnRtnMedTech.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
        '        txtComment.Enabled = False
        '        'cboVerify.Enabled = False
        '        'cboPathologist.Enabled = False
        '    ElseIf reader(0) = "Administrator" And (status = "Processing" Or status = "Result Received") Then
        '        Disconnect()
        '        btnRtnMedTech.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
        '    Else
        '        Disconnect()
        '        btnRtnMedTech.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
        '    End If
        'Else
        '    Disconnect()
        'End If
        'Disconnect()
        'end of return to medtech

        If CurrDept = "WARD" Then
            'If CurrType = "Doctor" Then
            btnReextract.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
            btnReextract.Visibility = DevExpress.XtraBars.BarItemVisibility.Never

        End If

        Connect()
        rs.Connection = conn
        rs.CommandType = CommandType.Text
        rs.CommandText = "SELECT `verificator` FROM `medtech` WHERE `id` = '" & CurrEmail & "'"
        reader = rs.ExecuteReader
        reader.Read()
        If reader(0).ToString = "0" And CurrType.Contains("Administrator") = True Then
            Disconnect()
            btnVerify.Enabled = False
        End If
        Disconnect()

        cboPathologist.Enabled = False
        cboMedTech.Enabled = False
        cboVerify.Enabled = False

        rs.Parameters.Clear()
        rs.Parameters.AddWithValue("@patientID", txtPatientID.Text)

        Connect()
        rs.Connection = conn
        rs.CommandType = CommandType.Text
        rs.CommandText = "Select * From `result` Where `Patient_id` = @patientID"
        reader = rs.ExecuteReader
        reader.Read()
        If reader.HasRows Then
            Disconnect()
            btnPreview.Enabled = True
        Else
            Disconnect()
            btnPreview.Enabled = False
        End If
        Disconnect()
    End Sub

    'Private Sub GridView_KeyDown(sender As Object, e As KeyEventArgs) Handles GridView.KeyDown

    '    Select Case e.KeyCode
    '        Case Keys.Down
    '            e.Handled = True
    '            btnVerify.Enabled = False
    '    End Select
    'End Sub

    Public Sub getMedDocID()
        Connect()
        rs.Connection = conn
        rs.CommandType = CommandType.Text
        rs.CommandText = "SELECT `id`, CONCAT(fname, ' ', mname, ' ', lname) AS `name` FROM `medtech` WHERE `verificator` = 1 AND `id` = '" & CurrEmail & "' ORDER BY `name`"
        reader = rs.ExecuteReader
        reader.Read()
        If reader.HasRows Then
            VerifyID = reader(0).ToString
        End If
        Disconnect()

        'MessageBox.Show(VerifyID)

    End Sub

    Public Sub DisablePermission()

        Connect()
        rs.Connection = conn
        rs.CommandType = CommandType.Text
        rs.CommandText = "SELECT * FROM `user_permission` WHERE `user_id` = '" & CurrEmail & "'"
        reader = rs.ExecuteReader
        While reader.Read()
            If reader(2).ToString = "Preview" Then
                If reader(3).ToString = 0 Then
                    Me.btnViewPrint.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                ElseIf reader(3).ToString = 1 Then
                    Me.btnViewPrint.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                End If
            End If

            If reader(2).ToString = "Print && Release" Then
                If reader(3).ToString = 0 Then
                    Me.btnPrint.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                ElseIf reader(3).ToString = 1 Then
                    Me.btnPrint.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                End If
            End If

            If reader(2).ToString = "Save" Then
                If reader(3).ToString = 0 Then
                    Me.btnValidate.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                ElseIf reader(3).ToString = 1 Then
                    Me.btnValidate.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                End If
            End If

            If reader(2).ToString = "Verify" Then
                If reader(3).ToString = 0 Then
                    Me.btnVerify.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                ElseIf reader(3).ToString = 1 Then
                    Me.btnVerify.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                End If
            End If

            If reader(2).ToString = "Print" Then
                If reader(3).ToString = 0 Then
                    Me.btnPrintNow.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                ElseIf reader(3).ToString = 1 Then
                    Me.btnPrintNow.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                End If
            End If

            If reader(2).ToString = "Validate && Release" Then
                If reader(3).ToString = 0 Then
                    Me.btnRelease.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                ElseIf reader(3).ToString = 1 Then
                    Me.btnRelease.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                End If
            End If

            If reader(2).ToString = "Show Delta Check" Then
                If reader(3).ToString = 0 Then
                    Me.btnPreview.Enabled = False
                ElseIf reader(3).ToString = 1 Then
                    Me.btnPreview.Enabled = True
                End If
            End If

            If reader(2).ToString = "Add Test" Then
                If reader(3).ToString = 0 Then
                    Me.btnAddTest.Enabled = False
                ElseIf reader(3).ToString = 1 Then
                    Me.btnAddTest.Enabled = True
                End If
            End If
        End While
        Disconnect()

    End Sub

    Private Sub GridView_CellValueChanged(sender As Object, e As CellValueChangedEventArgs) Handles GridView.CellValueChanged
        Try
            If e.Column.FieldName = "Result" Then
                LoadRangeAndValues()
            Else
                DiffCount()
            End If
        Catch
        End Try
    End Sub

    Private Sub btnAddTest_ItemClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnAddTest.Click
        frmHemaAddTest.mainID = mainID
        frmHemaAddTest.patientID = txtPatientID.Text
        frmHemaAddTest.TypeResult = "New"
        frmHemaAddTest.SubSection = SubSection
        frmHemaAddTest.Section = "Hematology"
        frmHemaAddTest.HISMainID = GridView.GetRowCellValue(0, GridView.Columns("HISMainID")).ToString
        frmHemaAddTest.ShowDialog()

        ActivityLogs(txtSampleID.Text,
                             txtPatientID.Text,
                              txtName.Text,
                             CurrUser,
                             "Opened Add Test Form",
                             cboRequest.Text,
                             Section,
                             SubSection)
    End Sub

    Private Sub txtAge_EditValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboSex.SelectedIndexChanged, cboSex.TextChanged, txtAge.TextChanged, txtClass.SelectedIndexChanged
        LoadRangeAndValues()
    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        frmHemaPatientList.Type = "New"
        frmHemaPatientList.ShowDialog()
    End Sub

    Private Sub btnDelete_ItemClick(ByVal sender As Object, ByVal e As EventArgs) 
        Try
            rs.Parameters.Clear()
            rs.Parameters.AddWithValue("@sample_id", mainID)
            'Dim rows As DataGridViewRow = dtResult.SelectedRows(0)

            'rs.Parameters.AddWithValue("@id", rows.Cells(11).Value)
            rs.Parameters.AddWithValue("@id", GridView.GetFocusedRowCellValue("ID").ToString)
            rs.Parameters.AddWithValue("@test_code", GridView.GetFocusedRowCellValue("TestCode").ToString)

            DeleteRecordSQL("DELETE FROM `tmpResult` WHERE sample_id = @sample_id AND `id` = @ID")
            'DeleteRecordSQL("DELETE FROM `lis_order` WHERE sample_id = @sample_id AND `order_code` = @test_code")
            'DeleteRecordSQL("DELETE FROM `patient_order` WHERE sample_id = @sample_id AND `id` = @ID")
            LoadTest()
        Catch ex As Exception
            MessageBox.Show("No Records Selected!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try

    End Sub

    Public Sub AutoLoadDoctor()
        Me.cboPhysician.Properties.Items.Clear()
        Connect()
        rs.Connection = conn
        rs.CommandType = CommandType.Text
        rs.CommandText = "SELECT `name` FROM `requestor` ORDER BY `name`"
        reader = rs.ExecuteReader
        While reader.Read
            Me.cboPhysician.Properties.Items.Add(reader(0))
        End While
        Disconnect()
    End Sub

    Public Sub AutoLoadRoom()
        Me.cboRoom.Properties.Items.Clear()
        Connect()
        rs.Connection = conn
        rs.CommandType = CommandType.Text
        rs.CommandText = "SELECT * FROM `department` ORDER BY `dept_name`"
        reader = rs.ExecuteReader
        While reader.Read
            Me.cboRoom.Properties.Items.Add(reader(1).ToString)
        End While
        Disconnect()
    End Sub

    Private Sub dtBDate_EditValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dtBDate.EditValueChanged
        'GetBDate()
    End Sub

    Private Sub btnValidate_ItemClick(ByVal sender As Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnValidate.ItemClick
        With My.Settings
            If .AuthenticateRelease = True Then
                frmAuth.Section = "Hematology"
                frmAuth.Method = "Verify"
                frmAuth.ShowDialog()
            Else
                VerifyResult()
            End If
        End With
    End Sub

    Private Sub btnPrint_ItemClick(ByVal sender As Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnPrint.ItemClick
        With My.Settings
            If .AuthenticateRelease = True Then
                frmAuth.Section = "Hematology"
                frmAuth.Method = "PrintRelease"
                frmAuth.ShowDialog()
            Else
                PrintReleaseResult()
            End If
        End With
    End Sub

    Private Sub btnRelease_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnRelease.ItemClick
        With My.Settings
            If .AuthenticateRelease = True Then
                frmAuth.Section = "Hematology"
                frmAuth.Method = "Release"
                frmAuth.ShowDialog()
            Else
                ReleaseResultNoPrint()
            End If
        End With
    End Sub

    Private Sub btnRetrive_Click(sender As Object, e As EventArgs) Handles btnRetrive.Click
        frmHemaRerun.mainID = mainID
        frmHemaRerun.Section = Section
        frmHemaRerun.SubSection = SubSection
        frmHemaRerun.ShowDialog()

    End Sub

    Private Sub btnPrintNow_ItemClick(ByVal sender As Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnPrintNow.ItemClick
        Try
            If Me.txtName.Text = "" Or cboMedTech.Text = "" Or txtAge.Text = "" Or cboSex.Text = "" Then
                'MessageBox.Show("Please verify the data carefully.", "Verification Failed", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                'Exit Sub
                If Me.txtName.Text = "" Or cboMedTech.Text = "" Or txtAge.Text = "" Or cboSex.Text = "" Then
                    If Me.txtName.Text = "" Then
                        MessageBox.Show("Please Fill Up Patient Name First.", "Patient Name is Empty", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    ElseIf txtAge.Text = "" Then
                        MessageBox.Show("Please Fill Up Patient Age First.", "Patient Age is Empty", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    ElseIf cboSex.Text = "" Then
                        MessageBox.Show("Please Fill Up Patient Name First.", "Patient Sex is Empty", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    ElseIf Me.cboMedTech.Text = "" Then
                        MessageBox.Show("Please Fill Up MedTech Field Signatory First.", "MedTech Field Signatory is Empty", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    ElseIf Me.cboVerify.Text = "" Then
                        MessageBox.Show("Please Fill Up Verify Field Signatory First.", "Verify Field Signatory is Empty", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Else
                        MessageBox.Show("Please verify the data carefully.", "Verification Failed", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    End If
                    Exit Sub
                End If
            End If

            rs.Parameters.Clear()
            rs.Parameters.AddWithValue("@mainID", mainID)
            rs.Parameters.AddWithValue("@Section", Section)
            rs.Parameters.AddWithValue("@SubSection", SubSection)

            rs.Parameters.AddWithValue("@status", "Printed")
            rs.Parameters.AddWithValue("@specimen_tracking_date_time", Now) 'version 1.6.0.0-beta

            UpdateRecordwthoutMSG("UPDATE `tmpWorklist` SET " _
                    & "`status` = @status" _
                    & " WHERE main_id = @mainID AND testtype = @Section AND sub_section = @SubSection"
                    )

            UpdateRecordwthoutMSG("UPDATE `specimen_tracking` SET " _
                & "`sample_id` = @mainID," _
                & "`printed` = @specimen_tracking_date_time" _
                & " WHERE sample_id = @mainID AND section = @Section AND sub_section = @Subsection"
                )

            Using myRDLCPrinter As New RDLCPrinterPrintNew(mainID, Section, SubSection, "", My.Settings.DefaultPrinter)
                If My.Settings.SaveAsPDF Then
                    Dim byteViewer As Byte() = myRDLCPrinter.LocalReport.Render("PDF")
                    Dim saveFileDialog1 As New SaveFileDialog()
                    saveFileDialog1.Filter = "*PDF files (*.pdf)|*.pdf"
                    saveFileDialog1.FilterIndex = 2
                    saveFileDialog1.RestoreDirectory = True
                    Dim newFile As New FileStream(My.Settings.PDFLocation & txtSampleID.Text & "_" & txtName.Text & ".pdf", FileMode.Create)
                    newFile.Write(byteViewer, 0, byteViewer.Length)
                    newFile.Close()

                    myRDLCPrinter.Print(1)
                Else
                    myRDLCPrinter.Print(1)
                End If
            End Using

            Connect()
            rs.Dispose()
            rs.Connection = conn
            rs.CommandType = CommandType.Text
            rs.CommandText = "INSERT INTO `order` (`status`, `sample_id`, `patient_id`, `patient_name`, `age`, `sex`, `bdate`, `physician`, `dept`, `medtech`, `verified_by`, `date`, `time`, `main_id`, `dt_released`, `patient_type`, `test`, `testtype`, `barcode`, `instrument`, `type`, `location`, `sub_section`) " _
                & "SELECT `status`, `sample_id`, `patient_id`, `patient_name`, `age`, `sex`, `bdate`, `physician`, `dept`, `medtech`, `verified_by`, `date`, `time`, `main_id`, `dt_released`, `patient_type`, `test`, `testtype`, `barcode`, `instrument`, `type`, `location`, `sub_section` FROM `tmpWorklist` WHERE `main_id` = @mainID AND testtype = @Section AND sub_section = @SubSection"
            rs.ExecuteNonQuery()
            Disconnect()

            Connect()
            rs.Connection = conn
            rs.CommandType = CommandType.Text
            rs.CommandText = "INSERT INTO `result` (`universal_id`, `flag`, `measurement`, `units`, `reference_range`, `value_conv`, `unit_conv`, `ref_conv`, `test_code`, `order_no`, `sample_id`, `test_group`, `date`, `patient_id`, `instrument`, `his_code`, `his_mainid`, `section`, `sub_section`, `print_status`) " _
                & "SELECT `universal_id`, `flag`, `measurement`, `units`, `reference_range`, `value_conv`, `unit_conv`, `ref_conv`, `test_code`, `order_no`, `sample_id`, `test_group`, `date`, `patient_id`, `instrument`, `his_code`, `his_mainid`, `section`, `sub_section`, `print_status` FROM `tmpResult` WHERE `sample_id` = @mainID AND section = @Section AND sub_section = @SubSection"
            rs.ExecuteNonQuery()
            Disconnect()

            Connect()
            rs.Connection = conn
            rs.CommandType = CommandType.Text
            rs.CommandText = "DELETE FROM `tmpWorklist` WHERE `main_id` = @mainID AND testtype = @Section AND sub_section = @SubSection"
            rs.ExecuteNonQuery()
            Disconnect()

            Connect()
            rs.Connection = conn
            rs.CommandType = CommandType.Text
            rs.CommandText = "DELETE FROM `tmpResult` WHERE `sample_id` = @mainID AND section = @Section AND sub_section = @SubSection"
            rs.ExecuteNonQuery()
            Disconnect()

            'UpdateWorkSheet()

            'version 1.6.0.0-beta
            'Save the Processed, Validated and Printed Time on 'specimen_tracking' table


            With My.Settings
                If .HL7Write = True Then
                    CreateHL7File()
                End If
            End With

            frmHemaWorklist.LoadRecords()
            frmHemaWorklist.LoadRecordsCompleted()

            ActivityLogs(txtSampleID.Text,
                             txtPatientID.Text,
                              txtName.Text,
                             CurrUser,
                             "print",
                             cboRequest.Text,
                             Section,
                             SubSection)

            Me.Close()
            Me.Dispose()
        Catch ex As Exception
            Disconnect()
            MessageBox.Show(ex.Message, "Mysql Exception", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub

    Private Sub btnViewPrint_ItemClick(ByVal sender As Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnViewPrint.ItemClick
        'For Each rows As DataGridViewRow In GridView.rows
        RPTresults.sample_id = mainID

        PrintPreview(mainID, "tmpworklist", "tmpresult", "1", Section, SubSection, RPTresults, RPTresults.ReportViewer1)

        'Next
    End Sub

    Private Sub GridView_RowCellStyle(sender As Object, e As RowCellStyleEventArgs) Handles GridView.RowCellStyle
        If e.Column.FieldName = "TestName" Then
            e.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default
        Else
            e.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        End If

        If GridView.GetRowCellValue(e.RowHandle, "Flag").ToString = "L" Then
            e.Appearance.BackColor = Color.Yellow
            e.Appearance.BackColor2 = Color.Yellow
            e.Appearance.ForeColor = Color.Black
        ElseIf GridView.GetRowCellValue(e.RowHandle, "Flag").ToString = "H" Then
            e.Appearance.BackColor = Color.Crimson
            e.Appearance.BackColor2 = Color.Crimson
            e.Appearance.ForeColor = Color.White
        End If
    End Sub

    '######################################### Shift Function #########################################
    Private Function getShift(ByVal shift As TimeSpan)
        Dim readerVar As String

        'Variable that Determine the Shift Next Day
        Dim thrdshft As New TimeSpan(22, 0, 0) '22:00:00
        Dim thrdshft2 As New TimeSpan(7, 0, 0) '07:00:00

        Connect()
        rs.Connection = conn
        rs.CommandType = CommandType.Text
        rs.CommandText = "SELECT * FROM `shift`"
        reader = rs.ExecuteReader
        While reader.Read
            'If Data in database (reader) is greater than equal or less than equal to variable above
            If reader(2) >= thrdshft And reader(3) <= thrdshft2 Then
                If shift > reader(2) Or shift < reader(3) Then
                    readerVar = reader(1).ToString
                    Disconnect()
                    Return readerVar
                    Exit Function
                ElseIf shift < reader(3) Then
                    readerVar = reader(1).ToString
                    Disconnect()
                    Return readerVar
                    Exit Function

                End If
            ElseIf shift > reader(2) Then
                If shift < reader(3) Then
                    readerVar = reader(1).ToString
                    Disconnect()
                    Return readerVar
                    Exit Function
                End If
            End If
        End While
        Disconnect()
        Return shift
    End Function
    '######################################### End of Shift Function #########################################

#Region "Procedure for Release"
    Public Sub ReleaseResultNoPrint()
        Try
            'If Me.txtName.Text = "" Or cboMedTech.Text = "" Or txtAge.Text = "" Or cboSex.Text = "" Then
            'MessageBox.Show("Please verify the data carefully.", "Verification Failed", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            'Exit Sub
            If Me.txtName.Text = "" Or cboMedTech.Text = "" Or cboVerify.Text = "" Or txtAge.Text = "" Or cboSex.Text = "" Then
                If Me.txtName.Text = "" Then
                    MessageBox.Show("Please Fill Up Patient Name First.", "Patient Name is Empty", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                ElseIf txtAge.Text = "" Then
                    MessageBox.Show("Please Fill Up Patient Age First.", "Patient Age is Empty", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                ElseIf cboSex.Text = "" Then
                    MessageBox.Show("Please Fill Up Patient Name First.", "Patient Sex is Empty", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                ElseIf Me.cboMedTech.Text = "" Then
                    MessageBox.Show("Please Fill Up MedTech Field Signatory First.", "MedTech Field Signatory is Empty", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                ElseIf Me.cboVerify.Text = "" Then
                    MessageBox.Show("Please Fill Up Verify Field Signatory First.", "Verify Field Signatory is Empty", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Else
                    MessageBox.Show("Please verify the data carefully.", "Verification Failed", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                End If
                Exit Sub
            End If
            'messagebox for no result
            Dim countEmpty2 As Integer = 0

            For x As Integer = 0 To GridView.RowCount - 1 Step 1
                If GridView.GetRowCellValue(x, "Result") = "" Then
                    'GridView.SetRowCellValue(x, "Result", 0)
                    countEmpty2 = countEmpty2 + 1
                End If
            Next

            If countEmpty2 > 0 Then
                If MessageBox.Show("Result(s) must have value to be released", "Empty Result(s)", MessageBoxButtons.OK, MessageBoxIcon.Warning) = DialogResult.OK Then
                    Exit Sub
                End If
            End If


            With My.Settings
                If .LISType = "Lite" Then
                    MainSampleID = ID_Randomizer()
                Else
                    MainSampleID = mainID
                End If
            End With

            rs.Parameters.Clear()
            rs.Parameters.AddWithValue("@mainID", mainID)
            rs.Parameters.AddWithValue("@MainSampleID", MainSampleID)

            rs.Parameters.AddWithValue("@medtech", cboMedTech.Text)
            rs.Parameters.AddWithValue("@medtechid", MedTechID)

            'If My.Settings.AuthenticateRelease = True Then
            '    rs.Parameters.AddWithValue("@verifyid", UserIDRelease)
            '    rs.Parameters.AddWithValue("@verify", UserRelease)
            'Else
            rs.Parameters.AddWithValue("@verifyid", VerifyID)

            rs.Parameters.AddWithValue("@verify", cboVerify.Text)
            'End If

            rs.Parameters.AddWithValue("@pathologist", PathologistID)

            rs.Parameters.AddWithValue("@remarks", txtRemarks.Text)
            rs.Parameters.AddWithValue("@lab_comment", txtComment.Text)
            rs.Parameters.AddWithValue("@status", "Printed")
            rs.Parameters.AddWithValue("@id", SampleID)
            rs.Parameters.AddWithValue("@sample_id", txtSampleID.Text)
            rs.Parameters.AddWithValue("@patient_id", txtPatientID.Text)
            rs.Parameters.AddWithValue("@name", txtName.Text)
            rs.Parameters.AddWithValue("@age", txtAge.Text)
            rs.Parameters.AddWithValue("@type", txtClass.Text)
            rs.Parameters.AddWithValue("@bdate", dtBDate.Text)
            rs.Parameters.AddWithValue("@physician", cboPhysician.Text)
            rs.Parameters.AddWithValue("@room", cboRoom.Text)
            rs.Parameters.AddWithValue("@sex", cboSex.Text)
            rs.Parameters.AddWithValue("@CS", cboCS.Text)
            rs.Parameters.AddWithValue("@address", txtAddress.Text)
            rs.Parameters.AddWithValue("@contact", txtContact.Text)
            rs.Parameters.AddWithValue("@test", cboRequest.Text)
            rs.Parameters.AddWithValue("@patient_type", cboPatientType.Text)
            rs.Parameters.AddWithValue("@time", tmTimeReceived.Value.ToLongTimeString)
            rs.Parameters.AddWithValue("@date_release", tmTimeReleased.Value)
            rs.Parameters.AddWithValue("@accession_no", txtAccession.Text)
            rs.Parameters.AddWithValue("@OR_No", txtORNo.Text)
            rs.Parameters.AddWithValue("@CS_No", txtChargeSlip.Text)

            rs.Parameters.AddWithValue("@Section", Section)
            rs.Parameters.AddWithValue("@SubSection", SubSection)
            rs.Parameters.AddWithValue("@specimen_tracking_date_time", Now) 'version 1.6.0.0-beta

            rs.Parameters.Add("@date", MySql.Data.MySqlClient.MySqlDbType.Date).Value = Format(dtReceived.Value, "yyyy-MM-dd")

            Connect()
            rs.Connection = conn
            rs.CommandType = CommandType.Text
            rs.CommandText = "SELECT * FROM patient_info WHERE `patient_id` LIKE @PATIENT_ID"
            reader = rs.ExecuteReader
            reader.Read()
            If reader.HasRows Then
                Disconnect()
                SaveRecordwthoutMSG("UPDATE `patient_info` SET " _
                        & "`patient_id` = @patient_id," _
                        & "`name` = @name," _
                        & "`sex` = @sex," _
                        & "`date_of_birth` = @bdate," _
                        & "`age` = @age," _
                        & "`civil_status` = @CS," _
                        & "`address` = @address," _
                        & "`contact_no` = @contact," _
                        & "`sample_id` = @MainSampleID" _
                        & " WHERE `patient_id` = @patient_id"
                        )
            Else
                Disconnect()
                SaveRecordwthoutMSG("INSERT INTO patient_info (patient_id, name, sex, date_of_birth, age, civil_status, address, contact_no, `date`, `sample_id`) VALUES " _
                        & "(" _
                        & "@patient_id," _
                        & "@name," _
                        & "@sex," _
                        & "@bdate," _
                        & "@age," _
                        & "@CS," _
                        & "@address," _
                        & "@contact," _
                        & "@date," _
                        & "@MainSampleID" _
                        & ")"
                        )
            End If
            Disconnect()

            UpdateRecordwthoutMSG("UPDATE `tmpWorklist` SET " _
                    & "`sample_id` = @sample_id," _
                    & "`patient_id` = @patient_id," _
                    & "`patient_name` = @name," _
                    & "`sex` = @sex," _
                    & "`bdate` = @bdate," _
                    & "`age` = @age," _
                    & "`type` = @type," _
                    & "`physician` = @physician," _
                    & "`dept` = @room," _
                    & "`medtech` = @medtech," _
                    & "`verified_by` = @verify," _
                    & "`test` = @test," _
                    & "`patient_type` = @patient_type," _
                    & "`status` = @status," _
                    & "`dt_released` = @date_release," _
                    & "`main_id` = @MainSampleID" _
                    & " WHERE `main_id` = @mainID AND `testtype` = @Section AND `sub_section` = @SubSection"
                    )

            Connect()
            rs.Connection = conn
            rs.CommandType = CommandType.Text
            rs.CommandText = "SELECT `sample_id` FROM `order_pathologist` WHERE `sample_id` = @MainSampleID"
            reader = rs.ExecuteReader
            reader.Read()
            If reader.HasRows Then
                Disconnect()
                UpdateRecordwthoutMSG("UPDATE `order_pathologist` SET `pathologist_id` = @pathologist WHERE `sample_id` = @MainSampleID")
            Else
                Disconnect()
                SaveRecordwthoutMSG("INSERT INTO `order_pathologist` (`sample_id`, `pathologist_id`) VALUES " _
                & "(" _
                & "@MainSampleID," _
                & "@pathologist" _
                & ")"
                )
            End If
            Disconnect()

            Connect()
            rs.Connection = conn
            rs.CommandType = CommandType.Text
            rs.CommandText = "SELECT `sample_id` FROM `order_medtech` WHERE `sample_id` = @MainSampleID"
            reader = rs.ExecuteReader
            reader.Read()
            If reader.HasRows Then
                Disconnect()
                UpdateRecordwthoutMSG("UPDATE `order_medtech` SET `medtech_id` = @medtechid WHERE `sample_id` = @MainSampleID")
            Else
                Disconnect()
                SaveRecordwthoutMSG("INSERT INTO `order_medtech` (`sample_id`, `medtech_id`) VALUES " _
                & "(" _
                & "@MainSampleID," _
                & "@medtechid" _
                & ")"
                )
            End If
            Disconnect()

            'Connect()
            'rs.Connection = conn
            'rs.CommandType = CommandType.Text
            'rs.CommandText = "SELECT `sample_id` FROM `order_Verified` WHERE `sample_id` = @MainSampleID"
            'reader = rs.ExecuteReader
            'reader.Read()
            'If reader.HasRows Then
            '    Disconnect()
            '    UpdateRecordwthoutMSG("UPDATE `order_Verified` SET `medtech_id` = @verifyid WHERE `sample_id` = @MainSampleID")
            'Else
            '    Disconnect()
            '    SaveRecordwthoutMSG("INSERT INTO `order_Verified` (`sample_id`, `medtech_id`) VALUES " _
            '    & "(" _
            '    & "@MainSampleID," _
            '    & "@verifyid" _
            '    & ")"
            '    )
            'End If
            'Disconnect()


            If Not txtAccession.Text = "" Or txtORNo.Text = "" Or txtChargeSlip.Text = "" Then
                Connect()
                rs.Connection = conn
                rs.CommandType = CommandType.Text
                rs.CommandText = "SELECT * FROM `additional_info` WHERE `sample_id` = @mainID AND section = @Section AND sub_section = @SubSection"
                reader = rs.ExecuteReader
                reader.Read()
                If reader.HasRows Then
                    Disconnect()
                    UpdateRecordwthoutMSG("UPDATE `additional_info` SET " _
                                & "`accession_no` = @accession_no," _
                                & "`or_no` = @OR_No," _
                                & "`cs_no` = @CS_No," _
                                & "`sample_id` = @MainSampleID" _
                                & " WHERE `sample_id` = @mainID AND section = @Section AND sub_section = @SubSection"
                                )
                Else
                    Disconnect()
                    SaveRecordwthoutMSG("INSERT INTO `additional_info` (`accession_no`, `or_no`, `cs_no`, section, sub_section, `sample_id`) VALUES " _
                                & "(" _
                                & "@accession_no," _
                                & "@OR_No," _
                                & "@CS_No," _
                                & "@Section," _
                                & "@SubSection," _
                                & "@MainSampleID" _
                                & ")"
                                )
                End If
                Disconnect()
            End If

            For x As Integer = 0 To GridView.RowCount - 1 Step 1

                If (GridView.IsRowSelected(x)) Then
                    UpdateRecordwthoutMSG("UPDATE `tmpresult` SET " _
                          & "`flag` = '" & GridView.GetRowCellValue(x, "Flag") & "'," _
                          & "`measurement` = '" & GridView.GetRowCellValue(x, "Result") & "'," _
                          & "`reference_range` = '" & GridView.GetRowCellValue(x, "ReferenceRange") & "'," _
                          & "`value_conv` = '" & GridView.GetRowCellValue(x, "Conventional") & "'," _
                          & "`ref_conv` = '" & GridView.GetRowCellValue(x, "RefRange") & "'," _
                          & "`patient_id` = @patient_id," _
                          & "`instrument` = '" & GridView.GetRowCellValue(x, "Instrument") & "'," _
                          & "`print_status` = '1'," _
                          & "`date` = @date_release," _
                          & "`sample_id` = @MainSampleID" _
                          & " WHERE `sample_id` = @mainID AND `test_code` = '" & GridView.GetRowCellValue(x, "TestCode") & "' AND `accepted_result` = '" & GridView.GetRowCellValue(x, "AcceptedResult") & "' AND `id` = '" & GridView.GetRowCellValue(x, "ID") & "'"
                          )
                Else
                    UpdateRecordwthoutMSG("UPDATE `tmpresult` SET " _
                          & "`flag` = '" & GridView.GetRowCellValue(x, "Flag") & "'," _
                          & "`measurement` = '" & GridView.GetRowCellValue(x, "Result") & "'," _
                          & "`reference_range` = '" & GridView.GetRowCellValue(x, "ReferenceRange") & "'," _
                          & "`value_conv` = '" & GridView.GetRowCellValue(x, "Conventional") & "'," _
                          & "`ref_conv` = '" & GridView.GetRowCellValue(x, "RefRange") & "'," _
                          & "`patient_id` = @patient_id," _
                          & "`instrument` = '" & GridView.GetRowCellValue(x, "Instrument") & "'," _
                          & "`print_status` = '0'," _
                          & "`date` = @date_release," _
                          & "`sample_id` = @MainSampleID" _
                          & " WHERE `sample_id` = @mainID AND `test_code` = '" & GridView.GetRowCellValue(x, "TestCode") & "' AND `accepted_result` = '" & GridView.GetRowCellValue(x, "AcceptedResult") & "' AND `id` = '" & GridView.GetRowCellValue(x, "ID") & "'"
                          )
                End If
            Next

            Connect()
            'rs.Parameters.Clear()
            rs.Parameters.AddWithValue("@Comment", txtExtracted.Text)
            'rs.Parameters.AddWithValue("@MainID", mainID)
            rs.Connection = conn
            rs.CommandType = CommandType.Text
            rs.CommandText = "SELECT `sample_id` FROM `lab_comment` WHERE `sample_id` = @mainID"
            reader = rs.ExecuteReader
            reader.Read()
            If reader.HasRows Then
                Disconnect()
                UpdateRecordwthoutMSG("UPDATE `lab_comment` SET `comment` = @Comment WHERE `sample_id` = @mainID")
            Else
                Disconnect()
                SaveRecordwthoutMSG("INSERT INTO `lab_comment` (`comment`, `sample_id`) VALUES (@Comment, @MainID)")
            End If
            Disconnect()

            Connect()
            rs.Connection = conn
            rs.CommandType = CommandType.Text
            rs.CommandText = "SELECT `sample_id` FROM `patient_remarks` WHERE `sample_id` = @mainID"
            reader = rs.ExecuteReader
            reader.Read()
            If reader.HasRows Then
                Disconnect()
                UpdateRecordwthoutMSG("UPDATE `patient_remarks` SET `remarks` = @remarks, `diagnosis` = @lab_comment WHERE `sample_id` = @mainID AND `section` = @Section AND `sub_section` = @SubSection")
            Else
                Disconnect()
                SaveRecordwthoutMSG("INSERT INTO `patient_remarks` (`remarks`, `sample_id`, `section`, `sub_section`) VALUES (@remarks, @MainSampleID, @Section, @SubSection)")
            End If
            Disconnect()

            Connect()
            rs.Connection = conn
            rs.CommandType = CommandType.Text
            rs.CommandText = "INSERT INTO `order` (`status`, `sample_id`, `patient_id`, `patient_name`, `age`, `sex`, `bdate`, `physician`, `dept`, `medtech`, `verified_by`, `date`, `time`, `main_id`, `dt_released`, `patient_type`, `test`, `testtype`, `barcode`, `instrument`, `type`, `location`, `sub_section`) " _
                & "SELECT `status`, `sample_id`, `patient_id`, `patient_name`, `age`, `sex`, `bdate`, `physician`, `dept`, `medtech`, `verified_by`, `date`, `time`, @MainSampleID, `dt_released`, `patient_type`, `test`, `testtype`, `barcode`, `instrument`, `type`, `location`, `sub_section` FROM `tmpWorklist` WHERE `main_id` = @MainSampleID AND testtype = @Section AND sub_section = @SubSection"
            rs.ExecuteNonQuery()
            Disconnect()

            Connect()
            rs.Connection = conn
            rs.CommandType = CommandType.Text
            rs.CommandText = "INSERT INTO `result` (`universal_id`, `flag`, `measurement`, `units`, `reference_range`, `value_conv`, `unit_conv`, `ref_conv`, `test_code`, `order_no`, `sample_id`, `test_group`, `date`, `patient_id`, `instrument`, `his_code`, `his_mainid`, `section`, `sub_section`, `print_status`, `accepted_result`) " _
                & "SELECT `universal_id`, `flag`, `measurement`, `units`, `reference_range`, `value_conv`, `unit_conv`, `ref_conv`, `test_code`, `order_no`, @MainSampleID, `test_group`, `date`, `patient_id`, `instrument`, `his_code`, `his_mainid`, `section`, `sub_section`, `print_status`, `accepted_result` FROM `tmpResult` WHERE `sample_id` = @MainSampleID AND section = @Section"
            rs.ExecuteNonQuery()
            Disconnect()

            Connect()
            rs.Connection = conn
            rs.CommandType = CommandType.Text
            rs.CommandText = "DELETE FROM `tmpWorklist` WHERE `main_id` = @MainSampleID AND testtype = @Section AND sub_section = @SubSection"
            rs.ExecuteNonQuery()
            Disconnect()

            'delete from tmpworklist
            Connect()
            rs.Connection = conn
            rs.CommandType = CommandType.Text
            rs.CommandText = "DELETE FROM `tmpResult` WHERE `sample_id` = @MainSampleID AND section = @Section AND sub_section = @SubSection"
            rs.ExecuteNonQuery()
            Disconnect()

            'UpdateWorkSheet()
            'version 1.6.0.0-beta
            'Save the Processed, Validated and Printed Time on 'specimen_tracking' table
            UpdateRecordwthoutMSG("UPDATE `specimen_tracking` SET " _
                & "`sample_id` = @MainSampleID," _
                & "`printed` = @specimen_tracking_date_time" _
                & " WHERE sample_id = @mainID AND section = @Section AND sub_section = @Subsection"
                )

            With My.Settings
                If .HL7Write = True Then
                    CreateHL7File()
                End If
            End With

            With My.Settings
                If .SQLConnection = True Then
                    UpdateResultiHOMIS()
                End If
            End With

            frmHemaWorklist.LoadRecords()
            frmHemaWorklist.LoadRecordsCompleted()

            ActivityLogs(txtSampleID.Text,
                             txtPatientID.Text,
                              txtName.Text,
                             CurrUser,
                             "Release Result",
                             cboRequest.Text,
                             Section,
                             SubSection)

            Me.Close()
            Me.Dispose()
        Catch ex As Exception
            Disconnect()
            MessageBox.Show(ex.Message, "Mysql Exception", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End Try
    End Sub

    Public Sub PrintReleaseResult()
        Try
            'If Me.txtName.Text = "" Or cboMedTech.Text = "" Or txtAge.Text = "" Or cboSex.Text = "" Then
            'MessageBox.Show("Please verify the data carefully.", "Verification Failed", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            'Exit Sub

            If Me.txtName.Text = "" Or cboMedTech.Text = "" Or cboVerify.Text = "" Or txtAge.Text = "" Or cboSex.Text = "" Then
                If Me.txtName.Text = "" Then
                    MessageBox.Show("Please Fill Up Patient Name First.", "Patient Name is Empty", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                ElseIf txtAge.Text = "" Then
                    MessageBox.Show("Please Fill Up Patient Age First.", "Patient Age is Empty", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                ElseIf cboSex.Text = "" Then
                    MessageBox.Show("Please Fill Up Patient Name First.", "Patient Sex is Empty", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                ElseIf Me.cboMedTech.Text = "" Then
                    MessageBox.Show("Please Fill Up MedTech Field Signatory First.", "MedTech Field Signatory is Empty", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                ElseIf Me.cboVerify.Text = "" Then
                    MessageBox.Show("Please Fill Up Verify Field Signatory First.", "Verify Field Signatory is Empty", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Else
                    MessageBox.Show("Please verify the data carefully.", "Verification Failed", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                End If
                Exit Sub
            End If
            'End If

            Dim countEmpty As Integer = 0
            For x As Integer = 0 To GridView.RowCount - 1 Step 1
                If GridView.GetRowCellValue(x, "Result") = "" Then
                    'GridView.SetRowCellValue(x, "Result", 0)
                    countEmpty = countEmpty + 1
                End If
            Next

            If countEmpty > 0 Then
                If MessageBox.Show("Result(s) must have value to be released", "Empty Result(s)", MessageBoxButtons.OK, MessageBoxIcon.Warning) = DialogResult.OK Then
                    Exit Sub
                End If
            End If

            With My.Settings
                If .LISType = "Lite" Then
                    MainSampleID = ID_Randomizer()
                Else
                    MainSampleID = mainID
                End If
            End With

            rs.Parameters.Clear()
            rs.Parameters.AddWithValue("@mainID", mainID)
            rs.Parameters.AddWithValue("@MainSampleID", MainSampleID)

            rs.Parameters.AddWithValue("@medtech", cboMedTech.Text)
            rs.Parameters.AddWithValue("@medtechid", MedTechID)

            If My.Settings.AuthenticateRelease = True Then
                rs.Parameters.AddWithValue("@verifyid", UserIDRelease)
                rs.Parameters.AddWithValue("@verify", UserRelease)
            Else
                rs.Parameters.AddWithValue("@verifyid", VerifyID)
                rs.Parameters.AddWithValue("@verify", cboVerify.Text)
            End If

            rs.Parameters.AddWithValue("@pathologist", PathologistID)

            rs.Parameters.AddWithValue("@remarks", txtRemarks.Text)
            rs.Parameters.AddWithValue("@lab_comment", txtComment.Text)
            rs.Parameters.AddWithValue("@status", "Printed")
            rs.Parameters.AddWithValue("@id", SampleID)
            rs.Parameters.AddWithValue("@sample_id", txtSampleID.Text)
            rs.Parameters.AddWithValue("@patient_id", txtPatientID.Text)
            rs.Parameters.AddWithValue("@name", txtName.Text)
            rs.Parameters.AddWithValue("@age", txtAge.Text)
            rs.Parameters.AddWithValue("@type", txtClass.Text)
            rs.Parameters.AddWithValue("@bdate", dtBDate.Text)
            rs.Parameters.AddWithValue("@physician", cboPhysician.Text)
            rs.Parameters.AddWithValue("@room", cboRoom.Text)
            rs.Parameters.AddWithValue("@sex", cboSex.Text)
            rs.Parameters.AddWithValue("@CS", cboCS.Text)
            rs.Parameters.AddWithValue("@address", txtAddress.Text)
            rs.Parameters.AddWithValue("@contact", txtContact.Text)
            rs.Parameters.AddWithValue("@test", cboRequest.Text)
            rs.Parameters.AddWithValue("@patient_type", cboPatientType.Text)
            rs.Parameters.AddWithValue("@time", tmTimeReceived.Value.ToLongTimeString)
            rs.Parameters.AddWithValue("@date_release", tmTimeReleased.Value)
            rs.Parameters.AddWithValue("@accession_no", txtAccession.Text)
            rs.Parameters.AddWithValue("@OR_No", txtORNo.Text)
            rs.Parameters.AddWithValue("@CS_No", txtChargeSlip.Text)

            rs.Parameters.AddWithValue("@Section", Section)
            rs.Parameters.AddWithValue("@SubSection", SubSection)
            rs.Parameters.AddWithValue("@specimen_tracking_date_time", Now) 'version 1.6.0.0-beta

            rs.Parameters.Add("@date", MySql.Data.MySqlClient.MySqlDbType.Date).Value = Format(dtReceived.Value, "yyyy-MM-dd")

            Connect()
            rs.Connection = conn
            rs.CommandType = CommandType.Text
            rs.CommandText = "SELECT * FROM patient_info WHERE `patient_id` = @PATIENT_ID"
            reader = rs.ExecuteReader
            reader.Read()
            If reader.HasRows Then
                Disconnect()
                SaveRecordwthoutMSG("UPDATE `patient_info` SET " _
                        & "`patient_id` = @patient_id," _
                        & "`name` = @name," _
                        & "`sex` = @sex," _
                        & "`date_of_birth` = @bdate," _
                        & "`age` = @age," _
                        & "`civil_status` = @CS," _
                        & "`address` = @address," _
                        & "`contact_no` = @contact," _
                        & "`sample_id` = @MainSampleID" _
                        & " WHERE `patient_id` = @patient_id"
                        )
            Else
                Disconnect()
                SaveRecordwthoutMSG("INSERT INTO patient_info (patient_id, name, sex, date_of_birth, age, civil_status, address, contact_no, `date`, `sample_id`) VALUES " _
                        & "(" _
                        & "@patient_id," _
                        & "@name," _
                        & "@sex," _
                        & "@bdate," _
                        & "@age," _
                        & "@CS," _
                        & "@address," _
                        & "@contact," _
                        & "@date," _
                        & "@MainSampleID" _
                        & ")"
                        )
            End If
            Disconnect()

            UpdateRecordwthoutMSG("UPDATE `tmpWorklist` SET " _
                    & "`sample_id` = @sample_id," _
                    & "`patient_id` = @patient_id," _
                    & "`patient_name` = @name," _
                    & "`sex` = @sex," _
                    & "`bdate` = @bdate," _
                    & "`age` = @age," _
                    & "`type` = @type," _
                    & "`physician` = @physician," _
                    & "`dept` = @room," _
                    & "`medtech` = @medtech," _
                    & "`verified_by` = @verify," _
                    & "`test` = @test," _
                    & "`patient_type` = @patient_type," _
                    & "`status` = @status," _
                    & "`dt_released` = @date_release," _
                    & "`main_id` = @MainSampleID" _
                    & " WHERE `main_id` = @mainID AND `testtype` = @Section"
                    )

            Connect2()
            rs2.Parameters.Clear()
            rs2.Parameters.AddWithValue("@Comment", txtExtracted.Text)
            rs2.Parameters.AddWithValue("@MainID", SampleID)
            rs2.Connection = conn2
            rs2.CommandType = CommandType.Text
            rs2.CommandText = "INSERT INTO `lab_comment` (`comment`,`sample_id`) VALUES (@Comment,@MainID)"
            rs2.ExecuteNonQuery()
            Disconnect2()

            Connect()
            rs.Connection = conn
            rs.CommandType = CommandType.Text
            rs.CommandText = "SELECT `sample_id` FROM `order_pathologist` WHERE `sample_id` = @MainSampleID"
            reader = rs.ExecuteReader
            reader.Read()
            If reader.HasRows Then
                Disconnect()
                UpdateRecordwthoutMSG("UPDATE `order_pathologist` SET `pathologist_id` = @pathologist WHERE `sample_id` = @MainSampleID")
            Else
                Disconnect()
                SaveRecordwthoutMSG("INSERT INTO `order_pathologist` (`sample_id`, `pathologist_id`) VALUES " _
                & "(" _
                & "@MainSampleID," _
                & "@pathologist" _
                & ")"
                )
            End If
            Disconnect()

            Connect()
            rs.Connection = conn
            rs.CommandType = CommandType.Text
            rs.CommandText = "SELECT `sample_id` FROM `order_medtech` WHERE `sample_id` = @MainSampleID"
            reader = rs.ExecuteReader
            reader.Read()
            If reader.HasRows Then
                Disconnect()
                UpdateRecordwthoutMSG("UPDATE `order_medtech` SET `medtech_id` = @medtechid WHERE `sample_id` = @MainSampleID")
            Else
                Disconnect()
                SaveRecordwthoutMSG("INSERT INTO `order_medtech` (`sample_id`, `medtech_id`) VALUES " _
                & "(" _
                & "@MainSampleID," _
                & "@medtechid" _
                & ")"
                )
            End If
            Disconnect()

            'Connect()
            'rs.Connection = conn
            'rs.CommandType = CommandType.Text
            'rs.CommandText = "SELECT `sample_id` FROM `order_Verified` WHERE `sample_id` = @MainSampleID"
            'reader = rs.ExecuteReader
            'reader.Read()
            'If reader.HasRows Then
            '    Disconnect()
            '    UpdateRecordwthoutMSG("UPDATE `order_Verified` SET `medtech_id` = @verifyid WHERE `sample_id` = @MainSampleID")
            'Else
            '    Disconnect()
            '    SaveRecordwthoutMSG("INSERT INTO `order_Verified` (`sample_id`, `medtech_id`) VALUES " _
            '    & "(" _
            '    & "@MainSampleID," _
            '    & "@verifyid" _
            '    & ")"
            '    )
            'End If
            'Disconnect()

            If Not txtAccession.Text = "" Or txtORNo.Text = "" Or txtChargeSlip.Text = "" Then
                Connect()
                rs.Connection = conn
                rs.CommandType = CommandType.Text
                rs.CommandText = "SELECT * FROM `additional_info` WHERE `sample_id` = @mainID AND section = @Section AND sub_section = @SubSection"
                reader = rs.ExecuteReader
                reader.Read()
                If reader.HasRows Then
                    Disconnect()
                    UpdateRecordwthoutMSG("UPDATE `additional_info` SET " _
                                & "`accession_no` = @accession_no," _
                                & "`or_no` = @OR_No," _
                                & "`cs_no` = @CS_No," _
                                & "`sample_id` = @MainSampleID" _
                                & " WHERE `sample_id` = @mainID AND section = @Section AND sub_section = @SubSection"
                                )
                Else
                    Disconnect()
                    SaveRecordwthoutMSG("INSERT INTO `additional_info` (`accession_no`, `or_no`, `cs_no`, section, sub_section, `sample_id`) VALUES " _
                                & "(" _
                                & "@accession_no," _
                                & "@OR_No," _
                                & "@CS_No," _
                                & "@Section," _
                                & "@SubSection," _
                                & "@MainSampleID" _
                                & ")"
                                )
                End If
                Disconnect()
            End If

            For x As Integer = 0 To GridView.RowCount - 1 Step 1
                If (GridView.IsRowSelected(x)) Then
                    UpdateRecordwthoutMSG("UPDATE `tmpresult` SET " _
                          & "`flag` = '" & GridView.GetRowCellValue(x, "Flag") & "'," _
                          & "`measurement` = '" & GridView.GetRowCellValue(x, "Result") & "'," _
                          & "`reference_range` = '" & GridView.GetRowCellValue(x, "ReferenceRange") & "'," _
                          & "`value_conv` = '" & GridView.GetRowCellValue(x, "Conventional") & "'," _
                          & "`ref_conv` = '" & GridView.GetRowCellValue(x, "RefRange") & "'," _
                          & "`patient_id` = @patient_id," _
                          & "`instrument` = '" & GridView.GetRowCellValue(x, "Instrument") & "'," _
                          & "`print_status` = '1'," _
                          & "`date` = @date_release," _
                          & "`sample_id` = @MainSampleID" _
                          & " WHERE `sample_id` = @mainID AND `test_code` = '" & GridView.GetRowCellValue(x, "TestCode") & "' AND `accepted_result` = '" & GridView.GetRowCellValue(x, "AcceptedResult") & "' AND `id` = '" & GridView.GetRowCellValue(x, "ID") & "'"
                          )
                Else
                    UpdateRecordwthoutMSG("UPDATE `tmpresult` SET " _
                          & "`flag` = '" & GridView.GetRowCellValue(x, "Flag") & "'," _
                          & "`measurement` = '" & GridView.GetRowCellValue(x, "Result") & "'," _
                          & "`reference_range` = '" & GridView.GetRowCellValue(x, "ReferenceRange") & "'," _
                          & "`value_conv` = '" & GridView.GetRowCellValue(x, "Conventional") & "'," _
                          & "`ref_conv` = '" & GridView.GetRowCellValue(x, "RefRange") & "'," _
                          & "`patient_id` = @patient_id," _
                          & "`instrument` = '" & GridView.GetRowCellValue(x, "Instrument") & "'," _
                          & "`print_status` = '0'," _
                          & "`date` = @date_release," _
                          & "`sample_id` = @MainSampleID" _
                          & " WHERE `sample_id` = @mainID AND `test_code` = '" & GridView.GetRowCellValue(x, "TestCode") & "' AND `accepted_result` = '" & GridView.GetRowCellValue(x, "AcceptedResult") & "' AND `id` = '" & GridView.GetRowCellValue(x, "ID") & "'"
                          )
                End If
            Next

            Connect()
            'rs.Parameters.Clear()
            rs.Parameters.AddWithValue("@Comment", txtExtracted.Text)
            'rs.Parameters.AddWithValue("@MainID", mainID)
            rs.Connection = conn
            rs.CommandType = CommandType.Text
            rs.CommandText = "SELECT `sample_id` FROM `lab_comment` WHERE `sample_id` = @mainID"
            reader = rs.ExecuteReader
            reader.Read()
            If reader.HasRows Then
                Disconnect()
                UpdateRecordwthoutMSG("UPDATE `lab_comment` SET `comment` = @Comment WHERE `sample_id` = @mainID")
            Else
                Disconnect()
                SaveRecordwthoutMSG("INSERT INTO `lab_comment` (`comment`, `sample_id`) VALUES (@Comment, @MainID)")
            End If
            Disconnect()

            Connect()
            rs.Connection = conn
            rs.CommandType = CommandType.Text
            rs.CommandText = "SELECT `sample_id` FROM `patient_remarks` WHERE `sample_id` = @mainID"
            reader = rs.ExecuteReader
            reader.Read()
            If reader.HasRows Then
                Disconnect()
                UpdateRecordwthoutMSG("UPDATE `patient_remarks` SET `remarks` = @remarks, `diagnosis` = @lab_comment WHERE `sample_id` = @mainID AND `section` = @Section AND `sub_section` = @SubSection")
            Else
                Disconnect()
                SaveRecordwthoutMSG("INSERT INTO `patient_remarks` (`remarks`, `sample_id`, `section`, `sub_section`) VALUES (@remarks, @MainSampleID, @Section, @SubSection)")
            End If
            Disconnect()

            UpdateRecordwthoutMSG("UPDATE `specimen_tracking` SET " _
                & "`sample_id` = @MainSampleID," _
                & "`printed` = @specimen_tracking_date_time" _
                & " WHERE sample_id = @mainID AND section = @Section"
                )

            Using myRDLCPrinter As New RDLCPrinterPrintNew(MainSampleID, Section, SubSection, "", My.Settings.DefaultPrinter)
                If My.Settings.SaveAsPDF Then
                    Dim byteViewer As Byte() = myRDLCPrinter.LocalReport.Render("PDF")
                    Dim saveFileDialog1 As New SaveFileDialog()
                    saveFileDialog1.Filter = "*PDF files (*.pdf)|*.pdf"
                    saveFileDialog1.FilterIndex = 2
                    saveFileDialog1.RestoreDirectory = True
                    Dim newFile As New FileStream(My.Settings.PDFLocation & txtSampleID.Text & "_" & txtName.Text & ".pdf", FileMode.Create)
                    newFile.Write(byteViewer, 0, byteViewer.Length)
                    newFile.Close()

                    myRDLCPrinter.Print(1)
                Else
                    myRDLCPrinter.Print(1)
                End If
            End Using

            Connect()
            rs.Connection = conn
            rs.CommandType = CommandType.Text
            rs.CommandText = "INSERT INTO `order` (`status`, `sample_id`, `patient_id`, `patient_name`, `age`, `sex`, `bdate`, `physician`, `dept`, `medtech`, `verified_by`, `date`, `time`, `main_id`, `dt_released`, `patient_type`, `test`, `testtype`, `barcode`, `instrument`, `type`, `location`, `sub_section`) " _
                & "SELECT `status`, `sample_id`, `patient_id`, `patient_name`, `age`, `sex`, `bdate`, `physician`, `dept`, `medtech`, `verified_by`, `date`, `time`, @MainSampleID, `dt_released`, `patient_type`, `test`, `testtype`, `barcode`, `instrument`, `type`, `location`, `sub_section` FROM `tmpWorklist` WHERE `main_id` = @MainSampleID AND testtype = @Section"
            rs.ExecuteNonQuery()
            Disconnect()

            Connect()
            rs.Connection = conn
            rs.CommandType = CommandType.Text
            rs.CommandText = "INSERT INTO `result` (`universal_id`, `flag`, `measurement`, `units`, `reference_range`, `value_conv`, `unit_conv`, `ref_conv`, `test_code`, `order_no`, `sample_id`, `test_group`, `date`, `patient_id`, `instrument`, `his_code`, `his_mainid`, `section`, `sub_section`, `print_status`, `accepted_result`, `status`) " _
                & "SELECT `universal_id`, `flag`, `measurement`, `units`, `reference_range`, `value_conv`, `unit_conv`, `ref_conv`, `test_code`, `order_no`, @MainSampleID, `test_group`, `date`, `patient_id`, `instrument`, `his_code`, `his_mainid`, `section`, `sub_section`, `print_status`, `accepted_result`, `status` FROM `tmpResult` WHERE `sample_id` = @MainSampleID AND section = @Section"
            rs.ExecuteNonQuery()
            Disconnect()

            Connect()
            rs.Connection = conn
            rs.CommandType = CommandType.Text
            rs.CommandText = "DELETE FROM `tmpWorklist` WHERE `main_id` = @MainSampleID AND testtype = @Section"
            rs.ExecuteNonQuery()
            Disconnect()

            Connect()
            rs.Connection = conn
            rs.CommandType = CommandType.Text
            rs.CommandText = "DELETE FROM `tmpResult` WHERE `sample_id` = @MainSampleID AND section = @Section"
            rs.ExecuteNonQuery()
            Disconnect()

            'UpdateWorkSheet()
            'version 1.6.0.0-beta
            'Save the Processed, Validated and Printed Time on 'specimen_tracking' table

            With My.Settings
                If .HL7Write = True Then
                    CreateHL7File()
                End If
            End With

            With My.Settings
                If .SQLConnection = True Then
                    UpdateResultiHOMIS()
                End If
            End With

            frmHemaWorklist.LoadRecords()
            frmHemaWorklist.LoadRecordsCompleted()

            ActivityLogs(txtSampleID.Text,
                             txtPatientID.Text,
                              txtName.Text,
                             CurrUser,
                             "Print Release Result",
                             cboRequest.Text,
                             Section,
                             SubSection)

            Me.Close()
            Me.Dispose()
        Catch ex As Exception
            Disconnect()
            MessageBox.Show(ex.Message, "Mysql Exception", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End Try
    End Sub

    Public Sub VerifyResult()
        'Try
        'If Me.txtName.Text = "" Or cboMedTech.Text = "" Or txtAge.Text = "" Or cboSex.Text = "" Then
        If Me.txtName.Text = "" Or txtAge.Text = "" Or cboSex.Text = "" Then
                If Me.txtName.Text = "" Then
                    MessageBox.Show("Please Fill Up Patient Name First.", "Patient Name is Empty", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                ElseIf txtAge.Text = "" Then
                    MessageBox.Show("Please Fill Up Patient Age First.", "Patient Age is Empty", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                ElseIf cboSex.Text = "" Then
                    MessageBox.Show("Please Fill Up Patient Name First.", "Patient Sex is Empty", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

                    'ElseIf Me.cboMedTech.Text = "" Then
                    '    MessageBox.Show("Please Fill Up MedTech Field Signatory First.", "MedTech Field Signatory is Empty", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

                Else
                    MessageBox.Show("Please verify the data carefully.", "Verification Failed", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                End If
                Exit Sub
            End If

            With My.Settings
                If .LISType = "Lite" Then
                    MainSampleID = ID_Randomizer()
                Else
                    MainSampleID = mainID
                End If
            End With

            rs.Parameters.Clear()
            rs.Parameters.AddWithValue("@mainID", mainID)
            rs.Parameters.AddWithValue("@MainSampleID", MainSampleID)

            rs.Parameters.AddWithValue("@medtech", cboMedTech.Text)
            rs.Parameters.AddWithValue("@medtechid", MedTechID)

            'If My.Settings.AuthenticateRelease = True Then
            '    rs.Parameters.AddWithValue("@verifyid", UserIDRelease)
            '    rs.Parameters.AddWithValue("@verify", UserRelease)
            'Else
            rs.Parameters.AddWithValue("@verifyid", VerifyID)
            rs.Parameters.AddWithValue("@verify", cboVerify.Text)
            'End If

            rs.Parameters.AddWithValue("@pathologist", PathologistID)

            rs.Parameters.AddWithValue("@remarks", txtRemarks.Text)
            rs.Parameters.AddWithValue("@lab_comment", txtComment.Text)

            'rs.Parameters.AddWithValue("@status", "Processing")

            rs.Parameters.AddWithValue("@id", SampleID)
            rs.Parameters.AddWithValue("@sample_id", txtSampleID.Text)
            rs.Parameters.AddWithValue("@patient_id", txtPatientID.Text)
            rs.Parameters.AddWithValue("@name", txtName.Text)
            rs.Parameters.AddWithValue("@age", txtAge.Text)
            rs.Parameters.AddWithValue("@type", txtClass.Text)
            rs.Parameters.AddWithValue("@bdate", dtBDate.Text)
            rs.Parameters.AddWithValue("@physician", cboPhysician.Text)
            rs.Parameters.AddWithValue("@room", cboRoom.Text)
            rs.Parameters.AddWithValue("@sex", cboSex.Text)
            rs.Parameters.AddWithValue("@CS", cboCS.Text)
            rs.Parameters.AddWithValue("@address", txtAddress.Text)
            rs.Parameters.AddWithValue("@contact", txtContact.Text)
            rs.Parameters.AddWithValue("@test", cboRequest.Text)
            rs.Parameters.AddWithValue("@patient_type", cboPatientType.Text)
            rs.Parameters.Add("@date", MySql.Data.MySqlClient.MySqlDbType.Date).Value = Format(dtReceived.Value, "yyyy-MM-dd")
            rs.Parameters.AddWithValue("@time", tmTimeReceived.Value.ToLongTimeString)
            'rs.Parameters.AddWithValue("@date_release", Format(Now, "yyyy-MM-dd") & " " & Now.ToLongTimeString)
            rs.Parameters.AddWithValue("@accession_no", txtAccession.Text)
            rs.Parameters.AddWithValue("@OR_No", txtORNo.Text)
            rs.Parameters.AddWithValue("@CS_No", txtChargeSlip.Text)

            rs.Parameters.AddWithValue("@Section", Section)
            rs.Parameters.AddWithValue("@SubSection", SubSection)
            Connect()
            rs.Connection = conn
            rs.CommandType = CommandType.Text
            rs.CommandText = "SELECT * FROM patient_info WHERE `patient_id` = @PATIENT_ID"
            reader = rs.ExecuteReader
            reader.Read()
            If reader.HasRows Then
                Disconnect()
                SaveRecordwthoutMSG("UPDATE `patient_info` SET " _
                        & "`patient_id` = @patient_id," _
                        & "`name` = @name," _
                        & "`sex` = @sex," _
                        & "`date_of_birth` = @bdate," _
                        & "`age` = @age," _
                        & "`civil_status` = @CS," _
                        & "`address` = @address," _
                        & "`contact_no` = @contact," _
                        & "`sample_id` = @MainSampleID" _
                        & " WHERE `patient_id` = @patient_id"
                        )
            Else
                Disconnect()
                SaveRecordwthoutMSG("INSERT INTO patient_info (patient_id, name, sex, date_of_birth, age, civil_status, address, contact_no, `date`, `sample_id`) VALUES " _
                        & "(" _
                        & "@patient_id," _
                        & "@name," _
                        & "@sex," _
                        & "@bdate," _
                        & "@age," _
                        & "@CS," _
                        & "@address," _
                        & "@contact," _
                        & "@date," _
                        & "@MainSampleID" _
                        & ")"
                        )
            End If
            Disconnect()

            UpdateRecordwthoutMSG("UPDATE `tmpWorklist` SET " _
                    & "`sample_id` = @sample_id," _
                    & "`patient_id` = @patient_id," _
                    & "`patient_name` = @name," _
                    & "`sex` = @sex," _
                    & "`bdate` = @bdate," _
                    & "`age` = @age," _
                    & "`type` = @type," _
                    & "`physician` = @physician," _
                    & "`dept` = @room," _
                    & "`medtech` = @medtech," _
                    & "`verified_by` = @verify," _
                    & "`test` = @test," _
                    & "`patient_type` = @patient_type," _
                    & "`date` = @date," _
                    & "`time` = @time," _
                    & "`main_id` = @MainSampleID" _
                    & " WHERE main_id = @mainID AND testtype = @Section AND sub_section = @SubSection"
                    )
        'MessageBox.Show("Trace 1")
        'SaveRecordwthoutMSG("INSERT INTO `order_pathologist` (`sample_id`, `pathologist_id`) VALUES " _
        '& "(" _
        '& "@MainSampleID," _
        '& "@pathologist" _
        '& ")"
        ')

        'Connect2()
        'rs2.Parameters.Clear()
        'rs2.Parameters.AddWithValue("@Comment", txtExtracted.Text)
        'rs2.Parameters.AddWithValue("@MainID", txtSampleID.Text)
        'rs2.Connection = conn2
        'rs2.CommandType = CommandType.Text
        'rs2.CommandText = "INSERT INTO `lab_comment` (`comment`,`sample_id`) VALUES (@Comment,@MainID)"
        'rs2.ExecuteNonQuery()
        'Disconnect2()

        'Connect()

        'rs.Connection = conn
        'rs.CommandType = CommandType.Text
        'rs.CommandText = "SELECT `sample_id` FROM `lab_comment` WHERE `sample_id` = '" & txtSampleID.Text & "' "
        'reader = rs.ExecuteReader
        'reader.Read()
        'If reader.HasRows Then
        '    Disconnect()
        '    UpdateRecordwthoutMSG("UPDATE `lab_comment` SET `comment` = '" & txtExtracted.Text & "' WHERE `sample_id` = '" & txtSampleID.Text & "' ")
        'Else
        '    Disconnect()
        '    'SaveRecordwthoutMSG("INSERT INTO `lab_comment` (`comment`,`sample_id`) VALUES (@Comment,@MainID)")
        '    SaveRecordwthoutMSG("INSERT INTO `lab_comment` (`comment`, `sample_id`) VALUES " _
        '    & "(" _
        '    & "@Comment," _
        '    & "@MainID" _
        '    & ")"
        '    )
        'End If
        'Disconnect()

        'MessageBox.Show("Trace 2")

        Connect()
            rs.Connection = conn
            rs.CommandType = CommandType.Text
            rs.CommandText = "SELECT `sample_id` FROM `order_pathologist` WHERE `sample_id` = '" & txtSampleID.Text & "'"
            reader = rs.ExecuteReader
            reader.Read()
            If reader.HasRows Then
                Disconnect()
                UpdateRecordwthoutMSG("UPDATE `order_pathologist` SET `pathologist_id` = '" & PathologistID & "' WHERE `sample_id` = '" & txtSampleID.Text & "'")
                'MessageBox.Show("Trace 2b")
            Else
                Disconnect()
                SaveRecordwthoutMSG("INSERT INTO `order_pathologist` (`sample_id`, `pathologist_id`) VALUES " _
                & "(" _
                & " '" & txtSampleID.Text & "'," _
                & "'" & PathologistID & "'" _
                & ")"
                )
                'MessageBox.Show("Trace 2c")
            End If
            Disconnect()
            'MessageBox.Show("Trace 3")

            'SaveRecordwthoutMSG("INSERT INTO `order_medtech` (`sample_id`, `medtech_id`) VALUES " _
            '& "(" _
            '& "@MainSampleID," _
            '& "@medtechid" _
            '& ")"
            ')
            Connect()
            rs.Connection = conn
            rs.CommandType = CommandType.Text
            rs.CommandText = "SELECT `sample_id` FROM `order_medtech` WHERE `sample_id` = '" & txtSampleID.Text & "'"
            reader = rs.ExecuteReader
            reader.Read()
            If reader.HasRows Then
                Disconnect()
                UpdateRecordwthoutMSG("UPDATE `order_medtech` SET `medtech_id` = '" & MedTechID & "' WHERE `sample_id` = '" & txtSampleID.Text & "'")
            Else
                Disconnect()
                SaveRecordwthoutMSG("INSERT INTO `order_medtech` (`sample_id`, `medtech_id`) VALUES " _
                & "(" _
                & "'" & txtSampleID.Text & "'," _
                & "'" & MedTechID & "'" _
                & ")"
                )
            End If
            Disconnect()
            'MessageBox.Show("Trace 4")

            'SaveRecordwthoutMSG("INSERT INTO `order_Verified` (`sample_id`, `medtech_id`) VALUES " _
            '& "(" _
            '& "@MainSampleID," _
            '& "@verifyid" _
            '& ")"
            ')

            Connect()
            rs.Connection = conn
            rs.CommandType = CommandType.Text
            rs.CommandText = "SELECT `sample_id` FROM `order_Verified` WHERE `sample_id` = @MainSampleID"
            reader = rs.ExecuteReader
            reader.Read()
            If reader.HasRows Then
                Disconnect()
                UpdateRecordwthoutMSG("UPDATE `order_Verified` SET `medtech_id` = @verifyid WHERE `sample_id` = @MainSampleID")
            Else
                Disconnect()
                SaveRecordwthoutMSG("INSERT INTO `order_Verified` (`sample_id`, `medtech_id`) VALUES " _
                & "(" _
                & "@MainSampleID," _
                & "@verifyid" _
                & ")"
                )
            End If
            Disconnect()
            'MessageBox.Show("Trace 5")
            If Not txtAccession.Text = "" Or txtORNo.Text = "" Or txtChargeSlip.Text = "" Then
                Connect()
                rs.Connection = conn
                rs.CommandType = CommandType.Text
                rs.CommandText = "SELECT * FROM `additional_info` WHERE `sample_id` = @mainID AND section = @Section AND sub_section = @SubSection"
                reader = rs.ExecuteReader
                reader.Read()
                If reader.HasRows Then
                    Disconnect()
                    UpdateRecordwthoutMSG("UPDATE `additional_info` SET " _
                                & "`accession_no` = @accession_no," _
                                & "`or_no` = @OR_No," _
                                & "`cs_no` = @CS_No," _
                                & "`sample_id` = @MainSampleID" _
                                & " WHERE `sample_id` = @mainID AND section = @Section AND sub_section = @SubSection"
                                )
                Else
                    Disconnect()
                    SaveRecordwthoutMSG("INSERT INTO `additional_info` (`accession_no`, `or_no`, `cs_no`, section, sub_section, `sample_id`) VALUES " _
                                & "(" _
                                & "@accession_no," _
                                & "@OR_No," _
                                & "@CS_No," _
                                & "@Section," _
                                & "@SubSection," _
                                & "@MainSampleID" _
                                & ")"
                                )
                End If
                Disconnect()
            End If
            'MessageBox.Show("Trace 6")
            For x As Integer = 0 To GridView.RowCount - 1 Step 1
                If (GridView.IsRowSelected(x)) Then
                    UpdateRecordwthoutMSG("UPDATE `tmpresult` SET " _
                          & "`flag` = '" & GridView.GetRowCellValue(x, "Flag") & "'," _
                          & "`measurement` = '" & GridView.GetRowCellValue(x, "Result") & "'," _
                          & "`reference_range` = '" & GridView.GetRowCellValue(x, "ReferenceRange") & "'," _
                          & "`value_conv` = '" & GridView.GetRowCellValue(x, "Conventional") & "'," _
                          & "`ref_conv` = '" & GridView.GetRowCellValue(x, "RefRange") & "'," _
                          & "`patient_id` = @patient_id," _
                          & "`instrument` = '" & GridView.GetRowCellValue(x, "Instrument") & "'," _
                          & "`print_status` = '1'," _
                          & "`sample_id` = @MainSampleID" _
                          & " WHERE `sample_id` = @mainID AND `test_code` = '" & GridView.GetRowCellValue(x, "TestCode") & "' AND `accepted_result` = '" & GridView.GetRowCellValue(x, "AcceptedResult") & "' AND `id` = '" & GridView.GetRowCellValue(x, "ID") & "'"
                          )
                Else
                    UpdateRecordwthoutMSG("UPDATE `tmpresult` SET " _
                          & "`flag` = '" & GridView.GetRowCellValue(x, "Flag") & "'," _
                          & "`measurement` = '" & GridView.GetRowCellValue(x, "Result") & "'," _
                          & "`reference_range` = '" & GridView.GetRowCellValue(x, "ReferenceRange") & "'," _
                          & "`value_conv` = '" & GridView.GetRowCellValue(x, "Conventional") & "'," _
                          & "`ref_conv` = '" & GridView.GetRowCellValue(x, "RefRange") & "'," _
                          & "`patient_id` = @patient_id," _
                          & "`instrument` = '" & GridView.GetRowCellValue(x, "Instrument") & "'," _
                          & "`print_status` = '0'," _
                          & "`sample_id` = @MainSampleID" _
                          & " WHERE `sample_id` = @mainID AND `test_code` = '" & GridView.GetRowCellValue(x, "TestCode") & "' AND `accepted_result` = '" & GridView.GetRowCellValue(x, "AcceptedResult") & "' AND `id` = '" & GridView.GetRowCellValue(x, "ID") & "'"
                          )
                End If
            Next
        'MessageBox.Show("Trace 7")

        Connect()
        'rs.Parameters.Clear()
        rs.Parameters.AddWithValue("@Comment", txtExtracted.Text)
        'rs.Parameters.AddWithValue("@MainID", mainID)
        rs.Connection = conn
        rs.CommandType = CommandType.Text
        rs.CommandText = "SELECT `sample_id` FROM `lab_comment` WHERE `sample_id` = @mainID"
        reader = rs.ExecuteReader
        reader.Read()
        If reader.HasRows Then
            Disconnect()
            UpdateRecordwthoutMSG("UPDATE `lab_comment` SET `comment` = @Comment WHERE `sample_id` = @mainID")
        Else
            Disconnect()
            SaveRecordwthoutMSG("INSERT INTO `lab_comment` (`comment`, `sample_id`) VALUES (@Comment, @MainID)")
        End If
        Disconnect()

        Connect()
            rs.Connection = conn
            rs.CommandType = CommandType.Text
            rs.CommandText = "SELECT `sample_id` FROM `patient_remarks` WHERE `sample_id` = @mainID"
            reader = rs.ExecuteReader
            reader.Read()
            If reader.HasRows Then
                Disconnect()
                UpdateRecordwthoutMSG("UPDATE `patient_remarks` SET `remarks` = @remarks, `diagnosis` = @lab_comment WHERE `sample_id` = @mainID AND `section` = @Section AND `sub_section` = @SubSection")
            Else
                Disconnect()
                SaveRecordwthoutMSG("INSERT INTO `patient_remarks` (`remarks`, `sample_id`, `section`, `sub_section`) VALUES (@remarks, @MainSampleID, @Section, @SubSection)")
            End If
            Disconnect()
            'MessageBox.Show("Trace 8")
            'btnPrint.Enabled = False
            'btnPrintNow.Enabled = True

            mainID = MainSampleID

            frmHemaWorklist.LoadRecords()


            MessageBox.Show("Information has been saved", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information)

            'Dim myform As New frmHemaWorklist.DisplayResult()
            'Me.Close()
            'myform.WindowState = FormWindowState.Maximized
            'myform.MdiParent = frmHemaWorklist
            'myform.Show()
            LoadTest()

            ActivityLogs(txtSampleID.Text,
                             txtPatientID.Text,
                              txtName.Text,
                             CurrUser,
                             "Update/Saved Patient Info",
                             cboRequest.Text,
                             Section,
                             SubSection)

        'Catch ex As Exception
        '    Disconnect()
        '    MessageBox.Show(ex.Message, "Mysql Exception", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        '    Exit Sub
        'End Try
    End Sub
#End Region

#Region "HIS Integration Methods"
    'Method for Creating HL7 File

    Private his_test_code As String
    Public Function getHIS_Test(ByVal accesion_no As String, or_no As String, ByVal cs_no As String)
        Dim additional_info_id As String
        Dim his_test As String
        Dim test_string As String

        rs.Parameters.Clear()
        rs.Parameters.AddWithValue("@accession_no", accesion_no)
        rs.Parameters.AddWithValue("@or_no", or_no)
        rs.Parameters.AddWithValue("@ChargeSlip", cs_no)
        rs.Parameters.AddWithValue("@section", "Hematology")
        rs.Parameters.AddWithValue("@sub_section", cboRequest.Text)

        Disconnect()
        Connect()
        rs.Connection = conn
        rs.CommandText = "SELECT id FROM additional_info WHERE accession_no = @accession_no AND or_no = @or_no AND cs_no = @ChargeSlip AND section = @section AND sub_section = @sub_section"
        reader = rs.ExecuteReader
        If reader.HasRows Then
            While reader.Read
                test_string = ""
                additional_info_id = reader(0).ToString
                Connect1()
                rs1.Connection = conn1
                rs1.CommandText = "SELECT his_test_code FROM hl7_result_his_code WHERE additional_info_id = '" & additional_info_id & "'"
                reader1 = rs1.ExecuteReader
                If reader1.HasRows Then
                    While reader1.Read
                        his_test = reader1(0).ToString + "^~"
                        test_string &= his_test
                    End While
                End If
                Disconnect1()
            End While
        End If
        Disconnect()

        Return test_string
    End Function

    Public Sub CreateHL7File()
        'Check If Date of Birth Is nothing then clear text formating
        If dtBDate.Text = "" Then
            'Clear text formating
            PatientBDate_Out = ""
            '--------------------
        Else
            'Create a format for date of birth (yyyyMMdd)
            Dim dDate As DateTime = DateTime.ParseExact(dtBDate.Text, "MM/dd/yyyy", CultureInfo.InvariantCulture)
            Dim reformatted As String = dDate.ToString("yyyyMMdd", CultureInfo.InvariantCulture)
            PatientBDate_Out = reformatted
            '--------------------------------------------
        End If
        '-----------------------------------------------------------
        Dim a As Integer = 0

        Dim HL7Log As FileStream
        Dim HL7Writer As StreamWriter
        Dim HL7File As String = "SBSILIS_" & txtSampleID.Text.Trim & ".HL7"

        HL7Log = New FileStream(My.Settings.HL7Destination & "\" & HL7File, FileMode.Create, FileAccess.Write)
        HL7Writer = New StreamWriter(HL7Log)

        his_test_code = getHIS_Test(txtAccession.Text, txtORNo.Text, txtChargeSlip.Text)

        HL7Writer.WriteLine("MSH|^~\&|BIZBOX|HOSPITAL|BIOTECH|HOSPITAL|" & FormatDateRegex() & "||ORU^RO1|1|P|2.3|||")
        HL7Writer.WriteLine("PID|1||" & txtPatientID.Text & "||" & Replace(txtName.Text, ", ", "^") & "||" & PatientBDate_Out & "|" & cboSex.Text.Substring(0, 1))
        HL7Writer.WriteLine("PV1|1|" & cboPatientType.Text.Substring(0, 1))
        HL7Writer.WriteLine("ORC|1|" & txtAccession.Text & "|||||||" & FormatDateRegex())
        'HL7Writer.WriteLine("OBR|1|" & txtAccession.Text & "||^|||" & FormatDateRegex())

        For x As Integer = 0 To his_test_code.Split("~").Count - 1 Step 1
            HL7Writer.WriteLine("OBR|1|" & txtAccession.Text & "||" & his_test_code.Split("~").GetValue(x).ToString & "|||" & FormatDateRegex())
        Next
        For x As Integer = 0 To GridView.RowCount - 1 Step 1
            'HL7Writer.WriteLine("OBX|" & x + 1 & "|NM|" & GridView.GetRowCellValue(x, "TestCode") & "||" & GridView.GetRowCellValue(x, "SI") & "|" & GridView.GetRowCellValue(x, "Unit") & "|" & GridView.GetRowCellValue(x, "ReferenceRange") & "|N|||F|||" & FormatDateRegex() & "|")
            HL7Writer.WriteLine("OBX|" & x + 1 & "|NM|" & GridView.GetRowCellValue(x, "TestCode") & "||" & GridView.GetRowCellValue(x, "Conventional") & "|||N|||F|||" & FormatDateRegex() & "|")
            a = x + 1
        Next
        'Remarks Section
        HL7Writer.WriteLine("OBX|" & a + 1 & "|NM|Remarks_H||" & txtRemarks.Text & "|||N|||F|||" & FormatDateRegex() & "|")
        HL7Writer.WriteLine("OBX|" & a + 2 & "|NM|Others_H||" & txtComment.Text & "|||N|||F|||" & FormatDateRegex() & "|")
        HL7Writer.Close()
        HL7Log.Close()
    End Sub
    'End Of HL7

    'Function for update result for iHOMIS
    Public Sub UpdateResultiHOMIS()
        Try

            If Not GridView.GetRowCellValue(0, GridView.Columns("HISMainID")).ToString = "" Then
                If GridView.RowCount = 1 Then
                    ConnectSQL()
                    rsSQL.Parameters.Clear()
                    rsSQL.Parameters.AddWithValue("@docointkey", GridView.GetRowCellValue(0, GridView.Columns("HISMainID")))
                    rsSQL.Connection = connSQL
                    rsSQL.CommandType = CommandType.Text
                    rsSQL.CommandText = "UPDATE hbldrslt SET " & GridView.GetRowCellValue(0, GridView.Columns("HISTestCode")) & " = '" & GridView.GetRowCellValue(0, GridView.Columns("Result")) & "'" _
                                & " WHERE docointkey = @docointkey"
                    rsSQL.ExecuteNonQuery()
                    DisconnectSQL()
                Else
                    For x As Integer = 0 To GridView.RowCount - 1 Step 1

                        ConnectSQL()
                        rsSQL.Parameters.Clear()
                        rsSQL.Parameters.AddWithValue("@docointkey", GridView.GetRowCellValue(x, GridView.Columns("HISMainID")))
                        rsSQL.Connection = connSQL
                        rsSQL.CommandType = CommandType.Text
                        rsSQL.CommandText = "UPDATE hbldrslt SET " & GridView.GetRowCellValue(x, GridView.Columns("HISTestCode")) & " = '" & GridView.GetRowCellValue(x, GridView.Columns("Result")) & "'" _
                                    & " WHERE docointkey = @docointkey"
                        rsSQL.ExecuteNonQuery()
                        DisconnectSQL()
                    Next
                End If
            Else
                Exit Sub
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub GridView_SelectionChanged(sender As Object, e As DevExpress.Data.SelectionChangedEventArgs) Handles GridView.SelectionChanged
        DiffCount()
    End Sub

    Private Sub btnVerify_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnVerify.ItemClick
        LoadTest()
        Dim getStatus As String = ""

        'Connect()
        'rs.Connection = conn
        'rs.CommandType = CommandType.Text
        'rs.CommandText = "SELECT `usertype` FROM `user_account` WHERE `email` = '" & CurrEmail & "'"
        'reader = rs.ExecuteReader
        'reader.Read()
        'If reader.HasRows Then
        If CurrType.Contains("Doctor") = True Then
            getStatus = "Verified"
            Disconnect()
            'MessageBox.Show(getStatus)
        ElseIf CurrType.Contains("Administrator") = True Then
            If status = "Processing" Then
                getStatus = "For Verification"
                Disconnect()
            ElseIf status = "Result Received" Then
                getStatus = "For Verification"
                Disconnect()
                'MessageBox.Show(getStatus)
            ElseIf status = "For Verification" Then
                getStatus = "Verified"
                Disconnect()
                'MessageBox.Show(getStatus)
            End If
        ElseIf CurrType.Contains("Medical Technol") = True Then
            getStatus = "For Verification"
            Disconnect()
        End If
        'End If
        Disconnect()

        Try
            'If Me.txtName.Text = "" Or cboMedTech.Text = "" Or txtAge.Text = "" Or cboSex.Text = "" Then
            If Me.txtName.Text = "" Or txtAge.Text = "" Or cboSex.Text = "" Then
                If Me.txtName.Text = "" Then
                    MessageBox.Show("Please Fill Up Patient Name First.", "Patient Name is Empty", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                ElseIf txtAge.Text = "" Then
                    MessageBox.Show("Please Fill Up Patient Age First.", "Patient Age is Empty", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                ElseIf cboSex.Text = "" Then
                    MessageBox.Show("Please Fill Up Patient Name First.", "Patient Sex is Empty", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

                    'ElseIf Me.cboMedTech.Text = "" Then
                    '    MessageBox.Show("Please Fill Up MedTech Field Signatory First.", "MedTech Field Signatory is Empty", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

                Else
                    MessageBox.Show("Please verify the data carefully.", "Verification Failed", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                End If
                Exit Sub
            End If

            'Dim countEmpty2 As Integer = 0
            'Dim countEmpty3 As Integer = 0

            'For x As Integer = 0 To GridView.RowCount - 1 Step 1
            '    'MessageBox.Show(GridView.GetRowCellValue(x, "PrintStatus"))
            '    If GridView.GetRowCellValue(x, "PrintStatus") = 1 Then
            '        countEmpty2 = countEmpty2 + 1
            '        MessageBox.Show(countEmpty2)
            '    End If

            'Next

            'If countEmpty2 > 0 Then
            '    MessageBox.Show("Result(s) must have value To verify", "Empty Result(s)", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            '    Exit Sub

            'End If

            'For x As Integer = 0 To GridView.RowCount - 1 Step 1
            '    MessageBox.Show(countEmpty3)
            '    If GridView.GetRowCellValue(x, "PrintStatus") = 1 Then
            '        countEmpty3 = countEmpty3 + 1
            '    End If

            'Next

            'If countEmpty3 = 0 Then
            '    If MessageBox.Show("Select at least 1 Result To verify", "Empty Result(s)", MessageBoxButtons.OK, MessageBoxIcon.Warning) Then
            '        Exit Sub
            '    End If
            'End If

            With My.Settings
                If .LISType = "Lite" Then
                    MainSampleID = ID_Randomizer()
                Else
                    MainSampleID = mainID
                End If
            End With

            rs.Parameters.Clear()
            rs.Parameters.AddWithValue("@mainID", mainID)
            rs.Parameters.AddWithValue("@MainSampleID", MainSampleID)

            rs.Parameters.AddWithValue("@medtech", cboMedTech.Text)
            rs.Parameters.AddWithValue("@medtechid", MedTechID)

            'If My.Settings.AuthenticateRelease = True Then
            '    rs.Parameters.AddWithValue("@verifyid", UserIDRelease)
            '    rs.Parameters.AddWithValue("@verify", UserRelease)
            'Else
            rs.Parameters.AddWithValue("@verifyid", VerifyID)
            rs.Parameters.AddWithValue("@verify", cboVerify.Text)
            'End If

            rs.Parameters.AddWithValue("@pathologist", PathologistID)

            rs.Parameters.AddWithValue("@remarks", txtRemarks.Text)
            rs.Parameters.AddWithValue("@lab_comment", txtComment.Text)

            rs.Parameters.AddWithValue("@status", getStatus)

            rs.Parameters.AddWithValue("@id", SampleID)
            rs.Parameters.AddWithValue("@sample_id", txtSampleID.Text)
            rs.Parameters.AddWithValue("@patient_id", txtPatientID.Text)
            rs.Parameters.AddWithValue("@name", txtName.Text)
            rs.Parameters.AddWithValue("@age", txtAge.Text)
            rs.Parameters.AddWithValue("@type", txtClass.Text)
            rs.Parameters.AddWithValue("@bdate", dtBDate.Text)
            rs.Parameters.AddWithValue("@physician", cboPhysician.Text)
            rs.Parameters.AddWithValue("@room", cboRoom.Text)
            rs.Parameters.AddWithValue("@sex", cboSex.Text)
            rs.Parameters.AddWithValue("@CS", cboCS.Text)
            rs.Parameters.AddWithValue("@address", txtAddress.Text)
            rs.Parameters.AddWithValue("@contact", txtContact.Text)
            rs.Parameters.AddWithValue("@test", cboRequest.Text)
            rs.Parameters.AddWithValue("@patient_type", cboPatientType.Text)
            rs.Parameters.Add("@Date", MySql.Data.MySqlClient.MySqlDbType.Date).Value = Format(dtReceived.Value, "yyyy-MM-dd")
            rs.Parameters.AddWithValue("@time", tmTimeReceived.Value.ToLongTimeString)
            'rs.Parameters.AddWithValue("@date_release", Format(Now, "yyyy-MM-dd") & " " & Now.ToLongTimeString)
            rs.Parameters.AddWithValue("@accession_no", txtAccession.Text)
            rs.Parameters.AddWithValue("@OR_No", txtORNo.Text)
            rs.Parameters.AddWithValue("@CS_No", txtChargeSlip.Text)

            rs.Parameters.AddWithValue("@specimen_tracking_date_time", Now)

            rs.Parameters.AddWithValue("@Section", Section)
            rs.Parameters.AddWithValue("@SubSection", SubSection)
            Connect()
            rs.Connection = conn
            rs.CommandType = CommandType.Text
            rs.CommandText = "Select * FROM patient_info WHERE `patient_id` = @PATIENT_ID"
            reader = rs.ExecuteReader
            reader.Read()
            If reader.HasRows Then
                Disconnect()
                SaveRecordwthoutMSG("UPDATE `patient_info` SET " _
                        & "`patient_id` = @patient_id," _
                        & "`name` = @name," _
                        & "`sex` = @sex," _
                        & "`date_of_birth` = @bdate," _
                        & "`age` = @age," _
                        & "`civil_status` = @CS," _
                        & "`address` = @address," _
                        & "`contact_no` = @contact," _
                        & "`sample_id` = @MainSampleID" _
                        & " WHERE `patient_id` = @patient_id"
                        )
            Else
                Disconnect()
                SaveRecordwthoutMSG("INSERT INTO patient_info (patient_id, name, sex, date_of_birth, age, civil_status, address, contact_no, `date`, `sample_id`) VALUES " _
                        & "(" _
                        & "@patient_id," _
                        & "@name," _
                        & "@sex," _
                        & "@bdate," _
                        & "@age," _
                        & "@CS," _
                        & "@address," _
                        & "@contact," _
                        & "@date," _
                        & "@MainSampleID" _
                        & ")"
                        )
            End If
            Disconnect()

            UpdateRecordwthoutMSG("UPDATE `tmpWorklist` SET " _
                    & "`status` = @status," _
                    & "`sample_id` = @sample_id," _
                    & "`patient_id` = @patient_id," _
                    & "`patient_name` = @name," _
                    & "`sex` = @sex," _
                    & "`bdate` = @bdate," _
                    & "`age` = @age," _
                    & "`type` = @type," _
                    & "`physician` = @physician," _
                    & "`dept` = @room," _
                    & "`medtech` = @medtech," _
                    & "`verified_by` = @verify," _
                    & "`test` = @test," _
                    & "`patient_type` = @patient_type," _
                    & "`date` = @date," _
                    & "`time` = @time," _
                    & "`main_id` = @MainSampleID" _
                    & " WHERE main_id = @mainID AND testtype = @Section AND sub_section = @SubSection"
                    )

            'SaveRecordwthoutMSG("INSERT INTO `order_pathologist` (`sample_id`, `pathologist_id`) VALUES " _
            '& "(" _
            '& "@MainSampleID," _
            '& "@pathologist" _
            '& ")"
            ')

            Connect2()
            rs2.Parameters.Clear()
            rs2.Parameters.AddWithValue("@Comment", txtExtracted.Text)
            rs2.Parameters.AddWithValue("@MainID", SampleID)
            rs2.Connection = conn2
            rs2.CommandType = CommandType.Text
            rs2.CommandText = "INSERT INTO `lab_comment` (`comment`,`sample_id`) VALUES (@Comment,@MainID)"
            rs2.ExecuteNonQuery()
            Disconnect2()

            Connect()
            rs.Connection = conn
            rs.CommandType = CommandType.Text
            rs.CommandText = "SELECT `sample_id` FROM `order_pathologist` WHERE `sample_id` = @MainSampleID"
            reader = rs.ExecuteReader
            reader.Read()
            If reader.HasRows Then
                Disconnect()
                UpdateRecordwthoutMSG("UPDATE `order_pathologist` SET `pathologist_id` = @pathologist WHERE `sample_id` = @MainSampleID")
            Else
                Disconnect()
                SaveRecordwthoutMSG("INSERT INTO `order_pathologist` (`sample_id`, `pathologist_id`) VALUES " _
                & "(" _
                & "@MainSampleID," _
                & "@pathologist" _
                & ")"
                )
            End If
            Disconnect()

            'SaveRecordwthoutMSG("INSERT INTO `order_medtech` (`sample_id`, `medtech_id`) VALUES " _
            '& "(" _
            '& "@MainSampleID," _
            '& "@medtechid" _
            '& ")"
            ')
            Connect()
            rs.Connection = conn
            rs.CommandType = CommandType.Text
            rs.CommandText = "SELECT `sample_id` FROM `order_medtech` WHERE `sample_id` = @MainSampleID"
            reader = rs.ExecuteReader
            reader.Read()
            If reader.HasRows Then
                Disconnect()
                UpdateRecordwthoutMSG("UPDATE `order_medtech` SET `medtech_id` = @medtechid WHERE `sample_id` = @MainSampleID")
            Else
                Disconnect()
                SaveRecordwthoutMSG("INSERT INTO `order_medtech` (`sample_id`, `medtech_id`) VALUES " _
                & "(" _
                & "@MainSampleID," _
                & "@medtechid" _
                & ")"
                )
            End If
            Disconnect()

            'SaveRecordwthoutMSG("INSERT INTO `order_Verified` (`sample_id`, `medtech_id`) VALUES " _
            '& "(" _
            '& "@MainSampleID," _
            '& "@verifyid" _
            '& ")"
            ')

            Connect()
            rs.Connection = conn
            rs.CommandType = CommandType.Text
            rs.CommandText = "SELECT `sample_id` FROM `order_Verified` WHERE `sample_id` = @MainSampleID"
            reader = rs.ExecuteReader
            reader.Read()
            If reader.HasRows Then
                Disconnect()
                UpdateRecordwthoutMSG("UPDATE `order_Verified` SET `medtech_id` = @verifyid WHERE `sample_id` = @MainSampleID")
            Else
                Disconnect()
                SaveRecordwthoutMSG("INSERT INTO `order_Verified` (`sample_id`, `medtech_id`) VALUES " _
                & "(" _
                & "@MainSampleID," _
                & "@verifyid" _
                & ")"
                )
            End If
            Disconnect()

            If Not txtAccession.Text = "" Or txtORNo.Text = "" Or txtChargeSlip.Text = "" Then
                Connect()
                rs.Connection = conn
                rs.CommandType = CommandType.Text
                rs.CommandText = "SELECT * FROM `additional_info` WHERE `sample_id` = @mainID AND section = @Section AND sub_section = @SubSection"
                reader = rs.ExecuteReader
                reader.Read()
                If reader.HasRows Then
                    Disconnect()
                    UpdateRecordwthoutMSG("UPDATE `additional_info` SET " _
                                & "`accession_no` = @accession_no," _
                                & "`or_no` = @OR_No," _
                                & "`cs_no` = @CS_No," _
                                & "`sample_id` = @MainSampleID" _
                                & " WHERE `sample_id` = @mainID AND section = @Section AND sub_section = @SubSection"
                                )
                Else
                    Disconnect()
                    SaveRecordwthoutMSG("INSERT INTO `additional_info` (`accession_no`, `or_no`, `cs_no`, section, sub_section, `sample_id`) VALUES " _
                                & "(" _
                                & "@accession_no," _
                                & "@OR_No," _
                                & "@CS_No," _
                                & "@Section," _
                                & "@SubSection," _
                                & "@MainSampleID" _
                                & ")"
                                )
                End If
                Disconnect()
            End If

            For x As Integer = 0 To GridView.RowCount - 1 Step 1
                If (GridView.IsRowSelected(x)) Then
                    UpdateRecordwthoutMSG("UPDATE `tmpresult` SET " _
                          & "`flag` = '" & GridView.GetRowCellValue(x, "Flag") & "'," _
                          & "`measurement` = '" & GridView.GetRowCellValue(x, "Result") & "'," _
                          & "`reference_range` = '" & GridView.GetRowCellValue(x, "ReferenceRange") & "'," _
                          & "`value_conv` = '" & GridView.GetRowCellValue(x, "Conventional") & "'," _
                          & "`ref_conv` = '" & GridView.GetRowCellValue(x, "RefRange") & "'," _
                          & "`patient_id` = @patient_id," _
                          & "`instrument` = '" & GridView.GetRowCellValue(x, "Instrument") & "'," _
                          & "`print_status` = '1'," _
                          & "`sample_id` = @MainSampleID" _
                          & " WHERE `sample_id` = @mainID AND `test_code` = '" & GridView.GetRowCellValue(x, "TestCode") & "' AND `accepted_result` = '" & GridView.GetRowCellValue(x, "AcceptedResult") & "' AND `id` = '" & GridView.GetRowCellValue(x, "ID") & "'"
                          )
                Else
                    UpdateRecordwthoutMSG("UPDATE `tmpresult` SET " _
                          & "`flag` = '" & GridView.GetRowCellValue(x, "Flag") & "'," _
                          & "`measurement` = '" & GridView.GetRowCellValue(x, "Result") & "'," _
                          & "`reference_range` = '" & GridView.GetRowCellValue(x, "ReferenceRange") & "'," _
                          & "`value_conv` = '" & GridView.GetRowCellValue(x, "Conventional") & "'," _
                          & "`ref_conv` = '" & GridView.GetRowCellValue(x, "RefRange") & "'," _
                          & "`patient_id` = @patient_id," _
                          & "`instrument` = '" & GridView.GetRowCellValue(x, "Instrument") & "'," _
                          & "`print_status` = '0'," _
                          & "`sample_id` = @MainSampleID" _
                          & " WHERE `sample_id` = @mainID AND `test_code` = '" & GridView.GetRowCellValue(x, "TestCode") & "' AND `accepted_result` = '" & GridView.GetRowCellValue(x, "AcceptedResult") & "' AND `id` = '" & GridView.GetRowCellValue(x, "ID") & "'"
                          )
                End If
            Next

            Connect()
            rs.Connection = conn
            rs.CommandType = CommandType.Text
            rs.CommandText = "SELECT `sample_id` FROM `patient_remarks` WHERE `sample_id` = @mainID"
            reader = rs.ExecuteReader
            reader.Read()
            If reader.HasRows Then
                Disconnect()
                UpdateRecordwthoutMSG("UPDATE `patient_remarks` SET `remarks` = @remarks, `diagnosis` = @lab_comment WHERE `sample_id` = @mainID AND `section` = @Section AND `sub_section` = @SubSection")
            Else
                Disconnect()
                SaveRecordwthoutMSG("INSERT INTO `patient_remarks` (`remarks`, `sample_id`, `section`, `sub_section`) VALUES (@remarks, @MainSampleID, @Section, @SubSection)")
            End If
            Disconnect()

            'Connect()
            'rs.Connection = conn
            'rs.CommandType = CommandType.Text
            'rs.CommandText = "SELECT `usertype` FROM `user_account` WHERE `email` = '" & CurrEmail & "'"
            'reader = rs.ExecuteReader
            'reader.Read()
            'If reader.HasRows Then
            If CurrType.Contains("Doctor") = True Or CurrType.Contains("Administrator") = True Then
                'Disconnect()
                UpdateRecordwthoutMSG("UPDATE `specimen_tracking` SET " _
               & "`validated` = @specimen_tracking_date_time" _
               & " WHERE sample_id = @mainID AND section = @Section AND sub_section = @Subsection"
               )

            End If
            'End If
            'Disconnect()


            'UpdateRecordwthoutMSG("UPDATE `specimen_tracking` SET " _
            '        & "`validated` = @specimen_tracking_date_time" _
            '        & " WHERE sample_id = @mainID AND section = @Section AND sub_section = @Subsection"
            '        )

            'btnPrint.Enabled = False
            'btnPrintNow.Enabled = True

            mainID = MainSampleID

            MessageBox.Show("Information has been saved for Verification", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information)

            frmHemaWorklist.LoadRecords()

            ActivityLogs(txtSampleID.Text,
                             txtPatientID.Text,
                              txtName.Text,
                             CurrUser,
                             "Verified",
                             cboRequest.Text,
                             Section,
                             SubSection)

            Me.Close()
            Me.Dispose()
        Catch ex As Exception
            Disconnect()
            MessageBox.Show(ex.Message, "Mysql Exception", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End Try
    End Sub

    Private Sub btnRtnMedTech_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnRtnMedTech.ItemClick

        rs.Parameters.Clear()
        rs.Parameters.AddWithValue("@mainID", mainID)
        rs.Parameters.AddWithValue("@Section", Section)
        rs.Parameters.AddWithValue("@SubSection", SubSection)
        rs.Parameters.AddWithValue("@status", "Processing")

        UpdateRecordwthoutMSG("UPDATE `tmpWorklist` SET " _
                    & "`status` = @status" _
                    & " WHERE main_id = @mainID AND testtype = @Section AND sub_section = @SubSection"
                    )

        UpdateRecordwthoutMSG("UPDATE `patient_remarks` SET `remarks` = @remarks WHERE `sample_id` = @mainID AND `section` = @Section AND `sub_section` = @SubSection")

        ActivityLogs(txtSampleID.Text,
                             txtPatientID.Text,
                              txtName.Text,
                             CurrUser,
                             "Return to MedTech",
                             cboRequest.Text,
                             Section,
                             SubSection)

        MessageBox.Show("Patient has been returned to MedTech", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Me.Close()
    End Sub

    Private Sub Reextract_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnReextract.ItemClick
        frmHemaChangeDateTime.MainID = txtSampleID.Text
        frmHemaChangeDateTime.Section = Section
        frmHemaChangeDateTime.SubSection = SubSection
        frmAuthChangeDate.ChangeDate = 1
        frmAuthChangeDate.ShowDialog()

        ActivityLogs(txtSampleID.Text,
                             txtPatientID.Text,
                              txtName.Text,
                             CurrUser,
                             "Change Date Login Attempt",
                             cboRequest.Text,
                             Section,
                             SubSection)

    End Sub

    Private Sub cboTestRun_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboTestRun.SelectedIndexChanged
        LoadTest()
    End Sub

    Private Sub resultChecker_Tick(sender As Object, e As EventArgs) Handles resultChecker.Tick

        Connect()
        rs.Connection = conn
        rs.CommandType = CommandType.Text
        rs.CommandText = "SELECT `measurement` FROM `tmpresult` WHERE `sample_id` = '" & txtSampleID.Text & "' AND `measurement` = '' "
        reader = rs.ExecuteReader
        reader.Read()
        If reader.HasRows Then
            Disconnect()
            btnVerify.Enabled = False
        Else
            Disconnect()
            btnVerify.Enabled = True
        End If
        Disconnect()
    End Sub

#End Region

End Class