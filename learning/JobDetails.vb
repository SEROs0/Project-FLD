Imports System.Data.SqlClient

Public Class JobDetails

    Public SelectedProductId As Integer
    Public SelectedOrderCode As String

    Private Sub JobDetails_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadJobDetail()
        LoadProductGrid()
        ShowWarningBanner()
        CheckPRExists()

        btncancelPR.Visible = False
    End Sub

    Private Sub LoadProductGrid()
        Dim connString As String = "Server=localhost\SQLEXPRESS; DATABASE=PracticeDB; Trusted_Connection=True; TrustServerCertificate=True;"
        Dim conn As New SqlConnection(connString)

        Try
            conn.Open()

            Dim sql As String = "
            SELECT 
                p.product_name                     AS สินค้า,
                i.qty                              AS สั่ง,
                p.stock_quantity                   AS สต๊อก,
                (i.qty - p.stock_quantity)         AS ขาด,
                (i.qty - p.stock_quantity)         AS ขอเพิ่ม,
                j.job_id                           AS job_id,
                p.product_id                       AS product_id,
                o.order_code                       AS order_code,
                c.cust_name                        AS cust_name,
                o.order_date                       AS order_date,
                o.order_id                         AS order_id
            FROM jobs j
            JOIN orders o  ON o.order_id  = j.order_id
            JOIN order_items i ON i.order_id = o.order_id
            JOIN products p ON p.product_id = i.product_id
            JOIN customers c ON o.cust_id = c.cust_id
            WHERE o.order_code = @orderCode
            AND p.stock_quantity < i.qty"

            Dim cmd As New SqlCommand(sql, conn)
            cmd.Parameters.AddWithValue("@orderCode", SelectedOrderCode)

            Dim adapter As New SqlDataAdapter(cmd)
            Dim dt As New DataTable()
            adapter.Fill(dt)

            Guna2DataGridView1.DataSource = dt

            HideInternalColumns()
            StyleProductGrid()

            conn.Close()

        Catch ex As Exception
            MessageBox.Show("ERROR (Load Product Grid): " & ex.Message)
        End Try
    End Sub

    Private Sub LoadJobDetail()
        Dim connString As String = "Server=localhost\SQLEXPRESS; DATABASE=PracticeDB; Trusted_Connection=True; TrustServerCertificate=True;"
        Dim conn As New SqlConnection(connString)

        Try
            conn.Open()

            Dim sql As String = "
            SELECT 
                o.order_code,
                o.order_date,
                c.cust_name,
                p.product_name,
                i.qty          AS order_qty,
                p.stock_quantity,
                (i.qty - p.stock_quantity) AS qty_short
            FROM orders o
            JOIN customers c   ON c.cust_id    = o.cust_id
            JOIN order_items i ON i.order_id   = o.order_id
            JOIN products p    ON p.product_id = i.product_id
            WHERE o.order_code  = @orderCode
            AND   p.product_id  = @productId
            AND   p.stock_quantity < i.qty"

            Dim cmd As New SqlCommand(sql, conn)
            cmd.Parameters.AddWithValue("@orderCode", SelectedOrderCode)
            cmd.Parameters.AddWithValue("@productId", SelectedProductId)

            Dim reader As SqlDataReader = cmd.ExecuteReader()

            If reader.Read() Then
                orderCode.Text = reader("order_code").ToString()
                NameCust.Text = reader("cust_name").ToString()
                DateOrder.Text = Convert.ToDateTime(reader("order_date")).ToString("dd/MM/yyyy")
            End If

            reader.Close()
            conn.Close()

        Catch ex As Exception
            MessageBox.Show("ERROR: " & ex.Message)
        End Try
    End Sub


    Private Sub HideInternalColumns()
        Dim hideCols() As String = {"job_id", "product_id", "order_code", "cust_name", "order_date", "order_id"}
        For Each col As String In hideCols
            If Guna2DataGridView1.Columns.Contains(col) Then
                Guna2DataGridView1.Columns(col).Visible = False
            End If
        Next
    End Sub
    Private Sub StyleProductGrid()
        Guna2DataGridView1.AllowUserToAddRows = False

        ' ปิดการกด Header
        Guna2DataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        For Each col As DataGridViewColumn In Guna2DataGridView1.Columns
            col.SortMode = DataGridViewColumnSortMode.NotSortable
        Next

        ' จัดสี Column สต๊อก → แดง
        If Guna2DataGridView1.Columns.Contains("สต๊อก") Then
            Guna2DataGridView1.Columns("สต๊อก").DefaultCellStyle.ForeColor = Color.FromArgb(180, 30, 30)
            Guna2DataGridView1.Columns("สต๊อก").DefaultCellStyle.Font = New Font("Segoe UI", 9, FontStyle.Bold)
        End If

        ' จัดสี Column ขาด → แดง
        If Guna2DataGridView1.Columns.Contains("ขาด") Then
            Guna2DataGridView1.Columns("ขาด").DefaultCellStyle.ForeColor = Color.FromArgb(180, 30, 30)
            Guna2DataGridView1.Columns("ขาด").DefaultCellStyle.Font = New Font("Segoe UI", 9, FontStyle.Bold)
        End If

        ' Column ขอเพิ่ม → แก้ได้
        If Guna2DataGridView1.Columns.Contains("ขอเพิ่ม") Then
            Guna2DataGridView1.Columns("ขอเพิ่ม").ReadOnly = False
            Guna2DataGridView1.Columns("ขอเพิ่ม").DefaultCellStyle.BackColor = Color.FromArgb(240, 248, 255)
            Guna2DataGridView1.Columns("ขอเพิ่ม").DefaultCellStyle.Font = New Font("Segoe UI", 9, FontStyle.Bold)
        End If

        Guna2DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
    End Sub

    Private Sub ShowWarningBanner()
        panelWarning.BackColor = Color.FromArgb(255, 243, 205)
        panelWarning.Visible = True
        lblWarning.Text = "⚠ สต๊อกไม่เพียงพอสำหรับ Order นี้ กรอกจำนวนที่ต้องการผลิต/สั่งซื้อเพิ่มเพื่อส่งให้ฝ่าย PR ดำเนินการ"
        lblWarning.ForeColor = Color.FromArgb(130, 80, 0)

    End Sub

    Private Sub DateOrd_Click(sender As Object, e As EventArgs) Handles DateOrder.Click

    End Sub

    Private Sub btncancel_Click(sender As Object, e As EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub orderCode_Click(sender As Object, e As EventArgs) Handles orderCode.Click

    End Sub

    Private Sub Guna2DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles Guna2DataGridView1.CellContentClick

    End Sub

    Private Sub btnPR_Click(sender As Object, e As EventArgs) Handles btnPR.Click

        If String.IsNullOrWhiteSpace(Guna2TextBox1.Text) Then
            MessageBox.Show("กรุณากรอกหมายเหตุสำหรับฝ่าย PR ก่อนครับ")
            Exit Sub
        End If

        Dim confirm As DialogResult = MessageBox.Show(
        "ยืนยันการส่ง PR ให้ฝ่ายจัดซื้อ?",
        "ยืนยัน",
        MessageBoxButtons.YesNo,
        MessageBoxIcon.Question)

        If confirm = DialogResult.No Then Exit Sub

        Dim connString As String = "Server=localhost\SQLEXPRESS; DATABASE=PracticeDB; Trusted_Connection=True; TrustServerCertificate=True;"
        Dim conn As New SqlConnection(connString)
        Dim trans As SqlTransaction = Nothing

        Try
            conn.Open()
            trans = conn.BeginTransaction()

            For Each row As DataGridViewRow In Guna2DataGridView1.Rows
                If row.IsNewRow Then Continue For

                Dim jobId As Integer = Convert.ToInt32(row.Cells("job_id").Value)
                Dim productId As Integer = Convert.ToInt32(row.Cells("product_id").Value)
                Dim orderId As Integer = Convert.ToInt32(row.Cells("order_id").Value)

                Dim qtyNeeded As Integer = 0
                Integer.TryParse(row.Cells("ขอเพิ่ม").Value?.ToString(), qtyNeeded)
                If qtyNeeded <= 0 Then Continue For

                Dim cmdNextId As New SqlCommand(
                "SELECT ISNULL(MAX(pr_id), 0) + 1 FROM purchase_requests",
                conn, trans)
                Dim nextId As Integer = cmdNextId.ExecuteScalar()
                Dim prCode As String = "PR-" & nextId.ToString("000")

                Dim sqlPR As String = "INSERT INTO purchase_requests 
                                   (pr_code,order_id, job_id, product_id, qty_needed, note, pr_status, created_date)
                                   VALUES (@prCode,@orderId, @jobId, @productId, @qtyNeeded, @note, 'PENDING', GETDATE())"

                Dim cmdPR As New SqlCommand(sqlPR, conn, trans)
                cmdPR.Parameters.AddWithValue("@prCode", prCode)
                cmdPR.Parameters.AddWithValue("@orderId", orderId)
                cmdPR.Parameters.AddWithValue("@jobId", jobId)
                cmdPR.Parameters.AddWithValue("@productId", productId)
                cmdPR.Parameters.AddWithValue("@qtyNeeded", qtyNeeded)
                cmdPR.Parameters.AddWithValue("@note", Guna2TextBox1.Text)
                cmdPR.ExecuteNonQuery()

                Dim sqlItem As String = "UPDATE order_items 
                                     SET item_status = 'WAITING'
                                     WHERE order_id = @orderId 
                                     AND product_id = @productId"

                Dim cmdItem As New SqlCommand(sqlItem, conn, trans)
                cmdItem.Parameters.AddWithValue("@orderId", orderId)
                cmdItem.Parameters.AddWithValue("@productId", productId)
                cmdItem.ExecuteNonQuery()

            Next

            trans.Commit()
            conn.Close()

            MessageBox.Show("ส่ง PR เรียบร้อยแล้ว!")

            btnPR.Visible = False

            Me.Close()

        Catch ex As Exception
            If trans IsNot Nothing Then trans.Rollback()
            MessageBox.Show("ERROR (Send PR): " & ex.Message)
        End Try

    End Sub

    Private Sub CheckPRExists()
        Dim connString As String = "Server=localhost\SQLEXPRESS; DATABASE=PracticeDB; Trusted_Connection=True; TrustServerCertificate=True;"
        Dim conn As New SqlConnection(connString)

        Try
            conn.Open()

            ' เช็คว่ามี PR สำหรับ order นี้อยู่แล้วไหม
            Dim cmd As New SqlCommand(
            "SELECT COUNT(*) FROM purchase_requests pr
             JOIN orders o ON o.order_id = pr.order_id
             WHERE o.order_code = @orderCode", conn)
            cmd.Parameters.AddWithValue("@orderCode", SelectedOrderCode)

            Dim existing As Integer = cmd.ExecuteScalar()

            If existing > 0 Then
                btnPR.Visible = False
                btncancelPR.Visible = False
            Else
                btnPR.Visible = True
                btncancelPR.Visible = True
            End If

            conn.Close()

        Catch ex As Exception
            MessageBox.Show("ERROR (Check PR): " & ex.Message)
        End Try
    End Sub

    Private Sub btncancelPR_Click(sender As Object, e As EventArgs) Handles btncancelPR.Click

    End Sub

    Private Sub Guna2TextBox1_TextChanged(sender As Object, e As EventArgs) Handles Guna2TextBox1.TextChanged

    End Sub
End Class