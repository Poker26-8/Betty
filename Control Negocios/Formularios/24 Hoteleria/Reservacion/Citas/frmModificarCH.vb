Public Class frmModificarCH
    Private Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click
        frmCitasH.tActuales.Start()
        Me.Close()
    End Sub

    Private Sub frmModificarCH_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        rtAsunto.Focus.Equals(True)

        If txtID.Text = "" Then
            FrmAgenda.tActuales.Start()
            Me.Close()
        End If

        Dim fsalida As Date = Nothing
        Dim fs As String = ""
        Dim hsalida As String = ""

        Try
            cnn1.Close() : cnn1.Open()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "select Hora,Minuto,Dia,Mes,Anio,Usuario,Asunto,FSalida from Agenda where Id=" & txtID.Text
            rd1 = cmd1.ExecuteReader
            If rd1.HasRows Then
                If rd1.Read Then
                    cboHora.Text = rd1("Hora").ToString
                    cboMinuto.Text = rd1("Minuto").ToString
                    txtDia.Text = rd1("Dia").ToString
                    cboMes.Tag = rd1("Mes").ToString
                    cboTagMes()
                    My.Application.DoEvents()
                    cboAño.Text = rd1("Anio").ToString
                    cboUsuario.Text = rd1("Usuario").ToString
                    rtAsunto.Text = rd1("Asunto").ToString

                    fsalida = rd1("FSalida").ToString
                    fs = Format(fsalida, "dd-MM-yyyy")
                    hsalida = Format(fsalida, "HH:mm:ss")
                    dtpFechaSalida.Value = fs
                    dtpHoraSalida.Text = hsalida
                End If
            End If
            rd1.Close()
            cnn1.Close()
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
            cnn1.Close()
        End Try
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

    Public Sub cboTagMes()
        Select Case cboMes.Tag
            Case Is = 1
                cboMes.Text = "Enero"
            Case Is = 2
                cboMes.Text = "Febrero"
            Case Is = 3
                cboMes.Text = "Marzo"
            Case Is = 4
                cboMes.Text = "Abril"
            Case Is = 5
                cboMes.Text = "Mayo"
            Case Is = 6
                cboMes.Text = "Junio"
            Case Is = 7
                cboMes.Text = "Julio"
            Case Is = 8
                cboMes.Text = "Agosto"
            Case Is = 9
                cboMes.Text = "Septiembre"
            Case Is = 10
                cboMes.Text = "Octubre"
            Case Is = 11
                cboMes.Text = "Noviembre"
            Case Is = 12
                cboMes.Text = "Diciembre"
        End Select
    End Sub

    Private Sub cboUsuario_DropDown(sender As Object, e As EventArgs) Handles cboUsuario.DropDown
        Try
            cboUsuario.Items.Clear()
            cnn5.Close() : cnn5.Open()
            cmd5 = cnn5.CreateCommand
            cmd5.CommandText = "SELECT DISTINCT Alias FROM Usuarios WHERE Alias<>'' ORDER BY Alias"
            rd5 = cmd5.ExecuteReader
            If rd5.HasRows Then
                Do While rd5.Read
                    cboUsuario.Items.Add(rd5(0).ToString)
                Loop
            End If
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

    Private Sub cboMes_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cboMes.KeyPress
        If AscW(e.KeyChar) = Keys.Enter Then
            If cboMes.Text <> "Enero" Or cboMes.Text <> "Febrero" Or cboMes.Text <> "Marzo" Or cboMes.Text <> "Abril" Or cboMes.Text <> "Mayo" Or cboMes.Text <> "Junio" Or cboMes.Text <> "Julio" Or cboMes.Text <> "Agosto" Or cboMes.Text <> "Septiembre" Or cboMes.Text <> "Octubre" Or cboMes.Text <> "Noviembre" Or cboMes.Text <> "Diciembre" Then
                cboMesTag()
                cboAño.Focus.Equals(True)
            End If
        End If
    End Sub

    Private Sub cboAño_DropDown(sender As Object, e As EventArgs) Handles cboAño.DropDown
        cboAño.Items.Clear()
        cboAño.Items.Add(Now.Year)
        For A As Integer = 1 To 20
            cboAño.Items.Add(
                Now.Year + A
                )
        Next
    End Sub

    Private Sub cboAño_GotFocus(sender As Object, e As EventArgs) Handles cboAño.GotFocus
        cboAño.SelectionStart = 0
        cboAño.SelectionLength = 20
    End Sub

    Private Sub cboAño_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cboAño.KeyPress
        If AscW(e.KeyChar) = Keys.Enter Then
            cboHora.Focus.Equals(True)
        End If
    End Sub

    Private Sub cboHora_DropDown(sender As Object, e As EventArgs) Handles cboHora.DropDown
        cboHora.Items.Clear()
        For H As Integer = 0 To 23
            Dim hora As String = ""
            If H < 10 Then
                hora = "0" & H
            Else
                hora = H
            End If
            cboHora.Items.Add(
                hora
                )
        Next
    End Sub

    Private Sub cboHora_GotFocus(sender As Object, e As EventArgs) Handles cboHora.GotFocus
        cboHora.SelectionStart = 0
        cboHora.SelectionLength = 20
    End Sub

    Private Sub cboHora_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cboHora.KeyPress
        If AscW(e.KeyChar) = Keys.Enter Then
            cboMinuto.Focus.Equals(True)
        End If
    End Sub

    Private Sub cboHora_LostFocus(sender As Object, e As EventArgs) Handles cboHora.LostFocus
        If CDec(cboHora.Text) < 10 Then
            cboHora.Text = "0" & cboHora.Text
        End If
    End Sub

    Private Sub cboMinuto_DropDown(sender As Object, e As EventArgs) Handles cboMinuto.DropDown
        cboMinuto.Items.Clear()
        For M As Integer = 0 To 59
            Dim minuto As String = ""
            If M < 10 Then
                'minuto = "0" & M
                minuto = M
            Else
                minuto = M
            End If
            cboMinuto.Items.Add(
                minuto
                )
        Next
    End Sub

    Private Sub cboMinuto_GotFocus(sender As Object, e As EventArgs) Handles cboMinuto.GotFocus
        cboMinuto.SelectionStart = 0
        cboMinuto.SelectionLength = 20
    End Sub

    Private Sub cboMinuto_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cboMinuto.KeyPress
        If AscW(e.KeyChar) = Keys.Enter Then
            txtDia.Focus.Equals(True)
        End If
    End Sub

    Private Sub cboMinuto_LostFocus(sender As Object, e As EventArgs) Handles cboMinuto.LostFocus
        If CDec(cboMinuto.Text) < 10 Then
            cboMinuto.Text = "0" & cboMinuto.Text
        End If
    End Sub

    Private Sub txtDia_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtDia.KeyPress
        If AscW(e.KeyChar) = Keys.Enter Then
            cboUsuario.Focus.Equals(True)
        End If
    End Sub

    Private Sub cboUsuario_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cboUsuario.KeyPress
        If AscW(e.KeyChar) = Keys.Enter Then
            rtAsunto.Focus.Equals(True)
        End If
    End Sub

    Private Sub cboUsuario_GotFocus(sender As Object, e As EventArgs) Handles cboUsuario.GotFocus
        cboUsuario.SelectionStart = 0
        cboUsuario.SelectionLength = 20
    End Sub

    Private Sub rtAsunto_KeyPress(sender As Object, e As KeyPressEventArgs) Handles rtAsunto.KeyPress
        e.KeyChar = UCase(e.KeyChar)
        If AscW(e.KeyChar) = Keys.Enter Then
            btnGuardar.Focus.Equals(True)
        End If
    End Sub

    Private Sub btnEliminar_Click(sender As Object, e As EventArgs) Handles btnEliminar.Click
        If MsgBox("¿Desea eliminar éste evento de manera permanente?" + vbNewLine + "Esta acción no se puede deshacer", vbInformation + vbOKCancel, titulohotelriaa) = vbOK Then
            Try
                cnn1.Close() : cnn1.Open()
                cmd1 = cnn1.CreateCommand
                cmd1.CommandText = "DELETE FROM Agenda WHERE Id=" & txtID.Text
                cmd1.ExecuteNonQuery()
                cnn1.Close()

                MsgBox("Evento eliminado con éxito.", vbInformation + vbOKOnly, titulohotelriaa)

                Me.Hide()
                If (frmCitasH.optHora.Checked) Then
                    frmCitasH.ActuHora(frmCitasH.grdCaptura, frmCitasH.cboUsuario.Text, frmCitasH.cboHabitacion.Text)
                End If
                If (FrmAgenda.optDia.Checked) Then
                    frmCitasH.ActuDiaHab(frmCitasH.grdCaptura, frmCitasH.cboUsuario.Text, frmCitasH.cboHabitacion.Text)
                End If
                frmCitasH.tActuales.Start()
                My.Application.DoEvents()
                Me.Close()
            Catch ex As Exception
                MessageBox.Show(ex.ToString)
                cnn1.Close()
            End Try
        End If
    End Sub

    Private Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click
        'Corrobora que todos los campos estén llenos
        If cboHora.Text = "" Or cboMinuto.Text = "" Or txtDia.Text = "" Or cboMes.Text = "" Or cboAño.Text = "" Or cboUsuario.Text = "" Or rtAsunto.Text = "" Then
            MsgBox("Necesita llenar todos los datos para guardar el evento.", vbInformation + vbOKOnly, titulohotelriaa)
            Exit Sub
        End If

        'Corrobora que la fecha sea coherente
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

        Try
            cnn1.Close()
            cnn1.Open()

            cmd1 = cnn1.CreateCommand
            cmd1.CommandText =
                "update Agenda set Hora=" & cboHora.Text & ", Minuto=" & cboMinuto.Text & ", Dia=" & txtDia.Text & ", Mes=" & cboMes.Tag & ", Anio=" & cboAño.Text & ", Asunto='" & QuitaSaltos(rtAsunto.Text, "") & "', Usuario='" & cboUsuario.Text & "', Activo=1,FSalida='" & Format(dtpFechaSalida.Value, "yyyy-MM-dd") & " " & Format(dtpHoraSalida.Value, "HH:mm:ss") & "' where Id=" & txtID.Text
            cmd1.ExecuteNonQuery()
            cnn1.Close()

            MsgBox("Evento modificado con éxito.", vbInformation + vbOKOnly, titulohotelriaa)

            Me.Hide()
            If (frmCitasH.optMes.Checked) Then
                frmCitasH.ActuMes(frmCitasH.grdCaptura, frmCitasH.cboUsuario.Text, frmCitasH.cboHabitacion.Text)
            End If

            If (frmCitasH.optHora.Checked) Then
                frmCitasH.ActuHora(frmCitasH.grdCaptura, frmCitasH.cboUsuario.Text, frmCitasH.cboHabitacion.Text)
            End If
            If (frmCitasH.optDia.Checked) Then
                frmCitasH.ActuDiaHab(frmCitasH.grdCaptura, frmCitasH.cboUsuario.Text, frmCitasH.cboHabitacion.Text)
            End If
            frmCitasH.tActuales.Start()
            My.Application.DoEvents()
            Me.Close()
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
            cnn1.Close()
        End Try
    End Sub
End Class