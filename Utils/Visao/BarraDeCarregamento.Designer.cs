namespace Visao
{
    partial class BarraDeCarregamento
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lbl_valor = new System.Windows.Forms.Label();
            this.pgb_progresso = new System.Windows.Forms.ProgressBar();
            this.lbl_texto = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lbl_valor
            // 
            this.lbl_valor.AutoSize = true;
            this.lbl_valor.Location = new System.Drawing.Point(232, 36);
            this.lbl_valor.Name = "lbl_valor";
            this.lbl_valor.Size = new System.Drawing.Size(60, 13);
            this.lbl_valor.TabIndex = 3;
            this.lbl_valor.Text = "1000/1000";
            // 
            // pgb_progresso
            // 
            this.pgb_progresso.BackColor = System.Drawing.Color.White;
            this.pgb_progresso.Location = new System.Drawing.Point(10, 32);
            this.pgb_progresso.Name = "pgb_progresso";
            this.pgb_progresso.Size = new System.Drawing.Size(215, 23);
            this.pgb_progresso.TabIndex = 2;
            // 
            // lbl_texto
            // 
            this.lbl_texto.AutoSize = true;
            this.lbl_texto.Location = new System.Drawing.Point(103, 9);
            this.lbl_texto.Name = "lbl_texto";
            this.lbl_texto.Size = new System.Drawing.Size(30, 13);
            this.lbl_texto.TabIndex = 3;
            this.lbl_texto.Text = "texto";
            // 
            // BarraDeCarregamento
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(249)))), ((int)(((byte)(238)))));
            this.ClientSize = new System.Drawing.Size(302, 65);
            this.ControlBox = false;
            this.Controls.Add(this.lbl_texto);
            this.Controls.Add(this.lbl_valor);
            this.Controls.Add(this.pgb_progresso);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "BarraDeCarregamento";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Aguarde";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbl_valor;
        private System.Windows.Forms.ProgressBar pgb_progresso;
        private System.Windows.Forms.Label lbl_texto;
    }
}