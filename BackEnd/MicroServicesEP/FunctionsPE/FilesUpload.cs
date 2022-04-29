using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Azure.Storage.Blobs;

namespace FunctionsPE
{
    public static class FilesUpload
    {
        [FunctionName("FilesUpload")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "FilesUpload/{typeUser}/{id}/{typeFile}")] HttpRequest req,
            string typeUser, string id, string typeFile,
            ILogger log
        )
        {
            try
            {
                string Connection = Environment.GetEnvironmentVariable("AzureWebJobsStorage");
                string containerName = Environment.GetEnvironmentVariable("ContainerName");
                var blobClient = new BlobContainerClient(Connection, $"{containerName}-{typeUser}");
                var blob = blobClient.GetBlobClient($"{typeUser}-{id}-{typeFile}");
                int maxSizeFileBytes = 1000000;
                var file = req.Form.Files[0];
                string result = "";
                Stream myBlob = file.OpenReadStream();
                if (file.Length <= maxSizeFileBytes)
                {
                    await blob.UploadAsync(myBlob, true);
                    result = $"Carga de archivo {file.FileName} realizada con éxito!";
                }
                else
                {
                    result = $"El archivo {file.FileName} no se puede cargar porque pesa más de 1 MB!";
                }
                return new OkObjectResult(result);

            }
            catch (Exception ex)
            {
                log.LogError(ex.Message);
                return new OkObjectResult($"La carga de archivos falló!");
            }

        }
    }
}
