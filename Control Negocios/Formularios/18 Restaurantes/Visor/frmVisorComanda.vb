Public Class frmVisorComanda

    Friend WithEvents panelComanda As System.Windows.Forms.Panel
    Friend WithEvents labelComanda As System.Windows.Forms.Label

    Private Sub frmVisorComanda_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        crear_paneles()
        Timer1.Start()
    End Sub

    Public Sub crear_paneles()
        Try
            'Información y características de cada panel / label
            Dim comanda As Integer = 0
            Dim nombre_panel As String = ""
            Dim id_panel As String = ""

            Dim nombre_label As String = ""
            Dim cantidad As Decimal = 0
            Dim producto As String = ""
            Dim nombre_mesa As String = ""

            'Medidas de posicionamiento de cada panel / label
            Dim left_panel As Integer = 0
            Dim paroimpar As Integer = 0
            Dim conteo_panel As Integer = 1
            Dim top_label As Integer = 0
            Dim maxleft As Integer = Math.Truncate(PComandas.Width / 250)
            Dim conteo_panes As Integer = 0

            Dim condicion_left As Double = 0

            cnn1.Close()
            PComandas.Controls.Clear()
            cnn1.Close()
            cnn1.Open()

            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "select distinct id from Comandas where Estado=0"
            rd1 = cmd1.ExecuteReader

            Do While rd1.Read
                If rd1.HasRows Then
                    id_panel = rd1(0).ToString

                    'Se crea el panel y se configura
                    panelComanda = New Panel
                    panelComanda.Name = id_panel
                    panelComanda.Width = 250
                    panelComanda.ForeColor = Color.Black
                    panelComanda.BorderStyle = BorderStyle.FixedSingle

                    'Alto del panel. va a ocupar la mitad de la pantalla
                    panelComanda.Height = PComandas.Height / 2
                    If conteo_panes <= maxleft - 1 Then
                        panelComanda.Top = 0
                    Else
                        condicion_left = (maxleft - 1) * 250
                        If left_panel > condicion_left Then
                            left_panel = 0
                        End If
                        panelComanda.Top = PComandas.Height / 2
                    End If

                    panelComanda.Left = left_panel

                    paroimpar = conteo_panel Mod 2
                    If paroimpar = 0 Then
                        panelComanda.BackColor = Color.Wheat
                    Else
                        panelComanda.BackColor = Color.Khaki
                    End If
                    AddHandler panelComanda.Click, AddressOf panelComanda_Click
                    PComandas.Controls.Add(panelComanda)
                    panelComanda.Controls.Clear()
                    cnn2.Close()
                    cnn2.Open()

                    cmd2 = cnn2.CreateCommand
                    ' cmd2.CommandText = "select * from Comandas where id=" & id_panel & " and (GPrint='MONITOR' or GPrint='EXTRAS') and Estado=0 order by IDC"
                    cmd2.CommandText = "select * from Comandas where id=" & id_panel & " and Estado=0 order by IDC"
                    rd2 = cmd2.ExecuteReader

                    Do While rd2.Read
                        If rd2.HasRows Then
                            labelComanda = New Label

                            nombre_mesa = rd2("NMESA").ToString

                            labelComanda.Name = rd2("IDC").ToString & "_" & rd2("Nombre").ToString
                            labelComanda.Width = panelComanda.Width
                            labelComanda.Height = 23

                            labelComanda.Top = top_label
                            labelComanda.TextAlign = ContentAlignment.MiddleLeft
                            labelComanda.Font = New Font("Lucida Sans Typewriter", 12, FontStyle.Regular)
                            If rd2("Grupo").ToString = "EXTRAS" Then
                                labelComanda.Text = "     " & rd2("Nombre").ToString
                            Else
                                labelComanda.Text = rd2("Cantidad").ToString & "   " & rd2("Nombre").ToString
                            End If
                            AddHandler labelComanda.Click, AddressOf labelComanda_Click
                            panelComanda.Controls.Add(labelComanda)

                            If rd2("Comentario").ToString <> "" Then
                                top_label += 23
                                labelComanda = New Label

                                labelComanda.Name = rd2("IDC").ToString & "_" & Mid(rd2("Comentario").ToString, 1, 7)

                                labelComanda.Top = top_label
                                labelComanda.TextAlign = ContentAlignment.TopLeft
                                labelComanda.AutoSize = False
                                labelComanda.Width = panelComanda.Width
                                labelComanda.Height = 46
                                labelComanda.Font = New Font("Lucida Sans Typewriter", 10, FontStyle.Regular)
                                labelComanda.Text = "     - " & rd2("Comentario").ToString
                                AddHandler labelComanda.Click, AddressOf labelComanda_Click
                                panelComanda.Controls.Add(labelComanda)
                                top_label += 46
                            Else
                                top_label += 23
                            End If
                        End If
                    Loop
                    top_label = 0

                    rd2.Close()
                    cnn2.Close()

                    labelComanda = New Label
                    labelComanda.Name = "lbl" & Trim(nombre_mesa)
                    labelComanda.Dock = DockStyle.Bottom
                    labelComanda.Font = New Font("Lucida Sans Typewriter", 11, FontStyle.Bold)
                    labelComanda.Text = nombre_mesa
                    labelComanda.TextAlign = ContentAlignment.MiddleCenter
                    panelComanda.Controls.Add(labelComanda)
                End If
                left_panel += 250
                conteo_panel += 1
                conteo_panes = conteo_panes + 1
            Loop
            rd1.Close()
            cnn1.Close()
            cnn2.Close()
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
            cnn1.Close()
            cnn2.Close()
        End Try
    End Sub

    Private Sub panelComanda_Click(ByVal sender As Object, ByVal e As EventArgs)

    End Sub

    Private Sub labelComanda_Click(ByVal sender As Object, ByVal e As EventArgs)

    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Timer1.Stop()
        crear_paneles()
        Timer1.Start()
    End Sub
End Class