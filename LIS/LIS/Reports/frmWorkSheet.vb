Imports DevExpress.XtraGrid.Views.Grid
Imports MySql.Data.MySqlClient

Public Class frmWorkSheet
    Public MedTechID As String = ""
    Public Sub AutoLoadTestName()
        cboLimit.Properties.Items.Clear()
        Connect()
        rs.Connection = conn
        rs.CommandType = CommandType.Text
        'update this in the future for additional tests
        rs.CommandText = "SELECT `test_name` FROM `testtype` WHERE `test_name` <> 'All' AND `test_name` <> 'Fecalysis' AND `test_name` <> 'Microbiology' AND `test_name` <> 'Molecular' AND `test_name` <> 'Other Test' AND `test_name` <> 'Urinalysis' AND `test_name` <> 'Immuno' ORDER BY `test_name`"
        reader = rs.ExecuteReader
        While reader.Read
            cboLimit.Properties.Items.Add(reader(0).ToString)
        End While
        Disconnect()

        cboLimit.SelectedIndex = 0
    End Sub

    Private Sub frmWorkListHema_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Me.Dispose()
    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.ItemClick
        'If cboLimit.Text = "Hematology" Then
        '    GenerateReport(cboLimit.Text, dtFrom, dtTo, "ReportWorksheetHema.rdlc", RPTWorksheet.ReportViewer1)
        '    RPTWorksheet.ShowDialog()
        'ElseIf cboLimit.Text = "Chemistry" Then
        '    GenerateReport(cboLimit.Text, dtFrom, dtTo, "ReportWorksheetChem.rdlc", RPTWorksheet.ReportViewer1)
        '    RPTWorksheet.ShowDialog()
        'End If

        GenerateReport(cboLimit.Text, dtFrom, dtTo, cboLimit.Text & ".rdlc", RPTWorksheet.ReportViewer1)
        RPTWorksheet.ShowDialog()
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.ItemClick
        Me.Close()
    End Sub

    '########################################### SEARCHING USING DATES AND PATIENT TYPE ############################################################
    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtFrom.TextChanged, dtTo.TextChanged, tmFrom.TextChanged, tmTo.TextChanged, cboLimit.SelectedIndexChanged, txtSearch.TextChanged
        Try

            GridView.Columns.Clear()
            GridView.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
            GridView.Appearance.HeaderPanel.FontStyleDelta = FontStyle.Bold

        If txtSearch.Text = Nothing Then

            Connect()
            rs.Parameters.Clear()
            rs.Parameters.AddWithValue("@DateFrom", Format(CDate(dtFrom.Value.ToShortDateString & " " & tmFrom.Value.ToLongTimeString), "yyyy-MM-dd hh:mm:ss tt"))
            rs.Parameters.AddWithValue("@DateTo", Format(CDate(dtTo.Value.ToShortDateString & " " & tmTo.Value.ToLongTimeString), "yyyy-MM-dd hh:mm:ss tt"))

                Dim sql As String
                sql = "worksheet_" & cboLimit.Text

            Dim dt As DataTable = New DataTable
            rs.Connection = conn
            rs.CommandType = CommandType.StoredProcedure
            rs.CommandText = sql
            Dim adapter As New MySqlDataAdapter(rs)

            adapter.Fill(dt)
            dtResult.DataSource = (dt)

            ' Make the grid read-only. 
            GridView.OptionsBehavior.Editable = False
            ' Prevent the focused cell from being highlighted. 
            GridView.OptionsSelection.EnableAppearanceFocusedCell = False
            ' Draw a dotted focus rectangle around the entire row. 
            GridView.FocusRectStyle = DrawFocusRectStyle.RowFullFocus
            GridView.OptionsView.BestFitMode = GridBestFitMode.Fast

            GridView.Columns("Instrument").Visible = False
            GridView.Columns("Section").Visible = False
            GridView.Columns("SubSection").Visible = False
            GridView.Columns("Date").Visible = False
            Disconnect()

            Me.lblCount.Text = "COUNT: " & Me.GridView.RowCount

        ElseIf cboLimit.Text = "Chemistry" And txtSearch.Text IsNot Nothing Then

            Dim SQL As String = "Select
			                        SampleID,
			                        PatientID,
			                        PatientName,
			                        DateOfBirth,
			                        Sex,
			                        Age,
			                        Requestor,
			                        MedTech,
			                        VerifiedBy,
			                        DateCollected,
			                        DateReleased,
			                        Section,
			                        SubSection,
			                        Instrument,
			                        PatientType,
			                        Ward,
			                        COALESCE(SUM(CASE WHEN ALP THEN ALP ELSE NULL END), NULL) as ALP,
			                        COALESCE(SUM(CASE WHEN ALT THEN ALT ELSE NULL END), NULL) as ALT,
			                        COALESCE(SUM(CASE WHEN AST THEN AST ELSE NULL END), NULL) as AST,
			                        COALESCE(SUM(CASE WHEN AMY THEN AMY ELSE NULL END), NULL) as AMY,
			                        COALESCE(SUM(CASE WHEN CK THEN CK ELSE NULL END), NULL) as CK,
			                        COALESCE(SUM(CASE WHEN CKMB THEN CKMB ELSE NULL END), NULL) as CKMB,
			                        COALESCE(SUM(CASE WHEN GGT THEN GGT ELSE NULL END), NULL) as GGT,
			                        COALESCE(SUM(CASE WHEN LIPASE THEN LIPASE ELSE NULL END), NULL) as LIPASE,
			                        COALESCE(SUM(CASE WHEN TROP THEN TROP ELSE NULL END), NULL) as TROP,
			                        COALESCE(SUM(CASE WHEN ALB THEN ALB ELSE NULL END), NULL) as ALB,
			                        COALESCE(SUM(CASE WHEN GLOB THEN GLOB ELSE NULL END), NULL) as GLOB,
			                        COALESCE(SUM(CASE WHEN AGRatio THEN AGRatio ELSE NULL END), NULL) as AGRatio,
			                        COALESCE(SUM(CASE WHEN BILI_T THEN BILI_T ELSE NULL END), NULL) as BILI_T,
			                        COALESCE(SUM(CASE WHEN BILI_D THEN BILI_D ELSE NULL END), NULL) as BILI_D,
			                        COALESCE(SUM(CASE WHEN BILI_I THEN BILI_I ELSE NULL END), NULL) as BILI_I,
			                        COALESCE(SUM(CASE WHEN CHOL THEN CHOL ELSE NULL END), NULL) as CHOL,
			                        COALESCE(SUM(CASE WHEN HDL THEN HDL ELSE NULL END), NULL) as HDL,
			                        COALESCE(SUM(CASE WHEN LDL THEN LDL ELSE NULL END), NULL) as LDL,
			                        COALESCE(SUM(CASE WHEN TRIGLY THEN TRIGLY ELSE NULL END), NULL) as TRIGLY,
			                        COALESCE(SUM(CASE WHEN VLDL THEN VLDL ELSE NULL END), NULL) as VLDL,
			                        COALESCE(SUM(CASE WHEN CREA THEN CREA ELSE NULL END), NULL) as CREA,
			                        COALESCE(SUM(CASE WHEN CRENZ THEN CRENZ ELSE NULL END), NULL) as CRENZ,
			                        COALESCE(SUM(CASE WHEN CREA_RB THEN CREA_RB ELSE NULL END), NULL) as CREA_RB,
			                        COALESCE(SUM(CASE WHEN CRP THEN CRP ELSE NULL END), NULL) as CRP,
			                        COALESCE(SUM(CASE WHEN CAAS THEN CAAS  ELSE NULL END), NULL) as CAAS ,
			                        COALESCE(SUM(CASE WHEN CO2 THEN CO2 ELSE NULL END), NULL) as CO2,
			                        COALESCE(SUM(CASE WHEN FRUCTO THEN FRUCTO ELSE NULL END), NULL) as FRUCTO,
			                        COALESCE(SUM(CASE WHEN IRON THEN IRON ELSE NULL END), NULL) as IRON,
			                        COALESCE(SUM(CASE WHEN LACT THEN LACT ELSE NULL END), NULL) as LACT,
			                        COALESCE(SUM(CASE WHEN MAGN THEN MAGN ELSE NULL END), NULL) as MAGN,
			                        COALESCE(SUM(CASE WHEN PHOS THEN PHOS ELSE NULL END), NULL) as PHOS,
			                        COALESCE(SUM(CASE WHEN TP THEN TP ELSE NULL END), NULL) as TP,
			                        COALESCE(SUM(CASE WHEN UA THEN UA ELSE NULL END), NULL) as UA,
			                        COALESCE(SUM(CASE WHEN UREA THEN UREA ELSE NULL END), NULL) as UREA,
			                        COALESCE(SUM(CASE WHEN BUN THEN BUN ELSE NULL END), NULL) as BUN,
			                        COALESCE(SUM(CASE WHEN LDH THEN LDH ELSE NULL END), NULL) as LDH,
			                        COALESCE(SUM(CASE WHEN NA THEN NA ELSE NULL END), NULL) as NA,
			                        COALESCE(SUM(CASE WHEN K THEN K ELSE NULL END), NULL) as K,
			                        COALESCE(SUM(CASE WHEN CL THEN CL ELSE NULL END), NULL) as CL,
			                        COALESCE(SUM(CASE WHEN Cal_I THEN Cal_I ELSE NULL END), NULL) as Cal_I,
			                        COALESCE(SUM(CASE WHEN CALC THEN CALC ELSE NULL END), NULL) as CALC,
			                        COALESCE(SUM(CASE WHEN GLUP THEN GLUP ELSE NULL END), NULL) as GLUP,
			                        COALESCE(SUM(CASE WHEN GLUK THEN GLUK ELSE NULL END), NULL) as GLUK,
			                        COALESCE(SUM(CASE WHEN FBS THEN FBS ELSE NULL END), NULL) as FBS,
			                        COALESCE(SUM(CASE WHEN RBS THEN RBS ELSE NULL END), NULL) as RBS,
			                        COALESCE(SUM(CASE WHEN OGTT THEN OGTT ELSE NULL END), NULL) as OGTT,
			                        COALESCE(SUM(CASE WHEN OGTT_1 THEN OGTT_1 ELSE NULL END), NULL) as OGTT_1,
			                        COALESCE(SUM(CASE WHEN OGTT_2 THEN OGTT_2 ELSE NULL END), NULL) as OGTT_2,
			                        COALESCE(SUM(CASE WHEN OGTT_3 THEN OGTT_3 ELSE NULL END), NULL) as OGTT_3,
			                        COALESCE(SUM(CASE WHEN G6PD THEN G6PD ELSE NULL END), NULL) as G6PD,
			                        COALESCE(SUM(CASE WHEN HbA1C THEN HbA1C ELSE NULL END), NULL) as HbA1C,
			                        `date` as Date
		                        FROM 
				                        (Select 
					                        result.sample_id AS SampleID,
					                        `order`.patient_id AS PatientID,
					                        `order`.patient_name AS PatientName,
					                        `order`.bdate AS DateOfBirth,
					                        `order`.sex AS Sex,
					                        `order`.age AS Age,
					                        `order`.physician as Requestor,
					                        `order`.medtech as MedTech,
					                        `order`.verified_by as VerifiedBy,
					                        CONCAT(`order`.`date`, ' ', `order`.`time`) AS DateCollected,
					                        `order`.dt_released AS DateReleased,
					                        `order`.testtype AS Section,
					                        `order`.sub_section AS SubSection,
					                        `order`.instrument AS Instrument,
					                        `order`.patient_type AS PatientType,
					                        `order`.dept AS Ward,
					                        CASE WHEN test_code = 'ALP' THEN measurement END AS ALP,
					                        CASE WHEN test_code = 'ALT' THEN measurement END AS ALT,
					                        CASE WHEN test_code = 'AST' THEN measurement END AS AST,
					                        CASE WHEN test_code = 'AMY' THEN measurement END AS AMY,
					                        CASE WHEN test_code = 'CK' THEN measurement END AS CK,
					                        CASE WHEN test_code = 'CKMB' THEN measurement END AS CKMB,
					                        CASE WHEN test_code = 'GGT' THEN measurement END AS GGT,
					                        CASE WHEN test_code = 'LIPASE' THEN measurement END AS LIPASE,
					                        CASE WHEN test_code = 'Trop' THEN measurement END AS Trop,
					                        #AG_Ratio, GLobulin, Albumin
					                        CASE WHEN test_code = 'ALB' THEN measurement END AS ALB,
					                        CASE WHEN test_code = 'GLOB' THEN measurement END AS GLOB,
					                        CASE WHEN test_code = 'AGRatio' THEN measurement END AS AGRatio,
					                        CASE WHEN test_code = 'BILI_T' THEN measurement END AS BILI_T,
					                        CASE WHEN test_code = 'BILI_D' THEN measurement END AS BILI_D,
					                        CASE WHEN test_code = 'BILI_I' THEN measurement END AS BILI_I,
					                        #Lipid Profile
					                        CASE WHEN test_code = 'C_CHOL' THEN measurement END AS CHOL,
					                        CASE WHEN test_code = 'C_HDL' THEN measurement END AS HDL,
					                        CASE WHEN test_code = 'C_LDL' THEN measurement END AS LDL,
					                        CASE WHEN test_code = 'Trigly' THEN measurement END AS Trigly,
					                        CASE WHEN test_code = 'VLDL' THEN measurement END AS VLDL,
					                        #Routine
					                        CASE WHEN test_code = 'CREA' THEN measurement END AS CREA,
					                        CASE WHEN test_code = 'CRENZ' THEN measurement END AS CRENZ,
					                        CASE WHEN test_code = 'CREA_RB' THEN measurement END AS CREA_RB,
					                        CASE WHEN test_code = 'CRP' THEN measurement END AS CRP,
					                        CASE WHEN test_code = 'CAAS' THEN measurement END AS CAAS,
					                        CASE WHEN test_code = 'CO2' THEN measurement END AS CO2,
					                        CASE WHEN test_code = 'FRUCTO' THEN measurement END AS FRUCTO,
					                        CASE WHEN test_code = 'IRON' THEN measurement END AS IRON,
					                        CASE WHEN test_code = 'LACT' THEN measurement END AS LACT,
					                        CASE WHEN test_code = 'MAGN' THEN measurement END AS MAGN,
					                        CASE WHEN test_code = 'PHOS' THEN measurement END AS PHOS,
					                        CASE WHEN test_code = 'TP' THEN measurement END AS TP,
					                        CASE WHEN test_code = 'UA' THEN measurement END AS UA,
					                        CASE WHEN test_code = 'UREA' THEN measurement END AS UREA,
					                        CASE WHEN test_code = 'BUN' THEN measurement END AS BUN,
					                        CASE WHEN test_code = 'LDH' THEN measurement END AS LDH,
					                        #Electrolytes
					                        CASE WHEN test_code = 'NA' THEN measurement END AS NA,
					                        CASE WHEN test_code = 'K' THEN measurement END AS K,
					                        CASE WHEN test_code = 'CL' THEN measurement END AS CL,
					                        CASE WHEN test_code = 'Cal_I' THEN measurement END AS Cal_I,
					                        CASE WHEN test_code = 'CALC' THEN measurement END AS CALC,
					                        #Glucose
					                        CASE WHEN test_code = 'GLUP' THEN measurement END AS GLUP,
					                        CASE WHEN test_code = 'GLUK' THEN measurement END AS GLUK,			
					                        CASE WHEN test_code = 'FBS' THEN measurement END AS FBS,
					                        CASE WHEN test_code = 'RBS' THEN measurement END AS RBS,
					                        CASE WHEN test_code = 'OGTT' THEN measurement END AS OGTT,
					                        CASE WHEN test_code = 'OGTT_1' THEN measurement END AS OGTT_1,
					                        CASE WHEN test_code = 'OGTT_2' THEN measurement END AS OGTT_2,
					                        CASE WHEN test_code = 'OGTT_3' THEN measurement END AS OGTT_3,
					                        #G6PD
					                        CASE WHEN test_code = 'G6PD' THEN measurement END AS G6PD,
					                        #HbA1C
					                        CASE WHEN test_code = 'HbA1C' THEN measurement END AS HbA1C,
					                        `order`.`date`
				                        FROM
					                        result
				                        LEFT JOIN `order` ON result.sample_id = `order`.sample_id
				                        WHERE `order`.`testtype` = 'Chemistry' AND `order`.`date` BETWEEN @DateFrom AND @DateTo
					                        ) AS Chemistry WHERE `SampleID` LIKE '" & txtSearch.Text & "%' OR `PatientID` LIKE '" & txtSearch.Text & "%' OR `PatientName` LIKE '" & txtSearch.Text & "%' GROUP BY SampleID;"

            Dim command As New MySql.Data.MySqlClient.MySqlCommand(SQL, conn)

            command.Parameters.Clear()
            command.Parameters.AddWithValue("@DateFrom", Format(CDate(dtFrom.Value.ToShortDateString & " " & tmFrom.Value.ToLongTimeString), "yyyy-MM-dd hh:mm:ss tt"))
            command.Parameters.AddWithValue("@DateTo", Format(CDate(dtTo.Value.ToShortDateString & " " & tmTo.Value.ToLongTimeString), "yyyy-MM-dd hh:mm:ss tt"))

            Dim adapter As New MySql.Data.MySqlClient.MySqlDataAdapter(command)

            Dim myTable As DataTable = New DataTable
            adapter.Fill(myTable)

            dtResult.DataSource = myTable

            ' Make the grid read-only. 
            GridView.OptionsBehavior.Editable = False
            ' Prevent the focused cell from being highlighted. 
            GridView.OptionsSelection.EnableAppearanceFocusedCell = False
            ' Draw a dotted focus rectangle around the entire row. 
            GridView.FocusRectStyle = DrawFocusRectStyle.RowFullFocus
            GridView.OptionsView.BestFitMode = GridBestFitMode.Fast

            GridView.Columns("Instrument").Visible = False
            GridView.Columns("Section").Visible = False
            GridView.Columns("SubSection").Visible = False
            GridView.Columns("Date").Visible = False
            Disconnect()

            Me.lblCount.Text = "COUNT: " & Me.GridView.RowCount

        ElseIf cboLimit.Text = "Hematology" And txtSearch.Text IsNot Nothing Then

            Dim SQL As String = "Select
	                            SampleID,
	                            PatientID,
	                            PatientName,
	                            DateOfBirth,
	                            Sex,
	                            Age,
	                            Requestor,
	                            MedTech,
	                            VerifiedBy,
	                            DateCollected,
	                            DateReleased,
	                            Section,
	                            SubSection,
	                            Instrument,
	                            PatientType,
	                            Ward,
	                            COALESCE(SUM(CASE WHEN WBC THEN WBC ELSE NULL END), NULL) AS WBC,
	                            COALESCE(SUM(CASE WHEN RBC THEN RBC ELSE NULL END), NULL) as RBC,
	                            COALESCE(SUM(CASE WHEN HGB THEN HGB ELSE NULL END), NULL) as HGB,
	                            COALESCE(SUM(CASE WHEN HCT THEN HCT ELSE NULL END), NULL) as HCT,
	                            COALESCE(SUM(CASE WHEN MCV THEN MCV ELSE NULL END), NULL) as MCV,
	                            COALESCE(SUM(CASE WHEN MCH THEN MCH ELSE NULL END), NULL) as MCH,
	                            COALESCE(SUM(CASE WHEN MCHC THEN MCHC ELSE NULL END), NULL) as MCHC,
	                            COALESCE(SUM(CASE WHEN PLT THEN PLT ELSE NULL END), NULL) as PLT,
	                            COALESCE(SUM(CASE WHEN RDWCV THEN RDWCV ELSE NULL END), NULL) as RDWCV,
	                            COALESCE(SUM(CASE WHEN RDWSD THEN RDWSD ELSE NULL END), NULL) as RDWSD,
	                            COALESCE(SUM(CASE WHEN PCT THEN PCT ELSE NULL END), NULL) as PCT,
	                            COALESCE(SUM(CASE WHEN PDW THEN PDW ELSE NULL END), NULL) as PDW,
	                            COALESCE(SUM(CASE WHEN NEU_P THEN NEU_P ELSE NULL END), NULL) as NEU_P,
	                            COALESCE(SUM(CASE WHEN LYM_P THEN LYM_P ELSE NULL END), NULL) as LYM_P,
	                            COALESCE(SUM(CASE WHEN MON_P THEN MON_P ELSE NULL END), NULL) as MON_P,
	                            COALESCE(SUM(CASE WHEN EOS_P THEN EOS_P ELSE NULL END), NULL) as EOS_P,
	                            COALESCE(SUM(CASE WHEN BAS_P THEN BAS_P ELSE NULL END), NULL) as BAS_P,
	                            COALESCE(SUM(CASE WHEN NEU_N THEN NEU_N ELSE NULL END), NULL) as NEU_N,
	                            COALESCE(SUM(CASE WHEN LYM_N THEN LYM_N ELSE NULL END), NULL) as LYM_N,
	                            COALESCE(SUM(CASE WHEN MON_N THEN MON_N ELSE NULL END), NULL) as MON_N,
	                            COALESCE(SUM(CASE WHEN EOS_N THEN EOS_N ELSE NULL END), NULL) as EOS_N,
	                            COALESCE(SUM(CASE WHEN BAS_N THEN BAS_N ELSE NULL END), NULL) as BAS_N,
	                            COALESCE(CASE WHEN BType THEN BType ELSE NULL END, NULL) as BType,
	                            COALESCE(SUM(CASE WHEN ESR THEN ESR ELSE NULL END), NULL) as ESR,
	                            COALESCE(SUM(CASE WHEN CT THEN ESR ELSE NULL END), NULL) as CT,
	                            COALESCE(SUM(CASE WHEN BT THEN ESR ELSE NULL END), NULL) as BT,
	                            COALESCE(SUM(CASE WHEN APTT THEN ESR ELSE NULL END), NULL) as APTT,
	                            COALESCE(SUM(CASE WHEN PTT THEN ESR ELSE NULL END), NULL) as PTT,
	                            `date` as Date
                            FROM 
		                            (Select 
			                            result.sample_id AS SampleID,
			                            `order`.patient_id AS PatientID,
			                            `order`.patient_name AS PatientName,
			                            `order`.bdate AS DateOfBirth,
			                            `order`.sex AS Sex,
			                            `order`.age AS Age,
			                            `order`.physician as Requestor,
			                            `order`.medtech as MedTech,
			                            `order`.verified_by as VerifiedBy,
			                            CONCAT(`order`.`date`, ' ', `order`.`time`) AS DateCollected,
			                            `order`.dt_released AS DateReleased,
			                            `order`.testtype AS Section,
			                            `order`.sub_section AS SubSection,
			                            `order`.instrument AS Instrument,
			                            `order`.patient_type AS PatientType,
			                            `order`.dept AS Ward,
			                            CASE WHEN test_code = 'WBC' THEN measurement END AS WBC,
			                            CASE WHEN test_code = 'RBC' THEN measurement END AS RBC,
			                            CASE WHEN test_code = 'HGB' THEN measurement END AS HGB,
			                            CASE WHEN test_code = 'HCT' THEN measurement END AS HCT,
			                            CASE WHEN test_code = 'MCV' THEN measurement END AS MCV,
			                            CASE WHEN test_code = 'MCH' THEN measurement END AS MCH,
			                            CASE WHEN test_code = 'MCHC' THEN measurement END AS MCHC,
			                            CASE WHEN test_code = 'PLT' THEN measurement END AS PLT,

			                            CASE WHEN test_code = 'RDWCV' THEN measurement END AS RDWCV,
			                            CASE WHEN test_code = 'RDWSD' THEN measurement END AS RDWSD,
			                            CASE WHEN test_code = 'PCT' THEN measurement END AS PCT,
			                            CASE WHEN test_code = 'PDW' THEN measurement END AS PDW,

			                            CASE WHEN test_code = 'NEU_P' THEN measurement END AS NEU_P,
			                            CASE WHEN test_code = 'LYM_P' THEN measurement END AS LYM_P,
			                            CASE WHEN test_code = 'MON_P' THEN measurement END AS MON_P,
			                            CASE WHEN test_code = 'EOS_P' THEN measurement END AS EOS_P,
			                            CASE WHEN test_code = 'BAS_P' THEN measurement END AS BAS_P,
			
			                            CASE WHEN test_code = 'NEU_N' THEN measurement END AS NEU_N,
			                            CASE WHEN test_code = 'LYM_N' THEN measurement END AS LYM_N,
			                            CASE WHEN test_code = 'MON_N' THEN measurement END AS MON_N,
			                            CASE WHEN test_code = 'EOS_N' THEN measurement END AS EOS_N,
			                            CASE WHEN test_code = 'BAS_N' THEN measurement END AS BAS_N,
			
			                            CASE WHEN test_code = 'BType' THEN measurement END AS BType,
			                            CASE WHEN test_code = 'ESR' THEN measurement END AS ESR,
			                            CASE WHEN test_code = 'CT' THEN measurement END AS CT,
			                            CASE WHEN test_code = 'BT' THEN measurement END AS BT,
			                            CASE WHEN test_code = 'APTT' THEN measurement END AS APTT,
			                            CASE WHEN test_code = 'PTT' THEN measurement END AS PTT,
			                            `order`.`date`
		                            FROM
			                            result
		                            LEFT JOIN `order` ON result.sample_id = `order`.sample_id
		                            WHERE `order`.`testtype` = 'Hematology' AND `order`.`date` BETWEEN @DateFrom AND @DateTo 
			                            ) AS Hematology WHERE `SampleID` LIKE '" & txtSearch.Text & "%' OR `PatientID` LIKE '" & txtSearch.Text & "%' OR `PatientName` LIKE '" & txtSearch.Text & "%' GROUP BY SampleID ;"

            Dim command As New MySql.Data.MySqlClient.MySqlCommand(Sql, conn)

            command.Parameters.Clear()
            command.Parameters.AddWithValue("@DateFrom", Format(CDate(dtFrom.Value.ToShortDateString & " " & tmFrom.Value.ToLongTimeString), "yyyy-MM-dd hh:mm:ss tt"))
            command.Parameters.AddWithValue("@DateTo", Format(CDate(dtTo.Value.ToShortDateString & " " & tmTo.Value.ToLongTimeString), "yyyy-MM-dd hh:mm:ss tt"))

            Dim adapter As New MySql.Data.MySqlClient.MySqlDataAdapter(command)

            Dim myTable As DataTable = New DataTable
            adapter.Fill(myTable)

            dtResult.DataSource = myTable

            ' Make the grid read-only. 
            GridView.OptionsBehavior.Editable = False
            ' Prevent the focused cell from being highlighted. 
            GridView.OptionsSelection.EnableAppearanceFocusedCell = False
            ' Draw a dotted focus rectangle around the entire row. 
            GridView.FocusRectStyle = DrawFocusRectStyle.RowFullFocus
            GridView.OptionsView.BestFitMode = GridBestFitMode.Fast

            GridView.Columns("Instrument").Visible = False
            GridView.Columns("Section").Visible = False
            GridView.Columns("SubSection").Visible = False
            GridView.Columns("Date").Visible = False
            Disconnect()

            Me.lblCount.Text = "COUNT: " & Me.GridView.RowCount
            
        End If
        Catch ex As Exception
        MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    '################################################## END ###################################################################

    Private Sub frmWorkSheet_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        AutoLoadTestName()
        'AutoLoadMedtech()
        'AutoloadShift()

        dtFrom.Text = "01/01/2000"
        tmFrom.Text = "00:00:00 AM"
        tmTo.Text = "11:59:59 PM"
    End Sub

End Class