﻿Imports MySql.Data.MySqlClient

Public Class frmMonederos

    Protected Sub folio_monedero()
        Dim id As Integer = 0
        Dim folio As Integer = 0

        Dim cnn2 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
        Dim rd2 As MySqlDataReader
        Dim cmd2 As MySqlCommand
        Try
            cnn2.Close()
            cnn2.Open()

            cmd2 = cnn2.CreateCommand
            cmd2.CommandText =
                 "select max(Id) from Monedero"
            rd2 = cmd2.ExecuteReader
            If rd2.HasRows Then
                If rd2.Read Then
                    id = IIf(rd2(0).ToString = "", 0, rd2(0).ToString)
                Else
                    id = 0
                End If
            Else
                id = 0
            End If
            rd2.Close()
            cnn2.Close()

            folio = id + 1
            txtFolio.Text = folio
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
            cnn2.Close()
        End Try
    End Sub

    Private Sub frmMonederos_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        txtFolio.Focus().Equals(True)
        folio_monedero()
    End Sub

    Private Sub cboCliente_DropDown(sender As System.Object, e As System.EventArgs) Handles cboCliente.DropDown
        cboCliente.Items.Clear()
        Dim cnn1 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
        Dim rd1 As MySqlDataReader
        Dim cmd1 As MySqlCommand
        Try
            cnn1.Close() : cnn1.Open()

            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "select DISTINCT Cliente FROM monedero order by Cliente"
            'cmd1.CommandText = "select DISTINCT Nombre FROM Clientes order by Nombre"
            rd1 = cmd1.ExecuteReader
            Do While rd1.Read
                If rd1.HasRows Then
                    cboCliente.Items.Add(
                         rd1(0).ToString
                         )
                End If
            Loop
            rd1.Close() : cnn1.Close()
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
            cnn1.Close()
        End Try
    End Sub

    Private Sub cboCliente_GotFocus(sender As Object, e As System.EventArgs) Handles cboCliente.GotFocus
        cboCliente.SelectionStart = 0
        cboCliente.SelectionLength = Len(cboCliente.Text)
    End Sub

    Private Sub cboCliente_KeyPress(sender As Object, e As System.Windows.Forms.KeyPressEventArgs) Handles cboCliente.KeyPress
        e.KeyChar = UCase(e.KeyChar)
        If AscW(e.KeyChar) = Keys.Enter Then
            Dim cnn1 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
            Dim rd1 As MySqlDataReader
            Dim cmd1 As MySqlCommand
            Try
                cnn1.Close() : cnn1.Open()

                cmd1 = cnn1.CreateCommand
                cmd1.CommandText =
                     "select Id,Telefono from Clientes where Nombre='" & cboCliente.Text & "'"
                rd1 = cmd1.ExecuteReader
                If rd1.HasRows Then
                    If rd1.Read Then
                        txtidcliente.Text = rd1("Id").ToString()
                        txtTelefono.Text = rd1("Telefono").ToString()
                        dtpCumple.Value = IIf(rd1("Cumple").ToString = "", Date.Now, rd1("Cumple").ToString)
                    End If
                End If
                rd1.Close() : cnn1.Close()
            Catch ex As Exception
                MessageBox.Show(ex.ToString)
                cnn1.Close()
            End Try
            txtTelefono.Focus().Equals(True)
        End If
    End Sub

    Private Sub cboCliente_SelectedValueChanged(sender As Object, e As System.EventArgs) Handles cboCliente.SelectedValueChanged
        Dim cnn1 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
        Dim rd1 As MySqlDataReader
        Dim cmd1 As MySqlCommand
        Try
            cnn1.Close() : cnn1.Open()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText =
                 "select Id,Barras,Cumple from monedero where Cliente='" & cboCliente.Text & "'"
            rd1 = cmd1.ExecuteReader
            If rd1.HasRows Then
                If rd1.Read Then
                    txtidcliente.Text = rd1("Id").ToString()
                    txtTelefono.Text = rd1("Barras").ToString()
                    dtpCumple.Value = IIf(rd1("Cumple").ToString = "", Date.Now, rd1("Cumple").ToString)
                End If
            End If
            rd1.Close() : cnn1.Close()
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
            cnn1.Close()
        End Try
    End Sub

    Private Sub txtTelefono_GotFocus(sender As Object, e As System.EventArgs) Handles txtTelefono.GotFocus
        txtTelefono.SelectionStart = 0
        txtTelefono.SelectionLength = Len(txtTelefono.Text)
    End Sub

    Private Sub txtTelefono_KeyPress(sender As System.Object, e As System.Windows.Forms.KeyPressEventArgs) Handles txtTelefono.KeyPress
        If AscW(e.KeyChar) = Keys.Enter Then
            Dim cnn1 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
            Dim rd1 As MySqlDataReader
            Dim cmd1 As MySqlCommand
            Try
                If txtTelefono.Text <> "" Then
                    cnn1.Close() : cnn1.Open()

                    cmd1 = cnn1.CreateCommand
                    cmd1.CommandText =
                         "select Folio,Id,Cliente,IdCliente,Saldo from Monedero where Barras='" & txtTelefono.Text & "'"
                    rd1 = cmd1.ExecuteReader
                    If rd1.HasRows Then
                        If rd1.Read Then
                            txtFolio.Text = rd1("Folio").ToString
                            txtidmonedero.Text = rd1("Id").ToString
                            cboCliente.Text = rd1("Cliente").ToString
                            txtidcliente.Text = rd1("IdCliente").ToString
                            txtSaldo.Text = FormatNumber(rd1("Saldo").ToString, 2)
                        End If
                    End If
                    rd1.Close() : cnn1.Close()
                End If
                txtSaldo.Focus().Equals(True)
            Catch ex As Exception
                MessageBox.Show(ex.ToString)
                cnn1.Close()
            End Try
        End If
    End Sub

    Private Sub txtSaldo_GotFocus(sender As Object, e As System.EventArgs) Handles txtSaldo.GotFocus
        txtSaldo.SelectionStart = 0
        txtSaldo.SelectionLength = Len(txtSaldo.Text)
    End Sub

    Private Sub txtSaldo_KeyPress(sender As System.Object, e As System.Windows.Forms.KeyPressEventArgs) Handles txtSaldo.KeyPress
        If AscW(e.KeyChar) = Keys.Enter Then
            txtSaldo.Text = FormatNumber(txtSaldo.Text, 2)
            btnGuardar.Focus().Equals(True)
        End If
    End Sub

    Private Sub txtPorcentaje_KeyPress(sender As System.Object, e As System.Windows.Forms.KeyPressEventArgs)
        If AscW(e.KeyChar) = Keys.Enter Then
            btnGuardar.Focus().Equals(True)
        End If
    End Sub

    Private Sub btnNuevo_Click(sender As System.Object, e As System.EventArgs) Handles btnNuevo.Click
        txtidcliente.Text = ""
        txtidmonedero.Text = ""
        txtFolio.Text = ""
        cboCliente.Items.Clear()
        cboCliente.Text = ""
        txtTelefono.Text = ""
        txtSaldo.Text = "0.00"
        dtpCumple.Value = Date.Now
        folio_monedero()
    End Sub

    Private Sub btnBorrar_Click(sender As System.Object, e As System.EventArgs) Handles btnBorrar.Click
        If txtidmonedero.Text = "" Then MsgBox("Selecciona un monedero existente para poder eliminarlo.", vbInformation + vbOKOnly, "Delsscom Control Negocios Pro") : txtFolio.Focus().Equals(True) : Exit Sub
        If txtFolio.Text = "" Then MsgBox("Selecciona un monedero existente para poder eliminarlo.", vbInformation + vbOKOnly, "Delsscom Control Negocios Pro") : txtFolio.Focus().Equals(True) : Exit Sub

        If MsgBox("¿Deseas eliminar el monedero con folio " & txtFolio.Text & "?", vbInformation + vbOKCancel, "Delsscom Control Negocios Pro") = vbOK Then

            Dim cnn1 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
            Dim cmd1 As MySqlCommand

            Try
                cnn1.Close()
                cnn1.Open()

                cmd1 = cnn1.CreateCommand
                cmd1.CommandText =
                     "delete from Monedero where Folio='" & txtFolio.Text & "'"
                If cmd1.ExecuteNonQuery Then
                    MsgBox("Monedero eliminado correctamente.", vbInformation + vbOKOnly, "Delsscom Control Negocios Pro")
                    btnNuevo.PerformClick()
                End If
                cnn1.Close()
            Catch ex As Exception
                MessageBox.Show(ex.ToString)
                cnn1.Close()
            End Try
        End If
    End Sub

    Private Sub btnGuardar_Click(sender As System.Object, e As System.EventArgs) Handles btnGuardar.Click
        If txtFolio.Text = "" Then MsgBox("Falta el folio del monedero.", vbInformation + vbOKOnly, "Delsscom Control Negocios Pro") : txtFolio.Focus().Equals(True)
        If cboCliente.Text = "" Then MsgBox("Selecciona un cliente para asignarle el monedero.", vbInformation + vbOKOnly, "Delsscom Control Negocios Pro") : cboCliente.Focus().Equals(True) : Exit Sub
        If txtTelefono.Text = "" Then MsgBox("Falta el número identificador del monedero.", vbInformation + vbOKOnly, "Delsscom Control Negcios Pro") : txtTelefono.Focus().Equals(True) : Exit Sub

        Dim query As String = ""
        Dim fecha As Date = Date.Now
        Dim fechacumple As String = Format(dtpCumple.Value, "yyyy-MM-dd")

        Dim cnn1 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
        Dim rd1 As MySqlDataReader
        Dim cmd1 As MySqlCommand

        Try
            Dim saldo As Double = txtSaldo.Text

            cnn1.Close()
            cnn1.Open()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "select Id from Monedero where Barras='" & txtTelefono.Text & "'"
            rd1 = cmd1.ExecuteReader
            If rd1.HasRows Then
                If rd1.Read Then
                    query = "update Monedero set Saldo=" & saldo & ", Actualiza='" & Format(Date.Now, "yyyy-MM-dd") & "', Cumple='" & fechacumple & "' where Barras='" & txtTelefono.Text & "'"
                Else
                    query = "insert into Monedero(Folio, Cliente, Saldo, Alta, Barras, Actualiza,Cumple) values('" & txtFolio.Text & "''" & cboCliente.Text & "'," & saldo & ",'" & Format(Date.Now, "yyy-MM-dd") & "','" & txtTelefono.Text & "','" & Format(fecha, "yyyy-MM-dd") & "','" & fechacumple & "')"
                End If
            Else
                query = "insert into Monedero(Folio, Cliente, Saldo, Alta, Barras, Actualiza,Cumple) values('" & txtFolio.Text & "','" & cboCliente.Text & "'," & saldo & ",'" & Format(Date.Now, "yyy-MM-dd") & "','" & txtTelefono.Text & "','" & Format(fecha, "yyyy-MM-dd") & "','" & fechacumple & "')"
            End If
            rd1.Close()

            'cmd1 = cnn1.CreateCommand
            'cmd1.CommandText =
            '    "update Clientes set Telefono='" & txtTelefono.Text & "' where Id=" & txtidcliente.Text
            'cmd1.ExecuteNonQuery()

            cmd1 = cnn1.CreateCommand
            cmd1.CommandText =
                 query
            If cmd1.ExecuteNonQuery Then
                MsgBox("Información registrada correctamente.", vbInformation + vbOKOnly, "Delsscom Control Negocios Pro")
                btnNuevo.PerformClick()
                folio_monedero()
            End If
            cnn1.Close()
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
            cnn1.Close()
        End Try
    End Sub

    Private Sub txtFolio_KeyPress(sender As System.Object, e As System.Windows.Forms.KeyPressEventArgs) Handles txtFolio.KeyPress
        If AscW(e.KeyChar) = Keys.Enter Then
            If txtFolio.Text <> "" Then
                Dim cnn1 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
                Dim rd1 As MySqlDataReader
                Dim cmd1 As MySqlCommand
                Try
                    cnn1.Close() : cnn1.Open()

                    cmd1 = cnn1.CreateCommand
                    cmd1.CommandText =
                         "select Id,Cliente,IdCliente,Barras,Saldo from Monedero where Folio='" & txtFolio.Text & "'"
                    rd1 = cmd1.ExecuteReader
                    If rd1.HasRows Then
                        If rd1.Read Then
                            txtidmonedero.Text = rd1("Id").ToString
                            cboCliente.Text = rd1("Cliente").ToString
                            txtidcliente.Text = rd1("IdCliente").ToString
                            txtTelefono.Text = rd1("Barras").ToString
                            txtSaldo.Text = FormatNumber(rd1("Saldo").ToString, 2)
                        End If
                    Else
                    End If
                    rd1.Close() : cnn1.Close()
                Catch ex As Exception
                    MessageBox.Show(ex.ToString)
                    cnn1.Close()
                End Try
            End If
            cboCliente.Focus().Equals(True)
        End If
    End Sub

    Private Sub frmMonederos_Shown(sender As Object, e As System.EventArgs) Handles Me.Shown
        txtTelefono.Focus().Equals(True)
    End Sub
End Class