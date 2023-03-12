namespace CandidateTesting.RafaelSouza.Infrastructure
{
    public class FileLog 
    {
        public static string GetFile(string address)
        {
            Uri uri = new Uri(address);

            using var httpClient = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, uri);
            var response = httpClient.Send(request);
            using var reader = new StreamReader(response.Content.ReadAsStream());
            var responseBody = reader.ReadToEnd();
            return responseBody;
        }

        public static void CreateLocalFile(string localPath, string content)
        {
            File.WriteAllText(localPath, content);
        }
    }
}
