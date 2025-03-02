﻿Imports System.IO
Imports MySql.Data.MySqlClient
Public Class frmPagoNomina
    Private Sub frmPagoNomina_Shown(sender As Object, e As System.EventArgs) Handles Me.Shown
        cbonombre.Focus().Equals(True)
    End Sub

    Private Sub cbonombre_DropDown(sender As System.Object, e As System.EventArgs) Handles cbonombre.DropDown
        cbonombre.Items.Clear()


        Dim cnn1 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
        Dim rd1 As MySqlDataReader
        Dim cmd1 As MySqlCommand

        Try
            cnn1.Close() : cnn1.Open()

            cmd1 = cnn1.CreateCommand
            cmd1.CommandText =
                "select Nombre from Usuarios order by Nombre"
            rd1 = cmd1.ExecuteReader
            Do While rd1.Read
                If rd1.HasRows Then cbonombre.Items.Add(rd1(0).ToString())
            Loop
            rd1.Close()
            cnn1.Close()
        Catch ex As Exception
            MessageBox.Show(ex.ToString())
            cnn1.Close()
        End Try
    End Sub

    Private Sub cbonombre_KeyPress(sender As Object, e As System.Windows.Forms.KeyPressEventArgs) Handles cbonombre.KeyPress
        If AscW(e.KeyChar) = Keys.Enter Then


            Dim cnn1 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
            Dim rd1 As MySqlDataReader
            Dim cmd1 As MySqlCommand

            Try
                cnn1.Close() : cnn1.Open()

                cmd1 = cnn1.CreateCommand
                cmd1.CommandText =
                    "select IdEmpleado,Ingreso,Area,Sueldo,Puesto,NSS from Usuarios where Nombre='" & cbonombre.Text & "'"
                rd1 = cmd1.ExecuteReader
                If rd1.HasRows Then
                    If rd1.Read Then
                        lblid_usu.Text = rd1("IdEmpleado").ToString()
                        dtpingreso.Value = rd1("Ingreso").ToString()
                        txtarea.Text = rd1("Area").ToString()
                        txtsueldo.Text = FormatNumber(IIf(rd1("Sueldo").ToString() = "", 0, rd1("Sueldo").ToString()), 2)
                        txtpuesta.Text = rd1("Puesto").ToString()
                        txtnss.Text = rd1("NSS").ToString()
                    End If
                End If
                rd1.Close()
                cnn1.Close()
            Catch ex As Exception
                MessageBox.Show(ex.ToString())
                cnn1.Close()
            End Try
            cbofolio.Focus().Equals(True)
        End If
    End Sub

    Private Sub cbonombre_SelectedValueChanged(sender As System.Object, e As System.EventArgs) Handles cbonombre.SelectedValueChanged

        Dim cnn1 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
        Dim rd1 As MySqlDataReader
        Dim cmd1 As MySqlCommand

        Try
            cnn1.Close() : cnn1.Open()

            cmd1 = cnn1.CreateCommand
            cmd1.CommandText =
                "select IdEmpleado,Ingreso,Area,Sueldo,Puesto,NSS from Usuarios where Nombre='" & cbonombre.Text & "'"
            rd1 = cmd1.ExecuteReader
            If rd1.HasRows Then
                If rd1.Read Then
                    lblid_usu.Text = rd1("IdEmpleado").ToString()
                    dtpingreso.Value = rd1("Ingreso").ToString()
                    txtarea.Text = rd1("Area").ToString()
                    txtsueldo.Text = FormatNumber(IIf(rd1("Sueldo").ToString() = "", 0, rd1("Sueldo").ToString()), 2)
                    txtpuesta.Text = rd1("Puesto").ToString()
                    txtnss.Text = rd1("NSS").ToString()
                    txtsueldo.Text = FormatNumber(IIf(rd1("Sueldo").ToString() = "", 0, rd1("Sueldo").ToString()), 2)
                    txtsueldo_neta.Text = FormatNumber(txtsueldo.Text, 2)
                End If
            End If
            rd1.Close()
            cnn1.Close()
        Catch ex As Exception
            MessageBox.Show(ex.ToString())
            cnn1.Close()
        End Try
    End Sub

    Private Sub cbofolio_DropDown(sender As System.Object, e As System.EventArgs) Handles cbofolio.DropDown
        cbofolio.Items.Clear()

        Dim cnn1 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
        Dim rd1 As MySqlDataReader
        Dim cmd1 As MySqlCommand

        Try

            If lblid_usu.Text <> "" Then
                cnn1.Close() : cnn1.Open()

                cmd1 = cnn1.CreateCommand
                cmd1.CommandText =
                    "select distinct Folio from SaldosEmpleados where Concepto='PRESTAMO' and Estado=0 and IdEmpleado=" & lblid_usu.Text
                rd1 = cmd1.ExecuteReader
                Do While rd1.Read
                    If rd1.HasRows Then cbofolio.Items.Add(rd1(0).ToString())
                Loop
                rd1.Close()
                cnn1.Close()
            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString())
            cnn1.Close()
        End Try
    End Sub

    Private Sub cbofolio_SelectedValueChanged(sender As System.Object, e As System.EventArgs) Handles cbofolio.SelectedValueChanged
        Dim cargos As Double = 0
        Dim abonos As Double = 0

        Dim cnn1 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
        Dim rd1 As MySqlDataReader
        Dim cmd1 As MySqlCommand

        Try
            cnn1.Close() : cnn1.Open()

            cmd1 = cnn1.CreateCommand
            cmd1.CommandText =
                "select * from SaldosEmpleados where Folio=" & cbofolio.Text & " and IdEmpleado=" & lblid_usu.Text
            rd1 = cmd1.ExecuteReader
            Do While rd1.Read
                If rd1.HasRows Then
                    cargos = cargos + IIf(rd1("Cargo").ToString() = "", 0, rd1("Cargo").ToString())
                    abonos = abonos + IIf(rd1("Abono").ToString() = "", 0, rd1("Abono").ToString())
                End If
            Loop
            rd1.Close()
            cnn1.Close()

            txtsaldo.Text = FormatNumber(cargos - abonos, 2)
        Catch ex As Exception
            MessageBox.Show(ex.ToString())
            cnn1.Close()
        End Try
    End Sub

    Private Sub txtmonto_Click(sender As Object, e As System.EventArgs) Handles txtmonto.Click
        txtmonto.SelectionStart = 0
        txtmonto.SelectionLength = Len(txtmonto.Text)
    End Sub

    Private Sub txtmonto_GotFocus(sender As Object, e As System.EventArgs) Handles txtmonto.GotFocus
        txtmonto.SelectionStart = 0
        txtmonto.SelectionLength = Len(txtmonto.Text)
    End Sub

    Private Sub txtmonto_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtmonto.TextChanged
        If txtmonto.Text = "" Then Exit Sub
        If txtsueldo.Text = "" Then Exit Sub
        If txthoras.Text = "" Then Exit Sub
        If txtotros_p.Text = "" Then Exit Sub
        If txtotros_d.Text = "" Then Exit Sub
        If txtsueldo_neta.Text = "" Then Exit Sub
        txtsueldo_neta.Text = (CDbl(txtsueldo.Text) - CDbl(txtmonto.Text)) + CDbl(txthoras.Text) + CDbl(txtotros_p.Text) + CDbl(txtotros_d.Text)
        txtsueldo_neta.Text = FormatNumber(txtsueldo_neta.Text, 2)
    End Sub

    Private Sub txtmonto_KeyPress(sender As System.Object, e As System.Windows.Forms.KeyPressEventArgs) Handles txtmonto.KeyPress
        If AscW(e.KeyChar) = Keys.Enter Then
            If txtmonto.Text = "" Then txtmonto.Text = "0.00"
            txthoras.Focus().Equals(True)
        End If
    End Sub

    Private Sub cbofolio_KeyPress(sender As System.Object, e As System.Windows.Forms.KeyPressEventArgs) Handles cbofolio.KeyPress
        If AscW(e.KeyChar) = Keys.Enter Then
            If cbofolio.Text = "" Then txthoras.Focus().Equals(True) : Exit Sub
            Dim cargos As Double = 0
            Dim abonos As Double = 0

            Dim cnn1 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
            Dim rd1 As MySqlDataReader
            Dim cmd1 As MySqlCommand

            Try
                cnn1.Close() : cnn1.Open()

                cmd1 = cnn1.CreateCommand
                cmd1.CommandText =
                    "select * from SaldosEmpleados where Folio=" & cbofolio.Text & " and IdEmpleado=" & lblid_usu.Text
                rd1 = cmd1.ExecuteReader
                Do While rd1.Read
                    If rd1.HasRows Then
                        cargos = cargos + IIf(rd1("Cargo").ToString() = "", 0, rd1("Cargo").ToString())
                        abonos = abonos + IIf(rd1("Abono").ToString() = "", 0, rd1("Abono").ToString())
                    End If
                Loop
                rd1.Close()
                cnn1.Close()

                txtsaldo.Text = FormatNumber(cargos - abonos, 2)
            Catch ex As Exception
                MessageBox.Show(ex.ToString())
                cnn1.Close()
            End Try
        End If
    End Sub

    Private Sub txthoras_Click(sender As System.Object, e As System.EventArgs) Handles txthoras.Click
        txthoras.SelectionStart = 0
        txthoras.SelectionLength = Len(txthoras.Text)
    End Sub

    Private Sub txthoras_GotFocus(sender As Object, e As System.EventArgs) Handles txthoras.GotFocus
        txthoras.SelectionStart = 0
        txthoras.SelectionLength = Len(txthoras.Text)
    End Sub

    Private Sub txthoras_KeyPress(sender As Object, e As System.Windows.Forms.KeyPressEventArgs) Handles txthoras.KeyPress
        If AscW(e.KeyChar) = Keys.Enter Then
            If txthoras.Text = "" Then txthoras.Text = "0.00" : Exit Sub
            txtotros_p.Focus().Equals(True)
        End If
    End Sub

    Private Sub txthoras_TextChanged(sender As Object, e As System.EventArgs) Handles txthoras.TextChanged
        If Not IsNumeric(txthoras.Text) Then txthoras.Text = "0.00" : Exit Sub
        If txtmonto.Text = "" Then Exit Sub
        If txtsueldo.Text = "" Then Exit Sub
        If txthoras.Text = "" Then Exit Sub
        If txtotros_p.Text = "" Then Exit Sub
        If txtotros_d.Text = "" Then Exit Sub
        If txtsueldo_neta.Text = "" Then Exit Sub
        txtsueldo_neta.Text = (CDbl(txtsueldo.Text) - CDbl(txtmonto.Text)) + CDbl(txthoras.Text) + CDbl(txtotros_p.Text) + CDbl(txtotros_d.Text)
        txtsueldo_neta.Text = FormatNumber(txtsueldo_neta.Text, 2)
    End Sub

    Private Sub txtotros_p_Click(sender As System.Object, e As System.EventArgs) Handles txtotros_p.Click
        txtotros_p.SelectionStart = 0
        txtotros_p.SelectionLength = Len(txtotros_p.Text)
    End Sub

    Private Sub txtotros_p_GotFocus(sender As Object, e As System.EventArgs) Handles txtotros_p.GotFocus
        txtotros_p.SelectionStart = 0
        txtotros_p.SelectionLength = Len(txtotros_p.Text)
    End Sub

    Private Sub txtotros_p_KeyPress(sender As Object, e As System.Windows.Forms.KeyPressEventArgs) Handles txtotros_p.KeyPress
        If AscW(e.KeyChar) = Keys.Enter Then
            If txtotros_p.Text = "" Then txtotros_p.Text = "0.00" : Exit Sub
            txtotros_d.Focus().Equals(True)
        End If
    End Sub

    Private Sub txtotros_p_TextChanged(sender As Object, e As System.EventArgs) Handles txtotros_p.TextChanged

        If Not IsNumeric(txtotros_p.Text) Then txtotros_p.Text = "0.00" : Exit Sub
        If txtmonto.Text = "" Then Exit Sub
        If txtsueldo.Text = "" Then Exit Sub
        If txthoras.Text = "" Then Exit Sub
        If txtotros_p.Text = "" Then Exit Sub
        If txtotros_d.Text = "" Then Exit Sub
        If txtsueldo_neta.Text = "" Then Exit Sub
        txtsueldo_neta.Text = (CDbl(txtsueldo.Text) - CDbl(txtmonto.Text)) + CDbl(txthoras.Text) + CDbl(txtotros_p.Text) + CDbl(txtotros_d.Text)
        txtsueldo_neta.Text = FormatNumber(txtsueldo_neta.Text, 2)
    End Sub

    Private Sub txtotros_d_Click(sender As System.Object, e As System.EventArgs) Handles txtotros_d.Click
        txtotros_d.SelectionStart = 0
        txtotros_d.SelectionLength = Len(txtotros_d.Text)
    End Sub

    Private Sub txtotros_d_GotFocus(sender As Object, e As System.EventArgs) Handles txtotros_d.GotFocus
        txtotros_d.SelectionStart = 0
        txtotros_d.SelectionLength = Len(txtotros_d.Text)
    End Sub

    Private Sub txtotros_d_KeyPress(sender As Object, e As System.Windows.Forms.KeyPressEventArgs) Handles txtotros_d.KeyPress
        If AscW(e.KeyChar) = Keys.Enter Then
            If txtotros_d.Text = "" Then txtotros_d.Text = "0.00" : Exit Sub
            dtpdesde.Focus().Equals(True)
        End If
    End Sub

    Private Sub txtotros_d_TextChanged(sender As Object, e As System.EventArgs) Handles txtotros_d.TextChanged

        If Not IsNumeric(txtotros_d.Text) Then txtotros_d.Text = "0.00" : Exit Sub
        '  If Strings.Left(txtefectivo.Text, 1) = "," Or Strings.Left(txtefectivo.Text, 1) = "." Then Exit Sub

        If txtmonto.Text = "" Then Exit Sub
        If txtsueldo.Text = "" Then Exit Sub
        If txthoras.Text = "" Then Exit Sub
        If txtotros_p.Text = "" Then Exit Sub
        If txtotros_d.Text = "" Then Exit Sub
        If txtsueldo_neta.Text = "" Then Exit Sub
        txtsueldo_neta.Text = (CDbl(txtsueldo.Text) - CDbl(txtmonto.Text)) + CDbl(txthoras.Text) + CDbl(txtotros_p.Text) - CDbl(txtotros_d.Text)
        txtsueldo_neta.Text = FormatNumber(txtsueldo_neta.Text, 2)
    End Sub

    Private Sub dtpdesde_KeyPress(sender As System.Object, e As System.Windows.Forms.KeyPressEventArgs) Handles dtpdesde.KeyPress
        If AscW(e.KeyChar) = Keys.Enter Then
            dtphasta.Focus().Equals(True)
        End If
    End Sub

    Private Sub dtphasta_KeyPress(sender As System.Object, e As System.Windows.Forms.KeyPressEventArgs) Handles dtphasta.KeyPress
        If AscW(e.KeyChar) = Keys.Enter Then
            txtefectivo.Focus().Equals(True)
        End If
    End Sub

    Private Sub txtcontraseña_Click(sender As System.Object, e As System.EventArgs) Handles txtcontraseña.Click
        txtcontraseña.SelectionStart = 0
        txtcontraseña.SelectionLength = Len(txtcontraseña.Text)
    End Sub

    Private Sub txtcontraseña_GotFocus(sender As Object, e As System.EventArgs) Handles txtcontraseña.GotFocus
        txtcontraseña.SelectionStart = 0
        txtcontraseña.SelectionLength = Len(txtcontraseña.Text)
    End Sub

    Private Sub txtcontraseña_KeyPress(sender As Object, e As System.Windows.Forms.KeyPressEventArgs) Handles txtcontraseña.KeyPress
        If AscW(e.KeyChar) = Keys.Enter Then
            Dim cnn1 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
            Dim rd1 As MySqlDataReader
            Dim cmd1 As MySqlCommand
            Try
                cnn1.Close() : cnn1.Open()

                cmd1 = cnn1.CreateCommand
                cmd1.CommandText =
                    "select * from Usuarios where Clave='" & txtcontraseña.Text & "'"
                rd1 = cmd1.ExecuteReader
                If rd1.HasRows Then
                    If rd1.Read Then
                        lblusuario.Text = rd1("Alias").ToString()
                        btnguardar.Focus().Equals(True)
                    End If
                Else
                    MsgBox("No se encuentra el usuario, revisa la contraseña.", vbInformation + vbOKOnly, "Delsscom Control Negocios Pro") : txtcontraseña.Focus().Equals(True) : Exit Sub
                End If
                rd1.Close()
                cnn1.Close()
            Catch ex As Exception
                MessageBox.Show(ex.ToString())
                cnn1.Close()
            End Try
        End If
    End Sub

    Private Sub btnguardar_Click(sender As System.Object, e As System.EventArgs) Handles btnguardar.Click
        If cbotipo.Text = "" Then MsgBox("Selecciona el tipo de movimiento para guardar.", vbInformation + vbOKOnly, "Delsscom Control Negocios Pro") : cbotipo.Focus().Equals(True) : Exit Sub
        If cbonombre.Text = "" Then MsgBox("Selecciona un usuario para guardar el movimiento.", vbInformation + vbOKOnly, "Delsscom Control Negocios Pro") : cbonombre.Focus().Equals(True) : Exit Sub
        If lblid_usu.Text = "" Then MsgBox("Selecciona un registro del catálogo de empleados.", vbInformation + vbOKOnly, "Delsscom Control Negocios Pro") : cbonombre.Focus().Equals(True) : Exit Sub
        If CDbl(txtmonto.Text) > 0 And cbofolio.Text = "" Then MsgBox("No puedes asignar un descuento sin seleccionar un folio del mismo.", vbInformation + vbOKOnly, "Delsscom Control Negocios Pro") : cbofolio.Focus().Equals(True) : Exit Sub
        If cbofolio.Text <> "" And CDbl(txtmonto.Text) <= 0 Then MsgBox("El monto descontado no puede ser 0.", vbInformation + vbOKOnly, "Delsscom Control Negocios Pro") : txtmonto.Focus().Equals(True) : Exit Sub
        If lblusuario.Text = "" Then MsgBox("Escribe tu contraseña paara guardar el movimiento.", vbInformation + vbOKOnly, "Delsscom Control Negocios Pro") : txtcontraseña.Focus().Equals(True) : Exit Sub
        If CDbl(txtmonto.Text) > CDbl(txtsaldo.Text) Then MsgBox("El monto descontado no puede ser mayor al restante del préstamo.", vbInformation + vbOKOnly, "Delsscom Control Negocios Pro") : txtmonto.Focus().Equals(True) : Exit Sub
        If CDbl(txtsueldo_neta.Text) <= 0 Then MsgBox("No puede guardar el movimiento sin un saldo neto mayor a 0.", vbInformation + vbOKOnly, "Delsscom Control Negocios Pro") : txthoras.Focus().Equals(True) : Exit Sub
        If ValidaPermisos(lblusuario.Text, "Egr_Nom") = False Then MsgBox("No cuentas con permisos para realizar este movimiento.", vbInformation + vbOKOnly, "Delsscom Control Negocios Pro") : txtcontraseña.Focus().Equals(True) : Exit Sub

        If MsgBox("¿Deseas guardar este movimiento de nómina?", vbInformation + vbOKCancel, "Delsscom Control Negocios Pro") = vbOK Then

            Dim cnn1 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
            Dim cnn2 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
            Dim rd1, rd2 As MySqlDataReader
            Dim cmd1, cmd2 As MySqlCommand

            Try
                Dim acuenta As Double = txtmonto.Text
                Dim efectivo As Double = txtefectivo.Text
                Dim total As Double = CDbl(txtefectivo.Text) + CDbl(txtPagos.Text)

                If total > txtsueldo_neta.Text Then
                    MsgBox("El total no debe de rebasar el sueldo neto", vbInformation + vbOKOnly, titulocentral)
                    txttotal.Text = "0.00"
                    Exit Sub
                ElseIf total < txtsueldo_neta.Text Then
                    MsgBox("El total no debe de ser menor al sueldo neto", vbInformation + vbOKOnly, titulocentral)
                    txttotal.Text = "0.00"
                    Exit Sub
                End If
                cnn1.Close() : cnn1.Open()

                If cbofolio.Text <> "" Then
                    Dim cargos As Double = 0
                    Dim abonos As Double = 0
                    Dim resta As Double = 0


                    cmd1 = cnn1.CreateCommand
                    cmd1.CommandText =
                        "select * from SaldosEmpleados where Folio=" & cbofolio.Text & " and IdEmpleado=" & lblid_usu.Text
                    rd1 = cmd1.ExecuteReader
                    Do While rd1.Read
                        If rd1.HasRows Then
                            cargos = cargos + CDbl(IIf(rd1("Cargo").ToString() = "", 0, rd1("Cargo").ToString()))
                            abonos = abonos + CDbl(IIf(rd1("Abono").ToString() = "", 0, rd1("Abono").ToString()))
                        End If
                    Loop
                    rd1.Close()

                    resta = cargos - abonos
                    If acuenta > resta Then
                        MsgBox("El monto descontado no puede ser mayor al restante del préstamo.", vbInformation + vbOKOnly, "Delsscom Control Negocios Pro") : txtmonto.Focus().Equals(True) : cnn1.Close() : Exit Sub
                    ElseIf acuenta = resta Then
                        'inserta pago y pone estado en 0
                        cmd1 = cnn1.CreateCommand
                        cmd1.CommandText =
                            "insert into SaldosEmpleados(Folio,IdEmpleado,Nombre,Fecha,FechaPago,FechaPagado,Cargo,Abono,Tipo,Concepto,Monto,Nota,Usuario,Corte,CorteU,Estado) values(" & cbofolio.Text & "," & lblid_usu.Text & ",'" & cbonombre.Text & "','" & Date.Now & "','" & Date.Now & "','',0," & CDbl(txtmonto.Text) & ",'" & cbotipo.Text & "','COBRO'," & CDbl(txtmonto.Text) & ",'DESCUENTO POR NÓMINA','" & lblusuario.Text & "',0,0)"
                        cmd1.ExecuteNonQuery()

                        cmd1 = cnn1.CreateCommand
                        cmd1.CommandText =
                            "update SaldosEmpleados set Estado=1, FechaPagado='" & Date.Now & "' where Folio=" & cbofolio.Text & " and Concepto='PRESTAMO' and IdEmpleado=" & lblid_usu.Text
                        cmd1.ExecuteNonQuery()
                    ElseIf acuenta < resta Then
                        'inserta pago
                        cmd1 = cnn1.CreateCommand
                        cmd1.CommandText =
                            "insert into SaldosEmpleados(Folio,IdEmpleado,Nombre,Fecha,FechaPago,FechaPagado,Cargo,Abono,Tipo,Concepto,Monto,Nota,Usuario,Corte,CorteU,Estado) values(" & cbofolio.Text & "," & lblid_usu.Text & ",'" & cbonombre.Text & "','" & Date.Now & "','" & Date.Now & "','',0," & CDbl(txtmonto.Text) & ",'" & cbotipo.Text & "','COBRO'," & CDbl(txtmonto.Text) & ",'DESCUENTO POR NÓMINA','" & lblusuario.Text & "',0,0,0)"
                        cmd1.ExecuteNonQuery()
                    End If
                End If

                cmd1 = cnn1.CreateCommand
                cmd1.CommandText =
                    "insert into Nominas(IdEmpleado,Nombre,Area,Puesto,Fecha,Sueldo,Descuento,Horas,OtrosD,OtrosP,SueldoNeto,Desde,Hasta,Usuario,Corte,CorteU) values(" & lblid_usu.Text & ",'" & cbonombre.Text & "','" & txtarea.Text & "','" & txtpuesta.Text & "','" & Format(Date.Now, "yyyy-MM-dd") & "'," & CDbl(txtsueldo.Text) & "," & CDbl(txtmonto.Text) & "," & CDbl(txthoras.Text) & "," & CDbl(txtotros_d.Text) & "," & CDbl(txtotros_p.Text) & "," & CDbl(txtsueldo_neta.Text) & ",'" & Format(dtpdesde.Value, "yyyy-MM-dd") & "','" & Format(dtphasta.Value, "yyyy-MM-dd") & "','" & lblusuario.Text & "',0,0)"
                If cmd1.ExecuteNonQuery Then
                    MsgBox("Movimiento de nómina registrado correctamente.", vbInformation + vbOKOnly, "Delsscom Control Negocios Pro")
                End If

                Dim folionomina As Integer = 0

                cnn2.Close() : cnn2.Open()
                cmd2 = cnn2.CreateCommand
                cmd2.CommandText = "SELECT MAX(Id) FROM nominas"
                rd2 = cmd2.ExecuteReader
                If rd2.HasRows Then
                    If rd2.Read Then
                        folionomina = IIf(rd2(0).ToString = "", 0, rd2(0).ToString)
                    End If
                Else
                    folionomina = 1
                End If
                rd2.Close()
                cnn2.Close()

                For luffy As Integer = 0 To grdpago.Rows.Count - 1


                    Dim formap As String = grdpago.Rows(luffy).Cells(0).Value.ToString
                    Dim bancop As String = grdpago.Rows(luffy).Cells(1).Value.ToString
                    Dim referenciap As String = grdpago.Rows(luffy).Cells(2).Value.ToString
                    Dim montop As Double = grdpago.Rows(luffy).Cells(3).Value.ToString
                    Dim fechap As Date = grdpago.Rows(luffy).Cells(4).Value.ToString
                    Dim comentariop As String = grdpago.Rows(luffy).Cells(5).Value.ToString
                    Dim cuentap As String = grdpago.Rows(luffy).Cells(6).Value.ToString
                    Dim bancocp As String = grdpago.Rows(luffy).Cells(7).Value.ToString
                    Dim fechanueva As String = ""
                    fechanueva = Format(fechap, "yyyy-MM-dd")

                    cmd1 = cnn1.CreateCommand
                    cmd1.CommandText = "INSERT INTO otrosgastos(Tipo,Concepto,Folio,Fecha,FormaPago,Monto,Total,Nota,Banco,Referencia,Comentario,CuentaC,BancoC,Usuario,Corte,CorteU) VALUES('" & cbotipo.Text & "','NOMINA','0','" & Format(Date.Now, "yyyy/MM/dd") & "','" & formap & "'," & montop & "," & montop & ",'','" & bancop & "','" & referenciap & "','" & comentariop & "','" & cuentap & "','" & bancocp & "','" & lblusuario.Text & "',0,0)"
                    cmd1.ExecuteNonQuery()

                    Dim saldocuenta As Double = 0

                    cnn2.Close() : cnn2.Open()
                    cmd2 = cnn2.CreateCommand
                    cmd2.CommandText = "SELECT Saldo FROM movCuenta WHERE Id=(SELECT MAX(Id) FROM movcuenta WHERE Cuenta='" & cuentap & "')"
                    rd2 = cmd2.ExecuteReader
                    If rd2.HasRows Then
                        If rd2.Read Then
                            saldocuenta = IIf(rd2(0).ToString = "", 0, rd2(0).ToString) - montop

                            cmd1 = cnn1.CreateCommand
                            cmd1.CommandText = "INSERT INTO movcuenta(Tipo,Banco,Referencia,Concepto,Total,Retiro,Deposito,Saldo,Fecha,Hora,Folio,Cliente,Comentario,Cuenta,BancoCuenta) VALUES('" & formap & "','" & bancop & "','" & referenciap & "','NOMINA'," & montop & "," & montop & ",0," & saldocuenta & ",'" & fechanueva & "','" & Format(Date.Now, "HH:mm:ss") & "','" & folionomina & "','" & cbonombre.Text & "','" & comentariop & "','" & cuentap & "','" & bancocp & "')"
                            cmd1.ExecuteNonQuery()
                        End If
                    Else
                        saldocuenta = -montop

                        cmd1 = cnn1.CreateCommand
                        cmd1.CommandText = "INSERT INTO movcuenta(Tipo,Banco,Referencia,Concepto,Total,Retiro,Deposito,Saldo,Fecha,Hora,Folio,Cliente,Comentario,Cuenta,BancoCuenta) VALUES('" & formap & "','" & bancop & "','" & referenciap & "','NOMINA'," & montop & "," & montop & ",0," & saldocuenta & ",'" & fechanueva & "','" & Format(Date.Now, "HH:mm:ss") & "','" & folionomina & "','" & cbonombre.Text & "','" & comentariop & "','" & cuentap & "','" & bancocp & "')"
                        cmd1.ExecuteNonQuery()
                    End If
                    rd2.Close()
                    cnn2.Close()
                Next
                cnn1.Close()

                If txtefectivo.Text > 0 Then
                    cnn1.Close() : cnn1.Open()
                    cmd1 = cnn1.CreateCommand
                    cmd1.CommandText = "INSERT INTO otrosgastos(Tipo,Concepto,Folio,Fecha,FormaPago,Monto,Total,Nota,Banco,Referencia,Comentario,CuentaC,BancoC,Usuario,Corte,CorteU) VALUES('" & cbotipo.Text & "','NOMINA','0','" & Format(Date.Now, "yyyy/MM/dd") & "','EFECTIVO'," & efectivo & "," & efectivo & ",'','','','','','','" & lblusuario.Text & "',0,0)"
                    cmd1.ExecuteNonQuery()
                    cnn1.Close()
                End If


                Dim tam As Integer = 0
                Dim impresora As String = ""
                tam = TamImpre()
                impresora = ImpresoraImprimir()

                If tam = "80" Then
                    PNomina80.DefaultPageSettings.PrinterSettings.PrinterName = impresora
                    PNomina80.Print()
                End If

                If tam = "58" Then
                    pNomina58.DefaultPageSettings.PrinterSettings.PrinterName = impresora
                    pNomina58.Print()
                End If

                btnnuevo.PerformClick()
            Catch ex As Exception
                MessageBox.Show(ex.ToString())
                cnn1.Close()
            End Try
        End If
    End Sub

    Private Sub cbotipo_KeyPress(sender As System.Object, e As System.Windows.Forms.KeyPressEventArgs) Handles cbotipo.KeyPress
        If AscW(e.KeyChar) = Keys.Enter Then
            cbonombre.Focus().Equals(True)
        End If
    End Sub

    Private Sub btnnuevo_Click(sender As System.Object, e As System.EventArgs) Handles btnnuevo.Click
        cbotipo.Items.Clear()
        cbotipo.Text = ""
        txtcontraseña.Text = ""
        lblusuario.Text = ""
        cbonombre.Items.Clear()
        cbonombre.Text = ""
        txtarea.Text = ""
        txtpuesta.Text = ""
        txtsueldo.Text = "0.00"
        txtnss.Text = ""
        cbofolio.Items.Clear()
        cbofolio.Text = ""
        txtmonto.Text = "0.00"
        txtsaldo.Text = "0.00"
        lblid_usu.Text = ""
        txthoras.Text = "0.00"
        txtotros_p.Text = "0.00"
        txtotros_d.Text = "0.00"
        txtsueldo_neta.Text = "0.00"

        txtefectivo.Text = "0.00"
        txtPagos.Text = "0.00"
        txttotal.Text = "0.00"
        dtpdesde.Value = Date.Now
        dtphasta.Value = Date.Now
        grdpago.Rows.Clear()
    End Sub

    Private Sub cbotipo_DropDown(sender As System.Object, e As System.EventArgs) Handles cbotipo.DropDown
        cbotipo.Items.Clear()
        cbotipo.Items.Add("ADMINISTRACION")
        cbotipo.Items.Add("OPERACION")
        cbotipo.Items.Add("VENTAS")
    End Sub

    Private Sub txtefectivo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtefectivo.KeyPress
        If AscW(e.KeyChar) = Keys.Enter Then
            If Not IsNumeric(txtefectivo.Text) Then txtefectivo.Text = "0"
            txtefectivo.Text = FormatNumber(txtefectivo.Text, 2)
            txttotal.Text = CDbl(txtefectivo.Text) + CDbl(txtPagos.Text)
            txttotal.Text = FormatNumber(txttotal.Text, 2)
            btnguardar.Focus().Equals(True)
        End If
    End Sub

    Private Sub txtefectivo_Click(sender As Object, e As EventArgs) Handles txtefectivo.Click
        txtefectivo.SelectionStart = 0
        txtefectivo.SelectionLength = Len(txtefectivo.Text)
    End Sub

    Private Sub txttransfe_Click(sender As Object, e As EventArgs) Handles txtPagos.Click
        txtPagos.SelectionStart = 0
        txtPagos.SelectionLength = Len(txtPagos.Text)
    End Sub

    Private Sub txtefectivo_GotFocus(sender As Object, e As EventArgs) Handles txtefectivo.GotFocus
        txtefectivo.SelectionStart = 0
        txtefectivo.SelectionLength = Len(txtefectivo.Text)
    End Sub

    Private Sub txtefectivo_LostFocus(sender As Object, e As EventArgs) Handles txtefectivo.LostFocus
        If Not IsNumeric(txtefectivo.Text) Then Exit Sub
        If Not IsNumeric(txtPagos.Text) Then Exit Sub
        txttotal.Text = CDbl(txtefectivo.Text) + CDbl(txtPagos.Text)
        txttotal.Text = FormatNumber(txttotal.Text, 2)
    End Sub

    Private Sub txttransfe_GotFocus(sender As Object, e As EventArgs) Handles txtPagos.GotFocus
        txtPagos.SelectionStart = 0
        txtPagos.SelectionLength = Len(txtPagos.Text)
    End Sub

    Private Sub txttransfe_LostFocus(sender As Object, e As EventArgs) Handles txtPagos.LostFocus
        If Not IsNumeric(txtefectivo.Text) Then Exit Sub
        If Not IsNumeric(txtPagos.Text) Then Exit Sub

        txttotal.Text = CDbl(txtefectivo.Text) + CDbl(txtPagos.Text)

    End Sub

    Private Sub PNomina80_PrintPage(sender As Object, e As Printing.PrintPageEventArgs) Handles PNomina80.PrintPage

        Dim cnn2 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
        Dim rd2 As MySqlDataReader
        Dim cmd2 As MySqlCommand

        Try
            Dim tipografia As String = "Lucida Sans Typewriter"
            Dim fuente_r As New Font("Lucida Sans Typewriter", 8, FontStyle.Regular)
            Dim fuente_b As New Font("Lucida Sans Typewriter", 8, FontStyle.Bold)
            Dim fuente_c As New Font("Lucida Sans Typewriter", 8, FontStyle.Regular)
            Dim fuente_p As New Font("Lucida Sans Typewriter", 7, FontStyle.Regular)
            Dim fuente_a As New Font("Lucida Sans Typewriter", 12, FontStyle.Bold)

            Dim derecha As New StringFormat With {.Alignment = StringAlignment.Far}
            Dim sc As New StringFormat With {.Alignment = StringAlignment.Center}
            Dim hoja As New Pen(Brushes.Black, 1)
            Dim Y As Double = 0

            Dim Logotipo As Drawing.Image = Nothing
            Dim nLogo As String = DatosRecarga("LogoG")
            Dim tLogo As String = DatosRecarga("TipoLogo")
            Dim simbolo As String = DatosRecarga("Simbolo")




            Dim pie1 As String = ""

            If tLogo <> "SIN" Then
                If File.Exists(My.Application.Info.DirectoryPath & "\" & nLogo) Then
                    Logotipo = Drawing.Image.FromFile(My.Application.Info.DirectoryPath & "\" & nLogo)
                End If
                If tLogo = "CUAD" Then
                    e.Graphics.DrawImage(Logotipo, 80, 0, 120, 120)
                    Y += 130
                End If
                If tLogo = "RECT" Then
                    e.Graphics.DrawImage(Logotipo, 30, 0, 240, 110)
                    Y += 120
                End If
            Else
                Y = 0
            End If

            cnn2.Close() : cnn2.Open()
            cmd2 = cnn2.CreateCommand
            cmd2.CommandText =
                "select * from Ticket"
            rd2 = cmd2.ExecuteReader
            If rd2.HasRows Then
                If rd2.Read Then
                    pie1 = rd2("Pie3").ToString
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
                    Y += 8
                End If
            Else
                Y += 0
            End If
            rd2.Close()
            cnn2.Close()

            e.Graphics.DrawString("----------------------------------------------------------------------------", fuente_b, Brushes.Black, 1, Y)
            Y += 11
            e.Graphics.DrawString("P A G O  D E  N Ó M I N A", fuente_b, Brushes.Black, 135, Y, sc)
            Y += 11
            e.Graphics.DrawString("----------------------------------------------------------------------------", fuente_b, Brushes.Black, 1, Y)
            Y += 15

            e.Graphics.DrawString("Empleado: " & cbonombre.Text, fuente_r, Brushes.Black, 1, Y)
            Y += 20
            e.Graphics.DrawString("Fecha: " & Format(Date.Now, "yyyy-MM-dd"), fuente_r, Brushes.Black, 1, Y)
            Y += 15
            e.Graphics.DrawString("----------------------------------------------------------------------------", fuente_b, Brushes.Black, 1, Y)
            Y += 15

            e.Graphics.DrawString("Sueldo: " & FormatNumber(txtsueldo.Text, 2), fuente_r, Brushes.Black, 270, Y, derecha)
            Y += 20
            e.Graphics.DrawString("Horas Extras: " & FormatNumber(txthoras.Text, 2), fuente_r, Brushes.Black, 270, Y, derecha)
            Y += 20
            e.Graphics.DrawString("Otras percepciones: " & FormatNumber(txtotros_p.Text, 2), fuente_r, Brushes.Black, 270, Y, derecha)
            Y += 20
            e.Graphics.DrawString("Otros descuentos: " & FormatNumber(txtotros_d.Text, 2), fuente_r, Brushes.Black, 270, Y, derecha)
            Y += 20
            e.Graphics.DrawString("Sueldo Neto: " & FormatNumber(txtsueldo_neta.Text, 2), fuente_r, Brushes.Black, 270, Y, derecha)
            Y += 20
            e.Graphics.DrawString("----------------------------------------------------------------------------", fuente_b, Brushes.Black, 1, Y)
            Y += 15

            e.Graphics.DrawString("EFECTIVO: " & FormatNumber(txtefectivo.Text, 2), fuente_r, Brushes.Black, 270, Y, derecha)
            Y += 20
            e.Graphics.DrawString("TRANSFERENCIA: " & FormatNumber(txtPagos.Text, 2), fuente_r, Brushes.Black, 270, Y, derecha)
            Y += 20
            e.Graphics.DrawString("TOTAL: " & FormatNumber(txttotal.Text, 2), fuente_r, Brushes.Black, 270, Y, derecha)
            Y += 20
            e.Graphics.DrawString(pie1, fuente_r, Brushes.Black, 1, Y)

            e.HasMorePages = False
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
            cnn2.Close()
        End Try
    End Sub

    Private Sub pNomina58_PrintPage(sender As Object, e As Printing.PrintPageEventArgs) Handles pNomina58.PrintPage

        Dim cnn2 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
        Dim rd2 As MySqlDataReader
        Dim cmd2 As MySqlCommand

        Try
            Dim tipografia As String = "Lucida Sans Typewriter"
            Dim fuente_r As New Font("Lucida Sans Typewriter", 7, FontStyle.Regular)
            Dim fuente_b As New Font("Lucida Sans Typewriter", 7, FontStyle.Bold)
            Dim fuente_c As New Font("Lucida Sans Typewriter", 7, FontStyle.Regular)
            Dim fuente_p As New Font("Lucida Sans Typewriter", 6, FontStyle.Regular)
            Dim fuente_a As New Font("Lucida Sans Typewriter", 10, FontStyle.Bold)

            Dim derecha As New StringFormat With {.Alignment = StringAlignment.Far}
            Dim sc As New StringFormat With {.Alignment = StringAlignment.Center}
            Dim hoja As New Pen(Brushes.Black, 1)
            Dim Y As Double = 0

            Dim Logotipo As Drawing.Image = Nothing
            Dim nLogo As String = DatosRecarga("LogoG")
            Dim tLogo As String = DatosRecarga("TipoLogo")
            Dim simbolo As String = DatosRecarga("Simbolo")



            Dim pie1 As String = ""

            If tLogo <> "SIN" Then
                If File.Exists(My.Application.Info.DirectoryPath & "\" & nLogo) Then
                    Logotipo = Drawing.Image.FromFile(My.Application.Info.DirectoryPath & "\" & nLogo)
                End If
                If tLogo = "CUAD" Then
                    e.Graphics.DrawImage(Logotipo, 50, 0, 100, 100)
                    Y += 130
                End If
                If tLogo = "RECT" Then
                    e.Graphics.DrawImage(Logotipo, 30, 0, 110, 110)
                    Y += 120
                End If
            Else
                Y = 0
            End If

            cnn2.Close() : cnn2.Open()
            cmd2 = cnn2.CreateCommand
            cmd2.CommandText =
                "select * from Ticket"
            rd2 = cmd2.ExecuteReader
            If rd2.HasRows Then
                If rd2.Read Then
                    pie1 = rd2("Pie3").ToString
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
                    Y += 8
                End If
            Else
                Y += 0
            End If
            rd2.Close()
            cnn2.Close()

            e.Graphics.DrawString("----------------------------------------------------------------------------", fuente_b, Brushes.Black, 1, Y)
            Y += 11
            e.Graphics.DrawString("P A G O  D E  N Ó M I N A", fuente_b, Brushes.Black, 90, Y, sc)
            Y += 11
            e.Graphics.DrawString("----------------------------------------------------------------------------", fuente_b, Brushes.Black, 1, Y)
            Y += 15

            e.Graphics.DrawString("Empleado: " & cbonombre.Text, fuente_r, Brushes.Black, 1, Y)
            Y += 20
            e.Graphics.DrawString("Fecha: " & Format(Date.Now, "yyyy-MM-dd"), fuente_r, Brushes.Black, 1, Y)
            Y += 15
            e.Graphics.DrawString("----------------------------------------------------------------------------", fuente_b, Brushes.Black, 1, Y)
            Y += 15

            e.Graphics.DrawString("Sueldo: " & FormatNumber(txtsueldo.Text, 2), fuente_r, Brushes.Black, 180, Y, derecha)
            Y += 20
            e.Graphics.DrawString("Horas Extras: " & FormatNumber(txthoras.Text, 2), fuente_r, Brushes.Black, 180, Y, derecha)
            Y += 20
            e.Graphics.DrawString("Otras percepciones: " & FormatNumber(txtotros_p.Text, 2), fuente_r, Brushes.Black, 180, Y, derecha)
            Y += 20
            e.Graphics.DrawString("Otros descuentos: " & FormatNumber(txtotros_d.Text, 2), fuente_r, Brushes.Black, 180, Y, derecha)
            Y += 20
            e.Graphics.DrawString("Sueldo Neto: " & FormatNumber(txtsueldo_neta.Text, 2), fuente_r, Brushes.Black, 180, Y, derecha)
            Y += 20
            e.Graphics.DrawString("----------------------------------------------------------------------------", fuente_b, Brushes.Black, 1, Y)
            Y += 15

            e.Graphics.DrawString("EFECTIVO: " & FormatNumber(txtefectivo.Text, 2), fuente_r, Brushes.Black, 180, Y, derecha)
            Y += 20
            e.Graphics.DrawString("TRANSFERENCIA: " & FormatNumber(txtPagos.Text, 2), fuente_r, Brushes.Black, 180, Y, derecha)
            Y += 20
            e.Graphics.DrawString("TOTAL: " & FormatNumber(txttotal.Text, 2), fuente_r, Brushes.Black, 180, Y, derecha)
            Y += 20

            e.Graphics.DrawString(pie1, fuente_r, Brushes.Black, 1, Y)

            e.HasMorePages = False
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
            cnn2.Close()
        End Try
    End Sub

    Private Sub cbotpago_DropDown(sender As Object, e As EventArgs) Handles cbotpago.DropDown
        Dim cnn5 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
        Dim rd5 As MySqlDataReader
        Dim cmd5 As MySqlCommand
        Try
            cbotpago.Items.Clear()

            cnn5.Close() : cnn5.Open()
            cmd5 = cnn5.CreateCommand
            cmd5.CommandText = "SELECT DISTINCT FormaPago FROM formaspago WHERE FormaPago<>'' ORDER BY FormaPago"
            rd5 = cmd5.ExecuteReader
            Do While rd5.Read
                If rd5.HasRows Then
                    cbotpago.Items.Add(rd5(0).ToString)
                End If
            Loop
            rd5.Close()
            cnn5.Close()

        Catch ex As Exception
            MessageBox.Show(ex.ToString)
            cnn5.Close()
        End Try
    End Sub

    Private Sub cbotpago_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cbotpago.KeyPress
        e.KeyChar = UCase(e.KeyChar)
        If AscW(e.KeyChar) = Keys.Enter Then
            cbobanco.Focus.Equals(True)
        End If
    End Sub

    Private Sub cbobanco_DropDown(sender As Object, e As EventArgs) Handles cbobanco.DropDown
        Dim cnn5 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
        Dim rd5 As MySqlDataReader
        Dim cmd5 As MySqlCommand
        Try
            cbobanco.Items.Clear()

            cnn5.Close() : cnn5.Open()
            cmd5 = cnn5.CreateCommand
            cmd5.CommandText = "SELECT DISTINCT Banco FROM bancos WHERE Banco<>'' ORDER BY Banco"
            rd5 = cmd5.ExecuteReader
            Do While rd5.Read
                If rd5.HasRows Then
                    cbobanco.Items.Add(rd5(0).ToString)
                End If
            Loop
            rd5.Close()
            cnn5.Close()

        Catch ex As Exception
            MessageBox.Show(ex.ToString)
            cnn5.Close()
        End Try
    End Sub

    Private Sub cbobanco_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cbobanco.KeyPress
        e.KeyChar = UCase(e.KeyChar)
        If AscW(e.KeyChar) = Keys.Enter Then
            txtnumref.Focus.Equals(True)
        End If
    End Sub

    Private Sub txtnumref_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtnumref.KeyPress
        e.KeyChar = UCase(e.KeyChar)
        If AscW(e.KeyChar) = Keys.Enter Then
            txtmontopago.Focus.Equals(True)
        End If
    End Sub

    Private Sub txtmontopago_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtmontopago.KeyPress
        If AscW(e.KeyChar) = Keys.Enter Then
            If IsNumeric(txtmontopago.Text) Then
                txtComentarioPago.Focus.Equals(True)
            End If
        End If
    End Sub

    Private Sub txtComentarioPago_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtComentarioPago.KeyPress
        e.KeyChar = UCase(e.KeyChar)
        If AscW(e.KeyChar) = Keys.Enter Then
            cboCuentaRecepcion.Focus.Equals(True)
        End If
    End Sub

    Private Sub cboCuentaRecepcion_DropDown(sender As Object, e As EventArgs) Handles cboCuentaRecepcion.DropDown
        Dim cnn5 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
        Dim rd5 As MySqlDataReader
        Dim cmd5 As MySqlCommand
        Try
            cboCuentaRecepcion.Items.Clear()

            cnn5.Close() : cnn5.Open()
            cmd5 = cnn5.CreateCommand
            cmd5.CommandText = "SELECT DISTINCT CuentaBan FROM cuentasbancarias WHERE CuentaBan<>'' ORDER BY CuentaBan"
            rd5 = cmd5.ExecuteReader
            Do While rd5.Read
                If rd5.HasRows Then
                    cboCuentaRecepcion.Items.Add(rd5(0).ToString)
                End If
            Loop
            rd5.Close()
            cnn5.Close()
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
            cnn5.Close()
        End Try
    End Sub

    Private Sub cboCuentaRecepcion_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cboCuentaRecepcion.KeyPress
        e.KeyChar = UCase(e.KeyChar)
        If AscW(e.KeyChar) = Keys.Enter Then
            btnPagos.Focus.Equals(True)
        End If
    End Sub

    Private Sub cboCuentaRecepcion_SelectedValueChanged(sender As Object, e As EventArgs) Handles cboCuentaRecepcion.SelectedValueChanged
        Dim cnn1 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
        Dim rd1 As MySqlDataReader
        Dim cmd1 As MySqlCommand
        Try
            cnn1.Close() : cnn1.Open()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "SELECT Banco FROM cuentasbancarias WHERE CuentaBan='" & cboCuentaRecepcion.Text & "'"
            rd1 = cmd1.ExecuteReader
            If rd1.HasRows Then
                If rd1.Read Then
                    cboBancoRecepcion.Text = rd1(0).ToString
                End If
            End If
            rd1.Close()
            cnn1.Close()
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
            cnn1.Close()
        End Try
    End Sub

    Private Sub btnPagos_Click(sender As Object, e As EventArgs) Handles btnPagos.Click
        Dim tipo As String = cbotpago.Text
        Dim banco As String = cbobanco.Text
        Dim referencia As String = txtnumref.Text
        Dim monto As Double = txtmontopago.Text
        Dim fecha As Date = Format(dtpFecha_P.Value, "yyyy-MM-dd")
        Dim cuenta As String = cboCuentaRecepcion.Text
        Dim bancoref As String = cboBancoRecepcion.Text
        Dim comentario As String = txtComentarioPago.Text
        Dim totalpagos As Double = 0

        If tipo = "MONEDERO" Then
            If referencia = "" Then
                MsgBox("Debe ingresar una refrencia", vbInformation + vbOKOnly, titulomensajes)
                Exit Sub
            End If

            If monto = 0 Then
                MsgBox("Debe ingresar un monto", vbInformation + vbOKOnly, titulomensajes)
                Exit Sub
            End If
        Else
            If tipo = "" Then
                MsgBox("Debe seleccionar una forma de pago", vbInformation + vbOKOnly, titulomensajes)
                Exit Sub
            End If

            If banco = "" Then
                MsgBox("Debe seleccionar una banco", vbInformation + vbOKOnly, titulomensajes)
                Exit Sub
            End If

            If referencia = "" Then
                MsgBox("Debe ingresar una refrencia", vbInformation + vbOKOnly, titulomensajes)
                Exit Sub
            End If

            If monto = 0 Then
                MsgBox("Debe ingresar un monto", vbInformation + vbOKOnly, titulomensajes)
                Exit Sub
            End If
        End If

        grdpago.Rows.Add(tipo, banco, referencia, monto, fecha, comentario, cuenta, bancoref)

        totalpagos = txtPagos.Text + monto
        txtPagos.Text = FormatNumber(totalpagos, 2)


        limpiarforma()
    End Sub

    Public Sub limpiarforma()
        cbotpago.Text = ""
        cbobanco.Text = ""
        txtnumref.Text = ""
        txtmontopago.Text = "0.00"
        cboCuentaRecepcion.Text = ""
        cboBancoRecepcion.Text = ""
        txtComentarioPago.Text = ""
    End Sub

    Private Sub grdpago_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles grdpago.CellDoubleClick
        Dim index As Integer = grdpago.CurrentRow.Index

        Dim importe = grdpago.Rows(index).Cells(3).Value.ToString

        grdpago.Rows.Remove(grdpago.Rows(index))
        txtPagos.Text = txtPagos.Text - CDec(importe)
        txtPagos.Text = FormatNumber(txtPagos.Text, 2)
    End Sub

    Private Sub txtPagos_TextChanged(sender As Object, e As EventArgs) Handles txtPagos.TextChanged

        If Not IsNumeric(txtefectivo.Text) Then Exit Sub
        txttotal.Text = CDbl(txtefectivo.Text) + CDbl(txtPagos.Text)
        txttotal.Text = FormatNumber(txttotal.Text, 2)

    End Sub

    Private Sub frmPagoNomina_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class