﻿
Imports System.Net
Imports System.IO
Imports Core.DAL.DE
Imports MySql.Data.MySqlClient


Public Class frmAgregarProducto

    Dim fecha As Date = Nothing
    Dim CantidadProductos As Integer = 0
    Dim CodigoProducto As String = ""

    Dim VarComensal As Integer = 0
    Public cantidad As Double = 0
    Dim cantidad2 As Double = 0
    Dim comensal As String = ""

    Dim banderaPromo As Integer = 0
    Dim banderacantidad As Integer = 0
    Dim banderacomentario As Integer = 0
    Dim banderaprecio As Integer = 0
    Dim banderaPROMOCION As Integer = 0

    Dim descripcionpro As String = ""

    Dim nompreferencia As String = ""
    Dim nombreExtras As String = ""
    Dim nombrepromocion As String = ""

    Dim TotDeptos As Integer = 0
    Dim TotGrupo As Integer = 0
    Dim TotProductos As Integer = 0
    Dim TotPrefe As Integer = 0
    Dim totExtras As Integer = 0
    Dim TotPromociones As Integer = 0

    Dim respuesta As String = ""

    Dim descripcion As String = ""
    Dim unidadventa As String = ""
    Dim minimo As Double = 0
    Dim ubicacion As String = ""
    Dim multiplo As Double = 0
    Dim doxuno As Integer = 0
    Dim tresxdos As Integer = 0
    Dim existencia As Double = 0
    Dim departamento As String = ""
    Dim GRUPO As String = ""
    Dim sipromo As Integer = 0

    Dim min As Double = 0
    Dim PU As Double = 0
    Dim Importe As Double = 0

    Dim damecodigo As String = ""

    Dim FolioVenta As String = ""

    Dim Prods() As String
    Dim impre As String = ""

    'promociones
    Public cantidadPromo As Double
    Public cantpromo As Double = 0
    Public codigop As String = 0

    Dim TestStr As String = ""

    Friend WithEvents btnDepto, btnGrupo, btnProd, btnPrefe, btnExtra, btnPromo As System.Windows.Forms.Button

    Public currentIndex As Integer = 0
    Public controlsPerClick As Integer = 12

    Public statusPromo As Integer = 0
    Private Sub Actualiza_Promos()
        Dim dia_hoy As Integer = Date.Now.DayOfWeek
        Dim dia_tex As String = ""

        If dia_hoy = 0 Then dia_tex = "Domingo"
        If dia_hoy = 1 Then dia_tex = "Lunes"
        If dia_hoy = 2 Then dia_tex = "Martes"
        If dia_hoy = 3 Then dia_tex = "Miercoles"
        If dia_hoy = 4 Then dia_tex = "Jueves"
        If dia_hoy = 5 Then dia_tex = "Viernes"
        If dia_hoy = 6 Then dia_tex = "Sabado"

        'COMENTARIO DE DÍAS'
        '0 -> Domingo       '4 -> Jueves
        '1 -> Lunes         '5 -> Viernes
        '2 -> Martes        '6 -> Sábado
        '3 -> Miércoles

        Dim cnn3 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
        Dim cnn4 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
        Dim rd3, rd4 As MySqlDataReader
        Dim cmd3, cmd4 As MySqlCommand

        Try
            cnn3.Close() : cnn3.Open()

            cmd3 = cnn3.CreateCommand
            cmd3.CommandText =
                "update Productos set E1=0, E2=0"
            cmd3.ExecuteNonQuery()

            cnn4.Close() : cnn4.Open()

            'Primero 2x1
            cmd3 = cnn3.CreateCommand
            cmd3.CommandText =
                "select Codigo from Promos where " & dia_tex & "=1"
            rd3 = cmd3.ExecuteReader
            Do While rd3.Read
                If rd3.HasRows Then
                    Dim codigo As String = rd3("Codigo").ToString()

                    cmd4 = cnn4.CreateCommand
                    cmd4.CommandText =
                        "update Productos set E1=1 where Codigo='" & codigo & "'"
                    cmd4.ExecuteNonQuery()
                End If
            Loop
            rd3.Close()

            'Primero 3x2
            cmd3 = cnn3.CreateCommand
            cmd3.CommandText =
                "select Codigo from Promos where " & dia_tex & "2=1"
            rd3 = cmd3.ExecuteReader
            Do While rd3.Read
                If rd3.HasRows Then
                    Dim codigo As String = rd3("Codigo").ToString()

                    cmd4 = cnn4.CreateCommand
                    cmd4.CommandText =
                        "update Productos set E2=1 where Codigo='" & codigo & "'"
                    cmd4.ExecuteNonQuery()
                End If
            Loop
            rd3.Close()
            cnn3.Close()

            cnn4.Close()
        Catch ex As Exception
            MessageBox.Show(ex.ToString())
            cnn3.Close()
        End Try
    End Sub
    Private Sub frmAgregarProducto_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Me.Text = "Agregar Productos" & Strings.Space(55) & "COMANDERO"

        Dim cnn1 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
        Dim rd1 As MySqlDataReader
        Dim cmd1 As MySqlCommand

        If File.Exists(My.Application.Info.DirectoryPath & "\ImagenesProductos\FondoComanda.jpg") Then
            pProductos.BackgroundImage = Image.FromFile(My.Application.Info.DirectoryPath & "\ImagenesProductos\FondoComanda.jpg")
            pProductos.BackgroundImageLayout = ImageLayout.Stretch
        End If

        TFolio.Start()
        TFecha.Start()

        Try
            cnn1.Close() : cnn1.Open()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "SELECT DISTINCT Departamento FROM Productos"
            rd1 = cmd1.ExecuteReader
            Do While rd1.Read
                If rd1.HasRows Then
                    TotDeptos = TotDeptos + 1
                End If
            Loop
            rd1.Close()
            cnn1.Close()

            pDepartamento.Controls.Clear()
            pgrupo.Controls.Clear()
            pProductos.Controls.Clear()
            pPreferencias.Controls.Clear()
            pExtras.Controls.Clear()
            pPromociones.Controls.Clear()
            Departamentos()

        Catch ex As Exception
            MessageBox.Show(ex.ToString)
            cnn1.Close()
        End Try

        Actualiza_Promos()
    End Sub


    Private Sub TFolio_Tick(sender As Object, e As EventArgs) Handles TFolio.Tick

        TFolio.Stop()

        Dim CFOLIO As Integer = 0
        Dim cnntimer2 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
        Dim rdtimer2 As MySqlDataReader
        Dim cmdtimer2 As MySqlCommand

        TFolio.Interval = 5000
        cnntimer2.Close() : cnntimer2.Open()
        cmdtimer2 = cnntimer2.CreateCommand
        cmdtimer2.CommandText = "SELECT Max(Id) FROM Comanda1"
        rdtimer2 = cmdtimer2.ExecuteReader
        If rdtimer2.HasRows Then
            If rdtimer2.Read Then

                lblFolio.Text = CDbl(IIf(rdtimer2(0).ToString = "", "0", rdtimer2(0).ToString)) + 1
            Else

                lblFolio.Text = "1"

            End If
        Else
            lblFolio.Text = "1"
        End If
        rdtimer2.Close()
        cnntimer2.Close()

        TFolio.Start()

    End Sub

    Public Sub Departamentos()

        Dim deptos As Integer = 0

        Dim cnn1 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
        Dim rd1 As MySqlDataReader
        Dim cmd1 As MySqlCommand

        Try

            cnn1.Close() : cnn1.Open()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "SELECT DISTINCT Departamento FROM Productos WHERE Departamento<>'INSUMO' AND Departamento<>'EXTRAS' AND Departamento<>'SERVICIOS' ORDER BY Departamento"
            rd1 = cmd1.ExecuteReader
            Do While rd1.Read
                If rd1.HasRows Then

                    Dim departamento As String = rd1("Departamento").ToString

                    btnDepto = New Button
                    btnDepto.Text = departamento
                    btnDepto.Name = "btnDepto(" & deptos & ")"
                    btnDepto.Left = 0
                    btnDepto.Height = 70
                    btnDepto.Width = 130

                    If TotDeptos <= 10 Then
                        btnDepto.Width = pDepartamento.Width
                    Else
                        btnDepto.Width = pDepartamento.Width - 17
                    End If

                    btnDepto.Top = (deptos) * (btnDepto.Height + 1)
                    btnDepto.BackColor = pDepartamento.BackColor
                    btnDepto.FlatStyle = FlatStyle.Popup
                    btnDepto.FlatAppearance.BorderSize = 0
                    AddHandler btnDepto.Click, AddressOf btnDepto_Click
                    pDepartamento.Controls.Add(btnDepto)
                    If deptos = 0 Then
                        Grupos(departamento)
                    End If
                    deptos += 1



                End If
            Loop
            rd1.Close()
            cnn1.Close()
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
            cnn1.Close()
        End Try

    End Sub

    Public Sub btnDepto_Click(sender As Object, e As EventArgs)


        Dim cnn2 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)

        Dim btnDepartamento As Button = CType(sender, Button)
        btnDepartamento.Font.Bold.Equals(True)

        pgrupo.Controls.Clear()
        pProductos.Controls.Clear()
        pPromociones.Controls.Clear()
        pExtras.Controls.Clear()
        pPromociones.Controls.Clear()

        If cnn2.State = 1 Then
            cnn2.Close()
        End If
        CantidadProductos = 0
        Grupos(btnDepartamento.Text)

    End Sub

    Public Sub Grupos(ByVal depto As String)

        Dim grupos As Integer = 0
        TotGrupo = 0

        Dim cnn2 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
        Dim rd2 As MySqlDataReader
        Dim cmd2 As MySqlCommand

        Try
            cnn2.Close() : cnn2.Open()
            cmd2 = cnn2.CreateCommand
            cmd2.CommandText = "SELECT distinct Grupo FROM Productos WHERE Departamento='" & depto & "' AND Grupo<>'PROMOCIONES' ORDER BY Grupo asc"
            rd2 = cmd2.ExecuteReader
            Do While rd2.Read
                If rd2.HasRows Then
                    TotGrupo = TotGrupo + 1
                End If
            Loop
            rd2.Close()

            'If TotGrupo <= 10 Then
            '    pgrupo.AutoScroll = False
            'Else
            '    pgrupo.AutoScroll = True
            'End If

            cmd2 = cnn2.CreateCommand
            cmd2.CommandText = "SELECT distinct Grupo FROM Productos WHERE Departamento='" & depto & "' AND Departamento<>'INGREDIENTES' AND Departamento<>'SERVICIOS' AND Grupo<>'EXTRAS' AND Grupo<>'PROMOCIONES' order by Grupo asc"
            rd2 = cmd2.ExecuteReader
            Do While rd2.Read
                If rd2.HasRows Then
                    Dim grupo As String = rd2(0).ToString
                    btnGrupo = New Button
                    btnGrupo.Text = grupo
                    btnGrupo.Tag = depto
                    btnGrupo.Name = "btnGrupo(" & grupos & ")"
                    btnGrupo.Left = 0

                    btnGrupo.Height = 70
                    btnGrupo.Width = 110

                    If TotGrupo <= 10 Then
                        btnGrupo.Width = pgrupo.Width
                    Else
                        btnGrupo.Width = pgrupo.Width - 17
                    End If
                    btnGrupo.Top = grupos * (btnGrupo.Height + 1)
                    btnGrupo.BackColor = pgrupo.BackColor
                    btnGrupo.FlatStyle = FlatStyle.Popup
                    btnGrupo.FlatAppearance.BorderSize = 0
                    AddHandler btnGrupo.Click, AddressOf btnGrupo_Click
                    pgrupo.Controls.Add(btnGrupo)
                    If grupos = 0 Then
                        Productos(depto, grupo)
                    End If
                    grupos += 1
                End If
            Loop
            rd2.Close()
            cnn2.Close()

        Catch ex As Exception
            MessageBox.Show(ex.ToString)
            cnn2.Close()
        End Try

    End Sub

    Private Sub btnGrupo_Click(sender As Object, e As EventArgs)


        Dim cnn3 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
        Dim btnGrupos As Button = CType(sender, Button)
        pProductos.Controls.Clear()
        pPreferencias.Controls.Clear()
        pExtras.Controls.Clear()
        pPromociones.Controls.Clear()

        If cnn3.State = 1 Then
            cnn3.Close()
        End If
        CantidadProductos = 0
        Productos(btnGrupo.Tag, btnGrupos.Text)

    End Sub



    Public Sub Productos(ByVal depto As String, ByVal grupo As String)
        Dim prods As Integer = 1
        Dim cuantos As Integer = Math.Truncate(pProductos.Height / 70)

        Dim cnn3 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
        Dim rd3 As MySqlDataReader
        Dim cmd3 As MySqlCommand

        Try

            cnn3.Close() : cnn3.Open()
            cmd3 = cnn3.CreateCommand
            cmd3.CommandText = "SELECT DISTINCT Nombre,Codigo FROM Productos WHERE Departamento='" & depto & "' AND Grupo='" & grupo & "' order by Nombre,Codigo"
            rd3 = cmd3.ExecuteReader
            Do While rd3.Read
                If rd3.HasRows Then
                    TotProductos = TotProductos + 1
                End If
            Loop
            rd3.Close()

            If TotProductos <= 10 Then
                pProductos.AutoScroll = False
            Else
                pProductos.AutoScroll = True
            End If

            cmd3 = cnn3.CreateCommand
            cmd3.CommandText = "SELECT DISTINCT Nombre,Codigo FROM Productos WHERE Departamento='" & depto & "' AND Grupo='" & grupo & "' order by Nombre,Codigo"
            rd3 = cmd3.ExecuteReader
            Do While rd3.Read
                If rd3.HasRows Then
                    Dim producto As String = rd3(0).ToString
                    btnProd = New Button
                    btnProd.Text = producto
                    btnProd.Tag = rd3(1).ToString
                    btnProd.Name = "btnProducto(" & prods & ")"
                    btnProd.Height = 70
                    btnProd.Width = 130

                    If prods > cuantos And prods < ((cuantos * 2) + 1) Then
                        btnProd.Left = (btnProd.Width * 1)
                        btnProd.Top = (prods - (cuantos + 1)) * (btnProd.Height + 0.5)
                        '2
                    ElseIf prods > (cuantos * 2) And prods < ((cuantos * 3) + 1) Then
                        btnProd.Left = (btnProd.Width * 2)
                        btnProd.Top = (prods - ((cuantos * 2) + 1)) * (btnProd.Height + 0.5)
                        '3
                    ElseIf prods > (cuantos * 3) And prods < ((cuantos * 4) + 1) Then
                        btnProd.Left = (btnProd.Width * 3)
                        btnProd.Top = (prods - ((cuantos * 3) + 1)) * (btnProd.Height + 0.5)
                        '4
                    ElseIf prods > (cuantos * 4) And prods < ((cuantos * 5) + 1) Then
                        btnProd.Left = (btnProd.Width * 4)
                        btnProd.Top = (prods - ((cuantos * 4) + 1)) * (btnProd.Height + 0.5)
                        '5
                    ElseIf prods > (cuantos * 5) And prods < ((cuantos * 6) + 1) Then
                        btnProd.Left = (btnProd.Width * 5)
                        btnProd.Top = (prods - ((cuantos * 5) + 1)) * (btnProd.Height + 0.5)
                        '6
                    ElseIf prods > (cuantos * 6) And prods < ((cuantos * 7) + 1) Then
                        btnProd.Left = (btnProd.Width * 6)
                        btnProd.Top = (prods - ((cuantos * 6) + 1)) * (btnProd.Height + 0.5)
                        '7
                    ElseIf prods > (cuantos * 7) And prods < ((cuantos * 8) + 1) Then
                        btnProd.Left = (btnProd.Width * 7)
                        btnProd.Top = (prods - ((cuantos * 7) + 1)) * (btnProd.Height + 0.5)
                        '8
                    ElseIf prods > (cuantos * 8) And prods < ((cuantos * 9) + 1) Then
                        btnProd.Left = (btnProd.Width * 8)
                        btnProd.Top = (prods - ((cuantos * 8) + 1)) * (btnProd.Height + 0.5)
                        '9
                    ElseIf prods > (cuantos * 9) And prods < ((cuantos * 10) + 1) Then
                        btnProd.Left = (btnProd.Width * 9)
                        btnProd.Top = (prods - ((cuantos * 9) + 1)) * (btnProd.Height + 0.5)
                        '10
                    ElseIf prods > (cuantos * 10) And prods < ((cuantos * 11) + 1) Then
                        btnProd.Left = (btnProd.Width * 10)
                        btnProd.Top = (prods - ((cuantos * 10) + 1)) * (btnProd.Height + 0.5)
                        '11
                    ElseIf prods > (cuantos * 11) And prods < ((cuantos * 12) + 1) Then
                        btnProd.Left = (btnProd.Width * 11)
                        btnProd.Top = (prods - ((cuantos * 11) + 1)) * (btnProd.Height + 0.5)
                        '12
                    ElseIf prods > (cuantos * 12) And prods < ((cuantos * 13) + 1) Then
                        btnProd.Left = (btnProd.Width * 12)
                        btnProd.Top = (prods - ((cuantos * 12) + 1)) * (btnProd.Height + 0.5)
                        '13
                    ElseIf prods > (cuantos * 13) And prods < ((cuantos * 14) + 1) Then
                        btnProd.Left = (btnProd.Width * 13)
                        btnProd.Top = (prods - ((cuantos * 13) + 1)) * (btnProd.Height + 0.5)
                        '14
                    ElseIf prods > (cuantos * 14) And prods < ((cuantos * 15) + 1) Then
                        btnProd.Left = (btnProd.Width * 14)
                        btnProd.Top = (prods - ((cuantos * 14) + 1)) * (btnProd.Height + 0.5)
                        '15
                    ElseIf prods > (cuantos * 15) And prods < ((cuantos * 16) + 1) Then
                        btnProd.Left = (btnProd.Width * 15)
                        btnProd.Top = (prods - ((cuantos * 15) + 1)) * (btnProd.Height + 0.5)
                        '16
                    ElseIf prods > (cuantos * 16) And prods < ((cuantos * 17) + 1) Then
                        btnProd.Left = (btnProd.Width * 16)
                        btnProd.Top = (prods - ((cuantos * 16) + 1)) * (btnProd.Height + 0.5)
                        '17
                    ElseIf prods > (cuantos * 17) And prods < ((cuantos * 18) + 1) Then
                        btnProd.Left = (btnProd.Width * 17)
                        btnProd.Top = (prods - ((cuantos * 17) + 1)) * (btnProd.Height + 0.5)
                        '18
                    ElseIf prods > (cuantos * 18) And prods < ((cuantos * 19) + 1) Then
                        btnProd.Left = (btnProd.Width * 18)
                        btnProd.Top = (prods - ((cuantos * 18) + 1)) * (btnProd.Height + 0.5)
                        '19
                    ElseIf prods > (cuantos * 19) And prods < ((cuantos * 20) + 1) Then
                        btnProd.Left = (btnProd.Width * 19)
                        btnProd.Top = (prods - ((cuantos * 19) + 1)) * (btnProd.Height + 0.5)
                        '20
                    ElseIf prods > (cuantos * 20) And prods < ((cuantos * 21) + 1) Then
                        btnProd.Left = (btnProd.Width * 20)
                        btnProd.Top = (prods - ((cuantos * 20) + 1)) * (btnProd.Height + 0.5)
                        '21
                    ElseIf prods > (cuantos * 21) And prods < ((cuantos * 22) + 1) Then
                        btnProd.Left = (btnProd.Width * 21)
                        btnProd.Top = (prods - ((cuantos * 21) + 1)) * (btnProd.Height + 0.5)
                        '22
                    ElseIf prods > (cuantos * 22) And prods < ((cuantos * 23) + 1) Then
                        btnProd.Left = (btnProd.Width * 22)
                        btnProd.Top = (prods - ((cuantos * 22) + 1)) * (btnProd.Height + 0.5)
                        '23
                    ElseIf prods > (cuantos * 23) And prods < ((cuantos * 24) + 1) Then
                        btnProd.Left = (btnProd.Width * 23)
                        btnProd.Top = (prods - ((cuantos * 23) + 1)) * (btnProd.Height + 0.5)
                        '24
                    ElseIf prods > (cuantos * 24) And prods < ((cuantos * 25) + 1) Then
                        btnProd.Left = (btnProd.Width * 24)
                        btnProd.Top = (prods - ((cuantos * 24) + 1)) * (btnProd.Height + 0.5)
                        '25
                    ElseIf prods > (cuantos * 25) And prods < ((cuantos * 26) + 1) Then
                        btnProd.Left = (btnProd.Width * 25)
                        btnProd.Top = (prods - ((cuantos * 25) + 1)) * (btnProd.Height + 0.5)
                        '26
                    ElseIf prods > (cuantos * 26) And prods < ((cuantos * 27) + 1) Then
                        btnProd.Left = (btnProd.Width * 26)
                        btnProd.Top = (prods - ((cuantos * 26) + 1)) * (btnProd.Height + 0.5)
                        '27
                    ElseIf prods > (cuantos * 27) And prods < ((cuantos * 28) + 1) Then
                        btnProd.Left = (btnProd.Width * 27)
                        btnProd.Top = (prods - ((cuantos * 27) + 1)) * (btnProd.Height + 0.5)
                        '28
                    ElseIf prods > (cuantos * 28) And prods < ((cuantos * 29) + 1) Then
                        btnProd.Left = (btnProd.Width * 28)
                        btnProd.Top = (prods - ((cuantos * 28) + 1)) * (btnProd.Height + 0.5)
                        '29
                    ElseIf prods > (cuantos * 29) And prods < ((cuantos * 30) + 1) Then
                        btnProd.Left = (btnProd.Width * 29)
                        btnProd.Top = (prods - ((cuantos * 29) + 1)) * (btnProd.Height + 0.5)
                        '30
                    ElseIf prods > (cuantos * 30) And prods < ((cuantos * 31) + 1) Then
                        btnProd.Left = (btnProd.Width * 30)
                        btnProd.Top = (prods - ((cuantos * 30) + 1)) * (btnProd.Height + 0.5)
                        '31
                    ElseIf prods > (cuantos * 31) And prods < ((cuantos * 32) + 1) Then
                        btnProd.Left = (btnProd.Width * 31)
                        btnProd.Top = (prods - ((cuantos * 31) + 1)) * (btnProd.Height + 0.5)
                        '32
                    ElseIf prods > (cuantos * 32) And prods < ((cuantos * 33) + 1) Then
                        btnProd.Left = (btnProd.Width * 32)
                        btnProd.Top = (prods - ((cuantos * 32) + 1)) * (btnProd.Height + 0.5)
                        '33
                    ElseIf prods > (cuantos * 33) And prods < ((cuantos * 34) + 1) Then
                        btnProd.Left = (btnProd.Width * 33)
                        btnProd.Top = (prods - ((cuantos * 33) + 1)) * (btnProd.Height + 0.5)
                        '34
                    ElseIf prods > (cuantos * 34) And prods < ((cuantos * 35) + 1) Then
                        btnProd.Left = (btnProd.Width * 34)
                        btnProd.Top = (prods - ((cuantos * 34) + 1)) * (btnProd.Height + 0.5)
                        '35
                    ElseIf prods > (cuantos * 35) And prods < ((cuantos * 36) + 1) Then
                        btnProd.Left = (btnProd.Width * 35)
                        btnProd.Top = (prods - ((cuantos * 35) + 1)) * (btnProd.Height + 0.5)
                        '36
                    ElseIf prods > (cuantos * 36) And prods < ((cuantos * 37) + 1) Then
                        btnProd.Left = (btnProd.Width * 36)
                        btnProd.Top = (prods - ((cuantos * 36) + 1)) * (btnProd.Height + 0.5)
                        '37
                    ElseIf prods > (cuantos * 37) And prods < ((cuantos * 38) + 1) Then
                        btnProd.Left = (btnProd.Width * 37)
                        btnProd.Top = (prods - ((cuantos * 37) + 1)) * (btnProd.Height + 0.5)
                        '38
                    ElseIf prods > (cuantos * 38) And prods < ((cuantos * 39) + 1) Then
                        btnProd.Left = (btnProd.Width * 38)
                        btnProd.Top = (prods - ((cuantos * 38) + 1)) * (btnProd.Height + 0.5)
                        '39
                    ElseIf prods > (cuantos * 39) And prods < ((cuantos * 40) + 1) Then
                        btnProd.Left = (btnProd.Width * 39)
                        btnProd.Top = (prods - ((cuantos * 39) + 1)) * (btnProd.Height + 0.5)
                        '40
                    ElseIf prods > (cuantos * 40) And prods < ((cuantos * 41) + 1) Then
                        btnProd.Left = (btnProd.Width * 40)
                        btnProd.Top = (prods - ((cuantos * 40) + 1)) * (btnProd.Height + 0.5)
                        '41
                    ElseIf prods > (cuantos * 41) And prods < ((cuantos * 42) + 1) Then
                        btnProd.Left = (btnProd.Width * 41)
                        btnProd.Top = (prods - ((cuantos * 41) + 1)) * (btnProd.Height + 0.5)
                        '42
                    ElseIf prods > (cuantos * 42) And prods < ((cuantos * 43) + 1) Then
                        btnProd.Left = (btnProd.Width * 42)
                        btnProd.Top = (prods - ((cuantos * 42) + 1)) * (btnProd.Height + 0.5)
                        '43
                    ElseIf prods > (cuantos * 43) And prods < ((cuantos * 44) + 1) Then
                        btnProd.Left = (btnProd.Width * 43)
                        btnProd.Top = (prods - ((cuantos * 43) + 1)) * (btnProd.Height + 0.5)
                        '44
                    ElseIf prods > (cuantos * 44) And prods < ((cuantos * 45) + 1) Then
                        btnProd.Left = (btnProd.Width * 44)
                        btnProd.Top = (prods - ((cuantos * 44) + 1)) * (btnProd.Height + 0.5)
                        '45
                    ElseIf prods > (cuantos * 45) And prods < ((cuantos * 46) + 1) Then
                        btnProd.Left = (btnProd.Width * 45)
                        btnProd.Top = (prods - ((cuantos * 45) + 1)) * (btnProd.Height + 0.5)
                        '46
                    ElseIf prods > (cuantos * 46) And prods < ((cuantos * 47) + 1) Then
                        btnProd.Left = (btnProd.Width * 46)
                        btnProd.Top = (prods - ((cuantos * 46) + 1)) * (btnProd.Height + 0.5)
                        '47
                    ElseIf prods > (cuantos * 47) And prods < ((cuantos * 48) + 1) Then
                        btnProd.Left = (btnProd.Width * 47)
                        btnProd.Top = (prods - ((cuantos * 47) + 1)) * (btnProd.Height + 0.5)
                        '48
                    ElseIf prods > (cuantos * 48) And prods < ((cuantos * 49) + 1) Then
                        btnProd.Left = (btnProd.Width * 48)
                        btnProd.Top = (prods - ((cuantos * 48) + 1)) * (btnProd.Height + 0.5)
                        '49
                    ElseIf prods > (cuantos * 49) And prods < ((cuantos * 50) + 1) Then
                        btnProd.Left = (btnProd.Width * 49)
                        btnProd.Top = (prods - ((cuantos * 49) + 1)) * (btnProd.Height + 0.5)
                        '50
                    ElseIf prods > (cuantos * 50) And prods < ((cuantos * 51) + 1) Then
                        btnProd.Left = (btnProd.Width * 50)
                        btnProd.Top = (prods - ((cuantos * 50) + 1)) * (btnProd.Height + 0.5)
                        '51
                    ElseIf prods > (cuantos * 51) And prods < ((cuantos * 52) + 1) Then
                        btnProd.Left = (btnProd.Width * 51)
                        btnProd.Top = (prods - ((cuantos * 51) + 1)) * (btnProd.Height + 0.5)
                        '52
                    ElseIf prods > (cuantos * 52) And prods < ((cuantos * 53) + 1) Then
                        btnProd.Left = (btnProd.Width * 52)
                        btnProd.Top = (prods - ((cuantos * 52) + 1)) * (btnProd.Height + 0.5)
                        '53
                    ElseIf prods > (cuantos * 53) And prods < ((cuantos * 54) + 1) Then
                        btnProd.Left = (btnProd.Width * 53)
                        btnProd.Top = (prods - ((cuantos * 53) + 1)) * (btnProd.Height + 0.5)
                        '54
                    ElseIf prods > (cuantos * 54) And prods < ((cuantos * 55) + 1) Then
                        btnProd.Left = (btnProd.Width * 54)
                        btnProd.Top = (prods - ((cuantos * 54) + 1)) * (btnProd.Height + 0.5)
                        '55
                    ElseIf prods > (cuantos * 55) And prods < ((cuantos * 56) + 1) Then
                        btnProd.Left = (btnProd.Width * 55)
                        btnProd.Top = (prods - ((cuantos * 55) + 1)) * (btnProd.Height + 0.5)
                        '56
                    ElseIf prods > (cuantos * 56) And prods < ((cuantos * 57) + 1) Then
                        btnProd.Left = (btnProd.Width * 56)
                        btnProd.Top = (prods - ((cuantos * 56) + 1)) * (btnProd.Height + 0.5)
                        '57
                    ElseIf prods > (cuantos * 57) And prods < ((cuantos * 58) + 1) Then
                        btnProd.Left = (btnProd.Width * 57)
                        btnProd.Top = (prods - ((cuantos * 57) + 1)) * (btnProd.Height + 0.5)
                        '58
                    ElseIf prods > (cuantos * 58) And prods < ((cuantos * 59) + 1) Then
                        btnProd.Left = (btnProd.Width * 58)
                        btnProd.Top = (prods - ((cuantos * 58) + 1)) * (btnProd.Height + 0.5)
                        '59
                    ElseIf prods > (cuantos * 59) And prods < ((cuantos * 60) + 1) Then
                        btnProd.Left = (btnProd.Width * 59)
                        btnProd.Top = (prods - ((cuantos * 59) + 1)) * (btnProd.Height + 0.5)
                        '60
                    ElseIf prods > (cuantos * 60) And prods < ((cuantos * 61) + 1) Then
                        btnProd.Left = (btnProd.Width * 60)
                        btnProd.Top = (prods - ((cuantos * 60) + 1)) * (btnProd.Height + 0.5)
                        '61
                    ElseIf prods > (cuantos * 61) And prods < ((cuantos * 62) + 1) Then
                        btnProd.Left = (btnProd.Width * 61)
                        btnProd.Top = (prods - ((cuantos * 61) + 1)) * (btnProd.Height + 0.5)
                        '62
                    ElseIf prods > (cuantos * 62) And prods < ((cuantos * 63) + 1) Then
                        btnProd.Left = (btnProd.Width * 62)
                        btnProd.Top = (prods - ((cuantos * 62) + 1)) * (btnProd.Height + 0.5)
                        '63
                    ElseIf prods > (cuantos * 63) And prods < ((cuantos * 64) + 1) Then
                        btnProd.Left = (btnProd.Width * 63)
                        btnProd.Top = (prods - ((cuantos * 63) + 1)) * (btnProd.Height + 0.5)
                        '64
                    ElseIf prods > (cuantos * 64) And prods < ((cuantos * 65) + 1) Then
                        btnProd.Left = (btnProd.Width * 64)
                        btnProd.Top = (prods - ((cuantos * 64) + 1)) * (btnProd.Height + 0.5)
                        '65
                    ElseIf prods > (cuantos * 65) And prods < ((cuantos * 66) + 1) Then
                        btnProd.Left = (btnProd.Width * 65)
                        btnProd.Top = (prods - ((cuantos * 65) + 1)) * (btnProd.Height + 0.5)
                        '66
                    ElseIf prods > (cuantos * 66) And prods < ((cuantos * 67) + 1) Then
                        btnProd.Left = (btnProd.Width * 66)
                        btnProd.Top = (prods - ((cuantos * 66) + 1)) * (btnProd.Height + 0.5)
                        '67
                    ElseIf prods > (cuantos * 67) And prods < ((cuantos * 68) + 1) Then
                        btnProd.Left = (btnProd.Width * 67)
                        btnProd.Top = (prods - ((cuantos * 67) + 1)) * (btnProd.Height + 0.5)
                        '68
                    ElseIf prods > (cuantos * 68) And prods < ((cuantos * 69) + 1) Then
                        btnProd.Left = (btnProd.Width * 68)
                        btnProd.Top = (prods - ((cuantos * 68) + 1)) * (btnProd.Height + 0.5)
                        '69
                    ElseIf prods > (cuantos * 69) And prods < ((cuantos * 70) + 1) Then
                        btnProd.Left = (btnProd.Width * 69)
                        btnProd.Top = (prods - ((cuantos * 69) + 1)) * (btnProd.Height + 0.5)
                        '70
                    ElseIf prods > (cuantos * 70) And prods < ((cuantos * 71) + 1) Then
                        btnProd.Left = (btnProd.Width * 70)
                        btnProd.Top = (prods - ((cuantos * 70) + 1)) * (btnProd.Height + 0.5)
                        '71
                    ElseIf prods > (cuantos * 71) And prods < ((cuantos * 72) + 1) Then
                        btnProd.Left = (btnProd.Width * 71)
                        btnProd.Top = (prods - ((cuantos * 71) + 1)) * (btnProd.Height + 0.5)
                        '72
                    ElseIf prods > (cuantos * 72) And prods < ((cuantos * 73) + 1) Then
                        btnProd.Left = (btnProd.Width * 72)
                        btnProd.Top = (prods - ((cuantos * 72) + 1)) * (btnProd.Height + 0.5)
                        '73
                    ElseIf prods > (cuantos * 73) And prods < ((cuantos * 74) + 1) Then
                        btnProd.Left = (btnProd.Width * 73)
                        btnProd.Top = (prods - ((cuantos * 73) + 1)) * (btnProd.Height + 0.5)
                        '74
                    ElseIf prods > (cuantos * 74) And prods < ((cuantos * 75) + 1) Then
                        btnProd.Left = (btnProd.Width * 74)
                        btnProd.Top = (prods - ((cuantos * 74) + 1)) * (btnProd.Height + 0.5)
                        '75
                    ElseIf prods > (cuantos * 75) And prods < ((cuantos * 76) + 1) Then
                        btnProd.Left = (btnProd.Width * 75)
                        btnProd.Top = (prods - ((cuantos * 75) + 1)) * (btnProd.Height + 0.5)
                        '76
                    ElseIf prods > (cuantos * 76) And prods < ((cuantos * 77) + 1) Then
                        btnProd.Left = (btnProd.Width * 76)
                        btnProd.Top = (prods - ((cuantos * 76) + 1)) * (btnProd.Height + 0.5)
                        '77
                    ElseIf prods > (cuantos * 77) And prods < ((cuantos * 78) + 1) Then
                        btnProd.Left = (btnProd.Width * 77)
                        btnProd.Top = (prods - ((cuantos * 77) + 1)) * (btnProd.Height + 0.5)
                        '78
                    ElseIf prods > (cuantos * 78) And prods < ((cuantos * 79) + 1) Then
                        btnProd.Left = (btnProd.Width * 78)
                        btnProd.Top = (prods - ((cuantos * 78) + 1)) * (btnProd.Height + 0.5)
                        '79
                    ElseIf prods > (cuantos * 79) And prods < ((cuantos * 80) + 1) Then
                        btnProd.Left = (btnProd.Width * 79)
                        btnProd.Top = (prods - ((cuantos * 79) + 1)) * (btnProd.Height + 0.5)
                        '80
                    ElseIf prods > (cuantos * 80) And prods < ((cuantos * 81) + 1) Then
                        btnProd.Left = (btnProd.Width * 80)
                        btnProd.Top = (prods - ((cuantos * 80) + 1)) * (btnProd.Height + 0.5)
                        '81
                    ElseIf prods > (cuantos * 81) And prods < ((cuantos * 82) + 1) Then
                        btnProd.Left = (btnProd.Width * 81)
                        btnProd.Top = (prods - ((cuantos * 81) + 1)) * (btnProd.Height + 0.5)
                        '82
                    ElseIf prods > (cuantos * 82) And prods < ((cuantos * 83) + 1) Then
                        btnProd.Left = (btnProd.Width * 82)
                        btnProd.Top = (prods - ((cuantos * 82) + 1)) * (btnProd.Height + 0.5)
                        '83
                    ElseIf prods > (cuantos * 83) And prods < ((cuantos * 84) + 1) Then
                        btnProd.Left = (btnProd.Width * 83)
                        btnProd.Top = (prods - ((cuantos * 83) + 1)) * (btnProd.Height + 0.5)
                        '84
                    ElseIf prods > (cuantos * 84) And prods < ((cuantos * 85) + 1) Then
                        btnProd.Left = (btnProd.Width * 84)
                        btnProd.Top = (prods - ((cuantos * 84) + 1)) * (btnProd.Height + 0.5)
                        '85
                    ElseIf prods > (cuantos * 85) And prods < ((cuantos * 86) + 1) Then
                        btnProd.Left = (btnProd.Width * 85)
                        btnProd.Top = (prods - ((cuantos * 85) + 1)) * (btnProd.Height + 0.5)
                        '86
                    ElseIf prods > (cuantos * 86) And prods < ((cuantos * 87) + 1) Then
                        btnProd.Left = (btnProd.Width * 86)
                        btnProd.Top = (prods - ((cuantos * 86) + 1)) * (btnProd.Height + 0.5)
                        '87
                    ElseIf prods > (cuantos * 87) And prods < ((cuantos * 88) + 1) Then
                        btnProd.Left = (btnProd.Width * 87)
                        btnProd.Top = (prods - ((cuantos * 87) + 1)) * (btnProd.Height + 0.5)
                        '88
                    ElseIf prods > (cuantos * 88) And prods < ((cuantos * 89) + 1) Then
                        btnProd.Left = (btnProd.Width * 88)
                        btnProd.Top = (prods - ((cuantos * 88) + 1)) * (btnProd.Height + 0.5)
                        '89
                    ElseIf prods > (cuantos * 89) And prods < ((cuantos * 90) + 1) Then
                        btnProd.Left = (btnProd.Width * 89)
                        btnProd.Top = (prods - ((cuantos * 89) + 1)) * (btnProd.Height + 0.5)
                        '90
                    ElseIf prods > (cuantos * 90) And prods < ((cuantos * 91) + 1) Then
                        btnProd.Left = (btnProd.Width * 90)
                        btnProd.Top = (prods - ((cuantos * 90) + 1)) * (btnProd.Height + 0.5)
                        '91
                    ElseIf prods > (cuantos * 91) And prods < ((cuantos * 92) + 1) Then
                        btnProd.Left = (btnProd.Width * 91)
                        btnProd.Top = (prods - ((cuantos * 91) + 1)) * (btnProd.Height + 0.5)
                        '92
                    ElseIf prods > (cuantos * 92) And prods < ((cuantos * 93) + 1) Then
                        btnProd.Left = (btnProd.Width * 92)
                        btnProd.Top = (prods - ((cuantos * 92) + 1)) * (btnProd.Height + 0.5)
                        '93
                    ElseIf prods > (cuantos * 93) And prods < ((cuantos * 94) + 1) Then
                        btnProd.Left = (btnProd.Width * 93)
                        btnProd.Top = (prods - ((cuantos * 93) + 1)) * (btnProd.Height + 0.5)
                        '94
                    ElseIf prods > (cuantos * 94) And prods < ((cuantos * 95) + 1) Then
                        btnProd.Left = (btnProd.Width * 94)
                        btnProd.Top = (prods - ((cuantos * 94) + 1)) * (btnProd.Height + 0.5)
                        '95
                    ElseIf prods > (cuantos * 95) And prods < ((cuantos * 96) + 1) Then
                        btnProd.Left = (btnProd.Width * 95)
                        btnProd.Top = (prods - ((cuantos * 95) + 1)) * (btnProd.Height + 0.5)
                        '96
                    ElseIf prods > (cuantos * 96) And prods < ((cuantos * 97) + 1) Then
                        btnProd.Left = (btnProd.Width * 96)
                        btnProd.Top = (prods - ((cuantos * 96) + 1)) * (btnProd.Height + 0.5)
                        '97
                    ElseIf prods > (cuantos * 97) And prods < ((cuantos * 98) + 1) Then
                        btnProd.Left = (btnProd.Width * 97)
                        btnProd.Top = (prods - ((cuantos * 97) + 1)) * (btnProd.Height + 0.5)
                        '98
                    ElseIf prods > (cuantos * 98) And prods < ((cuantos * 99) + 1) Then
                        btnProd.Left = (btnProd.Width * 98)
                        btnProd.Top = (prods - ((cuantos * 98) + 1)) * (btnProd.Height + 0.5)
                        '99
                    ElseIf prods > (cuantos * 99) And prods < ((cuantos * 100) + 1) Then
                        btnProd.Left = (btnProd.Width * 99)
                        btnProd.Top = (prods - ((cuantos * 99) + 1)) * (btnProd.Height + 0.5)
                        '100
                    ElseIf prods > (cuantos * 100) And prods < ((cuantos * 101) + 1) Then
                        btnProd.Left = (btnProd.Width * 100)
                        btnProd.Top = (prods - ((cuantos * 100) + 1)) * (btnProd.Height + 0.5)
                        '101
                    ElseIf prods > (cuantos * 101) And prods < ((cuantos * 102) + 1) Then
                        btnProd.Left = (btnProd.Width * 101)
                        btnProd.Top = (prods - ((cuantos * 101) + 1)) * (btnProd.Height + 0.5)
                        '102
                    ElseIf prods > (cuantos * 102) And prods < ((cuantos * 103) + 1) Then
                        btnProd.Left = (btnProd.Width * 102)
                        btnProd.Top = (prods - ((cuantos * 102) + 1)) * (btnProd.Height + 0.5)
                        '103
                    ElseIf prods > (cuantos * 103) And prods < ((cuantos * 104) + 1) Then
                        btnProd.Left = (btnProd.Width * 103)
                        btnProd.Top = (prods - ((cuantos * 103) + 1)) * (btnProd.Height + 0.5)
                        '104
                    ElseIf prods > (cuantos * 104) And prods < ((cuantos * 105) + 1) Then
                        btnProd.Left = (btnProd.Width * 104)
                        btnProd.Top = (prods - ((cuantos * 104) + 1)) * (btnProd.Height + 0.5)
                        '105
                    ElseIf prods > (cuantos * 105) And prods < ((cuantos * 106) + 1) Then
                        btnProd.Left = (btnProd.Width * 105)
                        btnProd.Top = (prods - ((cuantos * 105) + 1)) * (btnProd.Height + 0.5)
                        '106
                    ElseIf prods > (cuantos * 106) And prods < ((cuantos * 107) + 1) Then
                        btnProd.Left = (btnProd.Width * 106)
                        btnProd.Top = (prods - ((cuantos * 106) + 1)) * (btnProd.Height + 0.5)
                        '107
                    ElseIf prods > (cuantos * 107) And prods < ((cuantos * 108) + 1) Then
                        btnProd.Left = (btnProd.Width * 107)
                        btnProd.Top = (prods - ((cuantos * 107) + 1)) * (btnProd.Height + 0.5)

                    Else
                        btnProd.Left = 0
                        btnProd.Top = (prods - 1) * (btnProd.Height + 0.5)
                    End If

                    btnProd.BackColor = Color.Orange
                    btnProd.FlatStyle = FlatStyle.Popup
                    btnProd.FlatAppearance.BorderSize = 0

                    If File.Exists(My.Application.Info.DirectoryPath & "\ProductosImg" & baseseleccionada & "\" & rd3(1).ToString & ".jpg") Then
                        btnProd.BackgroundImage = Image.FromFile(My.Application.Info.DirectoryPath & "\ProductosImg" & baseseleccionada & "\" & rd3(1).ToString & ".jpg")
                        btnProd.BackgroundImageLayout = ImageLayout.Stretch
                        btnProd.TextAlign = ContentAlignment.BottomCenter
                        btnProd.ForeColor = Color.Black
                    End If

                    AddHandler btnProd.Click, AddressOf btnProd_Click
                    pProductos.Controls.Add(btnProd)
                    If prods = 0 Then
                        Preferencias(producto)
                        Extras(producto)
                    End If
                    prods += 1

                End If
            Loop
            rd3.Close()
            cnn3.Close()

        Catch ex As Exception
            MessageBox.Show(ex.ToString)
            cnn3.Close()
        End Try
    End Sub

    Private Sub btnProd_Click(sender As Object, e As EventArgs)

        Dim cnn1 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
        Dim cnn3 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
        Dim rd1 As MySqlDataReader
        Dim cmd1 As MySqlCommand

        Dim btnProducto As Button = CType(sender, Button)
        pPreferencias.Controls.Clear()
        pExtras.Controls.Clear()

        CodigoProducto = ""
        CodigoProducto = btnProducto.Tag
        cantidadPromo = 0

        If cnn3.State = 1 Then
            cnn3.Close()
        End If

        Preferencias(btnProducto.Tag)
        Extras(btnProducto.Tag)
        Promociones(btnProducto.Tag)

        banderaPromo = 0
        lblpromo.Text = "0"

        If cantidadPromo = 0 Then

            cantpromo = 0
            cnn1.Close() : cnn1.Open()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "select F44 from Productos where Codigo = '" & CodigoProducto & "'"
            rd1 = cmd1.ExecuteReader
            If rd1.HasRows Then
                If rd1.Read Then
                    cantidadPromo = CDec(IIf(rd1("F44").ToString = "", "0", rd1("F44").ToString))
                Else
                    cantidadPromo = 1
                End If
            Else
                cantidadPromo = 1
            End If
            rd1.Close()
            cnn1.Close()

        End If

        cnn1.Close() : cnn1.Open()
        cmd1 = cnn1.CreateCommand
        cmd1.CommandText = "SELECT Comensal FROM Ticket"
        rd1 = cmd1.ExecuteReader
        If rd1.HasRows Then
            If rd1.Read Then
                VarComensal = rd1(0).ToString

                If VarComensal = 0 Then

                    cantidad = 1
                    cantidad2 = 1
                    respuesta = ""
                    ObtenerProducto(btnProducto.Tag)

                ElseIf VarComensal = 1 Then

                    damecodigo = btnProducto.Tag

                    With pteclado
                        .Show()
                        gdato.Text = "Comensal"
                        cantidad = 1
                        txtRespuesta.Focus.Equals(True)
                    End With
                End If

            End If
        End If
        rd1.Close()
        cnn1.Close()


        nompreferencia = ""
    End Sub



    Private Sub btnSalir_Click(sender As Object, e As EventArgs) Handles btnSalir.Click
        Me.Close()
    End Sub

    Private Sub frmAgregarProducto_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        frmMesas.Show()
    End Sub

    Private Sub grdCaptura_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles grdCaptura.CellClick
        Dim celda As DataGridViewCellEventArgs = e
        Dim index As Integer = grdCaptura.CurrentRow.Index

        Dim cnn1 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
        Dim rd1 As MySqlDataReader
        Dim cmd1 As MySqlCommand


        cnn1.Close() : cnn1.Open()
        If celda.ColumnIndex = 2 Then
            pteclado.Show()
            txtRespuesta.Text = ""
            gdato.Text = "Cantidad"

            txtRespuesta.Focus.Equals(True)
            CodigoProducto = grdCaptura.Rows(index).Cells(0).Value.ToString


            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "SELECT Nombre FROM Productos WHERE Codigo='" & CodigoProducto & "'"
            rd1 = cmd1.ExecuteReader
            If rd1.HasRows Then
                If rd1.Read Then
                    descripcionpro = rd1(0).ToString
                End If
            End If
            rd1.Close()
            banderacantidad = 1
        End If

        If celda.ColumnIndex = 3 Then
            pteclado.Show()
            gteclas.Enabled = False
            txtRespuesta.Text = ""
            txtRespuesta.Focus.Equals(True)
            gdato.Text = "Cambiar Precio"
            CodigoProducto = grdCaptura.CurrentRow.Cells(0).Value.ToString
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "SELECT Nombre FROM Productos WHERE Codigo='" & CodigoProducto & "'"
            rd1 = cmd1.ExecuteReader
            If rd1.HasRows Then
                If rd1.Read Then
                    descripcionpro = rd1(0).ToString
                End If
            End If
            rd1.Close()
            banderaprecio = 1
        End If

        If celda.ColumnIndex = 4 Then
            If e.RowIndex >= 0 AndAlso e.ColumnIndex >= 0 Then
                grdCaptura.Rows(index).Cells(9).Value = 1
                pteclado.Show()
                gteclas.Enabled = True
                txtRespuesta.Focus.Equals(True)
                txtRespuesta.Text = grdCaptura.Rows(index).Cells(8).Value
                gdato.Text = "Comentario"
                CodigoProducto = grdCaptura.CurrentRow.Cells(0).Value.ToString
                banderacomentario = grdCaptura.Rows(index).Cells(9).Value
            End If

        End If

        cnn1.Close()
    End Sub

    Private Sub grdCaptura_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles grdCaptura.CellDoubleClick

        Dim INDEX As Double = grdCaptura.CurrentRow.Index

        Dim IMPORTE = grdCaptura.Rows(INDEX).Cells(4).Value.ToString
        lblTotalVenta.Text = lblTotalVenta.Text - IMPORTE
        lblTotalVenta.Text = FormatNumber(lblTotalVenta.Text, 2)
        grdCaptura.Rows.Remove(grdCaptura.CurrentRow)
    End Sub
    Private Sub lblTotalVenta_TextChanged(sender As Object, e As EventArgs) Handles lblTotalVenta.TextChanged
        If lblTotalVenta.Text = "" Then Exit Sub
        Dim TotalImporte As Double = lblTotalVenta.Text
        Dim CantidadLetra As String = ""
        If TotalImporte > 0 Then

            CantidadLetra = UCase(convLetras(TotalImporte))
        Else

            CantidadLetra = ""
        End If
        LBLLETRA.Text = CantidadLetra
    End Sub

    Private Sub txtRespuesta_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtRespuesta.KeyPress
        e.KeyChar = UCase(e.KeyChar)
        If AscW(e.KeyChar) = Keys.Enter Then

            respuesta = txtRespuesta.Text

            Dim totalventa As Double = 0

            Dim cnn1 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
            Dim cnn2 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
            Dim cnn3 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)

            Dim rd2, rd3 As MySqlDataReader
            Dim cmd2, cmd3 As MySqlCommand

            With Me.grdCaptura
                Dim banderaproducto As Integer = 0
                banderaproducto = 0
                Dim totalnuevo As Double = 0


                If gdato.Text = "Comensal" Then

                    ObtenerProducto(damecodigo)
                    banderaPROMOCION = 0
                End If

                If gdato.Text = "Cantidad" Then
                    If banderacantidad = 1 Then
                        For q As Integer = 0 To grdCaptura.Rows.Count - 1
                            lblTotalVenta.Text = "0.00"

                            If grdCaptura.Rows(q).Cells(0).Value = CodigoProducto Then
                                grdCaptura.Rows(q).Cells(1).Value = CodigoProducto & vbNewLine & descripcionpro
                                grdCaptura.Rows(q).Cells(2).Value = FormatNumber(respuesta, 2)
                                grdCaptura.Rows(q).Cells(3).Value = grdCaptura.Rows(q).Cells(3).Value.ToString
                                totalnuevo = respuesta * grdCaptura.Rows(q).Cells(3).Value.ToString
                                grdCaptura.Rows(q).Cells(4).Value = FormatNumber(totalnuevo, 2)

                                banderaproducto = 1
                            Else
                                'grdCaptura.Rows(q).Cells(2).Value = FormatNumber(respuesta, 2)
                            End If
                        Next q
                    End If
                    cnn1.Close()
                End If

                If gdato.Text = "Cambiar Precio" Then

                    Dim idempleado As Integer = 0

                    cnn3.Close() : cnn3.Open()
                    cmd3 = cnn3.CreateCommand
                    cmd3.CommandText = "SELECT IdEmpleado FROM usuarios WHERE Alias='" & lblatiende.Text & "'"
                    rd3 = cmd3.ExecuteReader
                    If rd3.HasRows Then
                        If rd3.Read Then
                            idempleado = rd3(0).ToString

                            cnn2.Close() : cnn2.Open()
                            cmd2 = cnn2.CreateCommand
                            cmd2.CommandText = "SELECT Prod_pre FROM permisos WHERE IdEmpleado=" & idempleado & ""
                            rd2 = cmd2.ExecuteReader
                            If rd2.HasRows Then
                                If rd2.Read Then
                                    If rd2(0).ToString = 1 Then

                                        If banderaprecio = 1 Then
                                            For q As Integer = 0 To grdCaptura.Rows.Count - 1
                                                lblTotalVenta.Text = "0.00"
                                                If grdCaptura.Rows(q).Cells(0).Value = CodigoProducto Then
                                                    grdCaptura.Rows(q).Cells(1).Value = CodigoProducto & vbNewLine & descripcionpro

                                                    grdCaptura.Rows(q).Cells(2).Value = grdCaptura.Rows(q).Cells(2).Value.ToString
                                                    grdCaptura.Rows(q).Cells(3).Value = FormatNumber(respuesta, 2)
                                                    totalnuevo = grdCaptura.Rows(q).Cells(2).Value.ToString * respuesta
                                                    grdCaptura.Rows(q).Cells(4).Value = FormatNumber(totalnuevo, 2)

                                                    lblTotalVenta.Text = lblTotalVenta.Text + totalnuevo
                                                    lblTotalVenta.Text = FormatNumber(lblTotalVenta.Text, 2)

                                                    banderaproducto = 1
                                                End If
                                            Next q
                                        End If
                                        cnn1.Close()

                                    Else
                                        MsgBox("El usuario no tiene permisos para asignar los precios", vbInformation + vbOKOnly, titulomensajes)
                                        txtRespuesta.Text = ""
                                        txtRespuesta.Focus.Equals(True)
                                        Exit Sub
                                    End If

                                End If
                            Else
                                MsgBox("El usuario no tiene permisos para asignar los precios", vbInformation + vbOKOnly, titulomensajes)
                                Exit Sub
                            End If
                            rd2.Close()
                            cnn2.Close()

                        End If
                    End If
                    rd3.Close()
                    cnn3.Close()



                End If

                If gdato.Text = "Comentario" Then
                    banderaPROMOCION = 0
                    If banderacomentario = 1 Then
                        For q As Integer = 0 To grdCaptura.Rows.Count - 1

                            If grdCaptura.Rows(q).Cells(0).Value = CodigoProducto Then
                                If grdCaptura.Rows(q).Cells(9).Value = 1 Then
                                    grdCaptura.Rows(q).Cells(8).Value = respuesta
                                    grdCaptura.Rows(q).Cells(9).Value = 0
                                End If

                            End If

                        Next
                    End If
                End If

                If banderaPROMOCION = 1 Then
                    If (txtRespuesta.Text + cantpromo) <= cantidadPromo Then
                        cantpromo = CDec(cantpromo) + CDec(txtRespuesta.Text)


                        'CodigoProducto
                        cantidad = txtRespuesta.Text
                        ObtenerProducto(CodigoProducto)
                    Else
                        MsgBox("La suma de los productos de la promoción no puede ser mayor a " & cantidadPromo & "")
                    End If
                End If

                For luffy As Integer = 0 To grdCaptura.Rows.Count - 1
                    totalventa = totalventa + grdCaptura.Rows(luffy).Cells(4).Value.ToString
                    lblTotalVenta.Text = FormatNumber(totalventa, 2)
                Next

            End With
            banderaprecio = 0
            banderacantidad = 0
            txtRespuesta.Text = ""
            pteclado.Visible = False

        End If
    End Sub

    Public Sub Preferencias(ByVal producto As String)

        Dim preferencia As Integer = 1
        Dim cuantospre As Integer = Math.Truncate(pPreferencias.Height / 55)

        Dim cnn1 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
        Dim rd1 As MySqlDataReader
        Dim cmd1 As MySqlCommand

        cnn1.Close() : cnn1.Open()
        cmd1 = cnn1.CreateCommand
        cmd1.CommandText = "SELECT DISTINCT IdPrefe FROM preferencia WHERE Codigo='" & CodigoProducto & "' order by Codigo"
        rd1 = cmd1.ExecuteReader
        Do While rd1.Read
            If rd1.HasRows Then
                TotPrefe = TotPrefe + 1
            End If
        Loop
        rd1.Close()

        If TotPrefe <= 4 Then
            pPreferencias.AutoScroll = False
        Else
            pPreferencias.AutoScroll = True
        End If

        Try

            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "SELECT distinct NombrePrefe FROM Preferencia WHERE Codigo='" & producto & "' order by NombrePrefe"
            rd1 = cmd1.ExecuteReader
            Do While rd1.Read
                If rd1.HasRows Then

                    Dim preferencias As String = rd1("NombrePrefe").ToString

                    btnPrefe = New Button
                    btnPrefe.Text = preferencias
                    btnPrefe.Name = "btnPrefe(" & preferencia & ")"
                    btnPrefe.Height = 55
                    btnPrefe.Width = 70

                    If preferencia > cuantospre And preferencia < ((cuantospre * 2) + 1) Then
                        btnPrefe.Left = (btnPrefe.Width * 1)
                        btnPrefe.Top = (preferencia - (cuantospre + 1)) * (btnPrefe.Height + 0.5)
                        '2
                    ElseIf preferencia > (cuantospre * 2) And preferencia < ((cuantospre * 3) + 1) Then
                        btnPrefe.Left = (btnPrefe.Width * 2)
                        btnPrefe.Top = (preferencia - ((cuantospre * 2) + 1)) * (btnPrefe.Height + 0.5)
                        '3
                    ElseIf preferencia > (cuantospre * 3) And preferencia < ((cuantospre * 4) + 1) Then
                        btnPrefe.Left = (btnPrefe.Width * 3)
                        btnPrefe.Top = (preferencia - ((cuantospre * 3) + 1)) * (btnPrefe.Height + 0.5)
                        '4
                    ElseIf preferencia > (cuantospre * 4) And preferencia < ((cuantospre * 5) + 1) Then
                        btnPrefe.Left = (btnPrefe.Width * 4)
                        btnPrefe.Top = (preferencia - ((cuantospre * 4) + 1)) * (btnPrefe.Height + 0.5)
                        '5
                    ElseIf preferencia > (cuantospre * 5) And preferencia < ((cuantospre * 6) + 1) Then
                        btnPrefe.Left = (btnPrefe.Width * 5)
                        btnPrefe.Top = (preferencia - ((cuantospre * 5) + 1)) * (btnPrefe.Height + 0.5)
                        '6
                    ElseIf preferencia > (cuantospre * 6) And preferencia < ((cuantospre * 7) + 1) Then
                        btnPrefe.Left = (btnPrefe.Width * 6)
                        btnPrefe.Top = (preferencia - ((cuantospre * 6) + 1)) * (btnPrefe.Height + 0.5)
                        '7
                    ElseIf preferencia > (cuantospre * 7) And preferencia < ((cuantospre * 8) + 1) Then
                        btnPrefe.Left = (btnPrefe.Width * 7)
                        btnPrefe.Top = (preferencia - ((cuantospre * 7) + 1)) * (btnPrefe.Height + 0.5)
                        '8
                    ElseIf preferencia > (cuantospre * 8) And preferencia < ((cuantospre * 9) + 1) Then
                        btnPrefe.Left = (btnPrefe.Width * 8)
                        btnPrefe.Top = (preferencia - ((cuantospre * 8) + 1)) * (btnPrefe.Height + 0.5)
                        '9
                    ElseIf preferencia > (cuantospre * 9) And preferencia < ((cuantospre * 10) + 1) Then
                        btnPrefe.Left = (btnPrefe.Width * 9)
                        btnPrefe.Top = (preferencia - ((cuantospre * 9) + 1)) * (btnPrefe.Height + 0.5)
                        '10
                    ElseIf preferencia > (cuantospre * 10) And preferencia < ((cuantospre * 11) + 1) Then
                        btnPrefe.Left = (btnPrefe.Width * 10)
                        btnPrefe.Top = (preferencia - ((cuantospre * 10) + 1)) * (btnPrefe.Height + 0.5)
                    Else
                        btnPrefe.Left = 0
                        btnPrefe.Top = (preferencia - 1) * (btnPrefe.Height + 0.5)
                    End If


                    btnPrefe.BackColor = Color.Orange
                    btnPrefe.FlatStyle = FlatStyle.Flat
                    btnPrefe.FlatAppearance.BorderSize = 1
                    AddHandler btnPrefe.Click, AddressOf btnPrefe_Click
                    pPreferencias.Controls.Add(btnPrefe)
                    preferencia += 1

                End If
            Loop
            rd1.Close()
            cnn1.Close()

        Catch ex As Exception
            MessageBox.Show(ex.ToString)
            cnn1.Close()
        End Try
    End Sub
    Private Sub btnPrefe_Click(sender As Object, e As EventArgs)
        Dim btnPreferencia As Button = CType(sender, Button)
        nompreferencia = btnPreferencia.Text

        For q As Integer = 0 To grdCaptura.Rows.Count - 1
            If grdCaptura.Rows(q).Cells(0).Value = CodigoProducto Then
                grdCaptura.Rows(q).Cells(8).Value = grdCaptura.Rows(q).Cells(8).Value + " " + nompreferencia
            End If
        Next
    End Sub

    Public Sub Extras(ByVal productos As String)

        Dim extra As Integer = 1

        Dim cuantosextra As Integer = Math.Truncate(pExtras.Height / 60)

        If totExtras <= 5 Then
            pExtras.AutoScroll = False
        Else
            pExtras.AutoScroll = True
        End If

        Dim cnn2 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
        Dim rd2 As MySqlDataReader
        Dim cmd2 As MySqlCommand

        Try
            cnn2.Close() : cnn2.Open()
            cmd2 = cnn2.CreateCommand
            cmd2.CommandText = "SELECT distinct Descx,Codigo FROM Extras WHERE CodigoAlpha='" & productos & "' order by Descx"
            rd2 = cmd2.ExecuteReader
            Do While rd2.Read
                If rd2.HasRows Then

                    Dim extras As String = rd2("Descx").ToString

                    btnExtra = New Button
                    btnExtra.Text = extras
                    btnExtra.Tag = rd2("Codigo").ToString
                    btnExtra.Name = "btnExtra(" & extra & ")"
                    btnExtra.Height = 70
                    btnExtra.Width = 80

                    If extra > cuantosextra And extra < ((cuantosextra * 2) + 1) Then
                        btnExtra.Left = (btnExtra.Width * 1)
                        btnExtra.Top = (extra - (cuantosextra + 1)) * (btnExtra.Height + 0.5)
                        '2
                    ElseIf extra > (cuantosextra * 2) And extra < ((cuantosextra * 3) + 1) Then
                        btnExtra.Left = (btnExtra.Width * 2)
                        btnExtra.Top = (extra - ((cuantosextra * 2) + 1)) * (btnExtra.Height + 0.5)
                        '3
                    ElseIf extra > (cuantosextra * 3) And extra < ((cuantosextra * 4) + 1) Then
                        btnExtra.Left = (btnExtra.Width * 3)
                        btnExtra.Top = (extra - ((cuantosextra * 3) + 1)) * (btnExtra.Height + 0.5)
                        '4
                    ElseIf extra > (cuantosextra * 4) And extra < ((cuantosextra * 5) + 1) Then
                        btnExtra.Left = (btnExtra.Width * 4)
                        btnExtra.Top = (extra - ((cuantosextra * 4) + 1)) * (btnExtra.Height + 0.5)
                        '5
                    ElseIf extra > (cuantosextra * 5) And extra < ((cuantosextra * 6) + 1) Then
                        btnExtra.Left = (btnExtra.Width * 5)
                        btnExtra.Top = (extra - ((cuantosextra * 5) + 1)) * (btnExtra.Height + 0.5)
                    Else
                        btnExtra.Left = 0
                        btnExtra.Top = (extra - 1) * (btnExtra.Height + 0.5)
                    End If

                    btnExtra.BackColor = Color.Orange
                    btnExtra.FlatStyle = FlatStyle.Flat
                    btnExtra.FlatAppearance.BorderSize = 1
                    AddHandler btnExtra.Click, AddressOf btnExtra_Click
                    pExtras.Controls.Add(btnExtra)
                    extra += 1

                End If
            Loop
            rd2.Close()
            cnn2.Close()

        Catch ex As Exception
            MessageBox.Show(ex.ToString)
            cnn2.Close()
        End Try

    End Sub

    Private Sub btnExtra_Click(sender As Object, e As EventArgs)

        Dim btnEx As Button = CType(sender, Button)
        CodigoProducto = btnEx.Tag

        If cantidad > 1 Then
        Else
            cantidad = 1
        End If
        ObtenerProducto(CodigoProducto)

    End Sub
    Public Sub Promociones(ByVal productos As String)

        Dim promocion As Integer = 1
        Dim cuantospro As Integer = Math.Truncate(pPromociones.Height / 70)

        Dim cnn3 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
        Dim rd3 As MySqlDataReader
        Dim cmd3 As MySqlCommand

        Try
            cnn3.Close() : cnn3.Open()

            cmd3 = cnn3.CreateCommand
            cmd3.CommandText = "SELECT count(Id) FROM Promociones WHERE CodigoAlpha='" & productos & "' ORDER BY Descx"
            rd3 = cmd3.ExecuteReader
            Do While rd3.Read
                If rd3.HasRows Then
                    TotPromociones = TotPromociones + 1
                End If
            Loop
            rd3.Close()

            If TotPromociones <= 10 Then
                pPromociones.AutoScroll = False
            Else
                pPromociones.AutoScroll = True
            End If

            cmd3 = cnn3.CreateCommand
            cmd3.CommandText = "SELECT distinct Descx,Codigo FROM Promociones WHERE CodigoAlpha='" & productos & "' ORDER BY Descx"
            rd3 = cmd3.ExecuteReader
            Do While rd3.Read
                If rd3.HasRows Then

                    Dim promociones As String = rd3("Descx").ToString
                    Dim codigopromo As String = rd3("Codigo").ToString

                    btnPromo = New Button
                    btnPromo.Text = promociones
                    btnPromo.Tag = codigopromo
                    btnPromo.Name = "btnPromo(" & promocion & ")"
                    btnPromo.Height = 70
                    btnPromo.Width = 80

                    If promocion > cuantospro And promocion < ((cuantospro * 2) + 1) Then
                        btnPromo.Left = (btnPromo.Width * 1)
                        btnPromo.Top = (promocion - (cuantospro + 1)) * (btnPromo.Height + 0.5)
                        '2
                    ElseIf promocion > (cuantospro * 2) And promocion < ((cuantospro * 3) + 1) Then
                        btnPromo.Left = (btnPromo.Width * 2)
                        btnPromo.Top = (promocion - ((cuantospro * 2) + 1)) * (btnPromo.Height + 0.5)
                        '3
                    ElseIf promocion > (cuantospro * 3) And promocion < ((cuantospro * 4) + 1) Then
                        btnPromo.Left = (btnPromo.Width * 3)
                        btnPromo.Top = (promocion - ((cuantospro * 3) + 1)) * (btnPromo.Height + 0.5)
                        '4
                    ElseIf promocion > (cuantospro * 4) And promocion < ((cuantospro * 5) + 1) Then
                        btnPromo.Left = (btnPromo.Width * 4)
                        btnPromo.Top = (promocion - ((cuantospro * 4) + 1)) * (btnPromo.Height + 0.5)
                        '5
                    ElseIf promocion > (cuantospro * 5) And promocion < ((cuantospro * 6) + 1) Then
                        btnPromo.Left = (btnPromo.Width * 5)
                        btnPromo.Top = (promocion - ((cuantospro * 5) + 1)) * (btnPromo.Height + 0.5)
                        '6
                    ElseIf promocion > (cuantospro * 6) And promocion < ((cuantospro * 7) + 1) Then
                        btnPromo.Left = (btnPromo.Width * 6)
                        btnPromo.Top = (promocion - ((cuantospro * 6) + 1)) * (btnPromo.Height + 0.5)
                        '7
                    ElseIf promocion > (cuantospro * 7) And promocion < ((cuantospro * 8) + 1) Then
                        btnPromo.Left = (btnPromo.Width * 7)
                        btnPromo.Top = (promocion - ((cuantospro * 7) + 1)) * (btnPromo.Height + 0.5)
                        '8
                    ElseIf promocion > (cuantospro * 8) And promocion < ((cuantospro * 9) + 1) Then
                        btnPromo.Left = (btnPromo.Width * 8)
                        btnPromo.Top = (promocion - ((cuantospro * 8) + 1)) * (btnPromo.Height + 0.5)
                        '9
                    ElseIf promocion > (cuantospro * 9) And promocion < ((cuantospro * 10) + 1) Then
                        btnPromo.Left = (btnPromo.Width * 9)
                        btnPromo.Top = (promocion - ((cuantospro * 9) + 1)) * (btnPromo.Height + 0.5)
                        '10
                    ElseIf promocion > (cuantospro * 10) And promocion < ((cuantospro * 11) + 1) Then
                        btnPromo.Left = (btnPromo.Width * 10)
                        btnPromo.Top = (promocion - ((cuantospro * 10) + 1)) * (btnPromo.Height + 0.5)
                    Else
                        btnPromo.Left = 0
                        btnPromo.Top = (promocion - 1) * (btnPromo.Height + 0.5)
                    End If

                    btnPromo.BackColor = Color.Orange
                    btnPromo.FlatStyle = FlatStyle.Flat
                    btnPromo.FlatAppearance.BorderSize = 1
                    AddHandler btnPromo.Click, AddressOf btnPromo_Click
                    pPromociones.Controls.Add(btnPromo)
                    promocion += 1

                End If
            Loop
            rd3.Close()
            cnn3.Close()

        Catch ex As Exception
            MessageBox.Show(ex.ToString)
            cnn3.Close()
        End Try
    End Sub

    Private Sub btnPromo_Click(sender As Object, e As EventArgs)
        Dim btnPromocion As Button = CType(sender, Button)
        CodigoProducto = btnPromocion.Tag
        'ObtenerProducto(CodigoProducto)

        Dim cnn1 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
        Dim rd1 As MySqlDataReader
        Dim cmd1 As MySqlCommand

        If cantpromo < cantidadPromo Then
            statusPromo = 1
            pteclado.Show()
            gteclas.Enabled = True
            txtRespuesta.Focus.Equals(True)
            txtRespuesta.Text = ""
            gdato.Text = "Cantidad"
            CodigoProducto = CodigoProducto
            banderaPROMOCION = 1

            cnn1.Close() : cnn1.Open()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "SELECT * FROM preferencia WHERE Codigo='" & CodigoProducto & "'"
            rd1 = cmd1.ExecuteReader
            If rd1.HasRows Then
                If rd1.Read Then
                    Preferenciaspro(CodigoProducto)
                End If
            End If
            rd1.Close()
            cnn1.Close()

        End If
    End Sub

    Public Sub Preferenciaspro(ByVal promocion As String)

        Dim preferencia As Integer = 1
        Dim cuantospre As Integer = Math.Truncate(pPreferencias.Height / 55)

        Dim cnn1 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
        Dim rd1 As MySqlDataReader
        Dim cmd1 As MySqlCommand

        cnn1.Close() : cnn1.Open()
        cmd1 = cnn1.CreateCommand
        cmd1.CommandText = "SELECT DISTINCT IdPrefe FROM preferencia WHERE Codigo='" & promocion & "' order by Codigo"
        rd1 = cmd1.ExecuteReader
        Do While rd1.Read
            If rd1.HasRows Then
                TotPrefe = TotPrefe + 1
            End If
        Loop
        rd1.Close()

        If TotPrefe <= 4 Then
            pPreferencias.AutoScroll = False
        Else
            pPreferencias.AutoScroll = True
        End If

        Try

            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "SELECT distinct NombrePrefe FROM Preferencia WHERE Codigo='" & promocion & "' order by NombrePrefe"
            rd1 = cmd1.ExecuteReader
            Do While rd1.Read
                If rd1.HasRows Then

                    Dim preferencias As String = rd1("NombrePrefe").ToString

                    btnPrefe = New Button
                    btnPrefe.Text = preferencias
                    btnPrefe.Name = "btnPrefe(" & preferencia & ")"
                    btnPrefe.Height = 55
                    btnPrefe.Width = 70

                    If preferencia > cuantospre And preferencia < ((cuantospre * 2) + 1) Then
                        btnPrefe.Left = (btnPrefe.Width * 1)
                        btnPrefe.Top = (preferencia - (cuantospre + 1)) * (btnPrefe.Height + 0.5)
                        '2
                    ElseIf preferencia > (cuantospre * 2) And preferencia < ((cuantospre * 3) + 1) Then
                        btnPrefe.Left = (btnPrefe.Width * 2)
                        btnPrefe.Top = (preferencia - ((cuantospre * 2) + 1)) * (btnPrefe.Height + 0.5)
                        '3
                    ElseIf preferencia > (cuantospre * 3) And preferencia < ((cuantospre * 4) + 1) Then
                        btnPrefe.Left = (btnPrefe.Width * 3)
                        btnPrefe.Top = (preferencia - ((cuantospre * 3) + 1)) * (btnPrefe.Height + 0.5)
                        '4
                    ElseIf preferencia > (cuantospre * 4) And preferencia < ((cuantospre * 5) + 1) Then
                        btnPrefe.Left = (btnPrefe.Width * 4)
                        btnPrefe.Top = (preferencia - ((cuantospre * 4) + 1)) * (btnPrefe.Height + 0.5)
                        '5
                    ElseIf preferencia > (cuantospre * 5) And preferencia < ((cuantospre * 6) + 1) Then
                        btnPrefe.Left = (btnPrefe.Width * 5)
                        btnPrefe.Top = (preferencia - ((cuantospre * 5) + 1)) * (btnPrefe.Height + 0.5)
                        '6
                    ElseIf preferencia > (cuantospre * 6) And preferencia < ((cuantospre * 7) + 1) Then
                        btnPrefe.Left = (btnPrefe.Width * 6)
                        btnPrefe.Top = (preferencia - ((cuantospre * 6) + 1)) * (btnPrefe.Height + 0.5)
                        '7
                    ElseIf preferencia > (cuantospre * 7) And preferencia < ((cuantospre * 8) + 1) Then
                        btnPrefe.Left = (btnPrefe.Width * 7)
                        btnPrefe.Top = (preferencia - ((cuantospre * 7) + 1)) * (btnPrefe.Height + 0.5)
                        '8
                    ElseIf preferencia > (cuantospre * 8) And preferencia < ((cuantospre * 9) + 1) Then
                        btnPrefe.Left = (btnPrefe.Width * 8)
                        btnPrefe.Top = (preferencia - ((cuantospre * 8) + 1)) * (btnPrefe.Height + 0.5)
                        '9
                    ElseIf preferencia > (cuantospre * 9) And preferencia < ((cuantospre * 10) + 1) Then
                        btnPrefe.Left = (btnPrefe.Width * 9)
                        btnPrefe.Top = (preferencia - ((cuantospre * 9) + 1)) * (btnPrefe.Height + 0.5)
                        '10
                    ElseIf preferencia > (cuantospre * 10) And preferencia < ((cuantospre * 11) + 1) Then
                        btnPrefe.Left = (btnPrefe.Width * 10)
                        btnPrefe.Top = (preferencia - ((cuantospre * 10) + 1)) * (btnPrefe.Height + 0.5)
                    Else
                        btnPrefe.Left = 0
                        btnPrefe.Top = (preferencia - 1) * (btnPrefe.Height + 0.5)
                    End If

                    btnPrefe.BackColor = Color.Orange
                    btnPrefe.FlatStyle = FlatStyle.Flat
                    btnPrefe.FlatAppearance.BorderSize = 1
                    AddHandler btnPrefe.Click, AddressOf btnPrefe_Click
                    pPreferencias.Controls.Add(btnPrefe)
                    preferencia += 1

                End If
            Loop
            rd1.Close()
            cnn1.Close()

        Catch ex As Exception
            MessageBox.Show(ex.ToString)
            cnn1.Close()
        End Try

    End Sub
    Public Sub ObtenerProducto(Codigo As String)

        Dim cnn1 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
        Dim rd1 As MySqlDataReader
        Dim cmd1 As MySqlCommand

        Try
            cnn1.Close() : cnn1.Open()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText =
                "SELECT Departamento,Nombre,UVenta,Min,Ubicacion,Multiplo,Existencia,Grupo,E1,E2,Promociones FROM Productos WHERE Codigo='" & Codigo & "'"
            rd1 = cmd1.ExecuteReader
            If rd1.HasRows Then
                If rd1.Read Then
                    If rd1("Departamento").ToString() = "SERVICIOS" Then
                        existencia = 0
                        Exit Sub
                    End If

                    descripcion = rd1("Nombre").ToString()
                    unidadventa = rd1("UVenta").ToString()
                    minimo = rd1("Min").ToString()
                    ubicacion = rd1("Ubicacion").ToString()
                    multiplo = rd1("Multiplo").ToString()
                    existencia = rd1("Existencia").ToString()
                    sipromo = rd1("Promociones").ToString
                    '  GRUPO = rd1("Grupo").ToString()
                    'Consulta sí hay 2x1
                    doxuno = rd1("E1").ToString()
                    'Consulta sí hay 3x2
                    tresxdos = rd1("E2").ToString()
                End If
            End If
            rd1.Close() : cnn1.Close()

            Call find_preciovta(Codigo)
            min = existencia

            PU = (PU)
            Importe = cantidad * PU

            'If GRUPO = "PROMOCIONES" Then
            If sipromo = 1 Then
                UpGridCaptura()
            Else
                ' If Importe <> 0 Then
                UpGridCaptura()
                ' Else
                ' MsgBox("Este producto no tiene precio de venta, no se agregara en la comanda", vbInformation + vbOKOnly, "Delsscom® Restaurant")
                'End If
            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString)
            cnn1.Close()
        End Try
    End Sub

    Public Sub find_preciovta(codigo As String)

        Dim MyPrecio As Double = 0
        Dim cnn2 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
        Dim rd2 As MySqlDataReader
        Dim cmd2 As MySqlCommand

        cnn2.Close() : cnn2.Open()

        cmd2 = cnn2.CreateCommand
        cmd2.CommandText =
            "SELECT Grupo,PrecioVentaIVA FROM Productos WHERE Codigo='" & codigo & "'"
        rd2 = cmd2.ExecuteReader
        If rd2.HasRows Then
            If rd2.Read Then
                If PEDI = False Then
                    If rd2("Grupo").ToString() = "PROMOCIONES" Then
                        MyPrecio = 0
                    Else
                        MyPrecio = rd2("PrecioVentaIVA").ToString()
                    End If
                ElseIf PEDI = True Then
                    If rd2("Grupo").ToString() = "PROMOCIONES" Then
                        MyPrecio = 0
                    Else
                        'MyPrecio = rd2("PrecioVentaMinIVA").ToString()
                        MyPrecio = rd2("PrecioVentaIVA").ToString()
                    End If
                End If
                If statusPromo = 1 Then
                    PU = 0.0
                    statusPromo = 0
                Else
                    PU = MyPrecio
                    statusPromo = 0
                End If

            End If
        End If
        rd2.Close() : cnn2.Close()
    End Sub

    Public Sub UpGridCaptura()
        Dim TotalVenta As Double = 0
        Dim esta As String = ""
        Dim acumula As Integer = 0
        Dim hora_dia As String = Format(Date.Now, "HH:mm:ss")

        Dim cnn1 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
        Dim cnn2 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
        Dim rd1, rd2 As MySqlDataReader
        Dim cmd1, cmd2 As MySqlCommand

        cnn1.Close() : cnn1.Open()
        cmd1 = cnn1.CreateCommand
        cmd1.CommandText =
            "SELECT Comensal FROM Ticket"
        rd1 = cmd1.ExecuteReader
        If rd1.HasRows Then
            If rd1.Read Then
                esta = rd1("Comensal").ToString
            End If
        End If
        rd1.Close()

        cmd1 = cnn1.CreateCommand
        cmd1.CommandText = "SELECT NotasCred FROM Formatos WHERE Facturas='Acumula'"
        rd1 = cmd1.ExecuteReader
        If rd1.HasRows Then
            If rd1.Read Then
                acumula = rd1(0).ToString
            End If
        End If
        rd1.Close()
        cnn1.Close()

        If esta = 1 Then
            With Me.grdCaptura
                Dim banderaentraa As Integer = 0
                banderaentraa = 0
                For qq As Integer = 0 To .Rows.Count - 1

                    If .Rows(qq).Cells(0).Value = CodigoProducto Then
                        .Rows(qq).Cells(1).Value = CodigoProducto & vbNewLine & descripcion
                        .Rows(qq).Cells(2).Value = .Rows(qq).Cells(2).Value.ToString + CDec(FormatNumber(cantidad, 2))
                        .Rows(qq).Cells(3).Value = FormatNumber(PU, 2)
                        .Rows(qq).Cells(4).Value = .Rows(qq).Cells(4).Value.ToString + CDec(FormatNumber(Importe, 2))
                        .Rows(qq).Cells(5).Value = txtRespuesta.Text

                        'grdcaptura.Rows(qq).Cells(6).Value = lblPromo.Text

                        lblTotalVenta.Text = lblTotalVenta.Text + Importe
                        lblTotalVenta.Text = FormatNumber(lblTotalVenta.Text, 2)
                        banderaentraa = 1
                    End If
                Next

                If banderaentraa = 0 Then
                    .Rows.Add(CodigoProducto, CodigoProducto & vbNewLine & descripcion, FormatNumber(cantidad, 2), FormatNumber(PU, 2), FormatNumber(Importe, 2), respuesta, "", lblpromo.Text, "")

                    lblTotalVenta.Text = lblTotalVenta.Text + Importe
                    lblTotalVenta.Text = FormatNumber(lblTotalVenta.Text, 2)
                End If
            End With
        Else
            Dim inicio1, fin1, inicio2, fin2 As String
            Dim hora_i1, hora_f1, hora_i2, hora_f2 As String

            Dim dia As Integer = Date.Now.DayOfWeek

            '2x1
            If doxuno = 1 Then

                cnn2.Close() : cnn2.Open()
                cmd2 = cnn2.CreateCommand
                cmd2.CommandText = "SELECT Promo2x1,Domingo,HInicioD,HFinD,HInicioD2,HFinD2,Lunes,HInicioL,HFinL,HInicioL2,HFinL2,Martes,HInicioM,HFinM,HInicioM2,HFinM2,Miercoles,HInicioMi,HFinMi,HInicioMi2,HFinMi2,Jueves,HInicioJ,HFinJ,HInicioJ2,HFinJ2,Viernes,HInicioV,HFinV,HInicioV2,HFinV2,Sabado,HInicioS,HFinS,HInicioS2,HFinS2 FROM Promos WHERE Codigo='" & CodigoProducto & "' AND Promo2x1=1"
                rd2 = cmd2.ExecuteReader
                If rd2.HasRows Then
                    If rd2.Read Then
                        If rd2("Promo2x1").ToString = 1 Then
                            dia = Weekday(Date.Now)
                            Select Case dia
                                Case = 1
                                    If rd2("Domingo").ToString = 1 Then
                                        TestStr = Format(Date.Now, "HH:mm:ss")
                                        If TestStr >= rd2("HInicioD").ToString And TestStr <= rd2("HFinD").ToString Or TestStr >= rd2("HInicioD2").ToString And TestStr <= rd2("HFinD2").ToString Then
                                            doxuno = 1
                                        Else
                                            doxuno = 0
                                        End If
                                    Else
                                        doxuno = 0
                                    End If

                                Case = 2
                                    If rd2("Lunes").ToString = 1 Then
                                        TestStr = Format(Date.Now, "HH:mm:ss")
                                        If TestStr >= rd2("HInicioL").ToString And TestStr <= rd2("HFinL").ToString Or TestStr >= rd2("HInicioL2").ToString And TestStr <= rd2("HFinL2").ToString Then
                                            doxuno = 1
                                        Else
                                            doxuno = 0
                                        End If
                                    Else
                                        doxuno = 0
                                    End If

                                Case = 3
                                    If rd2("Martes").ToString = 1 Then
                                        TestStr = Format(Date.Now, "HH:mm:ss")
                                        If TestStr >= rd2("HInicioM").ToString And TestStr <= rd2("HFinM").ToString Or TestStr >= rd2("HInicioM2").ToString And TestStr <= rd2("HFinM2").ToString Then
                                            doxuno = 1
                                        Else
                                            doxuno = 0
                                        End If
                                    Else
                                        doxuno = 0
                                    End If

                                Case = 4
                                    If rd2("Miercoles").ToString = 1 Then
                                        TestStr = Format(Date.Now, "HH:mm:ss")
                                        If TestStr >= rd2("HInicioMi").ToString And TestStr <= rd2("HFinMi").ToString Or TestStr >= rd2("HInicioMi2").ToString And TestStr <= rd2("HFinMi2").ToString Then
                                            doxuno = 1
                                        Else
                                            doxuno = 0
                                        End If
                                    Else
                                        doxuno = 0
                                    End If

                                Case = 5
                                    If rd2("Jueves").ToString = 1 Then
                                        TestStr = Format(Date.Now, "HH:mm:ss")
                                        If TestStr >= rd2("HInicioJ").ToString And TestStr <= rd2("HFinJ").ToString Or TestStr >= rd2("HInicioJ2").ToString And TestStr <= rd2("HFinJ2").ToString Then
                                            doxuno = 1
                                        Else
                                            doxuno = 0
                                        End If
                                    Else
                                        doxuno = 0
                                    End If

                                Case = 6
                                    If rd2("Viernes").ToString = 1 Then
                                        TestStr = Format(Date.Now, "HH:mm:ss")
                                        If TestStr >= rd2("HInicioV").ToString And TestStr <= rd2("HFinV").ToString Or TestStr >= rd2("HInicioV2").ToString And TestStr <= rd2("HFinV2").ToString Then
                                            doxuno = 1
                                        Else
                                            doxuno = 0
                                        End If
                                    Else
                                        doxuno = 0
                                    End If

                                Case = 7
                                    If rd2("Sabado").ToString = 1 Then
                                        TestStr = Format(Date.Now, "HH:mm:ss")
                                        If TestStr >= rd2("HInicioS").ToString And TestStr <= rd2("HFinS").ToString Or TestStr >= rd2("HInicioS2").ToString And TestStr <= rd2("HFinS2").ToString Then
                                            doxuno = 1
                                        Else
                                            doxuno = 0
                                        End If
                                    Else
                                        doxuno = 0
                                    End If

                                Case Else
                                    doxuno = 0
                            End Select
                        End If
                    Else
                        doxuno = 0
                    End If
                End If
                rd2.Close()
                cnn2.Close()

                With grdCaptura.Rows
                    .Add(CodigoProducto, CodigoProducto & vbNewLine & descripcion, FormatNumber(cantidad, 2), FormatNumber(PU, 2), FormatNumber(Importe, 2), "", "")

                    lblTotalVenta.Text = lblTotalVenta.Text + Importe
                    lblTotalVenta.Text = FormatNumber(lblTotalVenta.Text, 2)
                End With

                With Me.grdCaptura
                    For ii As Integer = 0 To grdCaptura.Rows.Count - 1
                        grdCaptura.Rows.Add(CodigoProducto, CodigoProducto & vbNewLine & descripcion, FormatNumber(cantidad, 2), FormatNumber(0, 2), FormatNumber(0, 2), "", "")
                    Next
                End With
                Exit Sub
            End If

            '3x2
            If tresxdos = 1 Then

                cnn2.Close() : cnn2.Open()
                cmd2 = cnn2.CreateCommand
                cmd2.CommandText = "SELECT Promo3x2,Domingo2,HInicioD3,HFinD3,HInicioD33,HFinD33,Lunes2,HInicioL3,HFinL3,HInicioL33,HFinL33,Martes2,HInicioM3,HFinM3,HInicioM33,HFinM33,Miercoles2,HInicioMi3,HFinMi3,HInicioMi33,HFinMi33,Jueves2,HInicioJ3,HFinJ3,HInicioJ33,HFinJ33,Viernes2,HInicioV3,HFinV3,HInicioV33,HFinV33,Sabado2,HInicioS3,HFinS3,HInicioS33,HFinS33 FROM promos WHERE Codigo='" & CodigoProducto & "' AND Promo3x2=1"
                rd2 = cmd2.ExecuteReader
                If rd2.HasRows Then
                    If rd2.Read Then
                        If rd2("Promo3x2").ToString = 1 Then
                            dia = Weekday(Date.Now)
                            Select Case dia
                                Case = 1
                                    If rd2("Domingo2").ToString = 1 Then
                                        TestStr = Format(Date.Now, "HH:mm:ss")
                                        If TestStr >= rd2("HInicioD3").ToString And TestStr <= rd2("HFinD3").ToString Or TestStr >= rd2("HInicioD33").ToString And TestStr <= rd2("HFinD33").ToString Then
                                            tresxdos = 1
                                        Else
                                            tresxdos = 0
                                        End If
                                    Else
                                        tresxdos = 0
                                    End If

                                Case = 2
                                    If rd2("Lunes2").ToString = 1 Then
                                        TestStr = Format(Date.Now, "HH:mm:ss")
                                        If TestStr >= rd2("HInicioL3").ToString And TestStr <= rd2("HFinL3").ToString Or TestStr >= rd2("HInicioL33").ToString And TestStr <= rd2("HFinL33").ToString Then
                                            tresxdos = 1
                                        Else
                                            tresxdos = 0
                                        End If
                                    Else
                                        tresxdos = 0
                                    End If

                                Case = 3
                                    If rd2("Martes2").ToString = 1 Then
                                        TestStr = Format(Date.Now, "HH:mm:ss")
                                        If TestStr >= rd2("HInicioM3").ToString And TestStr <= rd2("HFinM3").ToString Or TestStr >= rd2("HInicioM33").ToString And TestStr <= rd2("HFinM33").ToString Then
                                            tresxdos = 1
                                        Else
                                            tresxdos = 0
                                        End If
                                    Else
                                        tresxdos = 0
                                    End If

                                Case = 4
                                    If rd2("Miercoles2").ToString = 1 Then
                                        TestStr = Format(Date.Now, "HH:mm:ss")
                                        If TestStr >= rd2("HInicioMi3").ToString And TestStr <= rd2("HFinMi3").ToString Or TestStr >= rd2("HInicioMi33").ToString And TestStr <= rd2("HFinMi33").ToString Then
                                            tresxdos = 1
                                        Else
                                            tresxdos = 0
                                        End If
                                    Else
                                        tresxdos = 0
                                    End If

                                Case = 5
                                    If rd2("Jueves2").ToString = 1 Then
                                        TestStr = Format(Date.Now, "HH:mm:ss")
                                        If TestStr >= rd2("HInicioJ3").ToString And TestStr <= rd2("HFinJ3").ToString Or TestStr >= rd2("HInicioJ33").ToString And TestStr <= rd2("HFinJ33").ToString Then
                                            tresxdos = 1
                                        Else
                                            tresxdos = 0
                                        End If
                                    Else
                                        tresxdos = 0
                                    End If

                                Case = 6
                                    If rd2("Viernes2").ToString = 1 Then
                                        TestStr = Format(Date.Now, "HH:mm:ss")
                                        If TestStr >= rd2("HInicioV3").ToString And TestStr <= rd2("HFinV3").ToString Or TestStr >= rd2("HInicioV33").ToString And TestStr <= rd2("HFinV33").ToString Then
                                            tresxdos = 1
                                        Else
                                            tresxdos = 0
                                        End If
                                    Else
                                        tresxdos = 0
                                    End If

                                Case = 7
                                    If rd2("Sabado2").ToString = 1 Then
                                        TestStr = Format(Date.Now, "HH:mm:ss")
                                        If TestStr >= rd2("HInicioS3").ToString And TestStr <= rd2("HFinS3").ToString Or TestStr >= rd2("HInicioS33").ToString And TestStr <= rd2("HFinS33").ToString Then
                                            tresxdos = 1
                                        Else
                                            tresxdos = 0
                                        End If
                                    Else
                                        tresxdos = 0
                                    End If
                                Case Else
                                    tresxdos = 0
                            End Select
                        End If
                    Else
                        tresxdos = 0
                    End If
                Else
                    tresxdos = 0
                End If
                rd2.Close()
                cnn2.Close()

                With grdCaptura.Rows.Add(CodigoProducto, CodigoProducto & vbNewLine & descripcion, FormatNumber(cantidad, 2), FormatNumber(PU, 2), FormatNumber(Importe, 2), "", "")

                End With

                With Me.grdCaptura.Rows.Add(CodigoProducto, CodigoProducto & vbNewLine & descripcion, FormatNumber(cantidad, 2), FormatNumber(PU, 2), FormatNumber(Importe, 2), "", "")

                End With
                For q As Integer = 0 To grdCaptura.Rows.Count - 1
                    TotalVenta = TotalVenta + CDbl(grdCaptura.Rows(q).Cells(4).Value.ToString)
                    lblTotalVenta.Text = FormatNumber(TotalVenta, 2)
                Next

                With Me.grdCaptura.Rows.Add(CodigoProducto, CodigoProducto & vbNewLine & descripcion, FormatNumber(cantidad, 2), FormatNumber(0, 2), FormatNumber(0, 2), "", "")

                End With
                Exit Sub

                ''Domingo
                'If dia = 0 Then
                '    inicio1 = "HInicioD3"
                '    fin1 = "HFinD3"
                '    inicio2 = "HInicioD33"
                '    fin2 = "HFinD33"
                'End If

                ''Lunes
                'If dia = 1 Then
                '    inicio1 = "HInicioL3"
                '    fin1 = "HFinL3"
                '    inicio2 = "HInicioL33"
                '    fin2 = "HFinL33"
                'End If

                ''Martes
                'If dia = 2 Then
                '    inicio1 = "HInicioM3"
                '    fin1 = "HFinM3"
                '    inicio2 = "HInicioM33"
                '    fin2 = "HFinM33"
                'End If

                ''Miercoles
                'If dia = 3 Then
                '    inicio1 = "HInicioMi3"
                '    fin1 = "HFinMi3"
                '    inicio2 = "HInicioMi33"
                '    fin2 = "HFinMi33"
                'End If

                ''Jueves
                'If dia = 4 Then
                '    inicio1 = "HInicioJ3"
                '    fin1 = "HFinJ3"
                '    inicio2 = "HInicioJ33"
                '    fin2 = "HFinJ33"
                'End If

                ''Viernes
                'If dia = 5 Then
                '    inicio1 = "HInicioV3"
                '    fin1 = "HFinV3"
                '    inicio2 = "HInicioV33"
                '    fin2 = "HFinV33"
                'End If

                ''Sabado
                'If dia = 6 Then
                '    inicio1 = "HInicioS3"
                '    fin1 = "HFinS3"
                '    inicio2 = "HInicioS33"
                '    fin2 = "HFinS33"
                'End If

                'cnn2.Close() : cnn2.Open()

                'cmd2 = cnn2.CreateCommand
                'cmd2.CommandText =
                '    "select * from Promos where Codigo='" & CodigoProducto & "'"
                'rd2 = cmd2.ExecuteReader
                'If rd2.HasRows Then
                '    If rd2.Read Then
                '        hora_i1 = rd2(inicio1).ToString()
                '        hora_f1 = rd2(fin1).ToString()
                '        hora_i2 = rd2(inicio2).ToString()
                '        hora_f2 = rd2(fin2).ToString()
                '    End If
                'End If
                'rd2.Close() : cnn2.Close()

                ''COMENTARIO'
                ''Mayor que - >
                ''Menor que - <

                ''Primera validación
                'If CDate(hora_dia) > CDate(hora_i1) And CDate(hora_dia) < CDate(hora_f1) Then
                '    Dim canti As Double = 0

                '    'Pasa porque es el día y la hora
                '    '(1) - Primero cuenta los que ya están
                '    For p1 As Integer = 0 To grdCaptura.Rows.Count - 1
                '        If grdCaptura.Rows(p1).Cells(0).Value.ToString() = CodigoProducto Then
                '            canti = canti + grdCaptura.Rows(p1).Cells(2).Value()
                '        End If
                '    Next

                '    If canti = 0 Then
                '        grdCaptura.Rows.Add(CodigoProducto, CodigoProducto & vbNewLine & descripcion, FormatNumber(cantidad, 2), FormatNumber(PU, 2), FormatNumber(Importe, 2), 1, IIf(nompreferencia = "", "", nompreferencia), lblpromo.Text, "")

                '        lblTotalVenta.Text = lblTotalVenta.Text + Importe
                '        lblTotalVenta.Text = FormatNumber(lblTotalVenta.Text, 2)

                '        Exit Sub
                '    ElseIf canti = 1 Then
                '        For qq As Integer = 0 To grdCaptura.Rows.Count - 1
                '            If grdCaptura.Rows(qq).Cells(0).Value = CodigoProducto Then
                '                grdCaptura.Rows(qq).Cells(1).Value = CodigoProducto & vbNewLine & descripcion
                '                grdCaptura.Rows(qq).Cells(2).Value = grdCaptura.Rows(qq).Cells(2).Value.ToString + CDec(FormatNumber(cantidad, 2))
                '                grdCaptura.Rows(qq).Cells(3).Value = FormatNumber(PU, 2)
                '                grdCaptura.Rows(qq).Cells(4).Value = grdCaptura.Rows(qq).Cells(4).Value.ToString + CDec(FormatNumber(Importe, 2))
                '                grdCaptura.Rows(qq).Cells(5).Value = cantidad2
                '                grdCaptura.Rows(qq).Cells(6).Value = IIf(nompreferencia = "", "", nompreferencia)
                '            End If
                '        Next
                '        For p1 As Integer = 0 To grdCaptura.Rows.Count - 1
                '            Dim total As Double = 0
                '            If grdCaptura.Rows(p1).Cells(0).Value.ToString() = CodigoProducto Then
                '                total = total + grdCaptura.Rows(p1).Cells(4).Value()
                '            End If
                '            lblTotalVenta.Text = FormatNumber(total, 2)
                '        Next
                '        Exit Sub
                '    ElseIf canti > 1 Then
                '        Dim cantidad_final As Double = canti + cantidad
                '        Dim division As Double = 0
                '        Dim cantidad_valor As Double = 0

                '        division = FormatNumber(cantidad_final / 3.3, 0)
                '        cantidad_valor = cantidad_final - division

                '        Dim total_importe As Double = 0
                '        total_importe = PU * cantidad_valor

                '        For qq As Integer = 0 To grdCaptura.Rows.Count - 1
                '            If grdCaptura.Rows(qq).Cells(0).Value = CodigoProducto Then
                '                grdCaptura.Rows(qq).Cells(1).Value = CodigoProducto & vbNewLine & descripcion
                '                grdCaptura.Rows(qq).Cells(2).Value = grdCaptura.Rows(qq).Cells(2).Value.ToString + CDec(FormatNumber(cantidad, 2))
                '                grdCaptura.Rows(qq).Cells(3).Value = FormatNumber(PU, 2)
                '                grdCaptura.Rows(qq).Cells(4).Value = CDec(FormatNumber(total_importe, 2))
                '                grdCaptura.Rows(qq).Cells(5).Value = cantidad2
                '                grdCaptura.Rows(qq).Cells(6).Value = IIf(nompreferencia = "", "", nompreferencia)
                '            End If
                '        Next

                '        For p1 As Integer = 0 To grdCaptura.Rows.Count - 1
                '            Dim total As Double = 0
                '            If grdCaptura.Rows(p1).Cells(0).Value.ToString() = CodigoProducto Then
                '                total = total + grdCaptura.Rows(p1).Cells(4).Value()
                '            End If
                '            lblTotalVenta.Text = FormatNumber(total, 2)
                '        Next
                '        Exit Sub
                '    End If
                'End If

                ''Segunda validación
                'If CDate(hora_dia) > CDate(hora_i1) And CDate(hora_dia) < CDate(hora_f1) Then
                '    Dim canti As Double = 0

                '    'Pasa porque es el día y la hora
                '    '(1) - Primero cuenta los que ya están
                '    For p1 As Integer = 0 To grdCaptura.Rows.Count - 1
                '        If grdCaptura.Rows(p1).Cells(0).Value.ToString() = CodigoProducto Then
                '            canti = canti + grdCaptura.Rows(p1).Cells(2).Value()
                '        End If
                '    Next

                '    If canti = 0 Then
                '        grdCaptura.Rows.Add(CodigoProducto, CodigoProducto & vbNewLine & descripcion, FormatNumber(cantidad, 2), FormatNumber(PU, 2), FormatNumber(Importe, 2), 1, IIf(nompreferencia = "", "", nompreferencia), lblpromo.Text, "")

                '        lblTotalVenta.Text = lblTotalVenta.Text + Importe
                '        lblTotalVenta.Text = FormatNumber(lblTotalVenta.Text, 2)

                '        Exit Sub
                '    ElseIf canti = 1 Then
                '        For qq As Integer = 0 To grdCaptura.Rows.Count - 1
                '            If grdCaptura.Rows(qq).Cells(0).Value = CodigoProducto Then
                '                grdCaptura.Rows(qq).Cells(1).Value = CodigoProducto & vbNewLine & descripcion
                '                grdCaptura.Rows(qq).Cells(2).Value = grdCaptura.Rows(qq).Cells(2).Value.ToString + CDec(FormatNumber(cantidad, 2))
                '                grdCaptura.Rows(qq).Cells(3).Value = FormatNumber(PU, 2)
                '                grdCaptura.Rows(qq).Cells(4).Value = grdCaptura.Rows(qq).Cells(4).Value.ToString + CDec(FormatNumber(Importe, 2))
                '                grdCaptura.Rows(qq).Cells(5).Value = cantidad2
                '                grdCaptura.Rows(qq).Cells(6).Value = IIf(nompreferencia = "", "", nompreferencia)
                '            End If
                '        Next
                '        For p1 As Integer = 0 To grdCaptura.Rows.Count - 1
                '            Dim total As Double = 0
                '            If grdCaptura.Rows(p1).Cells(0).Value.ToString() = CodigoProducto Then
                '                total = total + grdCaptura.Rows(p1).Cells(4).Value()
                '            End If
                '            lblTotalVenta.Text = FormatNumber(total, 2)
                '        Next
                '        Exit Sub
                '    ElseIf canti > 1 Then
                '        Dim cantidad_final As Double = canti + cantidad
                '        Dim division As Double = 0
                '        Dim cantidad_valor As Double = 0

                '        division = FormatNumber(cantidad_final / 3.3, 0)
                '        cantidad_valor = cantidad_final - division

                '        Dim total_importe As Double = 0
                '        total_importe = PU * cantidad_valor

                '        For qq As Integer = 0 To grdCaptura.Rows.Count - 1
                '            If grdCaptura.Rows(qq).Cells(0).Value = CodigoProducto Then
                '                grdCaptura.Rows(qq).Cells(1).Value = CodigoProducto & vbNewLine & descripcion
                '                grdCaptura.Rows(qq).Cells(2).Value = grdCaptura.Rows(qq).Cells(2).Value.ToString + CDec(FormatNumber(cantidad, 2))
                '                grdCaptura.Rows(qq).Cells(3).Value = FormatNumber(PU, 2)
                '                grdCaptura.Rows(qq).Cells(4).Value = CDec(FormatNumber(total_importe, 2))
                '                grdCaptura.Rows(qq).Cells(5).Value = cantidad2
                '                grdCaptura.Rows(qq).Cells(6).Value = IIf(nompreferencia = "", "", nompreferencia)
                '            End If
                '        Next

                '        For p1 As Integer = 0 To grdCaptura.Rows.Count - 1
                '            Dim total As Double = 0
                '            If grdCaptura.Rows(p1).Cells(0).Value.ToString() = CodigoProducto Then
                '                total = total + grdCaptura.Rows(p1).Cells(4).Value()
                '            End If
                '            lblTotalVenta.Text = FormatNumber(total, 2)
                '        Next
                '        Exit Sub
                '    End If
                'End If
            End If

            With Me.grdCaptura

                Dim banderaentraa As Integer = 0
                banderaentraa = 0



                If acumula = 1 Then
                    For dx As Integer = 0 To grdCaptura.Rows.Count - 1
                        If CodigoProducto = grdCaptura.Rows(dx).Cells(0).Value.ToString Then
                            grdCaptura.Rows(dx).Cells(2).Value = cantidad + CDbl(grdCaptura.Rows(dx).Cells(2).Value.ToString)
                            grdCaptura.Rows(dx).Cells(4).Value = FormatNumber(Importe + CDbl(grdCaptura.Rows(dx).Cells(4).Value.ToString), 2)

                            lblTotalVenta.Text = lblTotalVenta.Text + Importe
                            lblTotalVenta.Text = FormatNumber(lblTotalVenta.Text, 2)

                            GoTo deku
                        End If
                    Next

                    .Rows.Add(CodigoProducto, CodigoProducto & vbNewLine & descripcion, FormatNumber(cantidad, 2), FormatNumber(PU, 2), FormatNumber(Importe, 2), 1, IIf(nompreferencia = "", "", nompreferencia), lblpromo.Text, "", 0)

                    lblTotalVenta.Text = lblTotalVenta.Text + Importe
                    lblTotalVenta.Text = FormatNumber(lblTotalVenta.Text, 2)

                Else
                    .Rows.Add(CodigoProducto, CodigoProducto & vbNewLine & descripcion, FormatNumber(cantidad, 2), FormatNumber(PU, 2), FormatNumber(Importe, 2), 1, IIf(nompreferencia = "", "", nompreferencia), lblpromo.Text, "", 0)

                    lblTotalVenta.Text = lblTotalVenta.Text + Importe
                    lblTotalVenta.Text = FormatNumber(lblTotalVenta.Text, 2)


                End If

deku:
            End With
        End If
    End Sub

    Private Sub Tiempos(ByVal min As Integer)
        Dim tiempo As Integer = 0


        For zi = 0 To grdCaptura.Rows.Count - 1
            grdCaptura.Rows(zi).Cells(7).Value = min
        Next
        If min = 5 Then
            grdCaptura.Rows.Add("--------------------", "Primer Tiempo", "0", "0", "0", "", "", "", "")
        ElseIf min = 10 Then
            grdCaptura.Rows.Add("--------------------", "Segundo Tiempo", "0", "0", "0", "", "", "", "")
        ElseIf min = 15 Then
            grdCaptura.Rows.Add("--------------------", "Tercer Tiempo", "0", "0", "0", "", "", "", "")
        End If


    End Sub

    Public Sub EnviarComanda()

        Dim MinutosTiempo As Integer = 0
        Dim HrTiempo As String = ""
        Dim HrEntrega As String = ""
        Dim drawLine As String = ""

        FolioVenta = Trim(lblFolio.Text)


        Dim IVAPRODUCTO As Double = 0
        Dim iva As Double = 0
        Dim totaliva As Double = 0

        Dim cnn1 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
        Dim cnn2 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
        Dim cnn3 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
        Dim cnn4 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
        Dim rd1, rd2, rd3, rd4 As MySqlDataReader
        Dim cmd1, cmd2, cmd3, cmd4 As MySqlCommand

        For n As Integer = 0 To grdCaptura.Rows.Count - 1
            If grdCaptura.Rows(n).Cells(0).Value <> "" Then

                'If grdCaptura.Rows(n).Cells(4).Value.ToString = "" Then
                '    Continue For
                'End If

                Prods = Split(grdCaptura.Rows(n).Cells(0).Value.ToString, vbCrLf)
                CodigoProducto = Prods(0)

                If CodigoProducto = "--------------------" Then
                    Continue For
                End If

                Dim TOTAL As Double = grdCaptura.Rows(n).Cells(4).Value.ToString

                If CodigoProducto <> "WXYZ" Then
                    cnn1.Close() : cnn1.Open()
                    cmd1 = cnn1.CreateCommand
                    cmd1.CommandText = "select IVA from Productos where Codigo='" & CodigoProducto & "'"
                    rd1 = cmd1.ExecuteReader
                    If rd1.Read Then
                        If grdCaptura.Rows(n).Cells(3).Value.ToString <> "" Then

                            If rd1(0).ToString > 0 Then
                                IVAPRODUCTO = CDbl(TOTAL) / (1 + rd1(0).ToString)
                                iva = CDbl(TOTAL) - CDbl(IVAPRODUCTO)
                                totaliva = totaliva + CDbl(iva)
                            End If

                        End If
                    End If
                    rd1.Close()
                    cnn1.Close()
                End If
            End If
        Next n
        totaliva = FormatNumber(totaliva, 2)

        If grdCaptura.Rows.Count < 1 Then
            Exit Sub
        End If

        If MsgBox("Desea enviar a producccion", vbInformation + vbOKCancel, titulomensajes) = vbCancel Then
            Exit Sub
        End If

        Dim mysubtotal As Double = 0
        Dim mytotalventa As Double = 0

        mytotalventa = lblTotalVenta.Text
        mysubtotal = CDbl(mytotalventa) - CDbl(totaliva)

        cnn1.Close() : cnn1.Open()
        cmd1 = cnn1.CreateCommand
        cmd1.CommandText = "INSERT INTO hismesa(Mesa,Mesero,Fabierto,HAbierto,FA,FCerrado,HCerrado,FH,Status) VALUES('" & lblmesa.Text & "','" & lblatiende.Text & "','" & Format(Date.Now, "yyyy-MM-dd") & "','" & Format(Date.Now, "HH:mm:ss") & "','" & Format(Date.Now, "yyyy-MM-dd HH:mm:ss") & "','','','" & Format(Date.Now, "yyyy-MM-dd HH:mm:ss") & "',0)"
        cmd1.ExecuteNonQuery()

        cmd1 = cnn1.CreateCommand
        cmd1.CommandText = "INSERT INTO Comanda1(Nombre,Subtotal,IVA,Totales,Resta,TComensales,IdCliente,Direccion,Usuario,FVenta,HVenta,FPago,FCancelado,Status,Comisionista) VALUES('" & lblmesa.Text & "'," & mysubtotal & "," & totaliva & "," & mytotalventa & "," & mytotalventa & "," & CInt(lblNcomensales.Text) & ",'','','" & lblatiende.Text & "','" & Format(Date.Now, "yyyy/MM/dd") & "','" & Format(Date.Now, "yyyy/MM/dd HH:mm:ss") & "','','','','')"
        cmd1.ExecuteNonQuery()
        cnn1.Close()

        Dim CFOLIO As Integer = 0
        Dim FOLIO1 As String = ""
        Dim MYCODE As String = ""
        Dim MyDesc As String = ""
        Dim MYCANT As Double = 0
        Dim MyPrecio As Double = 0
        Dim MyTotal As Double = 0
        Dim comensal As String = ""
        Dim comentario As String = ""
        Dim UVenta As String = ""

        Dim MYIVA As Double = 0
        Dim MyPrecioSin As Double = 0
        Dim MyTotalSin As Double = 0

        Dim MyCostVUE As Double = 0
        Dim MyProm As Double = 0
        Dim MyDepto As String = ""
        Dim MyGrupo As String = ""

        Dim MYMCD As Double = 0
        Dim MyMult2 As Double = 0
        Dim MyPRIN As String = ""

        Dim MyExInv As Double = 0
        Dim MyPrecioCompra As Double = 0
        Dim MyMultiplo As Double = 0

        For zi As Integer = 0 To grdCaptura.Rows.Count - 1

            If zi < grdCaptura.Rows.Count Then

                If grdCaptura.Rows(zi).Cells(0).Value = "--------------------" Then
                    drawLine = grdCaptura.Rows(zi).Cells(1).Value.ToString
                Else

                    FOLIO1 = Trim(lblmesa.Text)
                    MYCODE = grdCaptura.Rows(zi).Cells(0).Value.ToString
                    Prods = Split(grdCaptura.Rows(zi).Cells(1).Value.ToString, vbCrLf)
                    MyDesc = Prods(1)
                    MYCANT = grdCaptura.Rows(zi).Cells(2).Value.ToString
                    MyPrecio = grdCaptura.Rows(zi).Cells(3).Value.ToString
                    MyTotal = grdCaptura.Rows(zi).Cells(4).Value.ToString
                    comensal = grdCaptura.Rows(zi).Cells(5).Value.ToString
                    comentario = grdCaptura.Rows(zi).Cells(8).Value
                    MinutosTiempo = IIf(grdCaptura.Rows(zi).Cells(7).Value = 0, 0, grdCaptura.Rows(zi).Cells(7).Value)
                    MyPrecio = FormatNumber(MyPrecio, 2)
                    MyTotal = FormatNumber(MyTotal, 2)

                    If MYCODE <> "WXYZ" Then

                        cnn2.Close() : cnn2.Open()
                        cmd2 = cnn2.CreateCommand
                        cmd2.CommandText = "select IVA from Productos where Codigo='" & MYCODE & "'"
                        rd2 = cmd2.ExecuteReader
                        If rd2.Read Then

                            MYIVA = rd2(0).ToString
                            MyPrecioSin = IIf(MyPrecio = 0, 0, MyPrecio) / (1 + MYIVA)
                            MyTotalSin = IIf(MyTotal = 0, 0, MyTotal) / (1 + MYIVA)
                            MyPrecioSin = FormatNumber(MyPrecioSin, 2)
                        End If
                        rd2.Close()

                        cmd2 = cnn2.CreateCommand
                        cmd2.CommandText = "SELECT Departamento,Grupo,MCD,Multiplo,Departamento,Grupo,GPrint,UVenta FROM Productos where Codigo='" & MYCODE & "'"
                        rd2 = cmd2.ExecuteReader
                        If rd2.HasRows Then
                            If rd2.Read Then
                                If rd2("Departamento").ToString = "SERVICIOS" Then
                                    MyCostVUE = 0
                                    MyProm = 0
                                    MyDepto = rd2("Departamento").ToString
                                    MyGrupo = rd2("Grupo").ToString

                                End If
                                MYMCD = rd2("MCD").ToString
                                MyMult2 = rd2("Multiplo").ToString
                                MyDepto = rd2("Departamento").ToString
                                MyGrupo = rd2("Grupo").ToString
                                MyPRIN = rd2("GPrint").ToString
                                UVenta = rd2("UVenta").ToString
                            End If
                        End If
                        rd2.Close()

                        cmd2 = cnn2.CreateCommand
                        cmd2.CommandText = "SELECT Existencia,Multiplo,Departamento,PrecioCompra FROM Productos where Codigo='" & Strings.Left(MYCODE, 7) & "'"
                        rd2 = cmd2.ExecuteReader
                        If rd2.HasRows Then
                            If rd2.Read Then
                                MyExInv = rd2("Existencia").ToString / rd2("Multiplo").ToString
                                MyMultiplo = rd2("Multiplo").ToString

                                If rd2("Departamento").ToString <> "SERVICIOS" Then
                                    MyPrecioCompra = rd2("PrecioCompra").ToString
                                    MyCostVUE = (MyPrecioCompra) * (MYCANT / MYMCD)
                                End If
                            End If
                        End If
                        rd2.Close()
                        cnn2.Close()

                    End If


                    If grdCaptura.Rows(zi).Cells(0).Value.ToString <> "" Then

                        cnn3.Close() : cnn3.Open()
                        cmd3 = cnn3.CreateCommand
                        cmd3.CommandText = "select MAX(Id) from Comanda1"
                        rd3 = cmd3.ExecuteReader
                        If rd3.HasRows Then
                            If rd3.Read Then
                                CFOLIO = IIf(rd3(0).ToString = "", 0, rd3(0).ToString)
                                lblFolio.Text = CFOLIO
                            End If
                        Else
                            CFOLIO = 0
                            lblFolio.Text = CFOLIO
                        End If
                        rd3.Close()

                        If MinutosTiempo = 0 Then
                            HrTiempo = Format(Date.Now, "HH:mm:ss")
                            HrEntrega = Format(Date.Now, "HH:mm:ss")
                        ElseIf MinutosTiempo > 0 Then
                            HrTiempo = Format(Date.Now, "HH:mm:ss")
                            HrEntrega = Format(DateAdd("n", MinutosTiempo, Date.Now), "HH:mm:ss")
                        End If
                        cnn3.Close()

                        If MYCODE <> "WXYZ" Then
                            cnn1.Close() : cnn1.Open()
                            cmd1 = cnn1.CreateCommand
                            cmd1.CommandText = "insert into Comandas(Id,NMESA,Codigo,Nombre,Cantidad,UVenta,CostVUE,CostVP,Precio,Total,PrecioSinIVA,TotalSinIVA,Comisionista,Fecha,Depto,Comensal,Status,Comentario,GPrint,CUsuario,Total_comensales,Grupo,EstatusT,Hr,EntregaT) values(" & lblFolio.Text & ",'" & FOLIO1 & "','" & MYCODE & "','" & MyDesc & "'," & MYCANT & ",'" & UVenta & "'," & MyCostVUE & "," & IIf(MyProm = 0, 0, MyProm) & "," & MyPrecio & "," & MyTotal & "," & MyPrecioSin & "," & MyTotalSin & ",'" & drawLine & "','" & Format(Date.Now, "yyyy/MM/dd") & "','" & MyDepto & "','" & comensal & "','RESTA','" & comentario & "','" & MyPRIN & "','" & Trim(lblatiende.Text) & "','" & Trim(lblNcomensales.Text) & "','" & MyGrupo & "',0,'" & HrTiempo & "','" & HrEntrega & "')"
                            cmd1.ExecuteNonQuery()
                            cnn1.Close()
                        End If


                        If MYCODE = "WXYZ" Then
                            cnn1.Close() : cnn1.Open()
                            cmd1 = cnn1.CreateCommand
                            cmd1.CommandText = "insert into Comandas(Id,NMESA,Codigo,Nombre,Cantidad,UVenta,CostVUE,CostVP,Precio,Total,PrecioSinIVA,TotalSinIVA,Comisionista,Fecha,Depto,Comensal,Status,Comentario,GPrint,CUsuario,Total_comensales,Grupo,EstatusT,Hr,EntregaT) values(" & lblFolio.Text & ",'" & FOLIO1 & "','" & MYCODE & "','" & MyDesc & "'," & MYCANT & ",'PZA',0,0," & MyPrecio & "," & MyTotal & "," & MyPrecio & "," & MyTotal & ",'" & drawLine & "','" & Format(Date.Now, "yyyy/MM/dd") & "','UNICO','" & comensal & "','RESTA','" & comentario & "','UNICO','" & Trim(lblatiende.Text) & "','" & Trim(lblNcomensales.Text) & "','UNICO',0,'" & HrTiempo & "','" & HrEntrega & "')"
                            cmd1.ExecuteNonQuery()
                            cnn1.Close()
                        End If

                        If MYCODE <> "WXYZ" Then
                            cnn1.Close() : cnn1.Open()
                            cmd1 = cnn1.CreateCommand
                            cmd1.CommandText = "INSERT INTO Rep_Comandas(Id,NMESA,Codigo,Nombre,Cantidad,Uventa,CostVUE,CostVP,Precio,Total,PrecioSinIVA,TotalSinIVA,Comisionista,Fecha,Depto,Comensal,Status,Comentario,GPrint,CUsuario,Total_comensales,Grupo,EstatusT,Hr,EntregaT) VALUES(" & lblFolio.Text & ",'" & FOLIO1 & "','" & MYCODE & "','" & MyDesc & "'," & MYCANT & ",'" & UVenta & "'," & MyCostVUE & "," & IIf(MyProm = 0, 0, MyProm) & "," & MyPrecio & "," & MyTotal & "," & MyPrecioSin & "," & MyTotalSin & ",'" & drawLine & "','" & Format(Date.Now, "yyyy/MM/dd") & "','" & MyDepto & "','" & comensal & "','ORDENADA','" & comentario & "','" & MyPRIN & "','" & Trim(lblatiende.Text) & "','" & Trim(lblNcomensales.Text) & "','" & MyGrupo & "',0,'" & HrTiempo & "','" & HrEntrega & "')"
                            cmd1.ExecuteNonQuery()
                            cnn1.Close()
                        End If


                        If MYCODE = "WXYZ" Then
                            cnn1.Close() : cnn1.Open()
                            cmd1 = cnn1.CreateCommand
                            cmd1.CommandText = "INSERT INTO Rep_Comandas(Id,NMESA,Codigo,Nombre,Cantidad,Uventa,CostVUE,CostVP,Precio,Total,PrecioSinIVA,TotalSinIVA,Comisionista,Fecha,Depto,Comensal,Status,Comentario,GPrint,CUsuario,Total_comensales,Grupo,EstatusT,Hr,EntregaT) VALUES(" & lblFolio.Text & ",'" & FOLIO1 & "','" & MYCODE & "','" & MyDesc & "'," & MYCANT & ",'" & UVenta & "',0,0," & MyPrecio & "," & MyTotal & "," & MyPrecio & "," & MyTotal & ",'" & drawLine & "','" & Format(Date.Now, "yyyy/MM/dd") & "','UNICO','" & comensal & "','ORDENADA','" & comentario & "','" & DatosRecarga("PRINT_PRED") & "','" & Trim(lblatiende.Text) & "','" & Trim(lblNcomensales.Text) & "','UNICO',0,'" & HrTiempo & "','" & HrEntrega & "')"
                            cmd1.ExecuteNonQuery()
                            cnn1.Close()
                        End If

                    End If
                    'If drawLine <> "" Then
                    '    zi = zi + 1
                    '    drawLine = ""
                    'End If
                End If
            End If
        Next zi

        MyFolio = lblFolio.Text

        Dim tamimpre As Integer = 0
        Dim impresoracomanda As String = ""

        cnn4.Close() : cnn4.Open()
        cnn3.Close() : cnn3.Open()

        cmd4 = cnn4.CreateCommand
        cmd4.CommandText = "SELECT NotasCred FROM Formatos WHERE Facturas='TamImpre'"
        rd4 = cmd4.ExecuteReader
        If rd4.HasRows Then
            If rd4.Read Then
                tamimpre = rd4(0).ToString
            End If
        End If
        rd4.Close()

        cmd4 = cnn4.CreateCommand
        cmd4.CommandText = "SELECT DISTINCT GPrint FROM Comandas WHERE Nmesa='" & lblmesa.Text & "' AND Id=" & lblFolio.Text
        rd4 = cmd4.ExecuteReader
        Do While rd4.Read
            If rd4.HasRows Then
                impre = rd4(0).ToString

                cmd3 = cnn3.CreateCommand
                cmd3.CommandText = "SELECT Impresora from rutasimpresion where Tipo='" & impre & "' AND Equipo='" & ObtenerNombreEquipo() & "'"
                rd3 = cmd3.ExecuteReader
                If rd3.HasRows Then
                    If rd3.Read Then
                        impresoracomanda = rd3(0).ToString
                    End If
                End If
                rd3.Close()

                If impresoracomanda = "" Then
                Else
                    If tamimpre = 80 Then
                        PComanda80.DefaultPageSettings.PrinterSettings.PrinterName = impresoracomanda
                        If PComanda80.DefaultPageSettings.PrinterSettings.PrinterName = impresoracomanda Then
                            PComanda80.Print()
                        Else
                            MsgBox("La impresora no esta configurada", vbInformation + vbOKOnly, titulorestaurante)
                            GoTo SAFO
                        End If
                    End If

                    If tamimpre = 58 Then
                        If PComanda58.DefaultPageSettings.PrinterSettings.PrinterName = impresoracomanda Then
                            PComanda58.Print()
                        Else
                            GoTo SAFO
                        End If
                    End If
                End If

            End If
        Loop
        rd4.Close()
        cnn4.Close()
        cnn3.Close()
SAFO:
        '  BanderaIvaDescuento = 0
        btnnuevo.PerformClick()
        lblFolio.Text = MyFolio
        Me.Close()
        frmMesas.Show()


    End Sub

#Region "teclado"

    Private Sub btn1_Click(sender As Object, e As EventArgs) Handles btn1.Click
        txtRespuesta.Text = txtRespuesta.Text + btn1.Text
    End Sub

    Private Sub btn2_Click(sender As Object, e As EventArgs) Handles btn2.Click
        txtRespuesta.Text = txtRespuesta.Text + btn2.Text
    End Sub

    Private Sub btn3_Click(sender As Object, e As EventArgs) Handles btn3.Click
        txtRespuesta.Text = txtRespuesta.Text + btn3.Text
    End Sub

    Private Sub btn4_Click(sender As Object, e As EventArgs) Handles btn4.Click
        txtRespuesta.Text = txtRespuesta.Text + btn4.Text
    End Sub

    Private Sub btn5_Click(sender As Object, e As EventArgs) Handles btn5.Click
        txtRespuesta.Text = txtRespuesta.Text + btn5.Text
    End Sub

    Private Sub btnpunto_Click(sender As Object, e As EventArgs) Handles btnpunto.Click
        txtRespuesta.Text = txtRespuesta.Text + btnpunto.Text
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        pteclado.Visible = False
    End Sub

    Private Sub btn6_Click(sender As Object, e As EventArgs) Handles btn6.Click
        txtRespuesta.Text = txtRespuesta.Text + btn6.Text
    End Sub

    Private Sub btn7_Click(sender As Object, e As EventArgs) Handles btn7.Click
        txtRespuesta.Text = txtRespuesta.Text + btn7.Text
    End Sub

    Private Sub btn8_Click(sender As Object, e As EventArgs) Handles btn8.Click
        txtRespuesta.Text = txtRespuesta.Text + btn8.Text
    End Sub

    Private Sub btn9_Click(sender As Object, e As EventArgs) Handles btn9.Click
        txtRespuesta.Text = txtRespuesta.Text + btn9.Text
    End Sub

    Private Sub btn0_Click(sender As Object, e As EventArgs) Handles btn0.Click
        txtRespuesta.Text = txtRespuesta.Text + btn0.Text
    End Sub

    Private Sub btnborrar_Click(sender As Object, e As EventArgs) Handles btnborrar.Click
        txtRespuesta.Text = ""
    End Sub

    Private Sub btnaceptar_Click(sender As Object, e As EventArgs) Handles btnaceptar.Click
        txtRespuesta_KeyPress(txtRespuesta, New KeyPressEventArgs(ControlChars.Cr))
    End Sub

    Private Sub btnQ_Click(sender As Object, e As EventArgs) Handles btnQ.Click
        txtRespuesta.Text = txtRespuesta.Text + btnQ.Text
    End Sub

    Private Sub btnw_Click(sender As Object, e As EventArgs) Handles btnw.Click
        txtRespuesta.Text = txtRespuesta.Text + btnw.Text
    End Sub

    Private Sub btnE_Click(sender As Object, e As EventArgs) Handles btnE.Click
        txtRespuesta.Text = txtRespuesta.Text + btnE.Text
    End Sub

    Private Sub btnR_Click(sender As Object, e As EventArgs) Handles btnR.Click
        txtRespuesta.Text = txtRespuesta.Text + btnR.Text
    End Sub

    Private Sub btnT_Click(sender As Object, e As EventArgs) Handles btnT.Click
        txtRespuesta.Text = txtRespuesta.Text + btnT.Text
    End Sub

    Private Sub btnY_Click(sender As Object, e As EventArgs) Handles btnY.Click
        txtRespuesta.Text = txtRespuesta.Text + btnY.Text
    End Sub

    Private Sub btnU_Click(sender As Object, e As EventArgs) Handles btnU.Click
        txtRespuesta.Text = txtRespuesta.Text + btnU.Text
    End Sub

    Private Sub btnI_Click(sender As Object, e As EventArgs) Handles btnI.Click
        txtRespuesta.Text = txtRespuesta.Text + btnI.Text
    End Sub

    Private Sub btnO_Click(sender As Object, e As EventArgs) Handles btnO.Click
        txtRespuesta.Text = txtRespuesta.Text + btnO.Text
    End Sub

    Private Sub btnP_Click(sender As Object, e As EventArgs) Handles btnP.Click
        txtRespuesta.Text = txtRespuesta.Text + btnP.Text
    End Sub

    Private Sub btnA_Click(sender As Object, e As EventArgs) Handles btnA.Click
        txtRespuesta.Text = txtRespuesta.Text + btnA.Text
    End Sub

    Private Sub btnS_Click(sender As Object, e As EventArgs) Handles btnS.Click
        txtRespuesta.Text = txtRespuesta.Text + btnS.Text
    End Sub

    Private Sub btnD_Click(sender As Object, e As EventArgs) Handles btnD.Click
        txtRespuesta.Text = txtRespuesta.Text + btnD.Text
    End Sub

    Private Sub btnF_Click(sender As Object, e As EventArgs) Handles btnF.Click
        txtRespuesta.Text = txtRespuesta.Text + btnF.Text
    End Sub

    Private Sub btnG_Click(sender As Object, e As EventArgs) Handles btnG.Click
        txtRespuesta.Text = txtRespuesta.Text + btnG.Text
    End Sub

    Private Sub btnH_Click(sender As Object, e As EventArgs) Handles btnH.Click
        txtRespuesta.Text = txtRespuesta.Text + btnH.Text
    End Sub

    Private Sub btnJ_Click(sender As Object, e As EventArgs) Handles btnJ.Click
        txtRespuesta.Text = txtRespuesta.Text + btnJ.Text
    End Sub

    Private Sub btnK_Click(sender As Object, e As EventArgs) Handles btnK.Click
        txtRespuesta.Text = txtRespuesta.Text + btnK.Text
    End Sub

    Private Sub btnL_Click(sender As Object, e As EventArgs) Handles btnL.Click
        txtRespuesta.Text = txtRespuesta.Text + btnL.Text
    End Sub

    Private Sub btnÑ_Click(sender As Object, e As EventArgs) Handles btnÑ.Click
        txtRespuesta.Text = txtRespuesta.Text + btnÑ.Text
    End Sub

    Private Sub btnZ_Click(sender As Object, e As EventArgs) Handles btnZ.Click
        txtRespuesta.Text = txtRespuesta.Text + btnZ.Text
    End Sub

    Private Sub btnX_Click(sender As Object, e As EventArgs) Handles btnX.Click
        txtRespuesta.Text = txtRespuesta.Text + btnX.Text
    End Sub

    Private Sub btnC_Click(sender As Object, e As EventArgs) Handles btnC.Click
        txtRespuesta.Text = txtRespuesta.Text + btnC.Text
    End Sub

    Private Sub btnV_Click(sender As Object, e As EventArgs) Handles btnV.Click
        txtRespuesta.Text = txtRespuesta.Text + btnV.Text
    End Sub

    Private Sub btnB_Click(sender As Object, e As EventArgs) Handles btnB.Click
        txtRespuesta.Text = txtRespuesta.Text + btnB.Text
    End Sub

    Private Sub btnnuevo_Click(sender As Object, e As EventArgs) Handles btnnuevo.Click

        statusPromo = 0

        grdCaptura.Rows.Clear()
        pExtras.Controls.Clear()
        pPreferencias.Controls.Clear()
        pPromociones.Controls.Clear()

        lblTotalVenta.Text = "0.00"
        LBLLETRA.Text = ""

    End Sub

    Private Sub btnN_Click(sender As Object, e As EventArgs) Handles btnN.Click
        txtRespuesta.Text = txtRespuesta.Text + btnN.Text
    End Sub

    Private Sub btnM_Click(sender As Object, e As EventArgs) Handles btnM.Click
        txtRespuesta.Text = txtRespuesta.Text + btnM.Text
    End Sub

    Private Sub btntiempo1_Click(sender As Object, e As EventArgs) Handles btntiempo1.Click
        Tiempos("5")

    End Sub

    Private Sub btntiempo2_Click(sender As Object, e As EventArgs) Handles btntiempo2.Click
        Tiempos("10")
    End Sub

    Private Sub PComanda80_PrintPage(sender As Object, e As Printing.PrintPageEventArgs) Handles PComanda80.PrintPage
        Dim tipografia As String = "Lucida Sans Typewriter"
        Dim fuente_datos As New Drawing.Font(tipografia, 10, FontStyle.Regular)
        Dim fuente_prods As New Drawing.Font(tipografia, 9, FontStyle.Regular)

        Dim sc As New StringFormat With {.Alignment = StringAlignment.Center}
        Dim sf As New StringFormat With {.Alignment = StringAlignment.Far}
        Dim pen As New Pen(Brushes.Black, 1)

        Dim Y As Double = 0

        Dim Pie1 As String = ""
        Dim pie2 As String = ""
        Dim pie3 As String = ""

        Dim SinNumCoemensal As Integer = 0

        Dim conta As Integer = 0
        Dim contband As Integer = 0
        Dim comen_sal As String = ""

        Dim cnn1 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
        Dim cnn2 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
        Dim rd1, rd2 As MySqlDataReader
        Dim cmd1, cmd2 As MySqlCommand


        Try
            cnn2.Close() : cnn2.Open()
            cmd2 = cnn2.CreateCommand
            cmd2.CommandText = "select NotasCred from Formatos where Facturas='SinNumComensal'"
            rd2 = cmd2.ExecuteReader
            If rd2.HasRows Then
                If rd2.Read Then
                    SinNumCoemensal = rd2(0).ToString
                End If
            Else
                SinNumCoemensal = 0
            End If
            rd2.Close()


            e.Graphics.DrawString("-------------------------------", New Drawing.Font(tipografia, 12, FontStyle.Regular), Brushes.Black, 1, Y)
            Y += 15

            e.Graphics.DrawString("C O M A N D A", New Drawing.Font(tipografia, 12, FontStyle.Bold), Brushes.Black, 140, Y, sc)
            Y += 12
            e.Graphics.DrawString("-------------------------------", New Drawing.Font(tipografia, 12, FontStyle.Regular), Brushes.Black, 1, Y)
            Y += 18

            e.Graphics.DrawString("Folio: " & MyFolio, fuente_datos, Brushes.Black, 285, Y, sf)
            Y += 15
            e.Graphics.DrawString("Fecha: " & FormatDateTime(Date.Now, DateFormat.ShortDate), fuente_prods, Brushes.Black, 1, Y)
            e.Graphics.DrawString("Hora: " & FormatDateTime(Date.Now, DateFormat.LongTime), fuente_prods, Brushes.Black, 285, Y, sf)
            Y += 19
            e.Graphics.DrawString("-------------------------------", New Drawing.Font(tipografia, 12, FontStyle.Regular), Brushes.Black, 1, Y)
            Y += 18

            If SinNumCoemensal = 1 Then
                e.Graphics.DrawString("MESA: " & lblmesa.Text, fuente_datos, Brushes.Black, 1, Y)
                Y += 15
            Else
                e.Graphics.DrawString("N° MESA: " & lblmesa.Text, fuente_datos, Brushes.Black, 1, Y)
                e.Graphics.DrawString("Comensales: " & lblNcomensales.Text, fuente_datos, Brushes.Black, 285, Y, sf)
                Y += 15
            End If

            e.Graphics.DrawString("-------------------------------", New Drawing.Font(tipografia, 12, FontStyle.Regular), Brushes.Black, 1, Y)
            Y += 15

            e.Graphics.DrawString("CANTIDAD", New Drawing.Font(tipografia, 15, FontStyle.Bold), Brushes.Black, 1, Y)
            e.Graphics.DrawString("PRODUCTO", New Drawing.Font(tipografia, 15, FontStyle.Bold), Brushes.Black, 280, Y, sf)
            Y += 10
            e.Graphics.DrawString("-------------------------------", New Drawing.Font(tipografia, 12, FontStyle.Regular), Brushes.Black, 1, Y)
            Y += 15

            Dim codigo As String = ""
            Dim nombre As String = ""
            Dim cantidad As Double = 0
            Dim comensall As String = ""
            Dim comentario As String = ""
            Dim tiempo As String = ""
            Dim idc As Integer = 0
            If SinNumCoemensal = 1 Then

                cnn1.Close() : cnn1.Open()
                cnn2.Close() : cnn2.Open()

                cmd1 = cnn1.CreateCommand
                cmd1.CommandText = "select IDC,Codigo,Nombre,Cantidad,Comensal,Comentario,Comisionista from Comandas WHERE GPrint='" & impre & "' AND Id=" & MyFolio & " group by Comensal,IDC,Codigo,Nombre,Cantidad,Comentario,Comisionista order by comensal"
                rd1 = cmd1.ExecuteReader
                Do While rd1.Read
                    If rd1.HasRows Then

                        codigo = rd1("Codigo").ToString
                        nombre = rd1("Nombre").ToString
                        cantidad = rd1("Cantidad").ToString
                        comensall = rd1("Comensal").ToString
                        comentario = rd1("Comentario").ToString
                        tiempo = rd1("Comisionita").ToString

                        idc = rd1("IDC").ToString


                        e.Graphics.DrawString(cantidad, New Font("Arial", 13, FontStyle.Regular), Brushes.Black, 1, Y)

                        Dim caracteresPorLinea As Integer = 20
                        Dim texto As String = nombre
                        Dim inicio As Integer = 0
                        Dim longitudTexto As Integer = texto.Length

                        While inicio < longitudTexto
                            Dim longitudBloque As Integer = Math.Min(caracteresPorLinea, longitudTexto - inicio)
                            Dim bloque As String = texto.Substring(inicio, longitudBloque)
                            e.Graphics.DrawString(bloque, New Font("Arial", 13, FontStyle.Regular), Brushes.Black, 25, Y)
                            Y += 16
                            inicio += caracteresPorLinea
                        End While
                        Y += 3
                        If comentario <> "" Then

                            Dim caracteresPorLinea1 As Integer = 27
                            Dim texto1 As String = comentario
                            Dim inicio1 As Integer = 0
                            Dim longitudTexto1 As Integer = texto1.Length

                            While inicio1 < longitudTexto1
                                Dim longitudBloque1 As Integer = Math.Min(caracteresPorLinea1, longitudTexto1 - inicio1)
                                Dim bloque1 As String = texto1.Substring(inicio, longitudBloque1)
                                e.Graphics.DrawString(bloque1, fuente_datos, Brushes.Black, 1, Y)
                                Y += 16
                                inicio1 += caracteresPorLinea1
                            End While
                        End If


                        If tiempo <> "" Then
                            e.Graphics.DrawString("------------" & tiempo & "--------------", New Drawing.Font(tipografia, 8, FontStyle.Regular), Brushes.Black, 1, Y)
                            Y += 20
                        End If

                    End If
                Loop
                rd1.Close()
                cnn1.Close()
                e.Graphics.DrawString("-------------------------------", New Drawing.Font(tipografia, 12, FontStyle.Regular), Brushes.Black, 1, Y)
            Else
                cnn1.Close() : cnn1.Open()
                cnn2.Close() : cnn2.Open()
                cmd2 = cnn2.CreateCommand
                cmd2.CommandText = "SELECT DISTINCT Comisionista FROM comandas WHERE Id=" & MyFolio & ""
                rd2 = cmd2.ExecuteReader
                Do While rd2.Read
                    If rd2.HasRows Then
                        tiempo = rd2("Comisionista").ToString


                        cmd1 = cnn1.CreateCommand
                        cmd1.CommandText = "select IDC,Codigo,Nombre,Cantidad,Comensal,Comentario,Gprint,Comisionista from Comandas where id=" & MyFolio & " and GPrint='" & impre & "' AND Comisionista='" & tiempo & "' group by Comensal,IDC,Codigo,Nombre,Cantidad,Comentario,Gprint,Comisionista Order By Comensal"
                        rd1 = cmd1.ExecuteReader
                        Do While rd1.Read
                            If rd1.HasRows Then
                                codigo = rd1("Codigo").ToString
                                nombre = rd1("Nombre").ToString
                                cantidad = rd1("Cantidad").ToString
                                comensall = rd1("Comensal").ToString
                                comentario = rd1("Comentario").ToString
                                idc = rd1("IDC").ToString



                                e.Graphics.DrawString(cantidad, New Font("Arial", 13, FontStyle.Regular), Brushes.Black, 1, Y)

                                Dim caracteresPorLinea As Integer = 20
                                Dim texto As String = nombre
                                Dim inicio As Integer = 0
                                Dim longitudTexto As Integer = texto.Length

                                While inicio < longitudTexto
                                    Dim longitudBloque As Integer = Math.Min(caracteresPorLinea, longitudTexto - inicio)
                                    Dim bloque As String = texto.Substring(inicio, longitudBloque)
                                    e.Graphics.DrawString(bloque, New Font("Arial", 13, FontStyle.Regular), Brushes.Black, 25, Y)
                                    Y += 16
                                    inicio += caracteresPorLinea
                                End While


                                If comentario <> "" Then

                                    Dim caracteresPorLinea2 As Integer = 32
                                    Dim texto2 As String = comentario
                                    Dim inicio2 As Integer = 0
                                    Dim longitudTexto2 As Integer = texto2.Length
                                    ' e.Graphics.DrawString("NOTA:", fuente_datos, Brushes.Black, 1, Y)
                                    While inicio2 < longitudTexto2
                                        Dim longitudBloque2 As Integer = Math.Min(caracteresPorLinea2, longitudTexto2 - inicio2)
                                        Dim bloque2 As String = texto2.Substring(inicio2, longitudBloque2)

                                        e.Graphics.DrawString(bloque2, fuente_datos, Brushes.Black, 1, Y)
                                        Y += 16
                                        inicio2 += caracteresPorLinea2
                                    End While
                                End If

                            End If
                        Loop
                        rd1.Close()

                        If tiempo <> "" Then
                            e.Graphics.DrawString("------------" & tiempo & "--------------", New Drawing.Font(tipografia, 8, FontStyle.Regular), Brushes.Black, 1, Y)
                            Y += 20
                        End If

                    End If
                Loop
                rd2.Close()
                cnn2.Close()
                cnn1.Close()

                e.Graphics.DrawString("-------------------------------", New Drawing.Font(tipografia, 12, FontStyle.Regular), Brushes.Black, 1, Y)
            End If
            Y += 15
            e.Graphics.DrawString("MESERO", New Font("Arial", 17, FontStyle.Regular), Brushes.Black, 1, Y)
            e.Graphics.DrawString(lblatiende.Text, New Font("Arial", 17, FontStyle.Regular), Brushes.Black, 285, Y, sf)

            cnn2.Close()
            cnn1.Close()

            e.HasMorePages = False
        Catch ex As Exception
            MessageBox.Show(ex.ToString)

            cnn2.Close()
            cnn1.Close()
        End Try
    End Sub

    Private Sub PComanda58_PrintPage(sender As Object, e As Printing.PrintPageEventArgs) Handles PComanda58.PrintPage
        Dim tipografia As String = "Lucida Sans Typewriter"
        Dim fuente_r As New Font("Lucida Sans Typewriter", 6, FontStyle.Regular)
        Dim fuente_b As New Font("Lucida Sans Typewriter", 7, FontStyle.Bold)
        Dim fuente_c As New Font("Lucida Sans Typewriter", 7, FontStyle.Regular)
        Dim fuente_p As New Font("Lucida Sans Typewriter", 6, FontStyle.Regular)
        Dim derecha As New StringFormat With {.Alignment = StringAlignment.Far}
        Dim sc As New StringFormat With {.Alignment = StringAlignment.Center}
        Dim pluma As New Pen(Brushes.Black, 1)
        Dim Y As Double = 0

        Dim Pie1 As String = ""
        Dim pie2 As String = ""
        Dim pie3 As String = ""
        Dim SinNumCoemensal As Integer = 0

        Dim conta As Integer = 0
        Dim contband As Integer = 0
        Dim comen_sal As String = ""

        Dim cnn1 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
        Dim cnn2 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
        Dim rd1, rd2 As MySqlDataReader
        Dim cmd1, cmd2 As MySqlCommand

        Try
            cnn2.Close() : cnn2.Open()
            cmd2 = cnn2.CreateCommand
            cmd2.CommandText = "select NotasCred from Formatos where Facturas='SinNumComensal'"
            rd2 = cmd2.ExecuteReader
            If rd2.HasRows Then
                If rd2.Read Then
                    SinNumCoemensal = rd2(0).ToString
                End If
            Else
                SinNumCoemensal = 0
            End If
            rd2.Close()

            cmd2 = cnn2.CreateCommand
            cmd2.CommandText = "select Pie1,Pie2,Pie3,Cab0,Cab1,Cab2,Cab3,Cab4,Cab5,Cab6 from Ticket"
            rd2 = cmd2.ExecuteReader
            If rd2.HasRows Then
                If rd2.Read Then
                    Pie1 = rd2("Pie1")
                    pie2 = rd2("Pie2")
                    pie3 = rd2("Pie3")

                    If rd2("Cab0").ToString() <> "" Then
                        e.Graphics.DrawString(rd2("Cab0").ToString, fuente_c, Brushes.Black, 90, Y, sc)
                        Y += 11
                    End If

                    If rd2("Cab1").ToString() <> "" Then
                        e.Graphics.DrawString(rd2("Cab1").ToString, fuente_c, Brushes.Black, 90, Y, sc)
                        Y += 11
                    End If

                    If rd2("Cab2").ToString() <> "" Then
                        e.Graphics.DrawString(rd2("Cab2").ToString, fuente_c, Brushes.Gray, 90, Y, sc)
                        Y += 11
                    End If

                    If rd2("Cab3").ToString() <> "" Then
                        e.Graphics.DrawString(rd2("Cab3").ToString, fuente_c, Brushes.Gray, 90, Y, sc)
                        Y += 11
                    End If

                    If rd2("Cab4").ToString() <> "" Then
                        e.Graphics.DrawString(rd2("Cab4").ToString, fuente_c, Brushes.Gray, 90, Y, sc)
                        Y += 11
                    End If

                    If rd2("Cab5").ToString() <> "" Then
                        e.Graphics.DrawString(rd2("Cab5").ToString, fuente_c, Brushes.Gray, 90, Y, sc)
                        Y += 11
                    End If

                    If rd2("Cab6").ToString <> "" Then
                        e.Graphics.DrawString(rd2("Cab6").ToString, fuente_c, Brushes.Gray, 90, Y, sc)
                        Y += 11
                    End If
                    Y += 4
                End If
            Else
            End If
            rd2.Close()

            e.Graphics.DrawString("-------------------------------", fuente_b, Brushes.Black, 1, Y)
            Y += 14

            e.Graphics.DrawString("C O M A N D A", fuente_b, Brushes.Black, 90, Y, sc)
            Y += 11
            e.Graphics.DrawString("-------------------------------", fuente_b, Brushes.Black, 1, Y)
            Y += 17

            e.Graphics.DrawString("Folio: " & MyFolio, fuente_b, Brushes.Black, 1, Y)
            Y += 14
            e.Graphics.DrawString("Fecha: " & FormatDateTime(Date.Now, DateFormat.ShortDate), fuente_b, Brushes.Black, 1, Y)
            e.Graphics.DrawString(FormatDateTime(Date.Now, DateFormat.LongTime), fuente_b, Brushes.Black, 180, Y, derecha)
            Y += 18
            e.Graphics.DrawString("-------------------------------", fuente_b, Brushes.Black, 1, Y)
            Y += 16

            If SinNumCoemensal = 1 Then
                e.Graphics.DrawString("MESA: " & lblmesa.Text, fuente_b, Brushes.Black, 1, Y)
                Y += 14
            Else
                e.Graphics.DrawString("NMESA: " & lblmesa.Text, fuente_b, Brushes.Black, 1, Y)
                Y += 11
                e.Graphics.DrawString("Comensales: " & lblNcomensales.Text, fuente_b, Brushes.Black, 1, Y)
                Y += 14
            End If

            e.Graphics.DrawString("-------------------------------", fuente_b, Brushes.Black, 1, Y)
            Y += 13

            e.Graphics.DrawString("CANTIDAD", fuente_b, Brushes.Black, 1, Y)
            e.Graphics.DrawString("PRODUCTO", fuente_b, Brushes.Black, 130, Y, derecha)
            Y += 6
            e.Graphics.DrawString("-------------------------------", fuente_b, Brushes.Black, 1, Y)
            Y += 14

            Dim codigo As String = ""
            Dim nombre As String = ""
            Dim cantidad As Double = 0
            Dim comensall As String = ""
            Dim comentario As String = ""
            Dim idc As Integer = 0
            Dim tiempo As String = ""
            If SinNumCoemensal = 1 Then

                cnn1.Close() : cnn1.Open()
                cmd1 = cnn1.CreateCommand
                cmd1.CommandText = "select IDC,Codigo,Nombre,Cantidad,Comensal,Comentario,Comisionista from Comandas  where GPrint='" & impre & "' and Id=" & MyFolio & " group by Comensal,IDC,Codigo,Nombre,Cantidad,Comentario,Comisionista order by comensal"
                rd1 = cmd1.ExecuteReader
                Do While rd1.Read
                    If rd1.HasRows Then

                        codigo = rd1("Codigo").ToString
                        nombre = rd1("Nombre").ToString
                        cantidad = rd1("Cantidad").ToString
                        comensall = rd1("Comensal").ToString
                        comentario = rd1("Comentario").ToString
                        tiempo = rd1("Comisionista").ToString

                        idc = rd1("IDC").ToString

                        e.Graphics.DrawString(cantidad, fuente_b, Brushes.Black, 1, Y)

                        Dim caracteresPorLinea As Integer = 25
                        Dim texto As String = nombre
                        Dim inicio As Integer = 0
                        Dim longitudTexto As Integer = texto.Length

                        While inicio < longitudTexto
                            Dim longitudBloque As Integer = Math.Min(caracteresPorLinea, longitudTexto - inicio)
                            Dim bloque As String = texto.Substring(inicio, longitudBloque)
                            e.Graphics.DrawString(bloque, New Font("Arial", 10, FontStyle.Regular), Brushes.Black, 25, Y)
                            Y += 15
                            inicio += caracteresPorLinea
                        End While


                        If comentario <> "" Then
                            e.Graphics.DrawString("NOTA: " & comentario, fuente_b, Brushes.Black, 1, Y)
                            Y += 13
                        End If

                        If tiempo = "LN" Then
                            e.Graphics.DrawString("-------------------------------", New Drawing.Font(tipografia, 12, FontStyle.Regular), Brushes.Black, 1, Y)
                            Y += 15
                        End If

                    End If
                Loop
                rd1.Close()
                cnn1.Close()
                e.Graphics.DrawString("-------------------------------", fuente_b, Brushes.Black, 1, Y)
            Else
                cnn1.Close() : cnn1.Open()

                cmd1 = cnn1.CreateCommand
                cmd1.CommandText = "select IDC,Codigo,Nombre,Cantidad,Comensal,Comentario,Gprint,Comisionista from Comandas where id=" & MyFolio & " and GPrint='" & impre & "' group by Comensal,IDC,Codigo,Nombre,Cantidad,Comentario,Gprint,Comisionista Order By Comensal"
                rd1 = cmd1.ExecuteReader
                Do While rd1.Read
                    If rd1.HasRows Then
                        codigo = rd1("Codigo").ToString
                        nombre = rd1("Nombre").ToString
                        cantidad = rd1("Cantidad").ToString
                        comensall = rd1("Comensal").ToString
                        comentario = rd1("Comentario").ToString
                        tiempo = rd1("Comisionista").ToString
                        idc = rd1("IDC").ToString

                        e.Graphics.DrawString(cantidad, fuente_b, Brushes.Black, 1, Y)

                        Dim caracteresPorLinea As Integer = 25
                        Dim texto As String = nombre
                        Dim inicio As Integer = 0
                        Dim longitudTexto As Integer = texto.Length

                        While inicio < longitudTexto
                            Dim longitudBloque As Integer = Math.Min(caracteresPorLinea, longitudTexto - inicio)
                            Dim bloque As String = texto.Substring(inicio, longitudBloque)
                            e.Graphics.DrawString(bloque, New Font("Arial", 10, FontStyle.Regular), Brushes.Black, 25, Y)
                            Y += 15
                            inicio += caracteresPorLinea
                        End While

                        If comentario <> "" Then
                            e.Graphics.DrawString("NOTA: " & comentario, fuente_b, Brushes.Black, 1, Y)
                            Y += 14
                        End If

                        If tiempo = "LN" Then
                            e.Graphics.DrawString("-------------------------------", New Drawing.Font(tipografia, 12, FontStyle.Regular), Brushes.Black, 1, Y)
                            Y += 15
                        End If

                    End If
                Loop
                rd1.Close()
                cnn1.Close()
                e.Graphics.DrawString("-------------------------------", fuente_b, Brushes.Black, 1, Y)
            End If
            Y += 13
            e.Graphics.DrawString("MESERO: ", fuente_b, Brushes.Black, 1, Y)
            e.Graphics.DrawString(lblatiende.Text, fuente_b, Brushes.Black, 70, Y)

            cnn2.Close()
            cnn1.Close()

            e.HasMorePages = False
        Catch ex As Exception
            MessageBox.Show(ex.ToString)

            cnn2.Close()
            cnn1.Close()
        End Try
    End Sub

    Private Sub btnRepertir_Click(sender As Object, e As EventArgs) Handles btnRepertir.Click
        frmRepetirProducto.Show()
        frmRepetirProducto.BringToFront()
    End Sub

    Private Sub TFecha_Tick(sender As Object, e As EventArgs) Handles TFecha.Tick

        TFecha.Stop()
        lblFecha.Text = Format(Date.Now, "yyyy/MM/dd")
        TFecha.Start()

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim controlsToShow As Integer = Math.Min(controlsPerClick, pDepartamento.Controls.Count - currentIndex)

        For i As Integer = currentIndex To currentIndex + controlsToShow - 1
            pDepartamento.Controls(i).Visible = True
        Next

        currentIndex += controlsToShow

        If pDepartamento.VerticalScroll.Visible Then
            pDepartamento.AutoScrollPosition = New Point(0, pDepartamento.VerticalScroll.Value + 80)
        End If
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Dim controlsToShow As Integer = Math.Min(controlsPerClick, pDepartamento.Controls.Count - currentIndex)

        For i As Integer = currentIndex To currentIndex + controlsToShow - 1
            pDepartamento.Controls(i).Visible = True
        Next

        currentIndex += controlsToShow

        If pDepartamento.VerticalScroll.Visible Then
            pDepartamento.AutoScrollPosition = New Point(0, pDepartamento.VerticalScroll.Value - 80)
        End If
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        Dim controlsToShow As Integer = Math.Min(controlsPerClick, pgrupo.Controls.Count - currentIndex)

        For i As Integer = currentIndex To currentIndex + controlsToShow - 1
            pgrupo.Controls(i).Visible = True
        Next

        currentIndex += controlsToShow

        If pgrupo.VerticalScroll.Visible Then
            pgrupo.AutoScrollPosition = New Point(0, pgrupo.VerticalScroll.Value + 80)
        End If
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Dim controlsToShow As Integer = Math.Min(controlsPerClick, pgrupo.Controls.Count - currentIndex)

        For i As Integer = currentIndex To currentIndex + controlsToShow - 1
            pgrupo.Controls(i).Visible = True
        Next

        currentIndex += controlsToShow

        If pgrupo.VerticalScroll.Visible Then
            pgrupo.AutoScrollPosition = New Point(0, pgrupo.VerticalScroll.Value - 80)
        End If
    End Sub

    Private Sub btntiempo3_Click(sender As Object, e As EventArgs) Handles btntiempo3.Click
        Tiempos("15")
    End Sub



    Private Sub btncoma_Click(sender As Object, e As EventArgs) Handles btncoma.Click
        txtRespuesta.Text = txtRespuesta.Text + btncoma.Text
    End Sub

    Private Sub btnOcasional_Click(sender As Object, e As EventArgs) Handles btnOcasional.Click
        frmProductoOcasional.focomesasmostrar = 1
        frmProductoOcasional.Show()
    End Sub

    Private Sub btnespacio_Click(sender As Object, e As EventArgs) Handles btnespacio.Click
        txtRespuesta.Text = txtRespuesta.Text + btnespacio.Text
    End Sub

    Private Sub btnordenar_Click(sender As Object, e As EventArgs) Handles btnordenar.Click
        EnviarComanda()
    End Sub


#End Region

End Class