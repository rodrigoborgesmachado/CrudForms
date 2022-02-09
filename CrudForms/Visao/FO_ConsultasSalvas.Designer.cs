
namespace Visao
{
    partial class FO_ConsultasSalvas
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
            this.btn_executar = new System.Windows.Forms.Button();
            this.grb_configuracaoSQLSERVER = new System.Windows.Forms.GroupBox();
            this.btn_editar = new System.Windows.Forms.Button();
            this.dgv_generico = new System.Windows.Forms.DataGridView();
            this.pan_botton.SuspendLayout();
            this.grb_configuracaoSQLSERVER.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_generico)).BeginInit();
            this.SuspendLayout();
            // 
            // pan_botton
            // 
            this.pan_botton.Controls.Add(this.btn_editar);
            this.pan_botton.Controls.Add(this.btn_executar);
            this.pan_botton.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pan_botton.Location = new System.Drawing.Point(0, 425);
            this.pan_botton.Name = "pan_botton";
            this.pan_botton.Size = new System.Drawing.Size(744, 35);
            this.pan_botton.TabIndex = 14;
            // 
            // btn_executar
            // 
            this.btn_executar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_executar.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_executar.Location = new System.Drawing.Point(658, 3);
            this.btn_executar.Name = "btn_executar";
            this.btn_executar.Size = new System.Drawing.Size(83, 29);
            this.btn_executar.TabIndex = 23;
            this.btn_executar.Text = "Executar";
            this.btn_executar.UseVisualStyleBackColor = true;
            this.btn_executar.Click += new System.EventHandler(this.btn_executar_Click);
            // 
            // grb_configuracaoSQLSERVER
            // 
            this.grb_configuracaoSQLSERVER.Controls.Add(this.dgv_generico);
            this.grb_configuracaoSQLSERVER.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grb_configuracaoSQLSERVER.Location = new System.Drawing.Point(0, 0);
            this.grb_configuracaoSQLSERVER.Name = "grb_configuracaoSQLSERVER";
            this.grb_configuracaoSQLSERVER.Size = new System.Drawing.Size(744, 460);
            this.grb_configuracaoSQLSERVER.TabIndex = 15;
            this.grb_configuracaoSQLSERVER.TabStop = false;
            this.grb_configuracaoSQLSERVER.Text = "Consultas";
            // 
            // btn_editar
            // 
            this.btn_editar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_editar.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_editar.Location = new System.Drawing.Point(569, 3);
            this.btn_editar.Name = "btn_editar";
            this.btn_editar.Size = new System.Drawing.Size(83, 29);
            this.btn_editar.TabIndex = 24;
            this.btn_editar.Text = "Editar";
            this.btn_editar.UseVisualStyleBackColor = true;
            this.btn_editar.Click += new System.EventHandler(this.btn_editar_Click);
            // 
            // dgv_generico
            // 
            this.dgv_generico.AllowUserToAddRows = false;
            this.dgv_generico.AllowUserToDeleteRows = false;
            this.dgv_generico.AllowUserToResizeColumns = false;
            this.dgv_generico.AllowUserToResizeRows = false;
            this.dgv_generico.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv_generico.BackgroundColor = System.Drawing.Color.White;
            this.dgv_generico.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgv_generico.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;
            this.dgv_generico.ColumnHeadersHeight = 29;
            this.dgv_generico.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgv_generico.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_generico.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgv_generico.EnableHeadersVisualStyles = false;
            this.dgv_generico.Location = new System.Drawing.Point(3, 23);
            this.dgv_generico.MultiSelect = false;
            this.dgv_generico.Name = "dgv_generico";
            this.dgv_generico.RowHeadersVisible = false;
            this.dgv_generico.RowHeadersWidth = 51;
            this.dgv_generico.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgv_generico.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_generico.ShowCellErrors = false;
            this.dgv_generico.ShowCellToolTips = false;
            this.dgv_generico.Size = new System.Drawing.Size(738, 434);
            this.dgv_generico.StandardTab = true;
            this.dgv_generico.TabIndex = 13;
            // 
            // FO_ConsultasSalvas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(249)))), ((int)(((byte)(238)))));
            this.ClientSize = new System.Drawing.Size(744, 460);
            this.Controls.Add(this.pan_botton);
            this.Controls.Add(this.grb_configuracaoSQLSERVER);
            this.Font = new System.Drawing.Font("Times New Roman", 10F);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FO_ConsultasSalvas";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Consultas Salvas";
            this.pan_botton.ResumeLayout(false);
            this.grb_configuracaoSQLSERVER.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_generico)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pan_botton;
        private System.Windows.Forms.Button btn_executar;
        private System.Windows.Forms.GroupBox grb_configuracaoSQLSERVER;
        private System.Windows.Forms.Button btn_editar;
        private System.Windows.Forms.DataGridView dgv_generico;
    }
}