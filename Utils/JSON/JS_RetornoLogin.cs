using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSON
{
    public class JS_RetornoLogin
    {
        public JS_Usuario Usuario { get; set; }
        public int Codigo { get; set; }
        public string Mensagem { get; set; }
        public bool Sucesso { get; set; }
    }
}
