﻿/*
using Azure.Storage.Blobs;
using Azure.Storage;
using Microsoft.Azure.Management.Storage.Models;

namespace LandLeaser.API.Interfaces
{
    public interface IBlobStorageService
    {
        /// <summary>
        ///     Create a container in the blob storage
        /// </summary>
        /// <returns>C</returns>
        Task<BlobContainer> CreateContainerAsync(string containerName);

        /// <summary>
        ///     Crete a shared access signature for a Container.
        /// </summary>
        /// <param name="container"></param>
        /// <param name="key"></param>
        /// <param name="expireOn"></param>
        /// <param name="sasBuilderResource">
        ///     Specifies which resources are accessible via the shared access
        ///     signature.
        ///     Specify "b" if the shared resource is a blob. This grants access to
        ///     the content and metadata of the blob.
        ///     Specify "c" if the shared resource is a blob container. This grants
        ///     access to the content and metadata of any blob in the container,
        ///     and to the list of blobs in the container.
        ///     Beginning in version 2018-11-09, specify "bs" if the shared resource
        ///     is a blob snapshot.  This grants access to the content and
        ///     metadata of the specific snapshot, but not the corresponding root
        ///     blob.
        ///     Beginning in version 2019-12-12, specify "bv" if the shared resource
        ///     is a blob version.  This grants access to the content and
        ///     metadata of the specific version, but not the corresponding root
        ///     blob.
        /// </param>
        /// <param name="storedPolicyName"></param>
        /// <returns></returns>
        string GetSasToken(BlobContainerClient container, StorageSharedKeyCredential key,
            DateTimeOffset expireOn, string sasBuilderResource, string storedPolicyName = null);

        /// <summary>
        ///      Download Blob Items from a particular container in blob storage
        /// </summary>
        /// <param name="containerName"></param>
        /// <param name="uniqueStorageName"></param>
        /// <param name="artistId"></param>
        /// <returns></returns>
        /*ArtImage GetArtImages(string containerName, string uniqueStorageName, string artistId);#1#
    }
}
*/
