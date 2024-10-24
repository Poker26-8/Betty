Public Class frmCotizaciones
    Private Sub cboCotizaciones_DropDown(sender As Object, e As EventArgs) Handles cboCotizaciones.DropDown
        Try
            cboCotizaciones.Items.Clear()

            cnn5.Close() : cnn5.Open()
            cmd5 = cnn5.CreateCommand
            cmd5.CommandText = "SELECT DISTINCT Folio FROM cotped WHERE Folio<>'' AND Cliente='" & cboCliente.Text & "'"
            rd5 = cmd5.ExecuteReader
            Do While rd5.Read
                If rd5.HasRows Then
                    cboCotizaciones.Items.Add(rd5(0).ToString)
                End If
            Loop
            rd5.Close()
            cnn5.Close()

        Catch ex As Exception
            MessageBox.Show(ex.ToString)
            cnn5.Close()
        End Try
    End Sub

    Private Sub cboCliente_DropDown(sender As Object, e As EventArgs) Handles cboCliente.DropDown
        Try
            cboCliente.Items.Clear()

            cnn5.Close() : cnn5.Open()
            cmd5 = cnn5.CreateCommand
            cmd5.CommandText = "SELECT DISTINCT Nombre FROM clientes WHERE Nombre<>'' ORDER BY Nombre"
            rd5 = cmd5.ExecuteReader
            If rd5.HasRows Then
                Do While rd5.Read
                    cboCliente.Items.Add(rd5(0).ToString)
                Loop
            End If
            rd5.Close()
            cnn5.Close()
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
            cnn5.Close()
        End Try
    End Sub

    Private Sub cboVehiculo_DropDown(sender As Object, e As EventArgs) Handles cboVehiculo.DropDown
        Try

            If cboCliente.Text = "" Then Exit Sub

            cboVehiculo.Items.Clear()
            cnn5.Close() : cnn5.Open()
            cmd5 = cnn5.CreateCommand
            cmd5.CommandText = "SELECT DISTINCT Descripcion FROM vehiculo WHERE Cliente='" & cboCliente.Text & "'"
            rd5 = cmd5.ExecuteReader
            Do While rd5.Read
                If rd5.HasRows Then
                    cboVehiculo.Items.Add(rd5(0).ToString)
                End If
            Loop
            rd5.Close()
            cnn5.Close()

        Catch ex As Exception
            MessageBox.Show(ex.ToString)
            cnn5.Close()
        End Try
    End Sub

    Private Sub cboCotizaciones_SelectedValueChanged(sender As Object, e As EventArgs) Handles cboCotizaciones.SelectedValueChanged
        Try
            grdCaptura.Rows.Clear()
            txtTotalVenta.Text = "0.00"
            txtSubtotal.Text = "0.00"

            Dim subt As Double = 0
            cnn1.Close() : cnn1.Open()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "SELECT Codigo,Nombre,Unidad,Cantidad,Precio,Total FROM cotpeddet WHERE Folio='" & cboCotizaciones.Text & "'"
            rd1 = cmd1.ExecuteReader
            Do While rd1.Read
                If rd1.HasRows Then
                    grdCaptura.Rows.Add(rd1("Codigo").ToString,
                                        rd1("Nombre").ToString,
                                        rd1("Unidad").ToString,
                                        rd1("Cantidad").ToString,
                                        rd1("Precio").ToString,
                                        rd1("Total").ToString)

                    subt = subt + CDbl(rd1("Total").ToString)
                End If
            Loop
            rd1.Close()
            cnn1.Close()

            txtSubtotal.Text = FormatNumber(subt, 2)
            txtTotalVenta.Text = FormatNumber(txtSubtotal.Text, 2)
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
            cnn1.Close()
        End Try
    End Sub

    Private Sub btnLimpiar_Click(sender As Object, e As EventArgs) Handles btnLimpiar.Click
        cboCliente.Text = ""
        cboVehiculo.Text = ""
        cboCotizaciones.Text = ""
        grdCaptura.Rows.Clear()
        txtSubtotal.Text = "0.00"
        txtDescuento.Text = "0.00"
        txtTotalVenta.Text = "0.00"
        cboCliente.Focus.Equals(True)
    End Sub

    Private Sub txtDescuento_TextChanged(sender As Object, e As EventArgs) Handles txtDescuento.TextChanged
        Dim resta As Double = 0

        If txtDescuento.Text = "" Then
            txtDescuento.Text = "0.00"
            txtTotalVenta.Text = txtSubtotal.Text
            Exit Sub
        End If

        Dim descuento As Double = txtDescuento.Text
        Dim pordescuento As Double = 0
        pordescuento = (descuento * 100) / CDbl(IIf(txtSubtotal.Text = "", 0, txtSubtotal.Text))

        txtTotalVenta.Text = CDbl(IIf(txtSubtotal.Text = "", 0, txtSubtotal.Text)) - ((pordescuento / 100) * CDbl(IIf(txtSubtotal.Text = "", 0, txtSubtotal.Text)))
        txtTotalVenta.Text = FormatNumber(txtTotalVenta.Text, 2)

    End Sub

    Private Sub grdCaptura_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles grdCaptura.CellDoubleClick
        Dim index As Integer = grdCaptura.CurrentRow.Index

        Dim tot As Double = grdCaptura.Rows(index).Cells(5).Value.ToString
        txtTotalVenta.Text = CDbl(txtTotalVenta.Text) - CDbl(tot)
        txtTotalVenta.Text = FormatNumber(txtTotalVenta.Text, 2)

        grdCaptura.Rows.Remove(grdCaptura.CurrentRow)
    End Sub

    Private Sub txtUsuario_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtUsuario.KeyPress
        If AscW(e.KeyChar) = Keys.Enter Then
            cnn1.Clone() : cnn1.Open()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "SELECT Alias,Status FROM usuarios WHERE Clave='" & txtUsuario.Text & "'"
            rd1 = cmd1.ExecuteReader
            If rd1.HasRows Then
                If rd1.Read Then
                    If rd1(1).ToString = 1 Then
                        lblUsuario.Text = rd1(0).ToString
                    Else
                        MsgBox("El usuario esta inactivo, Contacte a su administrador.", vbInformation + vbOKOnly, titulorefaccionaria)
                        txtUsuario.Text = ""
                        lblUsuario.Text = ""
                        txtUsuario.Focus.Equals(True)
                        Exit Sub
                    End If
                End If
            Else
                MsgBox("Contraseña incorrecta.", vbInformation + vbOKOnly, titulorefaccionaria)
                txtUsuario.Text = ""
                lblUsuario.Text = ""
                txtUsuario.Focus.Equals(True)
                Exit Sub
            End If
            rd1.Close()
            cnn1.Close()

        End If
    End Sub
End Class