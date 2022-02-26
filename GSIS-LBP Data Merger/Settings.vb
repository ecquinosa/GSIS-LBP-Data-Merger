Public Class Settings

    Public IsSaveButtonClicked As Boolean = False
    Private ACESSFILE As String = Application.StartupPath & "\AccesFile"

    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        If Not IO.File.Exists(txtAccessFile.Text) Then
            MessageBox.Show("Please enter valid access file", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        IsSaveButtonClicked = True


        AccessFile.Write(txtAccessFile.Text)
        MessageBox.Show("Done...", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub cmdBrowse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdBrowse.Click
        Dim openfile As New OpenFileDialog
        openfile.Title = "Select access file..."
        openfile.InitialDirectory = Application.StartupPath
        openfile.Filter = "Access Files (*.accdb)|*.accdb"
        If openfile.ShowDialog = DialogResult.OK Then
            txtAccessFile.Text = openfile.FileName
        End If
        openfile.Dispose()
    End Sub

    Private Sub Setting_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        txtAccessFile.Text = AccessFile.Read 'My.Settings.DBASE

        If My.Settings.Dbase_Config <> "" Then
            Dim ConStr() As String = SharedFunction.DecryptData(My.Settings.Dbase_Config).Split("=")

            txtServer.Text = ConStr(1).Split(";")(0)
            txtDbase.Text = ConStr(2).Split(";")(0)
            txtUser.Text = ConStr(3).Split(";")(0)
            txtPassword.Text = ConStr(4).Split(";")(0)
        End If
    End Sub

    Private Sub cmdReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdReset.Click
        Dim strAccessFile As String = Application.StartupPath & "\DataMerger - bak.accdb"
        If Not System.IO.File.Exists(strAccessFile) Then
            MessageBox.Show("Unable to find " & strAccessFile, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Else
            If System.IO.File.Exists(Application.StartupPath & "\DataMerger.accdb") Then System.IO.File.Delete(Application.StartupPath & "\DataMerger.accdb")
            System.IO.File.Copy(strAccessFile, Application.StartupPath & "\DataMerger.accdb")

            MessageBox.Show("Done...", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub

    Private Sub TestConnection()
        'Private ConStr As String = "Server=localhost;Database=dbcLBPUMIDTOOLS;User=sa;Password=acc2016;"
        Dim ConStr As String = "Server=" & txtServer.Text & ";Database=" & txtDbase.Text & ";User=" & txtUser.Text & ";Password=" & txtPassword.Text & ";"
        Dim DAL As New DAL
        If DAL.IsConnectionOK(ConStr) Then
            SharedFunction.ShowInfoMessage("Connection is success")
        Else
            SharedFunction.ShowErrorMessage("Connection failed")
        End If
        DAL.Dispose()
        DAL = Nothing
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim ConStr As String = "Server=" & txtServer.Text & ";Database=" & txtDbase.Text & ";User=" & txtUser.Text & ";Password=" & txtPassword.Text & ";"
        My.Settings.Dbase_Config = SharedFunction.EncryptData(ConStr)
        My.Settings.Save()
        My.Settings.Reload()

        DAL.ConStr = SharedFunction.DecryptData(My.Settings.Dbase_Config)

        MessageBox.Show("Done...", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        TestConnection()
    End Sub

End Class