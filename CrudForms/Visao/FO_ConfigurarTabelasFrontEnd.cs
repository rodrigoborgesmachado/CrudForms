using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Visao
{
    public partial class FO_ConfigurarTabelasFrontEnd : Form
    {
        private readonly List<Model.MD_Tabela> tabelas;
        private bool locked = false;

        public FO_ConfigurarTabelasFrontEnd(List<Model.MD_Tabela> tabelas)
        {
            this.tabelas = tabelas ?? new List<Model.MD_Tabela>();
            InitializeComponent();
            IniciaForm();
        }

        private void IniciaForm()
        {
            if (Model.Parametros.ModoDark)
            {
                this.BackColor = Color.FromArgb(51, 51, 51);
                this.ForeColor = Color.White;
            }
            else
            {
                this.BackColor = Color.FromArgb(251, 249, 238);
                this.ForeColor = Color.Black;
            }

            foreach (Button button in this.Controls.OfType<Button>())
            {
                button.BackColor = this.BackColor;
                button.ForeColor = this.ForeColor;
            }

            foreach (Button button in this.grb_configuracaoSQLSERVER.Controls.OfType<Button>())
            {
                button.BackColor = this.BackColor;
                button.ForeColor = this.ForeColor;
            }

            grb_configuracaoSQLSERVER.ForeColor = this.ForeColor;
            button1.Visible = false;

            dgv_tabela_in.EditMode = DataGridViewEditMode.EditOnEnter;
            dgv_coluna.EditMode = DataGridViewEditMode.EditOnEnter;

            dgv_tabela_in.SelectionChanged += dgv_tabela_in_SelectionChanged;
            btn_editar_tabela.Click += btn_editar_tabela_Click;
            btn_editar_coluna.Click += btn_editar_coluna_Click;
            btn_confirmar.Click += btn_confirmar_Click;

            FillGridTabelas();
        }

        private void FillGridTabelas()
        {
            locked = true;
            dgv_tabela_in.Rows.Clear();
            dgv_tabela_in.Columns.Clear();

            dgv_tabela_in.Columns.Add("Nome", "Nome");
            dgv_tabela_in.Columns.Add("Apelido", "Apelido");
            dgv_tabela_in.Columns["Nome"].ReadOnly = true;

            foreach (var tabela in tabelas.OrderBy(t => t.DAO.Nome))
            {
                int rowIndex = dgv_tabela_in.Rows.Add(tabela.DAO.Nome, tabela.Apelido);
                dgv_tabela_in.Rows[rowIndex].Tag = tabela;
            }

            FormatGrid(dgv_tabela_in);
            locked = false;

            if (dgv_tabela_in.Rows.Count > 0)
            {
                dgv_tabela_in.Rows[0].Selected = true;
                FillGridColunas(GetSelectedTabela());
            }
        }

        private void FillGridColunas(Model.MD_Tabela tabela)
        {
            locked = true;
            dgv_coluna.Rows.Clear();
            dgv_coluna.Columns.Clear();

            dgv_coluna.Columns.Add("Nome", "Nome");
            dgv_coluna.Columns.Add("Apelido", "Apelido");
            dgv_coluna.Columns.Add(CreateCheckBoxColumn("VisivelListagem", "Visivel na Listagem"));
            dgv_coluna.Columns.Add(CreateCheckBoxColumn("VisivelInclusaoEdicao", "Visivel na Inclusao / Edicao"));
            dgv_coluna.Columns.Add(CreateCheckBoxColumn("VisivelDetalhes", "Visivel nos Detalhes"));
            dgv_coluna.Columns["Nome"].ReadOnly = true;

            if (tabela != null)
            {
                foreach (var coluna in tabela.CamposFrontEnd())
                {
                    if (IsPrimaryKeyCode(coluna))
                    {
                        coluna.VisivelInclusaoEdicao = false;
                    }

                    int rowIndex = dgv_coluna.Rows.Add(
                        coluna.DAO.Nome,
                        coluna.Apelido,
                        coluna.VisivelListagem,
                        coluna.VisivelInclusaoEdicao,
                        coluna.VisivelDetalhes);
                    dgv_coluna.Rows[rowIndex].Tag = coluna;

                    if (IsPrimaryKeyCode(coluna))
                    {
                        dgv_coluna.Rows[rowIndex].Cells["VisivelInclusaoEdicao"].ReadOnly = true;
                        dgv_coluna.Rows[rowIndex].Cells["VisivelInclusaoEdicao"].Style.BackColor = Color.LightGray;
                    }
                }
            }

            FormatGrid(dgv_coluna);
            locked = false;
        }

        private DataGridViewCheckBoxColumn CreateCheckBoxColumn(string name, string header)
        {
            return new DataGridViewCheckBoxColumn
            {
                Name = name,
                HeaderText = header
            };
        }

        private void FormatGrid(DataGridView grid)
        {
            for (int i = 0; i < grid.Columns.Count; i++)
            {
                grid.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                grid.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        private Model.MD_Tabela GetSelectedTabela()
        {
            if (dgv_tabela_in.SelectedRows.Count == 0)
            {
                return null;
            }

            return dgv_tabela_in.SelectedRows[0].Tag as Model.MD_Tabela;
        }

        private Model.MD_Campos GetSelectedColuna()
        {
            if (dgv_coluna.SelectedRows.Count == 0)
            {
                return null;
            }

            return dgv_coluna.SelectedRows[0].Tag as Model.MD_Campos;
        }

        private void dgv_tabela_in_SelectionChanged(object sender, EventArgs e)
        {
            if (locked)
            {
                return;
            }

            SaveColumnGridValues();
            FillGridColunas(GetSelectedTabela());
        }

        private void btn_editar_tabela_Click(object sender, EventArgs e)
        {
            if (GetSelectedTabela() == null)
            {
                Message.MensagemAlerta("Selecione uma tabela");
                return;
            }

            dgv_tabela_in.CurrentCell = dgv_tabela_in.SelectedRows[0].Cells["Apelido"];
            dgv_tabela_in.BeginEdit(true);
        }

        private void btn_editar_coluna_Click(object sender, EventArgs e)
        {
            if (GetSelectedColuna() == null)
            {
                Message.MensagemAlerta("Selecione uma coluna");
                return;
            }

            dgv_coluna.CurrentCell = dgv_coluna.SelectedRows[0].Cells["Apelido"];
            dgv_coluna.BeginEdit(true);
        }

        private void btn_confirmar_Click(object sender, EventArgs e)
        {
            SaveCurrentGridValues();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void SaveCurrentGridValues()
        {
            dgv_tabela_in.EndEdit();

            foreach (DataGridViewRow row in dgv_tabela_in.Rows)
            {
                var tabela = row.Tag as Model.MD_Tabela;
                if (tabela == null)
                {
                    continue;
                }

                tabela.Apelido = GetCellValue(row, "Apelido", tabela.DAO.Nome);
            }

            SaveColumnGridValues();
        }

        private void SaveColumnGridValues()
        {
            dgv_coluna.EndEdit();

            foreach (DataGridViewRow row in dgv_coluna.Rows)
            {
                var coluna = row.Tag as Model.MD_Campos;
                if (coluna == null)
                {
                    continue;
                }

                coluna.Apelido = GetCellValue(row, "Apelido", coluna.DAO.Nome);
                coluna.VisivelListagem = GetBoolCellValue(row, "VisivelListagem");
                coluna.VisivelInclusaoEdicao = IsPrimaryKeyCode(coluna) ? false : GetBoolCellValue(row, "VisivelInclusaoEdicao");
                coluna.VisivelDetalhes = GetBoolCellValue(row, "VisivelDetalhes");
            }
        }

        private string GetCellValue(DataGridViewRow row, string columnName, string defaultValue)
        {
            string value = row.Cells[columnName].Value?.ToString();
            return string.IsNullOrWhiteSpace(value) ? defaultValue : value;
        }

        private bool GetBoolCellValue(DataGridViewRow row, string columnName)
        {
            object value = row.Cells[columnName].Value;
            return value != null && bool.TryParse(value.ToString(), out bool result) && result;
        }

        private bool IsPrimaryKeyCode(Model.MD_Campos coluna)
        {
            return coluna.DAO.PrimaryKey || coluna.DAO.Nome.Equals("Code", StringComparison.OrdinalIgnoreCase) || coluna.DAO.Nome.Equals("Id", StringComparison.OrdinalIgnoreCase);
        }
    }
}
