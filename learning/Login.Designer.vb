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
        Dim CustomizableEdges1 As Guna.UI2.WinForms.Suite.CustomizableEdges = New Guna.UI2.WinForms.Suite.CustomizableEdges()
        Dim CustomizableEdges2 As Guna.UI2.WinForms.Suite.CustomizableEdges = New Guna.UI2.WinForms.Suite.CustomizableEdges()
        Dim CustomizableEdges3 As Guna.UI2.WinForms.Suite.CustomizableEdges = New Guna.UI2.WinForms.Suite.CustomizableEdges()
        Dim CustomizableEdges4 As Guna.UI2.WinForms.Suite.CustomizableEdges = New Guna.UI2.WinForms.Suite.CustomizableEdges()
        Label1 = New Label()
        TextBox1 = New TextBox()
        Label2 = New Label()
        TextBox2 = New TextBox()
        Label3 = New Label()
        Guna2PictureBox2 = New Guna.UI2.WinForms.Guna2PictureBox()
        BtnLogin = New Guna.UI2.WinForms.Guna2Button()
        CType(Guna2PictureBox2, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.BackColor = Color.Transparent
        Label1.Font = New Font("Segoe UI", 30F)
        Label1.ForeColor = SystemColors.ButtonFace
        Label1.Location = New Point(304, 104)
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
        ' Guna2PictureBox2
        ' 
        Guna2PictureBox2.CustomizableEdges = CustomizableEdges1
        Guna2PictureBox2.FillColor = Color.Transparent
        Guna2PictureBox2.Image = My.Resources.Resources.Gear
        Guna2PictureBox2.ImageRotate = 0F
        Guna2PictureBox2.Location = New Point(432, 108)
        Guna2PictureBox2.Name = "Guna2PictureBox2"
        Guna2PictureBox2.ShadowDecoration.CustomizableEdges = CustomizableEdges2
        Guna2PictureBox2.Size = New Size(55, 50)
        Guna2PictureBox2.TabIndex = 6
        Guna2PictureBox2.TabStop = False
        ' 
        ' BtnLogin
        ' 
        BtnLogin.CheckedState.FillColor = Color.Transparent
        BtnLogin.CustomizableEdges = CustomizableEdges3
        BtnLogin.DisabledState.BorderColor = Color.DarkGray
        BtnLogin.DisabledState.CustomBorderColor = Color.DarkGray
        BtnLogin.DisabledState.FillColor = Color.Transparent
        BtnLogin.DisabledState.ForeColor = Color.White
        BtnLogin.FillColor = Color.DodgerBlue
        BtnLogin.FocusedColor = Color.Transparent
        BtnLogin.Font = New Font("Segoe UI", 12F)
        BtnLogin.ForeColor = Color.White
        BtnLogin.Location = New Point(329, 302)
        BtnLogin.Name = "BtnLogin"
        BtnLogin.ShadowDecoration.CustomizableEdges = CustomizableEdges4
        BtnLogin.Size = New Size(122, 36)
        BtnLogin.TabIndex = 7
        BtnLogin.Text = "Login"
        ' 
        ' Login
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = SystemColors.Highlight
        ClientSize = New Size(819, 514)
        Controls.Add(BtnLogin)
        Controls.Add(Guna2PictureBox2)
        Controls.Add(Label3)
        Controls.Add(TextBox2)
        Controls.Add(Label2)
        Controls.Add(TextBox1)
        Controls.Add(Label1)
        Name = "Login"
        Text = "Form1"
        CType(Guna2PictureBox2, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents TextBox2 As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents Guna2PictureBox2 As Guna.UI2.WinForms.Guna2PictureBox
    Friend WithEvents BtnLogin As Guna.UI2.WinForms.Guna2Button

End Class
