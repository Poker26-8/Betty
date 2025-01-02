
Imports System.IO
Imports MySql.Data.MySqlClient
Public Class frmProduccion

    Dim nLogo As String = ""
    Dim tLogo As String = ""
    Private Sub cbocodigo_DropDown(sender As System.Object, e As System.EventArgs) Handles cbocodigo.DropDown
        cbocodigo.Items.Clear()
        Dim cnn1 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
        Dim rd1 As MySqlDataReader
        Dim cmd1 As MySqlCommand
        Try
            cnn1.Close()
            cnn1.Open()

            cmd1 = cnn1.CreateCommand
            cmd1.CommandText =
                "select distinct CodigoP from MiProd"
            rd1 = cmd1.ExecuteReader
            Do While rd1.Read
                If rd1.HasRows Then cbocodigo.Items.Add(
                    rd1(0).ToString
                    )
            Loop
            rd1.Close()
            cnn1.Close()
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
            cnn1.Close()
        End Try
    End Sub

    Private Sub cbocodigo_SelectedValueChanged(sender As Object, e As System.EventArgs) Handles cbocodigo.SelectedValueChanged
        grdcaptura.Rows.Clear()
        txtcostod.Text = "0.00"
        Dim codigo As String = ""
        Dim nombre As String = ""
        Dim unidad As String = ""
        Dim cantidad As Double = 0
        Dim precio_com As Double = 0
        Dim existencia As Double = 0
        Dim multiplo As Double = 0

        Dim MyTotal As Double = 0

        Dim cnn1 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
        Dim cnn2 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
        Dim cnn3 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
        Dim rd1, rd2, rd3 As MySqlDataReader
        Dim cmd1, cmd2, cmd3 As MySqlCommand

        Try
            cnn1.Close()
            cnn2.Close()
            cnn3.Close()
            cnn1.Open()
            cnn2.Open()
            cnn3.Open()

            cmd1 = cnn1.CreateCommand
            cmd1.CommandText =
                "select Codigo,DescripP,UVentaP,UVenta,Cantidad,Grupo from MiProd where CodigoP='" & cbocodigo.Text & "'"
            rd1 = cmd1.ExecuteReader
            If rd1.HasRows Then
                If rd1.Read Then
                    cbonombre.Text = rd1("DescripP").ToString
                    txtunidad.Text = rd1("UVentaP").ToString
                End If
            End If
            rd1.Close()

            rd1 = cmd1.ExecuteReader
            Do While rd1.Read
                cmd3 = cnn3.CreateCommand
                cmd3.CommandText =
                    "select PrecioCompra from Productos where Codigo='" & rd1("Codigo").ToString & "'"
                rd3 = cmd3.ExecuteReader
                If rd3.HasRows Then
                    If rd3.Read Then
                        precio_com = rd3("PrecioCompra").ToString
                    End If
                End If
                rd3.Close()

                cmd2 = cnn2.CreateCommand
                cmd2.CommandText =
                    "select Existencia,Multiplo,PrecioCompra from Productos where Codigo='" & Strings.Left(rd1("Codigo").ToString, 6) & "'"
                rd2 = cmd2.ExecuteReader
                If rd2.HasRows Then
                    If rd2.Read Then
                        codigo = rd1("Codigo").ToString
                        nombre = rd1("Descrip").ToString
                        unidad = rd1("UVenta").ToString
                        cantidad = rd1("Cantidad").ToString
                        existencia = rd2("Existencia").ToString
                        multiplo = rd2("Multiplo").ToString

                        MyTotal = CDec(rd2("PrecioCompra").ToString) * cantidad

                        If rd1("Grupo").ToString <> "SERVICIOS" Then
                            grdcaptura.Rows.Add(
                                codigo,
                                nombre,
                                unidad,
                                FormatNumber(cantidad, 6),
                                FormatNumber(precio_com, 4),
                                FormatNumber(cantidad * precio_com, 2),
                                FormatNumber(existencia / multiplo, 2),
                                FormatNumber((existencia / multiplo) / cantidad, 2)
                                )
                        Else
                            grdcaptura.Rows.Add(
                                codigo,
                                nombre,
                                unidad,
                                FormatNumber(cantidad, 6),
                                FormatNumber(precio_com, 4),
                                FormatNumber(cantidad * precio_com, 2),
                                "",
                                ""
                                )
                        End If
                    End If
                End If
                rd2.Close()
                txtcostod.Text = CDec(txtcostod.Text) + MyTotal
            Loop
            rd1.Close()
            cnn1.Close()
            cnn2.Close()
            cnn3.Close()
            txtcostod.Text = FormatNumber(txtcostod.Text, 2)
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
            cnn1.Close()
            cnn2.Close()
            cnn3.Close()
        End Try
    End Sub

    Private Sub cbocodigo_KeyPress(sender As System.Object, e As System.Windows.Forms.KeyPressEventArgs) Handles cbocodigo.KeyPress
        e.KeyChar = UCase(e.KeyChar)
        If AscW(e.KeyChar) = Keys.Enter Then
            Call cbocodigo_SelectedValueChanged(cbocodigo, New EventArgs())
            txtcantidad.Focus().Equals(True)
        End If
    End Sub

    Private Sub txtcantidad_Click(sender As System.Object, e As System.EventArgs) Handles txtcantidad.Click
        txtcantidad.SelectionStart = 0
        txtcantidad.SelectionLength = Len(txtcantidad.Text)
    End Sub

    Private Sub txtcantidad_GotFocus(sender As Object, e As System.EventArgs) Handles txtcantidad.GotFocus
        txtcantidad.SelectionStart = 0
        txtcantidad.SelectionLength = Len(txtcantidad.Text)
    End Sub

    Private Sub txtcantidad_KeyPress(sender As Object, e As System.Windows.Forms.KeyPressEventArgs) Handles txtcantidad.KeyPress
        Dim MyTotal As Double = 0
        If AscW(e.KeyChar) = Keys.Enter Then
            If txtcantidad.Text = "" Then Exit Sub
            If txtcantidad.Text = "." Then Exit Sub
            If Not IsNumeric(txtcantidad.Text) Then Exit Sub
            If CDec(txtcantidad.Text) = 0 Then Exit Sub
            Call cbonombre_SelectedValueChanged(cbonombre, New EventArgs())
            txtcostod.Text = "0.00"
            For d As Integer = 0 To grdcaptura.Rows.Count - 1
                Dim total As Double = 0
                Dim precio As Double = grdcaptura.Rows(d).Cells(4).Value.ToString
                Dim cantidad As String = grdcaptura.Rows(d).Cells(3).Value.ToString

                grdcaptura.Rows(d).Cells(3).Value = CDec(cantidad) * CDec(txtcantidad.Text)
                cantidad = grdcaptura.Rows(d).Cells(3).Value.ToString
                total = CDec(cantidad) * precio
                grdcaptura.Rows(d).Cells(5).Value = FormatNumber(total, 2)
                MyTotal += total
            Next
            txtcostod.Text = FormatNumber(MyTotal, 2)
            btnguardar.Focus().Equals(True)
        End If
    End Sub

    Private Sub cbonombre_DropDown(sender As System.Object, e As System.EventArgs) Handles cbonombre.DropDown
        cbonombre.Items.Clear()

        Dim cnn1 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
        Dim rd1 As MySqlDataReader
        Dim cmd1 As MySqlCommand

        Try
            cnn1.Close()
            cnn1.Open()

            cmd1 = cnn1.CreateCommand
            cmd1.CommandText =
                "select distinct DescripP from MiProd"
            rd1 = cmd1.ExecuteReader
            Do While rd1.Read
                If rd1.HasRows Then cbonombre.Items.Add(
                    rd1(0).ToString
                    )
            Loop
            rd1.Close()
            cnn1.Close()
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
            cnn1.Close()
        End Try
    End Sub

    Private Sub cbonombre_KeyPress(sender As Object, e As System.Windows.Forms.KeyPressEventArgs) Handles cbonombre.KeyPress
        e.KeyChar = UCase(e.KeyChar)
        If AscW(e.KeyChar) = Keys.Enter Then
            Call cbonombre_SelectedValueChanged(cbonombre, New EventArgs())
            txtcantidad.Focus().Equals(True)
        End If
    End Sub

    Private Sub cbonombre_SelectedValueChanged(sender As Object, e As System.EventArgs) Handles cbonombre.SelectedValueChanged
        grdcaptura.Rows.Clear()
        txtcostod.Text = "0.00"
        Dim codigo As String = ""
        Dim nombre As String = ""
        Dim unidad As String = ""
        Dim cantidad As String = ""
        Dim precio_com As Double = 0
        Dim existencia As Double = 0
        Dim multiplo As Double = 0

        Dim MyTotal As Double = 0

        Dim cnn1 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
        Dim cnn2 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
        Dim cnn3 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
        Dim rd1, rd2, rd3 As MySqlDataReader
        Dim cmd1, cmd2, cmd3 As MySqlCommand

        Try
            cnn1.Close()
            cnn2.Close()
            cnn3.Close()
            cnn1.Open()
            cnn2.Open()
            cnn3.Open()

            cmd1 = cnn1.CreateCommand
            cmd1.CommandText =
                "select Codigo,CodigoP,UVentaP,Descrip,UVenta,Cantidad,Grupo from MiProd where DescripP='" & cbonombre.Text & "'"
            rd1 = cmd1.ExecuteReader
            If rd1.HasRows Then
                If rd1.Read Then
                    cbocodigo.Text = rd1("CodigoP").ToString
                    txtunidad.Text = rd1("UVentaP").ToString
                End If
            End If
            rd1.Close()

            rd1 = cmd1.ExecuteReader
            Do While rd1.Read
                cmd3 = cnn3.CreateCommand
                cmd3.CommandText =
                    "select PrecioCompra from Productos where Codigo='" & rd1("Codigo").ToString & "'"
                rd3 = cmd3.ExecuteReader
                If rd3.HasRows Then
                    If rd3.Read Then
                        precio_com = rd3("PrecioCompra").ToString
                    End If
                End If
                rd3.Close()

                cmd2 = cnn2.CreateCommand
                cmd2.CommandText =
                    "select Existencia,Multiplo,PrecioCompra from Productos where Codigo='" & Strings.Left(rd1("Codigo").ToString, 6) & "'"
                rd2 = cmd2.ExecuteReader
                If rd2.HasRows Then
                    If rd2.Read Then
                        codigo = rd1("Codigo").ToString
                        nombre = rd1("Descrip").ToString
                        unidad = rd1("UVenta").ToString
                        cantidad = rd1("Cantidad").ToString
                        existencia = rd2("Existencia").ToString
                        multiplo = rd2("Multiplo").ToString

                        MyTotal = CDec(rd2("PrecioCompra").ToString) * CDec(cantidad)

                        If rd1("Grupo").ToString <> "SERVICIOS" Then
                            grdcaptura.Rows.Add(
                                codigo,
                                nombre,
                                unidad,
                                cantidad,
                                FormatNumber(precio_com, 4),
                                FormatNumber(CDec(cantidad) * precio_com, 2),
                                FormatNumber(existencia / multiplo, 2),
                                FormatNumber((existencia / multiplo) / CDec(cantidad), 2)
                                )
                        Else
                            grdcaptura.Rows.Add(
                                codigo,
                                nombre,
                                unidad,
                                FormatNumber(cantidad, 6),
                                FormatNumber(precio_com, 4),
                                FormatNumber(cantidad * precio_com, 2),
                                "",
                                ""
                                )
                        End If
                    End If
                End If
                rd2.Close()
                txtcostod.Text = CDec(txtcostod.Text) + MyTotal
            Loop
            rd1.Close()
            cnn1.Close()
            cnn2.Close()
            cnn3.Close()
            txtcostod.Text = FormatNumber(txtcostod.Text, 2)
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
            cnn1.Close()
            cnn2.Close()
            cnn3.Close()
        End Try
    End Sub

    Private Sub btncostod_Click(sender As System.Object, e As System.EventArgs) Handles btncostod.Click
        If cbocodigo.Text = "" Or cbonombre.Text = "" Then MsgBox("Selecciona un producto para guadar su costo de produccción.", vbInformation + vbOKOnly, "Delsscom Control Negocios Pro") : cbonombre.Focus().Equals(True) : Exit Sub
        If grdcaptura.Rows.Count = 0 Then MsgBox("Procedimiento erróneo.", vbInformation + vbOKOnly, "Delsscom Control Negocios Pro") : cbonombre.Focus().Equals(True) : Exit Sub
        Dim precio As Double = txtcostod.Text
        If MsgBox("¿Deseas guardar éste costo de producción para el producto?", vbInformation + vbOKCancel, "Delsscom Control Negocios Pro") = vbOK Then

            Dim cnn1 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
            Dim rd1 As MySqlDataReader
            Dim cmd1 As MySqlCommand

            Try
                cnn1.Close()
                cnn1.Open()

                cmd1 = cnn1.CreateCommand
                cmd1.CommandText =
                    "update Productos set Cargado=0, PrecioCompra=" & precio & " where Codigo='" & cbocodigo.Text & "'"
                If cmd1.ExecuteNonQuery Then
                    MsgBox("Costo directo de producción actualizado.", vbInformation + vbOKOnly, "Delsscom Control Negocios Pro")
                End If
                cnn1.Close()
            Catch ex As Exception
                MessageBox.Show(ex.ToString)
                cnn1.Close()
            End Try
        End If
    End Sub

    Private Sub btnguardar_Click(sender As System.Object, e As System.EventArgs) Handles btnguardar.Click
        If txtcantidad.Text = "" Or txtcantidad.Text = "." Or Not IsNumeric(txtcantidad.Text) Or CDec(txtcantidad.Text) = 0 Then
            MsgBox("La cantidad debe de ser como mínimo '1' para continuar.", vbInformation + vbOKOnly, "Delsscom Control Negocios Pro") : txtcantidad.Focus().Equals(True) : Exit Sub
        End If

        If cboEmpleado.Text = "" Then MsgBox("Debe seleccionar el empleado que realizara el trabajo.") : cboEmpleado.Focus.Equals(True) : Exit Sub

        Dim cnn1 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
        Dim rd1 As MySqlDataReader
        Dim cmd1 As MySqlCommand

        Try
            cnn1.Close()
            cnn1.Open()

            If txtcontraseña.Text <> "" Then
                cmd1 = cnn1.CreateCommand
                cmd1.CommandText =
                    "select Alias from Usuarios where Clave='" & txtcontraseña.Text & "'"
                rd1 = cmd1.ExecuteReader
                If rd1.HasRows Then
                    If rd1.Read Then
                        'lblusuario.Text = rd1("Alias").ToString
                    End If
                Else
                    MsgBox("La contraseña es incorrecta.", vbInformation + vbOKOnly, "Delsscom Control Negocios Pro")
                    rd1.Close()
                    cnn1.Close()
                    txtcontraseña.SelectionStart = 0
                    txtcontraseña.SelectionLength = Len(txtcontraseña.Text)
                    txtcontraseña.Focus().Equals(True)
                    Exit Sub
                End If
                rd1.Close()
            Else
                MsgBox("Escribe tu contraseña para continuar.", vbInformation + vbOKOnly, "Delsscom Control Negocios Pro")
                cnn1.Close()
                txtcontraseña.Focus().Equals(True)
                Exit Sub
            End If

            Dim existencia As Double = 0
            Dim cantidad As Double = txtcantidad.Text
            Dim multiplo As Double = 0
            Dim nueva_exis As Double = 0
            Dim precio_p As Double = 0

            If MsgBox("¿Deseas generar la producción de este producto?", vbInformation + vbOKCancel, "Delsscom Control Negocios Pro") = vbOK Then

                cmd1 = cnn1.CreateCommand
                cmd1.CommandText =
                    "select Existencia,Multiplo from Productos where Codigo='" & cbocodigo.Text & "'"
                rd1 = cmd1.ExecuteReader
                If rd1.HasRows Then
                    If rd1.Read Then
                        existencia = rd1("Existencia").ToString
                        multiplo = rd1("Multiplo").ToString
                    End If

                    cantidad = CDec(txtcantidad.Text) * multiplo
                    nueva_exis = existencia + cantidad
                    precio_p = cantidad * CDbl(txtcostod.Text)
                End If
                rd1.Close()

                cmd1 = cnn1.CreateCommand
                cmd1.CommandText =
                    "update Productos set Existencia=" & nueva_exis & ", PrecioCompra=" & precio_p & " where Codigo='" & cbocodigo.Text & "'"
                cmd1.ExecuteNonQuery()

                cmd1 = cnn1.CreateCommand
                cmd1.CommandText =
                    "insert into Cardex(Codigo,Nombre,Movimiento,Inicial,Cantidad,Final,Precio,Fecha,Usuario,Folio,Tipo,Cedula,Receta,Medico,Domicilio) values('" & cbocodigo.Text & "','" & cbonombre.Text & "','Producción'," & existencia & "," & cantidad & "," & nueva_exis & "," & precio_p & ",'" & Format(Date.Now, "yyyy- MM-dd HH:mm:ss") & "','','','','','','','')"
                cmd1.ExecuteNonQuery()

                For q As Integer = 0 To grdcaptura.Rows.Count - 1
                    Dim codi As String = grdcaptura.Rows(q).Cells(0).Value.ToString
                    Dim nomb As String = grdcaptura.Rows(q).Cells(1).Value.ToString
                    Dim unid As String = grdcaptura.Rows(q).Cells(2).Value.ToString
                    Dim cant As Double = grdcaptura.Rows(q).Cells(3).Value.ToString
                    Dim prec As Double = grdcaptura.Rows(q).Cells(4).Value.ToString

                    Dim exis As Double = 0
                    Dim nueva_ex As Double = 0
                    Dim multi As Double = 0
                    cmd1 = cnn1.CreateCommand
                    cmd1.CommandText =
                        "select Existencia,Multiplo from Productos where Codigo='" & codi & "'"
                    rd1 = cmd1.ExecuteReader
                    If rd1.HasRows Then
                        If rd1.Read Then
                            exis = rd1("Existencia").ToString
                            multi = rd1("Multiplo").ToString
                            nueva_ex = exis - (cant * multi)
                        End If
                    End If
                    rd1.Close()

                    cmd1 = cnn1.CreateCommand
                    cmd1.CommandText =
                        "update Productos set Cargado=0, CargadoInv=0, Existencia=" & nueva_ex & " where Codigo='" & codi & "'"
                    cmd1.ExecuteNonQuery()

                    cmd1 = cnn1.CreateCommand
                    cmd1.CommandText =
                        "insert into Cardex(Codigo,Nombre,Movimiento,Inicial,Cantidad,Final,Precio,Fecha,Usuario,Folio,Tipo,Cedula,Receta,Medico,Domicilio) values('" & codi & "','" & nomb & "','Descuento por producción'," & exis & "," & cant & "," & nueva_ex & "," & prec & ",'" & Format(Date.Now, "yyyy- MM-dd HH:mm:ss") & "','','','','','','','')"
                    cmd1.ExecuteNonQuery()
                Next


                cmd1 = cnn1.CreateCommand
                cmd1.CommandText = "INSERT INTO produccion(Codigo,Descripcion,Unidad,Cantidad,Florista,Usuario,FElaboracion,Fecha) VALUES('" & cbocodigo.Text & "','" & cbonombre.Text & "','" & txtunidad.Text & "'," & txtcantidad.Text & ",'" & cboEmpleado.Text & "','" & lblUsuario.Text & "','" & Format(Date.Now, "yyyy-MM-dd HH:mm:ss") & "','" & Format(Date.Now, "yyyy-MM-dd") & "')"
                cmd1.ExecuteNonQuery()

                Dim tamimpresora As Integer = TamImpre()
                Dim impresora As String = ImpresoraImprimir()

                If impresora = "" Then MsgBox("No se encontró una impresora.", vbInformation + vbOKOnly, titulocentral) : Exit Sub

                If tamimpresora = 80 Then

                    PDato80.DefaultPageSettings.PrinterSettings.PrinterName = impresora
                    Dim ps As New System.Drawing.Printing.PaperSize("Custom", 297, 3000)
                    PDato80.DefaultPageSettings.PaperSize = ps
                    If PDato80.DefaultPageSettings.PrinterSettings.PrinterName = impresora Then
                        PDato80.Print()
                    Else
                        MsgBox("La impresora no esta configurada", vbInformation + vbOKOnly, titulocentral)
                    End If

                End If

                If tamimpresora = 58 Then

                    PDato58.DefaultPageSettings.PrinterSettings.PrinterName = impresora
                    If PDato58.DefaultPageSettings.PrinterSettings.PrinterName = impresora Then
                        PDato58.Print()
                    Else
                        MsgBox("La impresora no esta configurada", vbInformation + vbOKOnly, titulocentral)
                    End If

                End If

                cboEmpleado.Text = ""
                cbocodigo.Items.Clear()
                cbocodigo.Text = ""
                cbonombre.Items.Clear()
                cbonombre.Text = ""
                txtunidad.Text = ""
                txtcantidad.Text = ""
                txtcostod.Text = "0.00"
                grdcaptura.Rows.Clear()

            End If
            cnn1.Close()
        Catch ex As Exception
            MessageBox.Show(ex.ToString) : cnn1.Close()
        End Try
    End Sub

    Private Sub txtcontraseña_KeyPress(sender As System.Object, e As System.Windows.Forms.KeyPressEventArgs) Handles txtcontraseña.KeyPress
        If AscW(e.KeyChar) = Keys.Enter Then

            Dim cnn1 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
            Dim rd1 As MySqlDataReader
            Dim cmd1 As MySqlCommand

            Try
                cnn1.Close()
                cnn1.Open()

                cmd1 = cnn1.CreateCommand
                cmd1.CommandText =
                    "select Alias from Usuarios where Clave='" & txtcontraseña.Text & "'"
                rd1 = cmd1.ExecuteReader
                If rd1.HasRows Then
                    If rd1.Read Then
                        lblUsuario.Text = rd1("Alias").ToString
                        btnguardar.Focus().Equals(True)
                    End If
                Else
                    MsgBox("La contraseña es incorrecta.", vbInformation + vbOKOnly, "Delsscom Control Negocios Pro")
                    rd1.Close()
                    cnn1.Close()
                    lblUsuario.Text = ""
                    txtcontraseña.SelectionStart = 0
                    txtcontraseña.SelectionLength = Len(txtcontraseña.Text)
                    txtcontraseña.Focus().Equals(True)
                    Exit Sub
                End If
                rd1.Close()
                cnn1.Close()
            Catch ex As Exception
                MessageBox.Show(ex.ToString) : cnn1.Close()
            End Try
        End If
    End Sub

    Private Sub btnnuevo_Click(sender As System.Object, e As System.EventArgs) Handles btnnuevo.Click
        cbocodigo.Items.Clear()
        cbocodigo.Text = ""
        cbonombre.Items.Clear()
        cbonombre.Text = ""
        txtunidad.Text = ""
        txtcantidad.Text = ""
        txtcostod.Text = "0.00"
        txtcontraseña.Text = ""
        grdcaptura.Rows.Clear()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Close()
    End Sub

    Private Sub frmProduccion_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        nLogo = DatosRecarga("LogoG")
        tLogo = DatosRecarga("TipoLogo")
    End Sub

    Private Sub cboEmpleado_DropDown(sender As Object, e As EventArgs) Handles cboEmpleado.DropDown
        Dim cnn5 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
        Dim rd5 As MySqlDataReader
        Dim cmd5 As MySqlCommand
        Try
            cboEmpleado.Items.Clear()

            cnn5.Clone() : cnn5.Open()
            cmd5 = cnn5.CreateCommand
            cmd5.CommandText = "SELECT DISTINCT Nombre FROM usuarios WHERE Nombre<>'' ORDER BY Nombre"
            rd5 = cmd5.ExecuteReader
            Do While rd5.Read
                If rd5.HasRows Then
                    cboEmpleado.Items.Add(rd5(0).ToString)
                End If
            Loop
            rd5.Close()
            cnn5.Close()

        Catch ex As Exception
            MessageBox.Show(ex.ToString)
            cnn5.Close()
        End Try
    End Sub

    Private Sub cboEmpleado_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cboEmpleado.KeyPress
        e.KeyChar = UCase(e.KeyChar)
        If AscW(e.KeyChar) = Keys.Enter Then
            btnguardar.Focus.Equals(True)
        End If
    End Sub

    Private Sub PDato80_PrintPage(sender As Object, e As Printing.PrintPageEventArgs) Handles PDato80.PrintPage
        'Fuentes prederminadas
        Dim tipografia As String = "Lucida Sans Typewriter"
        Dim fuente_datos As New Drawing.Font(tipografia, 10, FontStyle.Regular)
        Dim fuente_prods As New Drawing.Font(tipografia, 11, FontStyle.Regular)
        Dim fuente_fecha As New Drawing.Font(tipografia, 8, FontStyle.Regular)
        'Variables
        Dim sc As New StringFormat With {.Alignment = StringAlignment.Center}
        Dim sf As New StringFormat With {.Alignment = StringAlignment.Far}
        Dim pen As New Pen(Brushes.Black, 1)
        Dim Y As Double = 0
        Dim Logotipo As Drawing.Image = Nothing

        Dim cnn1 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
        Dim rd1 As MySqlDataReader
        Dim cmd1 As MySqlCommand

        Try

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

            cnn1.Close() : cnn1.Open()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText =
                "select Pie1,Pagare,Cab0,Cab1,Cab2,Cab3,Cab4,Cab5,Cab6 from Ticket"
            rd1 = cmd1.ExecuteReader
            If rd1.HasRows Then
                If rd1.Read Then

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
            e.Graphics.DrawString("--------------------------------------------------------", New Drawing.Font(tipografia, 12, FontStyle.Regular), Brushes.Black, 1, Y)
            Y += 15
            e.Graphics.DrawString("PRODUCCIÓN", New Drawing.Font(tipografia, 18, FontStyle.Bold), Brushes.Black, 140, Y, sc)
            Y += 17
            e.Graphics.DrawString("--------------------------------------------------------", New Drawing.Font(tipografia, 12, FontStyle.Regular), Brushes.Black, 1, Y)
            Y += 18

            e.Graphics.DrawString("Fecha: " & FormatDateTime(Date.Now, DateFormat.ShortDate), fuente_fecha, Brushes.Black, 1, Y)
            e.Graphics.DrawString("Hora: " & FormatDateTime(Date.Now, DateFormat.LongTime), fuente_fecha, Brushes.Black, 270, Y, sf)
            Y += 19
            e.Graphics.DrawString("Empleado: " & Mid(cboEmpleado.Text, 1, 48), fuente_prods, Brushes.Black, 1, Y)
            Y += 15

            e.Graphics.DrawString("--------------------------------------------------------", New Drawing.Font(tipografia, 12, FontStyle.Regular), Brushes.Black, 1, Y)
            Y += 15
            e.Graphics.DrawString("DESCRIPCIÓN", New Drawing.Font(tipografia, 18, FontStyle.Bold), Brushes.Black, 140, Y, sc)
            Y += 17
            e.Graphics.DrawString("--------------------------------------------------------", New Drawing.Font(tipografia, 12, FontStyle.Regular), Brushes.Black, 1, Y)
            Y += 15

            e.Graphics.DrawString("DESCRIPCIÓN", New Drawing.Font(tipografia, 9, FontStyle.Bold), Brushes.Black, 140, Y, sc)
            Y += 15
            e.Graphics.DrawString("CODIGO", New Drawing.Font(tipografia, 9, FontStyle.Bold), Brushes.Black, 5, Y)
            e.Graphics.DrawString("CANTIDAD", New Drawing.Font(tipografia, 9, FontStyle.Bold), Brushes.Black, 205, Y, sf)
            Y += 10
            e.Graphics.DrawString("--------------------------------------------------------", New Drawing.Font(tipografia, 12, FontStyle.Regular), Brushes.Black, 1, Y)
            Y += 18

            e.Graphics.DrawString(cbonombre.Text, New Drawing.Font(tipografia, 9, FontStyle.Regular), Brushes.Black, 1, Y)
            Y += 17
            e.Graphics.DrawString(cbocodigo.Text, New Drawing.Font(tipografia, 9, FontStyle.Regular), Brushes.Black, 5, Y)
            e.Graphics.DrawString(txtcantidad.Text, New Drawing.Font(tipografia, 9, FontStyle.Regular), Brushes.Black, 185, Y, sf)
            Y += 10
            e.Graphics.DrawString("--------------------------------------------------------", New Drawing.Font(tipografia, 12, FontStyle.Regular), Brushes.Black, 1, Y)
            Y += 18

            e.Graphics.DrawString("CAJERO: " & lblUsuario.Text, New Drawing.Font(tipografia, 9, FontStyle.Bold), Brushes.Black, 140, Y, sc)
            Y += 11
            e.HasMorePages = False

        Catch ex As Exception
            MessageBox.Show(ex.ToString)
            cnn1.Close()
        End Try
    End Sub

    Private Sub PDato58_PrintPage(sender As Object, e As Printing.PrintPageEventArgs) Handles PDato58.PrintPage
        'Fuentes prederminadas
        Dim tipografia As String = "Lucida Sans Typewriter"
        Dim fuente_datos As New Drawing.Font(tipografia, 8, FontStyle.Regular)
        Dim fuente_prods As New Drawing.Font(tipografia, 8, FontStyle.Regular)
        Dim fuente_fecha As New Drawing.Font(tipografia, 8, FontStyle.Regular)
        'Variables
        Dim sc As New StringFormat With {.Alignment = StringAlignment.Center}
        Dim sf As New StringFormat With {.Alignment = StringAlignment.Far}
        Dim pen As New Pen(Brushes.Black, 1)
        Dim Y As Double = 0
        Dim Logotipo As Drawing.Image = Nothing

        Dim cnn1 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
        Dim rd1 As MySqlDataReader
        Dim cmd1 As MySqlCommand

        Try

            If tLogo <> "SIN" Then
                If File.Exists(My.Application.Info.DirectoryPath & "\" & nLogo) Then
                    Logotipo = Drawing.Image.FromFile(My.Application.Info.DirectoryPath & "\" & nLogo)
                End If
                If tLogo = "CUAD" Then
                    e.Graphics.DrawImage(Logotipo, 45, 5, 110, 110)
                    Y += 125
                End If
                If tLogo = "RECT" Then
                    e.Graphics.DrawImage(Logotipo, 12, 5, 160, 110)
                    Y += 120
                End If
            Else
                Y = 0
            End If

            cnn1.Close() : cnn1.Open()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText =
                "select Pie1,Pagare,Cab0,Cab1,Cab2,Cab3,Cab4,Cab5,Cab6 from Ticket"
            rd1 = cmd1.ExecuteReader
            If rd1.HasRows Then
                If rd1.Read Then
                    'Razón social
                    If rd1("Cab0").ToString() <> "" Then
                        e.Graphics.DrawString(rd1("Cab0").ToString, New Drawing.Font(tipografia, 8, FontStyle.Bold), Brushes.Black, 90, Y, sc)
                        Y += 11
                    End If
                    'RFC
                    If rd1("Cab1").ToString() <> "" Then
                        e.Graphics.DrawString(rd1("Cab1").ToString, New Drawing.Font(tipografia, 8, FontStyle.Bold), Brushes.Black, 90, Y, sc)
                        Y += 11
                    End If
                    'Calle  N°.
                    If rd1("Cab2").ToString() <> "" Then
                        e.Graphics.DrawString(rd1("Cab2").ToString, New Drawing.Font(tipografia, 8, FontStyle.Regular), Brushes.Gray, 90, Y, sc)
                        Y += 10
                    End If
                    'Colonia
                    If rd1("Cab3").ToString() <> "" Then
                        e.Graphics.DrawString(rd1("Cab3").ToString, New Drawing.Font(tipografia, 8, FontStyle.Regular), Brushes.Gray, 90, Y, sc)
                        Y += 10
                    End If
                    'Delegación / Municipio - Entidad
                    If rd1("Cab4").ToString() <> "" Then
                        e.Graphics.DrawString(rd1("Cab4").ToString, New Drawing.Font(tipografia, 8, FontStyle.Regular), Brushes.Gray, 90, Y, sc)
                        Y += 10
                    End If
                    'Teléfono
                    If rd1("Cab5").ToString() <> "" Then
                        e.Graphics.DrawString(rd1("Cab5").ToString, New Drawing.Font(tipografia, 8, FontStyle.Regular), Brushes.Gray, 90, Y, sc)
                        Y += 10
                    End If
                    'Correo
                    If rd1("Cab6").ToString() <> "" Then
                        e.Graphics.DrawString(rd1("Cab6").ToString, New Drawing.Font(tipografia, 8, FontStyle.Regular), Brushes.Gray, 90, Y, sc)
                        Y += 10
                    End If
                    Y += 2
                End If
            Else
                Y += 0
            End If
            rd1.Close()

            cnn1.Close()

            '[1]. Datos de la venta
            e.Graphics.DrawString("--------------------------------------------------------", New Drawing.Font(tipografia, 12, FontStyle.Regular), Brushes.Black, 1, Y)
            Y += 15
            e.Graphics.DrawString("PRODUCCIÓN", New Drawing.Font(tipografia, 12, FontStyle.Bold), Brushes.Black, 90, Y, sc)
            Y += 17
            e.Graphics.DrawString("--------------------------------------------------------", New Drawing.Font(tipografia, 12, FontStyle.Regular), Brushes.Black, 1, Y)
            Y += 18

            e.Graphics.DrawString("Fecha: " & FormatDateTime(Date.Now, DateFormat.ShortDate), fuente_fecha, Brushes.Black, 1, Y)
            Y += 15
            e.Graphics.DrawString("Hora: " & FormatDateTime(Date.Now, DateFormat.LongTime), fuente_fecha, Brushes.Black, 1, Y)
            Y += 19
            e.Graphics.DrawString("Empleado: " & Mid(cboEmpleado.Text, 1, 48), fuente_prods, Brushes.Black, 1, Y)
            Y += 13

            e.Graphics.DrawString("--------------------------------------------------------", New Drawing.Font(tipografia, 12, FontStyle.Regular), Brushes.Black, 1, Y)
            Y += 15
            e.Graphics.DrawString("DESCRIPCIÓN", New Drawing.Font(tipografia, 12, FontStyle.Bold), Brushes.Black, 90, Y, sc)
            Y += 17
            e.Graphics.DrawString("--------------------------------------------------------", New Drawing.Font(tipografia, 12, FontStyle.Regular), Brushes.Black, 1, Y)
            Y += 15

            e.Graphics.DrawString("DESCRIPCIÓN", New Drawing.Font(tipografia, 9, FontStyle.Bold), Brushes.Black, 90, Y, sc)
            Y += 15
            e.Graphics.DrawString("CODIGO", New Drawing.Font(tipografia, 9, FontStyle.Bold), Brushes.Black, 5, Y)
            e.Graphics.DrawString("CANTIDAD", New Drawing.Font(tipografia, 9, FontStyle.Bold), Brushes.Black, 170, Y, sf)
            Y += 10
            e.Graphics.DrawString("--------------------------------------------------------", New Drawing.Font(tipografia, 12, FontStyle.Regular), Brushes.Black, 1, Y)
            Y += 18

            Dim caracteresPorLinea As Integer = 25
            Dim texto As String = cbonombre.Text
            Dim inicio As Integer = 0
            Dim longitudTexto As Integer = texto.Length

            While inicio < longitudTexto
                Dim longitudBloque As Integer = Math.Min(caracteresPorLinea, longitudTexto - inicio)
                Dim bloque As String = texto.Substring(inicio, longitudBloque)
                e.Graphics.DrawString(bloque, New Font("Arial", 8, FontStyle.Regular), Brushes.Black, 1, Y)
                Y += 15
                inicio += caracteresPorLinea
            End While
            Y += 5
            e.Graphics.DrawString(cbocodigo.Text, New Drawing.Font(tipografia, 9, FontStyle.Regular), Brushes.Black, 5, Y)
            e.Graphics.DrawString(txtcantidad.Text, New Drawing.Font(tipografia, 9, FontStyle.Regular), Brushes.Black, 140, Y, sf)
            Y += 10
            e.Graphics.DrawString("--------------------------------------------------------", New Drawing.Font(tipografia, 12, FontStyle.Regular), Brushes.Black, 1, Y)
            Y += 18

            e.Graphics.DrawString("CAJERO: " & lblUsuario.Text, New Drawing.Font(tipografia, 9, FontStyle.Bold), Brushes.Black, 90, Y, sc)
            Y += 11
            e.HasMorePages = False

        Catch ex As Exception
            MessageBox.Show(ex.ToString)
            cnn1.Close()
        End Try
    End Sub
End Class