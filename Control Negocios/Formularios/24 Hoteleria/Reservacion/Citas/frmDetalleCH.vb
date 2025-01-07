Imports MySql.Data.MySqlClient

Public Class frmDetalleCH

    Dim id_consulta As Integer = 0
    Dim cli As String = ""
    Dim hab As String = ""
    Private Sub frmDetalleCH_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Dim cnn1 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
        Dim rd1 As MySqlDataReader
        Dim cmd1 As MySqlCommand

        If (frmCitasH.optDia.Checked) Then
            grdCaptura.Rows.Clear()
            grdCaptura.ColumnCount = 0

            grdCaptura.ColumnCount = 6
            With grdCaptura.Columns(0)
                .Name = "Id"
                .Width = 50
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Visible = False
                .Resizable = DataGridViewTriState.False
            End With
            With grdCaptura.Columns(1)
                .Name = "Hora"
                .Width = 60
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
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
                .Name = "Cliente"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                .Visible = True
                .Resizable = DataGridViewTriState.False
            End With
            With grdCaptura.Columns(4)
                .Name = "Habitación"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                .Visible = True
                .Resizable = DataGridViewTriState.False
            End With
            With grdCaptura.Columns(5)
                .Name = "Activo"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
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

                    If cboHabi.Text = "" Then
                        cmd1.CommandText = "select Hora,Minuto,Id,Asunto,Activo,Cliente,Habitacion from Agenda where Minuto=" & minuto & " and Hora=" & refec & " and Dia=" & Fechita(1) & " and Mes=" & Fechita(2) & " and Anio=" & Fechita(3) & " and Usuario='" & cbousu.Text & "'"
                    Else
                        cmd1.CommandText = "select Hora,Minuto,Id,Asunto,Activo,Cliente,Habitacion from Agenda where Minuto=" & minuto & " and Hora=" & refec & " and Dia=" & Fechita(1) & " and Mes=" & Fechita(2) & " and Anio=" & Fechita(3) & " and Usuario='" & cbousu.Text & "' AND Habitacion='" & cboHabi.Text & "'"
                    End If

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
                            grdCaptura.Rows.Add(rd1("Id").ToString, hora & ":" & minuto, rd1("Asunto").ToString, rd1("Cliente").ToString, rd1("Habitacion").ToString, rd1("Activo").ToString)
                        End If
                    Else
                        If field < 10 Then
                            minuto = "0" & field
                        Else
                            minuto = field
                        End If
                        grdCaptura.Rows.Add("0", refec & ":" & minuto, "", "", "", "NULO")
                    End If
                    rd1.Close()

                    If grdCaptura.Rows(field).Cells(5).Value.ToString = "NULO" Then
                    Else
                        If grdCaptura.Rows(field).Cells(5).Value = False Then
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
            grdCaptura.ColumnCount = 7

            With grdCaptura.Columns(0)
                .Name = "Id"
                .Width = 50
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Visible = False
                .Resizable = DataGridViewTriState.False
            End With
            With grdCaptura.Columns(1)
                .Name = "Hora"
                .Width = 60
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Visible = True
                .Resizable = DataGridViewTriState.False
            End With
            With grdCaptura.Columns(2)
                .Name = "Minuto"
                .Width = 60
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Visible = True
                .Resizable = DataGridViewTriState.False
            End With
            With grdCaptura.Columns(3)
                .Name = "Evento"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                .Visible = True
                .Resizable = DataGridViewTriState.False
            End With
            With grdCaptura.Columns(4)
                .Name = "Activo"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                .Visible = False
                .Resizable = DataGridViewTriState.False
            End With
            With grdCaptura.Columns(5)
                .Name = "Cliente"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                .Visible = True
                .Resizable = DataGridViewTriState.False
            End With
            With grdCaptura.Columns(6)
                .Name = "Habitación"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                .Visible = True
                .Resizable = DataGridViewTriState.False
            End With

            Dim dia As String = ""
            Dim refc As Integer = txtReferencia.Text
            Dim hora As String = ""
            Dim minuto As String = ""

            Try
                cnn1.Close() : cnn1.Open()
                cmd1 = cnn1.CreateCommand
                If cboHabi.Text = "" Then
                    cmd1.CommandText = "select Hora,Minuto,Id,Asunto,Activo,Cliente,Habitacion FROM Agenda where Dia=" & refc & " AND Mes=" & Fechita(2) & " and Anio=" & Fechita(3) & " AND Usuario='" & cbousu.Text & "' order by Hora,Minuto"
                Else
                    cmd1.CommandText = "select Hora,Minuto,Id,Asunto,Activo,Cliente,Habitacion FROM Agenda where Dia=" & refc & " AND Mes=" & Fechita(2) & " and Anio=" & Fechita(3) & " AND Usuario='" & cbousu.Text & "' AND Habitacion='" & cboHabi.Text & "' order by Hora,Minuto"
                End If

                rd1 = cmd1.ExecuteReader
                If rd1.HasRows Then
                    Do While rd1.Read
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
                        grdCaptura.Rows.Add(rd1("Id").ToString, hora, minuto, rd1("Asunto").ToString, rd1("Activo").ToString, rd1("Cliente").ToString, rd1("Habitacion").ToString)
                    Loop
                End If
                rd1.Close()

                For R As Integer = 0 To grdCaptura.Rows.Count - 1
                    If grdCaptura.Rows(R).Cells(4).Value = False Then
                        grdCaptura.Rows(R).DefaultCellStyle.BackColor = Color.Blue
                        grdCaptura.Rows(R).DefaultCellStyle.ForeColor = Color.White
                    End If
                Next
                cnn1.Close()

            Catch ex As Exception
                MessageBox.Show(ex.ToString)
                cnn1.Close()
            End Try

        End If
    End Sub

    Private Sub frmDetalleCH_Activated(sender As Object, e As EventArgs) Handles Me.Activated

        Dim cnn1 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
        Dim rd1 As MySqlDataReader
        Dim cmd1 As MySqlCommand

        If (frmCitasH.optDia.Checked) Then
            grdCaptura.Rows.Clear()
            grdCaptura.ColumnCount = 0

            grdCaptura.ColumnCount = 6
            With grdCaptura.Columns(0)
                .Name = "Id"
                .Width = 50
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Visible = False
                .Resizable = DataGridViewTriState.False
            End With
            With grdCaptura.Columns(1)
                .Name = "Hora"
                .Width = 60
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
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
                .Name = "Cliente"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                .Visible = True
                .Resizable = DataGridViewTriState.False
            End With
            With grdCaptura.Columns(4)
                .Name = "Habitacion"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                .Visible = True
                .Resizable = DataGridViewTriState.False
            End With
            With grdCaptura.Columns(5)
                .Name = "Activo"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
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

                    If cboHabi.Text = "" Then
                        cmd1.CommandText = "select Hora,Minuto,Id,Asunto,Activo,Cliente,Habitacion from Agenda where Minuto=" & minuto & " and Hora=" & refec & " and Dia=" & Fechita(1) & " and Mes=" & Fechita(2) & " and Anio=" & Fechita(3) & " and Usuario='" & cbousu.Text & "'"
                    Else
                        cmd1.CommandText = "select Hora,Minuto,Id,Asunto,Activo,Cliente,Habitacion from Agenda where Minuto=" & minuto & " and Hora=" & refec & " and Dia=" & Fechita(1) & " and Mes=" & Fechita(2) & " and Anio=" & Fechita(3) & " and Usuario='" & cbousu.Text & "' AND Habitacion='" & cboHabi.Text & "'"
                    End If

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
                            grdCaptura.Rows.Add(rd1("Id").ToString, hora & ":" & minuto, rd1("Asunto").ToString, rd1("Cliente").ToString, rd1("Habitacion").ToString, rd1("Activo").ToString)
                        End If
                    Else
                        If field < 10 Then
                            minuto = "0" & field
                        Else
                            minuto = field
                        End If
                        grdCaptura.Rows.Add("0", refec & ":" & minuto, "", "", "", "NULO")
                    End If
                    rd1.Close()

                    If grdCaptura.Rows(field).Cells(5).Value.ToString = "NULO" Then
                    Else
                        If grdCaptura.Rows(field).Cells(5).Value = False Then
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
            grdCaptura.ColumnCount = 7

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
                .Name = "Minuto"
                .Width = 60
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Visible = True
                .Resizable = DataGridViewTriState.False
            End With
            With grdCaptura.Columns(3)
                .Name = "Evento"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                .Visible = True
                .Resizable = DataGridViewTriState.False
            End With
            With grdCaptura.Columns(4)
                .Name = "Activo"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                .Visible = False
                .Resizable = DataGridViewTriState.False
            End With
            With grdCaptura.Columns(5)
                .Name = "Cliente"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                .Visible = True
                .Resizable = DataGridViewTriState.False
            End With
            With grdCaptura.Columns(6)
                .Name = "Habitación"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                .Visible = True
                .Resizable = DataGridViewTriState.False
            End With

            Dim dia As String = ""
            Dim refc As Integer = txtReferencia.Text
            Dim hora As String = ""
            Dim minuto As String = ""

            Try
                cnn1.Close() : cnn1.Open()
                cmd1 = cnn1.CreateCommand
                If cboHabi.Text = "" Then
                    cmd1.CommandText = "select Hora,Minuto,Id,Asunto,Activo,Cliente,Habitacion FROM Agenda where Dia=" & refc & " AND Mes=" & Fechita(2) & " and Anio=" & Fechita(3) & " AND Usuario='" & cbousu.Text & "' order by Hora,Minuto"
                Else
                    cmd1.CommandText = "select Hora,Minuto,Id,Asunto,Activo,Cliente,Habitacion FROM Agenda where Dia=" & refc & " AND Mes=" & Fechita(2) & " and Anio=" & Fechita(3) & " AND Usuario='" & cbousu.Text & "' AND Habitacion='" & cboHabi.Text & "' order by Hora,Minuto"
                End If

                rd1 = cmd1.ExecuteReader
                If rd1.HasRows Then
                    Do While rd1.Read
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
                        grdCaptura.Rows.Add(rd1("Id").ToString, hora, minuto, rd1("Asunto").ToString, rd1("Activo").ToString, rd1("Cliente").ToString, rd1("Habitacion").ToString)
                    Loop
                End If
                rd1.Close()

                For R As Integer = 0 To grdCaptura.Rows.Count - 1
                    If grdCaptura.Rows(R).Cells(4).Value = False Then
                        grdCaptura.Rows(R).DefaultCellStyle.BackColor = Color.Blue
                        grdCaptura.Rows(R).DefaultCellStyle.ForeColor = Color.White
                    End If
                Next
                cnn1.Close()

            Catch ex As Exception
                MessageBox.Show(ex.ToString)
                cnn1.Close()
            End Try

        End If
    End Sub

    Private Sub frmDetalleCH_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        refer = ""
    End Sub

    Private Sub frmDetalleCH_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        refer = ""
    End Sub

    Private Sub btnModificar_Click(sender As Object, e As EventArgs) Handles btnModificar.Click
        If id_consulta = 0 Then
            MsgBox("Seleccione un registro con información para poder modificarlo.", vbInformation + vbOKOnly, titulohotelriaa)
            Exit Sub
        End If
        frmModificarCH.txtID.Text = id_consulta
        frmModificarCH.lblCliente.Text = cli
        frmModificarCH.lblHabitación.Text = hab
        My.Application.DoEvents()
        frmModificarCH.Show()
        Me.Close()
    End Sub

    Private Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click
        Me.Close()
    End Sub

    Private Sub grdCaptura_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles grdCaptura.CellClick
        id_consulta = grdCaptura.CurrentRow.Cells(0).Value.ToString

        If (frmCitasH.optDia.Checked) Then
            cli = grdCaptura.CurrentRow.Cells(3).Value.ToString
            hab = grdCaptura.CurrentRow.Cells(4).Value.ToString
        End If

        If (frmCitasH.optMes.Checked) Then
            cli = grdCaptura.CurrentRow.Cells(5).Value.ToString
            hab = grdCaptura.CurrentRow.Cells(6).Value.ToString
        End If

    End Sub


End Class