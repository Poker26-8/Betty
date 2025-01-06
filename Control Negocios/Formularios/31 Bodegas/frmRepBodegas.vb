
Imports ClosedXML.Excel
Imports MySql.Data.MySqlClient

Public Class frmRepBodegas
    Private Sub frmRepBodegas_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        mCalendar1.SetDate(Now)
        mCalendar2.SetDate(Now)
    End Sub

    Private Sub optbodega_CheckedChanged(sender As Object, e As EventArgs) Handles optbodega.CheckedChanged
        If (optbodega.Checked) Then
            grdCaptura.ColumnCount = 0
            grdCaptura.Rows.Clear()

            cbobodega.Visible = True
            cbogeneral.Visible = True
            cbogeneral.Text = ""
            cbogeneral.Items.Clear()
            cbobodega.Text = ""
            cbobodega.Items.Clear()

            grdCaptura.ColumnCount = 6
            With grdCaptura
                'Columna 'Id'
                .Columns(0).Name = "Id"
                .Columns(0).HeaderText = "Id"
                .Columns(0).Width = 50
                .Columns(0).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

                'Columna 'Fecha'
                .Columns(1).Name = "Fecha"
                .Columns(1).HeaderText = "Fecha"
                .Columns(1).Width = 110
                .Columns(1).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

                'Columna 'Hora'
                .Columns(2).Name = "Hora"
                .Columns(2).HeaderText = "Hora"
                .Columns(2).Width = 80
                .Columns(2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

                'Columna 'Nombre'
                .Columns(3).Name = "Nombre"
                .Columns(3).HeaderText = "Nombre"
                .Columns(3).Width = 200
                .Columns(3).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                .Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft

                'Columna 'Movimiento'
                .Columns(4).Name = "Movimiento"
                .Columns(4).HeaderText = "Movimiento"
                .Columns(4).Width = 180
                .Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft

                'Columna 'Usuario'
                .Columns(5).Name = "Usuario"
                .Columns(5).HeaderText = "Usuario"
                .Columns(5).Width = 90
                .Columns(5).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            End With
        End If
    End Sub

    Private Sub optcliente_CheckedChanged(sender As Object, e As EventArgs) Handles optcliente.CheckedChanged
        If (optcliente.Checked) Then
            grdCaptura.ColumnCount = 0
            grdCaptura.Rows.Clear()

            cbobodega.Visible = False
            cbogeneral.Visible = True
            cbogeneral.Text = ""
            cbogeneral.Items.Clear()
            cbobodega.Text = ""
            cbobodega.Items.Clear()

            grdCaptura.ColumnCount = 6
            With grdCaptura
                'Columna 'Id'
                .Columns(0).Name = "Id"
                .Columns(0).HeaderText = "Id"
                .Columns(0).Width = 50
                .Columns(0).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

                'Columna 'Movimiento'
                .Columns(1).Name = "Bodega"
                .Columns(1).HeaderText = "Bodega"
                .Columns(1).Width = 190
                .Columns(1).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft

                'Columna 'Fecha'
                .Columns(2).Name = "Movimiento"
                .Columns(2).HeaderText = "Movimiento"
                .Columns(2).Width = 170
                .Columns(2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft

                'Columna 'Hora'
                .Columns(3).Name = "Fecha"
                .Columns(3).HeaderText = "Fecha"
                .Columns(3).Width = 110
                .Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

                'Columna 'Nombre'
                .Columns(4).Name = "Hora"
                .Columns(4).HeaderText = "Hora"
                .Columns(4).Width = 80
                .Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

                'Columna 'Usuario'
                .Columns(5).Name = "Usuario"
                .Columns(5).HeaderText = "Usuario"
                .Columns(5).Width = 90
                .Columns(5).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            End With
        End If
    End Sub

    Private Sub optvisitas_CheckedChanged(sender As Object, e As EventArgs) Handles optvisitas.CheckedChanged
        If (optvisitas.Checked) Then
            grdCaptura.ColumnCount = 0
            grdCaptura.Rows.Clear()

            cbobodega.Visible = True
            cbogeneral.Visible = True
            cbogeneral.Text = ""
            cbogeneral.Items.Clear()
            cbobodega.Text = ""
            cbobodega.Items.Clear()

            grdCaptura.ColumnCount = 6
            With grdCaptura
                'Columna 'Id'
                .Columns(0).Name = "Id"
                .Columns(0).HeaderText = "Id"
                .Columns(0).Width = 50
                .Columns(0).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

                'Columna 'Movimiento'
                .Columns(1).Name = "Movimiento"
                .Columns(1).HeaderText = "Movimiento"
                .Columns(1).Width = 180
                .Columns(1).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft

                'Columna 'Fecha'
                .Columns(2).Name = "Fecha"
                .Columns(2).HeaderText = "Fecha"
                .Columns(2).Width = 110
                .Columns(2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

                'Columna 'Hora'
                .Columns(3).Name = "Hora"
                .Columns(3).HeaderText = "Hora"
                .Columns(3).Width = 80
                .Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

                'Columna 'Nombre'
                .Columns(4).Name = "Nombre"
                .Columns(4).HeaderText = "Nombre"
                .Columns(4).Width = 200
                .Columns(4).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                .Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft

                'Columna 'Usuario'
                .Columns(5).Name = "Usuario"
                .Columns(5).HeaderText = "Usuario"
                .Columns(5).Width = 90
                .Columns(5).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            End With
        End If
    End Sub

    Private Sub cbogeneral_DropDown(sender As Object, e As EventArgs) Handles cbogeneral.DropDown
        cbogeneral.Items.Clear()

        Dim cnn1 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
        Dim rd1 As MySqlDataReader
        Dim cmd1 As MySqlCommand

        If (optbodega.Checked) Or (optvisitas.Checked) Then
            Try
                cnn1.Close() : cnn1.Open()
                cmd1 = cnn1.CreateCommand
                cmd1.CommandText =
                    "select distinct Ubicacion from Bodegas"
                rd1 = cmd1.ExecuteReader
                Do While rd1.Read
                    If rd1.HasRows Then cbogeneral.Items.Add(
                        rd1(0).ToString
                        )
                Loop
                rd1.Close()
                cnn1.Close()
            Catch ex As Exception
                MessageBox.Show(ex.ToString)
                cnn1.Close()
            End Try
        End If

        If (optcliente.Checked) Then
            Try
                cnn1.Close() : cnn1.Open()
                cmd1 = cnn1.CreateCommand
                cmd1.CommandText =
                    "select distinct Nombre from Movimientos where Id_Cliente<>0"
                rd1 = cmd1.ExecuteReader
                Do While rd1.Read
                    If rd1.HasRows Then cbogeneral.Items.Add(
                        rd1(0).ToString
                        )
                Loop
                rd1.Close()
                cnn1.Close()
            Catch ex As Exception
                MessageBox.Show(ex.ToString)
                cnn1.Close()
            End Try
        End If
    End Sub

    Private Sub cbogeneral_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cbogeneral.KeyPress
        If AscW(e.KeyChar) = Keys.Enter Then
            If cbobodega.Visible = True Then cbobodega.Focus().Equals(True)
            btnreporte.Focus().Equals(True)
        End If
    End Sub

    Private Sub cbobodega_DropDown(sender As Object, e As EventArgs) Handles cbobodega.DropDown
        cbobodega.Items.Clear()

        Dim cnn1 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
        Dim rd1 As MySqlDataReader
        Dim cmd1 As MySqlCommand

        If cbogeneral.Text <> "" Then
            Try
                cnn1.Close() : cnn1.Open()

                cmd1 = cnn1.CreateCommand
                cmd1.CommandText =
                    "select Nombre from Bodegas where Ubicacion='" & cbogeneral.Text & "'"
                rd1 = cmd1.ExecuteReader
                Do While rd1.Read
                    If rd1.HasRows Then cbobodega.Items.Add(
                        rd1(0).ToString
                        )
                Loop
                rd1.Close()
                cnn1.Close()
            Catch ex As Exception
                MessageBox.Show(ex.ToString)
                cnn1.Close()
            End Try
        End If
    End Sub

    Private Sub cbobodega_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cbobodega.KeyPress
        If AscW(e.KeyChar) = Keys.Enter Then
            btnreporte.Focus().Equals(True)
        End If
    End Sub

    Private Sub btnreporte_Click(sender As Object, e As EventArgs) Handles btnreporte.Click

        grdCaptura.Rows.Clear()
        If Not (optbodega.Checked) And Not (optcliente.Checked) And Not (optvisitas.Checked) Then Exit Sub

        Dim conteo As Integer = 0
        Dim Month1 As Date = mCalendar1.SelectionStart.ToShortDateString
        Dim Month2 As Date = mCalendar2.SelectionStart.ToShortDateString

        Dim cnn1 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
        Dim rd1 As MySqlDataReader
        Dim cmd1 As MySqlCommand

        On Error GoTo mensaje

        If (optbodega.Checked) Then
            Dim id_bodega As Integer = 0

            cnn1.Close() : cnn1.Open()

            cmd1 = cnn1.CreateCommand
            cmd1.CommandText =
                "select Id from Bodegas where Nombre='" & cbobodega.Text & "' and Ubicacion='" & cbogeneral.Text & "'"
            rd1 = cmd1.ExecuteReader
            If rd1.HasRows Then
                If rd1.Read Then
                    id_bodega = rd1(0).ToString
                End If
            End If
            rd1.Close()

            cmd1 = cnn1.CreateCommand
            cmd1.CommandText =
                "select count(Id) from Movimientos where Id_Bodega=" & id_bodega & "  AND Fecha BETWEEN '" & Format(Month1, "yyyy-MM-dd") & "' and '" & Format(Month2, "yyyy-MM-dd") & "'"
            rd1 = cmd1.ExecuteReader
            If rd1.HasRows Then
                If rd1.Read Then
                    conteo = rd1(0).ToString
                End If
            End If
            rd1.Close()

            barra.Visible = True
            barra.Value = 0
            barra.Maximum = conteo

            cmd1 = cnn1.CreateCommand
            cmd1.CommandText =
                "select Id,Movimiento,Fecha,Hora,Nombre,Usuario from Movimientos where Id_Bodega=" & id_bodega & " and Fecha between '" & Format(Month1, "yyyy-MM-dd") & "' and '" & Format(Month2, "yyyy-MM-dd") & "' order by Id"
            rd1 = cmd1.ExecuteReader
            Do While rd1.Read
                If rd1.HasRows Then
                    Dim id As Integer = rd1("Id").ToString
                    Dim movi As String = rd1("Movimiento").ToString
                    Dim fecha As String = FormatDateTime(rd1("Fecha").ToString, DateFormat.ShortDate)
                    Dim hora As String = FormatDateTime(rd1("Hora").ToString, DateFormat.LongTime)
                    Dim nombre As String = rd1("Nombre").ToString
                    Dim usu As String = rd1("Usuario").ToString

                    grdCaptura.Rows.Add(
                        id,
                        fecha,
                        hora,
                        nombre,
                        movi,
                        usu
                        )
                    grdCaptura.Refresh()
                    barra.Value += 1
                End If
            Loop
            rd1.Close()

            barra.Visible = False
            barra.Value = 0
        End If

        If (optcliente.Checked) Then


            cnn1.Close() : cnn1.Open()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText =
                "select count(Id) from Movimientos where Nombre='" & cbogeneral.Text & "' and Fecha BETWEEN '" & Format(Month1, "yyyy-MM-dd") & "' and '" & Format(Month2, "yyyy-MM-dd") & "'"
            rd1 = cmd1.ExecuteReader
            If rd1.HasRows Then
                If rd1.Read Then
                    conteo = rd1(0).ToString
                End If
            End If
            rd1.Close()

            barra.Visible = True
            barra.Value = 0
            barra.Maximum = conteo

            cmd1 = cnn1.CreateCommand
            cmd1.CommandText =
                "select Id,Nombre_Bodega,Movimiento,Fecha,Hora,Usuario from Movimientos where Nombre='" & cbogeneral.Text & "' and Fecha between '" & Format(Month1, "yyyy-MM-dd") & "' and '" & Format(Month2, "yyyy-MM-dd") & "' order by Id"
            rd1 = cmd1.ExecuteReader
            Do While rd1.Read
                If rd1.HasRows Then
                    Dim id As Integer = rd1("Id").ToString
                    Dim bodega As String = rd1("Nombre_Bodega").ToString
                    Dim movimiento As String = rd1("Movimiento").ToString
                    Dim fecha As String = FormatDateTime(rd1("Fecha").ToString, DateFormat.ShortDate)
                    Dim hora As String = FormatDateTime(rd1("Hora").ToString, DateFormat.LongTime)
                    Dim usu As String = rd1("Usuario").ToString

                    grdCaptura.Rows.Add(
                        id,
                        bodega,
                        movimiento,
                        fecha,
                        hora,
                        usu
                        )
                    grdCaptura.Refresh()
                    barra.Value += 1
                End If
            Loop
            rd1.Close()

            barra.Visible = False
            barra.Value = 0

        End If

        If (optvisitas.Checked) Then

            Dim id_bodega As Integer = 0
            cnn1.Close() : cnn1.Open()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText =
                "select Id from Bodegas where Nombre='" & cbobodega.Text & "' and Ubicacion='" & cbogeneral.Text & "'"
            rd1 = cmd1.ExecuteReader
            If rd1.HasRows Then
                If rd1.Read Then
                    id_bodega = rd1(0).ToString
                End If
            End If
            rd1.Close()

            cmd1 = cnn1.CreateCommand
            cmd1.CommandText =
                "select count(Id) from Movimientos where Id_Bodega=" & id_bodega & " AND Movimiento='Entrada' or Movimiento='Salida' AND Fecha BETWEEN '" & Format(Month1, "yyyy-MM-dd") & "' and '" & Format(Month2, "yyyy-MM-dd") & "'"
            rd1 = cmd1.ExecuteReader
            If rd1.HasRows Then
                If rd1.Read Then
                    conteo = rd1(0).ToString
                End If
            End If
            rd1.Close()


            barra.Visible = True
            barra.Value = 0
            barra.Maximum = conteo

            cmd1 = cnn1.CreateCommand
            cmd1.CommandText =
                "select Id,Movimiento,Fecha,Hora,Nombre,Usuario from Movimientos where Id_Bodega=" & id_bodega & " AND Movimiento='Entrada' or Movimiento='Salida' AND Fecha between '" & Format(Month1, "yyyy-MM-dd") & "' and '" & Format(Month2, "yyyy-MM-dd") & "' order by Id"
            rd1 = cmd1.ExecuteReader
            Do While rd1.Read
                If rd1.HasRows Then
                    Dim id As Integer = rd1("Id").ToString
                    Dim movimiento As String = rd1("Movimiento").ToString
                    Dim fecha As String = FormatDateTime(rd1("Fecha").ToString, DateFormat.ShortDate)
                    Dim hora As String = FormatDateTime(rd1("Hora").ToString, DateFormat.LongTime)
                    Dim nombre As String = rd1("Nombre").ToString
                    Dim usuario As String = rd1("Usuario").ToString

                    grdCaptura.Rows.Add(
                        id,
                        movimiento,
                        fecha,
                        hora,
                        nombre,
                        usuario
                        )
                    grdCaptura.Refresh()
                    barra.Value += 1
                End If
            Loop
            rd1.Close()

            barra.Visible = False
            barra.Value = 0
            cnn1.Close()
        End If

        Exit Sub
mensaje:
        MsgBox(Err.Number & " - " & Err.Description)

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