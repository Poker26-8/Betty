Imports ClosedXML.Excel
Imports MySql.Data.MySqlClient

Public Class frmRepCuentas
    Private Sub btnEliminar_Click(sender As Object, e As EventArgs) Handles btnEliminar.Click
        grdcaptura.Rows.Clear()

        Dim m1 As Date = mCalendar1.SelectionStart.ToShortDateString()
        Dim m2 As Date = mCalendar2.SelectionStart.ToShortDateString()


        Dim cnn1 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
        Dim cnn2 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
        Dim cnn3 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
        Dim rd1, rd2, rd3 As MySqlDataReader
        Dim cmd1, cmd2, cmd3 As MySqlCommand

        Dim cuantas As Integer = 0
        txtcobrar.Text = "0.00"
        If (opttodos.Checked) Then



            Try
                cnn1.Close() : cnn1.Open()

                cmd1 = cnn1.CreateCommand
                cmd1.CommandText =
                    "select count(Id) from Alumnos"
                rd1 = cmd1.ExecuteReader
                If rd1.HasRows Then
                    If rd1.Read Then
                        cuantas = rd1(0).ToString()
                    End If
                End If
                rd1.Close()

                barcarga.Visible = True
                barcarga.Value = 0
                barcarga.Maximum = cuantas + 1

                cnn2.Close() : cnn2.Open()

                Dim acuenta As Double = 0
                Dim resta As Double = 0

                cmd1 = cnn1.CreateCommand
                cmd1.CommandText =
                    "select Nombre,Matricula,Grupo,Curso,Inscripcion,Baja from Alumnos order by Nombre"
                rd1 = cmd1.ExecuteReader
                Do While rd1.Read
                    If rd1.HasRows Then
                        Dim alumno As String = rd1("Nombre").ToString()
                        Dim matricula As Integer = rd1("Matricula").ToString()
                        Dim grupo As String = rd1("Grupo").ToString()
                        Dim curso As String = rd1("Curso").ToString()
                        Dim inscripcion As String = rd1("Inscripcion").ToString()
                        Dim baja As String = rd1("Baja").ToString()

                        cmd2 = cnn2.CreateCommand
                        cmd2.CommandText =
                            "select SUM(ACuenta), SUM(Resta) from Ventas where Cliente='" & alumno & "'"
                        rd2 = cmd2.ExecuteReader
                        If rd2.HasRows Then
                            If rd2.Read Then
                                acuenta = IIf(rd2(0).ToString() = "", 0, rd2(0).ToString())
                                resta = IIf(rd2(1).ToString() = "", 0, rd2(1).ToString())
                            End If
                        End If
                        rd2.Close()

                        grdcaptura.Rows.Add(matricula, alumno, grupo, curso, FormatNumber(acuenta, 2), FormatNumber(resta, 2), inscripcion, baja)
                        barcarga.Value += 1
                        txtcobrar.Text = CDbl(txtcobrar.Text) + resta
                        My.Application.DoEvents()
                    End If
                Loop
                rd1.Close() : cnn1.Close()
                cnn2.Close()
            Catch ex As Exception
                MessageBox.Show(ex.ToString())
                cnn1.Close()
            End Try
            barcarga.Visible = False
            barcarga.Value = 0
            txtcobrar.Text = FormatNumber(txtcobrar.Text, 2)
        End If

        If (optgrupo.Checked) Then
            If cbo.Text = "" Then MsgBox("Selecciona un grupo para continuar.", vbInformation + vbOKOnly, "Delsscom Control Negocios Pro") : cbo.Focus().Equals(True) : Exit Sub
            Try
                cnn1.Close() : cnn1.Open()

                cmd1 = cnn1.CreateCommand
                cmd1.CommandText =
                    "select count(Id) from Alumnos where Grupo='" & cbo.Text & "'"
                rd1 = cmd1.ExecuteReader
                If rd1.HasRows Then
                    If rd1.Read Then
                        cuantas = rd1(0).ToString()
                    End If
                End If
                rd1.Close()

                barcarga.Visible = True
                barcarga.Value = 0
                barcarga.Maximum = cuantas + 1

                cnn2.Close() : cnn2.Open()

                Dim acuenta As Double = 0
                Dim resta As Double = 0

                cmd1 = cnn1.CreateCommand
                cmd1.CommandText =
                    "select Nombre,Matricula,Inscripcion,Baja from Alumnos where Grupo='" & cbo.Text & "' order by Nombre"
                rd1 = cmd1.ExecuteReader
                Do While rd1.Read
                    If rd1.HasRows Then
                        Dim alumno As String = rd1("Nombre").ToString()
                        Dim matricula As Integer = rd1("Matricula").ToString()
                        Dim inscripcion As String = rd1("Inscripcion").ToString()
                        Dim baja As String = rd1("Baja").ToString()

                        cmd2 = cnn2.CreateCommand
                        cmd2.CommandText =
                            "select SUM(ACuenta), SUM(Resta) from Ventas where Cliente='" & alumno & "'"
                        rd2 = cmd2.ExecuteReader
                        If rd2.HasRows Then
                            If rd2.Read Then
                                acuenta = IIf(rd2(0).ToString() = "", 0, rd2(0).ToString())
                                resta = IIf(rd2(1).ToString() = "", 0, rd2(1).ToString())
                            End If
                        End If
                        rd2.Close()

                        grdcaptura.Rows.Add(matricula, alumno, FormatNumber(acuenta, 2), FormatNumber(resta, 2), inscripcion, baja)
                        barcarga.Value += 1
                        txtcobrar.Text = CDbl(txtcobrar.Text) + resta
                        My.Application.DoEvents()
                    End If
                Loop
                rd1.Close() : cnn1.Close()
                cnn2.Close()
            Catch ex As Exception
                MessageBox.Show(ex.ToString())
                cnn1.Close()
            End Try
            barcarga.Visible = False
            barcarga.Value = 0
            txtcobrar.Text = FormatNumber(txtcobrar.Text, 2)
        End If

        If (optestadocuenta.Checked) Then
            If cbo.Text = "" Then MsgBox("Selecciona un alumno para continuar.", vbInformation + vbOKOnly, "Delsscom Control Negocios Pro") : cbo.Focus().Equals(True) : Exit Sub

            Try
                cnn1.Close() : cnn1.Open()

                cmd1 = cnn1.CreateCommand
                cmd1.CommandText =
                    "select count(Id) from AbonoI where Cliente='" & cbo.Text & "' and Fecha between '" & Format(m1, "yyyy-MM-dd") & "' and '" & Format(m2, "yyyy-MM-dd") & "'"
                rd1 = cmd1.ExecuteReader
                If rd1.HasRows Then
                    If rd1.Read Then
                        cuantas = rd1(0).ToString()
                    End If
                End If
                rd1.Close()

                barcarga.Visible = True
                barcarga.Value = 0
                barcarga.Maximum = cuantas + 1

                Dim acuenta As Double = 0
                Dim resta As Double = 0

                cnn2.Close() : cnn2.Open()

                cmd1 = cnn1.CreateCommand
                cmd1.CommandText =
                    "select NumFolio,Concepto,Cargo,Abono,Saldo,Fecha from AbonoI where Cliente='" & cbo.Text & "'  and Fecha between '" & Format(m1, "yyyy-MM-dd") & "' and '" & Format(m2, "yyyy-MM-dd") & "' order by Id"
                rd1 = cmd1.ExecuteReader
                Do While rd1.Read
                    If rd1.HasRows Then
                        Dim Folio As String = rd1("NumFolio").ToString()
                        Dim concepto As String = rd1("Concepto").ToString()
                        Dim cargo As Double = rd1("Cargo").ToString()
                        Dim abono As Double = rd1("Abono").ToString()
                        Dim saldo As Double = rd1("Saldo").ToString()
                        Dim fecha As String = rd1("Fecha").ToString()

                        Dim concepto_pago As String = ""

                        cmd2 = cnn2.CreateCommand
                        cmd2.CommandText =
                            "select Nombre from VentasDetalle where Folio=" & Folio
                        rd2 = cmd2.ExecuteReader
                        If rd2.HasRows Then
                            If rd2.Read Then
                                concepto_pago = rd2("Nombre").ToString()
                            End If
                        End If
                        rd2.Close()

                        grdcaptura.Rows.Add(Folio, concepto_pago, concepto, FormatNumber(cargo, 2), FormatNumber(abono, 2), FormatNumber(saldo), fecha)
                        barcarga.Value += 1
                        My.Application.DoEvents()
                    End If
                Loop
                rd1.Close()

                cnn2.Close()

                cmd1 = cnn1.CreateCommand
                cmd1.CommandText =
                            "select Saldo from AbonoI where Id=(select MAX(Id) from AbonoI where Cliente='" & cbo.Text & "')"
                rd1 = cmd1.ExecuteReader
                If rd1.HasRows Then
                    If rd1.Read Then
                        resta = rd1("Saldo").ToString()
                    End If
                End If
                rd1.Close()
                cnn1.Close()

                barcarga.Visible = False
                barcarga.Value = 0
                txtcobrar.Text = FormatNumber(resta, 2)
            Catch ex As Exception
                MessageBox.Show(ex.ToString())
                cnn1.Close()
            End Try
        End If

        If (optpendientes_alumno.Checked) Then
            If cbo.Text = "" Then MsgBox("Selecciona un alumno para continuar.", vbInformation + vbOKOnly, "Delsscom Control Negocios Pro") : cbo.Focus().Equals(True) : Exit Sub
            Try
                cnn1.Close() : cnn1.Open()

                cmd1 = cnn1.CreateCommand
                cmd1.CommandText =
                    "select count(Folio) from Ventas where Cliente='" & cbo.Text & "' and Resta>0 and FPago between '" & Format(m1, "yyyy-MM-dd") & "' and '" & Format(m2, "yyyy-MM-dd") & "'"
                rd1 = cmd1.ExecuteReader
                If rd1.HasRows Then
                    If rd1.Read Then
                        cuantas = rd1(0).ToString()
                    End If
                End If
                rd1.Close()

                barcarga.Visible = True
                barcarga.Value = 0
                barcarga.Maximum = cuantas + 1

                cnn2.Close() : cnn2.Open()

                Dim acuenta As Double = 0
                Dim resta As Double = 0

                cmd1 = cnn1.CreateCommand
                cmd1.CommandText =
                    "select Folio,ACuenta,Resta from Ventas where Cliente='" & cbo.Text & "' and Resta>0 and FPago between '" & Format(m1, "yyyy-MM-dd") & "' and '" & Format(m2, "yyyy-MM-dd") & "'"
                rd1 = cmd1.ExecuteReader
                Do While rd1.Read
                    If rd1.HasRows Then
                        Dim folio As Integer = rd1("Folio").ToString()
                        acuenta = rd1("ACuenta").ToString()
                        resta = rd1("Resta").ToString()
                        Dim concepto As String = ""

                        cmd2 = cnn2.CreateCommand
                        cmd2.CommandText =
                            "select Nombre from VentasDetalle where Folio=" & folio & ""
                        rd2 = cmd2.ExecuteReader
                        If rd2.HasRows Then
                            If rd2.Read Then
                                concepto = rd2("Nombre").ToString()
                            End If
                        End If
                        rd2.Close()

                        grdcaptura.Rows.Add(folio, concepto, FormatNumber(acuenta, 2), FormatNumber(resta, 2))
                        barcarga.Value += 1
                        txtcobrar.Text = CDbl(txtcobrar.Text) + resta
                        My.Application.DoEvents()
                    End If
                Loop
                rd1.Close() : cnn1.Close()
                cnn2.Close()
            Catch ex As Exception
                MessageBox.Show(ex.ToString())
                cnn1.Close()
            End Try
            barcarga.Visible = False
            barcarga.Value = 0
            txtcobrar.Text = FormatNumber(txtcobrar.Text, 2)
        End If

        If (optpendientes_grupo.Checked) Then
            If cbo.Text = "" Then MsgBox("Selecciona un grupo para continuar.", vbInformation + vbOKOnly, "Delsscom Control Negocios Pro") : cbo.Focus().Equals(True) : Exit Sub
            Try
                cnn1.Close() : cnn1.Open()

                cmd1 = cnn1.CreateCommand
                cmd1.CommandText =
                    "select count(Id) from Alumnos where Grupo='" & cbo.Text & "'"
                rd1 = cmd1.ExecuteReader
                If rd1.HasRows Then
                    If rd1.Read Then
                        cuantas = rd1(0).ToString()
                    End If
                End If
                rd1.Close()

                barcarga.Visible = True
                barcarga.Value = 0
                barcarga.Maximum = cuantas + 1

                cnn2.Close() : cnn2.Open()
                cnn3.Close() : cnn3.Open()

                Dim acuenta As Double = 0
                Dim resta As Double = 0

                cmd1 = cnn1.CreateCommand
                cmd1.CommandText =
                    "select Nombre from Alumnos where Grupo='" & cbo.Text & "'"
                rd1 = cmd1.ExecuteReader
                Do While rd1.Read
                    If rd1.HasRows Then
                        Dim alumno As String = rd1("Nombre").ToString()

                        cmd2 = cnn2.CreateCommand
                        cmd2.CommandText =
                            "select Folio,ACuenta,Resta from Ventas where Cliente='" & alumno & "' and Resta>0 and FPago between '" & Format(m1, "yyyy-MM-dd") & "' and '" & Format(m2, "yyyy-MM-dd") & "'"
                        rd2 = cmd2.ExecuteReader
                        Do While rd2.Read
                            If rd2.HasRows Then
                                Dim folio As Integer = rd2("Folio").ToString()
                                acuenta = rd2("ACuenta").ToString()
                                resta = rd2("Resta").ToString()
                                Dim concepto As String = ""

                                cmd3 = cnn3.CreateCommand
                                cmd3.CommandText =
                                    "select Nombre from VentasDetalle where Folio=" & folio & ""
                                rd3 = cmd3.ExecuteReader
                                If rd3.HasRows Then
                                    If rd3.Read Then
                                        concepto = rd3("Nombre").ToString()
                                    End If
                                End If
                                rd3.Close()

                                grdcaptura.Rows.Add(folio, alumno, concepto, FormatNumber(acuenta, 2), FormatNumber(resta, 2))
                                barcarga.Value += 1
                                txtcobrar.Text = CDbl(txtcobrar.Text) + resta
                                My.Application.DoEvents()
                            End If
                        Loop
                        rd2.Close()
                    End If
                Loop
                rd1.Close() : cnn1.Close()
                cnn2.Close()
            Catch ex As Exception
                MessageBox.Show(ex.ToString())
                cnn1.Close()
            End Try
            barcarga.Visible = False
            barcarga.Value = 0
            txtcobrar.Text = FormatNumber(txtcobrar.Text, 2)
        End If

        If (RadioButton2.Checked) Then
            Try
                cnn1.Close() : cnn1.Open()

                cmd1 = cnn1.CreateCommand
                cmd1.CommandText =
                    "select count(Id) from Alumnos where Inscripcion between '" & Format(m1, "yyyy-MM-dd") & "' and '" & Format(m2, "yyyy-MM-dd") & "'"
                rd1 = cmd1.ExecuteReader
                If rd1.HasRows Then
                    If rd1.Read Then
                        cuantas = rd1(0).ToString()
                    End If
                End If
                rd1.Close()

                barcarga.Visible = True
                barcarga.Value = 0
                barcarga.Maximum = cuantas + 1

                cnn2.Close() : cnn2.Open()

                Dim acuenta As Double = 0
                Dim resta As Double = 0

                cmd1 = cnn1.CreateCommand
                cmd1.CommandText =
                    "select Nombre,Matricula,Grupo,Curso,Inscripcion from Alumnos where Inscripcion between '" & Format(m1, "yyyy-MM-dd") & "' and '" & Format(m2, "yyyy-MM-dd") & "' order by Nombre"
                rd1 = cmd1.ExecuteReader
                Do While rd1.Read
                    If rd1.HasRows Then
                        Dim alumno As String = rd1("Nombre").ToString()
                        Dim matricula As Integer = rd1("Matricula").ToString()
                        Dim grupo As String = rd1("Grupo").ToString()
                        Dim curso As String = rd1("Curso").ToString()
                        Dim inscripcion As String = rd1("Inscripcion").ToString()

                        grdcaptura.Rows.Add(matricula, alumno, grupo, curso, FormatDateTime(inscripcion, DateFormat.ShortDate))
                        barcarga.Value += 1
                        My.Application.DoEvents()
                    End If
                Loop
                rd1.Close() : cnn1.Close()
                cnn2.Close()
            Catch ex As Exception
                MessageBox.Show(ex.ToString())
                cnn1.Close()
            End Try
            barcarga.Visible = False
            barcarga.Value = 0
        End If

        If (RadioButton1.Checked) Then
            Try
                cnn1.Close() : cnn1.Open()

                cmd1 = cnn1.CreateCommand
                cmd1.CommandText =
                    "select count(Id) from Alumnos where Baja between '" & Format(m1, "yyyy-MM-dd") & "' and '" & Format(m2, "yyyy-MM-dd") & "'"
                rd1 = cmd1.ExecuteReader
                If rd1.HasRows Then
                    If rd1.Read Then
                        cuantas = rd1(0).ToString()
                    End If
                End If
                rd1.Close()

                barcarga.Visible = True
                barcarga.Value = 0
                barcarga.Maximum = cuantas + 1

                cnn2.Close() : cnn2.Open()

                Dim acuenta As Double = 0
                Dim resta As Double = 0

                cmd1 = cnn1.CreateCommand
                cmd1.CommandText =
                    "select Nombre,Matricula,Grupo,Curso,Baja from Alumnos where Baja between '" & Format(m1, "yyyy-MM-dd") & "' and '" & Format(m2, "yyyy-MM-dd") & "' order by Nombre"
                rd1 = cmd1.ExecuteReader
                Do While rd1.Read
                    If rd1.HasRows Then
                        Dim alumno As String = rd1("Nombre").ToString()
                        Dim matricula As Integer = rd1("Matricula").ToString()
                        Dim grupo As String = rd1("Grupo").ToString()
                        Dim curso As String = rd1("Curso").ToString()
                        Dim inscripcion As Date = rd1("Baja").ToString()

                        grdcaptura.Rows.Add(matricula, alumno, grupo, curso, Format(inscripcion, "dd/MM/yyyy"))
                        barcarga.Value += 1
                        My.Application.DoEvents()
                    End If
                Loop
                rd1.Close() : cnn1.Close()
                cnn2.Close()
            Catch ex As Exception
                MessageBox.Show(ex.ToString())
                cnn1.Close()
            End Try
            barcarga.Visible = False
            barcarga.Value = 0
        End If
    End Sub

    Private Sub opttodos_CheckedChanged(sender As Object, e As EventArgs) Handles opttodos.CheckedChanged
        If (opttodos.Checked) Then
            grdcaptura.Rows.Clear()
            grdcaptura.ColumnCount = 0
            cbo.Text = ""
            cbo.Visible = False
            cbo.Focus().Equals(True)

            grdcaptura.ColumnCount = 8
            With grdcaptura
                With .Columns(0)
                    .HeaderText = "N. Matricula"
                    .Width = 80
                    .AutoSizeMode = DataGridViewAutoSizeColumnMode.NotSet
                    .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                    .Visible = True
                End With
                With .Columns(1)
                    .HeaderText = "Alumno"
                    .Width = 300
                    .AutoSizeMode = DataGridViewAutoSizeColumnMode.NotSet
                    .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                    .Visible = True
                End With
                With .Columns(2)
                    .HeaderText = "Grupo"
                    .Width = 100
                    .AutoSizeMode = DataGridViewAutoSizeColumnMode.NotSet
                    .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                    .Visible = True
                End With
                With .Columns(3)
                    .HeaderText = "Curso"
                    .Width = 270
                    .AutoSizeMode = DataGridViewAutoSizeColumnMode.NotSet
                    .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                    .Visible = True
                End With
                With .Columns(4)
                    .HeaderText = "A cuenta"
                    .Width = 75
                    .AutoSizeMode = DataGridViewAutoSizeColumnMode.NotSet
                    .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                    .Visible = True
                End With
                With .Columns(5)
                    .HeaderText = "Resta"
                    .Width = 75
                    .AutoSizeMode = DataGridViewAutoSizeColumnMode.NotSet
                    .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                    .Visible = True
                End With
                With .Columns(6)
                    .HeaderText = "Inscripción"
                    .Width = 110
                    .AutoSizeMode = DataGridViewAutoSizeColumnMode.NotSet
                    .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                    .Visible = True
                End With
                With .Columns(7)
                    .HeaderText = "Baja"
                    .Width = 110
                    .AutoSizeMode = DataGridViewAutoSizeColumnMode.NotSet
                    .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                    .Visible = True
                End With
            End With
        End If
    End Sub

    Private Sub optestadocuenta_CheckedChanged(sender As Object, e As EventArgs) Handles optestadocuenta.CheckedChanged
        If (optestadocuenta.Checked) Then
            grdcaptura.Rows.Clear()
            grdcaptura.ColumnCount = 0
            cbo.Text = ""
            cbo.Visible = True
            cbo.Focus().Equals(True)

            grdcaptura.ColumnCount = 7
            With grdcaptura
                With .Columns(0)
                    .HeaderText = "Folio"
                    .Width = 80
                    .AutoSizeMode = DataGridViewAutoSizeColumnMode.NotSet
                    .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                    .Visible = True
                    .Resizable = DataGridViewTriState.False
                End With
                With .Columns(1)
                    .HeaderText = "Descripción"
                    .Width = 300
                    .AutoSizeMode = DataGridViewAutoSizeColumnMode.NotSet
                    .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                    .Visible = True
                    .Resizable = DataGridViewTriState.False
                End With
                With .Columns(2)
                    .HeaderText = "Concepto"
                    .Width = 100
                    .AutoSizeMode = DataGridViewAutoSizeColumnMode.NotSet
                    .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                    .Visible = True
                    .Resizable = DataGridViewTriState.False
                End With
                With .Columns(3)
                    .HeaderText = "Cargo"
                    .Width = 75
                    .AutoSizeMode = DataGridViewAutoSizeColumnMode.NotSet
                    .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                    .Visible = True
                    .Resizable = DataGridViewTriState.False
                End With
                With .Columns(4)
                    .HeaderText = "Abono"
                    .Width = 75
                    .AutoSizeMode = DataGridViewAutoSizeColumnMode.NotSet
                    .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                    .Visible = True
                    .Resizable = DataGridViewTriState.False
                End With
                With .Columns(5)
                    .HeaderText = "Saldo"
                    .Width = 75
                    .AutoSizeMode = DataGridViewAutoSizeColumnMode.NotSet
                    .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                    .Visible = True
                    .Resizable = DataGridViewTriState.False
                End With
                With .Columns(6)
                    .HeaderText = "Fecha"
                    .Width = 100
                    .AutoSizeMode = DataGridViewAutoSizeColumnMode.NotSet
                    .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                    .Visible = True
                    .Resizable = DataGridViewTriState.False
                End With
            End With
        End If
    End Sub

    Private Sub cbo_DropDown(sender As Object, e As EventArgs) Handles cbo.DropDown
        cbo.Items.Clear()

        Dim cnn1 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
        Dim rd1 As MySqlDataReader
        Dim cmd1 As MySqlCommand

        If (optpendientes_grupo.Checked) Or (optgrupo.Checked) Then
            Try
                cnn1.Close() : cnn1.Open()

                cmd1 = cnn1.CreateCommand
                cmd1.CommandText =
                    "select distinct Nombre from Grupos"
                rd1 = cmd1.ExecuteReader
                Do While rd1.Read
                    If rd1.HasRows Then
                        cbo.Items.Add(rd1(0).ToString())
                    End If
                Loop
                rd1.Close()
                cnn1.Close()
            Catch ex As Exception
                MessageBox.Show(ex.ToString())
                cnn1.Close()
            End Try
        Else
            Try
                cnn1.Close() : cnn1.Open()

                cmd1 = cnn1.CreateCommand
                cmd1.CommandText =
                    "select distinct Nombre from Alumnos where status=1 order by Nombre"
                rd1 = cmd1.ExecuteReader
                Do While rd1.Read
                    If rd1.HasRows Then
                        cbo.Items.Add(rd1(0).ToString())
                    End If
                Loop
                rd1.Close()
                cnn1.Close()
            Catch ex As Exception
                MessageBox.Show(ex.ToString())
                cnn1.Close()
            End Try
        End If
    End Sub

    Private Sub cbo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cbo.KeyPress
        If AscW(e.KeyChar) = Keys.Enter Then
            btnEliminar.Focus().Equals(True)
        End If
    End Sub

    Private Sub optpendientes_alumno_CheckedChanged(sender As Object, e As EventArgs) Handles optpendientes_alumno.CheckedChanged
        If (optpendientes_alumno.Checked) Then
            grdcaptura.Rows.Clear()
            grdcaptura.ColumnCount = 0
            cbo.Text = ""
            cbo.Visible = True
            cbo.Focus().Equals(True)

            grdcaptura.ColumnCount = 4
            With grdcaptura
                With .Columns(0)
                    .HeaderText = "Folio"
                    .Width = 80
                    .AutoSizeMode = DataGridViewAutoSizeColumnMode.NotSet
                    .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                    .Visible = True
                    .Resizable = DataGridViewTriState.False
                End With
                With .Columns(1)
                    .HeaderText = "Concepto"
                    .Width = 300
                    .AutoSizeMode = DataGridViewAutoSizeColumnMode.NotSet
                    .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                    .Visible = True
                    .Resizable = DataGridViewTriState.False
                End With
                With .Columns(2)
                    .HeaderText = "A cuenta"
                    .Width = 80
                    .AutoSizeMode = DataGridViewAutoSizeColumnMode.NotSet
                    .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                    .Visible = True
                    .Resizable = DataGridViewTriState.False
                End With
                With .Columns(3)
                    .HeaderText = "Resta"
                    .Width = 80
                    .AutoSizeMode = DataGridViewAutoSizeColumnMode.NotSet
                    .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                    .Visible = True
                    .Resizable = DataGridViewTriState.False
                End With
            End With
        End If
    End Sub

    Private Sub optpendientes_grupo_CheckedChanged(sender As Object, e As EventArgs) Handles optpendientes_grupo.CheckedChanged
        If (optpendientes_grupo.Checked) Then
            grdcaptura.Rows.Clear()
            grdcaptura.ColumnCount = 0
            cbo.Text = ""
            cbo.Visible = True
            cbo.Focus().Equals(True)

            grdcaptura.ColumnCount = 5
            With grdcaptura
                With .Columns(0)
                    .HeaderText = "Folio"
                    .Width = 80
                    .AutoSizeMode = DataGridViewAutoSizeColumnMode.NotSet
                    .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                    .Visible = True
                    .Resizable = DataGridViewTriState.False
                End With
                With .Columns(1)
                    .HeaderText = "Alumno"
                    .Width = 300
                    .AutoSizeMode = DataGridViewAutoSizeColumnMode.NotSet
                    .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                    .Visible = True
                    .Resizable = DataGridViewTriState.False
                End With
                With .Columns(2)
                    .HeaderText = "Concepto"
                    .Width = 300
                    .AutoSizeMode = DataGridViewAutoSizeColumnMode.NotSet
                    .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                    .Visible = True
                    .Resizable = DataGridViewTriState.False
                End With
                With .Columns(3)
                    .HeaderText = "A cuenta"
                    .Width = 80
                    .AutoSizeMode = DataGridViewAutoSizeColumnMode.NotSet
                    .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                    .Visible = True
                    .Resizable = DataGridViewTriState.False
                End With
                With .Columns(4)
                    .HeaderText = "Resta"
                    .Width = 80
                    .AutoSizeMode = DataGridViewAutoSizeColumnMode.NotSet
                    .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                    .Visible = True
                    .Resizable = DataGridViewTriState.False
                End With
            End With
        End If
    End Sub

    Private Sub optgrupo_CheckedChanged(sender As Object, e As EventArgs) Handles optgrupo.CheckedChanged
        If (optgrupo.Checked) Then
            grdcaptura.Rows.Clear()
            grdcaptura.ColumnCount = 0
            cbo.Text = ""
            cbo.Visible = True
            cbo.Focus().Equals(True)

            grdcaptura.ColumnCount = 6
            With grdcaptura
                With .Columns(0)
                    .HeaderText = "Matricula"
                    .Width = 80
                    .AutoSizeMode = DataGridViewAutoSizeColumnMode.NotSet
                    .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                    .Visible = True
                    .Resizable = DataGridViewTriState.False
                End With
                With .Columns(1)
                    .HeaderText = "Alumno"
                    .Width = 300
                    .AutoSizeMode = DataGridViewAutoSizeColumnMode.NotSet
                    .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                    .Visible = True
                    .Resizable = DataGridViewTriState.False
                End With
                With .Columns(2)
                    .HeaderText = "A cuenta"
                    .Width = 80
                    .AutoSizeMode = DataGridViewAutoSizeColumnMode.NotSet
                    .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                    .Visible = True
                    .Resizable = DataGridViewTriState.False
                End With
                With .Columns(3)
                    .HeaderText = "Resta"
                    .Width = 80
                    .AutoSizeMode = DataGridViewAutoSizeColumnMode.NotSet
                    .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                    .Visible = True
                    .Resizable = DataGridViewTriState.False
                End With
                With .Columns(4)
                    .HeaderText = "Inscripción"
                    .Width = 110
                    .AutoSizeMode = DataGridViewAutoSizeColumnMode.NotSet
                    .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                    .Visible = True
                    .Resizable = DataGridViewTriState.False
                End With
                With .Columns(5)
                    .HeaderText = "Baja"
                    .Width = 110
                    .AutoSizeMode = DataGridViewAutoSizeColumnMode.NotSet
                    .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                    .Visible = True
                    .Resizable = DataGridViewTriState.False
                End With
            End With
        End If
    End Sub

    Private Sub btnexportar_Click(sender As Object, e As EventArgs) Handles btnexportar.Click
        ExportarDataGridViewAExcel(grdcaptura)
    End Sub

    Public Sub ExportarDataGridViewAExcel(dgv As DataGridView)
        If grdcaptura.Rows.Count = 0 Then MsgBox("Genera el reporte para poder exportar los datos a Excel.", vbInformation + vbOKOnly, titulocentral) : Exit Sub
        If MsgBox("¿Deseas exportar la información a un archivo de Excel?", vbInformation + vbOKCancel, "Delsscom Control Negocios Pro") = vbOK Then

            Dim voy As Integer = 0
            ' Crea un nuevo libro de trabajo de Excel
            Using workbook As New XLWorkbook()

                ' Añade una nueva hoja de trabajo
                Dim worksheet As IXLWorksheet = workbook.Worksheets.Add("Datos")

                ' Escribe los encabezados de columna
                For colIndex As Integer = 0 To dgv.Columns.Count - 1
                    Dim headerCell As IXLCell = worksheet.Cell(1, colIndex + 1)
                    worksheet.Cell(1, colIndex + 1).Value = dgv.Columns(colIndex).HeaderText
                    headerCell.Value = dgv.Columns(colIndex).HeaderText
                    headerCell.Style.Font.Bold = True  ' Aplica negrita a los encabezados
                Next


                For rowIndex As Integer = 0 To dgv.Rows.Count - 1
                    For colIndex As Integer = 0 To dgv.Columns.Count - 1
                        Dim cellValue As Object = dgv.Rows(rowIndex).Cells(colIndex).Value
                        Dim cellValueString As String = If(cellValue Is Nothing, String.Empty, cellValue.ToString())
                        worksheet.Cell(rowIndex + 2, colIndex + 1).Value = cellValueString
                        Dim cell As IXLCell = worksheet.Cell(rowIndex + 2, colIndex + 1)
                        cell.Value = cellValueString
                        cell.Style.NumberFormat.Format = "@"
                    Next
                    voy = voy + 1
                    My.Application.DoEvents()
                Next

                worksheet.Columns().AdjustToContents()
                ' Usa MemoryStream para guardar el archivo en memoria y abrirlo
                Using memoryStream As New System.IO.MemoryStream()
                    ' Guarda el libro de trabajo en el MemoryStream
                    workbook.SaveAs(memoryStream)

                    ' Guarda el MemoryStream en un archivo temporal para abrirlo
                    Dim tempFilePath As String = IO.Path.GetTempPath() & Guid.NewGuid().ToString() & ".xlsx"
                    System.IO.File.WriteAllBytes(tempFilePath, memoryStream.ToArray())

                    ' Abre el archivo temporal en Excel
                    Process.Start(tempFilePath)
                End Using

                'workbook.SaveAs(filePath)
            End Using
            MessageBox.Show("Datos exportados exitosamente")

        End If
    End Sub

    Private Sub btnNuevo_Click(sender As Object, e As EventArgs) Handles btnNuevo.Click
        cbo.Visible = False
        cbo.Items.Clear()
        txtcobrar.Text = "0.00"
        grdcaptura.Rows.Clear()
        opttodos.Checked = True
    End Sub

    Private Sub RadioButton2_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton2.CheckedChanged
        If (RadioButton2.Checked) Then
            grdcaptura.Rows.Clear()
            grdcaptura.ColumnCount = 0
            cbo.Text = ""
            cbo.Visible = False
            cbo.Focus().Equals(True)

            grdcaptura.ColumnCount = 5
            With grdcaptura
                With .Columns(0)
                    .HeaderText = "N. Matricula"
                    .Width = 80
                    .AutoSizeMode = DataGridViewAutoSizeColumnMode.NotSet
                    .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                    .Visible = True
                End With
                With .Columns(1)
                    .HeaderText = "Alumno"
                    .Width = 300
                    .AutoSizeMode = DataGridViewAutoSizeColumnMode.NotSet
                    .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                    .Visible = True
                End With
                With .Columns(2)
                    .HeaderText = "Grupo"
                    .Width = 100
                    .AutoSizeMode = DataGridViewAutoSizeColumnMode.NotSet
                    .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                    .Visible = True
                End With
                With .Columns(3)
                    .HeaderText = "Curso"
                    .Width = 270
                    .AutoSizeMode = DataGridViewAutoSizeColumnMode.NotSet
                    .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                    .Visible = True
                End With
                With .Columns(4)
                    .HeaderText = "Inscripción"
                    .Width = 110
                    .AutoSizeMode = DataGridViewAutoSizeColumnMode.NotSet
                    .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                    .Visible = True
                End With
            End With
        End If
    End Sub

    Private Sub RadioButton1_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton1.CheckedChanged
        If (RadioButton1.Checked) Then
            grdcaptura.Rows.Clear()
            grdcaptura.ColumnCount = 0
            cbo.Text = ""
            cbo.Visible = False
            cbo.Focus().Equals(True)

            grdcaptura.ColumnCount = 5
            With grdcaptura
                With .Columns(0)
                    .HeaderText = "N. Matricula"
                    .Width = 80
                    .AutoSizeMode = DataGridViewAutoSizeColumnMode.NotSet
                    .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                    .Visible = True
                End With
                With .Columns(1)
                    .HeaderText = "Alumno"
                    .Width = 300
                    .AutoSizeMode = DataGridViewAutoSizeColumnMode.NotSet
                    .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                    .Visible = True
                End With
                With .Columns(2)
                    .HeaderText = "Grupo"
                    .Width = 100
                    .AutoSizeMode = DataGridViewAutoSizeColumnMode.NotSet
                    .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                    .Visible = True
                End With
                With .Columns(3)
                    .HeaderText = "Curso"
                    .Width = 270
                    .AutoSizeMode = DataGridViewAutoSizeColumnMode.NotSet
                    .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                    .Visible = True
                End With
                With .Columns(4)
                    .HeaderText = "Baja"
                    .Width = 110
                    .AutoSizeMode = DataGridViewAutoSizeColumnMode.NotSet
                    .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                    .Visible = True
                End With
            End With
        End If
    End Sub

    Private Sub frmRepCuentas_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class