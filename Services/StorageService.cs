using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using ScanPlantAPI.Services.Interfaces;
using System;
using System.IO;
using System.Threading.Tasks;

namespace ScanPlantAPI.Services
{
    /// <summary>
    /// Implementação do serviço de armazenamento usando Azure Blob Storage
    /// </summary>
    public class StorageService : IStorageService
    {
        private readonly BlobServiceClient _blobServiceClient;
        private readonly string _containerName;

        /// <summary>
        /// Construtor do serviço de armazenamento
        /// </summary>
        /// <param name="configuration">Configuração da aplicação</param>
        public StorageService(IConfiguration configuration)
        {
            _blobServiceClient = new BlobServiceClient(configuration["AzureBlobStorage:ConnectionString"]);
            _containerName = configuration["AzureBlobStorage:ContainerName"];
        }

        /// <summary>
        /// Faz upload de um arquivo
        /// </summary>
        /// <param name="file">Arquivo a ser enviado</param>
        /// <param name="fileName">Nome do arquivo (opcional)</param>
        /// <returns>URL pública do arquivo</returns>
        public async Task<string> UploadFileAsync(IFormFile file, string? fileName = null)
        {
            if (file == null || file.Length == 0)
            {
                throw new ArgumentException("Arquivo inválido", nameof(file));
            }

            // Cria o container se não existir
            var containerClient = _blobServiceClient.GetBlobContainerClient(_containerName);
            await containerClient.CreateIfNotExistsAsync(PublicAccessType.Blob);

            // Gera um nome único para o arquivo se não for fornecido
            if (string.IsNullOrEmpty(fileName))
            {
                fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            }

            // Obtém uma referência para o blob
            var blobClient = containerClient.GetBlobClient(fileName);

            // Faz upload do arquivo
            using (var stream = file.OpenReadStream())
            {
                await blobClient.UploadAsync(stream, true);
            }

            // Retorna a URL pública do arquivo
            return blobClient.Uri.ToString();
        }

        /// <summary>
        /// Obtém a URL pública de um arquivo
        /// </summary>
        /// <param name="fileName">Nome do arquivo</param>
        /// <returns>URL pública do arquivo</returns>
        public async Task<string> GetFileUrlAsync(string fileName)
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient(_containerName);
            var blobClient = containerClient.GetBlobClient(fileName);

            // Verifica se o blob existe
            if (!await blobClient.ExistsAsync())
            {
                return null;
            }

            return blobClient.Uri.ToString();
        }

        /// <summary>
        /// Remove um arquivo
        /// </summary>
        /// <param name="fileName">Nome do arquivo</param>
        /// <returns>Verdadeiro se o arquivo foi removido com sucesso</returns>
        public async Task<bool> DeleteFileAsync(string fileName)
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient(_containerName);
            var blobClient = containerClient.GetBlobClient(fileName);

            // Verifica se o blob existe
            if (!await blobClient.ExistsAsync())
            {
                return false;
            }

            // Remove o blob
            var response = await blobClient.DeleteAsync();
            return true;
        }
    }
}