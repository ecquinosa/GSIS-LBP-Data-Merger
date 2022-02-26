
Imports System.Threading
Imports System.IO

Public Class clsThread

    Private _FileRef As DataMerger.FileRef
    Private _TotalRecords As Integer
    Private _Cntr As Integer
    Private _txtFile As String
    Private _dt As DataTable
    Private _result As String

    Private intGood As Integer = 0
    Private intBad As Integer = 0

    Private _errorMessage As String

    Private m_Args(1) As Object

    Private m_MainWindow As Form

    Private Delegate Sub NotifyMainWindow(ByVal FileRef As DataMerger.FileRef, ByVal intCntr As Integer, ByVal result As String,
                                          ByVal errorMessage As String, ByVal IsProcessFinish As Boolean, ByVal intGood As Integer,
                                          ByVal intBad As Integer)

    Private Delegate Sub LastProcess()

    'We need an object of this deletegate
    Private m_NotifyMainWindow As NotifyMainWindow
    Private m_LastProcess As LastProcess

    Public Sub New(ByVal FileRef As DataMerger.FileRef, ByVal TotalRecords As Integer, ByRef MainWindow As DataMerger, _
                   Optional ByVal TxtFile As String = "", Optional ByVal dt As DataTable = Nothing)
        _FileRef = FileRef
        _TotalRecords = TotalRecords
        m_MainWindow = MainWindow
        _txtFile = TxtFile
        _dt = dt

        'We need to point our delegate to the Method, which we want to call from this thread
        m_NotifyMainWindow = AddressOf MainWindow.ReceiveThreadMessage
        m_LastProcess = AddressOf MainWindow.LastProcess
    End Sub

    Public Sub StartThread()
        Select Case _FileRef
            Case DataMerger.FileRef.Reference
                InsertGSISUMID()
            Case DataMerger.FileRef.Mapping
                Insertmapping_file()
            Case DataMerger.FileRef.Embossing
                InsertDECEMBFLEv2()
            Case DataMerger.FileRef.BR
                InsertGSIS_BranchCode()
            Case DataMerger.FileRef.JAI2
                InsertJAI2_UMID()
            Case DataMerger.FileRef.GSIS_Addr
                InsertGSISAddr_UMIDv2()
        End Select

        UpdateMessage(True)
    End Sub

    Public Sub StartThreadWrapper()
        While True
            System.Threading.Thread.Sleep(100)
            m_MainWindow.Invoke(m_LastProcess)
        End While
    End Sub

    Private Sub UpdateMessage(Optional ByVal _IsProcessFinish As Boolean = False)
        ReDim m_Args(6)
        m_Args(0) = _FileRef
        m_Args(1) = _Cntr
        m_Args(2) = _result
        m_Args(3) = _errorMessage
        m_Args(4) = _IsProcessFinish
        m_Args(5) = intGood
        m_Args(6) = intBad
        m_MainWindow.Invoke(m_NotifyMainWindow, m_Args)
    End Sub

    Private Sub InsertGSISUMID()
        Dim DBCon As New DBCon

        _result = ""
        _errorMessage = ""

        'Dim sr As New StreamReader(_txtFile)
        Dim sb As New System.Text.StringBuilder
        'Dim intGood As Integer = 0
        'Dim intBad As Integer = 0

        sb.AppendLine("**Insertion of Reference file**")

        Using sr As New StreamReader(_txtFile)
            Do While Not sr.EndOfStream
                Dim strLine As String = sr.ReadLine
                If IsValidData(strLine.Trim) Then
                    Try
                        If DBCon.InsertGSISUMID(strLine.Substring(0, 10), strLine.Substring(10, 11), strLine) Then
                            intGood += 1
                        Else
                            sb.AppendLine(strLine & ": Error - " & DBCon.ErrorMessage)
                            intBad += 1
                        End If
                    Catch ex As Exception
                        sb.AppendLine(strLine & ": Error - " & ex.Message)
                        DBCon.InsertErrorLog("GSISUMID", strLine, ex.Message)
                        intBad += 1
                    End Try

                    _Cntr += 1
                    UpdateMessage()
                End If
            Loop
            sr.Dispose()
            sr.Close()
        End Using

        DBCon = Nothing

        sb.AppendLine("")
        sb.AppendLine(String.Format("Success: {0}, Failed: {1}", intGood.ToString("N0"), intBad.ToString("N0")))
        sb.AppendLine("")

        _result = sb.ToString
        _errorMessage = ""
    End Sub

    'Private Sub InsertDECEMBFLE()
    '    Dim DBCon As New DBCon

    '    _result = ""
    '    _errorMessage = ""

    '    Dim sr As New StreamReader(_txtFile)
    '    Dim sb As New System.Text.StringBuilder
    '    Dim intGood As Integer = 0
    '    Dim intBad As Integer = 0

    '    sb.AppendLine("**Insertion of Embossing file**")

    '    Do While Not sr.EndOfStream
    '        Dim strLine As String = sr.ReadLine
    '        If IsValidData(strLine.Trim) Then
    '            Try
    '                Dim strEmbossID As String = strLine.Substring(54, 5)
    '                Dim strJAI As String = strLine.Substring(59, 1)
    '                Dim strGSISNo As String = strLine.Substring(135, 11)
    '                Dim strAcctNo As String = strLine.Substring(79, 10)

    '                strAcctNo = strAcctNo.Substring(0, 4) + " " + strAcctNo.Substring(4, 4) + " " + strAcctNo.Substring(8, 2)

    '                Dim strCardNo As String = strEmbossID & strLine.Substring(0, 10) & strJAI
    '                Dim strFormattedCardNo As String = strCardNo.Substring(0, 5) & " " & strCardNo.Substring(5, 4) & " " & strCardNo.Substring(9, 4) & " " & strCardNo.Substring(13, 3)

    '                If DBCon.InsertDECEMBFLE(strAcctNo, strLine.Substring(135, 11), strFormattedCardNo, strLine.Substring(59, 1), strLine.Substring(50, 4), "", strLine.Substring(60, 9), strLine.Substring(69, 1), strLine.Substring(70, 4), strLine) Then
    '                    intGood += 1
    '                Else
    '                    sb.AppendLine(strLine & ": Error - " & DBCon.ErrorMessage)
    '                    intBad += 1
    '                End If
    '            Catch ex As Exception
    '                sb.AppendLine(strLine & ": Error - " & ex.Message)
    '                DBCon.InsertErrorLog("DECEMBFLE", strLine, ex.Message)
    '                intBad += 1
    '            End Try

    '            _Cntr += 1
    '            UpdateMessage()
    '        End If
    '    Loop

    '    sr.Dispose()
    '    sr.Close()
    '    sr = Nothing
    '    DBCon = Nothing

    '    sb.AppendLine("")
    '    sb.AppendLine(String.Format("Success: {0}, Failed: {1}", intGood.ToString("N0"), intBad.ToString("N0")))
    '    sb.AppendLine("")

    '    _result = sb.ToString
    '    _errorMessage = ""
    '    UpdateMessage(True)
    'End Sub

    Private Sub InsertDECEMBFLEv2()
        Dim DBCon As New DBCon

        _result = ""
        _errorMessage = ""

        'Dim sr As New StreamReader(_txtFile)
        Dim sb As New System.Text.StringBuilder
        'Dim intGood As Integer = 0
        'Dim intBad As Integer = 0

        sb.AppendLine("**Insertion of Embossing file**")

        Using sr As New StreamReader(_txtFile)
            Do While Not sr.EndOfStream
                Dim strLine As String = sr.ReadLine
                If IsValidData(strLine.Trim) Then
                    Try
                        Dim strAcctNo As String = strLine.Substring(171 - 1, 10)
                        Dim strCardNo = strLine.Substring(8 - 1, 16)
                        Dim strGSISNo = strLine.Substring(122 - 1, 11)
                        Dim strJAI = "" 'strLine.Substring(71 - 1, 1)
                        Dim strCardExpiry = strLine.Substring(67 - 1, 4)
                        Dim strRouteNo = ""
                        Dim strSequenceNo = strLine.Substring(101 - 1, 2)
                        Dim strPIN As String = strLine.Substring(85 - 1, 4)
                        Dim strCVV2 As String = strLine.Substring(92 - 1, 3)
                        Dim striCVV As String = strLine.Substring(209 - 1, 3)
                        Dim strCardHolderName As String = strLine.Substring(183 - 1, 26)
                        Dim strLBP_BranchCode As String = strLine.Substring(103 - 1, 5)
                        Dim strMember_AgencyID As String = strLine.Substring(112 - 1, 4)

                        'strAcctNo = strAcctNo.Substring(0, 4) + " " + strAcctNo.Substring(4, 4) + " " + strAcctNo.Substring(8, 2)

                        'Dim strFormattedCardNo As String = strCardNo.Substring(0, 5) & " " & strCardNo.Substring(5, 4) & " " & strCardNo.Substring(9, 4) & " " & strCardNo.Substring(13, 3)
                        Dim strFormattedCardNo As String = strCardNo.Substring(0, 4) & " " & strCardNo.Substring(4, 4) & " " & strCardNo.Substring(8, 4) & " " & strCardNo.Substring(12, 4)

                        If DBCon.InsertDECEMBFLE(strAcctNo, strGSISNo, strFormattedCardNo, strJAI, strCardExpiry, strRouteNo, strSequenceNo, strPIN, strCVV2, strLine, striCVV, strCardHolderName, strLBP_BranchCode, strMember_AgencyID) Then
                            intGood += 1
                        Else
                            sb.AppendLine(strLine & ": Error - " & DBCon.ErrorMessage)
                            intBad += 1
                        End If
                    Catch ex As Exception
                        sb.AppendLine(strLine & ": Error - " & ex.Message)
                        DBCon.InsertErrorLog("DECEMBFLE", strLine, ex.Message)
                        intBad += 1
                    End Try

                    _Cntr += 1
                    UpdateMessage()
                End If
            Loop

            sr.Dispose()
            sr.Close()
        End Using


        DBCon = Nothing

        sb.AppendLine("")
        sb.AppendLine(String.Format("Success: {0}, Failed: {1}", intGood.ToString("N0"), intBad.ToString("N0")))
        sb.AppendLine("")

        _result = sb.ToString
        _errorMessage = ""
        UpdateMessage(True)
    End Sub

    Private Sub Insertmapping_file()
        Dim intDuplicateCntr As Integer = 0

        Dim DBCon As New DBCon

        _result = ""
        _errorMessage = ""

        Dim sr As New StreamReader(_txtFile)
        Dim sb As New System.Text.StringBuilder
        'Dim intGood As Integer = 0
        'Dim intBad As Integer = 0

        sb.AppendLine("**Insertion of Mapping file**")

        Do While Not sr.EndOfStream
            Dim strLine As String = sr.ReadLine
            If IsValidData(strLine.Trim) Then
                Try
                    Dim intCntr As Short = 0

                    If DBCon.IsExist_mappingfile(strLine, intCntr) Then
                        If intCntr = 0 Then
                            If DBCon.Insertmapping_file(strLine.Substring(21, 12), strLine.Substring(10, 11), strLine.Substring(0, 10), strLine) Then
                                intGood += 1
                            Else
                                sb.AppendLine(strLine & ": Error - " & DBCon.ErrorMessage)
                                intBad += 1
                            End If
                        Else
                            sb.AppendLine(strLine & ": Error - duplicate")
                            intDuplicateCntr += 1
                        End If
                    End If
                Catch ex As Exception
                    sb.AppendLine(strLine & ": Error - " & ex.Message)
                    DBCon.InsertErrorLog("mapping_file", strLine, ex.Message)
                    intBad += 1
                End Try

                _Cntr += 1
                UpdateMessage()
            End If
        Loop

        sr.Dispose()
        sr.Close()
        sr = Nothing
        DBCon = Nothing

        sb.AppendLine("")
        sb.AppendLine(String.Format("Success: {0}, Failed: {1}", intGood.ToString("N0"), intBad.ToString("N0")))
        sb.AppendLine("")

        _result = sb.ToString
        _errorMessage = ""
       UpdateMessage(True)
    End Sub

    Private Sub InsertGSIS_BranchCode()
        Dim intDuplicateCntr As Integer = 0

        Dim DBCon As New DBCon

        _result = ""
        _errorMessage = ""

        Dim sb As New System.Text.StringBuilder
        'Dim intGood As Integer = 0
        'Dim intBad As Integer = 0

        sb.AppendLine("**Insertion of Branch Code file**")

        If Not _dt Is Nothing Then
            If _dt.DefaultView.Count = 0 Then
                sb.AppendLine("Table have no record")
            Else
                Dim intCntr As Short = 0
                For Each rw As DataRow In _dt.Rows
                    Try
                        If DBCon.IsExist_GSIS_BranchCode(rw("GSIS NO"), intCntr) Then
                            If intCntr = 0 Then
                                If DBCon.InsertGSIS_BranchCode(rw("GSIS NO"), rw("BR CODE")) Then
                                    intGood += 1
                                Else
                                    sb.AppendLine(rw("GSIS NO") & ": Error - " & DBCon.ErrorMessage)
                                    intBad += 1
                                End If
                            Else
                                intDuplicateCntr += 1
                            End If
                        End If
                    Catch ex As Exception
                        sb.AppendLine(rw("GSIS NO") & ": Error - " & ex.Message)
                        DBCon.InsertErrorLog("GSIS_BranchCode", rw("[GSIS NO]"), ex.Message)
                        intBad += 1
                    End Try

                    _Cntr += 1
                    UpdateMessage()
                Next

                DBCon = Nothing
            End If
        Else
            sb.AppendLine("Table is empty")
        End If

        sb.AppendLine("")
        sb.AppendLine(String.Format("Success: {0}, Failed: {1}", intGood.ToString("N0"), intBad.ToString("N0")))
        sb.AppendLine("")

        _result = sb.ToString
        _errorMessage = ""
        UpdateMessage(True)
    End Sub

    Private Sub InsertJAI2_UMID()
        Dim intDuplicateCntr As Integer = 0

        Dim DBCon As New DBCon

        _result = ""
        _errorMessage = ""

        Dim sb As New System.Text.StringBuilder
        'Dim intGood As Integer = 0
        'Dim intBad As Integer = 0

        sb.AppendLine("**Insertion of JAI2 file**")

        If Not _dt Is Nothing Then
            If _dt.DefaultView.Count = 0 Then
                sb.AppendLine("Table have no record")
            Else
                Dim intCntr As Short = 0
                For Each rw As DataRow In _dt.Rows
                    Try
                        If DBCon.IsExist_JAI2_UMID(rw("GSIS NUMBER"), intCntr) Then
                            If intCntr = 0 Then
                                If DBCon.InsertJAI2UMID(rw("GSIS NUMBER")) Then
                                    intGood += 1
                                Else
                                    sb.AppendLine(rw("GSIS NUMBER") & ": Error - " & DBCon.ErrorMessage)
                                    intBad += 1
                                End If
                            Else
                                intDuplicateCntr += 1
                            End If
                        End If
                    Catch ex As Exception
                        sb.AppendLine(rw("GSIS NUMBER") & ": Error - " & ex.Message)
                        DBCon.InsertErrorLog("JAI2_UMID", rw("[GSIS NUMBER]"), ex.Message)
                        intBad += 1
                    End Try

                    _Cntr += 1
                    UpdateMessage()
                Next

                DBCon = Nothing
            End If
        Else
            sb.AppendLine("Table is empty")
        End If

        sb.AppendLine("")
        sb.AppendLine(String.Format("Success: {0}, Failed: {1}", intGood.ToString("N0"), intBad.ToString("N0")))
        sb.AppendLine("")

        _result = sb.ToString
        _errorMessage = ""
        UpdateMessage(True)
    End Sub

    Private Sub InsertGSISAddr_UMID()
        Dim intDuplicateCntr As Integer = 0

        Dim DBCon As New DBCon

        _result = ""
        _errorMessage = ""

        Dim sb As New System.Text.StringBuilder
        'Dim intGood As Integer = 0
        'Dim intBad As Integer = 0

        sb.AppendLine("**Insertion of Addr file**")


        If Not _dt Is Nothing Then
            If _dt.DefaultView.Count = 0 Then
                sb.AppendLine("Table have no record")
            Else
                Dim intCntr As Short = 0
                For Each rw As DataRow In _dt.Rows
                    Try
                        If DBCon.IsExist_GSISAddr(rw("GSIS ID NUMBER"), intCntr) Then
                            If intCntr = 0 Then
                                Dim presentAddr As String = GetAddress(rw, 1)
                                Dim permanentAddr As String = GetAddress(rw, 2)

                                'If DBCon.InsertGSISAddr(rw("GSIS ID NUMBER"), System.Text.RegularExpressions.Regex.Replace(presentAddr, " {2,}", " "), System.Text.RegularExpressions.Regex.Replace(permanentAddr, " {2,}", " ")) Then
                                '    intGood += 1
                                'Else
                                '    sb.AppendLine(rw("GSIS ID NUMBER") & ": Error - " & DBCon.ErrorMessage)
                                '    intBad += 1
                                'End If
                            Else
                                intDuplicateCntr += 1
                            End If
                        End If
                    Catch ex As Exception
                        sb.AppendLine(rw("GSIS ID NUMBER") & ": Error - " & ex.Message)
                        DBCon.InsertErrorLog("GSIS_Addr", rw("GSIS ID NUMBER"), ex.Message)
                        intBad += 1
                    End Try

                    _Cntr += 1
                    UpdateMessage()
                Next

                DBCon = Nothing
            End If
        Else
            sb.AppendLine("Table is empty")
        End If

        sb.AppendLine("")
        sb.AppendLine(String.Format("Success: {0}, Failed: {1}", intGood.ToString("N0"), intBad.ToString("N0")))
        sb.AppendLine("")

        _result = sb.ToString
        _errorMessage = ""
        UpdateMessage(True)
    End Sub

    Private Sub InsertGSISAddr_UMIDv2()
       Dim intDuplicateCntr As Integer = 0

        Dim DBCon As New DBCon

        _result = ""
        _errorMessage = ""

        'Dim sr As New StreamReader(_txtFile)
        Dim sb As New System.Text.StringBuilder
        'Dim intGood As Integer = 0
        'Dim intBad As Integer = 0

        sb.AppendLine("**Insertion of Address file**")

        Using sr As New StreamReader(_txtFile)
            Do While Not sr.EndOfStream
                Dim strLine As String = sr.ReadLine
                If IsValidData(strLine.Trim) Then
                    Try
                        Dim intCntr As Short = 0
                        Dim arrLine() As String = strLine.Split("|")

                        If arrLine.Length = 7 Then
                            If DBCon.IsExist_GSISAddr(arrLine(0), intCntr) Then
                                If intCntr = 0 Then
                                    If DBCon.InsertGSISAddr(arrLine(0), arrLine(1), arrLine(2), "", arrLine(3), arrLine(4), arrLine(5), arrLine(6), strLine) Then
                                        intGood += 1
                                    Else
                                        sb.AppendLine(strLine & ": Error - " & DBCon.ErrorMessage)
                                        intBad += 1
                                    End If
                                Else
                                    sb.AppendLine(strLine & ": Error - duplicate")
                                    intDuplicateCntr += 1
                                End If
                            End If
                        Else
                            sb.AppendLine(strLine & ": Error - Line array <> 7")
                            intDuplicateCntr += 1
                        End If
                    Catch ex As Exception
                        sb.AppendLine(strLine & ": Error - " & ex.Message)
                        DBCon.InsertErrorLog("address_file", strLine, ex.Message)
                        intBad += 1
                    End Try

                    _Cntr += 1
                    UpdateMessage()
                End If
            Loop

            sr.Dispose()
            sr.Close()
        End Using


        DBCon = Nothing

        sb.AppendLine("")
        sb.AppendLine(String.Format("Success: {0}, Failed: {1}", intGood.ToString("N0"), intBad.ToString("N0")))
        sb.AppendLine("")

        _result = sb.ToString
        _errorMessage = ""
        UpdateMessage(True)
    End Sub


    Private Function GetAddress(ByVal rw As DataRow, ByVal intType As Short) As String
        Dim sb As New System.Text.StringBuilder
        If intType = 1 Then 'present address
            For i As Short = 5 To 10
                Dim value As String = IIf(IsDBNull(rw(i)), "", rw(i).ToString.Trim)
                If sb.ToString = "" Then
                    sb.Append(value)
                Else
                    sb.Append(" " & value)
                End If
            Next
        ElseIf intType = 2 Then 'permanent address
            For i As Short = 11 To 16
                Dim value As String = IIf(IsDBNull(rw(i)), "", rw(i).ToString.Trim)
                If sb.ToString = "" Then
                    sb.Append(value)
                Else
                    sb.Append(" " & value)
                End If
            Next
        End If

        Return sb.ToString
    End Function

    Private Function IsValidData(ByVal strData As String) As Boolean
        Select Case strData
            Case "", ""
                Return False
            Case Else
                Return True
        End Select
    End Function

End Class
