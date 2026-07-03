Imports System.Data.SqlClient

Public Class OrderDetail

    Public SelectedOrderId As String

    Private Sub orderDetail_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If String.IsNullOrEmpty(SelectedOrderId) Then
            MessageBox.Show("ไม่พบรหัส Order")
            Me.Close()
            Exit Sub
        End If

        LoadOrderInfo(SelectedOrderId)
        LoadOrderItems(SelectedOrderId)
        StyleDataGridView()
    End Sub

    Private Sub LoadOrderInfo(orderId As String)
        Dim connString = "Server=localhost\SQLEXPRESS;DATABASE=PracticeDB;Trusted_Connection=True;TrustServerCertificate=True"
        Dim conn As New SqlConnection(connString)

        Try
            conn.Open()

            Dim sql As String = "SELECT o.order_code, o.order_date, o.amount, o.status, c.cust_name
                                 FROM orders o
                                 JOIN customers c ON c.cust_id = o.cust_id
                                 WHERE o.order_code = @orderId"

            Dim cmd As New SqlCommand(sql, conn)
            cmd.Parameters.AddWithValue("@orderId", orderId)

            Dim reader As SqlDataReader = cmd.ExecuteReader()

            If reader.Read() Then
                lblOrderID.Text = reader("order_code").ToString()
                lblCustomer.Text = reader("cust_name").ToString()
                lblDateTime.Text = Convert.ToDateTime(reader("order_date")).ToString("dd/MM/yyyy")
                lblStatus.Text = reader("status").ToString()
                lblTotal.Text = "฿ " & Convert.ToDecimal(reader("amount")).ToString("N2")

                Dim status As String = reader("status").ToString().ToUpper()

                If status = "PENDING" Then
                    btnJOB.Visible = True
                    btnCancelOrder.Visible = True
                Else
                    btnJOB.Visible = False
                    btnCancelOrder.Visible = False
                End If
            End If

            reader.Close()
            conn.Close()

        Catch ex As Exception
            MessageBox.Show("ERROR (Load Order Info): " & ex.Message)
        End Try
    End Sub

    Private Sub LoadOrderItems(orderId As String)
        Dim connString = "Server=localhost\SQLEXPRESS;DATABASE=PracticeDB;Trusted_Connection=True;TrustServerCertificate=True"
        Dim conn As New SqlConnection(connString)

        Try
            conn.Open()

            Dim sql As String = "SELECT p.product_name AS สินค้า,
                                        i.qty AS จำนวน,
                                        p.price AS ราคาต่อชิ้น,
                                        (i.qty * p.price) AS รวม
                                 FROM orders o
                                 JOIN order_items i ON o.order_id = i.order_id
                                 JOIN products p ON i.product_id = p.product_id
                                 WHERE o.order_code = @orderId"

            Dim cmd As New SqlCommand(sql, conn)
            cmd.Parameters.AddWithValue("@orderId", orderId)

            Dim adapter As New SqlDataAdapter(cmd)
            Dim dt As New DataTable()
            adapter.Fill(dt)

            DataGridView1.DataSource = dt
            DataGridView1.RowHeadersVisible = False
            DataGridView1.AllowUserToAddRows = False
            DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill

            conn.Close()

        Catch ex As Exception
            MessageBox.Show("ERROR (Load Order Items): " & ex.Message)
        End Try
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    Private Sub StyleDataGridView()
        StyleGrid(DataGridView1)

    End Sub

    Private Sub StyleGrid(grid As DataGridView)
        With grid
            .BackgroundColor = Color.White
            .BorderStyle = BorderStyle.None
            .RowHeadersVisible = False
            .AllowUserToAddRows = False
            .ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(245, 245, 245)
            .ColumnHeadersDefaultCellStyle.Font = New Font("Segoe UI", 9, FontStyle.Bold)
            .ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
            .DefaultCellStyle.Font = New Font("Segoe UI", 9)
            .DefaultCellStyle.SelectionBackColor = Color.FromArgb(220, 235, 252)
            .DefaultCellStyle.SelectionForeColor = Color.Black
            .AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(250, 250, 250)
            .GridColor = Color.FromArgb(230, 230, 230)
            .EnableHeadersVisualStyles = False
            .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            .ColumnHeadersVisible = True
            .ScrollBars = ScrollBars.None
        End With
    End Sub

    Private Sub btnJOB_Click(sender As Object, e As EventArgs) Handles btnJOB.Click

        Dim connString As String = "Server=localhost\SQLEXPRESS;DATABASE=PracticeDB;Trusted_Connection=True;TrustServerCertificate=True"
        Dim conn As New SqlConnection(connString)

        Try
            conn.Open()

            Dim cmdGetId As New SqlCommand(
            "SELECT order_id FROM orders WHERE order_code = @code", conn)
            cmdGetId.Parameters.AddWithValue("@code", SelectedOrderId)
            Dim orderId As Object = cmdGetId.ExecuteScalar()

            If IsDBNull(orderId) OrElse orderId Is Nothing Then
                MessageBox.Show("ไม่พบ Order นี้ในระบบ")
                conn.Close()
                Exit Sub
            End If

            Dim cmdCheck As New SqlCommand(
            "SELECT COUNT(*) FROM jobs WHERE order_id = @order_id", conn)
            cmdCheck.Parameters.AddWithValue("@order_id", orderId)
            Dim existing As Integer = cmdCheck.ExecuteScalar()

            If existing > 0 Then
                MessageBox.Show("Job สำหรับ Order นี้ถูกสร้างไปแล้ว")
                conn.Close()

            End If

            Dim cmdInsert As New SqlCommand(
            "INSERT INTO jobs (order_id, job_status, created_date) 
             VALUES (@order_id, 'CHECKING', GETDATE())", conn)
            cmdInsert.Parameters.AddWithValue("@order_id", orderId)
            cmdInsert.ExecuteNonQuery()

            conn.Close()
            MessageBox.Show("สร้าง Job เรียบร้อยแล้ว")

        Catch ex As Exception
            MessageBox.Show("ERROR: " & ex.Message)
        End Try


    End Sub

    Private Sub btnCancelOrder_Click(sender As Object, e As EventArgs) Handles btnCancelOrder.Click

        Dim result As DialogResult = MessageBox.Show(
        "คุณต้องการยกเลิก Order นี้ใช่หรือไม่?",
        "ยืนยันการยกเลิก",
        MessageBoxButtons.YesNo,
        MessageBoxIcon.Warning)

        If result = DialogResult.No Then Exit Sub

        Dim connString As String = "Server=localhost\SQLEXPRESS;DATABASE=PracticeDB;Trusted_Connection=True;TrustServerCertificate=True"
        Dim conn As New SqlConnection(connString)
        Dim trans As SqlTransaction = Nothing

        Try
            conn.Open()
            trans = conn.BeginTransaction()

            Dim cmdOrder As New SqlCommand(
            "UPDATE orders SET status = 'CANCELLED' WHERE order_code = @orderId",
            conn, trans)
            cmdOrder.Parameters.AddWithValue("@orderId", SelectedOrderId)
            cmdOrder.ExecuteNonQuery()

            Dim cmdItem As New SqlCommand(
            "UPDATE order_items SET item_status = 'CANCELLED' 
             WHERE order_id = (SELECT order_id FROM orders WHERE order_code = @orderId)",
            conn, trans)
            cmdItem.Parameters.AddWithValue("@orderId", SelectedOrderId)
            cmdItem.ExecuteNonQuery()

            Dim cmdJob As New SqlCommand(
                "UPDATE jobs SET job_status = 'CANCELLED'
                WHERE order_id = (SELECT order_id FROM orders WHERE order_code = @orderId)", conn, trans)
            cmdJob.Parameters.AddWithValue("@orderId", SelectedOrderId)
            cmdJob.ExecuteNonQuery()

            trans.Commit()
            conn.Close()

            MessageBox.Show("ยกเลิก Order เรียบร้อยแล้ว")
            Me.Close()

        Catch ex As Exception
            If trans IsNot Nothing Then
                trans.Rollback()
            End If
            MessageBox.Show("ERROR (Cancel Order): " & ex.Message)
        End Try

    End Sub
End Class