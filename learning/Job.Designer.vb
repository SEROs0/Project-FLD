<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Job
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
        Dim CustomizableEdges1 As Guna.UI2.WinForms.Suite.CustomizableEdges = New Guna.UI2.WinForms.Suite.CustomizableEdges()
        Dim CustomizableEdges2 As Guna.UI2.WinForms.Suite.CustomizableEdges = New Guna.UI2.WinForms.Suite.CustomizableEdges()
        Dim CustomizableEdges3 As Guna.UI2.WinForms.Suite.CustomizableEdges = New Guna.UI2.WinForms.Suite.CustomizableEdges()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Job))
        Dim CustomizableEdges4 As Guna.UI2.WinForms.Suite.CustomizableEdges = New Guna.UI2.WinForms.Suite.CustomizableEdges()
        Dim CustomizableEdges5 As Guna.UI2.WinForms.Suite.CustomizableEdges = New Guna.UI2.WinForms.Suite.CustomizableEdges()
        Dim CustomizableEdges6 As Guna.UI2.WinForms.Suite.CustomizableEdges = New Guna.UI2.WinForms.Suite.CustomizableEdges()
        Dim CustomizableEdges7 As Guna.UI2.WinForms.Suite.CustomizableEdges = New Guna.UI2.WinForms.Suite.CustomizableEdges()
        Dim CustomizableEdges8 As Guna.UI2.WinForms.Suite.CustomizableEdges = New Guna.UI2.WinForms.Suite.CustomizableEdges()
        Dim CustomizableEdges9 As Guna.UI2.WinForms.Suite.CustomizableEdges = New Guna.UI2.WinForms.Suite.CustomizableEdges()
        Dim CustomizableEdges10 As Guna.UI2.WinForms.Suite.CustomizableEdges = New Guna.UI2.WinForms.Suite.CustomizableEdges()
        Panel6 = New Panel()
        Guna2PictureBox1 = New Guna.UI2.WinForms.Guna2PictureBox()
        Label1 = New Label()
        Panel2 = New Panel()
        btnPo = New Guna.UI2.WinForms.Guna2Button()
        btnOrder = New Guna.UI2.WinForms.Guna2Button()
        btnSTORE = New Guna.UI2.WinForms.Guna2Button()
        Guna2PictureBox2 = New Guna.UI2.WinForms.Guna2PictureBox()
        Panel6.SuspendLayout()
        CType(Guna2PictureBox1, ComponentModel.ISupportInitialize).BeginInit()
        Panel2.SuspendLayout()
        CType(Guna2PictureBox2, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' Panel6
        ' 
        Panel6.BackColor = Color.DodgerBlue
        Panel6.Controls.Add(Guna2PictureBox1)
        Panel6.Controls.Add(Label1)
        Panel6.Location = New Point(124, 0)
        Panel6.Name = "Panel6"
        Panel6.Size = New Size(1235, 76)
        Panel6.TabIndex = 20
        ' 
        ' Guna2PictureBox1
        ' 
        Guna2PictureBox1.CustomizableEdges = CustomizableEdges1
        Guna2PictureBox1.Image = My.Resources.Resources.Task
        Guna2PictureBox1.ImageRotate = 0F
        Guna2PictureBox1.Location = New Point(24, 12)
        Guna2PictureBox1.Name = "Guna2PictureBox1"
        Guna2PictureBox1.ShadowDecoration.CustomizableEdges = CustomizableEdges2
        Guna2PictureBox1.Size = New Size(50, 50)
        Guna2PictureBox1.TabIndex = 2
        Guna2PictureBox1.TabStop = False
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Font = New Font("Segoe UI", 20F)
        Label1.ForeColor = SystemColors.ButtonHighlight
        Label1.Location = New Point(74, 19)
        Label1.Name = "Label1"
        Label1.Size = New Size(62, 37)
        Label1.TabIndex = 1
        Label1.Text = "JOB"
        ' 
        ' Panel2
        ' 
        Panel2.BackColor = SystemColors.HotTrack
        Panel2.Controls.Add(btnPo)
        Panel2.Controls.Add(btnOrder)
        Panel2.Controls.Add(btnSTORE)
        Panel2.Controls.Add(Guna2PictureBox2)
        Panel2.Location = New Point(0, 0)
        Panel2.Name = "Panel2"
        Panel2.Size = New Size(124, 756)
        Panel2.TabIndex = 19
        ' 
        ' btnPo
        ' 
        btnPo.CustomizableEdges = CustomizableEdges3
        btnPo.DisabledState.BorderColor = Color.DarkGray
        btnPo.DisabledState.CustomBorderColor = Color.DarkGray
        btnPo.DisabledState.FillColor = Color.FromArgb(CByte(169), CByte(169), CByte(169))
        btnPo.DisabledState.ForeColor = Color.FromArgb(CByte(141), CByte(141), CByte(141))
        btnPo.FillColor = Color.Transparent
        btnPo.FocusedColor = SystemColors.Highlight
        btnPo.Font = New Font("Segoe UI", 9F)
        btnPo.ForeColor = Color.White
        btnPo.Image = CType(resources.GetObject("btnPo.Image"), Image)
        btnPo.ImageAlign = HorizontalAlignment.Left
        btnPo.ImageSize = New Size(50, 50)
        btnPo.Location = New Point(3, 254)
        btnPo.Name = "btnPo"
        btnPo.ShadowDecoration.CustomizableEdges = CustomizableEdges4
        btnPo.Size = New Size(121, 71)
        btnPo.TabIndex = 6
        btnPo.Text = "PR/PO"
        btnPo.TextAlign = HorizontalAlignment.Right
        btnPo.TextOffset = New Point(-3, 0)
        ' 
        ' btnOrder
        ' 
        btnOrder.CustomizableEdges = CustomizableEdges5
        btnOrder.DisabledState.BorderColor = Color.DarkGray
        btnOrder.DisabledState.CustomBorderColor = Color.DarkGray
        btnOrder.DisabledState.FillColor = Color.FromArgb(CByte(169), CByte(169), CByte(169))
        btnOrder.DisabledState.ForeColor = Color.FromArgb(CByte(141), CByte(141), CByte(141))
        btnOrder.FillColor = Color.Transparent
        btnOrder.FocusedColor = SystemColors.Highlight
        btnOrder.Font = New Font("Segoe UI", 9F)
        btnOrder.ForeColor = Color.White
        btnOrder.Image = My.Resources.Resources.Document1
        btnOrder.ImageAlign = HorizontalAlignment.Left
        btnOrder.ImageSize = New Size(50, 50)
        btnOrder.Location = New Point(3, 175)
        btnOrder.Name = "btnOrder"
        btnOrder.ShadowDecoration.CustomizableEdges = CustomizableEdges6
        btnOrder.Size = New Size(121, 73)
        btnOrder.TabIndex = 5
        btnOrder.Text = "ORDER"
        btnOrder.TextAlign = HorizontalAlignment.Right
        ' 
        ' btnSTORE
        ' 
        btnSTORE.CustomizableEdges = CustomizableEdges7
        btnSTORE.DisabledState.BorderColor = Color.DarkGray
        btnSTORE.DisabledState.CustomBorderColor = Color.DarkGray
        btnSTORE.DisabledState.FillColor = Color.FromArgb(CByte(169), CByte(169), CByte(169))
        btnSTORE.DisabledState.ForeColor = Color.FromArgb(CByte(141), CByte(141), CByte(141))
        btnSTORE.FillColor = Color.Transparent
        btnSTORE.FocusedColor = SystemColors.Highlight
        btnSTORE.Font = New Font("Segoe UI", 9F)
        btnSTORE.ForeColor = Color.White
        btnSTORE.Image = My.Resources.Resources.Warehouse
        btnSTORE.ImageAlign = HorizontalAlignment.Left
        btnSTORE.ImageSize = New Size(50, 50)
        btnSTORE.Location = New Point(3, 99)
        btnSTORE.Name = "btnSTORE"
        btnSTORE.ShadowDecoration.CustomizableEdges = CustomizableEdges8
        btnSTORE.Size = New Size(121, 70)
        btnSTORE.TabIndex = 4
        btnSTORE.Text = "คลังสินค้า"
        btnSTORE.TextAlign = HorizontalAlignment.Right
        ' 
        ' Guna2PictureBox2
        ' 
        Guna2PictureBox2.CustomizableEdges = CustomizableEdges9
        Guna2PictureBox2.FillColor = Color.Transparent
        Guna2PictureBox2.Image = My.Resources.Resources.logo_1
        Guna2PictureBox2.ImageRotate = 0F
        Guna2PictureBox2.Location = New Point(26, 26)
        Guna2PictureBox2.Name = "Guna2PictureBox2"
        Guna2PictureBox2.ShadowDecoration.CustomizableEdges = CustomizableEdges10
        Guna2PictureBox2.Size = New Size(75, 50)
        Guna2PictureBox2.TabIndex = 3
        Guna2PictureBox2.TabStop = False
        ' 
        ' Job
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = Color.White
        ClientSize = New Size(1083, 748)
        Controls.Add(Panel6)
        Controls.Add(Panel2)
        Name = "Job"
        Text = "Job"
        Panel6.ResumeLayout(False)
        Panel6.PerformLayout()
        CType(Guna2PictureBox1, ComponentModel.ISupportInitialize).EndInit()
        Panel2.ResumeLayout(False)
        CType(Guna2PictureBox2, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
    End Sub
    Friend WithEvents Panel6 As Panel
    Friend WithEvents Guna2PictureBox1 As Guna.UI2.WinForms.Guna2PictureBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Panel2 As Panel
    Friend WithEvents btnPo As Guna.UI2.WinForms.Guna2Button
    Friend WithEvents btnOrder As Guna.UI2.WinForms.Guna2Button
    Friend WithEvents btnSTORE As Guna.UI2.WinForms.Guna2Button
    Friend WithEvents Guna2PictureBox2 As Guna.UI2.WinForms.Guna2PictureBox
End Class
