
namespace Visao
{
    partial class UC_FormularioGenerico
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

        #region Código gerado pelo Designer de Componentes

        /// <summary> 
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.pan_top = new System.Windows.Forms.Panel();
            this.pan_opcoes = new System.Windows.Forms.Panel();
            this.mst_opcoes = new System.Windows.Forms.MenuStrip();
            this.tsm_opcoes = new System.Windows.Forms.ToolStripMenuItem();
            this.exportarCSVToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.salvarConsultaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pan_fechar = new System.Windows.Forms.Panel();
            this.btn_fechar = new System.Windows.Forms.Button();
            this.dgv_generico = new System.Windows.Forms.DataGridView();
            this.grb_geral = new System.Windows.Forms.GroupBox();
            this.lbl_quantidadeLinhas = new System.Windows.Forms.Label();
            this.panel7 = new System.Windows.Forms.Panel();
            this.pan_botton = new System.Windows.Forms.Panel();
            this.btn_orderBy = new System.Windows.Forms.Button();
            this.btn_excluir = new System.Windows.Forms.Button();
            this.btn_incluir = new System.Windows.Forms.Button();
            this.btn_editar = new System.Windows.Forms.Button();
            this.btn_visualizar = new System.Windows.Forms.Button();
            this.btn_limparFiltro = new System.Windows.Forms.Button();
            this.btn_filtrar = new System.Windows.Forms.Button();
            this.pan_completo = new System.Windows.Forms.Panel();
            this.pan_tot = new System.Windows.Forms.Panel();
            this.btn_reload = new System.Windows.Forms.Button();
            this.pan_top.SuspendLayout();
            this.pan_opcoes.SuspendLayout();
            this.mst_opcoes.SuspendLayout();
            this.pan_fechar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_generico)).BeginInit();
            this.grb_geral.SuspendLayout();
            this.panel7.SuspendLayout();
            this.pan_botton.SuspendLayout();
            this.pan_completo.SuspendLayout();
            this.pan_tot.SuspendLayout();
            this.SuspendLayout();
            // 
            // pan_top
            // 
            this.pan_top.Controls.Add(this.pan_opcoes);
            this.pan_top.Controls.Add(this.pan_fechar);
            this.pan_top.Dock = System.Windows.Forms.DockStyle.Top;
            this.pan_top.Location = new System.Drawing.Point(0, 0);
            this.pan_top.Name = "pan_top";
            this.pan_top.Size = new System.Drawing.Size(740, 32);
            this.pan_top.TabIndex = 17;
            // 
            // pan_opcoes
            // 
            this.pan_opcoes.Controls.Add(this.mst_opcoes);
            this.pan_opcoes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pan_opcoes.Location = new System.Drawing.Point(0, 0);
            this.pan_opcoes.Name = "pan_opcoes";
            this.pan_opcoes.Size = new System.Drawing.Size(720, 32);
            this.pan_opcoes.TabIndex = 19;
            // 
            // mst_opcoes
            // 
            this.mst_opcoes.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.mst_opcoes.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsm_opcoes});
            this.mst_opcoes.Location = new System.Drawing.Point(0, 0);
            this.mst_opcoes.Name = "mst_opcoes";
            this.mst_opcoes.Size = new System.Drawing.Size(720, 28);
            this.mst_opcoes.TabIndex = 2;
            this.mst_opcoes.Text = "menuStrip1";
            // 
            // tsm_opcoes
            // 
            this.tsm_opcoes.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exportarCSVToolStripMenuItem,
            this.salvarConsultaToolStripMenuItem});
            this.tsm_opcoes.Name = "tsm_opcoes";
            this.tsm_opcoes.Size = new System.Drawing.Size(73, 24);
            this.tsm_opcoes.Text = "Opções";
            // 
            // exportarCSVToolStripMenuItem
            // 
            this.exportarCSVToolStripMenuItem.Name = "exportarCSVToolStripMenuItem";
            this.exportarCSVToolStripMenuItem.Size = new System.Drawing.Size(193, 26);
            this.exportarCSVToolStripMenuItem.Text = "Exportar CSV";
            this.exportarCSVToolStripMenuItem.Click += new System.EventHandler(this.exportarCSVToolStripMenuItem_Click);
            // 
            // salvarConsultaToolStripMenuItem
            // 
            this.salvarConsultaToolStripMenuItem.Name = "salvarConsultaToolStripMenuItem";
            this.salvarConsultaToolStripMenuItem.Size = new System.Drawing.Size(193, 26);
            this.salvarConsultaToolStripMenuItem.Text = "Salvar Consulta";
            this.salvarConsultaToolStripMenuItem.Click += new System.EventHandler(this.salvarConsultaToolStripMenuItem_Click);
            // 
            // pan_fechar
            // 
            this.pan_fechar.Controls.Add(this.btn_fechar);
            this.pan_fechar.Dock = System.Windows.Forms.DockStyle.Right;
            this.pan_fechar.Location = new System.Drawing.Point(720, 0);
            this.pan_fechar.Name = "pan_fechar";
            this.pan_fechar.Size = new System.Drawing.Size(20, 32);
            this.pan_fechar.TabIndex = 18;
            // 
            // btn_fechar
            // 
            this.btn_fechar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_fechar.BackColor = System.Drawing.Color.Red;
            this.btn_fechar.BackgroundImage = global::Pj.Properties.Resources.close_outline_100px20x20;
            this.btn_fechar.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_fechar.Location = new System.Drawing.Point(0, 0);
            this.btn_fechar.Name = "btn_fechar";
            this.btn_fechar.Size = new System.Drawing.Size(20, 20);
            this.btn_fechar.TabIndex = 17;
            this.btn_fechar.UseVisualStyleBackColor = false;
            this.btn_fechar.Click += new System.EventHandler(this.btn_fechar_Click);
            // 
            // dgv_generico
            // 
            this.dgv_generico.AllowUserToAddRows = false;
            this.dgv_generico.AllowUserToDeleteRows = false;
            this.dgv_generico.AllowUserToResizeColumns = false;
            this.dgv_generico.AllowUserToResizeRows = false;
            this.dgv_generico.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgv_generico.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv_generico.BackgroundColor = System.Drawing.Color.White;
            this.dgv_generico.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgv_generico.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;
            this.dgv_generico.ColumnHeadersHeight = 29;
            this.dgv_generico.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgv_generico.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgv_generico.EnableHeadersVisualStyles = false;
            this.dgv_generico.Location = new System.Drawing.Point(6, 35);
            this.dgv_generico.MultiSelect = false;
            this.dgv_generico.Name = "dgv_generico";
            this.dgv_generico.RowHeadersVisible = false;
            this.dgv_generico.RowHeadersWidth = 51;
            this.dgv_generico.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgv_generico.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_generico.ShowCellErrors = false;
            this.dgv_generico.ShowCellToolTips = false;
            this.dgv_generico.Size = new System.Drawing.Size(728, 439);
            this.dgv_generico.StandardTab = true;
            this.dgv_generico.TabIndex = 12;
            // 
            // grb_geral
            // 
            this.grb_geral.Controls.Add(this.btn_reload);
            this.grb_geral.Controls.Add(this.lbl_quantidadeLinhas);
            this.grb_geral.Controls.Add(this.dgv_generico);
            this.grb_geral.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grb_geral.Location = new System.Drawing.Point(0, 32);
            this.grb_geral.Name = "grb_geral";
            this.grb_geral.Size = new System.Drawing.Size(740, 494);
            this.grb_geral.TabIndex = 16;
            this.grb_geral.TabStop = false;
            this.grb_geral.Text = "<nome da tabela>";
            // 
            // lbl_quantidadeLinhas
            // 
            this.lbl_quantidadeLinhas.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lbl_quantidadeLinhas.AutoSize = true;
            this.lbl_quantidadeLinhas.Location = new System.Drawing.Point(4, 476);
            this.lbl_quantidadeLinhas.Name = "lbl_quantidadeLinhas";
            this.lbl_quantidadeLinhas.Size = new System.Drawing.Size(145, 19);
            this.lbl_quantidadeLinhas.TabIndex = 13;
            this.lbl_quantidadeLinhas.Text = "<qauntidade linhas>";
            // 
            // panel7
            // 
            this.panel7.BackColor = System.Drawing.Color.Transparent;
            this.panel7.Controls.Add(this.grb_geral);
            this.panel7.Controls.Add(this.pan_top);
            this.panel7.Controls.Add(this.pan_botton);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel7.Location = new System.Drawing.Point(0, 0);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(740, 562);
            this.panel7.TabIndex = 3;
            // 
            // pan_botton
            // 
            this.pan_botton.Controls.Add(this.btn_orderBy);
            this.pan_botton.Controls.Add(this.btn_excluir);
            this.pan_botton.Controls.Add(this.btn_incluir);
            this.pan_botton.Controls.Add(this.btn_editar);
            this.pan_botton.Controls.Add(this.btn_visualizar);
            this.pan_botton.Controls.Add(this.btn_limparFiltro);
            this.pan_botton.Controls.Add(this.btn_filtrar);
            this.pan_botton.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pan_botton.Location = new System.Drawing.Point(0, 526);
            this.pan_botton.Name = "pan_botton";
            this.pan_botton.Size = new System.Drawing.Size(740, 36);
            this.pan_botton.TabIndex = 20;
            // 
            // btn_orderBy
            // 
            this.btn_orderBy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_orderBy.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_orderBy.Location = new System.Drawing.Point(395, 4);
            this.btn_orderBy.Name = "btn_orderBy";
            this.btn_orderBy.Size = new System.Drawing.Size(81, 29);
            this.btn_orderBy.TabIndex = 17;
            this.btn_orderBy.Text = "Order By";
            this.btn_orderBy.UseVisualStyleBackColor = true;
            this.btn_orderBy.Click += new System.EventHandler(this.btn_orderBy_Click);
            // 
            // btn_excluir
            // 
            this.btn_excluir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btn_excluir.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_excluir.Location = new System.Drawing.Point(6, 3);
            this.btn_excluir.Name = "btn_excluir";
            this.btn_excluir.Size = new System.Drawing.Size(72, 29);
            this.btn_excluir.TabIndex = 16;
            this.btn_excluir.Text = "Excluir";
            this.btn_excluir.UseVisualStyleBackColor = true;
            this.btn_excluir.Click += new System.EventHandler(this.btn_excluir_Click);
            // 
            // btn_incluir
            // 
            this.btn_incluir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btn_incluir.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_incluir.Location = new System.Drawing.Point(84, 3);
            this.btn_incluir.Name = "btn_incluir";
            this.btn_incluir.Size = new System.Drawing.Size(72, 29);
            this.btn_incluir.TabIndex = 15;
            this.btn_incluir.Text = "Incluir";
            this.btn_incluir.UseVisualStyleBackColor = true;
            this.btn_incluir.Click += new System.EventHandler(this.btn_incluir_Click);
            // 
            // btn_editar
            // 
            this.btn_editar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btn_editar.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_editar.Location = new System.Drawing.Point(162, 3);
            this.btn_editar.Name = "btn_editar";
            this.btn_editar.Size = new System.Drawing.Size(72, 29);
            this.btn_editar.TabIndex = 14;
            this.btn_editar.Text = "Editar";
            this.btn_editar.UseVisualStyleBackColor = true;
            this.btn_editar.Click += new System.EventHandler(this.btn_editar_Click);
            // 
            // btn_visualizar
            // 
            this.btn_visualizar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btn_visualizar.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_visualizar.Location = new System.Drawing.Point(240, 3);
            this.btn_visualizar.Name = "btn_visualizar";
            this.btn_visualizar.Size = new System.Drawing.Size(101, 29);
            this.btn_visualizar.TabIndex = 13;
            this.btn_visualizar.Text = "Visualizar";
            this.btn_visualizar.UseVisualStyleBackColor = true;
            this.btn_visualizar.Click += new System.EventHandler(this.btn_visualizar_Click);
            // 
            // btn_limparFiltro
            // 
            this.btn_limparFiltro.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_limparFiltro.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_limparFiltro.Location = new System.Drawing.Point(482, 4);
            this.btn_limparFiltro.Name = "btn_limparFiltro";
            this.btn_limparFiltro.Size = new System.Drawing.Size(123, 29);
            this.btn_limparFiltro.TabIndex = 9;
            this.btn_limparFiltro.Text = "Limpar Filtro";
            this.btn_limparFiltro.UseVisualStyleBackColor = true;
            this.btn_limparFiltro.Click += new System.EventHandler(this.btn_limparFiltro_Click);
            // 
            // btn_filtrar
            // 
            this.btn_filtrar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_filtrar.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_filtrar.Location = new System.Drawing.Point(611, 4);
            this.btn_filtrar.Name = "btn_filtrar";
            this.btn_filtrar.Size = new System.Drawing.Size(123, 29);
            this.btn_filtrar.TabIndex = 8;
            this.btn_filtrar.Text = "Filtrar";
            this.btn_filtrar.UseVisualStyleBackColor = true;
            this.btn_filtrar.Click += new System.EventHandler(this.btn_filtrar_Click);
            // 
            // pan_completo
            // 
            this.pan_completo.Controls.Add(this.panel7);
            this.pan_completo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pan_completo.Location = new System.Drawing.Point(0, 0);
            this.pan_completo.Name = "pan_completo";
            this.pan_completo.Size = new System.Drawing.Size(740, 562);
            this.pan_completo.TabIndex = 1;
            // 
            // pan_tot
            // 
            this.pan_tot.BackColor = System.Drawing.Color.Transparent;
            this.pan_tot.Controls.Add(this.pan_completo);
            this.pan_tot.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pan_tot.Location = new System.Drawing.Point(0, 0);
            this.pan_tot.Name = "pan_tot";
            this.pan_tot.Size = new System.Drawing.Size(740, 562);
            this.pan_tot.TabIndex = 4;
            // 
            // btn_reload
            // 
            this.btn_reload.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_reload.BackgroundImage = global::Pj.Properties.Resources.loop_100px20x20;
            this.btn_reload.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_reload.Location = new System.Drawing.Point(714, 9);
            this.btn_reload.Name = "btn_reload";
            this.btn_reload.Size = new System.Drawing.Size(20, 20);
            this.btn_reload.TabIndex = 14;
            this.btn_reload.UseVisualStyleBackColor = true;
            this.btn_reload.Click += new System.EventHandler(this.btn_reload_Click);
            // 
            // UC_FormularioGenerico
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(249)))), ((int)(((byte)(238)))));
            this.Controls.Add(this.pan_tot);
            this.Font = new System.Drawing.Font("Times New Roman", 10F);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "UC_FormularioGenerico";
            this.Size = new System.Drawing.Size(740, 562);
            this.pan_top.ResumeLayout(false);
            this.pan_opcoes.ResumeLayout(false);
            this.pan_opcoes.PerformLayout();
            this.mst_opcoes.ResumeLayout(false);
            this.mst_opcoes.PerformLayout();
            this.pan_fechar.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_generico)).EndInit();
            this.grb_geral.ResumeLayout(false);
            this.grb_geral.PerformLayout();
            this.panel7.ResumeLayout(false);
            this.pan_botton.ResumeLayout(false);
            this.pan_completo.ResumeLayout(false);
            this.pan_tot.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pan_top;
        private System.Windows.Forms.Button btn_fechar;
        private System.Windows.Forms.DataGridView dgv_generico;
        private System.Windows.Forms.GroupBox grb_geral;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Panel pan_botton;
        private System.Windows.Forms.Button btn_editar;
        private System.Windows.Forms.Button btn_visualizar;
        private System.Windows.Forms.Button btn_limparFiltro;
        private System.Windows.Forms.Button btn_filtrar;
        private System.Windows.Forms.Panel pan_completo;
        private System.Windows.Forms.Panel pan_tot;
        private System.Windows.Forms.Button btn_excluir;
        private System.Windows.Forms.Button btn_incluir;
        private System.Windows.Forms.Label lbl_quantidadeLinhas;
        private System.Windows.Forms.Button btn_orderBy;
        private System.Windows.Forms.Panel pan_fechar;
        private System.Windows.Forms.Panel pan_opcoes;
        private System.Windows.Forms.MenuStrip mst_opcoes;
        private System.Windows.Forms.ToolStripMenuItem tsm_opcoes;
        private System.Windows.Forms.ToolStripMenuItem exportarCSVToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem salvarConsultaToolStripMenuItem;
        private System.Windows.Forms.Button btn_reload;
    }
}
