Imports ClosedXML.Excel
Imports Microsoft.Office.Interop
Imports MySql.Data.MySqlClient

Public Class frmRepGastos
    Private Sub frmRepGastos_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub rbArea_Click(sender As Object, e As EventArgs) Handles rbArea.Click

        If (rbArea.Checked) Then
            rbModelo.Checked = False
            rbTodos.Checked = False
            cboDatos.Text = ""
            grdCaptura.Rows.Clear()

            txtTotal.Text = "0.00"
            txtRegistros.Text = "0.00"
        End If

    End Sub

    Private Sub rbModelo_Click(sender As Object, e As EventArgs) Handles rbModelo.Click
        If (rbModelo.Checked) Then
            rbArea.Checked = False
            rbTodos.Checked = False
            cboDatos.Text = ""
            grdCaptura.Rows.Clear()

            txtTotal.Text = "0.00"
            txtRegistros.Text = "0.00"
        End If
    End Sub

    Private Sub rbTodos_Click(sender As Object, e As EventArgs) Handles rbTodos.Click
        If (rbTodos.Checked) Then
            rbArea.Checked = False
            rbModelo.Checked = False
            cboDatos.Text = ""
            txtTotal.Text = "0.00"
            txtRegistros.Text = "0.00"
            grdCaptura.Rows.Clear()

            Dim tot As Double = 0
            Dim cnn1 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
            Dim rd1 As MySqlDataReader
            Dim cmd1 As MySqlCommand

            Try
                cnn1.Close() : cnn1.Open()
                cmd1 = cnn1.CreateCommand
                cmd1.CommandText = "SELECT COUNT(Id) FROM otrosgastos"
                rd1 = cmd1.ExecuteReader
                If rd1.HasRows Then
                    If rd1.Read Then
                        txtRegistros.Text = rd1(0).ToString
                        txtRegistros.Text = FormatNumber(txtRegistros.Text, 2)
                    End If
                End If
                rd1.Close()

                cmd1 = cnn1.CreateCommand
                cmd1.CommandText = "SELECT Fecha,Tipo,Modelo,Placas,Folio,Concepto,FormaPago,Total,Nota FROM otrosgastos"
                rd1 = cmd1.ExecuteReader
                Do While rd1.Read
                    If rd1.HasRows Then

                        Dim f As Date = Nothing
                        Dim fe As String = ""
                        f = rd1("Fecha").ToString
                        fe = Format(f, "yyyy-MM-dd")

                        grdCaptura.Rows.Add(rd1("Tipo").ToString,
                                            rd1("Modelo").ToString,
                                            rd1("Placas").ToString,
                                            rd1("Folio").ToString,
                                            rd1("Concepto").ToString,
                                            rd1("FormaPago").ToString,
                                            rd1("Total").ToString,
                                            fe,
                                            rd1("Nota").ToString
                                            )
                        tot = tot + rd1("Total").ToString

                    End If
                Loop
                rd1.Close()
                cnn1.Close()
                txtTotal.Text = FormatNumber(tot, 2)
            Catch ex As Exception
                MessageBox.Show(ex.ToString)
                cnn1.Close()

            End Try
        End If
    End Sub

    Private Sub btnNuevo_Click(sender As Object, e As EventArgs) Handles btnNuevo.Click
        cboDatos.Text = ""
        txtRegistros.Text = "0.00"
        txtTotal.Text = "0.00"

        rbArea.Checked = True
        rbModelo.Checked = False
        rbTodos.Checked = False
        grdCaptura.Rows.Clear()
    End Sub

    Private Sub cboDatos_DropDown(sender As Object, e As EventArgs) Handles cboDatos.DropDown
        Dim cnn5 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
        Dim rd5 As MySqlDataReader
        Dim cmd5 As MySqlCommand
        Try
            cboDatos.Items.Clear()

            cnn5.Close() : cnn5.Open()
            cmd5 = cnn5.CreateCommand

            If (rbArea.Checked) Then
                cmd5.CommandText = "SELECT DISTINCT Tipo FROM otrosgastos WHERE Tipo<>'' ORDER BY Tipo"
            End If

            If (rbModelo.Checked) Then
                cmd5.CommandText = "SELECT DISTINCT Modelo FROM otrosgastos WHERE Modelo<>'' ORDER BY Modelo"
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

    Private Sub cboDatos_SelectedValueChanged(sender As Object, e As EventArgs) Handles cboDatos.SelectedValueChanged

        Dim cnn1 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
        Dim cnn2 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
        Dim rd1, rd2 As MySqlDataReader
        Dim cmd1, cmd2 As MySqlCommand
        Try
            grdCaptura.Rows.Clear()
            Dim cuantos As Integer = 0
            Dim totalgastos As Double = 0

            cnn1.Close() : cnn1.Open()
            cnn2.Close() : cnn2.Open()
            cmd1 = cnn1.CreateCommand
            cmd2 = cnn2.CreateCommand

            If (rbArea.Checked) Then
                cmd1.CommandText = "SELECT Tipo,Modelo,Placas,Folio,Concepto,FormaPago,Total,Nota,Fecha FROM otrosgastos WHERE Tipo='" & cboDatos.Text & "'"
                cmd2.CommandText = "SELECT COUNT(Id) FROM otrosgastos WHERE Tipo='" & cboDatos.Text & "'"
            End If

            If (rbModelo.Checked) Then
                cmd1.CommandText = "SELECT Tipo,Modelo,Placas,Folio,Concepto,FormaPago,Total,Nota,Fecha FROM otrosgastos WHERE Modelo='" & cboDatos.Text & "'"
                cmd2.CommandText = "SELECT COUNT(Id) FROM otrosgastos WHERE Modelo='" & cboDatos.Text & "'"
            End If

            rd2 = cmd2.ExecuteReader
            If rd2.HasRows Then
                If rd2.Read Then
                    cuantos = rd2(0).ToString
                    txtRegistros.Text = FormatNumber(cuantos, 2)
                End If
            End If
            rd2.Close()

            rd1 = cmd1.ExecuteReader
            Do While rd1.Read
                If rd1.HasRows Then

                    Dim fec As Date = Nothing
                    Dim fech As String = ""

                    fec = rd1("Fecha").ToString
                    fech = Format(fec, "yyyy-MM-dd")
                    grdCaptura.Rows.Add(rd1("Tipo").ToString,
                                        rd1("Modelo").ToString,
                                        rd1("Placas").ToString,
                                        rd1("Folio").ToString,
                                        rd1("Concepto").ToString,
                                        rd1("FormaPago").ToString,
                                        rd1("Total").ToString,
                                        rd1("Nota").ToString,
                                        fech)

                    totalgastos = totalgastos + CDbl(rd1("Total").ToString)

                End If
            Loop
            rd1.Close()
            cnn1.Close()
            txtTotal.Text = FormatNumber(totalgastos, 2)

        Catch ex As Exception
            MessageBox.Show(ex.ToString)
            cnn1.Close()
        End Try
    End Sub

    Private Sub btnSalir_Click(sender As Object, e As EventArgs) Handles btnSalir.Click
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