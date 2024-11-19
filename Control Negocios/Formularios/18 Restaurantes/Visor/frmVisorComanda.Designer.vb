<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmVisorComanda
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmVisorComanda))
        Me.PComandas = New System.Windows.Forms.Panel()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.SuspendLayout()
        '
        'PComandas
        '
        Me.PComandas.BackgroundImage = CType(resources.GetObject("PComandas.BackgroundImage"), System.Drawing.Image)
        Me.PComandas.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.PComandas.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PComandas.Location = New System.Drawing.Point(0, 0)
        Me.PComandas.Name = "PComandas"
        Me.PComandas.Size = New System.Drawing.Size(1142, 652)
        Me.PComandas.TabIndex = 0
        '
        'Timer1
        '
        Me.Timer1.Interval = 10000
        '
        'frmVisorComanda
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(1142, 652)
        Me.Controls.Add(Me.PComandas)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmVisorComanda"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Visor de Comandas"
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents PComandas As Panel
    Friend WithEvents Timer1 As Timer
End Class
