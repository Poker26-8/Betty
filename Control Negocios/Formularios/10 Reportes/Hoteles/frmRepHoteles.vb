Imports ClosedXML.Excel
Imports MySql.Data.MySqlClient

Public Class frmRepHoteles
    Private Sub frmRepHoteles_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        rbHabitacion.Checked = True
        grdCaptura.Rows.Clear()
        cboDatos.Text = ""
        dtpinicio.Text = "00:00:00"
        dtpFin.Text = "23:59:59"
    End Sub

    Private Sub rbHabitacion_CheckedChanged(sender As Object, e As EventArgs) Handles rbHabitacion.CheckedChanged
        If (rbHabitacion.Checked) Then
            grdCaptura.Rows.Clear()
            cboDatos.Text = ""
            cboDatos.Visible = True
            txtTotal.Text = "0.00"
        End If
    End Sub

    Private Sub rbTodos_CheckedChanged(sender As Object, e As EventArgs) Handles rbTodos.CheckedChanged
        If (rbTodos.Checked) Then
            grdCaptura.Rows.Clear()
            cboDatos.Text = ""
            cboDatos.Visible = False
            txtTotal.Text = "0.00"
        End If
    End Sub

    Private Sub cboDatos_DropDown(sender As Object, e As EventArgs) Handles cboDatos.DropDown


        Dim cnn5 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
        Dim rd5 As MySqlDataReader
        Dim cmd5 As MySqlCommand
        Try
            cboDatos.Items.Clear()
            cnn5.Close() : cnn5.Open()
            cmd5 = cnn5.CreateCommand

            If (rbHabitacion.Checked) Then
                cmd5.CommandText = "    SELECT DISTINCT NMESA FROM rep_comandas WHERE Codigo='xc3' AND Nombre='Tiempo Habitacion' AND NMESA<>'' ORDER BY NMESA"
            End If

            rd5 = cmd5.ExecuteReader
            Do While rd5.Read
                If rd5.HasRows Then
                    cboDatos.Items.Add(rd5(0).ToString)
                End If
            Loop
            rd5.Close()
            cnn5.Close()

        Catch ex As Exception
            MessageBox.Show(ex.ToString)
            cnn5.Close()
        End Try
    End Sub

    Private Sub cboDatos_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cboDatos.KeyPress
        If AscW(e.KeyChar) = Keys.Enter Then
            btnReporte.Focus.Equals(True)
        End If
    End Sub

    Private Sub btnReporte_Click(sender As Object, e As EventArgs) Handles btnReporte.Click


        Dim cnn1 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
        Dim rd1 As MySqlDataReader
        Dim cmd1 As MySqlCommand

        Try
            Dim m1 As Date = mcDesde.SelectionStart.ToShortDateString
            Dim m2 As Date = mcHasta.SelectionStart.ToShortDateString

            Dim fecha As Date = Nothing
            Dim fechan As String = ""

            Dim acumulado As Double = 0

            grdCaptura.Rows.Clear()





            cnn1.Close() : cnn1.Open()
            cmd1 = cnn1.CreateCommand

            If (rbHabitacion.Checked) Then

                If cboDatos.Text = "" Then MsgBox("Seleccione la habitación", vbInformation + vbOKOnly, titulohotelriaa) : cboDatos.Focus.Equals(True) : Exit Sub

                cmd1.CommandText = "SELECT Fecha,NMESA,Precio,Total,Hr FROM rep_comandas WHERE NMESA='" & cboDatos.Text & "' AND Fecha BETWEEN '" & Format(m1, "yyyy-MM-dd") & "' AND '" & Format(m2, "yyyy-MM-dd") & "' AND Hr BETWEEN '" & Format(dtpinicio.Value, "HH:mm:ss") & "' AND '" & Format(dtpFin.Value, "HH:mm:ss") & "'"
            End If

            If (rbTodos.Checked) Then
                cmd1.CommandText = "SELECT Fecha,NMESA,Precio,Total,Hr FROM rep_comandas WHERE Fecha BETWEEN '" & Format(m1, "yyyy-MM-dd") & "' AND '" & Format(m2, "yyyy-MM-dd") & "' AND Hr BETWEEN '" & Format(dtpinicio.Value, "HH:mm:ss") & "' AND '" & Format(dtpFin.Value, "HH:mm:ss") & "' AND Nombre='Tiempo Habitacion'"
            End If
            rd1 = cmd1.ExecuteReader
            Do While rd1.Read
                If rd1.HasRows Then
                    fecha = rd1("Fecha").ToString
                    fechan = Format(fecha, "yyyy-MM-dd")

                    grdCaptura.Rows.Add(rd1("NMESA").ToString,
rd1("Precio").ToString,
rd1("Total").ToString,
fechan,
rd1("Hr").ToString)

                    acumulado = acumulado + rd1("Total").ToString

                End If
            Loop
            rd1.Close()
            cnn1.Close()

            txtTotal.Text = FormatNumber(acumulado, 2)



        Catch ex As Exception
            MessageBox.Show(ex.ToString)
            cnn1.Close()
        End Try
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