Imports System.Drawing.Printing
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraPrinting.BarCode
Imports System.Data.SqlClient
Imports System.Data
Imports System

Public Class frmiHOMIS

    Dim LastID As Integer
    Dim SampleID As String

    Public mainID As String = ""
    Dim arrImage() As Byte
    Dim Mode As String
    Dim BDate As String
    Dim Age As String

    Dim Section As String
    Dim SubSection As String

    Private PrintDocType As String = "Barcode"
    'Private StrPrinterName As String = "EPSON L220 Series"
    Private StrPrinterName As String = My.Settings.BCPrinterName
    'Public Property WrapMode As DataGridViewTriState

    Dim procreslt As String = "002"
    Dim procreslt2 As String = "002"
    Dim Test As String

    Dim HISCode As String
    Dim HISCode2 As String

    Dim PatientOrder As String
    Dim Test_Name As String
    Dim LIS_TestCode As String
    Dim TestCode As String

    Dim HIS_TestCode As String
    Dim HIS_TestCode2 As String

    Dim Order_No As String
    Dim Order_No2 As String

    Dim Test_Group As String

    Dim Unit As String
    Dim Unit_Conv As String
    Dim Unit2 As String
    Dim Unit_Conv2 As String

    Dim ResultID As String
    Dim HIS_Field As String
    Dim Order_TestCode As String

    Dim Labor As String
    Dim hisMainID As String

    Dim Labor2 As String
    Dim hisMainID2 As String

    'for IPD
    Public Sub LoadRecordsiHomis()
        Me.GridView.Columns.Clear()

        'rdbHospital.Text = "HospitalNo"


        'Try
        'Load Records from HIS
        'CASE WHEN hperson.patsex = 'M' THEN 'Male'
        'WHEN hperson.patsex = 'F' THEN 'Female'
        'End As Sex
        'we can also use this alternative to the code in modBarcode

        If txtSearch.Text = "" Then
            'MessageBox.Show(1)
            'Dim SQL As String = "SELECT * FROM (SELECT TOP 1000 " _
            '        & "t1.hpercode As HospitalNo, " _
            '        & "t1.patlast + ', ' + t1.patfirst AS PatientName, " _
            '        & "t2.procdesc AS Test," _
            '        & "t5.toecode AS Location, " _
            '        & "t7.wardname AS Ward," _
            '        & "t8.rmname AS Room," _
            '        & "t0.dopriority AS Priority, " _
            '        & "('DR. ' + t4.lastname + ', ' + t4.firstname) AS RequestingPhysician, " _
            '        & "t0.pcchrgcod AS ChargeSlip, " _
            '        & "CONVERT(varchar, t0.dodate, 22) AS DateTimeRequested, " _
            '        & "CAST(t0.dodate As Date) As DateTimeRequested2, " _
            '        & "CASE " _
            '        & "WHEN t2.procreslt = 002 THEN 'Hematology' " _
            '        & "WHEN t2.procreslt = 008 THEN 'Chemistry' " _
            '        & "WHEN t2.procreslt = 009 THEN 'Hematology' " _
            '        & "WHEN t2.procreslt = 007 THEN 'Fecalysis' " _
            '        & "WHEN t2.procreslt = 001 THEN 'Urinalysis' " _
            '        & "WHEN t2.procreslt = 006 THEN 'ImmunoSero' " _
            '        & "WHEN t2.procreslt = 003 THEN 'Blood Culture' " _
            '        & "WHEN t2.procreslt = 012 THEN 'Blood Bank' " _
            '        & "END AS Section, " _
            '        & "t0.docointkey AS MainID, " _
            '        & "t0.proccode AS HISCode," _
            '        & "t1.patbdate AS DateOfBirth, " _
            '        & "t1.patsex AS Sex, " _
            '        & "(t9.patstr + ', ' + t10.bgyname + ', ' + t11.ctyname + ', ' + t12.provname) AS Address" _
            '        & " FROM " _
            '        & "hdocord t0 " _
            '        & "LEFT JOIN hperson t1 ON t0.hpercode = t1.hpercode " _
            '        & "LEFT JOIN hprocm t2 ON t0.proccode = t2.proccode " _
            '        & "LEFT JOIN hprovider t3 ON t0.licno = t3.licno " _
            '        & "LEFT JOIN hpersonal t4 ON t3.employeeid = t4.employeeid " _
            '        & "LEFT JOIN henctr t5 ON t0.enccode = t5.enccode " _
            '        & "LEFT JOIN hpatroom t6 ON t5.enccode = t6.enccode " _
            '        & "LEFT JOIN hward t7 ON t6.wardcode = t7.wardcode " _
            '        & "LEFT JOIN hroom t8 ON t6.rmintkey = t8.rmintkey " _
            '        & "Left JOIN haddr t9 On t0.hpercode = t9.hpercode " _
            '        & "Left Join hbrgy t10 On t9.brg = t10.bgycode " _
            '        & "Left Join hcity t11 ON t9.ctycode = t11.ctycode " _
            '        & "Left Join hprov t12 ON t9.provcode = t12.provcode " _
            '        & " WHERE " _
            '        & "t0.estatus = 'P' " _
            '        & " And " _
            '        & "t0.pcchrgcod NOT LIKE '' " _
            '        & "AND " _
            '        & "t2.procreslt = @Section) AS result " _
            '        & "WHERE " _
            '        & "Result.DateTimeRequested2 BETWEEN @DateFrom AND @Dateto " _
            '        & "ORDER BY result.DateTimeRequested2 DESC"

            '*********************************************************************************
            Dim SQL As String = "SELECT * FROM (Select TOP 1000 " _
                        & "t1.hpercode As HospitalNo, " _
                        & "t1.patlast + ', ' + t1.patfirst AS PatientName, " _
                        & "t2.procdesc AS Test," _
                        & "t5.toecode AS Location, " _
                        & "t7.wardname AS Ward," _
                        & "t8.rmname AS Room," _
                        & "t0.dopriority AS Priority, " _
                        & "('DR. ' + t4.lastname + ', ' + t4.firstname) AS RequestingPhysician, " _
                        & "t0.pcchrgcod AS ChargeSlip, " _
                        & "CONVERT(varchar, t0.dodate, 22) AS DateTimeRequested, " _
                        & "t1.patbdate AS DateOfBirth, " _
                        & "t1.patsex AS Sex, " _
                        & "(t9.patstr + ', ' + t10.bgyname + ', ' + t11.ctyname + ', ' + t12.provname) AS Address, " _
                        & "t0.ordreas AS ClinicalImpression, " _
                        & "t0.docointkey AS MainID, " _
                        & "CASE " _
                        & "WHEN t2.procreslt = 002 THEN 'Hematology' " _
                        & "WHEN t2.procreslt = 008 THEN 'Chemistry' " _
                        & "WHEN t2.procreslt = 009 THEN 'Hematology' " _
                        & "WHEN t2.procreslt = 007 THEN 'Fecalysis' " _
                        & "WHEN t2.procreslt = 001 THEN 'Urinalysis' " _
                        & "WHEN t2.procreslt = 006 THEN 'ImmunoSero' " _
                        & "WHEN t2.procreslt = 003 THEN 'Blood Culture' " _
                        & "WHEN t2.procreslt = 012 THEN 'Blood Bank' " _
                        & "END AS Section, " _
                        & "t0.proccode AS HISCode " _
                        & " FROM " _
                        & "hdocord t0 " _
                        & "LEFT JOIN hperson t1 ON t0.hpercode = t1.hpercode " _
                        & "LEFT JOIN hprocm t2 ON t0.proccode = t2.proccode " _
                        & "LEFT JOIN hprovider t3 ON t0.licno = t3.licno " _
                        & "LEFT JOIN hpersonal t4 ON t3.employeeid = t4.employeeid " _
                        & "LEFT JOIN henctr t5 ON t0.enccode = t5.enccode " _
                        & "LEFT JOIN hpatroom t6 ON t5.enccode = t6.enccode " _
                        & "LEFT JOIN hward t7 ON t6.wardcode = t7.wardcode " _
                        & "LEFT JOIN hroom t8 ON t6.rmintkey = t8.rmintkey " _
                        & "Left JOIN haddr t9 On t0.hpercode = t9.hpercode " _
                        & "Left Join hbrgy t10 On t9.brg = t10.bgycode " _
                        & "Left Join hcity t11 ON t9.ctycode = t11.ctycode " _
                        & "Left Join hprov t12 ON t9.provcode = t12.provcode " _
                        & " WHERE " _
                        & "t0.estatus = 'P'" _
                        & " AND " _
                        & "t0.pcchrgcod NOT LIKE ''" _
                        & " AND " _
                        & "t2.procreslt = @Section" _
                        & " AND " _
                        & "(t6.patrmstat = 'A' OR (SELECT COUNT(*) FROM hpatroom WHERE t0.hpercode = t6.hpercode) = 0)" _
                        & "And " _
                        & "(Convert(varchar, t0.dodate, 101) BETWEEN @DateFrom And @DateTo)) As result " _
                        & "ORDER BY result.DateTimeRequested DESC, result.Ward"

            '***********************************************
            '& "ORDER BY hdocord.dodate DESC) As result WHERE HospitalNo Like '" & txtSearch.Text & "%' OR PatientName LIKE '" & txtSearch.Text & "%' OR RequestingPhysician LIKE '" & txtSearch.Text & "%' OR ChargeSlip LIKE '" & txtSearch.Text & "%'"

            '& "CONVERT(varchar, hdocord.dodate, 101) AS DateTimeRequested, " _
            GridView.Columns.Clear()
            GridView.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
            GridView.Appearance.HeaderPanel.FontStyleDelta = FontStyle.Bold

            ConnectSQL()
            rsSQL.Connection = connSQL
            rsSQL.CommandType = CommandType.Text
            rsSQL.CommandText = SQL
            rsSQL.Parameters.Clear()
            rsSQL.Parameters.AddWithValue("@Section", procreslt)
            rsSQL.Parameters.Add("@DateFrom", SqlDbType.DateTime).Value = Format(dtFrom1.Value, "yyyy-MM-dd")
            rsSQL.Parameters.Add("@DateTo", SqlDbType.DateTime).Value = Format(dtTo1.Value, "yyyy-MM-dd")

            Dim adapter As New SqlDataAdapter(rsSQL)

            Dim myTable As DataTable = New DataTable
            adapter.Fill(myTable)

            dtList.DataSource = myTable

            'ElseIf txtSearch.Text <> "" And rdbHospital.Checked = True Then
            '    Dim SQL As String = "SELECT TOP 1000 " _
            '            & "t1.hpercode AS HospitalNo, " _
            '            & "t1.patlast + ', ' + t1.patfirst AS PatientName, " _
            '            & "t2.procdesc AS Test," _
            '            & "t5.toecode AS Location, " _
            '            & "t7.wardname AS Ward," _
            '            & "t8.rmname AS Room," _
            '            & "t0.dopriority AS Priority, " _
            '            & "('DR. ' + t4.lastname + ', ' + t4.firstname) AS RequestingPhysician, " _
            '            & "t0.pcchrgcod AS ChargeSlip, " _
            '            & "CONVERT(varchar, t0.dodate, 22) AS DateTimeRequested, " _
            '            & "t1.patbdate AS DateOfBirth, " _
            '            & "t1.patsex AS Sex, " _
            '            & "(t9.patstr + ', ' + t10.bgyname + ', ' + t11.ctyname + ', ' + t12.provname) AS Address," _
            '            & "t0.ordreas AS ClinicalImpression, " _
            '            & "t0.docointkey AS MainID " _
            '            & "CASE " _
            '            & "WHEN t2.procreslt = 002 THEN 'Hematology' " _
            '            & "WHEN t2.procreslt = 008 THEN 'Chemistry' " _
            '            & "WHEN t2.procreslt = 009 THEN 'Hematology' " _
            '            & "WHEN t2.procreslt = 007 THEN 'Fecalysis' " _
            '            & "WHEN t2.procreslt = 001 THEN 'Urinalysis' " _
            '            & "WHEN t2.procreslt = 006 THEN 'ImmunoSero' " _
            '            & "WHEN t2.procreslt = 003 THEN 'Blood Culture' " _
            '            & "WHEN t2.procreslt = 012 THEN 'Blood Bank' " _
            '            & "END AS Section, " _
            '            & "t0.proccode AS HISCode" _
            '            & " FROM " _
            '            & "hdocord t0 " _
            '            & "LEFT JOIN hperson t1 ON t0.hpercode = t1.hpercode " _
            '            & "LEFT JOIN hprocm t2 ON t0.proccode = t2.proccode " _
            '            & "LEFT JOIN hprovider t3 ON t0.licno = t3.licno " _
            '            & "LEFT JOIN hpersonal t4 ON t3.employeeid = t4.employeeid " _
            '            & "LEFT JOIN henctr t5 ON t0.enccode = t5.enccode " _
            '            & "LEFT JOIN hpatroom t6 ON t5.enccode = t6.enccode " _
            '            & "LEFT JOIN hward t7 ON t6.wardcode = t7.wardcode " _
            '            & "LEFT JOIN hroom t8 ON t6.rmintkey = t8.rmintkey " _
            '            & "Left JOIN haddr t9 On t0.hpercode = t9.hpercode " _
            '            & "Left Join hbrgy t10 On t9.brg = t10.bgycode " _
            '            & "Left Join hcity t11 ON t9.ctycode = t11.ctycode " _
            '            & "Left Join hprov t12 ON t9.provcode = t12.provcode " _
            '            & " WHERE " _
            '            & "t0.estatus = 'P'" _
            '            & " AND " _
            '            & "t0.pcchrgcod NOT LIKE ''" _
            '            & " AND " _
            '            & "t2.procreslt = @Section" _
            '            & " AND " _
            '            & "(t6.patrmstat = 'A' OR (SELECT COUNT(*) FROM hpatroom WHERE t0.hpercode = t6.hpercode) = 0)" _
            '            & "And " _
            '            & "(CONVERT(varchar, t0.dodate, 101) BETWEEN @DateFrom AND @DateTo)" _
            '            & " AND " _
            '            & "t1.hpercode Like '" & txtSearch.Text & "%' " _
            '            & "ORDER BY t0.dodate DESC, t7.wardname"

            '    GridView.Columns.Clear()
            '    GridView.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
            '    GridView.Appearance.HeaderPanel.FontStyleDelta = FontStyle.Bold

            '    ConnectSQL()
            '    rsSQL.Connection = connSQL
            '    rsSQL.CommandType = CommandType.Text
            '    rsSQL.CommandText = SQL
            '    rsSQL.Parameters.Clear()
            '    rsSQL.Parameters.AddWithValue("@Section", procreslt)
            '    rsSQL.Parameters.Add("@DateFrom", SqlDbType.DateTime).Value = Format(dtFrom1.Value, "yyyy-MM-dd")
            '    rsSQL.Parameters.Add("@DateTo", SqlDbType.DateTime).Value = Format(dtTo1.Value, "yyyy-MM-dd")

            '    Dim adapter As New SqlDataAdapter(rsSQL)

            '    Dim myTable As DataTable = New DataTable
            '    adapter.Fill(myTable)

            '    dtList.DataSource = myTable

            'ElseIf txtSearch.Text <> "" And rdbName.Checked = True Then
            '    Dim SQL As String = "SELECT TOP 1000 " _
            '            & "t1.hpercode AS HospitalNo, " _
            '            & "t1.patlast + ', ' + t1.patfirst AS PatientName, " _
            '            & "t2.procdesc AS Test," _
            '            & "t5.toecode AS Location, " _
            '            & "t7.wardname AS Ward," _
            '            & "t8.rmname AS Room," _
            '            & "t0.dopriority AS Priority, " _
            '            & "('DR. ' + t4.lastname + ', ' + t4.firstname) AS RequestingPhysician, " _
            '            & "t0.pcchrgcod AS ChargeSlip, " _
            '            & "CONVERT(varchar, t0.dodate, 22) AS DateTimeRequested, " _
            '            & "t1.patbdate AS DateOfBirth, " _
            '            & "t1.patsex AS Sex, " _
            '            & "(t9.patstr + ', ' + t10.bgyname + ', ' + t11.ctyname + ', ' + t12.provname) AS Address," _
            '            & "t0.ordreas AS ClinicalImpression, " _
            '            & "t0.docointkey AS MainID, " _
            '                & "CASE " _
            '                & "WHEN t2.procreslt = 002 THEN 'Hematology' " _
            '                & "WHEN t2.procreslt = 008 THEN 'Chemistry' " _
            '                & "WHEN t2.procreslt = 009 THEN 'Hematology' " _
            '                & "WHEN t2.procreslt = 007 THEN 'Fecalysis' " _
            '                & "WHEN t2.procreslt = 001 THEN 'Urinalysis' " _
            '                & "WHEN t2.procreslt = 006 THEN 'ImmunoSero' " _
            '                & "WHEN t2.procreslt = 003 THEN 'Blood Culture' " _
            '                & "WHEN t2.procreslt = 012 THEN 'Blood Bank' " _
            '                & "END AS Section, " _
            '                & "t0.proccode AS HISCode" _
            '            & " FROM " _
            '            & "hdocord t0 " _
            '            & "LEFT JOIN hperson t1 ON t0.hpercode = t1.hpercode " _
            '            & "LEFT JOIN hprocm t2 ON t0.proccode = t2.proccode " _
            '            & "LEFT JOIN hprovider t3 ON t0.licno = t3.licno " _
            '            & "LEFT JOIN hpersonal t4 ON t3.employeeid = t4.employeeid " _
            '            & "LEFT JOIN henctr t5 ON t0.enccode = t5.enccode " _
            '            & "LEFT JOIN hpatroom t6 ON t5.enccode = t6.enccode " _
            '            & "LEFT JOIN hward t7 ON t6.wardcode = t7.wardcode " _
            '            & "LEFT JOIN hroom t8 ON t6.rmintkey = t8.rmintkey " _
            '            & "Left JOIN haddr t9 On t0.hpercode = t9.hpercode " _
            '            & "Left Join hbrgy t10 On t9.brg = t10.bgycode " _
            '            & "Left Join hcity t11 ON t9.ctycode = t11.ctycode " _
            '            & "Left Join hprov t12 ON t9.provcode = t12.provcode " _
            '            & " WHERE " _
            '            & "t0.estatus = 'P'" _
            '            & " AND " _
            '            & "t0.pcchrgcod NOT LIKE ''" _
            '            & " AND " _
            '            & "t2.procreslt = @Section" _
            '            & " AND " _
            '            & "(t6.patrmstat = 'A' OR (SELECT COUNT(*) FROM hpatroom WHERE t0.hpercode = t6.hpercode) = 0)" _
            '            & "And " _
            '            & "(CONVERT(varchar, t0.dodate, 101) BETWEEN @DateFrom AND @DateTo)" _
            '            & " AND " _
            '            & "t1.patlast + ', ' + t1.patfirst Like '" & txtSearch.Text & "%' " _
            '            & "ORDER BY t0.dodate DESC, t7.wardname"

            '    GridView.Columns.Clear()
            '    GridView.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
            '    GridView.Appearance.HeaderPanel.FontStyleDelta = FontStyle.Bold

            '    ConnectSQL()
            '    rsSQL.Connection = connSQL
            '    rsSQL.CommandType = CommandType.Text
            '    rsSQL.CommandText = SQL
            '    rsSQL.Parameters.Clear()
            '    rsSQL.Parameters.AddWithValue("@Section", procreslt)
            '    rsSQL.Parameters.Add("@DateFrom", SqlDbType.DateTime).Value = Format(dtFrom1.Value, "yyyy-MM-dd")
            '    rsSQL.Parameters.Add("@DateTo", SqlDbType.DateTime).Value = Format(dtTo1.Value, "yyyy-MM-dd")

            '    Dim adapter As New SqlDataAdapter(rsSQL)

            '    Dim myTable As DataTable = New DataTable
            '    adapter.Fill(myTable)

            '    dtList.DataSource = myTable

        Else
            If rdbHospital.Checked = True Then
                Dim SQL As String = "SELECT TOP 1000 " _
                    & "t1.hpercode AS HospitalNo, " _
                    & "t1.patlast + ', ' + t1.patfirst AS PatientName, " _
                    & "t2.procdesc AS Test," _
                    & "t5.toecode AS Location, " _
                    & "t7.wardname AS Ward," _
                    & "t8.rmname AS Room," _
                    & "t0.dopriority AS Priority, " _
                    & "('DR. ' + t4.lastname + ', ' + t4.firstname) AS RequestingPhysician, " _
                    & "t0.pcchrgcod AS ChargeSlip, " _
                    & "CONVERT(varchar, t0.dodate, 22) AS DateTimeRequested, " _
                    & "t1.patbdate AS DateOfBirth, " _
                    & "t1.patsex AS Sex, " _
                    & "(t9.patstr + ', ' + t10.bgyname + ', ' + t11.ctyname + ', ' + t12.provname) AS Address," _
                    & "t0.ordreas AS ClinicalImpression, " _
                    & "t0.docointkey AS MainID, " _
                    & "CASE " _
                    & "WHEN t2.procreslt = 002 THEN 'Hematology' " _
                    & "WHEN t2.procreslt = 008 THEN 'Chemistry' " _
                    & "WHEN t2.procreslt = 009 THEN 'Hematology' " _
                    & "WHEN t2.procreslt = 007 THEN 'Fecalysis' " _
                    & "WHEN t2.procreslt = 001 THEN 'Urinalysis' " _
                    & "WHEN t2.procreslt = 006 THEN 'ImmunoSero' " _
                    & "WHEN t2.procreslt = 003 THEN 'Blood Culture' " _
                    & "WHEN t2.procreslt = 012 THEN 'Blood Bank' " _
                    & "END AS Section, " _
                    & "t0.proccode AS HISCode" _
                    & " FROM " _
                    & "hdocord t0 " _
                    & "LEFT JOIN hperson t1 ON t0.hpercode = t1.hpercode " _
                    & "LEFT JOIN hprocm t2 ON t0.proccode = t2.proccode " _
                    & "LEFT JOIN hprovider t3 ON t0.licno = t3.licno " _
                    & "LEFT JOIN hpersonal t4 ON t3.employeeid = t4.employeeid " _
                    & "LEFT JOIN henctr t5 ON t0.enccode = t5.enccode " _
                    & "LEFT JOIN hpatroom t6 ON t5.enccode = t6.enccode " _
                    & "LEFT JOIN hward t7 ON t6.wardcode = t7.wardcode " _
                    & "LEFT JOIN hroom t8 ON t6.rmintkey = t8.rmintkey " _
                    & "Left JOIN haddr t9 On t0.hpercode = t9.hpercode " _
                    & "Left Join hbrgy t10 On t9.brg = t10.bgycode " _
                    & "Left Join hcity t11 ON t9.ctycode = t11.ctycode " _
                    & "Left Join hprov t12 ON t9.provcode = t12.provcode " _
                    & " WHERE " _
                    & "t0.estatus = 'P'" _
                    & " AND " _
                    & "t0.pcchrgcod NOT LIKE ''" _
                    & " AND " _
                    & "t2.procreslt = @Section" _
                    & " AND " _
                    & "(t6.patrmstat = 'A' OR (SELECT COUNT(*) FROM hpatroom WHERE t0.hpercode = t6.hpercode) = 0)" _
                    & "And " _
                    & "(CONVERT(varchar, t0.dodate, 101) BETWEEN @DateFrom AND @DateTo)" _
                    & " AND " _
                    & "t1.hpercode Like '" & txtSearch.Text & "%' " _
                    & "ORDER BY t0.dodate DESC, t7.wardname"

                '& "hperson.patlast + ', ' + hperson.patfirst LIKE '" & txtSearch.Text & "%'" _
                '& "ORDER BY hdocord.dodate DESC) As result WHERE HospitalNo Like '" & txtSearch.Text & "%' OR PatientName LIKE '" & txtSearch.Text & "%' OR RequestingPhysician LIKE '" & txtSearch.Text & "%' OR ChargeSlip LIKE '" & txtSearch.Text & "%'"

                '& "CONVERT(varchar, hdocord.dodate, 101) AS DateTimeRequested, " _
                GridView.Columns.Clear()
                GridView.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                GridView.Appearance.HeaderPanel.FontStyleDelta = FontStyle.Bold

                ConnectSQL()
                rsSQL.Connection = connSQL
                rsSQL.CommandType = CommandType.Text
                rsSQL.CommandText = SQL
                rsSQL.Parameters.Clear()
                rsSQL.Parameters.AddWithValue("@Section", procreslt)
                rsSQL.Parameters.Add("@DateFrom", SqlDbType.DateTime).Value = Format(dtFrom1.Value, "yyyy-MM-dd")
                rsSQL.Parameters.Add("@DateTo", SqlDbType.DateTime).Value = Format(dtTo1.Value, "yyyy-MM-dd")

                Dim adapter As New SqlDataAdapter(rsSQL)

                Dim myTable As DataTable = New DataTable
                adapter.Fill(myTable)

                dtList.DataSource = myTable

            ElseIf rdbName.Checked = True Then

                Dim SQL As String = "SELECT TOP 1000 " _
                    & "t1.hpercode AS HospitalNo, " _
                    & "t1.patlast + ', ' + t1.patfirst AS PatientName, " _
                    & "t2.procdesc AS Test," _
                    & "t5.toecode AS Location, " _
                    & "t7.wardname AS Ward," _
                    & "t8.rmname AS Room," _
                    & "t0.dopriority AS Priority, " _
                    & "('DR. ' + t4.lastname + ', ' + t4.firstname) AS RequestingPhysician, " _
                    & "t0.pcchrgcod AS ChargeSlip, " _
                    & "CONVERT(varchar, t0.dodate, 22) AS DateTimeRequested, " _
                    & "t1.patbdate AS DateOfBirth, " _
                    & "t1.patsex AS Sex, " _
                    & "(t9.patstr + ', ' + t10.bgyname + ', ' + t11.ctyname + ', ' + t12.provname) AS Address," _
                    & "t0.ordreas AS ClinicalImpression, " _
                    & "t0.docointkey AS MainID, " _
                        & "CASE " _
                        & "WHEN t2.procreslt = 002 THEN 'Hematology' " _
                        & "WHEN t2.procreslt = 008 THEN 'Chemistry' " _
                        & "WHEN t2.procreslt = 009 THEN 'Hematology' " _
                        & "WHEN t2.procreslt = 007 THEN 'Fecalysis' " _
                        & "WHEN t2.procreslt = 001 THEN 'Urinalysis' " _
                        & "WHEN t2.procreslt = 006 THEN 'ImmunoSero' " _
                        & "WHEN t2.procreslt = 003 THEN 'Blood Culture' " _
                        & "WHEN t2.procreslt = 012 THEN 'Blood Bank' " _
                        & "END AS Section, " _
                        & "t0.proccode AS HISCode" _
                    & " FROM " _
                    & "hdocord t0 " _
                    & "LEFT JOIN hperson t1 ON t0.hpercode = t1.hpercode " _
                    & "LEFT JOIN hprocm t2 ON t0.proccode = t2.proccode " _
                    & "LEFT JOIN hprovider t3 ON t0.licno = t3.licno " _
                    & "LEFT JOIN hpersonal t4 ON t3.employeeid = t4.employeeid " _
                    & "LEFT JOIN henctr t5 ON t0.enccode = t5.enccode " _
                    & "LEFT JOIN hpatroom t6 ON t5.enccode = t6.enccode " _
                    & "LEFT JOIN hward t7 ON t6.wardcode = t7.wardcode " _
                    & "LEFT JOIN hroom t8 ON t6.rmintkey = t8.rmintkey " _
                    & "Left JOIN haddr t9 On t0.hpercode = t9.hpercode " _
                    & "Left Join hbrgy t10 On t9.brg = t10.bgycode " _
                    & "Left Join hcity t11 ON t9.ctycode = t11.ctycode " _
                    & "Left Join hprov t12 ON t9.provcode = t12.provcode " _
                    & " WHERE " _
                    & "t0.estatus = 'P'" _
                    & " AND " _
                    & "t0.pcchrgcod NOT LIKE ''" _
                    & " AND " _
                    & "t2.procreslt = @Section" _
                    & " AND " _
                    & "(t6.patrmstat = 'A' OR (SELECT COUNT(*) FROM hpatroom WHERE t0.hpercode = t6.hpercode) = 0)" _
                    & "And " _
                    & "(CONVERT(varchar, t0.dodate, 101) BETWEEN @DateFrom AND @DateTo)" _
                    & " AND " _
                    & "t1.patlast + ', ' + t1.patfirst Like '" & txtSearch.Text & "%' " _
                    & "ORDER BY t0.dodate DESC, t7.wardname"

                GridView.Columns.Clear()
                GridView.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                GridView.Appearance.HeaderPanel.FontStyleDelta = FontStyle.Bold

                ConnectSQL()
                rsSQL.Connection = connSQL
                rsSQL.CommandType = CommandType.Text
                rsSQL.CommandText = SQL
                rsSQL.Parameters.Clear()
                rsSQL.Parameters.AddWithValue("@Section", procreslt)
                rsSQL.Parameters.Add("@DateFrom", SqlDbType.DateTime).Value = Format(dtFrom1.Value, "yyyy-MM-dd")
                rsSQL.Parameters.Add("@DateTo", SqlDbType.DateTime).Value = Format(dtTo1.Value, "yyyy-MM-dd")

                Dim adapter As New SqlDataAdapter(rsSQL)

                Dim myTable As DataTable = New DataTable
                adapter.Fill(myTable)

                dtList.DataSource = myTable

            ElseIf rdbPhysician.Checked = True Then

                Dim SQL As String = "SELECT TOP 1000 " _
                    & "t1.hpercode AS HospitalNo, " _
                    & "t1.patlast + ', ' + t1.patfirst AS PatientName, " _
                    & "t2.procdesc AS Test," _
                    & "t5.toecode AS Location, " _
                    & "t7.wardname AS Ward," _
                    & "t8.rmname AS Room," _
                    & "t0.dopriority AS Priority, " _
                    & "('DR. ' + t4.lastname + ', ' + t4.firstname) AS RequestingPhysician, " _
                    & "t0.pcchrgcod AS ChargeSlip, " _
                    & "CONVERT(varchar, t0.dodate, 22) AS DateTimeRequested, " _
                    & "t1.patbdate AS DateOfBirth, " _
                    & "t1.patsex AS Sex, " _
                    & "(t9.patstr + ', ' + t10.bgyname + ', ' + t11.ctyname + ', ' + t12.provname) AS Address," _
                    & "t0.ordreas AS ClinicalImpression, " _
                    & "t0.docointkey AS MainID, " _
                        & "CASE " _
                        & "WHEN t2.procreslt = 002 THEN 'Hematology' " _
                        & "WHEN t2.procreslt = 008 THEN 'Chemistry' " _
                        & "WHEN t2.procreslt = 009 THEN 'Hematology' " _
                        & "WHEN t2.procreslt = 007 THEN 'Fecalysis' " _
                        & "WHEN t2.procreslt = 001 THEN 'Urinalysis' " _
                        & "WHEN t2.procreslt = 006 THEN 'ImmunoSero' " _
                        & "WHEN t2.procreslt = 003 THEN 'Blood Culture' " _
                        & "WHEN t2.procreslt = 012 THEN 'Blood Bank' " _
                        & "END AS Section, " _
                        & "t0.proccode AS HISCode" _
                    & " FROM " _
                    & "hdocord t0 " _
                    & "LEFT JOIN hperson t1 ON t0.hpercode = t1.hpercode " _
                    & "LEFT JOIN hprocm t2 ON t0.proccode = t2.proccode " _
                    & "LEFT JOIN hprovider t3 ON t0.licno = t3.licno " _
                    & "LEFT JOIN hpersonal t4 ON t3.employeeid = t4.employeeid " _
                    & "LEFT JOIN henctr t5 ON t0.enccode = t5.enccode " _
                    & "LEFT JOIN hpatroom t6 ON t5.enccode = t6.enccode " _
                    & "LEFT JOIN hward t7 ON t6.wardcode = t7.wardcode " _
                    & "LEFT JOIN hroom t8 ON t6.rmintkey = t8.rmintkey " _
                    & "Left JOIN haddr t9 On t0.hpercode = t9.hpercode " _
                    & "Left Join hbrgy t10 On t9.brg = t10.bgycode " _
                    & "Left Join hcity t11 ON t9.ctycode = t11.ctycode " _
                    & "Left Join hprov t12 ON t9.provcode = t12.provcode " _
                    & " WHERE " _
                    & "t0.estatus = 'P'" _
                    & " AND " _
                    & "t0.pcchrgcod NOT LIKE ''" _
                    & " AND " _
                    & "t2.procreslt = @Section" _
                    & " AND " _
                    & "(t6.patrmstat = 'A' OR (SELECT COUNT(*) FROM hpatroom WHERE t0.hpercode = t6.hpercode) = 0)" _
                    & "And " _
                    & "(CONVERT(varchar, t0.dodate, 101) BETWEEN @DateFrom AND @DateTo)" _
                    & " AND " _
                    & "('DR. ' + t4.lastname + ', ' + t4.firstname) Like '" & txtSearch.Text & "%' " _
                    & "ORDER BY t0.dodate DESC, t7.wardname"

                GridView.Columns.Clear()
                GridView.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                GridView.Appearance.HeaderPanel.FontStyleDelta = FontStyle.Bold

                ConnectSQL()
                rsSQL.Connection = connSQL
                rsSQL.CommandType = CommandType.Text
                rsSQL.CommandText = SQL
                rsSQL.Parameters.Clear()
                rsSQL.Parameters.AddWithValue("@Section", procreslt)
                rsSQL.Parameters.Add("@DateFrom", SqlDbType.DateTime).Value = Format(dtFrom1.Value, "yyyy-MM-dd")
                rsSQL.Parameters.Add("@DateTo", SqlDbType.DateTime).Value = Format(dtTo1.Value, "yyyy-MM-dd")

                Dim adapter As New SqlDataAdapter(rsSQL)

                Dim myTable As DataTable = New DataTable
                adapter.Fill(myTable)

                dtList.DataSource = myTable

            ElseIf rdbChargeSlip.Checked = True Then

                Dim SQL As String = "SELECT TOP 1000 " _
                    & "t1.hpercode AS HospitalNo, " _
                    & "t1.patlast + ', ' + t1.patfirst AS PatientName, " _
                    & "t2.procdesc AS Test," _
                    & "t5.toecode AS Location, " _
                    & "t7.wardname AS Ward," _
                    & "t8.rmname AS Room," _
                    & "t0.dopriority AS Priority, " _
                    & "('DR. ' + t4.lastname + ', ' + t4.firstname) AS RequestingPhysician, " _
                    & "t0.pcchrgcod AS ChargeSlip, " _
                    & "CONVERT(varchar, t0.dodate, 22) AS DateTimeRequested, " _
                    & "t1.patbdate AS DateOfBirth, " _
                    & "t1.patsex AS Sex, " _
                    & "(t9.patstr + ', ' + t10.bgyname + ', ' + t11.ctyname + ', ' + t12.provname) AS Address," _
                    & "t0.ordreas AS ClinicalImpression, " _
                    & "t0.docointkey AS MainID, " _
                        & "CASE " _
                        & "WHEN t2.procreslt = 002 THEN 'Hematology' " _
                        & "WHEN t2.procreslt = 008 THEN 'Chemistry' " _
                        & "WHEN t2.procreslt = 009 THEN 'Hematology' " _
                        & "WHEN t2.procreslt = 007 THEN 'Fecalysis' " _
                        & "WHEN t2.procreslt = 001 THEN 'Urinalysis' " _
                        & "WHEN t2.procreslt = 006 THEN 'ImmunoSero' " _
                        & "WHEN t2.procreslt = 003 THEN 'Blood Culture' " _
                        & "WHEN t2.procreslt = 012 THEN 'Blood Bank' " _
                        & "END AS Section, " _
                        & "t0.proccode AS HISCode" _
                    & " FROM " _
                    & "hdocord t0 " _
                    & "LEFT JOIN hperson t1 ON t0.hpercode = t1.hpercode " _
                    & "LEFT JOIN hprocm t2 ON t0.proccode = t2.proccode " _
                    & "LEFT JOIN hprovider t3 ON t0.licno = t3.licno " _
                    & "LEFT JOIN hpersonal t4 ON t3.employeeid = t4.employeeid " _
                    & "LEFT JOIN henctr t5 ON t0.enccode = t5.enccode " _
                    & "LEFT JOIN hpatroom t6 ON t5.enccode = t6.enccode " _
                    & "LEFT JOIN hward t7 ON t6.wardcode = t7.wardcode " _
                    & "LEFT JOIN hroom t8 ON t6.rmintkey = t8.rmintkey " _
                    & "Left JOIN haddr t9 On t0.hpercode = t9.hpercode " _
                    & "Left Join hbrgy t10 On t9.brg = t10.bgycode " _
                    & "Left Join hcity t11 ON t9.ctycode = t11.ctycode " _
                    & "Left Join hprov t12 ON t9.provcode = t12.provcode " _
                    & " WHERE " _
                    & "t0.estatus = 'P'" _
                    & " AND " _
                    & "t0.pcchrgcod NOT LIKE ''" _
                    & " AND " _
                    & "t2.procreslt = @Section" _
                    & " AND " _
                    & "(t6.patrmstat = 'A' OR (SELECT COUNT(*) FROM hpatroom WHERE t0.hpercode = t6.hpercode) = 0)" _
                    & "And " _
                    & "(CONVERT(varchar, t0.dodate, 101) BETWEEN @DateFrom AND @DateTo)" _
                    & " AND " _
                    & "t0.pcchrgcod Like '" & txtSearch.Text & "%' " _
                    & "ORDER BY t0.dodate DESC, t7.wardname"

                GridView.Columns.Clear()
                GridView.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                GridView.Appearance.HeaderPanel.FontStyleDelta = FontStyle.Bold

                ConnectSQL()
                rsSQL.Connection = connSQL
                rsSQL.CommandType = CommandType.Text
                rsSQL.CommandText = SQL
                rsSQL.Parameters.Clear()
                rsSQL.Parameters.AddWithValue("@Section", procreslt)
                rsSQL.Parameters.Add("@DateFrom", SqlDbType.DateTime).Value = Format(dtFrom1.Value, "yyyy-MM-dd")
                rsSQL.Parameters.Add("@DateTo", SqlDbType.DateTime).Value = Format(dtTo1.Value, "yyyy-MM-dd")

                Dim adapter As New SqlDataAdapter(rsSQL)

                Dim myTable As DataTable = New DataTable
                adapter.Fill(myTable)

                dtList.DataSource = myTable

            End If
        End If
        'GridView.Columns("MainID").Visible = False
        'GridView.Columns("HISCode").Visible = True
        'GridView.Columns("Section").Visible = True
        'GridView.Columns("PatientType").Visible = False

        ' Make the grid read-only. 
        GridView.OptionsBehavior.Editable = False
        ' Prevent the focused cell from being highlighted. 
        GridView.OptionsSelection.EnableAppearanceFocusedCell = False
        ' Draw a dotted focus rectangle around the entire row. 
        GridView.FocusRectStyle = DrawFocusRectStyle.RowFullFocus

        GridView.OptionsSelection.MultiSelect = True
        GridView.OptionsSelection.MultiSelectMode = GridMultiSelectMode.CheckBoxRowSelect

        'GridView.Columns(1).DefaultCellStyle.WrapMode = DataGridViewTriState.True

        'column adjustments

        'Gridview_Column_Width()

        GridView.OptionsView.ColumnAutoWidth = False

        GridView.Columns("HospitalNo").Width = 90
        GridView.Columns("PatientName").Width = 200
        GridView.Columns("RequestingPhysician").Width = 200
        GridView.Columns("Test").Width = 200
        GridView.Columns("Ward").Width = 130
        GridView.Columns("Room").Width = 130
        GridView.Columns("Sex").Width = 30
        GridView.Columns("DateTimeRequested").Width = 130
        GridView.Columns("Location").Width = 60
        GridView.Columns("Priority").Width = 60
        GridView.Columns("HISCode").Width = 60
        GridView.Columns("Section").Width = 60

        DisconnectSQL()
        'Catch ex As Exception
        '    Disconnect()
        '    MessageBox.Show("Error in retrieving data from iHomis. " + ex.Message, "iHomis>LIS Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '    Exit Sub
        'End Try

    End Sub

    Private Sub frmNewOrder_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'dtFrom1.Format = DateTimePickerFormat.Custom
        'dtFrom1.CustomFormat = "yyyy/MM/dd"
        'dtTo1.Format = DateTimePickerFormat.Custom
        'dtTo1.CustomFormat = "yyyy/MM/dd"
        Disconnect()
        DisconnectSQL()
        'LoadRecordsiHomis()
        DisablePermission()
        'LoadRecordsOPD()
    End Sub

    Public Sub DisablePermission()

        Connect()
        rs.Connection = conn
        rs.CommandType = CommandType.Text
        rs.CommandText = "SELECT * FROM `user_permission` WHERE `user_id` = '" & CurrEmail & "'"
        reader = rs.ExecuteReader
        While reader.Read()

            If reader(2).ToString = "Add" Then
                If reader(3).ToString = 0 Then
                    Me.btnAdd.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                ElseIf reader(3).ToString = 1 Then
                    Me.btnAdd.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                End If
            End If

            If reader(2).ToString = "Reject Order" Then
                If reader(3).ToString = 0 Then
                    Me.btnCancel.Enabled = False
                ElseIf reader(3).ToString = 1 Then
                    Me.btnCancel.Enabled = True
                End If
            End If

            If reader(2).ToString = "Delete" Then
                If reader(3).ToString = 0 Then
                    Me.btnCancel.Enabled = False
                ElseIf reader(3).ToString = 1 Then
                    Me.btnCancel.Enabled = True
                End If
            End If

            If reader(2).ToString = "Re-Print Barcode" Then
                If reader(3).ToString = 0 Then
                    Me.btnReprint.Enabled = False
                ElseIf reader(3).ToString = 1 Then
                    Me.btnReprint.Enabled = True
                End If
            End If

        End While
        Disconnect()

    End Sub

    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.ItemClick

        If XTabPatient.SelectedTabPageIndex = 0 Then
            'qwer start
            GetLastID()
            'GetBDate()

            'MessageBox.Show(SampleID)

            Dim count As Integer = 0
            Dim count1 As Integer = 0
            Dim count2 As Integer = 0
            Dim count3 As Integer = 0
            Dim count4 As Integer = 0
            Dim count5 As Integer = 0

            If GridView.SelectedRowsCount = 0 Then
                MessageBox.Show("You must select a patient to continue.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If

            Dim selectedRows() As Integer = GridView.GetSelectedRows()
            For Each rowhandle As Integer In selectedRows
                If rowhandle >= 0 Then
                    'change GridView.Columns("HospitalNo")) to GridView.Columns("ChargeSlip")
                    If Not GridView.GetRowCellValue(rowhandle, GridView.Columns("HospitalNo")).ToString.Equals(GridView.GetRowCellValue(rowhandle - count, GridView.Columns("HospitalNo")).ToString) Then
                        MessageBox.Show("You have selected a different Hospital No.", "Incorrect Test", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        Exit Sub
                    End If
                    count = count + 1
                End If
            Next rowhandle

            For Each rowhandle1 As Integer In selectedRows
                If rowhandle1 >= 0 Then
                    If Not Format(CDate(GridView.GetRowCellValue(rowhandle1, GridView.Columns("DateTimeRequested"))), "MM/dd/yyyy").ToString.Equals(Format(CDate(GridView.GetRowCellValue(rowhandle1 - count1, GridView.Columns("DateTimeRequested"))), "MM/dd/yyyy").ToString) Then
                        MessageBox.Show("You have selected a different Date Request", "Incorrect Test", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        Exit Sub
                    End If
                    count1 = count1 + 1
                End If
            Next rowhandle1

            'condition for same chargeslip
            For Each rowhandle2 As Integer In selectedRows
                If rowhandle2 >= 0 Then
                    If Not GridView.GetRowCellValue(rowhandle2, GridView.Columns("ChargeSlip")).ToString.Equals(GridView.GetRowCellValue(rowhandle2 - count2, GridView.Columns("ChargeSlip")).ToString) Then
                        MessageBox.Show("You have selected a different ChargeSlip Request", "Incorrect Test", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        Exit Sub
                    End If
                    count2 = count2 + 1
                End If
            Next rowhandle2

            For x As Integer = 0 To GridView.SelectedRowsCount - 1 Step 1
                count3 = count3 + 1
            Next

            'MessageBox.Show(count4)

            For Each rowhandle4 As Integer In selectedRows
                If rowhandle4 >= 0 Then
                    If GridView.GetRowCellValue(rowhandle4, GridView.Columns("HISCode")).ToString.Equals("LABOR00286") Then
                        count4 = count4 + 1
                    End If

                End If
            Next rowhandle4

            'MessageBox.Show(count4)

            If count4 >= 1 And count3 > 1 Then
                MessageBox.Show("Blood Type can't be checked-in with other test(s). Check-in as a solo test.", "Incorrect Test", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If

            For Each rowhandle5 As Integer In selectedRows
                If rowhandle5 >= 0 Then
                    If Not GridView.GetRowCellValue(rowhandle5, GridView.Columns("Section")).ToString.Equals(GridView.GetRowCellValue(rowhandle5 - count5, GridView.Columns("Section")).ToString) Then
                        MessageBox.Show("Tests must have the same section", "Incorrect Test", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        Exit Sub
                    End If
                    count5 = count5 + 1
                End If
            Next rowhandle5

            'MessageBox.Show(count3)

            Dim selectedRows2() As Integer = GridView.GetSelectedRows()
            For Each rowHandle1 As Integer In selectedRows2
                If rowHandle1 >= 0 Then
                    Connect1()
                    rs1.Parameters.Clear()
                    rs1.Parameters.AddWithValue("@HISCode", GridView.GetRowCellValue(rowHandle1, GridView.Columns("HISCode")))
                    rs1.Connection = conn1
                    rs1.CommandType = CommandType.Text
                    rs1.CommandText = "SELECT `test_name` FROM `default_specimen` WHERE `his_code` = @HISCode"
                    reader1 = rs1.ExecuteReader
                    reader1.Read()
                    If reader1.HasRows Then
                        HISCode = reader1(0).ToString
                    End If
                    Disconnect1()
                    rs3.Parameters.Clear()

                    'If GridView.GetRowCellValue(rowHandle, GridView.Columns("Section")) = cboSection.Text Then
                    Test &= GridView.GetRowCellValue(rowHandle1, GridView.Columns("Test")) & ", " & vbCrLf

                    rs3.Parameters.AddWithValue("@Test", Test.TrimEnd(", ") & vbCrLf)

                End If
            Next
            Disconnect1()

            'MessageBox.Show(Test)

            Dim Result As DialogResult = MessageBox.Show("You're about to Check-In Patient " & GridView.GetFocusedRowCellValue(GridView.Columns("PatientName")) & "." & vbCrLf & vbCrLf & "Test(s): " & vbCrLf & Test & "" & vbCrLf & "Do you want to continue to print Barcode Sticker " & SampleID & "?", "System Message", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)
            Test = ""

            If (Result = DialogResult.Yes) Then
                Try
                    Connect1()
                    rs1.Parameters.Clear()
                    rs1.Parameters.AddWithValue("@HISCode", GridView.GetFocusedRowCellValue(GridView.Columns("HISCode")))
                    rs1.Connection = conn1
                    rs1.CommandType = CommandType.Text
                    rs1.CommandText = "SELECT `test_name` FROM `default_specimen` WHERE `his_code` = @HISCode"
                    reader1 = rs1.ExecuteReader
                    reader1.Read()
                    If reader1.HasRows Then
                        HISCode = reader1(0).ToString
                        Disconnect1()
                    End If
                    Disconnect1()

                    PrintBarcode(GridView.GetFocusedRowCellValue(GridView.Columns("Test")).ToString,
                                             SampleID,
                                             GridView.GetFocusedRowCellValue(GridView.Columns("HospitalNo")).ToString,
                                             GridView.GetFocusedRowCellValue(GridView.Columns("PatientName")).ToString,
                                             GridView.GetFocusedRowCellValue(GridView.Columns("DateOfBirth")).ToString,
                                             GridView.GetFocusedRowCellValue(GridView.Columns("Sex")).ToString,
                                             GridView.GetFocusedRowCellValue(GridView.Columns("Section")).ToString,
                                             HISCode,
                                             GridView.GetFocusedRowCellValue(GridView.Columns("Ward")).ToString,
                                             GridView.GetFocusedRowCellValue(GridView.Columns("Room")).ToString,
                                             CurrUsername)

                Catch ex As Exception
                    Disconnect()
                    MessageBox.Show("Error in connection on printer. " + ex.Message, "Barcode Printing Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End Try
                GoTo A
            ElseIf (Result = DialogResult.No) Then
                GoTo A
            ElseIf (Result = DialogResult.Cancel) Then
                Exit Sub
            End If

A:
            'asdf body
            lv.Items.Clear()
            lvList.Items.Clear()
            packagename()

            Dim selectedRows5() As Integer = GridView.GetSelectedRows()
            For Each rowHandle5 As Integer In selectedRows5
                If rowHandle5 >= 0 Then
                    Connect1()
                    rs1.Parameters.Clear()
                    rs1.Parameters.AddWithValue("@HISCode", GridView.GetRowCellValue(rowHandle5, GridView.Columns("HISCode")))
                    rs1.Connection = conn1
                    rs1.CommandType = CommandType.Text
                    rs1.CommandText = "SELECT `test_name` FROM `default_specimen` WHERE `his_code` = @HISCode"
                    reader1 = rs1.ExecuteReader
                    reader1.Read()
                    If reader1.HasRows Then
                        HISCode = reader1(0).ToString
                    End If
                    Disconnect1()
                    rs3.Parameters.Clear()

                    'If GridView.GetRowCellValue(rowHandle, GridView.Columns("Section")) = cboSection.Text Then
                    Test &= GridView.GetRowCellValue(rowHandle5, GridView.Columns("Test")) & ", "

                    txtTest.Text = Test

                    If txtTest.Text.Contains("'") Then
                        Test = txtTest.Text.Replace("'", "")
                    End If

                End If
            Next

            Dim selectedRows1() As Integer = GridView.GetSelectedRows()
            For Each rowHandle As Integer In selectedRows1
                If rowHandle >= 0 Then
                    'Connect1()
                    'rs1.Parameters.Clear()
                    'rs1.Parameters.AddWithValue("@HISCode", GridView.GetRowCellValue(rowHandle, GridView.Columns("HISCode")))
                    'rs1.Connection = conn1
                    'rs1.CommandType = CommandType.Text
                    'rs1.CommandText = "SELECT `test_name` FROM `default_specimen` WHERE `his_code` = @HISCode"
                    'reader1 = rs1.ExecuteReader
                    'reader1.Read()
                    'If reader1.HasRows Then
                    '    HISCode = reader1(0).ToString
                    'End If
                    'Disconnect1()
                    'rs3.Parameters.Clear()

                    'If GridView.GetRowCellValue(rowHandle, GridView.Columns("Section")) = cboSection.Text Then

                    'Test &= GridView.GetRowCellValue(rowHandle, GridView.Columns("Test")) & ", "

                    'IIf(IsNothing(GridView.GetRowCellValue(rowHandle, GridView.Columns("DateOfBirth"))), vbNull, Format(CDate(GridView.GetRowCellValue(rowHandle, GridView.Columns("DateOfBirth"))), "MM/dd/yyyy"))


                    'rs3.Parameters.AddWithValue("@CHARGESLIP", GridView.GetRowCellValue(rowHandle, GridView.Columns("ChargeSlip")))

                    'rs3.Parameters.AddWithValue("@sample_id", SampleID)
                    'rs3.Parameters.AddWithValue("@NAME", GridView.GetRowCellValue(rowHandle, GridView.Columns("PatientName")))

                    'rs3.Parameters.AddWithValue("@AGE", "")
                    'rs3.Parameters.AddWithValue("@bdate", Format(CDate(GridView.GetRowCellValue(rowHandle, GridView.Columns("DateOfBirth"))), "MM/dd/yyyy"))
                    'rs3.Parameters.AddWithValue("@PHYSICIAN", GridView.GetRowCellValue(rowHandle, GridView.Columns("RequestingPhysician")))
                    'rs3.Parameters.AddWithValue("@DATE", Format(CDate(GridView.GetRowCellValue(rowHandle, GridView.Columns("DateTimeRequested"))), "yyyy-MM-dd"))
                    'rs3.Parameters.AddWithValue("@TIME", Format(CDate(GridView.GetRowCellValue(rowHandle, GridView.Columns("DateTimeRequested"))), "hh:mm:ss tt"))
                    'rs3.Parameters.AddWithValue("@PATIENT_ID", GridView.GetRowCellValue(rowHandle, GridView.Columns("HospitalNo")))

                    'If GridView.GetRowCellValue(rowHandle, GridView.Columns("Location")) = "ER" Or GridView.GetRowCellValue(rowHandle, GridView.Columns("Location")) = "ERADM" Then

                    '    rs3.Parameters.AddWithValue("@dept", "ER Covid^")
                    'Else
                    '    rs3.Parameters.AddWithValue("@dept", GridView.GetRowCellValue(rowHandle, GridView.Columns("Ward")) & "^" & GridView.GetRowCellValue(rowHandle, GridView.Columns("Room")))
                    'End If

                    'rs3.Parameters.AddWithValue("@status", "Checked-In")
                    'rs3.Parameters.AddWithValue("@address", "")
                    'rs3.Parameters.AddWithValue("@contact", "")
                    'rs3.Parameters.AddWithValue("@Barcode", "")
                    ''rs3.Parameters.AddWithValue("@TestType", GridView.GetRowCellValue(rowHandle, GridView.Columns("Section")))

                    'rs3.Parameters.AddWithValue("@Test", Test.TrimEnd(", "))


                    'rs3.Parameters.AddWithValue("@TestType", GridView.GetRowCellValue(rowHandle, GridView.Columns("Section")))

                    'rs3.Parameters.AddWithValue("@Patient_Type", "In Patient")
                    'rs3.Parameters.AddWithValue("@TYPE", "")
                    'rs3.Parameters.AddWithValue("@location", GridView.GetRowCellValue(rowHandle, GridView.Columns("Location")))

                    'rs3.Parameters.AddWithValue("@MainID", SampleID)

                    'If GridView.GetRowCellValue(rowHandle, GridView.Columns("Sex")) = "M" Then
                    '    rs3.Parameters.AddWithValue("@sex", "Male")
                    'ElseIf GridView.GetRowCellValue(rowHandle, GridView.Columns("Sex")) = "F" Then
                    '    rs3.Parameters.AddWithValue("@sex", "Female")
                    'End If

                    'If GridView.GetRowCellValue(rowHandle, GridView.Columns("Section")) = "002" Then
                    'MessageBox.Show(GridView.GetRowCellValue(rowHandle, GridView.Columns("Section")))
                    'MessageBox.Show(cboSection.Text)
                    'MessageBox.Show(GridView.GetRowCellValue(rowHandle, GridView.Columns("Test")))


                    'Else

                    'End If
                End If

                For x As Integer = 0 To lv.Items.Count - 1 Step 1

                    rs3.Parameters.Clear()
                    rs3.Parameters.AddWithValue("@CHARGESLIP", GridView.GetRowCellValue(rowHandle, GridView.Columns("ChargeSlip")))

                    rs3.Parameters.AddWithValue("@sample_id", SampleID)
                    rs3.Parameters.AddWithValue("@NAME", GridView.GetRowCellValue(rowHandle, GridView.Columns("PatientName")))

                    rs3.Parameters.AddWithValue("@AGE", "")
                    rs3.Parameters.AddWithValue("@bdate", Format(CDate(GridView.GetRowCellValue(rowHandle, GridView.Columns("DateOfBirth"))), "MM/dd/yyyy"))
                    rs3.Parameters.AddWithValue("@PHYSICIAN", GridView.GetRowCellValue(rowHandle, GridView.Columns("RequestingPhysician")))
                    rs3.Parameters.AddWithValue("@DATE", Format(CDate(GridView.GetRowCellValue(rowHandle, GridView.Columns("DateTimeRequested"))), "yyyy-MM-dd"))
                    rs3.Parameters.AddWithValue("@TIME", Format(CDate(GridView.GetRowCellValue(rowHandle, GridView.Columns("DateTimeRequested"))), "hh:mm:ss tt"))
                    rs3.Parameters.AddWithValue("@PATIENT_ID", GridView.GetRowCellValue(rowHandle, GridView.Columns("HospitalNo")))

                    If GridView.GetRowCellValue(rowHandle, GridView.Columns("Location")) = "ER" Or GridView.GetRowCellValue(rowHandle, GridView.Columns("Location")) = "ERADM" Then

                        rs3.Parameters.AddWithValue("@dept", "ER Covid^")
                    Else
                        rs3.Parameters.AddWithValue("@dept", GridView.GetRowCellValue(rowHandle, GridView.Columns("Ward")) & "^" & GridView.GetRowCellValue(rowHandle, GridView.Columns("Room")))
                    End If

                    rs3.Parameters.AddWithValue("@status", "Checked-In")
                    rs3.Parameters.AddWithValue("@address", "")
                    rs3.Parameters.AddWithValue("@contact", "")
                    rs3.Parameters.AddWithValue("@Barcode", "")
                    'rs3.Parameters.AddWithValue("@TestType", GridView.GetRowCellValue(rowHandle, GridView.Columns("Section")))

                    rs3.Parameters.AddWithValue(txtTest.Text, Test.TrimEnd(", "))
                    If txtTest.Text.Contains("'") Then
                        txtTest.Text = txtTest.Text.Replace("'", "")
                    End If

                    'MessageBox.Show(txtTest.Text)
                    rs3.Parameters.AddWithValue("@Test", txtTest.Text)

                    'MessageBox.Show(txtTest.Text)
                    'rs3.Parameters.AddWithValue("@TestType", GridView.GetRowCellValue(rowHandle, GridView.Columns("Section")))
                    rs3.Parameters.AddWithValue("@TestType", lv.Items(x).SubItems(1).Text)

                    rs3.Parameters.AddWithValue("@Patient_Type", "In Patient")
                    rs3.Parameters.AddWithValue("@TYPE", "")
                    rs3.Parameters.AddWithValue("@location", GridView.GetRowCellValue(rowHandle, GridView.Columns("Location")))

                    rs3.Parameters.AddWithValue("@MainID", SampleID)

                    If GridView.GetRowCellValue(rowHandle, GridView.Columns("Sex")) = "M" Then
                        rs3.Parameters.AddWithValue("@sex", "Male")
                    ElseIf GridView.GetRowCellValue(rowHandle, GridView.Columns("Sex")) = "F" Then
                        rs3.Parameters.AddWithValue("@sex", "Female")
                    End If
                    rs3.Parameters.AddWithValue("@SubSection1", lv.Items(x).SubItems(2).Text)

                    Connect1()
                    rs1.Connection = conn1
                    rs1.CommandType = CommandType.Text
                    rs1.CommandText = "SELECT * FROM `patient_info` WHERE `patient_id` = '" & GridView.GetRowCellValue(rowHandle, GridView.Columns("HospitalNo")) & "'"
                    reader1 = rs1.ExecuteReader
                    If reader1.HasRows Then
                        Connect2()
                        rs2.Connection = conn2
                        rs2.CommandType = CommandType.Text
                        rs2.CommandText = "UPDATE `patient_info` SET `address` = '" & GridView.GetRowCellValue(rowHandle, GridView.Columns("Address")) & "' WHERE `patient_id` = '" & GridView.GetRowCellValue(rowHandle, GridView.Columns("HospitalNo")) & "'"
                        rs2.ExecuteNonQuery()
                        Disconnect2()
                        'MessageBox.Show(1)
                    Else
                        'Save Patient Order According to Number of Test in the list
                        Connect2()
                        rs2.Connection = conn2
                        rs2.CommandType = CommandType.Text
                        rs2.CommandText = "INSERT INTO `patient_info` (`address`, `patient_id`, `name`) VALUES (" _
                            & "'" & GridView.GetRowCellValue(rowHandle, GridView.Columns("Address")) & "'," _
                            & "'" & GridView.GetRowCellValue(rowHandle, GridView.Columns("HospitalNo")) & "'," _
                            & "'" & GridView.GetRowCellValue(rowHandle, GridView.Columns("PatientName")) & "')"
                        rs2.ExecuteNonQuery()
                        Disconnect2()
                        'MessageBox.Show(2)
                        'End of Patient Order
                    End If
                    Disconnect1()

                    'clinicalimpression
                    Connect1()
                    rs1.Connection = conn1
                    rs1.CommandType = CommandType.Text
                    rs1.CommandText = "SELECT `sample_id` FROM `patient_remarks` WHERE `sample_id` = '" & SampleID & "' AND `section` = '" & lv.Items(x).SubItems(1).Text & "' AND `sub_section` = '" & lv.Items(x).SubItems(2).Text & "' "
                    reader1 = rs1.ExecuteReader
                    If Not reader1.HasRows Then
                        Connect2()
                        rs2.Parameters.Clear()
                        rs2.Parameters.AddWithValue("@Clinical_Impression", GridView.GetRowCellValue(rowHandle, GridView.Columns("ClinicalImpression")))
                        rs2.Parameters.AddWithValue("@MainID", SampleID)
                        rs2.Parameters.AddWithValue("@TestType", lv.Items(x).SubItems(1).Text)
                        rs2.Parameters.AddWithValue("@SubSection", lv.Items(x).SubItems(2).Text)
                        rs2.Connection = conn2
                        rs2.CommandType = CommandType.Text
                        rs2.CommandText = "INSERT INTO `patient_remarks` (`diagnosis`,`sample_id`, `section`, `sub_section`) VALUES (@Clinical_Impression, @MainID, @TestType, @SubSection)"
                        rs2.ExecuteNonQuery()
                        Disconnect2()
                        'MessageBox.Show(3)
                    End If
                    Disconnect1()

                    'If lv.Items.Count > 1 Then

                    '    Connect3()
                    '    rs3.Connection = conn3
                    '    rs3.CommandType = CommandType.Text
                    '    rs3.CommandText = "INSERT INTO `tmpWorklist` (`sample_id`, `patient_id`, `patient_name`, `sex`, `bdate`, `age`, `physician`, `dept`, `status`, `main_id`, `date`, `time`, `barcode`, `testtype`, `sub_section`, `test`, `patient_type`, `TYPE`, `location`) VALUES " _
                    '            & "(" _
                    '            & "@sample_id," _
                    '            & "@PATIENT_ID," _
                    '            & "@NAME," _
                    '            & "@sex," _
                    '            & "@bdate," _
                    '            & "@AGE," _
                    '            & "@PHYSICIAN," _
                    '            & "@dept," _
                    '            & "@status," _
                    '            & "@MainID," _
                    '            & "@DATE," _
                    '            & "@time," _
                    '            & "@Barcode," _
                    '            & "@TestType," _
                    '            & "@SubSection1," _
                    '            & "@Test," _
                    '            & "@Patient_Type," _
                    '            & "@TYPE," _
                    '            & "@location" _
                    '            & ")"
                    '    rs3.ExecuteNonQuery()
                    '    Disconnect3()
                    'Else
                    Connect1()
                    rs1.Connection = conn1
                    rs1.CommandType = CommandType.Text
                    rs1.CommandText = "SELECT * FROM `tmpWorklist` WHERE `sample_id` = '" & SampleID & "' AND `testtype` = '" & lv.Items(x).SubItems(1).Text & "' AND sub_section = '" & lv.Items(x).SubItems(2).Text & "' "
                    reader1 = rs1.ExecuteReader
                    If Not reader1.HasRows Then

                        Connect3()
                        rs3.Connection = conn3
                        rs3.CommandType = CommandType.Text
                        rs3.CommandText = "INSERT INTO `tmpWorklist` (`sample_id`, `patient_id`, `patient_name`, `sex`, `bdate`, `age`, `physician`, `dept`, `status`, `main_id`, `date`, `time`, `barcode`, `testtype`, `sub_section`, `test`, `patient_type`, `TYPE`, `location`) VALUES " _
                            & "(" _
                            & "@sample_id," _
                            & "@PATIENT_ID," _
                            & "@NAME," _
                            & "@sex," _
                            & "@bdate," _
                            & "@AGE," _
                            & "@PHYSICIAN," _
                            & "@dept," _
                            & "@status," _
                            & "@MainID," _
                            & "@DATE," _
                            & "@time," _
                            & "@Barcode," _
                            & "@TestType," _
                            & "@SubSection1," _
                            & "@Test," _
                            & "@Patient_Type," _
                            & "@TYPE," _
                            & "@location" _
                            & ")"

                        'MessageBox.Show(4)
                        'rs3.Parameters.AddWithValue("@Test", GridView.GetRowCellValue(x))

                        'rs3.Parameters.AddWithValue("@Patient_Type", "In Patient")
                        '    rs3.Parameters.AddWithValue("@TYPE", "")
                        '    rs3.Parameters.AddWithValue("@location", GridView.GetRowCellValue(rowHandle, GridView.Columns("Location")))
                        'rs3.Parameters.AddWithValue("@SubSection1", lv.Items(x).SubItems(2).Text)

                        'rs3.Parameters.AddWithValue("@MainID", SampleID)

                        'If GridView.GetRowCellValue(rowHandle, GridView.Columns("Sex")) = "M" Then
                        '    rs3.Parameters.AddWithValue("@sex", "Male")
                        'ElseIf GridView.GetRowCellValue(rowHandle, GridView.Columns("Sex")) = "F" Then
                        '    rs3.Parameters.AddWithValue("@sex", "Female")
                        'End If
                        rs3.ExecuteNonQuery()
                        Disconnect3()
                    End If
                    Disconnect1()
                    Disconnect3()
                    'End If

                    Connect1()
                    rs1.Connection = conn1
                    rs1.CommandType = CommandType.Text
                    rs1.CommandText = "SELECT `sample_id` FROM `additional_info` WHERE `sample_id` = '" & SampleID & "' AND `section` = '" & lv.Items(x).SubItems(1).Text & "' AND sub_section = '" & lv.Items(x).SubItems(2).Text & "' "
                    reader1 = rs1.ExecuteReader
                    If Not reader1.HasRows Then
                        Connect2()
                        rs2.Parameters.Clear()
                        rs2.Parameters.AddWithValue("@CHARGESLIP", GridView.GetRowCellValue(rowHandle, GridView.Columns("ChargeSlip")))
                        rs2.Parameters.AddWithValue("@MainID", SampleID)
                        rs2.Parameters.AddWithValue("@SubSection", lv.Items(x).SubItems(2).Text)
                        rs2.Parameters.AddWithValue("@TestType", lv.Items(x).SubItems(1).Text)
                        rs2.Connection = conn2
                        rs2.CommandType = CommandType.Text
                        rs2.CommandText = "INSERT INTO `additional_info` (`sample_id`, `cs_no`, `section`, `sub_section`) VALUES (@MainID, @CHARGESLIP, @TestType, @SubSection)"
                        rs2.ExecuteNonQuery()
                        Disconnect2()
                        'MessageBox.Show(5)
                        'End of Patient Order
                    End If
                    Disconnect1()

                Next

                For y As Integer = 0 To lvOrder.Items.Count - 1 Step 1

                    'Find Record in SBSILIS Database that match the Test Code
                    Connect1()
                    rs1.Connection = conn1
                    rs1.CommandType = CommandType.Text
                    rs1.CommandText = "Select his_code, test_code, specimen, order_no, test_group, si_unit, conventional_unit, his_field, test_name FROM `default_specimen` WHERE `his_code` = '" & lvOrder.Items(y).SubItems(8).Text & "' AND `specimen` = '" & lvOrder.Items(y).SubItems(1).Text & "' AND `status` = 'Enable'"
                    reader1 = rs1.ExecuteReader
                    While reader1.Read()
                        If reader1.HasRows Then
                            HIS_TestCode = reader1(0).ToString
                            TestCode = reader1(1).ToString
                            Test_Name = reader1(2).ToString
                            Order_No = reader1(3).ToString
                            Test_Group = reader1(4).ToString
                            Unit = reader1(5).ToString
                            Unit_Conv = reader1(6).ToString
                            HIS_Field = reader1(7).ToString
                            Order_TestCode = reader1(8).ToString

                            If Test_Group.Contains("'") Then
                                Test_Group = Test_Group.Replace("'", "")
                            End If
                        End If
                    End While
                    Disconnect1()

                    Connect()
                    rs.Connection = conn
                    rs.CommandType = CommandType.Text
                    rs.CommandText = "SELECT * FROM `tmpResult` WHERE `sample_id` = '" & SampleID & "' And `test_code` = '" & lvOrder.Items(y).SubItems(2).Text & "' "
                    reader = rs.ExecuteReader
                    reader.Read()
                    If Not reader.HasRows Then
                        Disconnect()
                        Connect2()
                        rs2.Parameters.Clear()
                        rs2.Parameters.AddWithValue("@PATIENT_ID", GridView.GetRowCellValue(rowHandle, GridView.Columns("HospitalNo")))
                        rs2.Parameters.AddWithValue("@TestType", lvOrder.Items(y).SubItems(3).Text)
                        rs2.Parameters.AddWithValue("@HISMainID", lvOrder.Items(y).SubItems(7).Text)
                        rs2.Parameters.AddWithValue("@MainID", SampleID)
                        rs2.Parameters.AddWithValue("@SubSection", lvOrder.Items(y).SubItems(6).Text)

                        'txtGroup.Text = ""
                        'rs2.Parameters.AddWithValue(txtGroup.Text, Test_Group)
                        'If txtGroup.Text.Contains("'") Then
                        '    Test_Group = txtGroup.Text.Replace("'", "")
                        'End If

                        'MessageBox.Show(Test_Group)

                        'MessageBox.Show(lvOrder.Items(y).SubItems(1).Text & vbCrLf & lvOrder.Items(y).SubItems(2).Text)


                        rs2.Connection = conn2
                        'rs2.CommandType = CommandType.Text
                        rs2.CommandText = "INSERT INTO `tmpResult` (`universal_id`, `measurement`, `test_code`, `sample_id`, `date`, `patient_id`, `order_no`, `units`, `unit_conv`, `instrument`, `status`, `test_group`, `his_code`, `his_mainid`, `section`, `sub_section`, `accepted_result`) VALUES " _
                                        & "(" _
                                        & "'" & lvOrder.Items(y).SubItems(1).Text & "', '', '" & lvOrder.Items(y).SubItems(2).Text & "', @MainID, NOW(), @patient_id, '" & Order_No & "', '" & Unit & "', '" & Unit_Conv & "', 'Other_Test', 0, '" & Test_Group & "', '" & HIS_Field & "', @HISMainID, @TestType, @SubSection, 1" _
                                        & ")"


                        'MessageBox.Show(lvOrder.Items(y).SubItems(2).Text)
                        rs2.ExecuteNonQuery()
                        Disconnect2()
                    End If
                    Disconnect()
                Next

                'Check existing Patient Order
                For y As Integer = 0 To lv.Items.Count - 1 Step 1
                    'Connect1()
                    'rs1.Connection = conn1
                    'rs1.CommandType = CommandType.Text
                    'rs1.CommandText = "SELECT `sample_id`, `test_code` FROM `patient_order` WHERE `sample_id` = '" & SampleID & "' AND `test_code` = '" & Order_TestCode & "'"
                    'reader1 = rs1.ExecuteReader
                    'If reader1.HasRows Then

                    'Else
                    'Save Patient Order According to Number of Test in the list
                    Connect2()
                    rs2.Connection = conn2
                    rs2.CommandType = CommandType.Text
                    rs2.CommandText = "INSERT INTO `patient_order` (`test_name`, `test_code`, `patient_id`, `sample_id`, `testtype`, `mode`, `his_mainID`, `sub_section`) VALUES (" _
                            & "'" & Order_TestCode & "'," _
                            & "'" & Order_TestCode & "'," _
                            & "'" & GridView.GetRowCellValue(rowHandle, GridView.Columns("HospitalNo")) & "'," _
                            & "'" & SampleID & "'," _
                            & "'" & lv.Items(y).SubItems(1).Text & "'," _
                            & "'" & GridView.GetRowCellValue(rowHandle, GridView.Columns("Priority")) & "'," _
                            & "'" & GridView.GetRowCellValue(rowHandle, GridView.Columns("MainID")) & "'," _
                            & "'" & lv.Items(y).SubItems(2).Text & "')"

                    rs2.ExecuteNonQuery()
                    Disconnect2()
                    'End of Patient Order
                    'End If
                    'Disconnect1()
                Next
                'End of Find

                'UpdateRecordwthoutMSG("UPDATE `patient_info` SET `address` = '" & GridView.GetRowCellValue(rowHandle, GridView.Columns("Address")) & "' WHERE `patient_id` = '" & GridView.GetRowCellValue(rowHandle, GridView.Columns("HospitalNo")) & "'")

                For x As Integer = 0 To lv.Items.Count - 1 Step 1
                    'Check existing Specimen Tracking
                    Connect1()
                    rs1.Connection = conn1
                    rs1.CommandType = CommandType.Text
                    rs1.CommandText = "SELECT * FROM `specimen_tracking` WHERE sample_id = '" & SampleID & "' AND section = '" & lv.Items(x).SubItems(1).Text & "' AND sub_section = '" & lv.Items(x).SubItems(2).Text & "'"
                    reader1 = rs1.ExecuteReader
                    reader1.Read()
                    If reader1.HasRows Then

                    Else
                        Connect2()
                        rs2.Parameters.Clear()
                        rs2.Parameters.AddWithValue("@DateReceived", Format(CDate(GridView.GetRowCellValue(rowHandle, GridView.Columns("DateTimeRequested"))), "yyyy-MM-dd hh:mm:ss tt"))
                        rs2.Parameters.AddWithValue("@DateCheckedIn", Format(CDate(Now), "yyyy-MM-dd HH:mm:ss"))
                        rs2.Connection = conn2
                        rs2.CommandType = CommandType.Text
                        rs2.CommandText = "INSERT INTO `specimen_tracking` (`sample_id`, `received`, `latest`, `section`, `sub_section`) VALUES ('" & SampleID & "', @DateReceived, @DateCheckedIn, '" & lv.Items(x).SubItems(1).Text & "', '" & lv.Items(x).SubItems(2).Text & "')"
                        rs2.ExecuteNonQuery()
                        Disconnect2()
                    End If
                    Disconnect1()
                Next

                'Update iHOMIS 'estatus' to 'S'
                Dim SQL_Update = "UPDATE hdocord SET estatus = 'S' WHERE docointkey = '" & GridView.GetRowCellValue(rowHandle, GridView.Columns("MainID")) & "'"
                ConnectSQL()
                rsSQL.Connection = connSQL
                rsSQL.CommandType = CommandType.Text
                rsSQL.CommandText = SQL_Update
                rsSQL.ExecuteNonQuery()
                DisconnectSQL()
                'end update iHOMIS.

            Next

            addLastID()
            'GridView.FocusedRowHandle = -1
            MessageBox.Show("Patient order successfully Checked-In", "Check-In", MessageBoxButtons.OK, MessageBoxIcon.Information)
            'RefreshForm()

            'Dim myform As New frmiHOMIS()
            'Me.Close()
            'myform.WindowState = FormWindowState.Maximized
            'myform.MdiParent = MainFOrm
            'myform.Show()
            'LoadRecordsiHomis()

            ActivityLogs(SampleID,
                             GridView.GetFocusedRowCellValue(GridView.Columns("HospitalNo")).ToString,
                             GridView.GetFocusedRowCellValue(GridView.Columns("PatientName")).ToString,
                             CurrUser,
                             "Checked In",
                             Test.TrimEnd(", "),
                             Section,
                             SubSection)


            Test = ""
            HISCode = ""
            LoadRecordsiHomis()
            'zxcv end

            'start of btnadd for outpatient
        ElseIf XTabPatient.SelectedTabPageIndex = 1 Then

            GetLastID()
            'GetBDate()

            'MessageBox.Show(SampleID)

            Dim count As Integer = 0
            Dim count1 As Integer = 0
            Dim count2 As Integer = 0
            Dim count3 As Integer = 0
            Dim count4 As Integer = 0
            Dim count5 As Integer = 0

            If GridView1.SelectedRowsCount = 0 Then
                MessageBox.Show("You must select a patient to continue.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If

            Dim selectedRows() As Integer = GridView1.GetSelectedRows()
            For Each rowhandle As Integer In selectedRows
                If rowhandle >= 0 Then
                    'MessageBox.Show(GridView1.GetRowCellValue(rowhandle, GridView1.Columns("HospitalNo")).ToString)
                    'change Gridview1.Columns("HospitalNo")) to Gridview1.Columns("ChargeSlip")
                    If Not GridView1.GetRowCellValue(rowhandle, GridView1.Columns("HospitalNo")).ToString.Equals(GridView1.GetRowCellValue(rowhandle - count, GridView1.Columns("HospitalNo")).ToString) Then
                        MessageBox.Show("You have selected a different Hospital No.", "Incorrect Test", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        Exit Sub
                    End If
                    count = count + 1
                End If

            Next rowhandle

            For Each rowhandle1 As Integer In selectedRows
                If rowhandle1 >= 0 Then
                    If Not Format(CDate(GridView1.GetRowCellValue(rowhandle1, GridView1.Columns("DateTimeRequested"))), "MM/dd/yyyy").ToString.Equals(Format(CDate(GridView1.GetRowCellValue(rowhandle1 - count1, GridView1.Columns("DateTimeRequested"))), "MM/dd/yyyy").ToString) Then
                        MessageBox.Show("You have selected a different Date Request", "Incorrect Test", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        Exit Sub
                    End If
                    count1 = count1 + 1
                End If
            Next rowhandle1

            'condition for same chargeslip
            For Each rowhandle2 As Integer In selectedRows
                If rowhandle2 >= 0 Then
                    If Not GridView1.GetRowCellValue(rowhandle2, GridView1.Columns("ChargeSlip")).ToString.Equals(GridView1.GetRowCellValue(rowhandle2 - count2, GridView1.Columns("ChargeSlip")).ToString) Then
                        MessageBox.Show("You have selected a different ChargeSlip Request", "Incorrect Test", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        Exit Sub
                    End If
                    count2 = count2 + 1
                End If
            Next rowhandle2

            For x As Integer = 0 To GridView1.SelectedRowsCount - 1 Step 1
                count3 = count3 + 1
            Next

            'MessageBox.Show(count4)

            For Each rowhandle4 As Integer In selectedRows
                If rowhandle4 >= 0 Then
                    If GridView1.GetRowCellValue(rowhandle4, GridView1.Columns("Proccode")).ToString.Equals("LABOR00286") Then
                        count4 = count4 + 1
                    End If

                End If
            Next rowhandle4

            'MessageBox.Show(count4)

            If count4 >= 1 And count3 > 1 Then
                MessageBox.Show("Blood Type can't be checked-in with other test(s). Check-in as a solo test.", "Incorrect Test", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If

            For Each rowhandle5 As Integer In selectedRows
                If rowhandle5 >= 0 Then
                    If Not GridView1.GetRowCellValue(rowhandle5, GridView1.Columns("Section")).ToString.Equals(GridView1.GetRowCellValue(rowhandle5 - count5, GridView1.Columns("Section")).ToString) Then
                        MessageBox.Show("Tests must have the same section", "Incorrect Test", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        Exit Sub
                    End If
                    count5 = count5 + 1
                End If
            Next rowhandle5

            'MessageBox.Show(count3)

            Dim selectedRows2() As Integer = GridView1.GetSelectedRows()
            For Each rowHandle1 As Integer In selectedRows2
                If rowHandle1 >= 0 Then
                    Connect1()
                    rs1.Parameters.Clear()
                    rs1.Parameters.AddWithValue("@HISCode", GridView1.GetRowCellValue(rowHandle1, GridView1.Columns("Proccode")))
                    rs1.Connection = conn1
                    rs1.CommandType = CommandType.Text
                    rs1.CommandText = "SELECT `test_name` FROM `default_specimen` WHERE `his_code` = @HISCode"
                    reader1 = rs1.ExecuteReader
                    reader1.Read()
                    If reader1.HasRows Then
                        HISCode2 = reader1(0).ToString
                        Disconnect1()
                    End If
                    Disconnect1()
                    rs3.Parameters.Clear()

                    'If Gridview1.GetRowCellValue(rowHandle, Gridview1.Columns("Section")) = cboSection.Text Then
                    Test &= GridView1.GetRowCellValue(rowHandle1, GridView1.Columns("Test")) & ", " & vbCrLf

                    rs3.Parameters.AddWithValue("@Test", Test.TrimEnd(", ") & vbCrLf)

                End If
            Next
            Disconnect1()

            'MessageBox.Show(Test)

            Dim Result As DialogResult = MessageBox.Show("You're about to Check-In Patient " & GridView1.GetFocusedRowCellValue(GridView1.Columns("PatientName")) & "." & vbCrLf & vbCrLf & "Test(s): " & vbCrLf & Test & "" & vbCrLf & "Do you want to continue to print Barcode Sticker " & SampleID & "?", "System Message", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)
            Test = ""

            If (Result = DialogResult.Yes) Then
                Try
                    Connect1()
                    rs1.Parameters.Clear()
                    rs1.Parameters.AddWithValue("@HISCode", GridView1.GetFocusedRowCellValue(GridView1.Columns("Proccode")))
                    rs1.Connection = conn1
                    rs1.CommandType = CommandType.Text
                    rs1.CommandText = "SELECT `test_name` FROM `default_specimen` WHERE `his_code` = @HISCode"
                    reader1 = rs1.ExecuteReader
                    reader1.Read()
                    If reader1.HasRows Then
                        HISCode2 = reader1(0).ToString
                        Disconnect1()
                    End If
                    Disconnect1()
                    'MessageBox.Show(HISCode2)
                    PrintBarcode(GridView1.GetFocusedRowCellValue(GridView1.Columns("Test")).ToString,
                                                 SampleID,
                                                 GridView1.GetFocusedRowCellValue(GridView1.Columns("HospitalNo")).ToString,
                                                 GridView1.GetFocusedRowCellValue(GridView1.Columns("PatientName")).ToString,
                                                 GridView1.GetFocusedRowCellValue(GridView1.Columns("DateOfBirth")).ToString,
                                                 GridView1.GetFocusedRowCellValue(GridView1.Columns("Sex")).ToString,
                                                 GridView1.GetFocusedRowCellValue(GridView1.Columns("Section")).ToString,
                                                 HISCode2,
                                                 GridView1.GetFocusedRowCellValue(GridView1.Columns("Ward")).ToString,
                                                 "",
                                                 CurrUsername)

                Catch ex As Exception
                    Disconnect()
                    MessageBox.Show("Error in connection on printer. " + ex.Message, "Barcode Printing Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End Try
                GoTo B
            ElseIf (Result = DialogResult.No) Then
                GoTo B
            ElseIf (Result = DialogResult.Cancel) Then
                Exit Sub
            End If
B:
            'asdf body
            lv2.Items.Clear()
            lvList2.Items.Clear()
            packagenameOPD()

            Dim selectedRows5() As Integer = GridView1.GetSelectedRows()
            For Each rowHandle5 As Integer In selectedRows5
                If rowHandle5 >= 0 Then
                    Connect1()
                    rs1.Parameters.Clear()
                    rs1.Parameters.AddWithValue("@HISCode", GridView1.GetRowCellValue(rowHandle5, GridView1.Columns("HISCode")))
                    rs1.Connection = conn1
                    rs1.CommandType = CommandType.Text
                    rs1.CommandText = "SELECT `test_name` FROM `default_specimen` WHERE `his_code` = @HISCode"
                    reader1 = rs1.ExecuteReader
                    reader1.Read()
                    If reader1.HasRows Then
                        HISCode = reader1(0).ToString
                    End If
                    Disconnect1()
                    rs3.Parameters.Clear()

                    'If Gridview1.GetRowCellValue(rowHandle, Gridview1.Columns("Section")) = cboSection.Text Then
                    Test &= GridView1.GetRowCellValue(rowHandle5, GridView1.Columns("Test")) & ", "

                    txtTest2.Text = Test

                    If txtTest2.Text.Contains("'") Then
                        Test = txtTest2.Text.Replace("'", "")
                    End If

                End If
            Next

            Dim selectedRows1() As Integer = GridView1.GetSelectedRows()
            For Each rowHandle As Integer In selectedRows1

                For x As Integer = 0 To lv2.Items.Count - 1 Step 1

                    rs3.Parameters.Clear()
                    rs3.Parameters.AddWithValue("@CHARGESLIP", GridView1.GetRowCellValue(rowHandle, GridView1.Columns("ChargeSlip")))

                    rs3.Parameters.AddWithValue("@sample_id", SampleID)
                    rs3.Parameters.AddWithValue("@NAME", GridView1.GetRowCellValue(rowHandle, GridView1.Columns("PatientName")))

                    rs3.Parameters.AddWithValue("@AGE", "")
                    rs3.Parameters.AddWithValue("@bdate", Format(CDate(GridView1.GetRowCellValue(rowHandle, GridView1.Columns("DateOfBirth"))), "MM/dd/yyyy"))
                    'rs3.Parameters.AddWithValue("@PHYSICIAN", GridView1.GetRowCellValue(rowHandle, GridView1.Columns("RequestingPhysician")))
                    rs3.Parameters.AddWithValue("@DATE", Format(CDate(GridView1.GetRowCellValue(rowHandle, GridView1.Columns("DateTimeRequested"))), "yyyy-MM-dd"))
                    rs3.Parameters.AddWithValue("@TIME", Format(CDate(GridView1.GetRowCellValue(rowHandle, GridView1.Columns("DateTimeRequested"))), "hh:mm:ss tt"))
                    rs3.Parameters.AddWithValue("@PATIENT_ID", GridView1.GetRowCellValue(rowHandle, GridView1.Columns("HospitalNo")))

                    If GridView1.GetRowCellValue(rowHandle, GridView1.Columns("Location")) = "ER" Or GridView1.GetRowCellValue(rowHandle, GridView1.Columns("Location")) = "ERADM" Then

                        rs3.Parameters.AddWithValue("@dept", "ER Covid^")
                    Else
                        rs3.Parameters.AddWithValue("@dept", GridView1.GetRowCellValue(rowHandle, GridView1.Columns("Ward")) & "^" & GridView1.GetRowCellValue(rowHandle, GridView1.Columns("Room")))
                    End If

                    rs3.Parameters.AddWithValue("@status", "Checked-In")
                    rs3.Parameters.AddWithValue("@address", "")
                    rs3.Parameters.AddWithValue("@contact", "")
                    rs3.Parameters.AddWithValue("@Barcode", "")
                    'rs3.Parameters.AddWithValue("@TestType", Gridview1.GetRowCellValue(rowHandle, Gridview1.Columns("Section")))

                    rs3.Parameters.AddWithValue(txtTest2.Text, Test.TrimEnd(", "))
                    If txtTest2.Text.Contains("'") Then
                        txtTest2.Text = txtTest2.Text.Replace("'", "")
                    End If

                    'MessageBox.Show(txtTest.Text)
                    rs3.Parameters.AddWithValue("@Test", txtTest2.Text)

                    'MessageBox.Show(txtTest.Text)
                    'rs3.Parameters.AddWithValue("@TestType", Gridview1.GetRowCellValue(rowHandle, Gridview1.Columns("Section")))
                    rs3.Parameters.AddWithValue("@TestType", lv2.Items(x).SubItems(1).Text)

                    rs3.Parameters.AddWithValue("@Patient_Type", "Out Patient")
                    rs3.Parameters.AddWithValue("@TYPE", "")
                    rs3.Parameters.AddWithValue("@location", GridView1.GetRowCellValue(rowHandle, GridView1.Columns("Location")))

                    rs3.Parameters.AddWithValue("@MainID", SampleID)

                    If GridView1.GetRowCellValue(rowHandle, GridView1.Columns("Sex")) = "M" Then
                        rs3.Parameters.AddWithValue("@sex", "Male")
                    ElseIf GridView1.GetRowCellValue(rowHandle, GridView1.Columns("Sex")) = "F" Then
                        rs3.Parameters.AddWithValue("@sex", "Female")
                    End If
                    rs3.Parameters.AddWithValue("@SubSection1", lv2.Items(x).SubItems(2).Text)

                    Connect1()
                    rs1.Connection = conn1
                    rs1.CommandType = CommandType.Text
                    rs1.CommandText = "SELECT * FROM `patient_info` WHERE `patient_id` = '" & GridView1.GetRowCellValue(rowHandle, GridView1.Columns("HospitalNo")) & "'"
                    reader1 = rs1.ExecuteReader
                    If reader1.HasRows Then
                        Connect2()
                        rs2.Connection = conn2
                        rs2.CommandType = CommandType.Text
                        rs2.CommandText = ("UPDATE `patient_info` SET " _
                                    & "`sample_id` = '" & SampleID & "'," _
                                    & "`name` = '" & GridView1.GetRowCellValue(rowHandle, GridView1.Columns("PatientName")) & "'," _
                                    & "`sex` = '" & GridView1.GetRowCellValue(rowHandle, GridView1.Columns("Sex")) & "'," _
                                    & "`date_of_birth` = '" & GridView1.GetRowCellValue(rowHandle, GridView1.Columns("DateOfBirth")) & "'," _
                                    & "`date` = '" & Format(CDate(GridView1.GetRowCellValue(rowHandle, GridView1.Columns("DateTimeRequested"))), "yyyy-MM-dd") & "'" _
                                    & " WHERE `patient_id` = '" & GridView1.GetRowCellValue(rowHandle, GridView1.Columns("HospitalNo")) & "'")
                        'rs2.CommandText = "UPDATE `patient_info` SET `address` = '" & GridView1.GetRowCellValue(rowHandle, GridView1.Columns("Address")) & "' WHERE `patient_id` = '" & GridView1.GetRowCellValue(rowHandle, GridView1.Columns("HospitalNo")) & "'"
                        rs2.ExecuteNonQuery()
                        Disconnect2()
                        'MessageBox.Show(1)
                    Else
                        'Save Patient Order According to Number of Test in the list
                        Connect2()
                        rs2.Connection = conn2
                        rs2.CommandType = CommandType.Text
                        'rs2.CommandText = "INSERT INTO `patient_info` (`address`, `patient_id`, `name`) VALUES (" _
                        '    & "'" & GridView1.GetRowCellValue(rowHandle, GridView1.Columns("Address")) & "'," _
                        '    & "'" & GridView1.GetRowCellValue(rowHandle, GridView1.Columns("HospitalNo")) & "'," _
                        '    & "'" & GridView1.GetRowCellValue(rowHandle, GridView1.Columns("PatientName")) & "')"
                        rs2.CommandText = ("INSERT INTO patient_info (sample_id, patient_id, name, sex, date_of_birth, `date`) VALUES " _
                                    & "(" _
                                    & "`sample_id` = '" & SampleID & "'," _
                                    & "`patient_id` = '" & GridView1.GetRowCellValue(rowHandle, GridView1.Columns("HospitalNo")) & "'," _
                                    & "`name` = '" & GridView1.GetRowCellValue(rowHandle, GridView1.Columns("PatientName")) & "'," _
                                    & "`sex` = '" & GridView1.GetRowCellValue(rowHandle, GridView1.Columns("Sex")) & "'," _
                                    & "`date_of_birth` = '" & GridView1.GetRowCellValue(rowHandle, GridView1.Columns("DateOfBirth")) & "'," _
                                    & "`date` = '" & Format(CDate(GridView1.GetRowCellValue(rowHandle, GridView1.Columns("DateTimeRequested"))), "yyyy-MM-dd") & "'" _
                                    & ")"
                                    )
                        'MessageBox.Show(2)
                        'End of Patient Order
                        Disconnect2()
                    End If
                    Disconnect1()

                    'clinicalimpression
                    Connect1()
                    rs1.Connection = conn1
                    rs1.CommandType = CommandType.Text
                    rs1.CommandText = "SELECT `sample_id` FROM `patient_remarks` WHERE `sample_id` = '" & SampleID & "' AND `section` = '" & lv2.Items(x).SubItems(1).Text & "' AND `sub_section` = '" & lv2.Items(x).SubItems(2).Text & "' "
                    reader1 = rs1.ExecuteReader
                    If Not reader1.HasRows Then
                        Connect2()
                        rs2.Parameters.Clear()
                        'rs2.Parameters.AddWithValue("@Clinical_Impression", GridView1.GetRowCellValue(rowHandle, GridView1.Columns("ClinicalImpression")))
                        rs2.Parameters.AddWithValue("@MainID", SampleID)
                        rs2.Parameters.AddWithValue("@TestType", lv2.Items(x).SubItems(1).Text)
                        rs2.Parameters.AddWithValue("@SubSection", lv2.Items(x).SubItems(2).Text)
                        rs2.Connection = conn2
                        rs2.CommandType = CommandType.Text
                        rs2.CommandText = "INSERT INTO `patient_remarks` (`sample_id`, `section`, `sub_section`) VALUES (@MainID, @TestType, @SubSection)"
                        rs2.ExecuteNonQuery()
                        Disconnect2()
                        'MessageBox.Show(3)
                    End If
                    Disconnect1()

                    Connect1()
                    rs1.Connection = conn1
                    rs1.CommandType = CommandType.Text
                    rs1.CommandText = "SELECT * FROM `tmpWorklist` WHERE `sample_id` = '" & SampleID & "' AND `testtype` = '" & lv2.Items(x).SubItems(1).Text & "' AND sub_section = '" & lv2.Items(x).SubItems(2).Text & "' "
                    reader1 = rs1.ExecuteReader
                    If Not reader1.HasRows Then
                        Connect3()
                        rs3.Connection = conn3
                        rs3.CommandType = CommandType.Text
                        rs3.CommandText = "INSERT INTO `tmpWorklist` (`sample_id`, `patient_id`, `patient_name`, `sex`, `bdate`, `age`, `dept`, `status`, `main_id`, `date`, `time`, `barcode`, `testtype`, `sub_section`, `test`, `patient_type`, `TYPE`, `location`) VALUES " _
                            & "(" _
                            & "@sample_id," _
                            & "@PATIENT_ID," _
                            & "@NAME," _
                            & "@sex," _
                            & "@bdate," _
                            & "@AGE," _
                            & "@dept," _
                            & "@status," _
                            & "@MainID," _
                            & "@DATE," _
                            & "@time," _
                            & "@Barcode," _
                            & "@TestType," _
                            & "@SubSection1," _
                            & "@Test," _
                            & "@Patient_Type," _
                            & "@TYPE," _
                            & "@location" _
                            & ")"


                        rs3.ExecuteNonQuery()
                        Disconnect3()
                    End If
                    Disconnect1()
                    Disconnect3()

                    Connect1()
                    rs1.Connection = conn1
                    rs1.CommandType = CommandType.Text
                    rs1.CommandText = "SELECT `sample_id` FROM `additional_info` WHERE `sample_id` = '" & SampleID & "' AND `section` = '" & lv2.Items(x).SubItems(1).Text & "' AND sub_section = '" & lv2.Items(x).SubItems(2).Text & "' "
                    reader1 = rs1.ExecuteReader
                    If Not reader1.HasRows Then
                        Connect2()
                        rs2.Parameters.Clear()
                        rs2.Parameters.AddWithValue("@CHARGESLIP", GridView1.GetRowCellValue(rowHandle, GridView1.Columns("ChargeSlip")))
                        rs2.Parameters.AddWithValue("@MainID", SampleID)
                        rs2.Parameters.AddWithValue("@SubSection", lv2.Items(x).SubItems(2).Text)
                        rs2.Parameters.AddWithValue("@TestType", lv2.Items(x).SubItems(1).Text)
                        rs2.Connection = conn2
                        rs2.CommandType = CommandType.Text
                        rs2.CommandText = "INSERT INTO `additional_info` (`sample_id`, `cs_no`, `section`, `sub_section`) VALUES (@MainID, @CHARGESLIP, @TestType, @SubSection)"
                        rs2.ExecuteNonQuery()
                        Disconnect2()
                        'MessageBox.Show(5)
                        'End of Patient Order
                    End If
                    Disconnect1()

                Next

                For y As Integer = 0 To lvOrder2.Items.Count - 1 Step 1
                    'MessageBox.Show(lvOrder2.Items(y).SubItems(0).Text)
                    'MessageBox.Show(lvOrder2.Items(y).SubItems(1).Text)
                    'MessageBox.Show(lvOrder2.Items(y).SubItems(2).Text)
                    'MessageBox.Show(lvOrder2.Items(y).SubItems(3).Text)
                    'MessageBox.Show(lvOrder2.Items(y).SubItems(4).Text)
                    'MessageBox.Show(lvOrder2.Items(y).SubItems(5).Text)
                    'MessageBox.Show(lvOrder2.Items(y).SubItems(6).Text)
                    'MessageBox.Show(lvOrder2.Items(y).SubItems(7).Text)
                    'MessageBox.Show(lvOrder2.Items(y).SubItems(8).Text)
                    'MessageBox.Show(lvOrder2.Items(y).SubItems(9).Text)
                    'MessageBox.Show(lvOrder2.Items(y).SubItems(10).Text)

                    'Find Record in SBSILIS Database that match the Test Code
                    Connect1()
                    rs1.Connection = conn1
                    rs1.CommandType = CommandType.Text
                    rs1.CommandText = "Select his_code, test_code, specimen, order_no, test_group, si_unit, conventional_unit, his_field, test_name FROM `default_specimen` WHERE `his_code` = '" & lvOrder2.Items(y).SubItems(10).Text & "' AND `specimen` = '" & lvOrder2.Items(y).SubItems(1).Text & "' AND `status` = 'Enable'"
                    reader1 = rs1.ExecuteReader
                    While reader1.Read()
                        If reader1.HasRows Then
                            HIS_TestCode2 = reader1(0).ToString
                            TestCode = reader1(1).ToString
                            Test_Name = reader1(2).ToString
                            Order_No2 = reader1(3).ToString
                            Test_Group = reader1(4).ToString
                            Unit2 = reader1(5).ToString
                            Unit_Conv2 = reader1(6).ToString
                            HIS_Field = reader1(7).ToString
                            Order_TestCode = reader1(8).ToString

                            If Test_Group.Contains("'") Then
                                Test_Group = Test_Group.Replace("'", "")
                            End If
                        End If
                    End While
                    Disconnect1()

                    Connect()
                    rs.Connection = conn
                    rs.CommandType = CommandType.Text
                    rs.CommandText = "SELECT * FROM `tmpResult` WHERE `sample_id` = '" & SampleID & "' And `test_code` = '" & lvOrder2.Items(y).SubItems(2).Text & "' "
                    reader = rs.ExecuteReader
                    reader.Read()
                    If Not reader.HasRows Then
                        Connect2()
                        rs2.Parameters.Clear()
                        rs2.Parameters.AddWithValue("@PATIENT_ID", GridView1.GetRowCellValue(rowHandle, GridView1.Columns("HospitalNo")))
                        rs2.Parameters.AddWithValue("@TestType", lvOrder2.Items(y).SubItems(3).Text)
                        rs2.Parameters.AddWithValue("@HISMainID", lvOrder2.Items(y).SubItems(7).Text)
                        rs2.Parameters.AddWithValue("@MainID", SampleID)
                        rs2.Parameters.AddWithValue("@SubSection", lvOrder2.Items(y).SubItems(6).Text)


                        'zzz
                        rs2.Connection = conn2
                        'rs2.CommandType = CommandType.Text
                        rs2.CommandText = "INSERT INTO `tmpResult` (`universal_id`, `measurement`, `test_code`, `sample_id`, `date`, `patient_id`, `order_no`, `units`, `unit_conv`, `instrument`, `status`, `test_group`, `his_code`, `his_mainid`, `section`, `sub_section`, `accepted_result`) VALUES " _
                                        & "(" _
                                        & "'" & lvOrder2.Items(y).SubItems(1).Text & "', '', '" & lvOrder2.Items(y).SubItems(2).Text & "', @MainID, NOW(), @patient_id, '" & Order_No2 & "', '" & Unit2 & "', '" & Unit_Conv2 & "', 'Other_Test', 0, '" & lvOrder2.Items(y).SubItems(9).Text & "', '" & lvOrder2.Items(y).SubItems(7).Text & "', '', @TestType, @SubSection, 1" _
                                        & ")"


                        'MessageBox.Show(lvOrder.Items(y).SubItems(2).Text)
                        rs2.ExecuteNonQuery()
                        Disconnect2()
                    End If
                    Disconnect()
                Next

                'Check existing Patient Order
                For y As Integer = 0 To lv2.Items.Count - 1 Step 1
                    '
                    'Save Patient Order According to Number of Test in the list
                    Connect2()
                    rs2.Connection = conn2
                    rs2.CommandType = CommandType.Text
                    rs2.CommandText = "INSERT INTO `patient_order` (`test_name`, `test_code`, `patient_id`, `sample_id`, `testtype`, `mode`, `his_mainID`, `sub_section`) VALUES (" _
                            & "'" & Order_TestCode & "'," _
                            & "'" & Order_TestCode & "'," _
                            & "'" & GridView1.GetRowCellValue(rowHandle, GridView1.Columns("HospitalNo")) & "'," _
                            & "'" & SampleID & "'," _
                            & "'" & lv2.Items(y).SubItems(1).Text & "'," _
                            & "'" & GridView1.GetRowCellValue(rowHandle, GridView1.Columns("Priority")) & "'," _
                            & "'" & GridView1.GetRowCellValue(rowHandle, GridView1.Columns("MainID")) & "'," _
                            & "'" & lv2.Items(y).SubItems(2).Text & "')"

                    rs2.ExecuteNonQuery()
                    Disconnect2()
                    'End of Patient Order
                    'End If
                    'Disconnect1()
                Next
                'End of Find

                For x As Integer = 0 To lv2.Items.Count - 1 Step 1
                    'Check existing Specimen Tracking
                    Connect1()
                    rs1.Connection = conn1
                    rs1.CommandType = CommandType.Text
                    rs1.CommandText = "SELECT * FROM `specimen_tracking` WHERE sample_id = '" & SampleID & "' AND section = '" & lv2.Items(x).SubItems(1).Text & "' AND sub_section = '" & lv2.Items(x).SubItems(2).Text & "'"
                    reader1 = rs1.ExecuteReader
                    reader1.Read()
                    If reader1.HasRows Then

                    Else
                        Connect2()
                        rs2.Parameters.Clear()
                        rs2.Parameters.AddWithValue("@DateReceived", Format(CDate(GridView1.GetRowCellValue(rowHandle, GridView1.Columns("DateTimeRequested"))), "yyyy-MM-dd hh:mm:ss tt"))
                        rs2.Parameters.AddWithValue("@DateCheckedIn", Format(CDate(Now), "yyyy-MM-dd HH:mm:ss"))
                        rs2.Connection = conn2
                        rs2.CommandType = CommandType.Text
                        rs2.CommandText = "INSERT INTO `specimen_tracking` (`sample_id`, `received`, `latest`, `section`, `sub_section`) VALUES ('" & SampleID & "', @DateReceived, @DateCheckedIn, '" & lv2.Items(x).SubItems(1).Text & "', '" & lv2.Items(x).SubItems(2).Text & "')"
                        rs2.ExecuteNonQuery()
                        Disconnect2()
                    End If
                    Disconnect1()
                Next
                '4321
                'need to change this 14/05/2022
                'Update iHOMIS 'estatus' to 'S'
                'MessageBox.Show(GridView1.GetRowCellValue(rowHandle, GridView1.Columns("HospitalNo")).ToString)
                Dim SQL_Update = "UPDATE hpatchrg SET estatus = 'S' WHERE  hpercode = '" & GridView1.GetRowCellValue(rowHandle, GridView1.Columns("HospitalNo")) & "' AND enccode = '" & GridView1.GetRowCellValue(rowHandle, GridView1.Columns("Enccode")) & "' AND itemcode = '" & GridView1.GetRowCellValue(rowHandle, GridView1.Columns("Itemcode")) & "' AND rvscode = '" & GridView1.GetRowCellValue(rowHandle, GridView1.Columns("Rvscode")) & "' "
                '                  GridView.GetRowCellValue(rowhandle, GridView.Columns("HospitalNo")).ToString
                'docointkey = '" & GridView1.GetRowCellValue(rowHandle, GridView1.Columns("MainID")) & "'"
                ConnectSQL()
                rsSQL.Connection = connSQL
                rsSQL.CommandType = CommandType.Text
                rsSQL.CommandText = SQL_Update
                rsSQL.ExecuteNonQuery()
                DisconnectSQL()
                'end update iHOMIS.

            Next

            addLastID()
            'Gridview1.FocusedRowHandle = -1
            MessageBox.Show("Patient order successfully Checked-In", "Check-In", MessageBoxButtons.OK, MessageBoxIcon.Information)
            'RefreshForm()


            ActivityLogs(SampleID,
                             GridView1.GetFocusedRowCellValue(GridView1.Columns("HospitalNo")).ToString,
                             GridView1.GetFocusedRowCellValue(GridView1.Columns("PatientName")).ToString,
                             CurrUser,
                             "Checked In",
                             Test.TrimEnd(", "),
                             Section,
                             SubSection)


            Test = ""
            HISCode = ""
            LoadRecordsiHomis()
            LoadRecordsOPD()
            'zxcv end

        End If
    End Sub

    Private Sub addLastID()
        'Save Last ID for Reference in the next Sample to check in
        Connect()
        rs.Connection = conn
        rs.CommandType = CommandType.Text
        rs.CommandText = "INSERT INTO `last_id` VALUES ('" & LastID + 1 & "')"
        rs.ExecuteNonQuery()
        Disconnect()
        'End of Last Id Checked-In
        'Private Sub RefreshForm()
        '    Me.Controls.Clear()
        '    Me.InitializeComponent()
        'End Sub
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.ItemClick
        'LoadRecordsiHomis()
        'LoadRecordsOPD()
        'packagename()
        packagenameOPD()
        'lvtest_items() XTab.TabPages
        'xtab()
    End Sub

    Public Sub xtab()
        If XTabPatient.SelectedTabPageIndex = 0 Then
            MessageBox.Show(1)
        ElseIf XTabPatient.SelectedTabPageIndex = 1 Then
            MessageBox.Show(2)
        End If
    End Sub

    Private Sub btnClose_ItemClick(ByVal sender As Object, ByVal e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnClose.ItemClick
        Me.Close()
        Me.Dispose()
    End Sub

    Private Sub frm_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        'MainFOrm.aceFecal.Appearance.Normal.BackColor = Color.FromArgb(6, 31, 71)
        MainFOrm.acciHOMIS.Appearance.Normal.BackColor = Color.FromArgb(16, 110, 190)
        MainFOrm.acciHOMIS.Appearance.Normal.ForeColor = Color.FromArgb(255, 255, 255)
    End Sub

    Private Sub frm_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        MainFOrm.lblTitle.Text = ""
        MainFOrm.acciHOMIS.Appearance.Normal.BackColor = Color.FromArgb(240, 240, 240)
        MainFOrm.acciHOMIS.Appearance.Normal.ForeColor = Color.FromArgb(27, 41, 62)
        Me.Dispose()
    End Sub

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        LoadRecordsiHomis()
    End Sub

    Private Sub cboSection_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboSection.SelectedIndexChanged, dtFrom1.TextChanged, dtTo1.TextChanged
        If cboSection.SelectedItem = "Hematology" Then
            procreslt = "002"
        ElseIf cboSection.SelectedItem = "Chemistry" Then
            procreslt = "008"
        ElseIf cboSection.SelectedItem = "Hematology" Then
            procreslt = "009"
        ElseIf cboSection.SelectedItem = "Fecalysis" Then
            procreslt = "007"
        ElseIf cboSection.SelectedItem = "Urinalysis" Then
            procreslt = "001"
        ElseIf cboSection.SelectedItem = "Blood Culture" Then
            procreslt = "003"
        ElseIf cboSection.SelectedItem = "Blood Bank" Then
            procreslt = "012"
        ElseIf cboSection.SelectedItem = "ImmunoSero" Then
            procreslt = "006"
        End If
        'LoadRecordsiHomis()
    End Sub

    Private Sub cboSection2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboSection2.SelectedIndexChanged, dtfrom2.TextChanged, dtto2.TextChanged
        If cboSection2.SelectedItem = "Hematology" Then
            procreslt2 = "002"
        ElseIf cboSection2.SelectedItem = "Chemistry" Then
            procreslt2 = "008"
        ElseIf cboSection2.SelectedItem = "Hematology" Then
            procreslt2 = "009"
        ElseIf cboSection2.SelectedItem = "Fecalysis" Then
            procreslt2 = "007"
        ElseIf cboSection2.SelectedItem = "Urinalysis" Then
            procreslt2 = "001"
        ElseIf cboSection2.SelectedItem = "Blood Culture" Then
            procreslt2 = "003"
        ElseIf cboSection2.SelectedItem = "Blood Bank" Then
            procreslt2 = "012"
        ElseIf cboSection2.SelectedItem = "ImmunoSero" Then
            procreslt2 = "006"
        End If
        'LoadRecordsiHomis()
    End Sub

    Private Sub GetLastID()

        Connect()
        rs.Connection = conn
        rs.CommandType = CommandType.Text
        rs.CommandText = "SELECT * FROM last_id ORDER BY lastidno DESC LIMIT 1"
        reader = rs.ExecuteReader
        reader.Read()
        If reader.HasRows Then
            LastID = reader(0).ToString
            If reader(0).ToString > 0 And reader(0).ToString <= 9 Then
                SampleID = "000000" & reader(0).ToString + 1
            ElseIf reader(0).ToString > 9 And reader(0).ToString <= 99 Then
                SampleID = "00000" & reader(0).ToString + 1
            ElseIf reader(0).ToString > 99 And reader(0).ToString <= 999 Then
                SampleID = "0000" & reader(0).ToString + 1
            ElseIf reader(0).ToString > 999 And reader(0).ToString <= 9999 Then
                SampleID = "000" & reader(0).ToString + 1
            ElseIf reader(0).ToString > 9999 And reader(0).ToString <= 99999 Then
                SampleID = "00" & reader(0).ToString + 1
            ElseIf reader(0).ToString > 99999 And reader(0).ToString <= 999999 Then
                SampleID = "0" & reader(0).ToString + 1
            ElseIf reader(0).ToString > 999999 And reader(0).ToString <= 9999999 Then
                SampleID = reader(0).ToString + 1
            End If
        Else
            LastID = 1
            SampleID = "000000" & 1
            'SampleID = "000000" & reader(0).ToString + 1
        End If
        Disconnect()
    End Sub

    Private Sub btnTxtSearch_Click(sender As Object, e As EventArgs)
        LoadRecordsiHomis()
    End Sub

    Private Sub chAll_CheckedChanged(sender As Object, e As EventArgs) Handles chAll.CheckedChanged
        If chAll.CheckState = CheckState.Checked Then
            For x As Integer = lvList.Items.Count - 1 To 0 Step -1
                lvList.Items(x).Checked = True
            Next
        Else
            For x As Integer = lvList.Items.Count - 1 To 0 Step -1
                lvList.Items(x).Checked = False
            Next
        End If
    End Sub

    Private Sub chAll2_CheckedChanged(sender As Object, e As EventArgs) Handles chAll2.CheckedChanged
        If chAll2.CheckState = CheckState.Checked Then
            For x As Integer = lvList2.Items.Count - 1 To 0 Step -1
                lvList2.Items(x).Checked = True
            Next
        Else
            For x As Integer = lvList2.Items.Count - 1 To 0 Step -1
                lvList2.Items(x).Checked = False
            Next
        End If
    End Sub

    Private Function codeExists(ByVal text As String) As Boolean
        For Each lvi As ListViewItem In lv.Items
            If lvi.Text.Equals(text) Then Return True
        Next
        Return False
    End Function

    Private Function codeExists2(ByVal text As String) As Boolean
        For Each lvi As ListViewItem In lv2.Items
            If lvi.Text.Equals(text) Then Return True
        Next
        Return False
    End Function

    'Public Sub GetBDate()
    '    Try
    '        If GridView.GetFocusedRowCellValue(GridView.Columns("DateOfBirth")).DateTime = Nothing Then
    '            Exit Sub
    '        End If

    '        Dim Birthday As Date = Format(CDate(GridView.GetRowCellValue(rowHandle, GridView.Columns("DateOfBirth"))), "MM/dd/yyyy")
    '        Dim endDate As Date = Date.Now

    '        Dim timeSpan As TimeSpan = endDate.Subtract(Birthday)
    '        Dim totalDays As Integer = timeSpan.TotalDays
    '        Dim totalYears As Integer = Math.Truncate(totalDays / 365)
    '        Dim totalMonths As Integer = Math.Truncate((totalDays Mod 365) / 30)
    '        Dim remainingDays As Integer = Math.Truncate((totalDays Mod 365) Mod 30)

    '        If totalDays <= 61 Then
    '            'txtClass.Text = "Day(s)"
    '            Age = totalDays & " Day(s)"

    '        ElseIf totalDays >= 62 And totalDays <= 364 Then
    '            'txtClass.Text = "Month(s)"
    '            Age = totalMonths & " Month(s)"

    '        ElseIf totalDays >= 365 Then
    '            'txtClass.Text = "Year(s)"
    '            Age = totalYears & " Year(s)"
    '        End If
    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    '    End Try
    'End Sub

    'Public Sub Gridview_Column_Width()
    '    GridView.Columns("Priority").Width = 45
    '    GridView.Columns("Sex").Width = 30
    '    GridView.Columns("Location").Width = 45
    '    GridView.Columns("Room").Width = 90
    '    GridView.Columns("Ward").Width = 90

    '    'GridView.OptionsView.ColumnAutoWidth = False
    'End Sub

    Public Sub packagename()

        lvList.Items.Clear()

        Dim selectedRows2() As Integer = GridView.GetSelectedRows()
        For Each rowHandle1 As Integer In selectedRows2
            If rowHandle1 >= 0 Then

                Labor = GridView.GetRowCellValue(rowHandle1, GridView.Columns("HISCode"))
                hisMainID = GridView.GetRowCellValue(rowHandle1, GridView.Columns("MainID"))

                'MessageBox.Show(Labor)
            End If

            chAll.Checked = False
            Dim a As Integer = 0
            'lvList.Items.Clear()

            Connect()
            rs.Connection = conn
            rs.CommandType = CommandType.Text
            rs.CommandText = "SELECT `default_specimen`.`specimen`, `default_specimen`.`test_code`, `default_specimen`.`order_no`, `default_specimen`.`section`, `default_specimen`.`test_name`, `default_specimen`.`specimen_type`, `packages`.`packagecode`, `packages`.`packagename` FROM `default_specimen` LEFT JOIN `packages` on `packages`.`packagecode` = `default_specimen`.`his_code` WHERE `his_code` = '" & Labor & "' AND `status` = 'Enable'"
            reader = rs.ExecuteReader
            While reader.Read
                a = a + 1
                Dim iItem As New ListViewItem((a).ToString, 0)
                iItem.SubItems.Add(reader(0).ToString) 'specimen
                iItem.SubItems.Add(reader(1).ToString) 'testcode
                iItem.SubItems.Add(reader(2).ToString) 'order_no
                iItem.SubItems.Add(reader(3).ToString) 'section
                iItem.SubItems.Add(reader(4).ToString) 'test_name
                iItem.SubItems.Add(reader(5).ToString) 'specimen_type
                iItem.SubItems.Add(reader(6).ToString) 'packagecode
                iItem.SubItems.Add(reader(7).ToString) 'packagename
                iItem.SubItems.Add(hisMainID) 'his_code
                lvList.Items.Add(iItem)
            End While

            Disconnect()

            chAll.Checked = True
        Next


        For x As Integer = 0 To Me.lvList.CheckedItems.Count - 1 Step 1

            If Not codeExists(lvList.CheckedItems(x).SubItems(5).Text) Then
                Dim iItems As New ListViewItem(lvList.Items(x).SubItems(0).Text)

                iItems.SubItems.Add(lvList.Items(x).SubItems(4).Text)
                iItems.SubItems.Add(lvList.Items(x).SubItems(5).Text)
                iItems.SubItems.Add(lvList.Items(x).SubItems(6).Text)
                lv.Items.Add(iItems)

            End If
        Next

        For x As Integer = 0 To Me.lvList.CheckedItems.Count - 1
            Dim iItem As New ListViewItem(lvList.CheckedItems(x).Text, 0)
            iItem.SubItems.Add(lvList.CheckedItems(x).SubItems(1).Text) 'test name
            iItem.SubItems.Add(lvList.CheckedItems(x).SubItems(2).Text) 'test code
            iItem.SubItems.Add(lvList.CheckedItems(x).SubItems(4).Text) 'section
            iItem.SubItems.Add(SampleID)

            iItem.SubItems.Add(lvList.CheckedItems(x).SubItems(3).Text) 'order code
            iItem.SubItems.Add(lvList.CheckedItems(x).SubItems(5).Text) 'sub section
            iItem.SubItems.Add(lvList.CheckedItems(x).SubItems(9).Text) 'his mainid
            iItem.SubItems.Add(lvList.CheckedItems(x).SubItems(7).Text) 'his code
            iItem.SubItems.Add(lvList.CheckedItems(x).SubItems(8).Text) 'package name

            lvOrder.Items.Add(iItem)
        Next

    End Sub

    Public Sub packagenameOPD()

        lvList2.Items.Clear()

        Dim selectedRows3() As Integer = GridView1.GetSelectedRows()
        For Each rowHandle3 As Integer In selectedRows3
            If rowHandle3 >= 0 Then

                Labor2 = GridView1.GetRowCellValue(rowHandle3, GridView1.Columns("Proccode"))
                'hisMainID2 = GridView1.GetRowCellValue(rowHandle3, GridView1.Columns("MainID")).ToString

                'MessageBox.Show(Labor)

                'Connect()
                'rs.Connection = conn
                'rs.CommandType = CommandType.Text
                'rs.CommandText = "SELECT his_code FROM default_specimen WHERE his_code = '" & GridView1.GetRowCellValue(rowHandle3, GridView.Columns("Proccode")) & "' "
                'reader = rs.ExecuteReader
                'reader.Read()
                'If reader.HasRows Then
                '    Labor2 = reader(0).ToString
                'End If
                'Disconnect()
            End If

            'MessageBox.Show(Labor2)
            'MessageBox.Show(hisMainID2)

            chAll2.Checked = False
            Dim a As Integer = 0
            'lvList.Items.Clear()
            'xxx
            Connect()
            rs.Connection = conn
            rs.CommandType = CommandType.Text
            rs.CommandText = "SELECT `default_specimen`.`specimen`, `default_specimen`.`test_code`, `default_specimen`.`order_no`, `default_specimen`.`section`, `default_specimen`.`test_name`, `default_specimen`.`specimen_type`, `packages`.`packagecode`, `packages`.`packagename`, `default_specimen`.`his_field`, `default_specimen`.`test_group` FROM `default_specimen` LEFT JOIN `packages` on `packages`.`packagecode` = `default_specimen`.`his_code` WHERE `his_code` = '" & Labor2 & "' AND `status` = 'Enable'"
            reader = rs.ExecuteReader
            While reader.Read
                a = a + 1
                Dim iItem As New ListViewItem((a).ToString, 0)
                iItem.SubItems.Add(reader(0).ToString) 'specimen
                iItem.SubItems.Add(reader(1).ToString) 'testcode
                iItem.SubItems.Add(reader(2).ToString) 'order_no
                iItem.SubItems.Add(reader(3).ToString) 'section
                iItem.SubItems.Add(reader(4).ToString) 'test_name
                iItem.SubItems.Add(reader(5).ToString) 'specimen_type
                iItem.SubItems.Add(reader(6).ToString) 'packagecode
                iItem.SubItems.Add(reader(7).ToString) 'packagename
                iItem.SubItems.Add(reader(8).ToString) 'hisfield
                iItem.SubItems.Add(reader(9).ToString) 'testgroup
                'iItem.SubItems.Add(hisMainID) 'his_code
                lvList2.Items.Add(iItem)
            End While

            Disconnect()

            chAll2.Checked = True
        Next


        For x As Integer = 0 To Me.lvList2.CheckedItems.Count - 1 Step 1

            If Not codeExists2(lvList2.CheckedItems(x).SubItems(5).Text) Then
                Dim iItems As New ListViewItem(lvList2.Items(x).SubItems(0).Text)

                iItems.SubItems.Add(lvList2.Items(x).SubItems(4).Text)
                iItems.SubItems.Add(lvList2.Items(x).SubItems(5).Text)
                iItems.SubItems.Add(lvList2.Items(x).SubItems(6).Text)
                lv2.Items.Add(iItems)

            End If
        Next

        For x As Integer = 0 To Me.lvList2.CheckedItems.Count - 1
            Dim iItem As New ListViewItem(lvList2.CheckedItems(x).Text, 0)
            iItem.SubItems.Add(lvList2.CheckedItems(x).SubItems(1).Text) 'test name
            iItem.SubItems.Add(lvList2.CheckedItems(x).SubItems(2).Text) 'test code
            iItem.SubItems.Add(lvList2.CheckedItems(x).SubItems(4).Text) 'section
            iItem.SubItems.Add(SampleID)

            iItem.SubItems.Add(lvList2.CheckedItems(x).SubItems(3).Text) 'order code
            iItem.SubItems.Add(lvList2.CheckedItems(x).SubItems(5).Text) 'sub section
            'iItem.SubItems.Add(lvList2.CheckedItems(x).SubItems(9).Text) 'his mainid
            iItem.SubItems.Add(lvList2.CheckedItems(x).SubItems(9).Text) 'his code
            iItem.SubItems.Add(lvList2.CheckedItems(x).SubItems(8).Text) 'package name
            iItem.SubItems.Add(lvList2.CheckedItems(x).SubItems(10).Text) 'testgroup
            iItem.SubItems.Add(lvList2.CheckedItems(x).SubItems(7).Text) 'package code LABORxxx

            lvOrder2.Items.Add(iItem)

            'MessageBox.Show(lvList2.CheckedItems(x).SubItems(7).Text)
            'MessageBox.Show(lvList2.CheckedItems(x).SubItems(10).Text)
        Next

    End Sub

    Private Sub GetLastIDChecker_Tick(sender As Object, e As EventArgs) Handles GetLastIDChecker.Tick
        GetLastID()
    End Sub

    'fro OPD
    Public Sub LoadRecordsOPD()
        Me.GridView1.Columns.Clear()

        If txtSearch2.Text = "" Then
            'MessageBox.Show(1)
            Dim SQL As String = "Select TOP 1000
							    t0.hpercode As HospitalNo,
							    t1.patlast + ', ' + t1.patfirst AS PatientName,  							
							    t3.procdesc AS Test,
                                t10.toecode AS Location, 
                                t12.wardname AS Ward,
                                t13.rmname AS Room,
							    t0.pcchrgcod AS ChargeSlip,
							    CONVERT(varchar, t0.pcchrgdte, 22) AS DateTimeRequested,
							    CASE 
      							    WHEN t3.procreslt = 002 THEN 'Hematology' 
      							    WHEN t3.procreslt = 008 THEN 'Chemistry' 
      							    WHEN t3.procreslt = 009 THEN 'Hematology' 
      							    WHEN t3.procreslt = 007 THEN 'Fecalysis' 
      							    WHEN t3.procreslt = 001 THEN 'Urinalysis' 
      							    WHEN t3.procreslt = 006 THEN 'ImmunoSero' 
      							    WHEN t3.procreslt = 003 THEN 'Blood Culture' 
      							    WHEN t3.procreslt = 012 THEN 'Blood Bank' 
      						    END AS Section,
							    t1.patbdate AS DateOfBirth, 
							    t1.patsex AS Sex, 
							    (t4.patstr + ', ' + t5.bgyname + ', ' + t6.ctyname + ', ' + t7.provname) AS Address,
                                t0.enccode As Enccode,
                                t0.itemcode As Itemcode,
                                t0.rvscode As Rvscode,
                                t3.proccode As Proccode

							    FROM hpatchrg t0
							    LEFT JOIN hperson t1 ON t0.hpercode = t1.hpercode
							    LEFT JOIN hproc t2 ON t0.itemcode = t2.prikey
							    LEFT JOIN hprocm t3 ON t2.proccode = t3.proccode
							    Left JOIN haddr t4 On t0.hpercode = t4.hpercode 
							    Left Join hbrgy t5 On t4.brg = t5.bgycode 
							    Left Join hcity t6 ON t4.ctycode = t6.ctycode 
							    Left Join hprov t7 ON t4.provcode = t7.provcode							
							    LEFT JOIN henctr t10 ON t0.enccode = t10.enccode
							    LEFT JOIN hpatroom t11 ON t10.enccode = t11.enccode
							    LEFT JOIN hward t12 ON t11.wardcode = t12.wardcode
							    LEFT JOIN hroom t13 ON t11.rmintkey = t13.rmintkey			

                                WHERE
                                 t0.estatus IS NULL
                                 and
                                 t0.pcchrgcod NOT LIKE ''
                                 AND
                                 t0.srcchrg = 'LABOR'
                                 AND
                                t3.procreslt = @Section
                                AND								
                                (CONVERT(varchar, t0.pcchrgdte, 101) BETWEEN @DateFrom AND @DateTo)
                                ORDER BY t0.pcchrgdte DESC"

            GridView1.Columns.Clear()
            GridView1.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
            GridView1.Appearance.HeaderPanel.FontStyleDelta = FontStyle.Bold

            ConnectSQL()
            rsSQL.Connection = connSQL
            rsSQL.CommandType = CommandType.Text
            rsSQL.CommandText = SQL
            rsSQL.Parameters.Clear()
            rsSQL.Parameters.AddWithValue("@Section", procreslt2)
            rsSQL.Parameters.Add("@DateFrom", SqlDbType.DateTime).Value = Format(dtfrom2.Value, "yyyy-MM-dd")
            rsSQL.Parameters.Add("@DateTo", SqlDbType.DateTime).Value = Format(dtto2.Value, "yyyy-MM-dd")

            Dim adapter As New SqlDataAdapter(rsSQL)

            Dim myTable As DataTable = New DataTable
            adapter.Fill(myTable)

            dtListOPD.DataSource = myTable

        Else
            If rdbHospital2.Checked = True Then
                'MessageBox.Show(2)
                Dim SQL As String = "Select TOP 1000
							    t0.hpercode As HospitalNo,
							    t1.patlast + ', ' + t1.patfirst AS PatientName,  							
							    t3.procdesc AS Test,
                                t10.toecode AS Location, 
                                t12.wardname AS Ward,
							    t0.pcchrgcod AS ChargeSlip,
							    CONVERT(varchar, t0.pcchrgdte, 22) AS DateTimeRequested,
							    CASE 
      							    WHEN t3.procreslt = 002 THEN 'Hematology' 
      							    WHEN t3.procreslt = 008 THEN 'Chemistry' 
      							    WHEN t3.procreslt = 009 THEN 'Hematology' 
      							    WHEN t3.procreslt = 007 THEN 'Fecalysis' 
      							    WHEN t3.procreslt = 001 THEN 'Urinalysis' 
      							    WHEN t3.procreslt = 006 THEN 'ImmunoSero' 
      							    WHEN t3.procreslt = 003 THEN 'Blood Culture' 
      							    WHEN t3.procreslt = 012 THEN 'Blood Bank' 
      						    END AS Section,
							    t1.patbdate AS DateOfBirth, 
							    t1.patsex AS Sex, 
							    (t4.patstr + ', ' + t5.bgyname + ', ' + t6.ctyname + ', ' + t7.provname) AS Address,
                                t0.enccode As Enccode,
                                t0.itemcode As Itemcode,
                                t0.rvscode as Rvscode,
                                t3.proccode	As Proccode			

							    FROM hpatchrg t0
							    LEFT JOIN hperson t1 ON t0.hpercode = t1.hpercode
							    LEFT JOIN hproc t2 ON t0.itemcode = t2.prikey
							    LEFT JOIN hprocm t3 ON t2.proccode = t3.proccode
							    Left JOIN haddr t4 On t0.hpercode = t4.hpercode 
							    Left Join hbrgy t5 On t4.brg = t5.bgycode 
							    Left Join hcity t6 ON t4.ctycode = t6.ctycode 
							    Left Join hprov t7 ON t4.provcode = t7.provcode							
							    LEFT JOIN henctr t10 ON t0.enccode = t10.enccode
							    LEFT JOIN hpatroom t11 ON t10.enccode = t11.enccode
							    LEFT JOIN hward t12 ON t11.wardcode = t12.wardcode
							    LEFT JOIN hroom t13 ON t11.rmintkey = t13.rmintkey							

							    WHERE
                                t0.estatus IS NULL
                                and
                                t0.pcchrgcod NOT LIKE ''
                                AND
                                t0.srcchrg = 'LABOR'
                                And
                                t3.procreslt = @Section
                                And								
                                (CONVERT(varchar, t0.pcchrgdte, 101) BETWEEN @DateFrom And @DateTo)
                                And
                                t0.hpercode Like '" & txtSearch2.Text & "%'
                                ORDER BY t0.pcchrgdte DESC"

                GridView1.Columns.Clear()
                GridView1.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                GridView1.Appearance.HeaderPanel.FontStyleDelta = FontStyle.Bold

                ConnectSQL()
                rsSQL.Connection = connSQL
                rsSQL.CommandType = CommandType.Text
                rsSQL.CommandText = SQL
                rsSQL.Parameters.Clear()
                rsSQL.Parameters.AddWithValue("@Section", procreslt2)
                rsSQL.Parameters.Add("@DateFrom", SqlDbType.DateTime).Value = Format(dtfrom2.Value, "yyyy-MM-dd")
                rsSQL.Parameters.Add("@DateTo", SqlDbType.DateTime).Value = Format(dtto2.Value, "yyyy-MM-dd")

                Dim adapter As New SqlDataAdapter(rsSQL)

                Dim myTable As DataTable = New DataTable
                adapter.Fill(myTable)

                dtListOPD.DataSource = myTable

            ElseIf rdbName2.Checked = True Then
                'MessageBox.Show(3)
                Dim SQL As String = "Select TOP 1000
							    t0.hpercode As HospitalNo,
							    t1.patlast + ', ' + t1.patfirst AS PatientName,  							
							    t3.procdesc AS Test,
                                t10.toecode AS Location, 
                                t12.wardname AS Ward,
							    t0.pcchrgcod AS ChargeSlip,
							    CONVERT(varchar, t0.pcchrgdte, 22) AS DateTimeRequested,
							    CASE 
      							    WHEN t3.procreslt = 002 THEN 'Hematology' 
      							    WHEN t3.procreslt = 008 THEN 'Chemistry' 
      							    WHEN t3.procreslt = 009 THEN 'Hematology' 
      							    WHEN t3.procreslt = 007 THEN 'Fecalysis' 
      							    WHEN t3.procreslt = 001 THEN 'Urinalysis' 
      							    WHEN t3.procreslt = 006 THEN 'ImmunoSero' 
      							    WHEN t3.procreslt = 003 THEN 'Blood Culture' 
      							    WHEN t3.procreslt = 012 THEN 'Blood Bank' 
      						    END AS Section,
							    t1.patbdate AS DateOfBirth, 
							    t1.patsex AS Sex, 
							    (t4.patstr + ', ' + t5.bgyname + ', ' + t6.ctyname + ', ' + t7.provname) AS Address,
                                t0.enccode As Enccode,
                                t0.itemcode As Itemcode,
                                t0.rvscode as Rvscode,
                                t3.proccode	As Proccode		

							    FROM hpatchrg t0
							    LEFT JOIN hperson t1 ON t0.hpercode = t1.hpercode
							    LEFT JOIN hproc t2 ON t0.itemcode = t2.prikey
							    LEFT JOIN hprocm t3 ON t2.proccode = t3.proccode
							    Left JOIN haddr t4 On t0.hpercode = t4.hpercode 
							    Left Join hbrgy t5 On t4.brg = t5.bgycode 
							    Left Join hcity t6 ON t4.ctycode = t6.ctycode 
							    Left Join hprov t7 ON t4.provcode = t7.provcode							
							    LEFT JOIN henctr t10 ON t0.enccode = t10.enccode
							    LEFT JOIN hpatroom t11 ON t10.enccode = t11.enccode
							    LEFT JOIN hward t12 ON t11.wardcode = t12.wardcode
							    LEFT JOIN hroom t13 ON t11.rmintkey = t13.rmintkey							

							    WHERE
                                t0.estatus IS NULL
                                and
                                t0.pcchrgcod NOT LIKE ''
                                AND
                                t0.srcchrg = 'LABOR'
                                AND
							    t3.procreslt = @Section
							    AND								
							    (CONVERT(varchar, t0.pcchrgdte, 101) BETWEEN @DateFrom AND @DateTo)
                                AND
                                t1.patlast + ', ' + t1.patfirst Like '" & txtSearch2.Text & "%'
							    ORDER BY t0.pcchrgdte DESC"

                GridView1.Columns.Clear()
                GridView1.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                GridView1.Appearance.HeaderPanel.FontStyleDelta = FontStyle.Bold

                ConnectSQL()
                rsSQL.Connection = connSQL
                rsSQL.CommandType = CommandType.Text
                rsSQL.CommandText = SQL
                rsSQL.Parameters.Clear()
                rsSQL.Parameters.AddWithValue("@Section", procreslt2)
                rsSQL.Parameters.Add("@DateFrom", SqlDbType.DateTime).Value = Format(dtfrom2.Value, "yyyy-MM-dd")
                rsSQL.Parameters.Add("@DateTo", SqlDbType.DateTime).Value = Format(dtto2.Value, "yyyy-MM-dd")

                Dim adapter As New SqlDataAdapter(rsSQL)

                Dim myTable As DataTable = New DataTable
                adapter.Fill(myTable)

                dtListOPD.DataSource = myTable

            ElseIf rdbPhysician.Checked = True Then

                Dim SQL As String = "Select TOP 1000
							    t0.hpercode As HospitalNo,
							    t1.patlast + ', ' + t1.patfirst AS PatientName,  							
							    t3.procdesc AS Test,
                                t10.toecode AS Location, 
                                t12.wardname AS Ward,
							    t0.pcchrgcod AS ChargeSlip,
							    CONVERT(varchar, t0.pcchrgdte, 22) AS DateTimeRequested,
							    CASE 
      							    WHEN t3.procreslt = 002 THEN 'Hematology' 
      							    WHEN t3.procreslt = 008 THEN 'Chemistry' 
      							    WHEN t3.procreslt = 009 THEN 'Hematology' 
      							    WHEN t3.procreslt = 007 THEN 'Fecalysis' 
      							    WHEN t3.procreslt = 001 THEN 'Urinalysis' 
      							    WHEN t3.procreslt = 006 THEN 'ImmunoSero' 
      							    WHEN t3.procreslt = 003 THEN 'Blood Culture' 
      							    WHEN t3.procreslt = 012 THEN 'Blood Bank' 
      						    END AS Section,
							    t1.patbdate AS DateOfBirth, 
							    t1.patsex AS Sex, 
							    (t4.patstr + ', ' + t5.bgyname + ', ' + t6.ctyname + ', ' + t7.provname) AS Address,
                                t0.enccode As Enccode,
                                t0.itemcode As Itemcode,
                                t0.rvscode as Rvscode,
                                t3.proccode	As Proccode

							    FROM hpatchrg t0
							    LEFT JOIN hperson t1 ON t0.hpercode = t1.hpercode
							    LEFT JOIN hproc t2 ON t0.itemcode = t2.prikey
							    LEFT JOIN hprocm t3 ON t2.proccode = t3.proccode
							    Left JOIN haddr t4 On t0.hpercode = t4.hpercode 
							    Left Join hbrgy t5 On t4.brg = t5.bgycode 
							    Left Join hcity t6 ON t4.ctycode = t6.ctycode 
							    Left Join hprov t7 ON t4.provcode = t7.provcode							
							    LEFT JOIN henctr t10 ON t0.enccode = t10.enccode
							    LEFT JOIN hpatroom t11 ON t10.enccode = t11.enccode
							    LEFT JOIN hward t12 ON t11.wardcode = t12.wardcode
							    LEFT JOIN hroom t13 ON t11.rmintkey = t13.rmintkey							

							    WHERE
                                t0.estatus IS NULL
                                and
                                t0.pcchrgcod NOT LIKE ''
                                AND
                                t0.srcchrg = 'LABOR'
                                AND
							    t3.procreslt = @Section
							    AND								
							    (CONVERT(varchar, t0.pcchrgdte, 101) BETWEEN @DateFrom AND @DateTo)
                                AND
                                ('DR. ' + t4.lastname + ', ' + t4.firstname) Like '" & txtSearch2.Text & "%'
							    ORDER BY t0.pcchrgdte DESC"

                GridView1.Columns.Clear()
                GridView1.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                GridView1.Appearance.HeaderPanel.FontStyleDelta = FontStyle.Bold

                ConnectSQL()
                rsSQL.Connection = connSQL
                rsSQL.CommandType = CommandType.Text
                rsSQL.CommandText = SQL
                rsSQL.Parameters.Clear()
                rsSQL.Parameters.AddWithValue("@Section", procreslt2)
                rsSQL.Parameters.Add("@DateFrom", SqlDbType.DateTime).Value = Format(dtfrom2.Value, "yyyy-MM-dd")
                rsSQL.Parameters.Add("@DateTo", SqlDbType.DateTime).Value = Format(dtto2.Value, "yyyy-MM-dd")

                Dim adapter As New SqlDataAdapter(rsSQL)

                Dim myTable As DataTable = New DataTable
                adapter.Fill(myTable)

                dtListOPD.DataSource = myTable

            ElseIf rdbChargeSlip.Checked = True Then
                '1234
                Dim SQL As String = "Select TOP 1000
							    t0.hpercode As HospitalNo,
							    t1.patlast + ', ' + t1.patfirst AS PatientName,  							
							    t3.procdesc AS Test,
                                t10.toecode AS Location, 
                                t12.wardname AS Ward,
							    t0.pcchrgcod AS ChargeSlip,
							    CONVERT(varchar, t0.pcchrgdte, 22) AS DateTimeRequested,
							    CASE 
      							    WHEN t3.procreslt = 002 THEN 'Hematology' 
      							    WHEN t3.procreslt = 008 THEN 'Chemistry' 
      							    WHEN t3.procreslt = 009 THEN 'Hematology' 
      							    WHEN t3.procreslt = 007 THEN 'Fecalysis' 
      							    WHEN t3.procreslt = 001 THEN 'Urinalysis' 
      							    WHEN t3.procreslt = 006 THEN 'ImmunoSero' 
      							    WHEN t3.procreslt = 003 THEN 'Blood Culture' 
      							    WHEN t3.procreslt = 012 THEN 'Blood Bank' 
      						    END AS Section,
							    t1.patbdate AS DateOfBirth, 
							    t1.patsex AS Sex, 
							    (t4.patstr + ', ' + t5.bgyname + ', ' + t6.ctyname + ', ' + t7.provname) AS Address,
                                t0.enccode As Enccode,
                                t0.itemcode As Itemcode,
                                t0.rvscode as Rvscode,
                                t3.proccode	As Proccode		

							    FROM hpatchrg t0
							    LEFT JOIN hperson t1 ON t0.hpercode = t1.hpercode
							    LEFT JOIN hproc t2 ON t0.itemcode = t2.prikey
							    LEFT JOIN hprocm t3 ON t2.proccode = t3.proccode
							    Left JOIN haddr t4 On t0.hpercode = t4.hpercode 
							    Left Join hbrgy t5 On t4.brg = t5.bgycode 
							    Left Join hcity t6 ON t4.ctycode = t6.ctycode 
							    Left Join hprov t7 ON t4.provcode = t7.provcode							
							    LEFT JOIN henctr t10 ON t0.enccode = t10.enccode
							    LEFT JOIN hpatroom t11 ON t10.enccode = t11.enccode
							    LEFT JOIN hward t12 ON t11.wardcode = t12.wardcode
							    LEFT JOIN hroom t13 ON t11.rmintkey = t13.rmintkey							

							    WHERE
                                t0.estatus IS NULL
                                and
                                t0.pcchrgcod NOT LIKE ''
                                AND
                                t0.srcchrg = 'LABOR'
                                AND
							    t3.procreslt = @Section
							    AND								
							    (CONVERT(varchar, t0.pcchrgdte, 101) BETWEEN @DateFrom AND @DateTo)
                                AND
                                t0.pcchrgcod Like '" & txtSearch2.Text & "%'
							    ORDER BY t0.pcchrgdte DESC"

                GridView1.Columns.Clear()
                GridView1.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                GridView1.Appearance.HeaderPanel.FontStyleDelta = FontStyle.Bold

                ConnectSQL()
                rsSQL.Connection = connSQL
                rsSQL.CommandType = CommandType.Text
                rsSQL.CommandText = SQL
                rsSQL.Parameters.Clear()
                rsSQL.Parameters.AddWithValue("@Section", procreslt2)
                rsSQL.Parameters.Add("@DateFrom", SqlDbType.DateTime).Value = Format(dtfrom2.Value, "yyyy-MM-dd")
                rsSQL.Parameters.Add("@DateTo", SqlDbType.DateTime).Value = Format(dtto2.Value, "yyyy-MM-dd")

                Dim adapter As New SqlDataAdapter(rsSQL)

                Dim myTable As DataTable = New DataTable
                adapter.Fill(myTable)

                dtListOPD.DataSource = myTable

            End If
        End If

        ' Make the grid read-only. 
        GridView1.OptionsBehavior.Editable = False
        ' Prevent the focused cell from being highlighted. 
        GridView1.OptionsSelection.EnableAppearanceFocusedCell = False
        ' Draw a dotted focus rectangle around the entire row. 
        GridView1.FocusRectStyle = DrawFocusRectStyle.RowFullFocus

        GridView1.OptionsSelection.MultiSelect = True
        GridView1.OptionsSelection.MultiSelectMode = GridMultiSelectMode.CheckBoxRowSelect

        'GridView.Columns(1).DefaultCellStyle.WrapMode = DataGridViewTriState.True

        'column adjustments

        'Gridview_Column_Width()

        'GridView.Columns("Enccode").Visible = False
        'GridView.Columns("Itemcode").Visible = False

        GridView1.OptionsView.ColumnAutoWidth = False

        GridView1.Columns("HospitalNo").Width = 90
        GridView1.Columns("PatientName").Width = 200
        GridView1.Columns("Test").Width = 200
        GridView1.Columns("Ward").Width = 130
        'GridView1.Columns("Room").Width = 130
        GridView1.Columns("Sex").Width = 30
        GridView1.Columns("DateTimeRequested").Width = 130
        GridView1.Columns("Location").Width = 60
        'GridView1.Columns("HISCode").Width = 60
        GridView1.Columns("Section").Width = 60

        DisconnectSQL()

    End Sub

    Private Sub btnPatientOrder_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnPatientOrder.ItemClick
        Try
            frmPatientOrderAE.AEstatus = "new"
            frmPatientOrderAE.ShowDialog()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnSearch2_Click(sender As Object, e As EventArgs) Handles btnSearch2.Click
        LoadRecordsOPD()
    End Sub

End Class