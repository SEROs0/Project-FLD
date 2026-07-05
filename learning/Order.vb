Imports System.Data.SqlClient
Imports Guna.UI2.WinForms

Public Class Order

    Private Sub Order_load(sender As Object, e As EventArgs) Handles MyBase.Load
        PanelContainer.AutoScroll = True
        Dim connString As String = "Server=localhost\SQLEXPRESS; DATABASE=PracticeDB; Trusted_Connection=True; TrustServerCertificate=True;"
        Dim conn As New SqlConnection(connString)

        Try
            conn.Open()

            ' Dropdown customer

            Dim sql As String = "SELECT cust_id, cust_name FROM customers"
            Dim cmd As New SqlCommand(sql, conn)
            Dim adapter As New SqlDataAdapter(cmd)
            Dim dt As New DataTable()
            adapter.Fill(dt)

            Dim dr As DataRow = dt.NewRow()
            dr("cust_id") = 0
            dr("cust_name") = "เลือกลูกค้า"
            dt.Rows.InsertAt(dr, 0)

            DropdownCustomer.DataSource = dt
            DropdownCustomer.DisplayMember = "cust_name"
            DropdownCustomer.ValueMember = "cust_id"
            DropdownCustomer.SelectedIndex = 0

            ' Dropdown product

            Dim sqlPrd As String = "Select product_id,product_name,price from products "
            Dim cmdprd As New SqlCommand(sqlPrd, conn)
            Dim adapterprd As New SqlDataAdapter(cmdprd)
            Dim dtprd As New DataTable()
            adapterprd.Fill(dtprd)

            Dim prd As DataRow = dtprd.NewRow()
            prd("product_id") = 0
            prd("product_name") = "เลือกสินค้า"
            dtprd.Rows.InsertAt(prd, 0)

            DropdownProduct.DataSource = dtprd
            DropdownProduct.DisplayMember = "product_name"
            DropdownProduct.ValueMember = "product_id"
            DropdownProduct.SelectedIndex = 0

            conn.Close()
        Catch ex As Exception
            MessageBox.Show("ERROR (Load Summary): " & ex.Message)
        End Try

        ' Dropdown status order
        btnStatus.Items.Clear()
        btnStatus.Items.Add("เลือกสถานะ")
        btnStatus.Items.Add("ทั้งหมด")
        btnStatus.Items.Add("COMPLETE")
        btnStatus.Items.Add("PENDING")
        btnStatus.Items.Add("CANCELLED")
        btnStatus.SelectedIndex = 0

        PanelPopup.Visible = False

        LoadSummaryCards()
        LoadAllOrders()
    End Sub


    Private Sub LoadSummaryCards()
        Dim connString As String = "Server=localhost\SQLEXPRESS; DATABASE=PracticeDB; Trusted_Connection=True; TrustServerCertificate=True;"
        Dim conn As New SqlConnection(connString)

        Try
            conn.Open()

            Dim cmd1 As New SqlCommand("select count(order_id) from orders", conn)
            Dim OrdersTotal = cmd1.ExecuteScalar()
            lblStorkValue.Text = If(IsDBNull(OrdersTotal), "0", OrdersTotal.ToString())

            Dim cmd2 As New SqlCommand("select count(status) from orders where status = 'PENDING'", conn)
            Dim StatusPendingTotal = cmd2.ExecuteScalar()
            lblStatusPendingValue.Text = If(IsDBNull(StatusPendingTotal), "0", StatusPendingTotal.ToString())

            Dim cmd3 As New SqlCommand("select count(status) from orders where status = 'COMPLETE'", conn)
            Dim StatusCompleteTotalc = cmd3.ExecuteScalar()
            lblStatusCompleteValue.Text = If(IsDBNull(StatusCompleteTotalc), "0", StatusCompleteTotalc.ToString())

            Dim cmd4 As New SqlCommand("select sum(amount) from orders", conn)
            Dim AmountTotalc = cmd4.ExecuteScalar()
            lblAmountTotalValue.Text = "฿ " & If(IsDBNull(AmountTotalc), "0", AmountTotalc.ToString())


            conn.Close()
        Catch ex As Exception
            MessageBox.Show("ERROR (Load Summary): " & ex.Message)
        End Try


    End Sub

    Private Sub LoadAllOrders()
        Dim connString As String = "Server=localhost\SQLEXPRESS; DATABASE=PracticeDB; Trusted_Connection=True; TrustServerCertificate=True;"
        Dim conn As New SqlConnection(connString)

        Try
            conn.Open()

            Dim sql As String = "select order_code as รหัสใบสั่งซื้อ,order_date as วันที่สั่ง,amount as จำนวน,status as สถานะ from orders order by order_id desc"
            Dim cmd As New SqlCommand(sql, conn)
            Dim adapter As New SqlDataAdapter(cmd)
            Dim dt As New DataTable()
            adapter.Fill(dt)

            dataOrder.DataSource = dt

            AddActionButtons()

            conn.Close()

        Catch ex As Exception
            MessageBox.Show("ERROR (Load All Orders): " & ex.Message)
        End Try
    End Sub

    Private Sub AddActionButtons()

        If dataOrder.Columns.Contains("btnView") Then
            dataOrder.Columns.Remove("btnView")
        End If
        'If DataGridView1.Columns.Contains("btnJob") Then
        'DataGridView1.Columns.Remove("btnJob")
        'End If 

        ' ปุ่ม "ดู"
        Dim viewBtn As New DataGridViewButtonColumn()
        viewBtn.Name = "btnView"
        viewBtn.HeaderText = "ดำเนินการ"
        viewBtn.Text = "ดู Order"
        viewBtn.UseColumnTextForButtonValue = True
        viewBtn.Width = 80
        dataOrder.Columns.Add(viewBtn)

        ' ปุ่ม "Job"
        'Dim jobBtn As New DataGridViewButtonColumn()
        'jobBtn.Name = "btnJob"
        'jobBtn.HeaderText = ""
        'jobBtn.Text = "Job"
        'jobBtn.UseColumnTextForButtonValue = True
        'jobBtn.Width = 80
        'DataGridView1.Columns.Add(jobBtn)
    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dataOrder.CellContentClick
        If e.RowIndex < 0 Then Exit Sub

        Dim columnName = dataOrder.Columns(e.ColumnIndex).Name
        Dim orderId = dataOrder.Rows(e.RowIndex).Cells("รหัสใบสั่งซื้อ").Value.ToString

        If columnName = "btnView" Then
            Dim detail As New OrderDetail()
            detail.SelectedOrderId = orderId
            detail.ShowDialog()

            'ElseIf columnName = "btnJob" Then
            'Dim jb As New Job()
            'jb.SelectedOrderId = orderId
            'jb.Show()
            'jb.Hide() '

        End If
    End Sub


    Private Sub Guna2Button1_Click(sender As Object, e As EventArgs) Handles btnSTORE.Click
        Dim St As New Store()
        St.Show()
        Hide()
    End Sub

    Private Sub Guna2Button2_Click(sender As Object, e As EventArgs) Handles btnJob.Click
        Dim Jb As New Job()
        Jb.Show()
        Me.Hide()
    End Sub

    Private Sub Guna2Button3_Click(sender As Object, e As EventArgs) Handles btnPo.Click
        Dim PO As New PR()
        PO.Show()
        Hide()
    End Sub

    Private Sub Guna2ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles btnStatus.SelectedIndexChanged

    End Sub

    Private Sub Guna2GradientButton1_Click(sender As Object, e As EventArgs) Handles btnSearch.Click

        If btnStatus.SelectedIndex <= 0 Then
            MessageBox.Show("กรุณาเลือกสถานะที่ต้องการค้นหาก่อนครับ")
            Exit Sub
        End If


        Dim connString As String = "Server=localhost\SQLEXPRESS; DATABASE=PracticeDB; Trusted_Connection=True; TrustServerCertificate=True;"
        Dim conn As New SqlConnection(connString)

        Try
            conn.Open()
            Dim sql As String = ""
            Dim cmd As New SqlCommand()
            cmd.Connection = conn

            If btnStatus.Text = "ทั้งหมด" Then
                sql = "SELECT order_code as รหัสใบสั่งซื้อ,order_date as วันที่สั่ง,amount as จำนวน,status as สถานะ FROM orders"
            Else
                sql = "SELECT order_code as รหัสใบสั่งซื้อ,order_date as วันที่สั่ง,amount as จำนวน,status as สถานะ FROM orders WHERE status = @status"
                cmd.Parameters.AddWithValue("@status", btnStatus.Text)
            End If

            cmd.CommandText = sql

            Dim adapter As New SqlDataAdapter(cmd)
            Dim dt As New DataTable()
            adapter.Fill(dt)

            dataOrder.DataSource = dt
            conn.Close()

        Catch ex As Exception
            MessageBox.Show("ERROR : " & ex.Message)
        End Try
    End Sub

    Private Sub Guna2ButtonbtnClearSearch_Click(sender As Object, e As EventArgs) Handles btnClearSearch.Click
        btnStatus.SelectedIndex = 0
        LoadAllOrders()
    End Sub

    Private Sub PanelPopup_Paint(sender As Object, e As PaintEventArgs)

    End Sub

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        PanelPopup.Visible = True
        amount.PlaceholderText = ""
        amount.Text = "1"

        CalculateTotal()
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        PanelPopup.Visible = False
    End Sub

    Private Sub Label6_Click(sender As Object, e As EventArgs) Handles Label6.Click

    End Sub

    Private Sub Guna2ComboBox1_SelectedIndexChanged_1(sender As Object, e As EventArgs) Handles DropdownCustomer.SelectedIndexChanged
        Dim connString As String = "Server=localhost\SQLEXPRESS; DATABASE=PracticeDB; Trusted_Connection=True; TrustServerCertificate=True;"
        Dim conn As New SqlConnection(connString)

        If DropdownCustomer.SelectedIndex = -1 Then
            MessageBox.Show("กรุณาเลือกลูกค้าก่อนทำรายการครับ")
            Exit Sub
        End If

    End Sub

    Private Sub PanelPopup_Paint_1(sender As Object, e As PaintEventArgs) Handles PanelPopup.Paint

    End Sub

    Private Sub Guna2Panel3_Paint(sender As Object, e As PaintEventArgs) Handles ProductPanel.Paint

    End Sub

    Private Sub addProducts_Click(sender As Object, e As EventArgs) Handles addProducts.Click

        Dim totalPanel As Integer = PanelContainer.Controls.OfType(Of Guna2Panel)().Count()
        Dim offsetY As Integer = totalPanel * (ProductPanel.Height + 8)

        Dim newPanel As New Guna2Panel()
        newPanel.Size = ProductPanel.Size
        newPanel.BackColor = ProductPanel.BackColor
        newPanel.Location = New Point(ProductPanel.Location.X, ProductPanel.Location.Y + offsetY)
        newPanel.Tag = "ClonedProductPanel"

        Dim productDt As DataTable = CType(DropdownProduct.DataSource, DataTable).Copy()

        Dim newDropdown As New Guna2ComboBox()
        newDropdown.Size = DropdownProduct.Size
        newDropdown.Location = DropdownProduct.Location
        newDropdown.DataSource = Nothing
        newDropdown.DisplayMember = "product_name"
        newDropdown.ValueMember = "product_id"
        newDropdown.DataSource = productDt

        newDropdown.BeginInvoke(Sub()
                                    If newDropdown.Items.Count > 0 Then
                                        newDropdown.SelectedIndex = 0
                                    End If
                                End Sub)

        Dim newPricePanel As New Guna2Panel()
        newPricePanel.Size = panelPrice.Size
        newPricePanel.Location = panelPrice.Location
        newPricePanel.BorderColor = panelPrice.BorderColor
        newPricePanel.BorderThickness = panelPrice.BorderThickness
        newPricePanel.BorderRadius = panelPrice.BorderRadius
        newPricePanel.FillColor = panelPrice.FillColor

        Dim newPriceLabel As New Label()
        newPriceLabel.Text = "฿ 0.00"
        newPriceLabel.AutoSize = False
        newPriceLabel.Size = priceLable.Size
        newPriceLabel.Location = priceLable.Location
        newPriceLabel.Font = priceLable.Font
        newPriceLabel.ForeColor = priceLable.ForeColor
        newPriceLabel.BackColor = Color.Transparent
        newPriceLabel.Dock = DockStyle.Fill
        newPricePanel.Controls.Add(newPriceLabel)

        Dim newAmount As New Guna2TextBox()
        newAmount.Size = amount.Size
        newAmount.Location = amount.Location
        newAmount.Text = "1"

        Dim newDeleteBtn As New Guna2Button()
        newDeleteBtn.Size = btndeletePrd.Size
        newDeleteBtn.Location = btndeletePrd.Location
        newDeleteBtn.FillColor = btndeletePrd.FillColor
        newDeleteBtn.Image = btndeletePrd.Image

        AddHandler newDropdown.SelectedIndexChanged, Sub(s, ev)
                                                         If newDropdown.SelectedIndex <= 0 Then
                                                             newPriceLabel.Text = "฿ 0.00"
                                                             Exit Sub
                                                         End If

                                                         Dim selectedRow As DataRowView = newDropdown.SelectedItem
                                                         Dim price As Decimal = selectedRow("price")
                                                         newPriceLabel.Text = "฿ " & price.ToString("N2")

                                                         CalculateTotal()
                                                     End Sub
        AddHandler newAmount.TextChanged, Sub(s, ev)
                                              CalculateTotal()
                                          End Sub

        AddHandler newDeleteBtn.Click, Sub(s, ev)
                                           PanelContainer.Controls.Remove(newPanel)
                                           newPanel.Dispose()
                                           CalculateTotal()
                                       End Sub

        newPanel.Controls.Add(newDropdown)
        newPanel.Controls.Add(newPricePanel)
        newPanel.Controls.Add(newAmount)
        newPanel.Controls.Add(newDeleteBtn)

        PanelContainer.Controls.Add(newPanel)



    End Sub


    Private Sub DropdownProduct_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropdownProduct.SelectedIndexChanged
        If DropdownProduct.SelectedIndex <= 0 Then
            priceLable.Text = "฿ 0.00"
            Exit Sub
        End If

        Dim selectedRow As DataRowView = DropdownProduct.SelectedItem
        Dim price As Decimal = selectedRow("price")

        priceLable.Text = "฿ " & price.ToString("N2")

        CalculateTotal()
    End Sub

    Private Sub amount_TextChanged(sender As Object, e As EventArgs) Handles amount.TextChanged
        CalculateTotal()
    End Sub

    Private Sub prrrrr_Paint(sender As Object, e As PaintEventArgs) Handles panelPrice.Paint

    End Sub

    Private Sub deletePrd_Click(sender As Object, e As EventArgs) Handles btndeletePrd.Click

    End Sub

    Private Sub CalculateTotal()
        Dim total As Decimal = 0

        If DropdownProduct.SelectedIndex > 0 Then
            Dim selectedRowOriginal As DataRowView = DropdownProduct.SelectedItem
            Dim priceOriginal As Decimal = selectedRowOriginal("price")

            Dim qtyOriginal As Integer = 0
            Integer.TryParse(amount.Text, qtyOriginal)

            total += priceOriginal * qtyOriginal
        End If

        For Each ctrl As Control In PanelContainer.Controls
            If TypeOf ctrl Is Guna2Panel Then
                Dim itemPanel As Guna2Panel = CType(ctrl, Guna2Panel)

                Dim dropdown As Guna2ComboBox = Nothing
                Dim amountBox As Guna2TextBox = Nothing
                Dim priceLbl As Label = Nothing

                For Each child As Control In itemPanel.Controls
                    If TypeOf child Is Guna2ComboBox Then
                        dropdown = CType(child, Guna2ComboBox)
                    ElseIf TypeOf child Is Guna2TextBox Then
                        amountBox = CType(child, Guna2TextBox)
                    ElseIf TypeOf child Is Panel Then
                        For Each grandChild As Control In child.Controls
                            If TypeOf grandChild Is Label Then
                                priceLbl = CType(grandChild, Label)
                            End If
                        Next
                    End If
                Next

                If dropdown IsNot Nothing AndAlso amountBox IsNot Nothing AndAlso priceLbl IsNot Nothing Then
                    If dropdown.SelectedIndex > 0 Then
                        Dim selectedRow As DataRowView = dropdown.SelectedItem
                        Dim price As Decimal = selectedRow("price")

                        Dim qty As Integer = 0
                        Integer.TryParse(amountBox.Text, qty)

                        total += price * qty
                    End If
                End If
            End If
        Next

        lblTotalAmount.Text = "฿ " & total.ToString("N2")
    End Sub

    Private Sub CancleBtn_Click(sender As Object, e As EventArgs) Handles CancelOrderBtn.Click
        Dim panelsToRemove As New List(Of Control)
        For Each ctrl As Control In PanelContainer.Controls
            If TypeOf ctrl Is Guna2Panel AndAlso ctrl.Tag IsNot Nothing AndAlso ctrl.Tag.ToString() = "ClonedProductPanel" Then
                panelsToRemove.Add(ctrl)
            End If
        Next
        For Each p As Control In panelsToRemove
            PanelContainer.Controls.Remove(p)
            p.Dispose()
        Next

        DropdownCustomer.SelectedIndex = 0
        DropdownProduct.SelectedIndex = 0
        amount.Text = "1"
        priceLable.Text = "฿ 0.00"
        lblTotalAmount.Text = "฿ 0.00"

        PanelPopup.Visible = False
    End Sub

    Private Sub SaveBtn_Click(sender As Object, e As EventArgs) Handles SaveOrderBtn.Click

        If DropdownCustomer.SelectedIndex <= 0 Then
            MessageBox.Show("กรุณาเลือกลูกค้าก่อนบันทึก")
            Exit Sub
        End If

        Dim productList As New List(Of (productId As Integer, qty As Integer, price As Decimal))

        If DropdownProduct.SelectedIndex > 0 Then
            Dim row As DataRowView = DropdownProduct.SelectedItem
            Dim pid As Integer = row("product_id")
            Dim price As Decimal = row("price")
            Dim qty As Integer = 0
            Integer.TryParse(amount.Text, qty)

            If qty > 0 Then
                productList.Add((pid, qty, price))
            End If
        End If

        For Each ctrl As Control In PanelContainer.Controls
            If TypeOf ctrl Is Guna2Panel AndAlso ctrl.Tag IsNot Nothing AndAlso ctrl.Tag.ToString() = "ClonedProductPanel" Then
                Dim itemPanel As Guna2Panel = CType(ctrl, Guna2Panel)



                Dim dropdown As Guna2ComboBox = Nothing
                Dim amountBox As Guna2TextBox = Nothing

                For Each child As Control In itemPanel.Controls
                    If TypeOf child Is Guna2ComboBox Then
                        dropdown = CType(child, Guna2ComboBox)
                    ElseIf TypeOf child Is Guna2TextBox Then
                        amountBox = CType(child, Guna2TextBox)
                    End If
                Next

                If dropdown IsNot Nothing AndAlso dropdown.SelectedIndex > 0 Then
                    Dim row As DataRowView = dropdown.SelectedItem
                    Dim pid As Integer = row("product_id")
                    Dim price As Decimal = row("price")
                    Dim qty As Integer = 0
                    Integer.TryParse(amountBox.Text, qty)

                    If qty > 0 Then
                        productList.Add((pid, qty, price))
                    End If
                End If
            End If

        Next


        If productList.Count = 0 Then
            MessageBox.Show("กรุณาเลือกสินค้าอย่างน้อย 1 รายการ")
            Exit Sub
        End If

        Dim totalAmount As Decimal = 0
        For Each item In productList
            totalAmount += item.price * item.qty
        Next

        Dim result As DialogResult = MessageBox.Show("คุณต้องการบันทึก Order นี้ใช่หรือไม่?" & vbCrLf &
                                                 "ยอดรวมทั้งสิ้น: ฿ " & totalAmount.ToString("N2"),
                                                 "ยืนยันการบันทึก",
                                                 MessageBoxButtons.YesNo,
                                                 MessageBoxIcon.Question)


        If result = DialogResult.No Then
            Exit Sub
        End If


        Dim connString As String = "Server=localhost\SQLEXPRESS;Database=PracticeDB;Trusted_Connection=True;TrustServerCertificate=True"
        Dim conn As New SqlConnection(connString)

        Try
            conn.Open()
            Dim trans As SqlTransaction = conn.BeginTransaction()

            Try

                Dim custId As Integer = DropdownCustomer.SelectedValue

                Dim sqlOrder As String = "INSERT INTO orders (cust_id, amount, status, order_date) " &
                                      "VALUES (@cust_id, @amount, 'PENDING', GETDATE()); " &
                                      "SELECT SCOPE_IDENTITY()"

                Dim cmdOrder As New SqlCommand(sqlOrder, conn, trans)
                cmdOrder.Parameters.AddWithValue("@cust_id", custId)
                cmdOrder.Parameters.AddWithValue("@amount", totalAmount)

                Dim newOrderId As Integer = Convert.ToInt32(cmdOrder.ExecuteScalar())


                For Each item In productList
                    Dim sqlItem As String = "INSERT INTO order_items (order_id, product_id, qty) VALUES (@order_id, @product_id, @qty)"
                    Dim cmdItem As New SqlCommand(sqlItem, conn, trans)
                    cmdItem.Parameters.AddWithValue("@order_id", newOrderId)
                    cmdItem.Parameters.AddWithValue("@product_id", item.productId)
                    cmdItem.Parameters.AddWithValue("@qty", item.qty)
                    cmdItem.ExecuteNonQuery()
                Next

                trans.Commit()
                conn.Close()

                MessageBox.Show("บันทึก Order สำเร็จ!")


                CancleBtn_Click(sender, e)
                LoadSummaryCards()
                LoadAllOrders()

            Catch ex As Exception

                trans.Rollback()
                Throw ex
            End Try

        Catch ex As Exception
            MessageBox.Show("ERROR (Save Order): " & ex.Message)
        End Try

    End Sub

End Class