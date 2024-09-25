Imports System.IO
Imports ClosedXML.Excel.XLPredefinedFormat
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Public Class frmNuevo

    Friend WithEvents btnMesa As System.Windows.Forms.Button
    Dim btnaccion = New DataGridViewButtonColumn()
    Dim btnaccion2 = New DataGridViewTextBoxCell()
    Dim rowIndex As Integer = 0

    Dim numcompleto As String = ""

    Private Sub frmNuevo_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub btnPdf_Click(sender As Object, e As EventArgs) Handles btnPdf.Click

        DataGridView1.Rows.Clear()

        Dim id As String = ""
        Dim codigo As String = ""
        Dim codunico As String = ""
        cnn1.Close() : cnn1.Open()
        cnn2.Close() : cnn2.Open()
        cmd1 = cnn1.CreateCommand
        cmd1.CommandText = "SELECT Id,Codigo,CodUnico FROM ventasdetalle WHERE Folio=21"
        rd1 = cmd1.ExecuteReader
        Do While rd1.Read
            If rd1.HasRows Then
                id = rd1(0).ToString
                codigo = rd1(1).ToString
                codunico = rd1(2).ToString

                DataGridView1.Rows.Add(id, codigo, codunico)
            End If
        Loop
        rd1.Close()

        Dim cunico As String = ""
        Dim ideli As Integer = 0
        Dim suma As Integer = 0
        For LUFFY As Integer = 0 To DataGridView1.Rows.Count - 1
            cunico = DataGridView1.Rows(LUFFY).Cells(2).Value.ToString

            cmd2 = cnn2.CreateCommand
            cmd2.CommandText = "SELECT COUNT(CodUnico),Id from ventasdetalle WHERE CodUnico='" & cunico & "' AND Folio='21'"
            rd2 = cmd2.ExecuteReader
            If rd2.HasRows Then
                If rd2.Read Then
                    suma = rd2(0).ToString
                    ideli = rd2(1).ToString
                    If suma > 1 Then
                        cnn3.Close() : cnn3.Open()
                        cmd3 = cnn3.CreateCommand
                        cmd3.CommandText = "DELETE FROM ventasdetalle WHERE Id=" & ideli & " AND CodUnico='" & cunico & "'"
                        cmd3.ExecuteNonQuery()
                        cnn3.Close()
                    End If
                End If
            Else

            End If
            rd2.Close()

        Next
        cnn1.Close()
        cnn2.Close()

    End Sub

    Private Sub ComboBox1_DropDown(sender As Object, e As EventArgs) Handles ComboBox1.DropDown
        Try
            ComboBox1.Items.Clear()

            cnn5.Close() : cnn5.Open()
            cmd5 = cnn5.CreateCommand
            cmd5.CommandText = "SELECT DISTINCT Codigo FROM productos WHERE Codigo<>'' ORDER BY Codigo"
            rd5 = cmd5.ExecuteReader
            Do While rd5.Read
                If rd5.HasRows Then
                    ComboBox1.Items.Add(rd5(0).ToString)
                End If
            Loop
            rd5.Close()
            cnn5.Close()

        Catch ex As Exception
            MessageBox.Show(ex.ToString)
            cnn5.Close()
        End Try
    End Sub

    Private Sub ComboBox1_SelectedValueChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedValueChanged
        Try
            cnn1.Close() : cnn1.Open()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "SELECT CodBarra FROM productos WHERE Codigo='" & ComboBox1.Text & "'"
            rd1 = cmd1.ExecuteReader
            If rd1.HasRows Then
                If rd1.Read Then
                    txtbarras.Text = rd1(0).ToString
                End If
            End If
            rd1.Close()
            cnn1.Close()
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
            cnn1.Close()
        End Try
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click



        Dim peso As String = txtpeso.Text
        Dim numeroSinPunto As String = peso.ToString().Replace(".", "")

        Dim resultado As String = ""
        Dim barras As String = txtbarras.Text

        If barras.Length = 2 Then
            resultado = "0" & barras
        Else
            resultado = barras
        End If

        numcompleto = txtInicial.Text & txtnumfijo.Text & resultado & txtfijo.Text & numeroSinPunto & txtalazar.Text
        txtticket.Text = numcompleto

        '55200010200295520001060030572000038005501

        'hay que cortar los primeros 2 digitos

        Dim primeros2num As String = txtconvertir.Text


    End Sub
End Class