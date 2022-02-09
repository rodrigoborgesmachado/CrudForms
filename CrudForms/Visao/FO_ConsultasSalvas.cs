﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Visao
{
    public partial class FO_ConsultasSalvas : Form
    {
        #region Atributos e Propriedades

        FO_Principal principal;
        List<Model.MD_Consultas> lista = new List<Model.MD_Consultas>();

        #endregion Atributos e Propriedades

        #region Eventos

        /// <summary>
        /// Evento lançado no clique da opção de editar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_editar_Click(object sender, EventArgs e)
        {
            if(this.dgv_generico.SelectedRows.Count == 0)
            {
                Message.MensagemAlerta("Selecione um item no grid");
            }

            FO_CadastraConsulta cadastraConsulta = new FO_CadastraConsulta(this.lista[this.dgv_generico.SelectedRows[0].Index], Util.Enumerator.Tarefa.EDITANDO);
            cadastraConsulta.Show();
            this.FillGrid();
        }

        /// <summary>
        /// Evento lançado no clique da opção de executar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_executar_Click(object sender, EventArgs e)
        {
            if (this.dgv_generico.SelectedRows.Count == 0)
            {
                Message.MensagemAlerta("Selecione um item no grid");
            }
            
            this.principal.AbreJanelaFormularioGenerico(this.lista[this.dgv_generico.SelectedRows[0].Index].DAO.Consulta.Replace("\"", "'"));
            this.Dispose();
        }

        #endregion Eventos

        #region Construtores

        /// <summary>
        /// Construtor principal da classe
        /// </summary>
        /// <param name="principal"></param>
        public FO_ConsultasSalvas(FO_Principal principal)
        {
            InitializeComponent();
            this.principal = principal;
            this.InicializaForm();
        }

        #endregion Construtores

        #region Métodos

        /// <summary>
        /// Método que monta a tela
        /// </summary>
        public void InicializaForm()
        {
            this.FillGrid();
        }

        /// <summary>
        /// Preenche o grid com o filtro atual
        /// </summary>
        public void FillGrid()
        {
            this.dgv_generico.Columns.Clear();
            this.dgv_generico.Rows.Clear();

            this.lista = Model.MD_Consultas.BuscaConsultas();

            foreach (DAO.MDN_Campo campo in new DAO.MD_Consultas().table.Fields_Table)
            {
                this.dgv_generico.Columns.Add(campo.Name_Field, campo.Name_Field);
            }

            FillGrid(lista);
        }

        /// <summary>
        /// Método que preenche a tabela com a lista
        /// </summary>
        /// <param name="lista"></param>
        private void FillGrid(List<Model.MD_Consultas> lista)
        {
            Visao.BarraDeCarregamento barra = new Visao.BarraDeCarregamento(lista.Count, "Carregando");
            barra.Show();

            this.lista.ForEach(p =>
            {
                FillGrid(p);
                barra.AvancaBarra(1);
            });

            for (int i = 0; i < this.dgv_generico.Columns.Count; i++)
            {
                this.dgv_generico.Columns[i].Width = 100;
                this.dgv_generico.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                this.dgv_generico.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            barra.Dispose();
        }

        /// <summary>
        /// Método que fill o grid
        /// </summary>
        /// <param name="consulta"></param>
        private void FillGrid(Model.MD_Consultas consulta)
        {
            List<string> list = new List<string>();
            list.Add(consulta.DAO.Codigo.ToString());
            list.Add(consulta.DAO.Nomeconsulta);
            list.Add(consulta.DAO.Consulta);

            this.dgv_generico.Rows.Add(list.ToArray());
        }

        #endregion Métodos

    }
}