using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace ScanPlantAPI.Services.Interfaces
{
    /// <summary>
    /// Interface para o serviço de armazenamento
    /// </summary>
    public interface IStorageService
    {
        /// <summary>
        /// Faz upload de um arquivo
        /// </summary>
        /// <param name="file">Arquivo a ser enviado</param>
        /// <param name="fileName">Nome do arquivo (opcional)</param>
        /// <returns>URL pública do arquivo</returns>
        Task<string> UploadFileAsync(IFormFile file, string? fileName = null);

        /// <summary>
        /// Obtém a URL pública de um arquivo
        /// </summary>
        /// <param name="fileName">Nome do arquivo</param>
        /// <returns>URL pública do arquivo</returns>
        Task<string> GetFileUrlAsync(string fileName);

        /// <summary>
        /// Remove um arquivo
        /// </summary>
        /// <param name="fileName">Nome do arquivo</param>
        /// <returns>Verdadeiro se o arquivo foi removido com sucesso</returns>
        Task<bool> DeleteFileAsync(string fileName);
    }
}