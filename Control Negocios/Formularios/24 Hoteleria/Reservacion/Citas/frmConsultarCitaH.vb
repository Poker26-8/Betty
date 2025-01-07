﻿Imports MySql.Data.MySqlClient

Public Class frmConsultarCitaH
    Private Sub btnSalir_Click(sender As Object, e As EventArgs) Handles btnSalir.Click
        Me.Close()
        frmCitasH.Show()
        frmCitasH.Activate()
    End Sub

    Private Sub optDia_Click(sender As Object, e As EventArgs) Handles optDia.Click
        grdCaptura.Rows.Clear()
        grdCaptura.ColumnCount = 0
        grdCaptura.RowHeadersVisible = False
        'Ajusta el grid
        grdCaptura.ColumnCount = 4

        With grdCaptura.Columns(0)
            .Name = "Id"
            .Width = 50
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Visible = False
            .Resizable = DataGridViewTriState.False
        End With
        With grdCaptura.Columns(1)
            .Name = "Hora"
            .Width = 60
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .Visible = True
            .Resizable = DataGridViewTriState.False
        End With
        With grdCaptura.Columns(2)
            .Name = "Evento"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            .Visible = True
            .Resizable = DataGridViewTriState.False
        End With
        With grdCaptura.Columns(3)
            .Name = "Activo"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            .Visible = False
            .Resizable = DataGridViewTriState.False
        End With

        My.Application.DoEvents()
    End Sub

    Private Sub cboAño_DropDown(sender As Object, e As EventArgs) Handles cboAño.DropDown
        cboAño.Items.Clear()
        Dim y As Integer = Now.Year
        cboAño.Items.Add(y)
        For mas As Integer = 1 To 9
            cboAño.Items.Add(y + mas)
        Next
    End Sub

    Private Sub cboMes_DropDown(sender As Object, e As EventArgs) Handles cboMes.DropDown
        cboMes.Items.Clear()

        Dim mes As String = ""
        For val As Integer = 1 To 12

            Select Case val
                Case Is = 1
                    mes = "Enero"
                Case Is = 2
                    mes = "Febrero"
                Case Is = 3
                    mes = "Marzo"
                Case Is = 4
                    mes = "Abril"
                Case Is = 5
                    mes = "Mayo"
                Case Is = 6
                    mes = "Junio"
                Case Is = 7
                    mes = "Julio"
                Case Is = 8
                    mes = "Agosto"
                Case Is = 9
                    mes = "Septiembre"
                Case Is = 10
                    mes = "Octubre"
                Case Is = 11
                    mes = "Noviembre"
                Case Is = 12
                    mes = "Diciembre"
            End Select
            cboMes.Items.Add(
                mes
                )
        Next
    End Sub

    Public Sub cboMesTag()
        If cboMes.Text = "Enero" Then
            cboMes.Tag = 1
        ElseIf cboMes.Text = "Febrero" Then
            cboMes.Tag = 1
        ElseIf cboMes.Text = "Marzo" Then
            cboMes.Tag = 3
        ElseIf cboMes.Text = "Abril" Then
            cboMes.Tag = 4
        ElseIf cboMes.Text = "Mayo" Then
            cboMes.Tag = 5
        ElseIf cboMes.Text = "Junio" Then
            cboMes.Tag = 6
        ElseIf cboMes.Text = "Julio" Then
            cboMes.Tag = 7
        ElseIf cboMes.Text = "Agosto" Then
            cboMes.Tag = 8
        ElseIf cboMes.Text = "Septiembre" Then
            cboMes.Tag = 9
        ElseIf cboMes.Text = "Octubre" Then
            cboMes.Tag = 10
        ElseIf cboMes.Text = "Noviembre" Then
            cboMes.Tag = 11
        ElseIf cboMes.Text = "Diciembre" Then
            cboMes.Tag = 12
        End If
    End Sub

    Private Sub cboMes_SelectedValueChanged(sender As Object, e As EventArgs) Handles cboMes.SelectedValueChanged
        cboMesTag()
    End Sub

    Private Sub btnConsultar_Click(sender As Object, e As EventArgs) Handles btnConsultar.Click

        Dim cnn1 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
        Dim cnn2 As MySqlConnection = New MySqlConnection(sTargetlocalmysql)
        Dim rd1, rd2 As MySqlDataReader
        Dim cmd1, cmd2 As MySqlCommand

        If (optMes.Checked) Then
            If cboAño.Text = "" Or cboMes.Text = "" Then
                MsgBox("Seleccione una fecha válida para consultar.", vbInformation + vbOKOnly, titulohotelriaa)
                Exit Sub
            End If

            grdCaptura.Rows.Clear()
            grdCaptura.ColumnCount = 0
            grdCaptura.RowHeadersVisible = False
            'Ajusta el grid
            grdCaptura.ColumnCount = 6
            With grdCaptura.Columns(0)
                .Name = "Id"
                .Width = 50
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Visible = False
                .Resizable = DataGridViewTriState.False
            End With
            With grdCaptura.Columns(1)
                .Name = "Dia"
                .Width = 60
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Visible = True
                .Resizable = DataGridViewTriState.False
            End With
            With grdCaptura.Columns(2)
                .Name = "Hora"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                .Visible = True
                .Resizable = DataGridViewTriState.False
            End With
            With grdCaptura.Columns(3)
                .Name = "Evento"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                .Visible = True
                .Resizable = DataGridViewTriState.False
            End With
            With grdCaptura.Columns(4)
                .Name = "Usuario"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                .Visible = True
                .Resizable = DataGridViewTriState.False
            End With
            With grdCaptura.Columns(5)
                .Name = "Activo"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                .Visible = False
                .Resizable = DataGridViewTriState.False
            End With

            Dim dia As Integer = 0
            Dim hora As String = ""
            Dim minu As String = ""

            Try
                cnn1.Close()
                cnn1.Open()

                cmd1 = cnn1.CreateCommand
                cmd1.CommandText =
                    "select Dia,Hora,Minuto,Id,Asunto,Usuario,Activo from Agenda where Anio=" & cboAño.Text & " and Mes=" & cboMes.Tag & " order by Dia"
                rd1 = cmd1.ExecuteReader
                Do While rd1.Read
                    If rd1.HasRows Then
                        dia = rd1("Dia").ToString

                        If rd1("Hora").ToString < 10 Then
                            hora = "0" & rd1("Hora").ToString
                        Else
                            hora = rd1("Hora").ToString
                        End If

                        If rd1("Minuto").ToString < 10 Then
                            minu = "0" & rd1("Minuto").ToString
                        Else
                            minu = rd1("Minuto").ToString
                        End If

                        grdCaptura.Rows.Add(rd1("Id").ToString, dia, hora & ":" & minu, rd1("Asunto").ToString, rd1("Usuario").ToString, rd1("Activo").ToString)
                    End If
                Loop
                rd1.Close()
                cnn1.Close()


                For lufy As Integer = 0 To grdCaptura.Rows.Count - 1
                    If grdCaptura.Rows(lufy).Cells(5).Value.ToString = "1" Then
                    Else
                        If grdCaptura.Rows(lufy).Cells(5).Value = False Then
                            grdCaptura.Rows(lufy).DefaultCellStyle.BackColor = Color.Blue
                            grdCaptura.Rows(lufy).DefaultCellStyle.ForeColor = Color.White
                        End If
                    End If
                Next
            Catch ex As Exception
                MessageBox.Show(ex.ToString)
                cnn1.Close()
            End Try
        End If

        If (optDia.Checked) Then
            grdCaptura.Rows.Clear()
            grdCaptura.ColumnCount = 0
            grdCaptura.RowHeadersVisible = False
            'Ajusta el grid
            grdCaptura.ColumnCount = 5

            With grdCaptura.Columns(0)
                .Name = "Id"
                .Width = 50
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Visible = False
                .Resizable = DataGridViewTriState.False
            End With
            With grdCaptura.Columns(1)
                .Name = "Hora"
                .Width = 60
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Visible = True
                .Resizable = DataGridViewTriState.False
            End With
            With grdCaptura.Columns(2)
                .Name = "Evento"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                .Visible = True
                .Resizable = DataGridViewTriState.False
            End With
            With grdCaptura.Columns(3)
                .Name = "Usuario"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                .Visible = True
                .Resizable = DataGridViewTriState.False
            End With
            With grdCaptura.Columns(4)
                .Name = "Activo"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                .Visible = False
                .Resizable = DataGridViewTriState.False
            End With

            Dim fechitasc(3) As String

            Dim consulta As String = Format(dtpDia.Value, "dd/MM/yyyy")
            Dim X As Integer = 0

            For vic As Integer = 0 To 3
                X = InStr(1, consulta, "/") - 1
                If X < 0 Then Exit For
                If vic = 0 Then
                    fechitasc(1) = Mid(consulta, 1, InStr(1, consulta, "/") - 1)
                ElseIf vic = 1 Then
                    fechitasc(2) = Mid(consulta, 1, InStr(1, consulta, "/") - 1)
                End If
                consulta = Mid(consulta, InStr(1, consulta, "/") + 1, 200)
            Next
            fechitasc(3) = consulta

            Try
                Dim hora As Integer = 0
                Dim horx As String = ""
                Dim evento As String = ""

                cnn1.Close() : cnn1.Open()
                cnn2.Close() : cnn2.Open()

                For field As Integer = 0 To 24
                    If field = 24 Then Exit For
                    hora = field

                    cmd1 = cnn1.CreateCommand
                    cmd1.CommandText =
                        "select Hora,Id,Usuario,Activo from Agenda where Hora=" & hora & " and Dia=" & fechitasc(1) & " and Mes=" & fechitasc(2) & " and Anio=" & fechitasc(3) & ""
                    rd1 = cmd1.ExecuteReader
                    If rd1.HasRows Then
                        If rd1.Read Then
                            cmd2 = cnn2.CreateCommand
                            cmd2.CommandText =
                                "select Asunto from Agenda where Hora=" & hora & " and Dia=" & fechitasc(1) & " and Mes=" & fechitasc(2) & " and Anio=" & fechitasc(3) & ""
                            rd2 = cmd2.ExecuteReader
                            Do While rd2.Read
                                If rd2.HasRows Then
                                    evento = evento & " - " & rd2(0).ToString
                                End If
                            Loop
                            rd2.Close()
                            evento = Mid(evento, 4, 10000)

                            If CDec(rd1("Hora").ToString) < 10 Then
                                horx = "0" & rd1("Hora").ToString
                            Else
                                horx = rd1("Hora").ToString
                            End If

                            grdCaptura.Rows.Add(rd1("Id").ToString, horx & ":00", evento, rd1("Usuario").ToString, rd1("Activo").ToString)
                        End If
                    Else
                        If hora < 10 Then
                            horx = "0" & field
                        Else
                            horx = field
                        End If

                        grdCaptura.Rows.Add("0", horx & ":00", "", "", "NULO")
                    End If

                    If grdCaptura.Rows(field).Cells(4).Value.ToString = "NULO" Then
                    Else
                        If grdCaptura.Rows(field).Cells(4).Value = False Then
                            grdCaptura.Rows(field).DefaultCellStyle.BackColor = Color.Blue
                            grdCaptura.Rows(field).DefaultCellStyle.ForeColor = Color.White
                        End If
                    End If

                    My.Application.DoEvents()
                    rd1.Close()
                Next
                cnn2.Close()
                cnn1.Close()
            Catch ex As Exception
                MessageBox.Show(ex.ToString)
                cnn1.Close()
                cnn2.Close()
            End Try
        End If
    End Sub

    Private Sub frmConsultarCitaH_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        optMes.Checked = True
    End Sub

    Private Sub optMes_Click(sender As Object, e As EventArgs) Handles optMes.Click
        If optMes.Checked Then

            grdCaptura.Rows.Clear()
            grdCaptura.ColumnCount = 0
            optDia.Checked = False
        End If
    End Sub
End Class