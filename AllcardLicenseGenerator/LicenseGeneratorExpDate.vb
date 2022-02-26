
Public Class LicenseGeneratorExpDate

    Private Sub LicenseGeneratorCount_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        If Not System.IO.File.Exists(AllcardLicenseHandler.LicenseHandler.licenseFile) Then
            AllcardLicenseHandler.LicenseHandler.GenerateLicenseByExpDate(False)
            Label1.Text = AllcardLicenseHandler.LicenseHandler.LicenseIsCreatedMsg
        Else
            Label1.Text = String.Format("License is detected")
            btnRegenerate.Visible = True
        End If
    End Sub

    Private Sub btnRegenerate_Click(sender As System.Object, e As System.EventArgs) Handles btnRegenerate.Click
        If AllcardLicenseHandler.SharedFunction.ShowMessage("Regenerate license?") = Windows.Forms.DialogResult.Yes Then
            AllcardLicenseHandler.LicenseHandler.GenerateLicenseByExpDate(False)
            Label1.Text = AllcardLicenseHandler.LicenseHandler.LicenseIsCreatedMsg
        End If
    End Sub

End Class