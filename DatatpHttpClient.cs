using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace datatp {
  public class DatatpHttpClient {
    private HttpClient httpClient;
    private string baseRestUrl;
    public string accessToken;

    public DatatpHttpClient(string baseRestUrl) {
      this.httpClient = new HttpClient();
      this.baseRestUrl = baseRestUrl;
    }

    public async Task<string> Get(string path) {
      try {
        string url = baseRestUrl + path;
        HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, url);
        Dictionary<string, string> headers = createHeaders(accessToken);
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

    public HttpResponseMessage Post(string path, string body = null) {
      try {
        string url = baseRestUrl + path;
        HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, url);
        Dictionary<string, string> headers = createHeaders(accessToken);
        if (headers != null) {
          foreach (var header in headers) {
            request.Headers.TryAddWithoutValidation(header.Key, header.Value);
          }
        }
        if (!string.IsNullOrEmpty(body)) {
          request.Content = new StringContent(body, Encoding.UTF8, "application/json");
        }
        HttpResponseMessage response = httpClient.SendAsync(request).GetAwaiter().GetResult();
        response.EnsureSuccessStatusCode();
        return response;
      } catch (Exception ex) {
        // Handle any exceptions
        MessageBox.Show($"Error: {ex.Message}", "Error");
        return null;
      }
    }


    public HttpResponseMessage Put(string path, string body = null) {
      try {
        string url = baseRestUrl + path;
        HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Put, url);
        Dictionary<string, string> headers = createHeaders(accessToken);
        if (headers != null) {
          foreach (var header in headers) {
            request.Headers.TryAddWithoutValidation(header.Key, header.Value);
          }
        }
        if (!string.IsNullOrEmpty(body)) {
          request.Content = new StringContent(body, Encoding.UTF8, "application/json");
        }
        HttpResponseMessage response = httpClient.SendAsync(request).GetAwaiter().GetResult();
        response.EnsureSuccessStatusCode();
        return response;
      } catch (Exception ex) {
        // Handle any exceptions
        MessageBox.Show($"Error: {ex.Message}", "Error");
        return null;
      }
    }

    public Dictionary<string, string> createHeaders(string accessToken = null) {
      Dictionary<string, string> headers = new Dictionary<string, string> {
        { "Content-Type", "application/json" },
        { "Accept-Charset", "UTF-8" },
        { "Accept", "*/*" },
      };
      if (accessToken != null) {
        headers.Add(
          "Datatp-Authorization", accessToken 
        );  
      }
      return headers;
    }

    public JObject GetResponseResultAsObject(Task<string> task = null) {
      JObject jsonObject = null;
      if (task != null) {
        jsonObject = JsonConvert.DeserializeObject<JObject>(task.Result);
      }
      return jsonObject;
    }
  }
}
