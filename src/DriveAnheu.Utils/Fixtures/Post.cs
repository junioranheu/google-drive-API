using Microsoft.AspNetCore.Http;

namespace DriveAnheu.Utils.Fixtures
{
    public static class Post
    {
        /// <summary>
        /// arquivo = o arquivo em si, a variável IFormFile;
        /// nomeArquivo = o nome do novo objeto em questão;
        /// nomePasta = nome do caminho do arquivo, da pasta. Por exemplo: /Uploads/Usuarios/. "Usuarios" é o caminho;
        /// nomeArquivoAnterior = o nome do arquivo que constava anterior, caso exista;
        /// hostingEnvironment = o caminho até o wwwroot. Ele deve ser passado por parâmetro, já que não funcionaria aqui diretamente no BaseController;
        /// </summary>
        public static async Task<string?> UparImagem(IFormFile arquivo, string nomeArquivo, string nomePasta, string? nomeArquivoAnterior, string webRootPath)
        {
            if (arquivo.Length <= 0)
            {
                return string.Empty;
            }

            // Procedimento de inicialização para salvar nova imagem;
            string restoCaminho = $"/{nomePasta}/"; // Acesso à pasta referente; 

            // Verificar se o arquivo tem extensão, se não tiver, adicione;
            if (!Path.HasExtension(nomeArquivo))
            {
                nomeArquivo = $"{nomeArquivo}.jpg";
            }

            // Verificar se já existe uma foto caso exista, delete-a;
            if (!string.IsNullOrEmpty(nomeArquivoAnterior))
            {
                string caminhoArquivoAtual = webRootPath + restoCaminho + nomeArquivoAnterior;

                // Verificar se o arquivo existe;
                if (System.IO.File.Exists(caminhoArquivoAtual))
                {
                    System.IO.File.Delete(caminhoArquivoAtual); // Se existe, apague-o; 
                }
            }

            // Salvar aquivo;
            string caminhoDestino = webRootPath + restoCaminho + nomeArquivo; // Caminho de destino para upar;
            using (FileStream fs = File.Create(caminhoDestino))
            {
                await arquivo.CopyToAsync(fs);
            }

            return nomeArquivo;
        }

        /// <summary>
        /// Deleta os arquivos de uma pasta;
        /// 
        /// - Todos os arquivos;
        /// DeletarArquivosEmPasta(path);
        ///
        /// - Extensões específicas;
        /// List<string> extensoesEspecificas = [".txt", ".mp4"];
        /// DeletarArquivosEmPasta(path, listaExtensoes: extensoesEspecificas);
        ///
        /// - Nomes específicos;
        /// List<string> nomesEspeficos = ["ola", "tmr_pes"];
        /// DeletarArquivosEmPasta(path, listaNomes: nomesEspeficos);
        /// </summary>
        public static bool DeletarArquivosEmPasta(string path, string webRootPath, List<string>? listaExtensoes = null, List<string>? listaNomes = null, bool? ignorarExtensaoEmListaNomes = true)
        {
            string fullPath = Path.Combine(webRootPath, path);

            if (Directory.Exists(fullPath))
            {
                string[] files;

                if (listaExtensoes is not null && listaExtensoes.Count > 0)
                {
                    files = Directory.GetFiles(fullPath).Where(x => listaExtensoes.Any(extensao => x.EndsWith(extensao, StringComparison.OrdinalIgnoreCase))).ToArray();
                }
                else if (listaNomes is not null && listaNomes.Count > 0)
                {
                    if (ignorarExtensaoEmListaNomes.GetValueOrDefault())
                    {
                        files = Directory.GetFiles(fullPath).Where(file => listaNomes.Contains(Path.GetFileNameWithoutExtension(file), StringComparer.OrdinalIgnoreCase)).ToArray();
                    }
                    else
                    {
                        files = Directory.GetFiles(fullPath).Where(x => listaNomes.Contains(Path.GetFileName(x), StringComparer.OrdinalIgnoreCase)).ToArray();
                    }
                }
                else
                {
                    files = Directory.GetFiles(fullPath);
                }

                foreach (string file in files)
                {
                    File.Delete(file);
                }

                return true;
            }

            return false;
        }
    }
}