Imports MySql.Data.MySqlClient
Imports System.Text.RegularExpressions

Public Class frmSeroPrevious

    Public TypeResult As String = ""
    Public mainID As String = ""
    Public patientID As String = ""
    Public section As String = ""
    Public SubSection As String = ""

    Dim ColumnCount As Integer
    Dim GetDate As String
    Dim GetTestCode() As String
    Dim x As Integer

    Public Sub LoadRecords()
        'Try
        Dim command As New MySqlCommand

        conn.Open()
        command.Connection = conn
        command.CommandType = CommandType.StoredProcedure
        command.CommandText = "worksheet"

        command.Parameters.AddWithValue("@PID", patientID)

        Dim adapter As New MySqlDataAdapter(command)

        Dim myTable As DataTable = New DataTable
        adapter.Fill(myTable)

        dtResult.Font = New Font("Tahoma", 9)
        dtResult.ForeColor = Color.Black
        dtResult.DataSource = myTable



        conn.Close()

        'For Each column In dtResult.Columns
        '    column.SortMode = DataGridViewColumnSortMode.NotSortable
        'Next

        'Catch ex As Exception
        '    Disconnect()
        '    MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '    Exit Sub
        'End Try

        'rs.Parameters.Clear()
        'rs.Parameters.AddWithValue("@PatientID", patientID)

        ''LoadRecordsOnLVSQL(lvTest, "SELECT DISTINCT `universal_id` from `result` WHERE `patient_id` LIKE @PatientID", 0)

        ''Connect()
        ''rs.Connection = conn
        ''rs.CommandText = "SELECT DISTINCT `universal_id` FROM `result` WHERE `patient_id` = @PatientID"
        ''reader = rs.ExecuteReader
        ''While reader.Read()
        ''    iItem = New ListViewItem(reader(0).ToString, 0)
        ''    lvTest.Items.Add(iItem)
        ''End While
        ''Disconnect()

        'Connect()
        'rs.Connection = conn
        'rs.CommandText = "SELECT `patient_id`, DATE_FORMAT(`date`, '%m/%d/%Y') FROM `result` WHERE `patient_id` = @PatientID GROUP BY `patient_id`, `date`"
        'reader = rs.ExecuteReader
        'While reader.Read()
        '    ColumnCount = ColumnCount + 1
        '    GetDate = reader(1).ToString
        '    lvTest.Columns.Add(GetDate, 100)
        'End While
        'Disconnect()

        'Dim datecolumn As String
        'Dim universal As String
        'Connect1()
        'rs1.Connection = conn1
        'rs1.CommandText = "SELECT `universal_id`, `measurement`, `sample_id`, `patient_id`, DATE_FORMAT(`date`, '%m/%d/%Y') FROM `result` WHERE `patient_id` = @PatientID"
        'rs1.Parameters.Clear()
        'rs1.Parameters.AddWithValue("@PatientID", patientID)
        'reader1 = rs1.ExecuteReader
        'Dim i As Integer = -1

        'While reader1.Read()
        '    iItem = New ListViewItem(reader1(0).ToString, 0)
        '    lvTest.Items.Add(iItem)
        '    universal = reader1(0).ToString
        '    datecolumn = reader1(4).ToString
        '    i = i + 1
        '    For a As Integer = 0 To lvTest.Columns.Count - 1 Step 1
        '        If lvTest.Items(i).Text = universal And lvTest.Columns(a).Text = datecolumn Then
        '            iItem.SubItems.Add(reader1(1).ToString)
        '        End If
        '    Next

        'End While
        'Disconnect1()

        ''For a As Integer = 0 To lvTest.Columns.Count - 1 Step 1
        ''For x As Integer = 0 To lvTest.Items.Count - 1 Step 1

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

End Class