
Imports ClosedXML.Excel
Public Class frmFaltantes

    Private Sub frmFaltantes_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

    End Sub

    Private Sub optproveedor_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles optproveedor.CheckedChanged
        If (optproveedor.Checked) Then
            cbofiltro.Enabled = True
            cbofiltro.Text = ""
            cbofiltro.Items.Clear()
            grdcaptura.Rows.Clear()
            cbofiltro.Focus().Equals(True)
        End If
    End Sub

    Private Sub optdepto_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles optdepto.CheckedChanged
        If (optdepto.Checked) Then
            cbofiltro.Enabled = True
            cbofiltro.Text = ""
            cbofiltro.Items.Clear()
            grdcaptura.Rows.Clear()
            cbofiltro.Focus().Equals(True)
        End If
    End Sub

    Private Sub optgrupo_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles optgrupo.CheckedChanged
        If (optgrupo.Checked) Then
            cbofiltro.Enabled = True
            cbofiltro.Text = ""
            cbofiltro.Items.Clear()
            grdcaptura.Rows.Clear()
            cbofiltro.Focus().Equals(True)
        End If
    End Sub

    Private Sub cbofiltro_DropDown(sender As System.Object, e As System.EventArgs) Handles cbofiltro.DropDown
        cbofiltro.Items.Clear()
        Try
            Dim consluta As String = ""
            If (optproveedor.Checked) Then consluta = "select distinct ProvPri from Productos where Codigo = Left(Codigo,6)"
            If (optdepto.Checked) Then consluta = "select distinct Departamento from Productos where Codigo = Left(Codigo,6)"
            If (optgrupo.Checked) Then consluta = "select distinct Grupo from Productos where Codigo = Left(Codigo,6)"

            cnn1.Close() : cnn1.Open()

            cmd1 = cnn1.CreateCommand
            cmd1.CommandText =
                consluta
            rd1 = cmd1.ExecuteReader
            Do While rd1.Read
                If rd1.HasRows Then cbofiltro.Items.Add(rd1(0).ToString())
            Loop
            rd1.Close() : cnn1.Close()
        Catch ex As Exception
            MessageBox.Show(ex.ToString())
            cnn1.Close()
        End Try
    End Sub

    Private Sub cbofiltro_SelectedValueChanged(sender As Object, e As System.EventArgs) Handles cbofiltro.SelectedValueChanged
        Dim consulta As String = ""
        If (optproveedor.Checked) Then consulta = "select Codigo,CodBarra,Nombre,UCompra,Existencia,ProvPri,PrecioCompra,Min,Max from Productos where ProvPri='" & cbofiltro.Text & "' order by Nombre"
        If (optdepto.Checked) Then consulta = "select Codigo,CodBarra,Nombre,UCompra,Existencia,ProvPri,PrecioCompra,Min,Max from Productos where Departamento='" & cbofiltro.Text & "' order by Nombre"
        If (optgrupo.Checked) Then consulta = "select Codigo,CodBarra,Nombre,UCompra,Existencia,ProvPri,PrecioCompra,Min,Max from Productos where Grupo='" & cbofiltro.Text & "' order by Nombre"

        grdcaptura.Rows.Clear()

        Try
            cnn1.Close() : cnn1.Open()

            cmd1 = cnn1.CreateCommand
            cmd1.CommandText =
                consulta
            rd1 = cmd1.ExecuteReader
            Do While rd1.Read
                If rd1.HasRows Then
                    Dim codigo As String = rd1("Codigo").ToString()
                    Dim barras As String = rd1("CodBarra").ToString()
                    Dim nombre As String = rd1("Nombre").ToString()
                    Dim unidad As String = rd1("UCompra").ToString()
                    Dim exist As Double = IIf(rd1("Existencia").ToString = "", 0, rd1("Existencia").ToString)
                    Dim prov As String = rd1("ProvPri").ToString
                    Dim precompra As Double = IIf(rd1("PrecioCompra").ToString = "", 0, rd1("PrecioCompra").ToString)
                    Dim mini As Double = IIf(rd1("Min").ToString = "", 1, rd1("Min").ToString)
                    Dim maxi As Double = IIf(rd1("Max").ToString = "", 1, rd1("Max").ToString)
                    Dim falta As Double = FormatNumber(maxi - exist, 2)
                    Dim cuantocuesta As Double = FormatNumber(falta * precompra)

                    grdcaptura.Rows.Add(codigo, barras, nombre, prov, exist & " " & unidad, mini & " " & unidad, maxi & " " & unidad, falta & " " & unidad, cuantocuesta)
                End If
            Loop
            rd1.Close() : cnn1.Close()
        Catch ex As Exception
            MessageBox.Show(ex.ToString())
            cnn1.Close()
        End Try
    End Sub

    Private Sub opttodos_CheckedChanged(sender As Object, e As EventArgs) Handles opttodos.CheckedChanged
        If opttodos.Checked = True Then
            Try
                grdcaptura.Rows.Clear()
                cnn1.Close()
                cnn1.Open()
                cmd1 = cnn1.CreateCommand
                cmd1.CommandText = "Select Codigo,CodBarra,Nombre,UCompra,Existencia,ProvPri,PrecioCompra,Min,Max from Productos"
                rd1 = cmd1.ExecuteReader
                Do While rd1.Read
                    Dim codigo As String = rd1("Codigo").ToString()
                    Dim barras As String = rd1("CodBarra").ToString()
                    Dim nombre As String = rd1("Nombre").ToString()
                    Dim unidad As String = rd1("UCompra").ToString()
                    Dim exist As Double = IIf(rd1("Existencia").ToString = "", 0, rd1("Existencia").ToString)
                    Dim prov As String = rd1("ProvPri").ToString
                    Dim precompra As Double = IIf(rd1("PrecioCompra").ToString = "", 0, rd1("PrecioCompra").ToString)
                    Dim mini As Double = IIf(rd1("Min").ToString = "", 1, rd1("Min").ToString)
                    Dim maxi As Double = IIf(rd1("Max").ToString = "", 1, rd1("Max").ToString)
                    Dim falta As Double = FormatNumber(maxi - exist, 2)
                    Dim cuantocuesta As Double = FormatNumber(falta * precompra)

                    grdcaptura.Rows.Add(codigo, barras, nombre, prov, exist & " " & unidad, mini & " " & unidad, maxi & " " & unidad, falta & " " & unidad, cuantocuesta)
                Loop
                rd1.Close()
                cnn1.Close()
            Catch ex As Exception
                MessageBox.Show(ex.ToString)
                cnn1.Close()
            End Try
        End If
    End Sub

    Private Sub btnsalir_Click(sender As Object, e As EventArgs) Handles btnsalir.Click
        grdcaptura.Rows.Clear()
        cbofiltro.Text = ""
        cbofiltro.Items.Clear()
    End Sub

    Private Sub btnexportar_Click(sender As Object, e As EventArgs) Handles btnexportar.Click
        ExportarDataGridViewAExcel(grdcaptura)
    End Sub

    Public Sub ExportarDataGridViewAExcel(dgv As DataGridView)
        If grdcaptura.Rows.Count = 0 Then MsgBox("Genera el reporte para poder exportar los datos a Excel.", vbInformation + vbOKOnly, titulocentral) : Exit Sub
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