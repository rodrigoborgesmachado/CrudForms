using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Visao
{
    public static class Message
    {

        /// <summary>
        /// Método que apresenta uma mensagem de sucesso
        /// </summary>
        /// <param name="texto">Texto a ser apresentado no dialog</param>
        /// <returns>DialogResult - OK</returns>
        public static DialogResult MensagemSucesso(string texto)
        {
            if (Util.Global.ApresentaInformacao == Util.Global.Informacao.NAOAPRESENTAR)
                return DialogResult.OK;

            return MessageBox.Show(texto, "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        /// <summary>
        /// Método que apresenta uma mensagem de Informção
        /// </summary>
        /// <param name="texto">Texto a ser apresentado no dialog</param>
        /// <returns>DialogResult - OK</returns>
        public static DialogResult MensagemInformacao(string texto)
        {
            return MessageBox.Show(texto, "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Método que apresenta uma mensagem de alerta
        /// </summary>
        /// <param name="texto">Texto a ser apresentado no dialog</param>
        /// <returns>DialogResult - OK</returns>
        public static DialogResult MensagemAlerta(string texto)
        {
            return MessageBox.Show(texto, "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        /// <summary>
        /// Método que apresenta uma mensagem de erro
        /// </summary>
        /// <param name="texto">Texto a ser apresentado no dialog</param>
        /// <returns>DialogResult - OK</returns>
        public static DialogResult MensagemErro(string texto)
        {
            return MessageBox.Show(texto, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        /// <summary>
        /// Método que apresenta uma mensagem de confirmação
        /// </summary>
        /// <param name="texto">Texto a ser apresentado no dialog</param>
        /// <returns>DialogResult - OK</returns>
        public static DialogResult MensagemConfirmaçãoYesNo(string texto)
        {
            return MessageBox.Show(texto, "Alerta", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        }

    }
}
