using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Regras
{
    public class InformacoesSaidaImportador
    {
        public List<Model.MD_Tabela> tabelasModel { get; set; }
        public List<Model.MD_Campos> camposModel { get; set; }
        public List<Model.MD_Relacao> relacoesModel { get; set; }
        public bool Importado { get; set; }
    }
}
