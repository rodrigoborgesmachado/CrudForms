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
            dgv_tabela_in.CurrentCellDirtyStateChanged += dgv_tabela_in_CurrentCellDirtyStateChanged;
            dgv_tabela_in.CellValueChanged += dgv_tabela_in_CellValueChanged;
            dgv_tabela_in.DataError += dgv_tabela_in_DataError;
            btn_editar_tabela.Click += btn_editar_tabela_Click;
            btn_editar_coluna.Click += btn_editar_coluna_Click;
            btn_up_tabela.Click += btn_up_tabela_Click;
            btn_down_tabela.Click += btn_down_tabela_Click;
            btn_up_coluna.Click += btn_up_coluna_Click;
            btn_down_coluna.Click += btn_down_coluna_Click;
            btn_confirmar.Click += btn_confirmar_Click;

            FillGridTabelas();
        }

        private void FillGridTabelas(Model.MD_Tabela tabelaSelecionada = null)
        {
            locked = true;
            dgv_tabela_in.Rows.Clear();
            dgv_tabela_in.Columns.Clear();

            dgv_tabela_in.Columns.Add("Nome", "Nome");
            dgv_tabela_in.Columns.Add("Apelido", "Apelido");
            dgv_tabela_in.Columns.Add(CreateIconColumn());
            dgv_tabela_in.Columns.Add("PreviewIcone", "Preview");
            dgv_tabela_in.Columns["Nome"].ReadOnly = true;
            dgv_tabela_in.Columns["PreviewIcone"].ReadOnly = true;

            foreach (var tabela in tabelas)
            {
                int rowIndex = dgv_tabela_in.Rows.Add(tabela.DAO.Nome, tabela.Apelido, tabela.IconeFrontEnd, GetIconPreview(tabela.IconeFrontEnd));
                dgv_tabela_in.Rows[rowIndex].Tag = tabela;
            }

            FormatGrid(dgv_tabela_in);

            if (dgv_tabela_in.Rows.Count > 0)
            {
                SelectRowByTag(dgv_tabela_in, tabelaSelecionada ?? tabelas[0]);
            }

            locked = false;
            FillGridColunas(GetSelectedTabela());
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

        private DataGridViewComboBoxColumn CreateIconColumn()
        {
            DataGridViewComboBoxColumn column = new DataGridViewComboBoxColumn
            {
                Name = "Icone",
                HeaderText = "Icone",
                FlatStyle = FlatStyle.Popup
            };

            foreach (string icon in GetIconOptions())
            {
                column.Items.Add(icon);
            }

            return column;
        }

        private void FormatGrid(DataGridView grid)
        {
            for (int i = 0; i < grid.Columns.Count; i++)
            {
                grid.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                grid.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            if (grid.Columns.Contains("Icone"))
            {
                grid.Columns["Icone"].FillWeight = 80;
            }

            if (grid.Columns.Contains("PreviewIcone"))
            {
                grid.Columns["PreviewIcone"].FillWeight = 80;
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

        private void dgv_tabela_in_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dgv_tabela_in.IsCurrentCellDirty)
            {
                dgv_tabela_in.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void dgv_tabela_in_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (locked || e.RowIndex < 0 || e.ColumnIndex < 0 || dgv_tabela_in.Columns[e.ColumnIndex].Name != "Icone")
            {
                return;
            }

            DataGridViewRow row = dgv_tabela_in.Rows[e.RowIndex];
            row.Cells["PreviewIcone"].Value = GetIconPreview(row.Cells["Icone"].Value?.ToString());
        }

        private void dgv_tabela_in_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.ThrowException = false;
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

        private void btn_up_tabela_Click(object sender, EventArgs e)
        {
            MoveSelectedTabela(-1);
        }

        private void btn_down_tabela_Click(object sender, EventArgs e)
        {
            MoveSelectedTabela(1);
        }

        private void btn_up_coluna_Click(object sender, EventArgs e)
        {
            MoveSelectedColuna(-1);
        }

        private void btn_down_coluna_Click(object sender, EventArgs e)
        {
            MoveSelectedColuna(1);
        }

        private void btn_confirmar_Click(object sender, EventArgs e)
        {
            SaveCurrentGridValues();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void MoveSelectedTabela(int direction)
        {
            var tabela = GetSelectedTabela();
            if (tabela == null)
            {
                Message.MensagemAlerta("Selecione uma tabela");
                return;
            }

            int currentIndex = tabelas.IndexOf(tabela);
            int targetIndex = currentIndex + direction;
            if (currentIndex < 0 || targetIndex < 0 || targetIndex >= tabelas.Count)
            {
                return;
            }

            SaveCurrentGridValues();

            var temp = tabelas[targetIndex];
            tabelas[targetIndex] = tabelas[currentIndex];
            tabelas[currentIndex] = temp;

            FillGridTabelas(tabela);
        }

        private void MoveSelectedColuna(int direction)
        {
            var tabela = GetSelectedTabela();
            var coluna = GetSelectedColuna();
            if (tabela == null)
            {
                Message.MensagemAlerta("Selecione uma tabela");
                return;
            }

            if (coluna == null)
            {
                Message.MensagemAlerta("Selecione uma coluna");
                return;
            }

            SaveColumnGridValues();

            var colunas = tabela.CamposFrontEnd();
            int currentIndex = colunas.IndexOf(coluna);
            int targetIndex = currentIndex + direction;
            if (currentIndex < 0 || targetIndex < 0 || targetIndex >= colunas.Count)
            {
                return;
            }

            var temp = colunas[targetIndex];
            colunas[targetIndex] = colunas[currentIndex];
            colunas[currentIndex] = temp;

            FillGridColunas(tabela);
            SelectRowByTag(dgv_coluna, coluna);
        }

        private void SelectRowByTag(DataGridView grid, object tag)
        {
            grid.ClearSelection();

            if (tag == null)
            {
                return;
            }

            foreach (DataGridViewRow row in grid.Rows)
            {
                if (!ReferenceEquals(row.Tag, tag))
                {
                    continue;
                }

                row.Selected = true;
                grid.CurrentCell = row.Cells[0];
                return;
            }
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
                tabela.IconeFrontEnd = GetCellValue(row, "Icone", "bi-table");
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

        private List<string> GetIconOptions()
        {
            return new List<string>
            {
                "bi-table",
                "bi-book",
                "bi-journal-bookmark",
                "bi-person",
                "bi-people",
                "bi-person-badge",
                "bi-building",
                "bi-box",
                "bi-box-seam",
                "bi-cart",
                "bi-bag",
                "bi-basket",
                "bi-calendar",
                "bi-clock",
                "bi-file-text",
                "bi-folder",
                "bi-card-checklist",
                "bi-clipboard-data",
                "bi-graph-up",
                "bi-cash-coin",
                "bi-credit-card",
                "bi-truck",
                "bi-tags",
                "bi-gear",
                "bi-tools",
                "bi-shield-check",
                "bi-envelope",
                "bi-telephone",
                "bi-geo-alt",
                "bi-house",
                "bi-shop",
                "bi-receipt",
                "bi-archive",
                "bi-database",
                "bi-list-task",
                "bi-kanban",
                "bi-bar-chart",
                "bi-pie-chart",
                "bi-lock",
                "bi-key",
                "bi-printer",
                "bi-upload",
                "bi-download",
                "bi-chat-dots",
                "bi-bell"
            };
        }

        private string GetIconPreview(string icon)
        {
            string selectedIcon = string.IsNullOrWhiteSpace(icon) ? "bi-table" : icon;
            return GetIconSymbol(selectedIcon) + " " + selectedIcon;
        }

        private string GetIconSymbol(string icon)
        {
            if (icon.Contains("book") || icon.Contains("journal"))
            {
                return "📚";
            }

            if (icon.Contains("people"))
            {
                return "👥";
            }

            if (icon.Contains("person"))
            {
                return "👤";
            }

            if (icon.Contains("building") || icon.Contains("house") || icon.Contains("shop"))
            {
                return "🏢";
            }

            if (icon.Contains("box") || icon.Contains("archive") || icon.Contains("database"))
            {
                return "📦";
            }

            if (icon.Contains("cart") || icon.Contains("bag") || icon.Contains("basket"))
            {
                return "🛒";
            }

            if (icon.Contains("calendar"))
            {
                return "📅";
            }

            if (icon.Contains("clock"))
            {
                return "🕒";
            }

            if (icon.Contains("file") || icon.Contains("folder") || icon.Contains("clipboard") || icon.Contains("card"))
            {
                return "📄";
            }

            if (icon.Contains("graph") || icon.Contains("chart"))
            {
                return "📈";
            }

            if (icon.Contains("cash") || icon.Contains("credit"))
            {
                return "💰";
            }

            if (icon.Contains("truck"))
            {
                return "🚚";
            }

            if (icon.Contains("tags"))
            {
                return "🏷";
            }

            if (icon.Contains("gear") || icon.Contains("tools"))
            {
                return "⚙";
            }

            if (icon.Contains("shield") || icon.Contains("lock") || icon.Contains("key"))
            {
                return "🔒";
            }

            if (icon.Contains("envelope") || icon.Contains("telephone") || icon.Contains("geo") || icon.Contains("chat") || icon.Contains("bell"))
            {
                return "✉";
            }

            if (icon.Contains("printer"))
            {
                return "🖨";
            }

            if (icon.Contains("upload"))
            {
                return "⬆";
            }

            if (icon.Contains("download"))
            {
                return "⬇";
            }

            return "▦";
        }
    }
}
