Imports System.IO

Public Class frmRepVentasDes

    Private Sub btnSalir_Click(sender As Object, e As EventArgs) Handles btnSalir.Click
        Me.Close()
    End Sub

    Private Sub btnConsultar_Click(sender As Object, e As EventArgs) Handles btnConsultar.Click
        Try

            Dim subtotal As Double = 0
            Dim descuento As Double = 0
            Dim total As Double = 0
            Dim sumasub As Double = 0
            Dim sumtotal As Double = 0

            cnn1.Close() : cnn1.Open()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "SELECT Folio,Subtotal,Descuento,Totales FROM ventas WHERE Fventa BETWEEN '" & Format(dtpInicial.Value, "yyyy-MM-dd") & "' AND '" & Format(dtpFinal.Value, "yyyy-MM-dd") & "'"
            rd1 = cmd1.ExecuteReader
            If rd1.HasRows Then
                Do While rd1.Read

                    subtotal = rd1("Totales").ToString
                    descuento = rd1("Descuento").ToString
                    total = rd1("Totales").ToString

                    sumasub = CDec(subtotal) + CDec(descuento)

                    grdVentas.Rows.Add(rd1("Folio").ToString, total, descuento, sumasub)

                    sumtotal = sumtotal + CDec(sumasub)
                Loop
            End If
            rd1.Close()
            cnn1.Close()

            lbltotal.Text = FormatNumber(sumtotal, 2)

        Catch ex As Exception
            MessageBox.Show(ex.ToString)
            cnn1.Close()
        End Try
    End Sub

    Private Sub PFolios80_PrintPage(sender As Object, e As Printing.PrintPageEventArgs) Handles PFolios80.PrintPage

        'Fuentes prederminadas
        Dim tipografia As String = "Lucida Sans Typewriter"
        Dim fuente_datos As New Drawing.Font(tipografia, 10, FontStyle.Regular)
        Dim fuente_prods As New Drawing.Font(tipografia, 9, FontStyle.Regular)
        Dim fuente_prods2 As New Drawing.Font(tipografia, 10, FontStyle.Regular)
        'Variables
        Dim sc As New StringFormat With {.Alignment = StringAlignment.Center}
        Dim sf As New StringFormat With {.Alignment = StringAlignment.Far}
        Dim pen As New Pen(Brushes.Black, 1)
        Dim Y As Double = 0
        Dim nLogo As String = DatosRecarga("LogoG")
        Dim Logotipo As Drawing.Image = Nothing
        Dim tLogo As String = DatosRecarga("TipoLogo")
        Dim simbolo As String = DatosRecarga("Simbolo")
        Dim Pie As String = ""

        Try
            '[°]. Logotipo
            If tLogo <> "SIN" Then
                If File.Exists(My.Application.Info.DirectoryPath & "\" & nLogo) Then
                    Logotipo = Drawing.Image.FromFile(My.Application.Info.DirectoryPath & "\" & nLogo)
                End If
                If tLogo = "CUAD" Then
                    e.Graphics.DrawImage(Logotipo, 80, 0, 120, 120)
                    Y += 130
                End If
                If tLogo = "RECT" Then
                    e.Graphics.DrawImage(Logotipo, 30, 0, 240, 110)
                    Y += 120
                End If
            Else
                Y = 0
            End If

            '[1]. Datos de la venta
            e.Graphics.DrawString("--------------------------------------------------------", New Drawing.Font(tipografia, 12, FontStyle.Regular), Brushes.Black, 1, Y)
            Y += 15
            e.Graphics.DrawString("VENTAS GLOBALES", New Drawing.Font(tipografia, 12, FontStyle.Bold), Brushes.Black, 140, Y, sc)
            Y += 12
            e.Graphics.DrawString("--------------------------------------------------------", New Drawing.Font(tipografia, 12, FontStyle.Regular), Brushes.Black, 1, Y)
            Y += 18

            e.Graphics.DrawString("Fecha Inicial: " & Format(dtpInicial.Value, "yyyy-MM-dd"), fuente_prods, Brushes.Black, 1, Y)
            Y += 15
            e.Graphics.DrawString("Fecha Final: " & Format(dtpFinal.Value, "yyyy-MM-dd"), fuente_prods2, Brushes.Black, 1, Y)
            Y += 13
            e.Graphics.DrawString("--------------------------------------------------------", New Drawing.Font(tipografia, 12, FontStyle.Regular), Brushes.Black, 1, Y)
            Y += 20

            e.Graphics.DrawString("Folio", New Drawing.Font(tipografia, 9, FontStyle.Bold), Brushes.Black, 1, Y)
            e.Graphics.DrawString("Subtotal", New Drawing.Font(tipografia, 9, FontStyle.Bold), Brushes.Black, 50, Y)
            e.Graphics.DrawString("Descuento", New Drawing.Font(tipografia, 9, FontStyle.Bold), Brushes.Black, 195, Y, sf)
            e.Graphics.DrawString("Total", New Drawing.Font(tipografia, 9, FontStyle.Bold), Brushes.Black, 250, Y, sf)
            Y += 20

            For luffy As Integer = 0 To grdVentas.Rows.Count - 1

                Dim folio As Integer = grdVentas.Rows(luffy).Cells(0).Value.ToString
                Dim subtotal As Double = grdVentas.Rows(luffy).Cells(1).Value.ToString
                Dim descuento As Double = grdVentas.Rows(luffy).Cells(2).Value.ToString
                Dim total As Double = grdVentas.Rows(luffy).Cells(3).Value.ToString

                e.Graphics.DrawString(folio, fuente_prods, Brushes.Black, 1, Y)
                e.Graphics.DrawString(subtotal, fuente_prods, Brushes.Black, 60, Y)
                e.Graphics.DrawString(simbolo & descuento, fuente_prods, Brushes.Black, 180, Y, sf)
                e.Graphics.DrawString(simbolo & FormatNumber(total, 2), fuente_prods, Brushes.Black, 270, Y, sf)
                Y += 15
            Next
            Y += 3
            e.Graphics.DrawString("--------------------------------------------------------", New Drawing.Font(tipografia, 12, FontStyle.Regular), Brushes.Black, 1, Y)
            Y += 20
            e.Graphics.DrawString("TOTAL DE VENTAS:", New Drawing.Font(tipografia, 9, FontStyle.Bold), Brushes.Black, 1, Y)
            e.Graphics.DrawString(lbltotal.Text, New Drawing.Font(tipografia, 9, FontStyle.Bold), Brushes.Black, 270, Y, sf)

        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
    End Sub

    Private Sub btnImprimir_Click(sender As Object, e As EventArgs) Handles btnImprimir.Click
        Dim tamimpresora As Integer = TamImpre()
        Dim impresora As String = ImpresoraImprimir()

        If impresora = "" Then MsgBox("No cuentas con una impresora configurada.") : Exit Sub

        If tamimpresora = "80" Then

            PFolios80.DefaultPageSettings.PrinterSettings.PrinterName = impresora
            Dim ps As New System.Drawing.Printing.PaperSize("Custom", 297, 3000)
            PFolios80.DefaultPageSettings.PaperSize = ps
            PFolios80.Print()
        End If

        If tamimpresora = "58" Then

        End If
    End Sub
End Class