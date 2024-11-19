namespace Xin_ToolKit.Utils;

internal class DownloadUtils
{
    public static async Task<bool> DownLoadAsync(string? uri, string localFileName)
    {
        var server = new Uri(uri!);
        var path = Path.GetDirectoryName(localFileName);
        if (path != null)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        var httpClient = new HttpClient();
        var responseMessage = await httpClient.GetAsync(server);
        if (responseMessage.IsSuccessStatusCode)
        {
            using var stream = File.Create(localFileName);
            using var streamFromService = await responseMessage.Content.ReadAsStreamAsync();
            await streamFromService.CopyToAsync(stream);
            return true;
        }
        else
        {
            return false;
        }
    }
}