using System;
using Sixgram.Auth.Common.Types;

namespace Sixgram.Auth.Core.Dto.File;

public class FileSendingDto
{
    public Guid SourceId { get; set; }
    public byte[] UploadedFile { get; set; }
    public string UploadedFileName { get; set; }
    public FileSource FileSource { get; set; }
}