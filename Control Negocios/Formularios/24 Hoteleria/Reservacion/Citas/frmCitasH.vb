Public Class frmCitasH
    Private Sub frmCitasH_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        tHora.Start()
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
End Class