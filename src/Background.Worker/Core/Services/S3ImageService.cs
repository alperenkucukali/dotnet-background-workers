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

        public Task Delete(string fileName)
        {
            throw new NotImplementedException();
        }

        public string Upload(byte[] imageDataByteArray, string fileName)
        {
            throw new NotImplementedException();
        }

        public string Upload(string base64string, string fileName)
        {
            throw new NotImplementedException();
        }
    }
}
