Imports ClosedXML.Excel

Public Class frmRepAuditoria
    Private Sub frmRepAuditoria_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        cbofiltro.Text = ""
    End Sub

    Private Sub cbofiltro_DropDown(sender As Object, e As EventArgs) Handles cbofiltro.DropDown
        Try
            cbofiltro.Items.Clear()

            cnn5.Close() : cnn5.Open()
            cmd5 = cnn5.CreateCommand

            If (rbexistencias.Checked) Then
                cmd5.CommandText = "SELECT DISTINCT Usuario FROM cardex WHERE Usuario<>'' ORDER BY Usuario"
            End If

            If (rbprecios.Checked) Then
                cmd5.CommandText = "SELECT DISTINCT Usuario FROM modprecios WHERE Usuario<>'' ORDER BY Usuario"
            End If

            rd5 = cmd5.ExecuteReader
            If rd5.HasRows Then
                If rd5.Read Then
                    cbofiltro.Items.Add(rd5(0).ToString)
                End If
            End If
            rd5.Close()
            cnn5.Close()

        Catch ex As Exception
            MessageBox.Show(ex.ToString)
            cnn5.Close()
        End Try
    End Sub

    Private Sub btnReporte_Click(sender As Object, e As EventArgs) Handles btnReporte.Click
        Try
            grdCaptura.Rows.Clear()

            Dim M1 As Date = mcdesde.SelectionStart.ToShortDateString
            Dim M2 As Date = mchasta.SelectionStart.ToShortDateString

            If (rbexistencias.Checked) Then

                cnn1.Close() : cnn1.Open()
                cmd1 = cnn1.CreateCommand

                If cbofiltro.Text = "" Then
                    cmd1.CommandText = "SELECT Codigo,Nombre,Movimiento,Final,Fecha,Usuario FROM cardex WHERE Fecha between '" & Format(M1, "yyyy-MM-dd 00:00:00") & "' and '" & Format(M2, "yyyy-MM-dd 23:59:59") & "' order by Id"
                Else
                    cmd1.CommandText = "SELECT Codigo,Nombre,Movimiento,Final,Fecha,Usuario FROM cardex WHERE Fecha between '" & Format(M1, "yyyy-MM-dd 00:00:00") & "' and '" & Format(M2, "yyyy-MM-dd 23:59:59") & "' AND Usuario='" & cbofiltro.Text & "' order by Id"
                End If

                rd1 = cmd1.ExecuteReader
                Do While rd1.Read
                    If rd1.HasRows Then

                        grdCaptura.Rows.Add(rd1("Codigo").ToString,
                                            rd1("Nombre").ToString,
                                            rd1("Movimiento").ToString,
                                            rd1("Final").ToString,
                                            rd1("Fecha").ToString,
                                            rd1("Usuario").ToString
)

                    End If
                Loop
                rd1.Close()
                cnn1.Close()

            End If

            If (rbprecios.Checked) Then

                cnn1.Close() : cnn1.Open()
                cmd1 = cnn1.CreateCommand

                If cbofiltro.Text = "" Then
                    cmd1.CommandText = "SELECT Codigo,Nombre,Nuevo,Fecha,Usuario FROM modprecios WHERE Fecha between '" & Format(M1, "yyyy-MM-dd 00:00:00") & "' and '" & Format(M2, "yyyy-MM-dd 23:59:59") & "' order by Id"
                Else
                    cmd1.CommandText = "SELECT Codigo,Nombre,Nuevo,Fecha,Usuario FROM modprecios WHERE Fecha between '" & Format(M1, "yyyy-MM-dd 00:00:00") & "' and '" & Format(M2, "yyyy-MM-dd 23:59:59") & "' AND Usuario='" & cbofiltro.Text & "' order by Id"
                End If

                rd1 = cmd1.ExecuteReader
                Do While rd1.Read
                    If rd1.HasRows Then

                        grdCaptura.Rows.Add(rd1("Codigo").ToString,
                                            rd1("Nombre").ToString,
                                            rd1("Nuevo").ToString,
                                            rd1("Fecha").ToString,
                                            rd1("Usuario").ToString()
                        )

                    End If
                Loop
                rd1.Close()
                cnn1.Close()
            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
    End Sub

    Private Sub rbexistencias_CheckedChanged(sender As Object, e As EventArgs) Handles rbexistencias.CheckedChanged

        If (rbexistencias.Checked) Then
            cbofiltro.Text = ""
            grdCaptura.Rows.Clear()
            grdCaptura.ColumnCount = 0

            grdCaptura.ColumnCount = 6
            With grdCaptura
                With .Columns(0)
                    .HeaderText = "Codigo"
                    .Width = 80
                    .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                    .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                    .Visible = True
                    .Resizable = DataGridViewTriState.False
                End With

                With .Columns(1)
                    .HeaderText = "Descripción"
                    .Width = 80
                    .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                    .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                    .Visible = True
                    .Resizable = DataGridViewTriState.False
                End With

                With .Columns(2)
                    .HeaderText = "Movimiento"
                    .Width = 80
                    .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                    .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                    .Visible = True
                    .Resizable = DataGridViewTriState.False
                End With

                With .Columns(3)
                    .HeaderText = "Cantidad Actualizada"
                    .Width = 80
                    .AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                    .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                    .Visible = True
                    .Resizable = DataGridViewTriState.False
                End With

                With .Columns(4)
                    .HeaderText = "Fecha"
                    .Width = 80
                    .AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                    .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                    .Visible = True
                    .Resizable = DataGridViewTriState.False
                End With

                With .Columns(5)
                    .HeaderText = "Usuario"
                    .Width = 80
                    .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                    .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                    .Visible = True
                    .Resizable = DataGridViewTriState.False
                End With

            End With

        End If
    End Sub

    Private Sub rbprecios_CheckedChanged(sender As Object, e As EventArgs) Handles rbprecios.CheckedChanged
        If (rbprecios.Checked) Then
            cbofiltro.Text = ""
            grdCaptura.Rows.Clear()
            grdCaptura.ColumnCount = 0

            grdCaptura.ColumnCount = 5
            With grdCaptura
                With .Columns(0)
                    .HeaderText = "Codigo"
                    .Width = 80
                    .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                    .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                    .Visible = True
                    .Resizable = DataGridViewTriState.False
                End With

                With .Columns(1)
                    .HeaderText = "Descripción"
                    .Width = 80
                    .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                    .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                    .Visible = True
                    .Resizable = DataGridViewTriState.False
                End With

                With .Columns(2)
                    .HeaderText = "Precio Nuevo"
                    .Width = 80
                    .AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                    .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                    .Visible = True
                    .Resizable = DataGridViewTriState.False
                End With

                With .Columns(3)
                    .HeaderText = "Fecha"
                    .Width = 80
                    .AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                    .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                    .Visible = True
                    .Resizable = DataGridViewTriState.False
                End With

                With .Columns(4)
                    .HeaderText = "Usuario"
                    .Width = 80
                    .AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                    .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                    .Visible = True
                    .Resizable = DataGridViewTriState.False
                End With
            End With
        End If
    End Sub

    Private Sub btnNuevo_Click(sender As Object, e As EventArgs) Handles btnNuevo.Click
        grdCaptura.Rows.Clear()
        cbofiltro.Text = ""
        rbexistencias.Checked = True
        rbprecios.Checked = False
    End Sub

    Private Sub btnexportar_Click(sender As Object, e As EventArgs) Handles btnexportar.Click
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