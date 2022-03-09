using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Sixgram.Auth.Core.File;

public interface IFileService
{
    Task<Guid?> Send(IFormFile file, Guid sourceId);
}