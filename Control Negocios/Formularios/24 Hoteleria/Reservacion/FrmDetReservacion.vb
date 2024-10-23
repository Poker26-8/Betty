Public Class FrmDetReservacion

    Dim varHoras As String = ""
    Dim minutosTiempoH As Double = 0
    Dim cfolio As Integer = 0
    Private Sub FrmDetReservacion_Load(sender As Object, e As EventArgs) Handles MyBase.Load



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

    Private Sub btnSalir_Click(sender As Object, e As EventArgs) Handles btnSalir.Click
        Me.Close()
        frmManejo.Show()
    End Sub

    Private Sub cboCLientes_DropDown(sender As Object, e As EventArgs) Handles cboCLientes.DropDown
        cboCLientes.Items.Clear()

        cnn5.Close() : cnn5.Open()
        cmd5 = cnn5.CreateCommand
        cmd5.CommandText = "SELECT DISTINCT Cliente FROM reservaciones WHERE Cliente<>'' AND Habitacion='" & lblHabitacion.Text & "' AND Status=0 ORDER BY Cliente"
        rd5 = cmd5.ExecuteReader
        Do While rd5.Read
            If rd5.HasRows Then
                cboCLientes.Items.Add(rd5(0).ToString)
            End If
        Loop
        rd5.Close()
        cnn5.Close()

    End Sub

    Private Sub cboFolio_DropDown(sender As Object, e As EventArgs) Handles cboFolio.DropDown
        Try
            cboFolio.Items.Clear()
            cnn5.Close() : cnn5.Open()
            cmd5 = cnn5.CreateCommand

            If cboCLientes.Text = "" Then
                cmd5.CommandText = "SELECT DISTINCT IdReservacion FROM reservaciones WHERE IdReservacion<>'' AND Status=0"
            Else
                cmd5.CommandText = "SELECT DISTINCT IdReservacion FROM reservaciones WHERE IdReservacion<>'' AND Cliente='" & cboCLientes.Text & "'AND Status=0"
            End If


            rd5 = cmd5.ExecuteReader
            If rd5.HasRows Then
                Do While rd5.Read
                    cboFolio.Items.Add(rd5(0).ToString)
                Loop
            End If
            rd5.Close()
            cnn5.Close()
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
            cnn5.Close()
        End Try
    End Sub

    Private Sub cboCLientes_SelectedValueChanged(sender As Object, e As EventArgs) Handles cboCLientes.SelectedValueChanged
        Try

            Dim precio As Double = 0
            Dim anticipo As Double = 0
            Dim resta As Double = 0

            cnn1.Close() : cnn1.Open()
            cnn2.Close() : cnn2.Open()

            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "SELECT * FROM reservaciones WHERE Cliente='" & cboCLientes.Text & "'"
            rd1 = cmd1.ExecuteReader
            If rd1.HasRows Then
                If rd1.Read Then
                    cboFolio.Text = rd1("IdReservacion").ToString
                    txttelefono.Text = rd1("Telefono").ToString


                    cnn2.Close() : cnn2.Open()
                    cmd2 = cnn2.CreateCommand
                    cmd2.CommandText = "SELECT * FROM reservaciones WHERE Habitacion='" & lblHabitacion.Text & "' AND Status=0 AND Cliente='" & cboCLientes.Text & "' AND IdReservacion=" & cboFolio.Text & ""
                    rd2 = cmd2.ExecuteReader
                    If rd2.HasRows Then
                        If rd2.Read Then

                            Dim fechaentrada As Date = rd2("FEntrada").ToString
                            Dim FECHASALIDA As Date = rd2("FSalida").ToString
                            Dim fentrada As String = Format(fechaentrada, "yyyy-MM-dd")
                            Dim horaentrada As String = Format(fechaentrada, "HH:mm:ss")
                            Dim horasalida As String = Format(FECHASALIDA, "HH:mm:ss")

                            If Date.Now > fechaentrada Then
                                pReservacion.Visible = True
                                cboCLientes.Text = rd2("Cliente").ToString
                                txttelefono.Text = rd2("Telefono").ToString
                                dtpEntrada.Value = fentrada
                                dtpSalida.Value = FECHASALIDA
                                dtphoraentrada.Text = horaentrada
                                dtphorasalida.Text = horasalida

                                varHoras = DateDiff(DateInterval.Hour, CDate(fechaentrada), FECHASALIDA)
                                cboTipo.Text = "DIA"
                                txtHoras.Text = varHoras
                                cboPrecio.Text = rd2("precio").ToString
                                precio = cboPrecio.Text
                                anticipo = rd2("Anticipo").ToString
                                resta = CDbl(precio) - CDbl(anticipo)
                                cboPrecio.Text = FormatNumber(resta, 2)
                            Else
                                MsgBox("Esta reservación aun no esta disponible", vbInformation + vbOKOnly, titulohotelriaa)
                                btnLimpiar.PerformClick()
                                cnn1.Close()
                                cnn2.Close()
                                Exit Sub
                            End If


                        End If
                    End If
                    rd1.Close()

                End If
            End If
            rd1.Close()
            cnn1.Close()

        Catch ex As Exception
            MessageBox.Show(ex.ToString)
            cnn1.Close()
        End Try
    End Sub

    Private Sub btnLimpiar_Click(sender As Object, e As EventArgs) Handles btnLimpiar.Click
        cboTipo.Text = ""
        txtHoras.Text = ""
        cboPrecio.Text = ""
        txttelefono.Text = ""
        cboCLientes.Text = ""
        cboFolio.Text = ""
        dtpEntrada.Value = Date.Now
        dtphoraentrada.Text = "00:00:00"
        dtphorasalida.Value = Date.Now
        dtphorasalida.Text = "23:59:59"
        cboCLientes.Focus.Equals(True)
    End Sub

    Private Sub cboCLientes_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cboCLientes.KeyPress
        e.KeyChar = UCase(e.KeyChar)
        If AscW(e.KeyChar) = Keys.Enter Then
            txttelefono.FOCUS.Equals(True)
        End If
    End Sub

    Private Sub txttelefono_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txttelefono.KeyPress
        If AscW(e.KeyChar) = Keys.Enter Then
            btnGuardar.Focus.Equals(True)
        End If
    End Sub

    Private Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click
        Dim ESTADO As String = ""
        ESTADO = "Ocupada"

        If lblusuario.Text = "" Then MsgBox("Ingrese la contraseña de usuario", vbInformation + vbOKOnly, titulohotelriaa) : txtcontra.Focus.Equals(True) : Exit Sub

        If txtHoras.Text = "" Then MsgBox("Debe de ingresar la cantidad de horas", vbInformation + vbOKOnly, titulohotelriaa) : txtHoras.Focus.Equals(True) : Exit Sub

        If cboCLientes.Text = "" Then MsgBox("Debe seleccionar el cliente para asignar la habitación", vbInformation + vbOKOnly, titulohotelriaa)

        If ESTADO = "Ocupada" Then
            Try
                If minutosTiempoH = 0 Then
                    HrTiempo = Format(Date.Now, "HH:mm:ss")
                    HrEntrega = Format(Date.Now, "HH:mm:ss")
                ElseIf minutosTiempoH > 0 Then
                    HrTiempo = Format(Date.Now, "HH:mm:ss")
                    'HrEntrega = Format(DateAdd("n", minutosTiempo, Date.Now), "HH:mm:ss")
                End If

                Dim tolerancia As Double = 0
                Dim fechaentrada As Date = Nothing
                Dim salida As String = ""

                Dim PRECIO As Double = 0
                PRECIO = cboPrecio.Text

                fechaentrada = Format(Date.Now, "yyyy-MM-dd HH:mm:ss")

                cnn1.Close() : cnn1.Open()
                cmd1.CommandText = "SELECT NotasCred FROM formatos WHERE Facturas='ToleHabi'"
                rd1 = cmd1.ExecuteReader
                If rd1.HasRows Then
                    If rd1.Read Then
                        tolerancia = IIf(rd1(0).ToString = "", 0, rd1(0).ToString)
                    End If
                End If
                rd1.Close()

                Dim fechasalida As DateTime = fechaentrada.AddHours(txtHoras.Text)

                If tolerancia > 0 Then
                    Dim fechatolerancia As DateTime = fechasalida.AddMinutes(tolerancia)
                    salida = Format(fechatolerancia, "yyyy-MM-dd HH:mm:ss")
                Else
                    salida = Format(fechasalida, "yyyy-MM-dd HH:mm:ss")
                End If


                cmd1 = cnn1.CreateCommand
                cmd1.CommandText = "INSERT INTO AsigPC(Nombre,Tipo,HorEnt,HorSal,Fecha,Ocupada,FechaSal) VALUES('" & lblHabitacion.Text & "','Habitacion','" & Format(Date.Now, "yyyy-MM-dd HH:mm:ss") & "','','" & Format(Date.Now, "yyyy/MM/dd HH:mm:ss") & "',1,'" & salida & "')"
                cmd1.ExecuteNonQuery()

                cnn2.Close() : cnn2.Open()
                cmd2 = cnn2.CreateCommand
                cmd2.CommandText = "SELECT MAX(Id) FROM Comanda1"
                rd2 = cmd2.ExecuteReader
                If rd2.HasRows Then
                    If rd2.Read Then
                        cfolio = CDbl(IIf(rd2(0).ToString = "", "0", rd2(0).ToString)) + 1
                    Else
                        cfolio = 1
                    End If
                Else
                    cfolio = 1
                End If
                rd2.Close()
                cnn2.Close()

                cmd1 = cnn1.CreateCommand
                cmd1.CommandText = "INSERT INTO Comanda1(IdCliente,Nombre,Direccion,Usuario,FVenta,HVenta,FPago,FCancelado,Status,Comisionista,TComensales) VALUES(" & IIf(lblidcliented.Text = "", "0", lblidcliented.Text) & ",'" & lblHabitacion.Text & "','','" & lblusuario.Text & "','" & Format(Date.Now, "yyyyy-MM-dd") & "','" & Format(Date.Now, "yyyyy-MM-dd HH:mm:ss") & "','','','','',0)"
                cmd1.ExecuteNonQuery()
                cnn1.Close()

                cnn1.Close() : cnn1.Open()
                cmd1 = cnn1.CreateCommand
                cmd1.CommandText = "SELECT Id FROM detallehotel WHERE Habitacion='" & lblHabitacion.Text & "'"
                rd1 = cmd1.ExecuteReader
                If rd1.HasRows Then
                    If rd1.Read Then
                    End If
                Else

                    cnn2.Close() : cnn2.Open()
                    cmd2 = cnn2.CreateCommand
                    cmd2.CommandText = "INSERT INTO detallehotel(Habitacion,Tipo,Estado,Horas,Precio,Cliente,Telefono,FEntrada,FSalida,Caracteristicas) VALUES('" & lblHabitacion.Text & "','" & lblTipo.Text & "','" & ESTADO & "'," & txtHoras.Text & "," & PRECIO & ",'" & cboCLientes.Text & "','" & txttelefono.Text & "','" & Format(dtpEntrada.Value, "yyyy/MM/dd HH:mm:ss") & "','" & Format(dtpSalida.Value, "yyyy/MM/dd") & "','" & lblCaracteristicas.Text & "')"
                    cmd2.ExecuteNonQuery()
                    cnn2.Close()

                    cnn2.Close() : cnn2.Open()
                    cmd2 = cnn2.CreateCommand
                    cmd2.CommandText = "INSERT INTO comandas(Id,NMESA,Codigo,Nombre,Cantidad,UVenta,CostVR,CostVP,CostVUE,Descuento,Precio,Total,PrecioSinIva,TotalSinIVA,Comisionista,Fecha,Comensal,Status,Comentario,GPrint,CUsuario,Total_comensales,Depto,Grupo,EstatusT,Hr,EntregaT) VALUES(" & cfolio & ",'" & lblHabitacion.Text & "','xc3','Tiempo Habitacion',1,'SER',0,0,0,0," & PRECIO & "," & PRECIO & "," & PRECIO & "," & PRECIO & ",'0','" & Format(Date.Now, "yyyy/MM/dd") & "',0,'RESTA','Renta de Habitacion','','" & lblusuario.Text & "',0,'HABITACION','HABITACION',0,'" & HrTiempo & "','" & HrEntrega & "')"
                    cmd2.ExecuteNonQuery()

                    cmd2 = cnn2.CreateCommand
                    cmd2.CommandText = "INSERT INTO rep_comandas(Id,NMESA,Codigo,Nombre,Cantidad,UVenta,CostVR,CostVP,CostVUE,Precio,Total,PrecioSinIVA,TotalSinIVA,Comisionista,Fecha,Comensal,Status,Comentario,GPrint,CUsuario,Total_comensales,Depto,Grupo,EstatusT,Hr,EntregaT) VALUES(" & cfolio & ",'" & lblHabitacion.Text & "','xc3','Tiempo Habitacion',1,'SER',0,'0',0," & PRECIO & "," & PRECIO & "," & PRECIO & "," & PRECIO & ",0,'" & Format(Date.Now, "yyyy/MM/dd") & "',0,'RESTA','xc3 ','','" & lblusuario.Text & "',0,'HABITACION','HABITACION',0,'" & HrTiempo & "','" & HrEntrega & "')"
                    cmd2.ExecuteNonQuery()
                    cnn2.Close()

                End If
                rd1.Close()
                cnn1.Close()

                cnn2.Close() : cnn2.Open()
                cmd2 = cnn2.CreateCommand
                cmd2.CommandText = "UPDATE habitacion SET Estado='" & ESTADO & "' WHERE N_Habitacion='" & lblHabitacion.Text & "'"
                cmd2.ExecuteNonQuery()

                cmd2 = cnn2.CreateCommand
                cmd2.CommandText = "UPDATE detallehotelprecios SET Horas=" & txtHoras.Text & ",PrecioA=" & PRECIO & " WHERE Nombre='" & cboTipo.Text & "'"
                cmd2.ExecuteNonQuery()

                cmd2 = cnn2.CreateCommand
                cmd2.CommandText = "UPDATE reservaciones SET Status=1,Reservo='" & lblusuario.Text & "' WHERE Habitacion='" & lblHabitacion.Text & "' AND Cliente='" & cboCLientes.Text & "'and IdReservacion=" & cboFolio.Text & ""
                cmd2.ExecuteNonQuery()
                cnn2.Close()

                MsgBox("La habitacion " & lblHabitacion.Text & " fue asignada correctamente", vbInformation + vbOKOnly, titulohotelriaa)

                frmPagarH.lblHabitacion.Text = lblHabitacion.Text
                ' frmPagarH.txtTotal.Text = cboPrecio.Text
                frmPagarH.lblAtendio.Text = lblusuario.Text
                frmPagarH.lblNumCliente.Text = lblidcliented.Text
                frmPagarH.lblCliente.Text = cboCLientes.Text
                frmPagarH.focoh = 1
                frmPagarH.Show()


                btnLimpiar.PerformClick()
                Me.Close()

            Catch ex As Exception
                MessageBox.Show(ex.ToString)
                cnn1.Close()
            End Try
        End If
    End Sub

    Private Sub txtcontra_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtcontra.KeyPress
        If AscW(e.KeyChar) = Keys.Enter Then
            Try
                cnn1.Close() : cnn1.Open()
                cmd1 = cnn1.CreateCommand
                cmd1.CommandText = "SELECT Alias,Status,IdEmpleado FROM Usuarios WHERE Clave='" & txtcontra.Text & "'"
                rd1 = cmd1.ExecuteReader
                If rd1.HasRows Then
                    If rd1.Read Then

                        If rd1(1).ToString = 1 Then
                            lblusuario.Text = rd1(0).ToString
                        Else
                            MsgBox("El usuario no esta activo por favor contacte a su administrador", vbInformation + vbOKOnly, titulohotelriaa)
                            txtcontra.Focus.Equals(True)
                            Exit Sub
                        End If

                    End If
                Else
                    MsgBox("Contraseña incorrecta.", vbInformation + vbOKOnly, titulohotelriaa)
                    txtcontra.Focus.Equals(True)
                    Exit Sub
                End If
                rd1.Close()
                cnn1.Close()
                cnn2.Close()

                btnGuardar.Focus.Equals(True)

            Catch ex As Exception
                MessageBox.Show(ex.ToString)
                cnn1.Close()
            End Try

        End If
    End Sub

    Private Sub cboFolio_SelectedValueChanged(sender As Object, e As EventArgs) Handles cboFolio.SelectedValueChanged
        Try
            cnn1.Close() : cnn1.Open()
            cnn2.Close() : cnn2.Open()

            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "SELECT * FROM reservaciones WHERE IdCliente=" & cboFolio.Text
            rd1 = cmd1.ExecuteReader
            If rd1.HasRows Then
                If rd1.Read Then
                    cboFolio.Text = rd1("IdReservacion").ToString
                    txttelefono.Text = rd1("Telefono").ToString


                    cnn2.Close() : cnn2.Open()
                    cmd2 = cnn2.CreateCommand
                    cmd2.CommandText = "SELECT * FROM reservaciones WHERE Habitacion='" & lblHabitacion.Text & "' AND Status=0 AND Cliente='" & cboCLientes.Text & "' AND IdReservacion=" & cboFolio.Text & ""
                    rd2 = cmd2.ExecuteReader
                    If rd2.HasRows Then
                        If rd2.Read Then

                            Dim fechaentrada As Date = rd2("FEntrada").ToString
                            Dim FECHASALIDA As Date = rd2("FSalida").ToString
                            Dim fentrada As String = Format(fechaentrada, "yyyy-MM-dd")
                            Dim horaentrada As String = Format(fechaentrada, "HH:mm:ss")
                            Dim horasalida As String = Format(FECHASALIDA, "HH:mm:ss")

                            If Date.Now > fechaentrada Then
                                pReservacion.Visible = True
                                cboCLientes.Text = rd2("Cliente").ToString
                                txttelefono.Text = rd2("Telefono").ToString
                                dtpEntrada.Value = fentrada
                                dtpSalida.Value = FECHASALIDA
                                dtphoraentrada.Text = horaentrada
                                dtphorasalida.Text = horasalida

                                varHoras = DateDiff(DateInterval.Hour, CDate(fechaentrada), FECHASALIDA)
                                cboTipo.Text = "DIA"
                                txtHoras.Text = varHoras
                            Else
                                MsgBox("Esta reservación aun no esta disponible", vbInformation + vbOKOnly, titulohotelriaa)
                                btnLimpiar.PerformClick()
                                cnn1.Close()
                                cnn2.Close()
                                Exit Sub
                            End If


                        End If
                    End If
                    rd1.Close()

                End If
            End If
            rd1.Close()
            cnn1.Close()

        Catch ex As Exception
            MessageBox.Show(ex.ToString)
            cnn1.Close()
        End Try
    End Sub
End Class