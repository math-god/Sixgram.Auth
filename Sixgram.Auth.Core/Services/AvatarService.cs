using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Linq;
using Sixgram.Auth.Common.Types;
using Sixgram.Auth.Core.Dto.File;
using Sixgram.Auth.Core.File;
using Sixgram.Auth.Core.Http;

namespace Sixgram.Auth.Core.Services;

public class AvatarService : IFileService
{
    private static readonly string[] Extensions = {"jpg", "png", "jpeg"};

    private readonly IFileHttpService _fileHttpService;

    public AvatarService
    (
        IFileHttpService fileHttpService
    )
    {
        _fileHttpService = fileHttpService;
    }

    public async Task<Guid?> Send(IFormFile file, Guid userId)
    {
        if (!CheckExtension(file))
        {
            return null;
        }

        byte[] data;
        using (var binaryReader = new BinaryReader(file.OpenReadStream()))
            data = binaryReader.ReadBytes((int) file.OpenReadStream().Length);

        var fileSendingDto = new FileSendingDto
        {
            SourceId = userId,
            UploadedFile = data,
            UploadedFileName = file.FileName,
            FileSource = FileSource.Avatar
        };

        var content = await _fileHttpService.SendRequest(fileSendingDto);

        if (content == null)
        {
            return null;
        }
        
        var result = new Guid(JObject.Parse(content)["id"].ToString());

        return result;
    }

    private static bool CheckExtension(IFormFile file)
        => Extensions.Contains(file.FileName.Split('.').Last());
}