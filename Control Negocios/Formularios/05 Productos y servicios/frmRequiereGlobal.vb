Public Class frmRequiereGlobal

    Private Sub cbonombre_DropDown(sender As System.Object, e As System.EventArgs) Handles cbonombre.DropDown
        cbonombre.Items.Clear()
        Try
            cnn1.Close() : cnn1.Open()

            cmd1 = cnn1.CreateCommand
            cmd1.CommandText =
                "select distinct DescripP from MiProd order by DescripP"
            rd1 = cmd1.ExecuteReader
            Do While rd1.Read
                If rd1.HasRows Then cbonombre.Items.Add(
                    rd1(0).ToString
                    )
            Loop
            rd1.Close()
            cnn1.Close()
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
            cnn1.Close()
        End Try
    End Sub

    Private Sub cbonombre_SelectedValueChanged(sender As System.Object, e As System.EventArgs) Handles cbonombre.SelectedValueChanged
        Try
            cnn1.Close() : cnn1.Open()

            cmd1 = cnn1.CreateCommand
            cmd1.CommandText =
                "select CodigoP,UVentaP from MiProd where DescripP='" & cbonombre.Text & "'"
            rd1 = cmd1.ExecuteReader
            If rd1.HasRows Then
                If rd1.Read Then
                    txtcodigo.Text = rd1("CodigoP").ToString
                    txtunidad.Text = rd1("UVentaP").ToString
                    txtcantidad.Text = "1.0"
                End If
            End If
            rd1.Close()
            cnn1.Close()
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
            cnn1.Close()
        End Try
    End Sub

    Private Sub cbonombre_KeyPress(sender As System.Object, e As System.Windows.Forms.KeyPressEventArgs) Handles cbonombre.KeyPress
        If AscW(e.KeyChar) = Keys.Enter Then
            temporal.Rows.Clear()
            Call cbonombre_SelectedValueChanged(cbonombre, New EventArgs())

            Try
                cnn1.Close() : cnn1.Open()

                cmd1 = cnn1.CreateCommand
                cmd1.CommandText =
                    "select Codigo,Descrip,UVenta,Cantidad from MiProd where DescripP='" & cbonombre.Text & "'"
                rd1 = cmd1.ExecuteReader
                Do While rd1.Read
                    If rd1.HasRows Then
                        temporal.Rows.Add(
                            rd1("Codigo").ToString,
                            rd1("Descrip").ToString,
                            rd1("UVenta").ToString,
                            rd1("Cantidad").ToString
                            )
                    End If
                Loop
                rd1.Close()
                cnn1.Close()
            Catch ex As Exception
                MessageBox.Show(ex.ToString)
                cnn1.Close()
            End Try


            txtcodigo.Focus().Equals(True)
        End If
    End Sub

    Private Sub txtcodigo_Click(sender As System.Object, e As System.EventArgs) Handles txtcodigo.Click
        txtcodigo.SelectionStart = 0
        txtcodigo.SelectionLength = Len(txtcodigo.Text)
    End Sub

    Private Sub txtcodigo_GotFocus(sender As Object, e As System.EventArgs) Handles txtcodigo.GotFocus
        txtcodigo.SelectionStart = 0
        txtcodigo.SelectionLength = Len(txtcodigo.Text)
    End Sub

    Private Sub txtcodigo_KeyPress(sender As System.Object, e As System.Windows.Forms.KeyPressEventArgs) Handles txtcodigo.KeyPress
        If AscW(e.KeyChar) = Keys.Enter Then

            Dim codigo_tem As String = ""
            Dim nombre_tem As String = ""
            Dim unidad_tem As String = ""
            Dim cantid_tem As Decimal = 0

            If txtcodigo.Text <> "" Then
                If (temporal.Rows.Count) > 0 Then
                    For dx As Integer = 0 To temporal.Rows.Count - 1
                        If temporal.Rows.Count = 0 Then Continue For
                        codigo_tem = temporal.Rows(0).Cells(0).Value.ToString
                        nombre_tem = temporal.Rows(0).Cells(1).Value.ToString
                        unidad_tem = temporal.Rows(0).Cells(2).Value.ToString
                        cantid_tem = temporal.Rows(0).Cells(3).Value.ToString
                        Dim pcompra As Double = 0
                        Dim prov As String = ""
                        Dim depto As String = ""
                        Dim grupo As String = ""
                        Dim costo As String = "0"
                        cnn1.Close()
                        cnn1.Open()
                        cmd1 = cnn1.CreateCommand
                        cmd1.CommandText = "Select ProvPri,PrecioCompra,Departamento,Grupo from Productos where COdigo='" & codigo_tem & "'"
                        rd1 = cmd1.ExecuteReader
                        If rd1.Read Then
                            pcompra = FormatNumber(rd1(1).ToString, 2)
                            prov = rd1(0).ToString
                            depto = rd1(2).ToString
                            grupo = rd1(3).ToString
                        End If
                        rd1.Close()
                        cnn1.Close()
                        costo = CDec(pcompra * cantid_tem)
                        grdnecesita.Rows.Add(
                            codigo_tem,
                            nombre_tem,
                            unidad_tem,
                            cantid_tem, pcompra, costo, prov, txtcodigo.Text, depto, grupo)
                        'End If
                        temporal.Rows.Remove(temporal.Rows(0))
                        My.Application.DoEvents()

                    Next dx

                End If
                txtcantidad.Focus().Equals(True)
                End If
            End If
    End Sub

    Private Sub txtcantidad_Click(sender As System.Object, e As System.EventArgs) Handles txtcantidad.Click
        txtcantidad.SelectionStart = 0
        txtcantidad.SelectionLength = Len(txtcantidad.Text)
    End Sub

    Private Sub txtcantidad_GotFocus(sender As Object, e As System.EventArgs) Handles txtcantidad.GotFocus
        txtcantidad.SelectionStart = 0
        txtcantidad.SelectionLength = Len(txtcantidad.Text)
    End Sub

    Private Sub txtcantidad_KeyPress(sender As System.Object, e As System.Windows.Forms.KeyPressEventArgs) Handles txtcantidad.KeyPress
        If AscW(e.KeyChar) = Keys.Enter Then
            If txtcantidad.Text = "" Or txtcantidad.Text = "0" Or txtcantidad.Text = "0.00" Then MsgBox("Ingresa una cantidad válida.", vbInformation + vbOKOnly, "Delsscom Control Negocios Pro") : Exit Sub
            If txtcantidad.Text = "" Then Exit Sub
            If txtcantidad.Text = "." Then Exit Sub
            If Not IsNumeric(txtcantidad.Text) Then Exit Sub
            If CDec(txtcantidad.Text) = 0 Then Exit Sub
            For d As Integer = 0 To grdnecesita.Rows.Count - 1
                If txtcodigo.Text = grdnecesita.Rows(d).Cells(7).Value.ToString Then
                    Dim total As Double = 0
                    Dim cantidad As Decimal = grdnecesita.Rows(d).Cells(3).Value.ToString
                    Dim pcompra As Decimal = grdnecesita.Rows(d).Cells(4).Value.ToString
                    Dim vaapagar As String = cantidad * CDec(txtcantidad.Text)
                    Dim vaapagar2 As String = vaapagar * pcompra
                    grdnecesita.Rows(d).Cells(3).Value = vaapagar
                    grdnecesita.Rows(d).Cells(5).Value = vaapagar2
                End If
            Next
            My.Application.DoEvents()
            Dim sumatoria As String = "0"
            For xxx As Integer = 0 To grdnecesita.Rows.Count - 1
                If txtcodigo.Text = grdnecesita.Rows(xxx).Cells(7).Value.ToString Then
                    sumatoria = CDec(sumatoria) + CDec(grdnecesita.Rows(xxx).Cells(5).Value.ToString)
                End If
            Next
            My.Application.DoEvents()
            grdproducir.Rows.Add(
                txtcodigo.Text,
                cbonombre.Text,
                txtunidad.Text,
                FormatNumber(txtcantidad.Text, 2), sumatoria)

            My.Application.DoEvents()

            TextBox1.Text = CDec(TextBox1.Text) + CDec(sumatoria)

            cbonombre.Text = ""
            txtcodigo.Text = ""
            txtunidad.Text = ""
            txtcantidad.Text = "0.00"
            cbonombre.Focus().Equals(True)
            btnexportar.Focus().Equals(True)
        End If
    End Sub

    Private Sub btnNuevo_Click(sender As Object, e As EventArgs) Handles btnNuevo.Click
        grdnecesita.Rows.Clear()
        grdproducir.Rows.Clear()
        cbonombre.Text = ""
        txtcodigo.Text = ""
        txtunidad.Text = ""
        txtcantidad.Text = "1.00"
        TextBox1.Text = "0.00"
    End Sub

    Private Sub btnexportar_Click(sender As Object, e As EventArgs) Handles btnexportar.Click
        If grdnecesita.Rows.Count = 0 Then
            Exit Sub
        End If

        If MsgBox("¿Deseas exportar la información a Excel?", vbInformation + vbOKCancel, "Delsscom Control Negocios Pro") = vbOK Then

            Dim exApp As New Microsoft.Office.Interop.Excel.Application
            Dim exBook As Microsoft.Office.Interop.Excel.Workbook
            Dim exSheet As Microsoft.Office.Interop.Excel.Worksheet

            Try
                exBook = exApp.Workbooks.Add
                exSheet = exBook.Worksheets.Application.ActiveSheet

                exSheet.Columns("A").NumberFormat = "@"

                Dim Fila As Integer = 0
                Dim Colu As Integer = 0

                Dim NCol As Integer = grdproducir.ColumnCount
                Dim NRow As Integer = grdproducir.RowCount

                Dim NCol2 As Integer = grdnecesita.ColumnCount
                Dim NRow2 As Integer = grdnecesita.RowCount

                exSheet.Cells.Range("A1:G1").Merge()
                exSheet.Cells.Item(1, 1) = "REQUISICIONES GLOBALES"
                'Titulo en negrita, Alineado al centro y que el tamaño de la columna se
                exSheet.Rows.Item(1).Font.Bold = 1
                exSheet.Rows.Item(1).HorizontalAlignment = 3

                exSheet.Cells.Range("A2:D2").Merge()
                exSheet.Cells.Item(2, 2) = "PRODUCIRÁN"
                'Titulo en negrita, Alineado al centro y que el tamaño de la columna se
                exSheet.Rows.Item(2).Font.Bold = 1
                exSheet.Rows.Item(2).HorizontalAlignment = 3

                'Títulos del grid
                exSheet.Cells.Item(3, 1) = "Código"
                exSheet.Cells.Item(3, 2) = "Descripción"
                exSheet.Cells.Item(3, 3) = "Unidad"
                exSheet.Cells.Item(3, 4) = "Cantidad"
                exSheet.Rows.Item(3).font.bold = 1
                exSheet.Rows.Item(3).HorizontalAlignment = 3

                For Fila = 0 To NRow - 1
                    For Col = 0 To NCol - 1
                        exSheet.Cells.Item(Fila + 4, Col + 1) = grdproducir.Rows(Fila).Cells(Col).Value.ToString
                    Next
                Next

                'Títulos del grid NECESITA
                exSheet.Cells.Item(7, 1) = "Código"
                exSheet.Cells.Item(7, 2) = "Descripción"
                exSheet.Cells.Item(7, 3) = "Unidad"
                exSheet.Cells.Item(7, 4) = "Cantidad"
                exSheet.Rows.Item(7).font.bold = 1
                exSheet.Rows.Item(7).HorizontalAlignment = 3

                For Fila = 0 To NRow2 - 1
                    For Col = 0 To NCol2 - 1
                        exSheet.Cells.Item(Fila + 8, Col + 1) = grdnecesita.Rows(Fila).Cells(Col).Value.ToString
                    Next
                Next

                exSheet.Columns.AutoFit()

                exApp.Application.Visible = True
                exSheet = Nothing
                exBook = Nothing
                exApp = Nothing

            Catch ex As Exception
                MessageBox.Show(ex.ToString)
            End Try

        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Close()
    End Sub

    Private Sub frmRequiereGlobal_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class