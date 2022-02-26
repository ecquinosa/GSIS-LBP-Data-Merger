Public Class Main

    Private Sub btnCountActivation_Click(sender As System.Object, e As System.EventArgs) Handles btnCountActivation.Click
        Dim frm As New CountActivation
        frm.ShowDialog()
    End Sub

    Private Sub btnDateActivation_Click(sender As System.Object, e As System.EventArgs) Handles btnDateActivation.Click
        Dim frm As New DateActivation
        frm.ShowDialog()
    End Sub

End Class