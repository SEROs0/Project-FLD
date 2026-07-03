Public Class PO
    Public SelectedOrderCode As String

    Private Sub btnSTORE_Click(sender As Object, e As EventArgs) Handles btnSTORE.Click
        Dim St As New Store()
        St.Show()
        Me.Hide()
    End Sub

    Private Sub btnJob_Click(sender As Object, e As EventArgs) Handles btnOrder.Click
        Dim Ord As New Order()
        Ord.Show()
        Me.Hide()
    End Sub

    Private Sub btnPo_Click(sender As Object, e As EventArgs) Handles btnJob.Click
        Dim Ord As New Job()
        Ord.Show()
        Me.Hide()
    End Sub

    Private Sub PO_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class