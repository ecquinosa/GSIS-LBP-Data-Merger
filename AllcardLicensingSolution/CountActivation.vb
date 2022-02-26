Public Class CountActivation

    Private Sub Button3_Click(sender As System.Object, e As System.EventArgs) Handles Button3.Click
        Dim openfile As New OpenFileDialog
        openfile.Title = "Select license file..."
        openfile.Filter = "License File (*.licx)|*.licx"
        'openfile.Filter = FileType
        'openfile.FilterIndex = 2
        'openfile.RestoreDirectory = True
        If openfile.ShowDialog = DialogResult.OK Then
            txtLicx.Text = openfile.FileName
            Dim arrLicx() As String = AllcardLicenseHandler.LicenseHandler.GetLicenseInfoByCount(txtLicx.Text)
            Try
                lblMACAddress.Text = arrLicx(0)
                lblCPUID.Text = arrLicx(1)
                lblMotherboardID.Text = arrLicx(2)
                lblCreationDate.Text = arrLicx(3)
                lblActivationDate.Text = arrLicx(4)
                lblLicenseLimit.Text = CInt(arrLicx(5)).ToString("N0")
                lblLicenseBalance.Text = CInt(arrLicx(6)).ToString("N0")
            Catch ex As Exception
                AllcardLicenseHandler.SharedFunction.ShowErrorMessage(ex.Message)
            End Try
        End If
        openfile.Dispose()
    End Sub

    Private Sub lblActivateLicense_Click(sender As System.Object, e As System.EventArgs) Handles lblActivateLicense.Click
        Dim limit As Integer = CInt(txtNewLicenseLimit.Text) + CInt(lblLicenseBalance.Text)
        If AllcardLicenseHandler.SharedFunction.ShowMessage("Are you sure you want to activate license with " & limit.ToString("N0") & " limit?") = Windows.Forms.DialogResult.Yes Then
            AllcardLicenseHandler.LicenseHandler.ActivateClientLicenseByCount(txtLicx.Text, limit)
            Close()
        End If
    End Sub
    
End Class