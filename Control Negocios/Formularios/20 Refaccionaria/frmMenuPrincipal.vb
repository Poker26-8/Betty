Public Class frmMenuPrincipal


    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        'frmProductoR.Show()
        frmAsignarRefa.Show()
        frmAsignarRefa.BringToFront()
        Me.Close()
    End Sub

    Private Sub btnconsultar_Click(sender As Object, e As EventArgs) Handles btnconsultar.Click
        ' frmConsultaR.Show()
        frmConsultarRefaccion.Show()
        frmConsultarRefaccion.BringToFront()
        Me.Close()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Me.Close()
    End Sub

    Private Sub frmMenuPrincipal_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        frmMarcas.Show()
        frmMarcas.BringToFront()

    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        frmModelos.Show()
        frmModelos.BringToFront()
    End Sub
End Class