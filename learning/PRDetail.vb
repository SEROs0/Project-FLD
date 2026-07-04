Public Class PRDetail
    Public SelectedPR As String

    Private Sub PRDetail_load(sender As Object, e As EventArgs) Handles MyBase.Load
        PRCode.Text = SelectedPR.ToString()
    End Sub
    Private Sub Guna2PictureBox1_Click(sender As Object, e As EventArgs) Handles Guna2PictureBox1.Click

    End Sub

    Private Sub OrderCode_Click(sender As Object, e As EventArgs) Handles OrderCode.Click

    End Sub

    Private Sub PRCode_Click(sender As Object, e As EventArgs) Handles PRCode.Click

    End Sub

    Private Sub statusPR_Click(sender As Object, e As EventArgs) Handles statusPR.Click

    End Sub

    Private Sub datePR_Click(sender As Object, e As EventArgs) Handles datePR.Click

    End Sub

    Private Sub JobCode_Click(sender As Object, e As EventArgs) Handles JobCode.Click

    End Sub

    Private Sub dataproduct_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dataproduct.CellContentClick

    End Sub

    Private Sub comment_TextChanged(sender As Object, e As EventArgs) Handles comment.TextChanged

    End Sub

    Private Sub btnREJECTED_Click(sender As Object, e As EventArgs) Handles btnREJECTED.Click

    End Sub

    Private Sub btnAPPROVED_Click(sender As Object, e As EventArgs) Handles btnAPPROVED.Click

    End Sub

    Private Sub btnClose2_Click(sender As Object, e As EventArgs) Handles btnClose2.Click
        MessageBox.Show("PR Detail : " & SelectedPR)
    End Sub

    Private Sub btnclose_Click(sender As Object, e As EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub
End Class