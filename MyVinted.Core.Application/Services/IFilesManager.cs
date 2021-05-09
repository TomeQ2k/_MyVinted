using Microsoft.AspNetCore.Http;
using MyVinted.Core.Application.Services.ReadOnly;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyVinted.Core.Application.Models;

namespace MyVinted.Core.Application.Services
{
    public interface IFilesManager : IReadOnlyFilesManager
    {
        Task<FileModel> Upload(IFormFile file, string filePath = null);
        Task<IList<FileModel>> Upload(IEnumerable<IFormFile> files, string filePath = null);

        void Delete(string path);
        void DeleteDirectory(string path, bool isRecursive = true);
    }
}