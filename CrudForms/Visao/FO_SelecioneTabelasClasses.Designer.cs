namespace Visao
{
    partial class FO_SelecioneTabelasClasses
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
            this.grb_configuracaoSQLSERVER = new System.Windows.Forms.GroupBox();
            this.btn_colocarTodos = new System.Windows.Forms.Button();
            this.btn_left = new System.Windows.Forms.Button();
            this.btn_rigth = new System.Windows.Forms.Button();
            this.dgv_tabela_out = new System.Windows.Forms.DataGridView();
            this.dgv_tabela_in = new System.Windows.Forms.DataGridView();
            this.pan_botton = new System.Windows.Forms.Panel();
            this.btn_gerar = new System.Windows.Forms.Button();
            this.pan_top = new System.Windows.Forms.Panel();
            this.tbx_message = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.pan_selectDirectory = new System.Windows.Forms.Panel();
            this.tbx_directory = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btn_path = new System.Windows.Forms.Button();
            this.grb_configuracaoSQLSERVER.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_tabela_out)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_tabela_in)).BeginInit();
            this.pan_botton.SuspendLayout();
            this.pan_top.SuspendLayout();
            this.pan_selectDirectory.SuspendLayout();
            this.SuspendLayout();
            // 
            // grb_configuracaoSQLSERVER
            // 
            this.grb_configuracaoSQLSERVER.Controls.Add(this.btn_colocarTodos);
            this.grb_configuracaoSQLSERVER.Controls.Add(this.btn_left);
            this.grb_configuracaoSQLSERVER.Controls.Add(this.btn_rigth);
            this.grb_configuracaoSQLSERVER.Controls.Add(this.dgv_tabela_out);
            this.grb_configuracaoSQLSERVER.Controls.Add(this.dgv_tabela_in);
            this.grb_configuracaoSQLSERVER.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grb_configuracaoSQLSERVER.Location = new System.Drawing.Point(0, 110);
            this.grb_configuracaoSQLSERVER.Name = "grb_configuracaoSQLSERVER";
            this.grb_configuracaoSQLSERVER.Size = new System.Drawing.Size(617, 288);
            this.grb_configuracaoSQLSERVER.TabIndex = 21;
            this.grb_configuracaoSQLSERVER.TabStop = false;
            this.grb_configuracaoSQLSERVER.Text = "Tabelas";
            // 
            // btn_colocarTodos
            // 
            this.btn_colocarTodos.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_colocarTodos.BackgroundImage = global::Pj.Properties.Resources.shuffle_variant_100px20x20;
            this.btn_colocarTodos.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_colocarTodos.Location = new System.Drawing.Point(282, 34);
            this.btn_colocarTodos.Name = "btn_colocarTodos";
            this.btn_colocarTodos.Size = new System.Drawing.Size(20, 20);
            this.btn_colocarTodos.TabIndex = 26;
            this.btn_colocarTodos.UseVisualStyleBackColor = true;
            this.btn_colocarTodos.Click += new System.EventHandler(this.btn_colocarTodos_Click);
            // 
            // btn_left
            // 
            this.btn_left.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_left.BackgroundImage = global::Pj.Properties.Resources.arrow_left_100px20x20;
            this.btn_left.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_left.Location = new System.Drawing.Point(282, 106);
            this.btn_left.Name = "btn_left";
            this.btn_left.Size = new System.Drawing.Size(20, 20);
            this.btn_left.TabIndex = 24;
            this.btn_left.UseVisualStyleBackColor = true;
            this.btn_left.Click += new System.EventHandler(this.btn_left_Click);
            // 
            // btn_rigth
            // 
            this.btn_rigth.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_rigth.BackgroundImage = global::Pj.Properties.Resources.arrow_right_100px20x20;
            this.btn_rigth.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_rigth.Location = new System.Drawing.Point(282, 70);
            this.btn_rigth.Name = "btn_rigth";
            this.btn_rigth.Size = new System.Drawing.Size(20, 20);
            this.btn_rigth.TabIndex = 25;
            this.btn_rigth.UseVisualStyleBackColor = true;
            this.btn_rigth.Click += new System.EventHandler(this.btn_rigth_Click);
            // 
            // dgv_tabela_out
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
            this.dgv_tabela_out.Location = new System.Drawing.Point(330, 19);
            this.dgv_tabela_out.MultiSelect = false;
            this.dgv_tabela_out.Name = "dgv_tabela_out";
            this.dgv_tabela_out.RowHeadersVisible = false;
            this.dgv_tabela_out.RowHeadersWidth = 51;
            this.dgv_tabela_out.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgv_tabela_out.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_tabela_out.ShowCellErrors = false;
            this.dgv_tabela_out.ShowCellToolTips = false;
            this.dgv_tabela_out.Size = new System.Drawing.Size(284, 266);
            this.dgv_tabela_out.StandardTab = true;
            this.dgv_tabela_out.TabIndex = 14;
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
            this.dgv_tabela_in.Location = new System.Drawing.Point(3, 19);
            this.dgv_tabela_in.MultiSelect = false;
            this.dgv_tabela_in.Name = "dgv_tabela_in";
            this.dgv_tabela_in.RowHeadersVisible = false;
            this.dgv_tabela_in.RowHeadersWidth = 51;
            this.dgv_tabela_in.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgv_tabela_in.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_tabela_in.ShowCellErrors = false;
            this.dgv_tabela_in.ShowCellToolTips = false;
            this.dgv_tabela_in.Size = new System.Drawing.Size(256, 266);
            this.dgv_tabela_in.StandardTab = true;
            this.dgv_tabela_in.TabIndex = 13;
            // 
            // pan_botton
            // 
            this.pan_botton.Controls.Add(this.btn_gerar);
            this.pan_botton.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pan_botton.Location = new System.Drawing.Point(0, 398);
            this.pan_botton.Name = "pan_botton";
            this.pan_botton.Size = new System.Drawing.Size(617, 35);
            this.pan_botton.TabIndex = 20;
            // 
            // btn_gerar
            // 
            this.btn_gerar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_gerar.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_gerar.Location = new System.Drawing.Point(508, 3);
            this.btn_gerar.Name = "btn_gerar";
            this.btn_gerar.Size = new System.Drawing.Size(106, 29);
            this.btn_gerar.TabIndex = 23;
            this.btn_gerar.Text = "Gerar Classes";
            this.btn_gerar.UseVisualStyleBackColor = true;
            this.btn_gerar.Click += new System.EventHandler(this.btn_gerar_Click);
            // 
            // pan_top
            // 
            this.pan_top.Controls.Add(this.tbx_message);
            this.pan_top.Controls.Add(this.label1);
            this.pan_top.Dock = System.Windows.Forms.DockStyle.Top;
            this.pan_top.Location = new System.Drawing.Point(0, 0);
            this.pan_top.Name = "pan_top";
            this.pan_top.Size = new System.Drawing.Size(617, 55);
            this.pan_top.TabIndex = 24;
            // 
            // tbx_message
            // 
            this.tbx_message.Location = new System.Drawing.Point(112, 9);
            this.tbx_message.Name = "tbx_message";
            this.tbx_message.Size = new System.Drawing.Size(493, 23);
            this.tbx_message.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(102, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Nome do projeto";
            // 
            // pan_selectDirectory
            // 
            this.pan_selectDirectory.Controls.Add(this.btn_path);
            this.pan_selectDirectory.Controls.Add(this.tbx_directory);
            this.pan_selectDirectory.Controls.Add(this.label2);
            this.pan_selectDirectory.Dock = System.Windows.Forms.DockStyle.Top;
            this.pan_selectDirectory.Location = new System.Drawing.Point(0, 55);
            this.pan_selectDirectory.Name = "pan_selectDirectory";
            this.pan_selectDirectory.Size = new System.Drawing.Size(617, 55);
            this.pan_selectDirectory.TabIndex = 25;
            // 
            // tbx_directory
            // 
            this.tbx_directory.Enabled = false;
            this.tbx_directory.Location = new System.Drawing.Point(128, 9);
            this.tbx_directory.Name = "tbx_directory";
            this.tbx_directory.Size = new System.Drawing.Size(401, 23);
            this.tbx_directory.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(119, 16);
            this.label2.TabIndex = 0;
            this.label2.Text = "Diretório do Projeto";
            // 
            // btn_path
            // 
            this.btn_path.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_path.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_path.Location = new System.Drawing.Point(535, 5);
            this.btn_path.Name = "btn_path";
            this.btn_path.Size = new System.Drawing.Size(70, 29);
            this.btn_path.TabIndex = 24;
            this.btn_path.Text = "Path";
            this.btn_path.UseVisualStyleBackColor = true;
            this.btn_path.Click += new System.EventHandler(this.btn_path_Click);
            // 
            // FO_SelecioneTabelasClasses
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(249)))), ((int)(((byte)(238)))));
            this.ClientSize = new System.Drawing.Size(617, 433);
            this.Controls.Add(this.grb_configuracaoSQLSERVER);
            this.Controls.Add(this.pan_botton);
            this.Controls.Add(this.pan_selectDirectory);
            this.Controls.Add(this.pan_top);
            this.Font = new System.Drawing.Font("Times New Roman", 10F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FO_SelecioneTabelasClasses";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Selecione as Tabelas para Geração das classes";
            this.grb_configuracaoSQLSERVER.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_tabela_out)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_tabela_in)).EndInit();
            this.pan_botton.ResumeLayout(false);
            this.pan_top.ResumeLayout(false);
            this.pan_top.PerformLayout();
            this.pan_selectDirectory.ResumeLayout(false);
            this.pan_selectDirectory.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grb_configuracaoSQLSERVER;
        private System.Windows.Forms.Button btn_left;
        private System.Windows.Forms.Button btn_rigth;
        private System.Windows.Forms.DataGridView dgv_tabela_out;
        private System.Windows.Forms.DataGridView dgv_tabela_in;
        private System.Windows.Forms.Panel pan_botton;
        private System.Windows.Forms.Button btn_gerar;
        private System.Windows.Forms.Panel pan_top;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbx_message;
        private System.Windows.Forms.Button btn_colocarTodos;
        private System.Windows.Forms.Panel pan_selectDirectory;
        private System.Windows.Forms.TextBox tbx_directory;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btn_path;
    }
}