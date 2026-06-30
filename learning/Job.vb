Public Class Job

    Private Sub btnSTORE_Click(sender As Object, e As EventArgs) Handles btnSTORE.Click
        Dim St As New Store()
        St.Show()
        Me.Hide()
    End Sub

    Private Sub btnJob_Click(sender As Object, e As EventArgs) Handles btnOrder.Click
        Dim Ors As New Order()
        Ors.Show()
        Me.Hide()
    End Sub

    Private Sub btnPo_Click(sender As Object, e As EventArgs) Handles btnPo.Click
        Dim PO As New PO()
        PO.Show()
        Me.Hide()
    End Sub
End Class