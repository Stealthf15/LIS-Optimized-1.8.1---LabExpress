﻿Imports System.Text.RegularExpressions

Public Class frmChemAddTest
    Public TypeResult As String = ""
    Public mainID As String = ""
    Public patientID As String = ""
    Public Section As String = ""
    Public SubSection As String = ""
    Public HISMainID As String = ""

    Public Sub LoadRecords()
        Try
            lvList.Items.Clear()
            Connect()
            rs.Connection = conn
            'rs.CommandText = "SELECT id, specimen, specimen_type, test_code, his_code, si_unit, conventional_unit, section, order_no, test_group FROM `default_specimen` WHERE `section` = 'Chemistry' AND `status` = 'Enable' ORDER BY `order_no`"
            rs.CommandText = "SELECT * FROM `specimen` WHERE `section` = 'Chemistry' AND `status` = 'Enable' AND `instrument` = 'Other_Test' ORDER BY `order_no`"
            reader = rs.ExecuteReader
            While reader.Read()
                If reader.HasRows Then
                    iItem = New ListViewItem(reader(0).ToString, 0)
                    For x As Integer = 1 To 15 Step 1
                        iItem.SubItems.Add(reader(x).ToString())
                    Next
                    lvList.Items.Add(iItem)
                End If
            End While
            Disconnect()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub frmAddTestSemi_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Me.Dispose()
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.ItemClick
        Me.Close()
    End Sub

    Private Sub frmResultsNew_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        LoadRecords()
    End Sub

    Private Sub txtSearch_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSearch.TextChanged
        Try
            'SearchRecordsOnLV(lvList, "default_specimen", 12, "specimen", txtSearch.Text, "specimen")
            LoadRecords()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnAddTest_ItemClick(ByVal sender As Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnAddTest.ItemClick
        'Dim count As Integer = 0 'Count
        'Dim main_id_count As String
        'Dim x_main_id_count As String


        'Connect()
        'rs.Parameters.Clear()
        'rs.Parameters.AddWithValue("@sample_id", mainID)
        'rs.Connection = conn
        'rs.CommandText = "SELECT main_id FROM `order` WHERE `sample_id` = @sample_id AND `main_id` = @sample_id"
        'reader = rs.ExecuteReader
        'If reader.Read Then
        '    If reader(0).ToString = mainID Then
        '        main_id_count = reader(0).ToString + "-1"
        '    Else
        '        main_id_count = reader(0).ToString + 1
        '    End If
        'End If
        'Disconnect()

        'If TypeResult = "New" Then
        '    For x = Me.lvList.CheckedItems.Count - 1 To 0 Step -1
        '        rs.Parameters.Clear()
        '        rs.Parameters.AddWithValue("@UniversalID", lvList.CheckedItems(x).SubItems(1).Text)
        '        rs.Parameters.AddWithValue("@Measurement", "")
        '        rs.Parameters.AddWithValue("@sample_id", mainID)
        '        rs.Parameters.AddWithValue("@patient_id", patientID)
        '        rs.Parameters.AddWithValue("@test_code", lvList.CheckedItems(x).SubItems(5).Text)
        '        rs.Parameters.AddWithValue("@his_testcode", lvList.CheckedItems(x).SubItems(6).Text)
        '        rs.Parameters.AddWithValue("@si_Unit", lvList.CheckedItems(x).SubItems(8).Text)
        '        rs.Parameters.AddWithValue("@conv_unit", lvList.CheckedItems(x).SubItems(9).Text)
        '        rs.Parameters.AddWithValue("@section", lvList.CheckedItems(x).SubItems(12).Text)
        '        rs.Parameters.AddWithValue("@order_no", lvList.CheckedItems(x).SubItems(14).Text)
        '        rs.Parameters.AddWithValue("@test_group", lvList.CheckedItems(x).SubItems(15).Text)
        '        rs.Parameters.AddWithValue("@date", Now)
        '        rs.Parameters.AddWithValue("@SubSection", SubSection)

        '        'SaveRecordwthoutMSG("INSERT INTO `tmpresult` (`universal_id`, `measurement`,`test_code`, `sample_id`, `date`, `patient_id`, `order_no`, `test_group`, `units`, `unit_conv`, `instrument`, `his_test_code`, `section`, `sub_section`) VALUES " _
        '        '            & "(" _
        '        '            & "@UniversalID, @Measurement, @test_code, @sample_id, @date, @patient_id, @order_no, @test_group, @si_unit, @conv_unit, 'Other_Test', @his_testcode, @section, @SubSection" _
        '        '            & ")"
        '        '            )
        '        SaveRecordwthoutMSG("INSERT INTO `tmpresult` (`universal_id`, `measurement`,`test_code`, `sample_id`, `date`, `patient_id`, `order_no`, `test_group`, `units`, `unit_conv`, `instrument`, `his_code`, `section`, `sub_section`, `print_status`) VALUES " _
        '                    & "(" _
        '                    & "@UniversalID, @Measurement, @test_code, @sample_id, @date, @patient_id, @order_no, @test_group, @si_unit, @conv_unit, 'Other_Test', @his_testcode, @section, @SubSection, '0'" _
        '                    & ")"
        '                    )

        '        frmChemNew.LoadTest()
        '    Next
        '    Me.Close()
        'ElseIf TypeResult = "Old" Then
        '    For x = Me.lvList.CheckedItems.Count - 1 To 0 Step -1
        '        rs.Parameters.Clear()
        '        rs.Parameters.AddWithValue("@UniversalID", lvList.CheckedItems(x).SubItems(1).Text)
        '        rs.Parameters.AddWithValue("@Measurement", "")
        '        rs.Parameters.AddWithValue("@sample_id", mainID)
        '        rs.Parameters.AddWithValue("@patient_id", patientID)
        '        rs.Parameters.AddWithValue("@test_code", lvList.CheckedItems(x).SubItems(5).Text)
        '        rs.Parameters.AddWithValue("@his_testcode", lvList.CheckedItems(x).SubItems(6).Text)
        '        rs.Parameters.AddWithValue("@si_Unit", lvList.CheckedItems(x).SubItems(8).Text)
        '        rs.Parameters.AddWithValue("@conv_unit", lvList.CheckedItems(x).SubItems(9).Text)
        '        rs.Parameters.AddWithValue("@section", lvList.CheckedItems(x).SubItems(12).Text)
        '        rs.Parameters.AddWithValue("@order_no", lvList.CheckedItems(x).SubItems(14).Text)
        '        rs.Parameters.AddWithValue("@test_group", lvList.CheckedItems(x).SubItems(15).Text)
        '        rs.Parameters.AddWithValue("@date", Now)
        '        rs.Parameters.AddWithValue("@SubSection", SubSection)

        '        'SaveRecordwthoutMSG("INSERT INTO `result` (`universal_id`, `measurement`,`test_code`, `sample_id`, `date`, `patient_id`, `order_no`, `test_group`, `units`, `unit_conv`, `instrument`, `his_test_code`, `section`, `sub_section`) VALUES " _
        '        '            & "(" _
        '        '            & "@UniversalID, @Measurement, @test_code, @sample_id, @date, @patient_id, @order_no, @test_group, @si_unit, @conv_unit, 'Other_Test', @his_testcode, @section, @SubSection" _
        '        '            & ")"
        '        '            )
        '        SaveRecordwthoutMSG("INSERT INTO `result` (`universal_id`, `measurement`,`test_code`, `sample_id`, `date`, `patient_id`, `order_no`, `test_group`, `units`, `unit_conv`, `instrument`, `his_code`, `section`, `sub_section`, `print_status`) VALUES " _
        '                    & "(" _
        '                    & "@UniversalID, @Measurement, @test_code, @sample_id, @date, @patient_id, @order_no, @test_group, @si_unit, @conv_unit, 'Other_Test', @his_testcode, @section, @SubSection, '0'" _
        '                    & ")"
        '                    )

        '        frmChemOrdered.LoadTest()
        '    Next
        '    Me.Close()
        'ElseIf TypeResult = "Run" Then
        '    For x = Me.lvList.CheckedItems.Count - 1 To 0 Step -1
        '        rs.Parameters.Clear()
        '        rs.Parameters.AddWithValue("@UniversalID", lvList.CheckedItems(x).SubItems(1).Text)
        '        rs.Parameters.AddWithValue("@Measurement", "")
        '        rs.Parameters.AddWithValue("@sample_id", mainID)
        '        rs.Parameters.AddWithValue("@patient_id", patientID)
        '        rs.Parameters.AddWithValue("@test_code", lvList.CheckedItems(x).SubItems(3).Text)
        '        rs.Parameters.AddWithValue("@his_testcode", lvList.CheckedItems(x).SubItems(4).Text)
        '        rs.Parameters.AddWithValue("@si_Unit", lvList.CheckedItems(x).SubItems(5).Text)
        '        rs.Parameters.AddWithValue("@conv_unit", lvList.CheckedItems(x).SubItems(6).Text)
        '        rs.Parameters.AddWithValue("@section", lvList.CheckedItems(x).SubItems(7).Text)
        '        rs.Parameters.AddWithValue("@order_no", lvList.CheckedItems(x).SubItems(8).Text)
        '        rs.Parameters.AddWithValue("@test_group", lvList.CheckedItems(x).SubItems(9).Text)
        '        rs.Parameters.AddWithValue("@date", Now)
        '        rs.Parameters.AddWithValue("@SubSection", SubSection)
        '        rs.Parameters.AddWithValue("@newMainID", main_id_count)


        '        rs2.Parameters.Clear()
        '        rs2.Parameters.AddWithValue("@UniversalID", lvList.CheckedItems(x).SubItems(1).Text)
        '        rs2.Parameters.AddWithValue("@Measurement", "")
        '        rs2.Parameters.AddWithValue("@sample_id", mainID)
        '        rs2.Parameters.AddWithValue("@patient_id", patientID)
        '        rs2.Parameters.AddWithValue("@test_code", lvList.CheckedItems(x).SubItems(3).Text)
        '        rs2.Parameters.AddWithValue("@his_testcode", lvList.CheckedItems(x).SubItems(4).Text)
        '        rs2.Parameters.AddWithValue("@si_Unit", lvList.CheckedItems(x).SubItems(5).Text)
        '        rs2.Parameters.AddWithValue("@conv_unit", lvList.CheckedItems(x).SubItems(6).Text)
        '        rs2.Parameters.AddWithValue("@section", lvList.CheckedItems(x).SubItems(7).Text)
        '        rs2.Parameters.AddWithValue("@order_no", lvList.CheckedItems(x).SubItems(8).Text)
        '        rs2.Parameters.AddWithValue("@test_group", lvList.CheckedItems(x).SubItems(9).Text)
        '        rs2.Parameters.AddWithValue("@date", Now)
        '        rs2.Parameters.AddWithValue("@SubSection", SubSection)
        '        rs2.Parameters.AddWithValue("@newMainID", main_id_count)

        '        'SaveRecordwthoutMSG("INSERT INTO `tmpresult` (`universal_id`, `measurement`,`test_code`, `sample_id`, `date`, `patient_id`, `order_no`, `test_group`, `units`, `unit_conv`, `instrument`, `his_test_code`, `section`, `sub_section`) VALUES " _
        '        '            & "(" _
        '        '            & "@UniversalID, @Measurement, @test_code, @sample_id, @date, @patient_id, @order_no, @test_group, @si_unit, @conv_unit, 'Other_Test', @his_testcode, @section, @SubSection" _
        '        '            & ")"
        '        '            )
        '        SaveRecordwthoutMSG("INSERT INTO `tmpresult` (`universal_id`, `measurement`,`test_code`, `sample_id`, `date`, `patient_id`, `order_no`, `test_group`, `units`, `unit_conv`, `instrument`, `his_code`, `section`, `sub_section`, `print_status`) VALUES " _
        '                    & "(" _
        '                    & "@UniversalID, @Measurement, @test_code, @newMainID, @date, @patient_id, @order_no, @test_group, @si_unit, @conv_unit, 'Other_Test', @his_testcode, @section, @SubSection, '0'" _
        '                    & ")"
        '                    )

        '    Next
        '    Connect2()
        '    rs2.Connection = conn2
        '    rs2.CommandText = "SELECT `status`, `sample_id`, `patient_id`, `patient_name`, `sex`, `bdate`, `age`, `physician`, `dept`, `medtech`, `verified_by`, `date`, `time`, `dt_released`, `test`, `patient_type`, `testtype`, `instrument`, `barcode`, `main_id`, `type`, `location`, sub_section FROM `Order` WHERE `sample_id` = @sample_id AND `main_id` = @sample_id"
        '    reader2 = rs2.ExecuteReader
        '    If reader2.Read Then
        '        Connect()
        '        rs.Connection = conn
        '        rs.CommandText = "SELECT * FROM `tmpworklist` WHERE `sample_id` = @sample_id AND `main_id` = @sample_id"
        '        reader = rs.ExecuteReader
        '        If reader.Read Then
        '            Disconnect()
        '        Else
        '            Disconnect()
        '            SaveRecordwthoutMSG("INSERT INTO `tmpworklist` (`status`, `sample_id`, `patient_id`, `patient_name`, `sex`, `bdate`, `age`, `physician`, `dept`, `medtech`, `verified_by`, `date`, `time`, `dt_released`, `test`, `patient_type`, `testtype`, `instrument`, `barcode`, `main_id`, `type`, `location`, sub_section) VALUES " _
        '                    & "(" _
        '                    & "'Checked-In' ," _
        '                    & "'" & reader2(1).ToString & "'," _
        '                    & "'" & reader2(2).ToString & "'," _
        '                    & "'" & reader2(3).ToString & "'," _
        '                    & "'" & reader2(4).ToString & "'," _
        '                    & "'" & reader2(5).ToString & "'," _
        '                    & "'" & reader2(6).ToString & "'," _
        '                    & "'" & reader2(7).ToString & "'," _
        '                    & "'" & reader2(8).ToString & "'," _
        '                    & "''," _
        '                    & "''," _
        '                    & "'" & Format(Convert.ToDateTime(Now), "yyyy-MM-dd") & "'," _
        '                    & "'" & Now.ToLongTimeString & "'," _
        '                    & "''," _
        '                    & "'" & reader2(14).ToString & "'," _
        '                    & "'" & reader2(15).ToString & "'," _
        '                    & "'" & reader2(16).ToString & "'," _
        '                    & "'Other_Test'," _
        '                    & "'" & reader2(18).ToString & "'," _
        '                    & "@newMainID," _
        '                    & "'" & reader2(20).ToString & "'," _
        '                    & "'" & reader2(21).ToString & "'," _
        '                    & "'" & reader2(22).ToString & "'" _
        '                    & ")"
        '                )
        '            MessageBox.Show("Request was successfully ordered.", "Successfully Ordered", MessageBoxButtons.OK, MessageBoxIcon.Information)
        '        End If
        '        Disconnect()
        '    End If
        '    Disconnect2()

        '    Dim received As DateTime
        '    Dim extraction As DateTime
        '    Connect2()
        '    rs2.Connection = conn2
        '    rs2.CommandText = "SELECT `sample_id`, `received`, `extracted`, `processing`, `validated`, `printed`, `latest`, `section`, `sub_section` FROM specimen_tracking WHERE sample_id = @sample_id"
        '    reader2 = rs2.ExecuteReader
        '    If reader2.Read Then
        '        SaveRecordwthoutMSG("INSERT INTO `specimen_tracking` (`sample_id`, `received`, `extracted`, `processing`, `validated`, `printed`, `latest`, `section`, `sub_section`) VALUES " _
        '                    & "(" _
        '                    & "@newMainID, '" & reader2(1) & "', '" & reader2(2) & "', '" & reader2(3).ToString & "', '" & reader2(4).ToString & "', '" & reader2(5).ToString & "', '" & reader2(6).ToString & "', '" & reader2(7).ToString & "', '" & reader2(8).ToString & "'" _
        '                    & ")"
        '                    )
        '    End If
        '    Disconnect2()

        '    frmChemWorklist.LoadRecords()
        '    frmChemNew.LoadTest()
        '    Me.Close()
        'End If

        If TypeResult = "New" Then
            For x = Me.lvList.CheckedItems.Count - 1 To 0 Step -1
                rs.Parameters.Clear()
                rs.Parameters.AddWithValue("@UniversalID", lvList.CheckedItems(x).SubItems(1).Text)
                rs.Parameters.AddWithValue("@Measurement", "")
                rs.Parameters.AddWithValue("@sample_id", mainID)
                rs.Parameters.AddWithValue("@patient_id", patientID)
                rs.Parameters.AddWithValue("@test_code", lvList.CheckedItems(x).SubItems(5).Text)
                rs.Parameters.AddWithValue("@his_testcode", lvList.CheckedItems(x).SubItems(6).Text)
                rs.Parameters.AddWithValue("@si_Unit", lvList.CheckedItems(x).SubItems(8).Text)
                rs.Parameters.AddWithValue("@conv_unit", lvList.CheckedItems(x).SubItems(9).Text)
                rs.Parameters.AddWithValue("@section", lvList.CheckedItems(x).SubItems(12).Text)
                rs.Parameters.AddWithValue("@order_no", lvList.CheckedItems(x).SubItems(14).Text + 1000)
                rs.Parameters.AddWithValue("@test_group", lvList.CheckedItems(x).SubItems(15).Text)
                rs.Parameters.AddWithValue("@date", Now)
                rs.Parameters.AddWithValue("@SubSection", SubSection)
                rs.Parameters.AddWithValue("@HISMainID", HISMainID)

                'SaveRecordwthoutMSG("INSERT INTO `tmpresult` (`universal_id`, `measurement`,`test_code`, `sample_id`, `date`, `patient_id`, `order_no`, `test_group`, `units`, `unit_conv`, `instrument`, `his_test_code`, `section`, `sub_section`) VALUES " _
                '                                            & "(" _
                '                                            & "@UniversalID, @Measurement, @test_code, @sample_id, @date, @patient_id, @order_no, @test_group, @si_unit, @conv_unit, 'Other_Test', @his_testcode, @section, @SubSection" _
                '                                            & ")"
                '                                            )

                SaveRecordwthoutMSG("INSERT INTO `tmpresult` (`universal_id`, `measurement`,`test_code`, `sample_id`, `date`, `patient_id`, `order_no`, `test_group`, `units`, `unit_conv`, `instrument`, `his_code`, `section`, `sub_section`, `print_status`, `his_mainid`, `accepted_result`) VALUES " _
                            & "(" _
                            & "@UniversalID, @Measurement, @test_code, @sample_id, @date, @patient_id, @order_no, @test_group, @si_unit, @conv_unit, 'Other_Test', @his_testcode, @section, @SubSection, '0', @HISMainID, 1" _
                            & ")"
                            )

                frmChemNew.LoadTest()
            Next
            Me.Close()
        ElseIf TypeResult = "Old" Then
            For x = Me.lvList.CheckedItems.Count - 1 To 0 Step -1
                rs.Parameters.Clear()
                rs.Parameters.AddWithValue("@UniversalID", lvList.CheckedItems(x).SubItems(1).Text)
                rs.Parameters.AddWithValue("@Measurement", "")
                rs.Parameters.AddWithValue("@sample_id", mainID)
                rs.Parameters.AddWithValue("@patient_id", patientID)
                rs.Parameters.AddWithValue("@test_code", lvList.CheckedItems(x).SubItems(5).Text)
                rs.Parameters.AddWithValue("@his_testcode", lvList.CheckedItems(x).SubItems(6).Text)
                rs.Parameters.AddWithValue("@si_Unit", lvList.CheckedItems(x).SubItems(8).Text)
                rs.Parameters.AddWithValue("@conv_unit", lvList.CheckedItems(x).SubItems(9).Text)
                rs.Parameters.AddWithValue("@section", lvList.CheckedItems(x).SubItems(12).Text)
                rs.Parameters.AddWithValue("@order_no", lvList.CheckedItems(x).SubItems(14).Text + 1000)
                rs.Parameters.AddWithValue("@test_group", lvList.CheckedItems(x).SubItems(15).Text)
                rs.Parameters.AddWithValue("@date", Now)
                rs.Parameters.AddWithValue("@SubSection", SubSection)
                rs.Parameters.AddWithValue("@HISMainID", HISMainID)

                'SaveRecordwthoutMSG("INSERT INTO `result` (`universal_id`, `measurement`,`test_code`, `sample_id`, `date`, `patient_id`, `order_no`, `test_group`, `units`, `unit_conv`, `instrument`, `his_test_code`, `section`, `sub_section`) VALUES " _
                '            & "(" _
                '            & "@UniversalID, @Measurement, @test_code, @sample_id, @date, @patient_id, @order_no, @test_group, @si_unit, @conv_unit, 'Other_Test', @his_testcode, @section, @SubSection" _
                '            & ")"
                '            )

                SaveRecordwthoutMSG("INSERT INTO `result` (`universal_id`, `measurement`,`test_code`, `sample_id`, `date`, `patient_id`, `order_no`, `test_group`, `units`, `unit_conv`, `instrument`, `his_code`, `section`, `sub_section`, `print_status`, `his_mainid`, `accepted_result`) VALUES " _
                            & "(" _
                            & "@UniversalID, @Measurement, @test_code, @sample_id, @date, @patient_id, @order_no, @test_group, @si_unit, @conv_unit, 'Other_Test', @his_testcode, @section, @SubSection, '0', @HISMainID, 1" _
                            & ")"
                            )

                frmChemOrdered.LoadTest()
            Next
            Me.Close()
        End If

        ActivityLogs(frmChemNew.txtSampleID.Text,
                             frmChemNew.txtPatientID.Text,
                             frmChemNew.txtName.Text,
                             CurrUser,
                             "Added Test(s)",
                             frmChemNew.cboRequest.Text,
                             Section,
                             SubSection)

    End Sub

    Private Sub cboLimit_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        LoadRecords()
    End Sub
End Class

'Dim C_Chol As Double
'Dim C_HDL As Double
'Dim Trigly As Double
'Dim LDL As Double

'If lvList.CheckedItems(x).SubItems(2).Text = "C_LDL" Then
'    Connect()
'    rs.Connection = conn
'    rs.CommandText = "Select `measurement` FROM `tmpresult` WHERE `sample_id` Like @sample_id And `test_code` Like 'C_Chol'"
'    reader = rs.ExecuteReader
'    reader.Read()
'    If reader.HasRows Then
'        C_Chol = reader(0)
'    End If
'    Disconnect()

'    Connect()
'    rs.Connection = conn
'    rs.CommandText = "SELECT `measurement` FROM `tmpresult` WHERE `sample_id` LIKE @sample_id AND `test_code` LIKE 'C_HDL'"
'    reader = rs.ExecuteReader
'    reader.Read()
'    If reader.HasRows Then
'        C_HDL = reader(0)
'    End If
'    Disconnect()

'    Connect()
'    rs.Connection = conn
'    rs.CommandText = "SELECT `measurement` FROM `tmpresult` WHERE `sample_id` LIKE @sample_id AND `test_code` LIKE 'Trigly'"
'    reader = rs.ExecuteReader
'    reader.Read()
'    If reader.HasRows Then
'        Trigly = reader(0)
'    End If
'    Disconnect()
'    LDL = C_Chol - C_HDL - (Trigly / 2.2)

'    SaveRecordwthoutMSG("INSERT INTO `tmpResult` (`universal_id`, `measurement`, `test_code`, `sample_id`, `date`, `patient_id`, `order_no`, `test_group`, `units`, `unit_conv`, `instrument`) VALUES " _
'        & "(" _
'        & "@UniversalID, '" & LDL & "', @test_code, @sample_id, @date, @patient_id, @order_no, @test_group, @si_unit, @conv_unit, 'Other_Test'" _
'        & ")"
'        )
'Else
'    SaveRecordwthoutMSG("INSERT INTO `tmpResult` (`universal_id`, `measurement`, `test_code`, `sample_id`, `date`, `patient_id`, `order_no`, `test_group`, `units`, `unit_conv`, `instrument`) VALUES " _
'        & "(" _
'        & "@UniversalID, @Measurement, @test_code, @sample_id, @date, @patient_id, @order_no, @test_group, @si_unit, @conv_unit, 'Other_Test'" _
'        & ")"
'        )
'End If

'Dim C_Chol As Double
'Dim C_HDL As Double
'Dim Trigly As Double
'Dim LDL As Double

'If lvList.CheckedItems(x).SubItems(2).Text = "C_LDL" Then
'    Connect()
'    rs.Connection = conn
'    rs.CommandText = "SELECT `measurement` FROM `result` WHERE `sample_id` LIKE @sample_id AND `test_code` LIKE 'C_Chol'"
'    reader = rs.ExecuteReader
'    reader.Read()
'    If reader.HasRows Then
'        C_Chol = reader(0)
'    End If
'    Disconnect()

'    Connect()
'    rs.Connection = conn
'    rs.CommandText = "SELECT `measurement` FROM `result` WHERE `sample_id` LIKE @sample_id AND `test_code` LIKE 'C_HDL'"
'    reader = rs.ExecuteReader
'    reader.Read()
'    If reader.HasRows Then
'        C_HDL = reader(0)
'    End If
'    Disconnect()

'    Connect()
'    rs.Connection = conn
'    rs.CommandText = "SELECT `measurement` FROM `result` WHERE `sample_id` LIKE @sample_id AND `test_code` LIKE 'Trigly'"
'    reader = rs.ExecuteReader
'    reader.Read()
'    If reader.HasRows Then
'        Trigly = reader(0)
'    End If
'    Disconnect()
'    LDL = C_Chol - C_HDL - (Trigly / 2.2)

'    SaveRecordwthoutMSG("INSERT INTO `result` (`universal_id`, `measurement`, `test_code`, `sample_id`, `date`, `patient_id`, `order_no`, `test_group`, `units`, `unit_conv`, `instrument`) VALUES " _
'        & "(" _
'        & "@UniversalID, '" & LDL & "', @test_code, @sample_id, @date, @patient_id, @order_no, @test_group, @si_unit, @conv_unit, 'Other_Test'" _
'        & ")"
'        )
'Else
'    SaveRecordwthoutMSG("INSERT INTO `result` (`universal_id`, `measurement`, `test_code`, `sample_id`, `date`, `patient_id`, `order_no`, `test_group`, `units`, `unit_conv`, `instrument`) VALUES " _
'        & "(" _
'        & "@UniversalID, @Measurement, @test_code, @sample_id, @date, @patient_id, @order_no, @test_group, @si_unit, @conv_unit, 'Other_Test'" _
'        & ")"
'        )
'End If