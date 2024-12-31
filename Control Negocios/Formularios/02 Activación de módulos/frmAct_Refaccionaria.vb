﻿Imports MySql.Data.MySqlClient

Public Class frmAct_Refaccionaria

    Dim act As Integer = 0

    Private Sub frmAct_Refaccionaria_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Label5.Text = Mid(SerialNumber(), 1, 7)
        SFormatos("Refaccionaria", "")
        act = DatosRecarga2("Refaccionaria")

        Dim REFA As Integer = 0

        Dim cnn1 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
        Dim rd1 As MySqlDataReader
        Dim cmd1 As MySqlCommand

        Try
            cnn1.Close() : cnn1.Open()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "SELECT NumPart FROM Formatos WHERE Facturas='Refaccionaria'"
            rd1 = cmd1.ExecuteReader
            If rd1.HasRows Then
                If rd1.Read Then
                    REFA = rd1(0).ToString
                    If REFA = 1 Then
                        btndesactivar.Enabled = True
                        Button1.Enabled = False

                    Else
                        btndesactivar.Enabled = False
                        Button1.Enabled = True
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

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        If MsgBox("¿Deseas activar el módulo de refaccionaria?", vbInformation + vbOKOnly, "Delsscom Control Negocios Pro") = vbOK Then

            If txtcontra.Text = "" Then MsgBox("Escribe la contraseña de activación." & vbNewLine & "Para generarla conmunícate con tu proveedor de software.", vbInformation + vbOKOnly, "Delsscom Control Negocios Pro") : Exit Sub

            Dim cnn1 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
            Dim rd1 As MySqlDataReader
            Dim cmd1 As MySqlCommand

            If txtcontra.Text = "jipl2211*" Then
                Try
                    cnn1.Close() : cnn1.Open()

                    cmd1 = cnn1.CreateCommand
                    cmd1.CommandText =
                        "update Formatos set NotasCred='" & FormatDateTime(Date.Now, DateFormat.ShortDate) & "', NumPart=1 where Facturas='Refaccionaria'"
                    If cmd1.ExecuteNonQuery Then
                        MsgBox("El sistema de refaccionaria ha sido activado de manera correcta." & vbNewLine & "El sistema se cerrará para completar el proceso.", vbInformation + vbOKOnly, titulomensajes)
                        End
                    End If

                    cnn1.Close()
                Catch ex As Exception
                    MessageBox.Show(ex.ToString())
                    cnn1.Close()
                End Try
            Else
                MsgBox("La clave de activación no es correcta.", vbInformation + vbOKOnly, "Delsscom Control Negocios Pro")
                txtcontra.SelectAll() : Exit Sub
            End If
        End If

    End Sub

    Private Sub btndesactivar_Click(sender As Object, e As EventArgs) Handles btndesactivar.Click
        If MsgBox("¿Deseas desactivar el módulo de refaccionaria?", vbInformation + vbOKOnly, "Delsscom Control Negocios Pro") = vbOK Then

            If txtcontra.Text = "" Then MsgBox("Escribe la contraseña de activación." & vbNewLine & "Para generarla conmunícate con tu proveedor de software.", vbInformation + vbOKOnly, "Delsscom Control Negocios Pro") : Exit Sub

            Dim cnn1 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
            Dim rd1 As MySqlDataReader
            Dim cmd1 As MySqlCommand

            If txtcontra.Text = "jipl2211*" Then
                Try
                    cnn1.Close() : cnn1.Open()

                    cmd1 = cnn1.CreateCommand
                    cmd1.CommandText =
                        "update Formatos set NotasCred='0', NumPart=0 where Facturas='Refaccionaria'"
                    If cmd1.ExecuteNonQuery Then
                        MsgBox("El sistema de refaccionaria ha sido desactivado de manera correcta." & vbNewLine & "El sistema se cerrará para completar el proceso.", vbInformation + vbOKOnly, titulomensajes)
                        End
                    End If

                    cnn1.Close()
                Catch ex As Exception
                    MessageBox.Show(ex.ToString())
                    cnn1.Close()
                End Try
            Else
                MsgBox("La clave de activación no es correcta.", vbInformation + vbOKOnly, "Delsscom Control Negocios Pro")
                txtcontra.SelectAll() : Exit Sub
            End If

        End If
    End Sub

    Private Sub txtcontra_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtcontra.KeyPress

        If AscW(e.KeyChar) = Keys.Enter Then
            If act = 1 Then
                btndesactivar.Focus.Equals(True)
            Else
                Button1.Focus.Equals(True)
            End If
        End If

    End Sub
End Class