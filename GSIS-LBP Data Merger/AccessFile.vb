
Imports System.IO

Public Class AccessFile

    Private Shared strAccessFile As String = Application.StartupPath & "\AccesFile"

    Public Shared Sub InitializeFile()
        If Not File.Exists(strAccessFile) Then
            Dim sw As New StreamWriter(strAccessFile)
            sw.WriteLine("")
            sw.Dispose()
            sw.Close()
        End If
    End Sub

    Public Shared Sub Write(ByVal strData As String)
        Dim sw As New StreamWriter(strAccessFile)
        sw.WriteLine(strData)
        sw.Dispose()
        sw.Close()
    End Sub

    Public Shared Function Read() As String
        Dim sr As New StreamReader(strAccessFile)
        Dim str As String = sr.ReadToEnd
        sr.Dispose()
        sr.Close()

        Return str.Trim
    End Function

End Class
