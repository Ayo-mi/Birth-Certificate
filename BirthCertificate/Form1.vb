Imports MySql.Data.MySqlClient
Public Class Form1

    Private Function connectstr() As String
        Dim cnstr As String = "server=localhost; password=; user=root; database=birth_reg; port=3306;"
        Return cnstr
    End Function

    Private Sub label8_Click(sender As Object, e As EventArgs) Handles label8.Click
        Application.Exit()
    End Sub

    Private Sub BunifuButton3_Click(sender As Object, e As EventArgs) Handles BunifuButton3.Click
        Dim sup As New SignUp
        sup.MdiParent = Me
        Panel1.Visible = False
        Panel2.Visible = False
        sup.Dock = DockStyle.Fill
        sup.Show()
        Me.StartPosition = FormStartPosition.CenterScreen
    End Sub

    Private Sub label8_MouseLeave(sender As Object, e As EventArgs) Handles label8.MouseLeave
        label8.BackColor = SystemColors.Control
        label8.ForeColor = Color.LimeGreen
    End Sub

    Private Sub label8_MouseEnter(sender As Object, e As EventArgs) Handles label8.MouseEnter
        label8.BackColor = Color.DarkGray
        label8.ForeColor = Color.White
    End Sub

    Private Sub BunifuButton2_Click(sender As Object, e As EventArgs) Handles BunifuButton2.Click
        Me.Close()
    End Sub

    Private Sub BunifuButton1_Click(sender As Object, e As EventArgs) Handles BunifuButton1.Click
        If bunifuMaterialTextbox1.Text = "" Then
            BunifuSnackbar1.Show(Me, "Enter staffID", BunifuSnackbar1.MessageTypes.Error, 5000, "", BunifuSnackbar1.Positions.MiddleRight)
            Exit Sub
        ElseIf BunifuMaterialTextbox2.Text = "" Then
            BunifuSnackbar1.Show(Me, "Enter password", BunifuSnackbar1.MessageTypes.Error, 5000, "", BunifuSnackbar1.Positions.MiddleRight)
            Exit Sub
        End If

        Dim cn As New MySqlConnection
        Dim cm As New MySqlCommand
        Dim dr As MySqlDataReader
        cn.ConnectionString = connectstr()
        cm.CommandText = "Select * from staff_table where staffID='" & bunifuMaterialTextbox1.Text & "' and passcode = '" & BunifuMaterialTextbox2.Text & "' limit 1"
        cm.Connection = cn
        Try
            cn.Open()
            dr = cm.ExecuteReader
            If dr.Read Then
                Dim sup As New Main
                sup.MdiParent = Me
                Panel1.Visible = False
                Panel2.Visible = False
                BunifuMaterialTextbox2.Text = ""
                sup.Dock = DockStyle.Fill
                sup.BunifuLabel1.Text = dr("firstName").ToString & " " & dr("lastName")
                sup.BunifuLabel2.Text = dr("staffID").ToString
                sup.BunifuLabel34.Text = dr("lga").ToString
                sup.BunifuLabel33.Text = dr("state").ToString
                sup.BunifuLabel37.Text = dr("town").ToString
                sup.BunifuLabel36.Text = dr("facilityName").ToString
                sup.uid.Text = dr("facilityUID")
                sup.Show()
                Me.StartPosition = FormStartPosition.CenterScreen

            Else
                BunifuSnackbar1.Show(Me, "StaffID or password not correct", BunifuSnackbar1.MessageTypes.Information, 5000, "", BunifuSnackbar1.Positions.MiddleCenter)
            End If

        Catch ex As Exception
            BunifuSnackbar1.Show(Me, ex.Message, BunifuSnackbar1.MessageTypes.Error, 5000, "", BunifuSnackbar1.Positions.MiddleCenter)
        End Try
    End Sub

    Private Sub BunifuButton4_Click(sender As Object, e As EventArgs) Handles BunifuButton4.Click
        Dim mat = InputBox("Enter Your ID", "Forget Password")
        If mat = "" Then
            Exit Sub
        End If
        Dim cn As New MySqlConnection
        Dim cm As New MySqlCommand
        Dim dr As MySqlDataReader
        cn.ConnectionString = connectstr()
        cm.CommandText = "Select staffID,firstName from staff_table where staffID='" & mat.Trim & "'"

        cm.Connection = cn
        Try
            cn.Open()
            dr = cm.ExecuteReader
            Dim ans, a As String
            ans = ""
            a = ""
            If dr.Read Then
                ans = InputBox(dr("firstName") & ", enter your new password", "New Password")

            Else
                BunifuSnackbar1.Show(Me, "staff not found, check if it's written correctly", BunifuSnackbar1.MessageTypes.Information, 5000, "", BunifuSnackbar1.Positions.MiddleCenter)
                Exit Sub
            End If
            If ans = "" Then
                BunifuSnackbar1.Show(Me, "Password was not changed", BunifuSnackbar1.MessageTypes.Information, 5000, "", BunifuSnackbar1.Positions.MiddleCenter)
                Exit Sub
            ElseIf ans.Length < 6 Then
                BunifuSnackbar1.Show(Me, "Password must be at least 6 characters", BunifuSnackbar1.MessageTypes.Information, 5000, "", BunifuSnackbar1.Positions.MiddleRight)
                Exit Sub
            Else
                Try
                    Dim con As New MySqlConnection
                    Dim com As New MySqlCommand
                    Dim dar As MySqlDataReader
                    con.ConnectionString = connectstr()
                    com.CommandText = "update staff_table set passcode = '" & ans & "' where staffID = '" & mat & "'"

                    com.Connection = con
                    con.Open()
                    Dim r As Integer
                    r = com.ExecuteNonQuery
                    If r > 0 Then
                        BunifuSnackbar1.Show(Me, "Password Changed Successfully", BunifuSnackbar1.MessageTypes.Information, 5000, "", BunifuSnackbar1.Positions.MiddleCenter)

                    Else
                        BunifuSnackbar1.Show(Me, "Sorry! An error occured while trying to change password", BunifuSnackbar1.MessageTypes.Information, 5000, "", BunifuSnackbar1.Positions.MiddleCenter)
                    End If

                Catch ex As Exception
                    BunifuSnackbar1.Show(Me, ex.Message, BunifuSnackbar1.MessageTypes.Error, 5000, "", BunifuSnackbar1.Positions.MiddleCenter)

                End Try
            End If
        Catch ex As Exception
            BunifuSnackbar1.Show(Me, ex.Message, BunifuSnackbar1.MessageTypes.Error, 5000, "", BunifuSnackbar1.Positions.MiddleCenter)
        End Try
    End Sub
End Class
