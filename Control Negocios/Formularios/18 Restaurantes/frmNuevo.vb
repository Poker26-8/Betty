Imports System.IO
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Public Class frmNuevo

    Friend WithEvents btnMesa As System.Windows.Forms.Button
    Dim btnaccion = New DataGridViewButtonColumn()
    Dim btnaccion2 = New DataGridViewTextBoxCell()
    Dim rowIndex As Integer = 0 ' 
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
End Class