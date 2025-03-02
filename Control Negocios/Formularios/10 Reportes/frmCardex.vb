﻿
Imports ClosedXML.Excel
Imports MySql.Data.MySqlClient

Public Class frmCardex

    Private Sub frmCardex_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        mCalendar1.SetDate(Now)
        mCalendar2.SetDate(Now)
    End Sub

    Private Sub cbonombre_DropDown(sender As System.Object, e As System.EventArgs) Handles cbonombre.DropDown
        Dim M1 As Date = mCalendar1.SelectionStart.ToShortDateString
        Dim M2 As Date = mCalendar2.SelectionStart.ToShortDateString
        cbonombre.Items.Clear()

        Dim cnn1 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
        Dim rd1 As MySqlDataReader
        Dim cmd1 As MySqlCommand

        Try
            cnn1.Close() : cnn1.Open()

            cmd1 = cnn1.CreateCommand
            cmd1.CommandText =
                "select distinct Nombre from Cardex where Fecha between'" & Format(M1, "yyyy-MM-dd 00:00:00") & "' and '" & Format(M2, "yyyy-MM-dd 23:59:59") & "' AND Nombre<>'' ORDER BY Nombre"
            rd1 = cmd1.ExecuteReader
            Do While rd1.Read
                If rd1.HasRows Then cbonombre.Items.Add(
                    rd1(0).ToString
                    )
            Loop
            rd1.Close() : cnn1.Close()
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
            cnn1.Close()
        End Try
    End Sub

    Public Sub cbonombre_SelectedValueChanged(sender As Object, e As System.EventArgs) Handles cbonombre.SelectedValueChanged
        txtcodigo.Text = ""
        lblExistencia.Text = ""


        Dim cnn1 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
        Dim rd1 As MySqlDataReader
        Dim cmd1 As MySqlCommand

        Try
            cnn1.Close() : cnn1.Open()

            cmd1 = cnn1.CreateCommand
            cmd1.CommandText =
                "select Codigo from Productos where Nombre='" & cbonombre.Text & "'"
            rd1 = cmd1.ExecuteReader
            If rd1.HasRows Then
                If rd1.Read Then
                    txtcodigo.Text = rd1("Codigo").ToString
                    cboCodigo.Text = rd1("Codigo").ToString
                End If
            End If
            rd1.Close() : cnn1.Close()

        Catch ex As Exception
            MessageBox.Show(ex.ToString)
            cnn1.Close()
        End Try
    End Sub

    Private Sub btnreporte_Click(sender As System.Object, e As System.EventArgs) Handles btnreporte.Click
        Dim M1 As Date = mCalendar1.SelectionStart.ToShortDateString
        Dim M2 As Date = mCalendar2.SelectionStart.ToShortDateString
        grdcaptura.Rows.Clear()


        Dim cnn1 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
        Dim cnn2 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
        Dim rd1, rd2 As MySqlDataReader
        Dim cmd1, cmd2 As MySqlCommand

        Try


            If cboCodigo.Text = "" And cbonombre.Text = "" Then
                MsgBox("Hay que seleccionar un código o una descripción para poder generar el Reporte")
                Exit Sub
            End If

            If cboCodigo.Text = "" And cbonombre.Text <> "" Then
                cnn1.Close() : cnn1.Open()
                cmd1 = cnn1.CreateCommand
                cmd1.CommandText =
                    "select Codigo from productos where Nombre='" & cbonombre.Text & "'"
                rd1 = cmd1.ExecuteReader
                If rd1.HasRows Then
                    If rd1.Read Then
                        cboCodigo.Text = rd1("Codigo").ToString
                    End If
                End If
                rd1.Close() : cnn1.Close()
            End If

            If cboCodigo.Text <> "" And cbonombre.Text = "" Then
                cnn1.Close() : cnn1.Open()
                cmd1 = cnn1.CreateCommand
                cmd1.CommandText =
                    "select Nombre from productos where Codigo='" & cboCodigo.Text & "'"
                rd1 = cmd1.ExecuteReader
                If rd1.HasRows Then
                    If rd1.Read Then
                        cbonombre.Text = rd1("Nombre").ToString
                    End If
                End If
                rd1.Close() : cnn1.Close()
            End If

            cnn1.Close() : cnn1.Open()
            cmd1 = cnn1.CreateCommand

            If cboCodigo.Text = "" Then
                cmd1.CommandText =
                    "select Codigo,Nombre,Folio,Movimiento,Precio,Inicial,Cantidad,Final,Usuario,Fecha from Cardex where Fecha between '" & Format(M1, "yyyy-MM-dd 00:00:00") & "' and '" & Format(M2, "yyyy-MM-dd 23:59:59") & "' order by Id"
            Else
                cmd1.CommandText =
                    "select Codigo,Nombre,Folio,Movimiento,Precio,Inicial,Cantidad,Final,Usuario,Fecha from Cardex where Fecha between '" & Format(M1, "yyyy-MM-dd 00:00:00") & "' and '" & Format(M2, "yyyy-MM-dd 23:59:59") & "' and mid(Codigo,1,6) = '" & Mid(cboCodigo.Text, 1, 6) & "' order by Id"
            End If

            rd1 = cmd1.ExecuteReader
            Do While rd1.Read

                Dim varmultiplo As Double = 1
                cnn2.Close() : cnn2.Open()
                cmd2 = cnn2.CreateCommand
                cmd2.CommandText =
                    "select Multiplo from Productos where Codigo='" & rd1("Codigo").ToString & "'"
                rd2 = cmd2.ExecuteReader
                If rd2.HasRows Then
                    If rd2.Read Then
                        varmultiplo = rd2(0).ToString
                    End If
                End If
                rd2.Close()
                cnn2.Close()

                Dim codigo As String = rd1("Codigo").ToString
                Dim nombre As String = rd1("Nombre").ToString
                Dim folio As String = rd1("Folio").ToString
                Dim movimi As String = rd1("Movimiento").ToString
                Dim precio As Double = rd1("Precio").ToString
                Dim inicia As Double = rd1("Inicial").ToString
                Dim cantidad As Double = Math.Abs(CDbl(rd1("Cantidad").ToString))
                Dim final As Double = rd1("Final").ToString
                Dim usuario As String = rd1("Usuario").ToString
                Dim fecha As String = rd1("Fecha").ToString

                If inicia > final Then
                    grdcaptura.Rows.Add(codigo, nombre, folio, movimi, FormatNumber(precio, 2), inicia * varmultiplo, "-" & cantidad * varmultiplo, final * varmultiplo, usuario, FormatDateTime(fecha, DateFormat.GeneralDate))
                Else
                    grdcaptura.Rows.Add(codigo, nombre, folio, movimi, FormatNumber(precio, 2), inicia * varmultiplo, "+" & cantidad * varmultiplo, final * varmultiplo, usuario, FormatDateTime(fecha, DateFormat.GeneralDate))
                End If


            Loop
            rd1.Close() : cnn1.Close()
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
            cnn1.Close()
        End Try

        If cboCodigo.Text <> "" Then
            cnn1.Close() : cnn1.Open()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText =
                    "select Existencia,Multiplo from Productos where Codigo='" & Mid(cboCodigo.Text, 1, 6) & "'"
            rd1 = cmd1.ExecuteReader
            If rd1.HasRows Then
                If rd1.Read Then
                    lblExistencia.Text = FormatNumber(rd1(0).ToString / rd1(1).ToString, 2)
                    lblExisDeriv.Text = rd1(0).ToString
                End If
            End If
            rd1.Close()
            cnn1.Close()
        End If

        MsgBox("Proceso terminado")

    End Sub

    Private Sub btnexportar_Click(sender As System.Object, e As System.EventArgs) Handles btnexportar.Click
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

    Private Sub cbonombre_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles cbonombre.KeyDown
        Select Case e.KeyCode
            Case Is = Keys.F3
                frmBuscaRepts.VieneDe = "Cardex"
                frmBuscaRepts.Show()
        End Select
    End Sub

    Private Sub frmCardex_Shown(sender As Object, e As System.EventArgs) Handles Me.Shown
        cbonombre.Focus().Equals(True)
    End Sub

    Private Sub cboCodigo_DropDown(sender As Object, e As EventArgs) Handles cboCodigo.DropDown
        Dim M1 As Date = mCalendar1.SelectionStart.ToShortDateString
        Dim M2 As Date = mCalendar2.SelectionStart.ToShortDateString
        cboCodigo.Items.Clear()


        Dim cnn1 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
        Dim rd1 As MySqlDataReader
        Dim cmd1 As MySqlCommand


        Try
            cnn1.Close() : cnn1.Open()

            cmd1 = cnn1.CreateCommand
            cmd1.CommandText =
                "SELECT DISTINCT Codigo FROM Cardex WHERE Fecha between'" & Format(M1, "yyyy-MM-dd 00:00:00") & "' and '" & Format(M2, "yyyy-MM-dd 23:59:59") & "' AND Codigo<>'' order by Codigo"
            rd1 = cmd1.ExecuteReader
            Do While rd1.Read
                If rd1.HasRows Then cboCodigo.Items.Add(
                    rd1(0).ToString
                    )
            Loop
            rd1.Close() : cnn1.Close()
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
            cnn1.Close()
        End Try
    End Sub

    Private Sub cboCodigo_SelectedValueChanged(sender As Object, e As EventArgs) Handles cboCodigo.SelectedValueChanged
        txtcodigo.Text = ""
        lblExistencia.Text = ""


        Dim cnn1 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
        Dim rd1 As MySqlDataReader
        Dim cmd1 As MySqlCommand


        Try
            cnn1.Close() : cnn1.Open()

            cmd1 = cnn1.CreateCommand
            cmd1.CommandText =
                "select Codigo,Nombre from Productos where Codigo='" & cboCodigo.Text & "'"
            rd1 = cmd1.ExecuteReader
            If rd1.HasRows Then
                If rd1.Read Then
                    txtcodigo.Text = rd1("Codigo").ToString
                    cbonombre.Text = rd1("Nombre").ToString
                End If
            End If
            rd1.Close() : cnn1.Close()

        Catch ex As Exception
            MessageBox.Show(ex.ToString)
            cnn1.Close()
        End Try
    End Sub
End Class