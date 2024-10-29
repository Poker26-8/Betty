<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmCitasH
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmCitasH))
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.lblMes = New System.Windows.Forms.Label()
        Me.lblDía = New System.Windows.Forms.Label()
        Me.lblHora = New System.Windows.Forms.Label()
        Me.optDia = New System.Windows.Forms.RadioButton()
        Me.optHora = New System.Windows.Forms.RadioButton()
        Me.optMes = New System.Windows.Forms.RadioButton()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cboHabitacion = New System.Windows.Forms.ComboBox()
        Me.cboUsuario = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.grdCaptura = New System.Windows.Forms.DataGridView()
        Me.tHora = New System.Windows.Forms.Timer(Me.components)
        Me.tActuales = New System.Windows.Forms.Timer(Me.components)
        Me.tEstado = New System.Windows.Forms.Timer(Me.components)
        Me.Button1 = New System.Windows.Forms.Button()
        Me.btnDetalle = New System.Windows.Forms.Button()
        Me.btnConsultar = New System.Windows.Forms.Button()
        Me.btnModificar = New System.Windows.Forms.Button()
        Me.btnAgregar = New System.Windows.Forms.Button()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.a = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ss = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column1saasas = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.azxzx = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Panel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.Panel4.SuspendLayout()
        CType(Me.grdCaptura, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.Panel2)
        Me.Panel1.Controls.Add(Me.lblMes)
        Me.Panel1.Controls.Add(Me.lblDía)
        Me.Panel1.Controls.Add(Me.lblHora)
        Me.Panel1.Controls.Add(Me.optDia)
        Me.Panel1.Controls.Add(Me.optHora)
        Me.Panel1.Controls.Add(Me.optMes)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.cboHabitacion)
        Me.Panel1.Controls.Add(Me.cboUsuario)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(600, 143)
        Me.Panel1.TabIndex = 0
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.PictureBox1)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Right
        Me.Panel2.Location = New System.Drawing.Point(345, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(255, 143)
        Me.Panel2.TabIndex = 46
        '
        'lblMes
        '
        Me.lblMes.BackColor = System.Drawing.Color.SkyBlue
        Me.lblMes.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMes.ForeColor = System.Drawing.Color.Black
        Me.lblMes.Location = New System.Drawing.Point(112, 63)
        Me.lblMes.Name = "lblMes"
        Me.lblMes.Size = New System.Drawing.Size(227, 23)
        Me.lblMes.TabIndex = 45
        Me.lblMes.Text = "Label3"
        Me.lblMes.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblDía
        '
        Me.lblDía.BackColor = System.Drawing.Color.SkyBlue
        Me.lblDía.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDía.ForeColor = System.Drawing.Color.Black
        Me.lblDía.Location = New System.Drawing.Point(112, 89)
        Me.lblDía.Name = "lblDía"
        Me.lblDía.Size = New System.Drawing.Size(227, 23)
        Me.lblDía.TabIndex = 44
        Me.lblDía.Text = "Label2"
        Me.lblDía.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblHora
        '
        Me.lblHora.BackColor = System.Drawing.Color.SkyBlue
        Me.lblHora.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHora.ForeColor = System.Drawing.Color.Black
        Me.lblHora.Location = New System.Drawing.Point(112, 114)
        Me.lblHora.Name = "lblHora"
        Me.lblHora.Size = New System.Drawing.Size(227, 23)
        Me.lblHora.TabIndex = 43
        Me.lblHora.Text = "Label1"
        Me.lblHora.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'optDia
        '
        Me.optDia.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optDia.ForeColor = System.Drawing.Color.Black
        Me.optDia.Location = New System.Drawing.Point(12, 88)
        Me.optDia.Name = "optDia"
        Me.optDia.Size = New System.Drawing.Size(59, 23)
        Me.optDia.TabIndex = 40
        Me.optDia.TabStop = True
        Me.optDia.Text = "Hora"
        Me.optDia.UseVisualStyleBackColor = True
        '
        'optHora
        '
        Me.optHora.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optHora.ForeColor = System.Drawing.Color.Black
        Me.optHora.Location = New System.Drawing.Point(12, 113)
        Me.optHora.Name = "optHora"
        Me.optHora.Size = New System.Drawing.Size(71, 23)
        Me.optHora.TabIndex = 41
        Me.optHora.TabStop = True
        Me.optHora.Text = "Minuto"
        Me.optHora.UseVisualStyleBackColor = True
        '
        'optMes
        '
        Me.optMes.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.optMes.ForeColor = System.Drawing.Color.Black
        Me.optMes.Location = New System.Drawing.Point(12, 63)
        Me.optMes.Name = "optMes"
        Me.optMes.Size = New System.Drawing.Size(48, 23)
        Me.optMes.TabIndex = 42
        Me.optMes.TabStop = True
        Me.optMes.Text = "Día"
        Me.optMes.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(3, 11)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(103, 21)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Habitación:"
        '
        'cboHabitacion
        '
        Me.cboHabitacion.FormattingEnabled = True
        Me.cboHabitacion.Location = New System.Drawing.Point(112, 12)
        Me.cboHabitacion.Name = "cboHabitacion"
        Me.cboHabitacion.Size = New System.Drawing.Size(227, 21)
        Me.cboHabitacion.TabIndex = 2
        '
        'cboUsuario
        '
        Me.cboUsuario.FormattingEnabled = True
        Me.cboUsuario.Location = New System.Drawing.Point(112, 39)
        Me.cboUsuario.Name = "cboUsuario"
        Me.cboUsuario.Size = New System.Drawing.Size(227, 21)
        Me.cboUsuario.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(3, 39)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(100, 21)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Usuario:"
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.Button1)
        Me.Panel3.Controls.Add(Me.btnDetalle)
        Me.Panel3.Controls.Add(Me.btnConsultar)
        Me.Panel3.Controls.Add(Me.btnModificar)
        Me.Panel3.Controls.Add(Me.btnAgregar)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel3.Location = New System.Drawing.Point(0, 574)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(600, 67)
        Me.Panel3.TabIndex = 1
        '
        'Panel4
        '
        Me.Panel4.Controls.Add(Me.grdCaptura)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel4.Location = New System.Drawing.Point(0, 143)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(600, 431)
        Me.Panel4.TabIndex = 2
        '
        'grdCaptura
        '
        Me.grdCaptura.AllowUserToAddRows = False
        Me.grdCaptura.AllowUserToDeleteRows = False
        Me.grdCaptura.BackgroundColor = System.Drawing.Color.White
        Me.grdCaptura.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdCaptura.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.a, Me.ss, Me.Column1saasas, Me.azxzx})
        Me.grdCaptura.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdCaptura.Location = New System.Drawing.Point(0, 0)
        Me.grdCaptura.Name = "grdCaptura"
        Me.grdCaptura.ReadOnly = True
        Me.grdCaptura.RowHeadersVisible = False
        Me.grdCaptura.Size = New System.Drawing.Size(600, 431)
        Me.grdCaptura.TabIndex = 0
        '
        'tHora
        '
        Me.tHora.Interval = 10
        '
        'tActuales
        '
        Me.tActuales.Interval = 60000
        '
        'tEstado
        '
        Me.tEstado.Interval = 10000
        '
        'Button1
        '
        Me.Button1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button1.BackColor = System.Drawing.Color.DodgerBlue
        Me.Button1.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.Button1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button1.ForeColor = System.Drawing.Color.White
        Me.Button1.Image = Global.Control_Negocios.My.Resources.Resources.salir
        Me.Button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Button1.Location = New System.Drawing.Point(482, 6)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(113, 55)
        Me.Button1.TabIndex = 4
        Me.Button1.Text = "Salir"
        Me.Button1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Button1.UseVisualStyleBackColor = False
        '
        'btnDetalle
        '
        Me.btnDetalle.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnDetalle.BackColor = System.Drawing.Color.DodgerBlue
        Me.btnDetalle.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnDetalle.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDetalle.ForeColor = System.Drawing.Color.White
        Me.btnDetalle.Image = CType(resources.GetObject("btnDetalle.Image"), System.Drawing.Image)
        Me.btnDetalle.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnDetalle.Location = New System.Drawing.Point(244, 6)
        Me.btnDetalle.Name = "btnDetalle"
        Me.btnDetalle.Size = New System.Drawing.Size(113, 55)
        Me.btnDetalle.TabIndex = 3
        Me.btnDetalle.Text = "Detalle"
        Me.btnDetalle.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnDetalle.UseVisualStyleBackColor = False
        '
        'btnConsultar
        '
        Me.btnConsultar.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnConsultar.BackColor = System.Drawing.Color.DodgerBlue
        Me.btnConsultar.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnConsultar.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnConsultar.ForeColor = System.Drawing.Color.White
        Me.btnConsultar.Image = CType(resources.GetObject("btnConsultar.Image"), System.Drawing.Image)
        Me.btnConsultar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnConsultar.Location = New System.Drawing.Point(363, 6)
        Me.btnConsultar.Name = "btnConsultar"
        Me.btnConsultar.Size = New System.Drawing.Size(113, 55)
        Me.btnConsultar.TabIndex = 2
        Me.btnConsultar.Text = "Consultar"
        Me.btnConsultar.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnConsultar.UseVisualStyleBackColor = False
        '
        'btnModificar
        '
        Me.btnModificar.BackColor = System.Drawing.Color.DodgerBlue
        Me.btnModificar.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnModificar.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnModificar.ForeColor = System.Drawing.Color.White
        Me.btnModificar.Image = Global.Control_Negocios.My.Resources.Resources.timbrar
        Me.btnModificar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnModificar.Location = New System.Drawing.Point(125, 6)
        Me.btnModificar.Name = "btnModificar"
        Me.btnModificar.Size = New System.Drawing.Size(113, 55)
        Me.btnModificar.TabIndex = 1
        Me.btnModificar.Text = "Modificar"
        Me.btnModificar.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnModificar.UseVisualStyleBackColor = False
        '
        'btnAgregar
        '
        Me.btnAgregar.BackColor = System.Drawing.Color.DodgerBlue
        Me.btnAgregar.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnAgregar.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAgregar.ForeColor = System.Drawing.Color.White
        Me.btnAgregar.Image = CType(resources.GetObject("btnAgregar.Image"), System.Drawing.Image)
        Me.btnAgregar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnAgregar.Location = New System.Drawing.Point(6, 6)
        Me.btnAgregar.Name = "btnAgregar"
        Me.btnAgregar.Size = New System.Drawing.Size(113, 55)
        Me.btnAgregar.TabIndex = 0
        Me.btnAgregar.Text = "Agregar"
        Me.btnAgregar.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnAgregar.UseVisualStyleBackColor = False
        '
        'PictureBox1
        '
        Me.PictureBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(0, 0)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(255, 143)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox1.TabIndex = 8
        Me.PictureBox1.TabStop = False
        '
        'a
        '
        Me.a.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.a.HeaderText = "Id"
        Me.a.Name = "a"
        Me.a.ReadOnly = True
        Me.a.Visible = False
        Me.a.Width = 22
        '
        'ss
        '
        Me.ss.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.ss.HeaderText = "Hora"
        Me.ss.Name = "ss"
        Me.ss.ReadOnly = True
        Me.ss.Width = 55
        '
        'Column1saasas
        '
        Me.Column1saasas.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.Column1saasas.HeaderText = "Evento"
        Me.Column1saasas.Name = "Column1saasas"
        Me.Column1saasas.ReadOnly = True
        '
        'azxzx
        '
        Me.azxzx.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.azxzx.HeaderText = "Activo"
        Me.azxzx.Name = "azxzx"
        Me.azxzx.ReadOnly = True
        Me.azxzx.Visible = False
        Me.azxzx.Width = 62
        '
        'frmCitasH
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(600, 641)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmCitasH"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Agendar Citas"
        Me.Panel1.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        Me.Panel4.ResumeLayout(False)
        CType(Me.grdCaptura, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents Label2 As Label
    Friend WithEvents cboHabitacion As ComboBox
    Friend WithEvents cboUsuario As ComboBox
    Friend WithEvents Label1 As Label
    Friend WithEvents optDia As RadioButton
    Friend WithEvents optHora As RadioButton
    Friend WithEvents optMes As RadioButton
    Friend WithEvents Panel2 As Panel
    Friend WithEvents lblMes As Label
    Friend WithEvents lblDía As Label
    Friend WithEvents lblHora As Label
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents Panel3 As Panel
    Friend WithEvents btnDetalle As Button
    Friend WithEvents btnConsultar As Button
    Friend WithEvents btnModificar As Button
    Friend WithEvents btnAgregar As Button
    Friend WithEvents Panel4 As Panel
    Friend WithEvents grdCaptura As DataGridView
    Friend WithEvents tHora As Timer
    Friend WithEvents tActuales As Timer
    Friend WithEvents tEstado As Timer
    Friend WithEvents Button1 As Button
    Friend WithEvents Column3 As DataGridViewTextBoxColumn
    Friend WithEvents Column1 As DataGridViewTextBoxColumn
    Friend WithEvents Column2 As DataGridViewTextBoxColumn
    Friend WithEvents Column4 As DataGridViewTextBoxColumn
    Friend WithEvents a As DataGridViewTextBoxColumn
    Friend WithEvents ss As DataGridViewTextBoxColumn
    Friend WithEvents Column1saasas As DataGridViewTextBoxColumn
    Friend WithEvents azxzx As DataGridViewTextBoxColumn
End Class
