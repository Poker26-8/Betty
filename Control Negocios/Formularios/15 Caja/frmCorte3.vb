﻿Public Class frmCorte3

    Dim SUMATOTALEFECTIVO As Double = 0
    Dim SUMAEFECTIVOSINPROPINA As Double = 0
    Dim saldoinicial As Double = 0
    Dim totalefectivo As Double = 0
    Dim propinas As Double = 0

    Private Sub frmCorte3_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            cnn1.Close() : cnn1.Open()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "SELECT * FROM cortecaja WHERE Fecha='" & Format(dtpFecha.Value, "yyyy-MM-dd") & "'"
            rd1 = cmd1.ExecuteReader
            If rd1.HasRows Then
                If rd1.Read Then
                    txtInicial.Text = rd1("Saldo_Ini").ToString
                End If
            End If
            rd1.Close()
            cnn1.Close()

        Catch ex As Exception
            MessageBox.Show(ex.ToString)
            cnn1.Close()
        End Try
    End Sub
    Private Sub btnCorteZ_Click(sender As Object, e As EventArgs) Handles btnCorteZ.Click
        Try
            Dim tamticket As Integer = 0
            Dim impresora As String = ""

            cnn1.Close() : cnn1.Open()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "SELECT SUM(Monto) FROM abonoi WHERE Fecha between '" & Format(dtpFecha.Value, "yyyy-MM-dd") & "' and '" & Format(dtpFecha.Value, "yyyy-MM-dd") & "' AND Concepto='ABONO' AND FormaPago='EFECTIVO'"
            rd1 = cmd1.ExecuteReader
            If rd1.HasRows Then
                If rd1.Read Then
                    totalefectivo = IIf(rd1(0).ToString = "", 0, rd1(0).ToString)
                End If
            End If
            rd1.Close()

            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "SELECT SUM(propina) FROM abonoi WHERE Fecha between '" & Format(dtpFecha.Value, "yyyy-MM-dd") & "' and '" & Format(dtpFecha.Value, "yyyy-MM-dd") & "' AND Concepto='ABONO' AND FormaPago='EFECTIVO'"
            rd1 = cmd1.ExecuteReader
            If rd1.HasRows Then
                If rd1.Read Then
                    propinas = IIf(rd1(0).ToString = "", 0, rd1(0).ToString)
                End If
            End If
            rd1.Close()
            cnn1.Close()

            SUMAEFECTIVOSINPROPINA = CDbl(totalefectivo) - CDbl(propinas)
            SUMATOTALEFECTIVO = CDbl(SUMAEFECTIVOSINPROPINA) + CDbl(propinas)

            tamticket = TamImpre()
            impresora = ImpresoraImprimir()

            If tamticket = 80 Then
                PCorte80.DefaultPageSettings.PrinterSettings.PrinterName = impresora
                PCorte80.Print()
            Else
                pCorte58.DefaultPageSettings.PrinterSettings.PrinterName = impresora
                pCorte58.Print()
            End If

        Catch ex As Exception
            MessageBox.Show(ex.ToString)
            cnn1.Close()
        End Try
    End Sub

    Private Sub PCorte80_PrintPage(sender As Object, e As Printing.PrintPageEventArgs) Handles PCorte80.PrintPage
        Try
            Dim tipografia As String = "Lucida Sans Typewriter"
            Dim fuente_r As New Font("Lucida Sans Typewriter", 8, FontStyle.Regular)
            Dim fuente_b As New Font("Lucida Sans Typewriter", 8, FontStyle.Bold)
            Dim fuente_c As New Font("Lucida Sans Typewriter", 8, FontStyle.Regular)
            Dim fuente_p As New Font("Lucida Sans Typewriter", 7, FontStyle.Regular)
            Dim fuente_a As New Font("Lucida Sans Typewriter", 12, FontStyle.Bold)

            Dim derecha As New StringFormat With {.Alignment = StringAlignment.Far}
            Dim centro As New StringFormat With {.Alignment = StringAlignment.Center}
            Dim hoja As New Pen(Brushes.Black, 1)
            Dim Y As Double = 0

            Dim pie1 As String = ""
            Dim simbolo = TraerSimbolo()
            Dim FORMAPAGO As String = ""
            Dim SUMAFORMA As Double = 0
            Dim SUMAFORMASINPROPINA As Double = 0
            Dim SUMAFORMAPRO As Double = 0
            Dim TotalForma As Double = 0
            Dim ACUMULADOFORMAS As Double = 0
            Dim ACUMULADO As Double = 0
            Dim ACUMULADO2 As Double = 0

            Dim SUMADESCUENTOS As Double = 0
            Dim SUMACANCELACIONES As Double = 0
            Dim CANTIDADCANCELADAS As Double = 0
            Dim SUMADEVOLUCIONES As Double = 0

            Dim SUMATOTAL As Double = 0
            Dim SUMATOTAL2 As Double = 0
            Dim SUMATOTALEGRESOS As Double = 0


            e.Graphics.DrawString("-----------------------------------------------------", fuente_b, Brushes.Black, 1, Y)
            Y += 11

            cnn2.Close() : cnn2.Open()
            cmd2 = cnn2.CreateCommand
            cmd2.CommandText = "select * from Ticket"
            rd2 = cmd2.ExecuteReader
            If rd2.HasRows Then
                If rd2.Read Then

                    pie1 = rd2("Pie1").ToString
                    'Razón social
                    If rd2("Cab0").ToString() <> "" Then
                        e.Graphics.DrawString(rd2("Cab0").ToString, New Drawing.Font(tipografia, 8, FontStyle.Bold), Brushes.Black, 140, Y, centro)
                        Y += 12.5
                    End If
                    'RFC
                    If rd2("Cab1").ToString() <> "" Then
                        e.Graphics.DrawString(rd2("Cab1").ToString, New Drawing.Font(tipografia, 8, FontStyle.Bold), Brushes.Black, 140, Y, centro)
                        Y += 12.5
                    End If
                    'Calle  N°.
                    If rd2("Cab2").ToString() <> "" Then
                        e.Graphics.DrawString(rd2("Cab2").ToString, New Drawing.Font(tipografia, 8, FontStyle.Regular), Brushes.Gray, 140, Y, centro)
                        Y += 12
                    End If
                    'Colonia
                    If rd2("Cab3").ToString() <> "" Then
                        e.Graphics.DrawString(rd2("Cab3").ToString, New Drawing.Font(tipografia, 8, FontStyle.Regular), Brushes.Gray, 140, Y, centro)
                        Y += 12
                    End If
                    'Delegación / Municipio - Entidad
                    If rd2("Cab4").ToString() <> "" Then
                        e.Graphics.DrawString(rd2("Cab4").ToString, New Drawing.Font(tipografia, 8, FontStyle.Regular), Brushes.Gray, 140, Y, centro)
                        Y += 12
                    End If
                    'Teléfono
                    If rd2("Cab5").ToString() <> "" Then
                        e.Graphics.DrawString(rd2("Cab5").ToString, New Drawing.Font(tipografia, 8, FontStyle.Regular), Brushes.Gray, 140, Y, centro)
                        Y += 12
                    End If
                    'Correo
                    If rd2("Cab6").ToString() <> "" Then
                        e.Graphics.DrawString(rd2("Cab6").ToString, New Drawing.Font(tipografia, 8, FontStyle.Regular), Brushes.Gray, 140, Y, centro)
                        Y += 12
                    End If
                    Y += 17
                End If
            Else
                Y += 0
            End If
            rd2.Close()
            cnn2.Close()

            e.Graphics.DrawString("-----------------------------------------------------", fuente_b, Brushes.Black, 1, Y)
            Y += 11
            e.Graphics.DrawString("C O R T E  D E  C A J A   C O R T E  Z", fuente_b, Brushes.Black, 135, Y, centro)
            Y += 11
            e.Graphics.DrawString("-----------------------------------------------------", fuente_b, Brushes.Black, 1, Y)
            Y += 11

            e.Graphics.DrawString("Fecha: " & Format(dtpFecha.Value, "yyyy-MM-dd"), fuente_b, Brushes.Black, 1, Y)
            Y += 11
            e.Graphics.DrawString("-----------------------------------------------------", fuente_b, Brushes.Black, 1, Y)
            Y += 11
            e.Graphics.DrawString("EFECTIVO", fuente_b, Brushes.Black, 1, Y)
            Y += 20
            e.Graphics.DrawString("(+) Ventas:", fuente_b, Brushes.Black, 1, Y)
            e.Graphics.DrawString(simbolo & FormatNumber(SUMAEFECTIVOSINPROPINA, 2), fuente_b, Brushes.Black, 270, Y, derecha)
            Y += 20
            e.Graphics.DrawString("(+) Propinas:", fuente_b, Brushes.Black, 1, Y)
            e.Graphics.DrawString(simbolo & FormatNumber(propinas, 2), fuente_b, Brushes.Black, 270, Y, derecha)
            Y += 20
            e.Graphics.DrawString("(-) Retiros:", fuente_b, Brushes.Black, 1, Y)
            e.Graphics.DrawString(simbolo, fuente_b, Brushes.Black, 270, Y, derecha)
            Y += 20
            e.Graphics.DrawString("(+) Depositos:", fuente_b, Brushes.Black, 1, Y)
            e.Graphics.DrawString(simbolo, fuente_b, Brushes.Black, 270, Y, derecha)
            Y += 20

            e.Graphics.DrawString("----------", fuente_b, Brushes.Black, 200, Y)
            Y += 11

            e.Graphics.DrawString("TOTAL:", fuente_b, Brushes.Black, 10, Y)
            e.Graphics.DrawString(simbolo & FormatNumber(SUMATOTALEFECTIVO, 2), fuente_b, Brushes.Black, 270, Y, derecha)
            Y += 20




            cnn2.Close() : cnn2.Open()
            cnn3.Close() : cnn3.Open()
            cnn4.Close() : cnn4.Open()

            cmd2 = cnn2.CreateCommand
            cmd2.CommandText = "SELECT DISTINCT FormaPago FROM abonoi WHERE Concepto='ABONO' AND FormaPago<>'EFECTIVO' AND Fecha between '" & Format(dtpFecha.Value, "yyyy-MM-dd") & "' and '" & Format(dtpFecha.Value, "yyyy-MM-dd") & "'"
            rd2 = cmd2.ExecuteReader
            Do While rd2.Read
                If rd2.HasRows Then
                    FORMAPAGO = rd2(0).ToString
                    TotalForma = 0
                    SUMAFORMA = 0
                    SUMAFORMAPRO = 0
                    e.Graphics.DrawString("-----------------------------------------------------", fuente_b, Brushes.Black, 1, Y)
                    Y += 11
                    e.Graphics.DrawString(FORMAPAGO, fuente_b, Brushes.Black, 1, Y)
                    Y += 20

                    cmd3 = cnn3.CreateCommand
                    cmd3.CommandText = "SELECT SUM(Monto) FROM abonoi WHERE FormaPago='" & FORMAPAGO & "' AND Fecha between '" & Format(dtpFecha.Value, "yyyy-MM-dd") & "' and '" & Format(dtpFecha.Value, "yyyy-MM-dd") & "' GROUP BY FormaPago"
                    rd3 = cmd3.ExecuteReader
                    If rd3.HasRows Then
                        If rd3.Read Then
                            SUMAFORMA = rd3(0).ToString

                        End If
                    End If
                    rd3.Close()

                    cmd4 = cnn4.CreateCommand
                    cmd4.CommandText = "SELECT SUM(Propina) FROM abonoi WHERE FormaPago='" & FORMAPAGO & "' AND Fecha between '" & Format(dtpFecha.Value, "yyyy-MM-dd") & "' and '" & Format(dtpFecha.Value, "yyyy-MM-dd") & "' GROUP BY FormaPago"
                    rd4 = cmd4.ExecuteReader
                    If rd4.HasRows Then
                        If rd4.Read Then
                            SUMAFORMAPRO = rd4(0).ToString

                        End If
                    End If
                    rd4.Close()

                    SUMAFORMASINPROPINA = CDbl(SUMAFORMA) - CDbl(SUMAFORMAPRO)
                    TotalForma = CDbl(SUMAFORMASINPROPINA) + CDbl(SUMAFORMAPRO)

                    e.Graphics.DrawString("(+) Ventas:", fuente_b, Brushes.Black, 1, Y)
                    e.Graphics.DrawString(simbolo & FormatNumber(SUMAFORMASINPROPINA, 2), fuente_b, Brushes.Black, 270, Y, derecha)
                    Y += 20

                    e.Graphics.DrawString("(+) Propinas:", fuente_b, Brushes.Black, 1, Y)
                    e.Graphics.DrawString(simbolo & FormatNumber(SUMAFORMAPRO, 2), fuente_b, Brushes.Black, 270, Y, derecha)
                    Y += 20

                    e.Graphics.DrawString("----------", fuente_b, Brushes.Black, 200, Y)
                    Y += 11
                    e.Graphics.DrawString("Faltante:", fuente_b, Brushes.Black, 10, Y)
                    e.Graphics.DrawString(simbolo & FormatNumber(TotalForma, 2), fuente_b, Brushes.Black, 270, Y, derecha)
                    Y += 13

                    ACUMULADOFORMAS = ACUMULADOFORMAS + CDbl(TotalForma)

                    SUMATOTAL = CDbl(ACUMULADOFORMAS)
                End If
            Loop
            rd2.Close()
            cnn2.Close()
            cnn3.Close()
            Y += 2
            e.Graphics.DrawString("-----------------------------------------------------", fuente_b, Brushes.Black, 1, Y)
            Y += 11
            SUMATOTAL2 = CDbl(SUMATOTAL) + CDbl(SUMATOTALEFECTIVO)
            e.Graphics.DrawString("SUMA GENERAL DE INGRESOS:", fuente_b, Brushes.Black, 1, Y)
            e.Graphics.DrawString(simbolo & FormatNumber(SUMATOTAL2, 2), fuente_b, Brushes.Black, 270, Y, derecha)
            Y += 11

            'e.Graphics.DrawString("-----------------------------------------------------", fuente_b, Brushes.Black, 1, Y)
            'Y += 13

            'e.Graphics.DrawString("A DEPOSITAR", fuente_b, Brushes.Black, 1, Y)
            'Y += 20
            'e.Graphics.DrawString("Reportado:", fuente_b, Brushes.Black, 1, Y)
            'e.Graphics.DrawString(simbolo, fuente_b, Brushes.Black, 270, Y, derecha)
            'Y += 20
            'e.Graphics.DrawString("(-)Fondo::", fuente_b, Brushes.Black, 1, Y)
            'e.Graphics.DrawString(simbolo, fuente_b, Brushes.Black, 270, Y, derecha)
            'Y += 20
            'e.Graphics.DrawString("----------", fuente_b, Brushes.Black, 200, Y)
            'Y += 11
            'e.Graphics.DrawString("TOTAL:", fuente_b, Brushes.Black, 10, Y)
            'e.Graphics.DrawString(simbolo, fuente_b, Brushes.Black, 270, Y, derecha)
            'Y += 20

            e.Graphics.DrawString("-----------------------------------------------------", fuente_b, Brushes.Black, 1, Y)
            Y += 11
            e.Graphics.DrawString("C A N C E L A C I O N E S", fuente_b, Brushes.Black, 135, Y, centro)
            Y += 11
            e.Graphics.DrawString("-----------------------------------------------------", fuente_b, Brushes.Black, 1, Y)
            Y += 11

            cnn2.Close() : cnn2.Open()
            cmd2 = cnn2.CreateCommand
            cmd2.CommandText = "SELECT COUNT(Id) FROM abonoi WHERE Concepto='NOTA CANCELADA' AND Fecha between '" & Format(dtpFecha.Value, "yyyy-MM-dd") & "' and '" & Format(dtpFecha.Value, "yyyy-MM-dd") & "'"
            rd2 = cmd2.ExecuteReader
            If rd2.HasRows Then
                If rd2.Read Then
                    CANTIDADCANCELADAS = rd2(0).ToString
                End If
            End If
            rd2.Close()

            cmd2 = cnn2.CreateCommand
            cmd2.CommandText = "SELECT SUM(Monto) FROM abonoi WHERE Concepto='NOTA CANCELADA' AND Fecha between '" & Format(dtpFecha.Value, "yyyy-MM-dd") & "' and '" & Format(dtpFecha.Value, "yyyy-MM-dd") & "'"
            rd2 = cmd2.ExecuteReader
            If rd2.HasRows Then
                If rd2.Read Then
                    SUMACANCELACIONES = IIf(rd2(0).ToString = "", 0, rd2(0).ToString)
                End If
            End If
            rd2.Close()
            cnn2.Close()

            e.Graphics.DrawString("# Cuentas canceladas", fuente_b, Brushes.Black, 1, Y)
            e.Graphics.DrawString(FormatNumber(CANTIDADCANCELADAS, 2), fuente_b, Brushes.Black, 270, Y, derecha)
            Y += 20
            e.Graphics.DrawString("Total", fuente_b, Brushes.Black, 1, Y)
            e.Graphics.DrawString(simbolo & FormatNumber(SUMACANCELACIONES, 2), fuente_b, Brushes.Black, 270, Y, derecha)
            Y += 20

            e.Graphics.DrawString("-----------------------------------------------------", fuente_b, Brushes.Black, 1, Y)
            Y += 11
            e.Graphics.DrawString("D E V O L U C I O N E S", fuente_b, Brushes.Black, 135, Y, centro)
            Y += 11
            e.Graphics.DrawString("-----------------------------------------------------", fuente_b, Brushes.Black, 1, Y)
            Y += 11

            cnn2.Close() : cnn2.Open()
            cmd2 = cnn2.CreateCommand
            cmd2.CommandText = "SELECT SUM(Monto) FROM abonoi WHERE Concepto='DEVOLUCION' AND Fecha between '" & Format(dtpFecha.Value, "yyyy-MM-dd") & "' and '" & Format(dtpFecha.Value, "yyyy-MM-dd") & "'"
            rd2 = cmd2.ExecuteReader
            If rd2.HasRows Then
                If rd2.Read Then
                    SUMADEVOLUCIONES = IIf(rd2(0).ToString = "", 0, rd2(0).ToString)
                End If
            End If
            rd2.Close()
            cnn2.Close()


            If SUMADEVOLUCIONES > 0 Then
                e.Graphics.DrawString("Total", fuente_b, Brushes.Black, 1, Y)
                e.Graphics.DrawString(simbolo & FormatNumber(SUMADEVOLUCIONES, 2), fuente_b, Brushes.Black, 270, Y, derecha)
                Y += 20
            Else
                e.Graphics.DrawString("No hay devoluciones", fuente_b, Brushes.Black, 1, Y)
                Y += 20
            End If



            e.Graphics.DrawString("-----------------------------------------------------", fuente_b, Brushes.Black, 1, Y)
            Y += 11
            e.Graphics.DrawString("D E S C U E N T O S", fuente_b, Brushes.Black, 135, Y, centro)
            Y += 11
            e.Graphics.DrawString("-----------------------------------------------------", fuente_b, Brushes.Black, 1, Y)
            Y += 11

            cnn2.Close() : cnn2.Open()
            cmd2 = cnn2.CreateCommand
            cmd2.CommandText = "SELECT SUM(Descuento) FROM abonoi WHERE Concepto='ABONO' AND Fecha between '" & Format(dtpFecha.Value, "yyyy-MM-dd") & "' and '" & Format(dtpFecha.Value, "yyyy-MM-dd") & "'"
            rd2 = cmd2.ExecuteReader
            If rd2.HasRows Then
                If rd2.Read Then
                    SUMADESCUENTOS = IIf(rd2(0).ToString = "", 0, rd2(0).ToString)
                End If
            End If
            rd2.Close()
            cnn2.Close()

            e.Graphics.DrawString("Total", fuente_b, Brushes.Black, 1, Y)
            e.Graphics.DrawString(simbolo & FormatNumber(SUMADESCUENTOS, 2), fuente_b, Brushes.Black, 270, Y, derecha)
            Y += 17

            e.Graphics.DrawString("-----------------------------------------------------", fuente_b, Brushes.Black, 1, Y)
            Y += 11
            SUMATOTALEGRESOS = CDbl(SUMACANCELACIONES) + CDbl(SUMADEVOLUCIONES)
            e.Graphics.DrawString("SUMA GENERAL DE EGRESOS:", fuente_b, Brushes.Black, 1, Y)
            e.Graphics.DrawString(simbolo & FormatNumber(SUMATOTALEGRESOS, 2), fuente_b, Brushes.Black, 270, Y, derecha)
            Y += 11
            e.Graphics.DrawString("-----------------------------------------------------", fuente_b, Brushes.Black, 1, Y)
            Y += 11

            ACUMULADO = CDbl(SUMATOTAL2) + CDbl(txtInicial.Text) - (CDbl(SUMACANCELACIONES) + CDbl(SUMADEVOLUCIONES))
            ACUMULADO2 = CDbl(SUMATOTAL2) - (CDbl(SUMACANCELACIONES) + CDbl(SUMADEVOLUCIONES))

            e.Graphics.DrawString("Fondo:", fuente_b, Brushes.Black, 10, Y)
            e.Graphics.DrawString(simbolo & FormatNumber(txtInicial.Text, 2), fuente_b, Brushes.Black, 270, Y, derecha)
            Y += 20
            e.Graphics.DrawString("TOTAL SIN FONDO INICIAL", fuente_b, Brushes.Black, 10, Y)
            e.Graphics.DrawString(simbolo & FormatNumber(ACUMULADO2, 2), fuente_b, Brushes.Black, 270, Y, derecha)
            Y += 20
            e.Graphics.DrawString("TOTAL CON FONDO INICIAL", fuente_b, Brushes.Black, 10, Y)
            e.Graphics.DrawString(simbolo & FormatNumber(ACUMULADO, 2), fuente_b, Brushes.Black, 270, Y, derecha)
            Y += 20


            e.Graphics.DrawString("-----------------------------------------------------", fuente_b, Brushes.Black, 1, Y)
            Y += 11
            e.Graphics.DrawString("ACUMULADO DIARIO Z", fuente_b, Brushes.Black, 10, Y)
            e.Graphics.DrawString(simbolo & FormatNumber(ACUMULADO, 2), fuente_b, Brushes.Black, 270, Y, derecha)
            Y += 20

            cnn1.Close() : cnn1.Open()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "UPDATE cortecaja SET Saldo_Fin=" & ACUMULADO & " WHERE Fecha='" & Format(dtpFecha.Value, "yyyy-MM-dd") & "'"
            cmd1.ExecuteNonQuery()
            cnn1.Close()

            e.HasMorePages = False
            cnn1.Close()
            cnn2.Close()
            cnn2.Close()
            cnn4.Close()
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
            cnn1.Close()
            cnn2.Close()
            cnn2.Close()
            cnn4.Close()
        End Try
    End Sub

    Private Sub pCorte58_PrintPage(sender As Object, e As Printing.PrintPageEventArgs) Handles pCorte58.PrintPage

    End Sub

    Private Sub btnSalIni_Click(sender As Object, e As EventArgs) Handles btnSalIni.Click
        Try
            cnn1.Close()
            cnn1.Open()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "SELECT * FROM cortecaja WHERE Fecha='" & Format(dtpFecha.Value, "yyyy-MM-dd") & "'"
            rd1 = cmd1.ExecuteReader
            If rd1.HasRows Then
                If rd1.Read Then

                End If
            Else
                cnn2.Close() : cnn2.Open()
                cmd2 = cnn2.CreateCommand
                cmd2.CommandText = "INSERT INTO cortecaja(NumCorte,Saldo_Ini,Fecha,Saldo_Fin) VALUES(0," & txtInicial.Text & ",'" & Format(dtpFecha.Value, "yyyy-MM-dd") & "',0)"
                If cmd2.ExecuteNonQuery() Then
                    MsgBox("Saldo inicial registrado correctamente", vbInformation + vbOKOnly, titulocentral)
                End If
                cnn2.Close()
            End If
            rd1.Close()
            cnn1.Close()

        Catch ex As Exception
            MessageBox.Show(ex.ToString)
            cnn1.Close()
        End Try
    End Sub

    Private Sub C_Global()
        Try
            cnn1.Close() : cnn1.Open()

            cmd1 = cnn1.CreateCommand
            cmd1.CommandText =
                "select * from CorteCaja where Fecha='" & Format(dtpFecha.Value, "yyyy-MM-dd") & "'"
            rd1 = cmd1.ExecuteReader
            If rd1.HasRows Then
                If rd1.Read Then
                    txtInicial.Text = FormatNumber(rd1("Saldo_ini").ToString, 2)
                End If
            Else
                txtInicial.Text = "0.00"
            End If
            rd1.Close() : cnn1.Close()
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
            cnn1.Close()
        End Try
    End Sub

    Private Sub dtpFecha_ValueChanged(sender As Object, e As EventArgs) Handles dtpFecha.ValueChanged
        C_Global()
    End Sub
End Class