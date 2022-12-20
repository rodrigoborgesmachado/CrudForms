namespace Visao
{
    partial class FO_Principal
    {
        /// <summary>
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Windows Form Designer

        /// <summary>
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FO_Principal));
            this.pan_left = new System.Windows.Forms.Panel();
            this.pan_projetos = new System.Windows.Forms.Panel();
            this.trv_tabelas = new System.Windows.Forms.TreeView();
            this.mst_opcoes = new System.Windows.Forms.MenuStrip();
            this.tsm_opcoes = new System.Windows.Forms.ToolStripMenuItem();
            this.selecionaBaseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.atualizaTabelasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.quantidadeLinhasTabelasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.filtrarNaAberturaDaTelaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.filtrarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nãoFiltrarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.abrirConsultaGenéricaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.consultasSalvasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gerarDERToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.devtoolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.identarJsonToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.enumeraLinhasDasTabelasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.quantidadeDeDiasParaAtualizaçãoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tbx_quantidade_dias_atualizacao = new System.Windows.Forms.ToolStripTextBox();
            this.pan_principal = new System.Windows.Forms.Panel();
            this.tbc_table_control = new System.Windows.Forms.TabControl();
            this.pan_descricoes = new System.Windows.Forms.Panel();
            this.lbl_base = new System.Windows.Forms.Label();
            this.lbl_valorVersao = new System.Windows.Forms.Label();
            this.lbl_empresa = new System.Windows.Forms.Label();
            this.lbl_versao = new System.Windows.Forms.Label();
            this.buscarAtualizaçãoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pan_left.SuspendLayout();
            this.pan_projetos.SuspendLayout();
            this.mst_opcoes.SuspendLayout();
            this.pan_principal.SuspendLayout();
            this.pan_descricoes.SuspendLayout();
            this.SuspendLayout();
            // 
            // pan_left
            // 
            this.pan_left.Controls.Add(this.pan_projetos);
            this.pan_left.Controls.Add(this.mst_opcoes);
            this.pan_left.Dock = System.Windows.Forms.DockStyle.Left;
            this.pan_left.Location = new System.Drawing.Point(0, 0);
            this.pan_left.Name = "pan_left";
            this.pan_left.Size = new System.Drawing.Size(246, 606);
            this.pan_left.TabIndex = 0;
            // 
            // pan_projetos
            // 
            this.pan_projetos.Controls.Add(this.trv_tabelas);
            this.pan_projetos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pan_projetos.Location = new System.Drawing.Point(0, 28);
            this.pan_projetos.Name = "pan_projetos";
            this.pan_projetos.Size = new System.Drawing.Size(246, 578);
            this.pan_projetos.TabIndex = 1;
            // 
            // trv_tabelas
            // 
            this.trv_tabelas.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(249)))), ((int)(((byte)(238)))));
            this.trv_tabelas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trv_tabelas.Font = new System.Drawing.Font("Times New Roman", 10F);
            this.trv_tabelas.Location = new System.Drawing.Point(0, 0);
            this.trv_tabelas.Name = "trv_tabelas";
            this.trv_tabelas.Size = new System.Drawing.Size(246, 578);
            this.trv_tabelas.TabIndex = 0;
            this.trv_tabelas.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.trv_projetos_AfterSelect);
            // 
            // mst_opcoes
            // 
            this.mst_opcoes.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.mst_opcoes.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsm_opcoes});
            this.mst_opcoes.Location = new System.Drawing.Point(0, 0);
            this.mst_opcoes.Name = "mst_opcoes";
            this.mst_opcoes.Size = new System.Drawing.Size(246, 28);
            this.mst_opcoes.TabIndex = 1;
            this.mst_opcoes.Text = "menuStrip1";
            // 
            // tsm_opcoes
            // 
            this.tsm_opcoes.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.selecionaBaseToolStripMenuItem,
            this.atualizaTabelasToolStripMenuItem,
            this.quantidadeLinhasTabelasToolStripMenuItem,
            this.filtrarNaAberturaDaTelaToolStripMenuItem,
            this.abrirConsultaGenéricaToolStripMenuItem,
            this.consultasSalvasToolStripMenuItem,
            this.gerarDERToolStripMenuItem,
            this.devtoolsToolStripMenuItem,
            this.enumeraLinhasDasTabelasToolStripMenuItem,
            this.quantidadeDeDiasParaAtualizaçãoToolStripMenuItem,
            this.buscarAtualizaçãoToolStripMenuItem});
            this.tsm_opcoes.Name = "tsm_opcoes";
            this.tsm_opcoes.Size = new System.Drawing.Size(73, 24);
            this.tsm_opcoes.Text = "Opções";
            // 
            // selecionaBaseToolStripMenuItem
            // 
            this.selecionaBaseToolStripMenuItem.Name = "selecionaBaseToolStripMenuItem";
            this.selecionaBaseToolStripMenuItem.Size = new System.Drawing.Size(336, 26);
            this.selecionaBaseToolStripMenuItem.Text = "Seleciona Base";
            this.selecionaBaseToolStripMenuItem.Click += new System.EventHandler(this.selecionaBaseToolStripMenuItem_Click);
            // 
            // atualizaTabelasToolStripMenuItem
            // 
            this.atualizaTabelasToolStripMenuItem.Name = "atualizaTabelasToolStripMenuItem";
            this.atualizaTabelasToolStripMenuItem.Size = new System.Drawing.Size(336, 26);
            this.atualizaTabelasToolStripMenuItem.Text = "Atualizar Tabelas";
            this.atualizaTabelasToolStripMenuItem.Click += new System.EventHandler(this.atualizaTabelasToolStripMenuItem_Click);
            // 
            // quantidadeLinhasTabelasToolStripMenuItem
            // 
            this.quantidadeLinhasTabelasToolStripMenuItem.Name = "quantidadeLinhasTabelasToolStripMenuItem";
            this.quantidadeLinhasTabelasToolStripMenuItem.Size = new System.Drawing.Size(336, 26);
            this.quantidadeLinhasTabelasToolStripMenuItem.Text = "Quantidade Linhas Tabelas";
            this.quantidadeLinhasTabelasToolStripMenuItem.Click += new System.EventHandler(this.quantidadeLinhasTabelasToolStripMenuItem_Click);
            // 
            // filtrarNaAberturaDaTelaToolStripMenuItem
            // 
            this.filtrarNaAberturaDaTelaToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.filtrarToolStripMenuItem,
            this.nãoFiltrarToolStripMenuItem});
            this.filtrarNaAberturaDaTelaToolStripMenuItem.Name = "filtrarNaAberturaDaTelaToolStripMenuItem";
            this.filtrarNaAberturaDaTelaToolStripMenuItem.Size = new System.Drawing.Size(336, 26);
            this.filtrarNaAberturaDaTelaToolStripMenuItem.Text = "Filtrar na abertura da tela";
            // 
            // filtrarToolStripMenuItem
            // 
            this.filtrarToolStripMenuItem.Name = "filtrarToolStripMenuItem";
            this.filtrarToolStripMenuItem.Size = new System.Drawing.Size(160, 26);
            this.filtrarToolStripMenuItem.Text = "Filtrar";
            this.filtrarToolStripMenuItem.Click += new System.EventHandler(this.filtrarToolStripMenuItem_Click);
            // 
            // nãoFiltrarToolStripMenuItem
            // 
            this.nãoFiltrarToolStripMenuItem.Name = "nãoFiltrarToolStripMenuItem";
            this.nãoFiltrarToolStripMenuItem.Size = new System.Drawing.Size(160, 26);
            this.nãoFiltrarToolStripMenuItem.Text = "Não filtrar";
            this.nãoFiltrarToolStripMenuItem.Click += new System.EventHandler(this.nãoFiltrarToolStripMenuItem_Click);
            // 
            // abrirConsultaGenéricaToolStripMenuItem
            // 
            this.abrirConsultaGenéricaToolStripMenuItem.Name = "abrirConsultaGenéricaToolStripMenuItem";
            this.abrirConsultaGenéricaToolStripMenuItem.Size = new System.Drawing.Size(336, 26);
            this.abrirConsultaGenéricaToolStripMenuItem.Text = "Abrir Consulta Genérica";
            this.abrirConsultaGenéricaToolStripMenuItem.Click += new System.EventHandler(this.abrirConsultaGenéricaToolStripMenuItem_Click);
            // 
            // consultasSalvasToolStripMenuItem
            // 
            this.consultasSalvasToolStripMenuItem.Name = "consultasSalvasToolStripMenuItem";
            this.consultasSalvasToolStripMenuItem.Size = new System.Drawing.Size(336, 26);
            this.consultasSalvasToolStripMenuItem.Text = "Consultas Salvas";
            this.consultasSalvasToolStripMenuItem.Click += new System.EventHandler(this.consultasSalvasToolStripMenuItem_Click);
            // 
            // gerarDERToolStripMenuItem
            // 
            this.gerarDERToolStripMenuItem.Name = "gerarDERToolStripMenuItem";
            this.gerarDERToolStripMenuItem.Size = new System.Drawing.Size(336, 26);
            this.gerarDERToolStripMenuItem.Text = "Gerar DER";
            this.gerarDERToolStripMenuItem.Click += new System.EventHandler(this.gerarDERToolStripMenuItem_Click);
            // 
            // devtoolsToolStripMenuItem
            // 
            this.devtoolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.identarJsonToolStripMenuItem1});
            this.devtoolsToolStripMenuItem.Name = "devtoolsToolStripMenuItem";
            this.devtoolsToolStripMenuItem.Size = new System.Drawing.Size(336, 26);
            this.devtoolsToolStripMenuItem.Text = "Devtools";
            // 
            // identarJsonToolStripMenuItem1
            // 
            this.identarJsonToolStripMenuItem1.Name = "identarJsonToolStripMenuItem1";
            this.identarJsonToolStripMenuItem1.Size = new System.Drawing.Size(171, 26);
            this.identarJsonToolStripMenuItem1.Text = "Identar Json";
            this.identarJsonToolStripMenuItem1.Click += new System.EventHandler(this.identarJsonToolStripMenuItem1_Click);
            // 
            // enumeraLinhasDasTabelasToolStripMenuItem
            // 
            this.enumeraLinhasDasTabelasToolStripMenuItem.Name = "enumeraLinhasDasTabelasToolStripMenuItem";
            this.enumeraLinhasDasTabelasToolStripMenuItem.Size = new System.Drawing.Size(336, 26);
            this.enumeraLinhasDasTabelasToolStripMenuItem.Text = "Enumera linhas das tabelas";
            this.enumeraLinhasDasTabelasToolStripMenuItem.Click += new System.EventHandler(this.enumeraLinhasDasTabelasToolStripMenuItem_Click);
            // 
            // quantidadeDeDiasParaAtualizaçãoToolStripMenuItem
            // 
            this.quantidadeDeDiasParaAtualizaçãoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tbx_quantidade_dias_atualizacao});
            this.quantidadeDeDiasParaAtualizaçãoToolStripMenuItem.Name = "quantidadeDeDiasParaAtualizaçãoToolStripMenuItem";
            this.quantidadeDeDiasParaAtualizaçãoToolStripMenuItem.Size = new System.Drawing.Size(336, 26);
            this.quantidadeDeDiasParaAtualizaçãoToolStripMenuItem.Text = "Quantidade de dias para atualização";
            // 
            // tbx_quantidade_dias_atualizacao
            // 
            this.tbx_quantidade_dias_atualizacao.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.tbx_quantidade_dias_atualizacao.Name = "tbx_quantidade_dias_atualizacao";
            this.tbx_quantidade_dias_atualizacao.Size = new System.Drawing.Size(100, 27);
            this.tbx_quantidade_dias_atualizacao.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbx_quantidade_dias_atualizacao_KeyPress);
            // 
            // pan_principal
            // 
            this.pan_principal.Controls.Add(this.tbc_table_control);
            this.pan_principal.Controls.Add(this.pan_descricoes);
            this.pan_principal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pan_principal.Location = new System.Drawing.Point(246, 0);
            this.pan_principal.Name = "pan_principal";
            this.pan_principal.Size = new System.Drawing.Size(864, 606);
            this.pan_principal.TabIndex = 1;
            // 
            // tbc_table_control
            // 
            this.tbc_table_control.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbc_table_control.Location = new System.Drawing.Point(0, 0);
            this.tbc_table_control.Name = "tbc_table_control";
            this.tbc_table_control.SelectedIndex = 0;
            this.tbc_table_control.Size = new System.Drawing.Size(864, 583);
            this.tbc_table_control.TabIndex = 0;
            // 
            // pan_descricoes
            // 
            this.pan_descricoes.Controls.Add(this.lbl_base);
            this.pan_descricoes.Controls.Add(this.lbl_valorVersao);
            this.pan_descricoes.Controls.Add(this.lbl_empresa);
            this.pan_descricoes.Controls.Add(this.lbl_versao);
            this.pan_descricoes.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pan_descricoes.Location = new System.Drawing.Point(0, 583);
            this.pan_descricoes.Name = "pan_descricoes";
            this.pan_descricoes.Size = new System.Drawing.Size(864, 23);
            this.pan_descricoes.TabIndex = 2;
            // 
            // lbl_base
            // 
            this.lbl_base.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lbl_base.AutoSize = true;
            this.lbl_base.Font = new System.Drawing.Font("Times New Roman", 9F);
            this.lbl_base.Location = new System.Drawing.Point(6, 4);
            this.lbl_base.Name = "lbl_base";
            this.lbl_base.Size = new System.Drawing.Size(54, 17);
            this.lbl_base.TabIndex = 2;
            this.lbl_base.Text = "<Base>";
            // 
            // lbl_valorVersao
            // 
            this.lbl_valorVersao.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_valorVersao.AutoSize = true;
            this.lbl_valorVersao.Font = new System.Drawing.Font("Times New Roman", 9F);
            this.lbl_valorVersao.Location = new System.Drawing.Point(805, 4);
            this.lbl_valorVersao.Name = "lbl_valorVersao";
            this.lbl_valorVersao.Size = new System.Drawing.Size(47, 17);
            this.lbl_valorVersao.TabIndex = 1;
            this.lbl_valorVersao.Text = "versao";
            // 
            // lbl_empresa
            // 
            this.lbl_empresa.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_empresa.AutoSize = true;
            this.lbl_empresa.Font = new System.Drawing.Font("Times New Roman", 9F);
            this.lbl_empresa.Location = new System.Drawing.Point(648, 4);
            this.lbl_empresa.Name = "lbl_empresa";
            this.lbl_empresa.Size = new System.Drawing.Size(74, 17);
            this.lbl_empresa.TabIndex = 1;
            this.lbl_empresa.Text = "CrudForms";
            // 
            // lbl_versao
            // 
            this.lbl_versao.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_versao.AutoSize = true;
            this.lbl_versao.Font = new System.Drawing.Font("Times New Roman", 9F);
            this.lbl_versao.Location = new System.Drawing.Point(757, 4);
            this.lbl_versao.Name = "lbl_versao";
            this.lbl_versao.Size = new System.Drawing.Size(52, 17);
            this.lbl_versao.TabIndex = 1;
            this.lbl_versao.Text = "Versão:";
            // 
            // buscarAtualizaçãoToolStripMenuItem
            // 
            this.buscarAtualizaçãoToolStripMenuItem.Name = "buscarAtualizaçãoToolStripMenuItem";
            this.buscarAtualizaçãoToolStripMenuItem.Size = new System.Drawing.Size(336, 26);
            this.buscarAtualizaçãoToolStripMenuItem.Text = "Buscar Atualização";
            this.buscarAtualizaçãoToolStripMenuItem.Click += new System.EventHandler(this.buscarAtualizaçãoToolStripMenuItem_Click);
            // 
            // FO_Principal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(249)))), ((int)(((byte)(238)))));
            this.ClientSize = new System.Drawing.Size(1110, 606);
            this.Controls.Add(this.pan_principal);
            this.Controls.Add(this.pan_left);
            this.Font = new System.Drawing.Font("Times New Roman", 10F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(1126, 644);
            this.Name = "FO_Principal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Crud Forms";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FO_Principal_Load);
            this.pan_left.ResumeLayout(false);
            this.pan_left.PerformLayout();
            this.pan_projetos.ResumeLayout(false);
            this.mst_opcoes.ResumeLayout(false);
            this.mst_opcoes.PerformLayout();
            this.pan_principal.ResumeLayout(false);
            this.pan_descricoes.ResumeLayout(false);
            this.pan_descricoes.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pan_left;
        private System.Windows.Forms.Panel pan_principal;
        private System.Windows.Forms.TabControl tbc_table_control;
        private System.Windows.Forms.Panel pan_projetos;
        private System.Windows.Forms.TreeView trv_tabelas;
        private System.Windows.Forms.Panel pan_descricoes;
        private System.Windows.Forms.Label lbl_valorVersao;
        private System.Windows.Forms.Label lbl_versao;
        private System.Windows.Forms.Label lbl_empresa;
        private System.Windows.Forms.MenuStrip mst_opcoes;
        private System.Windows.Forms.ToolStripMenuItem tsm_opcoes;
        private System.Windows.Forms.ToolStripMenuItem selecionaBaseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem quantidadeLinhasTabelasToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem filtrarNaAberturaDaTelaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem filtrarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem nãoFiltrarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem abrirConsultaGenéricaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem consultasSalvasToolStripMenuItem;
        private System.Windows.Forms.Label lbl_base;
        private System.Windows.Forms.ToolStripMenuItem gerarDERToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem devtoolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem identarJsonToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem atualizaTabelasToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem enumeraLinhasDasTabelasToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem quantidadeDeDiasParaAtualizaçãoToolStripMenuItem;
        private System.Windows.Forms.ToolStripTextBox tbx_quantidade_dias_atualizacao;
        private System.Windows.Forms.ToolStripMenuItem buscarAtualizaçãoToolStripMenuItem;
    }
}

