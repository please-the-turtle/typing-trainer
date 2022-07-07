using System.Net;

namespace Parsing
{
    internal class HtmlLoader
    {
        readonly HttpClient client;
        readonly string url;

        public HtmlLoader(IParserSettings settings)
        {
            client = new HttpClient();
            url = $"{settings.BaseUrl}/{settings.Prefix}";
        }

        public async Task<string?> GetSourceByPageId(int id)
        {
            var currentUrl = url.Replace("{CurrentId}", id.ToString());
            HttpResponseMessage response = new();

            try
            {
                response = await client.GetAsync(currentUrl);
            }
            catch(HttpRequestException e)
            {
                return string.Empty;
            }
            string source = null!;

            if (response != null && response.StatusCode == HttpStatusCode.OK)
            {
                source = await response.Content.ReadAsStringAsync();
            }

            return source;
        }
    }
}