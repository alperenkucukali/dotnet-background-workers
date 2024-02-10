using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Background.Worker.Core.Services.Interfaces
{
    public interface IImageService
    {
        string Upload(byte[] imageDataByteArray, string fileName);
        string Upload(string base64string, string fileName);
        Task Delete(string fileName);
    }
}
