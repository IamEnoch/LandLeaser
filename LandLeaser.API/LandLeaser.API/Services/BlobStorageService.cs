﻿/*
using Azure.Storage;
using Azure.Storage.Blobs;
using Azure.Storage.Sas;
using LandLeaser.API.Interfaces;
using Microsoft.Azure.Management.Storage.Models;

namespace LandLeaser.API.Services
{
    public class BlobStorageService : IBlobStorageService
	{
        private readonly IConfiguration _configuration;
        public BlobStorageService(IConfiguration configuration)
		{
            _configuration = configuration;
        }
        public async Task<BlobContainer> CreateContainerAsync(string containerName)
        {
            //Connection string of the storage account
            var connString = _configuration.GetConnectionString("BlobStorageConnectionString");

            var container = new BlobContainerClient(connString, containerName);
            await container.CreateIfNotExistsAsync();

            var expiresOn = DateTime.Now.AddDays(1);
            var key = new StorageSharedKeyCredential(_configuration["BlobStorageAccountName"],
                _configuration["BlobStorageAccountKey"]);
            var sasToken = GetSasToken(container, key, expiresOn, "c");
            var containerUrl = $"{container.Uri}/?{sasToken}";
            var tokenExpiry = expiresOn;
            var accountEndpoint = container.Uri.ToString().Replace($"/{containerName}", "");
            var connectionString = $"BlobEndpoint={accountEndpoint}/;SharedAccessSignature={sasToken}";

            return new BlobContainer(container.Name, containerUrl, connectionString, tokenExpiry.ToString());
        }
        public string GetSasToken(BlobContainerClient container, StorageSharedKeyCredential key,DateTimeOffset expireOn, string sasBuilderResource, string storedPolicyName = null)
        {
            var sasBuilder = new BlobSasBuilder
            {
                BlobContainerName = container.Name,
                Resource = sasBuilderResource
            };
            if (storedPolicyName == null)
            {
                sasBuilder.StartsOn = DateTimeOffset.UtcNow;
                sasBuilder.ExpiresOn = expireOn;
                sasBuilder.SetPermissions(BlobContainerSasPermissions.All);
            }
            else
            {
                sasBuilder.Identifier = storedPolicyName;
            }
            // Use the key to get the SAS token.
            return sasBuilder.ToSasQueryParameters(key).ToString();
        }
        /*public ArtImage GetArtImages(string containerName, string uniqueStorageName, string artistId)
        {
            var connectionString = _configuration.GetConnectionString("BlobStorageConnectionString");
            var containerClient = new BlobContainerClient(connectionString, containerName);
            containerClient.CreateIfNotExists();
            var blobStorageToken = GetBlobSasToken(containerClient);
            var prefix = $"{artistId}/{uniqueStorageName}";
            var blobItems = containerClient.GetBlobs(prefix: prefix).Where(i => i.Name.Contains($"{prefix}"));
            var artImages = blobItems
                .Select(blobItem => $"{containerClient.Uri}/{blobItem.Name}?{blobStorageToken.Token}")
                .ToList();
            return new ArtImage
            {
                ImageUrl = artImages,
                ExpiresOn = blobStorageToken.ExpiresOn
            };
        }#1#
        private BlobStorageToken GetBlobSasToken(BlobContainerClient containerClient)
        {
            var key = new StorageSharedKeyCredential(_configuration["BlobStorageAccountName"],
                _configuration["BlobStorageAccountKey"]);
            var expiresOn = DateTime.Now.AddHours(2);
            var (sasToken, tokenExpiry) = (GetSasToken(containerClient, key, expiresOn, "b"), expiresOn);
            var token = new BlobStorageToken
            {
                ExpiresOn = tokenExpiry,
                Token = sasToken
            };
            return token;
        }
    }
    
}
*/
