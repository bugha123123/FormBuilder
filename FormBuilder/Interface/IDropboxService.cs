namespace FormBuilder.Interface
{

    public interface IDropboxService
    {
        Task<bool> UploadJsonFileAsync(string fileName, string jsonContent);
    }

}
