
namespace Visao
{
    partial class FO_ImportaPlanilha
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
            this.pan_botton = new System.Windows.Forms.Panel();
            this.btn_confirmar = new System.Windows.Forms.Button();
            this.grb_configuracaoSQLSERVER = new System.Windows.Forms.GroupBox();
            this.lbl_tabela = new System.Windows.Forms.Label();
            this.tbx_nome_tabela = new System.Windows.Forms.TextBox();
            this.btn_select_file = new System.Windows.Forms.Button();
            this.lbl_planilha = new System.Windows.Forms.Label();
            this.tbx_caminhaPlanilha = new System.Windows.Forms.TextBox();
            this.pan_botton.SuspendLayout();
            this.grb_configuracaoSQLSERVER.SuspendLayout();
            this.SuspendLayout();
            // 
            // pan_botton
            // 
            this.pan_botton.Controls.Add(this.btn_confirmar);
            this.pan_botton.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pan_botton.Location = new System.Drawing.Point(0, 98);
            this.pan_botton.Name = "pan_botton";
            this.pan_botton.Size = new System.Drawing.Size(583, 35);
            this.pan_botton.TabIndex = 16;
            // 
            // btn_confirmar
            // 
            this.btn_confirmar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_confirmar.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_confirmar.Location = new System.Drawing.Point(497, 3);
            this.btn_confirmar.Name = "btn_confirmar";
            this.btn_confirmar.Size = new System.Drawing.Size(83, 29);
            this.btn_confirmar.TabIndex = 23;
            this.btn_confirmar.Text = "Confirmar";
            this.btn_confirmar.UseVisualStyleBackColor = true;
            this.btn_confirmar.Click += new System.EventHandler(this.btn_confirmar_Click);
            // 
            // grb_configuracaoSQLSERVER
            // 
            this.grb_configuracaoSQLSERVER.Controls.Add(this.lbl_tabela);
            this.grb_configuracaoSQLSERVER.Controls.Add(this.tbx_nome_tabela);
            this.grb_configuracaoSQLSERVER.Controls.Add(this.btn_select_file);
            this.grb_configuracaoSQLSERVER.Controls.Add(this.lbl_planilha);
            this.grb_configuracaoSQLSERVER.Controls.Add(this.tbx_caminhaPlanilha);
            this.grb_configuracaoSQLSERVER.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grb_configuracaoSQLSERVER.Location = new System.Drawing.Point(0, 0);
            this.grb_configuracaoSQLSERVER.Name = "grb_configuracaoSQLSERVER";
            this.grb_configuracaoSQLSERVER.Size = new System.Drawing.Size(583, 133);
            this.grb_configuracaoSQLSERVER.TabIndex = 17;
            this.grb_configuracaoSQLSERVER.TabStop = false;
            this.grb_configuracaoSQLSERVER.Text = "Importar Planilha";
            // 
            // lbl_tabela
            // 
            this.lbl_tabela.AutoSize = true;
            this.lbl_tabela.Location = new System.Drawing.Point(10, 59);
            this.lbl_tabela.Name = "lbl_tabela";
            this.lbl_tabela.Size = new System.Drawing.Size(114, 19);
            this.lbl_tabela.TabIndex = 26;
            this.lbl_tabela.Text = "Nome da tabela";
            // 
            // tbx_nome_tabela
            // 
            this.tbx_nome_tabela.Location = new System.Drawing.Point(130, 56);
            this.tbx_nome_tabela.MaxLength = 100;
            this.tbx_nome_tabela.Name = "tbx_nome_tabela";
            this.tbx_nome_tabela.Size = new System.Drawing.Size(446, 27);
            this.tbx_nome_tabela.TabIndex = 25;
            // 
            // btn_select_file
            // 
            this.btn_select_file.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_select_file.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_select_file.Location = new System.Drawing.Point(500, 22);
            this.btn_select_file.Name = "btn_select_file";
            this.btn_select_file.Size = new System.Drawing.Size(77, 29);
            this.btn_select_file.TabIndex = 24;
            this.btn_select_file.Text = "Arquivo";
            this.btn_select_file.UseVisualStyleBackColor = true;
            this.btn_select_file.Click += new System.EventHandler(this.btn_select_file_Click);
            // 
            // lbl_planilha
            // 
            this.lbl_planilha.AutoSize = true;
            this.lbl_planilha.Location = new System.Drawing.Point(13, 27);
            this.lbl_planilha.Name = "lbl_planilha";
            this.lbl_planilha.Size = new System.Drawing.Size(64, 19);
            this.lbl_planilha.TabIndex = 2;
            this.lbl_planilha.Text = "Planilha";
            // 
            // tbx_nomeConsulta
            // 
            this.tbx_caminhaPlanilha.Enabled = false;
            this.tbx_caminhaPlanilha.Location = new System.Drawing.Point(80, 23);
            this.tbx_caminhaPlanilha.MaxLength = 100;
            this.tbx_caminhaPlanilha.Name = "tbx_nomeConsulta";
            this.tbx_caminhaPlanilha.Size = new System.Drawing.Size(413, 27);
            this.tbx_caminhaPlanilha.TabIndex = 1;
            // 
            // FO_ImportaPlanilha
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(249)))), ((int)(((byte)(238)))));
            this.ClientSize = new System.Drawing.Size(583, 133);
            this.Controls.Add(this.pan_botton);
            this.Controls.Add(this.grb_configuracaoSQLSERVER);
            this.Font = new System.Drawing.Font("Times New Roman", 10F);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FO_ImportaPlanilha";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Importar planilha CSV";
            this.pan_botton.ResumeLayout(false);
            this.grb_configuracaoSQLSERVER.ResumeLayout(false);
            this.grb_configuracaoSQLSERVER.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pan_botton;
        private System.Windows.Forms.Button btn_confirmar;
        private System.Windows.Forms.GroupBox grb_configuracaoSQLSERVER;
        private System.Windows.Forms.TextBox tbx_caminhaPlanilha;
        private System.Windows.Forms.Label lbl_planilha;
        private System.Windows.Forms.Button btn_select_file;
        private System.Windows.Forms.TextBox tbx_nome_tabela;
        private System.Windows.Forms.Label lbl_tabela;
    }
}