
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports System.IO

Public Class FrmDetReservacion

    Dim varHoras As String = ""
    Dim minutosTiempoH As Double = 0
    Dim cfolio As Integer = 0
    Dim cadenafact As String = ""
    Dim email As String = ""
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
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "SELECT Id,Correo FROM clientes WHERE Nombre='" & cboCLientes.Text & "'"
            rd1 = cmd1.ExecuteReader
            If rd1.HasRows Then
                If rd1.Read Then
                    lblidcliented.Text = rd1(0).ToString
                    email = rd1(1).ToString
                End If
            End If
            rd1.Close()



            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "SELECT * FROM reservaciones WHERE Cliente='" & cboCLientes.Text & "'"
            rd1 = cmd1.ExecuteReader
            If rd1.HasRows Then
                If rd1.Read Then
                    lblINE.Text = rd1("INE").ToString
                    txttelefono.Text = rd1("Telefono").ToString

                    'cboFolio.Text = rd1("IdReservacion").ToString

                    'cnn2.Close() : cnn2.Open()
                    'cmd2 = cnn2.CreateCommand
                    'cmd2.CommandText = "SELECT SUM(Abono) FROM abonoi WHERE Comentario='" & rd1("IdReservacion").ToString & "' AND Cliente='" & cboCLientes.Text & "'"
                    'rd2 = cmd2.ExecuteReader
                    'If rd2.HasRows Then
                    '    If rd2.Read Then
                    '        anticipo = IIf(rd2(0).ToString = "", 0, rd2(0).ToString)
                    '    End If
                    'End If
                    'rd2.Close()

                    'If anticipo > 0 Then
                    '    txtAnticipo.Text = FormatNumber(anticipo, 2)
                    '    lblAnticipo.Visible = True
                    '    txtAnticipo.Visible = True
                    'Else
                    '    lblAnticipo.Visible = False
                    '    txtAnticipo.Visible = False
                    'End If


                    'cmd2 = cnn2.CreateCommand
                    'cmd2.CommandText = "SELECT * FROM reservaciones WHERE Habitacion='" & lblHabitacion.Text & "' AND Status=0 AND Cliente='" & cboCLientes.Text & "' AND IdReservacion=" & cboFolio.Text & ""
                    'rd2 = cmd2.ExecuteReader
                    'If rd2.HasRows Then
                    '    If rd2.Read Then


                    '        Dim fechaentrada As Date = rd2("FEntrada").ToString
                    '        Dim FECHASALIDA As Date = rd2("FSalida").ToString
                    '        Dim fentrada As String = Format(fechaentrada, "yyyy-MM-dd")
                    '        Dim fsalida As String = Format(FECHASALIDA, "yyyy-MM-dd")
                    '        Dim horaentrada As String = Format(fechaentrada, "HH:mm:ss")
                    '        Dim horasalida As String = Format(FECHASALIDA, "HH:mm:ss")

                    '        If Date.Now > fechaentrada Then
                    '            cboCLientes.Text = rd2("Cliente").ToString
                    '            txttelefono.Text = rd2("Telefono").ToString

                    '            lblEntrada.Text = fentrada & " " & horaentrada
                    '            lblSalida.Text = fsalida & " " & horasalida

                    '            varHoras = DateDiff(DateInterval.Hour, CDate(fechaentrada), FECHASALIDA)
                    '            cboTipo.Text = "DIA"
                    '            txtHoras.Text = varHoras
                    '            cboPrecio.Text = rd2("precio").ToString
                    '            precio = cboPrecio.Text
                    '            anticipo = rd2("Anticipo").ToString
                    '            resta = CDbl(precio) - CDbl(anticipo)
                    '            cboPrecio.Text = FormatNumber(resta, 2)
                    '        Else
                    '            MsgBox("Esta reservación aun no esta disponible", vbInformation + vbOKOnly, titulohotelriaa)
                    '            btnLimpiar.PerformClick()
                    '            cnn1.Close()
                    '            cnn2.Close()
                    '            Exit Sub
                    '        End If


                    '    End If
                    'End If
                    'rd2.Close()
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
        cboTipo.Text = ""
        txtHoras.Text = ""
        cboPrecio.Text = ""
        txttelefono.Text = ""
        cboCLientes.Text = ""
        cboFolio.Text = ""
        lblINE.Text = ""
        lblEntrada.Text = ""
        lblSalida.Text = ""

        txtEfectivo.Text = "0.00"
        txtTarjeta.Text = "0.00"
        txtTransfe.Text = "0.00"
        txtOtro.Text = "0.00"
        txtDescuento.Text = "0.00"
        txtResta.Text = "0.00"
        txtCambio.Text = "0.00"
        txtSubtotal.Text = "0.00"
        txtTotalVenta.Text = "0.00"
        txtAnticipo.Text = "0.00"

        lblAnticipo.Visible = False
        txtAnticipo.Visible = False

        lblidcliented.Text = ""
        btnAbonar.Enabled = True
        cboCLientes.Focus.Equals(True)
    End Sub

    Private Sub cboCLientes_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cboCLientes.KeyPress
        e.KeyChar = UCase(e.KeyChar)
        If AscW(e.KeyChar) = Keys.Enter Then
            cboFolio.Focus.Equals(True)
        End If
    End Sub

    Private Sub txttelefono_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txttelefono.KeyPress
        If AscW(e.KeyChar) = Keys.Enter Then
            btnGuardar.Focus.Equals(True)
        End If
    End Sub

    Private Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click
        Dim ESTADO As String = ""
        Dim ESTADOPAGO As String = ""


        If CDbl(txtResta.Text) > 0 Then
            ESTADOPAGO = "RESTA"
        Else
            ESTADOPAGO = "PAGADO"
        End If

        ESTADO = "Ocupada"

        If lblusuario.Text = "" Then MsgBox("Ingrese la contraseña de usuario", vbInformation + vbOKOnly, titulohotelriaa) : txtcontra.Focus.Equals(True) : Exit Sub

        If txtHoras.Text = "" Then MsgBox("Debe de ingresar la cantidad de horas", vbInformation + vbOKOnly, titulohotelriaa) : txtHoras.Focus.Equals(True) : Exit Sub

        If cboCLientes.Text = "" Then MsgBox("Debe seleccionar el cliente para asignar la habitación", vbInformation + vbOKOnly, titulohotelriaa) : Exit Sub

        If cboPrecio.Text <= 0 Then MsgBox("Debe ingresar el precio total de la habitación.", vbInformation + vbOKOnly, titulohotelriaa) : Exit Sub



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
                PRECIO = txtResta.Text

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
                    cmd2.CommandText = "INSERT INTO detallehotel(Habitacion,Tipo,Estado,Horas,Precio,Cliente,Telefono,FEntrada,FSalida,Caracteristicas) VALUES('" & lblHabitacion.Text & "','" & lblTipo.Text & "','" & ESTADOPAGO & "'," & txtHoras.Text & "," & CDbl(cboPrecio.Text) & ",'" & cboCLientes.Text & "','" & txttelefono.Text & "','" & lblEntrada.Text & "','" & lblSalida.Text & "','" & lblCaracteristicas.Text & "')"
                    cmd2.ExecuteNonQuery()
                    cnn2.Close()

                    cnn2.Close() : cnn2.Open()
                    If txtResta.Text > 0 Then

                        cmd2 = cnn2.CreateCommand
                        cmd2.CommandText = "INSERT INTO comandas(Id,NMESA,Codigo,Nombre,Cantidad,UVenta,CostVR,CostVP,CostVUE,Descuento,Precio,Total,PrecioSinIva,TotalSinIVA,Comisionista,Fecha,Comensal,Status,Comentario,GPrint,CUsuario,Total_comensales,Depto,Grupo,EstatusT,Hr,EntregaT) VALUES(" & cfolio & ",'" & lblHabitacion.Text & "','xc3','Tiempo Habitacion',1,'SER',0,0,0,0," & PRECIO & "," & PRECIO & "," & PRECIO & "," & PRECIO & ",'0','" & Format(Date.Now, "yyyy/MM/dd") & "',0,'RESTA','Renta de Habitacion','','" & lblusuario.Text & "',0,'HABITACION','HABITACION',0,'" & HrTiempo & "','" & HrEntrega & "')"
                        cmd2.ExecuteNonQuery()
                    Else

                    End If


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
                cmd2.CommandText = "UPDATE reservaciones SET Status=1,Reservo='" & lblusuario.Text & "',Precio='" & PRECIO & "' WHERE Habitacion='" & lblHabitacion.Text & "' AND Cliente='" & cboCLientes.Text & "'and IdReservacion=" & cboFolio.Text & ""
                cmd2.ExecuteNonQuery()
                cnn2.Close()

                MsgBox("La habitacion " & lblHabitacion.Text & " fue asignada correctamente", vbInformation + vbOKOnly, titulohotelriaa)



                'CUNAOD SE HSOPEDE
                'Genera PDF y lo guarda en la ruta
                Panel6.Visible = True
                My.Application.DoEvents()

                PDF_HOJA()

                Panel6.Visible = False
                My.Application.DoEvents()

                btnLimpiar.PerformClick()
                Me.Close()
                frmManejo.Show()
            Catch ex As Exception
                MessageBox.Show(ex.ToString)
                cnn1.Close()
            End Try
        End If
    End Sub

    Private Sub PDF_HOJA()
        Dim root_name_recibo As String = ""
        Dim FileOpen As New ProcessStartInfo
        Dim FileNta As New Reservacion
        Dim strServerName As String = Application.StartupPath
        Dim crtableLogoninfos As New TableLogOnInfos
        Dim crtableLogoninfo As New TableLogOnInfo
        Dim crConnectionInfo As New ConnectionInfo
        Dim CrTables As Tables
        Dim CrTable As Table

        Dim ruta As String = cboCLientes.Text

        crea_ruta("C:\ControlNegociosPro\ARCHIVOSDL1\HOSPEDAJE\")
        root_name_recibo = "C:\ControlNegociosPro\ARCHIVOSDL1\HOSPEDAJE\Hab_" & ruta & ".pdf"

        If File.Exists("C:\ControlNegociosPro\ARCHIVOSDL1\HOSPEDAJE\Hab_" & ruta & ".pdf") Then
            File.Delete("C:\ControlNegociosPro\ARCHIVOSDL1\HOSPEDAJE\Hab_" & ruta & ".pdf")
        End If

        If varrutabase <> "" Then
            If File.Exists("\\" & varrutabase & "\ControlNegociosPro\ARCHIVOSDL1\HOSPEDAJE\Hab_" & ruta & ".pdf") Then
                File.Delete("\\" & varrutabase & "\ControlNegociosPro\ARCHIVOSDL1\HOSPEDAJE\Hab_" & ruta & ".pdf")
            End If
        End If

        With crConnectionInfo
            .ServerName = "C:\ControlNegociosPro\DL1.mdb"
            .DatabaseName = "C:\ControlNegociosPro\DL1.mdb"
            .UserID = ""
            .Password = "jipl22"
        End With

        CrTables = FileNta.Database.Tables
        For Each CrTable In CrTables
            crtableLogoninfo = CrTable.LogOnInfo
            crtableLogoninfo.ConnectionInfo = crConnectionInfo
            CrTable.ApplyLogOnInfo(crtableLogoninfo)
        Next


        FileNta.DataDefinition.FormulaFields("Cliente").Text = "'" & cboCLientes.Text & "'"
        FileNta.DataDefinition.FormulaFields("Habitacion").Text = "'" & lblHabitacion.Text & "'"
        FileNta.DataDefinition.FormulaFields("Llegada").Text = "'" & lblEntrada.Text & "'"
        FileNta.DataDefinition.FormulaFields("Salida").Text = "'" & lblSalida.Text & "'"
        FileNta.DataDefinition.FormulaFields("Telefono").Text = "'" & txttelefono.Text & "'"
        FileNta.DataDefinition.FormulaFields("Correo").Text = "'" & email & "'"

        FileNta.Refresh()
        FileNta.Refresh()
        FileNta.Refresh()
        If File.Exists(root_name_recibo) Then
            File.Delete(root_name_recibo)
        End If

        Try
            Dim CrExportOptions As ExportOptions
            Dim CrDiskFileDestinationOptions As New DiskFileDestinationOptions()
            Dim CrFormatTypeOptions As New PdfRtfWordFormatOptions()

            CrDiskFileDestinationOptions.DiskFileName = root_name_recibo '"c:\crystalExport.pdf"
            CrExportOptions = FileNta.ExportOptions
            With CrExportOptions
                .ExportDestinationType = ExportDestinationType.DiskFile
                .ExportFormatType = ExportFormatType.PortableDocFormat
                .DestinationOptions = CrDiskFileDestinationOptions
                .FormatOptions = CrFormatTypeOptions
            End With

            FileNta.Export()
            FileOpen.UseShellExecute = True
            FileOpen.FileName = root_name_recibo

            My.Application.DoEvents()

            If MsgBox("¿Deseas abrir el archivo?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                Process.Start(FileOpen)
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        FileNta.Close()

        If varrutabase <> "" Then

            If File.Exists("\\" & varrutabase & "\ControlNegociosPro\ARCHIVOSDL1\HOSPEDAJE\Hab_" & ruta & ".pdf") Then
                File.Delete("\\" & varrutabase & "\ControlNegociosPro\ARCHIVOSDL1\HOSPEDAJE\Hab_" & ruta & ".pdf")
            End If

            System.IO.File.Copy("C:\ControlNegociosPro\ARCHIVOSDL1\HOSPEDAJE\Hab_" & ruta & ".pdf", "\\" & varrutabase & "\ControlNegociosPro\ARCHIVOSDL1\HOSPEDAJE\Hab_" & ruta & ".pdf")
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

                If txtEfectivo.Text > 0 Then
                    btnAbonar.Focus.Equals(True)
                Else
                    btnGuardar.Focus.Equals(True)
                End If


            Catch ex As Exception
                MessageBox.Show(ex.ToString)
                cnn1.Close()
            End Try

        End If
    End Sub

    Private Sub cboFolio_SelectedValueChanged(sender As Object, e As EventArgs) Handles cboFolio.SelectedValueChanged
        Try
            Dim anticipo As Double = 0
            Dim precio As Double = 0
            Dim resta As Double = 0



            cnn4.Close() : cnn4.Open()
            cmd4 = cnn4.CreateCommand
            cmd4.CommandText = "SELECT SUM(Abono) FROM abonoi WHERE Comentario='" & cboFolio.Text & "' AND Cliente='" & cboCLientes.Text & "'"
            rd4 = cmd4.ExecuteReader
            If rd4.HasRows Then
                If rd4.Read Then
                    anticipo = IIf(rd4(0).ToString = "", 0, rd4(0).ToString)
                End If
            End If
            rd4.Close()

            If anticipo > 0 Then
                txtAnticipo.Text = FormatNumber(anticipo, 2)
                lblAnticipo.Visible = True
                txtAnticipo.Visible = True
            Else
                lblAnticipo.Visible = False
                txtAnticipo.Visible = False
            End If

            cmd4 = cnn4.CreateCommand

            If cboCLientes.Text = "" Then
                cmd4.CommandText = "SELECT * FROM reservaciones WHERE Habitacion='" & lblHabitacion.Text & "' AND Status=0  AND IdReservacion=" & cboFolio.Text & ""
            Else
                cmd4.CommandText = "SELECT * FROM reservaciones WHERE Habitacion='" & lblHabitacion.Text & "' AND Status=0 AND Cliente='" & cboCLientes.Text & "' AND IdReservacion=" & cboFolio.Text & ""
            End If

            rd4 = cmd4.ExecuteReader
            If rd4.HasRows Then
                If rd4.Read Then
                    txttelefono.Text = rd4("Telefono").ToString
                    lblINE.Text = rd4("INE").ToString


                    Dim fechaentrada As Date = rd4("FEntrada").ToString
                    Dim FECHASALIDA As Date = rd4("FSalida").ToString
                    Dim fentrada As String = Format(fechaentrada, "yyyy-MM-dd")
                    Dim fsalida As String = Format(FECHASALIDA, "yyyy-MM-dd")
                    Dim horaentrada As String = Format(fechaentrada, "HH:mm:ss")
                    Dim horasalida As String = Format(FECHASALIDA, "HH:mm:ss")

                    lblEntrada.Text = fentrada & " " & horaentrada
                    lblSalida.Text = fsalida & " " & horasalida

                    If Date.Now > fechaentrada Then

                        If cboCLientes.Text = "" Then
                            cboCLientes.Text = rd4("Cliente").ToString
                        Else
                            cboFolio.Text = rd4("IdReservacion").ToString
                        End If

                        varHoras = DateDiff(DateInterval.Hour, CDate(fechaentrada), FECHASALIDA)
                        cboTipo.Text = "DIA"
                        txtHoras.Text = varHoras
                        cboPrecio.Text = IIf(rd4("precio").ToString = "", "0", rd4("Precio").ToString)
                        precio = rd4("precio").ToString
                        anticipo = rd4("Anticipo").ToString
                        resta = CDbl(precio) - CDbl(anticipo)
                        txtAnticipo.Text = FormatNumber(anticipo, 2)
                        txtResta.Text = FormatNumber(resta, 2)
                        txtSubtotal.Text = FormatNumber(rd4("precio").ToString, 2)
                        txtTotalVenta.Text = FormatNumber(resta, 2)
                        If resta = "0.00" Then
                            btnAbonar.Enabled = False
                        Else
                            btnAbonar.Enabled = True
                        End If
                    Else
                        MsgBox("Esta reservación aun no esta disponible", vbInformation + vbOKOnly, titulohotelriaa)
                        btnLimpiar.PerformClick()
                        cnn3.Close()
                        cnn4.Close()
                        Exit Sub
                    End If
                End If
            End If
            rd3.Close()
            cnn4.Close()

            If txtResta.Text > 0 Then
                txtEfectivo.Focus.Equals(True)
                txtEfectivo.Enabled = True
                txtTarjeta.Enabled = True
                txtTransfe.Enabled = True
                txtOtro.Enabled = True
                btnGuardar.Enabled = False
                btnAbonar.Enabled = True
            Else
                txtEfectivo.Enabled = False
                txtTarjeta.Enabled = False
                txtTransfe.Enabled = False
                txtOtro.Enabled = False
                btnAbonar.Enabled = False
                btnGuardar.Enabled = True
                If cboPrecio.Text = "0" Then
                    btnGuardar.Enabled = False
                    cboPrecio.Focus.Equals(True)
                Else
                    btnGuardar.Enabled = True
                    btnGuardar.Focus.Equals(True)
                End If

            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString)
            cnn3.Close()
            cnn4.Close()
        End Try
    End Sub

    Private Sub cboPrecio_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cboPrecio.KeyPress
        If AscW(e.KeyChar) = Keys.Enter Then
            If IsNumeric(cboPrecio.Text) Then
                If cboPrecio.Text > 0 Then
                    btnAbonar.Enabled = True
                Else
                    btnAbonar.Enabled = False
                End If
                txtTotalVenta.Text = FormatNumber(cboPrecio.Text, 2)
                txtSubtotal.Text = FormatNumber(cboPrecio.Text, 2)
                txtResta.Text = FormatNumber(cboPrecio.Text, 2)

                If txtResta.Text > 0 Then

                    txtEfectivo.Enabled = True
                    txtTarjeta.Enabled = True
                    txtTransfe.Enabled = True
                    txtOtro.Enabled = True
                    txtEfectivo.Focus.Equals(True)
                End If
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
    End Sub

    Private Sub txtDescuento_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtDescuento.KeyPress
        If AscW(e.KeyChar) = Keys.Enter Then
            txtEfectivo.Focus.Equals(True)
        End If
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

    Private Sub btnAbonar_Click(sender As Object, e As EventArgs) Handles btnAbonar.Click
        Try

            btnAbonar.Enabled = False

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

            If lblusuario.Text = "Contraseña" Then
                MsgBox("Ingrese la contraseña para continuar.", vbInformation + vbOKOnly, titulohotelriaa)
                txtcontra.Focus.Equals(True)
                btnAbonar.Enabled = True
                Exit Sub
            End If

            If cboCLientes.Text = "" Then
                MsgBox("Debe seleccionar el cliente.", vbInformation + vbOKOnly, titulohotelriaa)
                btnAbonar.Enabled = True
                Exit Sub
            End If

            If txtTotalVenta.Text = "0.00" Then
                MsgBox("Debe de haber un total a pagar.", vbInformation + vbOKOnly, titulohotelriaa)
                btnAbonar.Enabled = True
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
                cmd1.CommandText = "INSERT INTO ventas(IdCliente,Cliente,Direccion,Subtotal,IVA,Totales,Descuento,Devolucion,Acuenta,Resta,Usuario,FVenta,HVenta,FPago,FCancelado,Status,Comisionista,Comision,Concepto,MontoSinDesc,FEntrega,Comentario,StatusE,FolMonedero,CodFactura,IP,Formato,Franquicia,Pedido,Fecha) values(" & lblidcliented.Text & ",'" & cboCLientes.Text & "',''," & mysubtotal & ",0," & mytotalventa & "," & mydescuento & ",0," & acuenta & "," & myresta & ",'" & lblusuario.Text & "','" & Format(Date.Now, "yyyy-MM-dd") & "','" & Format(Date.Now, "HH:mm:ss") & "','','','" & mystatus & "','',0,'RESERVACION'," & mysubtotal & ",'" & Format(Date.Now, "yyyy-MM-dd") & "','',0,'','" & cadenafact & "','" & dameIP2() & "','TICKET',0,'','" & Format(Date.Now, "yyyy-MM-dd HH:mm:ss") & "')"
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
                cmd1.CommandText = "SELECT Saldo FROM abonoi WHERE Id=(SELECT MAX(Id) FROM abonoi WHERE Comentario='" & cboFolio.Text & "' AND Cliente='" & cboCLientes.Text & "')"
                rd1 = cmd1.ExecuteReader
                If rd1.HasRows Then
                    If rd1.Read Then
                        saldo = IIf(rd1(0).ToString = "", 0, rd1(0).ToString) - CDbl(txtTotalVenta.Text)
                    End If
                Else
                    saldo = CDbl(txtTotalVenta.Text)
                    cnn2.Close() : cnn2.Open()
                    cmd2 = cnn2.CreateCommand
                    cmd2.CommandText = "INSERT INTO abonoi(NumFolio,IdCliente,Cliente,Concepto,fecha,Hora,FechaCompleta,Cargo,Abono,Saldo,FormaPago,Monto,Banco,Referencia,Usuario,Descuento,MontoSF,Comentario) VALUES(" & myfolio & "," & lblidcliented.Text & ",'" & cboCLientes.Text & "','NOTA VENTA','" & Format(Date.Now, "yyyy-MM-dd") & "','" & Format(Date.Now, "HH:mm:ss") & "','" & Format(Date.Now, "yyyy-MM-dd HH:mm:ss") & "'," & CDbl(txtTotalVenta.Text) & ",0," & saldo & ",'',0,'','','" & lblusuario.Text & "'," & mydescuento & ",0,'" & cboFolio.Text & "')"
                    cmd2.ExecuteNonQuery()
                    cnn2.Close()
                End If
                rd1.Close()



                If efectivo > 0 Then
                    cmd1 = cnn1.CreateCommand
                    cmd1.CommandText = "SELECT Saldo FROM abonoi WHERE Id=(SELECT MAX(Id) FROM abonoi WHERE  Cliente='" & cboCLientes.Text & "')"
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
                    cmd1.CommandText = "INSERT INTO abonoi(NumFolio,IdCliente,Cliente,Concepto,Fecha,Hora,FechaCompleta,Cargo,Abono,Saldo,FormaPago,Monto,Banco,Referencia,Usuario,Comentario,Comisiones) VALUES(" & myfolio & "," & lblidcliented.Text & ",'" & cboCLientes.Text & "','ABONO','" & Format(Date.Now, "yyyy-MM-dd") & "','" & Format(Date.Now, "HH:mm:ss") & "','" & Format(Date.Now, "yyyy-MM-dd HH:mm:ss") & "',0," & efectivo & "," & saldo & ",'EFECTIVO'," & efectivo & ",'','','" & lblusuario.Text & "','" & cboFolio.Text & "',0)"
                    cmd1.ExecuteNonQuery()
                End If

                If tarjeta > 0 Then
                    cmd1 = cnn1.CreateCommand
                    cmd1.CommandText = "SELECT Saldo FROM abonoi WHERE Id=(SELECT MAX(Id) FROM abonoi WHERE Cliente='" & cboCLientes.Text & "')"
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
                    cmd1.CommandText = "INSERT INTO abonoi(NumFolio,IdCliente,Cliente,Concepto,Fecha,Hora,FechaCompleta,Cargo,Abono,Saldo,FormaPago,Monto,Banco,Referencia,Usuario,Comentario,Comisiones) VALUES(" & myfolio & "," & lblidcliented.Text & ",'" & cboCLientes.Text & "','ABONO','" & Format(Date.Now, "yyyy-MM-dd") & "','" & Format(Date.Now, "HH:mm:ss") & "','" & Format(Date.Now, "yyyy-MM-dd HH:mm:ss") & "',0," & tarjeta & "," & saldo & ",'TARJETA'," & tarjeta & ",'','','" & lblusuario.Text & "','" & cboFolio.Text & "',0)"
                    cmd1.ExecuteNonQuery()


                End If

                If transaferencia > 0 Then

                    cmd1 = cnn1.CreateCommand
                    cmd1.CommandText = "SELECT Saldo FROM abonoi WHERE Id=(SELECT MAX(Id) FROM abonoi WHERE Cliente='" & cboCLientes.Text & "')"
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
                    cmd1.CommandText = "INSERT INTO abonoi(NumFolio,IdCliente,Cliente,Concepto,Fecha,Hora,FechaCompleta,Cargo,Abono,Saldo,FormaPago,Monto,Banco,Referencia,Usuario,Comentario,Comisiones) VALUES(" & myfolio & "," & lblidcliented.Text & ",'" & cboCLientes.Text & "','ABONO','" & Format(Date.Now, "yyyy-MM-dd") & "','" & Format(Date.Now, "HH:mm:ss") & "','" & Format(Date.Now, "yyyy-MM-dd HH:mm:ss") & "',0," & transaferencia & "," & saldo & ",'TRANSFERENCIA'," & transaferencia & ",'','','" & lblusuario.Text & "','" & cboFolio.Text & "',0)"
                    cmd1.ExecuteNonQuery()
                End If

                If otro > 0 Then

                    cmd1 = cnn1.CreateCommand
                    cmd1.CommandText = "SELECT Saldo FROM abonoi WHERE Id=(SELECT MAX(Id) FROM abonoi WHERE Cliente='" & cboCLientes.Text & "')"
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
                    cmd1.CommandText = "INSERT INTO abonoi(NumFolio,IdCliente,Cliente,Concepto,Fecha,Hora,FechaCompleta,Cargo,Abono,Saldo,FormaPago,Monto,Banco,Referencia,Usuario,Comentario,Comisiones) VALUES(" & myfolio & "," & lblidcliented.Text & ",'" & cboCLientes.Text & "','ABONO','" & Format(Date.Now, "yyyy-MM-dd") & "','" & Format(Date.Now, "HH:mm:ss") & "','" & Format(Date.Now, "yyyy-MM-dd HH:mm:ss") & "',0," & otro & "," & saldo & ",'OTRO'," & otro & ",'','','" & lblusuario.Text & "','" & cboFolio.Text & "',0)"
                    cmd1.ExecuteNonQuery()


                End If
                cnn1.Close()




                cnn2.Close() : cnn2.Open()
                cmd2 = cnn2.CreateCommand
                cmd2.CommandText =
            "insert into VentasDetalle(Folio,Codigo,Nombre,Unidad,Cantidad,CostoVP,CostoVUE,Precio,Total,PrecioSinIVA,TotalSinIVA,Fecha,FechaCompleta,Comisionista,Facturado,Depto,Grupo,CostVR,Descto,VDCosteo,TotalIEPS,TasaIEPS,Caducidad,Lote,CantidadE,Promo_Monedero,Unico,Descuento,Gprint,CodUnico) values(" & myfolio & ",'xc3','Tiempo Habitacion','PZA',1,0,0," & mysubtotal & "," & mytotalventa & "," & mysubtotal & "," & mytotalventa & ",'" & Format(Date.Now, "yyyy-MM-dd") & "','" & Format(Date.Now, "yyyy-MM-dd HH:mm:ss") & "','0','0','HABITACION','HABITACION','0',0,0,0,0,'','',0,0,0," & mydescuento & ",'','')"
                cmd2.ExecuteNonQuery()

                cmd2 = cnn2.CreateCommand
                cmd2.CommandText = "UPDATE reservaciones SET Tipo='" & cboTipo.Text & "', Precio=" & cboPrecio.Text & ",Anticipo=" & CDbl(txtAnticipo.Text) + acuenta & " WHERE IdReservacion=" & cboFolio.Text & " AND Cliente='" & cboCLientes.Text & "'"
                cmd2.ExecuteNonQuery()
                cnn2.Close()

                Dim timpresora As Integer = TamImpre()
                Dim impresora As String = ImpresoraImprimir()

                If impresora = "" Then
                    MsgBox("La impresora no esta configurada.", vbInformation + vbOKOnly, titulohotelriaa)
                    GoTo deku
                End If

                If timpresora = "80" Then
                    '  PReservacion80.DefaultPageSettings.PrinterSettings.PrinterName = impresora
                    ' PReservacion80.Print()
                End If
deku:
                btnLimpiar.PerformClick()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
            cnn1.Close()
        End Try
    End Sub

    Private Sub cboFolio_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cboFolio.KeyPress
        If AscW(e.KeyChar) = Keys.Enter Then
            cboPrecio.Focus.Equals(True)
        End If
    End Sub
End Class