Public Class frmDetalleCH
    Private Sub frmDetalleCH_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        If (frmCitasH.optDia.Checked) Then
            grdCaptura.Rows.Clear()
            grdCaptura.ColumnCount = 0

            grdCaptura.ColumnCount = 4
            With grdCaptura.Columns(0)
                .Name = "Id"
                .Width = 50
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Visible = False
                .Resizable = DataGridViewTriState.False
            End With
            With grdCaptura.Columns(1)
                .Name = "Hora"
                .Width = 60
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Visible = True
                .Resizable = DataGridViewTriState.False
            End With
            With grdCaptura.Columns(2)
                .Name = "Evento"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                .Visible = True
                .Resizable = DataGridViewTriState.False
            End With
            With grdCaptura.Columns(3)
                .Name = "Activo"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                .Visible = False
                .Resizable = DataGridViewTriState.False
            End With

            Dim minuto As String = ""
            Dim refec As String = Mid(txtReferencia.Text, 1, 2)
            Dim hora As String = ""

            Try
                cnn1.Close() : cnn1.Open()

                For field As Integer = 0 To 60
                    If field = 60 Then Exit Sub
                    If field < 10 Then
                        minuto = "0" & field
                    Else
                        minuto = field
                    End If

                    cmd1 = cnn1.CreateCommand
                    cmd1.CommandText = "select Hora,Minuto,Id,Asunto,Activo from Agenda where Minuto=" & minuto & " and Hora=" & refec & " and Dia=" & Fechita(1) & " and Mes=" & Fechita(2) & " and Anio=" & Fechita(3) & " and Usuario='" & cbousu.Text & "' AND Habitacion='" & cbohabi.text & "'"
                    rd1 = cmd1.ExecuteReader
                    If rd1.HasRows Then
                        If rd1.Read Then
                            If CDec(rd1("Hora").ToString) < 10 Then
                                hora = "0" & rd1("Hora").ToString
                            Else
                                hora = rd1("Hora").ToString
                            End If
                            If CDec(rd1("Minuto").ToString) < 10 Then
                                minuto = "0" & rd1("Minuto").ToString
                            Else
                                minuto = rd1("Minuto").ToString
                            End If
                            grdCaptura.Rows.Add(rd1("Id").ToString, hora & ":" & minuto, rd1("Asunto").ToString, rd1("Activo").ToString)
                        End If
                    Else
                        If field < 10 Then
                            minuto = "0" & field
                        Else
                            minuto = field
                        End If
                        grdCaptura.Rows.Add("0", refec & ":" & minuto, "", "NULO")
                    End If
                    rd1.Close()

                    If grdCaptura.Rows(field).Cells(3).Value.ToString = "NULO" Then
                    Else
                        If grdCaptura.Rows(field).Cells(3).Value = False Then
                            grdCaptura.Rows(field).DefaultCellStyle.BackColor = Color.Blue
                            grdCaptura.Rows(field).DefaultCellStyle.ForeColor = Color.White
                        End If
                    End If
                Next
                cnn1.Close()
            Catch ex As Exception
                MessageBox.Show(ex.ToString)
                cnn1.Close()
            End Try
        End If

        If (frmCitasH.optMes.Checked) Then
            grdCaptura.Rows.Clear()
            grdCaptura.ColumnCount = 0
            grdCaptura.ColumnCount = 5
        End If
    End Sub
End Class