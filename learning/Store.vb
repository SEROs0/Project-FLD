Imports System.Data.SqlClient
Imports System.Windows

Public Class Store

    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim connString As String = "Server=localhost\SQLEXPRESS; DATABASE=PracticeDB; Trusted_Connection=True; TrustServerCertificate=True;"
        Dim conn As New SqlConnection(connString)

        DropdownCategory.Items.Clear()
        DropdownCategory.Items.Add("เลือกประเภทสินค้า")
        DropdownCategory.Items.Add("สินค้าทั้งหมด")
        DropdownCategory.Items.Add("ถุงพลาสติก")
        DropdownCategory.Items.Add("ถุงร้อน")
        DropdownCategory.Items.Add("ถุงซิปล็อค")
        DropdownCategory.Items.Add("ถุงเย็น")
        DropdownCategory.Items.Add("ถุงขยะ")
        DropdownCategory.SelectedIndex = 0

        LoadAllProducts()
        LoadSummaryCards()

    End Sub

    Private Sub DataGridView4_CellContentClick(sender As Object, e As DataGridViewCellEventArgs)

    End Sub

    Private Sub LoadAllProducts()
        Dim connString As String = "Server=localhost\SQLEXPRESS; DATABASE=PracticeDB; Trusted_Connection=True; TrustServerCertificate=True;"
        Dim conn As New SqlConnection(connString)

        Try
            conn.Open()

            Dim sql As String = "Select product_name as สินค้า, price as ราคา, category as ประเภทสินค้า , stock_quantity as จำนวนสินค้าในคลัง  from products"
            Dim cmd As New SqlCommand(sql, conn)
            Dim adapter As New SqlDataAdapter(cmd)
            Dim dt As New DataTable()
            adapter.Fill(dt)

            dataStore.DataSource = dt

            conn.Close()

        Catch ex As Exception
            MessageBox.Show("ERROR (Load All Products): " & ex.Message)
        End Try
    End Sub



    Private Sub LoadSummaryCards()
        Dim connString As String = "Server=localhost\SQLEXPRESS; DATABASE=PracticeDB; Trusted_Connection=True; TrustServerCertificate=True;"
        Dim conn As New SqlConnection(connString)

        Try
            conn.Open()

            Dim cmd1 As New SqlCommand("select sum([stock_quantity]) from products", conn)
            Dim storkTotal = cmd1.ExecuteScalar()
            lblStorkValue.Text = If(IsDBNull(storkTotal), "0", storkTotal.ToString())

            Dim cmd2 As New SqlCommand("Select sum(price * stock_quantity) as Value from products", conn)
            Dim ValueTotal = cmd2.ExecuteScalar()
            lblValue.Text = "฿ " & If(IsDBNull(ValueTotal), "0", ValueTotal.ToString())


            conn.Close()
        Catch ex As Exception
            MessageBox.Show("ERROR (Load Summary): " & ex.Message)
        End Try



    End Sub


    Private Sub btnSTORE_Click(sender As Object, e As EventArgs) Handles btnOrder.Click
        Dim Ord As New Order()
        Ord.Show()
        Me.Hide()
    End Sub

    Private Sub btnJob_Click(sender As Object, e As EventArgs) Handles btnJob.Click
        Dim Jb As New Job()
        Jb.Show()
        Me.Hide()
    End Sub

    Private Sub btnPo_Click(sender As Object, e As EventArgs) Handles btnPo.Click
        Dim PO As New PR()
        PO.Show()
        Me.Hide()
    End Sub

    Private Sub Guna2ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropdownCategory.SelectedIndexChanged

    End Sub

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        If DropdownCategory.SelectedIndex <= 0 Then
            MessageBox.Show("กรุณาเลือกประเภทสินค้าที่ต้องการ")
            Exit Sub
        End If


        Dim connString As String = "Server=localhost\SQLEXPRESS; DATABASE=PracticeDB; Trusted_Connection=True; TrustServerCertificate=True;"
        Dim conn As New SqlConnection(connString)

        Try
            conn.Open()
            Dim sql As String = ""
            Dim cmd As New SqlCommand()
            cmd.Connection = conn

            If DropdownCategory.Text = "สินค้าทั้งหมด" Then
                sql = "select product_name as สินค้า, price as ราคา, category as ประเภทสินค้า , stock_quantity as จำนวนสินค้าในคลัง from products"
            Else
                sql = "select product_name as สินค้า, price as ราคา, category as ประเภทสินค้า , stock_quantity as จำนวนสินค้าในคลัง from products where category = @category"
                cmd.Parameters.AddWithValue("@category", DropdownCategory.Text)
            End If

            cmd.CommandText = sql

            Dim adapter As New SqlDataAdapter(cmd)
            Dim dt As New DataTable()
            adapter.Fill(dt)

            dataStore.DataSource = dt
            conn.Close()

        Catch ex As Exception
            MessageBox.Show("ERROR : " & ex.Message)
        End Try

    End Sub

    Private Sub btnClearSearch_Click(sender As Object, e As EventArgs) Handles btnClearSearch.Click
        DropdownCategory.SelectedIndex = 0
        LoadAllProducts()
    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs)

    End Sub


End Class
