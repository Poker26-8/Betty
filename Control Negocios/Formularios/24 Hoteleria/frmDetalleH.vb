
Imports System.IO
Public Class frmDetalleH

    Public habitacionn As String = ""
    Dim minutosTiempoH As Double = 0
    Dim cfolio As Integer = 0

    Dim tLogo As String = ""
    Dim nLogo As String = ""
    Dim IDRESERVACION As Integer = 0

    Private Sub frmDetalleH_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        tLogo = DatosRecarga("TipoLogo")
        nLogo = DatosRecarga("LogoG")
        Try
            cnn1.Close() : cnn1.Open()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "SELECT Horas,PrecioH,PreDia FROM habitacion WHERE N_Habitacion='" & lblhabitacion.Text & "'"
            rd1 = cmd1.ExecuteReader
            If rd1.HasRows Then
                If rd1.Read Then
                    grdPrecios.Rows.Add(rd1(0).ToString, rd1(1).ToString, rd1(2).ToString)
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
        If lblEstado.Text = "Ocupada" Or lblEstado.Text = "Reservacion" Then
        Else
            cbocliente.Text = ""
            dtpEntrada.Value = Date.Now
            dtpSalida.Value = Date.Now
            txttelefono.Text = ""
            lblidcliented.Text = ""
            lblPrecio.Text = ""
            lblHoras.Text = ""
        End If
    End Sub

    Private Sub btnSalir_Click(sender As Object, e As EventArgs) Handles btnSalir.Click
        Me.Close()
        frmManejo.Show()
    End Sub

    Private Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click

        Dim ESTADO As String = ""

        If cboRegistro.Text = "HOSPEDAR" Then
            ESTADO = "Ocupada"
        ElseIf cboRegistro.Text = "RESERVACION" Then
            ESTADO = "Reservacion"
        ElseIf cboRegistro.Text = "MANTENIMIENTO" Then
            ESTADO = "Mantenimiento"
        ElseIf cboRegistro.Text = "LIMPIEZA" Then
            ESTADO = "Limpieza"
        ElseIf cboRegistro.Text = "VENTILACION" Then
            ESTADO = "Ventilacion"
        End If



        If lblusuario.Text = "" Then MsgBox("Ingrese la contraseña de usuario", vbInformation + vbOKOnly, titulohotelriaa) : txtcontra.Focus.Equals(True) : Exit Sub

        If ESTADO = "Ocupada" Then

            If txtHoras.Text = "" Then MsgBox("Debe de ingresar la cantidad de horas", vbInformation + vbOKOnly, titulohotelriaa) : txtHoras.Focus.Equals(True) : Exit Sub

            Try

                If cbocliente.Text = "" Then MsgBox("Debe seleccionar el cliente para asignar la habitación", vbInformation + vbOKOnly, titulohotelriaa)
                If cbocliente.Text <> "" Then

                    cnn1.Close() : cnn1.Open()
                    cmd1 = cnn1.CreateCommand
                    cmd1.CommandText = "SELECT Id FROM Clientes WHERE Nombre='" & cbocliente.Text & "'"
                    rd1 = cmd1.ExecuteReader
                    If rd1.HasRows Then
                        If rd1.Read Then

                        End If
                    Else
                        cnn2.Close() : cnn2.Open()
                        cmd2 = cnn2.CreateCommand
                        cmd2.CommandText = "INSERT INTO clientes(Nombre,RazonSocial,Telefono) VALUES('" & cbocliente.Text & "','" & cbocliente.Text & "','" & txttelefono.Text & "')"
                        cmd2.ExecuteNonQuery()
                        cnn2.Close()
                    End If
                    rd1.Close()
                    cnn1.Close()

                End If

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
                cmd1.CommandText = "INSERT INTO AsigPC(Nombre,Tipo,HorEnt,HorSal,Fecha,Ocupada,FechaSal) VALUES('" & lblhabitacion.Text & "','Habitacion','" & Format(Date.Now, "yyyy-MM-dd HH:mm:ss") & "','','" & Format(Date.Now, "yyyy/MM/dd HH:mm:ss") & "',1,'" & salida & "')"
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
                cmd1.CommandText = "INSERT INTO Comanda1(IdCliente,Nombre,Direccion,Usuario,FVenta,HVenta,FPago,FCancelado,Status,Comisionista,TComensales) VALUES(" & IIf(lblidcliented.Text = "", "0", lblidcliented.Text) & ",'" & lblhabitacion.Text & "','','" & lblusuario.Text & "','" & Format(Date.Now, "yyyyy-MM-dd") & "','" & Format(Date.Now, "yyyyy-MM-dd HH:mm:ss") & "','','','','',0)"
                cmd1.ExecuteNonQuery()
                cnn1.Close()

                cnn1.Close() : cnn1.Open()
                cmd1 = cnn1.CreateCommand
                cmd1.CommandText = "SELECT Id FROM detallehotel WHERE Habitacion='" & lblhabitacion.Text & "'"
                rd1 = cmd1.ExecuteReader
                If rd1.HasRows Then
                    If rd1.Read Then
                    End If
                Else

                    cnn2.Close() : cnn2.Open()
                    cmd2 = cnn2.CreateCommand
                    cmd2.CommandText = "INSERT INTO detallehotel(Habitacion,Tipo,Estado,Horas,Precio,Cliente,Telefono,FEntrada,FSalida,Caracteristicas) VALUES('" & lblhabitacion.Text & "','" & lbltipo.Text & "','" & ESTADO & "'," & txtHoras.Text & "," & cboPrecio.Text & ",'" & cbocliente.Text & "','" & txttelefono.Text & "','" & Format(dtpEntrada.Value, "yyyy/MM/dd HH:mm:ss") & "','" & Format(dtpSalida.Value, "yyyy/MM/dd") & "','" & lblCaracteristicas.Text & "')"
                    cmd2.ExecuteNonQuery()
                    cnn2.Close()

                    cnn2.Close() : cnn2.Open()
                    cmd2 = cnn2.CreateCommand
                    cmd2.CommandText = "INSERT INTO comandas(Id,NMESA,Codigo,Nombre,Cantidad,UVenta,CostVR,CostVP,CostVUE,Descuento,Precio,Total,PrecioSinIva,TotalSinIVA,Comisionista,Fecha,Comensal,Status,Comentario,GPrint,CUsuario,Total_comensales,Depto,Grupo,EstatusT,Hr,EntregaT) VALUES(" & cfolio & ",'" & lblhabitacion.Text & "','xc3','Tiempo Habitacion',1,'SER',0,0,0,0," & cboPrecio.Text & "," & cboPrecio.Text & "," & cboPrecio.Text & "," & cboPrecio.Text & ",'0','" & Format(Date.Now, "yyyy/MM/dd") & "',0,'RESTA','Renta de Habitacion','','" & lblusuario.Text & "',0,'HABITACION','HABITACION',0,'" & HrTiempo & "','" & HrEntrega & "')"
                    cmd2.ExecuteNonQuery()

                    cmd2 = cnn2.CreateCommand
                    cmd2.CommandText = "INSERT INTO rep_comandas(Id,NMESA,Codigo,Nombre,Cantidad,UVenta,CostVR,CostVP,CostVUE,Precio,Total,PrecioSinIVA,TotalSinIVA,Comisionista,Fecha,Comensal,Status,Comentario,GPrint,CUsuario,Total_comensales,Depto,Grupo,EstatusT,Hr,EntregaT) VALUES(" & cfolio & ",'" & lblhabitacion.Text & "','xc3','Tiempo Habitacion',1,'SER',0,'0',0," & cboPrecio.Text & "," & cboPrecio.Text & "," & cboPrecio.Text & "," & cboPrecio.Text & ",0,'" & Format(Date.Now, "yyyy/MM/dd") & "',0,'RESTA','xc3 ','','" & lblusuario.Text & "',0,'HABITACION','HABITACION',0,'" & HrTiempo & "','" & HrEntrega & "')"
                    cmd2.ExecuteNonQuery()
                    cnn2.Close()

                End If
                rd1.Close()
                cnn1.Close()

                cnn2.Close() : cnn2.Open()
                cmd2 = cnn2.CreateCommand
                cmd2.CommandText = "UPDATE habitacion SET Estado='" & ESTADO & "' WHERE N_Habitacion='" & lblhabitacion.Text & "'"
                cmd2.ExecuteNonQuery()

                cmd2 = cnn2.CreateCommand
                cmd2.CommandText = "UPDATE detallehotelprecios SET Horas=" & txtHoras.Text & ",PrecioA=" & cboPrecio.Text & " WHERE Nombre='" & cboTipo.Text & "'"
                cmd2.ExecuteNonQuery()
                cnn2.Close()

                MsgBox("La habitacion " & lblhabitacion.Text & " fue asignada correctamente", vbInformation + vbOKOnly, titulohotelriaa)

                frmPagarH.lblHabitacion.Text = lblhabitacion.Text
                frmPagarH.txtTotal.Text = lblPrecio.Text
                frmPagarH.lblAtendio.Text = lblusuario.Text
                frmPagarH.lblNumCliente.Text = lblidcliented.Text
                frmPagarH.lblCliente.Text = cbocliente.Text
                frmPagarH.focoh = 1
                frmPagarH.Show()

                btnLimpiar.PerformClick()
                Me.Close()

            Catch ex As Exception
                MessageBox.Show(ex.ToString)
                cnn1.Close()
            End Try

        End If

        If ESTADO = "Reservacion" Then

            If cbocliente.Text = "" Then MsgBox("Debe seleccionar un cliente", vbInformation + vbOKOnly, titulohotelriaa) : cbocliente.Focus.Equals(True) : Exit Sub

            Try
                Dim fechacentrada As String = ""
                Dim fechaentrada As Date = dtpEntrada.Value
                Dim horaentrada As Date = dtphoraentrada.Value

                Dim fentrada As String = Format(fechaentrada, "yyyy-MM-dd")
                Dim hentrada As String = Format(horaentrada, "HH:mm:ss")

                fechacentrada = fentrada & " " & hentrada

                Dim fechacsalida As String = ""
                Dim fechasalida As Date = dtpSalida.Value
                Dim horasalida As Date = dtphorasalida.Value

                Dim fsalida As String = Format(fechasalida, "yyyy-MM-dd")
                Dim hsalida As String = Format(horasalida, "HH:mm:ss")

                fechacsalida = fsalida & " " & hsalida


                cnn1.Close() : cnn1.Open()
                cmd1 = cnn1.CreateCommand
                cmd1.CommandText = "INSERT INTO Reservaciones(Cliente,Telefono,Habitacion,FEntrada,FSalida,Asigno) VALUES('" & cbocliente.Text & "','" & txttelefono.Text & "','" & lblhabitacion.Text & "','" & fechacentrada & "','" & fechacsalida & "','" & lblusuario.Text & "')"
                If cmd1.ExecuteNonQuery() Then
                    MsgBox("Habitación reservada correctamente", vbInformation + vbOKOnly, titulohotelriaa)
                End If
                cnn1.Close()

                cnn2.Close() : cnn2.Open()
                cmd2 = cnn2.CreateCommand
                cmd2.CommandText = "SELECT MAX(IdReservacion) FROM reservaciones"
                rd2 = cmd2.ExecuteReader
                If rd2.HasRows Then
                    If rd2.Read Then
                        IDRESERVACION = IIf(rd2(0).ToString = "", 0, rd2(0).ToString)
                    End If
                Else
                    IDRESERVACION = "1"
                End If
                rd2.Close()
                cnn2.Close()

                Dim tamim As Integer = TamImpre()
                Dim impresora As String = ImpresoraImprimir()

                If tamim = "80" Then
                    PReservacion80.DefaultPageSettings.PrinterSettings.PrinterName = impresora
                    PReservacion80.Print()
                End If

                If tamim = "58" Then
                    PRservacion58.DefaultPageSettings.PrinterSettings.PrinterName = impresora
                    PRservacion58.Print()
                End If

                btnLimpiar.PerformClick()
                Me.Close()
                frmManejo.Show()
                frmManejo.pUbicaciones.Controls.Clear()
                frmManejo.TRAERUBICACION()
            Catch ex As Exception
                MessageBox.Show(ex.ToString)
                cnn1.Close()
            End Try

        End If

        If ESTADO = "Mantenimiento" Then


            cnn1.Close() : cnn1.Open()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "UPDATE Habitacion SET Estado='Mantenimiento' WHERE N_Habitacion='" & lblhabitacion.Text & "'"
            cmd1.ExecuteNonQuery()
            cnn1.Close()

            MsgBox("La habitacion " & lblhabitacion.Text & " fue asignada correctamente", vbInformation + vbOKOnly, titulohotelriaa)
            btnLimpiar.PerformClick()
            Me.Close()
            frmManejo.Show()
            frmManejo.pUbicaciones.Controls.Clear()
            frmManejo.TRAERUBICACION()
        End If

        If ESTADO = "Limpieza" Then
            cnn1.Close() : cnn1.Open()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "UPDATE habitacion SET Estado='Limpieza' WHERE N_Habitacion='" & lblhabitacion.Text & "'"
            cmd1.ExecuteNonQuery()
            cnn1.Close()

            MsgBox("La habitacion " & lblhabitacion.Text & " fue asignada correctamente", vbInformation + vbOKOnly, titulohotelriaa)
            btnLimpiar.PerformClick()
            Me.Close()
            frmManejo.Show()
            frmManejo.pUbicaciones.Controls.Clear()
            frmManejo.TRAERUBICACION()
        End If

        If ESTADO = "Ventilacion" Then
            cnn1.Close() : cnn1.Open()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "UPDATE habitacion SET Estado='Ventilacion' WHERE N_Habitacion='" & lblhabitacion.Text & "'"
            cmd1.ExecuteNonQuery()
            cnn1.Close()

            MsgBox("La habitacion " & lblhabitacion.Text & " fue asignada correctamente", vbInformation + vbOKOnly, titulohotelriaa)
            btnLimpiar.PerformClick()
            Me.Close()
            frmManejo.Show()
            frmManejo.pUbicaciones.Controls.Clear()
            frmManejo.TRAERUBICACION()
        End If


    End Sub

    Private Sub cbocliente_DropDown(sender As Object, e As EventArgs) Handles cbocliente.DropDown

        Try
            cbocliente.Items.Clear()
            cnn5.Close() : cnn5.Open()
            cmd5 = cnn5.CreateCommand
            cmd5.CommandText = "SELECT DISTINCT Nombre FROM clientes WHERE Nombre<>'' ORDER BY Nombre"
            rd5 = cmd5.ExecuteReader
            Do While rd5.Read
                If rd5.HasRows Then
                    cbocliente.Items.Add(rd5(0).ToString)
                End If
            Loop
            rd5.Close()
            cnn5.Close()
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
            cnn5.Close()
        End Try

    End Sub

    Private Sub cbocliente_SelectedValueChanged(sender As Object, e As EventArgs) Handles cbocliente.SelectedValueChanged

        Try
            cnn1.Close() : cnn1.Open()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "SELECT Telefono,Id FROM clientes WHERE Nombre='" & cbocliente.Text & "'"
            rd1 = cmd1.ExecuteReader
            If rd1.HasRows Then
                If rd1.Read Then
                    txttelefono.Text = rd1(0).ToString
                    lblidcliented.Text = rd1(1).ToString
                End If
            End If
            rd1.Close()
            cnn1.Close()


        Catch ex As Exception
            MessageBox.Show(ex.ToString)
            cnn1.Close()
        End Try

    End Sub

    Private Sub cbocliente_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cbocliente.KeyPress
        e.KeyChar = UCase(e.KeyChar)
        If AscW(e.KeyChar) = Keys.Enter Then
            txttelefono.Focus.Equals(True)
        End If
    End Sub

    Private Sub cboRegistro_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cboRegistro.KeyPress
        e.KeyChar = UCase(e.KeyChar)
        If AscW(e.KeyChar) = Keys.Enter Then
            dtpEntrada.Focus.Equals(True)
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

                            cnn2.Close() : cnn2.Open()
                            cmd2 = cnn2.CreateCommand
                            cmd2.CommandText = "SELECT Desocupar FROM Permisos WHERE IdEmpleado=" & rd1(2).ToString
                            rd2 = cmd2.ExecuteReader
                            If rd2.HasRows Then
                                If rd2.Read Then
                                    If rd2(0).ToString = 1 And lblEstado.Text <> "Desocupada" Then
                                        pdesocupar.Visible = True
                                    Else
                                        pdesocupar.Visible = False
                                    End If
                                End If
                            End If
                            rd2.Close()

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

    Private Sub txttelefono_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txttelefono.KeyPress
        If AscW(e.KeyChar) = Keys.Enter Then
            cboRegistro.Focus.Equals(True)
        End If
    End Sub

    Private Sub dtpEntrada_KeyPress(sender As Object, e As KeyPressEventArgs) Handles dtpEntrada.KeyPress
        If AscW(e.KeyChar) = Keys.Enter Then
            dtpSalida.Focus.Equals(True)
        End If
    End Sub

    Private Sub btnDesocupar_Click(sender As Object, e As EventArgs) Handles btnDesocupar.Click
        Try

            If MsgBox("¿Desea realizar este cambio?", vbQuestion + vbYesNo + vbDefaultButton1, titulohotelriaa) = vbNo Then
                Exit Sub
            End If

            cnn2.Close() : cnn2.Open()
            cmd2 = cnn2.CreateCommand
            cmd2.CommandText = "SELECT Id FROM detallehotel WHERE Habitacion='" & lblhabitacion.Text & "'"
            rd2 = cmd2.ExecuteReader
            If rd2.HasRows Then
                If rd2.Read Then

                    cnn1.Close() : cnn1.Open()
                    cmd1 = cnn1.CreateCommand
                    cmd1.CommandText = "DELETE FROM detallehotel WHERE Habitacion='" & lblhabitacion.Text & "'"
                    cmd1.ExecuteNonQuery()

                    cmd1 = cnn1.CreateCommand
                    cmd1.CommandText = "UPDATE habitacion SET Estado='Desocupada' WHERE N_Habitacion='" & lblhabitacion.Text & "'"
                    If cmd1.ExecuteNonQuery Then
                        MsgBox("La habitacion esta lista para su uso", vbInformation + vbOKOnly, titulohotelriaa)
                    End If
                    cnn1.Close()

                End If
            Else
                cnn1.Close() : cnn1.Open()
                cmd1 = cnn1.CreateCommand
                cmd1.CommandText = "UPDATE habitacion SET Estado='Desocupada' WHERE N_Habitacion='" & lblhabitacion.Text & "'"
                If cmd1.ExecuteNonQuery Then
                    MsgBox("La habitacion esta lista para su uso", vbInformation + vbOKOnly, titulohotelriaa)
                End If
                cnn1.Close()
            End If
            rd2.Close()
            cnn2.Close()

            btnLimpiar.PerformClick()
            Me.Close()
            frmManejo.Show()
            frmManejo.pUbicaciones.Controls.Clear()
            frmManejo.TRAERUBICACION()
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
            cnn1.Close()
        End Try
    End Sub

    Private Sub dtpSalida_KeyPress(sender As Object, e As KeyPressEventArgs) Handles dtpSalida.KeyPress
        If AscW(e.KeyChar) = Keys.Enter Then
            btnGuardar.Focus.Equals(True)
        End If
    End Sub

    Private Sub grdPrecios_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles grdPrecios.CellClick

        Dim celda As DataGridViewCellEventArgs = e
        Dim index As Integer = grdPrecios.CurrentRow.Index
        Dim preciosel As Double = 0
        Dim hrs As Integer = 0
        If celda.ColumnIndex = 1 Then
            hrs = grdPrecios.Rows(index).Cells(0).Value.ToString
            preciosel = grdPrecios.Rows(index).Cells(1).Value.ToString
            lblPrecio.Text = preciosel
            lblHoras.Text = hrs
        End If

        If celda.ColumnIndex = 2 Then
            preciosel = grdPrecios.Rows(index).Cells(2).Value.ToString
            lblPrecio.Text = preciosel
            lblHoras.Text = "24"
        End If

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

    Private Sub cboTipo_SelectedValueChanged(sender As Object, e As EventArgs) Handles cboTipo.SelectedValueChanged
        Try
            cnn1.Close() : cnn1.Open()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "SELECT Horas FROM detallehotelprecios WHERE Nombre='" & cboTipo.Text & "'"
            rd1 = cmd1.ExecuteReader
            If rd1.HasRows Then
                If rd1.Read Then
                    txtHoras.Text = rd1(0).ToString
                End If
            End If
            rd1.Close()
            cnn1.Close()

        Catch ex As Exception
            MessageBox.Show(ex.ToString)
            cnn1.Close()
        End Try
    End Sub

    Private Sub cboTipo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cboTipo.KeyPress
        e.KeyChar = UCase(e.KeyChar)
        If AscW(e.KeyChar) = Keys.Enter Then
            txtHoras.Focus.Equals(True)
        End If
    End Sub

    Private Sub txtHoras_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtHoras.KeyPress
        e.KeyChar = UCase(e.KeyChar)
        If AscW(e.KeyChar) = Keys.Enter Then
            cboPrecio.Focus.Equals(True)
        End If
    End Sub

    Private Sub cboRegistro_SelectedValueChanged(sender As Object, e As EventArgs) Handles cboRegistro.SelectedValueChanged
        If cboRegistro.Text = "RESERVACION" Then
            pReservacion.Visible = True
        Else
            pReservacion.Visible = False
        End If
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

        Try

            If tLogo <> "SIN" Then
                If File.Exists(My.Application.Info.DirectoryPath & "\" & nLogo) Then
                    Logotipo = Drawing.Image.FromFile(My.Application.Info.DirectoryPath & "\" & nLogo)

                    If tLogo = "CUAD" Then
                        e.Graphics.DrawImage(Logotipo, 80, 0, 120, 120)
                        Y += 130
                    End If
                    If tLogo = "RECT" Then
                        e.Graphics.DrawImage(Logotipo, 30, 0, 240, 110)
                        Y += 120
                    End If
                End If

            Else
                Y = 0
            End If
            cnn2.Close() : cnn2.Open()
            cmd2 = cnn2.CreateCommand
            cmd2.CommandText =
                "select Pie1,Cab0,Cab1,Cab2,Cab3,Cab4,Cab5,Cab6 from Ticket"
            rd2 = cmd2.ExecuteReader
            If rd2.HasRows Then
                If rd2.Read Then
                    Pie = rd2("Pie1").ToString
                    'Razón social
                    If rd2("Cab0").ToString() <> "" Then
                        e.Graphics.DrawString(rd2("Cab0").ToString, New Drawing.Font(tipografia, 8, FontStyle.Bold), Brushes.Black, 140, Y, sc)
                        Y += 12.5
                    End If
                    'RFC
                    If rd2("Cab1").ToString() <> "" Then
                        e.Graphics.DrawString(rd2("Cab1").ToString, New Drawing.Font(tipografia, 8, FontStyle.Bold), Brushes.Black, 140, Y, sc)
                        Y += 12.5
                    End If
                    'Calle  N°.
                    If rd2("Cab2").ToString() <> "" Then
                        e.Graphics.DrawString(rd2("Cab2").ToString, New Drawing.Font(tipografia, 8, FontStyle.Regular), Brushes.Gray, 140, Y, sc)
                        Y += 12
                    End If
                    'Colonia
                    If rd2("Cab3").ToString() <> "" Then
                        e.Graphics.DrawString(rd2("Cab3").ToString, New Drawing.Font(tipografia, 8, FontStyle.Regular), Brushes.Gray, 140, Y, sc)
                        Y += 12
                    End If
                    'Delegación / Municipio - Entidad
                    If rd2("Cab4").ToString() <> "" Then
                        e.Graphics.DrawString(rd2("Cab4").ToString, New Drawing.Font(tipografia, 8, FontStyle.Regular), Brushes.Gray, 140, Y, sc)
                        Y += 12
                    End If
                    'Teléfono
                    If rd2("Cab5").ToString() <> "" Then
                        e.Graphics.DrawString(rd2("Cab5").ToString, New Drawing.Font(tipografia, 8, FontStyle.Regular), Brushes.Gray, 140, Y, sc)
                        Y += 12
                    End If
                    'Correo
                    If rd2("Cab6").ToString() <> "" Then
                        e.Graphics.DrawString(rd2("Cab6").ToString, New Drawing.Font(tipografia, 8, FontStyle.Regular), Brushes.Gray, 140, Y, sc)
                        Y += 12
                    End If
                    Y += 5
                End If
            Else
                Y += 0
            End If
            rd2.Close()
            cnn2.Close()

            e.Graphics.DrawString("----------------------------------------------------------------------------", fuente_b, Brushes.Black, 1, Y)
            Y += 11
            e.Graphics.DrawString("R E S E R V A C I Ó N", fuente_b, Brushes.Black, 135, Y, sc)
            Y += 11
            e.Graphics.DrawString("----------------------------------------------------------------------------", fuente_b, Brushes.Black, 1, Y)
            Y += 15
            e.Graphics.DrawString("Habitación: " & lblhabitacion.Text, New Font("Arial", 12, FontStyle.Regular), Brushes.Black, 1, Y)

            e.Graphics.DrawString("Folio: " & IDRESERVACION, fuente_r, Brushes.Black, 270, Y, derecha)
            Y += 23
            e.Graphics.DrawString("Fecha Entrada: ", fuente_r, Brushes.Black, 1, Y)
            e.Graphics.DrawString(Format(dtpEntrada.Value, "yyyy-MM-dd") & " " & dtphoraentrada.Text, fuente_r, Brushes.Black, 270, Y, derecha)
            Y += 11
            e.Graphics.DrawString("Fecha Salida: ", fuente_r, Brushes.Black, 1, Y)
            e.Graphics.DrawString(Format(dtpSalida.Value, "yyyy-MM-dd") & " " & dtphorasalida.Text, fuente_r, Brushes.Black, 270, Y, derecha)
            Y += 11
            e.Graphics.DrawString("----------------------------------------------------------------------------", fuente_b, Brushes.Black, 1, Y)
            Y += 15

            Dim caracteresPorLinea2 As Integer = 27
            Dim texto2 As String = Pie
            Dim inicio2 As Integer = 0
            Dim longitudTexto2 As Integer = texto2.Length

            While inicio2 < longitudTexto2
                Dim longitudBloque2 As Integer = Math.Min(caracteresPorLinea2, longitudTexto2 - inicio2)
                Dim bloque2 As String = texto2.Substring(inicio2, longitudBloque2)
                e.Graphics.DrawString(bloque2, New Font("Arial", 10, FontStyle.Regular), Brushes.Black, 1, Y)
                Y += 13
                inicio2 += caracteresPorLinea2
            End While
            Y += 7

            e.Graphics.DrawString("Lo atendio: " & lblusuario.Text, fuente_r, Brushes.Black, 1, Y)

        Catch ex As Exception
            MessageBox.Show(ex.ToString)
            cnn2.Close()
        End Try
    End Sub

    Private Sub PRservacion58_PrintPage(sender As Object, e As Printing.PrintPageEventArgs) Handles PRservacion58.PrintPage
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

        Try

            If tLogo <> "SIN" Then
                If File.Exists(My.Application.Info.DirectoryPath & "\" & nLogo) Then
                    Logotipo = Drawing.Image.FromFile(My.Application.Info.DirectoryPath & "\" & nLogo)

                    If tLogo = "CUAD" Then
                        e.Graphics.DrawImage(Logotipo, 45, 5, 110, 110)
                        Y += 130
                    End If
                    If tLogo = "RECT" Then
                        e.Graphics.DrawImage(Logotipo, 12, 0, 160, 80)
                        Y += 120
                    End If
                End If

            Else
                Y = 0
            End If
            cnn2.Close() : cnn2.Open()
            cmd2 = cnn2.CreateCommand
            cmd2.CommandText =
                "select Pie1,Cab0,Cab1,Cab2,Cab3,Cab4,Cab5,Cab6 from Ticket"
            rd2 = cmd2.ExecuteReader
            If rd2.HasRows Then
                If rd2.Read Then
                    Pie = rd2("Pie1").ToString
                    'Razón social
                    If rd2("Cab0").ToString() <> "" Then
                        e.Graphics.DrawString(rd2("Cab0").ToString, New Drawing.Font(tipografia, 8, FontStyle.Bold), Brushes.Black, 90, Y, sc)
                        Y += 12.5
                    End If
                    'RFC
                    If rd2("Cab1").ToString() <> "" Then
                        e.Graphics.DrawString(rd2("Cab1").ToString, New Drawing.Font(tipografia, 8, FontStyle.Bold), Brushes.Black, 90, Y, sc)
                        Y += 12.5
                    End If
                    'Calle  N°.
                    If rd2("Cab2").ToString() <> "" Then
                        e.Graphics.DrawString(rd2("Cab2").ToString, New Drawing.Font(tipografia, 8, FontStyle.Regular), Brushes.Gray, 90, Y, sc)
                        Y += 12
                    End If
                    'Colonia
                    If rd2("Cab3").ToString() <> "" Then
                        e.Graphics.DrawString(rd2("Cab3").ToString, New Drawing.Font(tipografia, 8, FontStyle.Regular), Brushes.Gray, 90, Y, sc)
                        Y += 12
                    End If
                    'Delegación / Municipio - Entidad
                    If rd2("Cab4").ToString() <> "" Then
                        e.Graphics.DrawString(rd2("Cab4").ToString, New Drawing.Font(tipografia, 8, FontStyle.Regular), Brushes.Gray, 90, Y, sc)
                        Y += 12
                    End If
                    'Teléfono
                    If rd2("Cab5").ToString() <> "" Then
                        e.Graphics.DrawString(rd2("Cab5").ToString, New Drawing.Font(tipografia, 8, FontStyle.Regular), Brushes.Gray, 90, Y, sc)
                        Y += 12
                    End If
                    'Correo
                    If rd2("Cab6").ToString() <> "" Then
                        e.Graphics.DrawString(rd2("Cab6").ToString, New Drawing.Font(tipografia, 8, FontStyle.Regular), Brushes.Gray, 90, Y, sc)
                        Y += 12
                    End If
                    Y += 5
                End If
            Else
                Y += 0
            End If
            rd2.Close()
            cnn2.Close()

            e.Graphics.DrawString("----------------------------------------------------------------------------", fuente_b, Brushes.Black, 1, Y)
            Y += 11
            e.Graphics.DrawString("R E S E R V A C I Ó N", fuente_b, Brushes.Black, 135, Y, sc)
            Y += 11
            e.Graphics.DrawString("----------------------------------------------------------------------------", fuente_b, Brushes.Black, 1, Y)
            Y += 15
            e.Graphics.DrawString("Habitación: " & lblhabitacion.Text, New Font("Arial", 10, FontStyle.Regular), Brushes.Black, 1, Y)

            e.Graphics.DrawString("Folio: " & IDRESERVACION, fuente_r, Brushes.Black, 180, Y, derecha)
            Y += 23
            e.Graphics.DrawString("Fecha Entrada: ", fuente_r, Brushes.Black, 1, Y)
            e.Graphics.DrawString(Format(dtpEntrada.Value, "yyyy-MM-dd") & " " & dtphoraentrada.Text, fuente_r, Brushes.Black, 180, Y, derecha)
            Y += 11
            e.Graphics.DrawString("Fecha Salida: ", fuente_r, Brushes.Black, 1, Y)
            e.Graphics.DrawString(Format(dtpSalida.Value, "yyyy-MM-dd") & " " & dtphorasalida.Text, fuente_r, Brushes.Black, 180, Y, derecha)
            Y += 11
            e.Graphics.DrawString("----------------------------------------------------------------------------", fuente_b, Brushes.Black, 1, Y)
            Y += 15

            Dim caracteresPorLinea2 As Integer = 27
            Dim texto2 As String = Pie
            Dim inicio2 As Integer = 0
            Dim longitudTexto2 As Integer = texto2.Length

            While inicio2 < longitudTexto2
                Dim longitudBloque2 As Integer = Math.Min(caracteresPorLinea2, longitudTexto2 - inicio2)
                Dim bloque2 As String = texto2.Substring(inicio2, longitudBloque2)
                e.Graphics.DrawString(bloque2, New Font("Arial", 10, FontStyle.Regular), Brushes.Black, 1, Y)
                Y += 13
                inicio2 += caracteresPorLinea2
            End While
            Y += 7

            e.Graphics.DrawString("Lo atendio: " & lblusuario.Text, fuente_r, Brushes.Black, 1, Y)

        Catch ex As Exception
            MessageBox.Show(ex.ToString)
            cnn2.Close()
        End Try
    End Sub
End Class