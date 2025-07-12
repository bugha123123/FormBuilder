// Services/DropboxService.cs
using Dropbox.Api;
using Dropbox.Api.Files;
using FormBuilder.Interface;
using System.IO;
using System.Text;
using System.Threading.Tasks;

public class DropboxService : IDropboxService
{
    private readonly string _accessToken;

    public DropboxService(string accessToken)
    {
        _accessToken = accessToken;
    }

    public async Task<bool> UploadJsonFileAsync(string fileName, string jsonContent)
    {
        try
        {
            using var dbx = new DropboxClient(_accessToken);

            using var memStream = new MemoryStream(Encoding.UTF8.GetBytes(jsonContent));

            await dbx.Files.UploadAsync(
                path: $"/Support/{fileName}",
                mode: WriteMode.Overwrite.Instance,
                body: memStream);

            return true;
        }
        catch
        {
            // Log error here
            return false;
        }
    }
}
