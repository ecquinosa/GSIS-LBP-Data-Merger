
#Region " Imports "

Imports System.Windows.Forms
Imports System.IO
Imports System.Threading

#End Region

Public Class DataMerger
    Implements IMessageFilter

    Public Function PreFilterMessage(ByRef m As Message) As Boolean Implements IMessageFilter.PreFilterMessage
        '' Retrigger timer on keyboard and mouse messages
        If (m.Msg >= &H100 And m.Msg <= &H109) Or (m.Msg >= &H200 And m.Msg <= &H20E) Then
            idleTimer.Stop()
            idleTimer.Start()
        End If
    End Function

    Private Sub idleTimer_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs)
        idleTimer.Stop()
        MessageBox.Show("Your session has expired due to inactivity.", "SESSION EXPIRED", MessageBoxButtons.OK, MessageBoxIcon.Information)
        CloseApplication()
    End Sub

    Private Sub CloseApplication()
        Application.Exit()
        Me.Close()
    End Sub

    Private idleTimer As New System.Windows.Forms.Timer

#Region " Public Variable "

    Private intGSISUMID_GoodCntr As Integer = 0
    Private intGSISUMID_BadCntr As Integer = 0
    Private intDECEMBFLE_GoodCntr As Integer = 0
    Private intDECEMBFLE_BadCntr As Integer = 0
    Private intmappingfile_GoodCntr As Integer = 0
    Private intmappingfile_BadCntr As Integer = 0
    Private intGSISBRCode_GoodCntr As Integer = 0
    Private intGSISBRCode_BadCntr As Integer = 0
    Private intGSISNo_GoodCntr As Integer = 0
    Private intGSISNo_BadCntr As Integer = 0

    Private intReferencefileTotal As Integer = 0
    Private intMappingfileTotal As Integer = 0
    Private intEmbossingfileTotal As Integer = 0
    Private intBRCodefileTotal As Integer = 0
    Private intJAI2fileTotal As Integer = 0

    Private intGSISNoAddr_GoodCntr As Integer = 0
    Private intGSISNoAddr_BadCntr As Integer = 0
    Private intAddrfileTotal As Integer = 0

    Private objReferenceThread As Thread
    Private objMappingThread As Thread
    Private objEmbossingThread As Thread
    Private objBRCodeThread As Thread
    Private objJAI2Thread As Thread

    Private objAddrThread As Thread

    Private objProcessWrapper As Thread

    Private dtBRCODE As DataTable
    Private dtGSISNo As DataTable
    Private dtADDRESS As DataTable

    Private IsReferenceDone As Boolean
    Private IsMappingDone As Boolean
    Private IsEmbossingDone As Boolean
    Private IsBRCodeDone As Boolean
    Private IsJAI2Done As Boolean
    Private IsAddrDone As Boolean

#End Region

#Region " Click Event "

    'Private Sub cmdBrowseReferenceFile_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    BrowseFile(txtReferenceFile)
    '    intReferencefileTotal = GetTextfileTotalRecords(txtReferenceFile.Text)
    'End Sub

    Private Sub cmdBrowseMappingFile_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdBrowseMappingFile.Click
        BrowseFile(txtMappingFile)
        intMappingfileTotal = GetTextfileTotalRecords(txtMappingFile.Text)
    End Sub

    Private Sub cmdBrowseEmbossingFile_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdBrowseEmbossingFile.Click
        BrowseFile(txtEmbossingFile, "Text/OUT Files|*.txt;*.out")
        intEmbossingfileTotal = GetTextfileTotalRecords(txtEmbossingFile.Text)
    End Sub


    'Private Sub cmdBrowseExcelFile_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    cmdLoad.BackColor = Color.Transparent
    '    LoadFile(txtExcelFile)
    '    LoadSheets(txtExcelFile.Text, cboSheets)
    'End Sub

    'Private Sub cmdLoad_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Try
    '        LoadExcelFile(txtExcelFile.Text, cboSheets.Text, dtBRCODE)
    '        cmdLoad2.BackColor = Color.Green
    '        intBRCodefileTotal = dtBRCODE.DefaultView.Count
    '    Catch ex As Exception
    '    End Try
    'End Sub

    Private Sub cmdMerge_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdMerge.Click
        MergeData()
    End Sub

    Private Sub MergeData()
        If Not ValidateFiles() Then Exit Sub

        If SharedFunction.ShowMessage("Embossing file will be deleted after the process. Make sure you have backup." & vbNewLine & vbNewLine & "Continue?") = DialogResult.No Then Return

        FormDispo(False)

        lblRecord.Text = ""
        rtbLog.Clear()

        IsReferenceDone = True
        IsMappingDone = False
        IsEmbossingDone = False
        IsBRCodeDone = False
        IsJAI2Done = False
        IsAddrDone = False

        Dim DBCon As New DBCon
        Dim intGSISUMIDCntr As Integer = 0
        Dim intDECEMBFLECntr As Integer = 0
        Dim intmappingfileCntr As Integer = 0
        Dim intGSISBRCodeCntr As Integer = 0
        Dim intGSISNoCntr As Integer = 0
        Dim intGSISNoAddrCntr As Integer = 0
        Dim intAddrCntr As Integer = 0

        'If DBCon.GetRecordCount("GSISUMID", intGSISUMIDCntr) Then
        '    If intGSISUMIDCntr = 0 Then
        '        StartThread(FileRef.Reference, objReferenceThread, intReferencefileTotal, txtReferenceFile.Text)
        '    Else
        '        If MessageBox.Show("GSISUMID table have " & intGSISUMIDCntr.ToString & " record(s). Delete existing record(s)?", Me.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
        '            'delete dbase
        '            DBCon.DeleteTableRecords("GSISUMID")
        '            StartThread(FileRef.Reference, objReferenceThread, intReferencefileTotal, txtReferenceFile.Text)
        '        Else
        '            IsReferenceDone = True
        '        End If
        '    End If
        'End If

        If DBCon.GetRecordCount("DECEMBFLE", intDECEMBFLECntr) Then
            If intDECEMBFLECntr = 0 Then
                StartThread(FileRef.Embossing, objEmbossingThread, intEmbossingfileTotal, txtEmbossingFile.Text)
                IsEmbossingDone = True
            Else
                If MessageBox.Show("DECEMBFLE table have " & intDECEMBFLECntr.ToString & " record(s). Delete existing record(s)?", Me.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    'delete dbase
                    DBCon.DeleteTableRecords("DECEMBFLE")
                    StartThread(FileRef.Embossing, objEmbossingThread, intEmbossingfileTotal, txtEmbossingFile.Text)
                    IsEmbossingDone = True
                Else
                    IsEmbossingDone = True
                End If
            End If
        End If

        If DBCon.GetRecordCount("mapping_file", intmappingfileCntr) Then
            If intmappingfileCntr = 0 Then
                StartThread(FileRef.Mapping, objMappingThread, intMappingfileTotal, txtMappingFile.Text)
                IsMappingDone = True
            Else
                If MessageBox.Show("mapping_file table have " & intmappingfileCntr.ToString & " record(s). Delete existing record(s)?", Me.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    'delete dbase
                    DBCon.DeleteTableRecords("mapping_file")
                    StartThread(FileRef.Mapping, objMappingThread, intMappingfileTotal, txtMappingFile.Text)
                    IsMappingDone = True
                Else
                    IsMappingDone = True
                End If
            End If
        End If

        If Not dtBRCODE Is Nothing Then
            If dtBRCODE.DefaultView.Count > 0 Then
                If DBCon.GetRecordCount("GSIS_BranchCode", intGSISBRCodeCntr) Then
                    If intGSISBRCodeCntr = 0 Then
                        StartThread(FileRef.BR, objBRCodeThread, intBRCodefileTotal, , dtBRCODE)
                        IsBRCodeDone = True
                    Else
                        If MessageBox.Show("GSIS_BranchCode table have " & intGSISBRCodeCntr.ToString & " record(s). Delete existing record(s)?", Me.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                            'delete dbase
                            DBCon.DeleteTableRecords("GSIS_BranchCode")
                            StartThread(FileRef.BR, objBRCodeThread, intBRCodefileTotal, , dtBRCODE)
                            IsBRCodeDone = True
                        Else
                            IsBRCodeDone = True
                        End If
                    End If
                End If
            Else
                IsBRCodeDone = True
            End If
        Else
            IsBRCodeDone = True
        End If

        If cboType.Text = "REPLACEMENT" Then
            DBCon.DeleteTableRecords("GSIS_Addr")
            IsAddrDone = True
        Else
            If DBCon.GetRecordCount("GSIS_Addr", intAddrCntr) Then
                If intAddrCntr = 0 Then
                    StartThread(FileRef.GSIS_Addr, objAddrThread, intAddrfileTotal, txtAddress.Text)
                    IsAddrDone = True
                Else
                    If MessageBox.Show("GSIS_Addr table have " & intAddrCntr.ToString & " record(s). Delete existing record(s)?", Me.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                        'delete dbase
                        DBCon.DeleteTableRecords("GSIS_Addr")
                        StartThread(FileRef.GSIS_Addr, objAddrThread, intAddrfileTotal, txtAddress.Text)
                        IsAddrDone = True
                    Else
                        IsAddrDone = True
                    End If
                End If
            End If
        End If

        'If Not dtADDRESS Is Nothing Then
        '    If dtADDRESS.DefaultView.Count > 0 Then
        '        If DBCon.GetRecordCount("GSIS_Addr", intGSISNoAddrCntr) Then
        '            If intGSISBRCodeCntr = 0 Then
        '                StartThread(FileRef.GSIS_Addr, objAddrThread, intAddrfileTotal, , dtADDRESS)
        '            Else
        '                If MessageBox.Show("GSIS_Addr table have " & intGSISNoAddrCntr.ToString & " record(s). Delete existing record(s)?", Me.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
        '                    'delete dbase
        '                    DBCon.DeleteTableRecords("GSIS_Addr")
        '                    StartThread(FileRef.GSIS_Addr, objAddrThread, intAddrfileTotal, , dtADDRESS)
        '                Else
        '                    IsAddrDone = True
        '                End If
        '            End If
        '        End If
        '    Else
        '        IsAddrDone = True
        '    End If
        'Else
        '    IsAddrDone = True
        'End If

        If chkWithJAI2.Checked Then
            If Not dtGSISNo Is Nothing Then
                If dtGSISNo.DefaultView.Count > 0 Then
                    If DBCon.GetRecordCount("JAI2_UMID", intGSISNoCntr) Then
                        If intGSISNoCntr = 0 Then
                            StartThread(FileRef.JAI2, objJAI2Thread, intJAI2fileTotal, , dtGSISNo)
                            IsJAI2Done = True
                        Else
                            If MessageBox.Show("JAI2_UMID table have " & intGSISNoCntr.ToString & " record(s). Delete existing record(s)?", Me.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                                'delete dbase
                                DBCon.DeleteTableRecords("JAI2_UMID")
                                StartThread(FileRef.JAI2, objJAI2Thread, intJAI2fileTotal, , dtGSISNo)
                                IsJAI2Done = True
                            Else
                                IsJAI2Done = True
                            End If
                        End If
                    End If
                Else
                    IsJAI2Done = True
                End If
            Else
                IsJAI2Done = True
            End If
        Else
            IsJAI2Done = True
        End If

        StartThreadWrapper()

        SharedFunction.AddSystemLog(String.Format("User {0} clicked merger button for files {1},{2},{3},{4}", SharedFunction.UserCompleteName, IO.Path.GetFileName(txtEmbossingFile.Text), IO.Path.GetFileName(txtMappingFile.Text), IO.Path.GetFileName(txtAddress.Text), IO.Path.GetFileName(txtExcelFile2.Text)))
    End Sub

    Dim dtError As DataTable
    Private Sub cboStorProc_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboStorProc.SelectedIndexChanged
        BindTableError()
    End Sub

    Private Sub BindTableError()
        Try
            Dim dbCon As New DBCon
            dtError = New DataTable
            If dbCon.SelectStorPorc(GetStorProcName, dtError) Then
                DataGridView1.DataSource = dtError
                lblRecord_tabTables.Text = dtError.DefaultView.Count.ToString & " record(s)"
            End If
        Catch ex As Exception
            lblRecord_tabTables.Text = ""
        End Try
    End Sub

#End Region

    Private Sub DataMerger_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed

    End Sub

    Private Sub DataMerger_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Try

        Catch ex As Exception

        End Try
    End Sub

    Private Sub DataMergerRevised_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            lblDate.Text = Now.ToLongDateString
            Label2.Text = "Powered By: Allcard Technologies, Corp. " & Year(Now)

            If Not System.IO.File.Exists(AllcardLicenseHandler.LicenseHandler.licenseFile) Then
                Do While TabControl1.TabPages(0).Name <> "tabLicense"
                    TabControl1.TabPages.RemoveAt(0)
                    btnGenerateLicense_License.Visible = True
                Loop
                MessageBox.Show("Please check license file", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If

            Dim response As String = ""
            If Not AllcardLicenseHandler.LicenseHandler.ValidateLicense(response) Then
                Do While TabControl1.TabPages(0).Name <> "tabLicense"
                    TabControl1.TabPages.RemoveAt(0)
                Loop

                MessageBox.Show(response, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                'If response.Contains("03") Then btnActivateLicense_License.Visible = True
                Exit Sub
            End If

            BindLicenseInfo()

            If Not CheckDBConnection() Then
                SharedFunction.ShowMessage("Communication to database failed. Please check your db configuration or database.", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

                Do While TabControl1.TabCount > 1
                    'If TabControl1.TabPages(0).Name <> "tabSettings" Then TabControl1.TabPages.RemoveAt(0)
                    TabControl1.TabPages.RemoveAt(0)
                Loop

                Return
            End If

            Me.Hide()

            'added on 04/12/2019
            Dim userLogin As New UserManagement.UserLogIn(SharedFunction.DecryptData(My.Settings.Dbase_Config))
            userLogin.ShowDialog()
            If Not userLogin.IsSuccess Then CloseApplication()

            SharedFunction.UserID = userLogin.UserParams(0)
            SharedFunction.UserName = userLogin.UserParams(1)
            SharedFunction.UserCompleteName = userLogin.UserParams(2)
            SharedFunction.UserPass = userLogin.UserParams(3)
            SharedFunction.RoleID = userLogin.UserParams(4)
            SharedFunction.RoleDesc = userLogin.UserParams(5)

            cboType.SelectedIndex = 0

            'TabControl1.TabPages.Remove(tabEncDec)

            AccessFile.InitializeFile()
            PopulateComboStorProc()
            'lblReferenceStatus.Text = ""
            lblMappingStatus.Text = ""
            lblEmbossingStatus.Text = ""
            'lblBranchStatus.Text = ""
            lblJAI2Status.Text = ""
            lblStatusAddr.Text = ""
            lblRecord.Text = ""

            'If My.Settings.DBASE = "" Then
            If AccessFile.Read = "" Then
                Dim frm As New Settings
                frm.ShowDialog()
                If Not frm.IsSaveButtonClicked Then CloseApplication()
                'Else
                '    If Not File.Exists(My.Settings.DBASE) Then
                '        ShowMessageBox("Please enter valid access file", , MessageBoxIcon.Error)
                '        Dim frm As New Settings
                '        frm.ShowDialog()
                '        If Not frm.IsSaveButtonClicked Then Close()
                '    End If
            End If

            Dim DAL As New DAL
            Dim dtRoleModules As DataTable = Nothing
            If DAL.SelectQuery("SELECT ModuleID FROM tblRelRoleModule WHERE RoleID IN (SELECT RoleID FROM tblRelUserRole WHERE UserID=" & SharedFunction.UserID & ")") Then
                dtRoleModules = DAL.TableResult
            End If
            DAL.Dispose()
            DAL = Nothing

            If dtRoleModules Is Nothing Then
                SharedFunction.ShowExcMessage("No module assigned for this role")
                NoAccess()
            Else
                If dtRoleModules.DefaultView.Count = 0 Then
                    SharedFunction.ShowExcMessage("No module assigned for this role")
                    NoAccess()
                Else
                    If dtRoleModules.Select("ModuleID=" & UserManagement.DataKeysEnum.ModuleID.Files_TAB_DATAMERGER).Length = 0 Then TabControl1.TabPages.Remove(tabFiles)
                    If dtRoleModules.Select("ModuleID=" & UserManagement.DataKeysEnum.ModuleID.Output_TAB_DATAMERGER).Length = 0 Then TabControl1.TabPages.Remove(tabOutput)
                    If dtRoleModules.Select("ModuleID=" & UserManagement.DataKeysEnum.ModuleID.Log_TAB_DATAMERGER).Length = 0 Then TabControl1.TabPages.Remove(tabLog)
                    If dtRoleModules.Select("ModuleID=" & UserManagement.DataKeysEnum.ModuleID.Tables_TAB_DATAMERGER).Length = 0 Then TabControl1.TabPages.Remove(tabTables)
                    If dtRoleModules.Select("ModuleID=" & UserManagement.DataKeysEnum.ModuleID.EncryptionDecryption_TAB_DATAMERGER).Length = 0 Then TabControl1.TabPages.Remove(tabEncDec)
                    If dtRoleModules.Select("ModuleID=" & UserManagement.DataKeysEnum.ModuleID.License_TAB_DATAMERGER).Length = 0 Then
                        TabControl1.TabPages.Remove(tabLicense)
                        LinkLabel1.Visible = False
                    End If
                End If
            End If

            Application.AddMessageFilter(Me)
            AddHandler idleTimer.Tick, AddressOf idleTimer_Tick
            idleTimer.Interval = SharedFunction.GetIdleTimeout * 60000
            idleTimer.Enabled = True
        Catch ex As Exception

        End Try
    End Sub

    Private Sub NoAccess()
        LinkLabel1.Visible = False
        TabControl1.TabPages.Remove(tabFiles)
        TabControl1.TabPages.Remove(tabOutput)
        TabControl1.TabPages.Remove(tabLog)
        TabControl1.TabPages.Remove(tabTables)
        TabControl1.TabPages.Remove(tabEncDec)
        TabControl1.TabPages.Remove(tabLicense)
    End Sub

#Region " Miscellaneous "

    Private Function FormDispo(ByVal bln As Boolean)
        TabControl1.Enabled = bln

        If bln Then
            Cursor = Cursors.Default
        Else
            Cursor = Cursors.WaitCursor
        End If
    End Function

    Private Sub PopulateComboStorProc()
        cboStorProc.Items.Add("- Select Item -")
        cboStorProc.Items.Add("Embossing File - Duplicate Card Number") '1
        cboStorProc.Items.Add("Embossing File - Duplicate GSIS Number") '2
        cboStorProc.Items.Add("Embossing File - Mapping File not matched") '3
        cboStorProc.Items.Add("Embossing File - JAI2 File not matched") '4
        cboStorProc.Items.Add("Embossing File - GSIS_Addr File not matched") '5
        cboStorProc.Items.Add("Mapping File - Duplicate CRN") '6
        cboStorProc.Items.Add("Mapping File - Duplicate GSIS Number") '7
        cboStorProc.Items.Add("GSIS Addr File - Duplicate GSIS Number") '8
        cboStorProc.Items.Add("JAI2 File - Duplicate GSIS Number") '9
        cboStorProc.SelectedIndex = 0

        'cboStorProc.Items.Add("Reference File - Duplicate AcctNo") '1
        'cboStorProc.Items.Add("Reference File - Embossing File not matched") '2
        'cboStorProc.Items.Add("Reference File - Duplicate GSIS#") '3
        'cboStorProc.Items.Add("Reference File - Mapping File not matched") '4
        'cboStorProc.Items.Add("Embossing File - Duplicate Card Number") '5
        'cboStorProc.Items.Add("Embossing File - Reference File not matched") '6
        'cboStorProc.Items.Add("Embossing File - Mapping File not matched") '7
        'cboStorProc.Items.Add("Embossing File - JAI2 File not matched") '8
    End Sub

    Private Function GetStorProcName() As String
        Select Case cboStorProc.SelectedIndex
            Case 1
                Return "qryDECEMBFLE_CardNo_duplicate"
            Case 2
                Return "qryDECEMBFLE_GSISNo_duplicate"
            Case 3
                Return "qryDECEMBFLE_mappingfile_notmatched"
            Case 4
                Return "qryDECEMBFLE_JAI2file_notmatched"
            Case 5
                Return "qryDECEMBFLE_GSIS_Addr_notmatched"
            Case 6
                Return "qrymappingfile_CRN_duplicate"
            Case 7
                Return "qrymappingfile_GSISNo_duplicate"
            Case 8
                Return "qryGSISAddr_GSISNo_duplicate"
            Case 9
                Return "qryJAI2_GSISNo_duplicate"
        End Select
    End Function

    Private Function ValidateFiles() As Boolean
        Dim bln As Boolean = True

        'If Not File.Exists(txtReferenceFile.Text) Then
        '    txtReferenceFile.Focus()
        '    txtReferenceFile.SelectAll()
        '    ShowMessageBox("Please enter reference file...", , MessageBoxIcon.Error)
        '    bln = False
        'Else       

        If Not File.Exists(txtEmbossingFile.Text) Then
            txtEmbossingFile.Focus()
            txtEmbossingFile.SelectAll()
            ShowMessageBox("Please enter embossing file...", , MessageBoxIcon.Error)
            bln = False
        Else
            If Not File.Exists(txtMappingFile.Text) Then
                txtMappingFile.Focus()
                txtMappingFile.SelectAll()
                ShowMessageBox("Please enter mapping file...", , MessageBoxIcon.Error)
                bln = False
            Else
                If cboType.Text <> "REPLACEMENT" And Not File.Exists(txtAddress.Text) Then
                    txtAddress.Focus()
                    txtAddress.SelectAll()
                    ShowMessageBox("Please enter address file...", , MessageBoxIcon.Error)
                    bln = False
                Else
                    If chkWithJAI2.Checked Then
                        If Not File.Exists(txtExcelFile2.Text) Then
                            txtEmbossingFile.Focus()
                            txtEmbossingFile.SelectAll()
                            ShowMessageBox("Please enter jai2 umid file...", , MessageBoxIcon.Error)
                            bln = False
                        End If
                    End If
                End If
            End If
        End If
        'End If

        If bln Then
            'If txtReferenceFile.Text = txtMappingFile.Text Then
            '    txtMappingFile.Focus()
            '    txtMappingFile.SelectAll()
            '    ShowMessageBox("Please enter mapping file different from reference file...", , MessageBoxIcon.Error)
            '    bln = False
            If txtMappingFile.Text = txtEmbossingFile.Text Then
                txtEmbossingFile.Focus()
                txtEmbossingFile.SelectAll()
                ShowMessageBox("Please enter embossing file different from mapping file...", , MessageBoxIcon.Error)
                bln = False
                'ElseIf txtReferenceFile.Text = txtEmbossingFile.Text Then
                '    txtEmbossingFile.Focus()
                '    txtEmbossingFile.SelectAll()
                '    ShowMessageBox("Please enter embossing file different from reference file...", , MessageBoxIcon.Error)
                '    bln = False
            ElseIf txtAddress.Text = txtEmbossingFile.Text Then
                txtAddress.Focus()
                txtAddress.SelectAll()
                ShowMessageBox("Please enter address file different from embossing file...", , MessageBoxIcon.Error)
                bln = False
            ElseIf txtAddress.Text = txtMappingFile.Text Then
                txtAddress.Focus()
                txtAddress.SelectAll()
                ShowMessageBox("Please enter address file different from mapping file...", , MessageBoxIcon.Error)
                bln = False
                'ElseIf txtExcelFile.Text <> "" Then
                '    If dtBRCODE Is Nothing Then
                '        txtExcelFile.Focus()
                '        txtExcelFile.SelectAll()
                '        ShowMessageBox("Please select br code sheet to load...", , MessageBoxIcon.Error)
                '        bln = False
                '    Else
                '        If dtBRCODE.DefaultView.Count = 0 Then
                '            txtExcelFile.Focus()
                '            txtExcelFile.SelectAll()
                '            ShowMessageBox("Please select br code sheet to load...", , MessageBoxIcon.Error)
                '            bln = False
                '        End If
                '    End If
            ElseIf txtExcelFile2.Text <> "" Then
                If dtGSISNo Is Nothing Then
                    txtExcelFile2.Focus()
                    txtExcelFile2.SelectAll()
                    ShowMessageBox("Please select jai2 sheet to load...", , MessageBoxIcon.Error)
                    bln = False
                Else
                    If dtGSISNo.DefaultView.Count = 0 Then
                        txtExcelFile2.Focus()
                        txtExcelFile2.SelectAll()
                        ShowMessageBox("Please select jai2 sheet to load...", , MessageBoxIcon.Error)
                        bln = False
                    End If
                End If
            End If
        End If


        Return bln
    End Function

    Private Sub ShowMessageBox(ByVal strMsg As String, Optional ByVal msgBoxBtns As MessageBoxButtons = MessageBoxButtons.OK, Optional ByVal msgBoxIcn As MessageBoxIcon = MessageBoxIcon.Information)
        MessageBox.Show(strMsg, Me.Text, msgBoxBtns, msgBoxIcn)
    End Sub

    Private Sub BrowseFile(ByVal txt As TextBox, Optional ByVal FileType As String = "Text Files (*.txt)|*.txt")
        Dim openfile As New OpenFileDialog
        openfile.Title = "Select file for merging..."
        'openfile.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*"
        openfile.Filter = FileType
        'openfile.FilterIndex = 2
        'openfile.RestoreDirectory = True
        If openfile.ShowDialog = DialogResult.OK Then
            txt.Text = openfile.FileName
            Application.DoEvents()
        End If
        openfile.Dispose()
        openfile = Nothing
    End Sub

    Private Sub WriteToLog(ByVal strData As String, Optional ByVal IsDoubleLine As Boolean = False, Optional ByVal IsSingleLine As Boolean = False)
        If IsDoubleLine Then rtbLog.AppendText(vbNewLine)
        rtbLog.AppendText(strData & vbNewLine)
        If IsDoubleLine Then rtbLog.AppendText("==================================================" & vbNewLine & vbNewLine)
        If IsDoubleLine Then rtbLog.AppendText(vbNewLine)
        If IsSingleLine Then rtbLog.AppendText("--------------------------------------------------" & vbNewLine & vbNewLine)
    End Sub

    Private dt As New DataTable
    'Private tempOutputFile As String = Application.StartupPath & "\tempOutput"

    Private Sub BindData()
        Dim DBCon As New DBCon

        Dim intRecordCntr As Integer = 0

        rtbOutput.Clear()
        rtbOutput.AppendText("CRN|Account#|Track1|Track2|GSIS#|BP#|Card#|CardExpiry|CVV|MemberType|Address|Addr_BranchCode|GSIS_BranchCode|GSIS_BranchGroup|AgencyName|Emboss_BranchCode|Emboss_AgencyID|EmbossOrigData" & vbNewLine)
        dt.Clear()

        Dim intCntr As Integer = 0
        Dim bln As Boolean

        If chkWithJAI2.Checked Then
            bln = DBCon.SelectDataForMerging(dt)
        Else
            bln = DBCon.SelectDataForMerging2(dt)
        End If

        If bln Then
            If dt.DefaultView.Count > 0 Then
                Dim IsFirstCol As Boolean

                For Each rw As DataRow In dt.Rows
                    Dim sbData As New System.Text.StringBuilder
                    'Dim strData As String = ""
                    IsFirstCol = True

                    For Each col As DataColumn In dt.Columns
                        'If rw(col).ToString = "02004824724" Then
                        '    Console.Write("TEST")
                        'End If

                        If IsFirstCol Then
                            'revised on May 2,2019 per lbp requirement
                            'sbData.Append(rw(col).ToString)
                            sbData.Append(ConvertNullToString(rw(col)))
                            IsFirstCol = False
                        Else
                            'revised on May 2,2019 per lbp requirement
                            'sbData.Append("|" & rw(col).ToString)
                            sbData.Append("|" & ConvertNullToString(rw(col)))
                        End If
                    Next

                    rtbOutput.AppendText(sbData.ToString & vbNewLine)

                    intCntr += 1

                    lblRecord.Text = "Binding data...      " & intCntr.ToString & " of " & dt.DefaultView.Count.ToString("N0")
                    Application.DoEvents()
                Next

                'rtbOutput.SaveFile(tempOutputFile, RichTextBoxStreamType.PlainText)
            End If
        Else
            MessageBox.Show("Unable to get merging query." & vbNewLine & vbNewLine & DBCon.ErrorMessage)
        End If

        lblRecord.Text = "Total: " & dt.DefaultView.Count.ToString("N0")
        Application.DoEvents()
        DBCon = Nothing
        GC.Collect()
    End Sub

    Private Function ConvertNullToString(ByVal obj As Object) As String
        If IsDBNull(obj) Then
            Return ""
        Else
            Return obj.ToString()
        End If
    End Function

    Private Function GetBRCode(ByVal strGSIS As String) As String
        Try
            'Return ""
            Dim dbCon As New DBCon
            Dim dt As New DataTable
            If dbCon.SelectGSIS_BranchCodeByGSISNo(strGSIS, dt) Then
                If dt.DefaultView.Count = 0 Then
                    Return ""
                Else
                    Return Trim(dt.Rows(0)("BRCode"))
                End If
            Else
                Return ""
            End If
        Catch ex As Exception
            Return ""
        End Try
    End Function

    Private Function GetTextfileTotalRecords(ByVal txtfile As String) As Integer
        If txtfile = "" Then Return 0
        Return File.ReadAllLines(txtfile).Length.ToString
    End Function

#Region " Excel "

    Private Function ExcelConStr(ByVal strExcelPath As String) As String
        'Return "Provider=Microsoft.Jet.OLEDB.4.0;Excel 8.0; Extended Properties=HDR=Yes; IMEX=1;Data Source=" + strExcelPath + ""
        If System.IO.Path.GetExtension(strExcelPath).ToUpper() = ".XLS" Then
            Return "Provider=Microsoft.Jet.OLEDB.4.0;Excel 8.0; Extended Properties=HDR=Yes; IMEX=1;Data Source=" + strExcelPath + ""
        Else
            Return "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + strExcelPath + ";Extended Properties=Excel 12.0;"
        End If
    End Function

    Public Sub LoadExcelFile(ByVal strExcelPath As String, ByVal strExcelSheet As String, ByRef dt As DataTable)
        Try
            Dim ds As New DataSet()
            Dim con As New System.Data.OleDb.OleDbConnection(ExcelConStr(strExcelPath))
            Dim cmd As System.Data.OleDb.OleDbCommand
            'cmd = New System.Data.OleDb.OleDbCommand("SELECT * FROM [" + strExcelSheet + "$] WHERE [last name] <> ''", con)
            'cmd = New System.Data.OleDb.OleDbCommand("SELECT [GSIS NO], [BR CODE] FROM [" + strExcelSheet + "]", con)
            cmd = New System.Data.OleDb.OleDbCommand("SELECT [GSIS NUMBER] FROM [" + strExcelSheet + "]", con)
            cmd.CommandType = CommandType.Text

            con.Open()

            Dim da As New System.Data.OleDb.OleDbDataAdapter(cmd)
            da.Fill(ds, "Result")
            'da.Fill(dt)

            dt = ds.Tables(0)
            con.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Failed to load excel file...", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Public Sub LoadExcelFileAddr(ByVal strExcelPath As String, ByVal strExcelSheet As String, ByRef dt As DataTable)
        Try
            Dim ds As New DataSet()
            Dim con As New System.Data.OleDb.OleDbConnection(ExcelConStr(strExcelPath))
            Dim cmd As System.Data.OleDb.OleDbCommand
            'cmd = New System.Data.OleDb.OleDbCommand("SELECT [GSIS ID NUMBER] FROM [" + strExcelSheet + "]", con)
            cmd = New System.Data.OleDb.OleDbCommand("SELECT * FROM [" + strExcelSheet + "]", con)
            cmd.CommandType = CommandType.Text

            con.Open()

            Dim da As New System.Data.OleDb.OleDbDataAdapter(cmd)
            da.Fill(ds, "Result")
            'da.Fill(dt)

            dt = ds.Tables(0)
            con.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Failed to load excel file...", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Public Sub LoadFile(ByRef txtFile As TextBox, Optional ByVal intFileType As Short = 0)
        Dim openDLG As New OpenFileDialog

        openDLG.InitialDirectory = Application.StartupPath
        If intFileType = 0 Then 'excel
            openDLG.Filter = "Microsoft Excel 2003 | *.xls; *.xlsx"
        ElseIf intFileType = 1 Then 'notepad
            openDLG.Filter = "Text Files | *.txt"
        End If


        If openDLG.ShowDialog = Windows.Forms.DialogResult.OK Then
            txtFile.Text = openDLG.FileName
        End If

        openDLG.Dispose()
    End Sub

    Public Sub LoadSheets(ByVal strExcelPath As String, ByVal cboSheet As ComboBox)
        Try
            Dim con As New System.Data.OleDb.OleDbConnection(ExcelConStr(strExcelPath))
            con.Open()

            Dim dtSheets As DataTable = con.GetOleDbSchemaTable(OleDb.OleDbSchemaGuid.Tables, Nothing)

            con.Close()

            For Each rw As DataRow In dtSheets.Rows
                cboSheet.Items.Add(rw("TABLE_NAME"))
            Next


            If cboSheet.Items.Count > 0 Then cboSheet.SelectedIndex = 0

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Failed to load sheets...", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

#End Region

#End Region

    Private Sub LinkLabel1_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        Dim frm As New Settings
        frm.ShowDialog()
    End Sub

    Private Sub cmdSaveOutput_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSaveOutput.Click
        'Invoke(New Action(AddressOf BindData))

        If rtbOutput.Text = "" Then
            SharedFunction.ShowExcMessage("No data to save")
            Exit Sub
        End If

        Dim intLicenseBalance As Integer = AllcardLicenseHandler.LicenseHandler.GetLicenseBalance

        If intLicenseBalance < dt.DefaultView.Count Then
            SharedFunction.ShowExcMessage("License balance is insufficient")
            Exit Sub
        End If

        'TempSave(Application.StartupPath & "\outputFile.txt")
        'Return


        'If SaveFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
        '    Dim fileName As String = SaveFileDialog1.FileName.Replace(".txt", "") & ".txt"
        '    rtbOutput.SaveFile(fileName, RichTextBoxStreamType.PlainText)

        '    Dim outputFile As String = ""
        '    Dim response As String = EncryptDecryptFile(fileName, True, outputFile)

        '    If response = "" Then
        '        Dim intBalance As Integer = intLicenseBalance - dt.DefaultView.Count
        '        SharedFunction.SaveToLog(String.Format("{0}cmdSaveOutput_Click(): {1} have {2} records. Balance {3}", SharedFunction.TimeStamp, fileName, dt.DefaultView.Count.ToString("N0"), intBalance.ToString("N0")))
        '        AllcardLicenseHandler.LicenseHandler.UpdateLicenseBalance(intBalance)
        '        My.Settings.LicenseBalance = intBalance
        '        My.Settings.Save()
        '        File.Delete(fileName)
        '        BindLicenseInfo()
        '        SharedFunction.ShowInfoMessage("File is successfully encrypted and saved at '" & outputFile & "'.")
        '    Else
        '        File.Delete(fileName)
        '        SharedFunction.ShowErrorMessage("File encryption failed")
        '    End If
        'End If
        'SaveFileDialog1.Dispose()

        Dim savefile As New SaveFileDialog
        'savefile.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory
        If savefile.ShowDialog = Windows.Forms.DialogResult.OK Then
            Dim fileName As String = savefile.FileName.Replace(".txt", "") & ".txt"
            rtbOutput.SaveFile(fileName, RichTextBoxStreamType.PlainText)

            Dim outputFile As String = ""
            Dim response As String = EncryptDecryptFile(fileName, True, outputFile)

            If response = "" Then
                Dim intBalance As Integer = intLicenseBalance - dt.DefaultView.Count
                SharedFunction.SaveToLog(String.Format("{0}cmdSaveOutput_Click(): {1} have {2} records. Balance {3}", SharedFunction.TimeStamp, fileName, dt.DefaultView.Count.ToString("N0"), intBalance.ToString("N0")))
                AllcardLicenseHandler.LicenseHandler.UpdateLicenseBalance(intBalance)
                My.Settings.LicenseBalance = intBalance
                My.Settings.Save()
                File.Delete(fileName)
                BindLicenseInfo()
                SharedFunction.ShowInfoMessage("File is successfully encrypted and saved at '" & outputFile & "'.")

                SharedFunction.AddSystemLog(String.Format("User {0} saved and encrypted file {1}", SharedFunction.UserCompleteName, IO.Path.GetFileName(outputFile)))
                'If File.Exists(tempOutputFile) Then File.Delete(tempOutputFile)
            Else
                File.Delete(fileName)
                SharedFunction.ShowErrorMessage("File encryption failed")
            End If
        End If
        savefile.Dispose()
        savefile = Nothing
    End Sub

    Private Sub TempSave(ByVal tempFile As String)
        rtbOutput.SaveFile(tempFile, RichTextBoxStreamType.PlainText)

        Dim outputFile As String = ""
        Dim response As String = EncryptDecryptFile(tempFile, True, outputFile)

        If response = "" Then
            SharedFunction.ShowInfoMessage("File is successfully encrypted and saved at '" & outputFile & "'.")
        Else
            File.Delete(tempFile)
            SharedFunction.ShowErrorMessage("File encryption failed")
        End If
    End Sub

    Private Sub cmdSaveLog_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSaveLog.Click
        If rtbLog.Text = "" Then
            MessageBox.Show("No data to save", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        Dim savefile As New SaveFileDialog
        If savefile.ShowDialog = Windows.Forms.DialogResult.OK Then
            rtbLog.SaveFile(savefile.FileName.Replace(".txt", "") & ".txt", RichTextBoxStreamType.PlainText)

            MessageBox.Show("File is saved at '" & savefile.FileName.Replace(".txt", "") & ".txt" & "'", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
        savefile.Dispose()
    End Sub

    Private Sub cmdBrowseExcelFile2_Click(sender As System.Object, e As System.EventArgs) Handles cmdBrowseExcelFile2.Click
        cmdLoad2.BackColor = Color.Transparent
        LoadFile(txtExcelFile2)
        LoadSheets(txtExcelFile2.Text, cboSheets2)
    End Sub

    Private Sub cmdLoad2_Click(sender As System.Object, e As System.EventArgs) Handles cmdLoad2.Click
        Try
            LoadExcelFile(txtExcelFile2.Text, cboSheets2.Text, dtGSISNo)
            cmdLoad2.BackColor = Color.Green
            intJAI2fileTotal = dtGSISNo.DefaultView.Count
        Catch ex As Exception
        End Try
    End Sub

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs)
        MergeData()

        Return
        WriteToLog("Exceptions", True)

        For i As Short = 1 To cboStorProc.Items.Count - 1
            cboStorProc.SelectedIndex = i
            WriteExceptions(dtError, cboStorProc.Text)
        Next

        WriteToLog("", False, True)
    End Sub

    Private Sub btnReset_Click(sender As System.Object, e As System.EventArgs) Handles btnReset.Click
        If MessageBox.Show("Reset form?", Me.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
            ResetForm()
        End If
    End Sub

    Private Sub ResetForm()
        'txtReferenceFile.Clear()
        txtMappingFile.Clear()
        txtEmbossingFile.Clear()
        'txtExcelFile.Clear()
        'cboSheets.Text = ""
        'cboSheets.Items.Clear()
        txtExcelFile2.Clear()
        cboSheets2.Text = ""
        cboSheets2.Items.Clear()
        txtAddress.Clear()
        'cboSheetsAddr.Text = ""
        'cboSheetsAddr.Items.Clear()

        intGSISUMID_GoodCntr = 0
        intGSISUMID_BadCntr = 0
        intDECEMBFLE_GoodCntr = 0
        intDECEMBFLE_BadCntr = 0
        intmappingfile_GoodCntr = 0
        intmappingfile_BadCntr = 0
        intGSISBRCode_GoodCntr = 0
        intGSISBRCode_BadCntr = 0
        intGSISNo_GoodCntr = 0
        intGSISNo_BadCntr = 0
        intGSISNoAddr_GoodCntr = 0
        intGSISNoAddr_BadCntr = 0

        'lblReferenceStatus.Text = ""
        lblMappingStatus.Text = ""
        lblEmbossingStatus.Text = ""
        'lblBranchStatus.Text = ""
        lblJAI2Status.Text = ""
        lblRecord.Text = ""
        lblStatusAddr.Text = ""

        'cmdLoad.BackColor = Color.Transparent
        cmdLoad2.BackColor = Color.Transparent
        'cmdLoadAddr.BackColor = Color.Transparent

        chkWithJAI2.Checked = False

        If Not dtBRCODE Is Nothing Then dtBRCODE.Clear()
        If Not dtGSISNo Is Nothing Then dtGSISNo.Clear()
        If Not dtADDRESS Is Nothing Then dtADDRESS.Clear()
    End Sub

    Private Sub chkWithJAI2_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkWithJAI2.CheckedChanged
        txtExcelFile2.Enabled = chkWithJAI2.Checked
        cmdBrowseExcelFile2.Enabled = chkWithJAI2.Checked
        cboSheets2.Enabled = chkWithJAI2.Checked
        cmdLoad2.Enabled = chkWithJAI2.Checked
    End Sub

    Private Sub StartThread(ByVal FileRef As FileRef, ByRef obj As Thread, ByVal intTotalRecords As Integer,
                            Optional ByVal txtFile As String = "", Optional ByVal dt As DataTable = Nothing)
        Dim objThreadClass As New clsThread(FileRef, intTotalRecords, Me, txtFile, dt)
        Dim objNewThread As New Thread(AddressOf objThreadClass.StartThread)
        objNewThread.IsBackground = True
        objNewThread.Start()
        obj = objNewThread
    End Sub

    Private Sub StartThreadWrapper()
        Dim objThreadClass As New clsThread(FileRef.NA, 0, Me)
        Dim objNewThread As New Thread(AddressOf objThreadClass.StartThreadWrapper)
        objNewThread.IsBackground = True
        objNewThread.Start()
        objProcessWrapper = objNewThread
    End Sub

    Private Sub AbortThread(ByRef obj As Thread, ByVal result As String)
        If obj.IsAlive Then
            obj.Abort()
            obj = Nothing
            WriteToLog(result)
        End If
    End Sub

    Public Sub ReceiveThreadMessage(ByVal FileRef As FileRef, ByVal intCntr As Integer, ByVal result As String,
                                    ByVal errorMessage As String, ByVal IsProcessFinish As Boolean, ByVal intGood As Integer,
                                    ByVal intBad As Integer)
        Select Case FileRef
            'Case DataMerger.FileRef.Reference
            'lblReferenceStatus.Text = String.Format("{0}/{1}", intCntr.ToString("N0"), intReferencefileTotal.ToString("N0"))
            'IsReferenceDone = IsProcessFinish
               'If IsProcessFinish Then AbortThread(objReferenceThread, result)
            Case DataMerger.FileRef.Mapping
                lblMappingStatus.Text = String.Format("{0}/{1}", intCntr.ToString("N0"), intMappingfileTotal.ToString("N0"))
                IsMappingDone = IsProcessFinish
                intmappingfile_GoodCntr = intGood
                intmappingfile_BadCntr = intBad
                If IsProcessFinish Then AbortThread(objMappingThread, result)
            Case DataMerger.FileRef.Embossing
                lblEmbossingStatus.Text = String.Format("{0}/{1}", intCntr.ToString("N0"), intEmbossingfileTotal.ToString("N0"))
                IsEmbossingDone = IsProcessFinish
                intDECEMBFLE_GoodCntr = intGood
                intDECEMBFLE_BadCntr = intBad
                If IsProcessFinish Then
                    AbortThread(objEmbossingThread, result)
                End If
            'Case DataMerger.FileRef.BR
            '    lblBranchStatus.Text = String.Format("{0}/{1}", intCntr.ToString("N0"), intBRCodefileTotal.ToString("N0"))
            '    IsBRCodeDone = IsProcessFinish
            '    If IsProcessFinish Then AbortThread(objBRCodeThread, result)
            Case DataMerger.FileRef.JAI2
                lblJAI2Status.Text = String.Format("{0}/{1}", intCntr.ToString("N0"), intJAI2fileTotal.ToString("N0"))
                IsJAI2Done = IsProcessFinish
                If IsProcessFinish Then AbortThread(objJAI2Thread, result)
            Case DataMerger.FileRef.GSIS_Addr
                lblStatusAddr.Text = String.Format("{0}/{1}", intCntr.ToString("N0"), intAddrfileTotal.ToString("N0"))
                IsAddrDone = IsProcessFinish
                intGSISNoAddr_GoodCntr = intGood
                intGSISNoAddr_BadCntr = intBad
                If IsProcessFinish Then AbortThread(objAddrThread, result)
        End Select
    End Sub

    Enum FileRef
        NA
        Reference = 1
        Mapping
        Embossing
        BR
        JAI2
        GSIS_Addr
    End Enum

    Public Sub LastProcess()
        If IsReferenceDone And IsMappingDone And IsEmbossingDone And IsBRCodeDone And IsAddrDone And IsJAI2Done Then
            BindData()

            Dim strMessage As String = "Complete..."
            strMessage += vbNewLine + vbNewLine
            'strMessage += "GSISUMID" + vbNewLine
            'strMessage += "    Success: " + intGSISUMID_GoodCntr.ToString & " Failed: " & intGSISUMID_BadCntr.ToString + vbNewLine
            'strMessage += vbNewLine
            strMessage += "DECEMBFLE" + vbNewLine
            strMessage += "    Success: " + intDECEMBFLE_GoodCntr.ToString & " Failed: " & intDECEMBFLE_BadCntr.ToString + vbNewLine
            strMessage += vbNewLine
            strMessage += "mapping_file" + vbNewLine
            strMessage += "    Success: " + intmappingfile_GoodCntr.ToString & " Failed: " & intmappingfile_BadCntr.ToString + vbNewLine
            strMessage += vbNewLine
            strMessage += "GSIS_Addr" + vbNewLine
            strMessage += "    Success: " + intGSISNoAddr_GoodCntr.ToString & " Failed: " & intGSISNoAddr_BadCntr.ToString + vbNewLine
            'strMessage += vbNewLine
            'strMessage += "GSIS_BranchCode" + vbNewLine
            'strMessage += "    Success: " + intGSISBRCode_GoodCntr.ToString & " Failed: " & intGSISBRCode_BadCntr.ToString + vbNewLine

            If chkWithJAI2.Checked Then
                strMessage += vbNewLine
                strMessage += "JAI2_UMID" + vbNewLine
                strMessage += "    Success: " + intGSISNo_GoodCntr.ToString & " Failed: " & intGSISNo_BadCntr.ToString + vbNewLine
            End If

            WriteToLog("Exceptions", True)

            For i As Short = 1 To cboStorProc.Items.Count - 1
                cboStorProc.SelectedIndex = i
                WriteExceptions(dtError, cboStorProc.Text)
            Next

            WriteToLog("", False, True)

            objProcessWrapper.Abort()
            objProcessWrapper = Nothing

            System.IO.File.Delete(txtEmbossingFile.Text)

            SharedFunction.SaveToProcessLog("")
            SharedFunction.SaveToProcessLog(Strings.StrDup(100, "="))
            SharedFunction.SaveToProcessLog("MERGED DATA " & Now.ToString)
            SharedFunction.SaveToProcessLog("")
            SharedFunction.SaveToProcessLog(rtbLog.Text)

            ShowMessageBox(strMessage)

            FormDispo(True)
        End If
    End Sub

    Private Sub WriteExceptions(ByVal _dt As DataTable, ByVal desc As String)
        WriteToLog(desc & IIf(_dt.DefaultView.Count > 0, " (" & _dt.DefaultView.Count.ToString & " record(s))", ""))
        If _dt.DefaultView.Count = 0 Then WriteToLog("")
        For Each rw As DataRow In _dt.Rows
            WriteToLog(String.Format("{0} {1}", _dt.Columns(0).ColumnName.ToUpper.Trim, rw(0)), False, False)
        Next
        If _dt.DefaultView.Count > 0 Then WriteToLog("")
    End Sub

    Public Shared Function EncryptDecryptFile(ByVal strFile As String, ByVal IsEncryptFile As Boolean, Optional ByRef outputFile As String = "") As String
        Dim salt As String = "lbpumid2017_allcard"

        'If salt.Split("|")(0) = "0" Then
        Dim encryptdecryptdata As New EncryptDecryptData()
        Dim strProcess As String
        Dim origFile As String = strFile
        If IsEncryptFile Then
            'Ium(81*Qtr
            encryptdecryptdata.EncryptFile(strFile, salt) 'encryptdecryptdata.salt)
            outputFile = encryptdecryptdata.OutputFile
            strProcess = "Encryption "
        Else
            'encryptdecryptdata.DecryptFile(strFile, salt.Split("|")(1)) 'encryptdecryptdata.salt)
            encryptdecryptdata.DecryptFile(strFile, salt)
            outputFile = encryptdecryptdata.OutputFile
            strProcess = "Decryption "
        End If

        Dim errmsg As String = encryptdecryptdata.ErrorMessage

        If encryptdecryptdata.IsSuccess Then
            encryptdecryptdata = Nothing
            Return ""
        Else
            encryptdecryptdata = Nothing
            Return strProcess & " process failed. Returned error " & errmsg
        End If
        'Else
        'Return salt.Split("|")(1)
        'End If
    End Function

    Private Sub btnBrowsePlainFile_EncDec_Click(sender As System.Object, e As System.EventArgs) Handles btnBrowsePlainFile_EncDec.Click
        BrowseFile(txtPlainFile_EncDec)
    End Sub

    Private Sub btnBrowseEncryptedFile_EncDec_Click(sender As System.Object, e As System.EventArgs) Handles btnBrowseEncryptedFile_EncDec.Click
        BrowseFile(txtEncryptedFile_EncDec, "Encrypted Files (*.lbp)|*.lbp")
    End Sub

    Private Sub btnEncryptFile_EncDec_Click(sender As System.Object, e As System.EventArgs) Handles btnEncryptFile_EncDec.Click
        If txtPlainFile_EncDec.Text = "" Then
            MessageBox.Show("Please select file to encrypt", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        ElseIf Not File.Exists(txtPlainFile_EncDec.Text) Then
            MessageBox.Show("Please select valid file to encrypt", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        FormDispo(False)

        Dim outputFile As String = ""
        Dim response As String = EncryptDecryptFile(txtPlainFile_EncDec.Text, True, outputFile)

        If response = "" Then
            MessageBox.Show("File is successfully encrypted and saved at '" & outputFile & "'.", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Else
            MessageBox.Show("File encryption failed", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If

        FormDispo(True)
    End Sub

    Private Sub btnDecryptFile_EncDec_Click(sender As System.Object, e As System.EventArgs) Handles btnDecryptFile_EncDec.Click
        If txtEncryptedFile_EncDec.Text = "" Then
            MessageBox.Show("Please select file to decrypt", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        ElseIf Not File.Exists(txtEncryptedFile_EncDec.Text) Then
            MessageBox.Show("Please select valid file to decrypt", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        FormDispo(False)

        Dim outputFile As String = ""
        Dim response As String = EncryptDecryptFile(txtEncryptedFile_EncDec.Text, False, outputFile)

        If response = "" Then
            MessageBox.Show("File is successfully decrypted and saved at '" & outputFile & "'.", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Else
            MessageBox.Show("File decryption failed", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If

        FormDispo(True)
    End Sub

    Private Sub Label12_Click(sender As Object, e As EventArgs) Handles Label12.Click

    End Sub

    Private Sub cmdBrowseAddr_Click(sender As Object, e As EventArgs) Handles cmdBrowseAddr.Click
        BrowseFile(txtAddress)
        intAddrfileTotal = GetTextfileTotalRecords(txtAddress.Text)

        'cmdLoadAddr.BackColor = Color.Transparent
        'LoadFile(txtAddress)
        'LoadSheets(txtAddress.Text, cboSheetsAddr)
    End Sub

    Private Sub btnGenerateOutput_Click_1(sender As System.Object, e As System.EventArgs) Handles btnGenerateOutput.Click
        Invoke(New Action(AddressOf BindData))
        'If File.Exists(tempOutputFile) Then rtbOutput.LoadFile(tempOutputFile)
    End Sub

    Private Sub Button2_Click(sender As System.Object, e As System.EventArgs)
        Dim file As String = "D:\test embossing file_21.out"
        Dim sb As New System.Text.StringBuilder
        Using sr As New StreamReader(file, System.Text.Encoding.ASCII)
            While Not sr.EndOfStream
                Dim strLine As String = sr.ReadLine

                Dim strAcctNo As String = strLine.Substring(171 - 1, 10)
                Dim strCardNo = strLine.Substring(8 - 1, 16)
                Dim strGSISNo = strLine.Substring(122 - 1, 11)
                Dim strJAI = strLine.Substring(71 - 1, 1)
                Dim strCardExpiry = strLine.Substring(67 - 1, 4)
                Dim strRouteNo = ""
                Dim strSequenceNo = strLine.Substring(101 - 1, 2)
                Dim strPIN = strLine.Substring(85 - 1, 4)

                sb.AppendLine(strAcctNo & "|" & strCardNo & "|" & strGSISNo & "|" & strJAI & "|" & strCardExpiry & "|" & strRouteNo & "|" & strSequenceNo & "|" & strPIN)
            End While
        End Using

        IO.File.WriteAllText(Application.StartupPath & "\sampleOUTPUT.txt", sb.ToString)

        MessageBox.Show("Done!")
    End Sub

    Private Sub btnGenerateLicense_License_Click(sender As Object, e As EventArgs) Handles btnGenerateLicense_License.Click
        AllcardLicenseHandler.LicenseHandler.GenerateLicense()
    End Sub

    Private Sub btnActivateLicense_License_Click(sender As System.Object, e As System.EventArgs) Handles btnActivateLicense_License.Click
        'LicenseHandler.ActivateLicenseClient()
    End Sub

    Private Sub pbActivateLicense_DoubleClick(sender As System.Object, e As System.EventArgs) Handles pbActivateLicense.DoubleClick
        Dim pass As String = InputBox("Enter authorization code", "LICENSE ACTIVATION")
        If pass = "c@st0RTr0y815" Then
            Dim frm As New Form1
            frm.ShowDialog()
        Else
            Close()
        End If
    End Sub

    Private Sub BindLicenseInfo()
        '0=MacAddr, 1=ProcessorID, 2=MotherBoardID, 3=LicxDateCreation, 4=LicxDateActivated, 5=CountLimit, 6=Balance
        Dim arrLicx() As String = AllcardLicenseHandler.LicenseHandler.GetLicenseInfo(AllcardLicenseHandler.LicenseHandler.licenseFile)
        lblMACAddr_License.Text = arrLicx(0)
        lblActivationDate_License.Text = arrLicx(4)
        lblLicenseBalance_License.Text = CInt(arrLicx(6)).ToString("N0")
    End Sub

    Private Function CheckDBConnection() As Boolean
        Dim DAL As New DAL
        Try
            If DAL.IsConnectionOK Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            DAL.Dispose()
            DAL = Nothing
        End Try
    End Function

End Class