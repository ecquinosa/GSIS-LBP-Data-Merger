
Public Class LicenseHandler

    Private Shared key As String = "LbpAllc@rD2017*"
    Public Shared licenseFile As String = System.Windows.Forms.Application.StartupPath & "\Allcard.licx"

#Region " Management objects "

    Public Shared Function getCPU_ID() As String

        Dim cpuID As String = String.Empty
        Dim mc As System.Management.ManagementClass = New System.Management.ManagementClass("Win32_Processor")
        Dim moc As System.Management.ManagementObjectCollection = mc.GetInstances()
        For Each mo As System.Management.ManagementObject In moc
            If (cpuID = String.Empty) Then
                cpuID = mo.Properties("ProcessorId").Value.ToString()
            End If
        Next
        Return cpuID
    End Function

    Public Shared Function getMotherBoardID() As [String]
        Dim serial As [String] = ""
        Try
            Dim mos As New System.Management.ManagementObjectSearcher("SELECT SerialNumber FROM Win32_BaseBoard")
            Dim moc As System.Management.ManagementObjectCollection = mos.[Get]()

            For Each mo As System.Management.ManagementObject In moc
                serial = mo("SerialNumber").ToString()
            Next
            Return serial
        Catch generatedExceptionName As Exception
            Return serial
        End Try
    End Function

    Public Shared Function GetMACAddress() As String
        Try
            Dim nic As System.Net.NetworkInformation.NetworkInterface = Nothing

            Dim mac_Address As String = ""

            For Each nic In System.Net.NetworkInformation.NetworkInterface.GetAllNetworkInterfaces

                mac_Address = nic.GetPhysicalAddress().ToString
                Select Case mac_Address
                    Case "", "00000000000000E0"
                    Case Else
                        Return mac_Address
                End Select
            Next

            nic = Nothing
        Catch ex As Exception
            'SharedFunction.ShowErrorMessage(ex.Message)
            Return ""
        End Try
    End Function

#End Region

    Public Shared Sub GenerateLicense()
        Dim allcardEncDec As New AllcardEncryptDecrypt.EncryptDecrypt(key)
        Try
            '0=MacAddr, 1=ProcessorID, 2=MotherBoardID, 3=LicxDateCreation, 4=LicxDateActivated, 5=CountLimit, 6=Balance
            Dim _date As Date = Now
            Dim _CounterLimit As Integer = 0
            Dim licx As String = String.Format("{0}^{1}^{2}^{3}^{4}^{5}^{6}", GetMACAddress, getCPU_ID, getMotherBoardID, _date.ToString, _date.ToString, _CounterLimit, _CounterLimit)
            Dim encLicx As String = allcardEncDec.TripleDesEncryptText(licx)
            System.IO.File.WriteAllText(licenseFile, encLicx)

            SharedFunction.SaveToLog(String.Format("{0}Generated license {1}", SharedFunction.TimeStamp, encLicx))

            SharedFunction.ShowInfoMessage(LicenseIsCreatedMsg)
        Catch ex As Exception
            SharedFunction.SaveToErrorLog(String.Format("{0}GenerateLicense(): {1}", SharedFunction.TimeStamp, ex.Message))
        Finally
            allcardEncDec = Nothing
        End Try
    End Sub

    Public Shared Sub GenerateLicenseByCount(Optional IsViewMsg As Boolean = True)
        Dim allcardEncDec As New AllcardEncryptDecrypt.EncryptDecrypt(key)
        Try
            '0=MacAddr, 1=ProcessorID, 2=MotherBoardID, 3=LicxDateCreation, 4=LicxDateActivated, 5=CountLimit, 6=Balance
            Dim _date As Date = Now
            Dim _CounterLimit As Integer = 0
            Dim licx As String = String.Format("{0}^{1}^{2}^{3}^{4}^{5}^{6}", GetMACAddress, getCPU_ID, getMotherBoardID, _date.ToString, _date.ToString, _CounterLimit, _CounterLimit)
            Dim encLicx As String = allcardEncDec.TripleDesEncryptText(licx)
            System.IO.File.WriteAllText(licenseFile, encLicx)

            SharedFunction.SaveToLog(String.Format("{0}Generated license {1}", SharedFunction.TimeStamp, encLicx))

            If IsViewMsg Then SharedFunction.ShowInfoMessage(LicenseIsCreatedMsg)
        Catch ex As Exception
            SharedFunction.SaveToErrorLog(String.Format("{0}GenerateLicense(): {1}", SharedFunction.TimeStamp, ex.Message))
        Finally
            allcardEncDec = Nothing
        End Try
    End Sub

    Public Shared Function LicenseIsCreatedMsg() As String
        Return "License is generated on '" & licenseFile & "'" & vbNewLine & vbNewLine & "Please email to Allcard for activation."
    End Function

    Public Shared Sub GenerateLicenseByExpDate(Optional IsViewMsg As Boolean = True)
        Dim allcardEncDec As New AllcardEncryptDecrypt.EncryptDecrypt(key)
        Try
            '0=MacAddr, 1=ProcessorID, 2=MotherBoardID, 3=LicxDateCreation, 4=LicxDateActivated, 5=LicxExpiryDate, 6=LastSuccessfulAccess
            Dim _date As Date = Now
            Dim licx As String = String.Format("{0}^{1}^{2}^{3}^{4}^{5}^{6}", GetMACAddress, getCPU_ID, getMotherBoardID, _date.ToString, _date.ToString, _date.ToString, "INITIAL")
            Dim encLicx As String = allcardEncDec.TripleDesEncryptText(licx)
            System.IO.File.WriteAllText(licenseFile, encLicx)

            SharedFunction.SaveToLog(String.Format("{0}Generated license {1}", SharedFunction.TimeStamp, encLicx))

            If IsViewMsg Then SharedFunction.ShowInfoMessage(LicenseIsCreatedMsg)
        Catch ex As Exception
            SharedFunction.SaveToErrorLog(String.Format("{0}GenerateLicense(): {1}", SharedFunction.TimeStamp, ex.Message))
        Finally
            allcardEncDec = Nothing
        End Try
    End Sub

    Public Shared Function ValidateLicense(ByRef ErrMsg As String) As Boolean
        '0=MacAddr, 1=ProcessorID, 2=MotherBoardID, 3=LicxDateCreation, 4=LicxDateActivated, 5=CountLimit, 6=Balance
        Dim allcardEncDec As New AllcardEncryptDecrypt.EncryptDecrypt(key)
        Try
            Dim licx As String = allcardEncDec.TripleDesDecryptText(System.IO.File.ReadAllText(licenseFile))
            Dim arrLicx() As String = licx.Split("^")
            If arrLicx(0) = GetMACAddress() Then
                If arrLicx(1) = getCPU_ID() Then
                    If arrLicx(2) = getMotherBoardID() Then
                        If arrLicx(5) > 0 Then
                            If arrLicx(3) <> arrLicx(4) Then
                                Return True
                            Else
                                'license not activated
                                ErrMsg = "License is not activated (03)"
                                Return False
                            End If
                        Else
                            'no balance
                            ErrMsg = "License is not verified (04)"
                            Return False
                        End If
                    Else
                        'not valid motherboardID
                        ErrMsg = "License is not valid for this terminal (02)"
                        Return False
                    End If
                Else
                    'not valid cpuID
                    ErrMsg = "License is not valid for this terminal (01)"
                    Return False
                End If
            Else
                'not valid macaddr
                ErrMsg = "License is not valid for this terminal (00)"
                Return False
            End If
        Catch ex As Exception
            ErrMsg = ex.Message
            SharedFunction.SaveToErrorLog(String.Format("{0}ValidateLicense(): {1}", SharedFunction.TimeStamp, ex.Message))
            Return False
        Finally
            allcardEncDec = Nothing
        End Try
    End Function

    Public Shared Function ValidateLicenseByCount(ByRef ErrMsg As String) As Boolean
        '0=MacAddr, 1=ProcessorID, 2=MotherBoardID, 3=LicxDateCreation, 4=LicxDateActivated, 5=CountLimit, 6=Balance
        If Not System.IO.File.Exists(licenseFile) Then
            ErrMsg = "No license found on this terminal"
            Return False
        End If


        Dim allcardEncDec As New AllcardEncryptDecrypt.EncryptDecrypt(key)
        Try
            Dim licx As String = allcardEncDec.TripleDesDecryptText(System.IO.File.ReadAllText(licenseFile))
            Dim arrLicx() As String = licx.Split("^")

            If arrLicx(0) <> GetMACAddress() Then
                'not valid macaddr
                ErrMsg = "License is not valid for this terminal (00)"
                Return False
            ElseIf arrLicx(1) <> getCPU_ID() Then
                'not valid cpuID
                ErrMsg = "License is not valid for this terminal (01)"
                Return False
            ElseIf arrLicx(2) <> getMotherBoardID() Then
                'not valid cpuID
                ErrMsg = "License is not valid for this terminal (02)"
                Return False
            ElseIf arrLicx(3) = arrLicx(4) Then
                'license not activated
                ErrMsg = "License is not activated (03)"
                Return False
            ElseIf arrLicx(5) = 0 Then
                'no balance
                ErrMsg = "License is not verified (04)"
                Return False
            Else
                Return True
            End If

        Catch ex As Exception
            ErrMsg = ex.Message
            SharedFunction.SaveToErrorLog(String.Format("{0}ValidateLicenseByExpiryDate(): {1}", SharedFunction.TimeStamp, ex.Message))
            Return False
        Finally
            allcardEncDec = Nothing
        End Try
    End Function

    Public Shared Function ValidateLicenseByExpiryDate(ByVal lastAccessed As String, ByVal LTAG As String, ByRef ErrMsg As String) As Boolean
        '0=MacAddr, 1=ProcessorID, 2=MotherBoardID, 3=LicxDateCreation, 4=LicxDateActivated, 5=ExpiryDate, 6=LastAccessed
        If Not System.IO.File.Exists(licenseFile) Then
            ErrMsg = "No license found on this terminal"
            Return False
        End If


        Dim allcardEncDec As New AllcardEncryptDecrypt.EncryptDecrypt(key)
        Try
            Dim licx As String = allcardEncDec.TripleDesDecryptText(System.IO.File.ReadAllText(licenseFile))
            Dim arrLicx() As String = licx.Split("^")

            If arrLicx(0) <> GetMACAddress() Then
                'not valid macaddr
                ErrMsg = "License is not valid for this terminal (00)"
                Return False
            ElseIf arrLicx(1) <> getCPU_ID() Then
                'not valid cpuID
                ErrMsg = "License is not valid for this terminal (01)"
                Return False
            ElseIf arrLicx(2) <> getMotherBoardID() Then
                'not valid cpuID
                ErrMsg = "License is not valid for this terminal (02)"
                Return False
            ElseIf arrLicx(3) = arrLicx(4) Then
                'license not activated
                ErrMsg = "License is not activated (03)"
                Return False
            ElseIf arrLicx(3) = arrLicx(5) Then
                'no expiry date set yet
                ErrMsg = "License is not verified (04)"
                Return False
            ElseIf arrLicx(6) = "INITIAL" Then
                ErrMsg = "RESTART_APP"
                SharedFunction.ShowInfoMessage("Initialization complete. Please restart application")
                RecreateClientLicenseByExpiryDate(lastAccessed)
                Return True
            ElseIf Now.Date > CDate(arrLicx(5)) Then
                ErrMsg = "License expired. Please contact Allcard"
                Return False
            ElseIf arrLicx(6) <> LTAG Then
                'tag discrepancy
                ErrMsg = "License is invalid (TAG DISC1). Please contact Allcard"
                Return False
            ElseIf Now < CDate(arrLicx(6)) Then
                'tag discrepancy
                ErrMsg = "License is invalid (TAG DISC2). Please contact Allcard"
                Return False
            Else
                RecreateClientLicenseByExpiryDate(lastAccessed)

                Return True
            End If

        Catch ex As Exception
            ErrMsg = ex.Message
            SharedFunction.SaveToErrorLog(String.Format("{0}ValidateLicenseByExpiryDate(): {1}", SharedFunction.TimeStamp, ex.Message))
            Return False
        Finally
            allcardEncDec = Nothing
        End Try
    End Function

    Public Shared Sub ActivateLicenseClient()
        '0=MacAddr, 1=ProcessorID, 2=MotherBoardID, 3=LicxDateCreation, 4=LicxDateActivated, 5=CountLimit, 6=Balance
        Dim allcardEncDec As New AllcardEncryptDecrypt.EncryptDecrypt(key)
        Try
            Dim licx As String = allcardEncDec.TripleDesDecryptText(System.IO.File.ReadAllText(licenseFile))
            Dim arrLicx() As String = licx.Split("^")
            If arrLicx(5) > 0 Then
                Dim newlicx As String = String.Format("{0}^{1}^{2}^{3}^{4}^{5}^{6}", arrLicx(0), arrLicx(1), arrLicx(2), arrLicx(3), Now.ToString, arrLicx(5), arrLicx(6))
                System.IO.File.WriteAllText(licenseFile, allcardEncDec.TripleDesEncryptText(newlicx))

                SharedFunction.ShowExcMessage("License is successfully activated")
            Else
                SharedFunction.ShowExcMessage("License is not verified (04)")
            End If
        Catch ex As Exception
            SharedFunction.SaveToErrorLog(String.Format("{0}ActivateLicenseClient(): {1}", SharedFunction.TimeStamp, ex.Message))
        Finally
            allcardEncDec = Nothing
        End Try
    End Sub

    Public Shared Sub ActivateLicenseVendor(ByVal limit As Integer)
        '0=MacAddr, 1=ProcessorID, 2=MotherBoardID, 3=LicxDateCreation, 4=LicxDateActivated, 5=CountLimit, 6=Balance
        Dim allcardEncDec As New AllcardEncryptDecrypt.EncryptDecrypt(key)
        Try
            Dim licx As String = allcardEncDec.TripleDesDecryptText(System.IO.File.ReadAllText(licenseFile))
            Dim arrLicx() As String = licx.Split("^")
            Dim newlicx As String = String.Format("{0}^{1}^{2}^{3}^{4}^{5}^{6}", arrLicx(0), arrLicx(1), arrLicx(2), arrLicx(3), Now.ToString, limit, limit)
            System.IO.File.WriteAllText(licenseFile, allcardEncDec.TripleDesEncryptText(newlicx))
            SharedFunction.ShowInfoMessage("License is successfully activated")
        Catch ex As Exception
            SharedFunction.SaveToErrorLog(String.Format("{0}ActivateLicense(): {1}", SharedFunction.TimeStamp, ex.Message))
        Finally
            allcardEncDec = Nothing
        End Try
    End Sub

    Public Shared Sub ActivateClientLicenseByCount(ByVal _licenseFile As String, ByVal limit As Integer)
        '0=MacAddr, 1=ProcessorID, 2=MotherBoardID, 3=LicxDateCreation, 4=LicxDateActivated, 5=CountLimit, 6=Balance
        Dim allcardEncDec As New AllcardEncryptDecrypt.EncryptDecrypt(key)
        Try
            Dim licx As String = allcardEncDec.TripleDesDecryptText(System.IO.File.ReadAllText(_licenseFile))
            Dim arrLicx() As String = licx.Split("^")
            Dim newlicx As String = String.Format("{0}^{1}^{2}^{3}^{4}^{5}^{6}", arrLicx(0), arrLicx(1), arrLicx(2), arrLicx(3), Now.ToString, limit, limit)
            System.IO.File.WriteAllText(_licenseFile, allcardEncDec.TripleDesEncryptText(newlicx))
            SharedFunction.ShowInfoMessage("License is successfully activated")
        Catch ex As Exception
            SharedFunction.SaveToErrorLog(String.Format("{0}ActivateLicense(): {1}", SharedFunction.TimeStamp, ex.Message))
        Finally
            allcardEncDec = Nothing
        End Try
    End Sub

    Public Shared Sub ActivateClientLicenseByExpiryDate(ByVal _licenseFile As String, ByVal expiryDate As String)
        '0=MacAddr, 1=ProcessorID, 2=MotherBoardID, 3=LicxDateCreation, 4=LicxDateActivated, 5=CountLimit, 6=Balance
        Dim allcardEncDec As New AllcardEncryptDecrypt.EncryptDecrypt(key)
        Try
            Dim licx As String = allcardEncDec.TripleDesDecryptText(System.IO.File.ReadAllText(_licenseFile))
            Dim arrLicx() As String = licx.Split("^")
            Dim newlicx As String = String.Format("{0}^{1}^{2}^{3}^{4}^{5}^{6}", arrLicx(0), arrLicx(1), arrLicx(2), arrLicx(3), Now.ToString, expiryDate, arrLicx(6))
            System.IO.File.WriteAllText(_licenseFile, allcardEncDec.TripleDesEncryptText(newlicx))
            SharedFunction.ShowInfoMessage("License is successfully activated")
        Catch ex As Exception
            SharedFunction.SaveToErrorLog(String.Format("{0}ActivateLicense(): {1}", SharedFunction.TimeStamp, ex.Message))
        Finally
            allcardEncDec = Nothing
        End Try
    End Sub

    Public Shared Sub RecreateClientLicenseByExpiryDate(ByVal lastAccessed As String)
        '0=MacAddr, 1=ProcessorID, 2=MotherBoardID, 3=LicxDateCreation, 4=LicxDateActivated, 5=ExpiryDate, 6=LastAccessed
        Dim allcardEncDec As New AllcardEncryptDecrypt.EncryptDecrypt(key)
        Try
            Dim licx As String = allcardEncDec.TripleDesDecryptText(System.IO.File.ReadAllText(licenseFile))
            Dim arrLicx() As String = licx.Split("^")
            Dim newlicx As String = String.Format("{0}^{1}^{2}^{3}^{4}^{5}^{6}", arrLicx(0), arrLicx(1), arrLicx(2), arrLicx(3), arrLicx(4), arrLicx(5), lastAccessed)
            System.IO.File.WriteAllText(licenseFile, allcardEncDec.TripleDesEncryptText(newlicx))
        Catch ex As Exception
            SharedFunction.SaveToErrorLog(String.Format("{0}RecreateClientLicenseByExpiryDate(): {1}", SharedFunction.TimeStamp, ex.Message))
        Finally
            allcardEncDec = Nothing
        End Try
    End Sub

    Public Shared Function GetLicenseBalance() As Integer
        '0=MacAddr, 1=ProcessorID, 2=MotherBoardID, 3=LicxDateCreation, 4=LicxDateActivated, 5=CountLimit, 6=Balance
        Dim allcardEncDec As New AllcardEncryptDecrypt.EncryptDecrypt(key)
        Try
            Dim licx As String = allcardEncDec.TripleDesDecryptText(System.IO.File.ReadAllText(licenseFile))
            Dim arrLicx() As String = licx.Split("^")
            Return arrLicx(6)
        Catch ex As Exception
            SharedFunction.SaveToErrorLog(String.Format("{0}GetLicenseBalance(): {1}", SharedFunction.TimeStamp, ex.Message))
            Return 0
        Finally
            allcardEncDec = Nothing
        End Try
    End Function

    Public Shared Function GetLicenseInfo(ByVal _licenseFile As String) As String()
        '0=MacAddr, 1=ProcessorID, 2=MotherBoardID, 3=LicxDateCreation, 4=LicxDateActivated, 5=CountLimit, 6=Balance
        Dim allcardEncDec As New AllcardEncryptDecrypt.EncryptDecrypt(key)
        Try
            Dim licx As String = allcardEncDec.TripleDesDecryptText(System.IO.File.ReadAllText(_licenseFile))
            Return licx.Split("^")
        Catch ex As Exception
            SharedFunction.SaveToErrorLog(String.Format("{0}GetLicenseInfo(): {1}", SharedFunction.TimeStamp, ex.Message))
            Return Nothing
        Finally
            allcardEncDec = Nothing
        End Try
    End Function


    Public Shared Function GetLicenseInfoByCount(ByVal _licenseFile As String) As String()
        '0=MacAddr, 1=ProcessorID, 2=MotherBoardID, 3=LicxDateCreation, 4=LicxDateActivated, 5=CountLimit, 6=Balance
        Dim allcardEncDec As New AllcardEncryptDecrypt.EncryptDecrypt(key)
        Try
            Dim licx As String = allcardEncDec.TripleDesDecryptText(System.IO.File.ReadAllText(_licenseFile))
            Return licx.Split("^")
        Catch ex As Exception
            SharedFunction.SaveToErrorLog(String.Format("{0}GetLicenseInfo(): {1}", SharedFunction.TimeStamp, ex.Message))
            Return Nothing
        Finally
            allcardEncDec = Nothing
        End Try
    End Function

    Public Shared Function GetLicenseInfoByExpiryDate(ByVal _licenseFile As String) As String()
        '0=MacAddr, 1=ProcessorID, 2=MotherBoardID, 3=LicxDateCreation, 4=LicxDateActivated, 5=ExpiryDate, 6=LastAccessed
        Dim allcardEncDec As New AllcardEncryptDecrypt.EncryptDecrypt(key)
        Try
            Dim licx As String = allcardEncDec.TripleDesDecryptText(System.IO.File.ReadAllText(_licenseFile))
            Return licx.Split("^")
        Catch ex As Exception
            SharedFunction.SaveToErrorLog(String.Format("{0}GetLicenseInfo(): {1}", SharedFunction.TimeStamp, ex.Message))
            Return Nothing
        Finally
            allcardEncDec = Nothing
        End Try
    End Function

    Public Shared Sub UpdateLicenseBalance(ByVal balance As Integer)
        '0=MacAddr, 1=ProcessorID, 2=MotherBoardID, 3=LicxDateCreation, 4=LicxDateActivated, 5=CountLimit, 6=Balance
        Dim allcardEncDec As New AllcardEncryptDecrypt.EncryptDecrypt(key)
        Try
            Dim licx As String = allcardEncDec.TripleDesDecryptText(System.IO.File.ReadAllText(licenseFile))
            Dim arrLicx() As String = licx.Split("^")
            Dim newlicx As String = String.Format("{0}^{1}^{2}^{3}^{4}^{5}^{6}", arrLicx(0), arrLicx(1), arrLicx(2), arrLicx(3), arrLicx(4), arrLicx(5), balance)
            System.IO.File.WriteAllText(licenseFile, allcardEncDec.TripleDesEncryptText(newlicx))
        Catch ex As Exception
            SharedFunction.SaveToErrorLog(String.Format("{0}UpdateLicenseBalance(): {1}", SharedFunction.TimeStamp, ex.Message))
        Finally
            allcardEncDec = Nothing
        End Try
    End Sub

    Public Shared Function EncryptData(ByVal data As String) As String
        Dim allcardEncDec As New AllcardEncryptDecrypt.EncryptDecrypt(key)
        Try
            Dim encyptedData As String = allcardEncDec.TripleDesDecryptText(data)
            Return encyptedData
        Catch ex As Exception
            SharedFunction.SaveToErrorLog(String.Format("{0}ActivateLicense(): {1}", SharedFunction.TimeStamp, ex.Message))
            Return "Error"
        Finally
            allcardEncDec = Nothing
        End Try
    End Function

End Class
