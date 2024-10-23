Imports DocumentFormat.OpenXml.Drawing.Charts

Public Class frmAnticiposReservaciones

    Dim fentrada As String = ""
    Dim fechae As Date = Nothing
    Dim fsalida As String = ""
    Dim fechas As Date = Nothing
    Dim cadenafact As String = ""

    Dim simbolo As String = ""
    Private Sub frmAnticiposReservaciones_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        simbolo = DatosRecarga("Simbolo")
    End Sub

    Private Sub cboFolio_DropDown(sender As Object, e As EventArgs) Handles cboFolio.DropDown
        Try
            cboFolio.Items.Clear()

            cnn5.Close() : cnn5.Open()
            cmd5 = cnn5.CreateCommand
            cmd5.CommandText = "SELECT DISTINCT IdReservacion FROM reservaciones WHERE IdReservacion<>'' AND Status=0 ORDER BY IdReservacion"
            rd5 = cmd5.ExecuteReader
            Do While rd5.Read
                If rd5.HasRows Then
                    cboFolio.Items.Add(rd5(0).ToString)
                End If
            Loop
            rd5.Close()
            cnn5.Close()

        Catch ex As Exception
            MessageBox.Show(ex.ToString)
            cnn5.Close()
        End Try
    End Sub

    Private Sub txtContra_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtContra.KeyPress
        If AscW(e.KeyChar) = Keys.Enter Then
            cnn1.Close() : cnn1.Open()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "SELECT Alias,Status FROM Usuarios WHERE Clave='" & txtContra.Text & "'"
            rd1 = cmd1.ExecuteReader
            If rd1.HasRows Then
                If rd1.Read Then
                    If rd1(1).ToString = 1 Then
                        lblUsuario.Text = rd1(0).ToString
                        btnAbonar.Focus.Equals(True)
                    Else
                        MsgBox("El usuario esta inactivo contacte a su administrador.", vbInformation + vbOKOnly, titulohotelriaa)
                    End If
                End If
            Else
                MsgBox("Contraseña incorrecta.", vbInformation + vbOKOnly, titulohotelriaa)
                txtContra.Text = ""
                txtContra.Focus.Equals(True)
                Exit Sub
            End If
            rd1.Close()
            cnn1.Close()

        End If
    End Sub

    Private Sub txtContra_TextChanged(sender As Object, e As EventArgs) Handles txtContra.TextChanged
        If txtContra.Text = "" Then
            lblUsuario.Text = "Contraseña"
            txtContra.Text = ""
            txtContra.Focus.Equals(True)
        End If
    End Sub

    Private Sub cboClIente_DropDown(sender As Object, e As EventArgs) Handles cboClIente.DropDown
        Try
            cboClIente.Items.Clear()

            cnn5.Close() : cnn5.Open()
            cmd5 = cnn5.CreateCommand
            cmd5.CommandText = "SELECT DISTINCT Cliente FROM reservaciones WHERE Cliente<>'' AND Status=0 ORDER BY Cliente"
            rd5 = cmd5.ExecuteReader
            Do While rd5.Read
                If rd5.HasRows Then
                    cboClIente.Items.Add(rd5(0).ToString)
                End If
            Loop
            rd5.Close()
            cnn5.Close()

        Catch ex As Exception
            MessageBox.Show(ex.ToString)
            cnn5.Close()
        End Try
    End Sub

    Public Sub cboFolio_SelectedValueChanged(sender As Object, e As EventArgs) Handles cboFolio.SelectedValueChanged
        Try
            Dim simon As Integer = 0
            Dim varhoras As Integer = 0
            Dim saldo As Double = 0
            Dim anticipo As Double = 0
            cnn1.Close() : cnn1.Open()

            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "SELECT Saldo FROM abonoi WHERE Id=(select max(Id) from abonoi WHERE Comentario='" & cboFolio.Text & "' )"
            rd1 = cmd1.ExecuteReader
            If rd1.HasRows Then
                If rd1.Read Then
                    saldo = IIf(rd1(0).ToString = "", 0, rd1(0).ToString)
                End If
            End If
            rd1.Close()

            If saldo > 0 Then
                txtResta.Text = FormatNumber(saldo, 2)
            End If



            cmd1 = cnn1.CreateCommand
            If cboClIente.Text = "" Then
                cmd1.CommandText = "SELECT * FROM reservaciones WHERE IdReservacion=" & cboFolio.Text & ""
                simon = 0
            Else
                cmd1.CommandText = "SELECT * FROM reservaciones WHERE IdReservacion=" & cboFolio.Text & " AND Cliente='" & cboClIente.Text & "'"
                simon = 1
            End If
            rd1 = cmd1.ExecuteReader
            If rd1.HasRows Then
                If rd1.Read Then
                    If simon = 0 Then
                        cboClIente.Text = rd1("Cliente").ToString
                    Else
                        cboFolio.Text = rd1("IdReservacion").ToString
                    End If
                    lblHabitacion.Text = rd1("Habitacion").ToString

                    fechae = rd1("Fentrada").ToString
                    fentrada = Format(fechae, "yyyy-MM-dd")
                    lblEntrada.Text = fentrada

                    fechas = rd1("FSalida").ToString
                    fsalida = Format(fechas, "yyyy-MM-dd")
                    lblSalida.Text = fsalida

                    cboTipo.Text = rd1("Tipo").ToString
                    cboPrecio.Text = rd1("Precio").ToString

                    varhoras = DateDiff(DateInterval.Hour, CDate(fechae), fechas)
                    txtHoras.Text = varhoras
                    cboTipo.Focus.Equals(True)
                End If
            End If
            rd1.Close()

            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "SELECT SUM(Abono) FROM abonoi WHERE Comentario='" & cboFolio.Text & "' AND Cliente='" & cboClIente.Text & "'"
            rd1 = cmd1.ExecuteReader
            If rd1.HasRows Then
                If rd1.Read Then
                    anticipo = IIf(rd1(0).ToString = "", 0, rd1(0).ToString)
                End If
            End If
            rd1.Close()

            If anticipo > 0 Then
                txtAnticipo.Text = FormatNumber(anticipo, 2)
                lblAnticipo.Visible = True
                txtAnticipo.Visible = True
            Else
                lblAnticipo.Visible = False
                txtAnticipo.Visible = False
            End If
            cnn1.Close()

        Catch ex As Exception
            MessageBox.Show(ex.ToString)
            cnn1.Close()
        End Try
    End Sub

    Private Sub cboTipo_DropDown(sender As Object, e As EventArgs) Handles cboTipo.DropDown
        Try
            cboTipo.Items.Clear()
            cnn5.Close() : cnn5.Open()
            cmd5 = cnn5.CreateCommand
            cmd5.CommandText = "SELECT DISTINCT Nombre FROM detallehotelprecios WHERE Nombre<>'' ORDER BY Nombre"
            rd5 = cmd5.ExecuteReader
            Do While rd5.Read
                If rd5.HasRows Then
                    cboTipo.Items.Add(rd5(0).ToString)
                End If
            Loop
            rd5.Close()
            cnn5.Close()

        Catch ex As Exception
            MessageBox.Show(ex.ToString)
            cnn5.Close()
        End Try
    End Sub

    Private Sub cboPrecio_DropDown(sender As Object, e As EventArgs) Handles cboPrecio.DropDown
        Try
            cboPrecio.Items.Clear()

            cnn5.Close() : cnn5.Open()
            cmd5 = cnn5.CreateCommand
            cmd5.CommandText = "SELECT PrecioA,PrecioB,PrecioC,PrecioD,PrecioE FROM detallehotelprecios WHERE Nombre='" & cboTipo.Text & "'"
            rd5 = cmd5.ExecuteReader
            Do While rd5.Read
                If rd5.HasRows Then
                    cboPrecio.Items.Add(rd5("PrecioA").ToString)
                    cboPrecio.Items.Add(rd5("PrecioB").ToString)
                    cboPrecio.Items.Add(rd5("PrecioC").ToString)
                    cboPrecio.Items.Add(rd5("PrecioD").ToString)
                    cboPrecio.Items.Add(rd5("PrecioE").ToString)
                End If
            Loop
            rd5.Close()
            cnn5.Close()

        Catch ex As Exception
            MessageBox.Show(ex.ToString)
            cnn5.Close()
        End Try
    End Sub

    Private Sub cboTipo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cboTipo.KeyPress
        If AscW(e.KeyChar) = Keys.Enter Then
            cboPrecio.Focus.Equals(True)
        End If
    End Sub

    Private Sub cboPrecio_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cboPrecio.KeyPress
        If AscW(e.KeyChar) = Keys.Enter Then
            If IsNumeric(cboPrecio.Text) Then
                txtTotalVenta.Text = FormatNumber(cboPrecio.Text, 2)
                txtSubtotal.Text = FormatNumber(cboPrecio.Text, 2)
                txtResta.Text = FormatNumber(cboPrecio.Text, 2)
                txtEfectivo.Focus.Equals(True)
            End If
        End If
    End Sub

    Private Sub txtDescuento_TextChanged(sender As Object, e As EventArgs) Handles txtDescuento.TextChanged

        Dim resta As Double = 0

        If txtDescuento.Text = "" Then
            txtDescuento.Text = "0.00"
            txtTotalVenta.Text = txtSubtotal.Text
            Exit Sub
        End If

        If CDbl(txtDescuento.Text) > 0 Then
            txtEfectivo.Text = "0.00"
            txtTarjeta.Text = "0.00"
            txtTransfe.Text = "0.00"
            txtOtro.Text = "0.00"
        End If

        Dim descuento As Double = txtDescuento.Text
        Dim pordescuento As Double = 0
        pordescuento = (descuento * 100) / CDbl(txtSubtotal.Text)

        txtTotalVenta.Text = CDbl(txtSubtotal.Text) - ((pordescuento / 100) * CDbl(txtSubtotal.Text))
        txtTotalVenta.Text = FormatNumber(txtTotalVenta.Text, 2)
        txtResta.Text = CDbl(txtSubtotal.Text) - ((pordescuento / 100) * CDbl(txtSubtotal.Text))
        txtResta.Text = FormatNumber(txtResta.Text, 2)

        cnn5.Close() : cnn5.Open()
        cmd5 = cnn5.CreateCommand
        cmd5.CommandText = "SELECT NotasCred FROM Formatos WHERE Facturas='Mdescuento'"
        rd5 = cmd5.ExecuteReader
        If rd5.HasRows Then
            If rd5.Read Then
                pordescuento = rd5(0).ToString
            End If
        Else

        End If
        rd5.Close()
        cnn5.Close()

    End Sub

    Private Sub txtEfectivo_TextChanged(sender As Object, e As EventArgs) Handles txtEfectivo.TextChanged

        If Not IsNumeric(txtEfectivo.Text) Then txtEfectivo.Text = "0.00" : Exit Sub
        If Strings.Left(txtEfectivo.Text, 1) = "," Or Strings.Left(txtEfectivo.Text, 1) = "." Then Exit Sub

        Dim MyOpe As Double = CDbl(IIf(txtTotalVenta.Text = "", "0", txtTotalVenta.Text)) - (CDbl(IIf(txtTarjeta.Text = "", "0", txtTarjeta.Text)) + CDbl(IIf(txtTransfe.Text = "", "0", txtTransfe.Text)) + CDbl(IIf(txtOtro.Text = "", "0", txtOtro.Text)) + CDbl(IIf(txtEfectivo.Text = "", "0", txtEfectivo.Text)))

        If MyOpe < 0 Then
            txtCambio.Text = FormatNumber(-MyOpe, 2)
            txtResta.Text = "0.00"
        Else
            txtResta.Text = FormatNumber(MyOpe, 2)
            txtCambio.Text = "0.00"
        End If
        txtCambio.Text = FormatNumber(txtCambio.Text, 2)
        txtResta.Text = FormatNumber(txtResta.Text, 2)
    End Sub

    Private Sub txtTarjeta_TextChanged(sender As Object, e As EventArgs) Handles txtTarjeta.TextChanged

        If Not IsNumeric(txtTarjeta.Text) Then txtTarjeta.Text = "0.00" : Exit Sub
        If Strings.Left(txtTarjeta.Text, 1) = "," Or Strings.Left(txtTarjeta.Text, 1) = "." Then Exit Sub

        Dim MyOpe As Double = CDbl(IIf(txtTotalVenta.Text = "", "0", txtTotalVenta.Text)) - (CDbl(IIf(txtTarjeta.Text = "", "0", txtTarjeta.Text)) + CDbl(IIf(txtTransfe.Text = "", "0", txtTransfe.Text)) + CDbl(IIf(txtOtro.Text = "", "0", txtOtro.Text)) + CDbl(IIf(txtEfectivo.Text = "", "0", txtEfectivo.Text)))

        If MyOpe < 0 Then
            txtCambio.Text = FormatNumber(-MyOpe, 2)
            txtResta.Text = "0.00"
        Else
            txtResta.Text = FormatNumber(MyOpe, 2)
            txtCambio.Text = "0.00"
        End If
        txtCambio.Text = FormatNumber(txtCambio.Text, 2)
        txtResta.Text = FormatNumber(txtResta.Text, 2)
    End Sub

    Private Sub txtTransfe_TextChanged(sender As Object, e As EventArgs) Handles txtTransfe.TextChanged

        If Not IsNumeric(txtTransfe.Text) Then txtTransfe.Text = "0.00" : Exit Sub
        If Strings.Left(txtTransfe.Text, 1) = "," Or Strings.Left(txtTransfe.Text, 1) = "." Then Exit Sub

        Dim MyOpe As Double = CDbl(IIf(txtTotalVenta.Text = "", "0", txtTotalVenta.Text)) - (CDbl(IIf(txtTarjeta.Text = "", "0", txtTarjeta.Text)) + CDbl(IIf(txtTransfe.Text = "", "0", txtTransfe.Text)) + CDbl(IIf(txtOtro.Text = "", "0", txtOtro.Text)) + CDbl(IIf(txtEfectivo.Text = "", "0", txtEfectivo.Text)))

        If MyOpe < 0 Then
            txtCambio.Text = FormatNumber(-MyOpe, 2)
            txtResta.Text = "0.00"
        Else
            txtResta.Text = FormatNumber(MyOpe, 2)
            txtCambio.Text = "0.00"
        End If
        txtCambio.Text = FormatNumber(txtCambio.Text, 2)
        txtResta.Text = FormatNumber(txtResta.Text, 2)
    End Sub

    Private Sub txtOtro_TextChanged(sender As Object, e As EventArgs) Handles txtOtro.TextChanged

        If Not IsNumeric(txtOtro.Text) Then txtOtro.Text = "0.00" : Exit Sub
        If Strings.Left(txtOtro.Text, 1) = "," Or Strings.Left(txtOtro.Text, 1) = "." Then Exit Sub

        Dim MyOpe As Double = CDbl(IIf(txtTotalVenta.Text = "", "0", txtTotalVenta.Text)) - (CDbl(IIf(txtTarjeta.Text = "", "0", txtTarjeta.Text)) + CDbl(IIf(txtTransfe.Text = "", "0", txtTransfe.Text)) + CDbl(IIf(txtOtro.Text = "", "0", txtOtro.Text)) + CDbl(IIf(txtEfectivo.Text = "", "0", txtEfectivo.Text)))

        If MyOpe < 0 Then
            txtCambio.Text = FormatNumber(-MyOpe, 2)
            txtResta.Text = "0.00"
        Else
            txtResta.Text = FormatNumber(MyOpe, 2)
            txtCambio.Text = "0.00"
        End If
        txtCambio.Text = FormatNumber(txtCambio.Text, 2)
        txtResta.Text = FormatNumber(txtResta.Text, 2)
    End Sub

    Private Sub txtEfectivo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtEfectivo.KeyPress
        If AscW(e.KeyChar) = Keys.Enter Then
            If IsNumeric(txtEfectivo.Text) Then
                txtTarjeta.Focus.Equals(True)
            Else
                txtEfectivo.Text = "0.00"
            End If
        End If
    End Sub

    Private Sub txtTarjeta_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtTarjeta.KeyPress
        If AscW(e.KeyChar) = Keys.Enter Then
            If IsNumeric(txtTarjeta.Text) Then
                txtTransfe.Focus.Equals(True)
            Else
                txtTransfe.Text = "0.00"
            End If
        End If
    End Sub

    Private Sub txtTransfe_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtTransfe.KeyPress
        If AscW(e.KeyChar) = Keys.Enter Then
            If IsNumeric(txtTransfe.Text) Then
                txtOtro.Focus.Equals(True)
            Else
                txtTransfe.Text = "00"
            End If
        End If
    End Sub

    Private Sub txtOtro_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtOtro.KeyPress
        If AscW(e.KeyChar) = Keys.Enter Then
            If IsNumeric(txtOtro.Text) Then
                btnAbonar.Focus.Equals(True)
            Else
                txtOtro.Text = "0.00"
            End If
        End If
    End Sub

    Private Sub btnSalir_Click(sender As Object, e As EventArgs) Handles btnSalir.Click
        Me.Close()
    End Sub

    Private Sub btnAbonar_Click(sender As Object, e As EventArgs) Handles btnAbonar.Click
        Try
            Dim idcliente As Integer = 0
            Dim saldo As Double = 0
            Dim acuenta As Double = 0
            Dim mystatus As String = ""
            Dim myresta As Double = 0
            Dim myfolioinicial As Integer = 0
            Dim myfolio As Integer = 0
            Dim mydescuento As Double = 0
            Dim mysubtotal As Double = 0
            Dim mytotalventa As Double = 0

            Dim ope1 As Double = 0
            Dim cadena As String = ""
            Dim Car As Integer = 0

            Dim letters As String = ""
            Dim Numeros As String = ""
            Dim Letras As String = ""
            Dim lic As String = ""
            Dim CodCadena As String = ""

            Dim efectivo As Double = 0
            Dim tarjeta As Double = 0
            Dim transaferencia As Double = 0
            Dim otro As Double = 0
            Dim cambio As Double = 0

            If lblUsuario.Text = "Contraseña" Then
                MsgBox("Ingrese la contraseña para continuar.", vbInformation + vbOKOnly, titulohotelriaa)
                txtContra.Focus.Equals(True)
                Exit Sub
            End If

            If MsgBox("¿Desea realizar el abono a esta reservación?", vbInformation + vbYesNo, titulocentral) = vbYes Then


                If CDbl(txtResta.Text) = 0 Then
                    mystatus = "PAGADO"
                Else
                    mystatus = "RESTA"
                End If

                efectivo = CDbl(txtEfectivo.Text)
                tarjeta = CDbl(txtTarjeta.Text)
                transaferencia = CDbl(txtTransfe.Text)
                otro = CDbl(txtOtro.Text)
                cambio = CDbl(txtCambio.Text)

                acuenta = FormatNumber((CDbl(efectivo) + CDbl(tarjeta) + CDbl(transaferencia) + CDbl(otro)) - CDbl(cambio), 2)
                mydescuento = FormatNumber(CDbl(txtDescuento.Text), 2)
                mysubtotal = FormatNumber(CDbl(txtSubtotal.Text), 2)
                mytotalventa = FormatNumber(CDbl(txtTotalVenta.Text), 2)
                myresta = FormatNumber(CDbl(txtResta.Text), 2)

                cnn1.Close() : cnn1.Open()
                cmd1 = cnn1.CreateCommand
                cmd1.CommandText = "SELECT Max(Folio) FROM ventas"
                rd1 = cmd1.ExecuteReader
                If rd1.HasRows Then
                    If rd1.Read Then
                        myfolioinicial = IIf(rd1(0).ToString = "", 0, rd1(0).ToString) + 1
                    End If
                Else
                    myfolioinicial = 1
                End If
                rd1.Close()
                cnn1.Close()

                ope1 = Math.Cos(CDbl(myfolioinicial))
                If ope1 > 0 Then
                    cadena = Strings.Left(Replace(CStr(ope1), ".", "9"), 10)
                Else
                    cadena = Strings.Left(Replace(CStr(Math.Abs(ope1)), ".", "8"), 10)
                End If
                For i = 1 To 10
                    Car = Mid(cadena, i, 1)
                    Select Case Car
                        Case Is = 0
                            letters = letters & "Y"
                        Case Is = 1
                            letters = letters & "Z"
                        Case Is = 2
                            letters = letters & "W"
                        Case Is = 3
                            letters = letters & "H"
                        Case Is = 4
                            letters = letters & "S"
                        Case Is = 5
                            letters = letters & "B"
                        Case Is = 6
                            letters = letters & "C"
                        Case Is = 7
                            letters = letters & "P"
                        Case Is = 8
                            letters = letters & "Q"
                        Case Is = 9
                            letters = letters & "A"
                        Case Else
                            letters = letters & Car
                    End Select
                Next
                For w = 1 To 10 Step 2
                    Numeros = Mid(myfolioinicial, w, 4)
                    Letras = Mid(letters, w, 4)
                    lic = lic & Numeros & Letras & "-"
                Next
                lic = Strings.Left(lic, Len(lic) - 1)
                CodCadena = lic
                cadenafact = Trim(CodCadena)



                cnn1.Close() : cnn1.Open()
                cmd1 = cnn1.CreateCommand
                cmd1.CommandText = "SELECT Id FROM clientes WHERE Nombre='" & cboClIente.Text & "'"
                rd1 = cmd1.ExecuteReader
                If rd1.HasRows Then
                    If rd1.Read Then
                        idcliente = rd1(0).ToString
                    End If
                Else
                    idcliente = 0
                End If
                rd1.Close()

                cmd1 = cnn1.CreateCommand
                cmd1.CommandText = "INSERT INTO ventas(IdCliente,Cliente,Direccion,Subtotal,IVA,Totales,Descuento,Devolucion,Acuenta,Resta,Usuario,FVenta,HVenta,FPago,FCancelado,Status,Comisionista,Comision,Concepto,MontoSinDesc,FEntrega,Comentario,StatusE,FolMonedero,CodFactura,IP,Formato,Franquicia,Pedido,Fecha) values(" & idcliente & ",'" & cboClIente.Text & "',''," & mysubtotal & ",0," & mytotalventa & "," & mydescuento & ",0," & acuenta & "," & myresta & ",'" & lblUsuario.Text & "','" & Format(Date.Now, "yyyy-MM-dd") & "','" & Format(Date.Now, "HH:mm:ss") & "','','','" & mystatus & "','',0,'RESERVACION'," & mysubtotal & ",'" & Format(Date.Now, "yyyy-MM-dd") & "','',0,'','" & cadenafact & "','" & dameIP2() & "','TICKET',0,'','" & Format(Date.Now, "yyyy-MM-dd HH:mm:ss") & "')"
                cmd1.ExecuteNonQuery()

                cmd1 = cnn1.CreateCommand
                cmd1.CommandText = "SELECT Max(Folio) FROM ventas WHERE IP='" & dameIP2() & "'"
                rd1 = cmd1.ExecuteReader
                If rd1.HasRows Then
                    If rd1.Read Then
                        myfolio = IIf(rd1(0).ToString = "", 0, rd1(0).ToString)
                    End If
                End If
                rd1.Close()


                cmd1 = cnn1.CreateCommand
                cmd1.CommandText = "SELECT Saldo FROM abonoi WHERE Id=(SELECT MAX(Id) FROM abonoi WHERE Cliente='" & cboClIente.Text & "')"
                rd1 = cmd1.ExecuteReader
                If rd1.HasRows Then
                    If rd1.Read Then
                        saldo = IIf(rd1(0).ToString = "", 0, rd1(0).ToString) + CDbl(txtTotalVenta.Text)
                    End If
                Else

                    saldo = CDbl(txtTotalVenta.Text)
                End If
                rd1.Close()

                cmd1 = cnn1.CreateCommand
                cmd1.CommandText = "INSERT INTO abonoi(NumFolio,IdCliente,Cliente,Concepto,fecha,Hora,FechaCompleta,Cargo,Abono,Saldo,FormaPago,Monto,Banco,Referencia,Usuario,Descuento,MontoSF,Comentario) VALUES(" & myfolio & "," & idcliente & ",'" & cboClIente.Text & "','NOTA VENTA','" & Format(Date.Now, "yyyy-MM-dd") & "','" & Format(Date.Now, "HH:mm:ss") & "','" & Format(Date.Now, "yyyy-MM-dd HH:mm:ss") & "'," & saldo & ",0," & saldo & ",'',0,'','','" & lblUsuario.Text & "'," & mydescuento & ",0,'" & cboFolio.Text & "')"
                cmd1.ExecuteNonQuery()

                If efectivo > 0 Then
                    cmd1 = cnn1.CreateCommand
                    cmd1.CommandText = "SELECT Saldo FROM abonoi WHERE Id=(SELECT MAX(Id) FROM abonoi WHERE Cliente='" & cboClIente.Text & "')"
                    rd1 = cmd1.ExecuteReader
                    If rd1.HasRows Then
                        If rd1.Read Then
                            saldo = FormatNumber(IIf(rd1(0).ToString = "", 0, rd1(0).ToString) - efectivo, 2)
                        End If
                    Else
                        saldo = FormatNumber(txtTotalVenta.Text, 2)
                    End If
                    rd1.Close()


                    cmd1 = cnn1.CreateCommand
                    cmd1.CommandText = "INSERT INTO abonoi(NumFolio,IdCliente,Cliente,Concepto,Fecha,Hora,FechaCompleta,Cargo,Abono,Saldo,FormaPago,Monto,Banco,Referencia,Usuario,Comentario,Comisiones) VALUES(" & myfolio & "," & idcliente & ",'" & cboClIente.Text & "','ABONO','" & Format(Date.Now, "yyyy-MM-dd") & "','" & Format(Date.Now, "HH:mm:ss") & "','" & Format(Date.Now, "yyyy-MM-dd HH:mm:ss") & "',0," & efectivo & "," & saldo & ",'EFECTIVO'," & efectivo & ",'','','" & lblUsuario.Text & "','" & cboFolio.Text & "',0)"
                    cmd1.ExecuteNonQuery()
                End If

                If tarjeta > 0 Then
                    cmd1 = cnn1.CreateCommand
                    cmd1.CommandText = "SELECT Saldo FROM abonoi WHERE Id=(SELECT MAX(Id) FROM abonoi WHERE Cliente='" & cboClIente.Text & "')"
                    rd1 = cmd1.ExecuteReader
                    If rd1.HasRows Then
                        If rd1.Read Then
                            saldo = FormatNumber(IIf(rd1(0).ToString = "", 0, rd1(0).ToString) - tarjeta, 2)
                        End If
                    Else
                        saldo = FormatNumber(txtTotalVenta.Text, 2)
                    End If
                    rd1.Close()


                    cmd1 = cnn1.CreateCommand
                    cmd1.CommandText = "INSERT INTO abonoi(NumFolio,IdCliente,Cliente,Concepto,Fecha,Hora,FechaCompleta,Cargo,Abono,Saldo,FormaPago,Monto,Banco,Referencia,Usuario,Comentario,Comisiones) VALUES(" & myfolio & "," & idcliente & ",'" & cboClIente.Text & "','ABONO','" & Format(Date.Now, "yyyy-MM-dd") & "','" & Format(Date.Now, "HH:mm:ss") & "','" & Format(Date.Now, "yyyy-MM-dd HH:mm:ss") & "',0," & tarjeta & "," & saldo & ",'TARJETA'," & tarjeta & ",'','','" & lblUsuario.Text & "','" & cboFolio.Text & "',0)"
                    cmd1.ExecuteNonQuery()


                End If

                If transaferencia > 0 Then

                    cmd1 = cnn1.CreateCommand
                    cmd1.CommandText = "SELECT Saldo FROM abonoi WHERE Id=(SELECT MAX(Id) FROM abonoi WHERE Cliente='" & cboClIente.Text & "')"
                    rd1 = cmd1.ExecuteReader
                    If rd1.HasRows Then
                        If rd1.Read Then
                            saldo = FormatNumber(IIf(rd1(0).ToString = "", 0, rd1(0).ToString) - transaferencia, 2)
                        End If
                    Else
                        saldo = FormatNumber(txtTotalVenta.Text, 2)
                    End If
                    rd1.Close()

                    cmd1 = cnn1.CreateCommand
                    cmd1.CommandText = "INSERT INTO abonoi(NumFolio,IdCliente,Cliente,Concepto,Fecha,Hora,FechaCompleta,Cargo,Abono,Saldo,FormaPago,Monto,Banco,Referencia,Usuario,Comentario,Comisiones) VALUES(" & myfolio & "," & idcliente & ",'" & cboClIente.Text & "','ABONO','" & Format(Date.Now, "yyyy-MM-dd") & "','" & Format(Date.Now, "HH:mm:ss") & "','" & Format(Date.Now, "yyyy-MM-dd HH:mm:ss") & "',0," & transaferencia & "," & saldo & ",'TRANSFERENCIA'," & transaferencia & ",'','','" & lblUsuario.Text & "','" & cboFolio.Text & "',0)"
                    cmd1.ExecuteNonQuery()
                End If

                If otro > 0 Then

                    cmd1 = cnn1.CreateCommand
                    cmd1.CommandText = "SELECT Saldo FROM abonoi WHERE Id=(SELECT MAX(Id) FROM abonoi WHERE Cliente='" & cboClIente.Text & "')"
                    rd1 = cmd1.ExecuteReader
                    If rd1.HasRows Then
                        If rd1.Read Then
                            saldo = FormatNumber(IIf(rd1(0).ToString = "", 0, rd1(0).ToString) - otro, 2)
                        End If
                    Else
                        saldo = FormatNumber(txtTotalVenta.Text, 2)
                    End If
                    rd1.Close()
                    cmd1 = cnn1.CreateCommand
                    cmd1.CommandText = "INSERT INTO abonoi(NumFolio,IdCliente,Cliente,Concepto,Fecha,Hora,FechaCompleta,Cargo,Abono,Saldo,FormaPago,Monto,Banco,Referencia,Usuario,Comentario,Comisiones) VALUES(" & myfolio & "," & idcliente & ",'" & cboClIente.Text & "','ABONO','" & Format(Date.Now, "yyyy-MM-dd") & "','" & Format(Date.Now, "HH:mm:ss") & "','" & Format(Date.Now, "yyyy-MM-dd HH:mm:ss") & "',0," & otro & "," & saldo & ",'OTRO'," & otro & ",'','','" & lblUsuario.Text & "','" & cboFolio.Text & "',0)"
                    cmd1.ExecuteNonQuery()


                End If
                cnn1.Close()

                cnn2.Close() : cnn2.Open()
                cmd2 = cnn2.CreateCommand
                cmd2.CommandText =
            "insert into VentasDetalle(Folio,Codigo,Nombre,Unidad,Cantidad,CostoVP,CostoVUE,Precio,Total,PrecioSinIVA,TotalSinIVA,Fecha,FechaCompleta,Comisionista,Facturado,Depto,Grupo,CostVR,Descto,VDCosteo,TotalIEPS,TasaIEPS,Caducidad,Lote,CantidadE,Promo_Monedero,Unico,Descuento,Gprint,CodUnico) values(" & myfolio & ",'xc3','Tiempo Habitacion','PZA',1,0,0," & mysubtotal & "," & mytotalventa & "," & mysubtotal & "," & mytotalventa & ",'" & Format(Date.Now, "yyyy-MM-dd") & "','" & Format(Date.Now, "yyyy-MM-dd HH:mm:ss") & "','0','0','HABITACION','HABITACION','0',0,0,0,0,'','',0,0,0," & mydescuento & ",'','')"
                cmd2.ExecuteNonQuery()

                cmd2 = cnn2.CreateCommand
                cmd2.CommandText = "UPDATE reservaciones SET Tipo='" & cboTipo.Text & "', Precio=" & cboPrecio.Text & ",Anticipo=" & acuenta & " WHERE IdReservacion=" & cboFolio.Text & " AND Cliente='" & cboClIente.Text & "'"
                cmd2.ExecuteNonQuery()
                cnn2.Close()

                Dim timpresora As Integer = TamImpre()
                Dim impresora As String = ImpresoraImprimir()

                If impresora = "" Then
                    MsgBox("La impresora no esta configurada.", vbInformation + vbOKOnly, titulohotelriaa)
                    GoTo deku
                End If

                If timpresora = "80" Then
                    PReservacion80.DefaultPageSettings.PrinterSettings.PrinterName = impresora
                    PReservacion80.Print()
                End If
deku:
                btnLimpiar.PerformClick()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
            cnn1.Close()
        End Try
    End Sub

    Private Sub txtEfectivo_Click(sender As Object, e As EventArgs) Handles txtEfectivo.Click
        txtEfectivo.SelectAll()
    End Sub

    Private Sub txtTarjeta_Click(sender As Object, e As EventArgs) Handles txtTarjeta.Click
        txtTarjeta.SelectAll()
    End Sub

    Private Sub txtTransfe_Click(sender As Object, e As EventArgs) Handles txtTransfe.Click
        txtTransfe.SelectAll()
    End Sub

    Private Sub txtOtro_Click(sender As Object, e As EventArgs) Handles txtOtro.Click
        txtOtro.SelectAll()
    End Sub

    Private Sub btnLimpiar_Click(sender As Object, e As EventArgs) Handles btnLimpiar.Click
        txtEfectivo.Text = "0.00"
        txtTarjeta.Text = "0.00"
        txtTransfe.Text = "0.00"
        txtOtro.Text = "0.00"
        txtDescuento.Text = "0.00"
        txtCambio.Text = "0.00"
        txtResta.Text = "0.00"
        txtCambio.Text = "0.00"
        txtTotalVenta.Text = "0.00"
        txtSubtotal.Text = "0.00"
        txtAnticipo.Text = "0.00"
        cboTipo.Text = ""
        txtHoras.Text = ""
        cboPrecio.Text = ""
        lblEntrada.Text = ""
        lblSalida.Text = ""
        cboClIente.Text = ""
        lblHabitacion.Text = ""
        cboFolio.Text = ""

        lblAnticipo.Visible = False
        txtAnticipo.Visible = False
    End Sub

    Private Sub PReservacion80_PrintPage(sender As Object, e As Printing.PrintPageEventArgs) Handles PReservacion80.PrintPage

        Dim tipografia As String = "Lucida Sans Typewriter"
        Dim fuente_r As New Font("Lucida Sans Typewriter", 8, FontStyle.Regular)
        Dim fuente_b As New Font("Lucida Sans Typewriter", 8, FontStyle.Bold)
        Dim fuente_c As New Font("Lucida Sans Typewriter", 8, FontStyle.Regular)
        Dim fuente_p As New Font("Lucida Sans Typewriter", 7, FontStyle.Regular)
        Dim derecha As New StringFormat With {.Alignment = StringAlignment.Far}
        Dim sc As New StringFormat With {.Alignment = StringAlignment.Center}
        Dim hoja As New Pen(Brushes.Black, 1)
        Dim Y As Double = 0
        Dim Logotipo As Drawing.Image = Nothing
        Dim Pie As String = ""

        cnn1.Close() : cnn1.Open()
        cmd1 = cnn1.CreateCommand
        cmd1.CommandText =
            "select Pie1,Pagare,Cab0,Cab1,Cab2,Cab3,Cab4,Cab5,Cab6 from Ticket"
        rd1 = cmd1.ExecuteReader
        If rd1.HasRows Then
            If rd1.Read Then
                pie = rd1("Pie1").ToString

                'Razón social
                If rd1("Cab0").ToString() <> "" Then
                    e.Graphics.DrawString(rd1("Cab0").ToString, New Drawing.Font(tipografia, 8, FontStyle.Bold), Brushes.Black, 140, Y, sc)
                    Y += 12.5
                End If
                'RFC
                If rd1("Cab1").ToString() <> "" Then
                    e.Graphics.DrawString(rd1("Cab1").ToString, New Drawing.Font(tipografia, 8, FontStyle.Bold), Brushes.Black, 140, Y, sc)
                    Y += 12.5
                End If
                'Calle  N°.
                If rd1("Cab2").ToString() <> "" Then
                    e.Graphics.DrawString(rd1("Cab2").ToString, New Drawing.Font(tipografia, 8, FontStyle.Regular), Brushes.Gray, 140, Y, sc)
                    Y += 12
                End If
                'Colonia
                If rd1("Cab3").ToString() <> "" Then
                    e.Graphics.DrawString(rd1("Cab3").ToString, New Drawing.Font(tipografia, 8, FontStyle.Regular), Brushes.Gray, 140, Y, sc)
                    Y += 12
                End If
                'Delegación / Municipio - Entidad
                If rd1("Cab4").ToString() <> "" Then
                    e.Graphics.DrawString(rd1("Cab4").ToString, New Drawing.Font(tipografia, 8, FontStyle.Regular), Brushes.Gray, 140, Y, sc)
                    Y += 12
                End If
                'Teléfono
                If rd1("Cab5").ToString() <> "" Then
                    e.Graphics.DrawString(rd1("Cab5").ToString, New Drawing.Font(tipografia, 8, FontStyle.Regular), Brushes.Gray, 140, Y, sc)
                    Y += 12
                End If
                'Correo
                If rd1("Cab6").ToString() <> "" Then
                    e.Graphics.DrawString(rd1("Cab6").ToString, New Drawing.Font(tipografia, 8, FontStyle.Regular), Brushes.Gray, 140, Y, sc)
                    Y += 12
                End If
                Y += 3
            End If
        Else
            Y += 0
        End If
        rd1.Close()
        cnn1.Close()


        '[1]. Datos de la venta
        e.Graphics.DrawString("-----------------------------------------", New Drawing.Font(tipografia, 12, FontStyle.Regular), Brushes.Black, 1, Y)
        Y += 15
        e.Graphics.DrawString("R E S E R V A C I Ó N", New Drawing.Font(tipografia, 9, FontStyle.Bold), Brushes.Black, 140, Y, sc)
        Y += 17
        e.Graphics.DrawString("-----------------------------------------", New Drawing.Font(tipografia, 12, FontStyle.Regular), Brushes.Black, 1, Y)
        Y += 18

        e.Graphics.DrawString("Folio: " & cboFolio.Text, fuente_r, Brushes.Black, 270, Y, derecha)
        Y += 23
        e.Graphics.DrawString("Fecha: " & Format(Date.Now, "yyyy-MM-dd"), fuente_r, Brushes.Black, 1, Y)
        e.Graphics.DrawString("Hora: " & Format(Date.Now, "HH:mm"), fuente_r, Brushes.Black, 270, Y, derecha)
        Y += 11

        If cboClIente.Text <> "" Then
            e.Graphics.DrawString("-----------------------------------------", New Drawing.Font(tipografia, 12, FontStyle.Regular), Brushes.Black, 1, Y)
            Y += 12
            e.Graphics.DrawString("C L I E N T E", New Drawing.Font(tipografia, 10, FontStyle.Bold), Brushes.Black, 140, Y, sc)
            Y += 7.5
            e.Graphics.DrawString("-----------------------------------------", New Drawing.Font(tipografia, 12, FontStyle.Regular), Brushes.Black, 1, Y)
            Y += 15

            Dim caracteresPorLinea2 As Integer = 35
            Dim texto2 As String = cboClIente.Text
            Dim inicio2 As Integer = 0
            Dim longitudTexto2 As Integer = texto2.Length

            While inicio2 < longitudTexto2
                Dim longitudBloque2 As Integer = Math.Min(caracteresPorLinea2, longitudTexto2 - inicio2)
                Dim bloque2 As String = texto2.Substring(inicio2, longitudBloque2)
                e.Graphics.DrawString(bloque2, New Font("Arial", 9, FontStyle.Regular), Brushes.Black, 1, Y)
                Y += 13
                inicio2 += caracteresPorLinea2
            End While
            Y += 5
            e.Graphics.DrawString("-----------------------------------------", New Drawing.Font(tipografia, 12, FontStyle.Regular), Brushes.Black, 1, Y)
            Y += 12
        End If

        e.Graphics.DrawString("CANT", fuente_b, Brushes.Black, 1, Y)
        e.Graphics.DrawString("DESCRIPION", fuente_b, Brushes.Black, 35, Y)
        e.Graphics.DrawString("PRECIO", fuente_b, Brushes.Black, 215, Y, derecha)
        e.Graphics.DrawString("IMPORTE", fuente_b, Brushes.Black, 280, Y, derecha)
        Y += 20

        e.Graphics.DrawString("1", fuente_p, Brushes.Black, 1, Y)

        Dim caracteresPorLinea4 As Integer = 27
        Dim texto4 As String = "Tiempo Habitación"
        Dim inicio4 As Integer = 0
        Dim longitudTexto4 As Integer = texto4.Length

        While inicio4 < longitudTexto4
            Dim longitudBloque4 As Integer = Math.Min(caracteresPorLinea4, longitudTexto4 - inicio4)
            Dim bloque4 As String = texto4.Substring(inicio4, longitudBloque4)
            e.Graphics.DrawString(bloque4, New Font("Arial", 10, FontStyle.Regular), Brushes.Black, 30, Y)
            Y += 13
            inicio4 += caracteresPorLinea4
        End While
        Y += 15
        e.Graphics.DrawString(simbolo & " " & FormatNumber(txtTotalVenta.Text, 2), fuente_p, Brushes.Black, 205, Y, derecha)
        e.Graphics.DrawString(simbolo & " " & FormatNumber(txtTotalVenta.Text, 2), fuente_p, Brushes.Black, 270, Y, derecha)
        Y += 12

        e.Graphics.DrawString("-----------------------------------------", New Drawing.Font(tipografia, 12, FontStyle.Regular), Brushes.Black, 1, Y)
        Y += 18

        e.Graphics.DrawString("Cantidad de articulos: " & "1", fuente_r, Brushes.Black, 1, Y)
        Y += 25

        e.Graphics.DrawString("SUBTOTAL: ", fuente_b, Brushes.Black, 1, Y)
        e.Graphics.DrawString(simbolo & " " & FormatNumber(txtSubtotal.Text, 2), fuente_b, Brushes.Black, 280, Y, derecha)
        Y += 20

        If txtDescuento.Text > 0 Then
            e.Graphics.DrawString("DESCUENTO: ", fuente_b, Brushes.Black, 1, Y)
            e.Graphics.DrawString(simbolo & " " & FormatNumber(txtDescuento.Text, 2), fuente_b, Brushes.Black, 280, Y, derecha)
            Y += 20
        End If

        e.Graphics.DrawString("TOTAL A PAGAR: ", fuente_b, Brushes.Black, 1, Y)
        e.Graphics.DrawString(simbolo & " " & FormatNumber(txtTotalVenta.Text, 2), fuente_b, Brushes.Black, 280, Y, derecha)
        Y += 20

        If CDec(txtEfectivo.Text) Then
            e.Graphics.DrawString("EFECTIVO", fuente_b, Brushes.Black, 1, Y)
            e.Graphics.DrawString(simbolo & " " & FormatNumber(txtEfectivo.Text, 2), fuente_b, Brushes.Black, 280, Y, derecha)
            Y += 20
        End If

        If CDec(txtTarjeta.Text) Then
            e.Graphics.DrawString("EFECTIVO", fuente_b, Brushes.Black, 1, Y)
            e.Graphics.DrawString(simbolo & " " & FormatNumber(txtTarjeta.Text, 2), fuente_b, Brushes.Black, 280, Y, derecha)
            Y += 20
        End If

        If CDec(txtTransfe.Text) Then
            e.Graphics.DrawString("EFECTIVO", fuente_b, Brushes.Black, 1, Y)
            e.Graphics.DrawString(simbolo & " " & FormatNumber(txtTransfe.Text, 2), fuente_b, Brushes.Black, 280, Y, derecha)
            Y += 20
        End If

        If CDec(txtOtro.Text) Then
            e.Graphics.DrawString("EFECTIVO", fuente_b, Brushes.Black, 1, Y)
            e.Graphics.DrawString(simbolo & " " & FormatNumber(txtOtro.Text, 2), fuente_b, Brushes.Black, 280, Y, derecha)
            Y += 20
        End If

        If CDec(txtResta.Text) <> 0 Then
            e.Graphics.DrawString("RESTA: ", fuente_b, Brushes.Black, 1, Y)
            e.Graphics.DrawString(simbolo & " " & FormatNumber(txtResta.Text, 2), fuente_b, Brushes.Black, 280, Y, derecha)
            Y += 20
        End If

        If CDec(txtCambio.Text) <> 0 Then
            e.Graphics.DrawString("CAMBIO: ", fuente_b, Brushes.Black, 1, Y)
            e.Graphics.DrawString(simbolo & " " & FormatNumber(txtCambio.Text, 2), fuente_b, Brushes.Black, 280, Y, derecha)
            Y += 10
        End If
        Y += 15

        Dim cantidadLetra As String = ""
        cantidadLetra = convLetras(txtTotalVenta.Text)

        Dim caracteresPorLinea As Integer = 37
        Dim texto As String = cantidadLetra
        Dim inicio As Integer = 0
        Dim longitudTexto As Integer = texto.Length

        While inicio < longitudTexto
            Dim longitudBloque As Integer = Math.Min(caracteresPorLinea, longitudTexto - inicio)
            Dim bloque As String = texto.Substring(inicio, longitudBloque)
            e.Graphics.DrawString(bloque, New Font("Arial", 9, FontStyle.Regular), Brushes.Black, 1, Y)
            Y += 13
            inicio += caracteresPorLinea
        End While
        Y += 5

        Dim caracteresPorLinea3 As Integer = 37
        Dim texto3 As String = Pie
        Dim inicio3 As Integer = 0
        Dim longitudTexto3 As Integer = texto3.Length

        While inicio3 < longitudTexto3
            Dim longitudBloque3 As Integer = Math.Min(caracteresPorLinea3, longitudTexto3 - inicio3)
            Dim bloque3 As String = texto3.Substring(inicio3, longitudBloque3)
            e.Graphics.DrawString(bloque3, New Font("Arial", 9, FontStyle.Regular), Brushes.Black, 1, Y)
            Y += 13
            inicio3 += caracteresPorLinea3
        End While
        Y += 5

        e.Graphics.DrawString("Lo atendio: " & lblUsuario.Text, fuente_r, Brushes.Black, 1, Y)
        Y += 20

        e.HasMorePages = False
    End Sub


End Class
