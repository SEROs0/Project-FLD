Imports System.Data.SqlClient

Public Class Job
    Public SelectedOrderId As String

    Private Sub Job_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim connString As String = "Server=localhost\SQLEXPRESS; DATABASE=PracticeDB; Trusted_Connection=True; TrustServerCertificate=True;"
        Dim conn As New SqlConnection(connString)

        Try
            conn.Open()
            Dim sql As String = "select o.order_code as รหัสใบสั่งซื้อ, c.cust_name as ชื่อลูกค้า, p.product_name as สินค้า, SUM(i.qty) as จำนวน, p.stock_quantity as สต๊อก, j.job_status as สถานะ
                                    from jobs j
                                    join orders o on o.order_id = j.order_id
                                    join order_items i on i.order_id = o.order_id
                                    join products p on p.product_id = i.product_id
                                    join customers c on c.cust_id = o.cust_id
                                    group by o.order_code, c.cust_name, p.product_name, p.stock_quantity, j.job_status
                                    "
            Dim cmd As New SqlCommand(sql, conn)
            Dim adapter As New SqlDataAdapter(cmd)
            Dim dt As New DataTable()
            adapter.Fill(dt)
            tableJob.DataSource = dt

            Dim cmd1 As New SqlCommand("select sum(job_id) from jobs", conn)
            Dim jobsTotal = cmd1.ExecuteScalar()
            lbljobs.Text = If(IsDBNull(jobsTotal), "0", jobsTotal.ToString())



            conn.Close()

        Catch ex As Exception

        End Try
    End Sub

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

    Private Sub tableJob_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles tableJob.CellContentClick

    End Sub
End Class