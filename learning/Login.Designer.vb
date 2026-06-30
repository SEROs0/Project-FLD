<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Login
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(disposing As Boolean)
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Label1 = New Label()
        TextBox1 = New TextBox()
        Button1 = New Button()
        Label2 = New Label()
        TextBox2 = New TextBox()
        Label3 = New Label()
        SuspendLayout()
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.BackColor = Color.Transparent
        Label1.Font = New Font("Segoe UI", 30F)
        Label1.ForeColor = SystemColors.ButtonFace
        Label1.Location = New Point(327, 104)
        Label1.Name = "Label1"
        Label1.Size = New Size(122, 54)
        Label1.TabIndex = 0
        Label1.Text = "Login"
        Label1.TextAlign = ContentAlignment.TopCenter
        ' 
        ' TextBox1
        ' 
        TextBox1.Location = New Point(315, 200)
        TextBox1.Name = "TextBox1"
        TextBox1.Size = New Size(151, 23)
        TextBox1.TabIndex = 1
        ' 
        ' Button1
        ' 
        Button1.Font = New Font("Segoe UI", 12F)
        Button1.Location = New Point(344, 297)
        Button1.Name = "Button1"
        Button1.Size = New Size(75, 38)
        Button1.TabIndex = 2
        Button1.Text = "Login"
        Button1.UseVisualStyleBackColor = True
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Font = New Font("Segoe UI", 13F)
        Label2.ForeColor = SystemColors.ButtonFace
        Label2.Location = New Point(315, 172)
        Label2.Name = "Label2"
        Label2.Size = New Size(47, 25)
        Label2.TabIndex = 3
        Label2.Text = "User"
        ' 
        ' TextBox2
        ' 
        TextBox2.Location = New Point(315, 254)
        TextBox2.Name = "TextBox2"
        TextBox2.Size = New Size(151, 23)
        TextBox2.TabIndex = 4
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.Font = New Font("Segoe UI", 13F)
        Label3.ForeColor = SystemColors.ButtonFace
        Label3.Location = New Point(315, 226)
        Label3.Name = "Label3"
        Label3.Size = New Size(87, 25)
        Label3.TabIndex = 5
        Label3.Text = "Password"
        ' 
        ' Login
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = SystemColors.Highlight
        ClientSize = New Size(819, 514)
        Controls.Add(Label3)
        Controls.Add(TextBox2)
        Controls.Add(Label2)
        Controls.Add(Button1)
        Controls.Add(TextBox1)
        Controls.Add(Label1)
        Name = "Login"
        Text = "Form1"
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents Button1 As Button
    Friend WithEvents Label2 As Label
    Friend WithEvents TextBox2 As TextBox
    Friend WithEvents Label3 As Label

End Class
