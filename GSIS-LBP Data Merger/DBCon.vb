
Imports System.Data
Imports System.Data.OleDb

Public Class DBCon

    'Private conStr As String = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + My.Settings.DBASE + ";Persist Security Info=False;"
    Private conStr As String = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + AccessFile.Read + ";Persist Security Info=False;"

    Dim strMessage As String = ""

    Public ReadOnly Property ErrorMessage() As String
        Get
            Return strMessage
        End Get
    End Property

    Public Function IsConnectionOk() As Boolean
        Try
            Dim con As OleDbConnection = New OleDbConnection(conStr)
            con.Open()
            con.Close()

            Return True
        Catch ex As Exception
            strMessage = ex.Message
            InsertErrorLog("", "", strMessage)
            Return False
        End Try
    End Function

    Public Function ExecuteQuery(ByVal strOleDbQuery As String) As Boolean
        Try
            Dim con As OleDbConnection = New OleDbConnection(conStr)
            Dim cmd As OleDbCommand = New OleDbCommand(strOleDbQuery, con)
            cmd.CommandType = CommandType.Text

            con.Open()
            cmd.ExecuteNonQuery()
            con.Close()

            Return True
        Catch ex As Exception
            strMessage = ex.Message
            InsertErrorLog("", "", strMessage)
            Return False
        End Try
    End Function

    Public Function IsGSISUMID_GSISNo_exist(ByVal strGSISNo As String, ByRef bln As Boolean) As Boolean
        Try
            Dim con As OleDbConnection = New OleDbConnection(conStr)
            Dim strQuery As String = "SELECT COUNT(GSISNo) FROM GSISUMID WHERE GSISNo='" & strGSISNo & "'"
            Dim cmd As OleDbCommand = New OleDbCommand(strQuery, con)
            cmd.CommandType = CommandType.Text

            con.Open()
            If CInt(cmd.ExecuteScalar) = 0 Then
                bln = False
            Else
                bln = True
            End If
            con.Close()

            Return True
        Catch ex As Exception
            strMessage = ex.Message
            InsertErrorLog("", "", strMessage)
            Return False
        End Try
    End Function

    Public Function InsertGSISUMID(ByVal strAcctNo As String, ByVal strGSISNo As String, ByVal strLine As String) As Boolean
        Try
            Dim con As OleDbConnection = New OleDbConnection(conStr)
            Dim cmd As OleDbCommand = New OleDbCommand("qryInsertGSISUMID", con)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("[@AcctNo]", strAcctNo)
            cmd.Parameters.AddWithValue("[@GSISNo]", strGSISNo)
            cmd.Parameters.AddWithValue("[@Line]", strLine)

            con.Open()
            cmd.ExecuteNonQuery()
            con.Close()

            Return True
        Catch ex As Exception
            strMessage = ex.Message
            InsertErrorLog("GSISUMID", strLine, strMessage)
            Return False
        End Try
    End Function

    Public Function InsertGSISAddr(ByVal strGSISNo As String, ByVal strMemberType As String,
                                   ByVal strPresentAddr As String, ByVal strPermaAddr As String,
                                   ByVal strBranchCode As String, ByVal strGSIS_BranchOffice As String,
                                   ByVal strGSIS_BranchGroup As String, ByVal strAgencyName As String,
                                   ByVal strLine As String) As Boolean
        Try
            Dim con As OleDbConnection = New OleDbConnection(conStr)
            Dim cmd As OleDbCommand = New OleDbCommand("qryInsertGSISAddr", con)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("[@GSISNo]", strGSISNo)
            cmd.Parameters.AddWithValue("[@MemberType]", strMemberType)
            cmd.Parameters.AddWithValue("[@PresentAddr]", strPresentAddr)
            cmd.Parameters.AddWithValue("[@PermaAddr]", strPermaAddr)
            If strBranchCode <> "" Then
                cmd.Parameters.AddWithValue("[@BranchCode]", strBranchCode)
            Else
                cmd.Parameters.AddWithValue("[@BranchCode]", 0)
            End If
            cmd.Parameters.AddWithValue("[@GSIS_BranchOffice]", strGSIS_BranchOffice)
            cmd.Parameters.AddWithValue("[@GSIS_BranchGroup]", strGSIS_BranchGroup)
            cmd.Parameters.AddWithValue("[@AgencyName]", strAgencyName)
            cmd.Parameters.AddWithValue("[@Line]", strLine)

            con.Open()
            cmd.ExecuteNonQuery()
            con.Close()

            Return True
        Catch ex As Exception
            strMessage = ex.Message
            InsertErrorLog("GSISUMID", strGSISNo, strMessage)
            Return False
        End Try
    End Function

    Public Function InsertJAI2UMID(ByVal strGSISNo As String) As Boolean
        Try
            Dim con As OleDbConnection = New OleDbConnection(conStr)
            Dim cmd As OleDbCommand = New OleDbCommand("qryInsertJAI2UMID", con)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("[@GSISNo]", strGSISNo)

            con.Open()
            cmd.ExecuteNonQuery()
            con.Close()

            Return True
        Catch ex As Exception
            strMessage = ex.Message
            InsertErrorLog("JAI2UMID", strGSISNo, strMessage)
            Return False
        End Try
    End Function

    Public Function InsertDECEMBFLE(ByVal strAcctNo As String, ByVal strGSISNo As String, ByVal strCardNo As String, _
                                    ByVal strJAI As String, ByVal strCardExpiry As String, ByVal strRouteNo As String, _
                                    ByVal strSequenceNo As String, ByVal strPIN As String, ByVal strCVV2 As String, ByVal strLine As String,
                                    ByVal striCVV As String, ByVal strCardHolderName As String, ByVal strLBP_BranchCode As String, _
                                    ByVal strMember_AgencyID As String) As Boolean
        Try
            Dim con As OleDbConnection = New OleDbConnection(conStr)
            Dim cmd As OleDbCommand = New OleDbCommand("qryInsertDECEMBFLE", con)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("[@AcctNo]", strAcctNo)
            cmd.Parameters.AddWithValue("[@GSISNo]", strGSISNo)
            cmd.Parameters.AddWithValue("[@CardNo]", strCardNo)
            cmd.Parameters.AddWithValue("[@JAI]", strJAI)
            cmd.Parameters.AddWithValue("[@CardExpiry]", strCardExpiry)
            cmd.Parameters.AddWithValue("[@RouteNo]", strRouteNo)
            cmd.Parameters.AddWithValue("[@SequenceNo]", strSequenceNo)
            cmd.Parameters.AddWithValue("[@PIN]", strPIN)
            cmd.Parameters.AddWithValue("[@CVV2]", strCVV2)
            cmd.Parameters.AddWithValue("[@Line]", strLine)
            cmd.Parameters.AddWithValue("[@iCVV]", striCVV)
            cmd.Parameters.AddWithValue("[@CardHolderName]", strCardHolderName)
            cmd.Parameters.AddWithValue("[@LBP_BranchCode]", strLBP_BranchCode)
            cmd.Parameters.AddWithValue("[@Member_AgencyID]", strMember_AgencyID)

            con.Open()
            cmd.ExecuteNonQuery()
            con.Close()

            Return True
        Catch ex As Exception
            strMessage = ex.Message
            InsertErrorLog("DECEMBFLE", strLine, strMessage)
            Return False
        End Try
    End Function

    Public Function Insertmapping_file(ByVal strCRN As String, ByVal strGSISNo As String, _
                                       ByVal strBPNo As String, ByVal strLine As String) As Boolean
        Try
            Dim con As OleDbConnection = New OleDbConnection(conStr)
            Dim cmd As OleDbCommand = New OleDbCommand("qryInsertmapping_file", con)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("[@CRN]", strCRN)
            cmd.Parameters.AddWithValue("[@GSISNo]", strGSISNo)
            cmd.Parameters.AddWithValue("[@BPNo]", strBPNo)
            cmd.Parameters.AddWithValue("[@Line]", strLine)

            con.Open()
            cmd.ExecuteNonQuery()
            con.Close()

            Return True
        Catch ex As Exception
            strMessage = ex.Message
            InsertErrorLog("mapping_file", strLine, strMessage)
            Return False
        End Try
    End Function

    Public Function InsertGSIS_BranchCode(ByVal strGSISNo As String, ByVal strBRCode As String) As Boolean
        Try
            Dim con As OleDbConnection = New OleDbConnection(conStr)
            Dim cmd As OleDbCommand = New OleDbCommand("qryInsertGSIS_BranchCode", con)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("[@GSISNo]", strGSISNo)
            cmd.Parameters.AddWithValue("[@BRCode]", strBRCode)

            con.Open()
            cmd.ExecuteNonQuery()
            con.Close()

            Return True
        Catch ex As Exception
            strMessage = ex.Message
            InsertErrorLog("GSIS_BranchCode", strGSISNo, strMessage)
            Return False
        End Try
    End Function

    Public Function InsertErrorLog(ByVal strTextfile As String, ByVal strLine As String, ByVal strError As String) As Boolean
        Try
            Dim con As OleDbConnection = New OleDbConnection(conStr)
            Dim cmd As OleDbCommand = New OleDbCommand("qryInsertErrorLog", con)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("[@Textfile]", strTextfile)
            cmd.Parameters.AddWithValue("[@Line]", strLine)
            cmd.Parameters.AddWithValue("[@Error]", strError)

            con.Open()
            cmd.ExecuteNonQuery()
            con.Close()

            Return True
        Catch ex As Exception
            strMessage = ex.Message
            Return False
        End Try
    End Function

    'Public Function InsertErrorLog(ByVal strError As String) As Boolean
    '    Try
    '        Dim con As OleDbConnection = New OleDbConnection(conStr)
    '        Dim cmd As OleDbCommand = New OleDbCommand("qryInsertErrorLog", con)
    '        cmd.CommandType = CommandType.StoredProcedure
    '        cmd.Parameters.AddWithValue("[@ErrorDesc]", strError)
    '        cmd.Parameters.AddWithValue("[@DatePost]", Now)

    '        con.Open()
    '        cmd.ExecuteNonQuery()
    '        con.Close()

    '        Return True
    '    Catch ex As Exception
    '        strMessage = ex.Message
    '        Return False
    '    End Try
    'End Function


    Public Function GetRecordCount(ByVal strTableName As String, ByRef intCntr As Integer) As Boolean
        Try
            Dim con As OleDbConnection = New OleDbConnection(conStr)
            Dim cmd As OleDbCommand = New OleDbCommand("SELECT COUNT(GSISNo) FROM " & strTableName, con)
            cmd.CommandType = CommandType.Text

            con.Open()
            Dim intValue As Object = cmd.ExecuteScalar

            If IsDBNull(intValue) Then
                intCntr = 0
            Else
                intCntr = CType(intValue, Decimal)
            End If

            con.Close()

            Return True
        Catch ex As Exception
            strMessage = ex.Message
            InsertErrorLog("", "", strMessage)
            Return False
        End Try
    End Function

    Public Function IsExist_mappingfile(ByVal strLine As String, ByRef intCntr As Integer) As Boolean
        Try
            Dim con As OleDbConnection = New OleDbConnection(conStr)
            Dim cmd As OleDbCommand = New OleDbCommand("SELECT COUNT(Line) FROM mapping_file WHERE Line='" & strLine & "'", con)
            cmd.CommandType = CommandType.Text

            con.Open()
            Dim intValue As Object = cmd.ExecuteScalar

            If IsDBNull(intValue) Then
                intCntr = 0
            Else
                intCntr = CType(intValue, Decimal)
            End If

            con.Close()

            Return True
        Catch ex As Exception
            strMessage = ex.Message
            InsertErrorLog("mapping_file", "", strMessage)
            Return False
        End Try
    End Function

    Public Function IsExist_GSIS_BranchCode(ByVal strGSISNo As String, ByRef intCntr As Integer) As Boolean
        Try
            Dim con As OleDbConnection = New OleDbConnection(conStr)
            Dim cmd As OleDbCommand = New OleDbCommand("SELECT COUNT(Line) FROM GSIS_BranchCode WHERE GSISNo='" & strGSISNo & "'", con)
            cmd.CommandType = CommandType.Text

            con.Open()
            Dim intValue As Object = cmd.ExecuteScalar

            If IsDBNull(intValue) Then
                intCntr = 0
            Else
                intCntr = CType(intValue, Decimal)
            End If

            con.Close()

            Return True
        Catch ex As Exception
            strMessage = ex.Message
            InsertErrorLog("mapping_file", "", strMessage)
            Return False
        End Try
    End Function

    Public Function IsExist_JAI2_UMID(ByVal strGSISNo As String, ByRef intCntr As Integer) As Boolean
        Try
            Dim con As OleDbConnection = New OleDbConnection(conStr)
            Dim cmd As OleDbCommand = New OleDbCommand("SELECT COUNT(GSISNo) FROM JAI2_UMID WHERE GSISNo='" & strGSISNo & "'", con)
            cmd.CommandType = CommandType.Text

            con.Open()
            Dim intValue As Object = cmd.ExecuteScalar

            If IsDBNull(intValue) Then
                intCntr = 0
            Else
                intCntr = CType(intValue, Decimal)
            End If

            con.Close()

            Return True
        Catch ex As Exception
            strMessage = ex.Message
            InsertErrorLog("mapping_file", "", strMessage)
            Return False
        End Try
    End Function

    Public Function IsExist_GSISAddr(ByVal strGSISNo As String, ByRef intCntr As Integer) As Boolean
        Try
            Dim con As OleDbConnection = New OleDbConnection(conStr)
            Dim cmd As OleDbCommand = New OleDbCommand("SELECT COUNT(GSISNo) FROM GSIS_Addr WHERE GSISNo='" & strGSISNo & "'", con)
            cmd.CommandType = CommandType.Text

            con.Open()
            Dim intValue As Object = cmd.ExecuteScalar

            If IsDBNull(intValue) Then
                intCntr = 0
            Else
                intCntr = CType(intValue, Decimal)
            End If

            con.Close()

            Return True
        Catch ex As Exception
            strMessage = ex.Message
            InsertErrorLog("GSIS_Addr", "", strMessage)
            Return False
        End Try
    End Function

    Public Function DeleteTableRecords(ByVal strTableName As String) As Boolean
        Try
            Dim con As OleDbConnection = New OleDbConnection(conStr)
            Dim cmd As OleDbCommand = New OleDbCommand("DELETE FROM " & strTableName, con)
            cmd.CommandType = CommandType.Text

            con.Open()
            cmd.ExecuteNonQuery()
            con.Close()

            Return True
        Catch ex As Exception
            strMessage = ex.Message
            InsertErrorLog("Deleting table " & strTableName, "", strMessage)
            Return False
        End Try
    End Function

    Public Function SelectTable(ByVal strTableName As String, ByRef dt As DataTable) As Boolean
        Try
            Dim ds As New DataSet
            Dim con As OleDbConnection = New OleDbConnection(conStr)
            Dim cmd As OleDbCommand = New OleDbCommand("SELECT * FROM " & strTableName, con)
            cmd.CommandType = CommandType.Text

            Dim da As OleDbDataAdapter = New OleDbDataAdapter(cmd)
            'da.Fill(ds, "Result")
            da.Fill(dt)
            da.Dispose()
            If con.State = ConnectionState.Open Then con.Close()

            Return True
        Catch ex As Exception
            strMessage = ex.Message
            InsertErrorLog("Selecting table " & strTableName, "", strMessage)
            Return False
        End Try
    End Function

    Public Function SelectStorPorc(ByVal SP As String, ByRef dt As DataTable) As Boolean
        Try
            Dim ds As New DataSet
            Dim con As OleDbConnection = New OleDbConnection(conStr)
            Dim cmd As OleDbCommand = New OleDbCommand(SP, con)
            cmd.CommandType = CommandType.StoredProcedure

            Dim da As OleDbDataAdapter = New OleDbDataAdapter(cmd)
            da.Fill(dt)
            da.Dispose()
            If con.State = ConnectionState.Open Then con.Close()

            Return True
        Catch ex As Exception
            strMessage = ex.Message
            InsertErrorLog("Selecting storproc " & SP, "", strMessage)
            Return False
        End Try
    End Function

    Public Function SelectGSIS_BranchCodeByGSISNo(ByVal strGSISNo As String, ByRef dt As DataTable) As Boolean
        Try
            Dim ds As New DataSet
            Dim con As OleDbConnection = New OleDbConnection(conStr)
            Dim cmd As OleDbCommand = New OleDbCommand("qrySelectGSIS_BranchCodeByGSISNo", con)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("[@GSISNo]", strGSISNo)

            Dim da As OleDbDataAdapter = New OleDbDataAdapter(cmd)
            da.Fill(dt)

            da.Dispose()
            If con.State = ConnectionState.Open Then con.Close()

            Return True
        Catch ex As Exception
            strMessage = ex.Message
            InsertErrorLog("Selecting qrySelectGSIS_BranchCodeByGSISNo", "", strMessage)
            Return False
        End Try
    End Function

    Public Function SelectDataForMerging(ByRef dt As DataTable) As Boolean
        'Dim ds As New DataSet
        'Dim con As OleDbConnection = New OleDbConnection(conStr)
        'Dim cmd As OleDbCommand = New OleDbCommand("qrySelectDataForMerging", con)
        'Try
        '    cmd.CommandType = CommandType.StoredProcedure
        '    Dim da As OleDbDataAdapter = New OleDbDataAdapter(cmd)
        '    da.Fill(dt)

        '    da.Dispose()
        '    da = Nothing
        '    If con.State = ConnectionState.Open Then con.Close()

        '    Return True
        'Catch ex As Exception
        '    strMessage = ex.Message
        '    InsertErrorLog("Selecting qrySelectDataForMerging", "", strMessage)
        '    Return False
        'Finally
        '    ds.Dispose()
        '    con.Dispose()
        '    cmd.Dispose()
        '    ds = Nothing
        '    con = Nothing
        '    cmd = Nothing
        'End Try

        Using myConnection As New OleDbConnection(conStr)
            Using myCmd As New OleDbCommand("qrySelectDataForMerging", myConnection)
                myCmd.CommandType = CommandType.StoredProcedure
                Dim myAdapter As New OleDbDataAdapter(myCmd)
                Dim myDataSet As New DataSet
                myAdapter.Fill(dt)

                myAdapter.Dispose()
                myAdapter = Nothing
                If myConnection.State = ConnectionState.Open Then
                    myConnection.Dispose()
                    myConnection.Close()
                End If

                Return True
            End Using
        End Using
    End Function

    Public Function SelectDataForMerging2(ByRef dt As DataTable) As Boolean
        Dim ds As New DataSet
        Dim con As OleDbConnection = New OleDbConnection(conStr)
        Dim cmd As OleDbCommand = New OleDbCommand("qrySelectDataForMerging2", con)
        Try
            cmd.CommandType = CommandType.StoredProcedure

            Dim da As OleDbDataAdapter = New OleDbDataAdapter(cmd)
            da.Fill(dt)

            da.Dispose()
            da = Nothing
            If con.State = ConnectionState.Open Then con.Close()

            Return True
        Catch ex As Exception
            strMessage = ex.Message
            InsertErrorLog("Selecting qrySelectDataForMerging2", "", strMessage)
            Return False
        Finally
            ds.Dispose()
            con.Dispose()
            cmd.Dispose()
            ds = Nothing
            con = Nothing
            cmd = Nothing
        End Try

        'Using myConnection As New OleDbConnection(conStr)
        '    Using myCmd As New OleDbCommand("qrySelectDataForMerging2", myConnection)
        '        myCmd.CommandType = CommandType.StoredProcedure
        '        Dim myAdapter As New OleDbDataAdapter(myCmd)
        '        Dim myDataSet As New DataSet
        '        myAdapter.Fill(dt)

        '        myAdapter.Dispose()
        '        myAdapter = Nothing
        '        If myConnection.State = ConnectionState.Open Then myConnection.Close()
        '        Return True
        '    End Using
        'End Using
    End Function

End Class