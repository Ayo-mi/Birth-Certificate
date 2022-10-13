Imports System.Text
Imports MySql.Data.MySqlClient
Imports word = Microsoft.Office.Interop.Word
Public Class Main
    Dim tabledata As New DataTable
    Dim birtid As String

    Private Sub getData(ByVal bknm As String, ByVal val As String, ByVal objDoc As word.Document)
        Dim bmRange As word.Range = Nothing
        Dim bkmrs As word.Bookmarks = objDoc.Bookmarks

        Dim bkIndex As Object = TryCast(bknm, Object)
        Dim bkm As word.Bookmark = bkmrs.Item(bkIndex)
        bkm.Range.Text = val
    End Sub
    Private Sub printCertificate()
        Try
            Dim objWordApp As New word.Application
            objWordApp.Visible = False
            Dim dt As Date
            dt = BunifuDatePicker1.Value.ToString
            Dim dat As Date = Date.Today
            Dim gen As String

            If BunifuRadioButton2.Checked Then
                gen = "Male"
            Else
                gen = "Female"
            End If

            Dim objDoc As word.Document = objWordApp.Documents.Open(Application.StartupPath & "\Template.dotx",, [ReadOnly]:=True)

            objDoc = objWordApp.ActiveDocument

            'objDoc.Content.Find.Execute(FindText:="birthID", ReplaceWith:="123456789", Replace:=word.WdReplace.wdReplaceAll)
            'While objDoc.Content.Find.Execute(FindText:="   ", Wrap:=word.WdFindWrap.wdFindContinue)
            'objDoc.Content.Find.Execute(FindText:="   ", ReplaceWith:="  ", Replace:=word.WdReplace.wdReplaceAll, Wrap:=word.WdFindWrap.wdFindContinue)
            'End While

            getData("birthID", birtid, objDoc)
            getData("bday", dt.ToString("dd"), objDoc)
            getData("bmonth", dt.ToString("MM"), objDoc)
            getData("byr", dt.ToString("yy"), objDoc)
            getData("childName", bunifuMaterialTextbox1.Text.Trim, objDoc)
            getData("curday", dat.ToString("dd"), objDoc)
            getData("curmonth", dat.ToString("MM"), objDoc)
            getData("curyr2", dat.ToString("yy"), objDoc)
            getData("curyr1", dat.ToString("yy"), objDoc)
            getData("ddate", dat.ToString("dd' / 'MM' / 'yyyy"), objDoc)
            getData("fatherName", BunifuMaterialTextbox14.Text.Trim, objDoc)
            getData("genSex", gen, objDoc)
            getData("i_d", getchildId(birtid), objDoc)
            getData("locgovar", BunifuLabel34.Text.Trim, objDoc)
            getData("regCenter", BunifuLabel36.Text.Trim, objDoc)
            getData("statt", BunifuLabel33.Text.Trim, objDoc)
            getData("placeOfIssStatt", BunifuLabel33.Text.Trim, objDoc)
            getData("townVillage", BunifuLabel37.Text.Trim, objDoc)
            getData("placeOfBirth", BunifuMaterialTextbox19.Text.Trim, objDoc)
            getData("nameOfStaff", BunifuLabel1.Text.Trim, objDoc)
            getData("motherName", BunifuMaterialTextbox4.Text.Trim, objDoc)

            Dim format As word.WdSaveFormat = word.WdSaveFormat.wdFormatDocumentDefault
            objDoc.SaveAs2(FileName:=Application.StartupPath & "\certificate", FileFormat:=format)

            objDoc.Close()
            objDoc = Nothing
            objWordApp.Quit()
            objWordApp = Nothing

            Dim psi As New ProcessStartInfo
            psi.UseShellExecute = True
            psi.Verb = "print"
            psi.WindowStyle = ProcessWindowStyle.Hidden
            psi.Arguments = PrintDialog1.PrinterSettings.PrinterName.ToString
            psi.FileName = Application.StartupPath & "\certificate.docx"
            Process.Start(psi)

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub reprintCertificate(bId As String)
        Try
            Dim objWordApp As New word.Application
            objWordApp.Visible = False
            Dim dt As Date
            dt = BunifuDatePicker1.Value.ToString
            Dim dat As Date
            Dim gen As String

            Dim objDoc As word.Document = objWordApp.Documents.Open(Application.StartupPath & "\Template.dotx",, [ReadOnly]:=True)

            objDoc = objWordApp.ActiveDocument

            Dim cn As New MySqlConnection
            Dim cm As New MySqlCommand
            Dim dr As MySqlDataReader
            cn.ConnectionString = connectstr()
            cm.CommandText = "SELECT c.names cna, c.dob cdob, c.sex csex, c.dateCreated cdcr, c.birthID cbid, c.id cid, c.birthPlace cbplace,
                                m.name mna, f.name fna, s.firstName, s.lastName
                                FROM child_data c,father_data f,mother_data m, staff_table s where c.birthID=m.childBirthID 
                                and c.birthID=f.childBirthID and c.staffID= s.staffID and s.staffID='" & BunifuLabel2.Text & "' and c.birthID='" & bId & "';"
            cm.Connection = cn

            Try
                cn.Open()
                dr = cm.ExecuteReader
                While dr.Read
                    dt = dr("cdob")
                    dat = dr("cdcr")
                    getData("birthID", dr("cbid"), objDoc)
                    getData("bday", dt.ToString("dd"), objDoc)
                    getData("bmonth", dt.ToString("MM"), objDoc)
                    getData("byr", dt.ToString("yy"), objDoc)
                    getData("childName", dr("cna").ToString.Trim, objDoc)
                    getData("curday", dat.ToString("dd"), objDoc)
                    getData("curmonth", dat.ToString("MM"), objDoc)
                    getData("curyr2", dat.ToString("yy"), objDoc)
                    getData("curyr1", dat.ToString("yy"), objDoc)
                    getData("ddate", dat.ToString("dd' / 'MM' / 'yyyy"), objDoc)
                    getData("fatherName", dr("fna").ToString.Trim, objDoc)
                    getData("genSex", dr("csex").ToString.Trim, objDoc)
                    getData("i_d", dr("cid").ToString.Trim, objDoc)
                    getData("locgovar", BunifuLabel34.Text.Trim, objDoc)
                    getData("regCenter", BunifuLabel36.Text.Trim, objDoc)
                    getData("statt", BunifuLabel33.Text.Trim, objDoc)
                    getData("placeOfIssStatt", BunifuLabel33.Text.Trim, objDoc)
                    getData("townVillage", BunifuLabel37.Text.Trim, objDoc)
                    getData("placeOfBirth", dr("cbplace").ToString.Trim, objDoc)
                    getData("nameOfStaff", dr("firstName") & " " & dr("lastName").ToString.Trim, objDoc)
                    getData("motherName", dr("mna").ToString.Trim, objDoc)

                End While

            Catch ex As Exception
                BunifuSnackbar1.Show(Me, ex.Message, BunifuSnackbar1.MessageTypes.Error, 5000, "", BunifuSnackbar1.Positions.MiddleCenter)
            End Try


            Dim format As word.WdSaveFormat = word.WdSaveFormat.wdFormatDocumentDefault
            objDoc.SaveAs2(FileName:=Application.StartupPath & "\certificate", FileFormat:=format)

            objDoc.Close()
            objDoc = Nothing
            objWordApp.Quit()
            objWordApp = Nothing

            Dim psi As New ProcessStartInfo
            psi.UseShellExecute = True
            psi.Verb = "print"
            psi.WindowStyle = ProcessWindowStyle.Hidden
            psi.Arguments = PrintDialog1.PrinterSettings.PrinterName.ToString
            psi.FileName = Application.StartupPath & "\certificate.docx"
            Process.Start(psi)

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub createColumn()
        With tabledata
            .Columns.Add("S/N", System.Type.GetType("System.Int32"))
            .Columns.Add("Birth ID", System.Type.GetType("System.String"))
            .Columns.Add("Name", System.Type.GetType("System.String"))
            .Columns.Add("Date of Birth", System.Type.GetType("System.String"))
            .Columns.Add("Gender", System.Type.GetType("System.String"))
            .Columns.Add("Birth Type", System.Type.GetType("System.String"))
        End With
    End Sub
Private Function connectstr() As String
        Dim cnstr As String = "server=localhost; password=; user=root; database=birth_reg; port=3306;"
        Return cnstr
    End Function

    Private Function birthID() As String
        Dim booloop As Boolean = False
        Dim sb As New StringBuilder()
        While booloop = False
            Dim validchars As String = "1234567890"

            Dim rand As New Random()
            For i As Integer = 1 To 7
                Dim idx As Integer = rand.Next(0, validchars.Length)
                Dim randomchar As Char = validchars(idx)
                sb.Append(randomchar)
            Next i

            Dim cn As New MySqlConnection
            Dim cm As New MySqlCommand
            Dim dr As MySqlDataReader
            cn.ConnectionString = connectstr()
            cm.CommandText = "Select birthID from child_data where birthID='" & sb.ToString & "'"
            cm.Connection = cn
            Try
                cn.Open()
                dr = cm.ExecuteReader
                If Not dr.Read Then
                    booloop = True
                End If

            Catch e As Exception
                BunifuSnackbar1.Show(Me, e.Message, BunifuSnackbar1.MessageTypes.Error, 5000, "", BunifuSnackbar1.Positions.MiddleCenter)
            End Try
        End While
        Return sb.ToString.ToUpper
    End Function

    Private Sub clearBoxes()
        bunifuMaterialTextbox1.Text = ""
        BunifuMaterialTextbox2.Text = ""
        BunifuMaterialTextbox3.Text = ""
        BunifuMaterialTextbox4.Text = ""
        BunifuMaterialTextbox5.Text = ""
        BunifuMaterialTextbox6.Text = ""
        BunifuMaterialTextbox7.Text = ""
        BunifuMaterialTextbox8.Text = ""
        BunifuMaterialTextbox9.Text = ""
        BunifuMaterialTextbox10.Text = ""
        BunifuMaterialTextbox11.Text = ""
        BunifuMaterialTextbox12.Text = ""
        BunifuMaterialTextbox13.Text = ""
        BunifuMaterialTextbox14.Text = ""
        BunifuMaterialTextbox15.Text = ""
        BunifuMaterialTextbox16.Text = ""
        BunifuMaterialTextbox17.Text = ""
        BunifuMaterialTextbox19.Text = ""


        ComboBox2.SelectedIndex = -1
        ComboBox3.SelectedIndex = -1
        ComboBox4.SelectedIndex = -1
        ComboBox5.SelectedIndex = -1
    End Sub

    Private Sub getAllRecords()
        Dim cn As New MySqlConnection
        Dim cm As New MySqlCommand
        Dim dr As MySqlDataReader
        cn.ConnectionString = connectstr()
        cm.CommandText = "Select birthID, names,sex,birthType,birthOrder, DATE_FORMAT(dob, '%D of %b, %Y')as dob
                            from child_data where facilityUID = '" & uid.Text & "'"
        cm.Connection = cn
        Try
            cn.Open()
            Dim i As Integer = 1
            dr = cm.ExecuteReader
            tabledata.Rows.Clear()

            While dr.Read
                Dim newRow3 As DataRow = tabledata.NewRow()

                newRow3.Item("S/N") = i
                newRow3.Item("Name") = dr("names").ToString
                newRow3.Item("Date of Birth") = dr("dob").ToString
                newRow3.Item("Gender") = dr("sex")
                newRow3.Item("Birth Type") = dr("birthType")
                newRow3.Item("Birth ID") = dr("birthID")

                tabledata.Rows.Add(newRow3)
                i = i + 1

            End While
            BunifuDataGridView1.DataSource = tabledata
        Catch ex As Exception
            BunifuSnackbar1.Show(Me, ex.Message, BunifuSnackbar1.MessageTypes.Error, 5000, "", BunifuSnackbar1.Positions.MiddleCenter)
        End Try

    End Sub
    Private Function getchildId(ByVal birid As String) As String
        Dim cn As New MySqlConnection
        Dim cm As New MySqlCommand
        Dim dr As MySqlDataReader
        cn.ConnectionString = connectstr()
        cm.CommandText = "Select id from child_data where birthID = '" & birid & "'"
        cm.Connection = cn
        Dim a As String
        Try
            cn.Open()
            dr = cm.ExecuteReader
            While dr.Read
                a = dr("id")
            End While

        Catch ex As Exception
            BunifuSnackbar1.Show(Me, ex.Message, BunifuSnackbar1.MessageTypes.Error, 5000, "", BunifuSnackbar1.Positions.MiddleCenter)
        End Try
        Return a
    End Function
    Private Sub label8_Click(sender As Object, e As EventArgs) Handles label8.Click
        Application.Exit()
    End Sub

    Private Sub label8_MouseEnter(sender As Object, e As EventArgs) Handles label8.MouseEnter
        label8.BackColor = Color.DarkGray
        label8.ForeColor = Color.White
    End Sub

    Private Sub label8_MouseLeave(sender As Object, e As EventArgs) Handles label8.MouseLeave
        label8.BackColor = SystemColors.Control
        label8.ForeColor = Color.LimeGreen
    End Sub

    Private Sub BunifuThinButton24_Click(sender As Object, e As EventArgs) Handles BunifuThinButton24.Click
        Form1.Panel1.Visible = True
        Form1.Panel2.Visible = True
        uid.Text = ""
        Me.Close()
    End Sub

    Private Sub BunifuThinButton22_Click(sender As Object, e As EventArgs) Handles BunifuThinButton22.Click
        BunifuPages1.SetPage(0)
    End Sub

    Private Sub BunifuThinButton23_Click(sender As Object, e As EventArgs) Handles BunifuThinButton23.Click
        getAllRecords()
        BunifuPages1.SetPage(1)
    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click
        Form1.Close()
    End Sub

    Private Sub Label1_MouseEnter(sender As Object, e As EventArgs) Handles Label1.MouseEnter
        Label1.BackColor = Color.DarkGray
        Label1.ForeColor = Color.White
    End Sub

    Private Sub Label1_MouseLeave(sender As Object, e As EventArgs) Handles Label1.MouseLeave
        Label1.BackColor = SystemColors.Control
        Label1.ForeColor = Color.LimeGreen
    End Sub

    Private Sub BunifuButton1_Click(sender As Object, e As EventArgs) Handles BunifuButton1.Click
        If BunifuCards2.Visible = True Then
            BunifuTransition1.HideSync(BunifuCards2, False)
            BunifuCards1.Location = New Point(80, 59)
            BunifuCards1.Size = New Size(526, 325)
            BunifuTransition1.ShowSync(BunifuCards1, False)
            BunifuButton1.Enabled = False

        ElseIf BunifuCards3.Visible = True Then
            BunifuTransition1.HideSync(BunifuCards3, False)
            BunifuCards2.Location = New Point(80, 59)
            BunifuCards2.Size = New Size(526, 325)
            BunifuTransition1.ShowSync(BunifuCards2, False)
            BunifuButton1.Enabled = True

        ElseIf BunifuCards4.Visible = True Then
            BunifuTransition1.HideSync(BunifuCards4, False)
            BunifuCards3.Location = New Point(80, 59)
            BunifuCards3.Size = New Size(526, 325)
            BunifuTransition1.ShowSync(BunifuCards3, False)
            BunifuButton1.Enabled = True
            BunifuButton2.Visible = True
            BunifuButton3.Visible = False
        End If

    End Sub

    Private Sub BunifuButton2_Click(sender As Object, e As EventArgs) Handles BunifuButton2.Click
        If BunifuCards1.Visible = True Then
            If bunifuMaterialTextbox1.Text = "" Or BunifuMaterialTextbox2.Text = "" Or BunifuMaterialTextbox19.Text = "" Or ComboBox2.SelectedIndex < 0 Then
                BunifuSnackbar1.Show(Me, "Enter all child's detail", BunifuSnackbar1.MessageTypes.Information, 5000, "", BunifuSnackbar1.Positions.MiddleCenter)
                Exit Sub
            End If

            BunifuTransition1.HideSync(BunifuCards1, False)
            BunifuCards2.Location = New Point(80, 59)
            BunifuCards2.Size = New Size(526, 325)
            BunifuTransition1.ShowSync(BunifuCards2, False)
            BunifuButton1.Enabled = True

        ElseIf BunifuCards2.Visible = True Then
            If BunifuMaterialTextbox3.Text = "" Or BunifuMaterialTextbox4.Text = "" Or BunifuMaterialTextbox5.Text = "" Or BunifuMaterialTextbox6.Text = "" Or BunifuMaterialTextbox7.Text = "" Or BunifuMaterialTextbox8.Text = "" Or ComboBox3.SelectedIndex < 0 Or ComboBox4.SelectedIndex < 0 Then
                BunifuSnackbar1.Show(Me, "Enter all mother's detail", BunifuSnackbar1.MessageTypes.Information, 5000, "", BunifuSnackbar1.Positions.MiddleCenter)
                Exit Sub
            End If

            BunifuTransition1.HideSync(BunifuCards2, False)
            BunifuCards3.Location = New Point(80, 59)
            BunifuCards3.Size = New Size(526, 325)
            BunifuTransition1.ShowSync(BunifuCards3, False)
            BunifuButton1.Enabled = True

        ElseIf BunifuCards3.Visible = True Then
            If BunifuMaterialTextbox9.Text = "" Or BunifuMaterialTextbox10.Text = "" Or BunifuMaterialTextbox11.Text = "" Or BunifuMaterialTextbox12.Text = "" Or BunifuMaterialTextbox13.Text = "" Or BunifuMaterialTextbox14.Text = "" Or ComboBox5.SelectedIndex < 0 Then
                BunifuSnackbar1.Show(Me, "Enter all father's detail", BunifuSnackbar1.MessageTypes.Information, 5000, "", BunifuSnackbar1.Positions.MiddleCenter)
                Exit Sub
            End If

            BunifuTransition1.HideSync(BunifuCards3, False)
            BunifuCards4.Location = New Point(80, 59)
            BunifuCards4.Size = New Size(526, 325)
            BunifuTransition1.ShowSync(BunifuCards4, False)
            BunifuButton1.Enabled = True
            BunifuButton3.Visible = True
            BunifuButton2.Visible = False
        End If


    End Sub

    Private Sub Main_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        createColumn()
    End Sub

    Private Sub BunifuMaterialTextbox3_KeyPress(sender As Object, e As KeyPressEventArgs) Handles BunifuMaterialTextbox3.KeyPress
        If (Not Char.IsControl(e.KeyChar) AndAlso (Not Char.IsDigit(e.KeyChar) AndAlso (e.KeyChar <> Microsoft.VisualBasic.ChrW(46)))) Then
            e.Handled = True
        End If
    End Sub

    Private Sub BunifuMaterialTextbox2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles BunifuMaterialTextbox2.KeyPress
        If (Not Char.IsControl(e.KeyChar) AndAlso (Not Char.IsDigit(e.KeyChar) AndAlso (e.KeyChar <> Microsoft.VisualBasic.ChrW(46)))) Then
            e.Handled = True
        End If
    End Sub

    Private Sub ComboBox2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox2.SelectedIndexChanged
        If ComboBox2.SelectedIndex = 0 Then
            BunifuMaterialTextbox2.Text = 1
        Else
            BunifuMaterialTextbox2.Text = ""
        End If
    End Sub

    Private Sub BunifuMaterialTextbox13_KeyPress(sender As Object, e As KeyPressEventArgs) Handles BunifuMaterialTextbox13.KeyPress
        If (Not Char.IsControl(e.KeyChar) AndAlso (Not Char.IsDigit(e.KeyChar) AndAlso (e.KeyChar <> Microsoft.VisualBasic.ChrW(46)))) Then
            e.Handled = True
        End If
    End Sub

    Private Sub BunifuButton3_Click(sender As Object, e As EventArgs) Handles BunifuButton3.Click
        If bunifuMaterialTextbox1.Text = "" Or BunifuMaterialTextbox2.Text = "" Or BunifuMaterialTextbox19.Text = "" Or ComboBox2.SelectedIndex < 0 Then
            BunifuSnackbar1.Show(Me, "Enter all child's detail", BunifuSnackbar1.MessageTypes.Information, 5000, "", BunifuSnackbar1.Positions.MiddleCenter)
            Exit Sub
        End If
        If BunifuMaterialTextbox3.Text = "" Or BunifuMaterialTextbox4.Text = "" Or BunifuMaterialTextbox5.Text = "" Or BunifuMaterialTextbox6.Text = "" Or BunifuMaterialTextbox7.Text = "" Or BunifuMaterialTextbox8.Text = "" Or ComboBox3.SelectedIndex < 0 Or ComboBox4.SelectedIndex < 0 Then
            BunifuSnackbar1.Show(Me, "Enter all mother's detail", BunifuSnackbar1.MessageTypes.Information, 5000, "", BunifuSnackbar1.Positions.MiddleCenter)
            Exit Sub
        End If
        If BunifuMaterialTextbox9.Text = "" Or BunifuMaterialTextbox10.Text = "" Or BunifuMaterialTextbox11.Text = "" Or BunifuMaterialTextbox12.Text = "" Or BunifuMaterialTextbox13.Text = "" Or BunifuMaterialTextbox14.Text = "" Or ComboBox5.SelectedIndex < 0 Then
            BunifuSnackbar1.Show(Me, "Enter all father's detail", BunifuSnackbar1.MessageTypes.Information, 5000, "", BunifuSnackbar1.Positions.MiddleCenter)
            Exit Sub
        End If

        Dim cmd As New MySqlCommand
        Dim con As New MySqlConnection
        Dim gen As String
        Try
            Dim dt As Date
            dt = BunifuDatePicker1.Value.ToString
            If BunifuRadioButton2.Checked Then
                gen = "Male"
            Else
                gen = "Female"
            End If
            cmd.CommandText = "insert into child_data (birthID, names, dob, sex, birthPlace, birthType, birthOrder,
                                staffID, facilityUID) VALUES (@a, @b, @c, @d, @e, @f, @g, @h, @i);
                               insert into mother_data (childBirthID, name, ageAtBirth, address, status, nationality, state, ethnic, job, 
                               staffID, facilityUID) VALUES (@a, @j, @k, @l, @m, @n, @o, @p, @q, @h, @i);
                               insert into father_data (childBirthID, name, ageAtBirth, address, nationality, state, ethnic, job, 
                               staffID, facilityUID) VALUES (@a, @r, @s, @t, @u, @v, @w, @x, @h, @i);
                               insert into informant_data (childBirthID, relWithChild, name, address, staffID, facilityUID)
                               VALUES (@a, @y, @z, @za, @h, @i)"

            birtid = birthID()
            cmd.Parameters.AddWithValue("@a", birtid)
            cmd.Parameters.AddWithValue("@b", bunifuMaterialTextbox1.Text.Trim)
            cmd.Parameters.AddWithValue("@c", dt.ToString("yyyy-MM-dd"))
            cmd.Parameters.AddWithValue("@d", gen)
            cmd.Parameters.AddWithValue("@e", BunifuMaterialTextbox19.Text.Trim)
            cmd.Parameters.AddWithValue("@f", ComboBox2.SelectedItem.ToString)
            cmd.Parameters.AddWithValue("@g", BunifuMaterialTextbox2.Text.ToString.Trim)
            cmd.Parameters.AddWithValue("@h", BunifuLabel2.Text)
            cmd.Parameters.AddWithValue("@i", uid.Text)
            cmd.Parameters.AddWithValue("@j", BunifuMaterialTextbox4.Text.Trim)
            cmd.Parameters.AddWithValue("@k", BunifuMaterialTextbox3.Text.ToString.Trim)
            cmd.Parameters.AddWithValue("@l", BunifuMaterialTextbox5.Text.Trim)
            cmd.Parameters.AddWithValue("@m", ComboBox4.SelectedItem.ToString)
            cmd.Parameters.AddWithValue("@n", ComboBox3.SelectedItem.ToString)
            cmd.Parameters.AddWithValue("@o", BunifuMaterialTextbox6.Text.Trim)
            cmd.Parameters.AddWithValue("@p", BunifuMaterialTextbox7.Text.Trim)
            cmd.Parameters.AddWithValue("@q", BunifuMaterialTextbox8.Text.Trim)
            cmd.Parameters.AddWithValue("@r", BunifuMaterialTextbox14.Text.Trim)
            cmd.Parameters.AddWithValue("@s", BunifuMaterialTextbox13.Text.ToString.Trim)
            cmd.Parameters.AddWithValue("@t", BunifuMaterialTextbox12.Text)
            cmd.Parameters.AddWithValue("@u", ComboBox5.SelectedItem.ToString)
            cmd.Parameters.AddWithValue("@v", BunifuMaterialTextbox11.Text.Trim)
            cmd.Parameters.AddWithValue("@w", BunifuMaterialTextbox10.Text.Trim)
            cmd.Parameters.AddWithValue("@x", BunifuMaterialTextbox9.Text.Trim)
            cmd.Parameters.AddWithValue("@y", BunifuMaterialTextbox16.Text.Trim)
            cmd.Parameters.AddWithValue("@z", BunifuMaterialTextbox15.Text.Trim)
            cmd.Parameters.AddWithValue("@za", BunifuMaterialTextbox17.Text.Trim)

            con.ConnectionString = connectstr()
            cmd.Connection = con
            con.Open()


            Dim r As Integer
            r = cmd.ExecuteNonQuery
            If r > 0 Then
                printCertificate()
                BunifuSnackbar1.Show(Me, "Birth registration successfully", BunifuSnackbar1.MessageTypes.Success, 5000, "", BunifuSnackbar1.Positions.MiddleCenter)
                clearBoxes()
            Else
                BunifuSnackbar1.Show(Me, "Birth registration was not successfull, an error occured.", BunifuSnackbar1.MessageTypes.Error, 5000, "", BunifuSnackbar1.Positions.MiddleCenter)
            End If

        Catch ex As Exception
            BunifuSnackbar1.Show(Me, ex.Message, BunifuSnackbar1.MessageTypes.Error, 5000, "", BunifuSnackbar1.Positions.MiddleCenter)
        End Try

    End Sub

    Private Sub BunifuMaterialTextbox18_OnValueChanged(sender As Object, e As EventArgs) Handles BunifuMaterialTextbox18.OnValueChanged
        If String.IsNullOrWhiteSpace(BunifuMaterialTextbox18.Text) Then
            getAllRecords()
        Else
            Dim cn As New MySqlConnection
            Dim cm As New MySqlCommand
            Dim dr As MySqlDataReader
            Dim s = BunifuMaterialTextbox18.Text
            cn.ConnectionString = connectstr()
            cm.CommandText = "Select names,birthID,sex,birthType,birthOrder, DATE_FORMAT(dob, '%D of %b, %Y')as dob from child_data where facilityUID = '" & uid.Text & "'
                             and (names like '%" & s & "%' or birthID like '%" & s & "%' or sex like '%" & s & "%' or birthType like '%" & s & "%' )"
            cm.Connection = cn
            Try
                cn.Open()
                Dim i As Integer = 1
                dr = cm.ExecuteReader
                tabledata.Rows.Clear()

                While dr.Read
                    Dim newRow3 As DataRow = tabledata.NewRow()

                    newRow3.Item("S/N") = i
                    newRow3.Item("Name") = dr("names").ToString
                    newRow3.Item("Date of Birth") = dr("dob").ToString
                    newRow3.Item("Gender") = dr("sex")
                    newRow3.Item("Birth Type") = dr("birthType")
                    newRow3.Item("Birth ID") = dr("birthID")

                    tabledata.Rows.Add(newRow3)
                    i = i + 1

                End While
                BunifuDataGridView1.DataSource = tabledata
            Catch ex As Exception
                BunifuSnackbar1.Show(Me, ex.Message, BunifuSnackbar1.MessageTypes.Error, 5000, "", BunifuSnackbar1.Positions.MiddleCenter)
            End Try

        End If
    End Sub

    Private Sub BunifuButton4_Click(sender As Object, e As EventArgs) Handles BunifuButton4.Click
        If BunifuDataGridView1.SelectedRows.Count = 1 Then
            BunifuButton4.Enabled = False
            BunifuButton4.Enabled = False
            reprintCertificate(BunifuDataGridView1.SelectedRows(0).Cells(1).Value.ToString())
            BunifuButton4.Enabled = True
            BunifuSnackbar1.Show(Me, "Certificate Reprinted Successfully", BunifuSnackbar1.MessageTypes.Success, 5000, "", BunifuSnackbar1.Positions.MiddleCenter)

        End If
    End Sub
End Class