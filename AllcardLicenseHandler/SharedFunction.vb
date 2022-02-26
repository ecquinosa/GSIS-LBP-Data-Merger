
Imports System.Windows.Forms

Public Class SharedFunction

    Const MsgHeader = "ALLCARD LICENSE"

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

#Region " Logs "

    Private Shared SystemLog As String = "Logs\" & Now.ToString("MMddyyyy") & "\System.log"
    Private Shared ErrorLog As String = "Logs\" & Now.ToString("MMddyyyy") & "\Error.log"

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
