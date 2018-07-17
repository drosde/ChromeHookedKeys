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
            this.chb_adblock = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_url = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txt_script = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lstLog
            // 
            this.lstLog.BackColor = System.Drawing.SystemColors.ControlLight;
            this.lstLog.Location = new System.Drawing.Point(12, 31);
            this.lstLog.Name = "lstLog";
            this.lstLog.Size = new System.Drawing.Size(494, 73);
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
            this.btn_iniciar.Enabled = false;
            this.btn_iniciar.FlatAppearance.BorderSize = 0;
            this.btn_iniciar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_iniciar.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_iniciar.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btn_iniciar.Location = new System.Drawing.Point(0, 435);
            this.btn_iniciar.Name = "btn_iniciar";
            this.btn_iniciar.Size = new System.Drawing.Size(556, 48);
            this.btn_iniciar.TabIndex = 2;
            this.btn_iniciar.Text = "Iniciar";
            this.btn_iniciar.UseVisualStyleBackColor = false;
            this.btn_iniciar.Click += new System.EventHandler(this.btn_iniciar_Click);
            // 
            // chb_adblock
            // 
            this.chb_adblock.AutoSize = true;
            this.chb_adblock.Checked = true;
            this.chb_adblock.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chb_adblock.Location = new System.Drawing.Point(12, 379);
            this.chb_adblock.Name = "chb_adblock";
            this.chb_adblock.Size = new System.Drawing.Size(100, 17);
            this.chb_adblock.TabIndex = 3;
            this.chb_adblock.Text = "Activar adblock";
            this.chb_adblock.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 116);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(20, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Url";
            // 
            // txt_url
            // 
            this.txt_url.Location = new System.Drawing.Point(15, 133);
            this.txt_url.Name = "txt_url";
            this.txt_url.Size = new System.Drawing.Size(261, 20);
            this.txt_url.TabIndex = 5;
            this.txt_url.Text = "https://open.spotify.com";
            this.txt_url.Leave += new System.EventHandler(this.txt_url_Leave);
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Location = new System.Drawing.Point(4, 19);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(442, 176);
            this.panel1.TabIndex = 9;
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(452, 19);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(38, 23);
            this.button1.TabIndex = 10;
            this.button1.Text = "+";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.panel1);
            this.groupBox1.Enabled = false;
            this.groupBox1.Location = new System.Drawing.Point(15, 174);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(496, 199);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Cookies";
            // 
            // txt_script
            // 
            this.txt_script.Location = new System.Drawing.Point(15, 409);
            this.txt_script.Name = "txt_script";
            this.txt_script.Size = new System.Drawing.Size(496, 20);
            this.txt_script.TabIndex = 13;
            this.txt_script.Text = "Ejecutar este script despues de iniciada la pagina";
            this.txt_script.Enter += new System.EventHandler(this.txt_script_Enter);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.ClientSize = new System.Drawing.Size(556, 483);
            this.Controls.Add(this.txt_script);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.txt_url);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.chb_adblock);
            this.Controls.Add(this.btn_iniciar);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lstLog);
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.ShowIcon = false;
            this.Text = "Chrome Hooked";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView lstLog;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_iniciar;
        private System.Windows.Forms.CheckBox chb_adblock;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_url;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txt_script;
    }
}

