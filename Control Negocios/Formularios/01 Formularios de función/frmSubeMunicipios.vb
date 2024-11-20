Imports System.Data.OleDb

Public Class frmSubeMunicipios
    Private Sub btnMunicipios_Click(sender As Object, e As EventArgs) Handles btnMunicipios.Click

        Dim cnn1 As OleDb.OleDbConnection = New OleDb.OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & My.Application.Info.DirectoryPath & "\BaseExportar\DL1.mdb;;Persist Security Info=True;Jet OLEDB:Database Password=jipl22")
        Dim cmd1 As OleDbCommand = New OleDbCommand
        Dim rd1 As OleDbDataReader
        Dim cuantos As Integer = 0

        Dim clavemun As String = ""
        Dim claveedo As String = ""
        Dim descripcion As String = ""

        Dim conteo As Integer = 0
        Try
            cnn1.Close() : cnn1.Open()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "select ClaveMun,ClaveEdo,Descripcion from portemunicipios"
            rd1 = cmd1.ExecuteReader
            If rd1.HasRows Then
                Do While rd1.Read
                    clavemun = rd1("ClaveMun").ToString
                    claveedo = rd1("ClaveEdo").ToString
                    descripcion = rd1("Descripcion").ToString


                    My.Application.DoEvents()

                    cnn2.Close()
                    cnn2.Open()
                    cmd2 = cnn2.CreateCommand
                    cmd2.CommandText = "Insert into portemunicipios(ClaveMun,ClaveEdo,Descripcion) values('" & clavemun & "','" & claveedo & "','" & descripcion & "')"
                    If cmd2.ExecuteNonQuery Then
                        cuantos = cuantos + 1
                        txtbarras.Text = cuantos
                        My.Application.DoEvents()
                    Else
                        MsgBox("Revisa la descripcion  " & descripcion & " hay un error", vbCritical + vbOKOnly)
                    End If


                Loop
                cnn2.Close()
                MsgBox("Se insertaron " & cuantos & " productos")
                rd1.Close()
                cnn1.Close()
                txtbarras.Text = ""
            End If
            My.Application.DoEvents()
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try

        My.Application.DoEvents()


    End Sub

    Private Sub btnColonias_Click(sender As Object, e As EventArgs) Handles btnColonias.Click
        Dim cnn1 As OleDb.OleDbConnection = New OleDb.OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & My.Application.Info.DirectoryPath & "\BaseExportar\DL1.mdb;;Persist Security Info=True;Jet OLEDB:Database Password=jipl22")
        Dim cmd1 As OleDbCommand = New OleDbCommand
        Dim rd1 As OleDbDataReader
        Dim cuantos As Integer = 0

        Dim clavecol As String = ""
        Dim cp As String = ""
        Dim descripcion As String = ""

        Dim conteo As Integer = 0
        Try
            cnn1.Close() : cnn1.Open()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "select ClaveColonia,CP,Nombre from portecolonia"
            rd1 = cmd1.ExecuteReader
            If rd1.HasRows Then
                Do While rd1.Read
                    clavecol = rd1("ClaveColonia").ToString
                    cp = rd1("CP").ToString
                    descripcion = rd1("Nombre").ToString


                    My.Application.DoEvents()

                    cnn2.Close()
                    cnn2.Open()
                    cmd2 = cnn2.CreateCommand
                    cmd2.CommandText = "Insert into portecolonia(ClaveColonia,CP,Nombre) values('" & clavecol & "','" & cp & "','" & descripcion & "')"
                    If cmd2.ExecuteNonQuery Then
                        cuantos = cuantos + 1
                        txtbarras.Text = cuantos
                        My.Application.DoEvents()
                    Else
                        MsgBox("Revisa la descripcion  " & descripcion & " hay un error", vbCritical + vbOKOnly)
                    End If


                Loop
                cnn2.Close()
                MsgBox("Se insertaron " & cuantos & " productos")
                rd1.Close()
                cnn1.Close()
                txtbarras.Text = ""
            End If
            My.Application.DoEvents()
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try

        My.Application.DoEvents()
    End Sub
End Class