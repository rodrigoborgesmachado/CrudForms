
namespace Visao
{
    partial class FO_Observer
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
            this.btn_inativar = new System.Windows.Forms.Button();
            this.btn_excluir = new System.Windows.Forms.Button();
            this.btn_editar = new System.Windows.Forms.Button();
            this.btn_adicionar = new System.Windows.Forms.Button();
            this.grb_configuracaoSQLSERVER = new System.Windows.Forms.GroupBox();
            this.dgv_observers = new System.Windows.Forms.DataGridView();
            this.pan_botton.SuspendLayout();
            this.grb_configuracaoSQLSERVER.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_observers)).BeginInit();
            this.SuspendLayout();
            // 
            // pan_botton
            // 
            this.pan_botton.Controls.Add(this.btn_inativar);
            this.pan_botton.Controls.Add(this.btn_excluir);
            this.pan_botton.Controls.Add(this.btn_editar);
            this.pan_botton.Controls.Add(this.btn_adicionar);
            this.pan_botton.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pan_botton.Location = new System.Drawing.Point(0, 303);
            this.pan_botton.Name = "pan_botton";
            this.pan_botton.Size = new System.Drawing.Size(792, 35);
            this.pan_botton.TabIndex = 16;
            // 
            // btn_inativar
            // 
            this.btn_inativar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btn_inativar.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_inativar.Location = new System.Drawing.Point(6, 3);
            this.btn_inativar.Name = "btn_inativar";
            this.btn_inativar.Size = new System.Drawing.Size(83, 29);
            this.btn_inativar.TabIndex = 26;
            this.btn_inativar.Text = "Desativar";
            this.btn_inativar.UseVisualStyleBackColor = true;
            this.btn_inativar.Click += new System.EventHandler(this.btn_inativar_Click);
            // 
            // btn_excluir
            // 
            this.btn_excluir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_excluir.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_excluir.Location = new System.Drawing.Point(528, 3);
            this.btn_excluir.Name = "btn_excluir";
            this.btn_excluir.Size = new System.Drawing.Size(83, 29);
            this.btn_excluir.TabIndex = 25;
            this.btn_excluir.Text = "Excluir";
            this.btn_excluir.UseVisualStyleBackColor = true;
            this.btn_excluir.Click += new System.EventHandler(this.btn_excluir_Click);
            // 
            // btn_editar
            // 
            this.btn_editar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_editar.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_editar.Location = new System.Drawing.Point(617, 3);
            this.btn_editar.Name = "btn_editar";
            this.btn_editar.Size = new System.Drawing.Size(83, 29);
            this.btn_editar.TabIndex = 24;
            this.btn_editar.Text = "Editar";
            this.btn_editar.UseVisualStyleBackColor = true;
            this.btn_editar.Click += new System.EventHandler(this.btn_editar_Click);
            // 
            // btn_adicionar
            // 
            this.btn_adicionar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_adicionar.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_adicionar.Location = new System.Drawing.Point(706, 3);
            this.btn_adicionar.Name = "btn_adicionar";
            this.btn_adicionar.Size = new System.Drawing.Size(83, 29);
            this.btn_adicionar.TabIndex = 23;
            this.btn_adicionar.Text = "Adicionar";
            this.btn_adicionar.UseVisualStyleBackColor = true;
            this.btn_adicionar.Click += new System.EventHandler(this.btn_adicionar_Click);
            // 
            // grb_configuracaoSQLSERVER
            // 
            this.grb_configuracaoSQLSERVER.Controls.Add(this.dgv_observers);
            this.grb_configuracaoSQLSERVER.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grb_configuracaoSQLSERVER.Location = new System.Drawing.Point(0, 0);
            this.grb_configuracaoSQLSERVER.Name = "grb_configuracaoSQLSERVER";
            this.grb_configuracaoSQLSERVER.Size = new System.Drawing.Size(792, 338);
            this.grb_configuracaoSQLSERVER.TabIndex = 17;
            this.grb_configuracaoSQLSERVER.TabStop = false;
            this.grb_configuracaoSQLSERVER.Text = "Observers";
            // 
            // dgv_observers
            // 
            this.dgv_observers.AllowUserToAddRows = false;
            this.dgv_observers.AllowUserToDeleteRows = false;
            this.dgv_observers.AllowUserToResizeColumns = false;
            this.dgv_observers.AllowUserToResizeRows = false;
            this.dgv_observers.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv_observers.BackgroundColor = System.Drawing.Color.White;
            this.dgv_observers.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgv_observers.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;
            this.dgv_observers.ColumnHeadersHeight = 29;
            this.dgv_observers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgv_observers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_observers.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgv_observers.EnableHeadersVisualStyles = false;
            this.dgv_observers.Location = new System.Drawing.Point(3, 23);
            this.dgv_observers.MultiSelect = false;
            this.dgv_observers.Name = "dgv_observers";
            this.dgv_observers.RowHeadersVisible = false;
            this.dgv_observers.RowHeadersWidth = 51;
            this.dgv_observers.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgv_observers.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_observers.ShowCellErrors = false;
            this.dgv_observers.ShowCellToolTips = false;
            this.dgv_observers.Size = new System.Drawing.Size(786, 312);
            this.dgv_observers.StandardTab = true;
            this.dgv_observers.TabIndex = 13;
            this.dgv_observers.SelectionChanged += new System.EventHandler(this.dgv_observers_SelectionChanged);
            // 
            // FO_Observer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(249)))), ((int)(((byte)(238)))));
            this.ClientSize = new System.Drawing.Size(792, 338);
            this.Controls.Add(this.pan_botton);
            this.Controls.Add(this.grb_configuracaoSQLSERVER);
            this.Font = new System.Drawing.Font("Times New Roman", 10F);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FO_Observer";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Controle de execuções automáticas";
            this.pan_botton.ResumeLayout(false);
            this.grb_configuracaoSQLSERVER.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_observers)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pan_botton;
        private System.Windows.Forms.Button btn_excluir;
        private System.Windows.Forms.Button btn_editar;
        private System.Windows.Forms.Button btn_adicionar;
        private System.Windows.Forms.GroupBox grb_configuracaoSQLSERVER;
        private System.Windows.Forms.DataGridView dgv_observers;
        private System.Windows.Forms.Button btn_inativar;
    }
}