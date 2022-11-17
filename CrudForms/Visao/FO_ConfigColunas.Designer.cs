
namespace Visao
{
    partial class FO_ConfigColunas
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
            this.pan_completo = new System.Windows.Forms.Panel();
            this.grb_configuracaoSQLSERVER = new System.Windows.Forms.GroupBox();
            this.pan_grid = new System.Windows.Forms.Panel();
            this.dgv_colunas = new System.Windows.Forms.DataGridView();
            this.pan_opcoes = new System.Windows.Forms.Panel();
            this.btn_visible = new System.Windows.Forms.Button();
            this.btn_down = new System.Windows.Forms.Button();
            this.btn_up = new System.Windows.Forms.Button();
            this.pan_botton = new System.Windows.Forms.Panel();
            this.btn_confirmar = new System.Windows.Forms.Button();
            this.pan_completo.SuspendLayout();
            this.grb_configuracaoSQLSERVER.SuspendLayout();
            this.pan_grid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_colunas)).BeginInit();
            this.pan_opcoes.SuspendLayout();
            this.pan_botton.SuspendLayout();
            this.SuspendLayout();
            // 
            // pan_completo
            // 
            this.pan_completo.Controls.Add(this.grb_configuracaoSQLSERVER);
            this.pan_completo.Controls.Add(this.pan_botton);
            this.pan_completo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pan_completo.Location = new System.Drawing.Point(0, 0);
            this.pan_completo.Name = "pan_completo";
            this.pan_completo.Size = new System.Drawing.Size(464, 403);
            this.pan_completo.TabIndex = 0;
            // 
            // grb_configuracaoSQLSERVER
            // 
            this.grb_configuracaoSQLSERVER.Controls.Add(this.pan_grid);
            this.grb_configuracaoSQLSERVER.Controls.Add(this.pan_opcoes);
            this.grb_configuracaoSQLSERVER.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grb_configuracaoSQLSERVER.Location = new System.Drawing.Point(0, 0);
            this.grb_configuracaoSQLSERVER.Name = "grb_configuracaoSQLSERVER";
            this.grb_configuracaoSQLSERVER.Size = new System.Drawing.Size(464, 368);
            this.grb_configuracaoSQLSERVER.TabIndex = 15;
            this.grb_configuracaoSQLSERVER.TabStop = false;
            this.grb_configuracaoSQLSERVER.Text = "Colunas";
            // 
            // pan_grid
            // 
            this.pan_grid.Controls.Add(this.dgv_colunas);
            this.pan_grid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pan_grid.Location = new System.Drawing.Point(3, 23);
            this.pan_grid.Name = "pan_grid";
            this.pan_grid.Size = new System.Drawing.Size(426, 342);
            this.pan_grid.TabIndex = 3;
            // 
            // dgv_colunas
            // 
            this.dgv_colunas.AllowUserToAddRows = false;
            this.dgv_colunas.AllowUserToDeleteRows = false;
            this.dgv_colunas.AllowUserToResizeColumns = false;
            this.dgv_colunas.AllowUserToResizeRows = false;
            this.dgv_colunas.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv_colunas.BackgroundColor = System.Drawing.Color.White;
            this.dgv_colunas.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgv_colunas.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;
            this.dgv_colunas.ColumnHeadersHeight = 29;
            this.dgv_colunas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgv_colunas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_colunas.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgv_colunas.EnableHeadersVisualStyles = false;
            this.dgv_colunas.Location = new System.Drawing.Point(0, 0);
            this.dgv_colunas.MultiSelect = false;
            this.dgv_colunas.Name = "dgv_colunas";
            this.dgv_colunas.RowHeadersVisible = false;
            this.dgv_colunas.RowHeadersWidth = 51;
            this.dgv_colunas.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgv_colunas.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_colunas.ShowCellErrors = false;
            this.dgv_colunas.ShowCellToolTips = false;
            this.dgv_colunas.Size = new System.Drawing.Size(426, 342);
            this.dgv_colunas.StandardTab = true;
            this.dgv_colunas.TabIndex = 13;
            // 
            // pan_opcoes
            // 
            this.pan_opcoes.Controls.Add(this.btn_visible);
            this.pan_opcoes.Controls.Add(this.btn_down);
            this.pan_opcoes.Controls.Add(this.btn_up);
            this.pan_opcoes.Dock = System.Windows.Forms.DockStyle.Right;
            this.pan_opcoes.Location = new System.Drawing.Point(429, 23);
            this.pan_opcoes.Name = "pan_opcoes";
            this.pan_opcoes.Size = new System.Drawing.Size(32, 342);
            this.pan_opcoes.TabIndex = 2;
            // 
            // btn_visible
            // 
            this.btn_visible.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_visible.BackgroundImage = global::Pj.Properties.Resources.eye_100px20x20;
            this.btn_visible.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_visible.Location = new System.Drawing.Point(6, 55);
            this.btn_visible.Name = "btn_visible";
            this.btn_visible.Size = new System.Drawing.Size(20, 20);
            this.btn_visible.TabIndex = 3;
            this.btn_visible.UseVisualStyleBackColor = true;
            this.btn_visible.Click += new System.EventHandler(this.btn_visible_Click);
            // 
            // btn_down
            // 
            this.btn_down.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_down.BackgroundImage = global::Pj.Properties.Resources.arrow_down_100px20x20;
            this.btn_down.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_down.Location = new System.Drawing.Point(6, 29);
            this.btn_down.Name = "btn_down";
            this.btn_down.Size = new System.Drawing.Size(20, 20);
            this.btn_down.TabIndex = 2;
            this.btn_down.UseVisualStyleBackColor = true;
            this.btn_down.Click += new System.EventHandler(this.btn_down_Click);
            // 
            // btn_up
            // 
            this.btn_up.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_up.BackgroundImage = global::Pj.Properties.Resources.arrow_up_100px20x20;
            this.btn_up.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_up.Location = new System.Drawing.Point(6, 3);
            this.btn_up.Name = "btn_up";
            this.btn_up.Size = new System.Drawing.Size(20, 20);
            this.btn_up.TabIndex = 1;
            this.btn_up.UseVisualStyleBackColor = true;
            this.btn_up.Click += new System.EventHandler(this.btn_up_Click);
            // 
            // pan_botton
            // 
            this.pan_botton.Controls.Add(this.btn_confirmar);
            this.pan_botton.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pan_botton.Location = new System.Drawing.Point(0, 368);
            this.pan_botton.Name = "pan_botton";
            this.pan_botton.Size = new System.Drawing.Size(464, 35);
            this.pan_botton.TabIndex = 14;
            // 
            // btn_confirmar
            // 
            this.btn_confirmar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_confirmar.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_confirmar.Location = new System.Drawing.Point(378, 3);
            this.btn_confirmar.Name = "btn_confirmar";
            this.btn_confirmar.Size = new System.Drawing.Size(83, 29);
            this.btn_confirmar.TabIndex = 4;
            this.btn_confirmar.Text = "Confirmar";
            this.btn_confirmar.UseVisualStyleBackColor = true;
            this.btn_confirmar.Click += new System.EventHandler(this.btn_confirmar_Click);
            // 
            // FO_ConfigColunas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(249)))), ((int)(((byte)(238)))));
            this.ClientSize = new System.Drawing.Size(464, 403);
            this.Controls.Add(this.pan_completo);
            this.Font = new System.Drawing.Font("Times New Roman", 10F);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FO_ConfigColunas";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Configuração das colunas";
            this.pan_completo.ResumeLayout(false);
            this.grb_configuracaoSQLSERVER.ResumeLayout(false);
            this.pan_grid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_colunas)).EndInit();
            this.pan_opcoes.ResumeLayout(false);
            this.pan_botton.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pan_completo;
        private System.Windows.Forms.Panel pan_botton;
        private System.Windows.Forms.Button btn_confirmar;
        private System.Windows.Forms.GroupBox grb_configuracaoSQLSERVER;
        private System.Windows.Forms.Button btn_up;
        private System.Windows.Forms.Panel pan_grid;
        private System.Windows.Forms.Panel pan_opcoes;
        private System.Windows.Forms.Button btn_down;
        private System.Windows.Forms.DataGridView dgv_colunas;
        private System.Windows.Forms.Button btn_visible;
    }
}