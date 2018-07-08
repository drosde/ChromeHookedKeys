namespace ChromeRemoto
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.lstLog = new System.Windows.Forms.ListView();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_iniciar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lstLog
            // 
            this.lstLog.BackColor = System.Drawing.SystemColors.ControlLight;
            this.lstLog.Location = new System.Drawing.Point(12, 31);
            this.lstLog.Name = "lstLog";
            this.lstLog.Size = new System.Drawing.Size(403, 73);
            this.lstLog.TabIndex = 0;
            this.lstLog.UseCompatibleStateImageBehavior = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.SystemColors.Control;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("Comic Sans MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(128, 18);
            this.label1.TabIndex = 1;
            this.label1.Text = "Teclas Presionadas:";
            // 
            // btn_iniciar
            // 
            this.btn_iniciar.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.btn_iniciar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btn_iniciar.FlatAppearance.BorderSize = 0;
            this.btn_iniciar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_iniciar.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_iniciar.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btn_iniciar.Location = new System.Drawing.Point(0, 228);
            this.btn_iniciar.Name = "btn_iniciar";
            this.btn_iniciar.Size = new System.Drawing.Size(467, 48);
            this.btn_iniciar.TabIndex = 2;
            this.btn_iniciar.Text = "Iniciar";
            this.btn_iniciar.UseVisualStyleBackColor = false;
            this.btn_iniciar.Click += new System.EventHandler(this.btn_iniciar_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(467, 276);
            this.Controls.Add(this.btn_iniciar);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lstLog);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Form1";
            this.ShowIcon = false;
            this.Text = "Chrome Hooked";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView lstLog;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_iniciar;
    }
}

