Public Class SharedFunction

    Const MsgHeader = "DATA MERGER"

    Private Shared EncryptionKey As String = "LbpAcc2018*@"
    Public Shared APP As String = "DATA MERGER"

    Public Shared UserID As Integer = 0
    Public Shared UserName As String
    Public Shared UserCompleteName As String
    Public Shared UserPass As String
    Public Shared RoleID As Integer
    Public Shared RoleDesc As String

    Public Shared Function ShowMessage(ByVal strMsg As String, Optional ByVal msgBoxBtn As MessageBoxButtons = MessageBoxButtons.YesNo, Optional ByVal msgBoxIcn As MessageBoxIcon = MessageBoxIcon.Question) As DialogResult
        Return MessageBox.Show(strMsg, MsgHeader, msgBoxBtn, msgBoxIcn)
    End Function

    Public Shared Function ShowInfoMessage(ByVal strMsg As String, Optional ByVal msgBoxBtn As MessageBoxButtons = MessageBoxButtons.OK) As DialogResult
        Return MessageBox.Show(strMsg, MsgHeader, msgBoxBtn, MessageBoxIcon.Information)
    End Function

    Public Shared Function ShowExcMessage(ByVal strMsg As String, Optional ByVal msgBoxBtn As MessageBoxButtons = MessageBoxButtons.OK) As DialogResult
        Return MessageBox.Show(strMsg, MsgHeader, msgBoxBtn, MessageBoxIcon.Exclamation)
    End Function

    Public Shared Function ShowErrorMessage(ByVal strMsg As String, Optional ByVal msgBoxBtn As MessageBoxButtons = MessageBoxButtons.OK) As DialogResult
        Return MessageBox.Show(strMsg, MsgHeader, msgBoxBtn, MessageBoxIcon.Error)
    End Function

    Public Shared Function GetIdleTimeout() As Integer
        Dim DAL As New DAL
        Try
            If DAL.ExecuteScalar("SELECT IdleTimeout FROM tblSystemParameter") Then Return DAL.ObjectResult
        Catch ex As Exception
            Return 15 'default 15minutes
        Finally
            DAL.Dispose()
            DAL = Nothing
        End Try
    End Function

    Public Shared Function EncryptData(ByVal value As String) As String
        Dim encryptor As New AllcardEncryptDecrypt.EncryptDecrypt(EncryptionKey)
        Dim encryptedValue As String = encryptor.TripleDesEncryptText(value)
        Return encryptedValue
    End Function

    Public Shared Function DecryptData(ByVal value As String) As String
        Dim decryptor As New AllcardEncryptDecrypt.EncryptDecrypt(EncryptionKey)
        Dim decryptedValue As String = decryptor.TripleDesDecryptText(value)
        Return decryptedValue
    End Function

    Public Shared Sub AddSystemLog(ByVal logDesc As String)
        Dim DAL As New DAL
        DAL.AddSystemLog_v2(UserID, logDesc)
        DAL.Dispose()
        DAL = Nothing
    End Sub

#Region " Logs "

    Private Shared SystemLog As String = "Logs\" & Now.ToString("MMddyyyy") & "\System.log"
    Private Shared ErrorLog As String = "Logs\" & Now.ToString("MMddyyyy") & "\Error.log"
    Private Shared ProcessLog As String = "Logs\" & Now.ToString("MMddyyyy") & "\Process.log"

    Private Shared Sub InitLogFolder()
        If Not IO.Directory.Exists("Logs\" & Now.ToString("MMddyyyy")) Then IO.Directory.CreateDirectory("Logs\" & Now.ToString("MMddyyyy"))
    End Sub

    Public Shared Sub SaveToLog(ByVal strData As String)
        Try
            InitLogFolder()
            Dim sw As New IO.StreamWriter(SystemLog, True)
            sw.WriteLine(strData)
            sw.Dispose()
            sw.Close()
        Catch ex As Exception
        End Try
    End Sub

    Public Shared Sub SaveToProcessLog(ByVal strData As String)
        Try
            InitLogFolder()
            Dim sw As New IO.StreamWriter(ProcessLog, True)
            sw.WriteLine(strData)
            sw.Dispose()
            sw.Close()
        Catch ex As Exception
        End Try
    End Sub

    Public Shared Sub SaveToErrorLog(ByVal strData As String)
        Try
            InitLogFolder()
            Dim sw As New IO.StreamWriter(ErrorLog, True)
            sw.WriteLine(strData)
            sw.Dispose()
            sw.Close()
        Catch ex As Exception
        End Try
    End Sub

    Public Shared Function TimeStamp() As String
        Return Now.ToString("MM/dd/yy hh:mm:ss tt").PadRight(25, " ")
    End Function

#End Region


End Class
