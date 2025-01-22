Imports System.Data.OleDb
Imports System.Windows.Forms.VisualStyles.VisualStyleElement.Button
Imports Core.DAL.DE
Imports DocumentFormat.OpenXml.Spreadsheet
Imports MySql.Data
Imports MySql.Data.MySqlClient
Imports Org.BouncyCastle.Asn1.Tsp

Public Class frmLoad

    Private Sub frmLoad_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Login.Hide()
        ProgressBar1.Visible = True
        ProgressBar1.Value = ProgressBar1.Value + 2
        Label1.Text = ProgressBar1.Value & "% Cargado, Espere Por Favor ..."


        'Timer1.Enabled = True
    End Sub

    Public Sub cargaTodo()

        'Dim cnn1 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
        'Dim cnn2 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
        'Dim rd1, rd2 As MySqlDataReader
        'Dim cmd1, cmd2 As MySqlCommand
        PrimeraConfig = ""
        Login.Hide()

        VerificarVentasDetalle()
        My.Application.DoEvents()

        Label1.Text = "Cargando base de datos..."
        ProgressBar1.Value = 25
        My.Application.DoEvents()

        ' BuscarTablas()

        ProgressBar1.Value = 40
        My.Application.DoEvents()
        verif()

        ProgressBar1.Value = 50
        Label1.Text = "Actualizando base de datos..."
        My.Application.DoEvents()

        Inicio.Permisos(id_usu_log)

        ProgressBar1.Value = 55
        Label1.Text = "Verificando datos..."
        My.Application.DoEvents()

        If varrutabase = "" Then
            ActualizaCampos()
        End If
        ProgressBar1.Value = 60
        Label1.Text = "Cargando Permisos de usuario..."
        My.Application.DoEvents()
        'Licencia()

        cnn1.Close()
        cnn1.Open()
        cmd1 = cnn1.CreateCommand
        cmd1.CommandText = "Select numero,usuario,password from loginrecargas"
        rd1 = cmd1.ExecuteReader
        If rd1.Read Then
            varnumero = rd1("numero").ToString
            varusuario = rd1("usuario").ToString
            varcontra = rd1("password").ToString
        End If
        rd1.Close()
        cnn1.Close()


        If tienda_enlinea = True Then
            Inicio.Nuevos_Pedidos()
        End If
        SformatosInicio()

        ProgressBar1.Value = ProgressBar1.Value + 1
        My.Application.DoEvents()

        Dim pediatra As Integer = DatosRecarga2("Pediatra")
        Dim tiendalinea As Integer = DatosRecarga2("TiendaLinea")
        Dim gimnasios As Integer = DatosRecarga2("Gimnasio")
        Dim consignacion As Integer = DatosRecarga2("Consignacion")
        ProgressBar1.Value = ProgressBar1.Value + 1
        My.Application.DoEvents()
        Dim nomina As Integer = DatosRecarga2("Nomina")
        Dim Mod_Asis As Integer = DatosRecarga2("Mod_Asis")
        ProgressBar1.Value = ProgressBar1.Value + 1
        My.Application.DoEvents()
        Dim Control_Servicios As Integer = DatosRecarga2("Control_Servicios")
        Dim series As Integer = DatosRecarga2("Series")
        ProgressBar1.Value = ProgressBar1.Value + 1
        My.Application.DoEvents()
        Dim produccion As Integer = DatosRecarga2("Produccion")
        Dim partes As Integer = DatosRecarga2("Partes")
        ProgressBar1.Value = ProgressBar1.Value + 1
        ProgressBar1.Value = 70
        My.Application.DoEvents()
        Dim escuelas As Integer = DatosRecarga2("Escuelas")
        Dim costeo As Integer = DatosRecarga2("Costeo")
        ProgressBar1.Value = ProgressBar1.Value + 1
        My.Application.DoEvents()
        Dim restaurante As Integer = DatosRecarga2("Restaurante")
        Dim taller As Integer = DatosRecarga2("Taller")
        Dim refaccionaria As Integer = DatosRecarga2("Refaccionaria")
        Dim pollos As Integer = DatosRecarga2("pollos")
        ProgressBar1.Value = ProgressBar1.Value + 1
        My.Application.DoEvents()
        Dim telefonia As Integer = DatosRecarga2("Telefonia")
        Dim Hoteles As Integer = DatosRecarga2("Hoteles")
        Dim Migracion As Integer = DatosRecarga2("Migracion")
        ProgressBar1.Value = ProgressBar1.Value + 1
        My.Application.DoEvents()
        Dim Optica As Integer = DatosRecarga2("Optica")
        Dim Mov_Cuenta As Integer = DatosRecarga2("Mov_Cuenta")
        Dim transportistas As Integer = DatosRecarga2("Transportistas")
        ProgressBar1.Value = ProgressBar1.Value + 1
        My.Application.DoEvents()
        Dim produccionpro As Integer = DatosRecarga2("ProduccionPro")
        Dim dentista As Integer = DatosRecarga2("Dentista")
        Dim VentasRuta As Integer = DatosRecarga2("VentasRuta")
        ' Dim dentista As Integer = Await ValidarAsync("Dentista")
        ProgressBar1.Value = ProgressBar1.Value + 1
        My.Application.DoEvents()
        Dim bodegas As Integer = DatosRecarga2("Bodegas")
        Dim papos As Integer = DatosRecarga2("Papos")

        ProgressBar1.Value = ProgressBar1.Value + 1
        My.Application.DoEvents()

        If pediatra = 1 Then
            Inicio.btnPediatra.Visible = True
        Else
            Inicio.btnPediatra.Visible = False
        End If

        If bodegas = 1 Then
            Inicio.btnBodegas.Visible = True
        Else
            Inicio.btnBodegas.Visible = False
        End If

        If dentista = 1 Then
            Inicio.btnDentista.Visible = True
        Else
            Inicio.btnDentista.Visible = False
        End If

        If VentasRuta = 1 Then
            If Inicio.perRuta = 0 Then
                Inicio.MenuVentasRuta.Enabled = False
            Else
                Inicio.MenuVentasRuta.Visible = True
            End If

        Else
            Inicio.MenuVentasRuta.Visible = False
        End If

        If produccionpro = 1 Then
            Inicio.TproduccionCos.Visible = True
        Else
            Inicio.TproduccionCos.Visible = False
        End If

        ProgressBar1.Value = ProgressBar1.Value + 1
        ProgressBar1.Value = 80
        My.Application.DoEvents()

        If tiendalinea = 1 Then
            Inicio.PedidosTiendaEnLíneaToolStripMenuItem.Visible = True
            Inicio.pedidos_tienda.Visible = True
        Else
            Inicio.PedidosTiendaEnLíneaToolStripMenuItem.Visible = False
            Inicio.pedidos_tienda.Visible = False
        End If

        If gimnasios = 1 Then
            Inicio.GimnasiosToolStripMenuItem.Visible = True
        Else
            Inicio.GimnasiosToolStripMenuItem.Visible = False
        End If

        ProgressBar1.Value = ProgressBar1.Value + 1
        My.Application.DoEvents()

        If consignacion = 1 Then
            Inicio.menuconsignaciones.Visible = True
        Else
            Inicio.menuconsignaciones.Visible = False
        End If

        ProgressBar1.Value = ProgressBar1.Value + 1
        My.Application.DoEvents()

        If nomina = 1 Then
            Inicio.NominaToolStripMenuItem.Visible = True
        Else
            Inicio.NominaToolStripMenuItem.Visible = False
        End If

        If Mod_Asis = 1 Then
            Inicio.pAsistencia.Visible = True
        Else
            Inicio.pAsistencia.Visible = False
        End If

        ProgressBar1.Value = ProgressBar1.Value + 1
        My.Application.DoEvents()

        If Control_Servicios = 1 Then
            Inicio.ControlDeServiciosToolStripMenuItem.Visible = True
        Else
            Inicio.ControlDeServiciosToolStripMenuItem.Visible = False
        End If

        ProgressBar1.Value = ProgressBar1.Value + 1
        My.Application.DoEvents()

        cnn2.Close() : cnn2.Open()
        cmd2 = cnn2.CreateCommand
        cmd2.CommandText = "SELECT Rep_Servicios FROM permisos WHERE IdEmpleado=" & id_usu_log
        rd2 = cmd2.ExecuteReader
        If rd2.HasRows Then
            If rd2.Read Then
                If rd2(0).ToString = 1 Then
                    If Control_Servicios = 1 Then
                        Inicio.ReporteDeControlDeServiciosToolStripMenuItem.Visible = True
                    Else
                        Inicio.ReporteDeControlDeServiciosToolStripMenuItem.Visible = False
                    End If
                Else
                    Inicio.ReporteDeControlDeServiciosToolStripMenuItem.Visible = False
                End If
            End If
        Else
            Inicio.ReporteDeControlDeServiciosToolStripMenuItem.Visible = False
        End If
        rd2.Close()
        cnn2.Close()
        My.Application.DoEvents()


        ProgressBar1.Value = ProgressBar1.Value + 1

        My.Application.DoEvents()

        If series = 1 Then
            Inicio.ReporteDeSeries.Visible = True
        Else
            Inicio.ReporteDeSeries.Visible = False
        End If

        If produccion = 1 Then
            Inicio.pMod_Produccion.Enabled = True
            Inicio.pMod_Produccion.Visible = True
            '  pControl_Procesos.Visible = False
        Else
            Inicio.pMod_Produccion.Enabled = False
            Inicio.pMod_Produccion.Visible = False
            'pControl_Procesos.Visible = False
        End If

        ProgressBar1.Value = ProgressBar1.Value + 1
        My.Application.DoEvents()


        If papos = 1 Then
            Inicio.ZapateríaToolStripMenuItem.Visible = True
            Inicio.pZapatería.Visible = True
        End If

        If partes = 1 Then
            Inicio.pVentasT.Visible = False
            Inicio.pMod_Precios.Visible = False
            Inicio.pMod_Produccion.Visible = False
            Inicio.Button5.Visible = False
        End If

        ProgressBar1.Value = ProgressBar1.Value + 1
        My.Application.DoEvents()

        If escuelas = 1 Then
            Inicio.pClientes.Visible = False
            Inicio.pMonederos.Visible = False
            Inicio.GruposToolStripMenuItem.Visible = True
            Inicio.AlumnosToolStripMenuItem.Visible = True
            Inicio.InscripciónToolStripMenuItem.Visible = True
            Inicio.ZapateríaToolStripMenuItem.Visible = False
            Inicio.ProductosToolStripMenuItem.Visible = True
            Inicio.ComprasTouchToolStripMenuItem.Visible = False
            Inicio.AlumnosToolStripMenuItem1.Visible = True
            Inicio.pVentasT.Visible = False
            Inicio.pAbonosV.Visible = False
            Inicio.Button5.Visible = False
        End If

        ProgressBar1.Value = ProgressBar1.Value + 1
        My.Application.DoEvents()

        If costeo = 1 Then
        Else
            MsgBox("Por favor configura el método de costeo de tu inventario antes de comenzar.", vbInformation + vbOKOnly, "Delsscom Control Negocios Pro")
            PrimeraConfig = "1"
            frmConfigs.Show()
            frmConfigs.tabFuncionalidades1.Focus().Equals(True)
            frmConfigs.tabFuncionalidades1.Select()
            frmConfigs.TopMost = True
        End If


        ProgressBar1.Value = 90

        ProgressBar1.Value = ProgressBar1.Value + 1
        My.Application.DoEvents()

        If restaurante = 1 Then
            Inicio.btnComandera.Visible = True
            Inicio.btnPagarComandas.Visible = True
            Inicio.btnvtatouch.Visible = True
            Inicio.CORTEMESERO.Visible = True
            Inicio.pMod_Produccion.Visible = True
            Inicio.pMod_Produccion.Enabled = True
            Inicio.btnVisor.Visible = True
            frmPermisos.btnPermisosRestaurante.Visible = True
            Inicio.repHistorialMesas.Visible = True
        Else
            Inicio.btnComandera.Visible = False
            Inicio.btnPagarComandas.Visible = False
            Inicio.btnvtatouch.Visible = False
            Inicio.btnVisor.Visible = False
            Inicio.CORTEMESERO.Visible = False
            frmPermisos.btnPermisosRestaurante.Visible = False
            Inicio.repHistorialMesas.Visible = False
        End If

        ProgressBar1.Value = ProgressBar1.Value + 1
        My.Application.DoEvents()

        If refaccionaria = 1 Then
            Inicio.btnRefaccionaria.Visible = True
        Else
            Inicio.btnRefaccionaria.Visible = False
        End If

        If taller = 1 Then
            Inicio.btnTaller.Visible = True
        Else
            Inicio.btnTaller.Visible = False
        End If

        If pollos = 1 Then
            Inicio.btnpollo.Visible = True
        Else
            Inicio.btnpollo.Visible = False
        End If

        ProgressBar1.Value = ProgressBar1.Value + 1
        My.Application.DoEvents()

        If telefonia = 1 Then
            Inicio.btnTelefonia.Visible = True
        Else
            Inicio.btnTelefonia.Visible = False
        End If

        ProgressBar1.Value = ProgressBar1.Value + 1
        My.Application.DoEvents()

        If Hoteles = 1 Then
            Inicio.btnHoteleria.Visible = True
            Inicio.ReporteDeHotelToolStripMenuItem.Visible = True
        Else
            Inicio.btnHoteleria.Visible = False
            Inicio.ReporteDeHotelToolStripMenuItem.Visible = False
        End If

        ProgressBar1.Value = ProgressBar1.Value + 1
        My.Application.DoEvents()

        If Migracion = 1 Then
            Inicio.pMigracion.Visible = True
        Else
            Inicio.pMigracion.Visible = False
        End If

        If Optica = 1 Then
            Inicio.btnOptica.Visible = True
        Else
            Inicio.btnOptica.Visible = False
        End If

        ProgressBar1.Value = ProgressBar1.Value + 1
        My.Application.DoEvents()

        If Mov_Cuenta = 1 Then
            Inicio.ReporteMovCuentasToolStripMenuItem.Visible = True
            Inicio.MovCuentasToolStripMenuItem.Visible = True
        Else
            Inicio.ReporteMovCuentasToolStripMenuItem.Visible = False
            Inicio.MovCuentasToolStripMenuItem.Visible = False
        End If

        If transportistas = 1 Then
            Inicio.TransportistasToolStripMenuItem.Visible = True
        Else
            Inicio.TransportistasToolStripMenuItem.Visible = False
        End If

        ProgressBar1.Value = ProgressBar1.Value + 1
        My.Application.DoEvents()

        Dim cnn As OleDbConnection = New OleDbConnection
        Dim sSQL As String = ""
        Dim sinfo As String = ""
        Dim oData As New ToolKitSQL.oledbdata
        Dim dr As DataRow
        sTarget = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & My.Application.Info.DirectoryPath & "\CIAS.mdb;"
        sSQL = "select base,Servidor from Server"
        With oData
            If .dbOpen(cnn, sTarget, sinfo) Then
                If .getDr(cnn, dr, sSQL, sinfo) Then
                    varrutabase = dr(1).ToString
                    If varrutabase = "" Then
                        sTarget = "server=" & dameIP2() & ";uid=Delsscom;password=jipl22;database=cn" & baseseleccionada & ";persist security info=false;connect timeout=300"
                        sTargetlocal = "server=" & dameIP2() & ";uid=Delsscom;password=jipl22;database=cn" & baseseleccionada & ";persist security info=false;connect timeout=300"
                    Else
                        sTarget = "server=" & varrutabase & ";uid=Delsscom;password=jipl22;database=cn" & baseseleccionada & ";persist security info=false;connect timeout=300"
                        sTargetlocal = "server=" & varrutabase & ";uid=Delsscom;password=jipl22;database=cn" & baseseleccionada & ";persist security info=false;connect timeout=300"
                    End If
                End If
                cnn.Close()
            End If
        End With


        sinfo = ""
        Dim dt As New DataTable
        Dim odata1 As New ToolKitSQL.myssql
        If odata1.dbOpen(cnn1, sTarget, sinfo) Then
            If odata1.getDt(cnn1, dt, "select IdNube from traslados", sinfo) Then
            Else
                If sinfo <> "" Then
                    odata1.runSp(cnn1, "ALTER TABLE traslados ADD COLUMN IdNube Integer DEFAULT 0", sinfo)
                    odata1.runSp(cnn1, "update traslados set IdNube = 1", sinfo)
                    sinfo = ""
                End If
            End If

            If odata1.getDt(cnn1, dt, "select IdNube, IdNubeActu from trasladosdet", sinfo) Then
            Else
                If sinfo <> "" Then
                    odata1.runSp(cnn1, "ALTER TABLE trasladosdet ADD COLUMN IdNube Integer DEFAULT 0", sinfo)
                    odata1.runSp(cnn1, "ALTER TABLE trasladosdet ADD COLUMN IdNubeActu Integer DEFAULT 0", sinfo)
                    odata1.runSp(cnn1, "update trasladosdet set IdNube = 1, IdNubeActu = 1 ", sinfo)
                    sinfo = ""
                End If
            End If

            cnn1.Close()
        End If

        ''Validación de la aditoria

        Try
            cnn1.Close() : cnn1.Open()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText =
                "select NotasCred from Formatos where Facturas='Audita'"
            rd1 = cmd1.ExecuteReader
            If rd1.HasRows Then
                If rd1.Read Then
                    validaciones.audita = rd1(0).ToString
                End If
            End If
            rd1.Close()
            cnn1.Close()

            ProgressBar1.Value = 100
            My.Application.DoEvents()

        Catch ex As Exception
            MessageBox.Show(ex.ToString)
            cnn1.Close()
        End Try

        Me.Hide()
        ProgressBar1.Value = 0
        Inicio.Show()
        My.Application.DoEvents()

    End Sub

    Public Sub verif()
        'cumpleaños monedero

        'Dim cnn1 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
        'Dim rd1 As MySqlDataReader
        'Dim cmd1 As MySqlCommand

        Try
            cnn1.Close()
            cnn1.Open()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "SELECT DMembre FROM productos"
            rd1 = cmd1.ExecuteReader
            If rd1.Read Then
            End If
            rd1.Close()
            cnn1.Close()
        Catch ex As Exception
            rd1.Close()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "ALTER TABLE productos add column DMembre VARCHAR(50) DEFAULT ''"
            cmd1.ExecuteNonQuery()
            cnn1.Close()
        End Try

        Try
            cnn1.Close()
            cnn1.Open()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "Select NotasCred from Formatos where Facturas='Lib'"
            rd1 = cmd1.ExecuteReader
            If rd1.Read Then
                rd1.Close()
            Else
                rd1.Close()
                cmd1 = cnn1.CreateCommand
                cmd1.CommandText = "Insert Into Formatos(Facturas,NotasCred,NumPart) values('Lib','0','0')"
                If cmd1.ExecuteNonQuery Then
                Else

                End If
            End If
            cnn1.Close()
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
            cnn1.Close()
        End Try

        Try
            cnn1.Close()
            cnn1.Open()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "SELECT Porcentaje FROM kits"
            rd1 = cmd1.ExecuteReader
            If rd1.Read Then
            End If
            rd1.Close()
            cnn1.Close()
        Catch ex As Exception
            rd1.Close()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "ALTER TABLE kits add column Porcentaje float DEFAULT '0'"
            cmd1.ExecuteNonQuery()
            cnn1.Close()
        End Try

        Try
            cnn1.Close()
            cnn1.Open()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "SELECT Cumple FROM monedero"
            rd1 = cmd1.ExecuteReader
            If rd1.Read Then
            End If
            rd1.Close()
            cnn1.Close()
        Catch ex As Exception
            rd1.Close()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "ALTER TABLE monedero add column Cumple date"
            cmd1.ExecuteNonQuery()
            cnn1.Close()
        End Try

        'AUXCOMPRAS
        Try
            cnn1.Close()
            cnn1.Open()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "SELECT Valor FROM auxcompras"
            rd1 = cmd1.ExecuteReader
            If rd1.Read Then
            End If
            rd1.Close()
            cnn1.Close()
        Catch ex As Exception
            rd1.Close()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "ALTER TABLE auxcompras add column Valor INT DEFAULT '1'"
            cmd1.ExecuteNonQuery()
            cnn1.Close()
        End Try

        ProgressBar1.Value = ProgressBar1.Value + 1
        My.Application.DoEvents()

        'msc
        Try
            cnn1.Close()
            cnn1.Open()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "SELECT Mesero FROM ventas"
            rd1 = cmd1.ExecuteReader
            If rd1.Read Then
            End If
            rd1.Close()
            cnn1.Close()
        Catch ex As Exception
            rd1.Close()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "ALTER TABLE ventas add column Mesero varchar(150) DEFAULT ''"
            cmd1.ExecuteNonQuery()
            cnn1.Close()
        End Try

        'falergia
        Try
            cnn1.Close()
            cnn1.Open()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "SELECT FAlergia FROM hisclinica"
            rd1 = cmd1.ExecuteReader
            If rd1.Read Then
            End If
            rd1.Close()
            cnn1.Close()
        Catch ex As Exception
            rd1.Close()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "ALTER TABLE hisclinica add column FAlergia varchar(50) NOT NULL DEFAULT '0'"
            cmd1.ExecuteNonQuery()
            cnn1.Close()
        End Try
        'msc
        Try
            cnn1.Close()
            cnn1.Open()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "SELECT MSC FROM hisclinica"
            rd1 = cmd1.ExecuteReader
            If rd1.Read Then
            End If
            rd1.Close()
            cnn1.Close()
        Catch ex As Exception
            rd1.Close()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "ALTER TABLE hisclinica add column MSC double NOT NULL DEFAULT '0'"
            cmd1.ExecuteNonQuery()
            cnn1.Close()
        End Try
        'n_serie2

        'promociones
        Try
            cnn1.Close()
            cnn1.Open()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "SELECT Promociones FROM productos"
            rd1 = cmd1.ExecuteReader
            If rd1.Read Then
            End If
            rd1.Close()
            cnn1.Close()
        Catch ex As Exception
            rd1.Close()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "ALTER TABLE productos add column Promociones int(1) DEFAULT '0'"
            cmd1.ExecuteNonQuery()
            cnn1.Close()
        End Try

        'trasladosdet
        Try
            cnn1.Close()
            cnn1.Open()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "SELECT N_Serie2 FROM productos"
            rd1 = cmd1.ExecuteReader
            If rd1.Read Then
            End If
            rd1.Close()
            cnn1.Close()
        Catch ex As Exception
            rd1.Close()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "ALTER TABLE productos add column N_Serie2 VARCHAR(150) DEFAULT ''"
            cmd1.ExecuteNonQuery()
            cnn1.Close()
        End Try

        ProgressBar1.Value = ProgressBar1.Value + 1
        My.Application.DoEvents()
        'codunico
        Try
            cnn1.Close()
            cnn1.Open()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "SELECT CodUnico FROM ventasdetalle"
            rd1 = cmd1.ExecuteReader
            If rd1.Read Then
            End If
            rd1.Close()
            cnn1.Close()
        Catch ex As Exception
            rd1.Close()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "ALTER TABLE ventasdetalle add column CodUnico VARCHAR(100) DEFAULT ''"
            cmd1.ExecuteNonQuery()
            cnn1.Close()
        End Try

        'trasladosdet
        Try
            cnn1.Close()
            cnn1.Open()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "SELECT Num_Traslado FROM trasladosdet"
            rd1 = cmd1.ExecuteReader
            If rd1.Read Then
            End If
            rd1.Close()
            cnn1.Close()
        Catch ex As Exception
            rd1.Close()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "ALTER TABLE trasladosdet add column Num_Traslado INT DEFAULT '0'"
            cmd1.ExecuteNonQuery()
            cnn1.Close()
        End Try

        Try
            cnn1.Close()
            cnn1.Open()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "SELECT Comisionista FROM trasladosdet"
            rd1 = cmd1.ExecuteReader
            If rd1.Read Then
            End If
            rd1.Close()
            cnn1.Close()
        Catch ex As Exception
            rd1.Close()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "ALTER TABLE trasladosdet add column Comisionista varchar(255) DEFAULT ''"
            cmd1.ExecuteNonQuery()
            cnn1.Close()
        End Try

        'devoluciones
        Try
            cnn1.Close()
            cnn1.Open()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "SELECT Comentario FROM devoluciones"
            rd1 = cmd1.ExecuteReader
            If rd1.Read Then
            End If
            rd1.Close()
            cnn1.Close()
        Catch ex As Exception
            rd1.Close()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "ALTER TABLE devoluciones add column Comentario varchar(255) DEFAULT ''"
            cmd1.ExecuteNonQuery()
            cnn1.Close()
        End Try

        'profesion
        Try
            cnn1.Close()
            cnn1.Open()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "SELECT Profesion FROM ctmedicos"
            rd1 = cmd1.ExecuteReader
            If rd1.Read Then
            End If
            rd1.Close()
            cnn1.Close()
        Catch ex As Exception
            rd1.Close()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "ALTER TABLE ctmedicos add column Profesion varchar(150) DEFAULT ''"
            cmd1.ExecuteNonQuery()
            cnn1.Close()
        End Try
        ProgressBar1.Value = ProgressBar1.Value + 1
        My.Application.DoEvents()

        'CEDULA
        Try
            cnn1.Close()
            cnn1.Open()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "SELECT Cedula FROM usuarios"
            rd1 = cmd1.ExecuteReader
            If rd1.Read Then
            End If
            rd1.Close()
            cnn1.Close()
        Catch ex As Exception
            rd1.Close()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "ALTER TABLE usuarios add column Cedula varchar(50) DEFAULT ''"
            cmd1.ExecuteNonQuery()
            cnn1.Close()
        End Try
        'escuela
        Try
            cnn1.Close()
            cnn1.Open()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "SELECT Escuela FROM usuarios"
            rd1 = cmd1.ExecuteReader
            If rd1.Read Then
            End If
            rd1.Close()
            cnn1.Close()
        Catch ex As Exception
            rd1.Close()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "ALTER TABLE usuarios add column Escuela varchar(255) DEFAULT ''"
            cmd1.ExecuteNonQuery()
            cnn1.Close()
        End Try
        'especialidad
        Try
            cnn1.Close()
            cnn1.Open()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "SELECT Especialidad FROM usuarios"
            rd1 = cmd1.ExecuteReader
            If rd1.Read Then
            End If
            rd1.Close()
            cnn1.Close()
        Catch ex As Exception
            rd1.Close()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "ALTER TABLE usuarios add column Especialidad varchar(255) DEFAULT ''"
            cmd1.ExecuteNonQuery()
            cnn1.Close()
        End Try
        'logor
        Try
            cnn1.Close()
            cnn1.Open()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "SELECT LogoR FROM usuarios"
            rd1 = cmd1.ExecuteReader
            If rd1.Read Then
            End If
            rd1.Close()
            cnn1.Close()
        Catch ex As Exception
            rd1.Close()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "ALTER TABLE usuarios add column LogoR varchar(50) DEFAULT ''"
            cmd1.ExecuteNonQuery()
            cnn1.Close()
        End Try
        'hismesa
        Try
            cnn1.Close()
            cnn1.Open()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "SELECT FH FROM hismesa"
            rd1 = cmd1.ExecuteReader
            If rd1.Read Then
            End If
            rd1.Close()
            cnn1.Close()
        Catch ex As Exception
            rd1.Close()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "ALTER TABLE hismesa add column FH datetime"
            cmd1.ExecuteNonQuery()
            cnn1.Close()
        End Try
        ProgressBar1.Value = ProgressBar1.Value + 1
        My.Application.DoEvents()
        'hismesa
        Try
            cnn1.Close()
            cnn1.Open()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "SELECT FA FROM hismesa"
            rd1 = cmd1.ExecuteReader
            If rd1.Read Then
            End If
            rd1.Close()
            cnn1.Close()
        Catch ex As Exception
            rd1.Close()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "ALTER TABLE hismesa add column FA datetime"
            cmd1.ExecuteNonQuery()
            cnn1.Close()
        End Try
        'ventas
        Try
            cnn1.Close()
            cnn1.Open()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "SELECT Comision FROM ventas"
            rd1 = cmd1.ExecuteReader
            If rd1.Read Then
            End If
            rd1.Close()
            cnn1.Close()
        Catch ex As Exception
            rd1.Close()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "ALTER TABLE ventas add column Comision double DEFAULT '0'"
            cmd1.ExecuteNonQuery()
            cnn1.Close()
        End Try
        'clientes
        Try
            cnn1.Close()
            cnn1.Open()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "SELECT NumCliente FROM clientes"
            rd1 = cmd1.ExecuteReader
            If rd1.Read Then
            End If
            rd1.Close()
            cnn1.Close()
        Catch ex As Exception
            rd1.Close()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "ALTER TABLE clientes add column NumCliente varchar(100) DEFAULT ''"
            cmd1.ExecuteNonQuery()
            cnn1.Close()
        End Try
        'PERMISOS
        Try
            cnn1.Close()
            cnn1.Open()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "SELECT ReimprimirTicket FROM permisos"
            rd1 = cmd1.ExecuteReader
            If rd1.Read Then
            End If
            rd1.Close()
            cnn1.Close()
        Catch ex As Exception
            rd1.Close()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "ALTER TABLE permisos add column ReimprimirTicket int(1) NOT NULL DEFAULT '0'"
            cmd1.ExecuteNonQuery()
            cnn1.Close()
        End Try

        Try
            cnn1.Close()
            cnn1.Open()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "SELECT Ad_Ruta FROM permisos"
            rd1 = cmd1.ExecuteReader
            If rd1.Read Then
            End If
            rd1.Close()
            cnn1.Close()
        Catch ex As Exception
            rd1.Close()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "ALTER TABLE permisos add column Ad_Ruta int(1) NOT NULL DEFAULT '0'"
            cmd1.ExecuteNonQuery()
            cnn1.Close()
        End Try
        ProgressBar1.Value = ProgressBar1.Value + 1
        My.Application.DoEvents()
        'pedidosvendet
        Try
            cnn1.Close()
            cnn1.Open()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "SELECT Comentario FROM pedidosvendet"
            rd1 = cmd1.ExecuteReader
            If rd1.Read Then
            End If
            rd1.Close()
            cnn1.Close()
        Catch ex As Exception
            rd1.Close()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "ALTER TABLE pedidosvendet add column Comentario varchar(255)"
            cmd1.ExecuteNonQuery()
            cnn1.Close()
        End Try

        'CotPedDet
        Try
            cnn1.Close()
            cnn1.Open()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "SELECT Comentario FROM CotPedDet"
            rd1 = cmd1.ExecuteReader
            If rd1.Read Then
            End If
            rd1.Close()
            cnn1.Close()
        Catch ex As Exception
            rd1.Close()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "ALTER TABLE CotPedDet add column Comentario varchar(255)"
            cmd1.ExecuteNonQuery()
            cnn1.Close()
        End Try

        'ventasdetalla
        Try
            cnn1.Close()
            cnn1.Open()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "SELECT FechaCompleta FROM ventasdetalle"
            rd1 = cmd1.ExecuteReader
            If rd1.Read Then
            End If
            rd1.Close()
            cnn1.Close()
        Catch ex As Exception
            rd1.Close()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "ALTER TABLE ventasdetalle add column FechaCompleta datetime"
            cmd1.ExecuteNonQuery()
            cnn1.Close()
        End Try

        'abonoe
        Try
            cnn1.Close()
            cnn1.Open()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "SELECT FechaCompleta FROM abonoe"
            rd1 = cmd1.ExecuteReader
            If rd1.Read Then
            End If
            rd1.Close()
            cnn1.Close()
        Catch ex As Exception
            rd1.Close()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "ALTER TABLE abonoe add column FechaCompleta datetime"
            cmd1.ExecuteNonQuery()
            cnn1.Close()
        End Try

        'abonoi
        Try
            cnn1.Close()
            cnn1.Open()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "SELECT FechaCompleta FROM abonoi"
            rd1 = cmd1.ExecuteReader
            If rd1.Read Then
            End If
            rd1.Close()
            cnn1.Close()
        Catch ex As Exception
            rd1.Close()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "ALTER TABLE abonoi add column FechaCompleta datetime"
            cmd1.ExecuteNonQuery()
            cnn1.Close()
        End Try

        ProgressBar1.Value = ProgressBar1.Value + 1
        My.Application.DoEvents()

        'productoeliminado
        Try
            cnn1.Close()
            cnn1.Open()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "SELECT Departamento FROM productoeliminado"
            rd1 = cmd1.ExecuteReader
            If rd1.Read Then
            End If
            rd1.Close()
            cnn1.Close()
        Catch ex As Exception
            rd1.Close()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "ALTER TABLE productoeliminado add column Departamento VARCHAR(100) default ''"
            cmd1.ExecuteNonQuery()
            cnn1.Close()
        End Try
        'clientes
        Try
            cnn1.Close()
            cnn1.Open()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "SELECT Id_Tienda FROM clientes"
            rd1 = cmd1.ExecuteReader
            If rd1.Read Then
            End If
            rd1.Close()
            cnn1.Close()
        Catch ex As Exception
            rd1.Close()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "ALTER TABLE clientes add column Id_Tienda INTEGER(11) default '0'"
            cmd1.ExecuteNonQuery()
            cnn1.Close()
        End Try
        'refaccionaria
        Try
            cnn1.Close()
            cnn1.Open()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "SELECT Motor FROM refaccionaria"
            rd1 = cmd1.ExecuteReader
            If rd1.Read Then
            End If
            rd1.Close()
            cnn1.Close()
        Catch ex As Exception
            rd1.Close()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "ALTER TABLE refaccionaria add column Motor varchar(255) default ''"
            cmd1.ExecuteNonQuery()
            cnn1.Close()
        End Try
        'vehiculo2
        Try
            cnn1.Close()
            cnn1.Open()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "SELECT Motor FROM vehiculo2"
            rd1 = cmd1.ExecuteReader
            If rd1.Read Then
            End If
            rd1.Close()
            cnn1.Close()
        Catch ex As Exception
            rd1.Close()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "ALTER TABLE vehiculo2 add column Motor varchar(255) default ''"
            cmd1.ExecuteNonQuery()
            cnn1.Close()
        End Try
        'permisos
        Try
            cnn1.Close()
            cnn1.Open()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "SELECT Precio FROM miprod"
            rd1 = cmd1.ExecuteReader
            If rd1.Read Then
            End If
            rd1.Close()
            cnn1.Close()
        Catch ex As Exception
            rd1.Close()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "ALTER TABLE miprod add column Precio double default '0'"
            cmd1.ExecuteNonQuery()
            cnn1.Close()
        End Try
        ProgressBar1.Value = ProgressBar1.Value + 1
        My.Application.DoEvents()

        Try
            cnn1.Close()
            cnn1.Open()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "SELECT PrecioIVA FROM miprod"
            rd1 = cmd1.ExecuteReader
            If rd1.Read Then
            End If
            rd1.Close()
            cnn1.Close()
        Catch ex As Exception
            rd1.Close()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "ALTER TABLE miprod add column PrecioIVA double default '0'"
            cmd1.ExecuteNonQuery()
            cnn1.Close()
        End Try

        'permisos
        Try
            cnn1.Close()
            cnn1.Open()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "SELECT EliAbono FROM permisos"
            rd1 = cmd1.ExecuteReader
            If rd1.Read Then
            End If
            rd1.Close()
            cnn1.Close()
        Catch ex As Exception
            rd1.Close()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "ALTER TABLE permisos add column EliAbono int(1) default '0'"
            cmd1.ExecuteNonQuery()
            cnn1.Close()
        End Try

        'abonoi
        Try
            cnn1.Close()
            cnn1.Open()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "SELECT Status FROM abonoi"
            rd1 = cmd1.ExecuteReader
            If rd1.Read Then
            End If
            rd1.Close()
            cnn1.Close()
        Catch ex As Exception
            rd1.Close()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "ALTER TABLE abonoi add column Status int default '0'"
            cmd1.ExecuteNonQuery()
            cnn1.Close()
        End Try

        'movcuenta
        Try
            cnn1.Close()
            cnn1.Open()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "SELECT Cliente FROM movcuenta"
            rd1 = cmd1.ExecuteReader
            If rd1.Read Then
            End If
            rd1.Close()
            cnn1.Close()
        Catch ex As Exception
            rd1.Close()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "ALTER TABLE movcuenta add column Cliente varchar(50) default ''"
            cmd1.ExecuteNonQuery()
            cnn1.Close()
        End Try

        Try
            cnn1.Close()
            cnn1.Open()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "SELECT Saldo FROM movcuenta"
            rd1 = cmd1.ExecuteReader
            If rd1.Read Then
            End If
            rd1.Close()
            cnn1.Close()
        Catch ex As Exception
            rd1.Close()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "ALTER TABLE movcuenta add column Saldo double default '0'"
            cmd1.ExecuteNonQuery()
            cnn1.Close()
        End Try

        ProgressBar1.Value = ProgressBar1.Value + 1
        My.Application.DoEvents()

        'saldosempleados
        Try
            cnn1.Close()
            cnn1.Open()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "SELECT FormaPago FROM saldosempleados"
            rd1 = cmd1.ExecuteReader
            If rd1.Read Then
            End If
            rd1.Close()
            cnn1.Close()
        Catch ex As Exception
            rd1.Close()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "ALTER TABLE saldosempleados add column FormaPago varchar(50) default ''"
            cmd1.ExecuteNonQuery()
            cnn1.Close()
        End Try

        Try
            cnn1.Close()
            cnn1.Open()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "SELECT Banco FROM saldosempleados"
            rd1 = cmd1.ExecuteReader
            If rd1.Read Then
            End If
            rd1.Close()
            cnn1.Close()
        Catch ex As Exception
            rd1.Close()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "ALTER TABLE saldosempleados add column Banco varchar(50) default ''"
            cmd1.ExecuteNonQuery()
            cnn1.Close()
        End Try

        Try
            cnn1.Close()
            cnn1.Open()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "SELECT Referencia FROM saldosempleados"
            rd1 = cmd1.ExecuteReader
            If rd1.Read Then
            End If
            rd1.Close()
            cnn1.Close()
        Catch ex As Exception
            rd1.Close()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "ALTER TABLE saldosempleados add column Referencia varchar(50) default ''"
            cmd1.ExecuteNonQuery()
            cnn1.Close()
        End Try

        Try
            cnn1.Close()
            cnn1.Open()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "SELECT Comentario FROM saldosempleados"
            rd1 = cmd1.ExecuteReader
            If rd1.Read Then
            End If
            rd1.Close()
            cnn1.Close()
        Catch ex As Exception
            rd1.Close()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "ALTER TABLE saldosempleados add column Comentario varchar(255) default ''"
            cmd1.ExecuteNonQuery()
            cnn1.Close()
        End Try

        Try
            cnn1.Close()
            cnn1.Open()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "SELECT Cuenta FROM saldosempleados"
            rd1 = cmd1.ExecuteReader
            If rd1.Read Then
            End If
            rd1.Close()
            cnn1.Close()
        Catch ex As Exception
            rd1.Close()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "ALTER TABLE saldosempleados add column Cuenta varchar(50) default ''"
            cmd1.ExecuteNonQuery()
            cnn1.Close()
        End Try

        ProgressBar1.Value = ProgressBar1.Value + 1
        My.Application.DoEvents()

        Try
            cnn1.Close()
            cnn1.Open()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "SELECT BancoC FROM saldosempleados"
            rd1 = cmd1.ExecuteReader
            If rd1.Read Then
            End If
            rd1.Close()
            cnn1.Close()
        Catch ex As Exception
            rd1.Close()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "ALTER TABLE saldosempleados add column BancoC varchar(50) default ''"
            cmd1.ExecuteNonQuery()
            cnn1.Close()
        End Try

        'OTROSGASTOS
        Try
            cnn1.Close()
            cnn1.Open()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "SELECT FormaPago FROM otrosgastos"
            rd1 = cmd1.ExecuteReader
            If rd1.Read Then
            End If
            rd1.Close()
            cnn1.Close()
        Catch ex As Exception
            rd1.Close()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "ALTER TABLE otrosgastos add column FormaPago varchar(50) default ''"
            cmd1.ExecuteNonQuery()
            cnn1.Close()
        End Try

        Try
            cnn1.Close()
            cnn1.Open()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "SELECT Monto FROM otrosgastos"
            rd1 = cmd1.ExecuteReader
            If rd1.Read Then
            End If
            rd1.Close()
            cnn1.Close()
        Catch ex As Exception
            rd1.Close()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "ALTER TABLE otrosgastos add column Monto double default '0'"
            cmd1.ExecuteNonQuery()
            cnn1.Close()
        End Try

        Try
            cnn1.Close()
            cnn1.Open()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "SELECT Banco FROM otrosgastos"
            rd1 = cmd1.ExecuteReader
            If rd1.Read Then
            End If
            rd1.Close()
            cnn1.Close()
        Catch ex As Exception
            rd1.Close()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "ALTER TABLE otrosgastos add column Banco varchar(100) default ''"
            cmd1.ExecuteNonQuery()
            cnn1.Close()
        End Try

        Try
            cnn1.Close()
            cnn1.Open()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "SELECT Referencia FROM otrosgastos"
            rd1 = cmd1.ExecuteReader
            If rd1.Read Then
            End If
            rd1.Close()
            cnn1.Close()
        Catch ex As Exception
            rd1.Close()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "ALTER TABLE otrosgastos add column Referencia varchar(100) default ''"
            cmd1.ExecuteNonQuery()
            cnn1.Close()
        End Try

        ProgressBar1.Value = ProgressBar1.Value + 1
        My.Application.DoEvents()

        Try
            cnn1.Close()
            cnn1.Open()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "SELECT Comentario FROM otrosgastos"
            rd1 = cmd1.ExecuteReader
            If rd1.Read Then
            End If
            rd1.Close()
            cnn1.Close()
        Catch ex As Exception
            rd1.Close()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "ALTER TABLE otrosgastos add column Comentario varchar(100) default ''"
            cmd1.ExecuteNonQuery()
            cnn1.Close()
        End Try

        Try
            cnn1.Close()
            cnn1.Open()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "SELECT CuentaC FROM otrosgastos"
            rd1 = cmd1.ExecuteReader
            If rd1.Read Then
            End If
            rd1.Close()
            cnn1.Close()
        Catch ex As Exception
            rd1.Close()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "ALTER TABLE otrosgastos add column CuentaC varchar(100) default ''"
            cmd1.ExecuteNonQuery()
            cnn1.Close()
        End Try

        Try
            cnn1.Close()
            cnn1.Open()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "SELECT BancoC FROM otrosgastos"
            rd1 = cmd1.ExecuteReader
            If rd1.Read Then
            End If
            rd1.Close()
            cnn1.Close()
        Catch ex As Exception
            rd1.Close()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "ALTER TABLE otrosgastos add column BancoC varchar(100) default ''"
            cmd1.ExecuteNonQuery()
            cnn1.Close()
        End Try

        'abonoe
        Try
            cnn1.Close()
            cnn1.Open()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "SELECT FormaPago FROM abonoe"
            rd1 = cmd1.ExecuteReader
            If rd1.Read Then
            End If
            rd1.Close()
            cnn1.Close()
        Catch ex As Exception
            rd1.Close()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "ALTER TABLE abonoe add column FormaPago varchar(100) default ''"
            cmd1.ExecuteNonQuery()
            cnn1.Close()
        End Try

        Try
            cnn1.Close()
            cnn1.Open()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "SELECT Monto FROM abonoe"
            rd1 = cmd1.ExecuteReader
            If rd1.Read Then
            End If
            rd1.Close()
            cnn1.Close()
        Catch ex As Exception
            rd1.Close()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "ALTER TABLE abonoe add column Monto double default '0'"
            cmd1.ExecuteNonQuery()
            cnn1.Close()
        End Try

        ProgressBar1.Value = ProgressBar1.Value + 1
        My.Application.DoEvents()

        Try
            cnn1.Close()
            cnn1.Open()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "SELECT Comentario FROM abonoe"
            rd1 = cmd1.ExecuteReader
            If rd1.Read Then
            End If
            rd1.Close()
            cnn1.Close()
        Catch ex As Exception
            rd1.Close()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "ALTER TABLE abonoe add column Comentario varchar(255) default ''"
            cmd1.ExecuteNonQuery()
            cnn1.Close()
        End Try

        Try
            cnn1.Close()
            cnn1.Open()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "SELECT CuentaRep FROM abonoe"
            rd1 = cmd1.ExecuteReader
            If rd1.Read Then
            End If
            rd1.Close()
            cnn1.Close()
        Catch ex As Exception
            rd1.Close()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "ALTER TABLE abonoe add column CuentaRep varchar(100) default ''"
            cmd1.ExecuteNonQuery()
            cnn1.Close()
        End Try

        Try
            cnn1.Close()
            cnn1.Open()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "SELECT BancoRep FROM abonoe"
            rd1 = cmd1.ExecuteReader
            If rd1.Read Then
            End If
            rd1.Close()
            cnn1.Close()
        Catch ex As Exception
            rd1.Close()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "ALTER TABLE abonoe add column BancoRep varchar(100) default ''"
            cmd1.ExecuteNonQuery()
            cnn1.Close()
        End Try

        'abonoi
        Try
            cnn1.Close()
            cnn1.Open()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "SELECT Mesero FROM abonoi"
            rd1 = cmd1.ExecuteReader
            If rd1.Read Then
            End If
            rd1.Close()
            cnn1.Close()
        Catch ex As Exception
            rd1.Close()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "ALTER TABLE abonoi add column Mesero varchar(100) default ''"
            cmd1.ExecuteNonQuery()
            cnn1.Close()
        End Try

        Try
            cnn1.Close()
            cnn1.Open()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "SELECT Parcialidad FROM abonoi"
            rd1 = cmd1.ExecuteReader
            If rd1.Read Then
            End If
            rd1.Close()
            cnn1.Close()
        Catch ex As Exception
            rd1.Close()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "ALTER TABLE abonoi add column Parcialidad varchar(100) default ''"
            cmd1.ExecuteNonQuery()
            cnn1.Close()
        End Try

        ProgressBar1.Value = ProgressBar1.Value + 1
        My.Application.DoEvents()

        Try
            cnn1.Close()
            cnn1.Open()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "SELECT Descuento FROM abonoi"
            rd1 = cmd1.ExecuteReader
            If rd1.Read Then
            End If
            rd1.Close()
            cnn1.Close()
        Catch ex As Exception
            rd1.Close()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "ALTER TABLE abonoi add column Descuento float default '0'"
            cmd1.ExecuteNonQuery()
            cnn1.Close()
        End Try

        Try
            cnn1.Close()
            cnn1.Open()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "SELECT cat_Formas FROM Permisos"
            rd1 = cmd1.ExecuteReader
            If rd1.Read Then
            End If
            rd1.Close()
            cnn1.Close()
        Catch ex As Exception
            rd1.Close()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "ALTER TABLE Permisos add column cat_Formas int(11) default 0"
            cmd1.ExecuteNonQuery()
            cnn1.Close()
        End Try

        Try
            cnn1.Close()
            cnn1.Open()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "SELECT cat_Bancos FROM Permisos"
            rd1 = cmd1.ExecuteReader
            If rd1.Read Then
            End If
            rd1.Close()
            cnn1.Close()
        Catch ex As Exception
            rd1.Close()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "ALTER TABLE Permisos add column cat_Bancos int(11) default 0"
            cmd1.ExecuteNonQuery()
            cnn1.Close()
        End Try

        Try
            cnn1.Close()
            cnn1.Open()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "SELECT cat_Cuentas FROM Permisos"
            rd1 = cmd1.ExecuteReader
            If rd1.Read Then
            End If
            rd1.Close()
            cnn1.Close()
        Catch ex As Exception
            rd1.Close()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "ALTER TABLE Permisos add column cat_Cuentas int(11) default 0"
            cmd1.ExecuteNonQuery()
            cnn1.Close()
        End Try

        Try
            cnn1.Close()
            cnn1.Open()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "Select Valor from FormasPago"
            rd1 = cmd1.ExecuteReader
            If rd1.Read Then
            End If
            rd1.Close()
            cnn1.Close()
        Catch ex As Exception
            rd1.Close()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "Alter table FormasPago add column Valor varchar(255)"
            cmd1.ExecuteNonQuery()
            cnn1.Close()
        End Try

        ProgressBar1.Value = ProgressBar1.Value + 1
        My.Application.DoEvents()

        Try
            cnn1.Close()
            cnn1.Open()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "Select NoPrintCom from Ticket"
            rd1 = cmd1.ExecuteReader
            If rd1.Read Then
            End If
            rd1.Close()
            cnn1.Close()
        Catch ex As Exception
            rd1.Close()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "Alter table Ticket add column NoPrintCom int(11) default 0"
            cmd1.ExecuteNonQuery()
            cnn1.Close()
        End Try
        'permisos
        Try
            cnn1.Close()
            cnn1.Open()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "Select Rep_Servicios from Permisos"
            rd1 = cmd1.ExecuteReader
            If rd1.Read Then
            End If
            rd1.Close()
            cnn1.Close()
        Catch ex As Exception
            rd1.Close()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "Alter table Permisos add column Rep_Servicios int(1) default 0"
            cmd1.ExecuteNonQuery()
            cnn1.Close()
        End Try

        Try
            cnn1.Close()
            cnn1.Open()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "Select Rep_CamPrecio from Permisos"
            rd1 = cmd1.ExecuteReader
            If rd1.Read Then
            End If
            rd1.Close()
            cnn1.Close()
        Catch ex As Exception
            rd1.Close()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "Alter table Permisos add column Rep_CamPrecio int(1) default 0"
            cmd1.ExecuteNonQuery()
            cnn1.Close()
        End Try

        Try
            cnn1.Close()
            cnn1.Open()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "SELECT Rep_EstResultado FROM Permisos"
            rd1 = cmd1.ExecuteReader
            If rd1.Read Then
            End If
            rd1.Close()
            cnn1.Close()
        Catch ex As Exception
            rd1.Close()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "ALTER TABLE Permisos ADD column Rep_EstResultado int(1) default 0"
            cmd1.ExecuteNonQuery()
            cnn1.Close()
        End Try

        Try
            cnn1.Close()
            cnn1.Open()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "SELECT Rep_Auditoria FROM Permisos"
            rd1 = cmd1.ExecuteReader
            If rd1.Read Then
            End If
            rd1.Close()
            cnn1.Close()
        Catch ex As Exception
            rd1.Close()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "ALTER TABLE Permisos ADD column Rep_Auditoria int(1) default 0"
            cmd1.ExecuteNonQuery()
            cnn1.Close()
        End Try

        ProgressBar1.Value = ProgressBar1.Value + 1
        My.Application.DoEvents()

        'ticket
        Try
            cnn1.Close()
            cnn1.Open()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "Select Cab7 from Ticket"
            rd1 = cmd1.ExecuteReader
            If rd1.Read Then
            End If
            rd1.Close()
            cnn1.Close()
        Catch ex As Exception
            rd1.Close()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "Alter table Ticket add column Cab7 varchar(255) default ''"
            cmd1.ExecuteNonQuery()
            cnn1.Close()
        End Try

        'compras
        Try
            cnn1.Close() : cnn1.Open()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "SELECT Serie FROM comprasdet"
            rd1 = cmd1.ExecuteReader
            If rd1.Read Then
            End If
            rd1.Close() : cnn1.Close()
        Catch ex As Exception
            rd1.Close()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "ALTER TABLE comprasdet add column Serie varchar(80) default ''"
            cmd1.ExecuteNonQuery()
            cnn1.Close()
        End Try

        'mesa
        Try
            cnn1.Close() : cnn1.Open()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "SELECT Impresion FROM mesa"
            rd1 = cmd1.ExecuteReader
            If rd1.Read Then
            End If
            rd1.Close() : cnn1.Close()
        Catch ex As Exception
            rd1.Close()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "ALTER TABLE mesa add column Impresion int(11) default 0"
            cmd1.ExecuteNonQuery()
            cnn1.Close()
        End Try

        'mesa
        Try
            cnn1.Close() : cnn1.Open()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "SELECT Color FROM usuarios"
            rd1 = cmd1.ExecuteReader
            If rd1.Read Then
            End If
            rd1.Close() : cnn1.Close()
        Catch ex As Exception
            rd1.Close()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "ALTER TABLE usuarios add column Color varchar(100) default ''"
            cmd1.ExecuteNonQuery()
            cnn1.Close()
        End Try

        ''permisosm
        Try
            cnn1.Close() : cnn1.Open()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "SELECT Mesas FROM permisosm"
            rd1 = cmd1.ExecuteReader
            If rd1.Read Then
            End If
            rd1.Close() : cnn1.Close()
        Catch ex As Exception
            rd1.Close()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "ALTER TABLE permisosm add column Mesas INT(1) default '0'"
            cmd1.ExecuteNonQuery()
            cnn1.Close()
        End Try

        ProgressBar1.Value = ProgressBar1.Value + 1
        My.Application.DoEvents()

        'MESASXEMPLEADOS
        Try
            cnn1.Close() : cnn1.Open()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "SELECT Letra FROM mesasxempleados"
            rd1 = cmd1.ExecuteReader
            If rd1.Read Then
            End If
            rd1.Close() : cnn1.Close()
        Catch ex As Exception
            rd1.Close()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "ALTER TABLE mesasxempleados add column Letra VARCHAR(10) default ''"
            cmd1.ExecuteNonQuery()
            cnn1.Close()
        End Try
        'cartaporte
        Try
            cnn1.Close() : cnn1.Open()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "SELECT PesoBrutoVehicular FROM cartaporte"
            rd1 = cmd1.ExecuteReader
            If rd1.Read Then
            End If
            rd1.Close() : cnn1.Close()
        Catch ex As Exception
            rd1.Close()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "ALTER TABLE cartaporte add column PesoBrutoVehicular varchar(100) default '0'"
            cmd1.ExecuteNonQuery()
            cnn1.Close()
        End Try
        'cartaporte1
        Try
            cnn1.Close() : cnn1.Open()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "SELECT PesoBrutoVehicular FROM cartaportei"
            rd1 = cmd1.ExecuteReader
            If rd1.Read Then
            End If
            rd1.Close() : cnn1.Close()
        Catch ex As Exception
            rd1.Close()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "ALTER TABLE cartaportei add column PesoBrutoVehicular varchar(100) default '0'"
            cmd1.ExecuteNonQuery()
            cnn1.Close()
        End Try
        'productos
        Try
            cnn1.Close() : cnn1.Open()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "SELECT PrecioVentaIVA2 FROM productos"
            rd1 = cmd1.ExecuteReader
            If rd1.Read Then
            End If
            rd1.Close() : cnn1.Close()
        Catch ex As Exception
            rd1.Close()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "ALTER TABLE productos add column PrecioVentaIVA2 float default '0'"
            cmd1.ExecuteNonQuery()
            cnn1.Close()
        End Try

        Try
            cnn1.Close()
            cnn1.Open()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "SELECT T_Entrega FROM productos"
            rd1 = cmd1.ExecuteReader
            If rd1.Read Then
            End If
            rd1.Close()
            cnn1.Close()
        Catch ex As Exception
            rd1.Close()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "ALTER TABLE productos add column T_Entrega float default '0'"
            cmd1.ExecuteNonQuery()
            cnn1.Close()
        End Try

        ProgressBar1.Value = ProgressBar1.Value + 1
        My.Application.DoEvents()

        Try
            cnn1.Close() : cnn1.Open()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "SELECT Resumen FROM productos"
            rd1 = cmd1.ExecuteReader
            If rd1.Read Then
            End If
            rd1.Close() : cnn1.Close()
        Catch ex As Exception
            rd1.Close()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "ALTER TABLE productos add column Resumen varchar(250) default ''"
            cmd1.ExecuteNonQuery()
            cnn1.Close()
        End Try

        Try
            cnn1.Close() : cnn1.Open()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "SELECT Descripcion_Tienda FROM productos"
            rd1 = cmd1.ExecuteReader
            If rd1.Read Then
            End If
            rd1.Close() : cnn1.Close()
        Catch ex As Exception
            rd1.Close()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "ALTER TABLE productos add column Descripcion_Tienda text"
            cmd1.ExecuteNonQuery()
            cnn1.Close()
        End Try


        Try
            cnn1.Close()
            cnn1.Open()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "SELECT Actu FROM Productos"
            rd1 = cmd1.ExecuteReader
            If rd1.Read Then
            End If
            rd1.Close()
            cnn1.Close()
        Catch ex As Exception
            rd1.Close()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "ALTER TABLE productos add column Actu Int(1) default 0"
            cmd1.ExecuteNonQuery()
            cnn1.Close()
        End Try



        Try
            cnn1.Close()
            cnn1.Open()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "SELECT Descripcion FROM productos"
            rd1 = cmd1.ExecuteReader
            If rd1.Read Then
            End If
            rd1.Close()
            cnn1.Close()
        Catch ex As Exception
            rd1.Close()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "ALTER TABLE productos add column Descripcion text default ''"
            cmd1.ExecuteNonQuery()
            cnn1.Close()
        End Try

        Try
            cnn1.Close() : cnn1.Open()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "SELECT Mililitros FROM productos"
            rd1 = cmd1.ExecuteReader
            If rd1.Read Then
            End If
            rd1.Close() : cnn1.Close()
        Catch ex As Exception
            rd1.Close()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "ALTER TABLE productos add column Mililitros float default '0'"
            cmd1.ExecuteNonQuery()
            cnn1.Close()
        End Try


        Try
            cnn1.Close() : cnn1.Open()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "SELECT Copas FROM productos"
            rd1 = cmd1.ExecuteReader
            If rd1.Read Then
            End If
            rd1.Close() : cnn1.Close()
        Catch ex As Exception
            rd1.Close()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "ALTER TABLE productos add column Copas float default '0'"
            cmd1.ExecuteNonQuery()
            cnn1.Close()
        End Try



        Try
            cnn1.Close() : cnn1.Open()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "SELECT CodBarra1 FROM productos"
            rd1 = cmd1.ExecuteReader
            If rd1.Read Then
            End If
            rd1.Close() : cnn1.Close()
        Catch ex As Exception
            rd1.Close()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "ALTER TABLE productos add column CodBarra1 varchar(50) default ''"
            cmd1.ExecuteNonQuery()
            cnn1.Close()
        End Try

        Try
            cnn1.Close() : cnn1.Open()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "SELECT CodBarra2 FROM productos"
            rd1 = cmd1.ExecuteReader
            If rd1.Read Then
            End If
            rd1.Close() : cnn1.Close()
        Catch ex As Exception
            rd1.Close()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "ALTER TABLE productos add column CodBarra2 varchar(50) default ''"
            cmd1.ExecuteNonQuery()
            cnn1.Close()
        End Try

        Try
            cnn1.Close() : cnn1.Open()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "SELECT CodBarra3 FROM productos"
            rd1 = cmd1.ExecuteReader
            If rd1.Read Then
            End If
            rd1.Close() : cnn1.Close()
        Catch ex As Exception
            rd1.Close()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "ALTER TABLE productos add column CodBarra3 varchar(50) default ''"
            cmd1.ExecuteNonQuery()
            cnn1.Close()
        End Try

        Try
            cnn1.Close() : cnn1.Open()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "SELECT PrecioVenta2 FROM productos"
            rd1 = cmd1.ExecuteReader
            If rd1.Read Then
            End If
            rd1.Close() : cnn1.Close()
        Catch ex As Exception
            rd1.Close()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "ALTER TABLE productos add column PrecioVenta2 float default '0'"
            cmd1.ExecuteNonQuery()
            cnn1.Close()
        End Try



        Try
            cnn1.Close() : cnn1.Open()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "SELECT PorcMin2 FROM productos"
            rd1 = cmd1.ExecuteReader
            If rd1.Read Then
            End If
            rd1.Close() : cnn1.Close()
        Catch ex As Exception
            rd1.Close()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "ALTER TABLE productos add column PorcMin2 float default '0'"
            cmd1.ExecuteNonQuery()
            cnn1.Close()
        End Try

        Try
            cnn1.Close() : cnn1.Open()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "SELECT PreMin2 FROM productos"
            rd1 = cmd1.ExecuteReader
            If rd1.Read Then
            End If
            rd1.Close() : cnn1.Close()
        Catch ex As Exception
            rd1.Close()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "ALTER TABLE productos add column PreMin2 float default '0'"
            cmd1.ExecuteNonQuery()
            cnn1.Close()
        End Try

        Try
            cnn1.Close() : cnn1.Open()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "SELECT PorcMay2 FROM productos"
            rd1 = cmd1.ExecuteReader
            If rd1.Read Then
            End If
            rd1.Close() : cnn1.Close()
        Catch ex As Exception
            rd1.Close()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "ALTER TABLE productos add column PorcMay2 float default '0'"
            cmd1.ExecuteNonQuery()
            cnn1.Close()
        End Try

        Try
            cnn1.Close() : cnn1.Open()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "SELECT PorcMM2 FROM productos"
            rd1 = cmd1.ExecuteReader
            If rd1.Read Then
            End If
            rd1.Close() : cnn1.Close()
        Catch ex As Exception
            rd1.Close()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "ALTER TABLE productos add column PorcMM2 float default '0'"
            cmd1.ExecuteNonQuery()
            cnn1.Close()
        End Try



        Try
            cnn1.Close() : cnn1.Open()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "SELECT PorcEsp2 FROM productos"
            rd1 = cmd1.ExecuteReader
            If rd1.Read Then
            End If
            rd1.Close() : cnn1.Close()
        Catch ex As Exception
            rd1.Close()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "ALTER TABLE productos add column PorcEsp2 float default '0'"
            cmd1.ExecuteNonQuery()
            cnn1.Close()
        End Try

        Try
            cnn1.Close() : cnn1.Open()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "SELECT PreMay2 FROM productos"
            rd1 = cmd1.ExecuteReader
            If rd1.Read Then
            End If
            rd1.Close() : cnn1.Close()
        Catch ex As Exception
            rd1.Close()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "ALTER TABLE productos add column PreMay2 float default '0'"
            cmd1.ExecuteNonQuery()
            cnn1.Close()
        End Try



        Try
            cnn1.Close() : cnn1.Open()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "SELECT PreMM2 FROM productos"
            rd1 = cmd1.ExecuteReader
            If rd1.Read Then
            End If
            rd1.Close() : cnn1.Close()
        Catch ex As Exception
            rd1.Close()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "ALTER TABLE productos add column PreMM2 float default '0'"
            cmd1.ExecuteNonQuery()
            cnn1.Close()
        End Try

        Try
            cnn1.Close() : cnn1.Open()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "SELECT PreEsp2 FROM productos"
            rd1 = cmd1.ExecuteReader
            If rd1.Read Then
            End If
            rd1.Close() : cnn1.Close()
        Catch ex As Exception
            rd1.Close()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "ALTER TABLE productos add column PreEsp2 float default '0'"
            cmd1.ExecuteNonQuery()
            cnn1.Close()
        End Try



        Try
            cnn1.Close() : cnn1.Open()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "SELECT CantMin3 FROM productos"
            rd1 = cmd1.ExecuteReader
            If rd1.Read Then
            End If
            rd1.Close() : cnn1.Close()
        Catch ex As Exception
            rd1.Close()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "ALTER TABLE productos add column CantMin3 float default '0'"
            cmd1.ExecuteNonQuery()
            cnn1.Close()
        End Try

        Try
            cnn1.Close() : cnn1.Open()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "SELECT CantMin4 FROM productos"
            rd1 = cmd1.ExecuteReader
            If rd1.Read Then
            End If
            rd1.Close() : cnn1.Close()
        Catch ex As Exception
            rd1.Close()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "ALTER TABLE productos add column CantMin4 float default '0'"
            cmd1.ExecuteNonQuery()
            cnn1.Close()
        End Try


        Try
            cnn1.Close() : cnn1.Open()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "SELECT CantMay3 FROM productos"
            rd1 = cmd1.ExecuteReader
            If rd1.Read Then
            End If
            rd1.Close() : cnn1.Close()
        Catch ex As Exception
            rd1.Close()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "ALTER TABLE productos add column CantMay3 float default '0'"
            cmd1.ExecuteNonQuery()
            cnn1.Close()
        End Try

        Try
            cnn1.Close() : cnn1.Open()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "SELECT CantMay4 FROM productos"
            rd1 = cmd1.ExecuteReader
            If rd1.Read Then
            End If
            rd1.Close() : cnn1.Close()
        Catch ex As Exception
            rd1.Close()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "ALTER TABLE productos add column CantMay4 float default '0'"
            cmd1.ExecuteNonQuery()
            cnn1.Close()
        End Try

        Try
            cnn1.Close() : cnn1.Open()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "SELECT CantMM3 FROM productos"
            rd1 = cmd1.ExecuteReader
            If rd1.Read Then
            End If
            rd1.Close() : cnn1.Close()
        Catch ex As Exception
            rd1.Close()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "ALTER TABLE productos add column CantMM3 float default '0'"
            cmd1.ExecuteNonQuery()
            cnn1.Close()
        End Try

        Try
            cnn1.Close() : cnn1.Open()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "SELECT CantMM4 FROM productos"
            rd1 = cmd1.ExecuteReader
            If rd1.Read Then
            End If
            rd1.Close() : cnn1.Close()
        Catch ex As Exception
            rd1.Close()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "ALTER TABLE productos add column CantMM4 float default '0'"
            cmd1.ExecuteNonQuery()
            cnn1.Close()
        End Try



        Try
            cnn1.Close() : cnn1.Open()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "SELECT CantEsp3 FROM productos"
            rd1 = cmd1.ExecuteReader
            If rd1.Read Then
            End If
            rd1.Close() : cnn1.Close()
        Catch ex As Exception
            rd1.Close()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "ALTER TABLE productos add column CantEsp3 float default '0'"
            cmd1.ExecuteNonQuery()
            cnn1.Close()
        End Try

        Try
            cnn1.Close() : cnn1.Open()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "SELECT CantEsp4 FROM productos"
            rd1 = cmd1.ExecuteReader
            If rd1.Read Then
            End If
            rd1.Close() : cnn1.Close()
        Catch ex As Exception
            rd1.Close()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "ALTER TABLE productos add column CantEsp4 float default '0'"
            cmd1.ExecuteNonQuery()
            cnn1.Close()
        End Try

        Try
            cnn1.Close() : cnn1.Open()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "SELECT CantLst3 FROM productos"
            rd1 = cmd1.ExecuteReader
            If rd1.Read Then
            End If
            rd1.Close() : cnn1.Close()
        Catch ex As Exception
            rd1.Close()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "ALTER TABLE productos add column CantLst3 float default '0'"
            cmd1.ExecuteNonQuery()
            cnn1.Close()
        End Try

        Try
            cnn1.Close() : cnn1.Open()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "SELECT CantLst4 FROM productos"
            rd1 = cmd1.ExecuteReader
            If rd1.Read Then
            End If
            rd1.Close() : cnn1.Close()
        Catch ex As Exception
            rd1.Close()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "ALTER TABLE productos add column CantLst4 float default '0'"
            cmd1.ExecuteNonQuery()
            cnn1.Close()
        End Try

        Try
            cnn1.Close() : cnn1.Open()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "SELECT Porcentaje2 FROM productos"
            rd1 = cmd1.ExecuteReader
            If rd1.Read Then
            End If
            rd1.Close() : cnn1.Close()
        Catch ex As Exception
            rd1.Close()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "ALTER TABLE productos add column Porcentaje2 float default '0'"
            cmd1.ExecuteNonQuery()
            cnn1.Close()
        End Try

        'usuarios
        Try
            cnn1.Close()
            cnn1.Open()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "Select P1,P2,P3 from Usuarios"
            rd1 = cmd1.ExecuteReader
            If rd1.Read Then
            End If
            rd1.Close()
            cnn1.Close()
        Catch ex As Exception
            rd1.Close()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "Alter table Usuarios add column P1 varchar(255)"
            cmd1.ExecuteNonQuery()
            cmd1.CommandText = "Alter table Usuarios add column P2 varchar(255)"
            cmd1.ExecuteNonQuery()
            cmd1.CommandText = "Alter table Usuarios add column P3 varchar(255)"
            cmd1.ExecuteNonQuery()
        End Try



        'clientes
        Try
            cnn1.Close() : cnn1.Open()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "SELECT Observaciones FROM clientes"
            rd1 = cmd1.ExecuteReader
            If rd1.Read Then
            End If
            rd1.Close() : cnn1.Close()
        Catch ex As Exception
            rd1.Close()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "ALTER TABLE clientes add column Observaciones varchar(150) default ''"
            cmd1.ExecuteNonQuery()
            cnn1.Close()
        End Try

        Try
            cnn1.Close()
            cnn1.Open()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "SELECT Referencia FROM Clientes"
            rd1 = cmd1.ExecuteReader
            If rd1.Read Then
            End If
            rd1.Close()
            cnn1.Close()
        Catch ex As Exception
            rd1.Close()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "ALTER TABLE Clientes add column Referencia varchar(255) default ''"
            cmd1.ExecuteNonQuery()
            cnn1.Close()
        End Try

        'ventas
        Try
            cnn1.Close() : cnn1.Open()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "SELECT Pedido FROM ventas"
            rd1 = cmd1.ExecuteReader
            If rd1.Read Then
            End If
            rd1.Close() : cnn1.Close()
        Catch ex As Exception
            rd1.Close()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "ALTER TABLE ventas add column Pedido int default '0'"
            cmd1.ExecuteNonQuery()
            cnn1.Close()
        End Try

        Try
            cnn1.Close()
            cnn1.Open()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "SELECT Fecha FROM ventas"
            rd1 = cmd1.ExecuteReader
            If rd1.Read Then
            End If
            rd1.Close()
            cnn1.Close()
        Catch ex As Exception
            rd1.Close()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "ALTER TABLE ventas add column Fecha datetime"
            cmd1.ExecuteNonQuery()
            cnn1.Close()
        End Try

        Try
            cnn1.Close()
            cnn1.Open()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "SELECT Consignar FROM ventas"
            rd1 = cmd1.ExecuteReader
            If rd1.Read Then
            End If
            rd1.Close()
            cnn1.Close()
        Catch ex As Exception
            rd1.Close()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "ALTER TABLE ventas add column Consignar int default '0'"
            cmd1.ExecuteNonQuery()
            cnn1.Close()
        End Try



        Try
            cnn1.Close()
            cnn1.Open()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "SELECT CantidadC FROM ventasdetalle"
            rd1 = cmd1.ExecuteReader
            If rd1.Read Then
            End If
            rd1.Close()
            cnn1.Close()
        Catch ex As Exception
            rd1.Close()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "ALTER TABLE ventasdetalle add column CantidadC int default '0'"
            cmd1.ExecuteNonQuery()
            cnn1.Close()
        End Try

        Try
            cnn1.Close()
            cnn1.Open()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "SELECT FechaSal FROM asigpc"
            rd1 = cmd1.ExecuteReader
            If rd1.Read Then
            End If
            rd1.Close()
            cnn1.Close()
        Catch ex As Exception
            rd1.Close()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "ALTER TABLE asigpc add column FechaSal datetime"
            cmd1.ExecuteNonQuery()
            cnn1.Close()
        End Try
    End Sub

    Public Sub ActualizaCampos()
        Try
            'Dim cnn2 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)

            cnn2.Close() : cnn2.Open()

            If Not TablaExiste(cnn2, "alumnos") Then
                Using command As MySqlClient.MySqlCommand = New MySqlClient.MySqlCommand(
                    "CREATE TABLE `alumnos` (
                    `Id` int(11) NOT NULL,
                    `Nombre` varchar(250) NOT NULL DEFAULT '',
                    `Matricula` varchar(100) NOT NULL DEFAULT '',
                    `Telefono` varchar(150) NOT NULL DEFAULT '',
                    `Tutor` varchar(250) NOT NULL DEFAULT '',
                    `Contacto` varchar(150) NOT NULL DEFAULT '',
                    `Calle` varchar(250) NOT NULL DEFAULT '',
                    `N_Int` varchar(100) NOT NULL DEFAULT '',
                    `N_Ext` varchar(100) NOT NULL DEFAULT '',
                    `Colonia` varchar(250) NOT NULL DEFAULT '',
                    `CP` varchar(20) DEFAULT NOT NUL DEFAULT '',
                    `Delegacion` varchar(250) DEFAULT '',
                    `Estado` varchar(250) DEFAULT '',
                    `Id_Grupo` int(11) DEFAULT '0',
                    `Grupo` varchar(250) DEFAULT '', 
                    `Lunes` int(11) DEFAULT '0', `Martes` int(11) DEFAULT '0', `Miercoles` int(11) DEFAULT '0', `Jueves` int(11) DEFAULT '0', `Viernes` int(11) DEFAULT '0', `Sabado` int(11) DEFAULT '0', `Domingo` int(11) DEFAULT '0',
                    `Inscripcion` DATE NOT NULL,
                    `F_Nacimiento` DATE NOT NULL,
                    `F_Inicio` DATE NOT NULL,
                    `Comentario` text NOT NULL,
                    `Curso` varchar(255) NOT NULL DEFAULT '',
                    `Baja` DATE NOT NULL,
                    `Status` INT(1) NOT NULL DEFAULT '0'
                    ) ENGINE=InnoDB DEFAULT CHARSET=latin1;", cnn2)
                    command.ExecuteNonQuery()
                End Using

                Using command2 As MySqlClient.MySqlCommand = New MySqlClient.MySqlCommand($"ALTER TABLE `alumnos` ADD PRIMARY KEY (`Id`);", cnn2)
                    command2.ExecuteNonQuery()
                End Using
                Using command2 As MySqlClient.MySqlCommand = New MySqlClient.MySqlCommand($"ALTER TABLE `alumnos` MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT;", cnn2)
                    command2.ExecuteNonQuery()
                End Using
            End If


            If Not TablaExiste(cnn2, "control_servicios") Then
                Using command As MySqlClient.MySqlCommand = New MySqlClient.MySqlCommand(
                    "CREATE TABLE `control_servicios` (
                    `Id` int(11) NOT NULL,
                    `Codigo` varchar(250) NOT NULL DEFAULT '',
                    `Nombre` varchar(250) NOT NULL DEFAULT '',
                    `Folio` int(11) NOT NULL DEFAULT '0',
                    `Encargado` varchar(100) NOT NULL DEFAULT '',
                    `Inicio` date NOT NULL,
                    `Termino`date NOT NULL,
                    `Status` int(1) NOT NULL DEFAULT '0',
                    `Comentario` text NOT NULL,
                    `Usuario` varchar(100) NOT NULL DEFAULT '',
                    `Entregado` DATE NOT NULL
                    ) ENGINE=InnoDB DEFAULT CHARSET=latin1;", cnn2)
                    command.ExecuteNonQuery()
                End Using

                Using command2 As MySqlClient.MySqlCommand = New MySqlClient.MySqlCommand($"ALTER TABLE `control_servicios` ADD PRIMARY KEY (`Id`);", cnn2)
                    command2.ExecuteNonQuery()
                End Using
                Using command2 As MySqlClient.MySqlCommand = New MySqlClient.MySqlCommand($"ALTER TABLE `control_servicios` MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT;", cnn2)
                    command2.ExecuteNonQuery()
                End Using
            End If

            If Not TablaExiste(cnn2, "control_servicios_det") Then
                Using command As MySqlClient.MySqlCommand = New MySqlClient.MySqlCommand(
                    "CREATE TABLE `control_servicios_det` (
                    `Id` int(11) NOT NULL,
                    `Id_cs` int(11) NOT NULL DEFAULT '0',
                    `Proceso` varchar(250) NOT NULL DEFAULT '',
                    `Entrega`date NOT NULL,
                    `Status` int(1) NOT NULL DEFAULT '0',
                    `Comentario` text NOT NULL,
                    `Entregado` DATE NOT NULL
                    ) ENGINE=InnoDB DEFAULT CHARSET=latin1;", cnn2)
                    command.ExecuteNonQuery()
                End Using

                Using command2 As MySqlClient.MySqlCommand = New MySqlClient.MySqlCommand($"ALTER TABLE `control_servicios_det` ADD PRIMARY KEY (`Id`);", cnn2)
                    command2.ExecuteNonQuery()
                End Using
                Using command2 As MySqlClient.MySqlCommand = New MySqlClient.MySqlCommand($"ALTER TABLE `control_servicios_det` MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT;", cnn2)
                    command2.ExecuteNonQuery()
                End Using
            End If



            If Not TablaExiste(cnn2, "clientes") Then
                Using command As MySqlClient.MySqlCommand = New MySqlClient.MySqlCommand("CREATE TABLE IF NOT EXISTS `clientes` (
                                      `Id` int(11) NOT NULL,
                                      `Nombre` varchar(255) NOT NULL DEFAULT '',
                                      `RazonSocial` varchar(255) NOT NULL DEFAULT '',
                                      `Tipo` varchar(100) NOT NULL DEFAULT '',
                                      `RFC` varchar(50) NOT NULL DEFAULT '',
                                      `Telefono` varchar(100) NOT NULL DEFAULT '',
                                      `Correo` varchar(100) NOT NULL DEFAULT '',
                                      `Credito` float NOT NULL DEFAULT '0',
                                      `DiasCred` float NOT NULL DEFAULT '0',
                                      `Comisionista` varchar(255) NOT NULL DEFAULT '',
                                      `Suspender` int(1) NOT NULL DEFAULT '0',
                                      `Calle` varchar(255) NOT NULL DEFAULT '',
                                      `Colonia` varchar(250) NOT NULL DEFAULT '',
                                      `CP` varchar(50) NOT NULL DEFAULT '',
                                      `Delegacion` varchar(250) NOT NULL DEFAULT '',
                                      `Entidad` varchar(100) NOT NULL DEFAULT '',
                                      `Pais` varchar(100) NOT NULL DEFAULT '',
                                      `RegFis` varchar(255) NOT NULL DEFAULT '',
                                      `NInterior` varchar(50) NOT NULL DEFAULT '',
                                      `NExterior` varchar(50) NOT NULL DEFAULT '',
                                      `CargadoAndroid` int(1) NOT NULL DEFAULT '0',
                                      `Cargado` int(1) NOT NULL DEFAULT '0',
                                      `Template` longtext NOT NULL DEFAULT '',
                                      `SaldoFavor` float NOT NULL DEFAULT '0'
                                    ) ENGINE=InnoDB DEFAULT CHARSET=latin1;", cnn2)
                    command.ExecuteNonQuery()
                End Using
                Using command2 As MySqlClient.MySqlCommand = New MySqlClient.MySqlCommand($"ALTER TABLE `clientes` ADD PRIMARY KEY (`Id`);", cnn2)
                    command2.ExecuteNonQuery()
                End Using
                Using command2 As MySqlClient.MySqlCommand = New MySqlClient.MySqlCommand($"ALTER TABLE `clientes` MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT;", cnn2)
                    command2.ExecuteNonQuery()
                End Using
            End If

            If Not TablaExiste(cnn2, "grupos") Then
                Using commmand As MySqlClient.MySqlCommand = New MySqlClient.MySqlCommand("CREATE TABLE `grupos` (                                      
                                      `Id` int(11) NOT NULL,
                                      `Nombre` varchar(250) DEFAULT '',
                                      `Inicio` varchar(255) DEFAULT '',
                                      `Termino` varchar(250) DEFAULT '',
                                      `Cupo` float DEFAULT '0'                                    
                                    ) ENGINE=InnoDB DEFAULT CHARSET=latin1;", cnn2)
                    commmand.ExecuteNonQuery()
                End Using
                Using command2 As MySqlClient.MySqlCommand = New MySqlClient.MySqlCommand($"ALTER TABLE `grupos` ADD PRIMARY KEY (`Id`);", cnn2)
                    command2.ExecuteNonQuery()
                End Using
                Using command2 As MySqlClient.MySqlCommand = New MySqlClient.MySqlCommand($"ALTER TABLE `grupos` MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT;", cnn2)
                    command2.ExecuteNonQuery()
                End Using
            End If

            'LoginRecargas
            If Not TablaExiste(cnn2, "loginrecargas") Then
                Using command As MySqlClient.MySqlCommand = New MySqlClient.MySqlCommand($"CREATE TABLE IF NOT EXISTS `loginrecargas` (
                                                                                        `Id` int(11) NOT NULL,
                                                                                        `numero` varchar(255) NOT NULL DEFAULT '',
                                                                                        `usuario` varchar(255) NOT NULL DEFAULT '',
                                                                                        `password` varchar(255) NOT NULL DEFAULT ''
                                                                                        ) ENGINE=InnoDB DEFAULT CHARSET=latin1;", cnn2)
                    command.ExecuteNonQuery()
                End Using
                Using command2 As MySqlClient.MySqlCommand = New MySqlClient.MySqlCommand($"ALTER TABLE `loginrecargas` ADD PRIMARY KEY (`Id`);", cnn2)
                    command2.ExecuteNonQuery()
                End Using
                Using command2 As MySqlClient.MySqlCommand = New MySqlClient.MySqlCommand($"ALTER TABLE `loginrecargas` MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT;", cnn2)
                    command2.ExecuteNonQuery()
                End Using
            End If


            cnn2.Close()
        Catch ex As Exception
            MessageBox.Show($"Error al agregar la columna: {ex.Message}")
        End Try

    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick

    End Sub

    Public Sub BuscarTablas()

        Try
            Process.Start(My.Application.Info.DirectoryPath & "\CreateDB_Users.bat")
            System.Threading.Thread.Sleep(5000)
            Dim cnnprueba As MySqlClient.MySqlConnection = New MySqlClient.MySqlConnection
            Dim sinfo As String = ""
            Dim odata As New ToolKitSQL.myssql
            Dim sTargetprueba = "server=" & servidor & ";uid=Delsscom;password=jipl22;database=cn" & baseseleccionada & ";persist security info=false;connect timeout=300"
            'Dim sTargetprueba = "Server=localhost;user id = root; password=;"
            With odata
                If .dbOpen(cnnprueba, sTargetprueba, sinfo) Then

                    'hisclinica
                    .runSp(cnnprueba, vartablahisclinica, sinfo)
                    .runSp(cnnprueba, VarKeyhisclinica, sinfo)
                    .runSp(cnnprueba, VarAutohiscliente, sinfo)
                    'prescripcion
                    .runSp(cnnprueba, VartablaPrescripcion, sinfo)
                    .runSp(cnnprueba, VarKeyPrescripcion, sinfo)
                    .runSp(cnnprueba, VarAutoPrescripcion, sinfo)
                    'hismesa
                    .runSp(cnnprueba, vartablahismesas, sinfo)
                    .runSp(cnnprueba, VarKeyhismesas, sinfo)
                    .runSp(cnnprueba, varAutohismesas, sinfo)

                    'dispositivos
                    .runSp(cnnprueba, vartablatallerd, sinfo)
                    .runSp(cnnprueba, VarKeytallerd, sinfo)
                    .runSp(cnnprueba, varAutotallerd, sinfo)

                    'dispositivos
                    .runSp(cnnprueba, vartabladispositivos, sinfo)
                    .runSp(cnnprueba, VarKeydispositivos, sinfo)
                    .runSp(cnnprueba, varAutodispositivos, sinfo)

                    'accesorios
                    .runSp(cnnprueba, vartablaaccesorios, sinfo)
                    .runSp(cnnprueba, VarKeyaccesorios, sinfo)
                    .runSp(cnnprueba, varAutoaccesorios, sinfo)

                    'unidadmedsat
                    .runSp(cnnprueba, vartablaunidadmedsat, sinfo)
                    .runSp(cnnprueba, VarKeyunidadmedsat, sinfo)
                    .runSp(cnnprueba, varAutounidadmedsatt, sinfo)

                    'productosat
                    .runSp(cnnprueba, vartablaproductosat, sinfo)
                    .runSp(cnnprueba, VarKeyproductosat, sinfo)
                    .runSp(cnnprueba, varAutoproductosat, sinfo)

                    'produccioncdetalle
                    .runSp(cnnprueba, vartablaproduccioncdetalle, sinfo)
                    .runSp(cnnprueba, VarKeyproduccioncdetalle, sinfo)
                    .runSp(cnnprueba, varAutoproduccioncdetalle, sinfo)

                    'produccionc
                    .runSp(cnnprueba, vartablaproduccionc, sinfo)
                    .runSp(cnnprueba, VarKeyproduccionc, sinfo)
                    .runSp(cnnprueba, varAutoproduccionc, sinfo)

                    'movingre
                    .runSp(cnnprueba, vartablamovingre, sinfo)
                    .runSp(cnnprueba, VarKeymovingre, sinfo)
                    .runSp(cnnprueba, varAutomovingre, sinfo)

                    'precios
                    .runSp(cnnprueba, vartablaprecios, sinfo)
                    .runSp(cnnprueba, VarKeyprecios, sinfo)
                    .runSp(cnnprueba, varAutoprecios, sinfo)

                    'marcas
                    .runSp(cnnprueba, vartablamarcas, sinfo)
                    .runSp(cnnprueba, VarKeymarcas, sinfo)
                    .runSp(cnnprueba, varAutomarcas, sinfo)

                    'vehiculo2
                    .runSp(cnnprueba, vartablavehiculo2, sinfo)
                    .runSp(cnnprueba, VarKeyvehiculo2, sinfo)
                    .runSp(cnnprueba, varAutovehiuclo2, sinfo)

                    'nominas
                    .runSp(cnnprueba, vartablanominass, sinfo)
                    .runSp(cnnprueba, VarKeynominass, sinfo)
                    .runSp(cnnprueba, varAutonominass, sinfo)

                    'pedidosvendet
                    .runSp(cnnprueba, vartablapedidosvendet, sinfo)
                    .runSp(cnnprueba, VarKeypedidosvendet, sinfo)
                    .runSp(cnnprueba, varAutopedidosvendet, sinfo)

                    'comandas_t
                    .runSp(cnnprueba, vartablacomandas_t, sinfo)
                    .runSp(cnnprueba, varKeycomandas_t, sinfo)
                    .runSp(cnnprueba, varAutocomandas_t, sinfo)

                    'precios_rango
                    .runSp(cnnprueba, vartablarangoprecios, sinfo)
                    .runSp(cnnprueba, varKeypreciosrango, sinfo)
                    .runSp(cnnprueba, varAutopreciosrango, sinfo)

                    'pedidosven
                    .runSp(cnnprueba, vartablapedidosven, sinfo)
                    .runSp(cnnprueba, VarKeypedidosven, sinfo)
                    .runSp(cnnprueba, varAutopedidosven, sinfo)

                    'detallehotelprecios
                    .runSp(cnnprueba, vartabladetallehotelprecios, sinfo)
                    .runSp(cnnprueba, VarKeydetallehotelprecios, sinfo)
                    .runSp(cnnprueba, varAutodetallehotelprecios, sinfo)

                    'promos
                    .runSp(cnnprueba, vartablapromos, sinfo)
                    .runSp(cnnprueba, varKeypromos, sinfo)
                    .runSp(cnnprueba, varAutopromos, sinfo)

                    'clienteeliminado
                    .runSp(cnnprueba, vartablaclienteeliminado, sinfo)
                    .runSp(cnnprueba, varKeyclienteeliminado, sinfo)
                    .runSp(cnnprueba, varAutoclienteeliminado, sinfo)

                    'productoeliminado
                    .runSp(cnnprueba, vartablaproductoeliminado, sinfo)
                    .runSp(cnnprueba, varKeyproductoeliminado, sinfo)
                    .runSp(cnnprueba, varAutoproductoeliminado, sinfo)

                    'pedidostemporal
                    .runSp(cnnprueba, vartablapedidostemporal, sinfo)
                    .runSp(cnnprueba, varKeypedidostemporal, sinfo)
                    .runSp(cnnprueba, varAutopedidostemporal, sinfo)

                    ' pedidoeliminado
                    .runSp(cnnprueba, vartablaPedidoEliminado, sinfo)
                    .runSp(cnnprueba, varKeypedidoeliminado, sinfo)
                    .runSp(cnnprueba, varAutopedidoeliminado, sinfo)

                    'detalle_nomina
                    .runSp(cnnprueba, vartabladetallenomina, sinfo)
                    .runSp(cnnprueba, varKeydetallenomina, sinfo)
                    .runSp(cnnprueba, varAutodetallenomina, sinfo)

                    'tipoincapacidadsat
                    .runSp(cnnprueba, vartablatipoincapacidadsat, sinfo)
                    Dim dtprueba11 As New DataTable
                    If .getDt(cnnprueba, dtprueba11, "SELECT Id from tipoincapacidadsat", sinfo) Then
                    Else
                        .runSp(cnnprueba, varinsertatipoincapacidadsat, sinfo)
                    End If
                    .runSp(cnnprueba, varKeyotipoincapacidadsat, sinfo)
                    .runSp(cnnprueba, varAutotipoincapacidadsat, sinfo)

                    'tiponomina
                    .runSp(cnnprueba, vartablatiponomina, sinfo)
                    Dim dtprueba10 As New DataTable
                    If .getDt(cnnprueba, dtprueba10, "SELECT Id from tiponomina", sinfo) Then
                    Else
                        .runSp(cnnprueba, varinsertatiponomina, sinfo)
                    End If
                    .runSp(cnnprueba, varKeyotiponomina, sinfo)
                    .runSp(cnnprueba, varAutotiponomina, sinfo)


                    'otrospagos
                    .runSp(cnnprueba, vartablaotrospagos, sinfo)
                    Dim dtprueba9 As New DataTable
                    If .getDt(cnnprueba, dtprueba9, "SELECT Id from otrospagos", sinfo) Then
                    Else
                        .runSp(cnnprueba, varinsertaotrospagos, sinfo)
                    End If
                    .runSp(cnnprueba, varKeyotrospagos, sinfo)
                    .runSp(cnnprueba, varAutootrospagos, sinfo)

                    'tipopercepcioncontable
                    .runSp(cnnprueba, vartablatipopercepcioncontable, sinfo)
                    Dim dtprueba8 As New DataTable
                    If .getDt(cnnprueba, dtprueba8, "SELECT Id from tipo_percepcion_contable", sinfo) Then
                    Else
                        .runSp(cnnprueba, varinsertatipopercepcioncontable, sinfo)
                    End If
                    .runSp(cnnprueba, varKeytipopercepcioncontable, sinfo)
                    .runSp(cnnprueba, varAutotipopercepcioncontable, sinfo)

                    'tipodeduccioncontable
                    .runSp(cnnprueba, vartablatipodeduccioncontable, sinfo)
                    Dim dtprueba7 As New DataTable
                    If .getDt(cnnprueba, dtprueba7, "SELECT Id from tipo_deduccion_contable", sinfo) Then
                    Else
                        .runSp(cnnprueba, varinsertatipodeduccioncontable, sinfo)
                    End If
                    .runSp(cnnprueba, varKeytipodeduccioncontable, sinfo)
                    .runSp(cnnprueba, varAutotipodeduccioncontable, sinfo)

                    'riesgopuesto
                    .runSp(cnnprueba, vartablariesgopuesto, sinfo)
                    Dim dtprueba6 As New DataTable
                    If .getDt(cnnprueba, dtprueba6, "SELECT Id from riesgo_puesto", sinfo) Then
                    Else
                        .runSp(cnnprueba, varinsertariesgopuesto, sinfo)
                    End If
                    .runSp(cnnprueba, varKeyriesgopuesto, sinfo)
                    .runSp(cnnprueba, varAutoriesgopuesto, sinfo)

                    'tipocontrato
                    .runSp(cnnprueba, vartablatipocontrato, sinfo)
                    Dim dtprueba5 As New DataTable
                    If .getDt(cnnprueba, dtprueba5, "SELECT Id from tipo_contrato", sinfo) Then
                    Else
                        .runSp(cnnprueba, varinsertatipocontrato, sinfo)
                    End If
                    .runSp(cnnprueba, varKeytipocontrato, sinfo)
                    .runSp(cnnprueba, varAutotipocontrato, sinfo)

                    'tipojornada
                    .runSp(cnnprueba, vartablatipojornada, sinfo)
                    Dim dtprueba4 As New DataTable
                    If .getDt(cnnprueba, dtprueba4, "SELECT Id from tipo_jornada", sinfo) Then
                    Else
                        .runSp(cnnprueba, varinsertatipojornada, sinfo)
                    End If
                    .runSp(cnnprueba, varKeytipojornada, sinfo)
                    .runSp(cnnprueba, varAutotipojornada, sinfo)

                    'periodicidad_pago
                    .runSp(cnnprueba, vartablaperiodicidadpago, sinfo)
                    Dim dtprueba3 As New DataTable
                    If .getDt(cnnprueba, dtprueba3, "SELECT Id from periodicidad_pago", sinfo) Then
                    Else
                        .runSp(cnnprueba, varinsertaperiodicidadpago, sinfo)
                    End If
                    .runSp(cnnprueba, varKeyperiodicidadpago, sinfo)
                    .runSp(cnnprueba, varAutoperiodicidadpago, sinfo)

                    'regimencontrataciontrabajador
                    .runSp(cnnprueba, vartablaregimencontrataciontrabajador, sinfo)
                    Dim dtprueba2 As New DataTable
                    If .getDt(cnnprueba, dtprueba2, "SELECT Id from regimen_contratacion_trabajador", sinfo) Then
                    Else
                        .runSp(cnnprueba, varinsertaregimencontrataciontrabajador, sinfo)
                    End If
                    .runSp(cnnprueba, varKeyregimencontrataciontrabajador, sinfo)
                    .runSp(cnnprueba, varAutoregimencontrataciontrabajador, sinfo)


                    'habitacion
                    .runSp(cnnprueba, vartablahabitacion, sinfo)
                    .runSp(cnnprueba, varKeyhabitacion, sinfo)
                    .runSp(cnnprueba, varAutohabitacion, sinfo)

                    'detallehotel
                    .runSp(cnnprueba, vartabladetallehotel, sinfo)
                    .runSp(cnnprueba, varKeydetallehotel, sinfo)
                    .runSp(cnnprueba, varAutodetallehotel, sinfo)

                    'controlserviciodet
                    .runSp(cnnprueba, vartablacontrolserviciosdet, sinfo)
                    .runSp(cnnprueba, varKeycontrolserviciodet, sinfo)
                    .runSp(cnnprueba, varAutocontrolserviciodet, sinfo)

                    'controlservicio
                    .runSp(cnnprueba, vartablacontrolservicios, sinfo)
                    .runSp(cnnprueba, varKeycontrolservicio, sinfo)
                    .runSp(cnnprueba, varAutocontrolservicio, sinfo)

                    .runSp(cnnprueba, vartablahisasigpc, sinfo)
                    .runSp(cnnprueba, varKeyhisasigpc, sinfo)
                    .runSp(cnnprueba, varAutohisasigpc, sinfo)

                    'vtaimpresion
                    .runSp(cnnprueba, vartablavtaimpresion, sinfo)
                    .runSp(cnnprueba, varKeyvtaimpresion, sinfo)
                    .runSp(cnnprueba, varAutovtaimpresion, sinfo)

                    'REP_COMANDAS
                    .runSp(cnnprueba, vartablarepcomandas, sinfo)
                    .runSp(cnnprueba, varKeyrepcomandas, sinfo)
                    .runSp(cnnprueba, varAutorepcomandas, sinfo)

                    'refaccionaria
                    .runSp(cnnprueba, vartablarefaccionaria, sinfo)
                    .runSp(cnnprueba, varKeyrefaccionaria, sinfo)
                    .runSp(cnnprueba, varAutorefaccionaria, sinfo)

                    'vehiculo
                    .runSp(cnnprueba, vartablavehiculo, sinfo)
                    .runSp(cnnprueba, varKeyvehiculo, sinfo)
                    .runSp(cnnprueba, varAutovehiculo, sinfo)

                    'comandasveh
                    .runSp(cnnprueba, vartablacomandasveh, sinfo)
                    .runSp(cnnprueba, varKeycomandasveh, sinfo)
                    .runSp(cnnprueba, varAutocomandasveh, sinfo)

                    'promociones
                    .runSp(cnnprueba, vartablapromociones, sinfo)
                    .runSp(cnnprueba, varKeypromociones, sinfo)
                    .runSp(cnnprueba, varAutopromociones, sinfo)

                    'permisosm
                    .runSp(cnnprueba, vartablapermisosm, sinfo)
                    If .getDt(cnnprueba, dtprueba9, "SELECT id from permisosm", sinfo) Then
                    Else
                        .runSp(cnnprueba, varinsertapermisosm, sinfo)
                    End If
                    .runSp(cnnprueba, varKeypermisosm, sinfo)
                    .runSp(cnnprueba, varAutopermisosm, sinfo)

                    'extras
                    .runSp(cnnprueba, vartablaextras, sinfo)
                    .runSp(cnnprueba, varKeyextras, sinfo)
                    .runSp(cnnprueba, varAutoextras, sinfo)

                    'preferencia
                    .runSp(cnnprueba, vartablapreferencias, sinfo)
                    .runSp(cnnprueba, varKeyprefecia, sinfo)
                    .runSp(cnnprueba, varAutopreferencias, sinfo)

                    'AbonoE
                    .runSp(cnnprueba, vartablaabonoe, sinfo)
                    .runSp(cnnprueba, varKeyabonoe, sinfo)
                    .runSp(cnnprueba, varAutoabonoe, sinfo)

                    'AbonoI
                    .runSp(cnnprueba, vartablaabonoi, sinfo)
                    .runSp(cnnprueba, varKeyabonoi, sinfo)
                    .runSp(cnnprueba, varAutoabonoi, sinfo)

                    'Acreedores
                    .runSp(cnnprueba, vartablaAcreedores, sinfo)
                    .runSp(cnnprueba, varKeyacreedores, sinfo)
                    .runSp(cnnprueba, varAutoacreedores, sinfo)

                    'Alumnos
                    .runSp(cnnprueba, vartablalumnos, sinfo)
                    .runSp(cnnprueba, varKeyalumnos, sinfo)
                    .runSp(cnnprueba, varAutoalumnos, sinfo)

                    'AsigPC
                    .runSp(cnnprueba, vartablaasigpc, sinfo)
                    .runSp(cnnprueba, varKeyasigpc, sinfo)
                    .runSp(cnnprueba, varAutoasigpc, sinfo)

                    'Asistencia
                    .runSp(cnnprueba, vartablaasistencia, sinfo)
                    .runSp(cnnprueba, varKeyasistencia, sinfo)
                    .runSp(cnnprueba, varAutoasistencia, sinfo)

                    .runSp(cnnprueba, vartablaasistenciagym, sinfo)
                    .runSp(cnnprueba, varKeyasistenciagym, sinfo)
                    .runSp(cnnprueba, varAutoasistenciagym, sinfo)

                    'Auditoria
                    .runSp(cnnprueba, vartablaauditoria, sinfo)
                    .runSp(cnnprueba, varKeyauditoria, sinfo)
                    .runSp(cnnprueba, varAutoauditoria, sinfo)

                    'AuxCompras
                    .runSp(cnnprueba, vartablaauxcompras, sinfo)
                    .runSp(cnnprueba, varKeyauxcompras, sinfo)
                    .runSp(cnnprueba, varAutoauxcompras, sinfo)

                    'AuxComprasSeries
                    .runSp(cnnprueba, vartablaauxcomprasseries, sinfo)
                    .runSp(cnnprueba, varKeyauxcomprasseries, sinfo)
                    .runSp(cnnprueba, varAutoauxcomprasseries, sinfo)

                    'AuxPedidos
                    .runSp(cnnprueba, vartablaauxpedidos, sinfo)
                    .runSp(cnnprueba, varKeyauxpedidos, sinfo)
                    .runSp(cnnprueba, varAutoauxpedidos, sinfo)

                    'Bancos
                    .runSp(cnnprueba, vartablabancos, sinfo)
                    Dim dtprueba As New DataTable
                    If .getDt(cnnprueba, dtprueba, "select Banco from bancos", sinfo) Then
                    Else
                        .runSp(cnnprueba, varinsertabancos, sinfo)
                    End If

                    'Cardex
                    .runSp(cnnprueba, vartablacardex, sinfo)
                    .runSp(cnnprueba, varKeycardex, sinfo)
                    .runSp(cnnprueba, varAutocardex, sinfo)

                    'CargosAbonos
                    .runSp(cnnprueba, vartablacargosabonos, sinfo)

                    'CartaPorte
                    .runSp(cnnprueba, vartablacartaporte, sinfo)
                    .runSp(cnnprueba, varKeycartaporte, sinfo)
                    .runSp(cnnprueba, varAutocartaporte, sinfo)

                    'CartaPorteDet
                    .runSp(cnnprueba, vartablacartaportedet, sinfo)
                    .runSp(cnnprueba, varKeycartaportedet, sinfo)
                    .runSp(cnnprueba, varAutocartaportedet, sinfo)

                    'CartaPorteDeti
                    .runSp(cnnprueba, vartablacartaportedeti, sinfo)
                    .runSp(cnnprueba, varKeycartaportedeti, sinfo)
                    .runSp(cnnprueba, varAutocartaportedeti, sinfo)

                    'CartaPorteI
                    .runSp(cnnprueba, vartablacartaportei, sinfo)
                    .runSp(cnnprueba, varKeycartaportei, sinfo)
                    .runSp(cnnprueba, varAutocartaportei, sinfo)

                    'Clientes
                    .runSp(cnnprueba, vartablaclientes, sinfo)
                    .runSp(cnnprueba, varKeyclientes, sinfo)
                    .runSp(cnnprueba, varAutoclientes, sinfo)

                    'Comanda1
                    .runSp(cnnprueba, vartablacomandas1, sinfo)
                    .runSp(cnnprueba, varKeycomandas1, sinfo)
                    .runSp(cnnprueba, varAutocomandas1, sinfo)

                    'Comandas
                    .runSp(cnnprueba, vartablacomandas, sinfo)
                    .runSp(cnnprueba, varKeycomandas, sinfo)
                    .runSp(cnnprueba, varAutocomandas, sinfo)

                    'Compras
                    .runSp(cnnprueba, vartablacompras, sinfo)
                    .runSp(cnnprueba, varKeycompras, sinfo)
                    .runSp(cnnprueba, varAutocompras, sinfo)

                    'ComprasDet
                    .runSp(cnnprueba, vartablacomprasdet, sinfo)
                    .runSp(cnnprueba, varKeycomprasdet, sinfo)
                    .runSp(cnnprueba, varAutocomprasdet, sinfo)
                    .runSp(cnnprueba, varForKcomprasdet, sinfo)

                    'CorteCaja
                    .runSp(cnnprueba, vartablacortecaja, sinfo)
                    .runSp(cnnprueba, varKeycortecaja, sinfo)
                    .runSp(cnnprueba, varAutocortecaja, sinfo)

                    'CorteUsuario
                    .runSp(cnnprueba, vartablacorteusuario, sinfo)
                    .runSp(cnnprueba, varKeycorteusuario, sinfo)
                    .runSp(cnnprueba, varAutocorteusuario, sinfo)

                    'CotPed
                    .runSp(cnnprueba, vartablacotped, sinfo)
                    .runSp(cnnprueba, varKeycotped, sinfo)
                    .runSp(cnnprueba, varAutocotped, sinfo)

                    'CotPedDet
                    .runSp(cnnprueba, vartablaccotpeddet, sinfo)
                    .runSp(cnnprueba, varKeycotpeddet, sinfo)
                    .runSp(cnnprueba, varAutocotpeddet, sinfo)

                    'CtMedicos
                    .runSp(cnnprueba, vartablactmedicos, sinfo)
                    .runSp(cnnprueba, varKeyctmedicos, sinfo)
                    .runSp(cnnprueba, varAutoctmedicos, sinfo)

                    'CuentasBancarias
                    .runSp(cnnprueba, vartablacuentasbancarias, sinfo)
                    .runSp(cnnprueba, varKeycuentasbancarias, sinfo)
                    .runSp(cnnprueba, varAutocuentasbancarias, sinfo)

                    'DatosNegocio
                    .runSp(cnnprueba, vartabladatosnegocio, sinfo)
                    .runSp(cnnprueba, varKeydatosnegocio, sinfo)
                    .runSp(cnnprueba, varAutodatosnegocio, sinfo)

                    'DatosProsepago
                    .runSp(cnnprueba, vartabladatosprosepago, sinfo)
                    .runSp(cnnprueba, varKeydatosprosepago, sinfo)
                    .runSp(cnnprueba, varAutodatosprosepago, sinfo)

                    'detalle_factura
                    .runSp(cnnprueba, vartabladdetalle_factura, sinfo)

                    'Deudores
                    .runSp(cnnprueba, vartabladeudores, sinfo)
                    .runSp(cnnprueba, varKeydeudores, sinfo)
                    .runSp(cnnprueba, varAutodeudores, sinfo)

                    'Devoluciones
                    .runSp(cnnprueba, vartabladevoluciones, sinfo)
                    .runSp(cnnprueba, varKeydevoluciones, sinfo)
                    .runSp(cnnprueba, varAutodevoluciones, sinfo)

                    'Entregas
                    .runSp(cnnprueba, vartablaentregas, sinfo)
                    .runSp(cnnprueba, varKeyentregas, sinfo)
                    .runSp(cnnprueba, varAutoentregas, sinfo)

                    'Facturas
                    .runSp(cnnprueba, vartablafacturas, sinfo)
                    .runSp(cnnprueba, varKeyfacturas, sinfo)
                    .runSp(cnnprueba, varAutofacturas, sinfo)

                    'FechaaCobros
                    .runSp(cnnprueba, vartablafechacobros, sinfo)
                    .runSp(cnnprueba, varKeyfechacobros, sinfo)
                    .runSp(cnnprueba, varAutofechacobros, sinfo)

                    'FormaPagoSat
                    .runSp(cnnprueba, vartablaformapagosat, sinfo)
                    dtprueba = New DataTable
                    If .getDt(cnnprueba, dtprueba, "select Id from formapagosat", sinfo) Then
                    Else
                        .runSp(cnnprueba, varinsertaformapagosat, sinfo)
                    End If
                    .runSp(cnnprueba, varKeyformapagosat, sinfo)
                    .runSp(cnnprueba, varAutoformapagosat, sinfo)

                    'FormasPago
                    .runSp(cnnprueba, vartablaformaspago, sinfo)
                    .runSp(cnnprueba, varKeyformaspago, sinfo)
                    .runSp(cnnprueba, varAutoformapagos, sinfo)

                    'Formatos
                    .runSp(cnnprueba, vartablaformatos, sinfo)
                    dtprueba = New DataTable
                    If .getDt(cnnprueba, dtprueba, "select Id from formatos", sinfo) Then
                    Else
                        .runSp(cnnprueba, varinsertaformatos, sinfo)
                    End If

                    .runSp(cnnprueba, vartablaformatos, sinfo)
                    .runSp(cnnprueba, varKeyformatos, sinfo)
                    .runSp(cnnprueba, varAutoformatos, sinfo)

                    'Gastos
                    .runSp(cnnprueba, vartablagastos, sinfo)

                    'Grupos
                    .runSp(cnnprueba, vartablagrupos, sinfo)
                    .runSp(cnnprueba, varKeygrupos, sinfo)
                    .runSp(cnnprueba, varAutogrupos, sinfo)

                    'HeResultados
                    .runSp(cnnprueba, vartablaheresultados, sinfo)

                    'Horarios
                    .runSp(cnnprueba, vartablahorarios, sinfo)

                    'ImpuestoSat
                    .runSp(cnnprueba, vartablaimpuestosat, sinfo)
                    dtprueba = New DataTable
                    If .getDt(cnnprueba, dtprueba, "select Id from impuestosat", sinfo) Then
                    Else
                        .runSp(cnnprueba, varinsertaimpuestosat, sinfo)
                    End If
                    .runSp(cnnprueba, varKeyimpuestosat, sinfo)
                    .runSp(cnnprueba, varAutoimpuestosat, sinfo)

                    'IVA
                    .runSp(cnnprueba, vartablaiva, sinfo)
                    dtprueba = New DataTable
                    If .getDt(cnnprueba, dtprueba, "select Id from iva", sinfo) Then
                    Else
                        .runSp(cnnprueba, varinsertaiva, sinfo)
                    End If
                    .runSp(cnnprueba, varKeyiva, sinfo)
                    .runSp(cnnprueba, varAutoiva, sinfo)



                    'Kits
                    .runSp(cnnprueba, vartablakits, sinfo)

                    'loginrecargas
                    .runSp(cnnprueba, vartablaloginrecargas, sinfo)
                    .runSp(cnnprueba, varKeyloginrecargas, sinfo)
                    .runSp(cnnprueba, varAutologinrecargas, sinfo)

                    .runSp(cnnprueba, vartablaliberacion, sinfo)
                    .runSp(cnnprueba, varKeyliberacion, sinfo)
                    .runSp(cnnprueba, varAutoliberacion, sinfo)

                    'LoteCaducidad
                    .runSp(cnnprueba, vartablalotecaducidad, sinfo)
                    .runSp(cnnprueba, varKeylotecaducidad, sinfo)
                    .runSp(cnnprueba, varAutolotecaducidad, sinfo)

                    .runSp(cnnprueba, vartablamembresiasgym, sinfo)
                    .runSp(cnnprueba, varKeymembresiasgym, sinfo)
                    .runSp(cnnprueba, varAutomembresiasgym, sinfo)

                    'Merma
                    .runSp(cnnprueba, vartablamerma, sinfo)
                    .runSp(cnnprueba, varKeymerma, sinfo)
                    .runSp(cnnprueba, varAutomerma, sinfo)

                    'Mesa
                    .runSp(cnnprueba, vartablamesa, sinfo)
                    .runSp(cnnprueba, varKeymesa, sinfo)
                    .runSp(cnnprueba, varAutomesa, sinfo)

                    'MesasxEmpleados
                    .runSp(cnnprueba, vartablamesasempleados, sinfo)
                    .runSp(cnnprueba, varKeymesasempleados, sinfo)
                    .runSp(cnnprueba, varAutomesasempleados, sinfo)

                    'MesesSat
                    .runSp(cnnprueba, vartablamesessat, sinfo)
                    dtprueba = New DataTable
                    If .getDt(cnnprueba, dtprueba, "select ID from mesessat", sinfo) Then
                    Else
                        .runSp(cnnprueba, varinsertamesessat, sinfo)
                    End If
                    .runSp(cnnprueba, varKeymesessat, sinfo)
                    .runSp(cnnprueba, varAutomesessat, sinfo)

                    'MetodoPagoSat
                    .runSp(cnnprueba, vartablametodopagosat, sinfo)
                    dtprueba = New DataTable
                    If .getDt(cnnprueba, dtprueba, "select Id from metodopagosat", sinfo) Then
                    Else
                        .runSp(cnnprueba, varinsertametodopagosat, sinfo)
                    End If
                    .runSp(cnnprueba, varKeymetodopagosat, sinfo)
                    .runSp(cnnprueba, varAutometodopagosat, sinfo)

                    'MiProd
                    .runSp(cnnprueba, vartablamiprod, sinfo)

                    .runSp(cnnprueba, vartablamodentregas, sinfo)
                    .runSp(cnnprueba, varKeymodentregas, sinfo)
                    .runSp(cnnprueba, varAutomodentregas, sinfo)

                    .runSp(cnnprueba, vartablamodentregasdet, sinfo)
                    .runSp(cnnprueba, varKeymodentregasdet, sinfo)
                    .runSp(cnnprueba, varAutomodentregasdet, sinfo)

                    .runSp(cnnprueba, vartablamodprecios, sinfo)
                    .runSp(cnnprueba, varKeymodprecios, sinfo)
                    .runSp(cnnprueba, varAutomodprecios, sinfo)

                    .runSp(cnnprueba, vartablamonedero, sinfo)
                    .runSp(cnnprueba, varKeymonedero, sinfo)
                    .runSp(cnnprueba, varAutomonedero, sinfo)

                    .runSp(cnnprueba, vartablamovcuenta, sinfo)
                    .runSp(cnnprueba, varKeymovcuenta, sinfo)
                    .runSp(cnnprueba, varAutomovcuenta, sinfo)

                    .runSp(cnnprueba, vartablamovmonedero, sinfo)
                    .runSp(cnnprueba, varKeymovmonedero, sinfo)
                    .runSp(cnnprueba, varAutomovmonedero, sinfo)

                    .runSp(cnnprueba, vartablanomina, sinfo)
                    .runSp(cnnprueba, varKeynomina, sinfo)
                    .runSp(cnnprueba, varAutonomina, sinfo)

                    .runSp(cnnprueba, vartablanota, sinfo)
                    .runSp(cnnprueba, varKeynota, sinfo)
                    .runSp(cnnprueba, varAutonota, sinfo)

                    .runSp(cnnprueba, vartablaotrosgastos, sinfo)
                    .runSp(cnnprueba, varKeyotrosgastos, sinfo)
                    .runSp(cnnprueba, varAutootrosgastos, sinfo)

                    .runSp(cnnprueba, vartablaordentrabajo, sinfo)
                    .runSp(cnnprueba, varKeyordentrabajo, sinfo)
                    .runSp(cnnprueba, varAutoordentrabajo, sinfo)

                    .runSp(cnnprueba, vartablaparametros, sinfo)
                    .runSp(cnnprueba, varinsertaparametros, sinfo)
                    .runSp(cnnprueba, varKeyparametros, sinfo)

                    'parcialidades
                    .runSp(cnnprueba, vartablaparcialidades, sinfo)
                    .runSp(cnnprueba, varKeyparcialidades, sinfo)
                    .runSp(cnnprueba, varAutoparcialidades, sinfo)

                    'parciaqlidadesdetalle
                    .runSp(cnnprueba, vartablaparcialidadesdetalle, sinfo)
                    .runSp(cnnprueba, varKeyparcialidadesdetalle, sinfo)
                    .runSp(cnnprueba, varAutoparcialidadesdetalle, sinfo)

                    'parcialidadesdetallemulti
                    .runSp(cnnprueba, vartablaparcialidadesdetallemulti, sinfo)
                    .runSp(cnnprueba, varKeyparcialidadesdetallemulti, sinfo)
                    .runSp(cnnprueba, varAutoparcialidadesdetallemulti, sinfo)

                    'parcialidadesmulti
                    .runSp(cnnprueba, vartablaparcialidadesmulti, sinfo)
                    .runSp(cnnprueba, varKeyparcialidadesmulti, sinfo)
                    .runSp(cnnprueba, varAutoparcialidadesmulti, sinfo)

                    .runSp(cnnprueba, vartablapedidos, sinfo)
                    .runSp(cnnprueba, varKeypedidos, sinfo)
                    .runSp(cnnprueba, varAutopedidos, sinfo)

                    .runSp(cnnprueba, vartablapedidosdet, sinfo)
                    .runSp(cnnprueba, varKeypedidosdet, sinfo)
                    .runSp(cnnprueba, varAutopedidosdet, sinfo)

                    .runSp(cnnprueba, vartablapeps, sinfo)
                    .runSp(cnnprueba, varKeypeps, sinfo)
                    .runSp(cnnprueba, varAutopeps, sinfo)

                    .runSp(cnnprueba, vartablaperiodicidadsat, sinfo)
                    dtprueba = New DataTable
                    If .getDt(cnnprueba, dtprueba, "select Id from periodicidadsat", sinfo) Then
                    Else
                        .runSp(cnnprueba, varinsertaperiodicidadsat, sinfo)
                    End If
                    .runSp(cnnprueba, varKeyperiodicidadsat, sinfo)
                    .runSp(cnnprueba, varAutoperiodicidadsat, sinfo)

                    .runSp(cnnprueba, vartablapermisos, sinfo)
                    dtprueba = New DataTable
                    If .getDt(cnnprueba, dtprueba, "select Id from permisos", sinfo) Then
                    Else
                        .runSp(cnnprueba, varinsertapermisos, sinfo)
                    End If
                    .runSp(cnnprueba, varKeypermisos, sinfo)
                    .runSp(cnnprueba, varAutopermisos, sinfo)

                    .runSp(cnnprueba, vartablaporteclavestcc, sinfo)
                    dtprueba = New DataTable
                    If .getDt(cnnprueba, dtprueba, "select Id from porteclavestcc", sinfo) Then
                    Else
                        .runSp(cnnprueba, varinsertaporteclavestcc, sinfo)
                    End If
                    .runSp(cnnprueba, varKeyporteclavestcc, sinfo)
                    .runSp(cnnprueba, varAutoporteclavestcc, sinfo)

                    .runSp(cnnprueba, vartablaportecolonia, sinfo)
                    .runSp(cnnprueba, varKeyportecolonia, sinfo)
                    .runSp(cnnprueba, varAutoportecolonia, sinfo)

                    .runSp(cnnprueba, vartablaporteconfigautotrans, sinfo)
                    dtprueba = New DataTable
                    If .getDt(cnnprueba, dtprueba, "select Id from porteconfigautotrans", sinfo) Then
                    Else
                        .runSp(cnnprueba, varinsertaporteconfigautotrans, sinfo)
                    End If
                    .runSp(cnnprueba, varKeyporteconfigautotrans, sinfo)
                    .runSp(cnnprueba, varAutoporteconfigautotrans, sinfo)

                    .runSp(cnnprueba, vartablaportedestino, sinfo)
                    .runSp(cnnprueba, varKeyportedestino, sinfo)
                    .runSp(cnnprueba, varAutoportedestino, sinfo)

                    .runSp(cnnprueba, vartablaporteestados, sinfo)
                    dtprueba = New DataTable
                    If .getDt(cnnprueba, dtprueba, "select Id from porteestados", sinfo) Then
                    Else
                        .runSp(cnnprueba, varinsertaporteestados, sinfo)
                    End If
                    .runSp(cnnprueba, varKeyporteestados, sinfo)
                    .runSp(cnnprueba, varAutoporteestados, sinfo)

                    .runSp(cnnprueba, vartablaportefigura, sinfo)
                    dtprueba = New DataTable
                    If .getDt(cnnprueba, dtprueba, "select Id from portefigura", sinfo) Then
                    Else
                        .runSp(cnnprueba, varinsertaportefigura, sinfo)
                    End If
                    .runSp(cnnprueba, varKeyportefigura, sinfo)
                    .runSp(cnnprueba, varAutoportefigura, sinfo)

                    .runSp(cnnprueba, vartablaportelocalidad, sinfo)
                    .runSp(cnnprueba, varKeyportelocalidad, sinfo)
                    .runSp(cnnprueba, varAutoportelocalidad, sinfo)

                    .runSp(cnnprueba, vartablaportematpeligrosos, sinfo)
                    dtprueba = New DataTable
                    If .getDt(cnnprueba, dtprueba, "select Id from portematpeligrosos", sinfo) Then
                    Else
                        .runSp(cnnprueba, varinsertaportematpeligrosos, sinfo)
                    End If
                    .runSp(cnnprueba, varKeyportematpeligrosos, sinfo)
                    .runSp(cnnprueba, varAutoportematpeligrosos, sinfo)

                    'PorteMercancia
                    .runSp(cnnprueba, vartablaportemercancia, sinfo)
                    .runSp(cnnprueba, varKeyportemercancia, sinfo)
                    .runSp(cnnprueba, varAutoportemercancia, sinfo)

                    'PorteMunicipios
                    .runSp(cnnprueba, vartablaportemunicipios, sinfo)
                    .runSp(cnnprueba, varKeyportemunicipios, sinfo)
                    .runSp(cnnprueba, varAutoportemunicipios, sinfo)

                    'PorteOperador
                    .runSp(cnnprueba, vartablaporteoperador, sinfo)
                    .runSp(cnnprueba, varKeyporteoperador, sinfo)
                    .runSp(cnnprueba, varAutoporteoperador, sinfo)

                    .runSp(cnnprueba, vartablaporteorigen, sinfo)
                    .runSp(cnnprueba, varKeyporteorigen, sinfo)
                    .runSp(cnnprueba, varAutoporteorigen, sinfo)

                    .runSp(cnnprueba, vartablaportepais, sinfo)
                    .runSp(cnnprueba, varKeyportepais, sinfo)
                    .runSp(cnnprueba, varAutoportepais, sinfo)

                    .runSp(cnnprueba, vartablaporteproducto, sinfo)
                    .runSp(cnnprueba, varKeyporteproducto, sinfo)
                    .runSp(cnnprueba, varAutoporteproducto, sinfo)

                    .runSp(cnnprueba, vartablaporteproductosat, sinfo)
                    .runSp(cnnprueba, varKeyporteproductosat, sinfo)
                    .runSp(cnnprueba, varAutoporteproductosat, sinfo)

                    .runSp(cnnprueba, vartablaportepropietario, sinfo)
                    .runSp(cnnprueba, varKeyportepropietario, sinfo)
                    .runSp(cnnprueba, varAutoportepropietario, sinfo)

                    .runSp(cnnprueba, vartablaportetipocarga, sinfo)
                    dtprueba = New DataTable
                    If .getDt(cnnprueba, dtprueba, "select Id from portetipocarga", sinfo) Then
                    Else
                        .runSp(cnnprueba, varinsertaportetipocarga, sinfo)
                    End If
                    .runSp(cnnprueba, varKeyportetipocarga, sinfo)
                    .runSp(cnnprueba, varAutoportetipocarga, sinfo)

                    .runSp(cnnprueba, vartablaportetipocarro, sinfo)
                    dtprueba = New DataTable
                    If .getDt(cnnprueba, dtprueba, "select Id from portetipocarro", sinfo) Then
                    Else
                        .runSp(cnnprueba, varinsertaportetipocarro, sinfo)
                    End If
                    .runSp(cnnprueba, varKeyportetipocarro, sinfo)
                    .runSp(cnnprueba, varAutoportetipocarro, sinfo)

                    .runSp(cnnprueba, vartablaportetipocontenedor, sinfo)
                    .runSp(cnnprueba, varKeyportetipocontenedor, sinfo)
                    .runSp(cnnprueba, varAutoportetipocontenedor, sinfo)

                    .runSp(cnnprueba, vartablaportetipoembalaje, sinfo)
                    .runSp(cnnprueba, varKeyportetipoembalaje, sinfo)
                    .runSp(cnnprueba, varAutoportetipoembalaje, sinfo)

                    .runSp(cnnprueba, vartablaportetipopermiso, sinfo)
                    .runSp(cnnprueba, varKeyportetipopermiso, sinfo)
                    .runSp(cnnprueba, varAutoportetipopermiso, sinfo)

                    .runSp(cnnprueba, vartablaportetiporemolque, sinfo)
                    .runSp(cnnprueba, varKeyportetiporemolque, sinfo)
                    .runSp(cnnprueba, varAutoportetiporemolque, sinfo)

                    .runSp(cnnprueba, vartablaportetransporte, sinfo)
                    dtprueba = New DataTable
                    If .getDt(cnnprueba, dtprueba, "select Id from portetransporte", sinfo) Then
                    Else
                        .runSp(cnnprueba, varinsertaportetransporte, sinfo)
                    End If
                    .runSp(cnnprueba, varKeyportetransporte, sinfo)
                    .runSp(cnnprueba, varAutoportetransporte, sinfo)

                    .runSp(cnnprueba, vartablaporteunidadmedemb, sinfo)
                    .runSp(cnnprueba, varKeyporteunidadmedemb, sinfo)
                    .runSp(cnnprueba, varAutoporteunidadmedemb, sinfo)

                    .runSp(cnnprueba, vartablaprocesos_prod, sinfo)
                    .runSp(cnnprueba, varKeyprocesos_prod, sinfo)
                    .runSp(cnnprueba, varAutoprocesos_prod, sinfo)

                    'Productos
                    .runSp(cnnprueba, vartablaproductos, sinfo)
                    .runSp(cnnprueba, varKeyproductos, sinfo)
                    .runSp(cnnprueba, varAutoproductos, sinfo)

                    'ProMasVen
                    .runSp(cnnprueba, vartablapromasven, sinfo)

                    'Proveedores
                    .runSp(cnnprueba, vartablaproveedores, sinfo)
                    .runSp(cnnprueba, varKeyproveedores, sinfo)
                    .runSp(cnnprueba, varAutoproveedores, sinfo)

                    'Recargas
                    .runSp(cnnprueba, vartablarecargas, sinfo)
                    .runSp(cnnprueba, varKeyrecargas, sinfo)
                    .runSp(cnnprueba, varAutorecargas, sinfo)

                    'RegFis
                    .runSp(cnnprueba, vartablaregimenfiscalsat, sinfo)
                    dtprueba = New DataTable
                    If .getDt(cnnprueba, dtprueba, "select ClaveRegFis from regimenfiscalsat", sinfo) Then
                    Else
                        .runSp(cnnprueba, varinsertaregimenfiscalsat, sinfo)
                    End If

                    .runSp(cnnprueba, vartablarepfactura, sinfo)

                    .runSp(cnnprueba, vartablarepomen, sinfo)

                    .runSp(cnnprueba, vartablarepsalidas, sinfo)

                    .runSp(cnnprueba, vartablarep_antib, sinfo)
                    .runSp(cnnprueba, varKeyrep_antib, sinfo)
                    .runSp(cnnprueba, varAutorep_antib, sinfo)

                    .runSp(cnnprueba, vartablarep_salidas, sinfo)

                    .runSp(cnnprueba, vartablarutasimpresion, sinfo)
                    .runSp(cnnprueba, varKeyrutasimpresion, sinfo)
                    .runSp(cnnprueba, varAutorutasimpresion, sinfo)

                    .runSp(cnnprueba, vartablasaldosempleados, sinfo)
                    .runSp(cnnprueba, varKeysaldosempleados, sinfo)
                    .runSp(cnnprueba, varAutosaldosempleados, sinfo)

                    .runSp(cnnprueba, vartablasseries, sinfo)
                    .runSp(cnnprueba, varKeyseries, sinfo)
                    .runSp(cnnprueba, varAutoseries, sinfo)

                    .runSp(cnnprueba, vartablaservicios, sinfo)
                    .runSp(cnnprueba, varKeyservicios, sinfo)
                    .runSp(cnnprueba, varAutoservicios, sinfo)

                    .runSp(cnnprueba, vartablatalmacen, sinfo)
                    .runSp(cnnprueba, varKeytalmacen, sinfo)
                    .runSp(cnnprueba, varAutotalmacen, sinfo)

                    .runSp(cnnprueba, vartablatb_moneda, sinfo)
                    dtprueba = New DataTable
                    If .getDt(cnnprueba, dtprueba, "select Id from tb_moneda", sinfo) Then
                    Else
                        .runSp(cnnprueba, varinsertatb_moneda, sinfo)
                    End If
                    .runSp(cnnprueba, varKeytb_moneda, sinfo)
                    .runSp(cnnprueba, varAutotb_moneda, sinfo)

                    .runSp(cnnprueba, vartablaticket, sinfo)
                    .runSp(cnnprueba, varKeyticket, sinfo)
                    .runSp(cnnprueba, varAutoticket, sinfo)
                    dtprueba = New DataTable
                    If .getDt(cnnprueba, dtprueba, "select Id from ticket", sinfo) Then
                    Else
                        .runSp(cnnprueba, varinsertaticket, sinfo)
                    End If

                    .runSp(cnnprueba, vartablatipofactorsat, sinfo)
                    .runSp(cnnprueba, varKeytipofactorsat, sinfo)
                    .runSp(cnnprueba, varAutotipofactorsat, sinfo)

                    .runSp(cnnprueba, vartablatiposcomprobantesat, sinfo)
                    dtprueba = New DataTable
                    If .getDt(cnnprueba, dtprueba, "select Id from tiposcomprobantesat", sinfo) Then
                    Else
                        .runSp(cnnprueba, varinsertatiposcomprobantesat, sinfo)
                    End If
                    .runSp(cnnprueba, varKeytiposcomprobantesat, sinfo)
                    .runSp(cnnprueba, varAutotiposcomprobantesat, sinfo)

                    .runSp(cnnprueba, vartablatiprelacioncfdisat, sinfo)
                    dtprueba = New DataTable
                    If .getDt(cnnprueba, dtprueba, "select Id from tiprelacioncfdisat", sinfo) Then
                    Else
                        .runSp(cnnprueba, varinsertatiprelacioncfdisat, sinfo)
                    End If
                    .runSp(cnnprueba, varKeytiprelacioncfdisat, sinfo)
                    .runSp(cnnprueba, varAutotiprelacioncfdisat, sinfo)

                    .runSp(cnnprueba, vartablatransporte, sinfo)
                    .runSp(cnnprueba, varKeytransporte, sinfo)
                    .runSp(cnnprueba, varAutotransporte, sinfo)

                    .runSp(cnnprueba, vartablatraslados, sinfo)
                    .runSp(cnnprueba, varKeytraslados, sinfo)
                    .runSp(cnnprueba, varAutotraslados, sinfo)

                    .runSp(cnnprueba, vartablatrasladosdet, sinfo)
                    .runSp(cnnprueba, varKeytrasladosdet, sinfo)
                    .runSp(cnnprueba, varAutotrasladosdet, sinfo)

                    .runSp(cnnprueba, vartablaumtblcfds, sinfo)
                    .runSp(cnnprueba, varKeyumtblcfds, sinfo)
                    .runSp(cnnprueba, varAutoumtblcfds, sinfo)

                    .runSp(cnnprueba, vartablausocfdisat, sinfo)
                    dtprueba = New DataTable
                    If .getDt(cnnprueba, dtprueba, "select ClaveUsoCFDI from usocfdisat", sinfo) Then
                    Else
                        .runSp(cnnprueba, varinsertausocfdisat, sinfo)
                    End If

                    .runSp(cnnprueba, vartablausocomprocfdisat, sinfo)
                    dtprueba = New DataTable
                    If .getDt(cnnprueba, dtprueba, "select Id from usocomprocfdisat", sinfo) Then
                    Else
                        .runSp(cnnprueba, varinsertausocomprocfdisat, sinfo)
                    End If
                    .runSp(cnnprueba, varKeyusocomprocfdisat, sinfo)
                    .runSp(cnnprueba, varAutousocomprocfdisat, sinfo)

                    .runSp(cnnprueba, vartablausuarios, sinfo)
                    dtprueba = New DataTable
                    If .getDt(cnnprueba, dtprueba, "select IdEmpleado from usuarios", sinfo) Then
                    Else
                        .runSp(cnnprueba, varinsertausuarios, sinfo)
                    End If
                    .runSp(cnnprueba, varKeyusuarios, sinfo)
                    .runSp(cnnprueba, varAutousuarios, sinfo)

                    .runSp(cnnprueba, vartablauuidrelacion, sinfo)
                    .runSp(cnnprueba, varKeyuuidrelacion, sinfo)
                    .runSp(cnnprueba, varAutouuidrelacion, sinfo)

                    .runSp(cnnprueba, vartablaventas, sinfo)
                    .runSp(cnnprueba, varKeyventas, sinfo)
                    .runSp(cnnprueba, varAutoventas, sinfo)

                    .runSp(cnnprueba, vartablaventasdetalle, sinfo)
                    .runSp(cnnprueba, varKeyventasdetalle, sinfo)
                    .runSp(cnnprueba, varAutoventasdetalle, sinfo)
                    .runSp(cnnprueba, varForKventasdetalle, sinfo)

                    cnnprueba.Close()

                Else
                    MsgBox("Error al crear la base")
                    MsgBox(sinfo)

                End If
            End With

        Catch ex As Exception
            MsgBox("El Archivo CreateDB_Users no existe hay que colocarlo en la carpeta correspondiente")
        End Try

    End Sub

    Public Sub VerificarVentasDetalle()

        'Dim cnn1 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
        'Dim cnn2 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
        'Dim cnn3 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
        'Dim cnn4 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)

        'Dim rd1, rd2, rd3, rd4 As MySqlDataReader
        'Dim cmd1, cmd2, cmd3, cmd4 As MySqlCommand

        Try

            Dim foliovd As Integer = 0
            Dim codunico As String = ""
            Dim idvd As Integer = 0
            Dim sumt As Integer = 0

            cnn1.Close() : cnn1.Open()
            cnn2.Close() : cnn2.Open()
            cnn3.Close() : cnn3.Open()

            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "SELECT Folio FROM ventas ORDER BY Folio desc LIMIT 60"
            rd1 = cmd1.ExecuteReader
            If rd1.HasRows Then
                Do While rd1.Read
                    foliovd = rd1(0).ToString

                    cmd2 = cnn2.CreateCommand
                    cmd2.CommandText = "SELECT CodUnico FROM ventasdetalle WHERE Folio=" & foliovd & " AND CodUnico<>''"
                    rd2 = cmd2.ExecuteReader
                    If rd2.HasRows Then
                        Do While rd2.Read
                            codunico = rd2(0).ToString
                            If codunico = "" Then
                            Else
                                cmd3 = cnn3.CreateCommand
                                cmd3.CommandText = "SELECT COUNT(CodUnico),Id FROM ventasdetalle WHERE CodUnico='" & codunico & "'"
                                rd3 = cmd3.ExecuteReader
                                If rd3.HasRows Then
                                    If rd3.Read Then
                                        sumt = rd3(0).ToString
                                        idvd = rd3(1).ToString

                                        If sumt > 1 Then
                                            cnn4.Close() : cnn4.Open()
                                            cmd4 = cnn4.CreateCommand
                                            cmd4.CommandText = "DELETE FROM ventasdetalle WHERE Id=" & idvd & " AND CodUnico='" & codunico & "'"
                                            cmd4.ExecuteNonQuery()
                                            cnn4.Close()
                                        End If
                                    End If
                                End If
                                rd3.Close()
                            End If
                        Loop
                    End If
                    rd2.Close()
                Loop
            End If
            rd1.Close()
            cnn1.Close()
            cnn2.Close()
            cnn3.Close()

        Catch ex As Exception
            MessageBox.Show(ex.ToString)
            cnn1.Close()
            cnn2.Close()
            cnn3.Close()
        End Try
    End Sub
End Class