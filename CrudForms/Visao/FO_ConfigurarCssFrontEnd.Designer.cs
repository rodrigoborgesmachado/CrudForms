namespace Visao
{
    partial class FO_ConfigurarCssFrontEnd
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
            this.dgv_variaveis = new System.Windows.Forms.DataGridView();
            this.pan_botton = new System.Windows.Forms.Panel();
            this.btn_restaurar = new System.Windows.Forms.Button();
            this.btn_confirmar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_variaveis)).BeginInit();
            this.pan_botton.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgv_variaveis
            // 
            this.dgv_variaveis.AllowUserToAddRows = false;
            this.dgv_variaveis.AllowUserToDeleteRows = false;
            this.dgv_variaveis.AllowUserToResizeRows = false;
            this.dgv_variaveis.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv_variaveis.BackgroundColor = System.Drawing.Color.White;
            this.dgv_variaveis.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgv_variaveis.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgv_variaveis.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_variaveis.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgv_variaveis.EnableHeadersVisualStyles = false;
            this.dgv_variaveis.Location = new System.Drawing.Point(0, 0);
            this.dgv_variaveis.MultiSelect = false;
            this.dgv_variaveis.Name = "dgv_variaveis";
            this.dgv_variaveis.RowHeadersVisible = false;
            this.dgv_variaveis.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_variaveis.ShowCellErrors = false;
            this.dgv_variaveis.ShowCellToolTips = false;
            this.dgv_variaveis.Size = new System.Drawing.Size(780, 425);
            this.dgv_variaveis.StandardTab = true;
            this.dgv_variaveis.TabIndex = 0;
            // 
            // pan_botton
            // 
            this.pan_botton.Controls.Add(this.btn_restaurar);
            this.pan_botton.Controls.Add(this.btn_confirmar);
            this.pan_botton.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pan_botton.Location = new System.Drawing.Point(0, 425);
            this.pan_botton.Name = "pan_botton";
            this.pan_botton.Size = new System.Drawing.Size(780, 35);
            this.pan_botton.TabIndex = 1;
            // 
            // btn_restaurar
            // 
            this.btn_restaurar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btn_restaurar.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_restaurar.Location = new System.Drawing.Point(3, 3);
            this.btn_restaurar.Name = "btn_restaurar";
            this.btn_restaurar.Size = new System.Drawing.Size(130, 29);
            this.btn_restaurar.TabIndex = 0;
            this.btn_restaurar.Text = "Restaurar default";
            this.btn_restaurar.UseVisualStyleBackColor = true;
            // 
            // btn_confirmar
            // 
            this.btn_confirmar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_confirmar.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_confirmar.Location = new System.Drawing.Point(688, 3);
            this.btn_confirmar.Name = "btn_confirmar";
            this.btn_confirmar.Size = new System.Drawing.Size(89, 29);
            this.btn_confirmar.TabIndex = 1;
            this.btn_confirmar.Text = "Confirmar";
            this.btn_confirmar.UseVisualStyleBackColor = true;
            // 
            // FO_ConfigurarCssFrontEnd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(249)))), ((int)(((byte)(238)))));
            this.ClientSize = new System.Drawing.Size(780, 460);
            this.Controls.Add(this.dgv_variaveis);
            this.Controls.Add(this.pan_botton);
            this.Font = new System.Drawing.Font("Times New Roman", 10F);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FO_ConfigurarCssFrontEnd";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Configuracao do tema do front end";
            ((System.ComponentModel.ISupportInitialize)(this.dgv_variaveis)).EndInit();
            this.pan_botton.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgv_variaveis;
        private System.Windows.Forms.Panel pan_botton;
        private System.Windows.Forms.Button btn_restaurar;
        private System.Windows.Forms.Button btn_confirmar;
    }
}
