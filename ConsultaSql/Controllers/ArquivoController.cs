using System.IO;

namespace ConsultaSql.Controllers
{
    internal class ArquivoController
    {
        #region Variáveis
        private string                                                                                                                                                                                                                                                                                                                                           fullFileName;
        private FileInfo fileInfo;
        #endregion

        #region Métodos
        /// <summary>
        /// Cria uma instância de ArquivoController
        /// </summary>
        /// <param name="fullFileName">Nome completo do arquivo. Inclui diretório, nome e extensão do arquivo.</param>
        public ArquivoController(string fullFileName)
        {
            this.fullFileName = fullFileName;
            CarregarInformacoesArquivo();
        }

        /// <summary>
        /// Carrega as informações do arquivo 'fullFileName' para a memória.
        /// </summary>
        private void CarregarInformacoesArquivo()
        {
            if (fileInfo != null)
            {
                fileInfo = null;
            }
            fileInfo = new FileInfo(fullFileName);
        }

        /// <summary>
        /// Lê o arquivo inteiro apontado em fullFileName, se esse existir.
        /// </summary>
        /// <returns>Todos os caracteres no arquivo fullFileName se esse existir. Caso contrário retorna uma string vazia.</returns>
        public string LerArquivoInteiro()
        {
            if (fileInfo.Exists)
            {
                using (StreamReader reader = new StreamReader(fullFileName))
                {
                    return reader.ReadToEnd();
                }
            }
            else
            {
                return string.Empty;
            }
        }
        #endregion
    }
}
