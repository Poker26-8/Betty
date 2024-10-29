Public Class frmCitasH
    Dim id_cita As Integer = 0
    Dim tipo As Integer = 0

    Private Sub frmCitasH_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        cboHabitacion.Focus.Equals(True)
        tHora.Start()

        My.Application.DoEvents()
        tActuales.Start()

        My.Application.DoEvents()
        tEstado.Start()

        refer = ""
        My.Application.DoEvents()

        btnAgregar.Visible = True
        btnModificar.Visible = False
        btnDetalle.Visible = False
    End Sub

    Private Sub tHora_Tick(sender As Object, e As EventArgs) Handles tHora.Tick

        lblHora.Text = FormatDateTime(Now, DateFormat.ShortTime)

        Dim CHora As String = ""

        CHora = Mid(lblHora.Text, 1, 2)
        lblHora.Tag = CHora

        Dim valor As Integer = Now.DayOfWeek
        Dim dia As String = ""
        If valor = 1 Then
            dia = "Lunes"
        ElseIf valor = 2 Then
            dia = "Martes"
        ElseIf valor = 3 Then
            dia = "Miércoles"
        ElseIf valor = 4 Then
            dia = "Jueves"
        ElseIf valor = 5 Then
            dia = "Viernes"
        ElseIf valor = 6 Then
            dia = "Sábado"
        ElseIf valor = 7 Then
            dia = "Domingo"
        End If

        Dim dian As String = Format(Date.Now, "dd")

        lblDía.Text = dia & ", " & dian
        lblDía.Tag = dia

        My.Application.DoEvents()

        Dim val As Integer = Now.Month
        Dim mes As String = ""
        If val = 1 Then
            mes = "Enero"
        ElseIf val = 2 Then
            mes = "Febrero"
        ElseIf val = 3 Then
            mes = "Marzo"
        ElseIf val = 4 Then
            mes = "Abril"
        ElseIf val = 5 Then
            mes = "Mayo"
        ElseIf val = 6 Then
            mes = "Junio"
        ElseIf val = 7 Then
            mes = "Julio"
        ElseIf val = 8 Then
            mes = "Agosto"
        ElseIf val = 9 Then
            mes = "Septiembre"
        ElseIf val = 10 Then
            mes = "Octubre"
        ElseIf val = 11 Then
            mes = "Noviembre"
        ElseIf val = 12 Then
            mes = "Diciembre"
        End If
        lblMes.Text = mes

        My.Application.DoEvents()

        Dim Hoy As String = Format(Now, "dd/MM/yyyy")
        Dim X As Integer = 0

        For vic As Integer = 0 To 3
            X = InStr(1, Hoy, "/") - 1
            If X < 0 Then Exit For
            If vic = 0 Then
                Fechita(1) = Mid(Hoy, 1, InStr(1, Hoy, "/") - 1)
            ElseIf vic = 1 Then
                Fechita(2) = Mid(Hoy, 1, InStr(1, Hoy, "/") - 1)
            End If
            Hoy = Mid(Hoy, InStr(1, Hoy, "/") + 1, 200)
        Next
        Fechita(3) = Hoy

        Dim A_horita As String = Format(Now, "HH:MM")
        Dim S As Integer = 0

        For dan As Integer = 0 To 2
            S = InStr(1, A_horita, ":") - 1
            If S < 0 Then Exit For
            If dan = 0 Then
                Tiempo(1) = Mid(A_horita, 1, InStr(1, A_horita, ":") - 1)
            End If
            A_horita = Mid(A_horita, InStr(1, A_horita, ":") + 1, 200)
        Next
        Tiempo(2) = A_horita

    End Sub

    Private Sub tActuales_Tick(sender As Object, e As EventArgs) Handles tActuales.Tick
        If (optDia.Checked) Then
            ActuDiaHab(grdCaptura, cboUsuario.Text, cboHabitacion.Text)
        End If

        If (optHora.Checked) Then
            ActuHora(grdCaptura, cboUsuario.Text, cboHabitacion.Text)
        End If

        If (optMes.Checked) Then
            ActuMes(grdCaptura, cboUsuario.Text, cboHabitacion.Text)
        End If
    End Sub

    Public Sub ActuDiaHab(ByRef grid As DataGridView, ByRef usuario As String, ByRef habita As String)

        grid.Rows.Clear()

        Dim HORA As String = ""
        Dim HORX As String = ""
        Dim EVENTO As String = ""
        Dim min As String = ""
        Try
            cnn2.Close() : cnn2.Open()
            cnn3.Close() : cnn3.Open()

            For field As Integer = 0 To 23
                If field < 10 Then
                    HORA = "0" & field
                Else
                    HORA = field
                End If

                cmd2 = cnn2.CreateCommand

                If cboHabitacion.Text = "" Then
                    cmd2.CommandText = "SELECT Hora,Id,Minuto,Activo FROM agenda WHERE Hora=" & HORA & " AND Dia=" & Fechita(1) & " AND mes=" & Fechita(2) & " AND Anio=" & Fechita(3) & " AND Usuario='" & usuario & "'"
                Else
                    cmd2.CommandText = "SELECT Hora,Id,Minuto,Activo FROM agenda WHERE Hora=" & HORA & " AND Dia=" & Fechita(1) & " AND mes=" & Fechita(2) & " AND Anio=" & Fechita(3) & " AND Usuario='" & usuario & "' AND Habitacion='" & cboHabitacion.Text & "'"
                End If

                rd2 = cmd2.ExecuteReader
                If rd2.HasRows Then
                    If rd2.Read Then
                        cmd3 = cnn3.CreateCommand

                        If cboHabitacion.Text = "" Then
                            cmd3.CommandText = "SELECT Asunto FROM agenda WHERE Hora=" & HORA & " AND Dia=" & Fechita(1) & " AND Mes=" & Fechita(2) & " AND Anio=" & Fechita(3) & " AND Usuario='" & usuario & "'"
                        Else
                            cmd3.CommandText = "SELECT Asunto FROM agenda WHERE Hora=" & HORA & " AND Dia=" & Fechita(1) & " AND Mes=" & Fechita(2) & " AND Anio=" & Fechita(3) & " AND Usuario='" & usuario & "' AND Habitacion='" & habita & "'"
                        End If

                        rd3 = cmd3.ExecuteReader
                        If rd3.HasRows Then
                            Do While rd3.Read
                                EVENTO = rd3(0).ToString
                            Loop
                        End If
                        rd3.Close()

                        If rd2("Hora").ToString < 10 Then
                            HORX = "0" & rd2("Hora").ToString
                        Else
                            HORX = rd2("Hora").ToString
                        End If

                        If rd2("Minuto").ToString = "0" Then
                            min = "0" & rd2("Minuto").ToString
                        Else
                            min = rd2("Minuto").ToString
                        End If
                        grid.Rows.Add(rd2("Id").ToString, rd2("Hora").ToString & ":" & min, EVENTO, rd2("Activo").ToString)
                    End If

                Else
                    If HORA < 10 Then
                        HORX = "0" & field
                    Else
                        HORX = field
                    End If
                    grid.Rows.Add("0", HORA & ":00", "", "DX")
                End If
                rd2.Close()
            Next
            cnn2.Close()
            cnn3.Close()
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
            cnn2.Close()
            cnn3.Close()
        End Try
    End Sub

    Public Sub ActuHora(ByVal grid As DataGridView, ByRef usuario As String, ByVal habita As String)

        grdCaptura.Rows.Clear()
        Dim minuto As String = ""
        Dim hora As String = ""
        Dim valortiem As String = ""
        Try
            cnn2.Close() : cnn2.Open()
            For field As Integer = 0 To 60
                If field = 60 Then Exit Sub
                minuto = field

                cmd2 = cnn2.CreateCommand

                If cboHabitacion.Text = "" Then
                    cmd2.CommandText = "select Hora,Minuto,Id,Asunto,Activo from Agenda where Minuto=" & minuto & " and Hora=" & Tiempo(1) & " and Dia=" & Fechita(1) & " and Mes=" & Fechita(2) & " and Anio=" & Fechita(3) & " and Usuario='" & usuario & "'"
                Else
                    cmd2.CommandText = "select Hora,Minuto,Id,Asunto,Activo from Agenda where Minuto=" & minuto & " and Hora=" & Tiempo(1) & " and Dia=" & Fechita(1) & " and Mes=" & Fechita(2) & " and Anio=" & Fechita(3) & " and Usuario='" & usuario & "' AND Habitacion='" & habita & "'"
                End If

                rd2 = cmd2.ExecuteReader
                If rd2.HasRows Then
                    If rd2.Read Then
                        If rd2("Hora").ToString < 10 Then
                            hora = "0" & rd2("Hora").ToString
                        Else
                            hora = rd2("Hora").ToString
                        End If

                        If rd2("Minuto").ToString < 10 Then
                            minuto = "0" & rd2("Minuto").ToString
                        Else
                            minuto = rd2("Minuto").ToString
                        End If

                        grdCaptura.Rows.Add(rd2("Id").ToString, hora & ":" & minuto, rd2("Asunto").ToString, rd2("Activo").ToString)
                    End If
                Else
                    If field < 10 Then
                        minuto = "0" & field
                    Else
                        minuto = field
                    End If

                    If CDec(Tiempo(1)) < 10 Then
                        hora = "0" & Tiempo(1)
                    Else
                        hora = Tiempo(1)
                    End If
                    valortiem = hora & ":" & minuto

                    grdCaptura.Rows.Add("0", valortiem, "", "DX")
                End If


                If grdCaptura.Rows(field).Cells(3).Value.ToString = "DX" Then
                Else
                    If grdCaptura.Rows(field).Cells(3).Value = 0 Then
                        grdCaptura.Rows(field).DefaultCellStyle.BackColor = Color.Blue
                        grdCaptura.Rows(field).DefaultCellStyle.ForeColor = Color.White
                    End If
                End If
                My.Application.DoEvents()
                rd2.Close()
            Next
            cnn2.Close()
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
            cnn2.Close()
        End Try
    End Sub

    Public Sub ActuMes(ByRef grid As DataGridView, ByRef usuario As String, ByRef habita As String)
        grdCaptura.Rows.Clear()
        grid.Rows.Clear()
        Dim dia As String = ""
        Dim evento As String = ""

        Try
            cnn2.Close() : cnn2.Open()
            cnn3.Close() : cnn3.Open()

            For field As Integer = 1 To Date.DaysInMonth(Now.Year, Now.Month)
                dia = field

                cmd2 = cnn2.CreateCommand

                If cboHabitacion.Text = "" Then
                    cmd2.CommandText = "select Id,Dia,Activo from Agenda where Dia=" & dia & " and Mes=" & Fechita(2) & " and Anio=" & Fechita(3) & " and Usuario='" & usuario & "'"
                Else
                    cmd2.CommandText = "select Id,Dia,Activo from Agenda where Dia=" & dia & " and Mes=" & Fechita(2) & " and Anio=" & Fechita(3) & " and Usuario='" & usuario & "' AND Habitacion='" & cboHabitacion.Text & "'"
                End If

                rd2 = cmd2.ExecuteReader
                If rd2.HasRows Then
                    If rd2.Read Then
                        cmd3 = cnn3.CreateCommand
                        If cboHabitacion.Text = "" Then
                            cmd3.CommandText = "SELECT Asunto FROM Agenda WHERE Dia=" & dia & " AND Mes=" & Fechita(2) & " AND Anio=" & Fechita(3) & " AND Usuario='" & usuario & "'"
                        Else
                            cmd3.CommandText = "SELECT Asunto FROM Agenda WHERE Dia=" & dia & " AND Mes=" & Fechita(2) & " AND Anio=" & Fechita(3) & " AND Usuario='" & usuario & "' AND Habitacion='" & habita & "'"
                        End If
                        rd3 = cmd3.ExecuteReader
                        If rd3.HasRows Then
                            If rd3.Read Then
                                evento = rd3(0).ToString
                            End If
                        End If
                        rd3.Close()

                        grdCaptura.Rows.Add(rd2("Id").ToString, rd2("Dia").ToString, evento, rd2("Activo").ToString)
                    End If
                Else
                    grdCaptura.Rows.Add("0", dia, "", "DX")
                End If


                If grdCaptura.Rows(field - 1).Cells(3).Value.ToString = "DX" Then
                Else
                    If grdCaptura.Rows(field - 1).Cells(3).Value = False Then
                        grdCaptura.Rows(field - 1).DefaultCellStyle.BackColor = Color.Blue
                        grdCaptura.Rows(field - 1).DefaultCellStyle.ForeColor = Color.White
                    End If
                End If
                My.Application.DoEvents()
                rd2.Close()
            Next
            cnn3.Close()
            cnn2.Close()
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
            cnn2.Close()
            cnn3.Close()
        End Try
    End Sub

    Public Sub EstadoH()
        Try
            cnn1.Close() : cnn1.Open()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "UPDATE agenda SET Activo=0 WHERE Anio<=" & CDec(Fechita(3)) & " AND Mes<=" & CDec(Fechita(2)) & " AND Dia<" & CDec(Fechita(1)) & ""
            cmd1.ExecuteNonQuery()

            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "UPDATE agenda SET Activo=0 WHERE Anio<=" & CDec(Fechita(3)) & " AND Mes<=" & CDec(Fechita(2)) & " AND Dia<" & CDec(Fechita(1)) & " AND Hora<" & CDec(Tiempo(1)) & ""
            cmd1.ExecuteNonQuery()

            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "UPDATE Agenda SET Activo=0 WHERE Anio<=" & CDec(Fechita(3)) & " AND Mes<=" & CDec(Fechita(2)) & " AND Dia<=" & CDec(Fechita(1)) & " AND Hora<=" & CDec(Fechita(1)) & " AND Minuto<" & CDec(Tiempo(2)) & ""
            cmd1.ExecuteNonQuery()
            cnn1.Close()

        Catch ex As Exception
            MessageBox.Show(ex.ToString)
            cnn1.Close()
        End Try
    End Sub

    Private Sub optMes_Click(sender As Object, e As EventArgs) Handles optMes.Click
        grdCaptura.Rows.Clear()
        tipo = 3

        With grdCaptura.Columns(1)
            .Name = "Dia"
            '.Width = 50
            '.AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            '.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            '.Visible = False
            '.Resizable = DataGridViewTriState.False
        End With

        If cboUsuario.Text = "" And cboHabitacion.Text = "" Then
            MsgBox("Seleccione un usuario y habitación", vbInformation + vbOKOnly, titulohotelriaa)
            cboUsuario.Focus.Equals(True)
            cboHabitacion.Focus.Equals(True)
            optMes.Checked = True
            Exit Sub
        End If


        Dim R As Integer = Date.DaysInMonth(Now.Year, Now.Month)
        Dim dia As String = ""
        Dim evento As String = ""

        Try
            cnn4.Close() : cnn4.Open()
            cnn2.Close() : cnn2.Open()

            For field As Integer = 1 To R
                dia = field
                cmd4 = cnn4.CreateCommand

                If cboHabitacion.Text = "" Then
                    cmd4.CommandText = "select Id,Dia,Activo from Agenda where Dia=" & dia & " and Mes=" & Fechita(2) & " and Anio=" & Fechita(3) & " and Usuario='" & cboUsuario.Text & "'"
                Else
                    cmd4.CommandText = "select Id,Dia,Activo from Agenda where Dia=" & dia & " and Mes=" & Fechita(2) & " and Anio=" & Fechita(3) & " and Usuario='" & cboUsuario.Text & "' AND Habitacion='" & cboHabitacion.Text & "'"
                End If


                rd4 = cmd4.ExecuteReader
                If rd4.HasRows Then
                    If rd4.Read Then
                        cmd2 = cnn2.CreateCommand

                        If cboHabitacion.Text = "" Then
                            cmd2.CommandText = "SELECT Asunto FROM agenda WHERE Dia=" & dia & " AND Mes=" & Fechita(2) & " AND anio=" & Fechita(3) & " AND Usuario='" & cboUsuario.Text & "'"
                        Else
                            cmd2.CommandText = "SELECT Asunto FROM agenda WHERE Dia=" & dia & " AND Mes=" & Fechita(2) & " AND anio=" & Fechita(3) & " AND Usuario='" & cboUsuario.Text & "' AND Habitacion='" & cboHabitacion.Text & "'"
                        End If

                        rd2 = cmd2.ExecuteReader
                        Do While rd2.Read
                            If rd2.HasRows Then
                                evento = rd2(0).ToString
                            End If
                        Loop
                        rd2.Close()

                        grdCaptura.Rows.Add(rd4("Id").ToString, rd4("Dia").ToString, evento, rd4("Activo").ToString)
                    End If
                Else
                    grdCaptura.Rows.Add("0", dia, "", "DX")
                End If

                If grdCaptura.Rows(field - 1).Cells(3).Value.ToString = "DX" Then
                Else
                    If grdCaptura.Rows(field - 1).Cells(3).Value = False Then
                        grdCaptura.Rows(field - 1).DefaultCellStyle.BackColor = Color.Blue
                        grdCaptura.Rows(field - 1).DefaultCellStyle.ForeColor = Color.White
                    End If
                End If
                My.Application.DoEvents()
                rd4.Close()
            Next
            cnn4.Close()
            cnn2.Close()
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
            cnn4.Close()
            cnn3.Close()
        End Try

        btnAgregar.Visible = True
        btnModificar.Visible = False
        btnDetalle.Visible = True
    End Sub

    Private Sub optDia_Click(sender As Object, e As EventArgs) Handles optDia.Click
        grdCaptura.Rows.Clear()
        tipo = 2
        If cboUsuario.Text = "" And cboHabitacion.Text = "" Then
            MsgBox("Seleccione un usuario y habitación.", vbInformation + vbOKOnly, titulohotelriaa)
            cboUsuario.Focus.Equals(True)
            cboHabitacion.Focus.Equals(True)
            optDia.Checked = True
            Exit Sub
        End If

        Dim hora As String = ""
        Dim horx As String = ""
        Dim evento As String = ""
        Try
            cnn3.Close() : cnn3.Open()
            cnn2.Close() : cnn2.Open()

            For field As Integer = 0 To 24
                If field = 24 Then Exit For
                hora = field

                cmd3 = cnn3.CreateCommand

                If cboHabitacion.Text = "" Then
                    cmd3.CommandText =
                   "select Hora,Id,Activo from Agenda where Hora=" & hora & " and Dia=" & Fechita(1) & " and Mes=" & Fechita(2) & " and Anio=" & Fechita(3) & " and Usuario='" & cboUsuario.Text & "'"
                Else
                    cmd3.CommandText =
                    "select Hora,Id,Activo from Agenda where Hora=" & hora & " and Dia=" & Fechita(1) & " and Mes=" & Fechita(2) & " and Anio=" & Fechita(3) & " and Usuario='" & cboUsuario.Text & "' AND Habitacion='" & cboHabitacion.Text & "'"
                End If

                rd3 = cmd3.ExecuteReader
                If rd3.HasRows Then
                    If rd3.Read Then
                        cmd2 = cnn2.CreateCommand
                        If cboHabitacion.Text = "" Then
                            cmd2.CommandText =
                           "select Asunto from Agenda where Hora=" & hora & " and Dia=" & Fechita(1) & " and Mes=" & Fechita(2) & " and Anio=" & Fechita(3) & " and Usuario='" & cboUsuario.Text & "'"
                        Else
                            cmd2.CommandText =
                            "select Asunto from Agenda where Hora=" & hora & " and Dia=" & Fechita(1) & " and Mes=" & Fechita(2) & " and Anio=" & Fechita(3) & " and Usuario='" & cboUsuario.Text & "' AND Habitacion='" & cboHabitacion.Text & "'"
                        End If

                        rd2 = cmd2.ExecuteReader
                        Do While rd2.Read
                            If rd2.HasRows Then
                                evento = rd2(0).ToString
                            End If
                        Loop
                        rd2.Close()

                        If CDec(rd3("Hora").ToString) < 10 Then
                            horx = "0" & rd3("Hora").ToString
                        Else
                            horx = rd3("Hora").ToString
                        End If

                        grdCaptura.Rows.Add(rd3("Id").ToString, horx & ":00", evento, rd3("Activo").ToString)
                    End If
                Else
                    If hora < 10 Then
                        horx = "0" & field
                    Else
                        horx = field
                    End If
                    grdCaptura.Rows.Add("0", horx & ":00", "", "DX")
                End If

                If grdCaptura.Rows(field).Cells(3).Value.ToString = "DX" Then
                Else
                    If grdCaptura.Rows(field).Cells(3).Value = False Then
                        grdCaptura.Rows(field).DefaultCellStyle.BackColor = Color.Blue
                        grdCaptura.Rows(field).DefaultCellStyle.ForeColor = Color.White
                    End If
                End If

                My.Application.DoEvents()
                rd3.Close()
            Next
            cnn2.Close()
            cnn3.Close()
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
            cnn3.Close()
            cnn2.Close()
        End Try

        btnAgregar.Visible = True
        btnModificar.Visible = True
        btnDetalle.Visible = True
    End Sub

    Private Sub optHora_Click(sender As Object, e As EventArgs) Handles optHora.Click
        grdCaptura.Rows.Clear()
        tipo = 1

        If cboUsuario.Text = "" And cboHabitacion.Text = "" Then
            MsgBox("Seleccione un usuario por consultar.", vbInformation + vbOKOnly, titulohotelriaa)
            cboUsuario.Focus.Equals(True)
            cboHabitacion.Focus.Equals(True)
            optHora.Checked = False
            Exit Sub
        End If

        Dim minuto As String = ""
        Dim hora As String = ""

        Try
            cnn1.Close() : cnn1.Open()

            For field As Integer = 0 To 60
                If field = 60 Then Exit For
                minuto = field

                cmd1 = cnn1.CreateCommand

                If cboHabitacion.Text = "" Then
                    cmd1.CommandText =
                    "select Hora,Minuto,Id,Asunto,Activo from Agenda where Minuto=" & minuto & " and Hora=" & Tiempo(1) & " and Dia=" & Fechita(1) & " and Mes=" & Fechita(2) & " and Anio=" & Fechita(3) & " and Usuario='" & cboUsuario.Text & "'"
                Else
                    cmd1.CommandText =
                    "select Hora,Minuto,Id,Asunto,Activo from Agenda where Minuto=" & minuto & " and Hora=" & Tiempo(1) & " and Dia=" & Fechita(1) & " and Mes=" & Fechita(2) & " and Anio=" & Fechita(3) & " and Usuario='" & cboUsuario.Text & "' AND Habitacion='" & cboHabitacion.Text & "'"
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

                        grdCaptura.Rows.Add(rd1("Id").ToString, hora & ":" & minuto, rd1("Asunto").ToString, rd1("Activo").ToString)
                    End If
                Else
                    If field < 10 Then
                        minuto = "0" & field
                    Else
                        minuto = field
                    End If

                    If CDec(Tiempo(1)) < 10 Then
                        hora = "0" & Tiempo(1)
                    Else
                        hora = Tiempo(1)
                    End If

                    grdCaptura.Rows.Add("0", hora & ":" & minuto, "", "DX")
                End If

                If grdCaptura.Rows(field).Cells(3).Value.ToString = "DX" Then
                Else
                    If grdCaptura.Rows(field).Cells(3).Value = False Then
                        grdCaptura.Rows(field).DefaultCellStyle.BackColor = Color.Blue
                        grdCaptura.Rows(field).DefaultCellStyle.ForeColor = Color.White
                    End If
                End If
                rd1.Close()
            Next
            cnn1.Close()
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
            cnn1.Close()
        End Try

        btnAgregar.Visible = True
        btnModificar.Visible = True
        btnDetalle.Visible = False

    End Sub

    Private Sub cboHabitacion_DropDown(sender As Object, e As EventArgs) Handles cboHabitacion.DropDown
        Try
            cboHabitacion.Items.Clear()

            cnn5.Close() : cnn5.Open()
            cmd5 = cnn5.CreateCommand
            cmd5.CommandText = "SELECT DISTINCT N_Habitacion FROM habitacion WHERE N_Habitacion<>'' ORDER BY N_Habitacion"
            rd5 = cmd5.ExecuteReader
            Do While rd5.Read
                If rd5.HasRows Then
                    cboHabitacion.Items.Add(rd5(0).ToString)
                End If
            Loop
            rd5.Close()
            cnn5.Close()

        Catch ex As Exception
            MessageBox.Show(ex.ToString)
            cnn5.Close()
        End Try
    End Sub

    Private Sub cboHabitacion_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cboHabitacion.KeyPress
        e.KeyChar = UCase(e.KeyChar)
        If AscW(e.KeyChar) = Keys.Enter Then
            cboUsuario.Focus.Equals(True)
        End If
    End Sub

    Private Sub cboUsuario_DropDown(sender As Object, e As EventArgs) Handles cboUsuario.DropDown
        Try
            cboUsuario.Items.Clear()

            cnn5.Close() : cnn5.Open()
            cmd5 = cnn5.CreateCommand
            cmd5.CommandText = "SELECT DISTINCT Alias FROM usuarios WHERE alias<>'' ORDER BY Alias"
            rd5 = cmd5.ExecuteReader
            Do While rd5.Read
                If rd5.HasRows Then
                    cboUsuario.Items.Add(rd5(0).ToString)
                End If
            Loop
            rd5.Close()
            cnn5.Close()

        Catch ex As Exception
            MessageBox.Show(ex.ToString)
            cnn5.Close()
        End Try
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Close()
        refer = ""
        cboHabitacion.Text = ""
        cboUsuario.Text = ""
    End Sub

    Private Sub btnConsultar_Click(sender As Object, e As EventArgs) Handles btnConsultar.Click
        frmConsultarCitaH.BringToFront()
        frmConsultarCitaH.Show()
    End Sub

    Private Sub btnDetalle_Click(sender As Object, e As EventArgs) Handles btnDetalle.Click
        If id_cita = 0 Then
            MsgBox("Seleccione un registro con información para poder modificarlo.", vbInformation + vbOKOnly, titulohotelriaa)
            Exit Sub
        End If

        If refer = "" Then
            MsgBox("Seleccione un registro con información para poder modificarlo.", vbInformation + vbOKOnly, titulohotelriaa)
            Exit Sub
        End If
        tActuales.Stop()
        If (optDia.Checked) Then
            frmDetalleCH.Text = "Detalle de la reservación del día " & lblDía.Text & " entre las " & Mid(refer, 1, 2) & ":00 y las " & Mid(refer, 1, 2) + 1 & ":00"
        End If
        If (optMes.Checked) Then
            frmDetalleCH.Text = "Detalle de la reservación del día " & refer & " de " & lblMes.Text
        End If
        frmDetalleCH.cbousu.Text = cboUsuario.Text
        frmDetalleCH.cboHabi.Text = cboHabitacion.Text
        frmDetalleCH.txtReferencia.Text = refer
        My.Application.DoEvents()
        frmDetalleCH.Show()
    End Sub

    Private Sub btnModificar_Click(sender As Object, e As EventArgs) Handles btnModificar.Click
        If id_cita = 0 Then
            MsgBox("Selecciona un registro con información para poder modificarlo.", vbInformation + vbOKOnly, titulohotelriaa)
            Exit Sub
        End If
        frmModificarCH.txtId.text = id_cita
        tActuales.Stop()
        My.Application.DoEvents()
        frmModificarCH.Show()
    End Sub

    Private Sub btnAgregar_Click(sender As Object, e As EventArgs) Handles btnAgregar.Click
        frmAddCitaH.BringToFront()
        frmAddCitaH.Show()
        tActuales.Stop()
    End Sub

    Private Sub tEstado_Tick(sender As Object, e As EventArgs) Handles tEstado.Tick
        EstadoH()
    End Sub

    Private Sub cboHabitacion_SelectedValueChanged(sender As Object, e As EventArgs) Handles cboHabitacion.SelectedValueChanged
        grdCaptura.Rows.Clear()
    End Sub

    Private Sub grdCaptura_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles grdCaptura.CellClick
        id_cita = grdCaptura.CurrentRow.Cells(0).Value.ToString
        refer = grdCaptura.CurrentRow.Cells(1).Value.ToString
    End Sub

    Private Sub frmCitasH_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        refer = ""
        cboHabitacion.Text = ""
        cboUsuario.Text = ""
    End Sub
End Class