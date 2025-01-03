Imports ClosedXML.Excel
Imports MySql.Data.MySqlClient

Public Class frmRepProduccion
    Private Sub rbTodos_CheckedChanged(sender As Object, e As EventArgs) Handles rbTodos.CheckedChanged
        If (rbTodos.Checked) Then
            Dim fecha As Date = Nothing
            Dim f As String = ""

            Dim cnn1 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
            Dim rd1 As MySqlDataReader
            Dim cmd1 As MySqlCommand

            grddatos.Rows.Clear()
            rbEmpelado.Checked = False
            cboEmpleado.Enabled = False
            dtpIncial.Enabled = False
            dtpFinal.Enabled = False
            Try
                Dim tota As Double = 0
                cnn1.Close() : cnn1.Open()
                cmd1 = cnn1.CreateCommand
                cmd1.CommandText = "SELECT Codigo,Descripcion,Florista,Cantidad,FElaboracion,Usuario FROM produccion ORDER BY Florista"
                rd1 = cmd1.ExecuteReader
                If rd1.HasRows Then
                    Do While rd1.Read
                        fecha = rd1(4).ToString
                        f = Format(fecha, "yyyy-MM-dd HH:mm:ss")

                        grddatos.Rows.Add(rd1(0).ToString,
                                          rd1(1).ToString,
                                          rd1(2).ToString,
                                          rd1(3).ToString,
                                          f,
                                          rd1(5).ToString)
                        tota = tota + CDec(rd1(3).ToString)
                    Loop
                End If
                rd1.Close()
                cnn1.Close()
                lblTotal.Text = FormatNumber(tota, 2)
            Catch ex As Exception
                MessageBox.Show(ex.ToString)
                cnn1.Close()
            End Try
        End If
    End Sub

    Private Sub rbEmpelado_CheckedChanged(sender As Object, e As EventArgs) Handles rbEmpelado.CheckedChanged
        If (rbEmpelado.Checked) Then
            grddatos.Rows.Clear()
            rbTodos.Checked = False
            cboEmpleado.Enabled = True
            dtpIncial.Enabled = True
            dtpFinal.Enabled = True
            cboEmpleado.Focus.Equals(True)
        End If
    End Sub

    Private Sub cboEmpleado_DropDown(sender As Object, e As EventArgs) Handles cboEmpleado.DropDown

        Dim cnn5 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
        Dim rd5 As MySqlDataReader
        Dim cmd5 As MySqlCommand

        Try
            cboEmpleado.Items.Clear()
            cnn5.Close() : cnn5.Open()
            cmd5 = cnn5.CreateCommand
            cmd5.CommandText = "SELECT DISTINCT Nombre FROM usuarios WHERE Nombre<>'' ORDER BY Nombre"
            rd5 = cmd5.ExecuteReader
            If rd5.HasRows Then
                Do While rd5.Read
                    cboEmpleado.Items.Add(rd5(0).ToString)
                Loop
            End If
            rd5.Close()
            cnn5.Close()

        Catch ex As Exception
            MessageBox.Show(ex.ToString)
            cnn5.Close()
        End Try
    End Sub

    Private Sub btnSalir_Click(sender As Object, e As EventArgs) Handles btnSalir.Click
        Me.Close()
    End Sub

    Private Sub btnLimpiar_Click(sender As Object, e As EventArgs) Handles btnLimpiar.Click
        grddatos.Rows.Clear()
        dtpIncial.Value = Date.Now
        dtpFinal.Value = Date.Now
        cboEmpleado.Text = ""
        rbEmpelado.Checked = False
        rbTodos.Checked = False

        dtpIncial.Enabled = False
        dtpFinal.Enabled = False
        cboEmpleado.Enabled = False
    End Sub

    Private Sub btnExportar_Click(sender As Object, e As EventArgs) Handles btnExportar.Click
        ExportarDataGridViewAExcel(grddatos)
    End Sub

    Public Sub ExportarDataGridViewAExcel(dgv As DataGridView)
        If grddatos.Rows.Count = 0 Then MsgBox("Genera el reporte para poder exportar los datos a Excel.", vbInformation + vbOKOnly, titulocentral) : Exit Sub
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

    Private Sub btnReporte_Click(sender As Object, e As EventArgs) Handles btnReporte.Click

        Dim cnn1 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
        Dim rd1 As MySqlDataReader
        Dim cmd1 As MySqlCommand

        Try
            Dim fecha As Date = Nothing
            Dim f As String = ""
            cnn1.Close() : cnn1.Open()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "SELECT Codigo,Descripcion,Florista,Cantidad,FElaboracion,Usuario FROM produccion WHERE Florista='" & cboEmpleado.Text & "' AND Fecha BETWEEN '" & Format(dtpIncial.Value, "yyyy-MM-dd") & "' AND '" & Format(dtpFinal.Value, "yyyy-MM-dd") & "' ORDER BY Florista"
            rd1 = cmd1.ExecuteReader
            If rd1.HasRows Then
                Do While rd1.Read
                    fecha = rd1(4).ToString
                    f = Format(fecha, "yyyy-MM-dd HH:mm:ss")
                    grddatos.Rows.Add(rd1(0).ToString,
                                      rd1(1).ToString,
                                      rd1(2).ToString,
                                      rd1(3).ToString,
                                     f,
                                      rd1(5).ToString)
                Loop
            End If
            rd1.Close()
            cnn1.Close()

        Catch ex As Exception
            MessageBox.Show(ex.ToString)
            cnn1.Close()
        End Try
    End Sub

    Private Sub cboEmpleado_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cboEmpleado.KeyPress
        e.KeyChar = UCase(e.KeyChar)
        If AscW(e.KeyChar) = Keys.Enter Then
            btnReporte.Focus.Equals(True)
        End If
    End Sub

    Private Sub frmRepProduccion_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class