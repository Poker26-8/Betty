﻿Public Class frmMenuHabitaciones
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        frmManejo.Show()
        Me.Close()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Close()
        'frmAsignarChofer.Show()
        'frmPedidosCli.Show()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        frmAdministracion.Show()
        Me.Close()
    End Sub

    Private Sub frmMenuHabitaciones_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click



        frmCitasH.BringToFront()
        frmCitasH.ShowDialog()
    End Sub

    Private Sub ReservacionesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ReservacionesToolStripMenuItem.Click
        frmRepReservaciones.BringToFront()
        frmRepReservaciones.Show()
    End Sub
End Class