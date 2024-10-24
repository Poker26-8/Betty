
Imports System.IO
Imports CrystalDecisions.Shared
Imports CrystalDecisions.CrystalReports.Engine
Imports DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing

Public Class frmAsignacionRef
    Dim idcliente As Integer = 0

    Public marcaveh As String = ""
    Public placa As String = ""
    Public idvehiculo As Integer = 0
    Private Sub frmAsignacionRef_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub txtNumParte_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtNumParte.KeyPress
        If AscW(e.KeyChar) = Keys.Enter Then

            txtCantidad.Focus.Equals(True)

        End If
    End Sub

    Private Sub txtCantidad_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtCantidad.KeyPress
        If AscW(e.KeyChar) = Keys.Enter Then

            Dim codigopro As String = ""
            Dim preciopro As Double = 0
            Dim tot As Double = 0

            cnn1.Close() : cnn1.Open()
            cmd1 = cnn1.CreateCommand

            If txtNumParte.Text <> "" Then
                cmd1.CommandText = "SELECT Codigo,Nombre,N_Serie,PrecioVentaIVA FROM productos  WHERE N_Serie='" & txtNumParte.Text & "'"
            End If

            If cboDescripcion.Text <> "" Then
                cmd1.CommandText = "SELECT Codigo,Nombre,N_Serie,PrecioVentaIVA FROM productos  WHERE Nombre='" & cboDescripcion.Text & "'"
            End If
            rd1 = cmd1.ExecuteReader
            If rd1.HasRows Then
                If rd1.Read Then
                    codigopro = rd1(0).ToString
                    preciopro = IIf(rd1(3).ToString = "", 0, rd1(3).ToString)

                    grdRefaccion.Rows.Add(codigopro,
                                          rd1(1).ToString,
                                          rd1(2).ToString,
                                          FormatNumber(txtCantidad.Text, 2),
                                          FormatNumber(preciopro, 2))

                    txtTotal.Text = CDbl(txtTotal.Text) + CDbl(preciopro)
                End If
            End If
            rd1.Close()
            cnn1.Close()

            txtCantidad.Text = "1"
            txtNumParte.Text = ""
            cboDescripcion.Text = ""
            cboDescripcion.Focus.Equals(True)

            txtTotal.Text = FormatNumber(txtTotal.Text, 2)
        End If
    End Sub

    Private Sub cboDescripcion_DropDown(sender As Object, e As EventArgs) Handles cboDescripcion.DropDown
        Try
            cboDescripcion.Items.Clear()

            cnn5.Close() : cnn5.Open()
            cmd5 = cnn5.CreateCommand
            cmd5.CommandText = "SELECT DISTINCT Nombre FROM productos WHERE Nombre<>'' ORDER BY Nombre"
            rd5 = cmd5.ExecuteReader
            Do While rd5.Read
                If rd5.HasRows Then
                    cboDescripcion.Items.Add(rd5(0).ToString)
                End If
            Loop
            rd5.Close()
            cnn5.Close()

        Catch ex As Exception
            MessageBox.Show(ex.ToString)
            cnn5.Close()
        End Try
    End Sub

    Private Sub cboDescripcion_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cboDescripcion.KeyPress
        If AscW(e.KeyChar) = Keys.Enter Then
            txtCantidad.Focus.Equals(True)
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Close()
    End Sub

    Private Sub grdRefaccion_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles grdRefaccion.CellDoubleClick
        Dim index As Integer = grdRefaccion.CurrentRow.Index

        Dim pre As Double = grdRefaccion.Rows(index).Cells(4).Value.ToString

        txtTotal.Text = CDbl(txtTotal.Text) - CDbl(pre)
        txtTotal.Text = FormatNumber(txtTotal.Text, 2)
        grdRefaccion.Rows.Remove(grdRefaccion.CurrentRow)
    End Sub

    Private Sub btnEnviar_Click(sender As Object, e As EventArgs) Handles btnEnviar.Click
        Try
            If grdRefaccion.Rows.Count > 0 Then
                For luffy As Integer = 0 To grdRefaccion.Rows.Count - 1

                    Dim codigo As String = grdRefaccion.Rows(luffy).Cells(0).Value.ToString
                    Dim nombre As String = grdRefaccion.Rows(luffy).Cells(1).Value.ToString
                    Dim parte As String = grdRefaccion.Rows(luffy).Cells(2).Value.ToString
                    Dim cantidad As String = grdRefaccion.Rows(luffy).Cells(3).Value.ToString
                    Dim PRECIO As Double = grdRefaccion.Rows(luffy).Cells(4).Value.ToString


                    cnn2.Close() : cnn2.Open()
                    cmd2 = cnn2.CreateCommand
                    cmd2.CommandText = "INSERT INTO comandasveh(IdVehiculo,Vehiculo,Placa,Cliente,Codigo,Nombre,NParte,Cantidad,Precio,Fecha) VALUES(" & idvehiculo & ",'" & txtVeh.Text & "','" & placa & "','" & txtCliente.Text & "','" & codigo & "','" & nombre & "','" & parte & "'," & cantidad & ",'" & PRECIO & "','" & Format(Date.Now, "yyyy-MM-dd") & "')"
                    cmd2.ExecuteNonQuery()
                    cnn2.Close()

                Next
                MsgBox("Refacciones agregadas correctamente.", vbInformation + vbOKOnly, titulorefaccionaria)
                grdRefaccion.Rows.Clear()
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

    Private Sub btnCotizacion_Click(sender As Object, e As EventArgs) Handles btnCotizacion.Click


        If grdRefaccion.Rows.Count = 0 Then MsgBox("Captura productos para guardar la cotización.", vbInformation + vbOKOnly, titulorefaccionaria) : cboDescripcion.Focus().Equals(True) : Exit Sub

        If MsgBox("¿Deseas guardar los datos de esta cotización?", vbInformation + vbOKCancel, titulorefaccionaria) = vbCancel Then cnn1.Close() : Exit Sub

        Dim MySubtotal As Double = 0
        Dim totalp As Double = 0
        Dim ivaventa As Double = 0
        Dim totalventa As Double = 0


        Try
            cnn1.Close() : cnn1.Open()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "SELECT Id FROM clientes WHERE Nombre='" & txtCliente.Text & "'"
            rd1 = cmd1.ExecuteReader
            If rd1.HasRows Then
                If rd1.Read Then
                    idcliente = rd1(0).ToString
                End If
            End If
            rd1.Close()

            For i As Integer = 0 To grdRefaccion.Rows.Count - 1

                totalp = CDbl(grdRefaccion.Rows(i).Cells(3).Value.ToString) * CDbl(grdRefaccion.Rows(i).Cells(4).Value.ToString)

                cmd1 = cnn1.CreateCommand
                cmd1.CommandText =
                "select IVA from Productos where Codigo='" & grdRefaccion.Rows(i).Cells(0).Value.ToString() & "'"
                rd1 = cmd1.ExecuteReader
                If rd1.HasRows Then
                    If rd1.Read Then
                        MySubtotal = MySubtotal + (CDbl(totalp) / (1 + CDbl(rd1(0).ToString)))
                    End If
                End If
                rd1.Close()

                totalventa = totalventa + totalp
            Next
            cnn1.Close()

            ivaventa = CDbl(totalventa) - CDbl(MySubtotal)

            cnn1.Close() : cnn1.Open()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText =
                "insert into CotPed(IdCliente,Cliente,Direccion,Subtotal,IVA,Totales,ACuenta,Resta,Usuario,Fecha,Hora,Status,Tipo,Comentario,IP) values(" & idcliente & ",'" & txtCliente.Text & "',''," & MySubtotal & "," & ivaventa & "," & totalventa & ",0,0,'','" & Format(Date.Now, "yyyy-MM-dd") & "','" & Format(Date.Now, "HH:mm:ss") & "','','COTIZACION','','" & dameIP2() & "')"
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
            For T As Integer = 0 To grdRefaccion.Rows.Count - 1
                If grdRefaccion.Rows(T).Cells(0).Value.ToString = "" Then GoTo Door

                Dim mycode As String = grdRefaccion.Rows(T).Cells(0).Value.ToString
                Dim mydesc As String = grdRefaccion.Rows(T).Cells(1).Value.ToString
                Dim mycant As Double = grdRefaccion.Rows(T).Cells(3).Value.ToString
                Dim myprecio As Double = grdRefaccion.Rows(T).Cells(4).Value.ToString
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

                If grdRefaccion.Rows(T).Cells(0).Value.ToString <> "" Then
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


            grdRefaccion.Rows.Clear()
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

        With oData
            If .dbOpen(a_cnn, Direcc_Access, sInfo) Then
                .runSp(a_cnn, "delete from CotPedDetalle", sInfo) : sInfo = ""
                .runSp(a_cnn, "delete from CotPed", sInfo) : sInfo = ""

                If txtCliente.Text <> "" Then
                    cnn3.Close() : cnn3.Open()
                    cmd3 = cnn3.CreateCommand
                    cmd3.CommandText =
                        "select Telefono from Clientes where Nombre='" & txtCliente.Text & "'"
                    rd3 = cmd3.ExecuteReader
                    If rd3.HasRows Then
                        If rd3.Read Then
                            tel_cliente = rd3("Telefono").ToString()
                        End If
                    End If
                    rd3.Close() : cnn3.Close()
                End If

                If .runSp(a_cnn, "insert into CotPed(idCliente,Nombre,Direccion,Totales,Descuento,ACuenta,Resta,Usuario,FVenta,HVenta,Status,MontoSnDesc,Comentario,Telefono) values(" & idcliente & ",'" & txtCliente.Text & "','',0,0,0,0,'',#" & FormatDateTime(Date.Now, DateFormat.ShortDate) & "#,#" & FormatDateTime(Date.Now, DateFormat.ShortTime) & "#,'COTIZACION',0,'','" & tel_cliente & "')", sInfo) Then
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

                For pipo As Integer = 0 To grdRefaccion.Rows.Count - 1
                    Dim myund As String = ""

                    Dim codigo As String = grdRefaccion.Rows(pipo).Cells(0).Value.ToString()
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


                    Dim nombre As String = grdRefaccion.Rows(pipo).Cells(1).Value.ToString()
                    Dim cantidad As Double = grdRefaccion.Rows(pipo).Cells(3).Value.ToString()
                    Dim precio_original As Double = grdRefaccion.Rows(pipo).Cells(4).Value.ToString()
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
                    If grdRefaccion.Rows(pipo).Cells(1).Value.ToString() <> "" Then
                        Dim id_a As Integer = 0
                        If .getDr(a_cnn, dr, "select MAX(Id) from CotPedDetalle", sInfo) Then
                            id_a = dr(0).ToString()
                        End If
                        'Es comentario 
                        .runSp(a_cnn, "update CotPedDetalle set Comentario='" & grdRefaccion.Rows(pipo).Cells(1).Value.ToString() & "' where Id=" & id_a, sInfo)
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
            For N As Integer = 0 To grdRefaccion.Rows.Count - 1
                If CStr(grdRefaccion.Rows(N).Cells(0).Value.ToString) <> "" Then

                    mytotalventa2 = mytotalventa2 + (CDbl(grdRefaccion.Rows(N).Cells(3).Value.ToString) * CDbl(grdRefaccion.Rows(N).Cells(4).Value.ToString))

                    cmd1 = cnn1.CreateCommand
                    cmd1.CommandText =
                        "select IVA from Productos where Codigo='" & CStr(grdRefaccion.Rows(N).Cells(0).Value.ToString) & "'"
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
        Total_Ve = FormatNumber(CDbl(mytotalventa2), 2)

        FileNta.SetDatabaseLogon("", "jipl22")
        FileNta.DataDefinition.FormulaFields("Folio").Text = "'" & MyFolio & "'"
        '   FileNta.DataDefinition.FormulaFields("Usuario").Text = "'" & lblusuario.Text & "'"
        FileNta.DataDefinition.FormulaFields("conLetra").Text = "'" & convLetras(mytotalventa2) & "'"

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

        'Dim total_des As Double = Total_Ve + CDbl(txtdescuento2.Text)

        FileNta.DataDefinition.FormulaFields("Total").Text = "'" & FormatNumber(Total_Ve, 2) & "'"             'Total
        'If CDbl(txtdescuento2.Text) > 0 Then
        '    FileNta.DataDefinition.FormulaFields("TTotal").Text = "'" & FormatNumber(total_des, 2) & "'"             'Total
        '    FileNta.DataDefinition.FormulaFields("Descuento").Text = "'" & FormatNumber(txtdescuento2.Text, 2) & "'"             'Total
        'End If


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

End Class