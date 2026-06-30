Imports System.Data.SqlClient

Public Class Login


    Private Sub Button1_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click

    End Sub

    Private Sub Label2_Click(sender As Object, e As EventArgs) Handles Label2.Click

    End Sub
    Private Sub TextBox1_Username(sender As Object, e As EventArgs) Handles TextBox1.TextChanged

    End Sub

    Private Sub TextBox2_Password(sender As Object, e As EventArgs) Handles TextBox2.TextChanged

    End Sub

    Private Sub BtnLogin_Click(sender As Object, e As EventArgs) Handles BtnLogin.Click
        If TextBox1.Text = "" Or TextBox2.Text = "" Then
            MessageBox.Show("กรุณาใส่ Username กับ Password")
            Exit Sub
        End If

        Dim connString As String = "Server=localhost\SQLEXPRESS;DATABASE=PracticeDB;Trusted_Connection=True;TrustServerCertificate=True"
        Dim conn As New SqlConnection(connString)

        Try
            conn.Open()

            Dim sql As String = "select count(*) from users where username = @username and password = @password"
            Dim cmd As New SqlCommand(sql, conn)

            cmd.Parameters.AddWithValue("@username", TextBox1.Text)
            cmd.Parameters.AddWithValue("@password", TextBox2.Text)

            Dim count As Integer = cmd.ExecuteScalar()

            If count > 0 Then
                MessageBox.Show("Login Success", TextBox1.Text)

                Dim f2 As New Store()
                f2.Show()
                Me.Hide()

            Else
                MessageBox.Show("Username or Password fail")
            End If

            conn.Close()

        Catch ex As Exception
            MessageBox.Show("ERROR: " & ex.Message)
        End Try
    End Sub
End Class
