Public Class frmAddCitaH
    Private Sub frmAddCitaH_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        rtAsunto.Focus.Equals(True)
        cboHora.Text = Format(Now, "HH")
        cboMinuto.Text = Format(Now, "mm")
        txtDia.Text = Format(Now, "dd")
        cboAño.Text = Format(Now, "yyyy")
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
        cbomesTag

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

    Private Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click
        Me.Close()
    End Sub

    Private Sub btnAGregar_Click(sender As Object, e As EventArgs) Handles btnAGregar.Click
        Dim observaciones As String = ""
        observaciones = rtAsunto.Text.TrimEnd(vbCrLf.ToCharArray)
    End Sub

    Private Sub cbocliente_DropDown(sender As Object, e As EventArgs) Handles cbocliente.DropDown
        Try
            cbocliente.Items.Clear()
            cnn5.Close() : cnn5.Open()
            cmd5 = cnn5.CreateCommand
            cmd5.CommandText = "SELECT DISTINCT Nombre FROM clientes WHERE Nombre<<'' ORDER BY Nombre"
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
            cmd5.CommandText = "SELECT DISTINCT N_Habitacion FROM habitacion WHERE H_Habitacion<>'' ORDER BY H_Habitacion"
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
End Class