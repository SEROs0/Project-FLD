Imports System.Data.SqlClient

Public Class Job
    Public SelectedOrderId As String

    Private _isLoading As Boolean = False

    Private Sub Job_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        InitDropdown()
        LoadJobTable()
    End Sub

    Private Sub InitDropdown()
        _isLoading = True
        btnStatus.Items.Clear()
        btnStatus.Items.Add("เลือกสถานะ")
        btnStatus.Items.Add("ทั้งหมด")
        btnStatus.Items.Add("PENDING")
        btnStatus.Items.Add("DONE")
        btnStatus.Items.Add("CANCELLED")
        btnStatus.Items.Add("WAITING")
        btnStatus.SelectedIndex = 0
        _isLoading = False
    End Sub

    Private Sub LoadJobTable()
        LoadJobTableWithFilter("")
    End Sub

    Private Sub LoadJobTableWithFilter(statusFilter As String)
        Dim connString As String = "Server=localhost\SQLEXPRESS; DATABASE=PracticeDB; Trusted_Connection=True; TrustServerCertificate=True;"
        Dim conn As New SqlConnection(connString)

        Try
            conn.Open()

            Dim baseSql As String = "
                SELECT 
                    o.order_code     AS รหัสใบสั่งซื้อ,
                    c.cust_name      AS ชื่อลูกค้า,
                    p.product_name   AS สินค้า,
                    i.qty            AS จำนวน,
                    p.stock_quantity AS สต๊อก,
                    j.job_status     AS สถานะ,
                    i.item_status    AS สถานะสินค้า,
                    j.job_id         AS job_id,
                    o.order_id       AS order_id,
                    p.product_id     AS product_id,
                    i.qty            AS order_qty,
                    i.item_id        AS item_id
                FROM jobs j
                JOIN orders o  ON o.order_id     = j.order_id
                JOIN order_items i ON i.order_id = o.order_id
                JOIN products p    ON p.product_id = i.product_id
                JOIN customers c   ON c.cust_id  = o.cust_id"

            Dim groupSql As String = "
                GROUP BY o.order_code, c.cust_name, p.product_name,
                         i.qty, p.stock_quantity, j.job_status,
                         i.item_status, j.job_id, o.order_id,
                         p.product_id, i.item_id
                ORDER BY j.job_id DESC"

            Dim cmd As New SqlCommand()
            cmd.Connection = conn

            If String.IsNullOrEmpty(statusFilter) OrElse statusFilter = "ทั้งหมด" Then
                cmd.CommandText = baseSql & groupSql
            Else
                cmd.CommandText = baseSql & " WHERE i.item_status = @status " & groupSql
                cmd.Parameters.AddWithValue("@status", statusFilter)
            End If

            Dim adapter As New SqlDataAdapter(cmd)
            Dim dt As New DataTable()
            adapter.Fill(dt)

            AddStockCheckColumn(dt)

            tableJob.DataSource = dt
            HideInternalColumns()
            AddActionButton()
            StyleJobGrid()

            lbljobs.Text = dt.Rows.Count.ToString()

            Dim countPo As Integer = 0
            Dim countNotPo As Integer = 0
            For Each row As DataRow In dt.Rows
                Dim jobStatus As String = row("สถานะ").ToString()
                If jobStatus <> "DONE" Then
                    Dim stock As Integer = Convert.ToInt32(row("สต๊อก"))
                    Dim qty As Integer = Convert.ToInt32(row("จำนวน"))
                    If stock >= qty Then countPo += 1 Else countNotPo += 1
                End If
            Next
            Guna2HtmlLabel3.Text = countPo.ToString()
            Guna2HtmlLabel5.Text = countNotPo.ToString()

            conn.Close()

        Catch ex As Exception
            MessageBox.Show("ERROR (Load Job): " & ex.Message)
        End Try
    End Sub

    Private Sub AddStockCheckColumn(dt As DataTable)
        If dt.Columns.Contains("ผลตรวจ") Then dt.Columns.Remove("ผลตรวจ")
        dt.Columns.Add(New DataColumn("ผลตรวจ", GetType(String)))

        For Each row As DataRow In dt.Rows
            Dim stock As Integer = Convert.ToInt32(row("สต๊อก"))
            Dim qty As Integer = Convert.ToInt32(row("จำนวน"))
            row("ผลตรวจ") = If(stock >= qty, "พอ", "ไม่พอ")
        Next
    End Sub

    Private Sub HideInternalColumns()
        Dim hideCols() As String = {"job_id", "order_id", "product_id", "order_qty", "item_id", "สถานะ"}
        For Each col As String In hideCols
            If tableJob.Columns.Contains(col) Then
                tableJob.Columns(col).Visible = False
            End If
        Next
    End Sub

    Private Sub AddActionButton()
        If tableJob.Columns.Contains("Action") Then
            tableJob.Columns.Remove("Action")
        End If

        Dim actionBtn As New DataGridViewButtonColumn()
        actionBtn.Name = "Action"
        actionBtn.HeaderText = "ดำเนินการ"
        actionBtn.UseColumnTextForButtonValue = False
        actionBtn.Width = 110
        tableJob.Columns.Add(actionBtn)

        For Each row As DataGridViewRow In tableJob.Rows
            If row.IsNewRow Then Continue For

            Dim itemStatus As String = row.Cells("สถานะสินค้า").Value?.ToString()
            Dim result As String = row.Cells("ผลตรวจ").Value?.ToString()

            If itemStatus = "DONE" OrElse itemStatus = "CANCELLED" Then
                row.Cells("Action") = New DataGridViewTextBoxCell()
                row.Cells("Action").Value = ""
                row.Cells("Action").ReadOnly = True
            ElseIf result = "พอ" Then
                row.Cells("Action").Value = "✓ Complete"
            Else
                row.Cells("Action").Value = "→ ส่ง PR"
            End If
        Next
    End Sub

    Private Sub StyleJobGrid()
        With tableJob
            .RowHeadersVisible = False
            .AllowUserToAddRows = False
            .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            .EnableHeadersVisualStyles = False
            .ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(0, 128, 255)
            .ColumnHeadersDefaultCellStyle.Font = New Font("Segoe UI", 10, FontStyle.Bold)
            .DefaultCellStyle.Font = New Font("Segoe UI", 9)
            .GridColor = Color.FromArgb(230, 230, 230)
            .BorderStyle = BorderStyle.None
            .BackgroundColor = Color.White
        End With
    End Sub

    Private Sub tableJob_RowPostPaint(sender As Object, e As DataGridViewRowPostPaintEventArgs) Handles tableJob.RowPostPaint
        If e.RowIndex < 0 OrElse e.RowIndex >= tableJob.Rows.Count Then Exit Sub
        Dim row As DataGridViewRow = tableJob.Rows(e.RowIndex)
        If row.IsNewRow Then Exit Sub

        Dim result As String = row.Cells("ผลตรวจ").Value?.ToString()
        Select Case result
            Case "พอ"
                row.DefaultCellStyle.ForeColor = Color.FromArgb(39, 80, 10)
                row.DefaultCellStyle.BackColor = Color.FromArgb(234, 243, 222)
            Case "ไม่พอ"
                row.DefaultCellStyle.ForeColor = Color.FromArgb(121, 31, 31)
                row.DefaultCellStyle.BackColor = Color.FromArgb(252, 235, 235)
        End Select
    End Sub

    Private Sub tableJob_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles tableJob.CellContentClick
        If e.RowIndex < 0 Then Exit Sub
        If tableJob.Columns(e.ColumnIndex).Name <> "Action" Then Exit Sub

        Dim row As DataGridViewRow = tableJob.Rows(e.RowIndex)
        Dim itemStatus As String = row.Cells("สถานะสินค้า").Value?.ToString()
        If itemStatus = "DONE" Then Exit Sub

        Dim result As String = row.Cells("ผลตรวจ").Value?.ToString()
        Dim orderCode As String = row.Cells("รหัสใบสั่งซื้อ").Value?.ToString()
        Dim jobId As Integer = Convert.ToInt32(row.Cells("job_id").Value)
        Dim orderId As Integer = Convert.ToInt32(row.Cells("order_id").Value)
        Dim productId As Integer = Convert.ToInt32(row.Cells("product_id").Value)
        Dim orderQty As Integer = Convert.ToInt32(row.Cells("order_qty").Value)
        Dim itemId As Integer = Convert.ToInt32(row.Cells("item_id").Value)

        If result = "พอ" Then
            CompleteOrder(jobId, orderId, productId, orderQty, itemId)
        Else
            GoToJobD(productId, orderCode)

        End If
    End Sub

    Private Sub tableJob_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles tableJob.CellFormatting
        If e.RowIndex < 0 Then Exit Sub
        If tableJob.Columns(e.ColumnIndex).Name <> "Action" Then Exit Sub
        Dim row As DataGridViewRow = tableJob.Rows(e.RowIndex)
        If row.IsNewRow Then Exit Sub

        Dim itemStatus As String = row.Cells("สถานะสินค้า").Value?.ToString()
        Dim result As String = row.Cells("ผลตรวจ").Value?.ToString()

        If itemStatus = "DONE" OrElse itemStatus = "CANCELLED" Then
            Dim bg = If(row.DefaultCellStyle.BackColor = Color.Empty,
                        Color.FromArgb(234, 243, 222),
                        row.DefaultCellStyle.BackColor)
            e.CellStyle.BackColor = bg
            e.CellStyle.ForeColor = Color.Transparent
            e.CellStyle.SelectionBackColor = bg
            e.CellStyle.SelectionForeColor = Color.Transparent
        ElseIf result = "พอ" Then
            e.CellStyle.BackColor = Color.FromArgb(39, 120, 10)
            e.CellStyle.ForeColor = Color.White
            e.CellStyle.SelectionBackColor = Color.FromArgb(39, 120, 10)
            e.CellStyle.SelectionForeColor = Color.White
        Else
            e.CellStyle.BackColor = Color.FromArgb(180, 80, 10)
            e.CellStyle.ForeColor = Color.White
            e.CellStyle.SelectionBackColor = Color.FromArgb(180, 80, 10)
            e.CellStyle.SelectionForeColor = Color.White
        End If
    End Sub

    Private Sub CompleteOrder(jobId As Integer, orderId As Integer, productId As Integer, qty As Integer, itemId As Integer)
        Dim confirm As DialogResult = MessageBox.Show(
            "ยืนยันการดำเนินการ?" & vbCrLf & "สต๊อกสินค้านี้จะถูกหักออก",
            "ยืนยัน", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If confirm = DialogResult.No Then Exit Sub

        Dim connString As String = "Server=localhost\SQLEXPRESS; DATABASE=PracticeDB; Trusted_Connection=True; TrustServerCertificate=True;"
        Dim conn As New SqlConnection(connString)

        Try
            conn.Open()
            Dim trans As SqlTransaction = conn.BeginTransaction()
            Try
                Dim cmdItem As New SqlCommand(
                    "UPDATE order_items SET item_status = 'DONE' WHERE item_id = @item_id",
                    conn, trans)
                cmdItem.Parameters.AddWithValue("@item_id", itemId)
                cmdItem.ExecuteNonQuery()

                Dim cmdStock As New SqlCommand(
                    "UPDATE products SET stock_quantity = stock_quantity - @qty WHERE product_id = @product_id",
                    conn, trans)
                cmdStock.Parameters.AddWithValue("@qty", qty)
                cmdStock.Parameters.AddWithValue("@product_id", productId)
                cmdStock.ExecuteNonQuery()

                Dim cmdCheck As New SqlCommand(
                    "SELECT COUNT(*) FROM order_items WHERE order_id = @order_id AND (item_status IS NULL OR item_status != 'DONE')",
                    conn, trans)
                cmdCheck.Parameters.AddWithValue("@order_id", orderId)
                Dim remaining As Integer = cmdCheck.ExecuteScalar()

                If remaining = 0 Then
                    Dim cmdJob As New SqlCommand(
                        "UPDATE jobs SET job_status = 'DONE' WHERE job_id = @job_id",
                        conn, trans)
                    cmdJob.Parameters.AddWithValue("@job_id", jobId)
                    cmdJob.ExecuteNonQuery()

                    Dim cmdOrder As New SqlCommand(
                        "UPDATE orders SET status = 'COMPLETE' WHERE order_id = @order_id",
                        conn, trans)
                    cmdOrder.Parameters.AddWithValue("@order_id", orderId)
                    cmdOrder.ExecuteNonQuery()

                    MessageBox.Show("สินค้าครบทุกรายการ! Order เปลี่ยนเป็น COMPLETE แล้ว")
                Else
                    MessageBox.Show("บันทึกสำเร็จ! ยังมีสินค้าอีก " & remaining & " รายการ")
                End If

                trans.Commit()
                conn.Close()
                LoadJobTable()

            Catch ex As Exception
                trans.Rollback()
                Throw ex
            End Try

        Catch ex As Exception
            MessageBox.Show("ERROR (Complete Order): " & ex.Message)
        End Try
    End Sub

    Private Sub GoToJobD(productId As Integer, orderCode As String)
        Dim Jobd As New JobDetails()
        Jobd.SelectedProductId = productId
        Jobd.SelectedOrderCode = orderCode
        Jobd.Show()
    End Sub

    Private Sub btnStatus_SelectedIndexChanged(sender As Object, e As EventArgs) Handles btnStatus.SelectedIndexChanged
        If _isLoading Then Exit Sub
    End Sub

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        If btnStatus.SelectedIndex <= 0 Then
            MessageBox.Show("กรุณาเลือกสถานะที่ต้องการค้นหาก่อนครับ")
            Exit Sub
        End If
        LoadJobTableWithFilter(btnStatus.Text)
    End Sub

    Private Sub btnClearSearch_Click(sender As Object, e As EventArgs) Handles btnClearSearch.Click
        _isLoading = True
        btnStatus.SelectedIndex = 0
        _isLoading = False
        LoadJobTable()
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

    Private Sub Guna2Panel3_Paint(sender As Object, e As PaintEventArgs) Handles Guna2Panel3.Paint

    End Sub
End Class