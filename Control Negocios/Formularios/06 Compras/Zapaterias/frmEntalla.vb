Public Class frmEntalla

    Dim cantpro As Integer = 0
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
            cmd5.CommandText = "SELECT DISTINCT NumRemision FROM comprasentalla"
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
            txtCantidad.Text = "00"

            Dim miidcompra As Integer = 0

            If Len(cboModelo) > 0 And Len(cboRemision) > 0 Then
                cnn1.Close() : cnn1.Open()
                cnn2.Close() : cnn2.Open()

                cmd1 = cnn1.CreateCommand
                cmd1.CommandText = "SELECT * FROM comprasentalla WHERE Modelo='" & cboRemision.Text & "'"
                rd1 = cmd1.ExecuteReader
                If rd1.HasRows Then
                    If rd1.Read Then
                        miidcompra = rd1("Id_compra").ToString
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
                cmd1.CommandText = "SELECT * FROM productos WHERE Grupo='" & cboModelo.Text & "'"
                rd1 = cmd1.ExecuteReader
                Do While rd1.Read
                    If rd1.HasRows Then

                        cmd2 = cnn2.CreateCommand
                        cmd2.CommandText = "SELECT * FROM comprasdet WHERE Id_compra=" & miidcompra
                        rd2 = cmd2.ExecuteReader
                        If rd2.HasRows Then
                            If rd2.Read Then

                            End If
                        End If
                        rd2.Close()
                    End If
                Loop
                rd1.Close()
                cnn2.Close()
                cnn1.Close()


            End If

        End If
    End Sub
End Class