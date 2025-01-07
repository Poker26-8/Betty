Imports System.Web.UI.WebControls.WebParts
Imports Core.DAL.CFDI33
Imports DocumentFormat.OpenXml.Drawing.Charts
Imports DocumentFormat.OpenXml.Vml
Imports System.IO
Imports MySql.Data.MySqlClient
Public Class frmComprasModelos

    Dim nLogo As String = ""
    Dim tLogo As String = ""
    Dim simbolo As String = ""

    Dim Contador As Integer = 0
    Dim printLine As Integer = 0
    Dim pagina As Integer = 0

    Dim mycodigo As String = ""
    Dim myivacal As Double = 0

    Dim tipo_cancelacion As String = ""
    Dim pasa_pago As Boolean = False
    Dim alias_compras As String = ""

    Dim DondeVoy As String = ""
    Dim tipo_impre As String = ""

    Public Structure Pagos
        Shared pc_porpagar As Double = 0
        Shared pc_anticipo As Double = 0
        Shared pc_efectivo As Double = 0
        Shared pc_tarjeta As Double = 0
        Shared pc_transfe As Double = 0
        Shared pc_otros As Double = 0
        Shared pc_resta As Double = 0
    End Structure

    Private Sub frmComprasModelos_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Dim cnn1 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
        Dim cnn2 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
        Dim rd1, rd2 As MySqlDataReader
        Dim cmd1, cmd2 As MySqlCommand

        nLogo = DatosRecarga("LogoG")
        tLogo = DatosRecarga("TipoLogo")
        simbolo = DatosRecarga("Simbolo")

        dtpfecha.Value = Now
        txtUnidad.Text = ""

        Dim tomacontra As Integer = DatosRecarga("TomaContra")
        cnn2.Close() : cnn2.Open()
        If tomacontra = "1" Then

            cmd2 = cnn2.CreateCommand
            cmd2.CommandText = "SELECT Clave,Alias FROM Usuarios WHERE IdEmpleado=" & id_usu_log
            rd2 = cmd2.ExecuteReader
            If rd2.HasRows Then
                If rd2.Read Then
                    txtusuario.Text = rd2(0).ToString
                    lblusuario.Text = rd2(1).ToString
                    txtusuario.PasswordChar = "*"
                    txtusuario.ForeColor = Color.Black
                End If
            End If
            rd2.Close()
        End If
        cnn2.Close()

        panpago_compra.Visible = False
        txtpc_apagar.Text = "0.00"
        txtpc_anticipo.Text = "0.00"
        txtpc_efectivo.Text = "0.00"
        txtpc_tarjeta.Text = "0.00"
        txtpc_transfe.Text = "0.00"
        txtpc_otro.Text = "0.00"
        txtpc_resta.Text = "0.00"

        Pagos.pc_porpagar = 0
        Pagos.pc_anticipo = 0
        Pagos.pc_efectivo = 0
        Pagos.pc_tarjeta = 0
        Pagos.pc_transfe = 0
        Pagos.pc_otros = 0
        Pagos.pc_resta = 0

        grdCaptura.Rows.Clear()
        grdCaptura.DefaultCellStyle.ForeColor = Color.Black
        printLine = 0
        Contador = 0
        pagina = 0
        VieneDe_Compras = ""

        cnn1.Close() : cnn1.Open()
        cmd1 = cnn1.CreateCommand
        cmd1.CommandText =
            "select tipo_cambio,id,nombre_moneda from tb_moneda where nombre_moneda='PESO' or nombre_moneda='PESOS'"
        rd1 = cmd1.ExecuteReader
        If rd1.HasRows Then
            If rd1.Read Then
                txtvalor.Text = FormatNumber(rd1("tipo_cambio").ToString, 4)
                cbomoneda.Tag = rd1("id").ToString
                cbomoneda.Items.Add(rd1("nombre_moneda").ToString)
                cbomoneda.SelectedIndex = 0
            End If
        Else
            cbomoneda.Tag = "0"
        End If
        rd1.Close()
        If cnn1.State = 0 Then cnn1.Open()

        cbonombre.Focus().Equals(True)
    End Sub

    Private Sub cboproveedor_DropDown(sender As Object, e As EventArgs) Handles cboproveedor.DropDown
        Dim cnn5 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
        Dim rd5 As MySqlDataReader
        Dim cmd5 As MySqlCommand
        Try
            cboproveedor.Items.Clear()
            cnn5.Close() : cnn5.Open()
            cmd5 = cnn5.CreateCommand
            cmd5.CommandText = "SELECT DISTINCT Compania FROM proveedores WHERE Compania<>'' ORDER BY Compania"
            rd5 = cmd5.ExecuteReader
            Do While rd5.Read
                If rd5.HasRows Then
                    cboproveedor.Items.Add(rd5(0).ToString)
                End If
            Loop
            rd5.Close()
            cnn5.Close()

        Catch ex As Exception
            MessageBox.Show(ex.ToString)
            cnn5.Close()
        End Try
    End Sub

    Private Sub cboproveedor_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cboproveedor.KeyPress
        e.KeyChar = UCase(e.KeyChar)
        If AscW(e.KeyChar) = Keys.Enter Then
            cboremision.Focus.Equals(True)
        End If
    End Sub

    Private Sub cboproveedor_SelectedValueChanged(sender As Object, e As EventArgs) Handles cboproveedor.SelectedValueChanged
        Dim cnn1 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
        Dim rd1 As MySqlDataReader
        Dim cmd1 As MySqlCommand
        Try
            If cboproveedor.Text <> "" Then
                txtsaldo.Text = "0.00"
                Dim dias As Integer = 0
                Dim mysaldo As Double = 0

                Try
                    cnn1.Close() : cnn1.Open()
                    cmd1 = cnn1.CreateCommand
                    cmd1.CommandText = "SELECT DiasCred,Saldo FROM Proveedores WHERE Compania='" & cboproveedor.Text & "'"
                    rd1 = cmd1.ExecuteReader
                    If rd1.HasRows Then
                        If rd1.Read Then
                            dias = rd1("DiasCred").ToString
                            dtpfpago.Value = DateAdd(DateInterval.Day, dias, Date.Now)
                            mysaldo = rd1("Saldo").ToString
                        End If
                    End If
                    rd1.Close()

                    If mysaldo > 0 Then
                        Button1.Enabled = True
                        txtpAnticipo.Enabled = True
                        lblpAnticipo.Enabled = True
                        txtsaldo.Text = FormatNumber(Math.Abs(mysaldo), 2)
                    Else
                        txtsaldo.Text = "0.00"
                        Button1.Enabled = False
                        txtpAnticipo.Enabled = False
                        lblpAnticipo.Enabled = False
                    End If
                    cnn1.Close()

                Catch ex As Exception
                    MessageBox.Show(ex.ToString)
                    cnn1.Close()
                End Try
            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString)
            cnn1.Close()
        End Try
    End Sub

    Private Sub cbomoneda_DropDown(sender As Object, e As EventArgs) Handles cbomoneda.DropDown
        Dim cnn5 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
        Dim rd5 As MySqlDataReader
        Dim cmd5 As MySqlCommand
        Try
            cbomoneda.Items.Clear()
            cnn5.Close() : cnn5.Open()
            cmd5 = cnn5.CreateCommand
            cmd5.CommandText = "SELECT nombre_moneda FROM tb_moneda WHERE nombre_moneda<>'' ORDER BY nombre_moneda"
            rd5 = cmd5.ExecuteReader
            Do While rd5.Read
                If rd5.HasRows Then
                    cbomoneda.Items.Add(rd5(0).ToString)
                End If
            Loop
            rd5.Close()
            cnn5.Close()
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
            cnn5.Close()
        End Try
    End Sub

    Private Sub cbomoneda_SelectedValueChanged(sender As Object, e As EventArgs) Handles cbomoneda.SelectedValueChanged
        Dim cnn1 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
        Dim rd1 As MySqlDataReader
        Dim cmd1 As MySqlCommand
        Try
            cnn1.Close() : cnn1.Open()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "SELECT tipo_cambio,Id FROM tb_moneda WHERE nombre_moneda='" & cbomoneda.Text & "'"
            rd1 = cmd1.ExecuteReader
            If rd1.HasRows Then
                If rd1.Read Then
                    txtvalor.Text = FormatNumber(rd1("tipo_cambio").ToString, 2)
                    cbomoneda.Tag = rd1("Id").ToString
                End If
            Else
                txtvalor.Text = "0.00"
            End If
            rd1.Close()
            cnn1.Close()
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
            cnn1.Close()
        End Try
    End Sub

    Private Sub cbomoneda_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cbomoneda.KeyPress
        If AscW(e.KeyChar) = Keys.Enter Then
            txtvalor.Focus.Equals(True)
        End If
    End Sub

    Private Sub txtvalor_Click(sender As Object, e As EventArgs) Handles txtvalor.Click
        txtvalor.SelectionStart = 0
        txtvalor.SelectionLength = Len(-txtvalor.Text)
    End Sub

    Private Sub txtvalor_GotFocus(sender As Object, e As EventArgs) Handles txtvalor.GotFocus
        txtvalor.SelectionStart = 0
        txtvalor.SelectionLength = Len(-txtvalor.Text)
    End Sub

    Private Sub txtvalor_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtvalor.KeyPress
        If Not IsNumeric(txtvalor.Text) Then txtvalor.Text = "0.00"
        If AscW(e.KeyChar) = Keys.Enter Then
            cbonombre.Focus().Equals(True)
        End If
    End Sub

    Private Sub cboremision_Click(sender As Object, e As EventArgs) Handles cboremision.Click
        cboremision.SelectionStart = 0
        cboremision.SelectionLength = Len(cboremision.Text)
    End Sub

    Private Sub cboremision_DropDown(sender As Object, e As EventArgs) Handles cboremision.DropDown
        Dim cnn5 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
        Dim rd5 As MySqlDataReader
        Dim cmd5 As MySqlCommand
        Try
            cboremision.Items.Clear()
            cnn5.Close() : cnn5.Open()
            cmd5 = cnn5.CreateCommand
            cmd5.CommandText = "SELECT DISTINCT NumRemision FROM comprasentalla WHERE Proveedor='" & cboproveedor.Text & "' AND NumRemision<>'' ORDER BY NumRemision"
            rd5 = cmd5.ExecuteReader
            Do While rd5.Read
                If rd5.HasRows Then
                    cboremision.Items.Add(rd5(0).ToString)
                End If
            Loop
            rd5.Close()
            cnn5.Close()

        Catch ex As Exception
            MessageBox.Show(ex.ToString)
            cnn5.Close()
        End Try
    End Sub

    Private Sub cboremision_GotFocus(sender As Object, e As EventArgs) Handles cboremision.GotFocus
        cboremision.SelectionStart = 0
        cboremision.SelectionLength = Len(cboremision.Text)
    End Sub

    Private Sub cboremision_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cboremision.KeyPress
        e.KeyChar = UCase(e.KeyChar)
        If AscW(e.KeyChar) = Keys.Enter Then
            If cboproveedor.Text <> "" And cboremision.Text = "" Then MsgBox("El número de remisión no puede permanecer vacío.", vbInformation + vbOKOnly, "Delsscom Control Negocios Pro") : Exit Sub
            Dim Pedido As Integer = 0
            Dim conteo As Double = 0
            Dim myiva As Double = 0

            Dim cnn1 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
            Dim cnn2 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
            Dim rd1, rd2 As MySqlDataReader
            Dim cmd1, cmd2 As MySqlCommand

            Try
                cnn1.Close() : cnn1.Open()
                cmd1 = cnn1.CreateCommand
                cmd1.CommandText = "SELECT IVA FROM IVA WHERE Id=1"
                rd1 = cmd1.ExecuteReader
                If rd1.HasRows Then
                    If rd1.Read Then
                        myiva = rd1(0).ToString
                    End If
                End If
                rd1.Close()

                cmd1 = cnn1.CreateCommand
                cmd1.CommandText = "select NumRemision, NumFactura, NumPedido, FechaC from Compras where NumRemision='" & cboremision.Text & "' and Proveedor='" & cboproveedor.Text & "' and Status<>'CANCELADA'"
                rd1 = cmd1.ExecuteReader
                If rd1.HasRows Then
                    If rd1.Read Then
                        MsgBox("Este número de remisión ya existe en una compra previa.", vbInformation + vbOKOnly, "Delsscom Control Negocios Pro")
                        Pedido = 0
                        cbofactura.Text = rd1(1).ToString
                        cbopedido.Text = rd1(2).ToString
                        dtpfecha.Value = rd1(3).ToString
                        btnGuardar.Enabled = False
                        btncancela.Enabled = True
                        btnactualiza.Enabled = True
                        txtsaldo.Enabled = False
                        Button1.Enabled = False
                        txtpAnticipo.Enabled = False
                        lblpAnticipo.Enabled = False
                        grdCaptura.DefaultCellStyle.ForeColor = Color.DarkGoldenrod
                    End If
                Else
                    btnactualiza.Enabled = False
                    btncancela.Enabled = False
                    btnGuardar.Enabled = True
                    txtsaldo.Enabled = True
                    grdCaptura.DefaultCellStyle.ForeColor = Color.Black
                    cbofactura.Focus().Equals(True)
                End If
                rd1.Close()

                If Pedido = 0 Then
                    txtsub1.Text = "0.00"
                    txtdesc1.Text = "0.00"
                    txtsub2.Text = "0.00"
                    txtiva.Text = "0.00"
                    txtTotalC.Text = "0.00"
                    txtdesc2.Text = "0.00"
                    txtieps.Text = "0.00"
                    txtapagar.Text = "0.00"
                    txtanticipo.Text = "0.00"
                    txtefectivo.Text = "0.00"
                    txtpagos.Text = "0.00"
                    txtresta.Text = "0.00"
                End If

                cnn2.Close() : cnn2.Open()

                cmd1 = cnn1.CreateCommand
                cmd1.CommandText = "select NumFactura FROM Compras where NumRemision='" & cboremision.Text & "' and Proveedor='" & cboproveedor.Text & "'"
                rd1 = cmd1.ExecuteReader
                If rd1.HasRows Then
                    If rd1.Read Then
                        cbofactura.Text = rd1(0).ToString
                    End If
                End If
                rd1.Close()

                Dim mycode As String = ""
                Dim mymodelo As String = ""
                Dim mydescripcion As String = ""
                Dim myucompra As String = ""
                Dim mymultiplo As Double = 0
                Dim mycant As Double = 0
                Dim mypreciou As Double = 0
                Dim precioconiva As Double = 0
                Dim precioventa As Double = 0
                Dim mytotal As Double = 0

                cmd1 = cnn1.CreateCommand
                cmd1.CommandText = "SELECT * FROM comprasentalla WHERE NumRemision='" & cboremision.Text & "' AND Proveedor='" & cboproveedor.Text & "'"
                rd1 = cmd1.ExecuteReader
                Do While rd1.Read
                    If rd1.HasRows Then

                        mycode = rd1("Codigo").ToString
                        mymodelo = rd1("Modelo").ToString

                        cmd2 = cnn2.CreateCommand
                        cmd2.CommandText = "SELECT Nombre,UCompra,Multiplo FROM productos WHERE Grupo='" & mymodelo & "'"
                        rd2 = cmd2.ExecuteReader
                        If rd2.HasRows Then
                            If rd2.Read Then
                                mydescripcion = rd2("Nombre").ToString
                                myucompra = rd2("UCompra").ToString
                                mymultiplo = rd2("Multiplo").ToString

                            End If
                        End If
                        rd2.Close()
                        mycant = rd1("Cantidad").ToString
                        mypreciou = rd1("Precio").ToString
                        precioconiva = mypreciou + (mypreciou * myiva)
                        precioventa = rd1("precioVenta").ToString
                        mytotal = CDec(IIf(mycant = 0, 0, mycant)) * CDec(IIf(mypreciou = 0, 0, mypreciou))

                        grdCaptura.Rows.Add(
                            mymodelo,
                            myucompra,
                            mycant,
                             FormatNumber(mypreciou, 2),
                            FormatNumber(precioconiva, 2),
                            precioventa,
                            mytotal,
                            0,
                            0,
                            0,
                            0
                            )
                        conteo = conteo + mycant
                    End If
                Loop
                rd1.Close()
                cnn2.Close()

                txtprods.Text = conteo

                cmd1 = cnn1.CreateCommand
                cmd1.CommandText =
                    "select Sub1,Desc1,Sub2,IVA,Total,Desc2,IEPS,Pagar,Resta,FechaC,FechaP from Compras where NumRemision='" & cboremision.Text & "' and NumFactura='" & cbofactura.Text & "' and Proveedor='" & cboproveedor.Text & "'"
                rd1 = cmd1.ExecuteReader
                If rd1.HasRows Then
                    If rd1.Read Then
                        txtsub1.Text = FormatNumber(rd1("Sub1").ToString, 2)
                        txtdesc1.Text = FormatNumber(rd1("Desc1").ToString, 2)
                        txtsub2.Text = FormatNumber(rd1("Sub2").ToString, 2)
                        txtiva.Text = FormatNumber(rd1("IVA").ToString, 2)
                        txtTotalC.Text = FormatNumber(rd1("Total")).ToString
                        txtdesc2.Text = FormatNumber(rd1("Desc2".ToString), 2)
                        txtieps.Text = FormatNumber(rd1("IEPS").ToString, 2)
                        txtapagar.Text = FormatNumber(rd1("Pagar").ToString, 2)
                        txtresta.Text = FormatNumber(rd1("Resta").ToString, 2)
                        dtpfecha.Value = rd1("FechaC").ToString
                        dtpfpago.Value = rd1("FechaP").ToString
                    End If
                End If
                rd1.Close()
                cnn1.Close()
            Catch ex As Exception
                MessageBox.Show(ex.ToString)
                cnn1.Close()
            End Try
            cbofactura.Focus().Equals(True)
        End If
    End Sub

    Private Sub cboremision_SelectedValueChanged(sender As Object, e As EventArgs) Handles cboremision.SelectedValueChanged
        If cboremision.Text <> "" Then
            Call cboremision_KeyPress(cboremision, New KeyPressEventArgs(ChrW(Keys.Enter)))
        End If
    End Sub

    Private Sub cbofactura_DropDown(sender As Object, e As EventArgs) Handles cbofactura.DropDown
        cbofactura.Items.Clear()

        Dim cnn5 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
        Dim rd5 As MySqlDataReader
        Dim cmd5 As MySqlCommand
        Try
            cnn5.Close() : cnn5.Open()
            cmd5 = cnn5.CreateCommand
            cmd5.CommandText = "SELECT DISTINCT NumFactura FROM comprasentalla WHERE Proveedor='" & cboproveedor.Text & "' and NumFactura<>''"
            rd5 = cmd5.ExecuteReader
            Do While rd5.Read
                If rd5.HasRows Then cbofactura.Items.Add(
                    rd5(0).ToString
                    )
            Loop
            rd5.Close()
            cnn5.Close()
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
            cnn5.Close()
        End Try
    End Sub

    Private Sub cbofactura_Click(sender As Object, e As EventArgs) Handles cbofactura.Click
        cbofactura.SelectionStart = 0
        cbofactura.SelectionLength = Len(cbofactura.Text)
    End Sub

    Private Sub cbofactura_GotFocus(sender As Object, e As EventArgs) Handles cbofactura.GotFocus
        cbofactura.SelectionStart = 0
        cbofactura.SelectionLength = Len(cbofactura.Text)
    End Sub

    Private Sub cbofactura_SelectedValueChanged(sender As Object, e As EventArgs) Handles cbofactura.SelectedValueChanged
        If cbofactura.Text <> "" Then
            Call cbofactura_KeyPress(cbofactura, New KeyPressEventArgs(ChrW(Keys.Enter)))
        End If
    End Sub

    Private Sub cbofactura_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cbofactura.KeyPress
        e.KeyChar = UCase(e.KeyChar)
        If AscW(e.KeyChar) = Keys.Enter Then
            If cbofactura.Text = "" Then cbopedido.Focus().Equals(True) : Exit Sub
            Dim Pedido As Integer = 0
            Dim myiva As Double = 0

            Dim cnn1 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
            Dim cnn2 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
            Dim rd1, rd2 As MySqlDataReader
            Dim cmd1, cmd2 As MySqlCommand

            Try
                cnn1.Close() : cnn1.Open()
                cmd1 = cnn1.CreateCommand
                cmd1.CommandText = "SELECT IVA FROM iva WHERE Id=1"
                rd1 = cmd1.ExecuteReader
                If rd1.HasRows Then
                    If rd1.Read Then
                        myiva = rd1(0).ToString
                    End If
                End If
                rd1.Close()

                cmd1 = cnn1.CreateCommand
                cmd1.CommandText =
                    "select NumRemision, NumFactura, NumPedido, FechaC from Compras where NumFactura='" & cbofactura.Text & "' and Proveedor='" & cboproveedor.Text & "' and Status<>'CANCELADA'"
                rd1 = cmd1.ExecuteReader
                If rd1.HasRows Then
                    If rd1.Read Then
                        MsgBox("Este número de factura ya existe en una compra previa.", vbInformation + vbOKOnly, "Delsscom Control Negocios Pro")
                        Pedido = 0
                        cboremision.Text = rd1(0).ToString
                        cbopedido.Text = rd1(2).ToString
                        dtpfecha.Value = rd1(3).ToString
                        btnGuardar.Enabled = False
                        btncancela.Enabled = True
                        btnactualiza.Enabled = True
                        txtsaldo.Enabled = False
                        Button1.Enabled = False
                        txtpAnticipo.Enabled = False
                        lblpAnticipo.Enabled = False
                        grdCaptura.DefaultCellStyle.ForeColor = Color.DarkGoldenrod
                    End If
                Else
                    btnactualiza.Enabled = False
                    btncancela.Enabled = False
                    btnGuardar.Enabled = True
                    txtsaldo.Enabled = True
                    grdCaptura.DefaultCellStyle.ForeColor = Color.Black
                    cbofactura.Focus().Equals(True)
                End If
                rd1.Close()

                If Pedido = 0 Then
                    txtsub1.Text = "0.00"
                    txtdesc1.Text = "0.00"
                    txtsub2.Text = "0.00"
                    txtiva.Text = "0.00"
                    txtTotalC.Text = "0.00"
                    txtdesc2.Text = "0.00"
                    txtieps.Text = "0.00"
                    txtapagar.Text = "0.00"
                    txtanticipo.Text = "0.00"
                    txtefectivo.Text = "0.00"
                    txtpagos.Text = "0.00"
                    txtresta.Text = "0.00"
                End If

                cnn2.Close() : cnn2.Open()

                cmd1 = cnn1.CreateCommand
                cmd1.CommandText = "SELECT * FROM comprasentalla WHERE NumFactura='" & cbofactura.Text & "' AND Proveedor='" & cboproveedor.Text & "'"
                rd1 = cmd1.ExecuteReader
                Do While rd1.Read
                    If rd1.HasRows Then
                        Dim codigo As String = rd1("Codigo").ToString
                        Dim modelo As String = rd1("modelo").ToString

                        Dim unidad As String = ""
                        Dim multiplo As Double = 0
                        Dim existencia As Double = 0
                        Dim nombre As String = ""

                        cmd2 = cnn2.CreateCommand : cmd2.CommandText =
                            "select Nombre,UCompra,Multiplo from Productos where Codigo='" & codigo & "'"
                        rd2 = cmd2.ExecuteReader
                        If rd2.HasRows Then
                            If rd2.Read Then
                                nombre = rd2("Nombre").ToString
                                unidad = rd2("UCompra").ToString
                                multiplo = rd2("Multiplo").ToString
                                'existencia = rd2("Existencia").ToString
                                'existencia = existencia / multiplo
                            End If
                        End If
                        rd2.Close()
                        Dim cantidad As Double = rd1("Cantidad").ToString
                        Dim precio As Double = rd1("Precio").ToString
                        Dim mypreciouiva As Double = precio + (precio * myiva)
                        Dim mypreciovta = rd1("PrecioVenta").ToString
                        Dim total As Double = precio * cantidad

                        grdCaptura.Rows.Add(
                            modelo,
                            unidad,
                            cantidad,
                            precio,
                            mypreciouiva,
                            FormatNumber(mypreciovta, 4),
                            FormatNumber(total, 2),
                           0,
                            0,
                            0,
                            0
                            )
                    End If
                Loop
                rd1.Close()
                cnn2.Close()

                cmd1 = cnn1.CreateCommand
                cmd1.CommandText =
                    "select Sub1,Desc1,Sub2,IVA,Total,Desc2,IEPS,Pagar,Resta,FechaC,FechaP from Compras where NumRemision='" & cboremision.Text & "' and NumFactura='" & cbofactura.Text & "' and Proveedor='" & cboproveedor.Text & "'"
                rd1 = cmd1.ExecuteReader
                If rd1.HasRows Then
                    If rd1.Read Then
                        txtsub1.Text = FormatNumber(rd1("Sub1").ToString, 2)
                        txtdesc1.Text = FormatNumber(rd1("Desc1").ToString, 2)
                        txtsub2.Text = FormatNumber(rd1("Sub2").ToString, 2)
                        txtiva.Text = FormatNumber(rd1("IVA").ToString, 2)
                        txtTotalC.Text = FormatNumber(rd1("Total")).ToString
                        txtdesc2.Text = FormatNumber(rd1("Desc2".ToString), 2)
                        txtieps.Text = FormatNumber(rd1("IEPS").ToString, 2)
                        txtapagar.Text = FormatNumber(rd1("Pagar").ToString, 2)
                        txtresta.Text = FormatNumber(rd1("Resta").ToString, 2)
                        dtpfecha.Value = rd1("FechaC").ToString
                        dtpfpago.Value = rd1("FechaP").ToString
                    End If
                End If
                rd1.Close()
                cnn1.Close()
            Catch ex As Exception
                MessageBox.Show(ex.ToString)
                cnn1.Close()
            End Try
            cbonombre.Focus().Equals(True)
        End If
    End Sub

    Private Sub txtcantidad_Click(sender As Object, e As EventArgs) Handles txtcantidad.Click
        txtcantidad.SelectionStart = 0
        txtcantidad.SelectionLength = Len(txtcantidad.Text)
    End Sub

    Private Sub txtcantidad_GotFocus(sender As Object, e As EventArgs) Handles txtcantidad.GotFocus
        txtcantidad.SelectionStart = 0
        txtcantidad.SelectionLength = Len(txtcantidad.Text)
    End Sub

    Private Sub txtcantidad_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtcantidad.KeyPress
        If Not IsNumeric(txtcantidad.Text) Then txtcantidad.Text = ""
        If AscW(e.KeyChar) = Keys.Enter Then
            txtPU.Focus().Equals(True)
        End If
    End Sub

    Private Sub txtcantidad_LostFocus(sender As Object, e As EventArgs) Handles txtcantidad.LostFocus
        If grdCaptura.DefaultCellStyle.ForeColor = Color.LightCoral Then
            txtUnidad.Text = ""
            cbonombre.Text = ""
            txtUnidad.Text = ""
            txtPU.Text = "0.00"
            txtPUIVA.Text = "0.00"
            txtpvtasiva.Text = "0.0000"
            dtpfecha.Value = Now
            txtcantidad.Text = ""
            txtTotal.Text = ""
            txtExistencia.Text = ""
            cbonombre.Focus().Equals(True)
            If MsgBox("Ya existe una compra bajo el número de remisión o factura. ¿Deseas realizar una nueva compra?", vbInformation + vbOKCancel, "Delsscom Control Negocios Pro") = vbOK Then btnLimpiar.PerformClick()
        End If
    End Sub

    Private Sub cbonombre_DropDown(sender As Object, e As EventArgs) Handles cbonombre.DropDown
        Dim cnn5 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
        Dim rd5 As MySqlDataReader
        Dim cmd5 As MySqlCommand
        Try
            cbonombre.Items.Clear()
            cnn5.Close() : cnn5.Open()
            cmd5 = cnn5.CreateCommand
            cmd5.CommandText = "SELECT DISTINCT Grupo FROM productos WHERE Grupo<>'' ORDER BY Grupo"
            rd5 = cmd5.ExecuteReader
            Do While rd5.Read
                If rd5.HasRows Then
                    cbonombre.Items.Add(rd5(0).ToString)
                End If
            Loop
            rd5.Close()
            cnn5.Close()

        Catch ex As Exception
            MessageBox.Show(ex.ToString)
            cnn5.Close()
        End Try
    End Sub

    Private Sub cbonombre_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cbonombre.KeyPress
        e.KeyChar = UCase(e.KeyChar)
        If AscW(e.KeyChar) = Keys.Enter Then

            Dim cnn1 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
            Dim rd1 As MySqlDataReader
            Dim cmd1 As MySqlCommand

            If cboremision.Visible = True And cboremision.Text = "" And cbofactura.Text = "" Then
                MsgBox("Debe asignar un número de remision o de factura.", vbInformation + vbOKOnly, titulocentral)
                cboremision.Focus.Equals(True)
                Exit Sub
            End If

            If cbonombre.Text = "" Then
                btnGuardar.Focus.Equals(True)
                Exit Sub
            End If

            cnn1.Close() : cnn1.Open()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "SELECT Codigo,UCompra FROM productos WHERE Grupo='" & cbonombre.Text & "'"
            rd1 = cmd1.ExecuteReader
            If rd1.HasRows Then
                If rd1.Read Then
                    mycodigo = rd1("Codigo").ToString
                    txtUnidad.Text = rd1("UCompra").ToString
                    txtcantidad.Focus.Equals(True)
                End If
            Else
                MsgBox("El modelo no existe", vbInformation + vbOKOnly, titulocentral)
                cbonombre.Focus.Equals(True)
                Exit Sub
            End If
            rd1.Close()
            cnn1.Close()

            If Len(cbonombre.Text) = 0 Then
                txtanticipo.Focus.Equals(True)
                Exit Sub
            End If
            If Len(cbonombre.Text) = 0 Then
                CodBarra()
            End If

        End If
    End Sub

    Public Function CodBarra() As Boolean

    End Function

    Private Sub txtPU_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPU.KeyPress
        If Not IsNumeric(txtPU.Text) Then txtPU.Text = ""
        If AscW(e.KeyChar) = Keys.Enter Then

            Dim cnn1 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
            Dim rd1 As MySqlDataReader
            Dim cmd1 As MySqlCommand

            cnn1.Close() : cnn1.Open()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "SELECT Iva FROM IVA where Id=1"
            rd1 = cmd1.ExecuteReader
            If rd1.Read Then
                If rd1.HasRows Then
                    txtPUIVA.Text = txtPU.Text + (txtPU.Text * rd1(0).ToString)
                End If
            End If
            rd1.Close()
            cnn1.Close()
            txtTotal.Text = CDec(txtcantidad.Text) * CDec(txtPU.Text)
            txtTotal.Text = FormatNumber(txtTotal.Text, 2)
            txtpvtasiva.Focus.Equals(True)
        End If
    End Sub

    Private Sub txtPU_LostFocus(sender As Object, e As EventArgs) Handles txtPU.LostFocus
        If Not IsNumeric(txtPU.Text) Then txtPU.Text = ""
        Dim cnn1 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
        Dim rd1 As MySqlDataReader
        Dim cmd1 As MySqlCommand

        If CDec(IIf(txtcantidad.Text = "", 0, txtcantidad.Text)) = 0 Or txtcantidad.Text = "" Then
            txtcantidad.Focus.Equals(True)
            Exit Sub
        Else
            cnn1.Close() : cnn1.Open()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "SELECT Iva FROM IVA where Id=1"
            rd1 = cmd1.ExecuteReader
            If rd1.Read Then
                If rd1.HasRows Then
                    txtPUIVA.Text = txtPU.Text + (txtPU.Text * rd1(0).ToString)
                End If
            End If
            rd1.Close()
            cnn1.Close()
            txtpvtasiva.Focus.Equals(True)
        End If

    End Sub

    Private Sub cbopedido_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cbopedido.KeyPress
        If AscW(e.KeyChar) = Keys.Enter Then
            dtpfecha.Focus.Equals(True)
        End If
    End Sub

    Private Sub txtpvtasiva_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtpvtasiva.KeyPress

        If AscW(e.KeyChar) = Keys.Enter Then
            If Len(cbonombre.Text) = 0 Then
                cbonombre.Focus.Equals(True)
                Exit Sub
            End If

            Dim cnn1 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
            Dim rd1 As MySqlDataReader
            Dim cmd1 As MySqlCommand

            txtPU.Text = FormatNumber(txtPU.Text, 2)
            txtTotal.Text = FormatNumber(CDec(IIf(txtcantidad.Text = 0, 0, txtcantidad.Text)) * CDec(IIf(txtPU.Text = 0, 0, txtPU.Text)), 2)

            txtsub1.Text = CDec(txtsub1.Text) + CDec(txtTotal.Text)
            txtsub1.Text = FormatNumber(txtsub1.Text, 2)
            IVA2()
            UpGrid()

            cbonombre.Text = ""
            txtUnidad.Text = ""
            txtcantidad.Text = "0.00"
            txtPU.Text = "0.00"
            txtPUIVA.Text = "0.00"
            txtpvtasiva.Text = "0.00"
            txtTotal.Text = "0.00"
            txtExistencia.Text = ""

            If cboremision.Text = "" Then
                Exit Sub
            End If

            myivacal = 0
            txtiva.Text = "0.00"
            For n As Integer = 0 To grdCaptura.Rows.Count - 1
                cnn1.Close() : cnn1.Open()
                cmd1 = cnn1.CreateCommand
                cmd1.CommandText = "SELECT IVA FROM productos WHERE Grupo='" & grdCaptura.Rows(n).Cells(0).Value.ToString & "'"
                rd1 = cmd1.ExecuteReader
                If rd1.HasRows Then
                    If rd1.Read Then
                        myivacal = myivacal + CDec(rd1(0).ToString)
                    End If
                End If
                rd1.Close()

            Next
            cnn1.Close()

            If grdCaptura.Rows.Count > 0 Then
                myivacal = myivacal / grdCaptura.Rows.Count
            End If
            cnn1.Close()

            txtiva.Text = myivacal * txtsub2.Text
            txtiva.Text = FormatNumber(txtiva.Text, 2)
            txtTotalC.Text = CDec(txtiva.Text) + CDec(IIf(txtsub2.Text = 0, 0, txtsub2.Text))
            txtTotalC.Text = FormatNumber(txtTotalC.Text, 2)
            If CDec(txtiva.Text) > 0 Then

            End If
            cbonombre.Focus.Equals(True)
        End If
    End Sub

    Private Sub dtpfecha_KeyPress(sender As Object, e As KeyPressEventArgs) Handles dtpfecha.KeyPress
        If AscW(e.KeyChar) = Keys.Enter Then
            cbonombre.Focus().Equals(True)
        End If
    End Sub

    Public Sub IVA2()
        Dim Zi As Integer = 0
        Dim codigo As String = ""
        Dim ImpIVA As Double = 0
        Dim ImIVA As Double = 0

        Dim cnn1 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
        Dim rd1 As MySqlDataReader
        Dim cmd1 As MySqlCommand

        If cbofactura.Text <> "" Then
            If grdCaptura.Rows.Count > 0 Then
                cnn1.Close() : cnn1.Open()
                Do While grdCaptura.Rows.Count <> Zi
                    codigo = grdCaptura.Rows(Zi).Cells(0).Value.ToString

                    cmd1 = cnn1.CreateCommand
                    cmd1.CommandText =
                        "select IVA from Productos where Codigo='" & codigo & "'"
                    rd1 = cmd1.ExecuteReader
                    If rd1.HasRows Then
                        If rd1.Read Then
                            ImpIVA = CDbl(rd1("IVA").ToString) * CDbl(grdCaptura.Rows(Zi).Cells(5).Value.ToString)
                            ImIVA = ImIVA + ImpIVA
                        End If
                    End If
                    rd1.Close()
                    Zi += 1
                Loop
                cnn1.Close()
            End If
        End If
        txtiva.Text = FormatNumber(ImIVA, 2)
    End Sub

    Public Sub UpGrid()

        Dim Conteo As Double = 0
        Dim ProductoIEPS As Double = 0

        For i As Integer = 0 To grdCaptura.Rows.Count - 1
            Conteo = Conteo + CDbl(grdCaptura.Rows(i).Cells(3).Value.ToString)
        Next

        txtprods.Text = Conteo

        grdCaptura.Rows.Add(cbonombre.Text,
                            txtUnidad.Text,
                            txtcantidad.Text,
                            txtPU.Text,
                            txtPUIVA.Text,
                            txtpvtasiva.Text,
                            txtTotal.Text,
                            txtExistencia.Text)
    End Sub

    Private Sub btnLimpiar_Click(sender As Object, e As EventArgs) Handles btnLimpiar.Click

        Dim cnn1 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
        Dim rd1 As MySqlDataReader
        Dim cmd1 As MySqlCommand

        grdCaptura.DefaultCellStyle.ForeColor = Color.Black
        dtpfpago.Value = Date.Now
        dtpfecha.Value = Date.Now

        printLine = 0
        Contador = 0
        pagina = 0
        txtusuario.Text = ""

        cbonombre.Items.Clear()
        cbonombre.Text = ""
        txtUnidad.Text = ""
        txtcantidad.Text = ""
        txtPU.Text = "0.00"
        txtPUIVA.Text = "0.00"
        txtpvtasiva.Text = "0.00"
        txtTotal.Text = "0.00"
        txtExistencia.Text = ""

        cboremision.Items.Clear()
        cboremision.Text = ""
        cbofactura.Items.Clear()
        cbofactura.Text = ""
        cbopedido.Items.Clear()
        cbopedido.Text = ""

        cboproveedor.Items.Clear()
        cboproveedor.Text = ""
        txtsaldo.Text = "0.00"
        txtpAnticipo.Text = "0.00"
        cbomoneda.Text = "PESO"
        cbomoneda.Tag = "0"
        txtvalor.Text = "1.00"
        lblmoneda.Text = ""
        lblvalor.Text = ""
        grdCaptura.Rows.Clear()

        txtsub1.Text = "0.00"
        txtdesc1.Text = "0.00"
        txtsub2.Text = "0.00"
        txtiva.Text = "0.00"
        txtTotalC.Text = "0.00"
        txtdesc2.Text = "0.00"
        txtieps.Text = "0.00"
        txtapagar.Text = "0.00"
        txtanticipo.Text = "0.00"
        txtpagos.Text = "0.00"
        txtefectivo.Text = "0.00"
        txtresta.Text = "0.00"
        txtprods.Text = "0"


        btncancela.Enabled = False
        btnactualiza.Enabled = False
        btnGuardar.Enabled = True
        btnCopia.Enabled = True
        Button1.Enabled = False

        txtpAnticipo.Enabled = False
        lblpAnticipo.Enabled = False

        panpago_compra.Visible = False
        txtpc_apagar.Text = "0.00"
        txtpc_anticipo.Text = "0.00"
        txtpc_efectivo.Text = "0.00"
        txtpc_tarjeta.Text = "0.00"
        txtpc_transfe.Text = "0.00"
        txtpc_otro.Text = "0.00"
        txtpc_resta.Text = "0.00"

        Pagos.pc_porpagar = 0
        Pagos.pc_anticipo = 0
        Pagos.pc_efectivo = 0
        Pagos.pc_tarjeta = 0
        Pagos.pc_transfe = 0
        Pagos.pc_otros = 0
        Pagos.pc_resta = 0

        Panel1.Visible = False
        tipo_cancelacion = ""

        pasa_pago = False

        cnn1.Close() : cnn1.Open()
        cmd1 = cnn1.CreateCommand
        cmd1.CommandText =
            "delete from AuxCompras"
        cmd1.ExecuteNonQuery() : cnn1.Close()

        cboproveedor.Focus().Equals(True)
    End Sub

    Private Sub txtpvtasiva_Click(sender As Object, e As EventArgs) Handles txtpvtasiva.Click
        txtpvtasiva.SelectionStart = 0
        txtpvtasiva.SelectionLength = Len(txtpvtasiva.Text)
    End Sub

    Private Sub txtpvtasiva_GotFocus(sender As Object, e As EventArgs) Handles txtpvtasiva.GotFocus
        txtpvtasiva.SelectionStart = 0
        txtpvtasiva.SelectionLength = Len(txtpvtasiva.Text)
    End Sub

    Private Sub txtsub1_TextChanged(sender As Object, e As EventArgs) Handles txtsub1.TextChanged
        txtsub2.Text = FormatNumber(txtsub1.Text)
    End Sub

    Private Sub txtdesc1_Click(sender As Object, e As EventArgs) Handles txtdesc1.Click
        txtdesc1.SelectionStart = 0
        txtdesc1.SelectionLength = Len(txtdesc1.Text)
    End Sub

    Private Sub txtdesc1_GotFocus(sender As Object, e As EventArgs) Handles txtdesc1.GotFocus
        txtdesc1.SelectionStart = 0
        txtdesc1.SelectionLength = Len(txtdesc1.Text)
    End Sub

    Private Sub txtdesc1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtdesc1.KeyPress
        If Not IsNumeric(txtdesc1.Text) Then txtdesc1.Text = ""
        If AscW(e.KeyChar) = Keys.Enter Then
            txtdesc2.Focus().Equals(True)
        End If
    End Sub

    Private Sub txtdesc1_LostFocus(sender As Object, e As EventArgs) Handles txtdesc1.LostFocus
        txtdesc1.Text = FormatNumber(txtdesc1.Text, 2)
    End Sub

    Private Sub txtdesc1_TextChanged(sender As Object, e As EventArgs) Handles txtdesc1.TextChanged
        If Strings.Left(txtdesc1.Text, 1) = "," Then Exit Sub
        If txtdesc1.Text = "" Then txtdesc1.Text = "0.00"
        If txtsub1.Text = "" Then txtsub1.Text = "0.00"
        If CDbl(txtdesc1.Text) > CDec(txtsub1.Text) Then
            MsgBox("Descuento no permitido.", vbInformation + vbOKOnly, "Delsscom Control Negocios Pro")
            txtdesc1.Text = Strings.Left(txtdesc1.Text, Len(txtdesc1.Text) - 1)
            txtdesc1.SelectionStart = 0
            txtdesc1.SelectionLength = Len(txtdesc1.Text)
        End If

        txtsub2.Text = CDbl(IIf(txtsub1.Text = "", 0, txtsub1.Text)) - CDbl(IIf(txtdesc1.Text = "", 0, txtdesc1.Text))
        txtsub2.Text = FormatNumber(txtsub2.Text, 2)

        txtiva.Text = FormatNumber(txtiva.Text, 2)
        txtTotalC.Text = FormatNumber(CDbl(txtsub2.Text) + CDbl(txtiva.Text), 2)
    End Sub

    Private Sub txtsub2_Click(sender As Object, e As EventArgs) Handles txtsub2.Click
        txtdesc2.SelectionStart = 0
        txtdesc2.SelectionLength = Len(txtdesc2.Text)
    End Sub

    Private Sub txtsub2_GotFocus(sender As Object, e As EventArgs) Handles txtsub2.GotFocus
        txtdesc2.SelectionStart = 0
        txtdesc2.SelectionLength = Len(txtdesc2.Text)
    End Sub

    Private Sub txtsub2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtsub2.KeyPress
        If AscW(e.KeyChar) = Keys.Enter Then
            txtefectivo.Focus().Equals(True)
        End If
    End Sub

    Private Sub txtsub2_LostFocus(sender As Object, e As EventArgs) Handles txtsub2.LostFocus
        txtdesc2.Text = FormatNumber(txtdesc2.Text, 2)
    End Sub

    Private Sub txtusuario_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtusuario.KeyPress
        Dim id_usu As Integer = 0
        If AscW(e.KeyChar) = Keys.Enter Then

            Dim cnn1 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
            Dim rd1 As MySqlDataReader
            Dim cmd1 As MySqlCommand

            Try
                cnn1.Close() : cnn1.Open()

                cmd1 = cnn1.CreateCommand
                cmd1.CommandText =
                    "select IdEmpleado,Alias from Usuarios where Clave='" & txtusuario.Text & "'"
                rd1 = cmd1.ExecuteReader
                If rd1.HasRows Then
                    If rd1.Read Then
                        id_usu = rd1("IdEmpleado").ToString()
                        alias_compras = rd1("Alias").ToString()
                        lblusuario.Text = rd1("Alias").ToString()
                    End If
                Else
                    txtusuario.Text = ""
                    txtusuario.Focus.Equals(True)
                End If
                rd1.Close()

                cmd1 = cnn1.CreateCommand
                cmd1.CommandText =
                    "select Comp_Com from Permisos where IdEmpleado=" & id_usu
                rd1 = cmd1.ExecuteReader
                If rd1.HasRows Then
                    If rd1.Read Then
                        If rd1("Comp_Com").ToString() = True Then
                            If btnGuardar.Enabled = False Then
                                btncancela.Focus().Equals(True)
                            Else
                                btnGuardar.Focus().Equals(True)
                            End If
                            rd1.Close() : cnn1.Close() : Exit Sub
                        Else
                            MsgBox("No cuentas con permisos suficientes para interactuar con este formulario.", vbInformation + vbOKOnly, "Delsscom Control Negocios Pro")
                            rd1.Close() : cnn1.Close() : Exit Sub
                        End If
                    End If
                End If
                rd1.Close() : cnn1.Close()
            Catch ex As Exception
                MessageBox.Show(ex.ToString)
                cnn1.Close()
            End Try
        End If
    End Sub

    Private Sub txtapagar_TextChanged(sender As Object, e As EventArgs) Handles txtapagar.TextChanged

        Dim cnn3 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
        Dim rd3 As MySqlDataReader
        Dim cmd3 As MySqlCommand

        cnn3.Close() : cnn3.Open()
        cmd3 = cnn3.CreateCommand
        cmd3.CommandText =
            "select Resta from Compras where NumRemision='" & cboremision.Text & "' and Proveedor='" & cboproveedor.Text & "'"
        rd3 = cmd3.ExecuteReader
        If rd3.HasRows Then
            If rd3.Read Then
                txtresta.Text = rd3(0).ToString
                txtresta.Text = FormatNumber(txtresta.Text, 2)
            End If
        Else
            txtresta.Text = CDbl(IIf(txtapagar.Text = "", 0, txtapagar.Text)) - (CDbl(IIf(txtanticipo.Text = "", 0, txtanticipo.Text)) + CDbl(IIf(txtefectivo.Text = "", 0, txtefectivo.Text)) + CDbl(IIf(txtpagos.Text = "", 0, txtpagos.Text)))
            txtresta.Text = FormatNumber(txtresta.Text, 2)
        End If
        rd3.Close()
        cnn3.Close()
    End Sub

    Private Sub txtiva_TextChanged(sender As Object, e As EventArgs) Handles txtiva.TextChanged
        If txtiva.Text = "" Then txtiva.Text = "0.00"
        If txtsub2.Text = "" Then txtsub2.Text = "0.00"
        txtTotalC.Text = FormatNumber(CDbl(txtsub2.Text) + CDbl(txtiva.Text), 2)
    End Sub

    Private Sub txtTotalC_TextChanged(sender As Object, e As EventArgs) Handles txtTotalC.TextChanged
        txtapagar.Text = CDbl(txtTotalC.Text) - CDbl(txtdesc2.Text)
        txtapagar.Text = FormatNumber(txtapagar.Text, 2)
    End Sub

    Private Sub txtanticipo_TextChanged(sender As Object, e As EventArgs) Handles txtanticipo.TextChanged
        txtresta.Text = CDbl(IIf(txtapagar.Text = "", 0, txtapagar.Text)) - (CDbl(IIf(txtanticipo.Text = "", 0, txtanticipo.Text)) + CDbl(IIf(txtefectivo.Text = "", 0, txtefectivo.Text)) + CDbl(IIf(txtpagos.Text = "", 0, txtpagos.Text)))
        txtresta.Text = FormatNumber(txtresta.Text, 2)
    End Sub

    Private Sub txtpagos_TextChanged(sender As Object, e As EventArgs) Handles txtpagos.TextChanged
        txtresta.Text = CDbl(IIf(txtapagar.Text = "", 0, txtapagar.Text)) - (CDbl(IIf(txtanticipo.Text = "", 0, txtanticipo.Text)) + CDbl(IIf(txtefectivo.Text = "", 0, txtefectivo.Text)) + CDbl(IIf(txtpagos.Text = "", 0, txtpagos.Text)))
        txtresta.Text = FormatNumber(txtresta.Text, 2)
    End Sub

    Private Sub txtefectivo_Click(sender As Object, e As EventArgs) Handles txtefectivo.Click
        txtefectivo.SelectionStart = 0
        txtefectivo.SelectionLength = Len(txtefectivo.Text)
    End Sub

    Private Sub txtefectivo_GotFocus(sender As Object, e As EventArgs) Handles txtefectivo.GotFocus
        txtefectivo.SelectionStart = 0
        txtefectivo.SelectionLength = Len(txtefectivo.Text)
    End Sub

    Private Sub txtefectivo_TextChanged(sender As Object, e As EventArgs) Handles txtefectivo.TextChanged
        If Strings.Left(txtefectivo.Text, 1) = "," Then Exit Sub
        txtresta.Text = CDbl(IIf(txtapagar.Text = "", 0, txtapagar.Text)) - (CDbl(IIf(txtanticipo.Text = "", 0, txtanticipo.Text)) + CDbl(IIf(txtefectivo.Text = "", 0, txtefectivo.Text)) + CDbl(IIf(txtpagos.Text = "", 0, txtpagos.Text)))
        txtresta.Text = FormatNumber(txtresta.Text, 2)
    End Sub

    Private Sub txtefectivo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtefectivo.KeyPress
        If Not IsNumeric(txtefectivo.Text) Then txtefectivo.Text = ""
        If AscW(e.KeyChar) = Keys.Enter Then
            btnGuardar.Focus().Equals(True)
        End If
    End Sub

    Private Sub txtefectivo_LostFocus(sender As Object, e As EventArgs) Handles txtefectivo.LostFocus
        txtefectivo.Text = FormatNumber(txtefectivo.Text, 2)
    End Sub

    Private Sub grdCaptura_KeyDown(sender As Object, e As KeyEventArgs) Handles grdCaptura.KeyDown

    End Sub

    Private Sub grdCaptura_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles grdCaptura.CellDoubleClick

    End Sub

    Private Sub txtdesc2_GotFocus(sender As Object, e As EventArgs) Handles txtdesc2.GotFocus
        txtdesc2.SelectionStart = 0
        txtdesc2.SelectionLength = Len(txtdesc2.Text)
    End Sub

    Private Sub txtdesc2_TextChanged(sender As Object, e As EventArgs) Handles txtdesc2.TextChanged
        If Strings.Left(txtdesc2.Text, 1) = "," Then Exit Sub
        If txtdesc2.Text = "" Then Exit Sub
        txtapagar.Text = CDbl(IIf(txtTotalC.Text = "", 0, txtTotalC.Text)) - CDbl(IIf(txtdesc2.Text = "", 0, txtdesc2.Text))
        txtapagar.Text = FormatNumber(txtapagar.Text, 2)
    End Sub

    Private Sub txtdesc2_Click(sender As Object, e As EventArgs) Handles txtdesc2.Click
        txtdesc2.SelectionStart = 0
        txtdesc2.SelectionLength = Len(txtdesc2.Text)
    End Sub

    Private Sub txtdesc2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtdesc2.KeyPress
        If Not IsNumeric(txtdesc2.Text) Then txtdesc2.Text = ""
        If AscW(e.KeyChar) = Keys.Enter Then
            txtefectivo.Focus().Equals(True)
        End If
    End Sub

    Private Sub txtdesc2_LostFocus(sender As Object, e As EventArgs) Handles txtdesc2.LostFocus
        txtdesc2.Text = FormatNumber(txtdesc2.Text, 2)
    End Sub

    Private Sub txtanticipo_Click(sender As Object, e As EventArgs) Handles txtanticipo.Click
        txtpAnticipo.SelectionStart = 0
        txtpAnticipo.SelectionLength = Len(txtpAnticipo.Text)
    End Sub

    Private Sub txtanticipo_GotFocus(sender As Object, e As EventArgs) Handles txtanticipo.GotFocus
        txtpAnticipo.SelectionStart = 0
        txtpAnticipo.SelectionLength = Len(txtpAnticipo.Text)
    End Sub

    Private Sub txtanticipo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtanticipo.KeyPress
        If Not IsNumeric(txtpAnticipo.Text) Then txtpAnticipo.Text = ""
        If AscW(e.KeyChar) = Keys.Enter Then
            Button1.Focus().Equals(True)
        End If
    End Sub

    Private Sub txtanticipo_LostFocus(sender As Object, e As EventArgs) Handles txtanticipo.LostFocus
        txtpAnticipo.Text = FormatNumber(txtpAnticipo.Text, 2)
    End Sub

    Private Sub frmComprasModelos_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        cboproveedor.Focus().Equals(True)
    End Sub

    Private Sub btncancela_Click(sender As Object, e As EventArgs) Handles btncancela.Click
        Dim cnn1 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
        Dim rd1 As MySqlDataReader
        Dim cmd1 As MySqlCommand

        Try
            If cboproveedor.Text = "" Then MsgBox("Necesitas seleccionar un proveedor para continuar con el proceso.", vbInformation + vbOKOnly, titulocentral) : cboproveedor.Focus().Equals(True) : Exit Sub
            If cboremision.Text = "" Then MsgBox("Selecciona un número de remisión para continuar con el proceso.", vbInformation + vbOKOnly, titulocentral) : cboremision.Focus().Equals(True) : Exit Sub
            If cboremision.Text = "" And cbofactura.Text = "" Then MsgBox("Necesitas seleccionar al menos el número de remisión para continuar con el proceso.", vbInformation + vbOKOnly, titulocentral) : cboremision.Focus().Equals(True) : Exit Sub
            If grdCaptura.Rows.Count = 0 Then MsgBox("Proceso erróneo, inténtalo de nuevo más tarde." & vbNewLine & "Sí el problema persiste comunícate con tu proveedor de software.", vbInformation + vbOKOnly, titulocentral) : btnLimpiar.PerformClick() : Exit Sub

            cnn1.Close() : cnn1.Open()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText =
                "select Status from Compras where NumRemision='" & cboremision.Text & "' and Proveedor='" & cboproveedor.Text & "'"
            rd1 = cmd1.ExecuteReader
            If rd1.HasRows Then
                If rd1.Read Then
                    If rd1("Status").ToString() = "CANCELADA" Then
                        MsgBox("Esta compra ya fue cancelada anteriormente.", vbInformation + vbOKOnly, "Delsscom Control Negocios Pro")
                        rd1.Close() : cnn1.Close() : Exit Sub
                    End If
                End If
            End If
            rd1.Close() : cnn1.Close()

            Dim id_compra As Double = 0
            Dim acuenta As Double = 0
            Dim resta As Double = 0
            Dim status As String = ""
            Dim saldo As Double = 0

            Dim id_prov As Double = 0

            Try
                cnn1.Close() : cnn1.Open()

                cmd1 = cnn1.CreateCommand
                cmd1.CommandText =
                    "select Id,ACuenta,Resta,Status from Compras where NumRemision='" & cboremision.Text & "' and Proveedor='" & cboproveedor.Text & "'"
                rd1 = cmd1.ExecuteReader
                If rd1.HasRows Then
                    If rd1.Read Then
                        id_compra = rd1("Id").ToString()
                        acuenta = rd1("ACuenta").ToString()
                        resta = rd1("Resta").ToString()
                        status = rd1("Status").ToString()
                    End If
                End If
                rd1.Close()

                If status = "CANCELADA" Then MsgBox("Esta compra ya fue cancelada anteriormente.", vbInformation + vbOKOnly, "Delsscom Control Negocios Pro") : cnn1.Close() : Exit Sub

                Dim peps As Boolean = True

                For rm As Integer = 0 To grdCaptura.Rows.Count - 1
                    Dim codigo As String = grdCaptura.Rows(rm).Cells(0).Value.ToString()

                    cmd1 = cnn1.CreateCommand
                    cmd1.CommandText =
                        "select Entrada,Saldo from Costeo where Referencia='" & cboremision.Text & "' and Concepto='COMPRA' and Codigo='" & codigo & "'"
                    rd1 = cmd1.ExecuteReader
                    If rd1.HasRows Then
                        If rd1.Read Then
                            Dim entrada As Double = rd1("Entrada").ToString()
                            Dim saldo_c As Double = rd1("Saldo").ToString()

                            If saldo_c < entrada Then
                                peps = False
                            End If
                        End If
                    End If
                    rd1.Close()
                Next

                If peps = False Then MsgBox("No puedes cancelar esta compra ya que uno o varios productos de la misma ya tuvieron ventas.", vbInformation + vbOKOnly, "Delsscom Control Negocios Pro") : cnn1.Close() : Exit Sub

                cnn1.Close()
            Catch ex As Exception
                MessageBox.Show(ex.ToString)
                cnn1.Close()
            End Try

            If MsgBox("¿Deseas cancelar esta compra?", vbInformation + vbOKCancel, "Delsscom Control Negocios Pro") = vbOK Then
                If acuenta > 0 Then
                    Panel1.Visible = True
                    Exit Sub
                ElseIf acuenta <= 0 Then
                    Dim fecha_cancela As String = Format(Date.Now, "yyyy-MM-dd")

                    MsgBox("Al no abonar nada a la compra, no habrá movimientos de caja ni de saldos.", vbInformation + vbOKOnly, "Delsscom Control Negocios Pro")
                    tipo_cancelacion = "CASO 1"

                    cnn1.Close() : cnn1.Open()

                    cmd1 = cnn1.CreateCommand
                    cmd1.CommandText =
                        "select IdProv,Id,ACuenta,Resta,Status from Compras where NumRemision='" & cboremision.Text & "' and Proveedor='" & cboproveedor.Text & "'"
                    rd1 = cmd1.ExecuteReader
                    If rd1.HasRows Then
                        If rd1.Read Then
                            id_prov = rd1("IdProv").ToString()
                            id_compra = rd1("Id").ToString()
                            acuenta = rd1("ACuenta").ToString()
                            resta = rd1("Resta").ToString()
                            status = rd1("Status").ToString()
                        End If
                    End If
                    rd1.Close()

                    cmd1 = cnn1.CreateCommand
                    cmd1.CommandText =
                        "update Compras set Status='CANCELADA', FechaCancela='" & fecha_cancela & "' where Id=" & id_compra
                    cmd1.ExecuteNonQuery()

                    cmd1 = cnn1.CreateCommand
                    cmd1.CommandText =
                        "update comprasentalla set Status='CANCELADA', FechaCancela='" & fecha_cancela & "' where Id_Compra=" & id_compra
                    cmd1.ExecuteNonQuery()

                    cmd1 = cnn1.CreateCommand
                    cmd1.CommandText =
                        "select Saldo from AbonoE where Id=(select max(Id) from AbonoE where IdProv=" & id_prov & " and NumRemision='" & cboremision.Text & "')"
                    rd1 = cmd1.ExecuteReader
                    If rd1.HasRows Then
                        If rd1.Read Then
                            saldo = rd1(0).ToString()
                        End If
                    End If
                    rd1.Close()

                    saldo = saldo - resta

                    cmd1 = cnn1.CreateCommand
                    cmd1.CommandText =
                        "insert into AbonoE(NumRemision,NumFactura,NumPedido,IdProv,Proveedor,Concepto,Fecha,Hora,FechaCompleta,Cargo,Abono,Saldo,Banco,Referencia,Usuario) values('" & cboremision.Text & "','" & cbofactura.Text & "','" & cbopedido.Text & "'," & id_prov & ",'" & cboproveedor.Text & "','CANCELACION','" & Format(dtpfecha.Value, "yyyy-MM-dd") & "','" & Format(Date.Now, "HH:mm:ss") & "','" & Format(Date.Now, "yyyy-MM-dd HH:mm:ss") & "',0," & resta & "," & saldo & ",'','','" & alias_compras & "')"
                    cmd1.ExecuteNonQuery()

                    For t As Integer = 0 To grdCaptura.Rows.Count - 1
                        Dim codigo As String = grdCaptura.Rows(t).Cells(0).Value.ToString()
                        Dim nombre As String = grdCaptura.Rows(t).Cells(1).Value.ToString()
                        Dim unidad As String = grdCaptura.Rows(t).Cells(2).Value.ToString()
                        Dim cantidad As Double = grdCaptura.Rows(t).Cells(3).Value.ToString()
                        Dim precio As Double = grdCaptura.Rows(t).Cells(4).Value.ToString()
                        Dim existencia As Double = 0

                        cmd1 = cnn1.CreateCommand
                        cmd1.CommandText =
                            "select  Existencia from Productos where Codigo='" & Strings.Left(codigo, 6) & "'"
                        rd1 = cmd1.ExecuteReader
                        If rd1.HasRows Then
                            If rd1.Read Then
                                existencia = rd1("Existencia").ToString()
                            End If
                        End If
                        rd1.Close()

                        'Actualiza el cardex
                        cmd1 = cnn1.CreateCommand
                        cmd1.CommandText =
                            "insert into Cardex(Codigo,Nombre,Movimiento,Inicial,Cantidad,Final,Precio,Fecha,Usuario,Folio,Tipo,Cedula,Receta,Medico,Domicilio) values('" & codigo & "','" & nombre & "','Cancela compra'," & existencia & "," & cantidad & "," & (existencia - cantidad) & "," & precio & ",'" & Format(Date.Now, "yyyy-MM-dd") & "','" & alias_compras & "','" & cboremision.Text & "','','','','','')"
                        cmd1.ExecuteNonQuery()

                        'Actualiza la tabla del PEPS
                        cmd1 = cnn1.CreateCommand
                        cmd1.CommandText =
                            "update Costeo set Saldo=0 where Referencia='" & cboremision.Text & "' and Concepto='COMPRA' and Codigo='" & Strings.Left(codigo, 6) & "'"
                        cmd1.ExecuteNonQuery()

                        'Actualiza la tabla de productos
                        cmd1 = cnn1.CreateCommand
                        cmd1.CommandText =
                            "update Productos set Existencia=" & (existencia - cantidad) & " where Codigo='" & Strings.Left(codigo, 6) & "'"
                        cmd1.ExecuteNonQuery()
                    Next
                    cnn1.Close()

                    If MsgBox("¿Deseas imprimir un recibo de la cancelación?", vbInformation + vbYesNo, "Delsscom Control Negocios Pro") = vbYes Then
                        Dim ImprimeEn As String = ""
                        Dim Impresora As String = ""
                        Dim tPapel As String = ""
                        Dim tMilimetros As String = ""
                        printLine = 0
                        Contador = 0
                        pagina = 0

                        cnn1.Close() : cnn1.Open()

                        cmd1 = cnn1.CreateCommand
                        cmd1.CommandText =
                            "select NotasCred from Formatos where Facturas='TPapelCP'"
                        rd1 = cmd1.ExecuteReader
                        If rd1.HasRows Then
                            If rd1.Read Then
                                tPapel = rd1(0).ToString
                                If tPapel = "CARTA" Or tPapel = "MEDIA CARTA" Then
                                    ImprimeEn = "ImpreC"
                                ElseIf tPapel = "TICKET" Then
                                    ImprimeEn = "ImpreT"
                                Else
                                    ImprimeEn = ""
                                End If
                            End If
                        Else
                            MsgBox("No se ha establecido un tamaño de papel para el formato de impresión de compras.", vbInformation + vbOKOnly, "Delsscom Control Negocios Pro")
                            rd1.Close()
                            cnn1.Close()
                            btnLimpiar.PerformClick()
                            Exit Sub
                        End If
                        rd1.Close()

                        cmd1 = cnn1.CreateCommand
                        cmd1.CommandText =
                            "select NotasCred from Formatos where Facturas='TamImpre'"
                        rd1 = cmd1.ExecuteReader
                        If rd1.HasRows Then
                            If rd1.Read Then
                                tMilimetros = rd1(0).ToString
                            End If
                        End If
                        rd1.Close()

                        If ImprimeEn <> "" Then
                            cmd1 = cnn1.CreateCommand
                            cmd1.CommandText =
                                "select NotasCred from Formatos where Facturas='" & ImprimeEn & "'"
                            rd1 = cmd1.ExecuteReader
                            If rd1.HasRows Then
                                If rd1.Read Then
                                    Impresora = rd1(0).ToString
                                End If
                            End If
                            rd1.Close()
                        Else
                            MsgBox("No tienes una impresora configurada para imprimir en formato " & tPapel & ".", vbInformation + vbOKOnly, "Delsscom Control Negocios Pro")
                            cnn1.Close()
                            btnLimpiar.PerformClick()
                            Exit Sub
                        End If
                        cnn1.Close()

                        If tPapel = "TICKET" Then
                            If tMilimetros = "80" Then
                                pCancela80.DefaultPageSettings.PrinterSettings.PrinterName = Impresora
                                pCancela80.Print()
                            End If
                            If tMilimetros = "58" Then
                                pCancela58.DefaultPageSettings.PrinterSettings.PrinterName = Impresora
                                pCancela58.Print()
                            End If
                        End If
                        If tPapel = "MEDIA CARTA" Then
                            pCancelaMC.DefaultPageSettings.PrinterSettings.PrinterName = Impresora
                            pCancelaMC.Print()
                        End If
                        If tPapel = "CARTA" Then
                            pCancelaCarta.DefaultPageSettings.PrinterSettings.PrinterName = Impresora
                            pCancelaCarta.Print()
                        End If
                        If tPapel = "PDF - CARTA" Then
                            'Genera PDF y lo guarda en la ruta

                        End If
                    End If
                    MsgBox("Compra cancelada correctamente.", vbInformation + vbOKOnly, titulocentral)
                    btnLimpiar.PerformClick()
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString())
            cnn1.Close()
        End Try
    End Sub

    Private Sub btnactualiza_Click(sender As Object, e As EventArgs) Handles btnactualiza.Click

    End Sub

    Private Sub txtpc_efectivo_Click(sender As Object, e As EventArgs) Handles txtpc_efectivo.Click
        txtpc_efectivo.SelectionStart = 0
        txtpc_efectivo.SelectionLength = Len(txtpc_efectivo.Text)
    End Sub

    Private Sub txtpc_efectivo_GotFocus(sender As Object, e As EventArgs) Handles txtpc_efectivo.GotFocus
        txtpc_efectivo.SelectionStart = 0
        txtpc_efectivo.SelectionLength = Len(txtpc_efectivo.Text)
    End Sub

    Private Sub txtpc_efectivo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtpc_efectivo.KeyPress
        If AscW(e.KeyChar) = Keys.Enter Then
            If txtpc_efectivo.Text = "" Then txtpc_efectivo.Text = "0"
            txtpc_efectivo.Text = FormatNumber(txtpc_efectivo.Text, 2)
            txtpc_tarjeta.Focus().Equals(True)
        End If
    End Sub

    Private Sub txtpc_tarjeta_Click(sender As Object, e As EventArgs) Handles txtpc_tarjeta.Click
        txtpc_tarjeta.SelectionStart = 0
        txtpc_tarjeta.SelectionLength = Len(txtpc_tarjeta.Text)
    End Sub

    Private Sub txtpc_tarjeta_GotFocus(sender As Object, e As EventArgs) Handles txtpc_tarjeta.GotFocus
        txtpc_tarjeta.SelectionStart = 0
        txtpc_tarjeta.SelectionLength = Len(txtpc_tarjeta.Text)
    End Sub

    Private Sub txtpc_tarjeta_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtpc_tarjeta.KeyPress
        If AscW(e.KeyChar) = Keys.Enter Then
            If txtpc_tarjeta.Text = "" Then txtpc_tarjeta.Text = "0"
            txtpc_tarjeta.Text = FormatNumber(txtpc_tarjeta.Text, 2)
            txtpc_transfe.Focus().Equals(True)
        End If
    End Sub

    Private Sub txtpc_transfe_Click(sender As Object, e As EventArgs) Handles txtpc_transfe.Click
        txtpc_transfe.SelectionStart = 0
        txtpc_transfe.SelectionLength = Len(txtpc_transfe.Text)
    End Sub

    Private Sub txtpc_transfe_GotFocus(sender As Object, e As EventArgs) Handles txtpc_transfe.GotFocus
        txtpc_transfe.SelectionStart = 0
        txtpc_transfe.SelectionLength = Len(txtpc_transfe.Text)
    End Sub

    Private Sub txtpc_transfe_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtpc_transfe.KeyPress
        If AscW(e.KeyChar) = Keys.Enter Then
            If txtpc_transfe.Text = "" Then txtpc_transfe.Text = "0"
            txtpc_transfe.Text = FormatNumber(txtpc_transfe.Text, 2)
            txtpc_otro.Focus().Equals(True)
        End If
    End Sub

    Private Sub txtpc_otro_Click(sender As Object, e As EventArgs) Handles txtpc_otro.Click
        txtpc_otro.SelectionStart = 0
        txtpc_otro.SelectionLength = Len(txtpc_otro.Text)
    End Sub

    Private Sub txtpc_otro_GotFocus(sender As Object, e As EventArgs) Handles txtpc_otro.GotFocus
        txtpc_otro.SelectionStart = 0
        txtpc_otro.SelectionLength = Len(txtpc_otro.Text)
    End Sub

    Private Sub txtpc_otro_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtpc_otro.KeyPress
        If AscW(e.KeyChar) = Keys.Enter Then
            If txtpc_otro.Text = "" Then txtpc_otro.Text = "0"
            txtpc_otro.Text = FormatNumber(txtpc_otro.Text, 2)
            btnpc_aceptar.Focus().Equals(True)
        End If
    End Sub

    Private Sub txtpc_efectivo_TextChanged(sender As Object, e As EventArgs) Handles txtpc_efectivo.TextChanged
        If txtpc_apagar.Text = "" Then Exit Sub
        If txtpc_anticipo.Text = "" Then Exit Sub
        If txtpc_efectivo.Text = "" Then Exit Sub
        If txtpc_tarjeta.Text = "" Then Exit Sub
        If txtpc_transfe.Text = "" Then Exit Sub
        If txtpc_otro.Text = "" Then Exit Sub
        If txtpc_resta.Text = "" Then Exit Sub

        txtpc_resta.Text = CDbl(txtpc_apagar.Text) - (CDbl(txtpc_anticipo.Text) + CDbl(txtpc_efectivo.Text) + CDbl(txtpc_tarjeta.Text) + CDbl(txtpc_transfe.Text) + CDbl(txtpc_otro.Text))
        txtpc_resta.Text = FormatNumber(txtpc_resta.Text, 2)
    End Sub

    Private Sub txtpc_tarjeta_TextChanged(sender As Object, e As EventArgs) Handles txtpc_tarjeta.TextChanged
        If txtpc_apagar.Text = "" Then Exit Sub
        If txtpc_anticipo.Text = "" Then Exit Sub
        If txtpc_efectivo.Text = "" Then Exit Sub
        If txtpc_tarjeta.Text = "" Then Exit Sub
        If txtpc_transfe.Text = "" Then Exit Sub
        If txtpc_otro.Text = "" Then Exit Sub
        If txtpc_resta.Text = "" Then Exit Sub
        If txtpc_tarjeta.Text = "" Then txtpc_tarjeta.Text = "0.00"
        txtpc_resta.Text = CDbl(txtpc_apagar.Text) - (CDbl(txtpc_anticipo.Text) + CDbl(txtpc_efectivo.Text) + CDbl(txtpc_tarjeta.Text) + CDbl(txtpc_transfe.Text) + CDbl(txtpc_otro.Text))
        txtpc_resta.Text = FormatNumber(txtpc_resta.Text, 2)
    End Sub

    Private Sub txtpc_transfe_TextChanged(sender As Object, e As EventArgs) Handles txtpc_transfe.TextChanged
        If txtpc_apagar.Text = "" Then Exit Sub
        If txtpc_anticipo.Text = "" Then Exit Sub
        If txtpc_efectivo.Text = "" Then Exit Sub
        If txtpc_tarjeta.Text = "" Then Exit Sub
        If txtpc_transfe.Text = "" Then Exit Sub
        If txtpc_otro.Text = "" Then Exit Sub
        If txtpc_resta.Text = "" Then Exit Sub

        txtpc_resta.Text = CDbl(txtpc_apagar.Text) - (CDbl(txtpc_anticipo.Text) + CDbl(txtpc_efectivo.Text) + CDbl(txtpc_tarjeta.Text) + CDbl(txtpc_transfe.Text) + CDbl(txtpc_otro.Text))
        txtpc_resta.Text = FormatNumber(txtpc_resta.Text, 2)
    End Sub

    Private Sub txtpc_otro_TextChanged(sender As Object, e As EventArgs) Handles txtpc_otro.TextChanged
        If txtpc_apagar.Text = "" Then Exit Sub
        If txtpc_anticipo.Text = "" Then Exit Sub
        If txtpc_efectivo.Text = "" Then Exit Sub
        If txtpc_tarjeta.Text = "" Then Exit Sub
        If txtpc_transfe.Text = "" Then Exit Sub
        If txtpc_otro.Text = "" Then Exit Sub
        If txtpc_resta.Text = "" Then Exit Sub

        txtpc_resta.Text = CDbl(txtpc_apagar.Text) - (CDbl(txtpc_anticipo.Text) + CDbl(txtpc_efectivo.Text) + CDbl(txtpc_tarjeta.Text) + CDbl(txtpc_transfe.Text) + CDbl(txtpc_otro.Text))
        txtpc_resta.Text = FormatNumber(txtpc_resta.Text, 2)
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        txtpc_efectivo.Text = "0.00"
        txtpc_transfe.Text = "0.00"
        txtpc_tarjeta.Text = "0.00"
        txtpc_otro.Text = "0.00"
        txtpc_resta.Text = txtpc_apagar.Text
        pasa_pago = False
        panpago_compra.Visible = False
    End Sub

    Private Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click
        Try


            DondeVoy = "Guarda"
            tipo_impre = "NORMAL"

            If cbofactura.Text = "" Then
                If cboremision.Text = "" Then MsgBox("Escribe el número de remisión para continuar.", vbInformation + vbOKOnly, "Delsscom Control Negocios Pro") : cboremision.Focus().Equals(True) : Exit Sub
            End If

            If cboproveedor.Text = "" Then MsgBox("Selecciona un proveedor para continuar con la compra.", vbInformation + vbOKOnly, "Delsscom Control Negocios Pro") : cboproveedor.Focus().Equals(True) : Exit Sub
            If CDbl(txtresta.Text) < 0 Then MsgBox("El abono a la compra no puede ser mayor al total de la misma.", vbInformation + vbOKOnly, "Delsscom Control Negocios Pro") : txtefectivo.Focus().Equals(True) : Exit Sub
            If CDbl(txtsub1.Text) = 0 Then MsgBox("Falta algún precio o alguna cantidad.", vbInformation + vbOKOnly, "Delsscom Control Negocios Pro") : cbonombre.Focus().Equals(True) : Exit Sub
            If lblusuario.Text = "" Then MsgBox("Escribe tu contraseña para continuar.", vbInformation + vbOKOnly, "Delsscom Control Negocios Pro") : txtusuario.Focus().Equals(True) : Exit Sub

            If pasa_pago = False Then

                If MsgBox("¿Deseas guardar la compra?", vbInformation + vbOKCancel) = vbOK Then
                    txtpc_apagar.Text = FormatNumber(txtapagar.Text, 2)
                    txtpc_anticipo.Text = FormatNumber(txtanticipo.Text, 2)
                    txtpc_resta.Text = FormatNumber(txtresta.Text, 2)

                    panpago_compra.Visible = True
                    pasa_pago = True
                    txtpc_efectivo.Focus().Equals(True)
                Else
                    pasa_pago = True
                    btnGuardar.Focus.Equals(True)
                End If

            End If

            If txtdesc1.Text = "" Then txtdesc1.Text = "0.00"
            If txtdesc2.Text = "" Then txtdesc2.Text = "0.00"
            If txtefectivo.Text = "" Then txtefectivo.Text = "0.00"
            If txtanticipo.Text = "" Then txtanticipo.Text = "0.00"
            If txtpagos.Text = "" Then txtpagos.Text = "0.00"

        Catch ex As Exception
            MessageBox.Show(ex.ToString())
        End Try
    End Sub

    Private Sub btnpc_aceptar_Click(sender As Object, e As EventArgs) Handles btnpc_aceptar.Click
        Pagos.pc_porpagar = txtpc_apagar.Text
        Pagos.pc_anticipo = txtpc_anticipo.Text
        Pagos.pc_efectivo = txtpc_efectivo.Text
        Pagos.pc_tarjeta = txtpc_tarjeta.Text
        Pagos.pc_transfe = txtpc_transfe.Text
        Pagos.pc_otros = txtpc_otro.Text
        Pagos.pc_resta = txtpc_resta.Text



        'Validaciones de datos
        '-- No puede abonarse más del total
        Dim suma_abono As Double = CDbl(txtpc_efectivo.Text) + CDbl(txtpc_tarjeta.Text) + CDbl(txtpc_anticipo.Text) + CDbl(txtpc_transfe.Text) + CDbl(txtpc_otro.Text)
        Dim tota_abono As Double = CDbl(txtpc_apagar.Text)
        If suma_abono > tota_abono Then
            MsgBox("No puedes realizar un abono mayor al total de la compra.", vbInformation + vbOKOnly, "Delsscom Control Negocios Pro")
            txtpc_efectivo.Focus().Equals(True)
            Exit Sub
        End If

        If CDbl(txtpc_resta.Text) < 0 Then
            MsgBox("El restante total de la compra no puede ser negativo/menor a 0.", vbInformation + vbOKOnly, "Delsscom Control Negocios Pro")
            txtpc_efectivo.Focus().Equals(True)
            Exit Sub
        End If

        txtefectivo.Text = FormatNumber(txtpc_efectivo.Text, 2)
        txtpagos.Text = FormatNumber((CDbl(txtpc_tarjeta.Text) + CDbl(txtpc_transfe.Text) + CDbl(txtpc_otro.Text)), 2)
        txtresta.Text = FormatNumber(txtpc_resta.Text, 2)

        panpago_compra.Visible = False
        pasa_pago = True

        '__________________
        Dim usuario As String = ""
        Dim MyID As Integer = 0
        Dim id_usuario As Integer = 0

        Dim cnn1 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
        Dim cnn2 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
        Dim rd1, rd2 As MySqlDataReader
        Dim cmd1, cmd2 As MySqlCommand

        cnn1.Close() : cnn1.Open()
        cmd1 = cnn1.CreateCommand
        cmd1.CommandText =
                "select Id from Proveedores where Compania='" & cboproveedor.Text & "'"
        rd1 = cmd1.ExecuteReader
        If rd1.HasRows Then
            If rd1.Read Then
                MyID = rd1(0).ToString
            End If
        Else
            MsgBox("Este proveedor no se encuentra egistrado en el catálogo de proveedores." & vbNewLine & "Resgístralo para continuar con la compra.", vbInformation + vbOKOnly, "Delsscom Control Negocios Pro")
            rd1.Close()
            cnn1.Close()
            cboproveedor.Focus().Equals(True)
            Exit Sub
        End If
        rd1.Close()

        cmd1 = cnn1.CreateCommand
        cmd1.CommandText =
                "select Alias from Usuarios where Clave='" & txtusuario.Text & "'"
        rd1 = cmd1.ExecuteReader
        If rd1.HasRows Then
            If rd1.Read Then
                alias_compras = rd1("Alias").ToString()
            End If
        Else
            MsgBox("No se encuentra el usuario registrado en el sistema.", vbInformation + vbOKOnly, "Delsscom Control Negocios Pro")
            txtusuario.SelectionStart = 0
            txtusuario.SelectionLength = Len(txtusuario.Text)
            txtusuario.Focus().Equals(True)
            pasa_pago = False
            Exit Sub
        End If
        rd1.Close()
        cnn1.Close()

        'Call txtefectivo_LostFocus(txtefectivo, New EventArgs())
        If MsgBox("¿Deseas guardar esta compra?", vbInformation + vbOKCancel, "Delsscom Control Negocios Pro") = vbCancel Then cnn1.Close() : Exit Sub

        For t As Integer = 0 To grdCaptura.Rows.Count - 1
            If CDbl(grdCaptura.Rows(t).Cells(3).Value.ToString) = 0 Then
                MsgBox("Falta alguna cantidad.", vbInformation + vbOKOnly, "Delsscom Control Negocios Pro")
                cnn1.Close()
                Exit Sub
            End If
            If CDbl(grdCaptura.Rows(t).Cells(4).Value.ToString) = 0 Then
                MsgBox("Falta algún precio.", vbInformation + vbOKOnly, "Delsscom Control Negocios Pro")
                cnn1.Close()
                Exit Sub
            End If
        Next

        Dim MySaldo As Double = 0
        Dim Status As String = ""
        Dim MyACuenta As Double = 0
        Dim MySaldoF As Double = txtsaldo.Text
        Dim MyResta As Double = txtresta.Text

        Dim efectivo As Double = txtefectivo.Text
        Dim banco As String = ""
        Dim refer As String = ""
        Dim tarjeta As Double = 0
        Dim transfe As Double = 0
        Dim otro As Double = 0

        Dim pagadoc_saldo As Double = 0
        Dim sobra_saldo As Double = 0

        cnn1.Close() : cnn1.Open()
        cnn2.Close() : cnn2.Open()

        If MySaldoF = 0 Then
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText =
                    "select Saldo from AbonoE where Id=(select max(Id) from AbonoE where IdProv=" & MyID & ")"
            rd1 = cmd1.ExecuteReader
            If rd1.HasRows Then
                If rd1.Read Then
                    MySaldo = CDbl(IIf(rd1(0).ToString = "", 0, rd1(0).ToString)) + CDbl(txtapagar.Text)
                End If
            Else
                MySaldo = CDbl(txtapagar.Text)
            End If
            rd1.Close()
            MySaldo = FormatNumber(MySaldo, 4)

            cmd1 = cnn1.CreateCommand
            cmd1.CommandText =
                    "insert into AbonoE(NumRemision,NumFactura,NumPedido,IdProv,Proveedor,Concepto,Fecha,Hora,FechaCompleta,Cargo,Abono,Saldo,Banco,Referencia,Usuario) values('" & cboremision.Text & "','" & cbofactura.Text & "','" & cbopedido.Text & "'," & MyID & ",'" & cboproveedor.Text & "','COMPRA','" & Format(dtpfecha.Value, "yyyy-MM-dd") & "','" & Format(Date.Now, "HH:mm:ss") & "','" & Format(Date.Now, "yyyy-MM-dd HH:mm:ss") & "'," & CDbl(txtapagar.Text) & ",0," & MySaldo & ",'','','" & alias_compras & "')"
            cmd1.ExecuteNonQuery()

            If CDbl(txtresta.Text) = 0 Then
                Status = "PAGADO"
            Else
                Status = "RESTA"
            End If

            MyACuenta = CDbl(txtefectivo.Text) + CDbl(txtpagos.Text)

            If MyACuenta > 0 Then
                cmd1 = cnn1.CreateCommand
                cmd1.CommandText =
                        "select Saldo from AbonoE where Id=(select max(Id) from AbonoE where IdProv=" & MyID & ")"
                rd1 = cmd1.ExecuteReader
                If rd1.HasRows Then
                    If rd1.Read Then
                        MySaldo = CDbl(IIf(rd1(0).ToString = "", 0, rd1(0).ToString)) - MyACuenta
                    End If
                Else
                    MySaldo = MyACuenta
                End If
                rd1.Close()

                tarjeta = txtpc_tarjeta.Text
                transfe = txtpc_transfe.Text
                otro = txtpc_otro.Text

                cmd1 = cnn1.CreateCommand
                cmd1.CommandText =
                        "insert into AbonoE(NumRemision,NumFactura,NumPedido,IdProv,Proveedor,Concepto,Fecha,Hora,FechaCompleta,Cargo,Abono,Saldo,Efectivo,Tarjeta,Transfe,Otro,Banco,Referencia,Usuario,Corte,CorteU,Cargado) values('" & cboremision.Text & "','" & cbofactura.Text & "','" & cbopedido.Text & "'," & MyID & ",'" & cboproveedor.Text & "','ABONO','" & Format(dtpfecha.Value, "yyyy-MM-dd") & "','" & Format(Date.Now, "HH:mm:ss") & "','" & Format(Date.Now, "yyyy-MM-dd HH:mm:ss") & "',0," & MyACuenta & "," & MySaldo & "," & efectivo & "," & tarjeta & "," & transfe & "," & otro & ",'','','" & alias_compras & "',0,0,0)"
                cmd1.ExecuteNonQuery()
            End If
        Else
            If CDbl(txtresta.Text) = 0 Then
                cmd1 = cnn1.CreateCommand
                cmd1.CommandText =
                        "select Saldo from AbonoE where Id=(select max(Id) from AbonoE where IdProv=" & MyID & ")"
                rd1 = cmd1.ExecuteReader
                If rd1.HasRows Then
                    If rd1.Read Then
                        MySaldo = CDbl(IIf(rd1(0).ToString = "", 0, rd1(0).ToString)) + CDbl(txtapagar.Text)
                    End If
                Else
                    MySaldo = CDbl(txtapagar.Text)
                End If
                rd1.Close()
                MySaldo = FormatNumber(MySaldo, 4)

                cmd1 = cnn1.CreateCommand
                cmd1.CommandText =
                        "insert into AbonoE(NumRemision,NumFactura,NumPedido,IdProv,Proveedor,Concepto,Fecha,Hora,FechaCompleta,Cargo,Abono,Saldo,Banco,Referencia,Usuario) values('" & cboremision.Text & "','" & cbofactura.Text & "','" & cbopedido.Text & "'," & MyID & ",'" & cboproveedor.Text & "','COMPRA','" & Format(dtpfecha.Value, "yyyy-MM-dd") & "','" & Format(Date.Now, "HH:mm:ss") & "','" & Format(Date.Now, "yyyy-MM-dd HH:mm:ss") & "'," & CDbl(txtapagar.Text) & ",0," & MySaldo & ",'','','" & alias_compras & "')"
                cmd1.ExecuteNonQuery()

                If CDbl(txtresta.Text) = 0 Then
                    Status = "PAGADO"
                Else
                    Status = "RESTA"
                End If

                MyACuenta = CDbl(txtefectivo.Text) + CDbl(txtpagos.Text) + CDbl(txtanticipo.Text)

                If MyACuenta > 0 Then
                    cmd1 = cnn1.CreateCommand
                    cmd1.CommandText =
                            "select Saldo from AbonoE where Id=(select max(Id) from AbonoE where IdProv=" & MyID & ")"
                    rd1 = cmd1.ExecuteReader
                    If rd1.HasRows Then
                        If rd1.Read Then
                            MySaldo = CDbl(IIf(rd1(0).ToString = "", 0, rd1(0).ToString)) - MyACuenta
                        End If
                    Else
                        MySaldo = MyACuenta
                    End If
                    rd1.Close()

                    tarjeta = txtpc_tarjeta.Text
                    transfe = txtpc_transfe.Text
                    otro = txtpc_otro.Text

                    cmd1 = cnn1.CreateCommand
                    cmd1.CommandText =
                            "insert into AbonoE(NumRemision,NumFactura,NumPedido,IdProv,Proveedor,Concepto,Fecha,Hora,FechaCompleta,Cargo,Abono,Saldo,Efectivo,Tarjeta,Transfe,Otro,Banco,Referencia,Usuario,Corte,CorteU,Cargado) values('" & cboremision.Text & "','" & cbofactura.Text & "','" & cbopedido.Text & "'," & MyID & ",'" & cboproveedor.Text & "','ABONO','" & Format(dtpfecha.Value, "yyyy-MM-dd") & "','" & Format(Date.Now, "HH:mm:ss") & "','" & Format(Date.Now, "yyyy-MM-dd HH:mm:ss") & "',0," & MyACuenta & "," & MySaldo & "," & efectivo & "," & tarjeta & "," & transfe & "," & otro & ",'" & banco & "','" & refer & "','" & alias_compras & "',0,0,0)"
                    cmd1.ExecuteNonQuery()
                End If
            Else
                MyACuenta = CDbl(txtefectivo.Text) + CDbl(txtpagos.Text)
                If MyACuenta > 0 Then
                    If MySaldoF > 0 Then
                        cmd1 = cnn1.CreateCommand
                        cmd1.CommandText =
                                "select Saldo from AbonoE where Id=(select max(Id) from AbonoE where IdProv=" & MyID & ")"
                        rd1 = cmd1.ExecuteReader
                        If rd1.HasRows Then
                            If rd1.Read Then
                                MySaldo = CDbl(IIf(rd1(0).ToString = "", 0, rd1(0).ToString))
                            End If
                        Else
                            MySaldo = CDbl(txtapagar.Text)
                        End If
                        rd1.Close()
                        MySaldo = FormatNumber(MySaldo, 4)

                        cmd1 = cnn1.CreateCommand
                        cmd1.CommandText =
                                "insert into AbonoE(NumRemision,NumFactura,NumPedido,IdProv,Proveedor,Concepto,Fecha,Hora,FechaCompleta,Cargo,Abono,Saldo,Banco,Referencia,Usuario) values('" & cboremision.Text & "','" & cbofactura.Text & "','" & cbopedido.Text & "'," & MyID & ",'" & cboproveedor.Text & "','COMPRA','" & Format(dtpfecha.Value, "yyyy-MM-dd") & "','" & Format(Date.Now, "HH:mm:ss") & "','" & Format(Date.Now, "yyyy-MM-dd HH:mm:ss") & "'," & CDbl(txtapagar.Text) & ",0," & MySaldo & ",'','','" & alias_compras & "')"
                        cmd1.ExecuteNonQuery()

                        Dim pagado As Double = 0
                        If MySaldoF > CDbl(txtresta.Text) Then
                            pagado = FormatNumber(MyACuenta + CDbl(txtresta.Text), 2)
                            sobra_saldo = MySaldoF - CDbl(txtresta.Text)
                        Else

                        End If

                        If pagado = CDbl(txtapagar.Text) Then
                            Status = "PAGADO"
                        Else
                            Status = "RESTA"
                        End If

                        If MyACuenta > 0 Then
                            cmd1 = cnn1.CreateCommand
                            cmd1.CommandText =
                                    "select Saldo from AbonoE where Id=(select max(Id) from AbonoE where IdProv=" & MyID & ")"
                            rd1 = cmd1.ExecuteReader
                            If rd1.HasRows Then
                                If rd1.Read Then
                                    MySaldo = CDbl(IIf(rd1(0).ToString = "", 0, rd1(0).ToString)) + CDbl(txtresta.Text)
                                End If
                            Else
                                MySaldo = MyACuenta
                            End If
                            rd1.Close()

                            tarjeta = txtpc_tarjeta.Text
                            transfe = txtpc_transfe.Text
                            otro = txtpc_otro.Text

                            cmd1 = cnn1.CreateCommand
                            cmd1.CommandText =
                                    "insert into AbonoE(NumRemision,NumFactura,NumPedido,IdProv,Proveedor,Concepto,Fecha,Hora,FechaCompleta,Cargo,Abono,Saldo,Efectivo,Tarjeta,Transfe,Otro,Banco,Referencia,Usuario,Corte,CorteU,Cargado) values('" & cboremision.Text & "','" & cbofactura.Text & "','" & cbopedido.Text & "'," & MyID & ",'" & cboproveedor.Text & "','ABONO','" & Format(dtpfecha.Value, "yyyy-MM-dd") & "','" & Format(Date.Now, "HH:mm:ss") & "','" & Format(Date.Now, "yyyy-MM-dd HH:mm:ss") & "',0," & MyACuenta & "," & MySaldo & "," & efectivo & "," & tarjeta & "," & transfe & "," & otro & ",'" & banco & "','" & refer & "','" & alias_compras & "',0,0,0)"
                            cmd1.ExecuteNonQuery()
                        End If
                    Else

                    End If
                Else
                    If MySaldoF > 0 Then
                        'cmd1 = cnn1.CreateCommand
                        'cmd1.CommandText =
                        '        "insert into AbonoE(NumRemision,NumFactura,NumPedido,IdProv,Proveedor,Concepto,Fecha,Hora,FechaCompleta,Cargo,Abono,Saldo,Banco,Referencia,Usuario) values('" & cboremision.Text & "','" & cbofactura.Text & "','" & cbopedido.Text & "'," & MyID & ",'" & cboproveedor.Text & "','COMPRA','" & Format(dtpfecha.Value, "yyyy-MM-dd") & "','" & Format(Date.Now, "HH:mm:ss") & "','" & Format(Date.Now, "yyyy-MM-dd HH:mm:ss") & "'," & CDbl(txtapagar.Text) & ",0," & tota_abono & ",'','','" & alias_compras & "')"
                        'cmd1.ExecuteNonQuery()
                    Else

                    End If
                End If

                If MySaldoF > CDbl(txtresta.Text) Then
                    cmd1 = cnn1.CreateCommand
                    cmd1.CommandText =
                            "select Saldo from AbonoE where Id=(select max(Id) from AbonoE where IdProv=" & MyID & ")"
                    rd1 = cmd1.ExecuteReader
                    If rd1.HasRows Then
                        If rd1.Read Then
                            MySaldo = CDbl(IIf(rd1(0).ToString = "", 0, rd1(0).ToString))
                        End If
                    Else
                        MySaldo = CDbl(txtapagar.Text)
                    End If
                    rd1.Close()
                    MySaldo = FormatNumber(MySaldo, 4)

                    cmd1 = cnn1.CreateCommand
                    cmd1.CommandText =
                            "insert into AbonoE(NumRemision,NumFactura,NumPedido,IdProv,Proveedor,Concepto,Fecha,Hora,FechaCompleta,Cargo,Abono,Saldo,Banco,Referencia,Usuario) values('" & cboremision.Text & "','" & cbofactura.Text & "','" & cbopedido.Text & "'," & MyID & ",'" & cboproveedor.Text & "','COMPRA','" & Format(dtpfecha.Value, "yyyy-MM-dd") & "','" & Format(Date.Now, "HH:mm:ss") & "','" & Format(Date.Now, "yyyy-MM-dd HH:mm:ss") & "'," & CDbl(txtapagar.Text) & ",0," & MySaldo & ",'','','" & alias_compras & "')"
                    cmd1.ExecuteNonQuery()

                    Status = "PAGADO"
                    MyResta = 0

                    MyACuenta = CDbl(txtefectivo.Text) + CDbl(txtpagos.Text) + CDbl(txtanticipo.Text) 'CDbl(txtapagar.Text)

                    cmd1 = cnn1.CreateCommand
                    cmd1.CommandText =
                            "select Saldo from AbonoE where Id=(select max(Id) from AbonoE where IdProv=" & MyID & ")"
                    rd1 = cmd1.ExecuteReader
                    If rd1.HasRows Then
                        If rd1.Read Then
                            MySaldo = CDbl(IIf(rd1(0).ToString = "", 0, rd1(0).ToString)) + MyACuenta
                        End If
                    Else
                        MySaldo = MyACuenta
                    End If
                    rd1.Close()

                    cmd1 = cnn1.CreateCommand
                    cmd1.CommandText =
                            "insert into AbonoE(NumRemision,NumFactura,NumPedido,IdProv,Proveedor,Concepto,Fecha,Hora,FechaCompleta,Cargo,Abono,Saldo,Efectivo,Tarjeta,Transfe,Otro,Banco,Referencia,Usuario,Corte,CorteU,Cargado) values('" & cboremision.Text & "','" & cbofactura.Text & "','" & cbopedido.Text & "'," & MyID & ",'" & cboproveedor.Text & "','ABONO','" & Format(dtpfecha.Value, "yyyy-MM-dd") & "','" & Format(Date.Now, "HH:mm:ss") & "','" & Format(Date.Now, "yyyy-MM-dd HH:mm:ss") & "',0," & MyACuenta & "," & MySaldo & "," & efectivo & "," & tarjeta & "," & transfe & "," & MyACuenta & ",'" & banco & "','" & refer & "','" & alias_compras & "',0,0,0)"
                    cmd1.ExecuteNonQuery()

                ElseIf MySaldoF < CDbl(txtresta.Text) Then
                    If CDbl(txtresta.Text) = 0 Then
                        Status = "PAGADO"
                    Else
                        Status = "RESTA"
                    End If

                    MyACuenta = CDbl(txtefectivo.Text) + CDbl(txtpagos.Text)

                    cmd1 = cnn1.CreateCommand
                    cmd1.CommandText =
                            "select Saldo from AbonoE where Id=(select max(Id) from AbonoE where IdProv=" & MyID & ")"
                    rd1 = cmd1.ExecuteReader
                    If rd1.HasRows Then
                        If rd1.Read Then
                            MySaldo = CDbl(IIf(rd1(0).ToString = "", 0, rd1(0).ToString)) - CDbl(txtanticipo.Text) 'MyACuenta
                        End If
                    Else
                        MySaldo = MyACuenta
                    End If
                    rd1.Close()

                    cmd1 = cnn1.CreateCommand
                    cmd1.CommandText =
                            "insert into AbonoE(NumRemision,NumFactura,NumPedido,IdProv,Proveedor,Concepto,Fecha,Hora,FechaCompleta,Cargo,Abono,Saldo,Banco,Referencia,Usuario) values('" & cboremision.Text & "','" & cbofactura.Text & "','" & cbopedido.Text & "'," & MyID & ",'" & cboproveedor.Text & "','COMPRA','" & Format(dtpfecha.Value, "yyyy-MM-dd") & "','" & Format(Date.Now, "HH:mm:ss") & "','" & Format(Date.Now, "yyyy-MM-dd HH:mm:ss") & "'," & CDbl(txtapagar.Text) & ",0," & MySaldo & ",'','','" & alias_compras & "')"
                    cmd1.ExecuteNonQuery()

                    Dim abonoc As Double = 0
                    abonoc = CDbl(txtapagar.Text) - CDbl(MySaldoF)

                    cmd1 = cnn1.CreateCommand
                    cmd1.CommandText =
                            "insert into AbonoE(NumRemision,NumFactura,NumPedido,IdProv,Proveedor,Concepto,Fecha,Hora,FechaCompleta,Cargo,Abono,Saldo,Efectivo,Tarjeta,Transfe,Otro,Banco,Referencia,Usuario,Corte,CorteU,Cargado) values('" & cboremision.Text & "','" & cbofactura.Text & "','" & cbopedido.Text & "'," & MyID & ",'" & cboproveedor.Text & "','ABONO','" & Format(dtpfecha.Value, "yyyy-MM-dd") & "','" & Format(Date.Now, "HH:mm:ss") & "','" & Format(Date.Now, "yyyy-MM-dd HH:mm:ss") & "',0," & MySaldoF & "," & abonoc & "," & efectivo & "," & tarjeta & "," & transfe & "," & MyACuenta & ",'" & banco & "','" & refer & "','" & alias_compras & "',0,0,0)"
                    cmd1.ExecuteNonQuery()

                End If
            End If
        End If

        cmd1 = cnn1.CreateCommand
        cmd1.CommandText =
                "insert into Compras(NumRemision,NumFactura,NumPedido,NotaCred,IdProv,Proveedor,Sub1,Desc1,Sub2,IVA,Total,Desc2,IEPS,Pagar,ACuenta,Resta,FechaC,FechaP,FechaNC,Status,FechaCancela,Usuario,Corte,CorteU,Cargado,Anticipo) values('" & cboremision.Text & "','" & cbofactura.Text & "','" & cbopedido.Text & "',''," & MyID & ",'" & cboproveedor.Text & "'," & CDbl(txtsub1.Text) & "," & CDbl(txtdesc1.Text) & "," & CDbl(txtsub2.Text) & "," & CDbl(txtiva.Text) & "," & CDbl(txtTotalC.Text) & "," & CDbl(txtdesc2.Text) & "," & CDbl(txtieps.Text) & "," & CDbl(txtapagar.Text) & "," & MyACuenta & "," & MyResta & ",'" & Format(Date.Now, "yyyy-MM-dd HH:mm:ss") & "','" & Format(dtpfpago.Value, "yyyy-MM-dd") & "','','" & Status & "','','" & alias_compras & "',0,0,0," & CDbl(txtanticipo.Text) & ")"
        cmd1.ExecuteNonQuery()

        Dim IdCompra As Integer = 0

        Do Until IdCompra <> 0
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText =
                    "select Id from Compras where NumRemision='" & cboremision.Text & "' and Proveedor='" & cboproveedor.Text & "'"
            rd1 = cmd1.ExecuteReader
            If rd1.HasRows Then
                If rd1.Read Then
                    IdCompra = rd1(0).ToString
                End If
            End If
            rd1.Close()
        Loop

        Dim i As Integer = 0

        For luffy As Integer = 0 To grdCaptura.Rows.Count - 1

            Dim descrip As String = grdCaptura.Rows(luffy).Cells(0).Value.ToString
            Dim uni As String = grdCaptura.Rows(luffy).Cells(1).Value.ToString
            Dim precioventa As Double = grdCaptura.Rows(luffy).Cells(5).Value.ToString
            Dim cantidad As Double = grdCaptura.Rows(luffy).Cells(2).Value.ToString
            Dim preciocompra As Double = grdCaptura.Rows(luffy).Cells(3).Value.ToString
            Dim total As Double = grdCaptura.Rows(luffy).Cells(6).Value.ToString
            Dim PRECIO As Double = grdCaptura.Rows(luffy).Cells(5).Value.ToString

            Dim grupo As String = ""
            Dim depto As String = ""
            Dim IVAA As Double = 0

            cmd2 = cnn2.CreateCommand
            cmd2.CommandText = "SELECT Grupo,Departamento,IVA FROM productos WHERE Grupo='" & descrip & "'"
            rd2 = cmd2.ExecuteReader
            If rd2.HasRows Then
                If rd2.Read Then
                    grupo = rd2(0).ToString
                    depto = rd2(1).ToString
                    IVAA = rd2(2).ToString
                End If
            Else
                MsgBox("El modelo no existe.", vbInformation + vbOKOnly, titulocentral)
                Exit Sub
            End If
            rd2.Close()

            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "INSERT INTO comprasentalla(Id_compra,proveedor,NumRemision,NumFactura,Codigo,Nombre,UCompra,PrecioVenta,Cantidad,Precio,Total,Fecha,Grupo,Depto,Modelo,Modif,Status) VALUES(" & IdCompra & ",'" & cboproveedor.Text & "','" & cboremision.Text & "','" & cbofactura.Text & "','1','" & descrip & "','" & uni & "'," & precioventa & "," & cantidad & "," & preciocompra & "," & total & ",'" & Format(Date.Now, "yyyy-MM-dd") & "','" & grupo & "','" & depto & "','" & descrip & "','','" & Status & "')"
            cmd1.ExecuteNonQuery()

            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "UPDATE productos SET PrecioCompra=" & preciocompra & ",PrecioVenta=" & CDec(PRECIO) / (1 + IVAA) & ",PrecioVentaIVA=" & PRECIO & " WHERE grupo='" & descrip & "'"
            cmd1.ExecuteNonQuery()

        Next
        cnn2.Close()

        If cbopedido.Text <> "" Then
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText =
                    "update Pedidos set Status=1 where Num='" & cbopedido.Text & "' and Proveedor='" & cboproveedor.Text & "'"
            cmd1.ExecuteNonQuery()
        End If

        If txtsaldo.Text > 0 Then
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "UPDATE proveedores SET Saldo=Saldo - " & txtanticipo.Text & " WHERE Compania='" & cboproveedor.Text & "'"
            cmd1.ExecuteNonQuery()
        End If

        cmd1 = cnn1.CreateCommand
        cmd1.CommandText = "delete from AuxCompras"
        cmd1.ExecuteNonQuery()
        cnn1.Close()

        'Pregunta sí se quiere imprimir la compra
        If MsgBox("¿Deseas imprimir la compra?", vbInformation + vbOKCancel, "Delsscom Control Negocios Pro") = vbOK Then
            Dim ImprimeEn As String = ""
            Dim Impresora As String = ""
            Dim tPapel As String = ""

            cnn1.Close() : cnn1.Open()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText =
                    "select Formato from RutasImpresion where Equipo='" & ObtenerNombreEquipo() & "' and Tipo='Compras'"
            rd1 = cmd1.ExecuteReader
            If rd1.HasRows Then
                If rd1.Read Then
                    tPapel = rd1(0).ToString
                End If
            Else
                MsgBox("No se ha establecido un tamaño de papel para el formato de impresión de ventas.", vbInformation + vbOKOnly, "Delsscom Control Negocios Pro")
                rd1.Close()
                GoTo deku
            End If
            rd1.Close()

            Dim taimpre As Integer = TamImpre()



            cmd1 = cnn1.CreateCommand
            cmd1.CommandText =
                    "select Impresora from RutasImpresion where Equipo='" & ObtenerNombreEquipo() & "' and Tipo='" & tPapel & "'"
            rd1 = cmd1.ExecuteReader
            If rd1.HasRows Then
                If rd1.Read Then
                    Impresora = rd1(0).ToString
                End If
            Else
                If tPapel = "MEDIA CARTA" Then
                    cnn2.Close() : cnn2.Open()
                    cmd2 = cnn2.CreateCommand
                    cmd2.CommandText =
                            "select Impresora from RutasImpresion where Equipo='" & ObtenerNombreEquipo() & "' and Tipo='CARTA'"
                    rd2 = cmd2.ExecuteReader
                    If rd2.HasRows Then
                        If rd2.Read Then
                            Impresora = rd2(0).ToString()
                        End If
                    Else
                        MsgBox("No tienes una impresora configurada para imprimir en formato " & tPapel & ".", vbInformation + vbOKOnly, "Delsscom Control Negocios Pro")
                    End If
                    rd2.Close() : cnn2.Close()
                End If
                cnn1.Close()
            End If
            rd1.Close() : cnn1.Close()

            If Impresora = "" Then
                MsgBox("Impresora no configurada", vbInformation + vbOKOnly, titulocentral)
                GoTo deku
            End If

            If tPapel = "TICKET" Then
                If taimpre = "80" Then
                    Dim ps As New System.Drawing.Printing.PaperSize("Custom", 269, 3000)
                    pTicket80.DefaultPageSettings.PaperSize = ps
                    pTicket80.DefaultPageSettings.PrinterSettings.PrinterName = Impresora
                    pTicket80.Print()
                End If
                If taimpre = "58" Then
                    pTicket58.DefaultPageSettings.PrinterSettings.PrinterName = Impresora
                    pTicket58.Print()
                End If
            Else
                If tPapel = "MEDIA CARTA" Then
                    'pMediaCarta.DefaultPageSettings.PrinterSettings.PrinterName = Impresora
                    'pMediaCarta.Print()
                End If
                If tPapel = "CARTA" Then
                    'pCarta.DefaultPageSettings.PrinterSettings.PrinterName = Impresora
                    'pCarta.Print()
                End If
            End If
            If tPapel = "PDF - CARTA" Then


            End If
        End If
deku:
        btnLimpiar.PerformClick()
        cboproveedor.Focus().Equals(True)
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim id_compra As Double = 0
        Dim acuenta As Double = 0
        Dim resta As Double = 0
        Dim status As String = ""

        Dim id_prov As Double = 0

        Dim cnn1 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
        Dim rd1 As MySqlDataReader
        Dim cmd1 As MySqlCommand

        On Error GoTo quepasowey
        If cnn1.State = 1 Then cnn1.Close()

        cnn1.Close() : cnn1.Open()

        cmd1 = cnn1.CreateCommand
        cmd1.CommandText =
            "select Id,IdProv,ACuenta,Resta,Status from Compras where NumRemision='" & cboremision.Text & "' and Proveedor='" & cboproveedor.Text & "' and Status<>'CANCELADA'"
        rd1 = cmd1.ExecuteReader
        If rd1.HasRows Then
            If rd1.Read Then
                id_compra = rd1("Id").ToString()
                id_prov = rd1("IdProv").ToString()
                acuenta = rd1("ACuenta").ToString()
                resta = rd1("Resta").ToString()
                status = rd1("Status").ToString()
            End If
        End If
        rd1.Close()

        Dim saldo As Double = 0
        Dim abono As Double = 0

        cmd1 = cnn1.CreateCommand
        cmd1.CommandText =
            "select Abono from AbonoE where NumRemision='" & cboremision.Text & "' and Concepto='ABONO' and IdProv=" & id_prov & ""
        rd1 = cmd1.ExecuteReader
        Do While rd1.Read
            If rd1.HasRows Then
                abono = abono + CDbl(rd1(0).ToString())
            End If
        Loop
        rd1.Close()

        cmd1 = cnn1.CreateCommand
        cmd1.CommandText =
            "select Saldo from AbonoE where Id=(select max(Id) from AbonoE where IdProv=" & id_prov & ")"
        rd1 = cmd1.ExecuteReader
        If rd1.HasRows Then
            If rd1.Read Then
                saldo = rd1(0).ToString
            End If
        End If
        rd1.Close()

        Dim mysaldo As Double = 0
        mysaldo = saldo - abono

        cmd1 = cnn1.CreateCommand
        cmd1.CommandText =
            "insert into AbonoE(NumRemision,NumFactura,NumPedido,IdProv,Proveedor,Concepto,Fecha,Hora,FechaCompleta,Cargo,Abono,Saldo,Banco,Referencia,Usuario) values('" & cboremision.Text & "','" & cbofactura.Text & "','" & cbopedido.Text & "'," & id_prov & ",'" & cboproveedor.Text & "','CANCELACION','" & Format(dtpfecha.Value, "yyyy-MM-dd") & "','" & Format(Date.Now, "HH:mm:ss") & "','" & Format(Date.Now, "yyyy-MM-dd HH:mm:ss") & "',0," & abono & "," & mysaldo & ",'','','" & alias_compras & "')"
        cmd1.ExecuteNonQuery()

        Dim fecha_cancela As String = Format(Date.Now, "yyyy-MM-dd")

        cmd1 = cnn1.CreateCommand
        cmd1.CommandText =
            "update Compras set Status='CANCELADA',FechaCancela='" & fecha_cancela & "' where Id=" & id_compra
        cmd1.ExecuteNonQuery()

        For t As Integer = 0 To grdCaptura.Rows.Count - 1
            Dim codigo As String = grdCaptura.Rows(t).Cells(0).Value.ToString()
            Dim nombre As String = grdCaptura.Rows(t).Cells(1).Value.ToString()
            Dim unidad As String = grdCaptura.Rows(t).Cells(2).Value.ToString()
            Dim cantidad As Double = grdCaptura.Rows(t).Cells(3).Value.ToString()
            Dim precio As Double = grdCaptura.Rows(t).Cells(4).Value.ToString()
            Dim existencia As Double = 0

            cmd1 = cnn1.CreateCommand
            cmd1.CommandText =
                "select Existencia from Productos where Codigo='" & Strings.Left(codigo, 6) & "'"
            rd1 = cmd1.ExecuteReader
            If rd1.HasRows Then
                If rd1.Read Then
                    existencia = rd1("Existencia").ToString()
                End If
            End If
            rd1.Close()

            'Actualiza el cardex
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText =
                "insert into Cardex(Codigo,Nombre,Movimiento,Inicial,Cantidad,Final,Precio,Fecha,Usuario,Folio,Tipo,Cedula,Receta,Medico,Domicilio) values('" & codigo & "','" & nombre & "','Cancela compra'," & existencia & "," & cantidad & "," & (existencia - cantidad) & "," & precio & ",'" & Format(Date.Now, "yyyy-MM-dd") & "','" & alias_compras & "','" & cboremision.Text & "','','','','','')"
            cmd1.ExecuteNonQuery()

            'Actualiza la tabla del Costeo
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText =
                "update Costeo set Saldo=0 where Referencia='" & cboremision.Text & "' and Concepto='COMPRA' and Codigo='" & Strings.Left(codigo, 6) & "'"
            cmd1.ExecuteNonQuery()

            'Actualiza la tabla de productos
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText =
                "update Productos set Existencia=" & (existencia - cantidad) & " where Codigo='" & Strings.Left(codigo, 6) & "'"
            cmd1.ExecuteNonQuery()
        Next
        cnn1.Close()

        If MsgBox("¿Deseas imprimir un recibo de la cancelación?", vbInformation + vbYesNo, "Delsscom Control Negocios Pro") = vbYes Then
            Dim ImprimeEn As String = ""
            Dim Impresora As String = ""
            Dim tPapel As String = ""
            Dim tMilimetros As String = ""
            printLine = 0
            Contador = 0
            pagina = 0

            tipo_cancelacion = "CASO 2"

            cnn1.Close() : cnn1.Open()

            cmd1 = cnn1.CreateCommand
            cmd1.CommandText =
                "select NotasCred from Formatos where Facturas='TPapelCP'"
            rd1 = cmd1.ExecuteReader
            If rd1.HasRows Then
                If rd1.Read Then
                    tPapel = rd1(0).ToString
                    If tPapel = "CARTA" Or tPapel = "MEDIA CARTA" Then
                        ImprimeEn = "ImpreC"
                    ElseIf tPapel = "TICKET" Then
                        ImprimeEn = "ImpreT"
                    Else
                        ImprimeEn = ""
                    End If
                End If
            Else
                MsgBox("No se ha establecido un tamaño de papel para el formato de impresión de compras.", vbInformation + vbOKOnly, titulocentral)
                rd1.Close()
                cnn1.Close()
                btnLimpiar.PerformClick()
                Exit Sub
            End If
            rd1.Close()

            cmd1 = cnn1.CreateCommand
            cmd1.CommandText =
                "select NotasCred from Formatos where Facturas='TamImpre'"
            rd1 = cmd1.ExecuteReader
            If rd1.HasRows Then
                If rd1.Read Then
                    tMilimetros = rd1(0).ToString
                End If
            End If
            rd1.Close()

            If ImprimeEn <> "" Then
                cmd1 = cnn1.CreateCommand
                cmd1.CommandText =
                    "select NotasCred from Formatos where Facturas='" & ImprimeEn & "'"
                rd1 = cmd1.ExecuteReader
                If rd1.HasRows Then
                    If rd1.Read Then
                        Impresora = rd1(0).ToString
                    End If
                End If
                rd1.Close()
            Else
                MsgBox("No tienes una impresora configurada para imprimir en formato " & tPapel & ".", vbInformation + vbOKOnly, titulocentral)
                cnn1.Close()
                btnLimpiar.PerformClick()
                Exit Sub
            End If
            cnn1.Close()

            If tPapel = "TICKET" Then
                If tMilimetros = "80" Then
                    pCancela80.DefaultPageSettings.PrinterSettings.PrinterName = Impresora
                    pCancela80.Print()
                End If
                If tMilimetros = "58" Then
                    pCancela58.DefaultPageSettings.PrinterSettings.PrinterName = Impresora
                    pCancela58.Print()
                End If
            End If
            If tPapel = "MEDIA CARTA" Then
                pCancelaMC.DefaultPageSettings.PrinterSettings.PrinterName = Impresora
                pCancelaMC.Print()
            End If
            If tPapel = "CARTA" Then
                pCancelaCarta.DefaultPageSettings.PrinterSettings.PrinterName = Impresora
                pCancelaCarta.Print()
            End If
            If tPapel = "PDF - CARTA" Then
                'Genera PDF y lo guarda en la ruta

            End If
        End If
        MsgBox("Compra cancelada correctamente.", vbInformation + vbOKOnly, titulocentral)
        btnLimpiar.PerformClick()
        Exit Sub
quepasowey:
        MsgBox(Err.Description & " - " & Err.Number & vbNewLine & "No se pudo realizar la cancelación debido a un error inesperado.", vbInformation + vbOKOnly, titulocentral)
        cnn1.Close()
        Exit Sub
    End Sub

    Private Sub pTicket80_PrintPage(sender As Object, e As Printing.PrintPageEventArgs) Handles pTicket80.PrintPage
        'Fuentes prederminadas
        Dim tipografia As String = "Lucida Sans Typewriter"
        Dim fuente_datos As New Drawing.Font(tipografia, 10, FontStyle.Regular)
        Dim fuente_prods As New Drawing.Font(tipografia, 9, FontStyle.Regular)
        'Variables
        Dim sc As New StringFormat With {.Alignment = StringAlignment.Center}
        Dim sf As New StringFormat With {.Alignment = StringAlignment.Far}
        Dim pen As New Pen(Brushes.Black, 1)
        Dim Y As Double = 0
        Dim Logotipo As Drawing.Image = Nothing

        Dim Pie As String = ""
        Dim cnn1 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
        Dim rd1 As MySqlDataReader
        Dim cmd1 As MySqlCommand

        On Error GoTo kakita

        '[°]. Logotipo
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

        '[°]. Datos del negocio
        cnn1.Close() : cnn1.Open()

        cmd1 = cnn1.CreateCommand
        cmd1.CommandText =
            "select Pie2,Cab0,Cab1,Cab2,Cab3,Cab4,Cab5,Cab6 from Ticket"
        rd1 = cmd1.ExecuteReader
        If rd1.HasRows Then
            If rd1.Read Then
                Pie = rd1("Pie2").ToString
                'Razón social
                If rd1("Cab0").ToString() <> "" Then
                    e.Graphics.DrawString(rd1("Cab0").ToString, New Drawing.Font(tipografia, 10, FontStyle.Bold), Brushes.Black, 140, Y, sc)
                    Y += 12.5
                End If
                'RFC
                If rd1("Cab1").ToString() <> "" Then
                    e.Graphics.DrawString(rd1("Cab1").ToString, New Drawing.Font(tipografia, 10, FontStyle.Bold), Brushes.Black, 140, Y, sc)
                    Y += 12.5
                End If
                'Calle  N°.
                If rd1("Cab2").ToString() <> "" Then
                    e.Graphics.DrawString(rd1("Cab2").ToString, New Drawing.Font(tipografia, 9, FontStyle.Regular), Brushes.Gray, 140, Y, sc)
                    Y += 12
                End If
                'Colonia
                If rd1("Cab3").ToString() <> "" Then
                    e.Graphics.DrawString(rd1("Cab3").ToString, New Drawing.Font(tipografia, 9, FontStyle.Regular), Brushes.Gray, 140, Y, sc)
                    Y += 12
                End If
                'Delegación / Municipio - Entidad
                If rd1("Cab4").ToString() <> "" Then
                    e.Graphics.DrawString(rd1("Cab4").ToString, New Drawing.Font(tipografia, 9, FontStyle.Regular), Brushes.Gray, 140, Y, sc)
                    Y += 12
                End If
                'Teléfono
                If rd1("Cab5").ToString() <> "" Then
                    e.Graphics.DrawString(rd1("Cab5").ToString, New Drawing.Font(tipografia, 9, FontStyle.Regular), Brushes.Gray, 140, Y, sc)
                    Y += 12
                End If
                'Correo
                If rd1("Cab6").ToString() <> "" Then
                    e.Graphics.DrawString(rd1("Cab6").ToString, New Drawing.Font(tipografia, 9, FontStyle.Regular), Brushes.Gray, 140, Y, sc)
                    Y += 12
                End If
                Y += 4
            End If
        Else
            Y += 0
        End If
        rd1.Close()

        '[1]. Datos de la compra
        e.Graphics.DrawString("--------------------------------------------------------", New Drawing.Font(tipografia, 12, FontStyle.Regular), Brushes.Black, 1, Y)
        Y += 15
        If tipo_impre = "NORMAL" Then
            e.Graphics.DrawString("C O M P R A", New Drawing.Font(tipografia, 12, FontStyle.Bold), Brushes.Black, 140, Y, sc)
            Y += 12
        End If
        If tipo_impre = "COPIA" Then
            e.Graphics.DrawString("C O P I A - C  O M P R A", New Drawing.Font(tipografia, 12, FontStyle.Bold), Brushes.Black, 140, Y, sc)
            Y += 12
        End If
        e.Graphics.DrawString("--------------------------------------------------------", New Drawing.Font(tipografia, 12, FontStyle.Regular), Brushes.Black, 1, Y)
        Y += 18

        e.Graphics.DrawString("N° Remisión: " & cboremision.Text, fuente_datos, Brushes.Black, 1, Y)
        Y += 13.5
        If cbofactura.Text <> "" Then
            e.Graphics.DrawString("N° Factura: " & cbofactura.Text, fuente_datos, Brushes.Black, 1, Y)
            Y += 13.5
        End If
        If cbopedido.Text <> "" Then
            e.Graphics.DrawString("N° Pedido: " & cbopedido.Text, fuente_datos, Brushes.Black, 1, Y)
            Y += 13.5
        End If
        Y += 4
        e.Graphics.DrawString("Fecha: " & Date.Now, New Drawing.Font(tipografia, 9, FontStyle.Regular), Brushes.Black, 1, Y)
        Y += 13.5
        e.Graphics.DrawString("Usuario: " & alias_compras, New Drawing.Font(tipografia, 9, FontStyle.Regular), Brushes.Black, 1, Y)
        Y += 17

        '[2]. Datos del proveedor
        cmd1 = cnn1.CreateCommand
        cmd1.CommandText =
            "select Compania,RFC,Correo from Proveedores where Compania='" & cboproveedor.Text & "'"
        rd1 = cmd1.ExecuteReader
        If rd1.HasRows Then
            If rd1.Read Then
                e.Graphics.DrawString("--------------------------------------------------------", New Drawing.Font(tipografia, 12, FontStyle.Regular), Brushes.Black, 1, Y)
                Y += 12
                e.Graphics.DrawString("PROVEEDOR", New Drawing.Font(tipografia, 10, FontStyle.Bold), Brushes.Black, 140, Y, sc)
                Y += 7.5
                e.Graphics.DrawString("--------------------------------------------------------", New Drawing.Font(tipografia, 12, FontStyle.Regular), Brushes.Black, 1, Y)
                Y += 15

                If rd1("Compania").ToString <> "" Then
                    e.Graphics.DrawString("Nombre: " & Mid(rd1("Compania").ToString, 1, 28), fuente_prods, Brushes.Black, 1, Y)
                    Y += 13.5
                    If Mid(rd1("Compania").ToString, 29, 100) <> "" Then
                        e.Graphics.DrawString(Mid(rd1("Compania").ToString, 29, 100), fuente_prods, Brushes.Black, 1, Y)
                        Y += 13.5
                    End If
                End If
                If rd1("RFC").ToString <> "" Then
                    e.Graphics.DrawString("RFC: " & rd1("RFC").ToString, fuente_prods, Brushes.Black, 1, Y)
                    Y += 13.5
                End If
                If rd1("Correo").ToString <> "" Then
                    e.Graphics.DrawString("Correo: " & Mid(rd1("Correo").ToString, 1, 28), fuente_prods, Brushes.Black, 1, Y)
                    Y += 13.5
                    If Mid(rd1("Correo").ToString, 29, 100) <> "" Then
                        e.Graphics.DrawString(Mid(rd1("Correo").ToString, 29, 100), fuente_prods, Brushes.Black, 1, Y)
                        Y += 13.5
                    End If
                End If
                Y += 8
                e.Graphics.DrawString("--------------------------------------------------------", New Drawing.Font(tipografia, 12, FontStyle.Regular), Brushes.Black, 1, Y)
                Y += 12
            End If
        End If
        rd1.Close()

        Dim ACUENTA As Double = 0
        Dim RESTA As Double = 0

        cmd1 = cnn1.CreateCommand
        cmd1.CommandText =
            "select ACuenta,Resta from Compras where NumRemision='" & cboremision.Text & "' and Proveedor='" & cboproveedor.Text & "'"
        rd1 = cmd1.ExecuteReader
        If rd1.HasRows Then
            If rd1.Read Then
                ACUENTA = rd1("ACuenta").ToString
                RESTA = rd1("Resta").ToString
            End If
        End If
        rd1.Close()
        cnn1.Close()

        e.Graphics.DrawString("PRODUCTO", New Drawing.Font(tipografia, 9, FontStyle.Bold), Brushes.Black, 140, Y, sc)
        Y += 15
        e.Graphics.DrawString("CANTIDAD", New Drawing.Font(tipografia, 9, FontStyle.Bold), Brushes.Black, 1, Y)
        e.Graphics.DrawString("PRECIO U.", New Drawing.Font(tipografia, 9, FontStyle.Bold), Brushes.Black, 184, Y, sf)
        e.Graphics.DrawString("TOTAL", New Drawing.Font(tipografia, 9, FontStyle.Bold), Brushes.Black, 240, Y)
        Y += 6
        e.Graphics.DrawString("--------------------------------------------------------", New Drawing.Font(tipografia, 12, FontStyle.Regular), Brushes.Black, 1, Y)
        Y += 18

        For parienton As Integer = 0 To grdCaptura.Rows.Count - 1
            Dim nombre As String = grdCaptura.Rows(parienton).Cells(0).Value.ToString()
            Dim canti As Double = grdCaptura.Rows(parienton).Cells(2).Value.ToString()
            Dim precio As Double = grdCaptura.Rows(parienton).Cells(3).Value.ToString()

            Dim caracteresPorLinea2 As Integer = 35
            Dim texto2 As String = nombre
            Dim inicio2 As Integer = 0
            Dim longitudTexto2 As Integer = texto2.Length

            While inicio2 < longitudTexto2
                Dim longitudBloque2 As Integer = Math.Min(caracteresPorLinea2, longitudTexto2 - inicio2)
                Dim bloque2 As String = texto2.Substring(inicio2, longitudBloque2)
                e.Graphics.DrawString(bloque2, New Font("Arial", 9, FontStyle.Regular), Brushes.Black, 55, Y)
                Y += 13
                inicio2 += caracteresPorLinea2
            End While

            e.Graphics.DrawString(canti, fuente_prods, Brushes.Black, 50, Y, sf)
            e.Graphics.DrawString("x", fuente_prods, Brushes.Black, 55, Y)
            e.Graphics.DrawString(simbolo & FormatNumber(precio, 1), fuente_prods, Brushes.Black, 180, Y, sf)
            e.Graphics.DrawString(simbolo & FormatNumber(CDec(canti) * CDec(precio), 1), fuente_prods, Brushes.Black, 285, Y, sf)
            Y += 21

        Next
        Y -= 3
        e.Graphics.DrawString("--------------------------------------------------------", New Drawing.Font(tipografia, 12, FontStyle.Regular), Brushes.Black, 1, Y)
        Y += 15
        e.Graphics.DrawString("TOTAL DE PRODUCTOS " & txtprods.Text, New Drawing.Font(tipografia, 8, FontStyle.Bold), Brushes.Black, 140, Y, sc)
        Y += 7
        e.Graphics.DrawString("--------------------------------------------------------", New Drawing.Font(tipografia, 12, FontStyle.Regular), Brushes.Black, 1, Y)
        Y += 18

        If CDbl(txtdesc1.Text) > 0 Then
            e.Graphics.DrawString("Descuento:", fuente_prods, Brushes.Black, 1, Y)
            e.Graphics.DrawString(simbolo & FormatNumber(txtdesc1.Text, 2), fuente_prods, Brushes.Black, 285, Y, sf)
            Y += 13.5
            e.Graphics.DrawString("Subtotal:", fuente_prods, Brushes.Black, 1, Y)
            e.Graphics.DrawString(simbolo & FormatNumber(txtsub2.Text, 2), fuente_prods, Brushes.Black, 285, Y, sf)
            Y += 13.5
        End If
        If cbofactura.Text <> "" And CDbl(txtiva.Text) > 0 Then
            e.Graphics.DrawString("IVA:", fuente_prods, Brushes.Black, 1, Y)
            e.Graphics.DrawString(simbolo & FormatNumber(txtiva.Text, 2), fuente_prods, Brushes.Black, 285, Y, sf)
            Y += 13.5
        End If
        If CDbl(txtdesc2.Text) > 0 Then
            e.Graphics.DrawString("Subtotal:", fuente_prods, Brushes.Black, 1, Y)
            e.Graphics.DrawString(simbolo & FormatNumber(txtTotalC.Text, 2), fuente_prods, Brushes.Black, 285, Y, sf)
            Y += 13.5
            e.Graphics.DrawString("Descuento:", fuente_prods, Brushes.Black, 1, Y)
            e.Graphics.DrawString(simbolo & FormatNumber(txtdesc2.Text, 2), fuente_prods, Brushes.Black, 285, Y, sf)
            Y += 13.5
        End If
        Y += 3
        e.Graphics.DrawString("Total a pagar:", fuente_prods, Brushes.Black, 1, Y)
        e.Graphics.DrawString(simbolo & FormatNumber(txtapagar.Text, 2), fuente_prods, Brushes.Black, 285, Y, sf)
        Y += 18
        e.Graphics.DrawString("--------------------------------------------------------", New Drawing.Font(tipografia, 12, FontStyle.Regular), Brushes.Black, 1, Y)
        Y += 18
        Dim MyAcuenta As Double = CDbl(txtanticipo.Text) + CDbl(txtefectivo.Text) + CDbl(txtpagos.Text)

        If MyAcuenta > 0 Then
            If CDbl(txtanticipo.Text) > 0 Then
                e.Graphics.DrawString("Anticipo:", fuente_prods, Brushes.Black, 1, Y)
                e.Graphics.DrawString(simbolo & FormatNumber(txtanticipo.Text, 2), fuente_prods, Brushes.Black, 285, Y, sf)
                Y += 13.5
            End If
            If CDbl(txtefectivo.Text) > 0 Then
                e.Graphics.DrawString("Efectivo:", fuente_prods, Brushes.Black, 1, Y)
                e.Graphics.DrawString(simbolo & FormatNumber(txtefectivo.Text, 2), fuente_prods, Brushes.Black, 285, Y, sf)
                Y += 13.5
            End If
            If CDbl(txtanticipo.Text) > 0 Then
                e.Graphics.DrawString("Pagos:", fuente_prods, Brushes.Black, 1, Y)
                e.Graphics.DrawString(simbolo & FormatNumber(txtpagos.Text, 2), fuente_prods, Brushes.Black, 285, Y, sf)
                Y += 13.5
            End If
            Y += 17
            e.Graphics.DrawString("--------------------------------------------------------", New Drawing.Font(tipografia, 12, FontStyle.Regular), Brushes.Black, 1, Y)
            Y += 18
        End If

        If tipo_impre = "NORMAL" Then
            If MyAcuenta > 0 Then
                e.Graphics.DrawString("A cuenta:", fuente_prods, Brushes.Black, 1, Y)
                e.Graphics.DrawString(simbolo & FormatNumber(MyAcuenta, 2), fuente_prods, Brushes.Black, 285, Y, sf)
                Y += 13.5
            End If
            If CDbl(txtresta.Text) > 0 Then
                e.Graphics.DrawString("Resta:", fuente_prods, Brushes.Black, 1, Y)
                e.Graphics.DrawString(simbolo & FormatNumber(txtresta.Text, 2), fuente_prods, Brushes.Black, 285, Y, sf)
                Y += 13.5
            End If
        End If

        If tipo_impre = "COPIA" Then
            If ACUENTA > 0 Then
                e.Graphics.DrawString("A cuenta:", fuente_prods, Brushes.Black, 1, Y)
                e.Graphics.DrawString(simbolo & FormatNumber(ACUENTA, 2), fuente_prods, Brushes.Black, 285, Y, sf)
                Y += 13.5
            End If
            If CDbl(RESTA) > 0 Then
                e.Graphics.DrawString("Resta:", fuente_prods, Brushes.Black, 1, Y)
                e.Graphics.DrawString(simbolo & FormatNumber(RESTA, 2), fuente_prods, Brushes.Black, 285, Y, sf)
                Y += 13.5
            End If
        End If
        e.HasMorePages = False
        Exit Sub
kakita:
        MsgBox("No se pudo generar el documento, a continuación se muestra la descripción del error." & vbNewLine & vbNewLine & Err.Number & " - " & Err.Description)
        cnn1.Close()
        Exit Sub
    End Sub

    Private Sub pTicket58_PrintPage(sender As Object, e As Printing.PrintPageEventArgs) Handles pTicket58.PrintPage
        'Fuentes prederminadas
        Dim tipografia As String = "Lucida Sans Typewriter"
        Dim fuente_datos As New Drawing.Font(tipografia, 10, FontStyle.Regular)
        Dim fuente_prods As New Drawing.Font(tipografia, 9, FontStyle.Regular)
        'Variables
        Dim sc As New StringFormat With {.Alignment = StringAlignment.Center}
        Dim sf As New StringFormat With {.Alignment = StringAlignment.Far}
        Dim pen As New Pen(Brushes.Black, 1)
        Dim Y As Double = 0
        Dim Logotipo As Drawing.Image = Nothing

        Dim Pie As String = ""
        Dim cnn1 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
        Dim rd1 As MySqlDataReader
        Dim cmd1 As MySqlCommand

        On Error GoTo kakita

        '[°]. Logotipo
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

        '[°]. Datos del negocio
        cnn1.Close() : cnn1.Open()

        cmd1 = cnn1.CreateCommand
        cmd1.CommandText =
            "select Pie2,Cab0,Cab1,Cab2,Cab3,Cab4,Cab5,Cab6 from Ticket"
        rd1 = cmd1.ExecuteReader
        If rd1.HasRows Then
            If rd1.Read Then
                Pie = rd1("Pie2").ToString
                'Razón social
                If rd1("Cab0").ToString() <> "" Then
                    e.Graphics.DrawString(rd1("Cab0").ToString, New Drawing.Font(tipografia, 10, FontStyle.Bold), Brushes.Black, 90, Y, sc)
                    Y += 12.5
                End If
                'RFC
                If rd1("Cab1").ToString() <> "" Then
                    e.Graphics.DrawString(rd1("Cab1").ToString, New Drawing.Font(tipografia, 10, FontStyle.Bold), Brushes.Black, 90, Y, sc)
                    Y += 12.5
                End If
                'Calle  N°.
                If rd1("Cab2").ToString() <> "" Then
                    e.Graphics.DrawString(rd1("Cab2").ToString, New Drawing.Font(tipografia, 9, FontStyle.Regular), Brushes.Gray, 90, Y, sc)
                    Y += 12
                End If
                'Colonia
                If rd1("Cab3").ToString() <> "" Then
                    e.Graphics.DrawString(rd1("Cab3").ToString, New Drawing.Font(tipografia, 9, FontStyle.Regular), Brushes.Gray, 90, Y, sc)
                    Y += 12
                End If
                'Delegación / Municipio - Entidad
                If rd1("Cab4").ToString() <> "" Then
                    e.Graphics.DrawString(rd1("Cab4").ToString, New Drawing.Font(tipografia, 9, FontStyle.Regular), Brushes.Gray, 90, Y, sc)
                    Y += 12
                End If
                'Teléfono
                If rd1("Cab5").ToString() <> "" Then
                    e.Graphics.DrawString(rd1("Cab5").ToString, New Drawing.Font(tipografia, 9, FontStyle.Regular), Brushes.Gray, 90, Y, sc)
                    Y += 12
                End If
                'Correo
                If rd1("Cab6").ToString() <> "" Then
                    e.Graphics.DrawString(rd1("Cab6").ToString, New Drawing.Font(tipografia, 9, FontStyle.Regular), Brushes.Gray, 90, Y, sc)
                    Y += 12
                End If
                Y += 4
            End If
        Else
            Y += 0
        End If
        rd1.Close()

        '[1]. Datos de la compra
        e.Graphics.DrawString("--------------------------------------------------------", New Drawing.Font(tipografia, 12, FontStyle.Regular), Brushes.Black, 1, Y)
        Y += 15
        If tipo_impre = "NORMAL" Then
            e.Graphics.DrawString("C O M P R A", New Drawing.Font(tipografia, 12, FontStyle.Bold), Brushes.Black, 90, Y, sc)
            Y += 12
        End If
        If tipo_impre = "COPIA" Then
            e.Graphics.DrawString("C O P I A - C  O M P R A", New Drawing.Font(tipografia, 12, FontStyle.Bold), Brushes.Black, 90, Y, sc)
            Y += 12
        End If
        e.Graphics.DrawString("--------------------------------------------------------", New Drawing.Font(tipografia, 12, FontStyle.Regular), Brushes.Black, 1, Y)
        Y += 18

        e.Graphics.DrawString("N° Remisión: " & cboremision.Text, fuente_datos, Brushes.Black, 1, Y)
        Y += 13.5
        If cbofactura.Text <> "" Then
            e.Graphics.DrawString("N° Factura: " & cbofactura.Text, fuente_datos, Brushes.Black, 1, Y)
            Y += 13.5
        End If
        If cbopedido.Text <> "" Then
            e.Graphics.DrawString("N° Pedido: " & cbopedido.Text, fuente_datos, Brushes.Black, 1, Y)
            Y += 13.5
        End If
        Y += 4
        e.Graphics.DrawString("Fecha: " & Date.Now, New Drawing.Font(tipografia, 9, FontStyle.Regular), Brushes.Black, 1, Y)
        Y += 13.5
        e.Graphics.DrawString("Usuario: " & alias_compras, New Drawing.Font(tipografia, 9, FontStyle.Regular), Brushes.Black, 1, Y)
        Y += 17

        '[2]. Datos del proveedor
        cmd1 = cnn1.CreateCommand
        cmd1.CommandText =
            "select Compania,RFC,Correo from Proveedores where Compania='" & cboproveedor.Text & "'"
        rd1 = cmd1.ExecuteReader
        If rd1.HasRows Then
            If rd1.Read Then
                e.Graphics.DrawString("--------------------------------------------------------", New Drawing.Font(tipografia, 12, FontStyle.Regular), Brushes.Black, 1, Y)
                Y += 12
                e.Graphics.DrawString("PROVEEDOR", New Drawing.Font(tipografia, 10, FontStyle.Bold), Brushes.Black, 90, Y, sc)
                Y += 7.5
                e.Graphics.DrawString("--------------------------------------------------------", New Drawing.Font(tipografia, 12, FontStyle.Regular), Brushes.Black, 1, Y)
                Y += 15

                If rd1("Compania").ToString <> "" Then
                    e.Graphics.DrawString("Nombre: " & Mid(rd1("Compania").ToString, 1, 28), fuente_prods, Brushes.Black, 1, Y)
                    Y += 13.5
                    If Mid(rd1("Compania").ToString, 29, 100) <> "" Then
                        e.Graphics.DrawString(Mid(rd1("Compania").ToString, 29, 100), fuente_prods, Brushes.Black, 1, Y)
                        Y += 13.5
                    End If
                End If
                If rd1("RFC").ToString <> "" Then
                    e.Graphics.DrawString("RFC: " & rd1("RFC").ToString, fuente_prods, Brushes.Black, 1, Y)
                    Y += 13.5
                End If
                If rd1("Correo").ToString <> "" Then
                    e.Graphics.DrawString("Correo: " & Mid(rd1("Correo").ToString, 1, 28), fuente_prods, Brushes.Black, 1, Y)
                    Y += 13.5
                    If Mid(rd1("Correo").ToString, 29, 100) <> "" Then
                        e.Graphics.DrawString(Mid(rd1("Correo").ToString, 29, 100), fuente_prods, Brushes.Black, 1, Y)
                        Y += 13.5
                    End If
                End If
                Y += 8
                e.Graphics.DrawString("--------------------------------------------------------", New Drawing.Font(tipografia, 12, FontStyle.Regular), Brushes.Black, 1, Y)
                Y += 12
            End If
        End If
        rd1.Close()

        Dim ACUENTA As Double = 0
        Dim RESTA As Double = 0

        cmd1 = cnn1.CreateCommand
        cmd1.CommandText =
            "select ACuenta,Resta from Compras where NumRemision='" & cboremision.Text & "' and Proveedor='" & cboproveedor.Text & "'"
        rd1 = cmd1.ExecuteReader
        If rd1.HasRows Then
            If rd1.Read Then
                ACUENTA = rd1("ACuenta").ToString
                RESTA = rd1("Resta").ToString
            End If
        End If
        rd1.Close()
        cnn1.Close()

        e.Graphics.DrawString("PRODUCTO", New Drawing.Font(tipografia, 9, FontStyle.Bold), Brushes.Black, 90, Y, sc)
        Y += 15
        e.Graphics.DrawString("CANTIDAD", New Drawing.Font(tipografia, 9, FontStyle.Bold), Brushes.Black, 1, Y)
        e.Graphics.DrawString("PRECIO U.", New Drawing.Font(tipografia, 9, FontStyle.Bold), Brushes.Black, 90, Y, sf)
        e.Graphics.DrawString("TOTAL", New Drawing.Font(tipografia, 9, FontStyle.Bold), Brushes.Black, 180, Y)
        Y += 6
        e.Graphics.DrawString("--------------------------------------------------------", New Drawing.Font(tipografia, 12, FontStyle.Regular), Brushes.Black, 1, Y)
        Y += 18

        For parienton As Integer = 0 To grdCaptura.Rows.Count - 1
            Dim nombre As String = grdCaptura.Rows(parienton).Cells(0).Value.ToString()
            Dim canti As Double = grdCaptura.Rows(parienton).Cells(2).Value.ToString()
            Dim precio As Double = grdCaptura.Rows(parienton).Cells(3).Value.ToString()

            Dim caracteresPorLinea2 As Integer = 35
            Dim texto2 As String = nombre
            Dim inicio2 As Integer = 0
            Dim longitudTexto2 As Integer = texto2.Length

            While inicio2 < longitudTexto2
                Dim longitudBloque2 As Integer = Math.Min(caracteresPorLinea2, longitudTexto2 - inicio2)
                Dim bloque2 As String = texto2.Substring(inicio2, longitudBloque2)
                e.Graphics.DrawString(bloque2, New Font("Arial", 9, FontStyle.Regular), Brushes.Black, 1, Y)
                Y += 13
                inicio2 += caracteresPorLinea2
            End While

            e.Graphics.DrawString(canti, fuente_prods, Brushes.Black, 30, Y, sf)
            e.Graphics.DrawString("x", fuente_prods, Brushes.Black, 35, Y)
            e.Graphics.DrawString(simbolo & FormatNumber(precio, 1), fuente_prods, Brushes.Black, 90, Y, sf)
            e.Graphics.DrawString(simbolo & FormatNumber(CDec(canti) * CDec(precio), 1), fuente_prods, Brushes.Black, 180, Y, sf)
            Y += 21

        Next
        Y -= 3
        e.Graphics.DrawString("--------------------------------------------------------", New Drawing.Font(tipografia, 12, FontStyle.Regular), Brushes.Black, 1, Y)
        Y += 15
        e.Graphics.DrawString("TOTAL DE PRODUCTOS " & txtprods.Text, New Drawing.Font(tipografia, 8, FontStyle.Bold), Brushes.Black, 90, Y, sc)
        Y += 7
        e.Graphics.DrawString("--------------------------------------------------------", New Drawing.Font(tipografia, 12, FontStyle.Regular), Brushes.Black, 1, Y)
        Y += 18

        If CDbl(txtdesc1.Text) > 0 Then
            e.Graphics.DrawString("Descuento:", fuente_prods, Brushes.Black, 1, Y)
            e.Graphics.DrawString(simbolo & FormatNumber(txtdesc1.Text, 2), fuente_prods, Brushes.Black, 180, Y, sf)
            Y += 13.5
            e.Graphics.DrawString("Subtotal:", fuente_prods, Brushes.Black, 1, Y)
            e.Graphics.DrawString(simbolo & FormatNumber(txtsub2.Text, 2), fuente_prods, Brushes.Black, 180, Y, sf)
            Y += 13.5
        End If
        If cbofactura.Text <> "" And CDbl(txtiva.Text) > 0 Then
            e.Graphics.DrawString("IVA:", fuente_prods, Brushes.Black, 1, Y)
            e.Graphics.DrawString(simbolo & FormatNumber(txtiva.Text, 2), fuente_prods, Brushes.Black, 180, Y, sf)
            Y += 13.5
        End If
        If CDbl(txtdesc2.Text) > 0 Then
            e.Graphics.DrawString("Subtotal:", fuente_prods, Brushes.Black, 1, Y)
            e.Graphics.DrawString(simbolo & FormatNumber(txtTotalC.Text, 2), fuente_prods, Brushes.Black, 180, Y, sf)
            Y += 13.5
            e.Graphics.DrawString("Descuento:", fuente_prods, Brushes.Black, 1, Y)
            e.Graphics.DrawString(simbolo & FormatNumber(txtdesc2.Text, 2), fuente_prods, Brushes.Black, 180, Y, sf)
            Y += 13.5
        End If
        Y += 3
        e.Graphics.DrawString("Total a pagar:", fuente_prods, Brushes.Black, 1, Y)
        e.Graphics.DrawString(simbolo & FormatNumber(txtapagar.Text, 2), fuente_prods, Brushes.Black, 180, Y, sf)
        Y += 18
        e.Graphics.DrawString("--------------------------------------------------------", New Drawing.Font(tipografia, 12, FontStyle.Regular), Brushes.Black, 1, Y)
        Y += 18
        Dim MyAcuenta As Double = CDbl(txtanticipo.Text) + CDbl(txtefectivo.Text) + CDbl(txtpagos.Text)

        If MyAcuenta > 0 Then
            If CDbl(txtanticipo.Text) > 0 Then
                e.Graphics.DrawString("Anticipo:", fuente_prods, Brushes.Black, 1, Y)
                e.Graphics.DrawString(simbolo & FormatNumber(txtanticipo.Text, 2), fuente_prods, Brushes.Black, 180, Y, sf)
                Y += 13.5
            End If
            If CDbl(txtefectivo.Text) > 0 Then
                e.Graphics.DrawString("Efectivo:", fuente_prods, Brushes.Black, 1, Y)
                e.Graphics.DrawString(simbolo & FormatNumber(txtefectivo.Text, 2), fuente_prods, Brushes.Black, 180, Y, sf)
                Y += 13.5
            End If
            If CDbl(txtanticipo.Text) > 0 Then
                e.Graphics.DrawString("Pagos:", fuente_prods, Brushes.Black, 1, Y)
                e.Graphics.DrawString(simbolo & FormatNumber(txtpagos.Text, 2), fuente_prods, Brushes.Black, 180, Y, sf)
                Y += 13.5
            End If
            Y += 17
            e.Graphics.DrawString("--------------------------------------------------------", New Drawing.Font(tipografia, 12, FontStyle.Regular), Brushes.Black, 1, Y)
            Y += 18
        End If

        If tipo_impre = "NORMAL" Then
            If MyAcuenta > 0 Then
                e.Graphics.DrawString("A cuenta:", fuente_prods, Brushes.Black, 1, Y)
                e.Graphics.DrawString(simbolo & FormatNumber(MyAcuenta, 2), fuente_prods, Brushes.Black, 180, Y, sf)
                Y += 13.5
            End If
            If CDbl(txtresta.Text) > 0 Then
                e.Graphics.DrawString("Resta:", fuente_prods, Brushes.Black, 1, Y)
                e.Graphics.DrawString(simbolo & FormatNumber(txtresta.Text, 2), fuente_prods, Brushes.Black, 180, Y, sf)
                Y += 13.5
            End If
        End If

        If tipo_impre = "COPIA" Then
            If ACUENTA > 0 Then
                e.Graphics.DrawString("A cuenta:", fuente_prods, Brushes.Black, 1, Y)
                e.Graphics.DrawString(simbolo & FormatNumber(ACUENTA, 2), fuente_prods, Brushes.Black, 180, Y, sf)
                Y += 13.5
            End If
            If CDbl(RESTA) > 0 Then
                e.Graphics.DrawString("Resta:", fuente_prods, Brushes.Black, 1, Y)
                e.Graphics.DrawString(simbolo & FormatNumber(RESTA, 2), fuente_prods, Brushes.Black, 180, Y, sf)
                Y += 13.5
            End If
        End If
        e.HasMorePages = False
        Exit Sub
kakita:
        MsgBox("No se pudo generar el documento, a continuación se muestra la descripción del error." & vbNewLine & vbNewLine & Err.Number & " - " & Err.Description)
        cnn1.Close()
        Exit Sub
    End Sub

    Private Sub btnCopia_Click(sender As Object, e As EventArgs) Handles btnCopia.Click

    End Sub
End Class