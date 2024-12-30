Imports System.IO

Public Class frmMembresias
    Private Sub cboCliente_DropDown(sender As Object, e As EventArgs) Handles cboCliente.DropDown
        Try
            cboCliente.Items.Clear()
            cnn5.Close() : cnn5.Open()
            cmd5 = cnn5.CreateCommand
            cmd5.CommandText = "SELECT DISTINCT Nombre FROM clientes WHERE Nombre<>'' ORDER BY Nombre"
            rd5 = cmd5.ExecuteReader
            If rd5.HasRows Then
                Do While rd5.Read
                    cboCliente.Items.Add(rd5(0).ToString)
                Loop
            End If
            rd5.Close()
            cnn5.Close()

        Catch ex As Exception
            MessageBox.Show(ex.ToString)
            cnn5.Close()
        End Try
    End Sub

    Private Sub cboCliente_SelectedValueChanged(sender As Object, e As EventArgs) Handles cboCliente.SelectedValueChanged
        Try
            cnn1.Close() : cnn1.Open()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "SELECT * FROM membresias WHERE Cliente='" & cboCliente.Text & "'"
            rd1 = cmd1.ExecuteReader
            If rd1.HasRows Then
                If rd1.Read Then
                    lblIdCliente.Text = rd1("IdCliente").ToString
                    txtBarras.Text = rd1("CodBarra").ToString
                    lblCodigo.Text = rd1("Codigo").ToString

                    If File.Exists(My.Application.Info.DirectoryPath & "\ProductosImg" & base & "\" & lblCodigo.Text & ".jpg") Then
                        picMembresia.Image = Image.FromFile(My.Application.Info.DirectoryPath & "\ProductosImg" & base & "\" & lblCodigo.Text & ".jpg")

                    End If
                End If
            End If
            rd1.Close()
            cnn1.Close()

        Catch ex As Exception

        End Try
    End Sub
End Class