Imports ClosedXML.Excel
Imports MySql.Data.MySqlClient

Public Class frmRepCitas
    Private Sub frmRepCitas_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        optClientes.Checked = True

    End Sub

    Private Sub cboDatos_DropDown(sender As Object, e As EventArgs) Handles cboDatos.DropDown
        Dim cnn5 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
        Dim rd5 As MySqlDataReader
        Dim cmd5 As MySqlCommand

        Try
            cboDatos.Items.Clear()

            cnn5.Close() : cnn5.Open()
            cmd5 = cnn5.CreateCommand
            If optClientes.Checked Then
                cmd5.CommandText = "SELECT DISTINCT Cliente FROM citas WHERE Cliente<>'' ORDER BY Cliente"
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

    Private Sub btnReporte_Click(sender As Object, e As EventArgs) Handles btnReporte.Click
        Dim m1 As Date = mcdesde.SelectionStart.ToShortDateString
        Dim m2 As Date = mchasta.SelectionStart.ToShortDateString

        Dim fecha As Date = Nothing
        Dim fechac As String = ""

        Dim cnn1 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
        Dim rd1 As MySqlDataReader
        Dim cmd1 As MySqlCommand

        Try

            If (optClientes.Checked) Then
                cnn1.Close() : cnn1.Open()
                cmd1 = cnn1.CreateCommand
                cmd1.CommandText = "SELECT Medico,Cliente,Telefono,FechaC,Motivo FROM citas WHERE Cliente='" & cboDatos.Text & "'"
                rd1 = cmd1.ExecuteReader
                Do While rd1.Read
                    If rd1.HasRows Then
                        fecha = rd1("FechaC").ToString
                        fechac = Format(fecha, "yyyy-MM-dd HH:mm:ss")

                        grdCaptura.Rows.Add(rd1("Medico").ToString,
                                            rd1("Cliente").ToString,
                                            rd1("Telefono").ToString,
                                            fechac,
                                            rd1("Motivo").ToString
                                            )
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

    Private Sub optClientes_Click(sender As Object, e As EventArgs) Handles optClientes.Click
        If (optClientes.Checked) Then
            grdCaptura.Rows.Clear()
            cboDatos.Text = ""
            optTodos.Checked = False
            cboDatos.Visible = True
        End If
    End Sub

    Private Sub optTodos_Click(sender As Object, e As EventArgs) Handles optTodos.Click

        Dim cnn1 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
        Dim rd1 As MySqlDataReader
        Dim cmd1 As MySqlCommand

        If (optTodos.Checked) Then
            grdCaptura.Rows.Clear()
            cboDatos.Text = ""
            optClientes.Checked = False
            cboDatos.Visible = False

            Dim fecha As Date = Nothing
            Dim fechac As String = ""
            Try
                cnn1.Close() : cnn1.Open()
                cmd1 = cnn1.CreateCommand
                cmd1.CommandText = "SELECT FechaC,Medico,Cliente,Telefono,Motivo FROM citas"
                rd1 = cmd1.ExecuteReader
                Do While rd1.Read
                    If rd1.HasRows Then
                        fecha = rd1("FechaC").ToString
                        fechac = Format(fecha, "yyyy-MM-dd HH:mm:ss")

                        grdCaptura.Rows.Add(rd1("Medico").ToString,
                                            rd1("Cliente").ToString,
                                            rd1("Telefono").ToString,
                                            fechac,
                                            rd1("Motivo").ToString
)

                    End If
                Loop
                rd1.Close()
                cnn1.Close()
            Catch ex As Exception
                MessageBox.Show(ex.ToString)
                cnn1.Close()
            End Try
        End If
    End Sub

    Private Sub btnExportar_Click(sender As Object, e As EventArgs) Handles btnExportar.Click
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