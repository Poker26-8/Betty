Public Class frmPromo
    Private Sub ComboBox1_DropDown(sender As Object, e As EventArgs) Handles ComboBox1.DropDown
        If rbGrupo.Checked = True Then
            Try
                ComboBox1.Items.Clear()
                cnn1.Close()
                cnn1.Open()
                cmd1 = cnn1.CreateCommand
                cmd1.CommandText = "Select distinct Grupo from Productos where Grupo<>''"
                rd1 = cmd1.ExecuteReader
                Do While rd1.Read
                    ComboBox1.Items.Add(rd1(0).ToString)
                Loop
                rd1.Close()
                cnn1.Close()
            Catch ex As Exception
                MessageBox.Show(ex.ToString)
                cnn1.Close()
            End Try
        End If
    End Sub

    Private Sub ComboBox1_SelectedValueChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedValueChanged
        Try
            grdProductos.Rows.Clear()
            cnn1.Close()
            cnn1.Open()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "Select Codigo,CodBarra,Nombre from Productos where Grupo='" & ComboBox1.Text & "'"
            rd1 = cmd1.ExecuteReader
            Do While rd1.Read
                grdProductos.Rows.Add(rd1(0).ToString, rd1(1).ToString, rd1(2).ToString, False)
            Loop
            rd1.Close()
            cnn1.Close()
            txtCantidad.SelectAll()
            txtCantidad.Focus.Equals(True)
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
            cnn1.Close()
        End Try
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            If txtCantidad.Text = "0" Then
                MsgBox("La cantidad no puede ser 0, ingresa una cantidad mayor", vbExclamation + vbOKOnly, "Delsscom Control Negocios Pro")
                txtCantidad.SelectAll()
                txtCantidad.Focus.Equals(True)
            End If
            Dim maxfol As Integer = 0
            cnn1.Close()
            cnn1.Open()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "Insert into Promo(Nombre,Cantidad) values('" & ComboBox1.Text & "','" & txtCantidad.Text & "')"
            If cmd1.ExecuteNonQuery Then
                cmd1 = cnn1.CreateCommand
                cmd1.CommandText = "Select MAX(Id) from Promo"
                rd1 = cmd1.ExecuteReader
                If rd1.Read Then
                    maxfol = rd1(0).ToString
                End If
                rd1.Close()

                For xxx As Integer = 0 To grdProductos.Rows.Count - 1
                    If grdProductos.Rows(xxx).Cells(3).Value Then
                        cmd1 = cnn1.CreateCommand
                        cmd1.CommandText = "Insert into PromoDet(Folio,Codigo) values(" & maxfol & ",'" & grdProductos.Rows(xxx).Cells(0).Value.ToString & "')"
                        If cmd1.ExecuteNonQuery Then

                        End If
                    End If
                Next
                MsgBox("Promocion guaradada correctamente", vbInformation + vbOKOnly, "Delsscom Control Negocios Pro")
                txtCantidad.Text = "1"
                ComboBox1.Text = ""
                grdProductos.Rows.Clear()
            End If
            cnn1.Close()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub frmPromo_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        rbGrupo.Checked = True
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If ComboBox1.Text = "" Then
            MsgBox("Selecciiona un grupo de promociones para eliminar", vbInformation + vbOKOnly, "Delsscom Control Negocios Pro")
            Exit Sub
        End If
        Try
            Dim folxd As Integer = 0
            cnn1.Close()
            cnn1.Open()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "Select Id from Promo where Nombre='" & ComboBox1.Text & "'"
            rd1 = cmd1.ExecuteReader
            If rd1.Read Then
                folxd = rd1(0).ToString
                rd1.Close()

                cmd1 = cnn1.CreateCommand
                cmd1.CommandText = "Delete from PromoDet where Folio=" & folxd & ""
                If cmd1.ExecuteNonQuery Then
                    cmd1 = cnn1.CreateCommand
                    cmd1.CommandText = "Delete from Promo where Id=" & folxd & ""
                    If cmd1.ExecuteNonQuery Then
                        MsgBox("Grupo de promoción eliminado correctamente", vbInformation + vbOKOnly, "Delsscom Control Negocios Pro")
                        grdProductos.Rows.Clear()
                        txtCantidad.Text = "1"
                        ComboBox1.Text = ""
                    End If
                    cnn1.Close()
                End If
                cnn1.Close()
            Else
                MsgBox("No hay ningun grupo de promoción registrado con este nombre", vbInformation + vbOKOnly, "Delsscom Control Negocios Pro")
                cnn1.Close()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
            cnn1.Close()
        End Try
    End Sub
End Class