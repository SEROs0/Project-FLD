Imports System.Data.SqlClient

Public Class PRDetail

    Public SelectedPR As String
    Public SelectDatePR As String
    Public SelectOrderCode As String
    Public SelectStatus As String
    Public SelectNote As String
    Public SelectCustName As String
    Public SelectJobCode As String

    Private Sub PRDetail_load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim connString As String = "Server=localhost\SQLEXPRESS; DATABASE=PracticeDB; Trusted_Connection=True; TrustServerCertificate=True;"
        Dim conn As New SqlConnection(connString)

        Try
            conn.Open()

            Dim sql1 As String = "Select p.product_name as สินค้า,pr.qty_needed as จำนวนสินค้าที่ต้องการเพิ่ม
                                    from purchase_requests pr
                                    join products p on p.product_id = pr.product_id
                                    join orders o on o.order_id = pr.order_id
                                    where pr.pr_code = @prCode"
            Dim cmd As New SqlCommand(sql1, conn)
            cmd.Parameters.AddWithValue("@PrCode", SelectedPR)

            Dim adapter As New SqlDataAdapter(cmd)
            Dim dt As New DataTable()
            adapter.Fill(dt)

            dataproduct.DataSource = dt

            conn.Close()
        Catch ex As Exception

        End Try

        PRCode.Text = SelectedPR.ToString()
        statusPR.Text = SelectStatus.ToString()
        OrderCode.Text = SelectOrderCode.ToString()
        CustomerName.Text = SelectCustName.ToString()
        datePR.Text = SelectDatePR.ToString()
        comment.Text = SelectNote.ToString()
        JobCode.Text = SelectJobCode.ToString()

        CheckButtonVisibility()

    End Sub

    Private Sub CheckButtonVisibility()
        If SelectStatus.ToUpper() = "PENDING" Then
            btnAPPROVED.Visible = True
            btnREJECTED.Visible = True
        Else
            btnAPPROVED.Visible = False
            btnREJECTED.Visible = False
        End If
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
        Dim confirm As DialogResult = MessageBox.Show(
        "ยืนยันการปฏิเสธ PR นี้?",
        "ยืนยัน",
        MessageBoxButtons.YesNo,
        MessageBoxIcon.Warning)

        If confirm = DialogResult.No Then Exit Sub

        Dim connString As String = "Server=localhost\SQLEXPRESS; DATABASE=PracticeDB; Trusted_Connection=True; TrustServerCertificate=True;"
        Dim conn As New SqlConnection(connString)

        Try
            conn.Open()

            Dim cmd As New SqlCommand(
            "UPDATE purchase_requests SET pr_status = 'REJECTED' WHERE pr_code = @PrCode", conn)
            cmd.Parameters.AddWithValue("@PrCode", SelectedPR)
            cmd.ExecuteNonQuery()



            conn.Close()

            MessageBox.Show("ปฏิเสธ PR เรียบร้อยแล้ว")

            SelectStatus = "REJECTED"
            statusPR.Text = "REJECTED"
            CheckButtonVisibility()

        Catch ex As Exception
            MessageBox.Show("ERROR: " & ex.Message)
        End Try
    End Sub

    Private Sub btnAPPROVED_Click(sender As Object, e As EventArgs) Handles btnAPPROVED.Click
        Dim confirm As DialogResult = MessageBox.Show(
        "ยืนยันการอนุมัติ PR นี้?",
        "ยืนยัน",
        MessageBoxButtons.YesNo,
        MessageBoxIcon.Question)

        If confirm = DialogResult.No Then Exit Sub

        Dim connString As String = "Server=localhost\SQLEXPRESS; DATABASE=PracticeDB; Trusted_Connection=True; TrustServerCertificate=True;"
        Dim conn As New SqlConnection(connString)

        Try
            conn.Open()

            Dim cmd As New SqlCommand(
            "UPDATE purchase_requests SET pr_status = 'APPROVED' WHERE pr_code = @PrCode", conn)
            cmd.Parameters.AddWithValue("@PrCode", SelectedPR)
            cmd.ExecuteNonQuery()

            conn.Close()

            MessageBox.Show("อนุมัติ PR เรียบร้อยแล้ว")

            SelectStatus = "APPROVED"
            statusPR.Text = "APPROVED"
            CheckButtonVisibility()

        Catch ex As Exception
            MessageBox.Show("ERROR: " & ex.Message)
        End Try
    End Sub

    Private Sub btnClose2_Click(sender As Object, e As EventArgs) Handles btnClose2.Click
        Me.Close()
    End Sub

    Private Sub btnclose_Click(sender As Object, e As EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub
End Class