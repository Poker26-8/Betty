Public Class frmAddCitaH
    Private Sub frmAddCitaH_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        rtAsunto.Focus.Equals(True)

        cboHora.Text = Format(Now, "HH")
        cboHoraSA.Text = Format(Now, "HH")
        cboMinuto.Text = Format(Now, "mm")
        cboMinutoSa.Text = Format(Now, "mm")
        txtDia.Text = Format(Now, "dd")
        txtDiaSa.Text = Format(Now, "dd")
        cboAño.Text = Format(Now, "yyyy")
        cboAñoSa.Text = Format(Now, "yyyy")
        Dim val As Integer = Now.Month
        Dim mes As String = ""

        Select Case val
            Case Is = 1
                mes = "Enero"
            Case Is = 2
                mes = "Febrero"
            Case Is = 3
                mes = "Marzo"
            Case Is = 4
                mes = "Abril"
            Case Is = 5
                mes = "Mayo"
            Case Is = 6
                mes = "Junio"
            Case Is = 7
                mes = "Julio"
            Case Is = 8
                mes = "Agosto"
            Case Is = 9
                mes = "Septiembre"
            Case Is = 10
                mes = "Octubre"
            Case Is = 11
                mes = "Noviembre"
            Case Is = 12
                mes = "Diciembre"
        End Select
        cboMes.Text = mes
        cboMesTag()

    End Sub

    Public Sub cboMesTag()
        If cboMes.Text = "Enero" Then
            cboMes.Tag = 1
        ElseIf cboMes.Text = "Febrero" Then
            cboMes.Tag = 2
        ElseIf cboMes.Text = "Marzo" Then
            cboMes.Tag = 3
        ElseIf cboMes.Text = "Abril" Then
            cboMes.Tag = 4
        ElseIf cboMes.Text = "Mayo" Then
            cboMes.Tag = 5
        ElseIf cboMes.Text = "Junio" Then
            cboMes.Tag = 6
        ElseIf cboMes.Text = "Julio" Then
            cboMes.Tag = 7
        ElseIf cboMes.Text = "Agosto" Then
            cboMes.Tag = 8
        ElseIf cboMes.Text = "Septiembre" Then
            cboMes.Tag = 9
        ElseIf cboMes.Text = "Octubre" Then
            cboMes.Tag = 10
        ElseIf cboMes.Text = "Noviembre" Then
            cboMes.Tag = 11
        ElseIf cboMes.Text = "Diciembre" Then
            cboMes.Tag = 12
        End If
    End Sub

    Public Sub cboMessaTag()
        If cboMesSa.Text = "Enero" Then
            cboMesSa.Tag = 1
        ElseIf cboMessa.Text = "Febrero" Then
            cboMesSa.Tag = 2
        ElseIf cboMessa.Text = "Marzo" Then
            cboMesSa.Tag = 3
        ElseIf cboMessa.Text = "Abril" Then
            cboMesSa.Tag = 4
        ElseIf cboMessa.Text = "Mayo" Then
            cboMesSa.Tag = 5
        ElseIf cboMessa.Text = "Junio" Then
            cboMesSa.Tag = 6
        ElseIf cboMessa.Text = "Julio" Then
            cboMesSa.Tag = 7
        ElseIf cboMessa.Text = "Agosto" Then
            cboMesSa.Tag = 8
        ElseIf cboMessa.Text = "Septiembre" Then
            cboMesSa.Tag = 9
        ElseIf cboMessa.Text = "Octubre" Then
            cboMesSa.Tag = 10
        ElseIf cboMessa.Text = "Noviembre" Then
            cboMesSa.Tag = 11
        ElseIf cboMessa.Text = "Diciembre" Then
            cboMesSa.Tag = 12
        End If
    End Sub

    Private Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click
        frmCitasH.tActuales.Start()
        Me.Hide()
        My.Application.DoEvents()
        If (frmCitasH.optHora.Checked) Then
            frmCitasH.ActuHora(frmCitasH.grdCaptura, frmCitasH.cboUsuario.Text, frmCitasH.cboHabitacion.Text)
        End If

        If frmCitasH.optDia.Checked Then
            frmCitasH.ActuDiaHab(frmCitasH.grdCaptura, frmCitasH.cboUsuario.Text, frmCitasH.cboHabitacion.Text)
        End If
        My.Application.DoEvents()
        Me.Close()
    End Sub

    Private Sub btnAGregar_Click(sender As Object, e As EventArgs) Handles btnAGregar.Click

        If cboHora.Text = "" Or cboMinuto.Text = "" Or txtDia.Text = "" Or cboMes.Text = "" Or cboAño.Text = "" Or cboUsuario.Text = "" Or rtAsunto.Text = "" Or cboHabitacion.Text = "" Then
            MsgBox("Necesita llenar todos los datos para guardar el evento.", vbInformation + vbOKOnly, titulohotelriaa)
            Exit Sub
        End If

        If cboHoraSA.Text = "" Or cboMinutoSa.Text = "" Or txtDiaSa.Text = "" Or cboMesSa.Text = "" Or cboAñoSa.Text = "" Then
            MsgBox("Necesita llenar todos los datos para guardar el evento.", vbInformation + vbOKOnly, titulohotelriaa)
            Exit Sub
        End If

        'CorroboraR que la fecha sea coherente
        Dim añito As String = Format(Now, "yyyy")
        Dim añote As String = cboAño.Text
        If añote < añito Then
            MsgBox("No puedes guardar un evento en un año pasado.", vbInformation + vbOKOnly, titulohotelriaa)
            cboAño.Focus()
            Exit Sub
        End If

        Dim mesesito As Integer = Now.Month
        Dim mesesote As Integer = CInt(cboMes.Tag)
        If añote = añito Then
            If mesesote < mesesito Then
                MsgBox("No puedes guardar un evento en un mes pasado.", vbInformation + vbOKOnly, titulohotelriaa)
                cboMes.Focus()
                Exit Sub
            End If
        End If

        Dim fechita As String = Format(Now, "dd")
        Dim fechota As String = txtDia.Text
        If añote = añito Then
            If mesesote = mesesito Then
                If fechota < fechita Then
                    MsgBox("No puedes guardar un evento en un día pasado.", vbInformation + vbOKOnly, titulohotelriaa)
                    txtDia.Focus()
                    Exit Sub
                End If
            End If
        End If


        Dim tiempito As String = Format(Now, "HHmm")
        Dim tiempote As String = cboHora.Text & cboMinuto.Text
        If añote = añito Then
            If mesesote = mesesito Then
                If fechota = fechita Then
                    If tiempote < tiempito Then
                        MsgBox("No puedes guardar un evento en una hora pasada.", vbInformation + vbOKOnly, titulohotelriaa)
                        txtDia.Focus()
                        Exit Sub
                    End If
                End If
            End If
        End If
        Dim fechaentera As String = cboAño.Text & "/" & cboMes.Tag & "/" & txtDia.Text & " " & cboHora.Text & ":" & cboMinuto.Text
        Dim fechasalida As String = cboAñoSa.Text & "/" & cboMesSa.Tag & "/" & txtDiaSa.Text & " " & cboHoraSA.Text & ":" & cboMinutoSa.Text


        Try
            cnn1.Close() : cnn1.Open()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "SELECT * FROM agenda WHERE Minuto=" & cboMinuto.Text & " AND Hora=" & cboHora.Text & " AND Dia=" & txtDia.Text & " AND Mes=" & cboMes.Tag & " AND Anio=" & cboAño.Text & " AND Usuario='" & cboUsuario.Text & "' AND Habitacion='" & cboHabitacion.Text & "' AND Activo=1"
            rd1 = cmd1.ExecuteReader
            If rd1.HasRows Then
                If rd1.Read Then
                    MsgBox("Ya hay una reservación programada para el día " & txtDia.Text & "/" & cboMes.Tag & "/" & cboAño.Text & " a las " & cboHora.Text & ":" & cboMinuto.Text & ".", vbInformation + vbOKOnly, titulohotelriaa)
                    rd1.Close()
                    cnn1.Close()
                    Exit Sub
                End If
            Else
                cnn2.Close() : cnn2.Open()
                cmd2 = cnn2.CreateCommand
                cmd2.CommandText = "INSERT INTO agenda(Hora,Minuto,DIa,Mes,Anio,FEntrada,FSalida,Asunto,Usuario,Habitacion,Cliente,Activo) VALUES(" & cboHora.Text & "," & cboMinuto.Text & "," & txtDia.Text & "," & cboMes.Tag & "," & cboAño.Text & ",'" & fechaentera & "','" & fechasalida & "','" & QuitaSaltos(rtAsunto.Text, " ") & "','" & cboUsuario.Text & "','" & cboHabitacion.Text & "','" & cbocliente.Text & "',1)"
                cmd2.ExecuteNonQuery()

                cmd2 = cnn2.CreateCommand
                cmd2.CommandText = "INSERT INTO reservaciones(Cliente,Telefono,Habitacion,FEntrada,FSalida,Asigno,Reservo,Status,Tipo,Precio,Anticipo) VALUES('" & cbocliente.Text & "','" & txtTelefono.Text & "','" & cboHabitacion.Text & "','" & fechaentera & "','" & fechasalida & "','" & cboUsuario.Text & "','',0,'',0,0)"
                cmd2.ExecuteNonQuery()
                cnn2.Close()

                MsgBox("Evento registrado con éxito.", vbInformation + vbOKOnly, titulohotelriaa)

            End If
            rd1.Close()
            cnn1.Close()

            frmCitasH.tActuales.Start()
            Me.Hide()
            If (frmCitasH.optHora.Checked) Then
                frmCitasH.ActuHora(frmCitasH.grdCaptura, frmCitasH.cboUsuario.Text, frmCitasH.cboHabitacion.Text)
            End If

            If (frmCitasH.optDia.Checked) Then
                frmCitasH.ActuDiaHab(frmCitasH.grdCaptura, frmCitasH.cboUsuario.Text, frmCitasH.cboHabitacion.Text)
            End If

            If (frmCitasH.optMes.Checked) Then
                frmCitasH.ActuMes(frmCitasH.grdCaptura, frmCitasH.cboUsuario.Text, frmCitasH.cboHabitacion.Text)
            End If
            My.Application.DoEvents()
            Me.Close()
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
            cnn1.Close()
        End Try


    End Sub

    Private Sub cbocliente_DropDown(sender As Object, e As EventArgs) Handles cbocliente.DropDown
        Try
            cbocliente.Items.Clear()
            cnn5.Close() : cnn5.Open()
            cmd5 = cnn5.CreateCommand
            cmd5.CommandText = "SELECT DISTINCT Nombre FROM clientes WHERE Nombre<>'' ORDER BY Nombre"
            rd5 = cmd5.ExecuteReader
            Do While rd5.Read
                If rd5.HasRows Then
                    cbocliente.Items.Add(rd5(0).ToString)
                End If
            Loop
            rd5.Close()
            cnn5.Close()

        Catch ex As Exception
            MessageBox.Show(ex.ToString)
            cnn5.Close()
        End Try
    End Sub

    Private Sub cbocliente_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cbocliente.KeyPress
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
            cmd5.CommandText = "SELECT DISTINCT Alias FROM usuarios WHERE Alias<>'' ORDER BY Alias"
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

    Private Sub cboUsuario_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cboUsuario.KeyPress
        e.KeyChar = UCase(e.KeyChar)
        If AscW(e.KeyChar) = Keys.Enter Then
            cboHabitacion.Focus.Equals(True)
        End If
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

    Private Sub cboMes_DropDown(sender As Object, e As EventArgs) Handles cboMes.DropDown
        cboMes.Items.Clear()

        Dim mes As String = ""
        For val As Integer = 1 To 12
            Select Case val
                Case Is = 1
                    mes = "Enero"
                Case Is = 2
                    mes = "Febrero"
                Case Is = 3
                    mes = "Marzo"
                Case Is = 4
                    mes = "Abril"
                Case Is = 5
                    mes = "Mayo"
                Case Is = 6
                    mes = "Junio"
                Case Is = 7
                    mes = "Julio"
                Case Is = 8
                    mes = "Agosto"
                Case Is = 9
                    mes = "Septiembre"
                Case Is = 10
                    mes = "Octubre"
                Case Is = 11
                    mes = "Noviembre"
                Case Is = 12
                    mes = "Diciembre"
            End Select
            cboMes.Items.Add(mes)
        Next
    End Sub

    Private Sub cboAño_DropDown(sender As Object, e As EventArgs) Handles cboAño.DropDown
        cboAño.Items.Clear()
        cboAño.Items.Add(Now.Year)
        For a As Integer = 1 To 20
            cboAño.Items.Add(Now.Year + a)
        Next
    End Sub

    Private Sub cboMes_SelectedValueChanged(sender As Object, e As EventArgs) Handles cboMes.SelectedValueChanged
        cboMesTag()
    End Sub

    Private Sub cboHora_DropDown(sender As Object, e As EventArgs) Handles cboHora.DropDown
        cboHora.Items.Clear()
        For h As Integer = 0 To 23
            Dim hora As String = ""
            If h < 10 Then
                hora = "0" & h
            Else
                hora = h
            End If
            cboHora.Items.Add(hora)
        Next
    End Sub

    Private Sub cboMinuto_DropDown(sender As Object, e As EventArgs) Handles cboMinuto.DropDown
        cboMinuto.Items.Clear()
        For m As Integer = 0 To 59
            Dim minuto As String = ""
            If m < 10 Then
                minuto = "0" & m
            Else
                minuto = m
            End If
            cboMinuto.Items.Add(minuto)
        Next
    End Sub

    Private Sub rtAsunto_KeyPress(sender As Object, e As KeyPressEventArgs)
        e.KeyChar = UCase(e.KeyChar)
        If AscW(e.KeyChar) = Keys.Enter Then
            btnAGregar.Focus.Equals(True)
        End If
    End Sub

    Private Sub rtAsunto_KeyPress_1(sender As Object, e As KeyPressEventArgs) Handles rtAsunto.KeyPress
        e.KeyChar = UCase(e.KeyChar)
    End Sub

    Private Sub cboHabitacion_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cboHabitacion.KeyPress
        e.KeyChar = UCase(e.KeyChar)
        If AscW(e.KeyChar) = Keys.Enter Then
            rtAsunto.Focus.Equals(True)
        End If
    End Sub

    Private Sub cboMesSa_DropDown(sender As Object, e As EventArgs) Handles cboMesSa.DropDown
        cboMesSa.Items.Clear()

        Dim mes As String = ""
        For val As Integer = 1 To 12
            Select Case val
                Case Is = 1
                    mes = "Enero"
                Case Is = 2
                    mes = "Febrero"
                Case Is = 3
                    mes = "Marzo"
                Case Is = 4
                    mes = "Abril"
                Case Is = 5
                    mes = "Mayo"
                Case Is = 6
                    mes = "Junio"
                Case Is = 7
                    mes = "Julio"
                Case Is = 8
                    mes = "Agosto"
                Case Is = 9
                    mes = "Septiembre"
                Case Is = 10
                    mes = "Octubre"
                Case Is = 11
                    mes = "Noviembre"
                Case Is = 12
                    mes = "Diciembre"
            End Select
            cboMesSa.Items.Add(mes)
        Next
    End Sub

    Private Sub cboMesSa_SelectedValueChanged(sender As Object, e As EventArgs) Handles cboMesSa.SelectedValueChanged
        cboMessaTag()
    End Sub

    Private Sub cboAñoSa_DropDown(sender As Object, e As EventArgs) Handles cboAñoSa.DropDown
        cboAñoSa.Items.Clear()
        cboAñoSa.Items.Add(Now.Year)
        For a As Integer = 1 To 20
            cboAñoSa.Items.Add(Now.Year + a)
        Next
    End Sub

    Private Sub cboHoraSA_DropDown(sender As Object, e As EventArgs) Handles cboHoraSA.DropDown
        cboHoraSA.Items.Clear()
        For h As Integer = 0 To 23
            Dim hora As String = ""
            If h < 10 Then
                hora = "0" & h
            Else
                hora = h
            End If
            cboHoraSA.Items.Add(hora)
        Next
    End Sub

    Private Sub cboMinutoSa_DropDown(sender As Object, e As EventArgs) Handles cboMinutoSa.DropDown
        cboMinutoSa.Items.Clear()
        For m As Integer = 0 To 59
            Dim minuto As String = ""
            If m < 10 Then
                minuto = "0" & m
            Else
                minuto = m
            End If
            cboMinutoSa.Items.Add(minuto)
        Next
    End Sub

    Private Sub cbocliente_SelectedValueChanged(sender As Object, e As EventArgs) Handles cbocliente.SelectedValueChanged
        cnn1.Close() : cnn1.Open()
        cmd1 = cnn1.CreateCommand
        cmd1.CommandText = "SELECT Telefono FROM clientes WHERE Nombre='" & cbocliente.Text & "'"
        rd1 = cmd1.ExecuteReader
        If rd1.HasRows Then
            If rd1.Read Then
                txtTelefono.Text = rd1(0).ToString
            End If
        End If
        rd1.Close()
        cnn1.Close()
    End Sub
End Class