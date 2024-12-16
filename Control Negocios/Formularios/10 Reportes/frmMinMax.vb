Imports ClosedXML.Excel
Imports Core.DAL.DE

Imports iTextSharp.text
Imports iTextSharp.text.pdf
Imports System.IO
Imports System.Net.Mail
Imports System.Text.RegularExpressions
Imports System.Web.UI.WebControls

Public Class frmMinMax

    Public sugerencia As Double = 0
    Private archivoadj As String = ""
    Private Sub optProveedor_CheckedChanged(sender As Object, e As EventArgs) Handles optProveedor.CheckedChanged
        If optProveedor.Checked Then
            Limpia()
            cbofiltro.Enabled = True
            cbofiltro.Focus()
        End If
    End Sub

    Private Sub optDepartamento_CheckedChanged(sender As Object, e As EventArgs) Handles optDepartamento.CheckedChanged
        If optDepartamento.Checked Then
            Limpia()
            cbofiltro.Enabled = True
            cbofiltro.Focus()
        End If
    End Sub

    Private Sub optGrupo_CheckedChanged(sender As Object, e As EventArgs) Handles optGrupo.CheckedChanged
        If optGrupo.Checked Then
            Limpia()
            cbofiltro.Enabled = True
            cbofiltro.Focus()
        End If
    End Sub

    Private Sub optTodos_CheckedChanged(sender As Object, e As EventArgs) Handles optTodos.CheckedChanged
        If optTodos.Checked Then
            Limpia()
            cbofiltro.Enabled = False
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If (optProveedor.Checked = True Or optDepartamento.Checked = True Or optGrupo.Checked = True) And cbofiltro.Text = "" Then MsgBox("Selecciona un registro para continuar.", vbInformation + vbOKOnly, "Delsscom Control Negocios Pro") : cbofiltro.Focus().Equals(True) : Exit Sub

        grdcaptura.Rows.Clear()

        Dim fecha_inicio As Date = dtpFecha.Value.ToShortDateString
        'fecha_inicio = fecha_inicio.AddMonths(-1)
        'fecha_inicio = fecha_inicio.AddDays(-15)
        Dim fecha_final As Date = Date.Now
        Dim diferencia_dias As Integer = 1
        diferencia_dias = DateDiff(DateInterval.Day, fecha_inicio, fecha_final)

        My.Application.DoEvents()

        grdcaptura.ColumnHeadersVisible = True

        Dim varsql As String = ""

        If optProveedor.Checked Then
            If Trim(cbofiltro.Text) <> "" Then
                varsql = "select Codigo, Nombre, existencia, MCD, Multiplo, UCompra, T_Entrega, PrecioCompra from productos where ProvPri = '" & cbofiltro.Text & "' order by Codigo "
            Else
                MsgBox("Hay que seleccionar un proveedor para poder generar el reporte", vbInformation + vbOKOnly, "Delsscom Control Negocios Pro")
                Exit Sub
            End If
        End If
        If optDepartamento.Checked Then
            If Trim(cbofiltro.Text) <> "" Then
                varsql = "select Codigo, Nombre, existencia, MCD, Multiplo, UCompra, T_Entrega, PrecioCompra from productos where Departamento = '" & cbofiltro.Text & "' order by Codigo "
            Else
                MsgBox("Hay que seleccionar un proveedor para poder generar el reporte", vbInformation + vbOKOnly, "Delsscom Control Negocios Pro")
                Exit Sub
            End If
        End If
        If optGrupo.Checked Then
            If Trim(cbofiltro.Text) <> "" Then
                varsql = "select Codigo, Nombre, existencia, MCD, Multiplo, UCompra, T_Entrega, PrecioCompra  from productos where Grupo = '" & cbofiltro.Text & "' order by Codigo "
            Else
                MsgBox("Hay que seleccionar un proveedor para poder generar el reporte", vbInformation + vbOKOnly, "Delsscom Control Negocios Pro")
                Exit Sub
            End If
        End If
        If optTodos.Checked Then
            varsql = "select Codigo, Nombre, existencia, MCD, Multiplo, UCompra, T_Entrega, PrecioCompra from productos order by Codigo "
        End If

        If Trim(txtStockSeguridad.Text) = "" Then MsgBox("El stock de seguridad no puede estar vacío") : Exit Sub
        If Trim(txtPorcentMax.Text) = "" Then MsgBox("El porcentaje de seguridad no puede estar vacío") : Exit Sub
        If IsNumeric(txtStockSeguridad.Text) = False Then MsgBox("El stock de seguridad tiene que ser un valor numérico") : Exit Sub
        If IsNumeric(txtPorcentMax.Text) = False Then MsgBox("El porcentaje de seguridad tiene que ser un valor numérico") : Exit Sub

        Dim codigoanterior As String = ""
        Dim varMultiploanterior As String = ""
        Dim vartiempoanterior As Integer = 1

        Dim cnnp As New MySql.Data.MySqlClient.MySqlConnection
        Dim sinfo As String = ""
        Dim odatap As New ToolKitSQL.myssql
        Dim dt As New DataTable
        Dim dr As DataRow
        With odatap
            If .dbOpen(cnnp, sTarget, sinfo) Then

                If .getDr(cnnp, dr, "select NotasCred from Formatos where Facturas='MinMax'", sinfo) Then
                    .runSp(cnnp, "update Formatos set NotasCred = '" & Format(dtpFecha.Value, "dd/MM/yyyy") & "' where Facturas = 'MinMax'", sinfo)
                Else
                    .runSp(cnnp, "insert into Formatos(Facturas, NotasCred) values('MinMax','" & Format(dtpFecha.Value, "dd/MM/yyyy") & "') ", sinfo)
                End If

                If .getDr(cnnp, dr, "select NotasCred from Formatos where Facturas='MinMax1'", sinfo) Then
                    .runSp(cnnp, "update Formatos set NotasCred = '" & Trim(txtStockSeguridad.Text) & "' where Facturas = 'MinMax1'", sinfo)
                Else
                    .runSp(cnnp, "insert into Formatos(Facturas, NotasCred) values('MinMax1','" & Trim(txtStockSeguridad.Text) & "') ", sinfo)
                End If

                If .getDr(cnnp, dr, "select NotasCred from Formatos where Facturas='MinMax2'", sinfo) Then
                    .runSp(cnnp, "update Formatos set NotasCred = '" & Trim(txtPorcentMax.Text) & "' where Facturas = 'MinMax2'", sinfo)
                Else
                    .runSp(cnnp, "insert into Formatos(Facturas, NotasCred) values('MinMax2','" & Trim(txtPorcentMax.Text) & "') ", sinfo)
                End If

                If .getDt(cnnp, dt, varsql, sinfo) Then
                    For Each dr In dt.Rows

                        My.Application.DoEvents()

                        Dim consumoPromedio As Double = 0
                        Dim cantidad_productos_vendidos As Double = 0

                        Dim tiempoEntregaHabitual As Integer = 1
                        If IsNumeric(dr("T_Entrega").ToString) Then
                            tiempoEntregaHabitual = dr("T_Entrega").ToString
                        End If

                        Dim bandera_entra As Integer = 0

                        Dim dr1 As DataRow
                        If .getDr(cnnp, dr1, "select SUM(Cantidad) as sumacanti from VentasDetalle where Codigo='" & dr("Codigo").ToString & "' and Fecha BETWEEN '" & Format(fecha_inicio, "yyyy-MM-dd") & "' AND '" & Format(fecha_final, "yyyy-MM-dd") & "'", sinfo) Then

                            If IsNumeric(dr1("sumacanti").ToString) Then
                                cantidad_productos_vendidos = CDec(dr1("sumacanti").ToString)
                            End If

                            If Mid(dr("Codigo").ToString, 1, 6) = codigoanterior Then
                                cantidad_productos_vendidos = CDec(cantidad_productos_vendidos * dr("Multiplo").ToString) / varMultiploanterior
                                tiempoEntregaHabitual = vartiempoanterior
                                bandera_entra = 1
                            End If

                        End If


                        ' Calcular Stock Mínimo
                        Dim stockMinimo As Double = 0

                        ' Calcular Stock de Seguridad (diferencia entre máximo y habitual)
                        Dim stockSeguridad As Double = 0

                        ' Calcular Stock Máximo
                        Dim stockMaximo As Double = 0

                        ' Calcular Punto de Reorden
                        Dim puntoReorden As Double = 0

                        Dim cantidadsugerida As Double = 0

                        If bandera_entra > 0 Then

                            For i = 0 To grdcaptura.Rows.Count - 1

                                If Mid(dr("Codigo").ToString, 1, 6) = grdcaptura.Rows(i).Cells(2).Value.ToString Then

                                    If cantidad_productos_vendidos > 0 Then
                                        cantidad_productos_vendidos = cantidad_productos_vendidos + CDec(grdcaptura.Rows(i).Cells(6).Value.ToString)
                                        consumoPromedio = FormatNumber(CDec(cantidad_productos_vendidos / diferencia_dias), 2)
                                    End If

                                    'para calcular el stock minimo se ocupa el tiempo de entrega del proveedor multiplicado por el consumo promedio diario del producto
                                    stockMinimo = tiempoEntregaHabitual * consumoPromedio

                                    'el stock de seguridad es un valor que se utiliza como proteccion de la empresa para no quedarse sin inventario en caso de algun imprevisto, la empresa suele asignar ese valor en dias y para hacer un calculo junto con el consumo promedio ejemplo: 2 dias * el consumo promedio diario = stock de seguridad
                                    stockSeguridad = CDec(IIf(IsNumeric(txtStockSeguridad.Text), txtStockSeguridad.Text, 0)) * consumoPromedio

                                    'el punto de reorden por lo general es el stock minimo que tenemos pero si llegamos a tener stock de seguridad el valor seria el stock minimo + el stock de seguridad 
                                    puntoReorden = stockMinimo + stockSeguridad

                                    stockMaximo = (puntoReorden * (CDec(txtPorcentMax.Text / 100) + 1))

                                    cantidadsugerida = CDec(grdcaptura.Rows(i).Cells(5).Value.ToString) - puntoReorden

                                    If cantidadsugerida >= 0 Then
                                        cantidadsugerida = 0
                                    Else
                                        cantidadsugerida = Math.Abs(cantidadsugerida)
                                    End If

                                    If FormatNumber(cantidadsugerida, 0) > 0 Then
                                        grdcaptura.Rows(i).Cells(0).Value = 1
                                    Else
                                        grdcaptura.Rows(i).Cells(0).Value = 0
                                    End If

                                    grdcaptura.Rows(i).Cells(6).Value = cantidad_productos_vendidos
                                    grdcaptura.Rows(i).Cells(7).Value = consumoPromedio
                                    grdcaptura.Rows(i).Cells(9).Value = puntoReorden
                                    grdcaptura.Rows(i).Cells(10).Value = FormatNumber(stockMinimo, 0)
                                    grdcaptura.Rows(i).Cells(11).Value = FormatNumber(stockMaximo, 0)
                                    grdcaptura.Rows(i).Cells(1).Value = FormatNumber(cantidadsugerida, 0)

                                    Exit For

                                End If

                            Next

                        Else

                            If cantidad_productos_vendidos > 0 Then
                                consumoPromedio = FormatNumber(CDec(cantidad_productos_vendidos / diferencia_dias), 2)
                            End If

                            'para calcular el stock minimo se ocupa el tiempo de entrega del proveedor multiplicado por el consumo promedio diario del producto
                            stockMinimo = tiempoEntregaHabitual * consumoPromedio

                            'el stock de seguridad es un valor que se utiliza como proteccion de la empresa para no quedarse sin inventario en caso de algun imprevisto, la empresa suele asignar ese valor en dias y para hacer un calculo junto con el consumo promedio ejemplo: 2 dias * el consumo promedio diario = stock de seguridad
                            stockSeguridad = CDec(IIf(IsNumeric(txtStockSeguridad.Text), txtStockSeguridad.Text, 0)) * consumoPromedio

                            'el punto de reorden por lo general es el stock minimo que tenemos pero si llegamos a tener stock de seguridad el valor seria el stock minimo + el stock de seguridad 
                            puntoReorden = stockMinimo + stockSeguridad

                            stockMaximo = (puntoReorden * (CDec(txtPorcentMax.Text / 100) + 1))

                            If CDec(dr("existencia").ToString) < 0 Then
                                cantidadsugerida = 0 - puntoReorden
                            Else
                                cantidadsugerida = CDec(dr("existencia").ToString / dr("Multiplo").ToString) - puntoReorden
                            End If

                            If cantidadsugerida >= 0 Then
                                cantidadsugerida = 0
                            Else
                                cantidadsugerida = Math.Abs(cantidadsugerida)
                            End If

                            If Len(dr("Codigo").ToString) < 7 Then

                                If CDec(dr("existencia").ToString) < 0 Then

                                    If FormatNumber(cantidadsugerida, 0) > 0 Then
                                        grdcaptura.Rows.Add(1, FormatNumber(cantidadsugerida, 0), dr("Codigo").ToString, dr("Nombre").ToString, dr("UCompra").ToString, 0, cantidad_productos_vendidos, consumoPromedio, tiempoEntregaHabitual, puntoReorden, FormatNumber(stockMinimo, 0), FormatNumber(stockMaximo, 0), dr("PrecioCompra").ToString)
                                    Else
                                        grdcaptura.Rows.Add(0, FormatNumber(cantidadsugerida, 0), dr("Codigo").ToString, dr("Nombre").ToString, dr("UCompra").ToString, 0, cantidad_productos_vendidos, consumoPromedio, tiempoEntregaHabitual, puntoReorden, FormatNumber(stockMinimo, 0), FormatNumber(stockMaximo, 0), dr("PrecioCompra").ToString)
                                    End If

                                Else

                                    If FormatNumber(cantidadsugerida, 0) > 0 Then
                                        grdcaptura.Rows.Add(1, FormatNumber(cantidadsugerida, 0), dr("Codigo").ToString, dr("Nombre").ToString, dr("UCompra").ToString, CDec(dr("existencia").ToString / dr("Multiplo").ToString), cantidad_productos_vendidos, consumoPromedio, tiempoEntregaHabitual, puntoReorden, FormatNumber(stockMinimo, 0), FormatNumber(stockMaximo, 0), dr("PrecioCompra").ToString)
                                    Else
                                        grdcaptura.Rows.Add(0, FormatNumber(cantidadsugerida, 0), dr("Codigo").ToString, dr("Nombre").ToString, dr("UCompra").ToString, CDec(dr("existencia").ToString / dr("Multiplo").ToString), cantidad_productos_vendidos, consumoPromedio, tiempoEntregaHabitual, puntoReorden, FormatNumber(stockMinimo, 0), FormatNumber(stockMaximo, 0), dr("PrecioCompra").ToString)
                                    End If

                                End If

                            End If

                            codigoanterior = Mid(dr("Codigo").ToString, 1, 6)

                            varMultiploanterior = dr("Multiplo").ToString

                            If IsNumeric(dr("T_Entrega").ToString) Then
                                vartiempoanterior = dr("T_Entrega").ToString
                            Else
                                vartiempoanterior = 1
                            End If

                        End If

                    Next
                End If
                cnnp.Close()
            End If
        End With

        MsgBox("Proceso Terminado")

    End Sub

    Private Sub Limpia()
        cbofiltro.Items.Clear()
        cbofiltro.Text = ""
        grdcaptura.Rows.Clear()
    End Sub

    Private Sub frmMinMax_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'SFormatos("MinMax", "")

        Dim varfecha As Date = Date.Now
        txtPorcentMax.Text = 20
        txtStockSeguridad.Text = 2
        dtpFecha.Text = varfecha.AddDays(-15)

        Dim cnnp As New MySql.Data.MySqlClient.MySqlConnection
        Dim sinfo As String = ""
        Dim odatap As New ToolKitSQL.myssql
        Dim dt As New DataTable
        Dim dr As DataRow
        With odatap
            If .dbOpen(cnnp, sTarget, sinfo) Then

                If .getDr(cnnp, dr, "select NotasCred from Formatos where Facturas='MinMax'", sinfo) Then
                    If dr("NotasCred").ToString <> "" And dr("NotasCred").ToString.Contains("/") And dr("NotasCred").ToString.Length > 9 Then
                        dtpFecha.Text = Format(CDate(dr("NotasCred").ToString), "dd/MM/yyyy")
                    End If
                End If

                If .getDr(cnnp, dr, "select NotasCred from Formatos where Facturas='MinMax1'", sinfo) Then
                    If IsNumeric(dr("NotasCred").ToString) Then
                        txtStockSeguridad.Text = dr("NotasCred").ToString
                    End If
                End If

                If .getDr(cnnp, dr, "select NotasCred from Formatos where Facturas='MinMax2'", sinfo) Then
                    If IsNumeric(dr("NotasCred").ToString) Then
                        txtPorcentMax.Text = dr("NotasCred").ToString
                    End If
                End If

                If .getDt(cnnp, dt, "select NComercial from Proveedores where NComercial <> '' order by NComercial ", sinfo) Then
                    For Each dr In dt.Rows
                        cboProveedor.Items.Add(dr("NComercial").ToString)
                    Next
                End If

                cnnp.Close()
            End If
        End With

        grdcaptura.Size = New System.Drawing.Size(977, 441)

    End Sub

    Private Sub cbofiltro_DropDown(sender As Object, e As EventArgs) Handles cbofiltro.DropDown
        cbofiltro.Items.Clear()
        Limpia()
        Try
            cnn1.Close() : cnn1.Open()

            cmd1 = cnn1.CreateCommand
            If (optProveedor.Checked) Then
                cmd1.CommandText =
                    "select distinct ProvPri from Productos where ProvPri<>''"
            End If
            If (optDepartamento.Checked) Then
                cmd1.CommandText =
                    "select distinct Departamento from Productos where Departamento <> 'SERVICIOS' and Departamento<>''"
            End If
            If (optGrupo.Checked) Then
                cmd1.CommandText =
                    "select distinct Grupo from Productos where Grupo<>''"
            End If
            rd1 = cmd1.ExecuteReader
            Do While rd1.Read
                If rd1.HasRows Then
                    cbofiltro.Items.Add(rd1(0).ToString)
                End If
            Loop
            rd1.Close() : cnn1.Close()
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
            cnn1.Close()
        End Try
    End Sub

    Private Sub cbofiltro_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cbofiltro.KeyPress
        If AscW(e.KeyChar) = Keys.Enter Then
            Button1.Focus().Equals(True)
        End If
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        ExportarDataGridViewAExcel(grdcaptura)
    End Sub

    Public Sub ExportarDataGridViewAExcel(dgv As DataGridView)
        If dgv.Rows.Count = 0 Then MsgBox("Genera el reporte para poder exportar los datos a Excel.", vbInformation + vbOKOnly, titulocentral) : Exit Sub
        If MsgBox("¿Deseas exportar la información a un archivo de Excel?", vbInformation + vbOKCancel, "Delsscom Control Negocios Pro") = vbOK Then

            Dim voy As Integer = 0
            ' Crea un nuevo libro de trabajo de Excel
            Using workbook As New XLWorkbook()

                ' Añade una nueva hoja de trabajo
                Dim worksheet As IXLWorksheet = workbook.Worksheets.Add("Datos")

                ' Escribe los encabezados de columna
                For colIndex As Integer = 1 To dgv.Columns.Count - 2
                    Dim headerCell As IXLCell = worksheet.Cell(1, colIndex + 1)
                    worksheet.Cell(1, colIndex + 1).Value = dgv.Columns(colIndex).HeaderText
                    headerCell.Value = dgv.Columns(colIndex).HeaderText
                    headerCell.Style.Font.Bold = True  ' Aplica negrita a los encabezados
                Next

                For rowIndex As Integer = 0 To dgv.Rows.Count - 1
                    For colIndex As Integer = 1 To dgv.Columns.Count - 2
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

                crea_dir("C:\ControlNegociosPro\ReportesPedidos\")

                Dim var1 As String = Format(Date.Now, "ddMMyyyyHHmmss")
                Dim root_name_recibo As String = "C:\ControlNegociosPro\ReportesPedidos\MaxMin_" & var1 & ".xlsx"

                worksheet.Columns().AdjustToContents()
                ' Usa MemoryStream para guardar el archivo en memoria y abrirlo
                Using memoryStream As New System.IO.MemoryStream()
                    ' Guarda el libro de trabajo en el MemoryStream
                    workbook.SaveAs(memoryStream)

                    '' Guarda el MemoryStream en un archivo temporal para abrirlo
                    'Dim tempFilePath As String = IO.Path.GetTempPath() & Guid.NewGuid().ToString() & ".xlsx"
                    System.IO.File.WriteAllBytes(root_name_recibo, memoryStream.ToArray())

                    ' Abre el archivo temporal en Excel
                    Process.Start(root_name_recibo)
                End Using

                'workbook.SaveAs(filePath)
            End Using
            MessageBox.Show("Datos exportados exitosamente")

        End If
    End Sub

    Public Sub ExportarDataGridViewAExcel2(dgv As DataGridView)

        If dgv.Rows.Count = 0 Then MsgBox("Hay que agregar productos al pedido para poder exportarlo a Excel.", vbInformation + vbOKOnly, titulocentral) : Exit Sub
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

                crea_dir("C:\ControlNegociosPro\ReportesPedidos\")

                Dim var1 As String = Format(Date.Now, "ddMMyyyyHHmmss")
                Dim root_name_recibo As String = "C:\ControlNegociosPro\ReportesPedidos\Pedido_" & var1 & ".xlsx"

                worksheet.Columns().AdjustToContents()
                ' Usa MemoryStream para guardar el archivo en memoria y abrirlo
                Using memoryStream As New System.IO.MemoryStream()
                    ' Guarda el libro de trabajo en el MemoryStream
                    workbook.SaveAs(memoryStream)

                    System.IO.File.WriteAllBytes(root_name_recibo, memoryStream.ToArray())

                    ' Abre el archivo temporal en Excel
                    Process.Start(root_name_recibo)
                End Using

                'workbook.SaveAs(filePath)
            End Using
            MessageBox.Show("Datos exportados exitosamente")

        End If
    End Sub

    Private Sub grdcaptura_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles grdcaptura.CellDoubleClick

        If grdcaptura.RowCount = 0 Then Exit Sub

        Dim INDEX As Integer = grdcaptura.CurrentRow.Index
        Dim banderaexistencia As Integer = 0
        Dim banderaentra As Integer = 0

        If grdcaptura.Rows(INDEX).Cells(1).Value.ToString > 0 Then

            If MsgBox("¿Desea agregar la cantidad sugerida?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                banderaexistencia = 1
            End If

        End If

        For ii = 0 To DataGridView1.Rows.Count - 1
            If DataGridView1.Rows(ii).Cells(0).Value.ToString = grdcaptura.Rows(INDEX).Cells(2).Value.ToString Then
                banderaentra = 1
                Exit For
            End If
        Next

        If banderaentra = 0 Then

            If banderaexistencia > 0 Then
                DataGridView1.Rows.Add(grdcaptura.Rows(INDEX).Cells(2).Value.ToString, grdcaptura.Rows(INDEX).Cells(3).Value.ToString, grdcaptura.Rows(INDEX).Cells(4).Value.ToString, grdcaptura.Rows(INDEX).Cells(1).Value.ToString, FormatNumber(grdcaptura.Rows(INDEX).Cells(12).Value.ToString, 2), FormatNumber(CDec(grdcaptura.Rows(INDEX).Cells(12).Value.ToString) * CDec(grdcaptura.Rows(INDEX).Cells(1).Value.ToString), 2))
            Else
                DataGridView1.Rows.Add(grdcaptura.Rows(INDEX).Cells(2).Value.ToString, grdcaptura.Rows(INDEX).Cells(3).Value.ToString, grdcaptura.Rows(INDEX).Cells(4).Value.ToString, 0, FormatNumber(grdcaptura.Rows(INDEX).Cells(12).Value.ToString, 2), 0)
            End If

        End If

        calcula()


        '    Dim COD As String = grdcaptura.Rows(INDEX).Cells(0).Value.ToString
        '    sugerencia = grdcaptura.Rows(INDEX).Cells(7).Value.ToString

        '    Dim codbarra As String = ""

        '    cnn1.Close() : cnn1.Open()
        '    cmd1 = cnn1.CreateCommand
        '    cmd1.CommandText = "SELECT CodBarra FROM productos WHERE Codigo='" & COD & "'"
        '    rd1 = cmd1.ExecuteReader
        '    If rd1.HasRows Then
        '        If rd1.Read Then
        '            codbarra = rd1(0).ToString
        '        End If
        '    End If
        '    rd1.Close()
        '    cnn1.Close()

        '    frmComparador.txtCodBarra.Text = codbarra
        '    frmComparador.suge = sugerencia
        '    frmComparador.Show()
        '    frmComparador.BringToFront()
    End Sub

    Private Sub btnActu_Click(sender As Object, e As EventArgs) Handles btnActu.Click

        If grdcaptura.Rows.Count > 0 Then

            If MsgBox("Desea actualizar los tiempos de entrega de los productos?", MsgBoxStyle.YesNo, "Delsscom Control Negocios Pro") = MsgBoxResult.Yes Then

                Dim cnnp As New MySql.Data.MySqlClient.MySqlConnection
                Dim sinfo As String = ""
                Dim odatap As New ToolKitSQL.myssql
                Dim dt As New DataTable
                Dim dr As DataRow
                With odatap
                    If .dbOpen(cnnp, sTarget, sinfo) Then
                        For i = 0 To grdcaptura.Rows.Count - 1
                            If grdcaptura.Rows(i).Cells(8).Value.ToString <> "" Then

                                If IsNumeric(grdcaptura.Rows(i).Cells(8).Value.ToString) Then

                                    If CDec(grdcaptura.Rows(i).Cells(8).Value.ToString) > 0 Then

                                        .runSp(cnnp, "update productos set T_Entrega = " & CDec(grdcaptura.Rows(i).Cells(8).Value.ToString) & " where Codigo = '" & grdcaptura.Rows(i).Cells(2).Value.ToString & "'", sinfo)


                                    End If

                                End If

                            End If
                        Next
                        cnnp.Close()

                        MsgBox("Proceso terminado")

                    End If
                End With

            End If

        Else
            MsgBox("Hay que generar el reporte antes de actualizar la información")
        End If

    End Sub

    Private Sub grdcaptura_RowPrePaint(sender As Object, e As DataGridViewRowPrePaintEventArgs) Handles grdcaptura.RowPrePaint
        ' Alternar el color entre blanco y el color RGB(255, 255, 192) para las filas
        If e.RowIndex Mod 2 = 0 Then
            ' Fila blanca (fila par)
            grdcaptura.Rows(e.RowIndex).DefaultCellStyle.BackColor = Color.White
        Else
            ' Fila con el color RGB(255, 255, 192) (fila impar)
            grdcaptura.Rows(e.RowIndex).DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 192)
        End If

        ' Cambiar el color de la columna 6 (índice 5) a azul claro (LightBlue)
        If e.RowIndex >= 0 Then ' Verifica que la fila es válida
            ' Cambiar solo el color de la celda en la columna 6 (índice 5)
            grdcaptura.Rows(e.RowIndex).Cells(8).Style.BackColor = Color.LightBlue
        End If

    End Sub



    Private Sub btnPedido_Click(sender As Object, e As EventArgs) Handles btnPedido.Click

        Dim gridHeight As Integer = grdcaptura.Height

        txtCodBarras.Text = ""
        cboProveedor.Text = ""
        txtCorreo.Text = ""
        btnLimpiarPed.PerformClick()

        If gridHeight > 214 Then

            If grdcaptura.RowCount > 0 Then
                GroupBox4.Visible = True
            End If

            grdcaptura.Size = New System.Drawing.Size(977, 214)
        Else
            GroupBox4.Visible = False
            grdcaptura.Size = New System.Drawing.Size(977, 441)
        End If


    End Sub

    Private Sub btnLimpiarPed_Click(sender As Object, e As EventArgs) Handles btnLimpiarPed.Click
        DataGridView1.Rows.Clear()
        txtCantidadT.Text = 0
        txtTotalI.Text = 0
    End Sub

    Private Sub DataGridView1_RowPrePaint(sender As Object, e As DataGridViewRowPrePaintEventArgs) Handles DataGridView1.RowPrePaint

        If e.RowIndex Mod 2 = 0 Then
            DataGridView1.Rows(e.RowIndex).DefaultCellStyle.BackColor = Color.White
        Else
            DataGridView1.Rows(e.RowIndex).DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 192)
        End If

        If e.RowIndex >= 0 Then
            DataGridView1.Rows(e.RowIndex).Cells(3).Style.BackColor = Color.LightBlue
        End If

    End Sub

    Private Sub btnSeleccionar_Click(sender As Object, e As EventArgs) Handles btnSeleccionar.Click

        Dim banderaexistencia As Integer = 0

        If grdcaptura.Rows.Count > 0 Then

            If MsgBox("¿Desea agregar las cantidades sugeridas?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                banderaexistencia = 1
            End If

        End If

        For i = 0 To grdcaptura.Rows.Count - 1

            If grdcaptura.Rows(i).Cells(0).Value Then

                Dim banderaentra As Integer = 0

                For ii = 0 To DataGridView1.Rows.Count - 1
                    If DataGridView1.Rows(ii).Cells(0).Value.ToString = grdcaptura.Rows(i).Cells(2).Value.ToString Then
                        banderaentra = 1
                        Exit For
                    End If
                Next

                If banderaentra = 0 Then
                    If banderaexistencia > 0 Then
                        DataGridView1.Rows.Add(grdcaptura.Rows(i).Cells(2).Value.ToString, grdcaptura.Rows(i).Cells(3).Value.ToString, grdcaptura.Rows(i).Cells(4).Value.ToString, grdcaptura.Rows(i).Cells(1).Value.ToString, FormatNumber(grdcaptura.Rows(i).Cells(12).Value.ToString, 2), FormatNumber(CDec(grdcaptura.Rows(i).Cells(12).Value.ToString) * CDec(grdcaptura.Rows(i).Cells(1).Value.ToString), 2))
                    Else
                        DataGridView1.Rows.Add(grdcaptura.Rows(i).Cells(2).Value.ToString, grdcaptura.Rows(i).Cells(3).Value.ToString, grdcaptura.Rows(i).Cells(4).Value.ToString, 0, FormatNumber(grdcaptura.Rows(i).Cells(12).Value.ToString, 2), 0)
                    End If
                End If

            End If

        Next

        calcula()

        MsgBox("Proceso Terminado")

    End Sub

    Sub calcula()

        txtCantidadT.Text = 0
        txtTotalI.Text = 0

        For ii = 0 To DataGridView1.Rows.Count - 1
            txtCantidadT.Text = CDec(txtCantidadT.Text) + CDec(DataGridView1.Rows(ii).Cells(3).Value.ToString)
            txtTotalI.Text = CDec(txtTotalI.Text) + CDec(DataGridView1.Rows(ii).Cells(5).Value.ToString)
        Next

        txtCantidadT.Text = FormatNumber(txtCantidadT.Text, 2)
        txtTotalI.Text = FormatNumber(txtTotalI.Text, 2)

    End Sub

    Private Sub btnExcelPedido_Click(sender As Object, e As EventArgs) Handles btnExcelPedido.Click
        ExportarDataGridViewAExcel2(DataGridView1)
    End Sub

    Private Sub cboProveedor_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cboProveedor.KeyPress
        If AscW(e.KeyChar) = Keys.Enter Then

            txtCorreo.Text = ""

            If Trim(cboProveedor.Text) = "" Then
                Exit Sub
            End If

            Dim cnnp As New MySql.Data.MySqlClient.MySqlConnection
            Dim sinfo As String = ""
            Dim odatap As New ToolKitSQL.myssql
            Dim dt As New DataTable
            Dim dr As DataRow
            With odatap
                If .dbOpen(cnnp, sTarget, sinfo) Then
                    If .getDr(cnnp, dr, "SELECT Correo FROM proveedores where NComercial = '" & cboProveedor.Text & "'", sinfo) Then
                        txtCorreo.Text = dr(0).ToString
                    End If
                    cnnp.Close()
                End If
            End With

        End If
    End Sub

    Private Sub cboProveedor_SelectedValueChanged(sender As Object, e As EventArgs) Handles cboProveedor.SelectedValueChanged
        On Error GoTo puertita

        txtCorreo.Text = ""

        Dim cnnp As New MySql.Data.MySqlClient.MySqlConnection
        Dim sinfo As String = ""
        Dim odatap As New ToolKitSQL.myssql
        Dim dr As DataRow
        With odatap
            If .dbOpen(cnnp, sTarget, sinfo) Then

                If .getDr(cnnp, dr, "SELECT Correo FROM proveedores where NComercial = '" & cboProveedor.Text & "'", sinfo) Then
                    txtCorreo.Text = dr(0).ToString
                End If
                cnnp.Close()

            End If
        End With

        Exit Sub
puertita:
        txtCorreo.Text = ""
        cnnp.Close()

    End Sub

    Private Sub grdcaptura_ColumnHeaderMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles grdcaptura.ColumnHeaderMouseClick
        ' Verificar si se ha hecho clic en la primera columna (índice 0)
        If e.ColumnIndex = 0 Then

            If lblTodos.Text = "TODOS" Then
                lblTodos.Text = "1"
                For i = 0 To grdcaptura.Rows.Count - 1
                    grdcaptura.Rows(i).Cells(0).Value = True
                Next
            Else
                lblTodos.Text = "TODOS"
                For i = 0 To grdcaptura.Rows.Count - 1
                    grdcaptura.Rows(i).Cells(0).Value = False
                Next
            End If

        End If
    End Sub

    Private Sub txtCodBarras_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtCodBarras.KeyPress
        If AscW(e.KeyChar) = Keys.Enter Then

            If Trim(txtCodBarras.Text) = "" Then
                Exit Sub
            End If

            Dim cnnp As New MySql.Data.MySqlClient.MySqlConnection
            Dim sinfo As String = ""
            Dim odatap As New ToolKitSQL.myssql
            Dim dt As New DataTable
            Dim dr As DataRow
            With odatap
                If .dbOpen(cnnp, sTarget, sinfo) Then
                    If .getDr(cnnp, dr, "select Codigo, Nombre, UCompra, PrecioCompra from productos where CodBarra = '" & Trim(txtCodBarras.Text) & "' and CHAR_LENGTH(Codigo) < 7  order by Codigo ", sinfo) Then

                        Dim banderaentra As Integer = 0

                        For ii = 0 To DataGridView1.Rows.Count - 1
                            If DataGridView1.Rows(ii).Cells(0).Value.ToString = dr("Codigo").ToString Then
                                banderaentra = 1
                                Exit For
                            End If
                        Next

                        If banderaentra = 0 Then
                            DataGridView1.Rows.Add(dr("Codigo").ToString, dr("Nombre").ToString, dr("UCompra").ToString, 0, FormatNumber(dr("PrecioCompra").ToString, 2), 0)
                        End If

                        txtCodBarras.Text = ""

                    End If

                    cnnp.Close()
                End If
            End With

            calcula()

        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        cbodesc.Text = ""
        GroupBox3.Visible = False
        txtProdClave.Text = ""
        txtCodBarras.Focus()
    End Sub

    Private Sub btnBusqueda_Click(sender As Object, e As EventArgs) Handles btnBusqueda.Click

        If GroupBox3.Visible Then
            cbodesc.Text = ""
            txtProdClave.Text = ""
            GroupBox3.Visible = False
            txtCodBarras.Focus()
        Else
            cbodesc.Text = ""
            txtProdClave.Text = ""
            GroupBox3.Visible = True
            cbodesc.Focus()
        End If

    End Sub

    Private Sub chkBuscaProd_CheckedChanged(sender As Object, e As EventArgs) Handles chkBuscaProd.CheckedChanged
        If (chkBuscaProd.Checked) Then
            txtProdClave.Text = ""
            Panel4.Visible = True
            txtProdClave.Focus().Equals(True)
        Else
            cbodesc.Focus()
            txtProdClave.Text = ""
            Panel4.Visible = False
        End If
    End Sub

    Private Sub cbodesc_DropDown(sender As Object, e As EventArgs) Handles cbodesc.DropDown
        cbodesc.Items.Clear()
        Dim cnnp As New MySql.Data.MySqlClient.MySqlConnection
        Dim sinfo As String = ""
        Dim odatap As New ToolKitSQL.myssql
        Dim dt As New DataTable
        Dim dr As DataRow
        With odatap
            If .dbOpen(cnnp, sTarget, sinfo) Then

                Dim varstr As String = ""
                If Trim(txtProdClave.Text) <> "" Then
                    varstr = "select distinct Nombre from productos where Nombre like '%" & Trim(txtProdClave.Text) & "%' and CHAR_LENGTH(Codigo) < 7 order by Nombre "
                Else
                    varstr = "select distinct Nombre from productos where CHAR_LENGTH(Codigo) < 7 order by Nombre "
                End If

                If .getDt(cnnp, dt, varstr, sinfo) Then
                    For Each dr In dt.Rows
                        cbodesc.Items.Add(dr(0).ToString)
                    Next
                End If

                cnnp.Close()
            End If
        End With
    End Sub

    Private Sub btnAgregar_Click(sender As Object, e As EventArgs) Handles btnAgregar.Click

        If Trim(cbodesc.Text) = "" Then
            Exit Sub
        End If

        Dim cnnp As New MySql.Data.MySqlClient.MySqlConnection
        Dim sinfo As String = ""
        Dim odatap As New ToolKitSQL.myssql
        Dim dt As New DataTable
        Dim dr As DataRow
        With odatap
            If .dbOpen(cnnp, sTarget, sinfo) Then
                If .getDr(cnnp, dr, "select Codigo, Nombre, UCompra, PrecioCompra from productos where Nombre = '" & Trim(cbodesc.Text) & "' and CHAR_LENGTH(Codigo) < 7  order by Codigo ", sinfo) Then

                    Dim banderaentra As Integer = 0

                    For ii = 0 To DataGridView1.Rows.Count - 1
                        If DataGridView1.Rows(ii).Cells(0).Value.ToString = dr("Codigo").ToString Then
                            banderaentra = 1
                            Exit For
                        End If
                    Next

                    If banderaentra = 0 Then
                        DataGridView1.Rows.Add(dr("Codigo").ToString, dr("Nombre").ToString, dr("UCompra").ToString, 0, FormatNumber(dr("PrecioCompra").ToString, 2), 0)
                    End If

                    cbodesc.Text = ""

                End If

                cnnp.Close()
            End If
        End With

        calcula()

    End Sub

    Private Sub cbodesc_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cbodesc.KeyPress

        If AscW(e.KeyChar) = Keys.Enter Then

            If Trim(cbodesc.Text) = "" Then
                Exit Sub
            End If

            Dim cnnp As New MySql.Data.MySqlClient.MySqlConnection
            Dim sinfo As String = ""
            Dim odatap As New ToolKitSQL.myssql
            Dim dt As New DataTable
            Dim dr As DataRow
            With odatap
                If .dbOpen(cnnp, sTarget, sinfo) Then
                    If .getDr(cnnp, dr, "select Codigo, Nombre, UCompra, PrecioCompra from productos where Nombre = '" & Trim(cbodesc.Text) & "' and CHAR_LENGTH(Codigo) < 7  order by Codigo ", sinfo) Then

                        Dim banderaentra As Integer = 0

                        For ii = 0 To DataGridView1.Rows.Count - 1
                            If DataGridView1.Rows(ii).Cells(0).Value.ToString = dr("Codigo").ToString Then
                                banderaentra = 1
                                Exit For
                            End If
                        Next

                        If banderaentra = 0 Then
                            DataGridView1.Rows.Add(dr("Codigo").ToString, dr("Nombre").ToString, dr("UCompra").ToString, 0, FormatNumber(dr("PrecioCompra").ToString, 2), 0)

                        End If

                        cbodesc.Text = ""
                        GroupBox3.Visible = False
                        txtCodBarras.Focus()


                    End If

                    cnnp.Close()
                End If
            End With

            calcula()

        End If

    End Sub

    Private Sub txtProdClave_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtProdClave.KeyPress

        If AscW(e.KeyChar) = Keys.Enter Then
            Panel4.Visible = False
        End If


    End Sub

    Private Sub DataGridView1_UserDeletedRow(sender As Object, e As DataGridViewRowEventArgs) Handles DataGridView1.UserDeletedRow
        calcula()
    End Sub

    Private Sub DataGridView1_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellValueChanged
        If e.RowIndex >= 0 And e.ColumnIndex = 3 Then

            Dim newValue As Object = DataGridView1.Rows(e.RowIndex).Cells(3).Value

            If IsNumeric(newValue) Then
                Dim ope As Double = DataGridView1.Rows(e.RowIndex).Cells(3).Value
                If ope < 0 Then
                    DataGridView1.Rows(e.RowIndex).Cells(5).Value = FormatNumber(0, 2)
                Else
                    ope = ope * DataGridView1.Rows(e.RowIndex).Cells(4).Value
                    DataGridView1.Rows(e.RowIndex).Cells(5).Value = FormatNumber(ope, 2)
                End If
            Else
                DataGridView1.Rows(e.RowIndex).Cells(3).Value = 0
                DataGridView1.Rows(e.RowIndex).Cells(5).Value = FormatNumber(0, 2)
            End If

            calcula()

        End If
    End Sub

    Private Sub btnSinAgregar_Click(sender As Object, e As EventArgs) Handles btnSinAgregar.Click
        GroupBox4.Visible = False
    End Sub

    Private Sub btnAgregarSugerida_Click(sender As Object, e As EventArgs) Handles btnAgregarSugerida.Click

        For i = 0 To grdcaptura.Rows.Count - 1

            If grdcaptura.Rows(i).Cells(0).Value Then

                Dim banderaentra As Integer = 0

                For ii = 0 To DataGridView1.Rows.Count - 1
                    If DataGridView1.Rows(ii).Cells(0).Value.ToString = grdcaptura.Rows(i).Cells(2).Value.ToString Then
                        banderaentra = 1
                        Exit For
                    End If
                Next

                If banderaentra = 0 Then
                    DataGridView1.Rows.Add(grdcaptura.Rows(i).Cells(2).Value.ToString, grdcaptura.Rows(i).Cells(3).Value.ToString, grdcaptura.Rows(i).Cells(4).Value.ToString, grdcaptura.Rows(i).Cells(1).Value.ToString, FormatNumber(grdcaptura.Rows(i).Cells(12).Value.ToString, 2), FormatNumber(CDec(grdcaptura.Rows(i).Cells(12).Value.ToString) * CDec(grdcaptura.Rows(i).Cells(1).Value.ToString), 2))
                End If

            End If

        Next

        calcula()

        GroupBox4.Visible = False

        MsgBox("Proceso Terminado")

    End Sub

    Private Sub btnAgregar0_Click(sender As Object, e As EventArgs) Handles btnAgregar0.Click

        For i = 0 To grdcaptura.Rows.Count - 1

            If grdcaptura.Rows(i).Cells(0).Value Then

                Dim banderaentra As Integer = 0

                For ii = 0 To DataGridView1.Rows.Count - 1
                    If DataGridView1.Rows(ii).Cells(0).Value.ToString = grdcaptura.Rows(i).Cells(2).Value.ToString Then
                        banderaentra = 1
                        Exit For
                    End If
                Next

                If banderaentra = 0 Then
                    DataGridView1.Rows.Add(grdcaptura.Rows(i).Cells(2).Value.ToString, grdcaptura.Rows(i).Cells(3).Value.ToString, grdcaptura.Rows(i).Cells(4).Value.ToString, 0, FormatNumber(grdcaptura.Rows(i).Cells(12).Value.ToString, 2), 0)
                End If

            End If

        Next

        calcula()

        GroupBox4.Visible = False

        MsgBox("Proceso Terminado")

    End Sub

    Private Sub btnPDFPedido_Click(sender As Object, e As EventArgs) Handles btnPDFPedido.Click

        crea_dir("C:\ControlNegociosPro\ReportesPedidos\")

        Dim var1 As String = Format(Date.Now, "ddMMyyyyHHmmss")
        Dim root_name_recibo As String = "C:\ControlNegociosPro\ReportesPedidos\Pedidos_" & var1 & ".pdf"

        printRecibo(root_name_recibo, 1)

    End Sub

    Public Sub printRecibo(ByVal root_name_recibo As String, ByVal varmostar As Integer)

        If File.Exists(root_name_recibo) Then
            File.Delete(root_name_recibo)
        End If

        Dim pdfDoc As New Document(PageSize.A4, 15.0F, 15.0F, 30.0F, 30.0F)
        Dim pdfWrite As PdfWriter

        pdfWrite = PdfWriter.GetInstance(pdfDoc, New FileStream(root_name_recibo, FileMode.CreateNew))

        Dim piepagina As New preubavb1
        pdfWrite.PageEvent = piepagina

        Dim Font8 As New iTextSharp.text.Font(FontFactory.GetFont(FontFactory.HELVETICA, 8, iTextSharp.text.Font.NORMAL))
        Dim Font12 As New iTextSharp.text.Font(FontFactory.GetFont(FontFactory.HELVETICA, 12, iTextSharp.text.Font.NORMAL))
        Dim FontB6 As New iTextSharp.text.Font(FontFactory.GetFont(FontFactory.HELVETICA, 6, iTextSharp.text.Font.BOLD))
        Dim FontB8 As New iTextSharp.text.Font(FontFactory.GetFont(FontFactory.HELVETICA, 8, iTextSharp.text.Font.BOLD))
        Dim FontB12 As New iTextSharp.text.Font(FontFactory.GetFont(FontFactory.HELVETICA, 12, iTextSharp.text.Font.BOLD))
        Dim FontB14 As New iTextSharp.text.Font(FontFactory.GetFont(FontFactory.HELVETICA, 14, iTextSharp.text.Font.BOLD))
        Dim FontB16 As New iTextSharp.text.Font(FontFactory.GetFont(FontFactory.HELVETICA, 16, iTextSharp.text.Font.BOLD))
        Dim FontB18 As New iTextSharp.text.Font(FontFactory.GetFont(FontFactory.HELVETICA, 18, iTextSharp.text.Font.BOLD))
        Dim CVacio As PdfPCell = New PdfPCell(New Phrase(""))
        CVacio.Border = 0

        pdfDoc.Open()

        Dim varYlinea As Integer = 810

        Dim Table1 As PdfPTable = New PdfPTable(5)
        Dim Col1 As PdfPCell
        Dim Col2 As PdfPCell
        Dim Col3 As PdfPCell
        Dim Col4 As PdfPCell
        Dim Col5 As PdfPCell
        Dim Col6 As PdfPCell
        Dim Col7 As PdfPCell
        Dim ILine As Integer
        Dim iFila As Integer
        Table1.WidthPercentage = 95

        Dim widths As Single() = New Single() {4.0F, 1.0F, 1.0F, 5.0F, 0.5F}

        widths = New Single() {4.0F, 1.0F, 1.0F, 5.0F, 0.5F}

        Table1.SetWidths(widths)

#Region "Tabla.Encabezado"

        Col1 = New PdfPCell(New Phrase("Pedido para " & Trim(cboProveedor.Text), FontB18))
        Col1.Border = 0
        Col1.Colspan = 5
        Col1.HorizontalAlignment = 1
        Table1.AddCell(Col1)

        varYlinea = varYlinea - 12

        Col1 = New PdfPCell(New Phrase(" ", FontB18))
        Col1.Border = 0
        Col1.Colspan = 5
        Col1.HorizontalAlignment = 1
        Table1.AddCell(Col1)

        varYlinea = varYlinea - 12

        Col1 = New PdfPCell(New Phrase(" ", FontB16))

        Col1.Border = 0
        Col1.Colspan = 3
        Table1.AddCell(Col1)

        Col2 = New PdfPCell(New Phrase("Fecha doc.: " & Trim(Format(Date.Now, "dd/MM/yyyy")), FontB16))
        Col2.Border = 0
        Col2.Colspan = 2
        Col2.HorizontalAlignment = 2
        Table1.AddCell(Col2)

        varYlinea = varYlinea - 12

        Col1 = New PdfPCell(New Phrase(" ", Font12))
        Col1.Border = 0
        Col1.Colspan = 5
        Col1.HorizontalAlignment = 1
        Table1.AddCell(Col1)

        varYlinea = varYlinea - 12

        pdfDoc.Add(Table1)

#End Region

        varYlinea = varYlinea - 12

        Dim Table3 As PdfPTable = New PdfPTable(3)
        Dim widths3 As Single() = New Single() {6.0F, 2.0F, 3.0F}
        Table3.WidthPercentage = 95
        Table3.SetWidths(widths3)
        'Table3.DefaultCell.BorderWidth = 2

        Col1 = New PdfPCell(New Phrase("Nombre", FontB8))
        Col1.BorderWidthLeft = 0
        Col1.BorderWidthRight = 0
        Col1.BorderWidth = 1
        Col1.HorizontalAlignment = 0
        Table3.AddCell(Col1)
        Col2 = New PdfPCell(New Phrase("Unidad", FontB8))
        Col2.BorderWidthLeft = 0
        Col2.BorderWidthRight = 0
        Col2.BorderWidth = 1
        Col1.HorizontalAlignment = 0
        Table3.AddCell(Col2)
        Col3 = New PdfPCell(New Phrase("Cantidad", FontB8))
        Col3.BorderWidthLeft = 0
        Col3.BorderWidthRight = 0
        Col3.BorderWidth = 1
        Col3.HorizontalAlignment = 1
        Table3.AddCell(Col3)

        pdfDoc.Add(Table3)

#Region "Tabla4-detalle"

        If DataGridView1.Rows.Count > 0 Then

            For i = 0 To DataGridView1.Rows.Count - 1

                Dim Table4 As PdfPTable = New PdfPTable(3)
                Dim widths4 As Single() = New Single() {6.0F, 2.0F, 3.0F}
                Table4.WidthPercentage = 95
                Table4.SetWidths(widths4)
                'Table4.DefaultCell.BorderWidth = 2

                Col1 = New PdfPCell(New Phrase(DataGridView1.Rows(i).Cells(1).Value.ToString, Font8))
                Col1.BorderWidthLeft = 0
                Col1.BorderWidthRight = 0
                Col1.BorderWidth = 0.3
                Col1.HorizontalAlignment = 0
                Table4.AddCell(Col1)

                Col1 = New PdfPCell(New Phrase(DataGridView1.Rows(i).Cells(2).Value.ToString, Font8))
                Col1.BorderWidthLeft = 0
                Col1.BorderWidthRight = 0
                Col1.BorderWidth = 0.3
                Col1.HorizontalAlignment = 0
                Table4.AddCell(Col1)

                Col1 = New PdfPCell(New Phrase(DataGridView1.Rows(i).Cells(3).Value.ToString, Font8))
                Col1.BorderWidthLeft = 0
                Col1.BorderWidthRight = 0
                Col1.BorderWidth = 0.3
                Col1.HorizontalAlignment = 1
                Table4.AddCell(Col1)

                pdfDoc.Add(Table4)

            Next

        End If

#End Region

        Dim Table5 As PdfPTable = New PdfPTable(3)
        Dim widths5 As Single() = New Single() {6.0F, 2.0F, 3.0F}
        Table5.WidthPercentage = 95
        Table5.SetWidths(widths5)

        Col1 = New PdfPCell(New Phrase(" ", Font12))
        Col1.Border = 0
        'Col1.Colspan = 6
        Col1.HorizontalAlignment = 1
        Table5.AddCell(Col1)

        varYlinea = varYlinea - 12

        Col1 = New PdfPCell(New Phrase(" ", FontB12))
        Col1.BorderWidthLeft = 0
        Col1.BorderWidthRight = 0
        Col1.Border = 0
        'Col1.BorderWidth = 0.3
        Col1.HorizontalAlignment = 1
        Table5.AddCell(Col1)

        Col1 = New PdfPCell(New Phrase("Cantidad de Productos", FontB12))
        Col1.BorderWidthLeft = 0
        Col1.BorderWidthRight = 0
        Col1.Border = 0
        'Col1.BorderWidth = 0.3
        Col1.HorizontalAlignment = 1
        Table5.AddCell(Col1)

        varYlinea = varYlinea - 12

        Col1 = New PdfPCell(New Phrase(" ", FontB12))
        Col1.BorderWidthLeft = 0
        Col1.BorderWidthRight = 0
        Col1.Border = 0
        'Col1.BorderWidth = 0.3
        Col1.HorizontalAlignment = 2
        Table5.AddCell(Col1)

        Col1 = New PdfPCell(New Phrase(" ", FontB12))
        Col1.BorderWidthLeft = 0
        Col1.BorderWidthRight = 0
        Col1.Border = 0
        'Col1.BorderWidth = 0.3
        Col1.HorizontalAlignment = 1
        Table5.AddCell(Col1)

        Col1 = New PdfPCell(New Phrase(txtCantidadT.Text, FontB12))
        Col1.BorderWidthLeft = 0
        Col1.BorderWidthRight = 0
        Col1.Border = 0
        'Col1.BorderWidth = 0.3
        Col1.HorizontalAlignment = 1
        Table5.AddCell(Col1)

        pdfDoc.Add(Table5)

        If varYlinea > 431 Then
            varYlinea -= 200 '307 '295
        End If

        For iFila = 1 To 32
            pdfDoc.Add(New Paragraph(" "))
            ILine = pdfWrite.GetVerticalPosition(True)
            If ILine < varYlinea Then
                Exit For
            End If
        Next

        pdfDoc.Close()

        Try

            My.Application.DoEvents()

            If varmostar = 1 Then
                'If MsgBox("¿Desea Abrir el Archivo?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                Process.Start(root_name_recibo)
                'End If
            End If


        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub btnCorreo_Click(sender As Object, e As EventArgs) Handles btnCorreo.Click

        If Trim(txtCorreo.Text) = "" Then
            MsgBox("Hay que colocar un correo")
            Exit Sub
        End If

        ' Obtener el valor del TextBox
        Dim email As String = txtCorreo.Text

        ' Expresión regular para validar el formato del correo electrónico
        Dim emailPattern As String = "^[\w\.-]+@[\w\.-]+\.[a-zA-Z]{2,}$"

        ' Verificar si el valor cumple con el patrón
        If Regex.IsMatch(email, emailPattern) Then
            'MsgBox("El correo electrónico es válido.")
        Else
            MsgBox("El correo electrónico no es válido, Favor de verificarlo.")
            txtCorreo.Focus()
            Exit Sub
        End If



        pbEnvioCorreo.Visible = True
        lblEnvioCorreo.Visible = True
        My.Application.DoEvents()

        crea_dir("C:\ControlNegociosPro\ReportesPedidos\")

        Dim var1 As String = Format(Date.Now, "ddMMyyyyHHmmss")
        Dim root_name_recibo As String = "C:\ControlNegociosPro\ReportesPedidos\Pedidos_" & var1 & ".pdf"

        printRecibo(root_name_recibo, 0)

        archivoadj = root_name_recibo

        Timer1.Start()

        Exit Sub

        'txtCodBarras.Text = ""
        'cboProveedor.Text = ""
        'txtCorreo.Text = ""
        'btnLimpiarPed.PerformClick()

        'pbEnvioCorreo.Visible = False
        'lblEnvioCorreo.Visible = False
        'My.Application.DoEvents()

    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Timer1.Stop()

        If envioPed(txtCorreo.Text, "Solicitud de pedido", "", archivoadj) Then
            txtCodBarras.Text = ""
            cboProveedor.Text = ""
            txtCorreo.Text = ""
            btnLimpiarPed.PerformClick()

            pbEnvioCorreo.Visible = False
            lblEnvioCorreo.Visible = False
            My.Application.DoEvents()
        Else
            pbEnvioCorreo.Visible = False
            lblEnvioCorreo.Visible = False
            My.Application.DoEvents()
        End If

    End Sub

    Private Function envioPed(ByVal paraf As String, ByVal asuntof As String, ByVal mensajef As String, ByVal archivof As String)

        If archivof <> "" Then

            Dim cuentamail As String = ""
            Dim nombree As String = ""
            Dim puerto As String = ""
            Dim seguridad As String = ""
            Dim passmail As String = ""

            recupera_campos(cuentamail, nombree, puerto, seguridad, passmail)

            Dim mail As New MailMessage
            Dim oAttch As Net.Mail.AttachmentBase = New Net.Mail.Attachment(archivof)
            Dim oAttch2 As Net.Mail.AttachmentBase
            Try
                mail.From = New MailAddress(cuentamail, nombree)
                mail.To.Add(paraf)
                mail.Subject = (asuntof)
                mail.Body = (mensajef)
                mail.Attachments.Add(oAttch)

                Dim servidorx As New SmtpClient(servidor)
                servidorx.Port = puerto
                servidorx.EnableSsl = seguridad
                servidorx.Credentials = New System.Net.NetworkCredential(cuentamail, passmail)
                servidorx.Send(mail)
                MessageBox.Show("Mensaje enviado correctamene", "Exito!", MessageBoxButtons.OK)
                txtCodBarras.Focus()
                Return True

            Catch ex As Exception
                MessageBox.Show(ex.ToString, "Error!", MessageBoxButtons.OK)
                Return False
            End Try
        Else
        End If
#Disable Warning BC42105 ' La función 'envio' no devuelve un valor en todas las rutas de acceso de código. Puede producirse una excepción de referencia NULL en tiempo de ejecución cuando se use el resultado.

    End Function

    Private Sub recupera_campos(ByRef var_cuentamail As String, ByRef var_nombree As String, ByRef var_puerto As String, ByRef var_seguridad As String, ByRef var_passmail As String)

        Dim cnn As MySql.Data.MySqlClient.MySqlConnection = New MySql.Data.MySqlClient.MySqlConnection
        Dim sSQL As String = "SELECT * FROM Formatos"
        Dim sinfo As String = ""
        Dim dr As DataRow
        Dim dt As New DataTable
        Dim oData As New ToolKitSQL.myssql
        With oData
            If .dbOpen(cnn, sTargetlocal, sinfo) Then
                If .getDt(cnn, dt, sSQL, sinfo) Then
                    For Each dr In dt.Rows
                        Select Case dr("Facturas").ToString
                            Case "Mail_Emi"
                                var_cuentamail = dr("NotasCred").ToString
                            Case "Shibboleth_ML"
                                var_passmail = dr("NotasCred").ToString
                            Case "Server_SMTP"
                                servidor = dr("NotasCred").ToString
                            Case "Pto_Mail"
                                var_puerto = dr("NotasCred").ToString
                            Case "SSL"
                                var_seguridad = dr("NotasCred").ToString
                        End Select
                    Next
                    var_seguridad = True
                    var_nombree = varnomemail
                Else
                    MsgBox("Error en la configuracion de correo")
                End If
                cnn.Close()
            End If
        End With

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        frmRepComparador2.Show()
        frmRepComparador2.lblProveedor.Text = "Proveedor: " & Trim(cboProveedor.Text)
        frmRepComparador2.grdPrecios.Rows.Clear()
        If DataGridView1.Rows.Count > 0 Then

            Dim cnn As MySql.Data.MySqlClient.MySqlConnection = New MySql.Data.MySqlClient.MySqlConnection
            Dim sinfo As String = ""
            Dim dr As DataRow
            Dim dt As New DataTable
            Dim oData As New ToolKitSQL.myssql
            With oData
                If .dbOpen(cnn, sTargetlocal, sinfo) Then

                    For i = 0 To DataGridView1.Rows.Count - 1

                        If Trim(DataGridView1.Rows(i).Cells(0).Value.ToString) <> "" Then

                            If .getDt(cnn, dt, "SELECT Codigo,Descripcion,Proveedor,PrecioAnt FROM precios WHERE Codigo='" & Trim(DataGridView1.Rows(i).Cells(0).Value.ToString) & "' order by PrecioAnt", sinfo) Then

                                For Each dr In dt.Rows
                                    frmRepComparador2.grdPrecios.Rows.Add(dr("Codigo").ToString, dr("Descripcion").ToString, dr("PrecioAnt").ToString, dr("Proveedor").ToString)
                                Next

                            End If

                        End If

                    Next

                    Dim varCodAnt As String = ""
                    Dim varprecio As Double = 0

                    For i = 0 To frmRepComparador2.grdPrecios.Rows.Count - 1

                        If varCodAnt <> Trim(frmRepComparador2.grdPrecios.Rows(i).Cells(0).Value.ToString) Then
                            varCodAnt = Trim(frmRepComparador2.grdPrecios.Rows(i).Cells(0).Value.ToString)
                            varprecio = frmRepComparador2.grdPrecios.Rows(i).Cells(2).Value.ToString
                            frmRepComparador2.grdPrecios.Rows(i).Cells(2).Style.BackColor = Color.GreenYellow
                        Else
                            If varprecio = frmRepComparador2.grdPrecios.Rows(i).Cells(2).Value.ToString Then
                                frmRepComparador2.grdPrecios.Rows(i).Cells(2).Style.BackColor = Color.GreenYellow
                            End If
                        End If

                        If Trim(frmRepComparador2.grdPrecios.Rows(i).Cells(3).Value.ToString) = Trim(cboProveedor.Text) Then
                            frmRepComparador2.grdPrecios.Rows(i).Cells(3).Style.BackColor = Color.Orange
                        End If

                    Next

                    cnn.Close()
                End If
            End With

        End If

    End Sub
End Class