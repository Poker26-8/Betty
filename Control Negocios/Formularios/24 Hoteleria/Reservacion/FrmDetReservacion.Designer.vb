<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmDetReservacion
    Inherits System.Windows.Forms.Form

    'Form reemplaza a Dispose para limpiar la lista de componentes.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmDetReservacion))
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.txtcontra = New System.Windows.Forms.TextBox()
        Me.lblusuario = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cboCLientes = New System.Windows.Forms.ComboBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.lblTipo = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.lblCaracteristicas = New System.Windows.Forms.Label()
        Me.lblHabitacion = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txttelefono = New System.Windows.Forms.TextBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.cboPrecio = New System.Windows.Forms.ComboBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.txtHoras = New System.Windows.Forms.TextBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.cboTipo = New System.Windows.Forms.ComboBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.btnLimpiar = New System.Windows.Forms.Button()
        Me.btnGuardar = New System.Windows.Forms.Button()
        Me.btnSalir = New System.Windows.Forms.Button()
        Me.lblidcliented = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.cboFolio = New System.Windows.Forms.ComboBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.lblINE = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.lblEntrada = New System.Windows.Forms.Label()
        Me.lblSalida = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.btnAbonar = New System.Windows.Forms.Button()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.Panel7 = New System.Windows.Forms.Panel()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.txtOtro = New System.Windows.Forms.TextBox()
        Me.txtTransfe = New System.Windows.Forms.TextBox()
        Me.txtTarjeta = New System.Windows.Forms.TextBox()
        Me.txtEfectivo = New System.Windows.Forms.TextBox()
        Me.txtAnticipo = New System.Windows.Forms.TextBox()
        Me.lblAnticipo = New System.Windows.Forms.Label()
        Me.txtCambio = New System.Windows.Forms.TextBox()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.txtSubtotal = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtResta = New System.Windows.Forms.TextBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.txtDescuento = New System.Windows.Forms.TextBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.txtTotalVenta = New System.Windows.Forms.TextBox()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Panel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.Panel4.SuspendLayout()
        Me.Panel6.SuspendLayout()
        Me.Panel7.SuspendLayout()
        Me.Panel5.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.Panel2)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(747, 40)
        Me.Panel1.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(0, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(504, 40)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Asignación de reservaciones"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.txtcontra)
        Me.Panel2.Controls.Add(Me.lblusuario)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Right
        Me.Panel2.Location = New System.Drawing.Point(504, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(243, 40)
        Me.Panel2.TabIndex = 1
        '
        'txtcontra
        '
        Me.txtcontra.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtcontra.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.txtcontra.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtcontra.Location = New System.Drawing.Point(125, 10)
        Me.txtcontra.Name = "txtcontra"
        Me.txtcontra.Size = New System.Drawing.Size(115, 22)
        Me.txtcontra.TabIndex = 91
        Me.txtcontra.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.txtcontra.UseSystemPasswordChar = True
        '
        'lblusuario
        '
        Me.lblusuario.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblusuario.BackColor = System.Drawing.Color.SkyBlue
        Me.lblusuario.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblusuario.ForeColor = System.Drawing.Color.Black
        Me.lblusuario.Location = New System.Drawing.Point(4, 10)
        Me.lblusuario.Name = "lblusuario"
        Me.lblusuario.Size = New System.Drawing.Size(115, 22)
        Me.lblusuario.TabIndex = 90
        Me.lblusuario.Text = "Contraseña"
        Me.lblusuario.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(6, 117)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(82, 24)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Cliente:"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cboCLientes
        '
        Me.cboCLientes.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.cboCLientes.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboCLientes.FormattingEnabled = True
        Me.cboCLientes.Location = New System.Drawing.Point(165, 118)
        Me.cboCLientes.Name = "cboCLientes"
        Me.cboCLientes.Size = New System.Drawing.Size(398, 23)
        Me.cboCLientes.TabIndex = 2
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.lblTipo)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.lblCaracteristicas)
        Me.GroupBox1.Controls.Add(Me.lblHabitacion)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.Location = New System.Drawing.Point(3, 6)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(741, 100)
        Me.GroupBox1.TabIndex = 3
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Detalle de Habitación"
        '
        'lblTipo
        '
        Me.lblTipo.BackColor = System.Drawing.Color.SkyBlue
        Me.lblTipo.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTipo.ForeColor = System.Drawing.Color.Black
        Me.lblTipo.Location = New System.Drawing.Point(71, 62)
        Me.lblTipo.Name = "lblTipo"
        Me.lblTipo.Size = New System.Drawing.Size(304, 23)
        Me.lblTipo.TabIndex = 9
        '
        'Label5
        '
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(12, 62)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(53, 23)
        Me.Label5.TabIndex = 8
        Me.Label5.Text = "Tipo:"
        '
        'lblCaracteristicas
        '
        Me.lblCaracteristicas.BackColor = System.Drawing.Color.SkyBlue
        Me.lblCaracteristicas.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCaracteristicas.ForeColor = System.Drawing.Color.Black
        Me.lblCaracteristicas.Location = New System.Drawing.Point(381, 18)
        Me.lblCaracteristicas.Name = "lblCaracteristicas"
        Me.lblCaracteristicas.Size = New System.Drawing.Size(356, 79)
        Me.lblCaracteristicas.TabIndex = 7
        '
        'lblHabitacion
        '
        Me.lblHabitacion.BackColor = System.Drawing.Color.SkyBlue
        Me.lblHabitacion.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHabitacion.ForeColor = System.Drawing.Color.Black
        Me.lblHabitacion.Location = New System.Drawing.Point(113, 28)
        Me.lblHabitacion.Name = "lblHabitacion"
        Me.lblHabitacion.Size = New System.Drawing.Size(262, 23)
        Me.lblHabitacion.TabIndex = 5
        '
        'Label3
        '
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(10, 28)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(118, 23)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "Habitación:"
        '
        'Label4
        '
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(6, 146)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(80, 22)
        Me.Label4.TabIndex = 4
        Me.Label4.Text = "Telefono:"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txttelefono
        '
        Me.txttelefono.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txttelefono.Location = New System.Drawing.Point(92, 146)
        Me.txttelefono.Name = "txttelefono"
        Me.txttelefono.Size = New System.Drawing.Size(170, 22)
        Me.txttelefono.TabIndex = 5
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.cboPrecio)
        Me.GroupBox2.Controls.Add(Me.Label13)
        Me.GroupBox2.Controls.Add(Me.txtHoras)
        Me.GroupBox2.Controls.Add(Me.Label12)
        Me.GroupBox2.Controls.Add(Me.cboTipo)
        Me.GroupBox2.Controls.Add(Me.Label6)
        Me.GroupBox2.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox2.Location = New System.Drawing.Point(9, 174)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(466, 81)
        Me.GroupBox2.TabIndex = 13
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Precios"
        '
        'cboPrecio
        '
        Me.cboPrecio.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboPrecio.FormattingEnabled = True
        Me.cboPrecio.Location = New System.Drawing.Point(318, 41)
        Me.cboPrecio.Name = "cboPrecio"
        Me.cboPrecio.Size = New System.Drawing.Size(142, 24)
        Me.cboPrecio.TabIndex = 5
        Me.cboPrecio.Text = "0.00"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(318, 20)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(49, 20)
        Me.Label13.TabIndex = 4
        Me.Label13.Text = "Total"
        '
        'txtHoras
        '
        Me.txtHoras.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.txtHoras.Location = New System.Drawing.Point(161, 41)
        Me.txtHoras.Name = "txtHoras"
        Me.txtHoras.Size = New System.Drawing.Size(150, 24)
        Me.txtHoras.TabIndex = 3
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(161, 20)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(57, 20)
        Me.Label12.TabIndex = 2
        Me.Label12.Text = "Horas"
        '
        'cboTipo
        '
        Me.cboTipo.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.cboTipo.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboTipo.FormattingEnabled = True
        Me.cboTipo.Location = New System.Drawing.Point(6, 41)
        Me.cboTipo.Name = "cboTipo"
        Me.cboTipo.Size = New System.Drawing.Size(150, 24)
        Me.cboTipo.TabIndex = 1
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(3, 20)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(43, 20)
        Me.Label6.TabIndex = 0
        Me.Label6.Text = "Tipo"
        '
        'btnLimpiar
        '
        Me.btnLimpiar.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnLimpiar.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnLimpiar.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnLimpiar.Image = CType(resources.GetObject("btnLimpiar.Image"), System.Drawing.Image)
        Me.btnLimpiar.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnLimpiar.Location = New System.Drawing.Point(416, 3)
        Me.btnLimpiar.Name = "btnLimpiar"
        Me.btnLimpiar.Size = New System.Drawing.Size(75, 71)
        Me.btnLimpiar.TabIndex = 21
        Me.btnLimpiar.Text = "Limpiar"
        Me.btnLimpiar.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnLimpiar.UseVisualStyleBackColor = True
        '
        'btnGuardar
        '
        Me.btnGuardar.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnGuardar.Enabled = False
        Me.btnGuardar.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnGuardar.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnGuardar.Image = CType(resources.GetObject("btnGuardar.Image"), System.Drawing.Image)
        Me.btnGuardar.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnGuardar.Location = New System.Drawing.Point(578, 3)
        Me.btnGuardar.Name = "btnGuardar"
        Me.btnGuardar.Size = New System.Drawing.Size(84, 71)
        Me.btnGuardar.TabIndex = 20
        Me.btnGuardar.Text = "Hospedar"
        Me.btnGuardar.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnGuardar.UseVisualStyleBackColor = True
        '
        'btnSalir
        '
        Me.btnSalir.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSalir.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnSalir.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSalir.Image = CType(resources.GetObject("btnSalir.Image"), System.Drawing.Image)
        Me.btnSalir.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnSalir.Location = New System.Drawing.Point(668, 3)
        Me.btnSalir.Name = "btnSalir"
        Me.btnSalir.Size = New System.Drawing.Size(75, 71)
        Me.btnSalir.TabIndex = 19
        Me.btnSalir.Text = "Salir"
        Me.btnSalir.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnSalir.UseVisualStyleBackColor = True
        '
        'lblidcliented
        '
        Me.lblidcliented.BackColor = System.Drawing.Color.SkyBlue
        Me.lblidcliented.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblidcliented.ForeColor = System.Drawing.Color.Black
        Me.lblidcliented.Location = New System.Drawing.Point(89, 118)
        Me.lblidcliented.Name = "lblidcliented"
        Me.lblidcliented.Size = New System.Drawing.Size(70, 22)
        Me.lblidcliented.TabIndex = 94
        Me.lblidcliented.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label8
        '
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(569, 117)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(72, 23)
        Me.Label8.TabIndex = 95
        Me.Label8.Text = "Folio:"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cboFolio
        '
        Me.cboFolio.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboFolio.FormattingEnabled = True
        Me.cboFolio.Location = New System.Drawing.Point(632, 117)
        Me.cboFolio.Name = "cboFolio"
        Me.cboFolio.Size = New System.Drawing.Size(108, 23)
        Me.cboFolio.TabIndex = 96
        '
        'Label9
        '
        Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(268, 145)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(51, 23)
        Me.Label9.TabIndex = 97
        Me.Label9.Text = "INE:"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblINE
        '
        Me.lblINE.BackColor = System.Drawing.Color.SkyBlue
        Me.lblINE.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblINE.ForeColor = System.Drawing.Color.Black
        Me.lblINE.Location = New System.Drawing.Point(310, 145)
        Me.lblINE.Name = "lblINE"
        Me.lblINE.Size = New System.Drawing.Size(165, 23)
        Me.lblINE.TabIndex = 10
        Me.lblINE.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label11
        '
        Me.Label11.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(573, 190)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(168, 22)
        Me.Label11.TabIndex = 98
        Me.Label11.Text = "Fecha Salida"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblEntrada
        '
        Me.lblEntrada.BackColor = System.Drawing.Color.SkyBlue
        Me.lblEntrada.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEntrada.ForeColor = System.Drawing.Color.Black
        Me.lblEntrada.Location = New System.Drawing.Point(573, 167)
        Me.lblEntrada.Name = "lblEntrada"
        Me.lblEntrada.Size = New System.Drawing.Size(168, 23)
        Me.lblEntrada.TabIndex = 99
        Me.lblEntrada.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblSalida
        '
        Me.lblSalida.BackColor = System.Drawing.Color.SkyBlue
        Me.lblSalida.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSalida.ForeColor = System.Drawing.Color.Black
        Me.lblSalida.Location = New System.Drawing.Point(572, 217)
        Me.lblSalida.Name = "lblSalida"
        Me.lblSalida.Size = New System.Drawing.Size(168, 23)
        Me.lblSalida.TabIndex = 100
        Me.lblSalida.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label10
        '
        Me.Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(573, 143)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(168, 22)
        Me.Label10.TabIndex = 101
        Me.Label10.Text = "Fecha Entrada"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.btnAbonar)
        Me.Panel3.Controls.Add(Me.btnSalir)
        Me.Panel3.Controls.Add(Me.btnGuardar)
        Me.Panel3.Controls.Add(Me.btnLimpiar)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel3.Location = New System.Drawing.Point(0, 451)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(747, 77)
        Me.Panel3.TabIndex = 102
        '
        'btnAbonar
        '
        Me.btnAbonar.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnAbonar.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnAbonar.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAbonar.Image = CType(resources.GetObject("btnAbonar.Image"), System.Drawing.Image)
        Me.btnAbonar.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnAbonar.Location = New System.Drawing.Point(497, 3)
        Me.btnAbonar.Name = "btnAbonar"
        Me.btnAbonar.Size = New System.Drawing.Size(75, 71)
        Me.btnAbonar.TabIndex = 22
        Me.btnAbonar.Text = "Abonar"
        Me.btnAbonar.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnAbonar.UseVisualStyleBackColor = True
        '
        'Panel4
        '
        Me.Panel4.Controls.Add(Me.Panel6)
        Me.Panel4.Controls.Add(Me.lblidcliented)
        Me.Panel4.Controls.Add(Me.GroupBox1)
        Me.Panel4.Controls.Add(Me.Label10)
        Me.Panel4.Controls.Add(Me.cboCLientes)
        Me.Panel4.Controls.Add(Me.lblSalida)
        Me.Panel4.Controls.Add(Me.Label4)
        Me.Panel4.Controls.Add(Me.lblEntrada)
        Me.Panel4.Controls.Add(Me.txttelefono)
        Me.Panel4.Controls.Add(Me.Label11)
        Me.Panel4.Controls.Add(Me.GroupBox2)
        Me.Panel4.Controls.Add(Me.lblINE)
        Me.Panel4.Controls.Add(Me.Label9)
        Me.Panel4.Controls.Add(Me.cboFolio)
        Me.Panel4.Controls.Add(Me.Label8)
        Me.Panel4.Controls.Add(Me.Label2)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel4.Location = New System.Drawing.Point(0, 40)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(747, 261)
        Me.Panel4.TabIndex = 103
        '
        'Panel6
        '
        Me.Panel6.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Panel6.BackColor = System.Drawing.Color.LightCoral
        Me.Panel6.Controls.Add(Me.Panel7)
        Me.Panel6.Location = New System.Drawing.Point(222, 145)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(282, 88)
        Me.Panel6.TabIndex = 218
        Me.Panel6.Visible = False
        '
        'Panel7
        '
        Me.Panel7.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel7.BackColor = System.Drawing.Color.White
        Me.Panel7.Controls.Add(Me.Label22)
        Me.Panel7.Location = New System.Drawing.Point(8, 9)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Size = New System.Drawing.Size(266, 71)
        Me.Panel7.TabIndex = 0
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.Font = New System.Drawing.Font("Segoe UI Black", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label22.Location = New System.Drawing.Point(17, 20)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(233, 30)
        Me.Label22.TabIndex = 0
        Me.Label22.Text = "IMPRIMIENDO PDF..."
        '
        'Panel5
        '
        Me.Panel5.Controls.Add(Me.Label18)
        Me.Panel5.Controls.Add(Me.Label19)
        Me.Panel5.Controls.Add(Me.Label20)
        Me.Panel5.Controls.Add(Me.Label21)
        Me.Panel5.Controls.Add(Me.txtOtro)
        Me.Panel5.Controls.Add(Me.txtTransfe)
        Me.Panel5.Controls.Add(Me.txtTarjeta)
        Me.Panel5.Controls.Add(Me.txtEfectivo)
        Me.Panel5.Controls.Add(Me.txtAnticipo)
        Me.Panel5.Controls.Add(Me.lblAnticipo)
        Me.Panel5.Controls.Add(Me.txtCambio)
        Me.Panel5.Controls.Add(Me.Label16)
        Me.Panel5.Controls.Add(Me.txtSubtotal)
        Me.Panel5.Controls.Add(Me.Label7)
        Me.Panel5.Controls.Add(Me.txtResta)
        Me.Panel5.Controls.Add(Me.Label14)
        Me.Panel5.Controls.Add(Me.txtDescuento)
        Me.Panel5.Controls.Add(Me.Label15)
        Me.Panel5.Controls.Add(Me.txtTotalVenta)
        Me.Panel5.Controls.Add(Me.Label17)
        Me.Panel5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel5.Location = New System.Drawing.Point(0, 301)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(747, 150)
        Me.Panel5.TabIndex = 104
        '
        'Label18
        '
        Me.Label18.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.Label18.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.ImageAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.Label18.Location = New System.Drawing.Point(519, 116)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(109, 29)
        Me.Label18.TabIndex = 50
        Me.Label18.Text = "Otra"
        Me.Label18.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label19
        '
        Me.Label19.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.ImageAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.Label19.Location = New System.Drawing.Point(519, 81)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(112, 29)
        Me.Label19.TabIndex = 49
        Me.Label19.Text = "Transferencia"
        Me.Label19.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label20
        '
        Me.Label20.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.ImageAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.Label20.Location = New System.Drawing.Point(519, 46)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(104, 29)
        Me.Label20.TabIndex = 48
        Me.Label20.Text = "Tarjeta"
        Me.Label20.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label21
        '
        Me.Label21.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label21.ImageAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.Label21.Location = New System.Drawing.Point(519, 11)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(109, 29)
        Me.Label21.TabIndex = 47
        Me.Label21.Text = "Efectivo"
        Me.Label21.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtOtro
        '
        Me.txtOtro.BackColor = System.Drawing.Color.White
        Me.txtOtro.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtOtro.ForeColor = System.Drawing.Color.Black
        Me.txtOtro.Location = New System.Drawing.Point(632, 116)
        Me.txtOtro.Name = "txtOtro"
        Me.txtOtro.Size = New System.Drawing.Size(108, 29)
        Me.txtOtro.TabIndex = 46
        Me.txtOtro.Text = "0.00"
        Me.txtOtro.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtTransfe
        '
        Me.txtTransfe.BackColor = System.Drawing.Color.White
        Me.txtTransfe.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTransfe.ForeColor = System.Drawing.Color.Black
        Me.txtTransfe.Location = New System.Drawing.Point(632, 81)
        Me.txtTransfe.Name = "txtTransfe"
        Me.txtTransfe.Size = New System.Drawing.Size(108, 29)
        Me.txtTransfe.TabIndex = 45
        Me.txtTransfe.Text = "0.00"
        Me.txtTransfe.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtTarjeta
        '
        Me.txtTarjeta.BackColor = System.Drawing.Color.White
        Me.txtTarjeta.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTarjeta.ForeColor = System.Drawing.Color.Black
        Me.txtTarjeta.Location = New System.Drawing.Point(632, 46)
        Me.txtTarjeta.Name = "txtTarjeta"
        Me.txtTarjeta.Size = New System.Drawing.Size(108, 29)
        Me.txtTarjeta.TabIndex = 44
        Me.txtTarjeta.Text = "0.00"
        Me.txtTarjeta.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtEfectivo
        '
        Me.txtEfectivo.BackColor = System.Drawing.Color.White
        Me.txtEfectivo.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtEfectivo.ForeColor = System.Drawing.Color.Black
        Me.txtEfectivo.Location = New System.Drawing.Point(632, 11)
        Me.txtEfectivo.Name = "txtEfectivo"
        Me.txtEfectivo.Size = New System.Drawing.Size(108, 29)
        Me.txtEfectivo.TabIndex = 43
        Me.txtEfectivo.Text = "0.00"
        Me.txtEfectivo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtAnticipo
        '
        Me.txtAnticipo.BackColor = System.Drawing.Color.SkyBlue
        Me.txtAnticipo.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAnticipo.ForeColor = System.Drawing.Color.Black
        Me.txtAnticipo.Location = New System.Drawing.Point(360, 73)
        Me.txtAnticipo.Name = "txtAnticipo"
        Me.txtAnticipo.ReadOnly = True
        Me.txtAnticipo.Size = New System.Drawing.Size(117, 29)
        Me.txtAnticipo.TabIndex = 42
        Me.txtAnticipo.Text = "0.00"
        Me.txtAnticipo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtAnticipo.Visible = False
        '
        'lblAnticipo
        '
        Me.lblAnticipo.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAnticipo.Location = New System.Drawing.Point(243, 73)
        Me.lblAnticipo.Name = "lblAnticipo"
        Me.lblAnticipo.Size = New System.Drawing.Size(80, 29)
        Me.lblAnticipo.TabIndex = 41
        Me.lblAnticipo.Text = "Anticipo:"
        Me.lblAnticipo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblAnticipo.Visible = False
        '
        'txtCambio
        '
        Me.txtCambio.BackColor = System.Drawing.Color.SkyBlue
        Me.txtCambio.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCambio.ForeColor = System.Drawing.Color.Black
        Me.txtCambio.Location = New System.Drawing.Point(116, 73)
        Me.txtCambio.Name = "txtCambio"
        Me.txtCambio.ReadOnly = True
        Me.txtCambio.Size = New System.Drawing.Size(117, 29)
        Me.txtCambio.TabIndex = 40
        Me.txtCambio.Text = "0.00"
        Me.txtCambio.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label16
        '
        Me.Label16.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.Location = New System.Drawing.Point(11, 73)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(75, 29)
        Me.Label16.TabIndex = 39
        Me.Label16.Text = "Cambio:"
        Me.Label16.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtSubtotal
        '
        Me.txtSubtotal.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSubtotal.Location = New System.Drawing.Point(360, 4)
        Me.txtSubtotal.Name = "txtSubtotal"
        Me.txtSubtotal.Size = New System.Drawing.Size(117, 29)
        Me.txtSubtotal.TabIndex = 38
        Me.txtSubtotal.Text = "0.00"
        Me.txtSubtotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label7
        '
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(243, 3)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(90, 29)
        Me.Label7.TabIndex = 37
        Me.Label7.Text = "Subtotal:"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtResta
        '
        Me.txtResta.BackColor = System.Drawing.Color.Red
        Me.txtResta.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtResta.ForeColor = System.Drawing.Color.White
        Me.txtResta.Location = New System.Drawing.Point(116, 39)
        Me.txtResta.Name = "txtResta"
        Me.txtResta.ReadOnly = True
        Me.txtResta.Size = New System.Drawing.Size(117, 29)
        Me.txtResta.TabIndex = 36
        Me.txtResta.Text = "0.00"
        Me.txtResta.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label14
        '
        Me.Label14.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.Location = New System.Drawing.Point(11, 38)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(94, 29)
        Me.Label14.TabIndex = 35
        Me.Label14.Text = "Resta:"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtDescuento
        '
        Me.txtDescuento.BackColor = System.Drawing.Color.DarkGreen
        Me.txtDescuento.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDescuento.ForeColor = System.Drawing.Color.White
        Me.txtDescuento.Location = New System.Drawing.Point(116, 4)
        Me.txtDescuento.Name = "txtDescuento"
        Me.txtDescuento.Size = New System.Drawing.Size(117, 29)
        Me.txtDescuento.TabIndex = 34
        Me.txtDescuento.Text = "0.00"
        Me.txtDescuento.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label15
        '
        Me.Label15.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.Location = New System.Drawing.Point(11, 3)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(120, 29)
        Me.Label15.TabIndex = 33
        Me.Label15.Text = "Descuento:"
        Me.Label15.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtTotalVenta
        '
        Me.txtTotalVenta.BackColor = System.Drawing.Color.SkyBlue
        Me.txtTotalVenta.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTotalVenta.ForeColor = System.Drawing.Color.Black
        Me.txtTotalVenta.Location = New System.Drawing.Point(360, 39)
        Me.txtTotalVenta.Name = "txtTotalVenta"
        Me.txtTotalVenta.ReadOnly = True
        Me.txtTotalVenta.Size = New System.Drawing.Size(117, 29)
        Me.txtTotalVenta.TabIndex = 32
        Me.txtTotalVenta.Text = "0.00"
        Me.txtTotalVenta.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label17
        '
        Me.Label17.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.Location = New System.Drawing.Point(243, 39)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(120, 28)
        Me.Label17.TabIndex = 31
        Me.Label17.Text = "Total a pagar:"
        Me.Label17.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'FrmDetReservacion
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(747, 528)
        Me.Controls.Add(Me.Panel5)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "FrmDetReservacion"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Detalle de Reservaciones"
        Me.Panel1.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.Panel3.ResumeLayout(False)
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        Me.Panel6.ResumeLayout(False)
        Me.Panel7.ResumeLayout(False)
        Me.Panel7.PerformLayout()
        Me.Panel5.ResumeLayout(False)
        Me.Panel5.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents Label1 As Label
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Label2 As Label
    Friend WithEvents cboCLientes As ComboBox
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents Label3 As Label
    Friend WithEvents lblCaracteristicas As Label
    Friend WithEvents lblHabitacion As Label
    Friend WithEvents lblTipo As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents txttelefono As TextBox
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents cboPrecio As ComboBox
    Friend WithEvents Label13 As Label
    Friend WithEvents txtHoras As TextBox
    Friend WithEvents Label12 As Label
    Friend WithEvents cboTipo As ComboBox
    Friend WithEvents Label6 As Label
    Friend WithEvents btnLimpiar As Button
    Friend WithEvents btnGuardar As Button
    Friend WithEvents btnSalir As Button
    Friend WithEvents lblidcliented As Label
    Friend WithEvents txtcontra As TextBox
    Friend WithEvents lblusuario As Label
    Friend WithEvents Label8 As Label
    Friend WithEvents cboFolio As ComboBox
    Friend WithEvents Label9 As Label
    Friend WithEvents lblINE As Label
    Friend WithEvents Label11 As Label
    Friend WithEvents lblEntrada As Label
    Friend WithEvents lblSalida As Label
    Friend WithEvents Label10 As Label
    Friend WithEvents Panel3 As Panel
    Friend WithEvents Panel4 As Panel
    Friend WithEvents Panel5 As Panel
    Friend WithEvents txtAnticipo As TextBox
    Friend WithEvents lblAnticipo As Label
    Friend WithEvents txtCambio As TextBox
    Friend WithEvents Label16 As Label
    Friend WithEvents txtSubtotal As TextBox
    Friend WithEvents Label7 As Label
    Friend WithEvents txtResta As TextBox
    Friend WithEvents Label14 As Label
    Friend WithEvents txtDescuento As TextBox
    Friend WithEvents Label15 As Label
    Friend WithEvents txtTotalVenta As TextBox
    Friend WithEvents Label17 As Label
    Friend WithEvents Label18 As Label
    Friend WithEvents Label19 As Label
    Friend WithEvents Label20 As Label
    Friend WithEvents Label21 As Label
    Friend WithEvents txtOtro As TextBox
    Friend WithEvents txtTransfe As TextBox
    Friend WithEvents txtTarjeta As TextBox
    Friend WithEvents txtEfectivo As TextBox
    Friend WithEvents btnAbonar As Button
    Friend WithEvents Panel6 As Panel
    Friend WithEvents Panel7 As Panel
    Friend WithEvents Label22 As Label
End Class
