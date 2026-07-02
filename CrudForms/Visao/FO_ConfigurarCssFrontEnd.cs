using Regras.FrontEndClasses;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Visao
{
    public class FO_ConfigurarCssFrontEnd : Form
    {
        private readonly List<Model.MD_FrontEndCssVariable> variaveis;
        private readonly DataGridView dgv_variaveis = new DataGridView();
        private readonly Button btn_confirmar = new Button();
        private readonly Button btn_restaurar = new Button();
        private readonly Panel pan_botton = new Panel();

        public FO_ConfigurarCssFrontEnd(List<Model.MD_FrontEndCssVariable> variaveis)
        {
            this.variaveis = variaveis ?? AssetsCreator.GetDefaultCssVariables();
            InitializeComponent();
            IniciaForm();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();

            dgv_variaveis.AllowUserToAddRows = false;
            dgv_variaveis.AllowUserToDeleteRows = false;
            dgv_variaveis.AllowUserToResizeRows = false;
            dgv_variaveis.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv_variaveis.BackgroundColor = Color.White;
            dgv_variaveis.BorderStyle = BorderStyle.Fixed3D;
            dgv_variaveis.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgv_variaveis.Dock = DockStyle.Fill;
            dgv_variaveis.EditMode = DataGridViewEditMode.EditOnEnter;
            dgv_variaveis.EnableHeadersVisualStyles = false;
            dgv_variaveis.MultiSelect = false;
            dgv_variaveis.RowHeadersVisible = false;
            dgv_variaveis.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv_variaveis.ShowCellErrors = false;
            dgv_variaveis.ShowCellToolTips = false;
            dgv_variaveis.StandardTab = true;

            btn_restaurar.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btn_restaurar.FlatStyle = FlatStyle.Popup;
            btn_restaurar.Location = new Point(3, 3);
            btn_restaurar.Name = "btn_restaurar";
            btn_restaurar.Size = new Size(130, 29);
            btn_restaurar.Text = "Restaurar default";
            btn_restaurar.UseVisualStyleBackColor = true;
            btn_restaurar.Click += btn_restaurar_Click;

            btn_confirmar.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btn_confirmar.FlatStyle = FlatStyle.Popup;
            btn_confirmar.Location = new Point(688, 3);
            btn_confirmar.Name = "btn_confirmar";
            btn_confirmar.Size = new Size(89, 29);
            btn_confirmar.Text = "Confirmar";
            btn_confirmar.UseVisualStyleBackColor = true;
            btn_confirmar.Click += btn_confirmar_Click;

            pan_botton.Controls.Add(btn_restaurar);
            pan_botton.Controls.Add(btn_confirmar);
            pan_botton.Dock = DockStyle.Bottom;
            pan_botton.Location = new Point(0, 425);
            pan_botton.Name = "pan_botton";
            pan_botton.Size = new Size(780, 35);

            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(780, 460);
            this.Controls.Add(dgv_variaveis);
            this.Controls.Add(pan_botton);
            this.Font = new Font("Times New Roman", 10F);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FO_ConfigurarCssFrontEnd";
            this.ShowIcon = false;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Configuracao do CSS";

            this.ResumeLayout(false);
        }

        private void IniciaForm()
        {
            if (Model.Parametros.ModoDark)
            {
                this.BackColor = Color.FromArgb(51, 51, 51);
                this.ForeColor = Color.White;
                dgv_variaveis.GridColor = this.ForeColor;
                dgv_variaveis.BackgroundColor = this.BackColor;
                dgv_variaveis.DefaultCellStyle.BackColor = this.BackColor;
                dgv_variaveis.DefaultCellStyle.ForeColor = this.ForeColor;
                dgv_variaveis.RowHeadersDefaultCellStyle.BackColor = this.BackColor;
                dgv_variaveis.RowHeadersDefaultCellStyle.ForeColor = this.ForeColor;
                dgv_variaveis.ColumnHeadersDefaultCellStyle.BackColor = this.BackColor;
                dgv_variaveis.ColumnHeadersDefaultCellStyle.ForeColor = this.ForeColor;
            }
            else
            {
                this.BackColor = Color.FromArgb(251, 249, 238);
                this.ForeColor = Color.Black;
            }

            foreach (Button button in pan_botton.Controls.OfType<Button>())
            {
                button.BackColor = this.BackColor;
                button.ForeColor = this.ForeColor;
            }

            FillGrid();
        }

        private void FillGrid()
        {
            dgv_variaveis.Rows.Clear();
            dgv_variaveis.Columns.Clear();

            dgv_variaveis.Columns.Add("Grupo", "Grupo");
            dgv_variaveis.Columns.Add("Nome", "Variavel");
            dgv_variaveis.Columns.Add("Valor", "Valor");
            dgv_variaveis.Columns["Grupo"].ReadOnly = true;
            dgv_variaveis.Columns["Nome"].ReadOnly = true;

            foreach (var variavel in variaveis)
            {
                int rowIndex = dgv_variaveis.Rows.Add(variavel.Grupo, variavel.Nome, variavel.Valor);
                dgv_variaveis.Rows[rowIndex].Tag = variavel;
            }

            for (int i = 0; i < dgv_variaveis.Columns.Count; i++)
            {
                dgv_variaveis.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dgv_variaveis.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        private void btn_restaurar_Click(object sender, EventArgs e)
        {
            variaveis.Clear();
            variaveis.AddRange(AssetsCreator.GetDefaultCssVariables());
            FillGrid();
        }

        private void btn_confirmar_Click(object sender, EventArgs e)
        {
            SaveGridValues();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void SaveGridValues()
        {
            dgv_variaveis.EndEdit();

            foreach (DataGridViewRow row in dgv_variaveis.Rows)
            {
                var variavel = row.Tag as Model.MD_FrontEndCssVariable;
                if (variavel == null)
                {
                    continue;
                }

                string valor = row.Cells["Valor"].Value?.ToString();
                if (!string.IsNullOrWhiteSpace(valor))
                {
                    variavel.Valor = valor.Trim();
                }
            }
        }
    }
}
