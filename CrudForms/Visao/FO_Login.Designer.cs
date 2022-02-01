namespace Visao
{
    partial class FO_Login
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FO_Login));
            this.lbl_versao = new System.Windows.Forms.Label();
            this.btn_sair = new System.Windows.Forms.Button();
            this.btn_login = new System.Windows.Forms.Button();
            this.tbx_password = new System.Windows.Forms.TextBox();
            this.tbx_login = new System.Windows.Forms.TextBox();
            this.lbl_password = new System.Windows.Forms.Label();
            this.lbl_login = new System.Windows.Forms.Label();
            this.pan_lateral = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // lbl_versao
            // 
            this.lbl_versao.AutoSize = true;
            this.lbl_versao.Font = new System.Drawing.Font("Arial Narrow", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_versao.Location = new System.Drawing.Point(9, 337);
            this.lbl_versao.Name = "lbl_versao";
            this.lbl_versao.Size = new System.Drawing.Size(39, 15);
            this.lbl_versao.TabIndex = 21;
            this.lbl_versao.Text = "Versão:";
            // 
            // btn_sair
            // 
            this.btn_sair.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(249)))), ((int)(((byte)(238)))));
            this.btn_sair.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_sair.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_sair.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_sair.Location = new System.Drawing.Point(125, 326);
            this.btn_sair.Name = "btn_sair";
            this.btn_sair.Size = new System.Drawing.Size(57, 26);
            this.btn_sair.TabIndex = 20;
            this.btn_sair.Text = "Sair";
            this.btn_sair.UseVisualStyleBackColor = false;
            this.btn_sair.Click += new System.EventHandler(this.btn_sair_Click);
            // 
            // btn_login
            // 
            this.btn_login.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(249)))), ((int)(((byte)(238)))));
            this.btn_login.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_login.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_login.Location = new System.Drawing.Point(188, 326);
            this.btn_login.Name = "btn_login";
            this.btn_login.Size = new System.Drawing.Size(58, 26);
            this.btn_login.TabIndex = 19;
            this.btn_login.Text = "Logar";
            this.btn_login.UseVisualStyleBackColor = false;
            this.btn_login.Click += new System.EventHandler(this.btn_login_Click);
            // 
            // tbx_password
            // 
            this.tbx_password.AcceptsTab = true;
            this.tbx_password.Location = new System.Drawing.Point(62, 285);
            this.tbx_password.MaxLength = 20;
            this.tbx_password.Name = "tbx_password";
            this.tbx_password.Size = new System.Drawing.Size(184, 23);
            this.tbx_password.TabIndex = 18;
            this.tbx_password.UseSystemPasswordChar = true;
            // 
            // tbx_login
            // 
            this.tbx_login.Location = new System.Drawing.Point(62, 243);
            this.tbx_login.MaxLength = 20;
            this.tbx_login.Name = "tbx_login";
            this.tbx_login.Size = new System.Drawing.Size(184, 23);
            this.tbx_login.TabIndex = 17;
            // 
            // lbl_password
            // 
            this.lbl_password.AutoSize = true;
            this.lbl_password.Location = new System.Drawing.Point(9, 286);
            this.lbl_password.Name = "lbl_password";
            this.lbl_password.Size = new System.Drawing.Size(45, 16);
            this.lbl_password.TabIndex = 15;
            this.lbl_password.Text = "Senha:";
            // 
            // lbl_login
            // 
            this.lbl_login.AutoSize = true;
            this.lbl_login.Location = new System.Drawing.Point(9, 244);
            this.lbl_login.Name = "lbl_login";
            this.lbl_login.Size = new System.Drawing.Size(43, 16);
            this.lbl_login.TabIndex = 16;
            this.lbl_login.Text = "Login:";
            // 
            // pan_lateral
            // 
            this.pan_lateral.BackgroundImage = global::Pj.Properties.Resources.project_management200x200;
            this.pan_lateral.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pan_lateral.Location = new System.Drawing.Point(23, 12);
            this.pan_lateral.Name = "pan_lateral";
            this.pan_lateral.Size = new System.Drawing.Size(212, 214);
            this.pan_lateral.TabIndex = 22;
            // 
            // FO_Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Snow;
            this.ClientSize = new System.Drawing.Size(258, 356);
            this.Controls.Add(this.pan_lateral);
            this.Controls.Add(this.lbl_versao);
            this.Controls.Add(this.btn_sair);
            this.Controls.Add(this.btn_login);
            this.Controls.Add(this.tbx_password);
            this.Controls.Add(this.tbx_login);
            this.Controls.Add(this.lbl_password);
            this.Controls.Add(this.lbl_login);
            this.Font = new System.Drawing.Font("Times New Roman", 10F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FO_Login";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Login";
            this.Load += new System.EventHandler(this.FO_Login_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbl_versao;
        private System.Windows.Forms.Button btn_sair;
        private System.Windows.Forms.Button btn_login;
        private System.Windows.Forms.TextBox tbx_password;
        private System.Windows.Forms.TextBox tbx_login;
        private System.Windows.Forms.Label lbl_password;
        private System.Windows.Forms.Label lbl_login;
        private System.Windows.Forms.Panel pan_lateral;
    }
}