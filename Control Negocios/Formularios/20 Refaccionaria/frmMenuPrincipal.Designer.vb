﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMenuPrincipal
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMenuPrincipal))
        Me.Button3 = New System.Windows.Forms.Button()
        Me.btnconsultar = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Button6 = New System.Windows.Forms.Button()
        Me.Button7 = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'Button3
        '
        Me.Button3.BackColor = System.Drawing.Color.White
        Me.Button3.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.Button3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button3.Image = CType(resources.GetObject("Button3.Image"), System.Drawing.Image)
        Me.Button3.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.Button3.Location = New System.Drawing.Point(336, 8)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(75, 75)
        Me.Button3.TabIndex = 25
        Me.Button3.Text = "Salir"
        Me.Button3.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.Button3.UseVisualStyleBackColor = False
        '
        'btnconsultar
        '
        Me.btnconsultar.BackColor = System.Drawing.Color.White
        Me.btnconsultar.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnconsultar.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnconsultar.Image = CType(resources.GetObject("btnconsultar.Image"), System.Drawing.Image)
        Me.btnconsultar.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnconsultar.Location = New System.Drawing.Point(255, 8)
        Me.btnconsultar.Name = "btnconsultar"
        Me.btnconsultar.Size = New System.Drawing.Size(75, 75)
        Me.btnconsultar.TabIndex = 24
        Me.btnconsultar.Text = "Consultar"
        Me.btnconsultar.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnconsultar.UseVisualStyleBackColor = False
        '
        'Button2
        '
        Me.Button2.BackColor = System.Drawing.Color.White
        Me.Button2.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.Button2.Image = CType(resources.GetObject("Button2.Image"), System.Drawing.Image)
        Me.Button2.Location = New System.Drawing.Point(174, 8)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(75, 75)
        Me.Button2.TabIndex = 23
        Me.Button2.Text = "Productos"
        Me.Button2.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.Button2.UseVisualStyleBackColor = False
        '
        'Button6
        '
        Me.Button6.BackColor = System.Drawing.Color.White
        Me.Button6.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.Button6.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button6.Image = CType(resources.GetObject("Button6.Image"), System.Drawing.Image)
        Me.Button6.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.Button6.Location = New System.Drawing.Point(12, 8)
        Me.Button6.Name = "Button6"
        Me.Button6.Size = New System.Drawing.Size(75, 75)
        Me.Button6.TabIndex = 28
        Me.Button6.Text = "Marcas"
        Me.Button6.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.Button6.UseVisualStyleBackColor = False
        '
        'Button7
        '
        Me.Button7.BackColor = System.Drawing.Color.White
        Me.Button7.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.Button7.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button7.Image = CType(resources.GetObject("Button7.Image"), System.Drawing.Image)
        Me.Button7.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.Button7.Location = New System.Drawing.Point(93, 8)
        Me.Button7.Name = "Button7"
        Me.Button7.Size = New System.Drawing.Size(75, 75)
        Me.Button7.TabIndex = 29
        Me.Button7.Text = "Modelos"
        Me.Button7.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.Button7.UseVisualStyleBackColor = False
        '
        'frmMenuPrincipal
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(418, 89)
        Me.Controls.Add(Me.Button7)
        Me.Controls.Add(Me.Button6)
        Me.Controls.Add(Me.Button3)
        Me.Controls.Add(Me.btnconsultar)
        Me.Controls.Add(Me.Button2)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmMenuPrincipal"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Delsscom® Control Refaccionaria"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Button3 As Button
    Friend WithEvents btnconsultar As Button
    Friend WithEvents Button2 As Button
    Friend WithEvents Button6 As Button
    Friend WithEvents Button7 As Button
End Class
