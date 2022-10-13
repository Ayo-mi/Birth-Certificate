Imports MySql.Data.MySqlClient
Public Class SignUp

    Private Function checkIfStaffExist(ByVal staffID As String) As Boolean
        Dim cn As New MySqlConnection
        Dim cm As New MySqlCommand
        Dim dr As MySqlDataReader
        Dim bool As Boolean
        cn.ConnectionString = connectstr()
        cm.CommandText = "Select staffID from staff_table where staffID='" & staffID & "'"
        cm.Connection = cn
        Try
            cn.Open()
            dr = cm.ExecuteReader
            If Not dr.Read Then
                bool = False
            Else
                bool = True
            End If
            Return bool
        Catch e As Exception
            BunifuSnackbar1.Show(Me, e.Message, BunifuSnackbar1.MessageTypes.Error, 5000, "", BunifuSnackbar1.Positions.MiddleCenter)
        End Try
    End Function

    Private Function connectstr() As String
        Dim cnstr As String = "server=localhost; password=; user=root; database=birth_reg; port=3306;"
        Return cnstr
    End Function
    Private Sub label8_Click(sender As Object, e As EventArgs) Handles cancel.Click
        Application.Exit()

    End Sub

    Private Sub BunifuButton2_Click(sender As Object, e As EventArgs) Handles BunifuButton2.Click
        Form1.Panel1.Visible = True
        Form1.Panel2.Visible = True
        Me.Close()
    End Sub

    Private Sub BunifuButton3_Click(sender As Object, e As EventArgs)
        Form1.Panel1.Visible = True
        Form1.Panel2.Visible = True
        Me.Close()
    End Sub

    Private Sub cancel_MouseEnter(sender As Object, e As EventArgs) Handles cancel.MouseEnter
        cancel.BackColor = Color.DarkGray
        cancel.ForeColor = Color.White
    End Sub

    Private Sub cancel_MouseLeave(sender As Object, e As EventArgs) Handles cancel.MouseLeave
        cancel.BackColor = Color.White
        cancel.ForeColor = Color.LimeGreen
    End Sub

    Private Sub BunifuButton1_Click(sender As Object, e As EventArgs) Handles BunifuButton1.Click
        If bunifuMaterialTextbox1.Text = "" Then
            BunifuSnackbar1.Show(Me, "Enter first name", BunifuSnackbar1.MessageTypes.Error, 5000, "", BunifuSnackbar1.Positions.MiddleRight)
            Exit Sub
        ElseIf bunifuMaterialTextbox2.Text = "" Then
            BunifuSnackbar1.Show(Me, "Enter last name", BunifuSnackbar1.MessageTypes.Error, 5000, "", BunifuSnackbar1.Positions.MiddleRight)
            Exit Sub
        ElseIf bunifuMaterialTextbox3.Text = "" Then
            BunifuSnackbar1.Show(Me, "Enter staff ID", BunifuSnackbar1.MessageTypes.Error, 5000, "", BunifuSnackbar1.Positions.MiddleRight)
            Exit Sub
        ElseIf bunifuMaterialTextbox4.Text = "" Then
            BunifuSnackbar1.Show(Me, "Enter facility unique ID", BunifuSnackbar1.MessageTypes.Error, 5000, "", BunifuSnackbar1.Positions.MiddleRight)
            Exit Sub
        ElseIf bunifuMaterialTextbox7.Text = "" Then
            BunifuSnackbar1.Show(Me, "Enter facility name", BunifuSnackbar1.MessageTypes.Error, 5000, "", BunifuSnackbar1.Positions.MiddleRight)
            Exit Sub
        ElseIf bunifuMaterialTextbox8.Text = "" Then
            BunifuSnackbar1.Show(Me, "Enter town or village", BunifuSnackbar1.MessageTypes.Error, 5000, "", BunifuSnackbar1.Positions.MiddleRight)
            Exit Sub
        ElseIf bunifuMaterialTextbox9.Text = "" Then
            BunifuSnackbar1.Show(Me, "Enter state and LGA (e.g Rivers, Bonny) ", BunifuSnackbar1.MessageTypes.Error, 5000, "", BunifuSnackbar1.Positions.MiddleRight)
            Exit Sub
        ElseIf Not bunifuMaterialTextbox9.Text.Contains(",") Then
            BunifuSnackbar1.Show(Me, "Separate your state and LGA with a comma (e.g Rivers, Bonny) ", BunifuSnackbar1.MessageTypes.Error, 5000, "", BunifuSnackbar1.Positions.MiddleRight)
            Exit Sub
        ElseIf bunifuMaterialTextbox5.Text = "" Then
            BunifuSnackbar1.Show(Me, "Enter Password", BunifuSnackbar1.MessageTypes.Error, 5000, "", BunifuSnackbar1.Positions.MiddleRight)
            Exit Sub
        ElseIf bunifuMaterialTextbox5.Text.Length < 6 Then
            BunifuSnackbar1.Show(Me, "Password must be at least 6 characters", BunifuSnackbar1.MessageTypes.Error, 5000, "", BunifuSnackbar1.Positions.MiddleRight)
            Exit Sub
        ElseIf Not bunifuMaterialTextbox5.Text = BunifuMaterialTextbox6.Text Then
            BunifuSnackbar1.Show(Me, "Password does not match", BunifuSnackbar1.MessageTypes.Error, 5000, "", BunifuSnackbar1.Positions.MiddleRight)
            Exit Sub
        End If

        If checkIfStaffExist(BunifuMaterialTextbox3.Text) Then
            BunifuSnackbar1.Show(Me, "User with staffID " & BunifuMaterialTextbox3.Text & " already exist",
                                 BunifuSnackbar1.MessageTypes.Error, 5000, "", BunifuSnackbar1.Positions.MiddleCenter)
            Exit Sub
        End If

        Dim cmd As New MySqlCommand
        Dim con As New MySqlConnection
        Try
            Dim sl As String() = New String(2) {}
            sl = BunifuMaterialTextbox9.Text.Split(CType(",", Char()), 2)

            cmd.CommandText = "insert into staff_table (staffID, firstName, lastName, facilityUID, passcode, facilityName,town,state,lga) VALUES (@a, @b, @c, @d, @e,@f,@g,@h,@i)"

            cmd.Parameters.AddWithValue("@a", BunifuMaterialTextbox3.Text.ToUpper.Trim)
            cmd.Parameters.AddWithValue("@b", bunifuMaterialTextbox1.Text.Substring(0, 1).ToUpper() + bunifuMaterialTextbox1.Text.Substring(1).ToLower())
            cmd.Parameters.AddWithValue("@c", BunifuMaterialTextbox2.Text.Substring(0, 1).ToUpper() + BunifuMaterialTextbox2.Text.Substring(1).ToLower())
            cmd.Parameters.AddWithValue("@d", BunifuMaterialTextbox4.Text.Trim)
            cmd.Parameters.AddWithValue("@e", BunifuMaterialTextbox5.Text.Trim)
            cmd.Parameters.AddWithValue("@f", BunifuMaterialTextbox7.Text.Trim)
            cmd.Parameters.AddWithValue("@g", BunifuMaterialTextbox8.Text.Trim)
            cmd.Parameters.AddWithValue("@h", sl(0).Trim)
            cmd.Parameters.AddWithValue("@i", sl(1).Trim)
            con.ConnectionString = connectstr()
            cmd.Connection = con
            con.Open()


            Dim r As Integer
            r = cmd.ExecuteNonQuery
            If r > 0 Then
                BunifuSnackbar1.Show(Me, "Account created successfully", BunifuSnackbar1.MessageTypes.Success, 5000, "", BunifuSnackbar1.Positions.MiddleCenter)
            Else
                BunifuSnackbar1.Show(Me, "Account not created, an error occured", BunifuSnackbar1.MessageTypes.Error, 5000, "", BunifuSnackbar1.Positions.MiddleCenter)
            End If

        Catch ex As Exception
            BunifuSnackbar1.Show(Me, ex.Message, BunifuSnackbar1.MessageTypes.Error, 5000, "", BunifuSnackbar1.Positions.MiddleCenter)
        End Try
    End Sub

    Private Sub BunifuMaterialTextbox4_KeyPress(sender As Object, e As KeyPressEventArgs) Handles BunifuMaterialTextbox4.KeyPress
        If (Not Char.IsControl(e.KeyChar) AndAlso (Not Char.IsDigit(e.KeyChar) AndAlso (e.KeyChar <> Microsoft.VisualBasic.ChrW(46)))) Then
            e.Handled = True
        End If
    End Sub

End Class