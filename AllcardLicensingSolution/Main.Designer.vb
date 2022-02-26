<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Main
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.btnDateActivation = New System.Windows.Forms.Button()
        Me.btnCountActivation = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'btnDateActivation
        '
        Me.btnDateActivation.Location = New System.Drawing.Point(25, 95)
        Me.btnDateActivation.Name = "btnDateActivation"
        Me.btnDateActivation.Size = New System.Drawing.Size(247, 99)
        Me.btnDateActivation.TabIndex = 0
        Me.btnDateActivation.Text = "ACTIVATE LICENSE BY EXPIRY DATE"
        Me.btnDateActivation.UseVisualStyleBackColor = True
        '
        'btnCountActivation
        '
        Me.btnCountActivation.Location = New System.Drawing.Point(25, 213)
        Me.btnCountActivation.Name = "btnCountActivation"
        Me.btnCountActivation.Size = New System.Drawing.Size(247, 99)
        Me.btnCountActivation.TabIndex = 1
        Me.btnCountActivation.Text = "ACTIVATE LICENSE BY COUNTER"
        Me.btnCountActivation.UseVisualStyleBackColor = True
        '
        'Main
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(297, 333)
        Me.Controls.Add(Me.btnCountActivation)
        Me.Controls.Add(Me.btnDateActivation)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Main"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "ALLCARD LICENSING"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnDateActivation As System.Windows.Forms.Button
    Friend WithEvents btnCountActivation As System.Windows.Forms.Button
End Class
