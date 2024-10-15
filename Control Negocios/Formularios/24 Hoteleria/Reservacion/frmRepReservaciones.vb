Public Class frmRepReservaciones
    Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint

    End Sub

    Private Sub rbTodos_Click(sender As Object, e As EventArgs) Handles rbTodos.Click
        If (rbTodos.Checked) Then

            cboFiltro.Visible = False
            cboFiltro.Text = ""
            grdCaptura.Rows.Clear()

            Dim estado As String = ""
            Dim fentrada As Date = Nothing
            Dim fsalida As Date = Nothing
            Dim fe As String = ""
            Dim fs As String = ""

            Try
                cnn1.Close() : cnn1.Open()
                cmd1 = cnn1.CreateCommand
                cmd1.CommandText = "SELECT * FROM reservaciones ORDER BY Cliente"
                rd1 = cmd1.ExecuteReader
                Do While rd1.Read
                    If rd1.HasRows Then

                        If rd1("Status").ToString = 1 Then
                            estado = "TERMINADA"
                        Else
                            estado = "EN RESERVACION"
                        End If

                        fentrada = rd1("FEntrada").ToString
                        fe = Format(fentrada, "yyyy-MM-dd")
                        fsalida = rd1("FSalida").ToString
                        fs = Format(fsalida, "yyyy-MM-dd")

                        grdCaptura.Rows.Add(rd1("Cliente").ToString,
                                            rd1("Telefono").ToString,
                                            rd1("Habitacion").ToString,
                                            fe,
                                            fs,
                                            rd1("Asigno").ToString,
                                            rd1("Reservo").ToString,
                                            estado
)
                    End If
                Loop
                rd1.Close()
                cnn1.Close()
            Catch ex As Exception
                MessageBox.Show(ex.ToString)
                cnn1.Close()
            End Try



        End If
    End Sub

    Private Sub rbClientes_Click(sender As Object, e As EventArgs) Handles rbClientes.Click
        If (rbClientes.Checked) Then

            grdCaptura.Rows.Clear()
            cboFiltro.Visible = True
            cboFiltro.Focus.Equals(True)
            cboFiltro.Text = ""
        End If
    End Sub

    Private Sub cboFiltro_DropDown(sender As Object, e As EventArgs) Handles cboFiltro.DropDown

        Try
            cboFiltro.Items.Clear()
            cnn5.Close() : cnn5.Open()
            cmd5 = cnn5.CreateCommand
            cmd5.CommandText = "SELECT DISTINCT Cliente FROM reservaciones WHERE Cliente<>'' ORDER BY Cliente"
            rd5 = cmd5.ExecuteReader
            Do While rd5.Read
                If rd5.HasRows Then
                    cboFiltro.Items.Add(rd5(0).ToString)
                End If
            Loop
            rd5.Close()
            cnn5.Close()

        Catch ex As Exception
            MessageBox.Show(ex.ToString)
            cnn5.Close()
        End Try
    End Sub

    Private Sub btnReporte_Click(sender As Object, e As EventArgs) Handles btnReporte.Click
        Try


            Dim estado As String = ""
            Dim fentrada As Date = Nothing
            Dim fsalida As Date = Nothing
            Dim fe As String = ""
            Dim fs As String = ""

            If (rbClientes.Checked) Then
                cnn1.Close() : cnn1.Open()
                cmd1 = cnn1.CreateCommand
                cmd1.CommandText = "SELECT * FROM reservaciones WHERE Cliente='" & cboFiltro.Text & "'"
                rd1 = cmd1.ExecuteReader
                Do While rd1.Read
                    If rd1.HasRows Then
                        If rd1("Status").ToString = 1 Then
                            Estado = "TERMINADA"
                        Else
                            Estado = "EN RESERVACION"
                        End If

                        fentrada = rd1("FEntrada").ToString
                        fe = Format(fentrada, "yyyy-MM-dd")
                        fsalida = rd1("FSalida").ToString
                        fs = Format(fsalida, "yyyy-MM-dd")

                        grdCaptura.Rows.Add(rd1("Cliente").ToString,
                                            rd1("Telefono").ToString,
                                            rd1("Habitacion").ToString,
                                            fe,
                                            fs,
                                            rd1("Asigno").ToString,
                                            rd1("Reservo").ToString,
                                            Estado
)
                    End If
                Loop
                rd1.Close()
                cnn1.Close()

            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
            cnn5.Close()
        End Try
    End Sub
End Class