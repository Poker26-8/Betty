Imports DocumentFormat.OpenXml.Drawing.Charts

Public Class frmkitsN


    Private Sub cboKit_DropDown(sender As Object, e As EventArgs) Handles cboKit.DropDown
        Try
            cboKit.Items.Clear()
            cnn5.Close() : cnn5.Open()
            cmd5 = cnn5.CreateCommand
            cmd5.CommandText = "SELECT DISTINCT Nombre FROM productos WHERE ProvRes=1 AND Nombre<>'' ORDER BY Nombre"
            rd5 = cmd5.ExecuteReader
            If rd5.HasRows Then
                Do While rd5.Read
                    cboKit.Items.Add(rd5(0).ToString)
                Loop
            End If
            rd5.Close()
            cnn5.Close()

        Catch ex As Exception
            MessageBox.Show(ex.ToString)
            cnn5.Close()
        End Try
    End Sub

    Private Sub cboKit_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cboKit.KeyPress
        e.KeyChar = UCase(e.KeyChar)
        If AscW(e.KeyChar) = Keys.Enter Then
            cbodescripcion.Focus.Equals(True)
        End If
    End Sub

    Private Sub cbodescripcion_DropDown(sender As Object, e As EventArgs) Handles cbodescripcion.DropDown
        Try
            cbodescripcion.Items.Clear()

            cnn5.Close() : cnn5.Open()
            cmd5 = cnn5.CreateCommand
            cmd5.CommandText = "SELECT DISTINCT Nombre FROM productos WHERE ProvRes<>1 AND Nombre<>'' ORDER BY Nombre"
            rd5 = cmd5.ExecuteReader
            If rd5.HasRows Then
                Do While rd5.Read
                    cbodescripcion.Items.Add(rd5(0).ToString)
                Loop
            End If
            rd5.Close()
            cnn5.Close()

        Catch ex As Exception
            MessageBox.Show(ex.ToString)
            cnn5.Close()
        End Try
    End Sub

    Private Sub cbodescripcion_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cbodescripcion.KeyPress
        e.KeyChar = UCase(e.KeyChar)
        If AscW(e.KeyChar) = Keys.Enter Then
            cnn1.Close() : cnn1.Open()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "SELECT Codigo FROM productos WHERE Nombre='" & cbodescripcion.Text & "'"
            rd1 = cmd1.ExecuteReader
            If rd1.HasRows Then
                If rd1.Read Then
                    cbocodigo.Text = rd1(0).ToString
                    cbocodigo.Focus.Equals(True)
                End If
            End If
            rd1.Close()
            cnn1.Close()


        End If
    End Sub

    Private Sub cbocodigo_DropDown(sender As Object, e As EventArgs) Handles cbocodigo.DropDown
        Try
            cbocodigo.Items.Clear()

            cnn5.Close() : cnn5.Open()
            cmd5 = cnn5.CreateCommand
            cmd5.CommandText = "SELECT DISTINCT Codigo FROM productos WHERE ProvRes<>1 AND Codigo<>'' ORDER BY Codigo"
            rd5 = cmd5.ExecuteReader
            If rd5.HasRows Then
                Do While rd5.Read
                    cbocodigo.Items.Add(rd5(0).ToString)
                Loop
            End If
            rd5.Close()
            cnn5.Close()

        Catch ex As Exception
            MessageBox.Show(ex.ToString)
            cnn5.Close()
        End Try
    End Sub

    Private Sub cbocodigo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cbocodigo.KeyPress
        e.KeyChar = UCase(e.KeyChar)
        If AscW(e.KeyChar) = Keys.Enter Then
            cnn1.Close() : cnn1.Open()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "SELECT Nombre FROM productos WHERE Codigo='" & cbocodigo.Text & "'"
            rd1 = cmd1.ExecuteReader
            If rd1.HasRows Then
                If rd1.Read Then
                    cbodescripcion.Text = rd1(0).ToString
                End If
            End If
            rd1.Close()
            cnn1.Close()
            txtCantidad.Focus.Equals(True)
        End If
    End Sub

    Private Sub txtCantidad_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtCantidad.KeyPress
        If AscW(e.KeyChar) = Keys.Enter Then
            If IsNumeric(txtCantidad.Text) Then
                txtPorcentaje.Focus.Equals(True)
            End If
        End If
    End Sub

    Private Sub txtPorcentaje_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPorcentaje.KeyPress
        If AscW(e.KeyChar) = Keys.Enter Then
            If IsNumeric(txtPorcentaje.Text) Then

                Dim unidad As String = ""
                Dim costo As Double = 0
                Dim cantidad As Double = txtCantidad.Text
                Dim porsentaje As Double = txtPorcentaje.Text
                Dim totporce As Double = 0
                Dim totporce2 As Double = 0
                Dim subtotal As Double = 0
                Dim total As Double = 0
                Dim sumcosto As Double = 0
                Dim totalkit As Double = 0
                Dim totalcosto As Double = 0

                cnn1.Close() : cnn1.Open()
                cmd1 = cnn1.CreateCommand
                cmd1.CommandText = "SELECT UVenta,PrecioCompra FROM productos WHERE Nombre='" & cbodescripcion.Text & "' AND Codigo='" & cbocodigo.Text & "'"
                rd1 = cmd1.ExecuteReader
                If rd1.HasRows Then
                    If rd1.Read Then
                        unidad = rd1("UVenta").ToString
                        costo = rd1("PrecioCompra").ToString

                        totporce = CDec(porsentaje / 100)
                        totporce2 = CDec(costo) * CDec(totporce)
                        subtotal = CDec(totporce2 + costo)
                        total = CDec(subtotal * cantidad)



                        grdDatos.Rows.Add(cbocodigo.Text,
                                          cbodescripcion.Text,
                                          unidad,
                                          cantidad,
                                          porsentaje,
                                          costo,
                                          subtotal,
                                          total)

                        For luffy As Integer = 0 To grdDatos.Rows.Count - 1
                            Dim tot As Double = grdDatos.Rows(luffy).Cells(7).Value.ToString
                            Dim cant As Double = grdDatos.Rows(luffy).Cells(3).Value.ToString
                            Dim cost As Double = grdDatos.Rows(luffy).Cells(5).Value.ToString

                            sumcosto = CDec(cant * cost)

                            totalkit = totalkit + CDec(tot)
                            totalcosto = totalcosto + CDec(sumcosto)
                        Next

                        txtPrecio.Text = FormatNumber(totalkit, 2)
                        txtUtilidad.Text = FormatNumber(totalkit - totalcosto, 2)

                        cbocodigo.Text = ""
                        cbodescripcion.Text = ""
                        txtCantidad.Text = "1"
                        txtPorcentaje.Text = ""
                        cbodescripcion.Focus.Equals(True)
                    End If
                End If
                rd1.Close()
                cnn1.Close()



            End If
        End If
    End Sub

    Private Sub cboKit_SelectedValueChanged(sender As Object, e As EventArgs) Handles cboKit.SelectedValueChanged
        Try
            Dim porcentaje As Double = 0
            Dim costo As Double = 0
            Dim totporce As Double = 0
            Dim totporce2 As Double = 0
            Dim subt As Double = 0

            Dim preciokit As Double = 0
            Dim utilidad As Double = 0
            cnn1.Close() : cnn1.Open()
            cnn2.Close() : cnn2.Open()

            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "SELECT Codigo FROM productos WHERE Nombre='" & cboKit.Text & "'"
            rd1 = cmd1.ExecuteReader
            If rd1.HasRows Then
                If rd1.Read Then
                    txtCodigoKit.Text = rd1(0).ToString

                    cmd2 = cnn2.CreateCommand
                    cmd2.CommandText = "SELECT * FROM kits WHERE Cod='" & txtCodigoKit.Text & "' AND Nombre='" & cboKit.Text & "'"
                    rd2 = cmd2.ExecuteReader
                    If rd2.HasRows Then
                        Do While rd2.Read
                            porcentaje = rd2("Porcentaje").ToString
                            costo = rd2("PPrecio").ToString

                            totporce = CDec(porcentaje / 100)
                            totporce2 = CDec(costo) * CDec(totporce)
                            subt = CDec(totporce2 + costo)
                            grdDatos.Rows.Add(rd2("Codigo").ToString,
                                              rd2("Descrip").ToString,
                                              rd2("UVenta").ToString,
                                              rd2("Cantidad").ToString,
                                              porcentaje,
                                              costo,
                                              subt,
                                              rd2("CTotal").ToString)

                            preciokit = preciokit + CDec(rd2("CTotal").ToString)
                            utilidad = utilidad + CDec(costo * rd2("Cantidad").ToString)

                        Loop
                    End If
                    rd2.Close()

                    txtPrecio.Text = FormatNumber(preciokit, 2)
                    txtUtilidad.Text = FormatNumber(txtPrecio.Text - utilidad, 2)
                End If
            End If
            rd1.Close()
            cnn1.Close()
            cnn2.Close()

        Catch ex As Exception
            MessageBox.Show(ex.ToString)
            cnn1.Close()
        End Try
    End Sub

    Private Sub btnLimpiar_Click(sender As Object, e As EventArgs) Handles btnLimpiar.Click
        grdDatos.Rows.Clear()
        cbocodigo.Text = ""
        cbodescripcion.Text = ""
        txtCantidad.Text = "1"
        txtPorcentaje.Text = "0"
        cboKit.Text = ""
        txtCodigoKit.Text = ""
        txtPrecio.Text = "0.00"
        txtUtilidad.Text = "0.00"
        cboKit.Focus.Equals(True)
    End Sub

    Private Sub btnSalir_Click(sender As Object, e As EventArgs) Handles btnSalir.Click
        Me.Close()
    End Sub

    Private Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click
        Try
            If MsgBox("¿Desea guardar la información?", vbInformation + vbYesNo, titulocentral) Then

                Dim precion As Double = txtPrecio.Text
                cnn2.Clone() : cnn2.Open()

                For isagi As Integer = 0 To grdDatos.Rows.Count - 1
                    Dim cod As String = grdDatos.Rows(isagi).Cells(0).Value.ToString
                    Dim desc As String = grdDatos.Rows(isagi).Cells(1).Value.ToString
                    Dim unidad As String = grdDatos.Rows(isagi).Cells(2).Value.ToString
                    Dim cantidad As Double = grdDatos.Rows(isagi).Cells(3).Value.ToString
                    Dim porcentaje As Double = grdDatos.Rows(isagi).Cells(4).Value.ToString
                    Dim costo As Double = grdDatos.Rows(isagi).Cells(5).Value.ToString
                    Dim total As Double = grdDatos.Rows(isagi).Cells(7).Value.ToString

                    cmd2 = cnn2.CreateCommand
                    cmd2.CommandText = "INSERT INTO kits(Cod,Nombre,Codigo,Descrip,UVenta,Cantidad,PPrecio,Porcentaje,CTotal,Fecha) VALUES('" & txtCodigoKit.Text & "','" & cboKit.Text & "','" & cod & "','" & desc & "','" & unidad & "'," & cantidad & "," & costo & "," & porcentaje & "," & total & ",'" & Format(Date.Now, "yyyy-MM-dd") & "')"
                    cmd2.ExecuteNonQuery()
                Next

                cmd2 = cnn2.CreateCommand
                cmd2.CommandText = "UPDATE productos SET PrecioVentaIVA=" & precion & " WHERE Codigo='" & txtCodigoKit.Text & "' AND Nombre='" & cboKit.Text & "'"
                If cmd2.ExecuteNonQuery() Then
                    MsgBox("Datos almacenados correctamente.", vbInformation + vbOKOnly, titulocentral)

                    cboKit.Text = ""
                    txtCodigoKit.Text = ""
                    cbocodigo.Text = ""
                    cbodescripcion.Text = ""
                    txtCantidad.Text = "1"
                    txtPorcentaje.Text = "0"
                    grdDatos.Rows.Clear()
                    txtPrecio.Text = "0.00"
                    txtUtilidad.Text = "0.00"

                End If
                cnn2.Close()
            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString)
            cnn2.Close()
        End Try
    End Sub

    Private Sub grdDatos_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles grdDatos.CellDoubleClick
        Dim CELDA As Integer = grdDatos.CurrentRow.Index
        Dim TOTAL As Double = 0
        Dim costo As Double = 0
        Dim uti As Double = 0

        cbocodigo.Text = grdDatos.Rows(CELDA).Cells(0).Value.ToString
        cbodescripcion.Text = grdDatos.Rows(CELDA).Cells(1).Value.ToString
        txtCantidad.Text = grdDatos.Rows(CELDA).Cells(3).Value.ToString
        txtPorcentaje.Text = grdDatos.Rows(CELDA).Cells(4).Value.ToString
        costo = grdDatos.Rows(CELDA).Cells(5).Value.ToString
        uti = grdDatos.Rows(CELDA).Cells(6).Value.ToString
        TOTAL = grdDatos.Rows(CELDA).Cells(7).Value.ToString

        txtPrecio.Text = txtPrecio.Text - CDec(TOTAL)
        txtUtilidad.Text = txtUtilidad.Text - (CDec(uti - costo))


        grdDatos.Rows.Remove(grdDatos.Rows(CELDA))
    End Sub

    Private Sub btnEliminar_Click(sender As Object, e As EventArgs) Handles btnEliminar.Click

    End Sub
End Class