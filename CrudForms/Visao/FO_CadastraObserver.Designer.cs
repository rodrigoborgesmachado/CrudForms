
namespace Visao
{
    partial class FO_CadastraObserver
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
            this.pan_botoes = new System.Windows.Forms.Panel();
            this.btn_executar = new System.Windows.Forms.Button();
            this.pan_completo = new System.Windows.Forms.Panel();
            this.grb_formulario = new System.Windows.Forms.GroupBox();
            this.pan_emails = new System.Windows.Forms.Panel();
            this.tbx_emails = new System.Windows.Forms.TextBox();
            this.lbl_emails = new System.Windows.Forms.Label();
            this.pan_consulta = new System.Windows.Forms.Panel();
            this.tbx_consulta = new System.Windows.Forms.TextBox();
            this.lbl_consulta = new System.Windows.Forms.Label();
            this.pan_descricao = new System.Windows.Forms.Panel();
            this.tbx_descricao = new System.Windows.Forms.TextBox();
            this.lbl_descricao = new System.Windows.Forms.Label();
            this.pan_intevalo = new System.Windows.Forms.Panel();
            this.tbx_intervalo = new System.Windows.Forms.TextBox();
            this.lbl_intervalo = new System.Windows.Forms.Label();
            this.pan_botoes.SuspendLayout();
            this.pan_completo.SuspendLayout();
            this.grb_formulario.SuspendLayout();
            this.pan_emails.SuspendLayout();
            this.pan_consulta.SuspendLayout();
            this.pan_descricao.SuspendLayout();
            this.pan_intevalo.SuspendLayout();
            this.SuspendLayout();
            // 
            // pan_botoes
            // 
            this.pan_botoes.Controls.Add(this.btn_executar);
            this.pan_botoes.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pan_botoes.Location = new System.Drawing.Point(0, 445);
            this.pan_botoes.Name = "pan_botoes";
            this.pan_botoes.Size = new System.Drawing.Size(900, 43);
            this.pan_botoes.TabIndex = 0;
            // 
            // btn_executar
            // 
            this.btn_executar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_executar.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_executar.Location = new System.Drawing.Point(814, 8);
            this.btn_executar.Name = "btn_executar";
            this.btn_executar.Size = new System.Drawing.Size(83, 29);
            this.btn_executar.TabIndex = 24;
            this.btn_executar.Text = "Incluir";
            this.btn_executar.UseVisualStyleBackColor = true;
            this.btn_executar.Click += new System.EventHandler(this.btn_executar_Click);
            // 
            // pan_completo
            // 
            this.pan_completo.Controls.Add(this.grb_formulario);
            this.pan_completo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pan_completo.Location = new System.Drawing.Point(0, 0);
            this.pan_completo.Name = "pan_completo";
            this.pan_completo.Size = new System.Drawing.Size(900, 445);
            this.pan_completo.TabIndex = 1;
            // 
            // grb_formulario
            // 
            this.grb_formulario.Controls.Add(this.pan_intevalo);
            this.grb_formulario.Controls.Add(this.pan_emails);
            this.grb_formulario.Controls.Add(this.pan_consulta);
            this.grb_formulario.Controls.Add(this.pan_descricao);
            this.grb_formulario.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grb_formulario.Location = new System.Drawing.Point(0, 0);
            this.grb_formulario.Name = "grb_formulario";
            this.grb_formulario.Size = new System.Drawing.Size(900, 445);
            this.grb_formulario.TabIndex = 0;
            this.grb_formulario.TabStop = false;
            this.grb_formulario.Text = "Formulário";
            // 
            // pan_emails
            // 
            this.pan_emails.Controls.Add(this.tbx_emails);
            this.pan_emails.Controls.Add(this.lbl_emails);
            this.pan_emails.Dock = System.Windows.Forms.DockStyle.Top;
            this.pan_emails.Location = new System.Drawing.Point(3, 312);
            this.pan_emails.Name = "pan_emails";
            this.pan_emails.Size = new System.Drawing.Size(894, 46);
            this.pan_emails.TabIndex = 2;
            // 
            // tbx_emails
            // 
            this.tbx_emails.Location = new System.Drawing.Point(184, 9);
            this.tbx_emails.MaxLength = 400;
            this.tbx_emails.Name = "tbx_emails";
            this.tbx_emails.Size = new System.Drawing.Size(701, 27);
            this.tbx_emails.TabIndex = 1;
            // 
            // lbl_emails
            // 
            this.lbl_emails.AutoSize = true;
            this.lbl_emails.Location = new System.Drawing.Point(9, 12);
            this.lbl_emails.Name = "lbl_emails";
            this.lbl_emails.Size = new System.Drawing.Size(169, 19);
            this.lbl_emails.TabIndex = 0;
            this.lbl_emails.Text = "Emails para notificação";
            // 
            // pan_consulta
            // 
            this.pan_consulta.Controls.Add(this.tbx_consulta);
            this.pan_consulta.Controls.Add(this.lbl_consulta);
            this.pan_consulta.Dock = System.Windows.Forms.DockStyle.Top;
            this.pan_consulta.Location = new System.Drawing.Point(3, 69);
            this.pan_consulta.Name = "pan_consulta";
            this.pan_consulta.Size = new System.Drawing.Size(894, 243);
            this.pan_consulta.TabIndex = 1;
            // 
            // tbx_consulta
            // 
            this.tbx_consulta.Location = new System.Drawing.Point(94, 9);
            this.tbx_consulta.MaxLength = 4500;
            this.tbx_consulta.Multiline = true;
            this.tbx_consulta.Name = "tbx_consulta";
            this.tbx_consulta.Size = new System.Drawing.Size(791, 231);
            this.tbx_consulta.TabIndex = 1;
            // 
            // lbl_consulta
            // 
            this.lbl_consulta.AutoSize = true;
            this.lbl_consulta.Location = new System.Drawing.Point(9, 12);
            this.lbl_consulta.Name = "lbl_consulta";
            this.lbl_consulta.Size = new System.Drawing.Size(69, 19);
            this.lbl_consulta.TabIndex = 0;
            this.lbl_consulta.Text = "Consulta";
            // 
            // pan_descricao
            // 
            this.pan_descricao.Controls.Add(this.tbx_descricao);
            this.pan_descricao.Controls.Add(this.lbl_descricao);
            this.pan_descricao.Dock = System.Windows.Forms.DockStyle.Top;
            this.pan_descricao.Location = new System.Drawing.Point(3, 23);
            this.pan_descricao.Name = "pan_descricao";
            this.pan_descricao.Size = new System.Drawing.Size(894, 46);
            this.pan_descricao.TabIndex = 0;
            // 
            // tbx_descricao
            // 
            this.tbx_descricao.Location = new System.Drawing.Point(94, 9);
            this.tbx_descricao.MaxLength = 400;
            this.tbx_descricao.Name = "tbx_descricao";
            this.tbx_descricao.Size = new System.Drawing.Size(791, 27);
            this.tbx_descricao.TabIndex = 1;
            // 
            // lbl_descricao
            // 
            this.lbl_descricao.AutoSize = true;
            this.lbl_descricao.Location = new System.Drawing.Point(9, 12);
            this.lbl_descricao.Name = "lbl_descricao";
            this.lbl_descricao.Size = new System.Drawing.Size(79, 19);
            this.lbl_descricao.TabIndex = 0;
            this.lbl_descricao.Text = "Descrição";
            // 
            // pan_intevalo
            // 
            this.pan_intevalo.Controls.Add(this.tbx_intervalo);
            this.pan_intevalo.Controls.Add(this.lbl_intervalo);
            this.pan_intevalo.Dock = System.Windows.Forms.DockStyle.Top;
            this.pan_intevalo.Location = new System.Drawing.Point(3, 358);
            this.pan_intevalo.Name = "pan_intevalo";
            this.pan_intevalo.Size = new System.Drawing.Size(894, 46);
            this.pan_intevalo.TabIndex = 3;
            // 
            // tbx_intervalo
            // 
            this.tbx_intervalo.Location = new System.Drawing.Point(184, 9);
            this.tbx_intervalo.MaxLength = 400;
            this.tbx_intervalo.Name = "tbx_intervalo";
            this.tbx_intervalo.Size = new System.Drawing.Size(701, 27);
            this.tbx_intervalo.TabIndex = 1;
            // 
            // lbl_intervalo
            // 
            this.lbl_intervalo.AutoSize = true;
            this.lbl_intervalo.Location = new System.Drawing.Point(9, 12);
            this.lbl_intervalo.Name = "lbl_intervalo";
            this.lbl_intervalo.Size = new System.Drawing.Size(163, 19);
            this.lbl_intervalo.TabIndex = 0;
            this.lbl_intervalo.Text = "Intervalo para executar";
            // 
            // FO_CadastraObserver
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(249)))), ((int)(((byte)(238)))));
            this.ClientSize = new System.Drawing.Size(900, 488);
            this.Controls.Add(this.pan_completo);
            this.Controls.Add(this.pan_botoes);
            this.Font = new System.Drawing.Font("Times New Roman", 10F);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FO_CadastraObserver";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cadastro consulta autmática";
            this.pan_botoes.ResumeLayout(false);
            this.pan_completo.ResumeLayout(false);
            this.grb_formulario.ResumeLayout(false);
            this.pan_emails.ResumeLayout(false);
            this.pan_emails.PerformLayout();
            this.pan_consulta.ResumeLayout(false);
            this.pan_consulta.PerformLayout();
            this.pan_descricao.ResumeLayout(false);
            this.pan_descricao.PerformLayout();
            this.pan_intevalo.ResumeLayout(false);
            this.pan_intevalo.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pan_botoes;
        private System.Windows.Forms.Panel pan_completo;
        private System.Windows.Forms.GroupBox grb_formulario;
        private System.Windows.Forms.Panel pan_consulta;
        private System.Windows.Forms.TextBox tbx_consulta;
        private System.Windows.Forms.Label lbl_consulta;
        private System.Windows.Forms.Panel pan_descricao;
        private System.Windows.Forms.TextBox tbx_descricao;
        private System.Windows.Forms.Label lbl_descricao;
        private System.Windows.Forms.Panel pan_emails;
        private System.Windows.Forms.TextBox tbx_emails;
        private System.Windows.Forms.Label lbl_emails;
        private System.Windows.Forms.Button btn_executar;
        private System.Windows.Forms.Panel pan_intevalo;
        private System.Windows.Forms.TextBox tbx_intervalo;
        private System.Windows.Forms.Label lbl_intervalo;
    }
}