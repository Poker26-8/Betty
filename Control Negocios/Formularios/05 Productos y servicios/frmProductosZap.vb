﻿Imports MySql.Data.MySqlClient

Public Class frmProductosZap

    Private Sub frmProductosZap_Shown(sender As Object, e As System.EventArgs) Handles Me.Shown
        cboarticulo.Focus().Equals(True)
    End Sub

    Private Sub frmProductosZap_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        REM Carga los códigos cortos del sistema (van a ser autonuméricos en el caso de zapaterías o tiendas de ropa)
        Dim codigo As Integer = 0
        Dim cnn3 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
        Dim rd3 As MySqlDataReader
        Dim cmd3 As MySqlCommand
        Try
            cnn3.Close() : cnn3.Open()

            cmd3 = cnn3.CreateCommand
            cmd3.CommandText =
                "select max(Codigo) from Productos"
            rd3 = cmd3.ExecuteReader
            If rd3.HasRows Then
                If rd3.Read Then
                    codigo = IIf(rd3(0).ToString() = "", 0, rd3(0).ToString()) + 1
                End If
            Else
                codigo = 1
            End If
            rd3.Close() : cnn3.Close()

            Dim cod As String = ""
            If codigo < 10 Then
                txtcodigo.Text = "00000" & codigo
            ElseIf codigo < 100 Then
                txtcodigo.Text = "0000" & codigo
            ElseIf codigo < 1000 Then
                txtcodigo.Text = "000" & codigo
            ElseIf codigo < 10000 Then
                txtcodigo.Text = "00" & codigo
            ElseIf codigo < 100000 Then
                txtcodigo.Text = "0" & codigo
            ElseIf codigo < 1000000 Then
                txtcodigo.Text = codigo
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString())
            cnn3.Close()
        End Try
    End Sub

    Private Sub cboarticulo_DropDown(sender As System.Object, e As System.EventArgs) Handles cboarticulo.DropDown
        cboarticulo.Items.Clear()
        Dim cnn1 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
        Dim rd1 As MySqlDataReader
        Dim cmd1 As MySqlCommand
        Try
            cnn1.Close() : cnn1.Open()

            cmd1 = cnn1.CreateCommand
            cmd1.CommandText =
                "select distinct Articulo from Productos where Articulo<>'' order by Articulo"
            rd1 = cmd1.ExecuteReader
            Do While rd1.Read
                If rd1.HasRows Then cboarticulo.Items.Add(
                    rd1(0).ToString()
                    )
            Loop
            rd1.Close() : cnn1.Close()
        Catch ex As Exception
            MessageBox.Show(ex.ToString())
            cnn1.Close()
        End Try
    End Sub

    Private Sub cbomarca_DropDown(sender As System.Object, e As System.EventArgs) Handles cbomarca.DropDown
        cbomarca.Items.Clear()

        Dim cnn1 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
        Dim rd1 As MySqlDataReader
        Dim cmd1 As MySqlCommand
        Try
            cnn1.Close() : cnn1.Open()

            cmd1 = cnn1.CreateCommand
            cmd1.CommandText =
                "select distinct Marca from Productos where Marca<>'' order by Marca"
            rd1 = cmd1.ExecuteReader
            Do While rd1.Read
                If rd1.HasRows Then cbomarca.Items.Add(
                    rd1(0).ToString()
                    )
            Loop
            rd1.Close() : cnn1.Close()
        Catch ex As Exception
            MessageBox.Show(ex.ToString())
            cnn1.Close()
        End Try
    End Sub

    Private Sub cbomodelo_DropDown(sender As System.Object, e As System.EventArgs) Handles cbomodelo.DropDown
        cbomodelo.Items.Clear()

        Dim cnn1 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
        Dim rd1 As MySqlDataReader
        Dim cmd1 As MySqlCommand

        Try
            cnn1.Close() : cnn1.Open()

            cmd1 = cnn1.CreateCommand
            cmd1.CommandText =
                "select distinct Grupo from Productos where Grupo<>'' order by Grupo"
            rd1 = cmd1.ExecuteReader
            Do While rd1.Read
                If rd1.HasRows Then cbomodelo.Items.Add(
                    rd1(0).ToString()
                    )
            Loop
            rd1.Close() : cnn1.Close()
        Catch ex As Exception
            MessageBox.Show(ex.ToString())
            cnn1.Close()
        End Try
    End Sub

    Private Sub cbocolor_DropDown(sender As System.Object, e As System.EventArgs) Handles cbocolor.DropDown
        cbocolor.Items.Clear()

        Dim cnn1 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
        Dim rd1 As MySqlDataReader
        Dim cmd1 As MySqlCommand

        Try
            cnn1.Close() : cnn1.Open()

            cmd1 = cnn1.CreateCommand
            cmd1.CommandText =
                "select distinct Color from Productos where Color<>'' order by Color"
            rd1 = cmd1.ExecuteReader
            Do While rd1.Read
                If rd1.HasRows Then cbocolor.Items.Add(
                    rd1(0).ToString()
                    )
            Loop
            rd1.Close() : cnn1.Close()
        Catch ex As Exception
            MessageBox.Show(ex.ToString())
            cnn1.Close()
        End Try
    End Sub

    Private Sub cboproveedor_DropDown(sender As System.Object, e As System.EventArgs) Handles cboproveedor.DropDown
        cboproveedor.Items.Clear()

        Dim cnn1 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
        Dim rd1 As MySqlDataReader
        Dim cmd1 As MySqlCommand

        Try
            cnn1.Close() : cnn1.Open()

            cmd1 = cnn1.CreateCommand
            cmd1.CommandText =
                "select distinct NComercial from Proveedores where NComercial<>'' order by NComercial"
            rd1 = cmd1.ExecuteReader
            Do While rd1.Read
                If rd1.HasRows Then cboproveedor.Items.Add(
                    rd1(0).ToString()
                    )
            Loop
            rd1.Close() : cnn1.Close()
        Catch ex As Exception
            MessageBox.Show(ex.ToString())
            cnn1.Close()
        End Try
    End Sub

    Private Sub cbodepto_DropDown(sender As System.Object, e As System.EventArgs) Handles cbodepto.DropDown
        cbodepto.Items.Clear()

        Dim cnn1 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
        Dim rd1 As MySqlDataReader
        Dim cmd1 As MySqlCommand

        Try
            cnn1.Close() : cnn1.Open()

            cmd1 = cnn1.CreateCommand
            cmd1.CommandText =
                "select distinct Departamento from Productos where Departamento<>'' order by Departamento"
            rd1 = cmd1.ExecuteReader
            Do While rd1.Read
                If rd1.HasRows Then cbodepto.Items.Add(
                    rd1(0).ToString()
                    )
            Loop
            rd1.Close() : cnn1.Close()
        Catch ex As Exception
            MessageBox.Show(ex.ToString())
            cnn1.Close()
        End Try
    End Sub

    Private Sub cboubica_DropDown(sender As System.Object, e As System.EventArgs) Handles cboubica.DropDown
        cboubica.Items.Clear()

        Dim cnn1 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
        Dim rd1 As MySqlDataReader
        Dim cmd1 As MySqlCommand

        Try
            cnn1.Close() : cnn1.Open()

            cmd1 = cnn1.CreateCommand
            cmd1.CommandText =
                "select distinct Ubicacion from Productos where Ubicacion<>'' order by Ubicacion"
            rd1 = cmd1.ExecuteReader
            Do While rd1.Read
                If rd1.HasRows Then cboubica.Items.Add(
                    rd1(0).ToString()
                    )
            Loop
            rd1.Close() : cnn1.Close()
        Catch ex As Exception
            MessageBox.Show(ex.ToString())
            cnn1.Close()
        End Try
    End Sub

    Private Sub cbogenero_DropDown(sender As System.Object, e As System.EventArgs) Handles cbogenero.DropDown
        cbogenero.Items.Clear()

        Dim cnn1 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
        Dim rd1 As MySqlDataReader
        Dim cmd1 As MySqlCommand

        Try
            cnn1.Close() : cnn1.Open()

            cmd1 = cnn1.CreateCommand
            cmd1.CommandText =
                "select distinct Genero from Productos where Genero<>'' order by Genero"
            rd1 = cmd1.ExecuteReader
            Do While rd1.Read
                If rd1.HasRows Then cbogenero.Items.Add(
                    rd1(0).ToString()
                    )
            Loop
            rd1.Close() : cnn1.Close()
        Catch ex As Exception
            MessageBox.Show(ex.ToString())
            cnn1.Close()
        End Try
    End Sub

    Private Sub cbouso_DropDown(sender As System.Object, e As System.EventArgs) Handles cbouso.DropDown
        cbouso.Items.Clear()

        Dim cnn1 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
        Dim rd1 As MySqlDataReader
        Dim cmd1 As MySqlCommand

        Try
            cnn1.Close() : cnn1.Open()

            cmd1 = cnn1.CreateCommand
            cmd1.CommandText =
                "select distinct Uso from Productos where Uso<>'' order by Uso"
            rd1 = cmd1.ExecuteReader
            Do While rd1.Read
                If rd1.HasRows Then cbouso.Items.Add(
                    rd1(0).ToString()
                    )
            Loop
            rd1.Close() : cnn1.Close()
        Catch ex As Exception
            MessageBox.Show(ex.ToString())
            cnn1.Close()
        End Try
    End Sub

    Private Sub cboiva_DropDown(sender As System.Object, e As System.EventArgs) Handles cboiva.DropDown
        cboiva.Items.Clear()

        Dim cnn1 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
        Dim rd1 As MySqlDataReader
        Dim cmd1 As MySqlCommand

        Try
            cnn1.Close() : cnn1.Open()

            cmd1 = cnn1.CreateCommand
            cmd1.CommandText =
                "select IVA from IVA"
            rd1 = cmd1.ExecuteReader
            Do While rd1.Read
                If rd1.HasRows Then cboiva.Items.Add(
                    rd1(0).ToString()
                    )
            Loop
            rd1.Close() : cnn1.Close()
        Catch ex As Exception
            MessageBox.Show(ex.ToString())
            cnn1.Close()
        End Try
    End Sub

    Private Sub cbotalla_DropDown(sender As System.Object, e As System.EventArgs) Handles cbotalla.DropDown
        cbotalla.Items.Clear()

        Dim cnn1 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
        Dim rd1 As MySqlDataReader
        Dim cmd1 As MySqlCommand

        Try
            cnn1.Close() : cnn1.Open()

            cmd1 = cnn1.CreateCommand
            cmd1.CommandText =
                "select distinct ProvEme from Productos where ProvEme<>'' order by ProvEme"
            rd1 = cmd1.ExecuteReader
            Do While rd1.Read
                If rd1.HasRows Then cbotalla.Items.Add(
                    rd1(0).ToString()
                    )
            Loop
            rd1.Close() : cnn1.Close()
        Catch ex As Exception
            MessageBox.Show(ex.ToString())
            cnn1.Close()
        End Try
    End Sub

    Private Sub cboarticulo_KeyPress(sender As System.Object, e As System.Windows.Forms.KeyPressEventArgs) Handles cboarticulo.KeyPress
        e.KeyChar = UCase(e.KeyChar)
        If AscW(e.KeyChar) = Keys.Enter Then
            txtnombre.Text = cboarticulo.Text & " " & cbomarca.Text & " " & cbomodelo.Text & " " & cbocolor.Text & "  " & cbotalla.Text
            cbomarca.Focus().Equals(True)
        End If
    End Sub

    Private Sub cbomarca_KeyPress(sender As System.Object, e As System.Windows.Forms.KeyPressEventArgs) Handles cbomarca.KeyPress
        e.KeyChar = UCase(e.KeyChar)
        If AscW(e.KeyChar) = Keys.Enter Then
            txtnombre.Text = cboarticulo.Text & " " & cbomarca.Text & " " & cbomodelo.Text & " " & cbocolor.Text & "  " & cbotalla.Text
            cbomodelo.Focus().Equals(True)
        End If
    End Sub

    Private Sub cbomodelo_KeyPress(sender As System.Object, e As System.Windows.Forms.KeyPressEventArgs) Handles cbomodelo.KeyPress
        e.KeyChar = UCase(e.KeyChar)
        If AscW(e.KeyChar) = Keys.Enter Then
            txtnombre.Text = cboarticulo.Text & " " & cbomarca.Text & " " & cbomodelo.Text & " " & cbocolor.Text & "  " & cbotalla.Text
            cbocolor.Focus().Equals(True)
        End If
    End Sub

    Private Sub cbocolor_KeyPress(sender As System.Object, e As System.Windows.Forms.KeyPressEventArgs) Handles cbocolor.KeyPress
        e.KeyChar = UCase(e.KeyChar)
        If AscW(e.KeyChar) = Keys.Enter Then
            txtnombre.Text = cboarticulo.Text & " " & cbomarca.Text & " " & cbomodelo.Text & " " & cbocolor.Text & "  " & cbotalla.Text
            cboproveedor.Focus().Equals(True)
        End If
    End Sub

    Private Sub cboproveedor_KeyPress(sender As System.Object, e As System.Windows.Forms.KeyPressEventArgs) Handles cboproveedor.KeyPress
        e.KeyChar = UCase(e.KeyChar)
        If AscW(e.KeyChar) = Keys.Enter Then
            Dim cnn1 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
            Dim rd1 As MySqlDataReader
            Dim cmd1 As MySqlCommand
            Try
                cnn1.Close() : cnn1.Open()

                cmd1 = cnn1.CreateCommand
                cmd1.CommandText =
                    "select NComercial from Proveedores where NComercial='" & cboproveedor.Text & "'"
                rd1 = cmd1.ExecuteReader
                If rd1.HasRows Then
                    If rd1.Read Then
                        cbodepto.Focus().Equals(True)
                    End If
                Else
                    MsgBox("El proveedor no existe en el catálogo de proveedores, regístralo para continuar.", vbInformation + vbOKOnly, "Delsscom Control Negocios Pro")
                End If
                rd1.Close() : cnn1.Close()
            Catch ex As Exception
                MessageBox.Show(ex.ToString())
                cnn1.Close()
            End Try
        End If
    End Sub

    Private Sub cbodepto_KeyPress(sender As System.Object, e As System.Windows.Forms.KeyPressEventArgs) Handles cbodepto.KeyPress
        e.KeyChar = UCase(e.KeyChar)
        If AscW(e.KeyChar) = Keys.Enter Then
            cboubica.Focus().Equals(True)
        End If
    End Sub

    Private Sub cboubica_KeyPress(sender As System.Object, e As System.Windows.Forms.KeyPressEventArgs) Handles cboubica.KeyPress
        e.KeyChar = UCase(e.KeyChar)
        If AscW(e.KeyChar) = Keys.Enter Then
            cbogenero.Focus().Equals(True)
        End If
    End Sub

    Private Sub cbogenero_KeyPress(sender As System.Object, e As System.Windows.Forms.KeyPressEventArgs) Handles cbogenero.KeyPress
        e.KeyChar = UCase(e.KeyChar)
        If AscW(e.KeyChar) = Keys.Enter Then
            cbouso.Focus().Equals(True)
        End If
    End Sub

    Private Sub cbouso_KeyPress(sender As System.Object, e As System.Windows.Forms.KeyPressEventArgs) Handles cbouso.KeyPress
        e.KeyChar = UCase(e.KeyChar)
        If AscW(e.KeyChar) = Keys.Enter Then
            cboiva.Focus().Equals(True)
        End If
    End Sub

    Private Sub cboiva_KeyPress(sender As System.Object, e As System.Windows.Forms.KeyPressEventArgs) Handles cboiva.KeyPress
        e.KeyChar = UCase(e.KeyChar)
        If AscW(e.KeyChar) = Keys.Enter Then
            txtpcompra.Focus().Equals(True)
        End If
    End Sub

    Private Sub txtpcompra_GotFocus(sender As Object, e As System.EventArgs) Handles txtpcompra.GotFocus
        txtpcompra.SelectionStart = 0
        txtpcompra.SelectionLength = Len(txtpcompra.Text)
    End Sub

    Private Sub txtpcompra_KeyPress(sender As Object, e As System.Windows.Forms.KeyPressEventArgs) Handles txtpcompra.KeyPress
        If Not IsNumeric(txtporcentaje.Text) Then txtporcentaje.Text = "0" : Exit Sub
        If Not IsNumeric(txtpcompra.Text) Then txtpcompra.Text = "0.00" : Exit Sub
        If AscW(e.KeyChar) = Keys.Enter Then
            txtporcentaje.Focus().Equals(True)
            txtpcompra.Text = FormatNumber(txtpcompra.Text, 2)
        End If
    End Sub

    Private Sub txtporcentaje_KeyPress(sender As System.Object, e As System.Windows.Forms.KeyPressEventArgs) Handles txtporcentaje.KeyPress
        If Not IsNumeric(txtporcentaje.Text) Then txtporcentaje.Text = "0" : Exit Sub
        If Not IsNumeric(txtpcompra.Text) Then txtpcompra.Text = "0.00" : Exit Sub
        If AscW(e.KeyChar) = Keys.Enter Then
            txtpventa.Text = CDbl(txtpcompra.Text) * CDbl(1 + (CDbl(txtporcentaje.Text) / 100))
            txtpventa.Text = FormatNumber(txtpventa.Text, 2)
            txtpventa.Focus().Equals(True)
        End If
    End Sub

    Private Sub txtpventa_KeyPress(sender As System.Object, e As System.Windows.Forms.KeyPressEventArgs) Handles txtpventa.KeyPress
        If Not IsNumeric(txtpventa.Text) Then txtpventa.Text = "0.00" : Exit Sub
        If Not IsNumeric(txtporcentaje.Text) Then txtporcentaje.Text = "0" : Exit Sub
        If AscW(e.KeyChar) = Keys.Enter Then
            Dim compra As Double = txtpcompra.Text
            Dim venta As Double = txtpventa.Text
            Dim utilidad As Double = 0
            Dim d As Double = 0

            d = venta * 100
            utilidad = (d / compra) - 100
            txtporcentaje.Text = FormatNumber(utilidad, 1)
            txtporcentaje.Text = CDbl(txtpventa.Text) / CDbl(1 + (CDbl(txtporcentaje.Text) / 100))
            txtclavesat.Focus().Equals(True)
            txtpventa.Text = FormatNumber(txtpventa.Text, 2)
        End If
    End Sub

    Private Sub txtunidadsat_KeyPress(sender As System.Object, e As System.Windows.Forms.KeyPressEventArgs) Handles txtunidadsat.KeyPress
        If AscW(e.KeyChar) = Keys.Enter Then
            txtexistencia.Focus().Equals(True)
        End If
    End Sub

    Private Sub txtclavesat_KeyPress(sender As System.Object, e As System.Windows.Forms.KeyPressEventArgs) Handles txtclavesat.KeyPress
        If AscW(e.KeyChar) = Keys.Enter Then
            txtunidadsat.Focus().Equals(True)
        End If
    End Sub

    Private Sub txtexistencia_KeyPress(sender As System.Object, e As System.Windows.Forms.KeyPressEventArgs) Handles txtexistencia.KeyPress
        If AscW(e.KeyChar) = Keys.Enter Then
            If Not IsNumeric(txtexistencia.Text) Then txtexistencia.Text = "" : Exit Sub
            cboUnidad.Focus().Equals(True)
        End If
    End Sub

    Private Sub cbotalla_KeyPress(sender As System.Object, e As System.Windows.Forms.KeyPressEventArgs) Handles cbotalla.KeyPress
        e.KeyChar = UCase(e.KeyChar)
        If AscW(e.KeyChar) = Keys.Enter Then
            txtnombre.Text = cboarticulo.Text & " " & cbomarca.Text & " " & cbomodelo.Text & " " & cbocolor.Text & "  " & cbotalla.Text
            UpGrid()
        End If
    End Sub

    Private Sub UpGrid()
        Try
            grdcaptura.Rows.Add(txtcodigo.Text, txtbarras.Text, txtnombre.Text, cboarticulo.Text, cbomarca.Text, cbomodelo.Text, cbocolor.Text, cboproveedor.Text, cbodepto.Text, cboUnidad.Text, cboubica.Text, cbogenero.Text, cbouso.Text, cboiva.Text, txtpcompra.Text, txtporcentaje.Text, txtpventa.Text, txtexistencia.Text, cbotalla.Text, txtclavesat.Text, txtunidadsat.Text)

            Dim codigo As Double = txtcodigo.Text

            If codigo < 10 Then
                txtcodigo.Text = "00000" & (codigo + 1)
            ElseIf codigo < 100 Then
                txtcodigo.Text = "0000" & (codigo + 1)
            ElseIf codigo < 1000 Then
                txtcodigo.Text = "000" & (codigo + 1)
            ElseIf codigo < 10000 Then
                txtcodigo.Text = "00" & (codigo + 1)
            ElseIf codigo < 100000 Then
                txtcodigo.Text = "0" & (codigo + 1)
            ElseIf codigo < 1000000 Then
                txtcodigo.Text = (codigo + 1)
            End If
            txtbarras.Text = ""
            cboiva.Text = "0"
            txtpcompra.Text = "0.00"
            txtporcentaje.Text = "0"
            txtpventa.Text = "0.00"
            txtexistencia.Text = "1"
            cbotalla.Text = ""
            txtclavesat.Text = ""
            txtunidadsat.Text = ""
            txtnombre.Text = cboarticulo.Text & " " & cbomarca.Text & " " & cbomodelo.Text & " " & cbocolor.Text
            txtregistros.Text = CDbl(IIf(txtregistros.Text = "", 0, txtregistros.Text)) + 1

            cboiva.Focus().Equals(True)
        Catch ex As Exception
            MessageBox.Show(ex.ToString())
        End Try
    End Sub

    Private Sub btnGuardar_Click(sender As System.Object, e As System.EventArgs) Handles btnGuardar.Click

        Dim cnn1 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
        Dim cnn2 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
        Dim rd1, rd2 As MySqlDataReader
        Dim cmd1, cmd2 As MySqlCommand
        Try
            If txtnombre.Text = "" Then MsgBox("Ingrese la descripción del producto") : txtnombre.Focus.Equals(True) : Exit Sub


            If MsgBox("¿Desea guardar los productos?", vbInformation + vbYesNo) = vbYes Then

                If grdcaptura.Rows.Count > 0 Then
                    cnn1.Close() : cnn1.Open()
                    cnn2.Close() : cnn2.Open()

                    For luffy As Integer = 0 To grdcaptura.Rows.Count - 1

                        Dim codigo As String = grdcaptura.Rows(luffy).Cells(0).Value.ToString
                        Dim barras As String = grdcaptura.Rows(luffy).Cells(1).Value.ToString
                        Dim descripcion As String = grdcaptura.Rows(luffy).Cells(2).Value.ToString
                        Dim articulo As String = grdcaptura.Rows(luffy).Cells(3).Value.ToString
                        Dim marca As String = grdcaptura.Rows(luffy).Cells(4).Value.ToString
                        Dim modelo As String = grdcaptura.Rows(luffy).Cells(5).Value.ToString
                        Dim color As String = grdcaptura.Rows(luffy).Cells(6).Value.ToString
                        Dim proveedor As String = grdcaptura.Rows(luffy).Cells(7).Value.ToString
                        Dim departamento As String = grdcaptura.Rows(luffy).Cells(8).Value.ToString
                        Dim umedida As String = grdcaptura.Rows(luffy).Cells(9).Value.ToString
                        Dim ubicacion As String = grdcaptura.Rows(luffy).Cells(10).Value.ToString
                        Dim genero As String = grdcaptura.Rows(luffy).Cells(11).Value.ToString
                        Dim uso As String = grdcaptura.Rows(luffy).Cells(12).Value.ToString
                        Dim iva As Double = grdcaptura.Rows(luffy).Cells(13).Value.ToString
                        Dim precompra As Double = grdcaptura.Rows(luffy).Cells(14).Value.ToString
                        Dim utilidad As Double = grdcaptura.Rows(luffy).Cells(15).Value.ToString
                        Dim preventa As Double = grdcaptura.Rows(luffy).Cells(16).Value.ToString
                        Dim existencia As Double = grdcaptura.Rows(luffy).Cells(17).Value.ToString
                        Dim talla As String = grdcaptura.Rows(luffy).Cells(18).Value.ToString
                        Dim clavesat As String = grdcaptura.Rows(luffy).Cells(19).Value.ToString
                        Dim unidadsat As String = grdcaptura.Rows(luffy).Cells(20).Value.ToString


                        cmd1 = cnn1.CreateCommand
                        cmd1.CommandText = "SELECT Codigo FROM productos WHERE Codigo='" & codigo & "'"
                        rd1 = cmd1.ExecuteReader
                        If rd1.HasRows Then
                            If rd1.Read Then

                            End If
                        Else

                            cmd2 = cnn2.CreateCommand
                            cmd2.CommandText = "INSERT INTO productos(Codigo,CodBarra,Nombre,NombreLargo,ProvPri,ProvEme,UCompra,UVenta,UMinima,MCD,Multiplo,Departamento,Grupo,Ubicacion,Min,Max,PrecioCompra,Almacen3,PrecioVenta,PrecioVentaIVA,IVA,Existencia,Porcentaje,ClaveSat,UnidadSat,Fecha,Articulo,Marca,Color,Genero,Uso,Fecha_Inicial,Fecha_Final,id_tbMoneda) VALUES('" & codigo & "','" & barras & "','" & descripcion & "','" & descripcion & "','" & proveedor & "','" & talla & "','" & umedida & "','" & umedida & "','" & umedida & "',1,1,'" & departamento & "','" & modelo & "','" & ubicacion & "',1,1," & precompra & "," & precompra & "," & preventa & "," & preventa & "," & iva & "," & existencia & "," & utilidad & ",'" & clavesat & "','" & unidadsat & "','" & Format(Date.Now, "yyyy-MM-dd") & "','" & articulo & "','" & marca & "','" & color & "','" & genero & "','" & uso & "','" & Format(Date.Now, "yyyy-MM-dd") & "','" & Format(Date.Now, "yyyy-MM-dd") & "',1)"
                            cmd2.ExecuteNonQuery()

                        End If
                        rd1.Close()

                    Next
                    MsgBox("Productos agregados correctamente", vbInformation + vbOKOnly, titulocentral)
                End If
                cnn1.Close()
                cnn2.Close()
            End If

            btnNuevo.PerformClick()
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
            cnn1.Close()
        End Try
    End Sub

    Private Sub cboarticulo_LostFocus(sender As Object, e As System.EventArgs) Handles cboarticulo.LostFocus
        txtnombre.Text = cboarticulo.Text & " " & cbomarca.Text & " " & cbomodelo.Text & " " & cbocolor.Text & "  " & cbotalla.Text
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Close()
    End Sub

    Private Sub btnNuevo_Click(sender As Object, e As EventArgs) Handles btnNuevo.Click
        txtnombre.Text = ""
        txtbarras.Text = ""
        cboarticulo.Text = ""
        cbomarca.Text = ""
        cbomodelo.Text = ""
        cbocolor.Text = ""
        cboproveedor.Text = ""
        cbodepto.Text = ""
        cboubica.Text = ""
        cbogenero.Text = ""
        cbouso.Text = ""
        cboiva.Text = "0"
        txtpcompra.Text = "0.00"
        txtporcentaje.Text = "0.00"
        txtpventa.Text = "0.00"
        txtclavesat.Text = ""
        txtunidadsat.Text = ""
        txtexistencia.Text = "0"
        cbotalla.Text = ""
        CheckBox1.Checked = False
        txtregistros.Text = "0"

        grdcaptura.Rows.Clear()
        cboUnidad.Text = ""
        cboarticulo.Focused.Equals(True)
    End Sub

    Private Sub cboUnidad_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cboUnidad.KeyPress
        e.KeyChar = UCase(e.KeyChar)
        If AscW(e.KeyChar) = Keys.Enter Then
            cbotalla.Focus().Equals(True)
        End If
    End Sub
End Class