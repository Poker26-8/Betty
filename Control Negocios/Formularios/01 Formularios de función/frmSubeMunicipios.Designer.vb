<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSubeMunicipios
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSubeMunicipios))
        Me.btnMunicipios = New System.Windows.Forms.Button()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.txtbarras = New System.Windows.Forms.TextBox()
        Me.btnColonias = New System.Windows.Forms.Button()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnMunicipios
        '
        Me.btnMunicipios.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnMunicipios.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnMunicipios.Image = CType(resources.GetObject("btnMunicipios.Image"), System.Drawing.Image)
        Me.btnMunicipios.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnMunicipios.Location = New System.Drawing.Point(12, 32)
        Me.btnMunicipios.Name = "btnMunicipios"
        Me.btnMunicipios.Size = New System.Drawing.Size(75, 79)
        Me.btnMunicipios.TabIndex = 0
        Me.btnMunicipios.Text = "Sube Municipios"
        Me.btnMunicipios.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnMunicipios.UseVisualStyleBackColor = True
        '
        'DataGridView1
        '
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Location = New System.Drawing.Point(12, 117)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.Size = New System.Drawing.Size(776, 321)
        Me.DataGridView1.TabIndex = 1
        '
        'txtbarras
        '
        Me.txtbarras.Location = New System.Drawing.Point(12, 6)
        Me.txtbarras.Name = "txtbarras"
        Me.txtbarras.Size = New System.Drawing.Size(100, 20)
        Me.txtbarras.TabIndex = 2
        '
        'btnColonias
        '
        Me.btnColonias.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnColonias.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnColonias.Image = CType(resources.GetObject("btnColonias.Image"), System.Drawing.Image)
        Me.btnColonias.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnColonias.Location = New System.Drawing.Point(93, 32)
        Me.btnColonias.Name = "btnColonias"
        Me.btnColonias.Size = New System.Drawing.Size(75, 79)
        Me.btnColonias.TabIndex = 3
        Me.btnColonias.Text = "Sube Colonias"
        Me.btnColonias.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnColonias.UseVisualStyleBackColor = True
        '
        'frmSubeMunicipios
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.btnColonias)
        Me.Controls.Add(Me.txtbarras)
        Me.Controls.Add(Me.DataGridView1)
        Me.Controls.Add(Me.btnMunicipios)
        Me.Name = "frmSubeMunicipios"
        Me.Text = "frmSubeMunicipios"
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents btnMunicipios As Button
    Friend WithEvents DataGridView1 As DataGridView
    Friend WithEvents txtbarras As TextBox
    Friend WithEvents btnColonias As Button
End Class
