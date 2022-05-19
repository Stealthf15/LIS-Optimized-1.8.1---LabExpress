Imports MySql.Data.MySqlClient
Imports System.Text.RegularExpressions
Imports DevExpress.XtraGrid.Views.Grid

Public Class frmHemaPrevious

    'Public TypeResult As String = ""
    'Public mainID As String = ""
    Public patientID As String = ""
    Public section As String = ""
    Public SubSection As String = ""

    'Public sample_id As String = ""
    'Public Classification As String = ""
    'Public Gender As String = ""
    'Public Age As String = ""

    Dim ColumnCount As Integer
    Dim GetDate As String
    Dim GetTestCode() As String
    Dim x As Integer

    Public Sub LoadTest()
        'On Error Resume Next
        'Try
        '    GridView.Columns.Clear()
        '    GridView.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        '    GridView.Appearance.HeaderPanel.FontStyleDelta = FontStyle.Bold

        '    'GridView.Appearance.OddRow.BackColor = Color.Gainsboro
        '    'GridView.OptionsView.EnableAppearanceOddRow = True
        '    'GridView.Appearance.EvenRow.BackColor = Color.White
        '    'GridView.OptionsView.EnableAppearanceEvenRow = True

        '    'MessageBox.Show(mainID)
        '    'MessageBox.Show(TypeResult)

        '    Dim SQL As String = "SELECT `universal_id` AS TestName, `flag` AS Flag, `measurement` AS SI, `units` as Unit,
        '        `reference_range` as ReferenceRange, `value_conv` AS Conventional, `unit_conv` AS ConvUnit, 
        '        `ref_conv` AS ConvRefRange,  `instrument` AS Instrument, `test_code` AS TestCode, `id` AS ID, 
        '        `test_group` AS TestGroup, `his_code` AS HISTestCode, `his_mainid` AS HISMainID, `print_status` AS PrintStatus, DATE_FORMAT(`date`, '%Y-%m-%d %H:%i') AS DateRelease, `patient_id` AS PatientID, `order_no` AS OrderNo FROM `result` 
        '        WHERE `patient_id` = @patientID AND `section` = @Section AND `sub_section` = @SubSection"

        '    'Dim SQL As String = "Select tmpresult.`universal_id` AS TestName, 
        '    '                      tmpresult.flag AS Flag,
        '    '                      T1.Result As PreviousResult,
        '    '                      tmpresult.`measurement` AS Result, 
        '    '                      reference_range.`si_range` as ReferenceRange, 
        '    '                      tmpresult.`units` as Unit,
        '    '                      tmpresult.`value_conv` AS Conventional, 
        '    '                      tmpresult.`unit_conv` AS Units, 
        '    '                      reference_range.`conv_range` AS RefRange,  
        '    '                      tmpresult.`instrument` AS Instrument, 
        '    '                      tmpresult.`test_code` AS TestCode, 
        '    '                      tmpresult.`id` AS ID, 
        '    '                      tmpresult.`test_group` AS TestGroup, 
        '    '                      tmpresult.`his_code` AS HISTestCode, 
        '    '                      tmpresult.`his_mainid` AS HISMainID, 
        '    '                      tmpresult.`print_status` AS PrintStatus,
        '    '                      reference_range.`low_value` AS LowValue,
        '    '                      reference_range.`high_value` AS HighValue,
        '    '                      Specimen.`convertion_factor` AS ConversionFactor,
        '    '                      Specimen.`convertion_multiplier` AS ConversionMultiplier,
        '    '                      Specimen.`order_no` AS DisplayNo
        '    '         From tmpresult
        '    '         Left Join(SELECT measurement AS Result, test_code As TestCode, patient_id As PatientID, result.`date` AS DateReleased FROM result WHERE date IN (SELECT MAX(date) FROM result WHERE result.patient_id = @patientID And result.section = @Section And result.sub_section = @SubSection GROUP BY test_code) ORDER BY date DESC) AS T1 ON tmpresult.patient_id = T1.PatientID And tmpresult.test_code = T1.TestCode
        '    '         Left Join reference_range ON tmpresult.test_code = reference_range.test_code And reference_range.machine = tmpresult.instrument
        '    '         Left Join specimen ON tmpresult.test_code = specimen.test_code And tmpresult.instrument = specimen.instrument
        '    '         WHERE tmpresult.`sample_id` = @MainID And tmpresult.section = @Section And tmpresult.sub_section = @SubSection And (reference_range.classification = @Classification And reference_range.gender = @Gender And (@Age BETWEEN reference_range.age_begin And reference_range.age_end)) GROUP BY T1.TestCode, tmpresult.test_code ORDER BY specimen.order_no ASC"



        '    Dim command As New MySql.Data.MySqlClient.MySqlCommand(SQL, conn)

        '    command.Parameters.Clear()
        '    command.Parameters.AddWithValue("@patientID", patientID)
        '    command.Parameters.AddWithValue("@Section", section)
        '    command.Parameters.AddWithValue("@SubSection", SubSection)

        '    'command.Parameters.AddWithValue("@MainID", sample_id)
        '    'command.Parameters.AddWithValue("@Classification", Classification)
        '    'command.Parameters.AddWithValue("@Gender", Gender)
        '    'command.Parameters.AddWithValue("@Age", Age)


        '    Dim adapter As New MySql.Data.MySqlClient.MySqlDataAdapter(command)

        '    Dim myTable As DataTable = New DataTable
        '    adapter.Fill(myTable)

        '    dtResult.Font = New Font("Tahoma", 10)
        '    dtResult.ForeColor = Color.Black
        '    dtResult.DataSource = myTable

        '    GridView.Columns("TestCode").Visible = False
        '    GridView.Columns("ID").Visible = False
        '    GridView.Columns("HISTestCode").Visible = False
        '    GridView.Columns("HISMainID").Visible = False
        '    GridView.Columns("TestGroup").Visible = False
        '    GridView.Columns("PrintStatus").Visible = False
        '    'GridView.Columns("PatientID").Visible = False
        '    'GridView.Columns("OrderNo").Visible = False
        '    'GridView.Columns("DateRelease").Group()
        '    'GridView.Columns("DateRelease").Visible = False

        '    ' Make the grid read-only. 
        '    'GridView.OptionsBehavior.Editable = False
        '    ' Prevent the focused cell from being highlighted. 
        '    GridView.OptionsSelection.EnableAppearanceFocusedCell = False
        '    ' Draw a dotted focus rectangle around the entire row. 
        '    GridView.FocusRectStyle = DrawFocusRectStyle.RowFullFocus

        '    For x As Integer = 0 To GridView.RowCount - 1 Step 1
        '        If GridView.GetRowCellValue(x, GridView.Columns("PrintStatus")) Then
        '            GridView.SelectRow(x)
        '        Else

        '        End If
        '    Next

        '    'GridView.Columns("DateRelease").SortOrder = DevExpress.Data.ColumnSortOrder.Descending
        '    'GridView.Columns("OrderNo").SortOrder = DevExpress.Data.ColumnSortOrder.Ascending
        'Finally
        '    If conn.State = ConnectionState.Open Then
        '        conn.Close()
        '    End If
        'End Try

        Dim command As New MySqlCommand

        conn.Open()
        command.Connection = conn
        command.CommandType = CommandType.StoredProcedure
        command.CommandText = "worksheet"

        command.Parameters.AddWithValue("@PatientID", patientID)

        Dim adapter As New MySqlDataAdapter(command)

        Dim myTable As DataTable = New DataTable
        adapter.Fill(myTable)

        dtResult.Font = New Font("Tahoma", 9)
        dtResult.ForeColor = Color.Black
        dtResult.DataSource = myTable

        GridView.OptionsView.ColumnAutoWidth = False


        conn.Close()

    End Sub


    Private Sub frmAddTestSemi_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Me.Dispose()
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.ItemClick
        Me.Close()
    End Sub

    Private Sub frmResultsNew_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        LoadTest()
    End Sub

End Class