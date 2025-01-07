Imports ClosedXML.Excel
Imports MySql.Data.MySqlClient

Public Class frmRepReservaciones
    Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint

    End Sub

    Private Sub rbTodos_Click(sender As Object, e As EventArgs) Handles rbTodos.Click
        If (rbTodos.Checked) Then

            cboFiltro.Visible = False
            cboFiltro.Text = ""
            grdCaptura.Rows.Clear()

            Dim estado As String = ""
            Dim fentrada As Date = Nothing
            Dim fsalida As Date = Nothing
            Dim fe As String = ""
            Dim fs As String = ""

            Dim cnn1 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
            Dim rd1 As MySqlDataReader
            Dim cmd1 As MySqlCommand


            Try
                cnn1.Close() : cnn1.Open()
                cmd1 = cnn1.CreateCommand
                cmd1.CommandText = "SELECT * FROM reservaciones ORDER BY Cliente"
                rd1 = cmd1.ExecuteReader
                Do While rd1.Read
                    If rd1.HasRows Then

                        If rd1("Status").ToString = 1 Then
                            estado = "TERMINADA"
                        Else
                            estado = "EN RESERVACION"
                        End If

                        fentrada = rd1("FEntrada").ToString
                        fe = Format(fentrada, "yyyy-MM-dd")
                        fsalida = rd1("FSalida").ToString
                        fs = Format(fsalida, "yyyy-MM-dd")

                        grdCaptura.Rows.Add(rd1("Cliente").ToString,
                                            rd1("Telefono").ToString,
                                            rd1("Habitacion").ToString,
                                            fe,
                                            fs,
                                            rd1("Asigno").ToString,
                                            rd1("Reservo").ToString,
                                            estado
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

    Private Sub rbClientes_Click(sender As Object, e As EventArgs) Handles rbClientes.Click
        If (rbClientes.Checked) Then

            grdCaptura.Rows.Clear()
            cboFiltro.Visible = True
            cboFiltro.Focus.Equals(True)
            cboFiltro.Text = ""
        End If
    End Sub

    Private Sub cboFiltro_DropDown(sender As Object, e As EventArgs) Handles cboFiltro.DropDown

        Dim cnn5 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
        Dim rd5 As MySqlDataReader
        Dim cmd5 As MySqlCommand

        Try
            cboFiltro.Items.Clear()
            cnn5.Close() : cnn5.Open()
            cmd5 = cnn5.CreateCommand
            cmd5.CommandText = "SELECT DISTINCT Cliente FROM reservaciones WHERE Cliente<>'' ORDER BY Cliente"
            rd5 = cmd5.ExecuteReader
            Do While rd5.Read
                If rd5.HasRows Then
                    cboFiltro.Items.Add(rd5(0).ToString)
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

        Dim cnn1 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
        Dim rd1 As MySqlDataReader
        Dim cmd1 As MySqlCommand

        Try
            Dim m1 As Date = dtpdesde.SelectionStart.ToShortDateString
            Dim m2 As Date = dtphasta.SelectionStart.ToShortDateString

            Dim estado As String = ""
            Dim fentrada As Date = Nothing
            Dim fsalida As Date = Nothing
            Dim fe As String = ""
            Dim fs As String = ""

            If (rbClientes.Checked) Then
                cnn1.Close() : cnn1.Open()
                cmd1 = cnn1.CreateCommand
                cmd1.CommandText = "SELECT * FROM reservaciones WHERE Cliente='" & cboFiltro.Text & "' AND FEntrada BETWEEN '" & Format(m1, "yyyy-MM-dd") & " " & "00:00:00" & "' AND '" & Format(m2, "yyyy-MM-dd") & " " & "23:59:59" & "'"
                rd1 = cmd1.ExecuteReader
                Do While rd1.Read
                    If rd1.HasRows Then
                        If rd1("Status").ToString = 1 Then
                            estado = "TERMINADA"
                        Else
                            estado = "EN RESERVACION"
                        End If

                        fentrada = rd1("FEntrada").ToString
                        fe = Format(fentrada, "yyyy-MM-dd")
                        fsalida = rd1("FSalida").ToString
                        fs = Format(fsalida, "yyyy-MM-dd")

                        grdCaptura.Rows.Add(rd1("Cliente").ToString,
                                            rd1("Telefono").ToString,
                                            rd1("Habitacion").ToString,
                                            fe,
                                            fs,
                                            rd1("Asigno").ToString,
                                            rd1("Reservo").ToString,
                                            estado
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

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Close()
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
                Dim worksheet As IXLWorksheet =
            workbook.Worksheets.Add("Datos")

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

    Private Sub frmRepReservaciones_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class