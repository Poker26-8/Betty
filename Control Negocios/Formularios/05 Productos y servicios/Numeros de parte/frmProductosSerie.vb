﻿Imports System.IO
Imports System.Net
Imports Microsoft.Office.Interop.Excel
Imports MySql.Data
Imports MySql.Data.MySqlClient

Public Class frmProductosSerie

    Dim DonVoy As String = ""

    Private Sub TraeDatos(ByVal vemos As String)
        Dim cnn1 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
        Dim rd1 As MySqlDataReader
        Dim cmd1 As MySqlCommand
        Try
            cnn1.Close() : cnn1.Open()

            cmd1 = cnn1.CreateCommand
            If vemos = "CODIGO" Then
                cmd1.CommandText =
                    "select Codigo,Nombre,CodBarra,NombreLargo,IVA,UCompra,UVenta,UMinima,MCD,Multiplo,Min,Max,Comision,ProvPri,ProvEme,Departamento,Grupo,Ubicacion,ProvRes,PercentIVAret,IIEPS,ClaveSat,UnidadSat,N_Serie,N_Serie2,GPrint from Productos where Codigo='" & cboCodigo.Text & "'"
            End If
            If vemos = "BARRAS" Then
                cmd1.CommandText =
                    "select Codigo,Nombre,CodBarra,NombreLargo,IVA,UCompra,UVenta,UMinima,MCD,Multiplo,Min,Max,Comision,ProvPri,ProvEme,Departamento,Grupo,Ubicacion,ProvRes,PercentIVAret,IIEPS,ClaveSat,UnidadSat,N_Serie,N_Serie2,GPrint from Productos where CodBarra='" & txtbarras.Text & "'"
            End If
            If vemos = "NOMBRE" Then
                cmd1.CommandText =
                    "select Codigo,Nombre,CodBarra,NombreLargo,IVA,UCompra,UVenta,UMinima,MCD,Multiplo,Min,Max,Comision,ProvPri,ProvEme,Departamento,Grupo,Ubicacion,ProvRes,PercentIVAret,IIEPS,ClaveSat,UnidadSat,N_Serie,N_Serie2,GPrint from Productos where Nombre='" & cboNombre.Text & "'"
            End If
            If vemos = "SERIE" Then
                cmd1.CommandText =
                    "select Codigo,Nombre,CodBarra,NombreLargo,IVA,UCompra,UVenta,UMinima,MCD,Multiplo,Min,Max,Comision,ProvPri,ProvEme,Departamento,Grupo,Ubicacion,ProvRes,PercentIVAret,IIEPS,ClaveSat,UnidadSat,N_Serie,N_Serie2,GPrint from Productos where N_Serie='" & txtn_serie.Text & "'"
            End If

            If vemos = "SERIE2" Then
                cmd1.CommandText =
                    "select Codigo,Nombre,CodBarra,NombreLargo,IVA,UCompra,UVenta,UMinima,MCD,Multiplo,Min,Max,Comision,ProvPri,ProvEme,Departamento,Grupo,Ubicacion,ProvRes,PercentIVAret,IIEPS,ClaveSat,UnidadSat,N_Serie,N_Serie2,GPrint from Productos where N_Serie2='" & txt_Serie2.Text & "'"
            End If

            rd1 = cmd1.ExecuteReader
            If rd1.HasRows Then
                If rd1.Read Then
                    cboCodigo.Text = rd1("Codigo").ToString
                    cboNombre.Text = rd1("Nombre").ToString
                    txtbarras.Text = rd1("CodBarra").ToString
                    txtNombreL.Text = rd1("NombreLargo").ToString
                    cboIVA.Text = rd1("IVA").ToString
                    txtMaxima.Text = rd1("UCompra").ToString
                    txtActual.Text = rd1("UVenta").ToString
                    txtMinima.Text = rd1("UMinima").ToString
                    txtmcd.Text = rd1("MCD").ToString
                    txtmultiplo.Text = rd1("Multiplo").ToString
                    txtMinimo.Text = rd1("Min").ToString
                    txtMaximo.Text = rd1("Max").ToString
                    txtComi.Text = FormatNumber(rd1("Comision").ToString, 2)
                    cboProvP.Text = rd1("ProvPri").ToString
                    cboProvE.Text = rd1("ProvEme").ToString
                    cboDepto.Text = rd1("Departamento").ToString
                    cboGrupo.Text = rd1("Grupo").ToString
                    cboubicacion.Text = rd1("Ubicacion").ToString()
                    chkKIT.Checked = IIf(rd1("ProvRes").ToString = True, True, False)
                    txtRetIVA.Text = rd1("PercentIVAret").ToString
                    txtIEPS.Text = rd1("IIEPS").ToString
                    txtCodigoSAT.Text = rd1("ClaveSat").ToString
                    txtClaveSAT.Text = rd1("UnidadSat").ToString
                    txtn_serie.Text = rd1("N_Serie").ToString()
                    txt_Serie2.Text = rd1("N_Serie2").ToString()
                    cboComanda.Text = rd1("GPrint").ToString
                    '-------------------------
                    '- Red
                    '------------
                    If File.Exists(My.Application.Info.DirectoryPath & "\ProductosImg" & base & "\" & cboCodigo.Text & ".jpg") Then
                        picImagen.Image = Image.FromFile(My.Application.Info.DirectoryPath & "\ProductosImg" & base & "\" & cboCodigo.Text & ".jpg")
                        txtrutaimagen.Text = ""
                    End If
                End If
            End If
            rd1.Close()

            cmd1 = cnn1.CreateCommand
            cmd1.CommandText =
                      "select tb.id,tb.nombre_moneda from tb_moneda tb,Productos p where p.Codigo='" & cboCodigo.Text & "' and p.id_tbMoneda=tb.id"
            rd1 = cmd1.ExecuteReader
            If rd1.HasRows Then
                If rd1.Read Then
                    cboMoneda.Tag = rd1("id").ToString
                    cboMoneda.Text = rd1("nombre_moneda").ToString
                End If
            End If
            rd1.Close()

            cboCodigo.SelectionStart = 0
            cboCodigo.SelectionLength = Len(cboCodigo.Text)

            cnn1.Close()
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
    End Sub

    Private Sub frmProductosSerie_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        cboNombre.Focus().Equals(True)
        'crea_ruta(My.Application.Info.DirectoryPath & "\ProductosImg\")
        Dim cnn1 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
        Dim rd1 As MySqlDataReader
        Dim cmd1 As MySqlCommand
        cnn1.Close() : cnn1.Open()
        cmd1 = cnn1.CreateCommand
        cmd1.CommandText =
            "select id,nombre_moneda from tb_moneda where nombre_moneda='PESO' or nombre_moneda='PESOS'"
        rd1 = cmd1.ExecuteReader
        If rd1.HasRows Then
            If rd1.Read Then
                cboMoneda.Tag = rd1("id").ToString
                cboMoneda.Items.Add(rd1("nombre_moneda").ToString)
                cboMoneda.Text = rd1("nombre_moneda").ToString
            End If
        Else
            cboMoneda.Tag = ""
        End If
        rd1.Close() : cnn1.Close()
    End Sub

    Private Sub cboCodigo_DropDown(sender As System.Object, e As System.EventArgs) Handles cboCodigo.DropDown
        cboCodigo.Items.Clear()
        picImagen.Image = Nothing

        Dim cnn1 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
        Dim rd1 As MySqlDataReader
        Dim cmd1 As MySqlCommand

        Try
            cnn1.Close() : cnn1.Open()

            cmd1 = cnn1.CreateCommand
            If cboCodigo.Text <> "" Then
                cmd1.CommandText =
                    "select Codigo from Productos where left(Codigo, 6)='" & Strings.Left(cboCodigo.Text, 6) & "' order by Codigo"
            Else
                If cboNombre.Text = "" Then
                    cmd1.CommandText =
                        "select Codigo from Productos order by Codigo"
                Else
                    cmd1.CommandText =
                        "select Codigo from Prouctos where Nombre='" & cboNombre.Text & "' order by Codigo"
                End If
            End If
            rd1 = cmd1.ExecuteReader
            Do While rd1.Read
                If rd1.HasRows Then cboCodigo.Items.Add(
                    rd1(0).ToString
                    )
            Loop
            rd1.Close() : cnn1.Close()
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
            cnn1.Close()
        End Try
    End Sub

    Private Sub cboCodigo_GotFocus(sender As Object, e As System.EventArgs) Handles cboCodigo.GotFocus
        cboCodigo.SelectionStart = 0
        cboCodigo.SelectionLength = Len(cboCodigo.Text)
    End Sub

    Private Sub cboCodigo_KeyPress(sender As Object, e As System.Windows.Forms.KeyPressEventArgs) Handles cboCodigo.KeyPress
        e.KeyChar = UCase(e.KeyChar)
        Dim cnn2 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
        If Len(cboCodigo.Text) > 7 Then
            cboCodigo.Text = Strings.Left(cboCodigo.Text, Len(cboCodigo.Text) - 1)
            cboCodigo.SelectionStart = 0
            cboCodigo.SelectionLength = Len(cboCodigo.Text)
        End If
        If AscW(e.KeyChar) = Keys.Enter Then
            If cboCodigo.Text <> "" Then
            End If
            If cnn2.State = 1 Then
                cnn2.Close()
            End If
            cboNombre.Focus().Equals(True)
            TraeDatos("CODIGO")
        End If
    End Sub

    Private Sub cboCodigo_LostFocus(sender As Object, e As System.EventArgs) Handles cboCodigo.LostFocus
        Dim cnn2 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
        Dim cnn3 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
        Dim rd2, rd3 As MySqlDataReader
        Dim cmd2, cmd3 As MySqlCommand
        Try
            cnn2.Close() : cnn2.Open()
            cmd2 = cnn2.CreateCommand
            cmd2.CommandText =
                "select CodBarra,Nombre,ProvPri,IVA,ProvEme,ProvRes,UCompra,UVenta,UMinima,Departamento,Grupo,Ubicacion,Max,Min,Comision,MCD,Multiplo,NombreLargo,PercentIVAret,IIEPS,N_Serie from Productos where Codigo='" & cboCodigo.Text & "'"
            rd2 = cmd2.ExecuteReader
            If rd2.HasRows Then
                If rd2.Read Then
                    txtbarras.Text = rd2("CodBarra").ToString
                    cboNombre.Text = rd2("Nombre").ToString
                    cboProvP.Text = rd2("ProvPri").ToString
                    cboIVA.Text = rd2("IVA").ToString
                    cboProvE.Text = rd2("ProvEme").ToString
                    chkKIT.Checked = IIf(rd2("ProvRes").ToString = True, True, False)
                    txtMaxima.Text = rd2("UCompra").ToString
                    txtActual.Text = rd2("UVenta").ToString
                    txtMinima.Text = rd2("UMinima").ToString
                    cboDepto.Text = rd2("Departamento").ToString
                    cboGrupo.Text = rd2("Grupo").ToString
                    cboubicacion.Text = rd2("Ubicacion").ToString()
                    txtMaximo.Text = rd2("Max").ToString
                    txtMinimo.Text = rd2("Min").ToString
                    txtComi.Text = rd2("Comision").ToString
                    txtmcd.Text = rd2("MCD").ToString
                    txtmultiplo.Text = rd2("Multiplo").ToString
                    txtNombreL.Text = rd2("NombreLargo").ToString
                    txtRetIVA.Text = rd2("PercentIVAret").ToString
                    txtIEPS.Text = rd2("IIEPS").ToString
                    txtn_serie.Text = rd2("N_Serie").ToString()
                End If
            Else
                cnn3.Close() : cnn3.Open()

                cmd3 = cnn3.CreateCommand
                cmd3.CommandText =
                    "select CodBarra,Nombre,ProvPri,IVA,ProvEme,ProvRes,UCompra,UVenta,UMinima,Departamento,Grupo,Ubicacion,Max,Min,Comision,MCD,Multiplo,NombreLargo,N_Serie from Productos where Codigo='" & Strings.Left(cboCodigo.Text, 6) & "'"
                rd3 = cmd3.ExecuteReader
                If rd3.HasRows Then
                    If rd3.Read Then
                        txtbarras.Text = rd3("CodBarra").ToString
                        cboNombre.Text = rd3("Nombre").ToString
                        cboProvP.Text = rd3("ProvPri").ToString
                        cboIVA.Text = rd3("IVA").ToString
                        cboProvE.Text = rd3("ProvEme").ToString
                        chkKIT.Checked = IIf(rd3("ProvRes").ToString = True, True, False)
                        txtMaxima.Text = rd3("UCompra").ToString
                        txtActual.Text = rd3("UVenta").ToString
                        txtMinima.Text = rd3("UMinima").ToString
                        cboDepto.Text = rd3("Departamento").ToString
                        cboGrupo.Text = rd3("Grupo").ToString
                        cboubicacion.Text = rd3("Ubicacion").ToString()
                        txtMaximo.Text = rd3("Max").ToString
                        txtMinimo.Text = rd3("Min").ToString
                        txtComi.Text = rd3("Comision").ToString
                        txtmcd.Text = rd3("MCD").ToString
                        txtmultiplo.Text = rd3("Multiplo").ToString()
                        txtNombreL.Text = rd3("NombreLargo").ToString()
                        txtn_serie.Text = rd3("N_Serie").ToString()
                        DonVoy = "Nvo"
                        txtbarras.Text = ""
                        txtActual.Text = ""
                        txtmcd.Text = ""
                        txtmultiplo.Text = ""
                    End If
                Else
                    'txtbarras.Text = ""
                    'cboNombre.Text = ""
                    'txtNombreL.Text = ""
                    'cboIVA.Text = ""
                    'txtMaxima.Text = ""
                    'txtActual.Text = ""
                    'txtMinima.Text = ""
                    'txtmcd.Text = ""
                    'txtmultiplo.Text = ""
                    'txtMinimo.Text = ""
                    'txtMaximo.Text = ""
                    'txtComi.Text = ""
                    'cboProvP.Text = ""
                    'cboProvP.Items.Clear()
                    'cboProvE.Text = ""
                    'cboProvE.Items.Clear()
                    'cboDepto.Text = ""
                    'cboDepto.Items.Clear()
                    'cboGrupo.Text = ""
                    'cboGrupo.Items.Clear()
                    'cboMoneda.Text = ""
                    'txtRetIVA.Text = "0"
                    'txtIEPS.Text = "0"
                    'txtCodigoSAT.Text = ""
                    'txtClaveSAT.Text = ""
                    'chkKIT.Checked = False
                End If
                cnn3.Close()
            End If
            rd2.Close() : cnn2.Close()

            If cboNombre.Text <> "" Then
                txtActual.Focus().Equals(True)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
            cnn2.Close()
        End Try
    End Sub

    Private Sub cboCodigo_SelectedValueChanged(sender As System.Object, e As System.EventArgs) Handles cboCodigo.SelectedValueChanged
        TraeDatos("CODIGO")
    End Sub

    Private Sub txtbarras_Click(sender As System.Object, e As System.EventArgs) Handles txtbarras.Click
        txtbarras.SelectionStart = 0
        txtbarras.SelectionLength = Len(txtbarras.Text)
    End Sub

    Private Sub txtbarras_GotFocus(sender As Object, e As System.EventArgs) Handles txtbarras.GotFocus
        txtbarras.SelectionStart = 0
        txtbarras.SelectionLength = Len(txtbarras.Text)
    End Sub

    Private Sub txtbarras_KeyPress(sender As System.Object, e As System.Windows.Forms.KeyPressEventArgs) Handles txtbarras.KeyPress
        If AscW(e.KeyChar) = Keys.Enter Then
            If cboCodigo.Text = "" Then
                If txtbarras.Text = "" Then cboCodigo.Focus().Equals(True) : Exit Sub
                TraeDatos("BARRAS")
            End If
            cboCodigo.Focus().Equals(True)
        End If
    End Sub

    Private Sub cboNombre_DropDown(sender As System.Object, e As System.EventArgs) Handles cboNombre.DropDown
        cboNombre.Items.Clear()
        Dim cnn1 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
        Dim rd1 As MySqlDataReader
        Dim cmd1 As MySqlCommand
        Try
            cnn1.Close() : cnn1.Open()

            cmd1 = cnn1.CreateCommand
            cmd1.CommandText =
                "select distinct Nombre from Productos order by Nombre"
            rd1 = cmd1.ExecuteReader
            Do While rd1.Read
                If rd1.HasRows Then cboNombre.Items.Add(
                    rd1(0).ToString
                    )
            Loop
            rd1.Close() : cnn1.Close()
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
            cnn1.Close()
        End Try
    End Sub

    Private Sub cboNombre_KeyPress(sender As Object, e As System.Windows.Forms.KeyPressEventArgs) Handles cboNombre.KeyPress
        e.KeyChar = UCase(e.KeyChar)
        Dim cnn1 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
        Dim cnn2 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
        Dim rd1, rd2 As MySqlDataReader
        Dim cmd1, cmd2 As MySqlCommand
        If AscW(e.KeyChar) = Keys.Enter Then
            Try
                cnn1.Close() : cnn1.Open()

                cmd1 = cnn1.CreateCommand
                cmd1.CommandText =
                    "select Codigo,CodBarra,ProvPri,ProvEme,ProvRes,UCompra,UVenta,UMinima,Departamento,Grupo,Ubicacion,Min,Max,IVA,Comision,MCD,Multiplo,NombreLargo,PercentIVAret,N_Serie from Productos where Nombre='" & cboNombre.Text & "' order by Codigo"
                rd1 = cmd1.ExecuteReader
                If rd1.HasRows Then
                    If rd1.Read Then
                        cboCodigo.Text = rd1("Codigo").ToString
                        If Not rd1("CodBarra").ToString = "" Then txtbarras.Text = rd1("CodBarra").ToString
                        cboProvP.Text = rd1("ProvPri").ToString
                        cboProvE.Text = rd1("ProvEme").ToString
                        chkKIT.Checked = rd1("ProvRes").ToString
                        txtMaxima.Text = rd1("UCompra").ToString
                        txtActual.Text = rd1("UVenta").ToString
                        txtMinima.Text = rd1("UMinima").ToString
                        cboDepto.Text = rd1("Departamento").ToString
                        cboGrupo.Text = rd1("Grupo").ToString
                        cboubicacion.Text = rd1("Ubicacion").ToString()
                        txtMinimo.Text = rd1("Min").ToString
                        txtMaximo.Text = rd1("Max").ToString
                        cboIVA.Text = rd1("IVA").ToString
                        txtComi.Text = rd1("Comision").ToString
                        txtmcd.Text = rd1("MCD").ToString
                        txtmultiplo.Text = rd1("Multiplo").ToString
                        txtNombreL.Text = rd1("NombreLargo").ToString
                        txtRetIVA.Text = rd1("PercentIVAret").ToString
                        txtn_serie.Text = rd1("N_Serie").ToString()

                        cnn2.Close() : cnn2.Open()
                        cmd2 = cnn2.CreateCommand
                        cmd2.CommandText =
                            "select id,nombre_moneda from tb_moneda,Productos where Codigo='" & cboCodigo.Text & "' and Productos.id_tbMoneda=tb_moneda.id"
                        rd2 = cmd2.ExecuteReader
                        If rd2.HasRows Then
                            If rd2.Read Then
                                cboMoneda.Tag = rd2("id").ToString
                                cboMoneda.Text = rd2("nombre_moneda").ToString
                            End If
                        End If
                        rd2.Close() : cnn2.Close()

                        If File.Exists(My.Application.Info.DirectoryPath & "\ProductosImg" & base & "\" & cboCodigo.Text & ".jpg") Then
                            picImagen.Image = Image.FromFile(My.Application.Info.DirectoryPath & "\ProductosImg" & base & "\" & cboCodigo.Text & ".jpg")
                            txtrutaimagen.Text = ""
                        End If
                    End If
                End If
                rd1.Close() : cnn1.Close()
            Catch ex As Exception
                MessageBox.Show(ex.ToString)
                cnn1.Close()
            End Try
            txtn_serie.Focus().Equals(True)
        End If
    End Sub

    Private Sub cboNombre_SelectedValueChanged(sender As Object, e As System.EventArgs) Handles cboNombre.SelectedValueChanged
        TraeDatos("NOMBRE")
    End Sub

    Private Sub txtNombreL_KeyPress(sender As System.Object, e As System.Windows.Forms.KeyPressEventArgs) Handles txtNombreL.KeyPress
        If AscW(e.KeyChar) = Keys.Enter Then
            cboIVA.Focus().Equals(True)
        End If
    End Sub

    Private Sub cboIVA_KeyPress(sender As System.Object, e As System.Windows.Forms.KeyPressEventArgs) Handles cboIVA.KeyPress
        e.KeyChar = UCase(e.KeyChar)
        If AscW(e.KeyChar) = Keys.Enter Then
            txtMaxima.Focus().Equals(True)
        End If
    End Sub

    Private Sub txtMaxima_Click(sender As System.Object, e As System.EventArgs) Handles txtMaxima.Click
        txtMaxima.SelectionStart = 0
        txtMaxima.SelectionLength = Len(txtMaxima.Text)
    End Sub

    Private Sub txtMaxima_GotFocus(sender As Object, e As System.EventArgs) Handles txtMaxima.GotFocus
        txtMaxima.SelectionStart = 0
        txtMaxima.SelectionLength = Len(txtMaxima.Text)
    End Sub

    Private Sub txtMaxima_KeyPress(sender As Object, e As System.Windows.Forms.KeyPressEventArgs) Handles txtMaxima.KeyPress
        If AscW(e.KeyChar) = Keys.Enter Then
            txtMaxima.Text = UCase(txtMaxima.Text)
            txtActual.Focus().Equals(True)
        End If
    End Sub

    Private Sub txtActual_Click(sender As Object, e As System.EventArgs) Handles txtActual.Click
        txtActual.SelectionStart = 0
        txtActual.SelectionLength = Len(txtActual.Text)
    End Sub

    Private Sub txtActual_GotFocus(sender As Object, e As System.EventArgs) Handles txtActual.GotFocus
        txtActual.SelectionStart = 0
        txtActual.SelectionLength = Len(txtActual.Text)
    End Sub

    Private Sub txtActual_KeyPress(sender As Object, e As System.Windows.Forms.KeyPressEventArgs) Handles txtActual.KeyPress
        If AscW(e.KeyChar) = Keys.Enter Then
            txtMinima.Focus().Equals(True)
        End If
    End Sub

    Private Sub txtActual_LostFocus(sender As Object, e As System.EventArgs) Handles txtActual.LostFocus
        If txtMaxima.Text <> "" And txtActual.Text <> "" Then
            lblConv1.Text = "¿Cuantos(as) " & txtActual.Text & " hay por cada " & txtMaxima.Text & "?"
        End If
    End Sub

    Private Sub txtActual_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtActual.TextChanged
        If txtMaxima.Text <> "" And txtActual.Text <> "" Then
            lblConv1.Text = "¿Cuantos(as) " & txtActual.Text & " hay por cada " & txtMaxima.Text & "?"
        End If
    End Sub

    Private Sub txtMinima_Click(sender As Object, e As System.EventArgs) Handles txtMinima.Click
        txtMinima.SelectionStart = 0
        txtMinima.SelectionLength = Len(txtMinima.Text)
    End Sub

    Private Sub txtMinima_LostFocus(sender As Object, e As System.EventArgs) Handles txtMinima.LostFocus
        If txtMaxima.Text <> "" And txtActual.Text <> "" Then
            lblConv2.Text = "¿Cuantos(as) " & txtMinima.Text & " hay por cada " & txtActual.Text & "?"
        End If
    End Sub

    Private Sub txtMinima_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtMinima.TextChanged
        If txtMaxima.Text <> "" And txtActual.Text <> "" Then
            lblConv2.Text = "¿Cuantos(as) " & txtMinima.Text & " hay por cada " & txtActual.Text & "?"
        End If
    End Sub

    Private Sub txtMinima_KeyPress(sender As System.Object, e As System.Windows.Forms.KeyPressEventArgs) Handles txtMinima.KeyPress
        If AscW(e.KeyChar) = Keys.Enter Then
            txtMinima.Text = UCase(txtMinima.Text)
            txtmcd.Focus().Equals(True)
        End If
    End Sub

    Private Sub txtMinima_GotFocus(sender As Object, e As System.EventArgs) Handles txtMinima.GotFocus
        txtMinima.SelectionStart = 0
        txtMinima.SelectionLength = Len(txtMinima.Text)
    End Sub

    Private Sub txtmcd_Click(sender As System.Object, e As System.EventArgs) Handles txtmcd.Click
        txtmcd.SelectionStart = 0
        txtmcd.SelectionLength = Len(txtmcd.Text)
    End Sub

    Private Sub txtmcd_GotFocus(sender As Object, e As System.EventArgs) Handles txtmcd.GotFocus
        txtmcd.SelectionStart = 0
        txtmcd.SelectionLength = Len(txtmcd.Text)
    End Sub

    Private Sub txtmcd_KeyPress(sender As Object, e As System.Windows.Forms.KeyPressEventArgs) Handles txtmcd.KeyPress
        If AscW(e.KeyChar) = Keys.Enter Then
            txtmultiplo.Focus().Equals(True)
        End If
    End Sub

    Private Sub txtmcd_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtmcd.TextChanged
        If txtmcd.Text <> "" Then
            If DonVoy = "Nvo" Then
                txtMinimo.Text = CDec(txtmcd.Text) * CDec(IIf(txtMinimo.Text = "", "1", txtMinimo.Text))
                txtMaximo.Text = CDec(txtmcd.Text) * CDec(IIf(txtMaximo.Text = "", "1", txtMaximo.Text))
            End If
        End If
    End Sub

    Private Sub txtmultiplo_Click(sender As System.Object, e As System.EventArgs) Handles txtmultiplo.Click
        txtmultiplo.SelectionStart = 0
        txtmultiplo.SelectionLength = Len(txtmultiplo.Text)
    End Sub

    Private Sub txtmultiplo_GotFocus(sender As Object, e As System.EventArgs) Handles txtmultiplo.GotFocus
        txtmultiplo.SelectionStart = 0
        txtmultiplo.SelectionLength = Len(txtmultiplo.Text)
    End Sub

    Private Sub txtmultiplo_KeyPress(sender As Object, e As System.Windows.Forms.KeyPressEventArgs) Handles txtmultiplo.KeyPress
        If AscW(e.KeyChar) = Keys.Enter Then
            txtMinimo.Focus().Equals(True)
        End If
    End Sub

    Private Sub txtMinimo_Click(sender As Object, e As System.EventArgs) Handles txtMinimo.Click
        txtMinimo.SelectionStart = 0
        txtMinimo.SelectionLength = Len(txtMinimo.Text)
    End Sub

    Private Sub txtMinimo_GotFocus(sender As Object, e As System.EventArgs) Handles txtMinimo.GotFocus
        txtMinimo.SelectionStart = 0
        txtMinimo.SelectionLength = Len(txtMinimo.Text)
    End Sub

    Private Sub txtMinimo_KeyPress(sender As System.Object, e As System.Windows.Forms.KeyPressEventArgs) Handles txtMinimo.KeyPress
        If AscW(e.KeyChar) = Keys.Enter Then
            txtMaximo.Focus().Equals(True)
        End If
    End Sub

    Private Sub txtMaximo_Click(sender As System.Object, e As System.EventArgs) Handles txtMaximo.Click
        txtMaximo.SelectionStart = 0
        txtMaximo.SelectionLength = Len(txtMaximo.Text)
    End Sub

    Private Sub txtMaximo_GotFocus(sender As Object, e As System.EventArgs) Handles txtMaximo.GotFocus
        txtMaximo.SelectionStart = 0
        txtMaximo.SelectionLength = Len(txtMaximo.Text)
    End Sub

    Private Sub txtMaximo_KeyPress(sender As Object, e As System.Windows.Forms.KeyPressEventArgs) Handles txtMaximo.KeyPress
        If AscW(e.KeyChar) = Keys.Enter Then
            cboProvP.Focus().Equals(True)
        End If
    End Sub

    Private Sub cboProvP_DropDown(sender As System.Object, e As System.EventArgs) Handles cboProvP.DropDown
        cboProvP.Items.Clear()
        Dim cnn1 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
        Dim rd1 As MySqlDataReader
        Dim cmd1 As MySqlCommand
        Try
            cnn1.Close() : cnn1.Open()

            cmd1 = cnn1.CreateCommand
            cmd1.CommandText =
                "select NComercial from Proveedores"
            rd1 = cmd1.ExecuteReader
            Do While rd1.Read
                If rd1.HasRows Then cboProvP.Items.Add(
                    rd1(0).ToString
                    )
            Loop
            rd1.Close() : cnn1.Close()
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
            cnn1.Close()
        End Try
    End Sub

    Private Sub cboProvP_KeyPress(sender As Object, e As System.Windows.Forms.KeyPressEventArgs) Handles cboProvP.KeyPress
        e.KeyChar = UCase(e.KeyChar)

        If AscW(e.KeyChar) = Keys.Enter And cboProvP.Text <> "" Then
            Dim cnn1 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
            Dim rd1 As MySqlDataReader
            Dim cmd1 As MySqlCommand
            Try
                cnn1.Close() : cnn1.Open()

                cmd1 = cnn1.CreateCommand
                cmd1.CommandText =
                    "select NComercial from Proveedores where NComercial='" & cboProvP.Text & "'"
                rd1 = cmd1.ExecuteReader

                If Not rd1.HasRows Then
                    MsgBox("El proveedor no existe en la base de datos del sistema.", vbInformation + vbOKOnly, "Delsscom Control Negocios Pro")
                    Exit Sub
                End If
                rd1.Close() : cnn1.Close()
            Catch ex As Exception
                MessageBox.Show(ex.ToString)
                cnn1.Close()
            End Try
        End If
        If AscW(e.KeyChar) = Keys.Enter Then
            cboProvE.Focus().Equals(True)
        End If
    End Sub

    Private Sub cboProvE_DropDown(sender As System.Object, e As System.EventArgs) Handles cboProvE.DropDown
        cboProvE.Items.Clear()
        Dim cnn1 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
        Dim rd1 As MySqlDataReader
        Dim cmd1 As MySqlCommand
        Try
            cnn1.Close() : cnn1.Open()

            cmd1 = cnn1.CreateCommand
            cmd1.CommandText =
                "select NComercial from Proveedores"
            rd1 = cmd1.ExecuteReader
            Do While rd1.Read
                If rd1.HasRows Then cboProvE.Items.Add(
                    rd1(0).ToString
                    )
            Loop
            rd1.Close() : cnn1.Close()
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
            cnn1.Close()
        End Try
    End Sub

    Private Sub cboProvE_KeyPress(sender As Object, e As System.Windows.Forms.KeyPressEventArgs) Handles cboProvE.KeyPress
        e.KeyChar = UCase(e.KeyChar)
        If AscW(e.KeyChar) = Keys.Enter And cboProvE.Text <> "" Then
            Dim cnn1 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
            Dim rd1 As MySqlDataReader
            Dim cmd1 As MySqlCommand
            Try
                cnn1.Close() : cnn1.Open()

                cmd1 = cnn1.CreateCommand
                cmd1.CommandText =
                    "select NComercial from Proveedores where NComercial='" & cboProvE.Text & "'"
                rd1 = cmd1.ExecuteReader

                If Not rd1.HasRows Then
                    MsgBox("El proveedor no existe en la base de datos del sistema.", vbInformation + vbOKOnly, "Delsscom Control Negocios Pro")
                    Exit Sub
                End If
                rd1.Close() : cnn1.Close()
            Catch ex As Exception
                MessageBox.Show(ex.ToString)
                cnn1.Close()
            End Try
        End If
        If AscW(e.KeyChar) = Keys.Enter Then
            cboDepto.Focus().Equals(True)
        End If
    End Sub

    Private Sub cboDepto_DropDown(sender As System.Object, e As System.EventArgs) Handles cboDepto.DropDown
        cboDepto.Items.Clear()
        Dim cnn1 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
        Dim rd1 As MySqlDataReader
        Dim cmd1 As MySqlCommand
        Try
            cnn1.Close() : cnn1.Open()

            cmd1 = cnn1.CreateCommand
            cmd1.CommandText =
                "select distinct Departamento from Productos order by Departamento"
            rd1 = cmd1.ExecuteReader
            Do While rd1.Read
                If rd1.HasRows Then cboDepto.Items.Add(
                    rd1(0).ToString
                    )
            Loop
            rd1.Close() : cnn1.Close()
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
            cnn1.Close()
        End Try
    End Sub

    Private Sub cboDepto_KeyPress(sender As Object, e As System.Windows.Forms.KeyPressEventArgs) Handles cboDepto.KeyPress
        e.KeyChar = UCase(e.KeyChar)
        If AscW(e.KeyChar) = Keys.Enter Then
            cboGrupo.Focus().Equals(True)
        End If
    End Sub

    Private Sub cboGrupo_DropDown(sender As System.Object, e As System.EventArgs) Handles cboGrupo.DropDown
        cboGrupo.Items.Clear()
        Dim cnn1 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
        Dim rd1 As MySqlDataReader
        Dim cmd1 As MySqlCommand
        Try
            cnn1.Close() : cnn1.Open()

            cmd1 = cnn1.CreateCommand
            cmd1.CommandText =
                "select distinct Grupo from Productos where Departamento='" & cboDepto.Text & "' order by Grupo"
            rd1 = cmd1.ExecuteReader
            Do While rd1.Read
                If rd1.HasRows Then cboGrupo.Items.Add(
                    rd1(0).ToString
                    )
            Loop
            rd1.Close() : cnn1.Close()
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
            cnn1.Close()
        End Try
    End Sub

    Private Sub cboGrupo_KeyPress(sender As Object, e As System.Windows.Forms.KeyPressEventArgs) Handles cboGrupo.KeyPress
        e.KeyChar = UCase(e.KeyChar)
        If AscW(e.KeyChar) = Keys.Enter Then
            cboMoneda.Focus().Equals(True)
        End If
    End Sub

    Private Sub cboMoneda_DropDown(sender As System.Object, e As System.EventArgs) Handles cboMoneda.DropDown
        cboMoneda.Items.Clear()
        Dim cnn1 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
        Dim rd1 As MySqlDataReader
        Dim cmd1 As MySqlCommand
        Try
            cnn1.Close() : cnn1.Open()

            cmd1 = cnn1.CreateCommand
            cmd1.CommandText =
                "select nombre_moneda from tb_moneda"
            rd1 = cmd1.ExecuteReader
            Do While rd1.Read
                If rd1.HasRows Then cboMoneda.Items.Add(
                    rd1(0).ToString
                    )
            Loop
            rd1.Close() : cnn1.Close()
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
            cnn1.Close()
        End Try
    End Sub

    Private Sub cboMoneda_KeyPress(sender As Object, e As System.Windows.Forms.KeyPressEventArgs) Handles cboMoneda.KeyPress
        e.KeyChar = UCase(e.KeyChar)
        If AscW(e.KeyChar) = Keys.Enter Then
            txtRetIVA.Focus().Equals(True)
        End If
    End Sub

    Private Sub cboMoneda_SelectedValueChanged(sender As Object, e As System.EventArgs) Handles cboMoneda.SelectedValueChanged
        Dim cnn1 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
        Dim rd1 As MySqlDataReader
        Dim cmd1 As MySqlCommand
        Try
            cnn1.Close() : cnn1.Open()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText =
                "select id from tb_moneda where nombre_moneda='" & cboMoneda.Text & "'"
            rd1 = cmd1.ExecuteReader
            If rd1.HasRows Then
                If rd1.Read Then
                    cboMoneda.Tag = rd1("id").ToString
                End If
            End If
            rd1.Close() : cnn1.Close()
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
            cnn1.Close()
        End Try
    End Sub

    Private Sub txtRetIVA_GotFocus(sender As Object, e As System.EventArgs) Handles txtRetIVA.GotFocus
        txtRetIVA.SelectionStart = 0
        txtRetIVA.SelectionLength = Len(txtRetIVA.Text)
    End Sub

    Private Sub txtRetIVA_KeyPress(sender As System.Object, e As System.Windows.Forms.KeyPressEventArgs) Handles txtRetIVA.KeyPress
        If AscW(e.KeyChar) = Keys.Enter Then
            txtIEPS.Focus().Equals(True)
        End If
    End Sub

    Private Sub txtIEPS_GotFocus(sender As Object, e As System.EventArgs) Handles txtIEPS.GotFocus
        txtIEPS.SelectionStart = 0
        txtIEPS.SelectionLength = Len(txtIEPS.Text)
    End Sub

    Private Sub txtIEPS_KeyPress(sender As System.Object, e As System.Windows.Forms.KeyPressEventArgs) Handles txtIEPS.KeyPress
        If AscW(e.KeyChar) = Keys.Enter Then
            cboComanda.Focus().Equals(True)
        End If
    End Sub

    Private Sub txtComi_Click(sender As Object, e As System.EventArgs) Handles txtComi.Click
        txtComi.SelectionStart = 0
        txtComi.SelectionLength = Len(txtComi.Text)
    End Sub

    Private Sub txtComi_GotFocus(sender As Object, e As System.EventArgs) Handles txtComi.GotFocus
        txtComi.SelectionStart = 0
        txtComi.SelectionLength = Len(txtComi.Text)
    End Sub

    Private Sub txtComi_KeyPress(sender As System.Object, e As System.Windows.Forms.KeyPressEventArgs) Handles txtComi.KeyPress
        If AscW(e.KeyChar) = Keys.Enter Then
            txtComi.Text = FormatNumber(txtComi.Text, 2)
            txtCodigoSAT.Focus().Equals(True)
        End If
    End Sub

    Private Sub txtClaveSAT_GotFocus(sender As Object, e As System.EventArgs) Handles txtClaveSAT.GotFocus
        txtClaveSAT.SelectionStart = 0
        txtClaveSAT.SelectionLength = Len(txtClaveSAT.Text)
    End Sub

    Private Sub txtClaveSAT_KeyPress(sender As System.Object, e As System.Windows.Forms.KeyPressEventArgs) Handles txtClaveSAT.KeyPress
        If AscW(e.KeyChar) = Keys.Enter Then
            btnGuardar.Focus().Equals(True)
        End If
    End Sub

    Private Sub txtCodigoSAT_GotFocus(sender As Object, e As System.EventArgs) Handles txtCodigoSAT.GotFocus
        txtCodigoSAT.SelectionStart = 0
        txtCodigoSAT.SelectionLength = Len(txtCodigoSAT.Text)
    End Sub

    Private Sub txtCodigoSAT_KeyPress(sender As System.Object, e As System.Windows.Forms.KeyPressEventArgs) Handles txtCodigoSAT.KeyPress
        If AscW(e.KeyChar) = Keys.Enter Then
            cboProvP.Focus().Equals(True)
        End If
    End Sub

    Private Sub btnNuevo_Click(sender As System.Object, e As System.EventArgs) Handles btnNuevo.Click
        txtbarras.Text = ""
        cboCodigo.Items.Clear()
        cboCodigo.Text = ""
        cboNombre.Items.Clear()
        cboNombre.Text = ""
        txtNombreL.Text = ""
        cboIVA.Items.Clear()
        cboIVA.Text = ""
        txtMaxima.Text = ""
        txtActual.Text = ""
        txtMinima.Text = ""
        txtmcd.Text = ""
        txtmultiplo.Text = ""
        txtMinimo.Text = "0"
        txtMaximo.Text = "0"
        txtComi.Text = "0"
        cboProvP.Items.Clear()
        cboProvP.Text = ""
        cboProvE.Items.Clear()
        cboProvE.Text = ""
        cboDepto.Items.Clear()
        cboDepto.Text = ""
        cboGrupo.Items.Clear()
        cboGrupo.Text = ""
        cboMoneda.Items.Clear()
        cboMoneda.Text = ""
        cboMoneda.Tag = ""
        cboubicacion.Items.Clear()
        cboubicacion.Text = ""
        txtRetIVA.Text = "0"
        txtIEPS.Text = "0"
        chkKIT.Checked = False
        txtCodigoSAT.Text = ""
        txtClaveSAT.Text = ""
        picImagen.Image = Nothing
        txtrutaimagen.Text = ""
        txtn_serie.Text = ""
        txt_Serie2.Text = ""
        cboComanda.Text = ""
        frmProductosSerie_Load(Me, New EventArgs())
    End Sub

    Private Sub btnImagen_Click(sender As System.Object, e As System.EventArgs) Handles btnImagen.Click

        Dim cnn1 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
        Dim rd1 As MySqlDataReader
        Dim cmd1 As MySqlCommand

        Try
            If cboCodigo.Text = "" Then MsgBox("Necesitas seleccionar un producto para asignar una imagne.", vbInformation + vbOKOnly, "Delsscom Control Negocios Pro") : cboCodigo.Focus().Equals(True) : Exit Sub
            txtrutaimagen.Text = ""
            My.Application.DoEvents()

            cnn1.Close() : cnn1.Open()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText =
                "select Codigo from Productos where Codigo='" & cboCodigo.Text & "'"
            rd1 = cmd1.ExecuteReader
            If rd1.HasRows Then
            Else
                MsgBox("Producto no válido.", vbInformation + vbOKOnly, "Delsscom Control Negocios Pro")
                rd1.Close() : cnn1.Close() : Exit Sub
            End If
            rd1.Close() : cnn1.Close()

            Dim imagen As New OpenFileDialog
            If (picImagen.Image Is Nothing) Then
            Else
                picImagen.Image.Dispose()
            End If

            With imagen
                .Filter = "Archivos de imagen (*.jpg;*.png)|*.jpg;*.png"
                If .ShowDialog = DialogResult.OK Then
                    txtrutaimagen.Text = .FileName
                    picImagen.Image = Image.FromFile(.FileName)
                    picImagen.BackgroundImageLayout = ImageLayout.Zoom
                End If
            End With
        Catch ex As Exception
            MessageBox.Show(ex.ToString())
            cnn1.Close()
        End Try
    End Sub

    Private Sub btnGuardar_Click(sender As System.Object, e As System.EventArgs) Handles btnGuardar.Click
        If cboCodigo.Text = "" Then MsgBox("El código interno del producto no puede estar vacío.", vbInformation + vbOKOnly, "Delsscom Control Negocios Pro") : cboCodigo.Focus().Equals(True) : Exit Sub
        If Len(cboCodigo.Text) > 7 Then MsgBox("El código corto debe de ser de 7 caracteres com máximo. (En el caso de un derivado)", vbInformation + vbOKOnly, "Delsscom Control Negocios Pro") : cboCodigo.Focus().Equals(True) : Exit Sub
        If cboNombre.Text = "" Then MsgBox("Necesitas escribir el nombre del producto.", vbInformation + vbOKOnly, "Delsscom Control Negocios Pro") : cboNombre.Focus().Equals(True) : Exit Sub
        If cboIVA.Text = "" Then MsgBox("Selecciona un impuesto válido para el producto. Sí el producto no genera impuestos, selecciona '0'.", vbInformation + vbOKOnly, "Delsscom Control Negocios Pro") : cboIVA.Focus().Equals(True) : Exit Sub
        If txtMaxima.Text = "" Then MsgBox("Escribe la unidad *Máxima* del producto, es decir la unidad base del mismo.", vbInformation + vbOKOnly, "Delsscom Control Negocios Pro") : txtMaxima.Focus().Equals(True) : Exit Sub
        If txtActual.Text = "" Then MsgBox("Escribe la unidad *Actual* para el producto que estás registrando.", vbInformation + vbOKOnly, "Delsscom Control Negocios Pro") : txtActual.Focus().Equals(True) : Exit Sub
        If txtMinima.Text = "" Then MsgBox("Escribe la unidad *Mínima* del producto, es decir la mínima unidad de venta para el mismo.", vbInformation + vbOKOnly, "Delsscom Control Negocios Pro") : txtMinima.Focus().Equals(True) : Exit Sub
        If txtmcd.Text = "" Then MsgBox("No puedes omitir las equivalencias para la venta.", vbInformation + vbOKOnly, "Delsscom Control Negocios Pro") : txtmcd.Focus().Equals(True) : Exit Sub
        If txtmultiplo.Text = "" Then MsgBox("No puedes omitir las equivalencias para la venta.", vbInformation + vbOKOnly, "Delsscom Control Negocios Pro") : txtmultiplo.Focus().Equals(True) : Exit Sub
        If txtMinimo.Text = "" Then MsgBox("Escribe el mínimo en el almacén para este producto.", vbInformation + vbOKOnly, "Delsscom Control Negocios Pro") : txtMinimo.Focus().Equals(True) : Exit Sub
        If txtMaximo.Text = "" Then MsgBox("Escribe el máximo en el almacén para este producto.", vbInformation + vbOKOnly, "Delsscom Control Negocios Pro") : txtMaximo.Focus().Equals(True) : Exit Sub
        If cboProvP.Text = "" Then MsgBox("Selecciona un proveedor de la lista de proveedores.", vbInformation + vbOKOnly, "Delsscom Control Negocios Pro") : cboProvP.Focus().Equals(True) : Exit Sub
        If cboDepto.Text = "" Then MsgBox("Escribe o selecciona un departamento para la clasificación del producto.", vbInformation + vbOKOnly, "Delsscom Control Negocios Pro") : cboDepto.Focus().Equals(True) : Exit Sub
        If cboGrupo.Text = "" Then MsgBox("Escribe o selecciona un grupo para la clasifiación del producto.", vbInformation + vbOKOnly, "Delsscom Control Negocios Pro") : cboGrupo.Focus().Equals(True) : Exit Sub
        If cboMoneda.Text = "" Then MsgBox("Selecciona un moneda de compra-venta para el producto.", vbInformation + vbOKOnly, "Delsscom Control Negocios Pro") : cboMoneda.Focus().Equals(True)

        Dim mcd As Double = txtmcd.Text
        Dim multiplo As Double = txtmultiplo.Text
        Dim minimo As Double = txtMinimo.Text
        Dim maximo As Double = txtMaximo.Text
        Dim comision As Double = IIf(txtComi.Text = "", 0, txtComi.Text)
        Dim retIVA As Double = IIf(txtRetIVA.Text = "", 0, txtRetIVA.Text)
        Dim ieps As Double = IIf(txtIEPS.Text = "", 0, txtIEPS.Text)
        Dim fecha As String = Format(Date.Now, "yyyy-MM-dd")

        Dim img As String = ""

        Dim cnn1 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
        Dim cnn2 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
        Dim rd1, rd2 As MySqlDataReader
        Dim cmd1, cmd2 As MySqlCommand

        Try
            cnn1.Close() : cnn1.Open()

            cmd1 = cnn1.CreateCommand
            cmd1.CommandText =
                "select Id from Proveedores where NComercial='" & cboProvP.Text & "'"
            rd1 = cmd1.ExecuteReader
            If Not rd1.HasRows Then
                MsgBox("Este proveedor no está registrado en el catálogo. Regístralo para continuar.", vbInformation + vbOKOnly, "Delsscom Control Negocios Pro")
                rd1.Close()
                cnn1.Close()
                Exit Sub
            End If
            rd1.Close()

            cmd1 = cnn1.CreateCommand
            cmd1.CommandText =
                "select Codigo from Productos where Codigo='" & cboCodigo.Text & "'"
            rd1 = cmd1.ExecuteReader
            If rd1.HasRows Then
                If rd1.Read Then
                    'Actualiza
                    cnn2.Close() : cnn2.Open()

                    cmd2 = cnn2.CreateCommand
                    cmd2.CommandText =
                        "update Productos set CodBarra='" & txtbarras.Text & "', Nombre='" & cboNombre.Text & "', NombreLargo='" & txtNombreL.Text & "', ProvPri='" & cboProvP.Text & "', ProvEme='" & cboProvE.Text & "', ProvRes=" & IIf(chkKIT.Checked, 1, 0) & ", UCompra='" & txtMaxima.Text & "', UVenta='" & txtActual.Text & "', UMinima='" & txtMinima.Text & "', MCD=" & mcd & ", Multiplo=" & multiplo & ", Departamento='" & cboDepto.Text & "', Grupo='" & cboGrupo.Text & "', Ubicacion='" & cboubicacion.Text & "', Min=" & minimo & ", Max=" & maximo & ", Comision=" & comision & ", IVA=" & cboIVA.Text & ", id_tbMoneda=" & cboMoneda.Tag & ", PercentIVAret=" & retIVA & ", IIEPS=" & ieps & ", Cargado=0, Unico=0, N_Serie='" & txtn_serie.Text & "',N_Serie2='" & txt_Serie2.Text & "',GPrint='" & cboComanda.Text & "' where Codigo='" & cboCodigo.Text & "'"
                    If cmd2.ExecuteNonQuery Then

                        If (picImagen.Image Is Nothing) Then
                        Else
                            If txtrutaimagen.Text <> "" Then
                                If File.Exists(My.Application.Info.DirectoryPath & "\ProductosImg" & base & "\" & cboCodigo.Text & ".jpg") Then
                                    File.Delete(My.Application.Info.DirectoryPath & "\ProductosImg" & base & "\" & cboCodigo.Text & ".jpg")
                                End If
                                picImagen.Image.Save(My.Application.Info.DirectoryPath & "\ProductosImg" & base & "\" & cboCodigo.Text & ".jpg", System.Drawing.Imaging.ImageFormat.Jpeg)
                            End If
                        End If

                        MsgBox("Producto actualizado correctamente.", vbInformation + vbOKOnly, "Delsscom Control Negocios Pro")
                        btnNuevo.PerformClick()
                    End If
                    cnn2.Close()
                End If
            Else
                'Inserta
                cnn2.Close() : cnn2.Open()

                cmd2 = cnn2.CreateCommand
                cmd2.CommandText =
                    "insert into Productos(Codigo,CodBarra,Nombre,NombreLargo,ProvPri,ProvEme,ProvRes,UCompra,UVenta,UMinima,MCD,Multiplo,Departamento,Grupo,Ubicacion,Min,Max,Comision,PrecioCompra,PrecioVenta,PrecioVentaIVA,IVA,Existencia,Fecha,pres_vol,id_tbMoneda,Promocion,Afecta_exis,PercentIVAret,Almacen3,IIEPS,ClaveSat,UnidadSat,Cargado,CargadoInv,Uso,Color,Genero,Marca,Articulo,Dia,Descu,Fecha_Inicial,Fecha_Final,Promo_Monedero,Unico,N_Serie,N_Serie2,GPrint) values('" & cboCodigo.Text & "','" & txtbarras.Text & "','" & cboNombre.Text & "','" & txtNombreL.Text & "','" & cboProvP.Text & "','" & cboProvE.Text & "'," & IIf(chkKIT.Checked, 1, 0) & ",'" & txtMaxima.Text & "','" & txtActual.Text & "','" & txtMinima.Text & "'," & mcd & "," & multiplo & ",'" & cboDepto.Text & "','" & cboGrupo.Text & "','" & cboubicacion.Text & "'," & minimo & "," & maximo & "," & comision & ",0,0,0," & cboIVA.Text & ",0,'" & fecha & "',0," & cboMoneda.Tag & ",0,0," & retIVA & ",0," & ieps & ",'" & txtCodigoSAT.Text & "','" & txtClaveSAT.Text & "',0,0,'','','','','',0,'0','" & fecha & "','" & fecha & "',0,0,'" & txtn_serie.Text & "','" & txt_Serie2.Text & "','" & cboComanda.Text & "')"
                If cmd2.ExecuteNonQuery Then

                    If (picImagen.Image Is Nothing) Then
                    Else
                        If txtrutaimagen.Text <> "" Then
                            If File.Exists(My.Application.Info.DirectoryPath & "\ProductosImg" & base & "\" & cboCodigo.Text & ".jpg") Then
                                File.Delete(My.Application.Info.DirectoryPath & "\ProductosImg" & base & "\" & cboCodigo.Text & ".jpg")
                            End If
                            picImagen.Image.Save(My.Application.Info.DirectoryPath & "\ProductosImg" & base & "\" & cboCodigo.Text & ".jpg", System.Drawing.Imaging.ImageFormat.Jpeg)
                        End If
                    End If

                    MsgBox("Producto registrado correctamente.", vbInformation + vbOKOnly, "Delsscom Control Negocios Pro")
                    btnNuevo.PerformClick()
                End If
                cnn2.Close()
            End If
            rd1.Close() : cnn1.Close()
        Catch ex As Exception
            MessageBox.Show(ex.ToString())
            cnn1.Close() : cnn2.Close()
        End Try
    End Sub

    Private Sub btnEliminar_Click(sender As System.Object, e As System.EventArgs) Handles btnEliminar.Click
        If cboCodigo.Text = "" Then MsgBox("Selecciona un producto para poder eliminarlo.", vbInformation + vbOKOnly, "Delsscom Control Negocios Pro") : cboCodigo.Focus().Equals(True) : Exit Sub
        If cboNombre.Text = "" Then MsgBox("Selecciona un producto para poder eliminarlo.", vbInformation + vbOKOnly, "Delsscom Control Negocios Pro") : cboNombre.Focus().Equals(True) : Exit Sub

        Dim cnn1 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
        Dim rd1 As MySqlDataReader
        Dim cmd1 As MySqlCommand

        Try
            cnn1.Close() : cnn1.Open()

            cmd1 = cnn1.CreateCommand
            cmd1.CommandText =
                "select Codigo from Productos where Codigo='" & cboCodigo.Text & "' and Nombre='" & cboNombre.Text & "'"
            rd1 = cmd1.ExecuteReader
            If Not rd1.HasRows Then
                MsgBox("No puedes elimnar un producto que no está dado de alta en el sistema.", vbInformation + vbOKOnly, "Delsscom Control Negocios Pro")
                rd1.Close()
                cnn1.Close()
                Exit Sub
            End If
            rd1.Close()

            cmd1 = cnn1.CreateCommand
            cmd1.CommandText =
                "delete from Productos where Codigo='" & cboCodigo.Text & "' and Nombre='" & cboNombre.Text & "'"
            If cmd1.ExecuteNonQuery Then

                If (picImagen.Image Is Nothing) Then
                Else
                    If txtrutaimagen.Text = "" Then
                        picImagen.Image.Dispose()
                        If File.Exists(My.Application.Info.DirectoryPath & "\ProductosImg" & base & "\" & cboCodigo.Text & ".jpg") Then
                            File.Delete(My.Application.Info.DirectoryPath & "\ProductosImg" & base & "\" & cboCodigo.Text & ".jpg")
                            picImagen.Image = Nothing
                        End If
                    Else
                        picImagen.Image = Nothing
                    End If
                End If

                MsgBox("Producto eliminado correctamente.", vbInformation + vbOKOnly, "Delsscom Control Negocios Pro")
                btnNuevo.PerformClick()
            End If

            cmd1 = cnn1.CreateCommand
            cmd1.CommandText =
                 "delete from Kits where Cod='" & cboCodigo.Text & "'"
            cmd1.ExecuteNonQuery()

            cmd1 = cnn1.CreateCommand
            cmd1.CommandText =
                 "delete from Kits wwhere Codigo='" & cboCodigo.Text & "'"
            cmd1.ExecuteNonQuery()

            cnn1.Close()
        Catch ex As Exception
            MessageBox.Show(ex.ToString())
            cnn1.Close()
        End Try
    End Sub

    Private Sub frmProductos_Shown(sender As Object, e As System.EventArgs) Handles Me.Shown
        cboNombre.Focus().Equals(True)
    End Sub

    Private Sub btnImportar_Click(sender As System.Object, e As System.EventArgs) Handles btnImportar.Click
        If MsgBox("Estas apunto de importar tu catálogo desde un archivo de Excel, para evitar errores asegúrate de que la hoja de Excel tiene el nombre de 'Hoja1' y creciórate de que el archivo está guardado y cerrado.", vbInformation + vbOKCancel, "Delsscom Control Negocios Pro") = vbOK Then
            Excel_Grid_SQL(grdcaptura)
        End If
    End Sub

    Private Sub Excel_Grid_SQL(ByVal tabla As DataGridView)
        Dim con As OleDb.OleDbConnection
        Dim dt As New System.Data.DataTable
        Dim ds As New DataSet
        Dim da As OleDb.OleDbDataAdapter
        Dim cuadro_dialogo As New OpenFileDialog
        Dim ruta As String = "", sheet As String = "hoja1"

        Dim cnn1 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
        Dim rd1 As MySqlDataReader
        Dim cmd1 As MySqlCommand

        With cuadro_dialogo
            .Filter = "Archivos de cálculo(*.xls;*.xlsx)|*.xls;*.xlsx"
            .Title = "Selecciona el archivo a importar"
            .Multiselect = False
            .InitialDirectory = My.Application.Info.DirectoryPath & "\Archivos de importación"
            .ShowDialog()
        End With

        Try
            If cuadro_dialogo.FileName.ToString() <> "" Then
                ruta = cuadro_dialogo.FileName.ToString()
                con = New OleDb.OleDbConnection(
                    "Provider=Microsoft.ACE.OLEDB.12.0;" &
                    " Data Source='" & ruta & "'; " &
                    "Extended Properties='Excel 12.0 Xml;HDR=Yes'")

                da = New OleDb.OleDbDataAdapter("select * from [" & sheet & "$]", con)

                con.Open()
                da.Fill(ds, "MyData")
                dt = ds.Tables("MyData")
                tabla.DataSource = ds
                tabla.DataMember = "MyData"
                con.Close()
            End If

            'Variables para alojar los datos de archivo de Excel
            Dim codigo, barras, nombrec, nombrel, proveedorp, proveedore, depto, grupo, unidad, clave_sat, unidad_sat, n_serie As String
            Dim mini, maxi, comision, iva, pcompra, pcompraiva, utilidad, pventasiva, pventaciva, existencia As Double
            Dim fecha As String = Format(Date.Now, "yyyy-MM-dd")
            Dim conteo As Integer = 0

            barsube.Value = 0
            barsube.Maximum = grdcaptura.Rows.Count

            cnn1.Close() : cnn1.Open()

            For zef As Integer = 0 To grdcaptura.Rows.Count - 1
                codigo = UCase(NulCad(grdcaptura.Rows(zef).Cells(0).Value.ToString()))
                If codigo = "" Then Exit For
                barras = NulCad(grdcaptura.Rows(zef).Cells(1).Value.ToString())
                nombrec = UCase(NulCad(grdcaptura.Rows(zef).Cells(2).Value.ToString()))
                nombrel = UCase(NulCad(grdcaptura.Rows(zef).Cells(3).Value.ToString()))
                proveedorp = UCase(NulCad(grdcaptura.Rows(zef).Cells(4).Value.ToString()))
                proveedore = UCase(NulCad(grdcaptura.Rows(zef).Cells(5).Value.ToString()))
                unidad = UCase(NulCad(grdcaptura.Rows(zef).Cells(6).Value.ToString()))
                depto = UCase(NulCad(grdcaptura.Rows(zef).Cells(7).Value.ToString()))
                grupo = UCase(NulCad(grdcaptura.Rows(zef).Cells(8).Value.ToString()))
                mini = NulVa(grdcaptura.Rows(zef).Cells(9).Value.ToString())
                maxi = NulVa(grdcaptura.Rows(zef).Cells(10).Value.ToString())
                comision = NulVa(grdcaptura.Rows(zef).Cells(11).Value.ToString())
                iva = NulVa(grdcaptura.Rows(zef).Cells(12).Value.ToString())
                pcompra = NulVa(grdcaptura.Rows(zef).Cells(13).Value.ToString())
                pcompraiva = CDbl(pcompra) * (1 + CDbl(iva))
                utilidad = NulVa(grdcaptura.Rows(zef).Cells(14).Value.ToString())
                pventasiva = NulVa(grdcaptura.Rows(zef).Cells(15).Value.ToString())
                pventaciva = NulVa(grdcaptura.Rows(zef).Cells(16).Value.ToString())
                clave_sat = NulCad(grdcaptura.Rows(zef).Cells(17).Value.ToString())
                unidad_sat = NulCad(grdcaptura.Rows(zef).Cells(18).Value.ToString())
                existencia = NulVa(grdcaptura.Rows(zef).Cells(19).Value.ToString())
                n_serie = NulCad(grdcaptura.Rows(zef).Cells(20).Value.ToString())

                If (Comprueba(codigo, nombrec, barras, proveedorp, proveedore, n_serie)) Then
                    If cnn1.State = 0 Then cnn1.Open()

                    cmd1 = cnn1.CreateCommand
                    cmd1.CommandText =
                        "insert into Productos(Codigo,CodBarra,Nombre,NombreLargo,ProvPri,ProvEme,ProvRes,UCompra,UVenta,UMinima,MCD,Multiplo,Departamento,Grupo,Ubicacion,MinMax,Comision,PrecioCompra,PrecioVenta,PrecioVentaIVA,IVA,Existencia,Porcentaje,Fecha,pres_vol,id_tbMoneda,Promocion,Afecta_exis,Almacen3,ClaveSat,UnidadSat,Cargado,CargadoInv,Uso,Color,Genero,Marca,Articulo,Dia,Descu,Fecha_Inicial,Fecha_Final,Promo_Monedero,Unico,N_Serie,Existencia) values('" & codigo & "','" & barras & "','" & nombrec & "','" & nombrel & "','" & proveedorp & "','" & proveedore & "',0,'" & unidad & "','" & unidad & "','" & unidad & "',1,1,'" & depto & "','" & grupo & "','',1,1,0," & pcompra & "," & pventasiva & "," & pventaciva & "," & iva & ",0," & utilidad & ",'" & fecha & "',0,1,0,0," & pcompra & ",'" & clave_sat & "','" & unidad_sat & "',0,0,'','','','','',0,'0','" & fecha & "','" & fecha & "',0,0,'" & n_serie & "'," & existencia & ")"
                    cmd1.ExecuteNonQuery()
                Else
                    conteo += 1
                    barsube.Value = conteo
                    Continue For
                End If
                conteo += 1
                barsube.Value = conteo
            Next
            cnn1.Close()
            MsgBox(conteo & " productos fueron importados correctamente.", vbInformation + vbOKOnly, "Delsscom Control Negocios Pro")
            tabla.DataSource = Nothing
            tabla.Dispose()
            grdcaptura.Rows.Clear()
            barsube.Value = 0
        Catch ex As Exception
            MessageBox.Show(ex.ToString())
            cnn1.Close()
        End Try
    End Sub

    Private Function NulCad(ByVal cadena As String) As String
        If IsDBNull(cadena) Then
            NulCad = ""
        Else
            NulCad = Replace(cadena, "'", "") : Replace(cadena, "*", "")
        End If
    End Function

    Private Function NulVa(ByVal cifra As Double) As Double
        If IsDBNull(cifra) Then
            NulVa = 0
        Else
            NulVa = cifra
        End If
    End Function

    Private Function Comprueba(ByVal cod As String, ByVal desc As String, ByVal codbarra As String, ByVal prov1 As String, ByVal prov2 As String, ByVal n_serie As String) As Boolean
        Dim valida As Boolean = True

        Dim cnn2 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
        Dim cnn3 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
        Dim rd2 As MySqlDataReader
        Dim cmd2, cmd3 As MySqlCommand

        cnn2.Close() : cnn2.Open()

        cmd2 = cnn2.CreateCommand
        cmd2.CommandText =
            "select Id from Proveedores where NComercial='" & prov1 & "'"
        rd2 = cmd2.ExecuteReader
        If rd2.HasRows Then
        Else
            cnn3.Close() : cnn3.Open() : cmd3 = cnn3.CreateCommand
            cmd3.CommandText =
                "insert into Proveedores(NComercial,Compania,RFC,CURP,Calle,Colonia,CP,Delegacion,Entidad,Telefono,Facebook,Correo,Saldo,Credito,DiasCred) values('" & prov1 & "','" & prov1 & "','','','','','','','','','','',0,0,0)"
            cmd3.ExecuteNonQuery() : cnn3.Close()
        End If
        rd2.Close()

        cmd2 = cnn2.CreateCommand
        cmd2.CommandText =
            "select Id from Proveedores where NComercial='" & prov2 & "'"
        rd2 = cmd2.ExecuteReader
        If rd2.HasRows Then
        Else
            cnn3.Close() : cnn3.Open() : cmd3 = cnn3.CreateCommand
            cmd3.CommandText =
                "insert into Proveedores(NComercial,Compania,RFC,CURP,Calle,Colonia,CP,Delegacion,Entidad,Telefono,Facebook,Correo,Saldo,Credito,DiasCred) values('" & prov2 & "','" & prov2 & "','','','','','','','','','','',0,0,0)"
            cmd3.ExecuteNonQuery() : cnn3.Close()
        End If
        rd2.Close()

        cmd2 = cnn2.CreateCommand
        cmd2.CommandText =
            "select Codigo from Productos where Codigo='" & cod & "'"
        rd2 = cmd2.ExecuteReader
        If rd2.HasRows Then
            If rd2.Read Then
                MsgBox("Ya cuentas con un producto registrado con el código corto " & cod & ".", vbInformation + vbOKOnly, "Delsscom Control Negocios Pro")
                valida = False
            End If
        End If
        rd2.Close()

        If codbarra <> "" Then
            cmd2 = cnn2.CreateCommand
            cmd2.CommandText =
                "select CodBarra from Productos where CodBarra='" & codbarra & "'"
            rd2 = cmd2.ExecuteReader
            If rd2.HasRows Then
                If rd2.Read Then
                    MsgBox("Ya cuentas con un producto registrado con el código de barras " & codbarra & ".", vbInformation + vbOKOnly, "Delsscom Control Negocios Pro")
                    valida = False
                End If
            End If
            rd2.Close()
        End If

        cmd2 = cnn2.CreateCommand
        cmd2.CommandText =
            "select Nombre from Productos where Nombre='" & desc & "'"
        rd2 = cmd2.ExecuteReader
        If rd2.HasRows Then
            If rd2.Read Then
                MsgBox("Ya cuentas con un producto registrado con el nombre " & desc & ".", vbInformation + vbOKOnly, "Delsscom Control Negocios Pro")
                valida = False
            End If
        End If
        rd2.Close()

        If n_serie = "" Then
        Else
            cmd2 = cnn2.CreateCommand
            cmd2.CommandText =
                "select N_Serie from Productos where N_Serie='" & n_serie & "'"
            rd2 = cmd2.ExecuteReader
            If rd2.HasRows Then
                If rd2.Read Then
                    MsgBox("Ya cuentas con un producto registrado con el número de parte " & n_serie & ".", vbInformation + vbOKOnly, "Delsscom Control Negocios Pro")
                    valida = False
                End If
            End If
            rd2.Close()
        End If

        cnn2.Close()
        Return valida
    End Function

    Private Sub txtn_serie_Click(sender As Object, e As EventArgs) Handles txtn_serie.Click
        txtn_serie.SelectionStart = 0
        txtn_serie.SelectionLength = Len(txtn_serie.Text)
    End Sub

    Private Sub txtn_serie_GotFocus(sender As Object, e As EventArgs) Handles txtn_serie.GotFocus
        txtn_serie.SelectionStart = 0
        txtn_serie.SelectionLength = Len(txtn_serie.Text)
    End Sub

    Private Sub txtn_serie_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtn_serie.KeyPress
        If AscW(e.KeyChar) = Keys.Enter Then
            If txtn_serie.Text = "" Then
            Else
                TraeDatos("SERIE")
            End If
            txt_Serie2.Focus().Equals(True)
        End If
    End Sub

    Private Sub cboubicacion_DropDown(sender As Object, e As EventArgs) Handles cboubicacion.DropDown
        cboubicacion.Items.Clear()

        Dim cnn1 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
        Dim rd1 As MySqlDataReader
        Dim cmd1 As MySqlCommand
        Try
            cnn1.Close() : cnn1.Open()

            cmd1 = cnn1.CreateCommand()
            cmd1.CommandText =
                "select distinct Ubicacion from Productos order by Ubicacion"
            rd1 = cmd1.ExecuteReader
            Do While rd1.Read
                If rd1.HasRows Then cboubicacion.Items.Add(rd1(0).ToString())
            Loop
            rd1.Close() : cnn1.Close()
        Catch ex As Exception
            MessageBox.Show(ex.ToString())
            cnn1.Close()
        End Try
    End Sub

    Private Sub cboubicacion_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cboubicacion.KeyPress
        e.KeyChar = UCase(e.KeyChar)
        If AscW(e.KeyChar) = Keys.Enter Then
            txtRetIVA.Focus().Equals(True)
        End If
    End Sub

    Private Sub cboComanda_DropDown(sender As Object, e As EventArgs) Handles cboComanda.DropDown
        Dim cnn5 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
        Dim rd5 As MySqlDataReader
        Dim cmd5 As MySqlCommand
        Try
            cnn5.Close() : cnn5.Open()
            cmd5 = cnn5.CreateCommand
            cmd5.CommandText = "SELECT DISTINCT GPrint FROM Productos WHERE GPrint<>'' ORDER BY GPrint"
            rd5 = cmd5.ExecuteReader
            Do While rd5.Read
                If rd5.HasRows Then
                    cboComanda.Items.Add(rd5(0).ToString)
                End If
            Loop
            rd5.Close()
            cnn5.Close()

        Catch ex As Exception
            MessageBox.Show(ex.ToString)
            cnn5.Close()
        End Try
    End Sub

    Private Sub cboComanda_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cboComanda.KeyPress
        If AscW(e.KeyChar) = Keys.Enter Then
            txtClaveSAT.Focus().Equals(True)
        End If
    End Sub

    Private Sub txt_Serie2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_Serie2.KeyPress

        If AscW(e.KeyChar) = Keys.Enter Then
            If txt_Serie2.Text = "" Then
            Else
                TraeDatos("SERIE2")
            End If
            txtNombreL.Focus().Equals(True)
        End If
    End Sub


End Class