<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmComprasModelos
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmComprasModelos))
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.lblpAnticipo = New System.Windows.Forms.Label()
        Me.txtpAnticipo = New System.Windows.Forms.TextBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.cbopedido = New System.Windows.Forms.ComboBox()
        Me.Label31 = New System.Windows.Forms.Label()
        Me.dtpfecha = New System.Windows.Forms.DateTimePicker()
        Me.txtvalor = New System.Windows.Forms.TextBox()
        Me.cbomoneda = New System.Windows.Forms.ComboBox()
        Me.btncancela = New System.Windows.Forms.Button()
        Me.btnactualiza = New System.Windows.Forms.Button()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.cbofactura = New System.Windows.Forms.ComboBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.txtsaldo = New System.Windows.Forms.TextBox()
        Me.cboremision = New System.Windows.Forms.ComboBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.cboproveedor = New System.Windows.Forms.ComboBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.btnprod = New System.Windows.Forms.Button()
        Me.lblusuario = New System.Windows.Forms.Label()
        Me.txtusuario = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cbonombre = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtcodigo = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(170, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label1.Font = New System.Drawing.Font("Segoe UI Historic", 12.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.White
        Me.Label1.Location = New System.Drawing.Point(0, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Padding = New System.Windows.Forms.Padding(0, 3, 0, 0)
        Me.Label1.Size = New System.Drawing.Size(937, 31)
        Me.Label1.TabIndex = 5
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.btnprod)
        Me.Panel1.Controls.Add(Me.lblusuario)
        Me.Panel1.Controls.Add(Me.txtusuario)
        Me.Panel1.Controls.Add(Me.lblpAnticipo)
        Me.Panel1.Controls.Add(Me.txtpAnticipo)
        Me.Panel1.Controls.Add(Me.Button1)
        Me.Panel1.Controls.Add(Me.cbopedido)
        Me.Panel1.Controls.Add(Me.Label31)
        Me.Panel1.Controls.Add(Me.dtpfecha)
        Me.Panel1.Controls.Add(Me.txtvalor)
        Me.Panel1.Controls.Add(Me.cbomoneda)
        Me.Panel1.Controls.Add(Me.btncancela)
        Me.Panel1.Controls.Add(Me.btnactualiza)
        Me.Panel1.Controls.Add(Me.Label14)
        Me.Panel1.Controls.Add(Me.cbofactura)
        Me.Panel1.Controls.Add(Me.Label15)
        Me.Panel1.Controls.Add(Me.Label13)
        Me.Panel1.Controls.Add(Me.txtsaldo)
        Me.Panel1.Controls.Add(Me.cboremision)
        Me.Panel1.Controls.Add(Me.Label12)
        Me.Panel1.Controls.Add(Me.cboproveedor)
        Me.Panel1.Controls.Add(Me.Label11)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 31)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(937, 89)
        Me.Panel1.TabIndex = 6
        '
        'Panel2
        '
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel2.Location = New System.Drawing.Point(0, 508)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(937, 100)
        Me.Panel2.TabIndex = 7
        '
        'lblpAnticipo
        '
        Me.lblpAnticipo.AutoSize = True
        Me.lblpAnticipo.Enabled = False
        Me.lblpAnticipo.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblpAnticipo.Location = New System.Drawing.Point(478, 63)
        Me.lblpAnticipo.Name = "lblpAnticipo"
        Me.lblpAnticipo.Size = New System.Drawing.Size(58, 16)
        Me.lblpAnticipo.TabIndex = 178
        Me.lblpAnticipo.Text = "Anticipo:"
        '
        'txtpAnticipo
        '
        Me.txtpAnticipo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtpAnticipo.Enabled = False
        Me.txtpAnticipo.Location = New System.Drawing.Point(544, 60)
        Me.txtpAnticipo.Name = "txtpAnticipo"
        Me.txtpAnticipo.Size = New System.Drawing.Size(112, 20)
        Me.txtpAnticipo.TabIndex = 177
        Me.txtpAnticipo.Text = "0.00"
        Me.txtpAnticipo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Button1
        '
        Me.Button1.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Button1.Enabled = False
        Me.Button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button1.Font = New System.Drawing.Font("Segoe UI Semibold", 7.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button1.Location = New System.Drawing.Point(481, 31)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(175, 23)
        Me.Button1.TabIndex = 176
        Me.Button1.Text = "Aplicar anticipo"
        Me.Button1.UseVisualStyleBackColor = False
        '
        'cbopedido
        '
        Me.cbopedido.FormattingEnabled = True
        Me.cbopedido.Location = New System.Drawing.Point(345, 31)
        Me.cbopedido.Name = "cbopedido"
        Me.cbopedido.Size = New System.Drawing.Size(120, 21)
        Me.cbopedido.TabIndex = 175
        '
        'Label31
        '
        Me.Label31.AutoSize = True
        Me.Label31.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label31.Location = New System.Drawing.Point(271, 37)
        Me.Label31.Name = "Label31"
        Me.Label31.Size = New System.Drawing.Size(54, 16)
        Me.Label31.TabIndex = 174
        Me.Label31.Text = "Pedido:"
        '
        'dtpfecha
        '
        Me.dtpfecha.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpfecha.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpfecha.Location = New System.Drawing.Point(345, 60)
        Me.dtpfecha.Name = "dtpfecha"
        Me.dtpfecha.Size = New System.Drawing.Size(120, 23)
        Me.dtpfecha.TabIndex = 173
        '
        'txtvalor
        '
        Me.txtvalor.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtvalor.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtvalor.Location = New System.Drawing.Point(743, 59)
        Me.txtvalor.Name = "txtvalor"
        Me.txtvalor.Size = New System.Drawing.Size(75, 23)
        Me.txtvalor.TabIndex = 172
        Me.txtvalor.Text = "0.00"
        Me.txtvalor.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'cbomoneda
        '
        Me.cbomoneda.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbomoneda.FormattingEnabled = True
        Me.cbomoneda.Location = New System.Drawing.Point(662, 59)
        Me.cbomoneda.Name = "cbomoneda"
        Me.cbomoneda.Size = New System.Drawing.Size(75, 23)
        Me.cbomoneda.TabIndex = 171
        Me.cbomoneda.Text = "PESO"
        '
        'btncancela
        '
        Me.btncancela.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.btncancela.Enabled = False
        Me.btncancela.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btncancela.Font = New System.Drawing.Font("Segoe UI Semibold", 7.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btncancela.Location = New System.Drawing.Point(743, 7)
        Me.btncancela.Name = "btncancela"
        Me.btncancela.Size = New System.Drawing.Size(75, 49)
        Me.btncancela.TabIndex = 170
        Me.btncancela.Text = "Cancelar compra"
        Me.btncancela.UseVisualStyleBackColor = False
        '
        'btnactualiza
        '
        Me.btnactualiza.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.btnactualiza.Enabled = False
        Me.btnactualiza.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnactualiza.Font = New System.Drawing.Font("Segoe UI Semibold", 7.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnactualiza.Location = New System.Drawing.Point(662, 7)
        Me.btnactualiza.Name = "btnactualiza"
        Me.btnactualiza.Size = New System.Drawing.Size(75, 49)
        Me.btnactualiza.TabIndex = 169
        Me.btnactualiza.Text = "Actualizar factura"
        Me.btnactualiza.UseVisualStyleBackColor = False
        Me.btnactualiza.Visible = False
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.Location = New System.Drawing.Point(271, 63)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(48, 16)
        Me.Label14.TabIndex = 168
        Me.Label14.Text = "Fecha:"
        '
        'cbofactura
        '
        Me.cbofactura.FormattingEnabled = True
        Me.cbofactura.Location = New System.Drawing.Point(92, 32)
        Me.cbofactura.Name = "cbofactura"
        Me.cbofactura.Size = New System.Drawing.Size(120, 21)
        Me.cbofactura.TabIndex = 167
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.Location = New System.Drawing.Point(12, 37)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(55, 16)
        Me.Label15.TabIndex = 166
        Me.Label15.Text = "Factura:"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(12, 63)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(62, 16)
        Me.Label13.TabIndex = 165
        Me.Label13.Text = "A cuenta:"
        '
        'txtsaldo
        '
        Me.txtsaldo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtsaldo.Location = New System.Drawing.Point(92, 58)
        Me.txtsaldo.Name = "txtsaldo"
        Me.txtsaldo.Size = New System.Drawing.Size(120, 20)
        Me.txtsaldo.TabIndex = 164
        Me.txtsaldo.Text = "0.00"
        Me.txtsaldo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'cboremision
        '
        Me.cboremision.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.cboremision.FormattingEnabled = True
        Me.cboremision.Location = New System.Drawing.Point(544, 8)
        Me.cboremision.Name = "cboremision"
        Me.cboremision.Size = New System.Drawing.Size(112, 21)
        Me.cboremision.TabIndex = 163
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.ForeColor = System.Drawing.Color.Black
        Me.Label12.Location = New System.Drawing.Point(471, 11)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(67, 16)
        Me.Label12.TabIndex = 162
        Me.Label12.Text = "Remisión:"
        '
        'cboproveedor
        '
        Me.cboproveedor.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.cboproveedor.FormattingEnabled = True
        Me.cboproveedor.Location = New System.Drawing.Point(92, 8)
        Me.cboproveedor.Name = "cboproveedor"
        Me.cboproveedor.Size = New System.Drawing.Size(373, 21)
        Me.cboproveedor.TabIndex = 161
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(12, 11)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(74, 16)
        Me.Label11.TabIndex = 160
        Me.Label11.Text = "Proveedor:"
        '
        'btnprod
        '
        Me.btnprod.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnprod.BackColor = System.Drawing.Color.WhiteSmoke
        Me.btnprod.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnprod.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnprod.Location = New System.Drawing.Point(835, 55)
        Me.btnprod.Name = "btnprod"
        Me.btnprod.Size = New System.Drawing.Size(99, 27)
        Me.btnprod.TabIndex = 233
        Me.btnprod.Text = "Productos"
        Me.btnprod.UseVisualStyleBackColor = False
        '
        'lblusuario
        '
        Me.lblusuario.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblusuario.BackColor = System.Drawing.Color.Navy
        Me.lblusuario.ForeColor = System.Drawing.Color.White
        Me.lblusuario.Location = New System.Drawing.Point(835, 4)
        Me.lblusuario.Name = "lblusuario"
        Me.lblusuario.Size = New System.Drawing.Size(99, 23)
        Me.lblusuario.TabIndex = 232
        Me.lblusuario.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtusuario
        '
        Me.txtusuario.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtusuario.BackColor = System.Drawing.Color.White
        Me.txtusuario.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtusuario.Location = New System.Drawing.Point(835, 30)
        Me.txtusuario.Name = "txtusuario"
        Me.txtusuario.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtusuario.Size = New System.Drawing.Size(99, 23)
        Me.txtusuario.TabIndex = 231
        Me.txtusuario.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.SystemColors.HotTrack
        Me.Label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label2.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.White
        Me.Label2.Location = New System.Drawing.Point(4, 123)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(235, 21)
        Me.Label2.TabIndex = 76
        Me.Label2.Text = "Modelo"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'cbonombre
        '
        Me.cbonombre.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.cbonombre.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbonombre.FormattingEnabled = True
        Me.cbonombre.Location = New System.Drawing.Point(4, 143)
        Me.cbonombre.Name = "cbonombre"
        Me.cbonombre.Size = New System.Drawing.Size(235, 23)
        Me.cbonombre.TabIndex = 75
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.SystemColors.HotTrack
        Me.Label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label3.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.White
        Me.Label3.Location = New System.Drawing.Point(240, 123)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(99, 21)
        Me.Label3.TabIndex = 77
        Me.Label3.Text = "U.Compra"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtcodigo
        '
        Me.txtcodigo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtcodigo.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtcodigo.Location = New System.Drawing.Point(240, 143)
        Me.txtcodigo.Name = "txtcodigo"
        Me.txtcodigo.Size = New System.Drawing.Size(99, 23)
        Me.txtcodigo.TabIndex = 78
        Me.txtcodigo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.SystemColors.HotTrack
        Me.Label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label4.Font = New System.Drawing.Font("Segoe UI Semibold", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.White
        Me.Label4.Location = New System.Drawing.Point(345, 123)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(99, 21)
        Me.Label4.TabIndex = 79
        Me.Label4.Text = "Cantidad"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'frmComprasModelos
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(937, 608)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.txtcodigo)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.cbonombre)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Label1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmComprasModelos"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Compras por Modelos"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents Panel1 As Panel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents lblpAnticipo As Label
    Friend WithEvents txtpAnticipo As TextBox
    Friend WithEvents Button1 As Button
    Friend WithEvents cbopedido As ComboBox
    Friend WithEvents Label31 As Label
    Friend WithEvents dtpfecha As DateTimePicker
    Friend WithEvents txtvalor As TextBox
    Friend WithEvents cbomoneda As ComboBox
    Friend WithEvents btncancela As Button
    Friend WithEvents btnactualiza As Button
    Friend WithEvents Label14 As Label
    Friend WithEvents cbofactura As ComboBox
    Friend WithEvents Label15 As Label
    Friend WithEvents Label13 As Label
    Friend WithEvents txtsaldo As TextBox
    Friend WithEvents cboremision As ComboBox
    Friend WithEvents Label12 As Label
    Friend WithEvents cboproveedor As ComboBox
    Friend WithEvents Label11 As Label
    Friend WithEvents btnprod As Button
    Friend WithEvents lblusuario As Label
    Friend WithEvents txtusuario As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents cbonombre As ComboBox
    Friend WithEvents Label3 As Label
    Friend WithEvents txtcodigo As TextBox
    Friend WithEvents Label4 As Label
End Class
