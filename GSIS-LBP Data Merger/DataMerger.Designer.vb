<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class DataMerger
    Inherits System.Windows.Forms.Form

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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(DataMerger))
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.tabFiles = New System.Windows.Forms.TabPage()
        Me.cboType = New System.Windows.Forms.ComboBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.lblStatusAddr = New System.Windows.Forms.Label()
        Me.cmdBrowseAddr = New System.Windows.Forms.Button()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.txtAddress = New System.Windows.Forms.TextBox()
        Me.lblJAI2Status = New System.Windows.Forms.Label()
        Me.lblBranchStatus = New System.Windows.Forms.Label()
        Me.lblEmbossingStatus = New System.Windows.Forms.Label()
        Me.lblMappingStatus = New System.Windows.Forms.Label()
        Me.lblReferenceStatus = New System.Windows.Forms.Label()
        Me.chkWithJAI2 = New System.Windows.Forms.CheckBox()
        Me.btnReset = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.cmdLoad2 = New System.Windows.Forms.Button()
        Me.cboSheets2 = New System.Windows.Forms.ComboBox()
        Me.cmdBrowseExcelFile2 = New System.Windows.Forms.Button()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.txtExcelFile2 = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cmdMerge = New System.Windows.Forms.Button()
        Me.lblRecord = New System.Windows.Forms.Label()
        Me.cmdLoad = New System.Windows.Forms.Button()
        Me.cboSheets = New System.Windows.Forms.ComboBox()
        Me.cmdBrowseExcelFile = New System.Windows.Forms.Button()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtExcelFile = New System.Windows.Forms.TextBox()
        Me.cmdBrowseEmbossingFile = New System.Windows.Forms.Button()
        Me.cmdBrowseMappingFile = New System.Windows.Forms.Button()
        Me.cmdBrowseReferenceFile = New System.Windows.Forms.Button()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtEmbossingFile = New System.Windows.Forms.TextBox()
        Me.txtMappingFile = New System.Windows.Forms.TextBox()
        Me.txtReferenceFile = New System.Windows.Forms.TextBox()
        Me.tabOutput = New System.Windows.Forms.TabPage()
        Me.btnGenerateOutput = New System.Windows.Forms.Button()
        Me.cmdSaveOutput = New System.Windows.Forms.Button()
        Me.rtbOutput = New System.Windows.Forms.RichTextBox()
        Me.tabLog = New System.Windows.Forms.TabPage()
        Me.cmdSaveLog = New System.Windows.Forms.Button()
        Me.rtbLog = New System.Windows.Forms.RichTextBox()
        Me.tabTables = New System.Windows.Forms.TabPage()
        Me.cboStorProc = New System.Windows.Forms.ComboBox()
        Me.lblRecord_tabTables = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.tabEncDec = New System.Windows.Forms.TabPage()
        Me.btnDecryptFile_EncDec = New System.Windows.Forms.Button()
        Me.btnBrowseEncryptedFile_EncDec = New System.Windows.Forms.Button()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.txtEncryptedFile_EncDec = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.txtPlainFile_EncDec = New System.Windows.Forms.TextBox()
        Me.btnEncryptFile_EncDec = New System.Windows.Forms.Button()
        Me.btnBrowsePlainFile_EncDec = New System.Windows.Forms.Button()
        Me.tabLicense = New System.Windows.Forms.TabPage()
        Me.pbActivateLicense = New System.Windows.Forms.PictureBox()
        Me.lblLicenseBalance_License = New System.Windows.Forms.Label()
        Me.lblActivationDate_License = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.btnActivateLicense_License = New System.Windows.Forms.Button()
        Me.lblMACAddr_License = New System.Windows.Forms.Label()
        Me.btnGenerateLicense_License = New System.Windows.Forms.Button()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.LinkLabel1 = New System.Windows.Forms.LinkLabel()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.lblDate = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.SaveFileDialog1 = New System.Windows.Forms.SaveFileDialog()
        Me.TabControl1.SuspendLayout()
        Me.tabFiles.SuspendLayout()
        Me.tabOutput.SuspendLayout()
        Me.tabLog.SuspendLayout()
        Me.tabTables.SuspendLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tabEncDec.SuspendLayout()
        Me.tabLicense.SuspendLayout()
        CType(Me.pbActivateLicense, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'TabControl1
        '
        Me.TabControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TabControl1.Controls.Add(Me.tabFiles)
        Me.TabControl1.Controls.Add(Me.tabOutput)
        Me.TabControl1.Controls.Add(Me.tabLog)
        Me.TabControl1.Controls.Add(Me.tabTables)
        Me.TabControl1.Controls.Add(Me.tabEncDec)
        Me.TabControl1.Controls.Add(Me.tabLicense)
        Me.TabControl1.Location = New System.Drawing.Point(0, 51)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(726, 481)
        Me.TabControl1.TabIndex = 0
        '
        'tabFiles
        '
        Me.tabFiles.Controls.Add(Me.cboType)
        Me.tabFiles.Controls.Add(Me.Label13)
        Me.tabFiles.Controls.Add(Me.Button2)
        Me.tabFiles.Controls.Add(Me.lblStatusAddr)
        Me.tabFiles.Controls.Add(Me.cmdBrowseAddr)
        Me.tabFiles.Controls.Add(Me.Label12)
        Me.tabFiles.Controls.Add(Me.txtAddress)
        Me.tabFiles.Controls.Add(Me.lblJAI2Status)
        Me.tabFiles.Controls.Add(Me.lblBranchStatus)
        Me.tabFiles.Controls.Add(Me.lblEmbossingStatus)
        Me.tabFiles.Controls.Add(Me.lblMappingStatus)
        Me.tabFiles.Controls.Add(Me.lblReferenceStatus)
        Me.tabFiles.Controls.Add(Me.chkWithJAI2)
        Me.tabFiles.Controls.Add(Me.btnReset)
        Me.tabFiles.Controls.Add(Me.Button1)
        Me.tabFiles.Controls.Add(Me.cmdLoad2)
        Me.tabFiles.Controls.Add(Me.cboSheets2)
        Me.tabFiles.Controls.Add(Me.cmdBrowseExcelFile2)
        Me.tabFiles.Controls.Add(Me.Label8)
        Me.tabFiles.Controls.Add(Me.txtExcelFile2)
        Me.tabFiles.Controls.Add(Me.Label2)
        Me.tabFiles.Controls.Add(Me.cmdMerge)
        Me.tabFiles.Controls.Add(Me.lblRecord)
        Me.tabFiles.Controls.Add(Me.cmdLoad)
        Me.tabFiles.Controls.Add(Me.cboSheets)
        Me.tabFiles.Controls.Add(Me.cmdBrowseExcelFile)
        Me.tabFiles.Controls.Add(Me.Label6)
        Me.tabFiles.Controls.Add(Me.txtExcelFile)
        Me.tabFiles.Controls.Add(Me.cmdBrowseEmbossingFile)
        Me.tabFiles.Controls.Add(Me.cmdBrowseMappingFile)
        Me.tabFiles.Controls.Add(Me.cmdBrowseReferenceFile)
        Me.tabFiles.Controls.Add(Me.Label5)
        Me.tabFiles.Controls.Add(Me.Label4)
        Me.tabFiles.Controls.Add(Me.Label1)
        Me.tabFiles.Controls.Add(Me.txtEmbossingFile)
        Me.tabFiles.Controls.Add(Me.txtMappingFile)
        Me.tabFiles.Controls.Add(Me.txtReferenceFile)
        Me.tabFiles.Location = New System.Drawing.Point(4, 22)
        Me.tabFiles.Name = "tabFiles"
        Me.tabFiles.Padding = New System.Windows.Forms.Padding(3)
        Me.tabFiles.Size = New System.Drawing.Size(718, 455)
        Me.tabFiles.TabIndex = 0
        Me.tabFiles.Text = "Files"
        Me.tabFiles.UseVisualStyleBackColor = True
        '
        'cboType
        '
        Me.cboType.FormattingEnabled = True
        Me.cboType.Items.AddRange(New Object() {"NEW/ RECARD", "REPLACEMENT"})
        Me.cboType.Location = New System.Drawing.Point(96, 23)
        Me.cboType.Name = "cboType"
        Me.cboType.Size = New System.Drawing.Size(130, 21)
        Me.cboType.TabIndex = 72
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(14, 26)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(31, 13)
        Me.Label13.TabIndex = 71
        Me.Label13.Text = "Type"
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(302, 371)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(98, 34)
        Me.Button2.TabIndex = 70
        Me.Button2.Text = "Merge Data"
        Me.Button2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Button2.UseVisualStyleBackColor = True
        Me.Button2.Visible = False
        '
        'lblStatusAddr
        '
        Me.lblStatusAddr.AutoSize = True
        Me.lblStatusAddr.Location = New System.Drawing.Point(638, 118)
        Me.lblStatusAddr.Name = "lblStatusAddr"
        Me.lblStatusAddr.Size = New System.Drawing.Size(71, 13)
        Me.lblStatusAddr.TabIndex = 69
        Me.lblStatusAddr.Text = "10000/10000"
        '
        'cmdBrowseAddr
        '
        Me.cmdBrowseAddr.Image = Global.GSIS_LBP_Data_Merger.My.Resources.Resources.browse_explore_icon_png_16x16
        Me.cmdBrowseAddr.Location = New System.Drawing.Point(606, 114)
        Me.cmdBrowseAddr.Name = "cmdBrowseAddr"
        Me.cmdBrowseAddr.Size = New System.Drawing.Size(30, 23)
        Me.cmdBrowseAddr.TabIndex = 66
        Me.cmdBrowseAddr.UseVisualStyleBackColor = True
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(14, 119)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(65, 13)
        Me.Label12.TabIndex = 65
        Me.Label12.Text = "Address File"
        '
        'txtAddress
        '
        Me.txtAddress.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAddress.Location = New System.Drawing.Point(96, 115)
        Me.txtAddress.Name = "txtAddress"
        Me.txtAddress.Size = New System.Drawing.Size(510, 21)
        Me.txtAddress.TabIndex = 64
        '
        'lblJAI2Status
        '
        Me.lblJAI2Status.AutoSize = True
        Me.lblJAI2Status.Location = New System.Drawing.Point(638, 181)
        Me.lblJAI2Status.Name = "lblJAI2Status"
        Me.lblJAI2Status.Size = New System.Drawing.Size(71, 13)
        Me.lblJAI2Status.TabIndex = 63
        Me.lblJAI2Status.Text = "10000/10000"
        '
        'lblBranchStatus
        '
        Me.lblBranchStatus.AutoSize = True
        Me.lblBranchStatus.Location = New System.Drawing.Point(638, 332)
        Me.lblBranchStatus.Name = "lblBranchStatus"
        Me.lblBranchStatus.Size = New System.Drawing.Size(71, 13)
        Me.lblBranchStatus.TabIndex = 62
        Me.lblBranchStatus.Text = "10000/10000"
        Me.lblBranchStatus.Visible = False
        '
        'lblEmbossingStatus
        '
        Me.lblEmbossingStatus.AutoSize = True
        Me.lblEmbossingStatus.Location = New System.Drawing.Point(638, 58)
        Me.lblEmbossingStatus.Name = "lblEmbossingStatus"
        Me.lblEmbossingStatus.Size = New System.Drawing.Size(71, 13)
        Me.lblEmbossingStatus.TabIndex = 61
        Me.lblEmbossingStatus.Text = "10000/10000"
        '
        'lblMappingStatus
        '
        Me.lblMappingStatus.AutoSize = True
        Me.lblMappingStatus.Location = New System.Drawing.Point(638, 89)
        Me.lblMappingStatus.Name = "lblMappingStatus"
        Me.lblMappingStatus.Size = New System.Drawing.Size(71, 13)
        Me.lblMappingStatus.TabIndex = 60
        Me.lblMappingStatus.Text = "10000/10000"
        '
        'lblReferenceStatus
        '
        Me.lblReferenceStatus.AutoSize = True
        Me.lblReferenceStatus.Location = New System.Drawing.Point(638, 302)
        Me.lblReferenceStatus.Name = "lblReferenceStatus"
        Me.lblReferenceStatus.Size = New System.Drawing.Size(71, 13)
        Me.lblReferenceStatus.TabIndex = 59
        Me.lblReferenceStatus.Text = "10000/10000"
        Me.lblReferenceStatus.Visible = False
        '
        'chkWithJAI2
        '
        Me.chkWithJAI2.AutoSize = True
        Me.chkWithJAI2.Location = New System.Drawing.Point(17, 152)
        Me.chkWithJAI2.Name = "chkWithJAI2"
        Me.chkWithJAI2.Size = New System.Drawing.Size(73, 17)
        Me.chkWithJAI2.TabIndex = 58
        Me.chkWithJAI2.Text = "With JAI2"
        Me.chkWithJAI2.UseVisualStyleBackColor = True
        '
        'btnReset
        '
        Me.btnReset.Image = CType(resources.GetObject("btnReset.Image"), System.Drawing.Image)
        Me.btnReset.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnReset.Location = New System.Drawing.Point(121, 225)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(91, 34)
        Me.btnReset.TabIndex = 57
        Me.btnReset.Text = "Reset"
        Me.btnReset.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnReset.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(11, 371)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(98, 34)
        Me.Button1.TabIndex = 56
        Me.Button1.Text = "Merge Data"
        Me.Button1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Button1.UseVisualStyleBackColor = True
        Me.Button1.Visible = False
        '
        'cmdLoad2
        '
        Me.cmdLoad2.Enabled = False
        Me.cmdLoad2.Location = New System.Drawing.Point(594, 176)
        Me.cmdLoad2.Name = "cmdLoad2"
        Me.cmdLoad2.Size = New System.Drawing.Size(42, 23)
        Me.cmdLoad2.TabIndex = 54
        Me.cmdLoad2.Text = "LOAD"
        Me.cmdLoad2.UseVisualStyleBackColor = True
        '
        'cboSheets2
        '
        Me.cboSheets2.Enabled = False
        Me.cboSheets2.FormattingEnabled = True
        Me.cboSheets2.Location = New System.Drawing.Point(462, 177)
        Me.cboSheets2.Name = "cboSheets2"
        Me.cboSheets2.Size = New System.Drawing.Size(130, 21)
        Me.cboSheets2.TabIndex = 53
        '
        'cmdBrowseExcelFile2
        '
        Me.cmdBrowseExcelFile2.Enabled = False
        Me.cmdBrowseExcelFile2.Image = Global.GSIS_LBP_Data_Merger.My.Resources.Resources.browse_explore_icon_png_16x16
        Me.cmdBrowseExcelFile2.Location = New System.Drawing.Point(428, 177)
        Me.cmdBrowseExcelFile2.Name = "cmdBrowseExcelFile2"
        Me.cmdBrowseExcelFile2.Size = New System.Drawing.Size(30, 23)
        Me.cmdBrowseExcelFile2.TabIndex = 52
        Me.cmdBrowseExcelFile2.UseVisualStyleBackColor = True
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(14, 181)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(61, 13)
        Me.Label8.TabIndex = 51
        Me.Label8.Text = "JAI2_UMID"
        '
        'txtExcelFile2
        '
        Me.txtExcelFile2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtExcelFile2.Enabled = False
        Me.txtExcelFile2.Location = New System.Drawing.Point(96, 178)
        Me.txtExcelFile2.Name = "txtExcelFile2"
        Me.txtExcelFile2.Size = New System.Drawing.Size(330, 21)
        Me.txtExcelFile2.TabIndex = 50
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.ForeColor = System.Drawing.SystemColors.ButtonShadow
        Me.Label2.Location = New System.Drawing.Point(8, 437)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(229, 13)
        Me.Label2.TabIndex = 48
        Me.Label2.Text = "Powered By: Allcard Technologies, Corp. 2013"
        '
        'cmdMerge
        '
        Me.cmdMerge.Image = CType(resources.GetObject("cmdMerge.Image"), System.Drawing.Image)
        Me.cmdMerge.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdMerge.Location = New System.Drawing.Point(17, 225)
        Me.cmdMerge.Name = "cmdMerge"
        Me.cmdMerge.Size = New System.Drawing.Size(98, 34)
        Me.cmdMerge.TabIndex = 47
        Me.cmdMerge.Text = "Merge Data"
        Me.cmdMerge.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.cmdMerge.UseVisualStyleBackColor = True
        '
        'lblRecord
        '
        Me.lblRecord.AutoSize = True
        Me.lblRecord.Location = New System.Drawing.Point(14, 265)
        Me.lblRecord.Name = "lblRecord"
        Me.lblRecord.Size = New System.Drawing.Size(38, 13)
        Me.lblRecord.TabIndex = 46
        Me.lblRecord.Text = "Ready"
        '
        'cmdLoad
        '
        Me.cmdLoad.Location = New System.Drawing.Point(594, 328)
        Me.cmdLoad.Name = "cmdLoad"
        Me.cmdLoad.Size = New System.Drawing.Size(42, 23)
        Me.cmdLoad.TabIndex = 42
        Me.cmdLoad.Text = "LOAD"
        Me.cmdLoad.UseVisualStyleBackColor = True
        Me.cmdLoad.Visible = False
        '
        'cboSheets
        '
        Me.cboSheets.FormattingEnabled = True
        Me.cboSheets.Location = New System.Drawing.Point(462, 329)
        Me.cboSheets.Name = "cboSheets"
        Me.cboSheets.Size = New System.Drawing.Size(130, 21)
        Me.cboSheets.TabIndex = 41
        Me.cboSheets.Visible = False
        '
        'cmdBrowseExcelFile
        '
        Me.cmdBrowseExcelFile.Image = Global.GSIS_LBP_Data_Merger.My.Resources.Resources.browse_explore_icon_png_16x16
        Me.cmdBrowseExcelFile.Location = New System.Drawing.Point(428, 329)
        Me.cmdBrowseExcelFile.Name = "cmdBrowseExcelFile"
        Me.cmdBrowseExcelFile.Size = New System.Drawing.Size(30, 23)
        Me.cmdBrowseExcelFile.TabIndex = 40
        Me.cmdBrowseExcelFile.UseVisualStyleBackColor = True
        Me.cmdBrowseExcelFile.Visible = False
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(14, 333)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(68, 13)
        Me.Label6.TabIndex = 39
        Me.Label6.Text = "Branch Code"
        Me.Label6.Visible = False
        '
        'txtExcelFile
        '
        Me.txtExcelFile.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtExcelFile.Location = New System.Drawing.Point(96, 330)
        Me.txtExcelFile.Name = "txtExcelFile"
        Me.txtExcelFile.Size = New System.Drawing.Size(330, 21)
        Me.txtExcelFile.TabIndex = 38
        Me.txtExcelFile.Visible = False
        '
        'cmdBrowseEmbossingFile
        '
        Me.cmdBrowseEmbossingFile.Image = Global.GSIS_LBP_Data_Merger.My.Resources.Resources.browse_explore_icon_png_16x16
        Me.cmdBrowseEmbossingFile.Location = New System.Drawing.Point(606, 54)
        Me.cmdBrowseEmbossingFile.Name = "cmdBrowseEmbossingFile"
        Me.cmdBrowseEmbossingFile.Size = New System.Drawing.Size(30, 23)
        Me.cmdBrowseEmbossingFile.TabIndex = 37
        Me.cmdBrowseEmbossingFile.UseVisualStyleBackColor = True
        '
        'cmdBrowseMappingFile
        '
        Me.cmdBrowseMappingFile.Image = Global.GSIS_LBP_Data_Merger.My.Resources.Resources.browse_explore_icon_png_16x16
        Me.cmdBrowseMappingFile.Location = New System.Drawing.Point(606, 84)
        Me.cmdBrowseMappingFile.Name = "cmdBrowseMappingFile"
        Me.cmdBrowseMappingFile.Size = New System.Drawing.Size(30, 23)
        Me.cmdBrowseMappingFile.TabIndex = 36
        Me.cmdBrowseMappingFile.UseVisualStyleBackColor = True
        '
        'cmdBrowseReferenceFile
        '
        Me.cmdBrowseReferenceFile.Image = Global.GSIS_LBP_Data_Merger.My.Resources.Resources.browse_explore_icon_png_16x16
        Me.cmdBrowseReferenceFile.Location = New System.Drawing.Point(606, 299)
        Me.cmdBrowseReferenceFile.Name = "cmdBrowseReferenceFile"
        Me.cmdBrowseReferenceFile.Size = New System.Drawing.Size(30, 23)
        Me.cmdBrowseReferenceFile.TabIndex = 35
        Me.cmdBrowseReferenceFile.UseVisualStyleBackColor = True
        Me.cmdBrowseReferenceFile.Visible = False
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(14, 59)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(76, 13)
        Me.Label5.TabIndex = 34
        Me.Label5.Text = "Embossing File"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(14, 87)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(64, 13)
        Me.Label4.TabIndex = 33
        Me.Label4.Text = "Mapping file"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(14, 303)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(76, 13)
        Me.Label1.TabIndex = 32
        Me.Label1.Text = "Reference File"
        Me.Label1.Visible = False
        '
        'txtEmbossingFile
        '
        Me.txtEmbossingFile.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtEmbossingFile.Location = New System.Drawing.Point(96, 55)
        Me.txtEmbossingFile.Name = "txtEmbossingFile"
        Me.txtEmbossingFile.Size = New System.Drawing.Size(510, 21)
        Me.txtEmbossingFile.TabIndex = 31
        '
        'txtMappingFile
        '
        Me.txtMappingFile.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtMappingFile.Location = New System.Drawing.Point(96, 85)
        Me.txtMappingFile.Name = "txtMappingFile"
        Me.txtMappingFile.Size = New System.Drawing.Size(510, 21)
        Me.txtMappingFile.TabIndex = 30
        '
        'txtReferenceFile
        '
        Me.txtReferenceFile.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtReferenceFile.Location = New System.Drawing.Point(96, 300)
        Me.txtReferenceFile.Name = "txtReferenceFile"
        Me.txtReferenceFile.Size = New System.Drawing.Size(510, 21)
        Me.txtReferenceFile.TabIndex = 29
        Me.txtReferenceFile.Visible = False
        '
        'tabOutput
        '
        Me.tabOutput.Controls.Add(Me.btnGenerateOutput)
        Me.tabOutput.Controls.Add(Me.cmdSaveOutput)
        Me.tabOutput.Controls.Add(Me.rtbOutput)
        Me.tabOutput.Location = New System.Drawing.Point(4, 22)
        Me.tabOutput.Name = "tabOutput"
        Me.tabOutput.Padding = New System.Windows.Forms.Padding(3)
        Me.tabOutput.Size = New System.Drawing.Size(718, 455)
        Me.tabOutput.TabIndex = 1
        Me.tabOutput.Text = "Output"
        Me.tabOutput.UseVisualStyleBackColor = True
        '
        'btnGenerateOutput
        '
        Me.btnGenerateOutput.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnGenerateOutput.Location = New System.Drawing.Point(11, 8)
        Me.btnGenerateOutput.Name = "btnGenerateOutput"
        Me.btnGenerateOutput.Size = New System.Drawing.Size(98, 25)
        Me.btnGenerateOutput.TabIndex = 71
        Me.btnGenerateOutput.Text = "Generate Output"
        Me.btnGenerateOutput.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnGenerateOutput.UseVisualStyleBackColor = True
        '
        'cmdSaveOutput
        '
        Me.cmdSaveOutput.Location = New System.Drawing.Point(114, 8)
        Me.cmdSaveOutput.Name = "cmdSaveOutput"
        Me.cmdSaveOutput.Size = New System.Drawing.Size(75, 25)
        Me.cmdSaveOutput.TabIndex = 30
        Me.cmdSaveOutput.Text = "Save File"
        Me.cmdSaveOutput.UseVisualStyleBackColor = True
        '
        'rtbOutput
        '
        Me.rtbOutput.BackColor = System.Drawing.Color.White
        Me.rtbOutput.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.rtbOutput.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.rtbOutput.Location = New System.Drawing.Point(3, 40)
        Me.rtbOutput.Name = "rtbOutput"
        Me.rtbOutput.ReadOnly = True
        Me.rtbOutput.Size = New System.Drawing.Size(712, 412)
        Me.rtbOutput.TabIndex = 29
        Me.rtbOutput.Text = ""
        Me.rtbOutput.WordWrap = False
        '
        'tabLog
        '
        Me.tabLog.Controls.Add(Me.cmdSaveLog)
        Me.tabLog.Controls.Add(Me.rtbLog)
        Me.tabLog.Location = New System.Drawing.Point(4, 22)
        Me.tabLog.Name = "tabLog"
        Me.tabLog.Size = New System.Drawing.Size(718, 455)
        Me.tabLog.TabIndex = 2
        Me.tabLog.Text = "Log"
        Me.tabLog.UseVisualStyleBackColor = True
        '
        'cmdSaveLog
        '
        Me.cmdSaveLog.Location = New System.Drawing.Point(8, 8)
        Me.cmdSaveLog.Name = "cmdSaveLog"
        Me.cmdSaveLog.Size = New System.Drawing.Size(75, 23)
        Me.cmdSaveLog.TabIndex = 31
        Me.cmdSaveLog.Text = "Save Log"
        Me.cmdSaveLog.UseVisualStyleBackColor = True
        '
        'rtbLog
        '
        Me.rtbLog.BackColor = System.Drawing.Color.White
        Me.rtbLog.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.rtbLog.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.rtbLog.Location = New System.Drawing.Point(0, 43)
        Me.rtbLog.Name = "rtbLog"
        Me.rtbLog.ReadOnly = True
        Me.rtbLog.Size = New System.Drawing.Size(718, 412)
        Me.rtbLog.TabIndex = 30
        Me.rtbLog.Text = ""
        Me.rtbLog.WordWrap = False
        '
        'tabTables
        '
        Me.tabTables.Controls.Add(Me.cboStorProc)
        Me.tabTables.Controls.Add(Me.lblRecord_tabTables)
        Me.tabTables.Controls.Add(Me.Label3)
        Me.tabTables.Controls.Add(Me.DataGridView1)
        Me.tabTables.Location = New System.Drawing.Point(4, 22)
        Me.tabTables.Name = "tabTables"
        Me.tabTables.Size = New System.Drawing.Size(718, 455)
        Me.tabTables.TabIndex = 3
        Me.tabTables.Text = "Tables"
        Me.tabTables.UseVisualStyleBackColor = True
        '
        'cboStorProc
        '
        Me.cboStorProc.FormattingEnabled = True
        Me.cboStorProc.Location = New System.Drawing.Point(77, 23)
        Me.cboStorProc.Name = "cboStorProc"
        Me.cboStorProc.Size = New System.Drawing.Size(334, 21)
        Me.cboStorProc.TabIndex = 47
        '
        'lblRecord_tabTables
        '
        Me.lblRecord_tabTables.AutoSize = True
        Me.lblRecord_tabTables.Location = New System.Drawing.Point(419, 27)
        Me.lblRecord_tabTables.Name = "lblRecord_tabTables"
        Me.lblRecord_tabTables.Size = New System.Drawing.Size(0, 13)
        Me.lblRecord_tabTables.TabIndex = 46
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(14, 26)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(33, 13)
        Me.Label3.TabIndex = 33
        Me.Label3.Text = "Table"
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToDeleteRows = False
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.DataGridView1.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle2
        Me.DataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.DataGridView1.Location = New System.Drawing.Point(0, 67)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.ReadOnly = True
        Me.DataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DataGridView1.Size = New System.Drawing.Size(718, 388)
        Me.DataGridView1.TabIndex = 0
        '
        'tabEncDec
        '
        Me.tabEncDec.Controls.Add(Me.btnDecryptFile_EncDec)
        Me.tabEncDec.Controls.Add(Me.btnBrowseEncryptedFile_EncDec)
        Me.tabEncDec.Controls.Add(Me.Label10)
        Me.tabEncDec.Controls.Add(Me.txtEncryptedFile_EncDec)
        Me.tabEncDec.Controls.Add(Me.Label9)
        Me.tabEncDec.Controls.Add(Me.txtPlainFile_EncDec)
        Me.tabEncDec.Controls.Add(Me.btnEncryptFile_EncDec)
        Me.tabEncDec.Controls.Add(Me.btnBrowsePlainFile_EncDec)
        Me.tabEncDec.Location = New System.Drawing.Point(4, 22)
        Me.tabEncDec.Name = "tabEncDec"
        Me.tabEncDec.Size = New System.Drawing.Size(718, 455)
        Me.tabEncDec.TabIndex = 4
        Me.tabEncDec.Text = "Encryption/ Decryption"
        Me.tabEncDec.UseVisualStyleBackColor = True
        '
        'btnDecryptFile_EncDec
        '
        Me.btnDecryptFile_EncDec.BackColor = System.Drawing.Color.White
        Me.btnDecryptFile_EncDec.Image = CType(resources.GetObject("btnDecryptFile_EncDec.Image"), System.Drawing.Image)
        Me.btnDecryptFile_EncDec.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnDecryptFile_EncDec.Location = New System.Drawing.Point(29, 163)
        Me.btnDecryptFile_EncDec.Name = "btnDecryptFile_EncDec"
        Me.btnDecryptFile_EncDec.Size = New System.Drawing.Size(98, 34)
        Me.btnDecryptFile_EncDec.TabIndex = 52
        Me.btnDecryptFile_EncDec.Text = "Decrypt File"
        Me.btnDecryptFile_EncDec.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnDecryptFile_EncDec.UseVisualStyleBackColor = False
        '
        'btnBrowseEncryptedFile_EncDec
        '
        Me.btnBrowseEncryptedFile_EncDec.Image = Global.GSIS_LBP_Data_Merger.My.Resources.Resources.browse_explore_icon_png_16x16
        Me.btnBrowseEncryptedFile_EncDec.Location = New System.Drawing.Point(618, 135)
        Me.btnBrowseEncryptedFile_EncDec.Name = "btnBrowseEncryptedFile_EncDec"
        Me.btnBrowseEncryptedFile_EncDec.Size = New System.Drawing.Size(30, 23)
        Me.btnBrowseEncryptedFile_EncDec.TabIndex = 51
        Me.btnBrowseEncryptedFile_EncDec.UseVisualStyleBackColor = True
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(26, 140)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(23, 13)
        Me.Label10.TabIndex = 50
        Me.Label10.Text = "File"
        '
        'txtEncryptedFile_EncDec
        '
        Me.txtEncryptedFile_EncDec.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtEncryptedFile_EncDec.Location = New System.Drawing.Point(55, 136)
        Me.txtEncryptedFile_EncDec.Name = "txtEncryptedFile_EncDec"
        Me.txtEncryptedFile_EncDec.Size = New System.Drawing.Size(563, 21)
        Me.txtEncryptedFile_EncDec.TabIndex = 49
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(26, 45)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(23, 13)
        Me.Label9.TabIndex = 37
        Me.Label9.Text = "File"
        '
        'txtPlainFile_EncDec
        '
        Me.txtPlainFile_EncDec.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPlainFile_EncDec.Location = New System.Drawing.Point(55, 41)
        Me.txtPlainFile_EncDec.Name = "txtPlainFile_EncDec"
        Me.txtPlainFile_EncDec.Size = New System.Drawing.Size(563, 21)
        Me.txtPlainFile_EncDec.TabIndex = 36
        '
        'btnEncryptFile_EncDec
        '
        Me.btnEncryptFile_EncDec.BackColor = System.Drawing.Color.White
        Me.btnEncryptFile_EncDec.Image = CType(resources.GetObject("btnEncryptFile_EncDec.Image"), System.Drawing.Image)
        Me.btnEncryptFile_EncDec.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnEncryptFile_EncDec.Location = New System.Drawing.Point(29, 68)
        Me.btnEncryptFile_EncDec.Name = "btnEncryptFile_EncDec"
        Me.btnEncryptFile_EncDec.Size = New System.Drawing.Size(98, 34)
        Me.btnEncryptFile_EncDec.TabIndex = 48
        Me.btnEncryptFile_EncDec.Text = "Encrypt File"
        Me.btnEncryptFile_EncDec.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnEncryptFile_EncDec.UseVisualStyleBackColor = False
        '
        'btnBrowsePlainFile_EncDec
        '
        Me.btnBrowsePlainFile_EncDec.Image = Global.GSIS_LBP_Data_Merger.My.Resources.Resources.browse_explore_icon_png_16x16
        Me.btnBrowsePlainFile_EncDec.Location = New System.Drawing.Point(618, 40)
        Me.btnBrowsePlainFile_EncDec.Name = "btnBrowsePlainFile_EncDec"
        Me.btnBrowsePlainFile_EncDec.Size = New System.Drawing.Size(30, 23)
        Me.btnBrowsePlainFile_EncDec.TabIndex = 38
        Me.btnBrowsePlainFile_EncDec.UseVisualStyleBackColor = True
        '
        'tabLicense
        '
        Me.tabLicense.Controls.Add(Me.pbActivateLicense)
        Me.tabLicense.Controls.Add(Me.lblLicenseBalance_License)
        Me.tabLicense.Controls.Add(Me.lblActivationDate_License)
        Me.tabLicense.Controls.Add(Me.Label15)
        Me.tabLicense.Controls.Add(Me.Label14)
        Me.tabLicense.Controls.Add(Me.btnActivateLicense_License)
        Me.tabLicense.Controls.Add(Me.lblMACAddr_License)
        Me.tabLicense.Controls.Add(Me.btnGenerateLicense_License)
        Me.tabLicense.Controls.Add(Me.Label11)
        Me.tabLicense.Location = New System.Drawing.Point(4, 22)
        Me.tabLicense.Name = "tabLicense"
        Me.tabLicense.Size = New System.Drawing.Size(718, 455)
        Me.tabLicense.TabIndex = 5
        Me.tabLicense.Text = "License"
        Me.tabLicense.UseVisualStyleBackColor = True
        '
        'pbActivateLicense
        '
        Me.pbActivateLicense.Location = New System.Drawing.Point(675, 416)
        Me.pbActivateLicense.Name = "pbActivateLicense"
        Me.pbActivateLicense.Size = New System.Drawing.Size(40, 36)
        Me.pbActivateLicense.TabIndex = 56
        Me.pbActivateLicense.TabStop = False
        '
        'lblLicenseBalance_License
        '
        Me.lblLicenseBalance_License.AutoSize = True
        Me.lblLicenseBalance_License.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLicenseBalance_License.Location = New System.Drawing.Point(155, 93)
        Me.lblLicenseBalance_License.Name = "lblLicenseBalance_License"
        Me.lblLicenseBalance_License.Size = New System.Drawing.Size(113, 13)
        Me.lblLicenseBalance_License.TabIndex = 54
        Me.lblLicenseBalance_License.Text = "[LICENSE BALANCE]"
        '
        'lblActivationDate_License
        '
        Me.lblActivationDate_License.AutoSize = True
        Me.lblActivationDate_License.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblActivationDate_License.Location = New System.Drawing.Point(155, 65)
        Me.lblActivationDate_License.Name = "lblActivationDate_License"
        Me.lblActivationDate_License.Size = New System.Drawing.Size(118, 13)
        Me.lblActivationDate_License.TabIndex = 53
        Me.lblActivationDate_License.Text = "[ACTIVATION DATE]"
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(35, 65)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(85, 13)
        Me.Label15.TabIndex = 52
        Me.Label15.Text = "Activation Date:"
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(35, 37)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(65, 13)
        Me.Label14.TabIndex = 51
        Me.Label14.Text = "Licensed to:"
        '
        'btnActivateLicense_License
        '
        Me.btnActivateLicense_License.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnActivateLicense_License.Location = New System.Drawing.Point(133, 217)
        Me.btnActivateLicense_License.Name = "btnActivateLicense_License"
        Me.btnActivateLicense_License.Size = New System.Drawing.Size(98, 34)
        Me.btnActivateLicense_License.TabIndex = 50
        Me.btnActivateLicense_License.Text = "Activate License"
        Me.btnActivateLicense_License.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnActivateLicense_License.UseVisualStyleBackColor = True
        Me.btnActivateLicense_License.Visible = False
        '
        'lblMACAddr_License
        '
        Me.lblMACAddr_License.AutoSize = True
        Me.lblMACAddr_License.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMACAddr_License.Location = New System.Drawing.Point(155, 37)
        Me.lblMACAddr_License.Name = "lblMACAddr_License"
        Me.lblMACAddr_License.Size = New System.Drawing.Size(87, 13)
        Me.lblMACAddr_License.TabIndex = 49
        Me.lblMACAddr_License.Text = "[LICENSED TO]"
        '
        'btnGenerateLicense_License
        '
        Me.btnGenerateLicense_License.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnGenerateLicense_License.Location = New System.Drawing.Point(29, 217)
        Me.btnGenerateLicense_License.Name = "btnGenerateLicense_License"
        Me.btnGenerateLicense_License.Size = New System.Drawing.Size(98, 34)
        Me.btnGenerateLicense_License.TabIndex = 48
        Me.btnGenerateLicense_License.Text = "Generate License"
        Me.btnGenerateLicense_License.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnGenerateLicense_License.UseVisualStyleBackColor = True
        Me.btnGenerateLicense_License.Visible = False
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(34, 93)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(86, 13)
        Me.Label11.TabIndex = 35
        Me.Label11.Text = "License Balance:"
        '
        'LinkLabel1
        '
        Me.LinkLabel1.AutoSize = True
        Me.LinkLabel1.Location = New System.Drawing.Point(665, 2)
        Me.LinkLabel1.Name = "LinkLabel1"
        Me.LinkLabel1.Size = New System.Drawing.Size(55, 13)
        Me.LinkLabel1.TabIndex = 49
        Me.LinkLabel1.TabStop = True
        Me.LinkLabel1.Text = "SETTINGS"
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.lblDate)
        Me.Panel1.Controls.Add(Me.Label7)
        Me.Panel1.Controls.Add(Me.LinkLabel1)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(726, 45)
        Me.Panel1.TabIndex = 1
        '
        'lblDate
        '
        Me.lblDate.AutoSize = True
        Me.lblDate.Location = New System.Drawing.Point(558, 22)
        Me.lblDate.Name = "lblDate"
        Me.lblDate.Size = New System.Drawing.Size(37, 13)
        Me.lblDate.TabIndex = 48
        Me.lblDate.Text = "Today"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Arial", 15.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.Color.Green
        Me.Label7.Location = New System.Drawing.Point(10, 10)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(267, 24)
        Me.Label7.TabIndex = 48
        Me.Label7.Text = "LANDBANK DATA MERGER"
        '
        'DataMerger
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(726, 532)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.TabControl1)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "DataMerger"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "LBP DATA MERGER v1.8"
        Me.TabControl1.ResumeLayout(False)
        Me.tabFiles.ResumeLayout(False)
        Me.tabFiles.PerformLayout()
        Me.tabOutput.ResumeLayout(False)
        Me.tabLog.ResumeLayout(False)
        Me.tabTables.ResumeLayout(False)
        Me.tabTables.PerformLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tabEncDec.ResumeLayout(False)
        Me.tabEncDec.PerformLayout()
        Me.tabLicense.ResumeLayout(False)
        Me.tabLicense.PerformLayout()
        CType(Me.pbActivateLicense, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents tabFiles As System.Windows.Forms.TabPage
    Friend WithEvents tabOutput As System.Windows.Forms.TabPage
    Friend WithEvents tabLog As System.Windows.Forms.TabPage
    Friend WithEvents cmdLoad As System.Windows.Forms.Button
    Friend WithEvents cboSheets As System.Windows.Forms.ComboBox
    Friend WithEvents cmdBrowseExcelFile As System.Windows.Forms.Button
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtExcelFile As System.Windows.Forms.TextBox
    Friend WithEvents cmdBrowseEmbossingFile As System.Windows.Forms.Button
    Friend WithEvents cmdBrowseMappingFile As System.Windows.Forms.Button
    Friend WithEvents cmdBrowseReferenceFile As System.Windows.Forms.Button
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtEmbossingFile As System.Windows.Forms.TextBox
    Friend WithEvents txtMappingFile As System.Windows.Forms.TextBox
    Friend WithEvents txtReferenceFile As System.Windows.Forms.TextBox
    Friend WithEvents rtbOutput As System.Windows.Forms.RichTextBox
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents lblRecord As System.Windows.Forms.Label
    Friend WithEvents cmdMerge As System.Windows.Forms.Button
    Friend WithEvents rtbLog As System.Windows.Forms.RichTextBox
    Friend WithEvents tabTables As System.Windows.Forms.TabPage
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents lblRecord_tabTables As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents lblDate As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents LinkLabel1 As System.Windows.Forms.LinkLabel
    Friend WithEvents cmdSaveOutput As System.Windows.Forms.Button
    Friend WithEvents cmdSaveLog As System.Windows.Forms.Button
    Friend WithEvents cmdLoad2 As System.Windows.Forms.Button
    Friend WithEvents cboSheets2 As System.Windows.Forms.ComboBox
    Friend WithEvents cmdBrowseExcelFile2 As System.Windows.Forms.Button
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txtExcelFile2 As System.Windows.Forms.TextBox
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents btnReset As System.Windows.Forms.Button
    Friend WithEvents chkWithJAI2 As System.Windows.Forms.CheckBox
    Friend WithEvents lblReferenceStatus As System.Windows.Forms.Label
    Friend WithEvents lblJAI2Status As System.Windows.Forms.Label
    Friend WithEvents lblBranchStatus As System.Windows.Forms.Label
    Friend WithEvents lblEmbossingStatus As System.Windows.Forms.Label
    Friend WithEvents lblMappingStatus As System.Windows.Forms.Label
    Friend WithEvents tabEncDec As System.Windows.Forms.TabPage
    Friend WithEvents btnDecryptFile_EncDec As System.Windows.Forms.Button
    Friend WithEvents btnBrowseEncryptedFile_EncDec As System.Windows.Forms.Button
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents txtEncryptedFile_EncDec As System.Windows.Forms.TextBox
    Friend WithEvents btnEncryptFile_EncDec As System.Windows.Forms.Button
    Friend WithEvents btnBrowsePlainFile_EncDec As System.Windows.Forms.Button
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents txtPlainFile_EncDec As System.Windows.Forms.TextBox
    Friend WithEvents lblStatusAddr As Label
    Friend WithEvents cmdBrowseAddr As Button
    Friend WithEvents Label12 As Label
    Friend WithEvents txtAddress As TextBox
    Friend WithEvents btnGenerateOutput As System.Windows.Forms.Button
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents tabLicense As TabPage
    Friend WithEvents Label11 As Label
    Friend WithEvents lblMACAddr_License As Label
    Friend WithEvents btnGenerateLicense_License As Button
    Friend WithEvents btnActivateLicense_License As Button
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents lblLicenseBalance_License As System.Windows.Forms.Label
    Friend WithEvents lblActivationDate_License As System.Windows.Forms.Label
    Friend WithEvents pbActivateLicense As System.Windows.Forms.PictureBox
    Friend WithEvents cboStorProc As ComboBox
    Friend WithEvents SaveFileDialog1 As SaveFileDialog
    Friend WithEvents cboType As ComboBox
    Friend WithEvents Label13 As Label
End Class
