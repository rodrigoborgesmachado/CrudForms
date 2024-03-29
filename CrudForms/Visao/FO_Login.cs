﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Visao
{
    public partial class FO_Login : Form
    {
        public FO_Login()
        {
            InitializeComponent();
            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
            System.Diagnostics.FileVersionInfo fvi = System.Diagnostics.FileVersionInfo.GetVersionInfo(assembly.Location);
            string version = fvi.FileVersion;

            this.lbl_versao.Text = "Versão: " + version;

            if (Model.Parametros.ModoDark)
            {
                this.BackColor = Color.FromArgb(51, 51, 51);
                this.ForeColor = Color.White;
                this.pan_lateral.BackgroundImage = global::Pj.Properties.Resources.project_management_white_200x200;
            }
            else
            {
                this.pan_lateral.BackgroundImage = global::Pj.Properties.Resources.project_management200x200;
                this.BackColor = Color.FromArgb(251, 249, 238);
                this.ForeColor = Color.Black;
            }

            foreach (Button button in this.Controls.OfType<Button>())
            {
                button.BackColor = this.BackColor;
                button.ForeColor = this.ForeColor;
            }
        }

        private void btn_sair_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Dispose();
        }

        private void btn_login_Click(object sender, EventArgs e)
        {
            string pass = Hash(this.tbx_password.Text).ToString();
            var taask = Util.WebUtil.Login.ValidaLoginAsync(this.tbx_login.Text, pass);
            while (!taask.IsCompleted) ;
            if (taask.Result)
            {
                this.DialogResult = DialogResult.OK;
                this.Dispose();
            }
            else
            {
                Visao.Message.MensagemAlerta("Login inválido!");
            }
        }

        private void FO_Login_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Método que calcula o hash de uma string
        /// </summary>
        /// <param name="texto"></param>
        /// <returns></returns>
        private long Hash(string texto)
        {
            long hash = 0;

            if (texto.Length == 0) return hash;

            for (int i = 0; i< texto.Length; i++)
            {
                int t = (int)texto[i];
                hash = ((hash << 5) - hash) + t;
                hash = hash & hash;
            }

            return hash;
        }
    }
}
