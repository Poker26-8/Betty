Public Class frmEntalla

    Dim cantpro As Integer = 0
    Dim bandera As Integer = 0
    Dim codigoproducto As String = ""

    Private Sub frmEntalla_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        cboModelo.Focus.Equals(True)
    End Sub

    Private Sub frmEntalla_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        cboModelo.Focus.Equals(True)
    End Sub

    Private Sub cboRemision_DropDown(sender As Object, e As EventArgs) Handles cboRemision.DropDown
        Try
            cboRemision.Items.Clear()
            cnn5.Close() : cnn5.Open()
            cmd5 = cnn5.CreateCommand
            cmd5.CommandText = "SELECT DISTINCT NumRemision FROM comprasentalla WHERE Entalle=0 AND NumRemision<>'' ORDER BY NumRemision"
            rd5 = cmd5.ExecuteReader
            Do While rd5.Read
                If rd5.HasRows Then
                    cboRemision.Items.Add(rd5(0).ToString)
                End If
            Loop
            rd5.Close()
            cnn5.Close()

        Catch ex As Exception
            MessageBox.Show(ex.ToString)
            cnn5.Close()
        End Try
    End Sub

    Private Sub cboRemision_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cboRemision.KeyPress
        e.KeyChar = UCase(e.KeyChar)
        If AscW(e.KeyChar) = Keys.Enter Then
            txtSuma.Text = "0.00"
            txtCantidad.Text = "0"

            Dim myfolio As String = ""
            Dim mynombre As String = ""
            Dim myunidad As String = ""
            Dim mymodelo As String = ""
            Dim mycantidad As Double = 0

            Dim miidcompra As Integer = 0
            Dim mifracremi As String = ""
            Dim mifacremi As String = ""
            Dim proveedor As String = ""
            Dim mifecha As Date = Nothing
            Dim f As String = ""

            Dim micodigo As String = ""
            Dim minombre As String = ""
            Dim miucompra As String = ""
            Dim mipreciocompra As Double = 0
            Dim migrupo As String = ""
            Dim midepto As String = ""


            If Len(cboModelo.Text) > 0 And Len(cboRemision.Text) > 0 Then
                cnn1.Close() : cnn1.Open()
                cnn2.Close() : cnn2.Open()

                cmd1 = cnn1.CreateCommand
                cmd1.CommandText = "SELECT Id_compra,NumRemision,Proveedor,Fecha FROM comprasentalla WHERE Modelo='" & cboModelo.Text & "' AND Entalle=0 AND NumRemision='" & cboRemision.Text & "'"
                rd1 = cmd1.ExecuteReader
                If rd1.HasRows Then
                    If rd1.Read Then
                        miidcompra = rd1("Id_compra").ToString
                        mifracremi = IIf(rd1("NumRemision").ToString = "", "0", rd1("NumRemision").ToString)
                        proveedor = rd1("Proveedor").ToString
                        mifecha = rd1("Fecha").ToString
                        f = Format(mifecha, "yyyy-MM-dd")
                    End If
                Else
                    MsgBox("El modelo no existe.", vbInformation + vbOK, titulocentral)
                    cboModelo.Focus.Equals(True)
                    Exit Sub
                End If
                rd1.Close()

                cmd1 = cnn1.CreateCommand
                cmd1.CommandText = "SELECT Cantidad FROM comprasentalla WHERE Modelo='" & cboModelo.Text & "'"
                rd1 = cmd1.ExecuteReader
                Do While rd1.Read
                    If rd1.HasRows Then
                        txtCantidadmod.Text = txtCantidadmod.Text + CDec(rd1(0).ToString)
                    End If
                Loop
                rd1.Close()
                txtCantidadmod.Text = FormatNumber(txtCantidadmod.Text, 2)

                cmd1 = cnn1.CreateCommand
                cmd1.CommandText = "SELECT Codigo,Nombre,UCompra,PrecioCompra,Grupo,Departamento FROM productos WHERE Grupo='" & cboModelo.Text & "'"
                rd1 = cmd1.ExecuteReader
                Do While rd1.Read
                    If rd1.HasRows Then

                        micodigo = rd1("Codigo").ToString
                        minombre = rd1("Nombre").ToString
                        miucompra = rd1("UCompra").ToString
                        mipreciocompra = rd1("PrecioCompra").ToString
                        migrupo = rd1("Grupo").ToString
                        midepto = rd1("Departamento").ToString


                        cmd2 = cnn2.CreateCommand
                        cmd2.CommandText = "SELECT * FROM comprasdet WHERE Id_compra=" & miidcompra & " AND NumRemision='" & mifracremi & "' AND Modelo='" & cboModelo.Text & "' AND Proveedor='" & proveedor & "' AND Codigo='" & micodigo & "'"
                        rd2 = cmd2.ExecuteReader
                        If rd2.HasRows Then
                            If rd2.Read Then
                                cnn3.Close() : cnn3.Open()
                                cmd3 = cnn3.CreateCommand
                                cmd3.CommandText = "UPDATE comprasdet SET CANTIDAD_MODIFICADA='" & rd2("Cantidad").ToString & "' WHERE Id_compra=" & miidcompra & " AND NumRemision='" & mifracremi & "' AND Modelo='" & cboModelo.Text & "' AND Proveedor='" & proveedor & "' AND Codigo='" & micodigo & "'"
                                cmd3.ExecuteNonQuery()
                                cnn3.Close()
                            End If
                        Else
                            cnn3.Close() : cnn3.Open()
                            cmd3 = cnn3.CreateCommand
                            cmd3.CommandText = "INSERT INTO comprasdet(Id_compra,NumRemision,NumFactura,Proveedor,Codigo,Nombre,UCompra,Cantidad,Precio,Total,FechaC,Grupo,Depto,Modelo,CANTIDAD_MODIFICADA) VALUES(" & miidcompra & ",'" & mifracremi & "','" & mifacremi & "','" & proveedor & "','" & micodigo & "','" & minombre & "','" & miucompra & "',0.00," & mipreciocompra & ",0.00,'" & f & "','" & migrupo & "','" & midepto & "','" & cboModelo.Text & "','0.00')"
                            cmd3.ExecuteNonQuery()
                            cnn3.Close()
                        End If
                        rd2.Close()
                    End If
                Loop
                rd1.Close()
                cnn2.Close()
                cnn1.Close()
            End If

            If Len(proveedor) > 0 Then
                cnn1.Close() : cnn1.Open()
                cmd1 = cnn1.CreateCommand
                cmd1.CommandText = "SELECT Codigo,Nombre,UCompra,Cantidad,Modelo FROM comprasdet WHERE Id_compra=" & miidcompra & " AND NumRemision='" & mifracremi & "' AND Modelo='" & cboModelo.Text & "' AND Proveedor='" & proveedor & "' ORDER BY Nombre"
                rd1 = cmd1.ExecuteReader
                Do While rd1.Read
                    If rd1.HasRows Then
                        myfolio = rd1("Codigo").ToString
                        mynombre = rd1("Nombre").ToString
                        myunidad = rd1("UCompra").ToString
                        mycantidad = rd1("Cantidad").ToString
                        mymodelo = rd1("Modelo").ToString

                        grdCaptura.Rows.Add(myfolio, mynombre, myunidad, mycantidad, mymodelo, "0")
                    End If
                Loop
                rd1.Close()
                cnn1.Close()
            Else
                txtContra.Focus.Equals(True)
            End If

        End If
    End Sub

    Private Sub cboModelo_DropDown(sender As Object, e As EventArgs) Handles cboModelo.DropDown
        Try
            cboModelo.Items.Clear()
            cnn5.Close() : cnn5.Open()
            cmd5 = cnn5.CreateCommand
            cmd5.CommandText = "SELECT DISTINCT Modelo FROM comprasentalla WHERE Entalle=0 AND Modelo<>'' ORDER BY Modelo"
            rd5 = cmd5.ExecuteReader
            Do While rd5.Read
                If rd5.HasRows Then
                    cboModelo.Items.Add(rd5(0).ToString)
                End If
            Loop
            rd5.Close()
            cnn5.Close()

        Catch ex As Exception
            MessageBox.Show(ex.ToString)
            cnn5.Close()
        End Try
    End Sub

    Private Sub cboModelo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cboModelo.KeyPress
        e.KeyChar = UCase(e.KeyChar)
        If AscW(e.KeyChar) = Keys.Enter Then
            If Len(cboModelo.Text) = 0 Then
                cboModelo.Focus.Equals(True)
                Exit Sub
            End If

            cnn1.Close() : cnn1.Open()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "SELECT Modelo FROM comprasentalla WHERE Modelo='" & cboModelo.Text & "'"
            rd1 = cmd1.ExecuteReader
            If rd1.HasRows Then
                If rd1.Read Then
                    cboRemision.Focus.Equals(True)
                End If
            Else
                MsgBox("El modelo no existe. En la tabla comprasentalla", vbInformation + vbOKOnly, titulocentral)
                cboModelo.Text = ""
                cboModelo.Focused.Equals(True)
                Exit Sub

            End If
            rd1.Close()
            cnn1.Close()
        End If
    End Sub

    Private Sub cboRemision_SelectedValueChanged(sender As Object, e As EventArgs) Handles cboRemision.SelectedValueChanged
        If cboRemision.Text <> "" Then
            Call cboRemision_KeyPress(cboRemision, New KeyPressEventArgs(ChrW(Keys.Enter)))
        End If
    End Sub

    Private Sub btnLimpiar_Click(sender As Object, e As EventArgs) Handles btnLimpiar.Click

        If Not txtCantidadmod.Text = txtSuma.Text Then
            MsgBox("La cantidad total de la compra no es igual a la suma de cada talla, revisar, realmente quiere otro modelo", vbInformation + vbOKOnly, titulocentral)
        End If

        txtCantidadmod.Text = "0.00"
        txtDiferencia.Text = "0.00"
        txtSuma.Text = "0.00"
        cboModelo.Text = ""
        cboRemision.Text = ""
        pcantidad.Visible = False
        grdCaptura.Rows.Clear()
        cboModelo.Focus.Equals(True)
    End Sub

    Private Sub txtContra_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtContra.KeyPress
        If AscW(e.KeyChar) = Keys.Enter Then

            cnn1.Close() : cnn1.Open()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "SELECT Alias,Status FROM usuarios WHERE Clave='" & txtContra.Text & "'"
            rd1 = cmd1.ExecuteReader
            If rd1.HasRows Then
                If rd1.Read Then
                    If rd1(1).ToString = 1 Then
                        lblusuario.Text = rd1(0).ToString
                    End If

                End If
            Else
                MsgBox("Contraseña incorrecta.", vbInformation + vbOKOnly, titulocentral)
                lblusuario.Text = "Contraseña"
                txtContra.Text = ""
                txtContra.Focus.Equals(True)
                Exit Sub
            End If
            rd1.Close()
            cnn1.Close()

            btnGuardar.Focus.Equals(True)
        End If
    End Sub

    Private Sub txtContra_TextChanged(sender As Object, e As EventArgs) Handles txtContra.TextChanged
        If txtContra.Text = "" Then
            lblusuario.Text = "Contraseña"
        End If
    End Sub

    Private Sub txtCantidad_Click(sender As Object, e As EventArgs) Handles txtCantidad.Click
        txtCantidad.SelectionStart = 0
        txtCantidad.SelectionLength = Len(txtCantidad.Text)
    End Sub

    Private Sub grdCaptura_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles grdCaptura.CellClick
        If grdCaptura.Rows.Count > 0 Then

            Dim celda As DataGridViewCellEventArgs = e
            Dim index As Integer = grdCaptura.CurrentRow.Index

            If celda.ColumnIndex = 3 Then
                pcantidad.Visible = True
                txtCantidad.Focus.Equals(True)
                codigoproducto = grdCaptura.Rows(index).Cells(0).Value.ToString
                bandera = 1
            End If
        End If
    End Sub

    Private Sub txtCantidad_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtCantidad.KeyPress
        If AscW(e.KeyChar) = Keys.Enter Then

            If txtCantidad.Text = "0.00" Then
                bandera = 0
            End If

            With Me.grdCaptura
                If bandera = 1 Then
                    For q As Integer = 0 To grdCaptura.Rows.Count - 1
                        If grdCaptura.Rows(q).Cells(0).Value = codigoproducto Then
                            grdCaptura.Rows(q).Cells(3).Value = txtCantidad.Text
                            grdCaptura.Rows(q).Cells(5).Value = 1

                            txtSuma.Text = CDec(txtSuma.Text) + CDec(txtCantidad.Text)
                            If txtSuma.Text > txtCantidadmod.Text Then
                                For d As Integer = 0 To grdCaptura.Rows.Count - 1
                                    grdCaptura.Rows(d).Cells(3).Value = "0"
                                    grdCaptura.Rows(d).Cells(5).Value = "0"
                                Next
                            End If

                        End If
                    Next
                End If
            End With
            txtSuma.Text = FormatNumber(txtSuma.Text, 2)

            If txtSuma.Text > txtCantidadmod.Text Then
                MsgBox("Las cantidades digitadas es mayor al total de compra pro favor revisar.", vbInformation + vbOKOnly, titulocentral)
                txtSuma.Text = "0.00"
                txtCantidad.Text = "0"
                txtCantidad.Focus.Equals(True)
            ElseIf txtSuma.Text = txtCantidadmod.Text Then
                btnGuardar.Focus.Equals(True)
            Else
                btnGuardar.Focus.Equals(True)
            End If

            pcantidad.Visible = False
            txtCantidad.Text = "0"
        End If
    End Sub

    Private Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click

        If grdCaptura.Rows.Count < 0 Then
            Exit Sub
        End If

        If lblusuario.Text = "" Then MsgBox("Ingrese la contraseña para continuar", vbInformation + vbOKOnly, titulocentral) : txtContra.Focus.Equals(True) : Exit Sub

        If Not txtCantidadmod.Text = txtSuma.Text Then
            MsgBox("La cantidad total de la compra no es igual a la suma de cada talla,revise la información.", vbInformation + vbOKOnly, titulocentral)
            Exit Sub
        End If

        If MsgBox("¿Desea gaurdar esta información?", vbInformation + vbYesNo) = vbYes Then

            Dim mycode As String = ""
            Dim mycantidad As Double = 0
            Dim mycantmod As Double = 0

            Dim mycalc As Double = 0
            Dim mycalc1 As Double = 0

            cnn1.Close() : cnn1.Open()

            For luffy As Integer = 0 To grdCaptura.Rows.Count - 1
                mycode = grdCaptura.Rows(luffy).Cells(0).Value.ToString
                mycantidad = grdCaptura.Rows(luffy).Cells(3).Value.ToString
                mycantmod = mycantidad

                cmd1 = cnn1.CreateCommand
                cmd1.CommandText = "SELECT Multiplo FROM productos WHERE Codigo='" & mycode & "'"
                rd1 = cmd1.ExecuteReader
                If rd1.HasRows Then
                    If rd1.Read Then
                        mycalc = mycantidad * IIf(rd1(0).ToString = "", 0, rd1(0).ToString)
                        mycalc1 = mycantmod * rd1(0).ToString
                    End If
                End If
                rd1.Close()

                cnn2.Close() : cnn2.Open()
                cmd2 = cnn2.CreateCommand
                cmd2.CommandText = "UPDATE productos SET Existencia=Existencia +'" & mycalc & "' WHERE Codigo='" & mycode & "'"
                cmd2.ExecuteNonQuery()

                cmd2 = cnn2.CreateCommand
                cmd2.CommandText = "UPDATE comprasdet SET Cantidad=" & mycantidad & ", CANTIDAD_MODIFICADA='" & mycantmod & "',Total=Precio * " & CDec(mycantidad) & " WHERE Codigo='" & mycode & "' AND Modelo='" & cboModelo.Text & "' AND NumRemision='" & cboRemision.Text & "'"
                cmd2.ExecuteNonQuery()
                cnn2.Close()

            Next
            cnn1.Close()

            cnn2.Close() : cnn2.Open()
            cmd2 = cnn2.CreateCommand
            cmd2.CommandText = "SELECT Entalle FROM comprasentalla WHERE modelo='" & cboModelo.Text & "' AND Entalle=0 AND NumRemision='" & cboRemision.Text & "'"
            rd2 = cmd2.ExecuteReader
            If rd2.HasRows Then
                If rd2.Read Then


                    cnn3.Close() : cnn3.Open()
                    cmd3 = cnn3.CreateCommand
                    cmd3.CommandText = "UPDATE comprasentalla SET Entalle=1 WHERE modelo='" & cboModelo.Text & "' AND Entalle=0 AND NumRemision='" & cboRemision.Text & "'"
                    cmd3.ExecuteNonQuery()
                    cnn3.Close()
                End If
            End If
            rd2.Close()

            cmd2 = cnn2.CreateCommand
            cmd2.CommandText = "SELECT * FROM comprasdet WHERE Modelo='" & cboModelo.Text & "' AND Cantidad<>0"
            rd2 = cmd2.ExecuteReader
            If rd2.HasRows Then
                If rd2.Read Then
                    cnn3.Close() : cnn3.Open()
                    cmd3 = cnn3.CreateCommand
                    cmd3.CommandText = "UPDATE comprasdet SET Entallado=1 WHERE Modelo='" & cboModelo.Text & "'"
                    cmd3.ExecuteNonQuery()
                    cnn3.Close()
                End If
            End If
            rd2.Close()
            cnn2.Close()
        End If
        btnLimpiar.PerformClick()
        cboModelo.Focus.Equals(True)
    End Sub
End Class