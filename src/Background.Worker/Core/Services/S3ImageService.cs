using Amazon.S3.Model;
using Amazon.S3;
using Background.Worker.Core.Options;
using Background.Worker.Core.Services.Interfaces;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Background.Worker.Core.Services
{
    public class S3ImageService : IImageService
    {
        private readonly AwsOptions _awsOptions;

        public S3ImageService(IOptions<AwsOptions> awsOptions)
        {
            _awsOptions = awsOptions.Value;
        }

        public string Upload(byte[] imageDataByteArray, string fileName)
        {
            using var imageDataStream = new MemoryStream(imageDataByteArray);
            imageDataStream.Position = 0;

            var filePath = fileName;

            Upload(imageDataStream, filePath.TrimStart('/'));

            return filePath;
        }

        public string Upload(string base64string, string fileName)
        {
            if (base64string.Contains(","))
                base64string = base64string.Split(',')[1];
            using var imageDataStream = new MemoryStream(Convert.FromBase64String(base64string));
            imageDataStream.Position = 0;

            var filePath = fileName;

            Upload(imageDataStream, filePath.TrimStart('/'));

            return filePath;
        }

        private void Upload(Stream stream, string filePath)
        {
            using var client = new AmazonS3Client(_awsOptions.AccessKey, _awsOptions.SecretKey, Amazon.RegionEndpoint.GetBySystemName(_awsOptions.RegionName));
            try
            {
                PutObjectRequest putRequest = new PutObjectRequest
                {
                    BucketName = _awsOptions.BucketName,
                    CannedACL = S3CannedACL.PublicRead,
                    Key = filePath,
                    InputStream = stream,
                };
                putRequest.Headers.CacheControl = "max-age=31556926";

                PutObjectResponse response = client.PutObjectAsync(putRequest).Result;
            }
            catch (AmazonS3Exception amazonS3Exception)
            {
                if (amazonS3Exception.ErrorCode != null &&
                    (amazonS3Exception.ErrorCode.Equals("InvalidAccessKeyId")
                    ||
                    amazonS3Exception.ErrorCode.Equals("InvalidSecurity")))
                {
                    throw new Exception("Check the provided AWS Credentials.");
                }
                else
                {
                    throw new Exception("Error occurred: " + amazonS3Exception.Message);
                }
            }
        }

        public async Task Delete(string fileName)
        {
            using var client = new AmazonS3Client(_awsOptions.AccessKey, _awsOptions.SecretKey, Amazon.RegionEndpoint.GetBySystemName(_awsOptions.RegionName));
            try
            {
                DeleteObjectRequest deleteRequest = new DeleteObjectRequest
                {
                    BucketName = _awsOptions.BucketName,
                    Key = fileName.TrimStart('/')
                };

                _ = await client.DeleteObjectAsync(deleteRequest);
            }
            catch (AmazonS3Exception amazonS3Exception)
            {
                if (amazonS3Exception.ErrorCode != null &&
                    (amazonS3Exception.ErrorCode.Equals("InvalidAccessKeyId")
                    ||
                    amazonS3Exception.ErrorCode.Equals("InvalidSecurity")))
                {
                    throw new Exception("Check the provided AWS Credentials.");
                }
                else
                {
                    throw new Exception("Error occurred: " + amazonS3Exception.Message);
                }
            }
        }
    }
}
