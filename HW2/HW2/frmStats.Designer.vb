<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmStats
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.lstJobCodeStats = New System.Windows.Forms.ListBox()
        Me.lstCompanyStats = New System.Windows.Forms.ListBox()
        Me.btnCloseStats = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'lstJobCodeStats
        '
        Me.lstJobCodeStats.FormattingEnabled = True
        Me.lstJobCodeStats.ItemHeight = 20
        Me.lstJobCodeStats.Location = New System.Drawing.Point(39, 90)
        Me.lstJobCodeStats.Name = "lstJobCodeStats"
        Me.lstJobCodeStats.Size = New System.Drawing.Size(427, 444)
        Me.lstJobCodeStats.TabIndex = 0
        '
        'lstCompanyStats
        '
        Me.lstCompanyStats.FormattingEnabled = True
        Me.lstCompanyStats.ItemHeight = 20
        Me.lstCompanyStats.Location = New System.Drawing.Point(585, 90)
        Me.lstCompanyStats.Name = "lstCompanyStats"
        Me.lstCompanyStats.Size = New System.Drawing.Size(416, 444)
        Me.lstCompanyStats.TabIndex = 1
        '
        'btnCloseStats
        '
        Me.btnCloseStats.Location = New System.Drawing.Point(860, 562)
        Me.btnCloseStats.Name = "btnCloseStats"
        Me.btnCloseStats.Size = New System.Drawing.Size(154, 58)
        Me.btnCloseStats.TabIndex = 2
        Me.btnCloseStats.Text = "Close"
        Me.btnCloseStats.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 13.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(52, 40)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(427, 32)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "Stats by Job Code"
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 13.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(594, 40)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(427, 32)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "Company Wide Stats"
        '
        'frmStats
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1079, 630)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.btnCloseStats)
        Me.Controls.Add(Me.lstCompanyStats)
        Me.Controls.Add(Me.lstJobCodeStats)
        Me.Name = "frmStats"
        Me.Text = "frmStats"
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents lstJobCodeStats As ListBox
    Friend WithEvents lstCompanyStats As ListBox
    Friend WithEvents btnCloseStats As Button
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
End Class
