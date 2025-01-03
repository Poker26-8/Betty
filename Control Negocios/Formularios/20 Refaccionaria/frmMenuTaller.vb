Public Class frmMenuTaller
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        frmVehiculoTa.Show()
        frmVehiculoTa.BringToFront()
        Me.Close()
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        frmTallerR.Show()
        Me.Close()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Close()
    End Sub

    Private Sub frmMenuTaller_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class