Public Class frmRegalos
    Private Sub grdRegalo_CurrentCellDirtyStateChanged(sender As Object, e As EventArgs) Handles grdRegalo.CurrentCellDirtyStateChanged


        If grdRegalo.IsCurrentCellDirty Then
            grdRegalo.CommitEdit(DataGridViewDataErrorContexts.Commit)
        End If
    End Sub

    Private Sub grdRegalo_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles grdRegalo.CellValueChanged
        If e.ColumnIndex = 4 AndAlso e.RowIndex >= 0 Then
            Dim currentValue As Boolean = CBool(grdRegalo.Rows(e.RowIndex).Cells(4).Value) ' Columna del CheckBox
            Dim currentValueCol1 As String = grdRegalo.Rows(e.RowIndex).Cells(0).Value.ToString() ' Columna 1

            ' Si el CheckBox está seleccionado, deselecciona otros con la misma Columna 1
            If currentValue Then
                For Each row As DataGridViewRow In grdRegalo.Rows
                    If row.Index <> e.RowIndex AndAlso row.Cells(0).Value.ToString() = currentValueCol1 Then
                        row.Cells(4).Value = False
                    End If
                Next
            End If
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        For xxx As Integer = 0 To grdRegalo.Rows.Count - 1
            If grdRegalo.Rows(xxx).Cells(4).Value Then
                Dim varcodunico As String = Format(CDate(Date.Now), "yyyy/MM/ddHH:mm:ss.fff") & grdRegalo.Rows(xxx).Cells(1).Value.ToString
                varcodunico = QuitarCaracteresEspeciales(varcodunico)
                frmVentas3.grdcaptura.Rows.Add(grdRegalo.Rows(xxx).Cells(1).Value.ToString, grdRegalo.Rows(xxx).Cells(3).Value.ToString, "pza", 1, FormatNumber(grdRegalo.Rows(xxx).Cells(5).Value, 2), 0.00, 0, 0, "", "", 0.00, 0.00, 0.00, 0.00, 0, varcodunico)

            End If
        Next

        Me.Close()
    End Sub
    Function QuitarCaracteresEspeciales(ByVal input As String) As String
        ' Utilizamos una expresión regular para reemplazar todos los caracteres que no son letras o números.
        Dim regex As New System.Text.RegularExpressions.Regex("[^a-zA-Z0-9]")
        Return regex.Replace(input, String.Empty)
    End Function

End Class