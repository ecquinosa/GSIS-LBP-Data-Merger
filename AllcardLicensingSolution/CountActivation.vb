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
            Dim arrLicx() As String = LicenseHandler.GetLicenseInfoByCount(txtLicx.Text)
            lblMACAddress.Text = arrLicx(0)
            lblCPUID.Text = arrLicx(1)
            lblMotherboardID.Text = arrLicx(2)
            lblCreationDate.Text = arrLicx(3)
            lblActivationDate.Text = arrLicx(4)
            lblLicenseLimit.Text = CInt(arrLicx(5)).ToString("N0")
            lblLicenseBalance.Text = CInt(arrLicx(6)).ToString("N0")
        End If
        openfile.Dispose()
    End Sub

    Private Sub lblActivateLicense_Click(sender As System.Object, e As System.EventArgs) Handles lblActivateLicense.Click
        Dim limit As Integer = CInt(txtNewLicenseLimit.Text) + CInt(lblLicenseBalance.Text)
        If AllcardLicenseHandler.SharedFunction.ShowMessage("Are you sure you want to activate license with " & limit.ToString("N0") & " limit?") = Windows.Forms.DialogResult.Yes Then
            LicenseHandler.ActivateLicenseVendorv2(limit, txtLicx.Text)
            Close()
        End If
    End Sub
    
End Class