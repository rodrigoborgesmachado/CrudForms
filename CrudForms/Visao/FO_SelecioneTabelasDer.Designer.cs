
namespace Visao
{
    partial class FO_SelecioneTabelasDer
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
            this.dgv_tabela_in = new System.Windows.Forms.DataGridView();
            this.pan_botton = new System.Windows.Forms.Panel();
            this.btn_gerar = new System.Windows.Forms.Button();
            this.btn_rigth = new System.Windows.Forms.Button();
            this.btn_left = new System.Windows.Forms.Button();
            this.grb_configuracaoSQLSERVER = new System.Windows.Forms.GroupBox();
            this.dgv_tabela_out = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_tabela_in)).BeginInit();
            this.pan_botton.SuspendLayout();
            this.grb_configuracaoSQLSERVER.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_tabela_out)).BeginInit();
            this.SuspendLayout();
            // 
            // dgv_tabela_in
            // 
            this.dgv_tabela_in.AllowUserToAddRows = false;
            this.dgv_tabela_in.AllowUserToDeleteRows = false;
            this.dgv_tabela_in.AllowUserToResizeColumns = false;
            this.dgv_tabela_in.AllowUserToResizeRows = false;
            this.dgv_tabela_in.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv_tabela_in.BackgroundColor = System.Drawing.Color.White;
            this.dgv_tabela_in.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgv_tabela_in.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;
            this.dgv_tabela_in.ColumnHeadersHeight = 29;
            this.dgv_tabela_in.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgv_tabela_in.Dock = System.Windows.Forms.DockStyle.Left;
            this.dgv_tabela_in.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgv_tabela_in.EnableHeadersVisualStyles = false;
            this.dgv_tabela_in.Location = new System.Drawing.Point(3, 23);
            this.dgv_tabela_in.MultiSelect = false;
            this.dgv_tabela_in.Name = "dgv_tabela_in";
            this.dgv_tabela_in.RowHeadersVisible = false;
            this.dgv_tabela_in.RowHeadersWidth = 51;
            this.dgv_tabela_in.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgv_tabela_in.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_tabela_in.ShowCellErrors = false;
            this.dgv_tabela_in.ShowCellToolTips = false;
            this.dgv_tabela_in.Size = new System.Drawing.Size(256, 293);
            this.dgv_tabela_in.StandardTab = true;
            this.dgv_tabela_in.TabIndex = 13;
            // 
            // pan_botton
            // 
            this.pan_botton.Controls.Add(this.btn_gerar);
            this.pan_botton.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pan_botton.Location = new System.Drawing.Point(0, 319);
            this.pan_botton.Name = "pan_botton";
            this.pan_botton.Size = new System.Drawing.Size(617, 35);
            this.pan_botton.TabIndex = 18;
            // 
            // btn_gerar
            // 
            this.btn_gerar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_gerar.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_gerar.Location = new System.Drawing.Point(531, 3);
            this.btn_gerar.Name = "btn_gerar";
            this.btn_gerar.Size = new System.Drawing.Size(83, 29);
            this.btn_gerar.TabIndex = 23;
            this.btn_gerar.Text = "Gerar";
            this.btn_gerar.UseVisualStyleBackColor = true;
            this.btn_gerar.Click += new System.EventHandler(this.btn_gerar_Click);
            // 
            // btn_rigth
            // 
            this.btn_rigth.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_rigth.BackgroundImage = global::Pj.Properties.Resources.arrow_right_100px20x20;
            this.btn_rigth.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_rigth.Location = new System.Drawing.Point(282, 101);
            this.btn_rigth.Name = "btn_rigth";
            this.btn_rigth.Size = new System.Drawing.Size(20, 20);
            this.btn_rigth.TabIndex = 25;
            this.btn_rigth.UseVisualStyleBackColor = true;
            this.btn_rigth.Click += new System.EventHandler(this.btn_rigth_Click);
            // 
            // btn_left
            // 
            this.btn_left.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_left.BackgroundImage = global::Pj.Properties.Resources.arrow_left_100px20x20;
            this.btn_left.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_left.Location = new System.Drawing.Point(282, 137);
            this.btn_left.Name = "btn_left";
            this.btn_left.Size = new System.Drawing.Size(20, 20);
            this.btn_left.TabIndex = 24;
            this.btn_left.UseVisualStyleBackColor = true;
            this.btn_left.Click += new System.EventHandler(this.btn_left_Click);
            // 
            // grb_configuracaoSQLSERVER
            // 
            this.grb_configuracaoSQLSERVER.Controls.Add(this.btn_left);
            this.grb_configuracaoSQLSERVER.Controls.Add(this.btn_rigth);
            this.grb_configuracaoSQLSERVER.Controls.Add(this.dgv_tabela_out);
            this.grb_configuracaoSQLSERVER.Controls.Add(this.dgv_tabela_in);
            this.grb_configuracaoSQLSERVER.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grb_configuracaoSQLSERVER.Location = new System.Drawing.Point(0, 0);
            this.grb_configuracaoSQLSERVER.Name = "grb_configuracaoSQLSERVER";
            this.grb_configuracaoSQLSERVER.Size = new System.Drawing.Size(617, 319);
            this.grb_configuracaoSQLSERVER.TabIndex = 19;
            this.grb_configuracaoSQLSERVER.TabStop = false;
            this.grb_configuracaoSQLSERVER.Text = "Tabelas";
            // 
            // dataGridView1
            // 
            this.dgv_tabela_out.AllowUserToAddRows = false;
            this.dgv_tabela_out.AllowUserToDeleteRows = false;
            this.dgv_tabela_out.AllowUserToResizeColumns = false;
            this.dgv_tabela_out.AllowUserToResizeRows = false;
            this.dgv_tabela_out.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv_tabela_out.BackgroundColor = System.Drawing.Color.White;
            this.dgv_tabela_out.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgv_tabela_out.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;
            this.dgv_tabela_out.ColumnHeadersHeight = 29;
            this.dgv_tabela_out.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgv_tabela_out.Dock = System.Windows.Forms.DockStyle.Right;
            this.dgv_tabela_out.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgv_tabela_out.EnableHeadersVisualStyles = false;
            this.dgv_tabela_out.Location = new System.Drawing.Point(330, 23);
            this.dgv_tabela_out.MultiSelect = false;
            this.dgv_tabela_out.Name = "dataGridView1";
            this.dgv_tabela_out.RowHeadersVisible = false;
            this.dgv_tabela_out.RowHeadersWidth = 51;
            this.dgv_tabela_out.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgv_tabela_out.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_tabela_out.ShowCellErrors = false;
            this.dgv_tabela_out.ShowCellToolTips = false;
            this.dgv_tabela_out.Size = new System.Drawing.Size(284, 293);
            this.dgv_tabela_out.StandardTab = true;
            this.dgv_tabela_out.TabIndex = 14;
            // 
            // FO_SelecioneTabelasDer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(249)))), ((int)(((byte)(238)))));
            this.ClientSize = new System.Drawing.Size(617, 354);
            this.Controls.Add(this.grb_configuracaoSQLSERVER);
            this.Controls.Add(this.pan_botton);
            this.Font = new System.Drawing.Font("Times New Roman", 10F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FO_SelecioneTabelasDer";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Selecione as Tabelas para Geração do Der";
            ((System.ComponentModel.ISupportInitialize)(this.dgv_tabela_in)).EndInit();
            this.pan_botton.ResumeLayout(false);
            this.grb_configuracaoSQLSERVER.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_tabela_out)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgv_tabela_in;
        private System.Windows.Forms.Panel pan_botton;
        private System.Windows.Forms.Button btn_rigth;
        private System.Windows.Forms.Button btn_left;
        private System.Windows.Forms.Button btn_gerar;
        private System.Windows.Forms.GroupBox grb_configuracaoSQLSERVER;
        private System.Windows.Forms.DataGridView dgv_tabela_out;
    }
}