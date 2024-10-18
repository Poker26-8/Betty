<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAnticiposReservaciones
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmAnticiposReservaciones))
        Me.lblHabitacion = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.txtSubtotal = New System.Windows.Forms.TextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.txtResta = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.txtDescuento = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.txtTotalVenta = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.btnSalir = New System.Windows.Forms.Button()
        Me.btnAbonar = New System.Windows.Forms.Button()
        Me.txtOtro = New System.Windows.Forms.TextBox()
        Me.txtTransfe = New System.Windows.Forms.TextBox()
        Me.txtTarjeta = New System.Windows.Forms.TextBox()
        Me.txtEfectivo = New System.Windows.Forms.TextBox()
        Me.pReservacion = New System.Windows.Forms.Panel()
        Me.lbl = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.cboPrecio = New System.Windows.Forms.ComboBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.txtHoras = New System.Windows.Forms.TextBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.cboTipo = New System.Windows.Forms.ComboBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.txtContra = New System.Windows.Forms.TextBox()
        Me.lblUsuario = New System.Windows.Forms.Label()
        Me.lblEntrada = New System.Windows.Forms.Label()
        Me.lblSalida = New System.Windows.Forms.Label()
        Me.cboClIente = New System.Windows.Forms.ComboBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.cboFolio = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtCambio = New System.Windows.Forms.TextBox()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.btnLimpiar = New System.Windows.Forms.Button()
        Me.Panel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.pReservacion.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.Panel4.SuspendLayout()
        Me.SuspendLayout()
        '
        'lblHabitacion
        '
        Me.lblHabitacion.BackColor = System.Drawing.Color.DodgerBlue
        Me.lblHabitacion.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHabitacion.Location = New System.Drawing.Point(111, 48)
        Me.lblHabitacion.Name = "lblHabitacion"
        Me.lblHabitacion.Size = New System.Drawing.Size(254, 30)
        Me.lblHabitacion.TabIndex = 0
        Me.lblHabitacion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(4, 48)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(101, 30)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Habitación:"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(4, 88)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(101, 21)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Cliente:"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.txtCambio)
        Me.Panel1.Controls.Add(Me.Label16)
        Me.Panel1.Controls.Add(Me.Label6)
        Me.Panel1.Controls.Add(Me.Label5)
        Me.Panel1.Controls.Add(Me.Label4)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.txtSubtotal)
        Me.Panel1.Controls.Add(Me.Label11)
        Me.Panel1.Controls.Add(Me.txtResta)
        Me.Panel1.Controls.Add(Me.Label10)
        Me.Panel1.Controls.Add(Me.txtDescuento)
        Me.Panel1.Controls.Add(Me.Label9)
        Me.Panel1.Controls.Add(Me.txtTotalVenta)
        Me.Panel1.Controls.Add(Me.Label8)
        Me.Panel1.Controls.Add(Me.Panel2)
        Me.Panel1.Controls.Add(Me.txtOtro)
        Me.Panel1.Controls.Add(Me.txtTransfe)
        Me.Panel1.Controls.Add(Me.txtTarjeta)
        Me.Panel1.Controls.Add(Me.txtEfectivo)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 253)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(533, 254)
        Me.Panel1.TabIndex = 4
        '
        'txtSubtotal
        '
        Me.txtSubtotal.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSubtotal.Location = New System.Drawing.Point(328, 5)
        Me.txtSubtotal.Name = "txtSubtotal"
        Me.txtSubtotal.Size = New System.Drawing.Size(117, 29)
        Me.txtSubtotal.TabIndex = 18
        Me.txtSubtotal.Text = "0.00"
        Me.txtSubtotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label11
        '
        Me.Label11.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(238, 4)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(90, 29)
        Me.Label11.TabIndex = 17
        Me.Label11.Text = "Subtotal:"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtResta
        '
        Me.txtResta.BackColor = System.Drawing.Color.Red
        Me.txtResta.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtResta.ForeColor = System.Drawing.Color.White
        Me.txtResta.Location = New System.Drawing.Point(115, 40)
        Me.txtResta.Name = "txtResta"
        Me.txtResta.ReadOnly = True
        Me.txtResta.Size = New System.Drawing.Size(117, 29)
        Me.txtResta.TabIndex = 16
        Me.txtResta.Text = "0.00"
        Me.txtResta.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label10
        '
        Me.Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(10, 39)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(94, 29)
        Me.Label10.TabIndex = 15
        Me.Label10.Text = "Resta:"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtDescuento
        '
        Me.txtDescuento.BackColor = System.Drawing.Color.DarkGreen
        Me.txtDescuento.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDescuento.ForeColor = System.Drawing.Color.White
        Me.txtDescuento.Location = New System.Drawing.Point(115, 5)
        Me.txtDescuento.Name = "txtDescuento"
        Me.txtDescuento.Size = New System.Drawing.Size(117, 29)
        Me.txtDescuento.TabIndex = 14
        Me.txtDescuento.Text = "0.00"
        Me.txtDescuento.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label9
        '
        Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(10, 4)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(120, 29)
        Me.Label9.TabIndex = 13
        Me.Label9.Text = "Descuento:"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtTotalVenta
        '
        Me.txtTotalVenta.BackColor = System.Drawing.SystemColors.HotTrack
        Me.txtTotalVenta.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTotalVenta.ForeColor = System.Drawing.Color.White
        Me.txtTotalVenta.Location = New System.Drawing.Point(328, 40)
        Me.txtTotalVenta.Name = "txtTotalVenta"
        Me.txtTotalVenta.ReadOnly = True
        Me.txtTotalVenta.Size = New System.Drawing.Size(117, 29)
        Me.txtTotalVenta.TabIndex = 12
        Me.txtTotalVenta.Text = "0.00"
        Me.txtTotalVenta.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label8
        '
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(242, 40)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(80, 29)
        Me.Label8.TabIndex = 11
        Me.Label8.Text = "Total:"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.btnLimpiar)
        Me.Panel2.Controls.Add(Me.btnSalir)
        Me.Panel2.Controls.Add(Me.btnAbonar)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Right
        Me.Panel2.Location = New System.Drawing.Point(451, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(82, 254)
        Me.Panel2.TabIndex = 10
        '
        'btnSalir
        '
        Me.btnSalir.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnSalir.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSalir.Image = CType(resources.GetObject("btnSalir.Image"), System.Drawing.Image)
        Me.btnSalir.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnSalir.Location = New System.Drawing.Point(3, 145)
        Me.btnSalir.Name = "btnSalir"
        Me.btnSalir.Size = New System.Drawing.Size(75, 64)
        Me.btnSalir.TabIndex = 1
        Me.btnSalir.Text = "Salir"
        Me.btnSalir.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnSalir.UseVisualStyleBackColor = True
        '
        'btnAbonar
        '
        Me.btnAbonar.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnAbonar.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAbonar.Image = CType(resources.GetObject("btnAbonar.Image"), System.Drawing.Image)
        Me.btnAbonar.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnAbonar.Location = New System.Drawing.Point(3, 4)
        Me.btnAbonar.Name = "btnAbonar"
        Me.btnAbonar.Size = New System.Drawing.Size(75, 64)
        Me.btnAbonar.TabIndex = 0
        Me.btnAbonar.Text = "Abonar"
        Me.btnAbonar.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnAbonar.UseVisualStyleBackColor = True
        '
        'txtOtro
        '
        Me.txtOtro.BackColor = System.Drawing.Color.SteelBlue
        Me.txtOtro.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtOtro.ForeColor = System.Drawing.Color.White
        Me.txtOtro.Location = New System.Drawing.Point(344, 195)
        Me.txtOtro.Name = "txtOtro"
        Me.txtOtro.Size = New System.Drawing.Size(101, 29)
        Me.txtOtro.TabIndex = 9
        Me.txtOtro.Text = "0.00"
        Me.txtOtro.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtTransfe
        '
        Me.txtTransfe.BackColor = System.Drawing.Color.SteelBlue
        Me.txtTransfe.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTransfe.ForeColor = System.Drawing.Color.White
        Me.txtTransfe.Location = New System.Drawing.Point(225, 195)
        Me.txtTransfe.Name = "txtTransfe"
        Me.txtTransfe.Size = New System.Drawing.Size(113, 29)
        Me.txtTransfe.TabIndex = 8
        Me.txtTransfe.Text = "0.00"
        Me.txtTransfe.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtTarjeta
        '
        Me.txtTarjeta.BackColor = System.Drawing.Color.SteelBlue
        Me.txtTarjeta.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTarjeta.ForeColor = System.Drawing.Color.White
        Me.txtTarjeta.Location = New System.Drawing.Point(119, 195)
        Me.txtTarjeta.Name = "txtTarjeta"
        Me.txtTarjeta.Size = New System.Drawing.Size(100, 29)
        Me.txtTarjeta.TabIndex = 7
        Me.txtTarjeta.Text = "0.00"
        Me.txtTarjeta.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtEfectivo
        '
        Me.txtEfectivo.BackColor = System.Drawing.Color.SteelBlue
        Me.txtEfectivo.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtEfectivo.ForeColor = System.Drawing.Color.White
        Me.txtEfectivo.Location = New System.Drawing.Point(5, 195)
        Me.txtEfectivo.Name = "txtEfectivo"
        Me.txtEfectivo.Size = New System.Drawing.Size(108, 29)
        Me.txtEfectivo.TabIndex = 6
        Me.txtEfectivo.Text = "0.00"
        Me.txtEfectivo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'pReservacion
        '
        Me.pReservacion.Controls.Add(Me.lblSalida)
        Me.pReservacion.Controls.Add(Me.lblEntrada)
        Me.pReservacion.Controls.Add(Me.lbl)
        Me.pReservacion.Controls.Add(Me.Label7)
        Me.pReservacion.Location = New System.Drawing.Point(364, 7)
        Me.pReservacion.Name = "pReservacion"
        Me.pReservacion.Size = New System.Drawing.Size(154, 123)
        Me.pReservacion.TabIndex = 19
        '
        'lbl
        '
        Me.lbl.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl.Location = New System.Drawing.Point(3, 1)
        Me.lbl.Name = "lbl"
        Me.lbl.Size = New System.Drawing.Size(148, 22)
        Me.lbl.TabIndex = 6
        Me.lbl.Text = "Fecha de Entrada"
        Me.lbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label7
        '
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(3, 60)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(148, 22)
        Me.Label7.TabIndex = 2
        Me.Label7.Text = "Fecha de Salida"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.cboPrecio)
        Me.GroupBox2.Controls.Add(Me.Label13)
        Me.GroupBox2.Controls.Add(Me.txtHoras)
        Me.GroupBox2.Controls.Add(Me.Label12)
        Me.GroupBox2.Controls.Add(Me.cboTipo)
        Me.GroupBox2.Controls.Add(Me.Label14)
        Me.GroupBox2.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox2.Location = New System.Drawing.Point(5, 8)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(316, 122)
        Me.GroupBox2.TabIndex = 20
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Precios"
        '
        'cboPrecio
        '
        Me.cboPrecio.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.cboPrecio.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboPrecio.FormattingEnabled = True
        Me.cboPrecio.Location = New System.Drawing.Point(7, 89)
        Me.cboPrecio.Name = "cboPrecio"
        Me.cboPrecio.Size = New System.Drawing.Size(150, 24)
        Me.cboPrecio.TabIndex = 5
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(7, 68)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(51, 18)
        Me.Label13.TabIndex = 4
        Me.Label13.Text = "Precio"
        '
        'txtHoras
        '
        Me.txtHoras.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.txtHoras.Location = New System.Drawing.Point(160, 41)
        Me.txtHoras.Name = "txtHoras"
        Me.txtHoras.Size = New System.Drawing.Size(150, 24)
        Me.txtHoras.TabIndex = 3
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(160, 20)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(49, 18)
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
        Me.cboTipo.Size = New System.Drawing.Size(151, 24)
        Me.cboTipo.TabIndex = 1
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(3, 20)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(37, 18)
        Me.Label14.TabIndex = 0
        Me.Label14.Text = "Tipo"
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.GroupBox2)
        Me.Panel3.Controls.Add(Me.pReservacion)
        Me.Panel3.Location = New System.Drawing.Point(5, 118)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(521, 134)
        Me.Panel3.TabIndex = 21
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.FromArgb(CType(CType(44, Byte), Integer), CType(CType(44, Byte), Integer), CType(CType(124, Byte), Integer))
        Me.Panel4.Controls.Add(Me.txtContra)
        Me.Panel4.Controls.Add(Me.lblUsuario)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel4.Location = New System.Drawing.Point(0, 0)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(533, 45)
        Me.Panel4.TabIndex = 22
        '
        'txtContra
        '
        Me.txtContra.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtContra.Location = New System.Drawing.Point(403, 10)
        Me.txtContra.Name = "txtContra"
        Me.txtContra.Size = New System.Drawing.Size(123, 26)
        Me.txtContra.TabIndex = 24
        Me.txtContra.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'lblUsuario
        '
        Me.lblUsuario.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblUsuario.ForeColor = System.Drawing.Color.White
        Me.lblUsuario.Location = New System.Drawing.Point(296, 8)
        Me.lblUsuario.Name = "lblUsuario"
        Me.lblUsuario.Size = New System.Drawing.Size(108, 30)
        Me.lblUsuario.TabIndex = 23
        Me.lblUsuario.Text = "Contraseña"
        Me.lblUsuario.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblEntrada
        '
        Me.lblEntrada.BackColor = System.Drawing.Color.LightSkyBlue
        Me.lblEntrada.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEntrada.Location = New System.Drawing.Point(3, 30)
        Me.lblEntrada.Name = "lblEntrada"
        Me.lblEntrada.Size = New System.Drawing.Size(148, 23)
        Me.lblEntrada.TabIndex = 7
        Me.lblEntrada.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblSalida
        '
        Me.lblSalida.BackColor = System.Drawing.Color.LightSkyBlue
        Me.lblSalida.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSalida.Location = New System.Drawing.Point(3, 89)
        Me.lblSalida.Name = "lblSalida"
        Me.lblSalida.Size = New System.Drawing.Size(148, 23)
        Me.lblSalida.TabIndex = 8
        Me.lblSalida.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'cboClIente
        '
        Me.cboClIente.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboClIente.FormattingEnabled = True
        Me.cboClIente.Location = New System.Drawing.Point(111, 88)
        Me.cboClIente.Name = "cboClIente"
        Me.cboClIente.Size = New System.Drawing.Size(418, 24)
        Me.cboClIente.TabIndex = 23
        '
        'Label15
        '
        Me.Label15.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.Location = New System.Drawing.Point(368, 48)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(58, 30)
        Me.Label15.TabIndex = 24
        Me.Label15.Text = "Folio:"
        Me.Label15.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cboFolio
        '
        Me.cboFolio.FormattingEnabled = True
        Me.cboFolio.Location = New System.Drawing.Point(427, 55)
        Me.cboFolio.Name = "cboFolio"
        Me.cboFolio.Size = New System.Drawing.Size(102, 21)
        Me.cboFolio.TabIndex = 25
        '
        'Label3
        '
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Image = CType(resources.GetObject("Label3.Image"), System.Drawing.Image)
        Me.Label3.ImageAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.Label3.Location = New System.Drawing.Point(6, 117)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(107, 72)
        Me.Label3.TabIndex = 23
        Me.Label3.Text = "Efectivo"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'Label4
        '
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Image = CType(resources.GetObject("Label4.Image"), System.Drawing.Image)
        Me.Label4.ImageAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.Label4.Location = New System.Drawing.Point(119, 117)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(100, 72)
        Me.Label4.TabIndex = 24
        Me.Label4.Text = "Tarjeta"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'Label5
        '
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Image = CType(resources.GetObject("Label5.Image"), System.Drawing.Image)
        Me.Label5.ImageAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.Label5.Location = New System.Drawing.Point(226, 117)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(112, 72)
        Me.Label5.TabIndex = 25
        Me.Label5.Text = "Transferencia"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'Label6
        '
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ImageAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.Label6.Location = New System.Drawing.Point(344, 117)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(101, 72)
        Me.Label6.TabIndex = 26
        Me.Label6.Text = "Otra"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'txtCambio
        '
        Me.txtCambio.BackColor = System.Drawing.Color.RoyalBlue
        Me.txtCambio.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCambio.ForeColor = System.Drawing.Color.White
        Me.txtCambio.Location = New System.Drawing.Point(115, 74)
        Me.txtCambio.Name = "txtCambio"
        Me.txtCambio.ReadOnly = True
        Me.txtCambio.Size = New System.Drawing.Size(117, 29)
        Me.txtCambio.TabIndex = 28
        Me.txtCambio.Text = "0.00"
        Me.txtCambio.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label16
        '
        Me.Label16.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.Location = New System.Drawing.Point(12, 74)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(75, 29)
        Me.Label16.TabIndex = 27
        Me.Label16.Text = "Cambio:"
        Me.Label16.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btnLimpiar
        '
        Me.btnLimpiar.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnLimpiar.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnLimpiar.Image = CType(resources.GetObject("btnLimpiar.Image"), System.Drawing.Image)
        Me.btnLimpiar.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnLimpiar.Location = New System.Drawing.Point(4, 75)
        Me.btnLimpiar.Name = "btnLimpiar"
        Me.btnLimpiar.Size = New System.Drawing.Size(75, 64)
        Me.btnLimpiar.TabIndex = 2
        Me.btnLimpiar.Text = "Limpiar"
        Me.btnLimpiar.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnLimpiar.UseVisualStyleBackColor = True
        '
        'frmAnticiposReservaciones
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(533, 507)
        Me.Controls.Add(Me.cboFolio)
        Me.Controls.Add(Me.Label15)
        Me.Controls.Add(Me.cboClIente)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.lblHabitacion)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmAnticiposReservaciones"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Anticipos a Reservaciones"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.pReservacion.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.Panel3.ResumeLayout(False)
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents lblHabitacion As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Panel1 As Panel
    Friend WithEvents txtEfectivo As TextBox
    Friend WithEvents txtOtro As TextBox
    Friend WithEvents txtTransfe As TextBox
    Friend WithEvents txtTarjeta As TextBox
    Friend WithEvents Panel2 As Panel
    Friend WithEvents btnAbonar As Button
    Friend WithEvents pReservacion As Panel
    Friend WithEvents lbl As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents txtTotalVenta As TextBox
    Friend WithEvents Label8 As Label
    Friend WithEvents txtDescuento As TextBox
    Friend WithEvents Label9 As Label
    Friend WithEvents txtResta As TextBox
    Friend WithEvents Label10 As Label
    Friend WithEvents txtSubtotal As TextBox
    Friend WithEvents Label11 As Label
    Friend WithEvents btnSalir As Button
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents cboPrecio As ComboBox
    Friend WithEvents Label13 As Label
    Friend WithEvents txtHoras As TextBox
    Friend WithEvents Label12 As Label
    Friend WithEvents Panel3 As Panel
    Friend WithEvents Panel4 As Panel
    Friend WithEvents txtContra As TextBox
    Friend WithEvents lblUsuario As Label
    Friend WithEvents cboTipo As ComboBox
    Friend WithEvents Label14 As Label
    Friend WithEvents lblSalida As Label
    Friend WithEvents lblEntrada As Label
    Friend WithEvents cboClIente As ComboBox
    Friend WithEvents Label15 As Label
    Friend WithEvents cboFolio As ComboBox
    Friend WithEvents Label5 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents txtCambio As TextBox
    Friend WithEvents Label16 As Label
    Friend WithEvents btnLimpiar As Button
End Class
