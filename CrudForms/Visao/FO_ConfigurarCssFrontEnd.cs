using Regras.FrontEndClasses;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Visao
{
    public partial class FO_ConfigurarCssFrontEnd : Form
    {
        private readonly List<Model.MD_FrontEndCssVariable> variaveis;

        public FO_ConfigurarCssFrontEnd()
            : this(AssetsCreator.GetDefaultCssVariables())
        {
        }

        public FO_ConfigurarCssFrontEnd(List<Model.MD_FrontEndCssVariable> variaveis)
        {
            this.variaveis = variaveis ?? AssetsCreator.GetDefaultCssVariables();
            InitializeComponent();
            IniciaForm();
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

            btn_restaurar.Click += btn_restaurar_Click;
            btn_confirmar.Click += btn_confirmar_Click;
            dgv_variaveis.CellContentClick += dgv_variaveis_CellContentClick;
            dgv_variaveis.CellEndEdit += dgv_variaveis_CellEndEdit;

            FillGrid();
        }

        private void FillGrid()
        {
            dgv_variaveis.Rows.Clear();
            dgv_variaveis.Columns.Clear();

            dgv_variaveis.Columns.Add("Grupo", "Grupo");
            dgv_variaveis.Columns.Add("Nome", "Variavel");
            dgv_variaveis.Columns.Add("Valor", "Valor");
            dgv_variaveis.Columns.Add("Preview", "Cor");
            dgv_variaveis.Columns.Add(CreateButtonColumn("SelecionarCor", "Selecionar"));
            dgv_variaveis.Columns["Grupo"].ReadOnly = true;
            dgv_variaveis.Columns["Nome"].ReadOnly = true;
            dgv_variaveis.Columns["Preview"].ReadOnly = true;

            foreach (var variavel in variaveis)
            {
                int rowIndex = dgv_variaveis.Rows.Add(variavel.Grupo, variavel.Nome, variavel.Valor, string.Empty, IsColorValue(variavel.Valor) ? "..." : string.Empty);
                dgv_variaveis.Rows[rowIndex].Tag = variavel;
                UpdateColorPreview(dgv_variaveis.Rows[rowIndex]);
            }

            for (int i = 0; i < dgv_variaveis.Columns.Count; i++)
            {
                dgv_variaveis.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dgv_variaveis.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            dgv_variaveis.Columns["Preview"].FillWeight = 20;
            dgv_variaveis.Columns["SelecionarCor"].FillWeight = 24;
        }

        private DataGridViewButtonColumn CreateButtonColumn(string name, string header)
        {
            return new DataGridViewButtonColumn
            {
                Name = name,
                HeaderText = header,
                UseColumnTextForButtonValue = false
            };
        }

        private void dgv_variaveis_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || dgv_variaveis.Columns[e.ColumnIndex].Name != "SelecionarCor")
            {
                return;
            }

            DataGridViewRow row = dgv_variaveis.Rows[e.RowIndex];
            string valorAtual = row.Cells["Valor"].Value?.ToString();
            if (!TryParseColor(valorAtual, out Color corAtual))
            {
                Message.MensagemAlerta("Esta variavel nao possui um valor de cor valido.");
                return;
            }

            using (ColorDialog dialog = new ColorDialog())
            {
                dialog.Color = corAtual;
                dialog.FullOpen = true;

                if (dialog.ShowDialog() != DialogResult.OK)
                {
                    return;
                }

                row.Cells["Valor"].Value = ColorToHex(dialog.Color);
                row.Cells["SelecionarCor"].Value = "...";
                UpdateColorPreview(row);
            }
        }

        private void dgv_variaveis_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || dgv_variaveis.Columns[e.ColumnIndex].Name != "Valor")
            {
                return;
            }

            UpdateColorPreview(dgv_variaveis.Rows[e.RowIndex]);
        }

        private void UpdateColorPreview(DataGridViewRow row)
        {
            string valor = row.Cells["Valor"].Value?.ToString();
            bool isColor = TryParseColor(valor, out Color color);

            row.Cells["Preview"].Style.BackColor = isColor ? color : dgv_variaveis.DefaultCellStyle.BackColor;
            row.Cells["Preview"].Style.ForeColor = isColor ? color : dgv_variaveis.DefaultCellStyle.ForeColor;
            row.Cells["Preview"].Value = isColor ? string.Empty : "-";
            row.Cells["SelecionarCor"].Value = isColor ? "..." : string.Empty;
        }

        private bool IsColorValue(string value)
        {
            return TryParseColor(value, out _);
        }

        private bool TryParseColor(string value, out Color color)
        {
            color = Color.Empty;

            if (string.IsNullOrWhiteSpace(value))
            {
                return false;
            }

            string normalized = value.Trim();
            if (!normalized.StartsWith("#") && !Color.FromName(normalized).IsKnownColor)
            {
                return false;
            }

            try
            {
                color = ColorTranslator.FromHtml(normalized);
                return color != Color.Empty;
            }
            catch
            {
                return false;
            }
        }

        private string ColorToHex(Color color)
        {
            return $"#{color.R:X2}{color.G:X2}{color.B:X2}";
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
