Imports System.Data.SqlClient

Public Class PO
    Public SelectedOrderCode As String
    Private _isLoading As Boolean = False

    Private Sub po_load() Handles MyBase.Load
        InitDropdown()
        LoadSummaryCards()
        LoadPRTable()
    End Sub

    Private Sub InitDropdown()
        _isLoading = True
        drpPR.Items.Clear()
        drpPR.Items.Add("เลือกสถานะ")
        drpPR.Items.Add("ทั้งหมด")
        drpPR.Items.Add("PENDING")
        drpPR.Items.Add("APPROVED")
        drpPR.Items.Add("REJECTED")
        drpPR.SelectedIndex = 0
        _isLoading = False
    End Sub

    Private Sub LoadSummaryCards()
        Dim connString As String = "Server=localhost\SQLEXPRESS; DATABASE=PracticeDB; Trusted_Connection=True; TrustServerCertificate=True;"
        Dim conn As New SqlConnection(connString)

        Try
            conn.Open()

            ' PR ทั้งหมด
            Dim cmd1 As New SqlCommand("SELECT COUNT(*) FROM purchase_requests", conn)
            lblPR.Text = cmd1.ExecuteScalar().ToString()

            ' รออนุมัติ
            Dim cmd2 As New SqlCommand("SELECT COUNT(*) FROM purchase_requests WHERE pr_status = 'PENDING'", conn)
            lblwait.Text = cmd2.ExecuteScalar().ToString()

            ' อนุมัติแล้ว
            Dim cmd3 As New SqlCommand("SELECT COUNT(*) FROM purchase_requests WHERE pr_status = 'APPROVED'", conn)
            lblapprov.Text = cmd3.ExecuteScalar().ToString()

            conn.Close()
        Catch ex As Exception
            MessageBox.Show("ERROR (Load Summary): " & ex.Message)
        End Try
    End Sub

    Private Sub LoadPRTable(Optional statusFilter As String = "")
        Dim connString As String = "Server=localhost\SQLEXPRESS; DATABASE=PracticeDB; Trusted_Connection=True; TrustServerCertificate=True;"
        Dim conn As New SqlConnection(connString)

        Try
            conn.Open()

            Dim sql As String = "
                SELECT 
                    pr.pr_code          AS รหัสPR,
                    pr.pr_id            AS ไอดีPR,
                    p.product_name      AS สินค้า,
                    o.order_code        AS รหัสOrder,
                    pr.qty_needed       AS จำนวน,
                    pr.created_date     AS วันที่,
                    pr.pr_status        AS สถานะ,
                    pr.note             AS หมายเหตุ,
                    pr.pr_id            AS pr_id_hidden
                FROM purchase_requests pr
                JOIN orders o   ON o.order_id   = pr.order_id
                JOIN products p ON p.product_id = pr.product_id"

            Dim cmd As New SqlCommand()
            cmd.Connection = conn

            If statusFilter = "" OrElse statusFilter = "ทั้งหมด" Then
                cmd.CommandText = sql & " ORDER BY pr.pr_id DESC"
            Else
                cmd.CommandText = sql & " WHERE pr.pr_status = @status ORDER BY pr.pr_id DESC"
                cmd.Parameters.AddWithValue("@status", statusFilter)
            End If

            Dim adapter As New SqlDataAdapter(cmd)
            Dim dt As New DataTable()
            adapter.Fill(dt)

            PRtable.DataSource = dt

            HideInternalColumns()
            AddActionButtons()

            conn.Close()

        Catch ex As Exception
            MessageBox.Show("ERROR (Load PR Table): " & ex.Message)
        End Try
    End Sub

    Private Sub HideInternalColumns()
        Dim hideCols() As String = {"หมายเหตุ", "pr_id_hidden", "ไอดีPR"}
        For Each col As String In hideCols
            If PRtable.Columns.Contains(col) Then
                PRtable.Columns(col).Visible = False
            End If
        Next
    End Sub

    Private Sub AddActionButtons()
        If PRtable.Columns.Contains("btnView") Then PRtable.Columns.Remove("btnView")

        Dim viewBtn As New DataGridViewButtonColumn()
        viewBtn.Name = "btnView"
        viewBtn.HeaderText = "ดำเนินการ"
        viewBtn.Text = "ดู"
        viewBtn.UseColumnTextForButtonValue = True
        viewBtn.Width = 50
        PRtable.Columns.Add(viewBtn)

    End Sub

    Private Sub PRtable_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles PRtable.CellContentClick
        If e.RowIndex < 0 Then Exit Sub

        Dim colName As String = PRtable.Columns(e.ColumnIndex).Name
        Dim row As DataGridViewRow = PRtable.Rows(e.RowIndex)
        Dim prCode As String = row.Cells("รหัสPR").Value?.ToString()
        Dim prId As Integer = Convert.ToInt32(row.Cells("pr_id_hidden").Value)
        Dim status As String = row.Cells("สถานะ").Value?.ToString()

        If colName = "btnView" Then
            Dim detail As New PRDetail()
            detail.SelectedPR = prCode   ' ← ส่ง prCode ไป
            detail.ShowDialog()
        End If


    End Sub

    Private Sub UpdatePRStatus(prId As Integer, newStatus As String)
        Dim msg As String = If(newStatus = "APPROVED", "อนุมัติ", "ปฏิเสธ")
        Dim confirm As DialogResult = MessageBox.Show(
            "ยืนยันการ" & msg & " PR นี้?",
            "ยืนยัน",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question)

        If confirm = DialogResult.No Then Exit Sub

        Dim connString As String = "Server=localhost\SQLEXPRESS; DATABASE=PracticeDB; Trusted_Connection=True; TrustServerCertificate=True;"
        Dim conn As New SqlConnection(connString)

        Try
            conn.Open()

            Dim cmd As New SqlCommand(
                "UPDATE purchase_requests SET pr_status = @status WHERE pr_id = @prId", conn)
            cmd.Parameters.AddWithValue("@status", newStatus)
            cmd.Parameters.AddWithValue("@prId", prId)
            cmd.ExecuteNonQuery()

            conn.Close()

            MessageBox.Show(msg & " PR เรียบร้อยแล้ว")
            LoadSummaryCards()
            LoadPRTable()

        Catch ex As Exception
            MessageBox.Show("ERROR (Update PR): " & ex.Message)
        End Try
    End Sub

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        If drpPR.SelectedIndex <= 0 Then
            MessageBox.Show("กรุณาเลือกสถานะที่ต้องการค้นหาก่อนครับ")
            Exit Sub
        End If
        LoadPRTable(drpPR.Text)
    End Sub

    Private Sub btnClearSearch_Click(sender As Object, e As EventArgs) Handles btnClearSearch.Click
        _isLoading = True
        drpPR.SelectedIndex = 0
        _isLoading = False
        LoadPRTable()
    End Sub

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
        Dim Jb As New Job()
        Jb.Show()
        Me.Hide()
    End Sub

End Class