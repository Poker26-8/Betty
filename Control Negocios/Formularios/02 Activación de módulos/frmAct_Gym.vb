﻿Public Class frmAct_Gym

    Dim act As Integer = 0

    Private Sub frmAct_Gym_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Label5.Text = Mid(SerialNumber(), 1, 7)
        SFormatos("Gimnasio", "")

        act = DatosRecarga2("Gimnasio")

        Dim resta As Integer = 0
        Try
            cnn1.Close() : cnn1.Open()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "SELECT NumPart FROM formatos WHERE Facturas='Gimnasio'"
            rd1 = cmd1.ExecuteReader
            If rd1.HasRows Then
                If rd1.Read Then
                    resta = rd1(0).ToString
                    If resta = 1 Then
                        btnDesactivar.Enabled = True
                        Button1.Enabled = False
                    Else
                        btnDesactivar.Enabled = False
                        Button1.Visible = True
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
        If MsgBox("¿Deseas activar el módulo de control de gimnasios?", vbInformation + vbOKOnly, "Delsscom Control Negocios Pro") = vbOK Then

            If txtcontra.Text = "" Then MsgBox("Escribe la contraseña de activación." & vbNewLine & "Para generarla conmunícate con tu proveedor de software.", vbInformation + vbOKOnly, "Delsscom Control Negocios Pro") : Exit Sub

            If txtcontra.Text = "jipl2211*" Then
                Try
                    cnn1.Close() : cnn1.Open()

                    cmd1 = cnn1.CreateCommand
                    cmd1.CommandText =
                        "update Formatos set NotasCred='" & FormatDateTime(Date.Now, DateFormat.ShortDate) & "', NumPart=1 where Facturas='Gimnasio'"
                    If cmd1.ExecuteNonQuery Then
                        MsgBox("El sistema de gimnasios ha sido activado de manera correcta." & vbNewLine & "El sistema se cerrará para completar el proceso.", vbInformation + vbOKOnly, titulomensajes)
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

    Private Sub btnDesactivar_Click(sender As Object, e As EventArgs) Handles btnDesactivar.Click
        If MsgBox("¿Deseas desactivar el módulo de control de gimnasios?", vbInformation + vbOKOnly, "Delsscom Control Negocios Pro") = vbOK Then

            If txtcontra.Text = "" Then MsgBox("Escribe la contraseña de activación." & vbNewLine & "Para generarla conmunícate con tu proveedor de software.", vbInformation + vbOKOnly, "Delsscom Control Negocios Pro") : Exit Sub

            If txtcontra.Text = "jipl2211*" Then
                Try
                    cnn1.Close() : cnn1.Open()

                    cmd1 = cnn1.CreateCommand
                    cmd1.CommandText = "UPDATE Formatos SET NotasCred='',NumPart=0 WHERE Facturas='Gimnasio'"
                    If cmd1.ExecuteNonQuery Then
                        MsgBox("El sistema de gimnasios ha sido desactivado de manera correcta." & vbNewLine & "El sistema se cerrará para completar el proceso.", vbInformation + vbOKOnly, titulomensajes)
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
                btnDesactivar.Focus.Equals(True)
            Else
                Button1.Focus.Equals(True)
            End If
        End If
    End Sub
End Class