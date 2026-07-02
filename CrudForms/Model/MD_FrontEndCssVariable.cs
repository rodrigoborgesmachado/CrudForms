namespace Model
{
    public class MD_FrontEndCssVariable
    {
        public string Grupo { get; set; }
        public string Nome { get; set; }
        public string Valor { get; set; }

        public MD_FrontEndCssVariable(string grupo, string nome, string valor)
        {
            Grupo = grupo;
            Nome = nome;
            Valor = valor;
        }
    }
}
