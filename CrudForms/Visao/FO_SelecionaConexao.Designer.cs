
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
            this.tbx_connectionStrings = new System.Windows.Forms.TextBox();
            this.lbl_servidorSQLServer = new System.Windows.Forms.Label();
            this.btn_info_servidorSqlServer = new System.Windows.Forms.Button();
            this.pan_botton = new System.Windows.Forms.Panel();
            this.btn_confirmar = new System.Windows.Forms.Button();
            this.grb_configuracaoSQLSERVER.SuspendLayout();
            this.pan_botton.SuspendLayout();
            this.SuspendLayout();
            // 
            // grb_configuracaoSQLSERVER
            // 
            this.grb_configuracaoSQLSERVER.Controls.Add(this.pan_botton);
            this.grb_configuracaoSQLSERVER.Controls.Add(this.btn_info_servidorSqlServer);
            this.grb_configuracaoSQLSERVER.Controls.Add(this.tbx_connectionStrings);
            this.grb_configuracaoSQLSERVER.Controls.Add(this.lbl_servidorSQLServer);
            this.grb_configuracaoSQLSERVER.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grb_configuracaoSQLSERVER.Location = new System.Drawing.Point(0, 0);
            this.grb_configuracaoSQLSERVER.Name = "grb_configuracaoSQLSERVER";
            this.grb_configuracaoSQLSERVER.Size = new System.Drawing.Size(599, 113);
            this.grb_configuracaoSQLSERVER.TabIndex = 1;
            this.grb_configuracaoSQLSERVER.TabStop = false;
            this.grb_configuracaoSQLSERVER.Text = "Configuração";
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
            // btn_info_servidorSqlServer
            // 
            this.btn_info_servidorSqlServer.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_info_servidorSqlServer.BackgroundImage")));
            this.btn_info_servidorSqlServer.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_info_servidorSqlServer.Location = new System.Drawing.Point(567, 26);
            this.btn_info_servidorSqlServer.Name = "btn_info_servidorSqlServer";
            this.btn_info_servidorSqlServer.Size = new System.Drawing.Size(20, 20);
            this.btn_info_servidorSqlServer.TabIndex = 9;
            this.btn_info_servidorSqlServer.UseVisualStyleBackColor = true;
            // 
            // pan_botton
            // 
            this.pan_botton.Controls.Add(this.btn_confirmar);
            this.pan_botton.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pan_botton.Location = new System.Drawing.Point(3, 75);
            this.pan_botton.Name = "pan_botton";
            this.pan_botton.Size = new System.Drawing.Size(593, 35);
            this.pan_botton.TabIndex = 11;
            // 
            // btn_confirmar
            // 
            this.btn_confirmar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_confirmar.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_confirmar.Location = new System.Drawing.Point(515, 3);
            this.btn_confirmar.Name = "btn_confirmar";
            this.btn_confirmar.Size = new System.Drawing.Size(75, 29);
            this.btn_confirmar.TabIndex = 23;
            this.btn_confirmar.Text = "Confirmar";
            this.btn_confirmar.UseVisualStyleBackColor = true;
            this.btn_confirmar.Click += new System.EventHandler(this.btn_confirmar_Click);
            // 
            // FO_SelecionaConexao
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(249)))), ((int)(((byte)(238)))));
            this.ClientSize = new System.Drawing.Size(599, 113);
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
    }
}