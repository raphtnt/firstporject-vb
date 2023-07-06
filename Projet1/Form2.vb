Imports Microsoft.Office.Interop
Imports OracleConnection = System.Data.OracleClient.OracleConnection

Public Class Form2

    Dim v(9) As String
    Dim l() As String = {"prenom", "nom", "email", "address", "ville", "phone", "country", "postal", "dates"}
    Dim prenom() As String = {"Jean", "Raphael", "Maxime", "Dylan", "Autres", "JSP", "Pourquoi", "Pas", "..."}

    Dim t(999999) As String

    Dim APP As New Excel.Application
    Dim worksheet As Excel.Worksheet
    Dim workbook As Excel.Workbook
    Dim oBook As Object
    Dim sheet

    Dim oradb As String = "User Id=" + "VB" + ";Password=" & "VB" + ";Data Source=" + "" + ";"

    Private Function searchData(type As String) As String
        Dim result As String
        Dim conn As New OracleConnection(oradb)
        conn.Open()

        Dim cmd As New OracleClient.OracleCommand
        cmd.Connection = conn
        cmd.CommandText = "select " & type & " from TABLE1 where id = ROUND(DBMS_RANDOM.VALUE(1, 10))"
        cmd.CommandType = CommandType.Text

        Dim dr As OracleClient.OracleDataReader = cmd.ExecuteReader()
        dr.Read()

        result = dr.Item(type)

        conn.Dispose()

        Return result

    End Function


    ' Load Menu
    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Randomize()

        DataGridView1.Columns.Add("0", "0")
        DataGridView1.Update()

        ComboBox1.Items.Clear()
        ComboBox1.Items.Add("firstname")
        ComboBox1.Items.Add("lastname")
        ComboBox1.Items.Add("Pays")
        ComboBox1.Items.Add("Ville")
    '    ComboBox1.Items.Add("Ville de Belgique")
        ComboBox1.Text = "Select from..."

    End Sub

    ' Close Menu
    Private Sub Form2_Closed(sender As Object, e As EventArgs) Handles MyBase.Closed
        MsgBox("EXIT")
        If workbook IsNot Nothing Then
            workbook.Close()
        End If
    End Sub

    ' Nb Columns
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If TextBox2.Text = Nothing Then
            MsgBox("Veuillez entrez un nombre de columns valide!")
        Else
            ' DataGridView1.Columns.Clear()
            For index As Integer = 1 To TextBox2.Text
                DataGridView1.Columns.Add(index, index)
            Next
        End If

    End Sub

    ' GetDATA
    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click

        workbook = APP.Workbooks.Open(TextBox9.Text)
        APP.Workbooks.Add()
        worksheet = workbook.Worksheets(Val(TextBox10.Text))

        Dim ListItem2 As ListViewItem
        Dim vals As Integer
        vals = DataGridView1.RowCount - 1
        For indexs As Integer = 0 To DataGridView1.ColumnCount - 1
            For index As Integer = 0 To vals
                If worksheet.Cells(index + 1, indexs + 1).Value IsNot Nothing Then
                    DataGridView1.Rows(index).Cells(indexs).Value = worksheet.Cells(index + 1, indexs + 1).Value.ToString
                End If
            Next
        Next


    End Sub


    ' Save
    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        Dim vals As Integer
        vals = DataGridView1.RowCount - 2
        For indexs As Integer = 0 To DataGridView1.ColumnCount - 1
            For index As Integer = 0 To vals
                If DataGridView1.Rows(index).Cells(indexs).Value IsNot Nothing Then
                    worksheet.Cells(index + 1, indexs + 1).Value = DataGridView1.Rows(index).Cells(indexs).Value.ToString()
                Else
                    worksheet.Cells(index + 1, indexs + 1).Value = " "
                End If
            Next
        Next
        workbook.Save()
        MsgBox("Le fichier excel à été sauvegardé !")
    End Sub

    ' Generate Data
    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        Dim lowerbound As Integer = 1
        Dim upperbound As Integer = 5
        Dim row As Integer = TextBox5.Text
        For index As Integer = 0 To TextBox7.Text - 1
            Dim random As Integer = CInt(Math.Floor((upperbound - lowerbound + 1) * Rnd())) + lowerbound
            DataGridView1.Rows(row).Cells(TextBox6.Text).Value = searchData(ComboBox1.Text)
            '  DataGridView1.Rows(row).Cells(TextBox6.Text).Value = prenom(random)
            DataGridView1.Update()
            row = row + 1
        Next
    End Sub

    ' NB Row
    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        If TextBox8.Text = Nothing Then
            MsgBox("Veuillez entrez un nombre de row valide!")
        Else
            'DataGridView1.Columns.Clear()
            For index As Integer = 1 To TextBox8.Text
                DataGridView1.Rows.Add(index)
            Next
        End If
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged

    End Sub

    Private Sub TextBox9_TextChanged(sender As Object, e As EventArgs) Handles TextBox9.TextChanged

    End Sub
End Class
