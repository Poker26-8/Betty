Imports ClosedXML.Excel



Public Class frmCumple
    Dim telefono As String = ""
    Private Sub frmCumple_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        traeMes()
    End Sub
    Public Sub traeMes()
        cboMes.Items.Clear()
        For x As Integer = 1 To 12
            cboMes.Items.Add(New DateTime(1, x, 1).ToString("MMMM").ToUpper)
        Next
        cboMes.SelectedIndex = 0

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Try
            Button2.Enabled = False
            Dim mesesEspañolAIngles As New Dictionary(Of String, String) From {
    {"enero", "January"}, {"febrero", "February"}, {"marzo", "March"},
    {"abril", "April"}, {"mayo", "May"}, {"junio", "June"},
    {"julio", "July"}, {"agosto", "August"}, {"septiembre", "September"},
    {"octubre", "October"}, {"noviembre", "November"}, {"diciembre", "December"}
}

            Dim mesEnIngles As String = mesesEspañolAIngles(cboMes.Text.ToLower())
            grdCaptura.Rows.Clear()
            cnn1.Close()
            cnn1.Open()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "Select * from Monedero where MONTHNAME(Cumple)='" & mesEnIngles & "'"
            rd1 = cmd1.ExecuteReader
            Do While rd1.Read
                Dim fecha As Date = rd1("Cumple").ToString
                Dim fechaformateada As String = Format(fecha, "dd-MM-yyyy")
                grdCaptura.Rows.Add(rd1("Cliente").ToString, rd1("Barras").ToString, fechaformateada)
            Loop
            rd1.Close()
            cnn1.Close()
            Button2.Enabled = True
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
            grdCaptura.Rows.Clear()
            Button2.Enabled = True
        End Try
    End Sub

    Private Sub btnuevo_Click(sender As Object, e As EventArgs) Handles btnuevo.Click
        grdCaptura.Rows.Clear()
        cboMes.SelectedIndex = 0
        Button2.Enabled = True
        Exportar.Enabled = True
    End Sub

    Private Sub Exportar_Click(sender As Object, e As EventArgs) Handles Exportar.Click
        Try
            If grdCaptura.Rows.Count = 0 Then
                MsgBox("Genera el reporte para poder exportar a un documento de Excel", vbInformation + vbOKOnly, "Delsscom Control Negocios Pro")
                Exit Sub
            End If
            ExportarDataGridViewAExcel(grdCaptura)
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
            Exportar.Enabled = True
        End Try

    End Sub

    Public Sub ExportarDataGridViewAExcel(dgv As DataGridView)
        If grdCaptura.Rows.Count = 0 Then MsgBox("Genera el reporte para poder exportar los datos a Excel.", vbInformation + vbOKOnly, titulocentral) : Exit Sub
        If MsgBox("¿Deseas exportar la información a un archivo de Excel?", vbInformation + vbOKCancel, "Delsscom Control Negocios Pro") = vbOK Then
            Exportar.Enabled = False
            Dim voy As Integer = 0
            Using workbook As New XLWorkbook()
                Dim worksheet As IXLWorksheet = workbook.Worksheets.Add("Datos")
                For colIndex As Integer = 0 To dgv.Columns.Count - 1
                    Dim headerCell As IXLCell = worksheet.Cell(1, colIndex + 1)
                    worksheet.Cell(1, colIndex + 1).Value = dgv.Columns(colIndex).HeaderText
                    headerCell.Value = dgv.Columns(colIndex).HeaderText
                    headerCell.Style.Font.Bold = True  ' Aplica negrita a los encabezados
                Next
                For rowIndex As Integer = 0 To dgv.Rows.Count - 1
                    For colIndex As Integer = 0 To dgv.Columns.Count - 1
                        Dim cellValue As Object = dgv.Rows(rowIndex).Cells(colIndex).Value
                        Dim cellValueString As String = If(cellValue Is Nothing, String.Empty, cellValue.ToString())
                        Dim cell As IXLCell = worksheet.Cell(rowIndex + 2, colIndex + 1)
                        worksheet.Cell(rowIndex + 2, colIndex + 1).Value = cellValueString
                        cell.Value = cellValueString
                        If worksheet.Cell(rowIndex + 2, colIndex + 1).Value = cellValueString Then
                            Dim number As Double
                            If Double.TryParse(cellValueString, number) Then
                                cell.Value = number
                                cell.Style.NumberFormat.Format = "0"
                            Else
                                cell.Style.NumberFormat.Format = "@"
                            End If
                        Else
                            cell.Style.NumberFormat.Format = "@"
                        End If
                    Next
                    voy = voy + 1
                    My.Application.DoEvents()
                Next
                worksheet.Columns().AdjustToContents()
                Using memoryStream As New System.IO.MemoryStream()
                    workbook.SaveAs(memoryStream)
                    Dim tempFilePath As String = IO.Path.GetTempPath() & Guid.NewGuid().ToString() & ".xlsx"
                    System.IO.File.WriteAllBytes(tempFilePath, memoryStream.ToArray())
                    Process.Start(tempFilePath)
                End Using
            End Using
            Exportar.Enabled = True
            MessageBox.Show("Datos exportados exitosamente")
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            If MsgBox("¿Deseas enviar un mensaje de WhatsApp al numero seleccionado:?" & telefono, vbQuestion + vbOKCancel, "Delsscom Control Negocios Pro") = vbCancel Then
                Exit Sub
            End If
            AbrirWhatsApp()
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
    End Sub
    Private Sub AbrirWhatsApp()
        Try
            If telefono <> "" Then
                Dim numero As String = telefono
                numero = numero.Replace(" ", "").Replace("+", "").Replace("-", "")

                If IsNumeric(numero) AndAlso numero.Length >= 10 Then
                    Dim url As String = $"https://wa.me/{numero}"
                    Process.Start(New ProcessStartInfo(url) With {.UseShellExecute = True})
                Else
                    MessageBox.Show("El número de teléfono no es válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If
            Else
                MessageBox.Show("Por favor, selecciona una fila.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If
        Catch ex As Exception
            MessageBox.Show($"Ocurrió un error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub grdCaptura_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles grdCaptura.CellClick
        Dim celda As DataGridViewCellEventArgs = e
        If celda.ColumnIndex = 1 Then
            telefono = grdCaptura.CurrentRow.Cells(1).Value.ToString
        End If
    End Sub
End Class