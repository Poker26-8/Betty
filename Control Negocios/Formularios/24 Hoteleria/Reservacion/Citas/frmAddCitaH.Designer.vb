<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAddCitaH
    Inherits System.Windows.Forms.Form

    'Form reemplaza a Dispose para limpiar la lista de componentes.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
    'Se puede modificar usando el Diseñador de Windows Forms.  
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmAddCitaH))
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cbocliente = New System.Windows.Forms.ComboBox()
        Me.cboUsuario = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.cboHabitacion = New System.Windows.Forms.ComboBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.txtTelefono = New System.Windows.Forms.TextBox()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.cboAñoSa = New System.Windows.Forms.ComboBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.cboMesSa = New System.Windows.Forms.ComboBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.cboMinutoSa = New System.Windows.Forms.ComboBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.txtDiaSa = New System.Windows.Forms.TextBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.cboHoraSA = New System.Windows.Forms.ComboBox()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.cboAño = New System.Windows.Forms.ComboBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.cboMes = New System.Windows.Forms.ComboBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.cboMinuto = New System.Windows.Forms.ComboBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtDia = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.cboHora = New System.Windows.Forms.ComboBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.btnCancelar = New System.Windows.Forms.Button()
        Me.btnAGregar = New System.Windows.Forms.Button()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.rtAsunto = New System.Windows.Forms.RichTextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.txtIne = New System.Windows.Forms.TextBox()
        Me.Panel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(3, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(69, 21)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Cliente:"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(3, 62)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(65, 21)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Usuario:"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cbocliente
        '
        Me.cbocliente.FormattingEnabled = True
        Me.cbocliente.Location = New System.Drawing.Point(78, 9)
        Me.cbocliente.Name = "cbocliente"
        Me.cbocliente.Size = New System.Drawing.Size(423, 21)
        Me.cbocliente.TabIndex = 2
        '
        'cboUsuario
        '
        Me.cboUsuario.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboUsuario.FormattingEnabled = True
        Me.cboUsuario.Location = New System.Drawing.Point(78, 62)
        Me.cboUsuario.Name = "cboUsuario"
        Me.cboUsuario.Size = New System.Drawing.Size(160, 21)
        Me.cboUsuario.TabIndex = 3
        '
        'Label3
        '
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(244, 62)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(92, 21)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "Habitación:"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cboHabitacion
        '
        Me.cboHabitacion.BackColor = System.Drawing.Color.White
        Me.cboHabitacion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboHabitacion.FormattingEnabled = True
        Me.cboHabitacion.Location = New System.Drawing.Point(331, 62)
        Me.cboHabitacion.Name = "cboHabitacion"
        Me.cboHabitacion.Size = New System.Drawing.Size(170, 21)
        Me.cboHabitacion.TabIndex = 5
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.txtIne)
        Me.Panel1.Controls.Add(Me.Label18)
        Me.Panel1.Controls.Add(Me.txtTelefono)
        Me.Panel1.Controls.Add(Me.Label17)
        Me.Panel1.Controls.Add(Me.cboAñoSa)
        Me.Panel1.Controls.Add(Me.Label12)
        Me.Panel1.Controls.Add(Me.cboMesSa)
        Me.Panel1.Controls.Add(Me.Label13)
        Me.Panel1.Controls.Add(Me.cboMinutoSa)
        Me.Panel1.Controls.Add(Me.Label14)
        Me.Panel1.Controls.Add(Me.txtDiaSa)
        Me.Panel1.Controls.Add(Me.Label15)
        Me.Panel1.Controls.Add(Me.cboHoraSA)
        Me.Panel1.Controls.Add(Me.Label16)
        Me.Panel1.Controls.Add(Me.Label11)
        Me.Panel1.Controls.Add(Me.Label10)
        Me.Panel1.Controls.Add(Me.cboAño)
        Me.Panel1.Controls.Add(Me.Label8)
        Me.Panel1.Controls.Add(Me.cboMes)
        Me.Panel1.Controls.Add(Me.Label7)
        Me.Panel1.Controls.Add(Me.cboMinuto)
        Me.Panel1.Controls.Add(Me.Label6)
        Me.Panel1.Controls.Add(Me.txtDia)
        Me.Panel1.Controls.Add(Me.Label5)
        Me.Panel1.Controls.Add(Me.cboHora)
        Me.Panel1.Controls.Add(Me.Label4)
        Me.Panel1.Controls.Add(Me.cbocliente)
        Me.Panel1.Controls.Add(Me.cboHabitacion)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.cboUsuario)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(510, 250)
        Me.Panel1.TabIndex = 6
        '
        'txtTelefono
        '
        Me.txtTelefono.Location = New System.Drawing.Point(78, 36)
        Me.txtTelefono.Name = "txtTelefono"
        Me.txtTelefono.Size = New System.Drawing.Size(160, 20)
        Me.txtTelefono.TabIndex = 31
        '
        'Label17
        '
        Me.Label17.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.Location = New System.Drawing.Point(3, 36)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(82, 20)
        Me.Label17.TabIndex = 30
        Me.Label17.Text = "Telefono:"
        Me.Label17.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cboAñoSa
        '
        Me.cboAñoSa.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboAñoSa.FormattingEnabled = True
        Me.cboAñoSa.Location = New System.Drawing.Point(145, 220)
        Me.cboAñoSa.Name = "cboAñoSa"
        Me.cboAñoSa.Size = New System.Drawing.Size(86, 21)
        Me.cboAñoSa.TabIndex = 29
        '
        'Label12
        '
        Me.Label12.BackColor = System.Drawing.Color.DodgerBlue
        Me.Label12.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.ForeColor = System.Drawing.Color.White
        Me.Label12.Location = New System.Drawing.Point(145, 195)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(86, 21)
        Me.Label12.TabIndex = 28
        Me.Label12.Text = "Año"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'cboMesSa
        '
        Me.cboMesSa.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboMesSa.FormattingEnabled = True
        Me.cboMesSa.Location = New System.Drawing.Point(8, 220)
        Me.cboMesSa.Name = "cboMesSa"
        Me.cboMesSa.Size = New System.Drawing.Size(131, 21)
        Me.cboMesSa.TabIndex = 27
        '
        'Label13
        '
        Me.Label13.BackColor = System.Drawing.Color.DodgerBlue
        Me.Label13.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.ForeColor = System.Drawing.Color.White
        Me.Label13.Location = New System.Drawing.Point(8, 195)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(131, 21)
        Me.Label13.TabIndex = 26
        Me.Label13.Text = "Mes"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'cboMinutoSa
        '
        Me.cboMinutoSa.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboMinutoSa.FormattingEnabled = True
        Me.cboMinutoSa.Location = New System.Drawing.Point(414, 219)
        Me.cboMinutoSa.Name = "cboMinutoSa"
        Me.cboMinutoSa.Size = New System.Drawing.Size(87, 21)
        Me.cboMinutoSa.TabIndex = 25
        '
        'Label14
        '
        Me.Label14.BackColor = System.Drawing.Color.DodgerBlue
        Me.Label14.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.ForeColor = System.Drawing.Color.White
        Me.Label14.Location = New System.Drawing.Point(414, 195)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(87, 21)
        Me.Label14.TabIndex = 24
        Me.Label14.Text = "Minuto"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtDiaSa
        '
        Me.txtDiaSa.Location = New System.Drawing.Point(235, 220)
        Me.txtDiaSa.Name = "txtDiaSa"
        Me.txtDiaSa.Size = New System.Drawing.Size(87, 20)
        Me.txtDiaSa.TabIndex = 23
        Me.txtDiaSa.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label15
        '
        Me.Label15.BackColor = System.Drawing.Color.DodgerBlue
        Me.Label15.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.ForeColor = System.Drawing.Color.White
        Me.Label15.Location = New System.Drawing.Point(324, 195)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(87, 21)
        Me.Label15.TabIndex = 22
        Me.Label15.Text = "Hora"
        Me.Label15.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'cboHoraSA
        '
        Me.cboHoraSA.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboHoraSA.FormattingEnabled = True
        Me.cboHoraSA.Location = New System.Drawing.Point(324, 219)
        Me.cboHoraSA.Name = "cboHoraSA"
        Me.cboHoraSA.Size = New System.Drawing.Size(87, 21)
        Me.cboHoraSA.TabIndex = 21
        '
        'Label16
        '
        Me.Label16.BackColor = System.Drawing.Color.DodgerBlue
        Me.Label16.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.ForeColor = System.Drawing.Color.White
        Me.Label16.Location = New System.Drawing.Point(236, 195)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(86, 21)
        Me.Label16.TabIndex = 20
        Me.Label16.Text = "Día"
        Me.Label16.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label11
        '
        Me.Label11.BackColor = System.Drawing.Color.DodgerBlue
        Me.Label11.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.ForeColor = System.Drawing.Color.White
        Me.Label11.Location = New System.Drawing.Point(8, 86)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(493, 21)
        Me.Label11.TabIndex = 19
        Me.Label11.Text = "Fecha de Entrada"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label10
        '
        Me.Label10.BackColor = System.Drawing.Color.DodgerBlue
        Me.Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.Color.White
        Me.Label10.Location = New System.Drawing.Point(8, 165)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(493, 18)
        Me.Label10.TabIndex = 16
        Me.Label10.Text = "Fecha de Salida"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'cboAño
        '
        Me.cboAño.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboAño.FormattingEnabled = True
        Me.cboAño.Location = New System.Drawing.Point(145, 141)
        Me.cboAño.Name = "cboAño"
        Me.cboAño.Size = New System.Drawing.Size(86, 21)
        Me.cboAño.TabIndex = 15
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.DodgerBlue
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.Color.White
        Me.Label8.Location = New System.Drawing.Point(145, 116)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(86, 21)
        Me.Label8.TabIndex = 14
        Me.Label8.Text = "Año"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'cboMes
        '
        Me.cboMes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboMes.FormattingEnabled = True
        Me.cboMes.Location = New System.Drawing.Point(8, 141)
        Me.cboMes.Name = "cboMes"
        Me.cboMes.Size = New System.Drawing.Size(131, 21)
        Me.cboMes.TabIndex = 13
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.DodgerBlue
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.Color.White
        Me.Label7.Location = New System.Drawing.Point(8, 116)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(131, 21)
        Me.Label7.TabIndex = 12
        Me.Label7.Text = "Mes"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'cboMinuto
        '
        Me.cboMinuto.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboMinuto.FormattingEnabled = True
        Me.cboMinuto.Location = New System.Drawing.Point(414, 140)
        Me.cboMinuto.Name = "cboMinuto"
        Me.cboMinuto.Size = New System.Drawing.Size(87, 21)
        Me.cboMinuto.TabIndex = 11
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.DodgerBlue
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.White
        Me.Label6.Location = New System.Drawing.Point(414, 116)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(87, 21)
        Me.Label6.TabIndex = 10
        Me.Label6.Text = "Minuto"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtDia
        '
        Me.txtDia.Location = New System.Drawing.Point(235, 141)
        Me.txtDia.Name = "txtDia"
        Me.txtDia.Size = New System.Drawing.Size(87, 20)
        Me.txtDia.TabIndex = 9
        Me.txtDia.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.DodgerBlue
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.White
        Me.Label5.Location = New System.Drawing.Point(324, 116)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(87, 21)
        Me.Label5.TabIndex = 8
        Me.Label5.Text = "Hora"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'cboHora
        '
        Me.cboHora.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboHora.FormattingEnabled = True
        Me.cboHora.Location = New System.Drawing.Point(324, 140)
        Me.cboHora.Name = "cboHora"
        Me.cboHora.Size = New System.Drawing.Size(87, 21)
        Me.cboHora.TabIndex = 7
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.DodgerBlue
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.White
        Me.Label4.Location = New System.Drawing.Point(236, 116)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(86, 21)
        Me.Label4.TabIndex = 6
        Me.Label4.Text = "Día"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.btnCancelar)
        Me.Panel2.Controls.Add(Me.btnAGregar)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel2.Location = New System.Drawing.Point(0, 456)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(510, 59)
        Me.Panel2.TabIndex = 7
        '
        'btnCancelar
        '
        Me.btnCancelar.BackColor = System.Drawing.Color.DodgerBlue
        Me.btnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnCancelar.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCancelar.ForeColor = System.Drawing.Color.White
        Me.btnCancelar.Image = CType(resources.GetObject("btnCancelar.Image"), System.Drawing.Image)
        Me.btnCancelar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnCancelar.Location = New System.Drawing.Point(378, 3)
        Me.btnCancelar.Name = "btnCancelar"
        Me.btnCancelar.Size = New System.Drawing.Size(123, 53)
        Me.btnCancelar.TabIndex = 1
        Me.btnCancelar.Text = "Cancelar"
        Me.btnCancelar.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnCancelar.UseVisualStyleBackColor = False
        '
        'btnAGregar
        '
        Me.btnAGregar.BackColor = System.Drawing.Color.DodgerBlue
        Me.btnAGregar.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnAGregar.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAGregar.ForeColor = System.Drawing.Color.White
        Me.btnAGregar.Image = CType(resources.GetObject("btnAGregar.Image"), System.Drawing.Image)
        Me.btnAGregar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnAGregar.Location = New System.Drawing.Point(247, 3)
        Me.btnAGregar.Name = "btnAGregar"
        Me.btnAGregar.Size = New System.Drawing.Size(125, 53)
        Me.btnAGregar.TabIndex = 0
        Me.btnAGregar.Text = "Agregar"
        Me.btnAGregar.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnAGregar.UseVisualStyleBackColor = False
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.rtAsunto)
        Me.Panel3.Controls.Add(Me.Label9)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel3.Location = New System.Drawing.Point(0, 250)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(510, 206)
        Me.Panel3.TabIndex = 8
        '
        'rtAsunto
        '
        Me.rtAsunto.Dock = System.Windows.Forms.DockStyle.Fill
        Me.rtAsunto.Location = New System.Drawing.Point(0, 26)
        Me.rtAsunto.Name = "rtAsunto"
        Me.rtAsunto.Size = New System.Drawing.Size(510, 180)
        Me.rtAsunto.TabIndex = 8
        Me.rtAsunto.Text = ""
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.Color.DodgerBlue
        Me.Label9.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.Color.White
        Me.Label9.Location = New System.Drawing.Point(0, 0)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(510, 26)
        Me.Label9.TabIndex = 7
        Me.Label9.Text = "DESCRIPCIÓN"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label18
        '
        Me.Label18.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.Location = New System.Drawing.Point(240, 36)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(45, 20)
        Me.Label18.TabIndex = 32
        Me.Label18.Text = "INE:"
        Me.Label18.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtIne
        '
        Me.txtIne.Location = New System.Drawing.Point(280, 36)
        Me.txtIne.Name = "txtIne"
        Me.txtIne.Size = New System.Drawing.Size(221, 20)
        Me.txtIne.TabIndex = 33
        '
        'frmAddCitaH
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(510, 515)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmAddCitaH"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Nueva Cita"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents cbocliente As ComboBox
    Friend WithEvents cboUsuario As ComboBox
    Friend WithEvents Label3 As Label
    Friend WithEvents cboHabitacion As ComboBox
    Friend WithEvents Panel1 As Panel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents btnAGregar As Button
    Friend WithEvents btnCancelar As Button
    Friend WithEvents cboHora As ComboBox
    Friend WithEvents Label4 As Label
    Friend WithEvents cboAño As ComboBox
    Friend WithEvents Label8 As Label
    Friend WithEvents cboMes As ComboBox
    Friend WithEvents Label7 As Label
    Friend WithEvents cboMinuto As ComboBox
    Friend WithEvents Label6 As Label
    Friend WithEvents txtDia As TextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents Panel3 As Panel
    Friend WithEvents rtAsunto As RichTextBox
    Friend WithEvents Label9 As Label
    Friend WithEvents Label10 As Label
    Friend WithEvents Label11 As Label
    Friend WithEvents cboAñoSa As ComboBox
    Friend WithEvents Label12 As Label
    Friend WithEvents cboMesSa As ComboBox
    Friend WithEvents Label13 As Label
    Friend WithEvents cboMinutoSa As ComboBox
    Friend WithEvents Label14 As Label
    Friend WithEvents txtDiaSa As TextBox
    Friend WithEvents Label15 As Label
    Friend WithEvents cboHoraSA As ComboBox
    Friend WithEvents Label16 As Label
    Friend WithEvents txtTelefono As TextBox
    Friend WithEvents Label17 As Label
    Friend WithEvents txtIne As TextBox
    Friend WithEvents Label18 As Label
End Class
