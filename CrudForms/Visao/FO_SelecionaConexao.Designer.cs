
namespace Visao
{
    partial class FO_SelecionaConexao
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FO_SelecionaConexao));
            this.grb_configuracaoSQLSERVER = new System.Windows.Forms.GroupBox();
            this.pan_botton = new System.Windows.Forms.Panel();
            this.btn_confirmar = new System.Windows.Forms.Button();
            this.btn_info_servidorSqlServer = new System.Windows.Forms.Button();
            this.tbx_connectionStrings = new System.Windows.Forms.TextBox();
            this.lbl_servidorSQLServer = new System.Windows.Forms.Label();
            this.tbx_nome = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.grb_configuracaoSQLSERVER.SuspendLayout();
            this.pan_botton.SuspendLayout();
            this.SuspendLayout();
            // 
            // grb_configuracaoSQLSERVER
            // 
            this.grb_configuracaoSQLSERVER.Controls.Add(this.tbx_nome);
            this.grb_configuracaoSQLSERVER.Controls.Add(this.label1);
            this.grb_configuracaoSQLSERVER.Controls.Add(this.pan_botton);
            this.grb_configuracaoSQLSERVER.Controls.Add(this.btn_info_servidorSqlServer);
            this.grb_configuracaoSQLSERVER.Controls.Add(this.tbx_connectionStrings);
            this.grb_configuracaoSQLSERVER.Controls.Add(this.lbl_servidorSQLServer);
            this.grb_configuracaoSQLSERVER.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grb_configuracaoSQLSERVER.Location = new System.Drawing.Point(0, 0);
            this.grb_configuracaoSQLSERVER.Name = "grb_configuracaoSQLSERVER";
            this.grb_configuracaoSQLSERVER.Size = new System.Drawing.Size(599, 130);
            this.grb_configuracaoSQLSERVER.TabIndex = 1;
            this.grb_configuracaoSQLSERVER.TabStop = false;
            this.grb_configuracaoSQLSERVER.Text = "Configuração";
            // 
            // pan_botton
            // 
            this.pan_botton.Controls.Add(this.btn_confirmar);
            this.pan_botton.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pan_botton.Location = new System.Drawing.Point(3, 92);
            this.pan_botton.Name = "pan_botton";
            this.pan_botton.Size = new System.Drawing.Size(593, 35);
            this.pan_botton.TabIndex = 11;
            // 
            // btn_confirmar
            // 
            this.btn_confirmar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_confirmar.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_confirmar.Location = new System.Drawing.Point(508, 3);
            this.btn_confirmar.Name = "btn_confirmar";
            this.btn_confirmar.Size = new System.Drawing.Size(82, 29);
            this.btn_confirmar.TabIndex = 23;
            this.btn_confirmar.Text = "Confirmar";
            this.btn_confirmar.UseVisualStyleBackColor = true;
            this.btn_confirmar.Click += new System.EventHandler(this.btn_confirmar_Click);
            // 
            // btn_info_servidorSqlServer
            // 
            this.btn_info_servidorSqlServer.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_info_servidorSqlServer.BackgroundImage")));
            this.btn_info_servidorSqlServer.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_info_servidorSqlServer.Location = new System.Drawing.Point(567, 26);
            this.btn_info_servidorSqlServer.Name = "btn_info_servidorSqlServer";
            this.btn_info_servidorSqlServer.Size = new System.Drawing.Size(20, 20);
            this.btn_info_servidorSqlServer.TabIndex = 9;
            this.btn_info_servidorSqlServer.UseVisualStyleBackColor = true;
            this.btn_info_servidorSqlServer.Click += new System.EventHandler(this.btn_info_servidorSqlServer_Click);
            // 
            // tbx_connectionStrings
            // 
            this.tbx_connectionStrings.Location = new System.Drawing.Point(148, 23);
            this.tbx_connectionStrings.Name = "tbx_connectionStrings";
            this.tbx_connectionStrings.Size = new System.Drawing.Size(413, 27);
            this.tbx_connectionStrings.TabIndex = 1;
            // 
            // lbl_servidorSQLServer
            // 
            this.lbl_servidorSQLServer.AutoSize = true;
            this.lbl_servidorSQLServer.Location = new System.Drawing.Point(9, 26);
            this.lbl_servidorSQLServer.Name = "lbl_servidorSQLServer";
            this.lbl_servidorSQLServer.Size = new System.Drawing.Size(133, 19);
            this.lbl_servidorSQLServer.TabIndex = 0;
            this.lbl_servidorSQLServer.Text = "Connection String";
            // 
            // tbx_nome
            // 
            this.tbx_nome.Location = new System.Drawing.Point(148, 56);
            this.tbx_nome.Name = "tbx_nome";
            this.tbx_nome.Size = new System.Drawing.Size(413, 27);
            this.tbx_nome.TabIndex = 13;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 59);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(104, 19);
            this.label1.TabIndex = 12;
            this.label1.Text = "Nome da base";
            // 
            // FO_SelecionaConexao
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(249)))), ((int)(((byte)(238)))));
            this.ClientSize = new System.Drawing.Size(599, 130);
            this.Controls.Add(this.grb_configuracaoSQLSERVER);
            this.Font = new System.Drawing.Font("Times New Roman", 10F);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FO_SelecionaConexao";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Conexão";
            this.grb_configuracaoSQLSERVER.ResumeLayout(false);
            this.grb_configuracaoSQLSERVER.PerformLayout();
            this.pan_botton.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grb_configuracaoSQLSERVER;
        private System.Windows.Forms.Button btn_info_servidorSqlServer;
        private System.Windows.Forms.TextBox tbx_connectionStrings;
        private System.Windows.Forms.Label lbl_servidorSQLServer;
        private System.Windows.Forms.Panel pan_botton;
        private System.Windows.Forms.Button btn_confirmar;
        private System.Windows.Forms.TextBox tbx_nome;
        private System.Windows.Forms.Label label1;
    }
}