Imports ClosedXML.Excel
Imports MySql.Data.MySqlClient

Public Class frmRepMovCuentas
    Private Sub frmRepMovCuentas_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        grdCaptura.Rows.Clear()
        mcdesde.SetDate(Now)
        mchasta.SetDate(Now)
        dtpinicio.Text = "00:00:00"
        dtpFin.Text = "23:59:59"

        rbTodos.Checked = True
    End Sub

    Private Sub btnReporte_Click(sender As Object, e As EventArgs) Handles btnReporte.Click
        Try
            grdCaptura.Rows.Clear()
            Dim m1 As Date = mcdesde.SelectionStart.ToShortDateString
            Dim m2 As Date = mchasta.SelectionStart.ToShortDateString

            Dim formapago As String = ""
            Dim banco As String = ""
            Dim referencia As String = ""
            Dim concepto As String = ""
            Dim total As Double = 0
            Dim retiro As Double = 0
            Dim deposito As Double = 0
            Dim saldo As Double = 0
            Dim fecha As Date = Nothing
            Dim hora As String = Nothing
            Dim folio As String = ""
            Dim comentario As String = ""
            Dim cunta As String = ""
            Dim bancoc As String = ""
            Dim cliente As String = ""

            Dim fechan As String = ""

            Dim cnn1 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
            Dim rd1 As MySqlDataReader
            Dim cmd1 As MySqlCommand

            If (rbTodos.Checked) Then

                cnn1.Close() : cnn1.Open()
                cmd1 = cnn1.CreateCommand
                cmd1.CommandText = "SELECT Tipo,Banco,Referencia,Concepto,Total,Retiro,Deposito,Saldo,Fecha,Hora,Folio,Comentario,Cuenta,BancoCuenta,Cliente FROM movcuenta WHERE Fecha BETWEEN '" & Format(m1, "yyyy-MM-dd") & "' AND '" & Format(m2, "yyyy-MM-dd") & "' AND Hora BETWEEN '" & Format(dtpinicio.Value, "HH:mm:ss") & "' AND '" & Format(dtpFin.Value, "HH:mm:ss") & "'"
                rd1 = cmd1.ExecuteReader
                Do While rd1.Read
                    If rd1.HasRows Then

                        formapago = rd1("Tipo").ToString
                        banco = rd1("Banco").ToString
                        referencia = rd1("Referencia").ToString
                        concepto = rd1("Concepto").ToString
                        total = rd1("Total").ToString
                        retiro = rd1("Retiro").ToString
                        deposito = rd1("Deposito").ToString
                        saldo = rd1("Saldo").ToString
                        fecha = rd1("Fecha").ToString
                        hora = rd1("Hora").ToString
                        folio = rd1("Folio").ToString
                        comentario = rd1("Comentario").ToString
                        cunta = rd1("Cuenta").ToString
                        bancoc = rd1("BancoCuenta").ToString
                        fechan = Format(fecha, "yyyy-MM-dd")
                        cliente = rd1("Cliente").ToString

                        grdCaptura.Rows.Add(formapago,
                                            banco,
                                            referencia,
                                            concepto,
                                            FormatNumber(total, 2),
                                            FormatNumber(retiro, 2),
                                            FormatNumber(deposito, 2),
                                            FormatNumber(saldo, 2),
                                            fechan,
                                            hora,
                                            folio,
                                            cliente,
                                            comentario,
                                            cunta,
                                            bancoc)
                    End If
                Loop
                rd1.Close()
                cnn1.Close()

            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString)
            cnn1.Close()

        End Try
    End Sub

    Private Sub btnNuevo_Click(sender As Object, e As EventArgs) Handles btnNuevo.Click
        grdCaptura.Rows.Clear()
    End Sub

    Private Sub btnExcel_Click(sender As Object, e As EventArgs) Handles btnExcel.Click
        ExportarDataGridViewAExcel(grdCaptura)
    End Sub

    Public Sub ExportarDataGridViewAExcel(dgv As DataGridView)
        If grdCaptura.Rows.Count = 0 Then MsgBox("Genera el reporte para poder exportar los datos a Excel.", vbInformation + vbOKOnly, titulocentral) : Exit Sub
        If MsgBox("¿Deseas exportar la información a un archivo de Excel?", vbInformation + vbOKCancel, "Delsscom Control Negocios Pro") = vbOK Then

            Dim voy As Integer = 0
            ' Crea un nuevo libro de trabajo de Excel
            Using workbook As New XLWorkbook()

                ' Añade una nueva hoja de trabajo
                Dim worksheet As IXLWorksheet = workbook.Worksheets.Add("Datos")

                ' Escribe los encabezados de columna
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
                        worksheet.Cell(rowIndex + 2, colIndex + 1).Value = cellValueString
                        Dim cell As IXLCell = worksheet.Cell(rowIndex + 2, colIndex + 1)
                        cell.Value = cellValueString
                        cell.Style.NumberFormat.Format = "@"
                    Next
                    voy = voy + 1

                    My.Application.DoEvents()
                Next

                worksheet.Columns().AdjustToContents()
                ' Usa MemoryStream para guardar el archivo en memoria y abrirlo
                Using memoryStream As New System.IO.MemoryStream()
                    ' Guarda el libro de trabajo en el MemoryStream
                    workbook.SaveAs(memoryStream)

                    ' Guarda el MemoryStream en un archivo temporal para abrirlo
                    Dim tempFilePath As String = IO.Path.GetTempPath() & Guid.NewGuid().ToString() & ".xlsx"
                    System.IO.File.WriteAllBytes(tempFilePath, memoryStream.ToArray())

                    ' Abre el archivo temporal en Excel
                    Process.Start(tempFilePath)
                End Using

                'workbook.SaveAs(filePath)
            End Using
            MessageBox.Show("Datos exportados exitosamente")

        End If
    End Sub
End Class