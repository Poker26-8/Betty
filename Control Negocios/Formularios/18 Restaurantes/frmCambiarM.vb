﻿Imports MySql.Data.MySqlClient

Public Class frmCambiarM
    Private Sub frmCambiarM_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub cbomesa_DropDown(sender As Object, e As EventArgs) Handles cbomesa.DropDown

        Dim cnn5 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
        Dim rd5 As MySqlDataReader
        Dim cmd5 As MySqlCommand
        Try
            cbomesa.Items.Clear()

            cnn5.Close() : cnn5.Open()
            cmd5 = cnn5.CreateCommand
            cmd5.CommandText = "SELECT DISTINCT Nombre_mesa FROM Mesa WHERE Nombre_mesa<>'' ORDER BY Nombre_mesa"
            rd5 = cmd5.ExecuteReader
            Do While rd5.Read
                If rd5.HasRows Then
                    cbomesa.Items.Add(rd5(0).ToString)
                End If
            Loop
            rd5.Close()
            cnn5.Close()

        Catch ex As Exception
            MessageBox.Show(ex.ToString)
            cnn5.Close()
        End Try
    End Sub

    Private Sub btnSalir_Click(sender As Object, e As EventArgs) Handles btnSalir.Click
        Me.Close()
    End Sub

    Private Sub btnCambiar_Click(sender As Object, e As EventArgs) Handles btnCambiar.Click

        Dim cnn1 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
        Dim cnn2 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
        Dim cnn3 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
        Dim rd1, rd2, rd3 As MySqlDataReader
        Dim cmd1, cmd2, cmd3 As MySqlCommand

        Try
            If cbomesa.Text = "" Then MsgBox("Necesita seleccionar la mesa de destino", vbInformation + vbOKOnly, titulorestaurante) : Exit Sub : cbomesa.Focus.Equals(True)

            Dim varconta As Integer = 0
            Dim idemp As Integer = 0

            cnn1.Close() : cnn1.Open()
            cnn2.Close() : cnn2.Open()

            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "SELECT IdEmpleado FROM usuarios WHERE Alias='" & frmMesas.lblusuario.Text & "'"
            rd1 = cmd1.ExecuteReader
            If rd1.HasRows Then
                If rd1.Read Then
                    idemp = rd1(0).ToString

                    cmd2 = cnn2.CreateCommand
                    cmd2.CommandText = "SELECT CambioM FROM permisosm WHERE IdEmpleado=" & idemp
                    rd2 = cmd2.ExecuteReader
                    If rd2.HasRows Then
                        If rd2.Read Then
                            If rd2(0).ToString = 1 Then
                            Else
                                MsgBox("No cuentas con permiso para cambiar de mesa", vbInformation + vbOKOnly, titulorestaurante)
                                Exit Sub
                            End If
                        End If
                    Else
                        MsgBox("No tienes asignados permisos contacta a tu administrador", vbInformation + vbOKOnly, titulorestaurante)
                        Exit Sub
                    End If
                    rd2.Close()

                End If
            Else
                MsgBox("El usuario no esta registrado", vbInformation + vbOKOnly, titulorestaurante)
                Exit Sub
            End If
            rd1.Close()
            cnn2.Close()

            cnn1.Close() : cnn1.Open()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "SELECT Contabiliza FROM Mesa WHERE Nombre_mesa='" & lblmesa.Text & "'"
            rd1 = cmd1.ExecuteReader
            If rd1.HasRows Then
                If rd1.Read Then
                    varconta = rd1("Contabiliza").ToString
                End If
            End If
            rd1.Close()

            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "SELECT Contabiliza FROM mesa WHERE Nombre_mesa='" & lblmesa.Text & "'"
            rd1 = cmd1.ExecuteReader
            If rd1.HasRows Then
                If rd1.Read Then
                    If rd1("Contabiliza").ToString = 1 Then

                        cnn2.Close() : cnn2.Open()
                        cmd2 = cnn2.CreateCommand
                        cmd2.CommandText = "SELECT Id FROM asigpc WHERE Nombre='" & lblmesa.Text & "'"
                        rd2 = cmd2.ExecuteReader
                        If rd2.HasRows Then
                            If rd2.Read Then
                                MsgBox("Tiene que terminar el tiempo de la mesa para poder cambiarla", vbInformation + vbOKOnly, titulorestaurante)
                                Exit Sub
                            End If
                        End If
                        rd2.Close()
                        cnn2.Close()


                    End If
                End If
            End If
            rd1.Close()
            cnn1.Close()

            If lblmesa.Text <> "" Then

                cnn3.Close() : cnn3.Open()
                cmd3 = cnn3.CreateCommand
                cmd3.CommandText = "UPDATE Comandas SET NMESA='" & cbomesa.Text & "' WHERE NMESA='" & lblmesa.Text & "'"
                cmd3.ExecuteNonQuery()

                cmd3 = cnn3.CreateCommand
                cmd3.CommandText = "UPDATE Comanda1 SET Nombre='" & cbomesa.Text & "' WHERE Nombre='" & lblmesa.Text & "'"
                cmd3.ExecuteNonQuery()

                cmd3 = cnn3.CreateCommand
                cmd3.CommandText = "UPDATE Rep_Comandas SET NMESA='" & cbomesa.Text & "' WHERE NMESA='" & lblmesa.Text & "'"
                cmd3.ExecuteNonQuery()
                cnn3.Close()

            Else
                MsgBox("Seleccione la mesa para el cambio", vbInformation + vbOKOnly, titulomensajes)
                Exit Sub
            End If
            lblmesa.Text = ""
            cbomesa.Text = ""
            btnSalir.PerformClick()
            frmMesas.btnLimpiar.PerformClick()
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
            cnn1.Close()
        End Try
    End Sub
End Class