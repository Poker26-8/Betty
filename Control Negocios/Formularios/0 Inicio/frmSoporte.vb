Imports System.Data.OleDb
Imports MySql.Data

Public Class frmSoporte
    Public valida As Integer = 0

    Private Sub dtpsoporte_KeyPress(sender As System.Object, e As System.Windows.Forms.KeyPressEventArgs) Handles dtpsoporte.KeyPress
        If AscW(e.KeyChar) = Keys.Enter Then
            txtcontraseña.Focus().Equals(True)
            txtcontraseña.SelectAll()
        End If
    End Sub

    Private Sub txtcontraseña_KeyPress(sender As System.Object, e As System.Windows.Forms.KeyPressEventArgs) Handles txtcontraseña.KeyPress
        If AscW(e.KeyChar) = Keys.Enter Then
            If txtcontraseña.Text = "jipl22" Then
                valida = 1
                btnactualiza.Focus().Equals(True)
                My.Application.DoEvents()
            Else
                MsgBox("Contraseña incorrecta.", vbInformation + vbOKOnly, "Delsscom Control Negocios Pro")
                txtcontraseña.Text = ""
                valida = 0

            End If
        End If
    End Sub

    Private Sub btnactualiza_Click(sender As System.Object, e As System.EventArgs) Handles btnactualiza.Click
        Try
            If valida = 0 Then
                MsgBox("Ingresa la contraseña para continuar...", vbCritical + vbOKOnly, "Delsscom COntrol Negocios Pro")
                txtcontraseña.Focus.Equals(True)
            Else
                Dim conexion As OleDb.OleDbConnection = New OleDb.OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & My.Application.Info.DirectoryPath & "\CIAS.mdb;")
                Dim comando As OleDbCommand = New OleDbCommand
                Dim lector As OleDbDataReader = Nothing

                Dim fecha As String = ""
                Dim cadena As String = ""

                fecha = FormatDateTime(dtpsoporte.Value, DateFormat.ShortDate)
                If fecha <> "" Then
                    cadena = Replace(fecha, "/", "?#$-")
                    cadena = Replace(cadena, "0", "pCjm")
                    cadena = Replace(cadena, "1", "FrDtee")
                    cadena = Replace(cadena, "2", "<pzef>")
                    cadena = Replace(cadena, "3", "_sdbEz")
                    cadena = Replace(cadena, "4", "@ddET")
                    cadena = "***" & cadena
                    cadena = Replace(cadena, "5", "-()opY")
                    cadena = Replace(cadena, "6", "TrdHJ")
                    cadena = Replace(cadena, "7", "Bno_o")
                    cadena = Replace(cadena, "8", "nRtwun")
                    cadena = cadena & "ASDWE"
                    cadena = Replace(cadena, "9", "uuIoo")
                End If

                conexion.Close() : conexion.Open()
                comando = conexion.CreateCommand
                comando.CommandText =
                    "update Server set Soporte='" & cadena & "'"
                comando.ExecuteNonQuery()

                MsgBox("Fecha de soporte actualizada.", vbInformation + vbOKOnly, "Delsscom Control Negocios Pro")
                Login.cboEmpresa_SelectedValueChanged(Login.cboEmpresa, New EventArgs())
                Me.Close()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
            valida = 0
        End Try
    End Sub

    Private Sub frmSoporte_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

    End Sub
End Class