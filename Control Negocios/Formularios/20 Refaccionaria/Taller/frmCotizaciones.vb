Imports CrystalDecisions.Shared
Imports System.IO
Imports CrystalDecisions.CrystalReports.Engine
Imports MySql.Data.MySqlClient.Memcached
Imports System.Numerics
Imports MySql.Data.MySqlClient

Public Class frmCotizaciones
    Dim descuento As Integer = 0
    Dim idcliente As Integer = 0
    Dim telefono As String = ""
    Dim tim As New System.Windows.Forms.Timer()

    Dim marca As String = ""
    Dim modelo As String = ""
    Dim my_folio As Integer = 0

    Private Sub cboCotizaciones_DropDown(sender As Object, e As EventArgs) Handles cboCotizaciones.DropDown

        Dim cnn5 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
        Dim rd5 As MySqlDataReader
        Dim cmd5 As MySqlCommand

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
        Dim cnn5 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
        Dim rd5 As MySqlDataReader
        Dim cmd5 As MySqlCommand

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

        Dim cnn5 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
        Dim rd5 As MySqlDataReader
        Dim cmd5 As MySqlCommand

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

        Dim cnn1 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
        Dim rd1 As MySqlDataReader
        Dim cmd1 As MySqlCommand

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
        lblidVehiculo.Text = ""
        lblPlaca.Text = ""
        cboCliente.Focus.Equals(True)
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

            Dim cnn1 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
            Dim rd1 As MySqlDataReader
            Dim cmd1 As MySqlCommand

            cnn1.Close() : cnn1.Open()
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

    Private Sub btnSalir_Click(sender As Object, e As EventArgs) Handles btnSalir.Click
        Me.Close()
    End Sub

    Private Sub btnCotizacion_Click(sender As Object, e As EventArgs) Handles btnCotizacion.Click
        If grdCaptura.Rows.Count = 0 Then MsgBox("Captura productos para guardar la cotización.", vbInformation + vbOKOnly, titulorefaccionaria) : cboDescripcion.Focus().Equals(True) : Exit Sub

        Dim cnn1 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
        Dim rd1 As MySqlDataReader
        Dim cmd1 As MySqlCommand

        If MsgBox("¿Deseas guardar los datos de esta cotización?", vbInformation + vbOKCancel, titulorefaccionaria) = vbCancel Then cnn1.Close() : Exit Sub

        Dim MySubtotal As Double = 0
        Dim totalp As Double = 0
        Dim ivaventa As Double = 0
        Dim totalventa As Double = 0
        Dim descuento As Double = 0
        Dim subtotal As Double = 0
        Dim totreal As Double = 0



        Try
            cnn1.Close() : cnn1.Open()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "SELECT Id FROM clientes WHERE Nombre='" & cboCliente.Text & "'"
            rd1 = cmd1.ExecuteReader
            If rd1.HasRows Then
                If rd1.Read Then
                    idcliente = rd1(0).ToString
                End If
            End If
            rd1.Close()

            For i As Integer = 0 To grdCaptura.Rows.Count - 1

                cmd1 = cnn1.CreateCommand
                cmd1.CommandText =
                "select IVA from Productos where Codigo='" & grdCaptura.Rows(i).Cells(0).Value.ToString() & "'"
                rd1 = cmd1.ExecuteReader
                If rd1.HasRows Then
                    If rd1.Read Then
                        MySubtotal = MySubtotal + (CDbl(grdCaptura.Rows(i).Cells(5).Value.ToString) / (1 + CDbl(rd1(0).ToString)))
                    End If
                End If
                rd1.Close()

                totalventa = totalventa + grdCaptura.Rows(i).Cells(5).Value.ToString
            Next
            cnn1.Close()

            ivaventa = CDbl(totalventa) - CDbl(MySubtotal)
            descuento = txtdescu.Text
            subtotal = txtSubtotal.Text
            cnn1.Close() : cnn1.Open()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText =
                "insert into CotPed(IdCliente,Cliente,Direccion,Subtotal,IVA,Descuento,Totales,ACuenta,Resta,Usuario,Fecha,Hora,Status,Tipo,Comentario,IP,MontoSinDesc) values(" & idcliente & ",'" & cboCliente.Text & "',''," & MySubtotal & "," & ivaventa & "," & descuento & "," & totreal & ",0,0,'" & lblUsuario.Text & "','" & Format(Date.Now, "yyyy-MM-dd") & "','" & Format(Date.Now, "HH:mm:ss") & "','','COTIZACION','','" & dameIP2() & "'," & subtotal & ")"
            cmd1.ExecuteNonQuery()

            Do Until MyFolio <> 0
                cmd1 = cnn1.CreateCommand
                cmd1.CommandText = "select MAX(Folio) from CotPed where Tipo='COTIZACION' and IP='" & dameIP2() & "'"
                rd1 = cmd1.ExecuteReader
                If rd1.HasRows Then
                    If rd1.Read Then
                        MyFolio = rd1(0).ToString
                    End If
                End If
                rd1.Close()
            Loop
            cnn1.Close()

            cnn1.Close() : cnn1.Open()
            For T As Integer = 0 To grdCaptura.Rows.Count - 1
                If grdCaptura.Rows(T).Cells(0).Value.ToString = "" Then GoTo Door

                Dim mycode As String = grdCaptura.Rows(T).Cells(0).Value.ToString
                Dim mydesc As String = grdCaptura.Rows(T).Cells(1).Value.ToString
                Dim mycant As Double = grdCaptura.Rows(T).Cells(3).Value.ToString
                Dim myprecio As Double = grdCaptura.Rows(T).Cells(4).Value.ToString
                Dim mytotal As Double = FormatNumber(CDbl(mycant) * CDbl(myprecio), 4)

                Dim myunidad As String = ""
                Dim myiva As Double = 0
                Dim myprecios As Double = 0
                Dim mytotals As Double = 0
                Dim mydepto As String = ""
                Dim mygrupo As String = ""
                Dim mymcd As Double = 0
                Dim MyCostVUE As Double = 0
                Dim MyProm As Double = 0
                Dim Pre_Comp As Double = 0

                cmd1 = cnn1.CreateCommand
                cmd1.CommandText =
                "select IVA,Departamento,Grupo,MCD,UVenta from Productos where Codigo='" & mycode & "'"
                rd1 = cmd1.ExecuteReader
                If rd1.HasRows Then
                    If rd1.Read Then
                        myiva = rd1(0).ToString
                        mydepto = rd1("Departamento").ToString
                        mygrupo = rd1("Grupo").ToString
                        mymcd = rd1("MCD").ToString
                        myunidad = rd1("UVenta").ToString
                        MyCostVUE = 0
                        MyProm = 0
                    End If
                End If
                rd1.Close()

                cmd1 = cnn1.CreateCommand
                cmd1.CommandText = "select Departamento,PrecioCompra from Productos where Codigo='" & Strings.Left(mycode, 6) & "'"
                rd1 = cmd1.ExecuteReader
                If rd1.HasRows Then
                    If rd1.Read Then
                        Pre_Comp = rd1("PrecioCompra").ToString
                        MyCostVUE = Pre_Comp * (mycant / mymcd)
                    End If
                End If
                rd1.Close()

                myprecios = FormatNumber(myprecio / (1 + myiva), 6)
                mytotals = FormatNumber(mytotal / (1 + myiva), 6)
Door:

                If grdCaptura.Rows(T).Cells(0).Value.ToString <> "" Then
                    cmd1 = cnn1.CreateCommand
                    cmd1.CommandText =
                   "insert into CotPedDet(Folio,Codigo,Nombre,Cantidad,Unidad,CostoV,Precio,Total,PrecioSIVA,TotalSIVA,Fecha,Usuario,Depto,Grupo,CostVR,Tipo) values(" & MyFolio & ",'" & mycode & "','" & mydesc & "'," & mycant & ",'" & myunidad & "'," & MyCostVUE & "," & myprecio & "," & mytotal & "," & myprecios & "," & mytotals & ",'" & Format(Date.Now, "yyyy-MM-dd HH:mm:ss") & "','','" & mydepto & "','" & mygrupo & "','','COTIZACION')"
                    cmd1.ExecuteNonQuery()
                End If

            Next
            cnn1.Close()

            Panel6.Visible = True
            My.Application.DoEvents()
            Insert_Cotizacion()
            PDF_Cotizacion()
            Panel6.Visible = False
            My.Application.DoEvents()

            tim.Stop()
            My.Application.DoEvents()

            grdCaptura.Rows.Clear()
            txtTotal.Text = "0.00"
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
            cnn1.Close()
        End Try
    End Sub

    Public Sub Insert_Cotizacion()
        Dim oData As New ToolKitSQL.oledbdata
        Dim sSql As String = ""
        Dim a_cnn As OleDb.OleDbConnection = New OleDb.OleDbConnection
        Dim sInfo As String = ""
        Dim dr As DataRow = Nothing
        Dim dt As New DataTable

        Dim my_folio As Integer = 0
        Dim MyStatus As String = ""
        Dim tel_cliente As String = ""

        Dim cnn1 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
        Dim cnn3 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
        Dim rd1, rd3 As MySqlDataReader
        Dim cmd1, cmd3 As MySqlCommand

        With oData
            If .dbOpen(a_cnn, Direcc_Access, sInfo) Then
                .runSp(a_cnn, "delete from CotPedDetalle", sInfo) : sInfo = ""
                .runSp(a_cnn, "delete from CotPed", sInfo) : sInfo = ""

                If cboCliente.Text <> "" Then
                    cnn3.Close() : cnn3.Open()
                    cmd3 = cnn3.CreateCommand
                    cmd3.CommandText =
                        "select Telefono from Clientes where Nombre='" & cboCliente.Text & "'"
                    rd3 = cmd3.ExecuteReader
                    If rd3.HasRows Then
                        If rd3.Read Then
                            tel_cliente = rd3("Telefono").ToString()
                        End If
                    End If
                    rd3.Close() : cnn3.Close()
                End If

                If .runSp(a_cnn, "insert into CotPed(idCliente,Nombre,Direccion,Totales,Descuento,ACuenta,Resta,Usuario,FVenta,HVenta,Status,MontoSnDesc,Comentario,Telefono) values(" & idcliente & ",'" & cboCliente.Text & "','',0,0,0,0,'',#" & FormatDateTime(Date.Now, DateFormat.ShortDate) & "#,#" & FormatDateTime(Date.Now, DateFormat.ShortTime) & "#,'COTIZACION',0,'','" & tel_cliente & "')", sInfo) Then
                    sInfo = ""
                Else
                    MsgBox(sInfo)
                End If

                If .getDr(a_cnn, dr, "select MAX(Folio) from CotPed", sInfo) Then
                    my_folio = dr(0).ToString()
                End If

                Dim cod_temp As String = ""

                Dim ruta_imagen As String = ""


                cnn1.Close() : cnn1.Open()

                For pipo As Integer = 0 To grdCaptura.Rows.Count - 1
                    Dim myund As String = ""

                    Dim codigo As String = grdCaptura.Rows(pipo).Cells(0).Value.ToString()
                    If codigo = "" Then GoTo doorcita

                    'Traa la imgen del producto para la cotización
                    If File.Exists("C:\ControlNegociosPro\ProductosImg\" & codigo & ".jpg") Then
                        ruta_imagen = "C:\ControlNegociosPro\ProductosImg\" & codigo & ".jpg"
                    Else
                        If varrutabase <> "" Then
                            If File.Exists("\\" & varrutabase & "\ControlNegociosPro\ProductosImg\" & codigo & ".jpg") Then
                                ruta_imagen = "\\" & varrutabase & "\ControlNegociosPro\ProductosImg\" & codigo & ".jpg"
                            Else
                                ruta_imagen = ""
                            End If
                        Else
                            ruta_imagen = ""
                        End If
                    End If

                    cmd1 = cnn1.CreateCommand
                    cmd1.CommandText = "SELECT UVenta FROM productos WHERE Codigo='" & codigo & "'"
                    rd1 = cmd1.ExecuteReader
                    If rd1.HasRows Then
                        If rd1.Read Then
                            myund = rd1(0).ToString
                        End If
                    End If
                    rd1.Close()


                    Dim nombre As String = grdCaptura.Rows(pipo).Cells(1).Value.ToString()
                    Dim cantidad As Double = grdCaptura.Rows(pipo).Cells(3).Value.ToString()
                    Dim precio_original As Double = grdCaptura.Rows(pipo).Cells(4).Value.ToString()
                    Dim total_original As Double = precio_original * cantidad

                    If codigo <> "" Then
                        cod_temp = codigo
                        If .runSp(a_cnn, "insert into CotPedDetalle(Folio,Codigo,Nombre,Cantidad,UVenta,Precio_Original,Total_Original,Descuento_Unitario,Descuento_Total,Precio_Descuento,Total_Descuento,Comisionista,Comentario,Ruta_Imagen) values(" & my_folio & ",'" & codigo & "','" & nombre & "'," & cantidad & ",'" & myund & "'," & precio_original & "," & total_original & ",0,0,0,0,'','','" & ruta_imagen & "')", sInfo) Then
                            sInfo = ""
                        Else
                            MsgBox(sInfo)
                        End If
                    End If
                    Continue For
doorcita:
                    If grdCaptura.Rows(pipo).Cells(1).Value.ToString() <> "" Then
                        Dim id_a As Integer = 0
                        If .getDr(a_cnn, dr, "select MAX(Id) from CotPedDetalle", sInfo) Then
                            id_a = dr(0).ToString()
                        End If
                        'Es comentario 
                        .runSp(a_cnn, "update CotPedDetalle set Comentario='" & grdCaptura.Rows(pipo).Cells(1).Value.ToString() & "' where Id=" & id_a, sInfo)
                        sInfo = ""
                    End If

                Next
                cnn1.Close()
                a_cnn.Close()
            End If
        End With
    End Sub

    Public Sub PDF_Cotizacion()
        Dim root_name_recibo As String = ""
        Dim FileOpen As New ProcessStartInfo

        'Nombre del CrystalReport
        Dim FileNta As New Cotización

        Dim strServerName As String = Application.StartupPath
        Dim crtableLogoninfos As New TableLogOnInfos
        Dim crtableLogoninfo As New TableLogOnInfo
        Dim crConnectionInfo As New ConnectionInfo
        Dim CrTables As Tables
        Dim CrTable As Table

        Dim cnn1 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
        Dim rd1 As MySqlDataReader
        Dim cmd1 As MySqlCommand


        crea_ruta(My.Application.Info.DirectoryPath & "\ARCHIVOSDL1\COTIZACIONES\")
        root_name_recibo = My.Application.Info.DirectoryPath & "\ARCHIVOSDL1\COTIZACIONES\" & MyFolio & ".pdf"

        If File.Exists(My.Application.Info.DirectoryPath & "\ARCHIVOSDL1\COTIZACIONES\" & MyFolio & ".pdf") Then
            File.Delete(My.Application.Info.DirectoryPath & "\ARCHIVOSDL1\COTIZACIONES\" & MyFolio & ".pdf")
        End If

        If varrutabase <> "" Then
            If File.Exists("\\" & varrutabase & "\ControlNegociosPro\ARCHIVOSDL1\COTIZACIONES\" & MyFolio & ".pdf") Then
                File.Delete("\\" & varrutabase & "\ControlNegociosPro\ARCHIVOSDL1\COTIZACIONES\" & MyFolio & ".pdf")
            End If
        End If

        With crConnectionInfo
            .ServerName = My.Application.Info.DirectoryPath & "\DL1.mdb"
            .DatabaseName = My.Application.Info.DirectoryPath & "\DL1.mdb"
            .UserID = ""
            .Password = "jipl22"
        End With

        CrTables = FileNta.Database.Tables
        For Each CrTable In CrTables
            crtableLogoninfo = CrTable.LogOnInfo
            crtableLogoninfo.ConnectionInfo = crConnectionInfo
            CrTable.ApplyLogOnInfo(crtableLogoninfo)
        Next

        Dim PieNota As String = ""

        Try
            cnn1.Close() : cnn1.Open()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText =
                "Select Pie2 from Ticket"
            rd1 = cmd1.ExecuteReader
            If rd1.HasRows Then
                If rd1.Read Then
                    PieNota = rd1(0).ToString()
                End If
            End If
            rd1.Close()
            cnn1.Close()
        Catch ex As Exception
            MessageBox.Show(ex.ToString())
            cnn1.Close()
        End Try

        Dim TotalIEPSPrint As Double = 0
        Dim SubtotalPrint As Double = 0
        Dim MySubtotal As Double = 0
        Dim TotalIVAPrint As Double = 0

        Dim SubTotal As Double = 0
        Dim IVA_Vent As Double = 0
        Dim Total_Ve As Double = 0

        Dim DesglosaIVA As String = DatosRecarga("Desglosa")
        Dim mytotalventa2 As Double = 0
        Try


            cnn1.Close() : cnn1.Open()
            For N As Integer = 0 To grdCaptura.Rows.Count - 1
                If CStr(grdCaptura.Rows(N).Cells(0).Value.ToString) <> "" Then

                    mytotalventa2 = mytotalventa2 + CDbl(grdCaptura.Rows(N).Cells(5).Value.ToString)

                    cmd1 = cnn1.CreateCommand
                    cmd1.CommandText =
                        "select IVA from Productos where Codigo='" & CStr(grdCaptura.Rows(N).Cells(0).Value.ToString) & "'"
                    rd1 = cmd1.ExecuteReader
                    If rd1.HasRows Then
                        If rd1.Read Then
                            MySubtotal = MySubtotal + (CDbl(mytotalventa2) / (1 + CDbl(rd1(0).ToString)))
                        End If
                    End If
                    rd1.Close()
                End If
            Next
            TotalIVAPrint = FormatNumber(TotalIVAPrint, 2)
            MySubtotal = FormatNumber(MySubtotal, 2)
            mytotalventa2 = FormatNumber(mytotalventa2, 2)

            cnn1.Close()
        Catch ex As Exception
            MessageBox.Show(ex.ToString())
            cnn1.Close()
        End Try

        IVA_Vent = FormatNumber(CDbl(mytotalventa2) - CDbl(TotalIVAPrint), 2)
        SubTotal = FormatNumber(TotalIVAPrint, 2)
        Total_Ve = FormatNumber(CDbl(txtTotalVenta.Text), 2)

        FileNta.SetDatabaseLogon("", "jipl22")
        FileNta.DataDefinition.FormulaFields("Folio").Text = "'" & MyFolio & "'"
        FileNta.DataDefinition.FormulaFields("Usuario").Text = "'" & lblUsuario.Text & "'"
        FileNta.DataDefinition.FormulaFields("conLetra").Text = "'" & convLetras(txtTotalVenta.Text) & "'"

        ''Pagos
        'If DesglosaIVA = "1" Then
        '    If SubTotal > 0 Then
        '        FileNta.DataDefinition.FormulaFields("Subtotal").Text = "'" & FormatNumber(SubTotal, 4) & "'"       'Subtotal
        '    End If
        '    If IVA_Vent > 0 Then
        '        If IVA_Vent > 0 And IVA_Vent <> CDbl(txtPagar.Text) Then
        '            FileNta.DataDefinition.FormulaFields("IVA").Text = "'" & FormatNumber(IVA_Vent, 4) & "'"   'IVA
        '        End If
        '    End If
        'End If

        Dim total_des As Double = Total_Ve + CDbl(txtdescu.Text)

        FileNta.DataDefinition.FormulaFields("Total").Text = "'" & FormatNumber(Total_Ve, 2) & "'"             'Total
        If CDbl(txtdescu.Text) > 0 Then
            FileNta.DataDefinition.FormulaFields("TTotal").Text = "'" & FormatNumber(total_des, 2) & "'"             'Total
            FileNta.DataDefinition.FormulaFields("Descuento").Text = "'" & FormatNumber(txtdescu.Text, 2) & "'"             'Total
        End If


        If PieNota <> "" Then
            FileNta.DataDefinition.FormulaFields("pieNota").Text = "'" & PieNota & "'"          'Pie de nota
        End If

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
            System.IO.File.Copy(My.Application.Info.DirectoryPath & "\ARCHIVOSDL1\COTIZACIONES\" & MyFolio & ".pdf", "\\" & varrutabase & "\ControlNegociosPro\ARCHIVOSDL1\COTIZACIONES\" & MyFolio & ".pdf")
        End If
    End Sub

    Private Sub cboDescripcion_DropDown(sender As Object, e As EventArgs) Handles cboDescripcion.DropDown
        Dim cnn5 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
        Dim rd5 As MySqlDataReader
        Dim cmd5 As MySqlCommand

        Try
            cboDescripcion.Items.Clear()

            cnn5.Close() : cnn5.Open()
            cmd5 = cnn5.CreateCommand
            cmd5.CommandText = "SELECT DISTINCT Nombre FROM productos WHERE Nombre<>'' ORDER BY nombre"
            rd5 = cmd5.ExecuteReader
            If rd5.HasRows Then
                Do While rd5.Read
                    cboDescripcion.Items.Add(rd5(0).ToString)
                Loop
            End If
            rd5.Close()
            cnn5.Close()

        Catch ex As Exception
            MessageBox.Show(ex.ToString)
            cnn5.Close()
        End Try
    End Sub

    Private Sub cboDescripcion_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cboDescripcion.KeyPress
        If AscW(e.KeyChar) = Keys.Enter Then

            Dim cnn1 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
            Dim rd1 As MySqlDataReader
            Dim cmd1 As MySqlCommand

            cnn1.Close() : cnn1.Open()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "SELECT Codigo FROM productos WHERE Nombre='" & cboDescripcion.Text & "'"
            rd1 = cmd1.ExecuteReader
            If rd1.HasRows Then
                If rd1.Read Then
                    cboCodigo.Text = rd1(0).ToString
                End If
            End If
            rd1.Close()
            cnn1.Close()
            cboCodigo.Focus.Equals(True)


        End If
    End Sub

    Private Sub cboCodigo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cboCodigo.KeyPress
        If AscW(e.KeyChar) = Keys.Enter Then

            Dim cnn1 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
            Dim rd1 As MySqlDataReader
            Dim cmd1 As MySqlCommand

            cnn1.Close() : cnn1.Open()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "SELECT UVenta,PrecioVentaIVA FROM productos WHERE Nombre='" & cboDescripcion.Text & "' AND Codigo='" & cboCodigo.Text & "'"
            rd1 = cmd1.ExecuteReader
            If rd1.HasRows Then
                If rd1.Read Then
                    txtUnidad.Text = rd1(0).ToString
                    txtPrecio.Text = FormatNumber(rd1(1).ToString, 2)
                End If
            End If
            rd1.Close()
            cnn1.Close()
            txtCantidad.Focus.Equals(True)
        End If
    End Sub

    Private Sub txtCantidad_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtCantidad.KeyPress
        If AscW(e.KeyChar) = Keys.Enter Then
            txtPrecio.Focus.Equals(True)
        End If
    End Sub

    Private Sub txtPrecio_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPrecio.KeyPress
        If AscW(e.KeyChar) = Keys.Enter Then

            If cboDescripcion.Text = "" Then cboDescripcion.Focus.Equals(True) : Exit Sub

            grdCaptura.Rows.Add(cboCodigo.Text,
                                cboDescripcion.Text,
                                txtUnidad.Text,
                                FormatNumber(txtCantidad.Text, 2),
                                FormatNumber(txtPrecio.Text, 2),
                                FormatNumber(txtTotal.Text, 2))

            grdCaptura.FirstDisplayedScrollingRowIndex = grdCaptura.RowCount - 1

            txtSubtotal.Text = CDbl(txtSubtotal.Text) + CDbl(txtTotal.Text)
            txtSubtotal.Text = FormatNumber(txtSubtotal.Text, 2)
            txtTotalVenta.Text = FormatNumber(txtSubtotal.Text, 2)

            cboCodigo.Text = ""
            cboDescripcion.Text = ""
            txtUnidad.Text = ""
            txtCantidad.Text = "1"
            txtPrecio.Text = "0.00"
            txtTotal.Text = "0.00"
            cboDescripcion.Focus.Equals(True)
        End If
    End Sub

    Private Sub cboCodigo_DropDown(sender As Object, e As EventArgs) Handles cboCodigo.DropDown
        Dim cnn5 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
        Dim rd5 As MySqlDataReader
        Dim cmd5 As MySqlCommand

        Try
            cboCodigo.Items.Clear()
            cnn5.Close() : cnn5.Open()
            cmd5 = cnn5.CreateCommand
            cmd5.CommandText = "SELECT DISTINCT Codigo FROM productos WHERE LEFT(Codigo, 6) = LEFT('" & cboCodigo.Text & "', 6) ORDER BY Codigo"
            rd5 = cmd5.ExecuteReader
            If rd5.HasRows Then
                Do While rd5.Read
                    cboCodigo.Items.Add(rd5(0).ToString)
                Loop
            End If
            rd5.Close()
            cnn5.Close()

        Catch ex As Exception
            MessageBox.Show(ex.ToString)
            cnn5.Close()
        End Try
    End Sub

    Private Sub txtPrecio_TextChanged(sender As Object, e As EventArgs) Handles txtPrecio.TextChanged
        txtTotal.Text = CDbl(IIf(txtCantidad.Text = "" Or txtCantidad.Text = ".", "0", txtCantidad.Text)) * CDbl(IIf(txtPrecio.Text = "" Or txtPrecio.Text = ".", "0", txtPrecio.Text))
        txtTotal.Text = FormatNumber(txtTotal.Text, 4)
    End Sub

    Private Sub txtCantidad_TextChanged(sender As Object, e As EventArgs) Handles txtCantidad.TextChanged

        If txtCantidad.Text = "" Or txtCantidad.Text = "." Then Exit Sub

        txtTotal.Text = CDbl(IIf(txtPrecio.Text = "", "0", txtPrecio.Text)) * CDbl(IIf(txtCantidad.Text = "", "0", txtCantidad.Text))
        txtTotal.Text = FormatNumber(txtTotal.Text, 4)
    End Sub

    Private Sub txtDescuento_TextChanged(sender As Object, e As EventArgs) Handles txtDescuento.TextChanged

        If descuento = 1 Then
            Dim resta As Double = 0

            If txtDescuento.Text = "" Then
                txtDescuento.Text = "0.00"
                txtTotalVenta.Text = txtSubtotal.Text
                Exit Sub
            End If

            Dim SUBTOTAL As Double = IIf(txtSubtotal.Text = "", "0", txtSubtotal.Text)
            Dim descuento As Double = IIf(txtDescuento.Text = "", "0", txtDescuento.Text)
            Dim pordescuento As Double = 0
            pordescuento = (descuento * 100) / CDbl(SUBTOTAL)

            txtdescu.Text = (descuento / 100) * CDbl(SUBTOTAL)
            txtdescu.Text = FormatNumber(txtdescu.Text, 2)
            txtTotalVenta.Text = CDbl(SUBTOTAL) - ((descuento / 100) * CDbl(SUBTOTAL))
            txtTotalVenta.Text = FormatNumber(txtTotalVenta.Text, 2)
        End If


    End Sub

    Private Sub frmCotizaciones_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        descuento = 0

        tim.Interval = 5000
        AddHandler tim.Tick, AddressOf Timer_Tick
        tim.Start()

    End Sub

    Private Sub Timer_Tick(sender As Object, e As EventArgs)
        tim.Stop()

        Dim cnn2 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
        Dim rd2 As MySqlDataReader
        Dim cmd2 As MySqlCommand

        Try
            cnn2.Close() : cnn2.Open()
            cmd2 = cnn2.CreateCommand
            cmd2.CommandText = "SELECT MAX(Folio) FROM cotped"
            rd2 = cmd2.ExecuteReader
            If rd2.HasRows Then
                If rd2.Read Then
                    lblFolio.Text = IIf(rd2(0).ToString = "", "0", rd2(0).ToString) + 1
                End If
            Else
                lblFolio.Text = "1"
            End If
            rd2.Close()
            cnn2.Close()
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
            cnn2.Close()
        End Try

        tim.Start()
    End Sub

    Private Sub txtDescuento_Click(sender As Object, e As EventArgs) Handles txtDescuento.Click
        descuento = 1
        txtDescuento.SelectionStart = 0
        txtDescuento.SelectionLength = Len(txtDescuento.Text)
    End Sub

    Private Sub txtDescuento_GotFocus(sender As Object, e As EventArgs) Handles txtDescuento.GotFocus
        descuento = 1
        txtDescuento.SelectionStart = 0
        txtDescuento.SelectionLength = Len(txtDescuento.Text)
    End Sub

    Private Sub btnAsignar_Click(sender As Object, e As EventArgs) Handles btnAsignar.Click

        Dim cnn2 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
        Dim rd2 As MySqlDataReader
        Dim cmd2 As MySqlCommand

        Try

            If lblUsuario.Text = "" Then MsgBox("Ingrese la contraseña de favor", vbInformation + vbOKOnly) : txtUsuario.Focus.Equals(True)

            If grdCaptura.Rows.Count > 0 Then
                For luffy As Integer = 0 To grdCaptura.Rows.Count - 1

                    Dim codigo As String = grdCaptura.Rows(luffy).Cells(0).Value.ToString
                    Dim nombre As String = grdCaptura.Rows(luffy).Cells(1).Value.ToString
                    Dim parte As String = grdCaptura.Rows(luffy).Cells(6).Value
                    Dim cantidad As String = grdCaptura.Rows(luffy).Cells(3).Value.ToString
                    Dim PRECIO As Double = grdCaptura.Rows(luffy).Cells(4).Value.ToString


                    cnn2.Close() : cnn2.Open()
                    cmd2 = cnn2.CreateCommand
                    cmd2.CommandText = "INSERT INTO comandasveh(IdVehiculo,Vehiculo,Placa,Cliente,Codigo,Nombre,NParte,Cantidad,Precio,Fecha) VALUES(" & lblidVehiculo.Text & ",'" & cboVehiculo.Text & "','" & lblPlaca.Text & "','" & cboCliente.Text & "','" & codigo & "','" & nombre & "','" & parte & "'," & cantidad & ",'" & PRECIO & "','" & Format(Date.Now, "yyyy-MM-dd") & "')"
                    cmd2.ExecuteNonQuery()
                    cnn2.Close()

                Next

                Panel6.Visible = True
                My.Application.DoEvents()
                Insert_Asignacion()
                PDF_Asignacion()
                Panel6.Visible = False
                My.Application.DoEvents()

                MsgBox("Refacciones agregadas correctamente.", vbInformation + vbOKOnly, titulorefaccionaria)
                grdCaptura.Rows.Clear()
                frmTallerR.TVehiculo.Start()
            Else
                MsgBox("Debes de seleccionar las refacciones", vbInformation + vbOKOnly, titulorefaccionaria)
                Exit Sub
            End If
            frmTallerR.pVehiculos.Controls.Clear()
            Me.Close()
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
            cnn2.Close()
        End Try

    End Sub

    Private Sub cboVehiculo_SelectedValueChanged(sender As Object, e As EventArgs) Handles cboVehiculo.SelectedValueChanged
        Dim cnn1 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
        Dim rd1 As MySqlDataReader
        Dim cmd1 As MySqlCommand

        Try
            cnn1.Close() : cnn1.Open()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "SELECT IdVehiculo,Placa FROM vehiculo WHERE Descripcion='" & cboVehiculo.Text & "' AND Cliente='" & cboCliente.Text & "'"
            rd1 = cmd1.ExecuteReader
            If rd1.HasRows Then
                If rd1.Read Then
                    lblidVehiculo.Text = rd1(0).ToString
                    lblPlaca.Text = rd1(1).ToString
                End If
            End If
            rd1.Close()
            cnn1.Close()

        Catch ex As Exception
            MessageBox.Show(ex.ToString)
            cnn1.Close()
        End Try
    End Sub

    Public Sub Insert_Asignacion()
        Dim oData As New ToolKitSQL.oledbdata
        Dim sSql As String = ""
        Dim a_cnn As OleDb.OleDbConnection = New OleDb.OleDbConnection
        Dim sInfo As String = ""
        Dim dr As DataRow = Nothing
        Dim dt As New DataTable

        Dim cnn3 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
        Dim rd3 As MySqlDataReader
        Dim cmd3 As MySqlCommand

        Dim MyStatus As String = ""

        With oData
            If .dbOpen(a_cnn, Direcc_Access, sInfo) Then
                .runSp(a_cnn, "delete from Taller", sInfo) : sInfo = ""
                .runSp(a_cnn, "delete from TallerDet", sInfo) : sInfo = ""

                If cboCliente.Text <> "" Then
                    cnn3.Close() : cnn3.Open()
                    cmd3 = cnn3.CreateCommand
                    cmd3.CommandText =
                        "select Marca,Modelo from vehiculo WHERE Descripcion='" & cboVehiculo.Text & "'"
                    rd3 = cmd3.ExecuteReader
                    If rd3.HasRows Then
                        If rd3.Read Then
                            marca = rd3("Marca").ToString
                            modelo = rd3("Modelo").ToString
                        End If
                    End If
                    rd3.Close() : cnn3.Close()
                End If

                If .runSp(a_cnn, "insert into Taller(IdCliente,Cliente,Telefono,IdVehiculo,Vehiculo,Modelo,Marca,Placa) values(" & idcliente & ",'" & cboCliente.Text & "','" & telefono & "'," & lblidVehiculo.Text & ",'" & cboVehiculo.Text & "','" & modelo & "','" & marca & "','" & lblPlaca.Text & "')", sInfo) Then
                    sInfo = ""
                Else
                    MsgBox(sInfo)
                End If

                If .getDr(a_cnn, dr, "select MAX(Folio) from Taller", sInfo) Then
                    my_folio = dr(0).ToString()
                End If

                Dim cod_temp As String = ""

                Dim ruta_imagen As String = ""




                For pipo As Integer = 0 To grdCaptura.Rows.Count - 1
                    Dim myund As String = ""

                    Dim codigo As String = grdCaptura.Rows(pipo).Cells(0).Value.ToString()
                    If codigo = "" Then GoTo doorcita

                    'Traa la imgen del producto para la cotización
                    If File.Exists("C:\ControlNegociosPro\ProductosImg\" & codigo & ".jpg") Then
                        ruta_imagen = "C:\ControlNegociosPro\ProductosImg\" & codigo & ".jpg"
                    Else
                        If varrutabase <> "" Then
                            If File.Exists("\\" & varrutabase & "\ControlNegociosPro\ProductosImg\" & codigo & ".jpg") Then
                                ruta_imagen = "\\" & varrutabase & "\ControlNegociosPro\ProductosImg\" & codigo & ".jpg"
                            Else
                                ruta_imagen = ""
                            End If
                        Else
                            ruta_imagen = ""
                        End If
                    End If

                    Dim nombre As String = grdCaptura.Rows(pipo).Cells(1).Value.ToString()
                    Dim unidad As String = grdCaptura.Rows(pipo).Cells(2).Value.ToString
                    Dim cantidad As Double = grdCaptura.Rows(pipo).Cells(3).Value.ToString()
                    Dim precio_original As Double = grdCaptura.Rows(pipo).Cells(4).Value.ToString()
                    Dim total_original As Double = precio_original * cantidad

                    If codigo <> "" Then
                        cod_temp = codigo
                        If .runSp(a_cnn, "insert into TallerDet(FolioT,Codigo,Nombre,Unidad,Cantidad,Precio,Total) values(" & my_folio & ",'" & codigo & "','" & nombre & "','" & unidad & "'," & cantidad & "," & precio_original & "," & total_original & ")", sInfo) Then
                            sInfo = ""
                        Else
                            MsgBox(sInfo)
                        End If
                    End If
                    Continue For
doorcita:


                Next
                a_cnn.Close()
            End If
        End With
    End Sub

    Public Sub PDF_Asignacion()
        Dim root_name_recibo As String = ""
        Dim FileOpen As New ProcessStartInfo

        'Nombre del CrystalReport
        Dim FileNta As New HojaServ

        Dim strServerName As String = Application.StartupPath
        Dim crtableLogoninfos As New TableLogOnInfos
        Dim crtableLogoninfo As New TableLogOnInfo
        Dim crConnectionInfo As New ConnectionInfo
        Dim CrTables As Tables
        Dim CrTable As Table

        Dim cnn1 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
        Dim rd1 As MySqlDataReader
        Dim cmd1 As MySqlCommand

        crea_ruta(My.Application.Info.DirectoryPath & "\ARCHIVOSDL1\ORDEN_SERVICIO\")
        root_name_recibo = My.Application.Info.DirectoryPath & "\ARCHIVOSDL1\ORDEN_SERVICIO\" & my_folio & ".pdf"

        If File.Exists(My.Application.Info.DirectoryPath & "\ARCHIVOSDL1\ORDEN_SERVICIO\" & my_folio & ".pdf") Then
            File.Delete(My.Application.Info.DirectoryPath & "\ARCHIVOSDL1\ORDEN_SERVICIO\" & my_folio & ".pdf")
        End If

        If varrutabase <> "" Then
            If File.Exists("\\" & varrutabase & "\ControlNegociosPro\ARCHIVOSDL1\ORDEN_SERVICIO\" & my_folio & ".pdf") Then
                File.Delete("\\" & varrutabase & "\ControlNegociosPro\ARCHIVOSDL1\ORDEN_SERVICIO\" & my_folio & ".pdf")
            End If
        End If

        With crConnectionInfo
            .ServerName = My.Application.Info.DirectoryPath & "\DL1.mdb"
            .DatabaseName = My.Application.Info.DirectoryPath & "\DL1.mdb"
            .UserID = ""
            .Password = "jipl22"
        End With

        CrTables = FileNta.Database.Tables
        For Each CrTable In CrTables
            crtableLogoninfo = CrTable.LogOnInfo
            crtableLogoninfo.ConnectionInfo = crConnectionInfo
            CrTable.ApplyLogOnInfo(crtableLogoninfo)
        Next


        Dim TotalIEPSPrint As Double = 0
        Dim SubtotalPrint As Double = 0
        Dim MySubtotal As Double = 0
        Dim TotalIVAPrint As Double = 0

        Dim SubTotal As Double = 0
        Dim IVA_Vent As Double = 0
        Dim Total_Ve As Double = 0
        Dim mytotalventa2 As Double = 0

        Try
            cnn1.Close() : cnn1.Open()
            For N As Integer = 0 To grdCaptura.Rows.Count - 1
                If CStr(grdCaptura.Rows(N).Cells(0).Value.ToString) <> "" Then

                    mytotalventa2 = mytotalventa2 + CDbl(grdCaptura.Rows(N).Cells(5).Value.ToString)

                    cmd1 = cnn1.CreateCommand
                    cmd1.CommandText =
                        "select IVA from Productos where Codigo='" & CStr(grdCaptura.Rows(N).Cells(0).Value.ToString) & "'"
                    rd1 = cmd1.ExecuteReader
                    If rd1.HasRows Then
                        If rd1.Read Then
                            MySubtotal = MySubtotal + (CDbl(mytotalventa2) / (1 + CDbl(rd1(0).ToString)))
                        End If
                    End If
                    rd1.Close()
                End If
            Next
            TotalIVAPrint = FormatNumber(TotalIVAPrint, 2)
            MySubtotal = FormatNumber(MySubtotal, 2)
            mytotalventa2 = FormatNumber(mytotalventa2, 2)

            cnn1.Close()
        Catch ex As Exception
            MessageBox.Show(ex.ToString())
            cnn1.Close()
        End Try

        IVA_Vent = FormatNumber(CDbl(mytotalventa2) - CDbl(TotalIVAPrint), 2)
        SubTotal = FormatNumber(TotalIVAPrint, 2)
        Total_Ve = FormatNumber(CDbl(txtTotalVenta.Text), 2)

        FileNta.SetDatabaseLogon("", "jipl22")
        FileNta.DataDefinition.FormulaFields("conLetra").Text = "'" & convLetras(txtTotalVenta.Text) & "'"
        FileNta.DataDefinition.FormulaFields("Usuario").Text = "'" & lblUsuario.Text & "'"





        Dim total_des As Double = Total_Ve - CDbl(txtdescu.Text)

        'Total
        If CDbl(txtdescu.Text) > 0 Then
            FileNta.DataDefinition.FormulaFields("Subtotal").Text = "'" & FormatNumber(Total_Ve, 2) & "'"
            FileNta.DataDefinition.FormulaFields("TotalT").Text = "'" & FormatNumber(total_des, 2) & "'"             'Total
            FileNta.DataDefinition.FormulaFields("Descuento").Text = "'" & FormatNumber(txtdescu.Text, 2) & "'"
        Else
            FileNta.DataDefinition.FormulaFields("TotalT").Text = "'" & FormatNumber(Total_Ve, 2) & "'"
        End If



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
            System.IO.File.Copy(My.Application.Info.DirectoryPath & "\ARCHIVOSDL1\ORDEN_SERVICIO\" & my_folio & ".pdf", "\\" & varrutabase & "\ControlNegociosPro\ARCHIVOSDL1\ORDEN_SERVICIO\" & my_folio & ".pdf")
        End If
    End Sub

    Private Sub cboCliente_SelectedValueChanged(sender As Object, e As EventArgs) Handles cboCliente.SelectedValueChanged
        Dim cnn1 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
        Dim rd1 As MySqlDataReader
        Dim cmd1 As MySqlCommand

        Try
            cnn1.Close() : cnn1.Open()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "SELECT Id,Telefono FROM clientes WHERE Nombre='" & cboCliente.Text & "'"
            rd1 = cmd1.ExecuteReader
            If rd1.HasRows Then
                If rd1.Read Then
                    idcliente = rd1(0).ToString
                    telefono = rd1(1).ToString
                End If
            End If
            rd1.Close()
            cnn1.Close()

        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
    End Sub

    Private Sub cboVehiculo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cboVehiculo.KeyPress
        If AscW(e.KeyChar) = Keys.Enter Then
            cboDescripcion.Focus.Equals(True)
        End If
    End Sub

    Private Sub cboCliente_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cboCliente.KeyPress
        If AscW(e.KeyChar) = Keys.Enter Then
            cboVehiculo.Focus.Equals(True)
        End If
    End Sub
End Class