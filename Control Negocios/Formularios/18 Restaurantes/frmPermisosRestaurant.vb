﻿Imports MySql.Data.MySqlClient

Public Class frmPermisosRestaurant

    Dim idmesa As Integer = 0
    Dim contraa As String = ""
    Private Sub frmPermisosRestaurant_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Dim cnn2 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
        Dim rd2 As MySqlDataReader
        Dim cmd2 As MySqlCommand

        Try
            lblEquipo.Text = ObtenerNombreEquipo()
            txtToleranciaV.Text = DatosRecarga2("ToleVisor")
            txtTolerancia2.Text = DatosRecarga2("ToleVisor2")
            cnn2.Close() : cnn2.Open()

            cmd2 = cnn2.CreateCommand
            cmd2.CommandText = "SELECT Tipo FROM rutasimpresion WHERE Equipo='" & lblEquipo.Text & "' AND Formato='VISOR'"
            rd2 = cmd2.ExecuteReader
            If rd2.HasRows Then
                If rd2.Read Then
                    lblcomanda.Text = rd2("Tipo").ToString
                End If
            End If
            rd2.Close()

            cmd2 = cnn2.CreateCommand
            cmd2.CommandText = "SELECT Comensal FROM Ticket"
            rd2 = cmd2.ExecuteReader
            If rd2.HasRows Then
                If rd2.Read Then

                    If rd2(0).ToString = "1" Then
                        cbSeparadas.Checked = True
                    Else
                        cbSeparadas.Checked = False
                    End If

                End If
            End If
            rd2.Close()
            cnn2.Close()

            Dim pantallaexcel As Integer = DatosRecarga2("PantallaExtras")
            If pantallaexcel = 1 Then
                chkPantallaExtras.Checked = True
            Else
                chkPantallaExtras.Checked = False
            End If

            Dim propina As String = DatosRecarga("Propina")
            txtPorcentage.Text = propina

            Dim cobroexacto As String = DatosRecarga("CobroExacto")
            If cobroexacto = 1 Then
                cbCobroExacto.Checked = True
            Else
                cbCobroExacto.Checked = False
            End If

            Dim cobrosimplificado As String = DatosRecarga("CobroSimplificado")
            If cobrosimplificado = "1" Then
                cbCobroSimplificado.Checked = True
            Else
                cbCobroSimplificado.Checked = False
            End If

            Dim tolebillar As String = DatosRecarga("ToleBillar")
            txttolerancia.Text = tolebillar

            Dim propias As Integer = DatosRecarga2("MesasPropias")
            If propias = 1 Then
                cbMesasPropias.Checked = True
            Else
                cbMesasPropias.Checked = False
            End If

            Dim copa As Integer = DatosRecarga2("Copa")
            If copa = 1 Then
                cbCopas.Checked = True
            Else
                cbCopas.Checked = False
            End If

            Dim TIPOCOBRO As String = DatosRecarga("TipoCobroBillar")
            If TIPOCOBRO = "HORA" Then
                rbhora.Checked = True
            ElseIf TIPOCOBRO = "MINUTO" Then
                rbminuto.Checked = True
            ElseIf TIPOCOBRO = "MEDIAHORA5MIN" Then
                rbMediaHora5Min.Checked = True
            End If

            Dim sinnumcomensal As String = DatosRecarga("SinNumCoemensal")
            If sinnumcomensal = 1 Then
                chkSinComensal.Checked = True
            Else
                chkSinComensal.Checked = False
            End If

            Dim mapeo As String = DatosRecarga("Mapeo")
            If mapeo = 1 Then
                rbM.Checked = True
            Else
                rbNM.Checked = True
            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString)
            cnn2.Close()
        End Try
    End Sub

    Private Sub cboNombre_DropDown(sender As Object, e As EventArgs) Handles cboNombre.DropDown



        Dim cnn1 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
        Dim rd1 As MySqlDataReader
        Dim cmd1 As MySqlCommand

        Try
            cboNombre.Items.Clear()

            cnn1.Close() : cnn1.Open()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "SELECT DISTINCT Nombre FROM Usuarios WHERE Nombre<>'' ORDER BY Nombre"
            rd1 = cmd1.ExecuteReader
            Do While rd1.Read
                If rd1.HasRows Then
                    cboNombre.Items.Add(rd1(0).ToString)
                End If
            Loop
            rd1.Close()
            cnn1.Close()

        Catch ex As Exception
            MessageBox.Show(ex.ToString)
            cnn1.Close()
        End Try
    End Sub

    Private Sub cboNombre_SelectedValueChanged(sender As Object, e As EventArgs) Handles cboNombre.SelectedValueChanged



        Dim cnn1 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
        Dim rd1 As MySqlDataReader
        Dim cmd1 As MySqlCommand

        Try
            cnn1.Close() : cnn1.Open()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "SELECT IdEmpleado,Area,Clave FROM Usuarios WHERE Nombre='" & cboNombre.Text & "'"
            rd1 = cmd1.ExecuteReader
            If rd1.HasRows Then
                If rd1.Read Then
                    lblid_usu.Text = rd1(0).ToString
                    txtarea.Text = rd1(1).ToString
                    txtcontraR.Text = rd1(2).ToString
                    contraa = txtcontraR.Text
                End If
            End If
            rd1.Close()


            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "SELECT Precuenta,CambioM,CancelarM,CortesiaM,JuntarM,CobrarM FROM PermisosM WHERE IdEmpleado=" & lblid_usu.Text
            rd1 = cmd1.ExecuteReader
            If rd1.HasRows Then
                If rd1.Read Then

                    If rd1("Precuenta").ToString = True Then cbPrecuentas.Checked = True Else cbPrecuentas.Checked = False
                    If rd1("CambioM").ToString = True Then cbCambioM.Checked = True Else cbCambioM.Checked = False
                    If rd1("CancelarM").ToString = True Then cbCancelarComanda.Checked = True Else cbCancelarComanda.Checked = False
                    If rd1("CortesiaM").ToString = True Then cbCortesia.Checked = True Else cbCortesia.Checked = False
                    If rd1("JuntarM").ToString = True Then cbJuntar.Checked = True Else cbJuntar.Checked = False
                    If rd1("CobrarM").ToString = True Then cbCobrar.Checked = True Else cbCobrar.Checked = False
                End If
            Else
                cbPrecuentas.Checked = False
                cbCambioM.Checked = False
                cbCancelarComanda.Checked = False
                cbCortesia.Checked = False
                cbJuntar.Checked = False
                cbCobrar.Checked = False

            End If
            rd1.Close()
            cnn1.Close()

        Catch ex As Exception
            MessageBox.Show(ex.ToString)
            cnn1.Close()
        End Try

    End Sub

    Private Sub Label12_MouseHover(sender As Object, e As EventArgs) Handles Label12.MouseHover
        txtcontraR.PasswordChar = ""
    End Sub

    Private Sub Label12_MouseLeave(sender As Object, e As EventArgs) Handles Label12.MouseLeave
        txtcontraR.PasswordChar = "*"
    End Sub

    Private Sub txtcontraseña_Click(sender As Object, e As EventArgs) Handles txtcontraseña.Click
        txtcontraseña.SelectionStart = 0
        txtcontraseña.SelectionLength = Len(txtcontraseña.Text)
    End Sub

    Private Sub txtcontraseña_GotFocus(sender As Object, e As EventArgs) Handles txtcontraseña.GotFocus
        txtcontraseña.SelectionStart = 0
        txtcontraseña.SelectionLength = Len(txtcontraseña.Text)
    End Sub

    Private Sub txtcontraseña_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtcontraseña.KeyPress
        If AscW(e.KeyChar) = Keys.Enter Then
            If txtcontraseña.Text <> "" Then

                Dim cnn1 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
                Dim rd1 As MySqlDataReader
                Dim cmd1 As MySqlCommand

                Dim id_empleado As Integer = 0
                Try
                    cnn1.Close() : cnn1.Open()
                    cmd1 = cnn1.CreateCommand
                    cmd1.CommandText = "SELECT Area,IdEmpleado,Alias FROM Usuarios WHERE Clave='" & txtcontraseña.Text & "'"
                    rd1 = cmd1.ExecuteReader
                    If rd1.HasRows Then
                        If rd1.Read Then
                            If rd1("Area").ToString = "ADMINISTRACIÓN" Or rd1("Area").ToString = "OPERACIÓN" Then
                                id_empleado = rd1("IdEmpleado").ToString
                                lblusuario.Text = rd1("Alias").ToString
                            Else
                                MsgBox("Solo los administradores pueden modificar los permisos", vbInformation + vbOKOnly, titulorestaurante)
                                txtcontraR.Text = ""
                                txtcontraR.Focus.Equals(True)
                                Exit Sub
                            End If

                        End If
                    End If
                    rd1.Close()

                    cmd1 = cnn1.CreateCommand
                    cmd1.CommandText = "SELECT NumPart FROM Formatos WHERE Facturas='Restaurante'"
                    rd1 = cmd1.ExecuteReader
                    If rd1.HasRows Then
                        If rd1.Read Then

                            If rd1(0).ToString = 1 Then
                                btnguardar.Focus.Equals(True)
                            Else
                                MsgBox("No cuentas con permisos suficientes para interactuar con este formulario.", vbInformation + vbOKOnly, titulomensajes)
                                rd1.Close() : cnn1.Close() : Exit Sub
                            End If

                        End If
                    End If
                    rd1.Close()
                    cnn1.Close()

                Catch ex As Exception
                    MessageBox.Show(ex.ToString)
                    cnn1.Close()
                End Try

            End If
            btnguardar.Focus.Equals(True)
        End If
    End Sub

    Private Sub btnnuevo_Click(sender As Object, e As EventArgs) Handles btnnuevo.Click

        contraa = ""
        cboNombre.Text = ""
        txtarea.Text = ""
        txtcontraR.Text = ""
        lblusuario.Text = ""
        cbCambioM.Checked = False
        cbPrecuentas.Checked = False
        cbCancelarComanda.Checked = False
        cbCortesia.Checked = False
        cbJuntar.Checked = False
        cbCobrar.Checked = False
        cbSeparadas.Checked = False
        chkSinComensal.Checked = False
        cbCopas.Checked = False
    End Sub

    Private Sub btnguardar_Click(sender As Object, e As EventArgs) Handles btnguardar.Click
        If cboNombre.Text = "" Then MsgBox("Selecciona un usuario para poder asignarle permisos.", vbInformation + vbOKOnly, "Delsscom Control Negocios Pro") : cboNombre.Focus().Equals(True) : Exit Sub
        If txtcontraR.Text = "" Then MsgBox("Asígnale una contraseña al usuario para continuar.", vbInformation + vbOK, "Delsscom Control Negocios Pro") : txtcontraR.Focus().Equals(True) : Exit Sub

        If lblusuario.Text = "" Then MsgBox("Debe ingresar una contraseña para continuar.", vbInformation + vbOKOnly, titulomensajes) : lblusuario.Focus.Equals(True) : Exit Sub

        If MsgBox("¿Deseas registrar los permisos de este empleado?", vbInformation + vbOKCancel, "Delsscom Control Negocios Pro") = vbOK Then
            Dim id_emp As Double = 0

            Dim cnn1 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
            Dim cnn2 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
            Dim rd1, rd2 As MySqlDataReader
            Dim cmd1, cmd2 As MySqlCommand

            Try
                cnn1.Close() : cnn1.Open()
                cnn2.Close() : cnn2.Open()

                cmd1 = cnn1.CreateCommand
                cmd1.CommandText =
                    "SELECT IdEmpleado FROM Usuarios WHERE Clave='" & txtcontraR.Text & "'"
                rd1 = cmd1.ExecuteReader
                If rd1.HasRows Then
                    If rd1.Read Then
                        If rd1("IdEmpleado").ToString = lblid_usu.Text Then
                        Else
                            MsgBox("Ya cuentas con un usuario registrado bajo esa contraseña.", vbInformation + vbOKOnly, titulomensajes)
                            rd1.Close() : cnn1.Close()
                            txtcontraR.SelectAll()
                            Exit Sub
                        End If
                    End If
                End If
                rd1.Close()

                cmd1 = cnn1.CreateCommand
                cmd1.CommandText =
                    "SELECT IdEmpleado FROM Usuarios WHERE Nombre='" & cboNombre.Text & "'"
                rd1 = cmd1.ExecuteReader
                If rd1.HasRows Then
                    If rd1.Read Then
                        id_emp = rd1("IdEmpleado").ToString
                    End If
                End If
                rd1.Close()

                cmd1 = cnn1.CreateCommand
                cmd1.CommandText =
                    "select IdEmpleado from PermisosM where IdEmpleado=" & id_emp
                rd1 = cmd1.ExecuteReader
                If rd1.HasRows Then
                    If rd1.Read Then

                        cmd2 = cnn2.CreateCommand
                        cmd2.CommandText = "UPDATE PermisosM set Precuenta=" & IIf(cbPrecuentas.Checked = True, 1, 0) & ",CambioM=" & IIf(cbCambioM.Checked = True, 1, 0) & ",CancelarM=" & IIf(cbCancelarComanda.Checked = True, 1, 0) & ",CortesiaM=" & IIf(cbCortesia.Checked = True, 1, 0) & ",JuntarM=" & IIf(cbJuntar.Checked = True, 1, 0) & ",CobrarM=" & IIf(cbCobrar.Checked = True, 1, 0) & " WHERE IdEmpleado=" & id_emp

                        If cmd2.ExecuteNonQuery() Then
                            MsgBox("Permisos actualizados correctamente", vbInformation + vbOKOnly, "Delsscom® Control Negocios Pro")
                        End If

                        cmd2 = cnn2.CreateCommand
                        cmd2.CommandText =
                            "update Usuarios set Clave='" & txtcontraR.Text & "' where IdEmpleado=" & lblid_usu.Text
                        cmd2.ExecuteNonQuery()
                        cnn2.Close()

                    End If
                Else

                    cmd2 = cnn2.CreateCommand
                    cmd2.CommandText = "INSERT INTO PermisosM(IdEmpleado,Precuenta,CambioM,CancelarM,CortesiaM,JuntarM,CobrarM) VALUES(" & id_emp & "," & IIf(cbPrecuentas.Checked = True, 1, 0) & "," & IIf(cbCambioM.Checked = True, 1, 0) & "," & IIf(cbCancelarComanda.Checked = True, 1, 0) & "," & IIf(cbCortesia.Checked = True, 1, 0) & "," & IIf(cbJuntar.Checked = True, 1, 0) & "," & IIf(cbCobrar.Checked = True, 1, 0) & ")"
                    If cmd2.ExecuteNonQuery() Then
                        MsgBox("Permisos insertados correctamente", vbInformation + vbOKOnly, "Delsscom® Control Negocios Pro")
                    End If

                    cmd2 = cnn2.CreateCommand
                    cmd2.CommandText =
                        "update Usuarios set Clave='" & txtcontraR.Text & "' where IdEmpleado=" & lblid_usu.Text
                    cmd2.ExecuteNonQuery()
                    cnn2.Close()
                End If
                rd1.Close()
                cnn1.Close()
                cnn2.Close()

                btnnuevo.PerformClick()
            Catch ex As Exception
                MessageBox.Show(ex.ToString)
                cnn1.Close()
            End Try
        End If
    End Sub

    Private Sub cbSeparadas_CheckedChanged(sender As Object, e As EventArgs) Handles cbSeparadas.CheckedChanged

        Dim valor As Integer = 0

        If (cbSeparadas.Checked) Then
            valor = 1
        Else
            valor = 0
        End If
        Dim cnn1 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
        Dim cmd1 As MySqlCommand


        cnn1.Close() : cnn1.Open()
        cmd1 = cnn1.CreateCommand
        cmd1.CommandText = "UPDATE Ticket SET Comensal=" & valor & ""
        cmd1.ExecuteNonQuery()
        cnn1.Close()

    End Sub

    Private Sub btnporcentage_Click(sender As Object, e As EventArgs) Handles btnporcentage.Click
        If txtPorcentage.Text = "" Then Exit Sub

        Dim cnn1 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
        Dim cnn2 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
        Dim rd1, rd2 As MySqlDataReader
        Dim cmd1, cmd2 As MySqlCommand

        cnn1.Close() : cnn1.Open()
        cnn2.Close() : cnn2.Open()
        cmd1 = cnn1.CreateCommand
        cmd1.CommandText = "select Facturas,NotasCred,NumPart from Formatos where Facturas='Propina'"
        rd1 = cmd1.ExecuteReader
        If rd1.HasRows Then
            If rd1.Read Then

                cmd2 = cnn2.CreateCommand
                cmd2.CommandText = "update Formatos set NotasCred='" & txtPorcentage.Text & "' where Facturas='Propina'"
                If cmd2.ExecuteNonQuery() Then
                    MsgBox("Datos actualizados correctamente", vbInformation + vbOKOnly, titulomensajes)
                End If
            End If
        Else
            cmd2 = cnn2.CreateCommand
            cmd2.CommandText = "insert into Formatos(Facturas,NotasCred,NumPart) values('Propina','" & txtPorcentage.Text & "',0)"
            If cmd2.ExecuteNonQuery() Then
                MsgBox("Datos guardados correctamente", vbInformation + vbOKOnly, titulomensajes)
            End If

        End If
        rd1.Close()
        cnn1.Close()
    End Sub

    Private Sub btnguardartolerancia_Click(sender As Object, e As EventArgs) Handles btnguardartolerancia.Click

        Dim cnn1 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
        Dim cnn2 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
        Dim rd1, rd2 As MySqlDataReader
        Dim cmd1, cmd2 As MySqlCommand

        Try
            cnn1.Close() : cnn1.Open()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "SELECT Facturas FROM Formatos WHERE Facturas='ToleBillar'"
            rd1 = cmd1.ExecuteReader
            If rd1.HasRows Then
                If rd1.Read Then
                    cnn2.Close() : cnn2.Open()
                    cmd2 = cnn2.CreateCommand
                    cmd2.CommandText = "UPDATE Formatos SET NotasCred='" & txttolerancia.Text & "' WHERE Facturas='ToleBillar'"
                    cmd2.ExecuteNonQuery()
                    cnn2.Close()
                End If
            Else
                cnn2.Close() : cnn2.Open()
                cmd2 = cnn2.CreateCommand
                cmd2.CommandText = "INSERT INTO Formatos(Facturas,NotasCred,NumPart)VALUES('ToleBillar','" & txttolerancia.Text & "','0')"
                cmd2.ExecuteNonQuery()
                cnn2.Close()
            End If
            rd1.Close()
            cnn1.Close()
            MsgBox("Tolerancia agregada corectamente", vbInformation + vbOKOnly, titulorestaurante)
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
            cnn1.Close()
        End Try
    End Sub

    Public Sub CambioTipoBillar()

        Dim cnn1 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
        Dim cnn2 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
        Dim rd1, rd2 As MySqlDataReader
        Dim cmd1, cmd2 As MySqlCommand
        Try

            If (rbhora.Checked) Then

                cnn1.Close() : cnn1.Open()
                cmd1 = cnn1.CreateCommand
                cmd1.CommandText = "SELECT Facturas FROM Formatos WHERE Facturas='TipoCobroBillar'"
                rd1 = cmd1.ExecuteReader
                If rd1.HasRows Then
                    If rd1.Read Then
                        cnn2.Close() : cnn2.Open()
                        cmd2 = cnn2.CreateCommand
                        cmd2.CommandText = "UPDATE Formatos SET NotasCred='HORA' WHERE Facturas='TipoCobroBillar'"
                        cmd2.ExecuteNonQuery()
                        cnn2.Close()
                    End If
                Else
                    cnn2.Close() : cnn2.Open()
                    cmd2 = cnn2.CreateCommand
                    cmd2.CommandText = "Insert INTO Formatos(Facturas,NotasCred,NumPart) VALUES('TipoCobroBillar','HORA','0')"
                    cmd2.ExecuteNonQuery()
                    cnn2.Close()
                End If
                rd1.Close()
                cnn1.Close()

            ElseIf (rbminuto.Checked) Then

                cnn1.Close() : cnn1.Open()
                cmd1 = cnn1.CreateCommand
                cmd1.CommandText = "SELECT Facturas FROM Formatos WHERE Facturas='TipoCobroBillar'"
                rd1 = cmd1.ExecuteReader
                If rd1.HasRows Then
                    If rd1.Read Then
                        cnn2.Close() : cnn2.Open()
                        cmd2 = cnn2.CreateCommand
                        cmd2.CommandText = "UPDATE Formatos SET NotasCred='MINUTO' WHERE Facturas='TipoCobroBillar'"
                        cmd2.ExecuteNonQuery()
                        cnn2.Close()
                    End If
                Else
                    cnn2.Close() : cnn2.Open()
                    cmd2 = cnn2.CreateCommand
                    cmd2.CommandText = "Insert INTO Formatos(Facturas,NotasCred,NumPart) VALUES('TipoCobroBillar','MINUTOS','0')"
                    cmd2.ExecuteNonQuery()
                    cnn2.Close()
                End If
                rd1.Close()
                cnn1.Close()
            ElseIf (rbMediaHora5Min.Checked) Then

                cnn1.Close() : cnn1.Open()
                cmd1 = cnn1.CreateCommand
                cmd1.CommandText = "SELECT Facturas FROM Formatos WHERE Facturas='TipoCobroBillar'"
                rd1 = cmd1.ExecuteReader
                If rd1.HasRows Then
                    If rd1.Read Then
                        cnn2.Close() : cnn2.Open()
                        cmd2 = cnn2.CreateCommand
                        cmd2.CommandText = "UPDATE Formatos SET NotasCred='MEDIAHORA5MIN' WHERE Facturas='TipoCobroBillar'"
                        cmd2.ExecuteNonQuery()
                        cnn2.Close()
                    End If
                Else
                    cnn2.Close() : cnn2.Open()
                    cmd2 = cnn2.CreateCommand
                    cmd2.CommandText = "Insert INTO Formatos(Facturas,NotasCred,NumPart) VALUES('TipoCobroBillar','MEDIAHORA5MIN','0')"
                    cmd2.ExecuteNonQuery()
                    cnn2.Close()
                End If
                rd1.Close()
                cnn1.Close()
            End If



        Catch ex As Exception
            MessageBox.Show(ex.ToString)
            cnn1.Close()
            cnn2.Close()
        End Try

    End Sub

    Private Sub rbhora_Click(sender As Object, e As EventArgs) Handles rbhora.Click
        CambioTipoBillar()
    End Sub

    Private Sub rbminuto_Click(sender As Object, e As EventArgs) Handles rbminuto.Click
        CambioTipoBillar()
    End Sub

    Private Sub chkSinComensal_CheckedChanged(sender As Object, e As EventArgs) Handles chkSinComensal.CheckedChanged

        Dim cnn1 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
        Dim cnn2 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
        Dim rd1, rd2 As MySqlDataReader
        Dim cmd1, cmd2 As MySqlCommand
        Try
            Dim sincomensal As Integer = 0

            If (chkSinComensal.Checked) Then
                sincomensal = 1
            Else
                sincomensal = 0
            End If

            cnn1.Close() : cnn1.Open()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "SELECT Facturas FROM Formatos WHERE Facturas='SinNumCoemensal'"
            rd1 = cmd1.ExecuteReader
            If rd1.HasRows Then
                If rd1.Read Then
                    cnn2.Close() : cnn2.Open()
                    cmd2 = cnn2.CreateCommand
                    cmd2.CommandText = "UPDATE Formatos SET NotasCred='" & sincomensal & "' WHERE Facturas='SinNumCoemensal'"
                    cmd2.ExecuteNonQuery()
                    cnn2.Close()
                End If
            Else
                cnn2.Close() : cnn2.Open()
                cmd2 = cnn2.CreateCommand
                cmd2.CommandText = "INSERT INTO Formatos(Facturas,NotasCred,NumPart) VALUES('SinNumCoemensal','" & sincomensal & "','0')"
                cmd2.ExecuteNonQuery()
                cnn2.Close()
            End If
            rd1.Close()
            cnn1.Close()
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
            cnn1.Close()
        End Try
    End Sub

    Private Sub cboImpre_Comanda_DropDown(sender As Object, e As EventArgs) Handles cboImpre_Comanda.DropDown
        cboImpre_Comanda.Items.Clear()
        For Each printer As String In Drawing.Printing.PrinterSettings.InstalledPrinters
            cboImpre_Comanda.Items.Add(printer)
        Next
    End Sub

    Private Sub Button24_Click(sender As Object, e As EventArgs) Handles Button24.Click

        Dim cnn1 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
        Dim cnn2 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
        Dim rd1, rd2 As MySqlDataReader
        Dim cmd1, cmd2 As MySqlCommand

        Try
            cnn1.Close() : cnn1.Open()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "SELECT Tipo FROM rutasimpresion WHERE Tipo='UNICO'"
            rd1 = cmd1.ExecuteReader
            If rd1.HasRows Then
                If rd1.Read Then
                    cnn2.Close() : cnn2.Open()
                    cmd2 = cnn2.CreateCommand
                    cmd2.CommandText = "UPDATE rutasimpresion SET Equipo='" & ObtenerNombreEquipo() & "', Impresora='" & cboImpre_Comanda.Text & "' WHERE Tipo='UNICO'"
                    If cmd2.ExecuteNonQuery() Then
                        MsgBox("Impresora actualizada correctamente", vbInformation + vbOKOnly, titulomensajes)
                    End If
                    cnn2.Close()

                End If
            Else
                cnn2.Close() : cnn2.Open()
                cmd2 = cnn2.CreateCommand
                cmd2.CommandText = "INSERT INTO rutasimpresion(Equipo,Tipo,Formato,Impresora)VALUES('" & ObtenerNombreEquipo() & "','UNICO','','" & cboImpre_Comanda.Text & "')"
                If cmd2.ExecuteNonQuery() Then
                    MsgBox("Impresora agregada correctaente", vbInformation + vbOKOnly, titulomensajes)
                End If
                cnn2.Close()
            End If
            rd1.Close()
            cnn1.Close()

        Catch ex As Exception
            MessageBox.Show(ex.ToString)
            cnn1.Close()
        End Try
    End Sub

    Private Sub cboNombre_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cboNombre.KeyPress
        If AscW(e.KeyChar) = Keys.Enter Then
            txtcontraseña.Focus.Equals(True)
        End If
    End Sub

    Private Sub btnclose_Click(sender As Object, e As EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub cbMesasPropias_CheckedChanged(sender As Object, e As EventArgs) Handles cbMesasPropias.CheckedChanged

        Dim cnn1 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
        Dim rd1 As MySqlDataReader
        Dim cmd1 As MySqlCommand

        Try
            If (cbMesasPropias.Checked) Then
                cnn1.Close() : cnn1.Open()
                cmd1 = cnn1.CreateCommand
                cmd1.CommandText = "UPDATE Formatos SET NotasCred='1',NumPart='1' WHERE Facturas='MesasPropias'"
                cmd1.ExecuteNonQuery()
                cnn1.Close()
            Else
                cnn1.Close() : cnn1.Open()
                cmd1 = cnn1.CreateCommand
                cmd1.CommandText = "UPDATE Formatos SET NotasCred='0',NumPart='0' WHERE Facturas='MesasPropias'"
                cmd1.ExecuteNonQuery()
                cnn1.Close()

            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
    End Sub

    Private Sub cbCopas_CheckedChanged(sender As Object, e As EventArgs) Handles cbCopas.CheckedChanged
        Dim cnn1 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
        Dim rd1 As MySqlDataReader
        Dim cmd1 As MySqlCommand
        Try
            If (cbCopas.Checked) Then
                cnn1.Close() : cnn1.Open()
                cmd1 = cnn1.CreateCommand
                cmd1.CommandText = "UPDATE Formatos SET NotasCred='1',NumPart='1' WHERE Facturas='Copa'"
                cmd1.ExecuteNonQuery()
                cnn1.Close()
            Else
                cnn1.Close() : cnn1.Open()
                cmd1 = cnn1.CreateCommand
                cmd1.CommandText = "UPDATE Formatos SET NotasCred='0',NumPart='0' WHERE Facturas='Copa'"
                cmd1.ExecuteNonQuery()
                cnn1.Close()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
            cnn1.Close()
        End Try
    End Sub


    Public Sub CambiodeMesa()
        Dim cnn1 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
        Dim cnn2 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
        Dim rd1, rd2 As MySqlDataReader
        Dim cmd1, cmd2 As MySqlCommand
        Try
            If (rbM.Checked) Then
                cnn1.Close() : cnn1.Open()
                cmd1 = cnn1.CreateCommand
                cmd1.CommandText = "SELECT Facturas FROM formatos WHERE Facturas='Mapeo'"
                rd1 = cmd1.ExecuteReader
                If rd1.HasRows Then
                    If rd1.Read Then
                        cnn2.Close() : cnn2.Open()
                        cmd2 = cnn2.CreateCommand
                        cmd2.CommandText = "UPDATE formatos SET Facturas='Mapeo',NotasCred='1',NumPart=0 WHERE Facturas='Mapeo'"
                        cmd2.ExecuteNonQuery()
                        cnn2.Close()
                    End If
                Else
                    cnn2.Close() : cnn2.Open()
                    cmd2 = cnn2.CreateCommand
                    cmd2.CommandText = "INSERT INTO formatos(Facturas,NotasCred,NumPart) VALUES('Mapeo','1',0)"
                    cmd2.ExecuteNonQuery()
                    cnn2.Close()
                End If
                rd1.Close()
                cnn1.Close()

            End If

            If (rbNM.Checked) Then

                cnn1.Close() : cnn1.Open()
                cmd1 = cnn1.CreateCommand
                cmd1.CommandText = "SELECT Facturas FROM formatos WHERE Facturas='Mapeo'"
                rd1 = cmd1.ExecuteReader
                If rd1.HasRows Then
                    If rd1.Read Then

                        cnn2.Close() : cnn2.Open()
                        cmd2 = cnn2.CreateCommand
                        cmd2.CommandText = "UPDATE formatos SET Facturas='Mapeo',NotasCred='0',NumPart=0 WHERE Facturas='Mapeo'"
                        cmd2.ExecuteNonQuery()
                        cnn2.Close()

                    End If
                Else
                    cnn2.Close() : cnn2.Open()
                    cmd2 = cnn2.CreateCommand
                    cmd2.CommandText = "INSERT INTO formatos(Facturas,NotasCred,NumPart) VALUES('Mapeo','0',0)"
                    cmd2.ExecuteNonQuery()
                    cnn2.Close()
                End If
                rd1.Close()
                cnn1.Close()

            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
    End Sub

    Private Sub rbM_Click(sender As Object, e As EventArgs) Handles rbM.Click
        CambiodeMesa()
    End Sub

    Private Sub rbNM_Click(sender As Object, e As EventArgs) Handles rbNM.Click
        CambiodeMesa()
    End Sub



    Private Sub cboMesa_DropDown(sender As Object, e As EventArgs) Handles cboMesa.DropDown
        Dim cnn5 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
        Dim rd5 As MySqlDataReader
        Dim cmd5 As MySqlCommand
        Try
            cboMesa.Items.Clear()
            cnn5.Close() : cnn5.Open()
            cmd5 = cnn5.CreateCommand
            cmd5.CommandText = "SELECT DISTINCT Nombre_mesa FROM mesa WHERE Nombre_mesa<>'' AND temporal=0 ORDER BY Nombre_mesa"
            rd5 = cmd5.ExecuteReader
            Do While rd5.Read
                If rd5.HasRows Then
                    cboMesa.Items.Add(rd5(0).ToString)
                End If
            Loop
            rd5.Close()
            cnn5.Close()

        Catch ex As Exception
            MessageBox.Show(ex.ToString)
            cnn5.Close()
        End Try
    End Sub

    Private Sub cboMesa_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cboMesa.KeyPress
        e.KeyChar = UCase(e.KeyChar)
        If AscW(e.KeyChar) = Keys.Enter Then
            cboUbicacion.Focus.Equals(True)
        End If
    End Sub

    Private Sub cboUbicacion_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cboUbicacion.KeyPress
        e.KeyChar = UCase(e.KeyChar)
        If AscW(e.KeyChar) = Keys.Enter Then
            cboPara.Focus.Equals(True)
        End If
    End Sub

    Private Sub cboUbicacion_DropDown(sender As Object, e As EventArgs) Handles cboUbicacion.DropDown
        Dim cnn5 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
        Dim rd5 As MySqlDataReader
        Dim cmd5 As MySqlCommand

        Try
            cboUbicacion.Items.Clear()
            cnn5.Close() : cnn5.Open()
            cmd5 = cnn5.CreateCommand
            cmd5.CommandText = "SELECT DISTINCT Ubicacion FROM mesa WHERE Ubicacion<>'' ORDER BY Ubicacion"
            rd5 = cmd5.ExecuteReader
            Do While rd5.Read
                If rd5.HasRows Then
                    cboUbicacion.Items.Add(rd5(0).ToString)
                End If
            Loop
            rd5.Close()
            cnn5.Close()
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
            cnn5.Close()
        End Try
    End Sub

    Private Sub cboPara_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cboPara.KeyPress
        If AscW(e.KeyChar) = Keys.Enter Then
            txtPrecio.Focus.Equals(True)
        End If
    End Sub

    Private Sub txtPrecio_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPrecio.KeyPress
        If AscW(e.KeyChar) = Keys.Enter Then
            If IsNumeric(txtPrecio.Text) Then
                btnGM.Focus.Equals(True)
            End If
        End If
    End Sub

    Private Sub btnNM_Click(sender As Object, e As EventArgs) Handles btnNM.Click
        cboMesa.Text = ""
        cboUbicacion.Text = ""
        cboPara.Text = ""
        txtPrecio.Text = "0.00"
        cbTiempo.Checked = False
        cboMesa.Focus.Equals(True)
    End Sub

    Private Sub cboMesa_SelectedValueChanged(sender As Object, e As EventArgs) Handles cboMesa.SelectedValueChanged

        Dim cnn1 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
        Dim rd1 As MySqlDataReader
        Dim cmd1 As MySqlCommand

        Try
            Dim timepo As Integer = 0
            cnn1.Close() : cnn1.Open()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "SELECT IdMesa,Ubicacion,Tipo,precio,Contabiliza FROM mesa WHERE Nombre_mesa='" & cboMesa.Text & "'"
            rd1 = cmd1.ExecuteReader
            If rd1.HasRows Then
                If rd1.Read Then
                    idmesa = rd1("IdMesa").ToString
                    cboUbicacion.Text = rd1("Ubicacion").ToString
                    cboPara.Text = rd1("Tipo").ToString
                    txtPrecio.Text = rd1("precio").ToString
                    timepo = rd1("Contabiliza").ToString

                    If timepo = 1 Then
                        cbTiempo.Checked = True
                    Else
                        cbTiempo.Checked = False
                    End If

                End If
            End If
            rd1.Close()
            cnn1.Close()

        Catch ex As Exception
            MessageBox.Show(ex.ToString)
            cnn1.Close()
        End Try
    End Sub

    Private Sub btnGM_Click(sender As Object, e As EventArgs) Handles btnGM.Click

        Dim cnn1 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
        Dim cnn2 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
        Dim rd1, rd2 As MySqlDataReader
        Dim cmd1, cmd2 As MySqlCommand

        Try
            If cboMesa.Text = "" Then MsgBox("Debe ingresar el nombre de la mesa", vbInformation + vbOKOnly, titulorestaurante) : cboMesa.Focus.Equals(True) : Exit Sub
            If cboUbicacion.Text = "" Then MsgBox("Debe ingresar la ubiación de la mesa", vbInformation + vbOKOnly, titulorestaurante) : cboUbicacion.Focus.Equals(True) : Exit Sub

            Dim ORDEN As Integer = 0
            Dim TIEMPO As Integer = 0

            If (cbTiempo.Checked) Then
                TIEMPO = 1
            Else
                TIEMPO = 0
            End If

            cnn1.Close() : cnn1.Open()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "SELECT MAX(Orden) FROM mesa"
            rd1 = cmd1.ExecuteReader
            If rd1.HasRows Then
                If rd1.Read Then
                    ORDEN = IIf(rd1(0).ToString = "", 0, rd1(0).ToString) + 1
                Else
                    ORDEN = 1
                End If
            Else
                ORDEN = 1
            End If
            rd1.Close()

            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "SELECT IdMesa FROM Mesa WHERE IdMesa=" & idmesa & ""
            rd1 = cmd1.ExecuteReader
            If rd1.HasRows Then
                If rd1.Read Then
                    cnn2.Close() : cnn2.Open()
                    cmd2 = cnn2.CreateCommand
                    cmd2.CommandText = "UPDATE mesa SET Temporal=0, Contabiliza=" & TIEMPO & ",Precio=" & txtPrecio.Text & ",Ubicacion='" & cboUbicacion.Text & "',Tipo='" & cboPara.Text & "',Nombre_mesa='" & cboMesa.Text & "' WHERE IdMesa='" & idmesa & "'"
                    cmd2.ExecuteNonQuery()

                    cmd2 = cnn2.CreateCommand
                    cmd2.CommandText = "UPDATE mesasxempleados SET Mesa='" & cboMesa.Text & "' WHERE IdMesa=" & idmesa & ""
                    If cmd2.ExecuteNonQuery() Then
                        MsgBox("Mesa actualizada correctamente", vbInformation + vbOKOnly, titulorestaurante)
                    End If
                    cnn2.Close()
                End If
            Else
                cnn2.Close() : cnn2.Open()
                cmd2 = cnn2.CreateCommand
                cmd2.CommandText = "INSERT INTO mesa(Nombre_mesa,Temporal,Status,Contabiliza,Precio,Orden,TempNom,IdEmpleado,Mesero,Ubicacion,X,Y,Tipo,Impresion) VALUES('" & cboMesa.Text & "',0,'Desocupada'," & TIEMPO & "," & txtPrecio.Text & "," & ORDEN & ",'',0,'','" & cboUbicacion.Text & "',0,0,'" & cboPara.Text & "',0)"
                cmd2.ExecuteNonQuery()

                cmd2 = cnn2.CreateCommand
                cmd2.CommandText = "INSERT INTO mesasxempleados(Mesa,IdEmpleado,Grupo,Temporal) VALUES('" & cboMesa.Text & "',0,'',0)"
                If cmd2.ExecuteNonQuery() Then
                    MsgBox("Mesa agregada correctamente", vbInformation + vbOKOnly, titulorestaurante)
                End If
                cnn2.Close()
            End If
            rd1.Close()
            cnn1.Close()

            btnNM.PerformClick()
            frmMesas.btnLimpiar.PerformClick()
            cboMesa.Focus.Equals(True)

        Catch ex As Exception
            MessageBox.Show(ex.ToString)
            cnn1.Close()
            cnn2.Close()
        End Try
    End Sub

    Private Sub btnEM_Click(sender As Object, e As EventArgs) Handles btnEM.Click

        If MsgBox("¿Desea eliminar la mesa seleccionada?", vbInformation + vbYesNo, titulorestaurante) = vbYes Then

            Dim cnn1 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
            Dim cnn2 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)

            Dim rd1, rd2 As MySqlDataReader
            Dim cmd1, cmd2 As MySqlCommand

            Try
                cnn1.Close() : cnn1.Open()
                cmd1 = cnn1.CreateCommand
                cmd1.CommandText = "SELECT IdMesa FROM mesa WHERE Nombre_mesa='" & cboMesa.Text & "'"
                rd1 = cmd1.ExecuteReader
                If rd1.HasRows Then
                    If rd1.Read Then
                        cnn2.Close() : cnn2.Open()
                        cmd2 = cnn2.CreateCommand
                        cmd2.CommandText = "DELETE FROM Mesa WHERE Nombre_mesa='" & cboMesa.Text & "' AND Ubicacion='" & cboUbicacion.Text & "'"
                        cmd2.ExecuteNonQuery()

                        cmd2 = cnn2.CreateCommand
                        cmd2.CommandText = "DELETE FROM mesasxempleados WHERE Mesa='" & cboMesa.Text & "'"
                        cmd2.ExecuteNonQuery()
                        cnn2.Close()
                    End If
                End If
                rd1.Close()
                cnn1.Close()
            Catch ex As Exception
                MessageBox.Show(ex.ToString)
                cnn1.Close()
            End Try

        End If

    End Sub

    Private Sub cbCobroExacto_CheckedChanged(sender As Object, e As EventArgs) Handles cbCobroExacto.CheckedChanged

        Dim cnn1 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
        Dim cnn3 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
        Dim rd1, rd3 As MySqlDataReader
        Dim cmd1, cmd3 As MySqlCommand

        Try
            Dim cobro As Integer = 0

            If (cbCobroExacto.Checked) Then
                cobro = 1
            Else
                cobro = 0
            End If

            cnn1.Close() : cnn1.Open()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "SELECT Facturas FROM Formatos WHERE Facturas='CobroExacto'"
            rd1 = cmd1.ExecuteReader
            If rd1.HasRows Then
                If rd1.Read Then
                    cnn3.Close() : cnn3.Open()
                    cmd3 = cnn3.CreateCommand
                    cmd3.CommandText = "UPDATE Formatos SET NotasCred='" & cobro & "' WHERE Facturas='CobroExacto'"
                    cmd3.ExecuteNonQuery()
                    cnn3.Close()
                End If
            Else
                cnn3.Close() : cnn3.Open()
                cmd3 = cnn3.CreateCommand
                cmd3.CommandText = "INSERT INTO Formatos(Facturas,NotasCred,NumPart) VALUES('CobroExacto','" & cobro & "','0')"
                cmd3.ExecuteNonQuery()
                cnn3.Close()
            End If
            rd1.Close()
            cnn1.Close()
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
            cnn1.Close()
        End Try
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        frmTecladoPersonas.Show()
        frmTecladoPersonas.BringToFront()
    End Sub

    Private Sub cbCobroSimplificado_Click(sender As Object, e As EventArgs) Handles cbCobroSimplificado.Click

        Dim cnn1 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
        Dim cnn3 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)

        Dim rd1, rd3 As MySqlDataReader
        Dim cmd1, cmd3 As MySqlCommand

        Try
            Dim simple As Integer = 0

            If (cbCobroSimplificado.Checked) Then
                simple = 1
            Else
                simple = 0
            End If

            cnn1.Close() : cnn1.Open()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "SELECT Facturas FROM Formatos WHERE Facturas='CobroSimplificado'"
            rd1 = cmd1.ExecuteReader
            If rd1.HasRows Then
                If rd1.Read Then
                    cnn3.Close() : cnn3.Open()
                    cmd3 = cnn3.CreateCommand
                    cmd3.CommandText = "UPDATE Formatos SET NotasCred='" & simple & "' WHERE Facturas='CobroSimplificado'"
                    cmd3.ExecuteNonQuery()
                    cnn3.Close()
                End If
            Else
                cnn3.Close() : cnn3.Open()
                cmd3 = cnn3.CreateCommand
                cmd3.CommandText = "INSERT INTO Formatos(Facturas,NotasCred,NumPart) VALUES('CobroSimplificado','" & simple & "','0')"
                cmd3.ExecuteNonQuery()
                cnn3.Close()
            End If
            rd1.Close()
            cnn1.Close()
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
            cnn1.Close()
        End Try
    End Sub



    Private Sub rbMediaHora5Min_Click(sender As Object, e As EventArgs) Handles rbMediaHora5Min.Click
        CambioTipoBillar()
    End Sub

    Private Sub chkPantallaExtras_CheckedChanged(sender As Object, e As EventArgs) Handles chkPantallaExtras.CheckedChanged

        Dim cnn1 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
        Dim cnn2 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
        Dim rd1, rd2 As MySqlDataReader
        Dim cmd1, cmd2 As MySqlCommand

        Try

            If (chkPantallaExtras.Checked) Then

                cnn1.Close() : cnn1.Open()
                cnn2.Close() : cnn2.Open()
                cmd1 = cnn1.CreateCommand
                cmd1.CommandText = "select Facturas,NotasCred,NumPart from Formatos where Facturas='PantallaExtras'"
                rd1 = cmd1.ExecuteReader
                If rd1.HasRows Then
                    If rd1.Read Then
                        cmd2 = cnn2.CreateCommand
                        cmd2.CommandText = "UPDATE Formatos SET NotasCred='1',NumPart='1' WHERE Facturas='PantallaExtras'"
                        If cmd2.ExecuteNonQuery() Then
                            'MsgBox("Configuracion Actualizada correctamente", vbInformation + vbOKOnly, titulomensajes)
                        End If
                    End If
                Else
                    cmd2 = cnn2.CreateCommand
                    cmd2.CommandText = "insert into Formatos(Facturas,NotasCred,NumPart) values('PantallaExtras','1','1')"
                    If cmd2.ExecuteNonQuery() Then
                        'MsgBox("Configuracion Guardada correctamente", vbInformation + vbOKOnly, titulomensajes)
                    End If
                End If
                rd1.Close()
                cnn1.Close()

            Else

                cnn1.Close() : cnn1.Open()
                cnn2.Close() : cnn2.Open()
                cmd1 = cnn1.CreateCommand
                cmd1.CommandText = "select Facturas,NotasCred,NumPart from Formatos where Facturas='PantallaExtras'"
                rd1 = cmd1.ExecuteReader
                If rd1.HasRows Then
                    If rd1.Read Then
                        cmd2 = cnn2.CreateCommand
                        cmd2.CommandText = "UPDATE Formatos SET NotasCred='0',NumPart='0' WHERE Facturas='PantallaExtras'"
                        If cmd2.ExecuteNonQuery() Then
                            'MsgBox("Configuracion Actualizada correctamente", vbInformation + vbOKOnly, titulomensajes)
                        End If
                    End If
                Else
                    cmd2 = cnn2.CreateCommand
                    cmd2.CommandText = "insert into Formatos(Facturas,NotasCred,NumPart) values('PantallaExtras','0','0')"
                    If cmd2.ExecuteNonQuery() Then
                        'MsgBox("Configuracion Guardada correctamente", vbInformation + vbOKOnly, titulomensajes)
                    End If
                End If
                rd1.Close()
                cnn1.Close()

            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
            cnn1.Close()
        End Try
    End Sub

    Private Sub btnVisor_Click(sender As Object, e As EventArgs) Handles btnVisor.Click

        Dim cnn1 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
        Dim cnn2 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
        Dim rd1, rd2 As MySqlDataReader
        Dim cmd1, cmd2 As MySqlCommand

        Try
            cnn1.Close() : cnn1.Open()

            If txtToleranciaV.Text = "" Then
            Else
                cmd1 = cnn1.CreateCommand
                cmd1.CommandText = "SELECT NumPart FROM formatos WHERE Facturas='ToleVisor'"
                rd1 = cmd1.ExecuteReader
                If rd1.HasRows Then
                    If rd1.Read Then
                        cnn2.Close() : cnn2.Open()
                        cmd2 = cnn2.CreateCommand
                        cmd2.CommandText = "UPDATE formatos SET NumPart=" & txtToleranciaV.Text & " WHERE Facturas='ToleVisor'"
                        cmd2.ExecuteNonQuery()

                        cmd2 = cnn2.CreateCommand
                        cmd2.CommandText = "UPDATE formatos SET NumPart=" & txtToleranciaV.Text & " WHERE Facturas='ToleVisor2'"
                        cmd2.ExecuteNonQuery()
                        cnn2.Close()
                    End If
                Else
                    cnn2.Close() : cnn2.Open()
                    cmd2 = cnn2.CreateCommand
                    cmd2.CommandText = "INSERT INTO formatos(Facturas,NotasCred,NumPart) VALUES('ToleVisor','0'," & txtToleranciaV.Text & ")"
                    cmd2.ExecuteNonQuery()

                    cmd2 = cnn2.CreateCommand
                    cmd2.CommandText = "INSERT INTO formatos(Facturas,NotasCred,NumPart) VALUES('ToleVisor2','0'," & txtToleranciaV.Text & ")"
                    cmd2.ExecuteNonQuery()
                    cnn2.Close()
                End If
                rd1.Close()

            End If

            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "select Equipo from RutasImpresion where Equipo='" & ObtenerNombreEquipo() & "' and Tipo='" & cbonombrecomanda.Text & "'"
            rd1 = cmd1.ExecuteReader
            If rd1.HasRows Then
                If rd1.Read Then
                    cnn2.Close() : cnn2.Open()
                    cmd2 = cnn2.CreateCommand
                    cmd2.CommandText = "UPDATE rutasimpresion SET Tipo='" & cbonombrecomanda.Text & "' WHERE Equipo='" & ObtenerNombreEquipo() & "' AND Formato='VISOR'"
                    If cmd2.ExecuteNonQuery Then
                        MsgBox("Ruta agregada correctamente", vbInformation + vbOKOnly, titulorestaurante)
                        cbonombrecomanda.Text = ""
                    End If

                End If
            Else
                cnn2.Close() : cnn2.Open()
                cmd2 = cnn2.CreateCommand
                cmd2.CommandText = "INSERT INTO rutasimpresion(Equipo,Tipo,Formato) VALUES('" & ObtenerNombreEquipo() & "','" & cbonombrecomanda.Text & "','VISOR')"
                If cmd2.ExecuteNonQuery Then
                    MsgBox("Ruta agregada correctamente", vbInformation + vbOKOnly, titulorestaurante)
                    lblcomanda.Text = cbonombrecomanda.Text
                    cbonombrecomanda.Text = ""
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