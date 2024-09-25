<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmNuevo
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
        Me.pmesas = New System.Windows.Forms.Panel()
        Me.psuperior = New System.Windows.Forms.Panel()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.Colum = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column11 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column12 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.btnPdf = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtInicial = New System.Windows.Forms.TextBox()
        Me.txtnumfijo = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtbarras = New System.Windows.Forms.TextBox()
        Me.ComboBox1 = New System.Windows.Forms.ComboBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtfijo = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtpeso = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtalazar = New System.Windows.Forms.TextBox()
        Me.txtticket = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.DataGridView2 = New System.Windows.Forms.DataGridView()
        Me.txtconvertir = New System.Windows.Forms.TextBox()
        Me.Column112 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column1xc = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.pmesas.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DataGridView2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pmesas
        '
        Me.pmesas.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.pmesas.Controls.Add(Me.DataGridView2)
        Me.pmesas.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pmesas.Location = New System.Drawing.Point(0, 100)
        Me.pmesas.Name = "pmesas"
        Me.pmesas.Size = New System.Drawing.Size(447, 540)
        Me.pmesas.TabIndex = 0
        '
        'psuperior
        '
        Me.psuperior.Dock = System.Windows.Forms.DockStyle.Top
        Me.psuperior.Location = New System.Drawing.Point(0, 0)
        Me.psuperior.Name = "psuperior"
        Me.psuperior.Size = New System.Drawing.Size(930, 100)
        Me.psuperior.TabIndex = 0
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.txtconvertir)
        Me.Panel1.Controls.Add(Me.Button1)
        Me.Panel1.Controls.Add(Me.Label8)
        Me.Panel1.Controls.Add(Me.txtticket)
        Me.Panel1.Controls.Add(Me.txtalazar)
        Me.Panel1.Controls.Add(Me.Label7)
        Me.Panel1.Controls.Add(Me.txtpeso)
        Me.Panel1.Controls.Add(Me.Label6)
        Me.Panel1.Controls.Add(Me.txtfijo)
        Me.Panel1.Controls.Add(Me.Label5)
        Me.Panel1.Controls.Add(Me.Label4)
        Me.Panel1.Controls.Add(Me.ComboBox1)
        Me.Panel1.Controls.Add(Me.txtbarras)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.txtnumfijo)
        Me.Panel1.Controls.Add(Me.txtInicial)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.DataGridView1)
        Me.Panel1.Controls.Add(Me.btnPdf)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Right
        Me.Panel1.Location = New System.Drawing.Point(447, 100)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(483, 540)
        Me.Panel1.TabIndex = 1
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToDeleteRows = False
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Colum, Me.Column11, Me.Column12})
        Me.DataGridView1.Location = New System.Drawing.Point(40, 6)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.ReadOnly = True
        Me.DataGridView1.Size = New System.Drawing.Size(431, 150)
        Me.DataGridView1.TabIndex = 4
        '
        'Colum
        '
        Me.Colum.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.Colum.HeaderText = "Id"
        Me.Colum.Name = "Colum"
        Me.Colum.ReadOnly = True
        Me.Colum.Width = 41
        '
        'Column11
        '
        Me.Column11.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.Column11.HeaderText = "Codigo"
        Me.Column11.Name = "Column11"
        Me.Column11.ReadOnly = True
        Me.Column11.Width = 65
        '
        'Column12
        '
        Me.Column12.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.Column12.HeaderText = "CodUnico"
        Me.Column12.Name = "Column12"
        Me.Column12.ReadOnly = True
        '
        'btnPdf
        '
        Me.btnPdf.Location = New System.Drawing.Point(376, 162)
        Me.btnPdf.Name = "btnPdf"
        Me.btnPdf.Size = New System.Drawing.Size(95, 55)
        Me.btnPdf.TabIndex = 3
        Me.btnPdf.Text = "TXT"
        Me.btnPdf.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(37, 212)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(69, 16)
        Me.Label1.TabIndex = 5
        Me.Label1.Text = "NumInicial"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(37, 239)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(54, 16)
        Me.Label2.TabIndex = 6
        Me.Label2.Text = "numFijo"
        '
        'txtInicial
        '
        Me.txtInicial.Location = New System.Drawing.Point(112, 211)
        Me.txtInicial.Name = "txtInicial"
        Me.txtInicial.Size = New System.Drawing.Size(59, 20)
        Me.txtInicial.TabIndex = 7
        Me.txtInicial.Text = "55"
        '
        'txtnumfijo
        '
        Me.txtnumfijo.Location = New System.Drawing.Point(112, 239)
        Me.txtnumfijo.Name = "txtnumfijo"
        Me.txtnumfijo.Size = New System.Drawing.Size(59, 20)
        Me.txtnumfijo.TabIndex = 8
        Me.txtnumfijo.Text = "2000"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(37, 273)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(65, 16)
        Me.Label3.TabIndex = 9
        Me.Label3.Text = "CodBarra"
        '
        'txtbarras
        '
        Me.txtbarras.Location = New System.Drawing.Point(112, 273)
        Me.txtbarras.Name = "txtbarras"
        Me.txtbarras.Size = New System.Drawing.Size(169, 20)
        Me.txtbarras.TabIndex = 10
        '
        'ComboBox1
        '
        Me.ComboBox1.FormattingEnabled = True
        Me.ComboBox1.Location = New System.Drawing.Point(112, 162)
        Me.ComboBox1.Name = "ComboBox1"
        Me.ComboBox1.Size = New System.Drawing.Size(100, 21)
        Me.ComboBox1.TabIndex = 11
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(37, 163)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(51, 16)
        Me.Label4.TabIndex = 12
        Me.Label4.Text = "Código"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(37, 306)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(54, 16)
        Me.Label5.TabIndex = 13
        Me.Label5.Text = "numFijo"
        '
        'txtfijo
        '
        Me.txtfijo.Location = New System.Drawing.Point(112, 306)
        Me.txtfijo.Name = "txtfijo"
        Me.txtfijo.Size = New System.Drawing.Size(59, 20)
        Me.txtfijo.TabIndex = 14
        Me.txtfijo.Text = "0"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(37, 336)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(39, 16)
        Me.Label6.TabIndex = 15
        Me.Label6.Text = "Peso"
        '
        'txtpeso
        '
        Me.txtpeso.Location = New System.Drawing.Point(112, 336)
        Me.txtpeso.Name = "txtpeso"
        Me.txtpeso.Size = New System.Drawing.Size(59, 20)
        Me.txtpeso.TabIndex = 16
        Me.txtpeso.Text = "0"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(37, 369)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(72, 16)
        Me.Label7.TabIndex = 17
        Me.Label7.Text = "Numalazar"
        '
        'txtalazar
        '
        Me.txtalazar.Location = New System.Drawing.Point(112, 369)
        Me.txtalazar.Name = "txtalazar"
        Me.txtalazar.Size = New System.Drawing.Size(59, 20)
        Me.txtalazar.TabIndex = 18
        Me.txtalazar.Text = "0"
        '
        'txtticket
        '
        Me.txtticket.Location = New System.Drawing.Point(6, 413)
        Me.txtticket.Name = "txtticket"
        Me.txtticket.Size = New System.Drawing.Size(465, 20)
        Me.txtticket.TabIndex = 19
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(6, 394)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(61, 16)
        Me.Label8.TabIndex = 20
        Me.Label8.Text = "N° Ticket"
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(376, 355)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(95, 55)
        Me.Button1.TabIndex = 21
        Me.Button1.Text = "Generar"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'DataGridView2
        '
        Me.DataGridView2.AllowUserToAddRows = False
        Me.DataGridView2.AllowUserToDeleteRows = False
        Me.DataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView2.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Column112, Me.Column1xc})
        Me.DataGridView2.Location = New System.Drawing.Point(3, 355)
        Me.DataGridView2.Name = "DataGridView2"
        Me.DataGridView2.ReadOnly = True
        Me.DataGridView2.Size = New System.Drawing.Size(431, 150)
        Me.DataGridView2.TabIndex = 5
        '
        'txtconvertir
        '
        Me.txtconvertir.Location = New System.Drawing.Point(6, 439)
        Me.txtconvertir.Name = "txtconvertir"
        Me.txtconvertir.Size = New System.Drawing.Size(465, 20)
        Me.txtconvertir.TabIndex = 22
        Me.txtconvertir.Text = "55200010200295520001060030572000038005501"
        '
        'Column112
        '
        Me.Column112.HeaderText = "Codigo"
        Me.Column112.Name = "Column112"
        Me.Column112.ReadOnly = True
        '
        'Column1xc
        '
        Me.Column1xc.HeaderText = "Peso"
        Me.Column1xc.Name = "Column1xc"
        Me.Column1xc.ReadOnly = True
        '
        'frmNuevo
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(930, 640)
        Me.Controls.Add(Me.pmesas)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.psuperior)
        Me.Name = "frmNuevo"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "frmNuevo"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.pmesas.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DataGridView2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents pmesas As Panel
    Friend WithEvents psuperior As Panel
    Friend WithEvents Panel1 As Panel
    Friend WithEvents grdCaptura1 As DataGridView
    Friend WithEvents Column1 As DataGridViewTextBoxColumn
    Friend WithEvents Column2 As DataGridViewTextBoxColumn
    Friend WithEvents btnPdf As Button
    Friend WithEvents DataGridView1 As DataGridView
    Friend WithEvents Colum As DataGridViewTextBoxColumn
    Friend WithEvents Column11 As DataGridViewTextBoxColumn
    Friend WithEvents Column12 As DataGridViewTextBoxColumn
    Friend WithEvents Label7 As Label
    Friend WithEvents txtpeso As TextBox
    Friend WithEvents Label6 As Label
    Friend WithEvents txtfijo As TextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents ComboBox1 As ComboBox
    Friend WithEvents txtbarras As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents txtnumfijo As TextBox
    Friend WithEvents txtInicial As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents txtalazar As TextBox
    Friend WithEvents Label8 As Label
    Friend WithEvents txtticket As TextBox
    Friend WithEvents Button1 As Button
    Friend WithEvents DataGridView2 As DataGridView
    Friend WithEvents DataGridViewTextBoxColumn1 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn2 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn3 As DataGridViewTextBoxColumn
    Friend WithEvents Column112 As DataGridViewTextBoxColumn
    Friend WithEvents Column1xc As DataGridViewTextBoxColumn
    Friend WithEvents txtconvertir As TextBox
End Class
