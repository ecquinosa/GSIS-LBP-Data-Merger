
Public Class DateActivation

    Private Sub DateActivation_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        txtExpiryDate.Text = Now.Date
    End Sub

    Private Sub Button3_Click(sender As System.Object, e As System.EventArgs) Handles Button3.Click
        Dim openfile As New OpenFileDialog
        openfile.Title = "Select license file..."
        openfile.Filter = "License File (*.licx)|*.licx"
        If openfile.ShowDialog = DialogResult.OK Then
            txtLicx.Text = openfile.FileName
            Dim arrLicx() As String = AllcardLicenseHandler.LicenseHandler.GetLicenseInfoByExpiryDate(txtLicx.Text)
            lblMACAddress.Text = arrLicx(0)
            lblCPUID.Text = arrLicx(1)
            lblMotherboardID.Text = arrLicx(2)
            lblCreationDate.Text = arrLicx(3)
            lblActivationDate.Text = arrLicx(4)
            lblExpiryDate.Text = arrLicx(5)
            lblLicenseLastAccessed.Text = arrLicx(6)
            txtExpiryDate.Text = DateAdd(DateInterval.Year, 1, CDate(arrLicx(5))).ToString("MM/dd/yyyy")
        End If
        openfile.Dispose()
    End Sub

    Private Sub lblActivateLicense_Click(sender As System.Object, e As System.EventArgs) Handles lblActivateLicense.Click
        If AllcardLicenseHandler.SharedFunction.ShowMessage("Are you sure you want to activate license and set expiry to " & txtExpiryDate.Text & "?") = Windows.Forms.DialogResult.Yes Then
            AllcardLicenseHandler.LicenseHandler.ActivateClientLicenseByExpiryDate(txtLicx.Text, txtExpiryDate.Text)
            Close()
        End If
    End Sub



End Class