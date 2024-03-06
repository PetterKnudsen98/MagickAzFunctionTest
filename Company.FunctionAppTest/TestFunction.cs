
using System;
using System.IO;
using System.Net.Security;
using System.Threading.Tasks;
using ImageMagick;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;

namespace Company.FunctionAppTest;

public class TestFunction
{
    [FunctionName(nameof(Test))]
    public async Task<IActionResult> Test([HttpTrigger(AuthorizationLevel.Function, "get", Route = "test")] HttpRequest req)
    {
        var bytes = new byte[] { 1, 2, 3, 4 };
        using var inStream = new MemoryStream(bytes);

        var memoryStreamNoExif = new MemoryStream();
        using var image = new MagickImage(inStream);

        image.RemoveProfile("exif");

        await image.WriteAsync(memoryStreamNoExif);

        return new OkResult();
    }
}