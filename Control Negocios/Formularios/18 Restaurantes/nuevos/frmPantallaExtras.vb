Imports MySql.Data.MySqlClient

Public Class frmPantallaExtras
    Dim totextras As Integer = 0
    Friend WithEvents btnExtra As System.Windows.Forms.Button
    Public CodigoProducto As String = 0
    Private Sub frmPantallaExtras_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Extras("0001")
    End Sub

    Public Sub Extras(ByVal producto As String)

        Dim cnn1 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
        Dim rd1 As MySqlDataReader
        Dim cmd1 As MySqlCommand

        Try
            Dim cuantosextras As UInteger = Math.Truncate(pExtras.Height / 90)
            Dim extras As Integer = 1

            cnn1.Close() : cnn1.Open()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "SELECT DISTINCT Descx,Codigo FROM extras WHERE CodigoAlpha='" & producto & "' order by Descx"
            rd1 = cmd1.ExecuteReader
            If rd1.HasRows Then
                Do While rd1.Read
                    If rd1.HasRows Then totextras = totextras + 1
                Loop

            Else
                rd1.Close()
                Me.Close()
            End If

            rd1.Close()

            If totextras <= 7 Then
                pExtras.AutoScroll = False
            Else
                pExtras.AutoScroll = True - 17
            End If

            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "SELECT DISTINCT Descx,Codigo FROM Extras WHERE CodigoAlpha='" & producto & "' order by Descx"
            rd1 = cmd1.ExecuteReader
            Do While rd1.Read
                If rd1.HasRows Then

                    Dim extra As String = rd1(0).ToString

                    btnExtra = New Button
                    btnExtra.Text = extra
                    btnExtra.Tag = rd1(1).ToString
                    btnExtra.Name = "btnExtra(" & extras & ")"
                    btnExtra.Width = 150 ' 90
                    btnExtra.Height = 110 ' 70

                    If extras > cuantosextras And extras < ((cuantosextras * 2) + 1) Then
                        btnExtra.Left = (btnExtra.Width * 1)
                        btnExtra.Top = (extras - (cuantosextras + 1)) * (btnExtra.Height + 0.5)
                        '2
                    ElseIf extras > (cuantosextras * 2) And extras < ((cuantosextras * 3) + 1) Then
                        btnExtra.Left = (btnExtra.Width * 2)
                        btnExtra.Top = (extras - ((cuantosextras * 2) + 1)) * (btnExtra.Height + 0.5)
                        '3
                    ElseIf extras > (cuantosextras * 3) And extras < ((cuantosextras * 4) + 1) Then
                        btnExtra.Left = (btnExtra.Width * 3)
                        btnExtra.Top = (extras - ((cuantosextras * 3) + 1)) * (btnExtra.Height + 0.5)
                        '4
                    ElseIf extras > (cuantosextras * 4) And extras < ((cuantosextras * 5) + 1) Then
                        btnExtra.Left = (btnExtra.Width * 4)
                        btnExtra.Top = (extras - ((cuantosextras * 4) + 1)) * (btnExtra.Height + 0.5)
                        '5
                    ElseIf extras > (cuantosextras * 5) And extras < ((cuantosextras * 6) + 1) Then
                        btnExtra.Left = (btnExtra.Width * 5)
                        btnExtra.Top = (extras - ((cuantosextras * 5) + 1)) * (btnExtra.Height + 0.5)
                    Else
                        btnExtra.Left = 0
                        btnExtra.Top = (extras - 1) * (btnExtra.Height + 0.5)
                    End If

                    btnExtra.BackColor = Color.Orange
                    btnExtra.FlatStyle = FlatStyle.Flat
                    btnExtra.FlatAppearance.BorderSize = 1
                    AddHandler btnExtra.Click, AddressOf btnExtra_Click
                    pExtras.Controls.Add(btnExtra)
                    extras += 1

                End If
            Loop
            rd1.Close()
            cnn1.Close()
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
            cnn1.Close()
        End Try
    End Sub

    Private Sub btnExtra_Click(sender As Object, e As EventArgs)

        Dim btnExt As Button = CType(sender, Button)
        CodigoProducto = btnExt.Tag

        frmVTouchR.ObtenerProducto(CodigoProducto)

    End Sub

    Private Sub btnSalir_Click(sender As Object, e As EventArgs) Handles btnSalir.Click
        Me.Close()
    End Sub
End Class