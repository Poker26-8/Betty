Public Class frmDetalleCH
    Private Sub frmDetalleCH_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        If (frmCitasH.optDia.Checked) Then
            grdCaptura.Rows.Clear()
            grdCaptura.ColumnCount = 0

            grdCaptura.ColumnCount = 4
            With grdCaptura.Columns(0)
                .Name = "Id"
                .Width = 50
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Visible = False
                .Resizable = DataGridViewTriState.False
            End With
            With grdCaptura.Columns(1)
                .Name = "Hora"
                .Width = 60
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Visible = True
                .Resizable = DataGridViewTriState.False
            End With
            With grdCaptura.Columns(2)
                .Name = "Evento"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                .Visible = True
                .Resizable = DataGridViewTriState.False
            End With
            With grdCaptura.Columns(3)
                .Name = "Activo"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                .Visible = False
                .Resizable = DataGridViewTriState.False
            End With

            Dim minuto As String = ""
            Dim refec As String = Mid(txtReferencia.Text, 1, 2)
            Dim hora As String = ""

            Try

            Catch ex As Exception
                MessageBox.Show(ex.ToString)
                cnn1.Close()
            End Try
        End If

    End Sub
End Class