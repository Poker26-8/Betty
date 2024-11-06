Imports System.IO
Imports System.Runtime.InteropServices
Imports ClosedXML.Excel.XLPredefinedFormat
Imports Core.DAL.DE
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Public Class frmNuevo

    Friend WithEvents btnMesa As System.Windows.Forms.Button
    Dim btnaccion = New DataGridViewButtonColumn()
    Dim btnaccion2 = New DataGridViewTextBoxCell()
    Dim rowIndex As Integer = 0

    Dim numcompleto As String = ""
    Friend WithEvents btnDepto As System.Windows.Forms.Button
    Dim TotDeptos As Integer = 0


    ' Define constantes para el ancho del scroll
    Private Const SB_THUMBPOSITION As Integer = 4
    Private Const SB_VERT As Integer = 1

    Dim deptos As Integer = 0

    Public currentIndex As Integer = 0
    ' Tamaño del panel (especificado en píxeles)
    Public panelHeight As Integer = 0
    ' Número de controles a mostrar por clic
    Public controlsPerClick As Integer = 6


    Private Sub frmNuevo_Load(sender As Object, e As EventArgs) Handles MyBase.Load


        Departamentos()

        ' Cambiar el ancho del scroll del Panel1 a 30 píxeles
        ChangeScrollBarWidth(pDeptos, 30)

    End Sub

    ' DLL para cambiar el ancho del scroll
    <DllImport("user32.dll", CharSet:=CharSet.Auto)>
    Public Shared Function SetScrollInfo(hWnd As IntPtr, nBar As Integer, ByRef lpsi As SCROLLINFO, bRepaint As Boolean) As Integer
    End Function

    ' Estructura para modificar la barra de scroll
    <StructLayout(LayoutKind.Sequential)>
    Public Structure SCROLLINFO
        Public cbSize As UInteger
        Public fMask As UInteger
        Public nMin As Integer
        Public nMax As Integer
        Public nPage As UInteger
        Public nPos As Integer
        Public nTrackPos As Integer
    End Structure

    ' Cambiar el ancho del scroll
    Private Sub ChangeScrollBarWidth(ByVal panel As Panel, ByVal width As Integer)
        Dim si As New SCROLLINFO()
        si.cbSize = CUInt(Marshal.SizeOf(si))
        si.fMask = &H1 Or &H2 ' SIF_RANGE y SIF_PAGE
        si.nMin = 0
        si.nMax = panel.VerticalScroll.Maximum
        si.nPage = CUInt(panel.ClientSize.Height)

        ' Asignar nuevo ancho de scroll
        SetScrollInfo(panel.Handle, SB_VERT, si, True)

        ' Cambiar el tamaño de la barra
        panel.VerticalScroll.SmallChange = width
    End Sub





    Private Sub btnPdf_Click(sender As Object, e As EventArgs) Handles btnPdf.Click

        DataGridView1.Rows.Clear()

        Dim id As String = ""
        Dim codigo As String = ""
        Dim codunico As String = ""
        cnn1.Close() : cnn1.Open()
        cnn2.Close() : cnn2.Open()
        cmd1 = cnn1.CreateCommand
        cmd1.CommandText = "SELECT Id,Codigo,CodUnico FROM ventasdetalle WHERE Folio=21"
        rd1 = cmd1.ExecuteReader
        Do While rd1.Read
            If rd1.HasRows Then
                id = rd1(0).ToString
                codigo = rd1(1).ToString
                codunico = rd1(2).ToString

                DataGridView1.Rows.Add(id, codigo, codunico)
            End If
        Loop
        rd1.Close()

        Dim cunico As String = ""
        Dim ideli As Integer = 0
        Dim suma As Integer = 0
        For LUFFY As Integer = 0 To DataGridView1.Rows.Count - 1
            cunico = DataGridView1.Rows(LUFFY).Cells(2).Value.ToString

            cmd2 = cnn2.CreateCommand
            cmd2.CommandText = "SELECT COUNT(CodUnico),Id from ventasdetalle WHERE CodUnico='" & cunico & "' AND Folio='21'"
            rd2 = cmd2.ExecuteReader
            If rd2.HasRows Then
                If rd2.Read Then
                    suma = rd2(0).ToString
                    ideli = rd2(1).ToString
                    If suma > 1 Then
                        cnn3.Close() : cnn3.Open()
                        cmd3 = cnn3.CreateCommand
                        cmd3.CommandText = "DELETE FROM ventasdetalle WHERE Id=" & ideli & " AND CodUnico='" & cunico & "'"
                        cmd3.ExecuteNonQuery()
                        cnn3.Close()
                    End If
                End If
            Else

            End If
            rd2.Close()

        Next
        cnn1.Close()
        cnn2.Close()

    End Sub

    Private Sub ComboBox1_DropDown(sender As Object, e As EventArgs) Handles ComboBox1.DropDown
        Try
            ComboBox1.Items.Clear()

            cnn5.Close() : cnn5.Open()
            cmd5 = cnn5.CreateCommand
            cmd5.CommandText = "SELECT DISTINCT Codigo FROM productos WHERE Codigo<>'' ORDER BY Codigo"
            rd5 = cmd5.ExecuteReader
            Do While rd5.Read
                If rd5.HasRows Then
                    ComboBox1.Items.Add(rd5(0).ToString)
                End If
            Loop
            rd5.Close()
            cnn5.Close()

        Catch ex As Exception
            MessageBox.Show(ex.ToString)
            cnn5.Close()
        End Try
    End Sub

    Private Sub ComboBox1_SelectedValueChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedValueChanged
        Try
            cnn1.Close() : cnn1.Open()
            cmd1 = cnn1.CreateCommand
            cmd1.CommandText = "SELECT CodBarra FROM productos WHERE Codigo='" & ComboBox1.Text & "'"
            rd1 = cmd1.ExecuteReader
            If rd1.HasRows Then
                If rd1.Read Then
                    txtbarras.Text = rd1(0).ToString
                End If
            End If
            rd1.Close()
            cnn1.Close()
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
            cnn1.Close()
        End Try
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click



        Dim peso As String = txtpeso.Text
        Dim numeroSinPunto As String = peso.ToString().Replace(".", "")

        Dim resultado As String = ""
        Dim barras As String = txtbarras.Text

        If barras.Length = 2 Then
            resultado = "0" & barras
        Else
            resultado = barras
        End If

        numcompleto = txtInicial.Text & txtnumfijo.Text & resultado & txtfijo.Text & numeroSinPunto & txtalazar.Text
        txtticket.Text = numcompleto

        '55200010200295520001060030572000038005501

        'hay que cortar los primeros 2 digitos

        Dim primeros2num As String = txtconvertir.Text


    End Sub

    Private Sub Departamentos()

        Try

            cnn1.Close() : cnn1.Open()

            cmd1 = cnn1.CreateCommand
            cmd1.CommandText =
                "select distinct Departamento from Productos where Departamento<>'' order by Departamento"
            rd1 = cmd1.ExecuteReader
            Do While rd1.Read
                If rd1.HasRows Then
                    Dim departamento As String = rd1(0).ToString
                    btnDepto = New Button
                    btnDepto.Text = departamento
                    btnDepto.Name = "btnDepto(" & deptos & ")"
                    btnDepto.Left = 0
                    btnDepto.Height = 55
                    If TotDeptos <= 10 Then
                        btnDepto.Width = pDeptos.Width
                    Else
                        btnDepto.Width = pDeptos.Width - 17
                    End If
                    btnDepto.Top = (deptos) * (btnDepto.Height + 0.5)
                    btnDepto.BackColor = pDeptos.BackColor
                    btnDepto.FlatStyle = FlatStyle.Flat
                    btnDepto.Font = New Font("Segoe UI", 10, FontStyle.Bold)
                    btnDepto.FlatAppearance.BorderSize = 0
                    AddHandler btnDepto.Click, AddressOf btnDepto_Click
                    pDeptos.Controls.Add(btnDepto)

                    If deptos = 0 Then
                        ' Grupos(departamento)
                    End If
                    deptos += 1
                End If
            Loop
            rd1.Close() : cnn1.Close()
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
            cnn1.Close()
        End Try
    End Sub

    Private Sub btnDepto_Click(sender As Object, e As EventArgs)
        Dim btnDepartamento As Button = CType(sender, Button)
        btnDepartamento.Font.Bold.Equals(True)
        ' pGrupos.Controls.Clear()
        ' pProductos.Controls.Clear()
        If cnn2.State = 1 Then
            cnn2.Close()
        End If
        ' CantidadProd = 0
        ' Grupos(btnDepartamento.Text)
    End Sub

    Private Sub BTNBAJAR_Click(sender As Object, e As EventArgs) Handles BTNBAJAR.Click
        Dim controlsToShow As Integer = Math.Min(controlsPerClick, pDeptos.Controls.Count - currentIndex)

        For i As Integer = currentIndex To currentIndex + controlsToShow - 1
            pDeptos.Controls(i).Visible = True
        Next

        ' Actualizamos el índice para el siguiente clic
        currentIndex += controlsToShow

        ' Desplazar el panel hacia abajo en una cantidad fija
        ' Este valor depende de la altura de los controles (40px) y el número de controles visibles por clic
        ' If pDeptos.VerticalScroll.Visible Then
        pDeptos.AutoScrollPosition = New Point(0, pDeptos.VerticalScroll.Value + 40)
        'End If
    End Sub



    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim controlsToShow As Integer = Math.Min(controlsPerClick, pDeptos.Controls.Count - currentIndex)

        For i As Integer = currentIndex To currentIndex + controlsToShow - 1
            pDeptos.Controls(i).Visible = True
        Next


        currentIndex += controlsToShow


        pDeptos.AutoScrollPosition = New Point(0, pDeptos.VerticalScroll.Value - 40)
    End Sub

End Class