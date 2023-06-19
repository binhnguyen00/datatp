using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace datatp {
  public class DatatpHttpClient {
    private readonly HttpClient httpClient;
    private string baseRestUrl;

    public DatatpHttpClient(string baseRestUrl) {
      this.httpClient = new HttpClient();
      this.baseRestUrl = baseRestUrl;
    }

    public async Task<string> Get(string path) {
      try {
        string url = baseRestUrl + path;
        HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, url);
        Dictionary<string, string> headers = createHeaders();
        if (headers != null) {
          foreach (var header in headers) {
            request.Headers.TryAddWithoutValidation(header.Key, header.Value);
          }
        }
        HttpResponseMessage response = await httpClient.SendAsync(request);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync();
      } catch (Exception ex) {
        // Handle any exceptions
        Console.WriteLine($"Error: {ex.Message}");
        return string.Empty;
      }
    }

    public async Task<string> Post(string path, string body = null) {
      try {
        string url = baseRestUrl + path;
        HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, url);
        Dictionary<string, string> headers = createHeaders();
        if (headers != null) {
          foreach (var header in headers) {
            request.Headers.TryAddWithoutValidation(header.Key, header.Value);
          }
        }
        if (!string.IsNullOrEmpty(body)) {
          request.Content = new StringContent(body, Encoding.UTF8, "application/json");
        }
        HttpResponseMessage response = await httpClient.SendAsync(request);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync();
      } catch (Exception ex) {
        // Handle any exceptions
        Console.WriteLine($"Error: {ex.Message}");
        return string.Empty;
      }
    }

    public Dictionary<string, string> createHeaders() {
      Dictionary<string, string> headers = new Dictionary<string, string> {
        { "Content-Type", "application/json" },
        { "Accept-Charset", "UTF-8" },
        { "Accept", "*/*" },
      };
      return headers;
    }
  }
}
