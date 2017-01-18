using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using Microsoft.WindowsAzure.Storage.Auth;

namespace test.controllers
{
    public class TransactionUri 
    {
            public string GetUriForAttachment(string imgPath,int id, int tid)
            {
            var _connString = "DefaultEndpointsProtocol=https;AccountName=sanjaywebapi2;AccountKey=zkdbW2P8LlPZYhObin1XZoor3zIIZsZ+nQhYwQ+Pg2yxhCW/LHgFTyUO9eYd3PFgKSctLxiOfHXQb2YRrztSNA==";
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(_connString);

            CloudBlobClient client = storageAccount.CreateCloudBlobClient();

            CloudBlobContainer container = client.GetContainerReference(tid + "proofs");
            //container.CreateIfNotExistsAsync();

                Task creatingNewContainer = container.CreateIfNotExistsAsync();
                creatingNewContainer.Wait();

            Task setingpermisiion = container.SetPermissionsAsync(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Blob });

                setingpermisiion.Wait();


            CloudBlockBlob blob = container.GetBlockBlobReference(tid.ToString());
            try
            {

                using (var fs = new FileStream(imgPath, FileMode.Open))
                {
                    Task uploadAttachment = blob.UploadFromStreamAsync(fs);

                    uploadAttachment.Wait();

                }
            }
            catch(Exception e) { return null; }
           
            return blob.Uri.ToString();

        }

    }
}

