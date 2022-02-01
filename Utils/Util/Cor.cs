using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Util
{
    public class Cor
    {
        #region Atributos e Propriedades

        public int r = 255;

        public int g = 255;

        public int b = 255;

        public bool empty = true;

        #endregion Atributos e Propriedades

        /// <summary>
        /// Construtor principal da classe
        /// </summary>
        /// <param name="cor"></param>
        public Cor(string cor, ref string mensagem)
        {
            Util.CL_Files.WriteOnTheLog("Cor.Cor()", Util.Global.TipoLog.DETALHADO);

            try
            {
                string[] text = cor.Split(',');
                r = int.Parse(text[0]);
                g = int.Parse(text[1]);
                b = int.Parse(text[2]);
                empty = false;
            }
            catch(Exception e)
            {
                mensagem = e.Message;
                empty = true;
            }
        }

        /// <summary>
        /// Método que retorna a cor vermelha
        /// </summary>
        /// <returns></returns>
        public static Color Red()
        {
            return Color.FromArgb(255, 0, 0);
        }

        /// <summary>
        /// Método que retorna a cor branca
        /// </summary>
        /// <returns></returns>
        public static Color White()
        {
            return Color.FromArgb(255, 255, 255);
        }

        /// <summary>
        /// Método que retorna a cor branca
        /// </summary>
        /// <returns></returns>
        public static Color Black()
        {
            return Color.FromArgb(0, 0, 0);
        }

        /// <summary>
        /// Método que retorna a cor vermelha
        /// </summary>
        /// <returns></returns>
        public static Color Green()
        {
            return Color.FromArgb(0, 255, 0);
        }

        /// <summary>
        /// Método que retorna a cor vermelha
        /// </summary>
        /// <returns></returns>
        public static Color Blue()
        {
            return Color.FromArgb(0, 0, 255);
        }

    }
}
