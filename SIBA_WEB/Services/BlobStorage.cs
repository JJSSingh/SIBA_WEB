using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace SIBA_WEB.Services
{
    public class BlobStorage : IBlobStorage
    {
        String StorageAccount;
        String StorageKey;
        //private readonly IHostingEnvironment hostingEnvironment;

        public BlobStorage(String StorageAccount, String StorageKey)
        {
            this.StorageAccount = StorageAccount;
            this.StorageKey = StorageKey;
        }

        private async Task<CloudBlobContainer> getCloudBlobContainer(String ContainerName)
        {
            CloudStorageAccount storageAccount = new CloudStorageAccount(
                new StorageCredentials(this.StorageAccount, this.StorageKey), false);

            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer container = blobClient.GetContainerReference(ContainerName);
            //await container.CreateIfNotExistsAsync();
            return container;
        }

        //-----------------------------

        public async Task<String> UploadBlob(String ContainerName,String directory, IFormFile file)
        {
            CloudBlobContainer container = await getCloudBlobContainer(ContainerName);
            await container.SetPermissionsAsync(new BlobContainerPermissions
            {
                PublicAccess = BlobContainerPublicAccessType.Blob
            });

            CloudBlobDirectory dir = container.GetDirectoryReference(directory);
            CloudBlockBlob blob = dir.GetBlockBlobReference(file.FileName);

            //CloudBlockBlob blob2 = container.GetBlockBlobReference(fileName);
            String result = "";
            try
            {
                using (Stream fileStream = file.OpenReadStream())
                {
                    blob.UploadFromStreamAsync(fileStream).Wait();
                    if (blob.Properties.Length >=0)
                    {
                        result = "Success!";
                    }
                }
            }
            catch(Exception ex)
            {
                result = ex.Message;
            }
            return result;
            
        }

        public async Task<List<String>> ListBlobs(String ContainerName, String folder)
        {
            List<String> blobs = new List<String>();

            CloudBlobContainer container = await getCloudBlobContainer(ContainerName);
            CloudBlobDirectory dir = container.GetDirectoryReference(folder);
            BlobResultSegment resultSegment = dir.ListBlobsSegmentedAsync(null).Result;

            foreach (IListBlobItem item in resultSegment.Results)
            {
                if (item.GetType() == typeof(CloudBlockBlob))
                {
                    CloudBlockBlob blob = (CloudBlockBlob)item;
                    blobs.Add(blob.Name);
                }
                else if (item.GetType() == typeof(CloudPageBlob))
                {
                    CloudPageBlob blob = (CloudPageBlob)item;
                    blobs.Add(blob.Name);
                }
                else if (item.GetType() == typeof(CloudBlobDirectory))
                {
                    CloudBlobDirectory dirr = (CloudBlobDirectory)item;
                    blobs.Add(dirr.Uri.ToString());
                }
            }
            return blobs;
        }

        public async Task<List<String>> ListBlobsUrl(String ContainerName, String folder)
        {
            List<String> blobs = new List<String>();

            CloudBlobContainer container = await getCloudBlobContainer(ContainerName);
            CloudBlobDirectory dir = container.GetDirectoryReference(folder);
            BlobResultSegment resultSegment = dir.ListBlobsSegmentedAsync(null).Result;

            foreach (IListBlobItem item in resultSegment.Results)
            {
                if (item.GetType() == typeof(CloudBlockBlob))
                {
                    CloudBlockBlob blob = (CloudBlockBlob)item;
                    blobs.Add(blob.Uri.AbsoluteUri);
                }
                else if (item.GetType() == typeof(CloudPageBlob))
                {
                    CloudPageBlob blob = (CloudPageBlob)item;
                    blobs.Add(blob.Uri.AbsoluteUri);
                }
                else if (item.GetType() == typeof(CloudBlobDirectory))
                {
                    CloudBlobDirectory dirr = (CloudBlobDirectory)item;
                    blobs.Add(dirr.Uri.AbsoluteUri);
                }
            }
            return blobs;
        }

        

        public async Task DownloadBlob(String ContainerName, String folder)
        {
            CloudBlobContainer container = await getCloudBlobContainer(ContainerName);
            CloudBlobDirectory dir = container.GetDirectoryReference(folder);

            BlobResultSegment blobResult = dir.ListBlobsSegmentedAsync(null).Result;

            foreach (IListBlobItem item in blobResult.Results)
            {
                CloudBlockBlob blob = (CloudBlockBlob)item;

                using (var FileStream = System.IO.File.OpenWrite(blob.Name))
                {
                    //System.Threading.Thread.Sleep(1000);
                    blob.DownloadToStreamAsync(FileStream).Wait();
                }
            }
        }
        

        
    }

    public interface IBlobStorage
    {
        //Task<CloudBlobContainer> getCloudBlobContainer();
        Task<String> UploadBlob(String ContainerName, String directory, IFormFile file);
        Task<List<String>> ListBlobs(String ContainerName,String folder);

        Task DownloadBlob(String ContainerName, String folder);

    }
}
