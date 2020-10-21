using autoservise.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

public delegate  void SuckessDelegate();
public delegate void ErorDelegate();

namespace autoservise.Controllers
{
    class ServerController
    {
        private static ServerController _instance = null;

        UserModel usermodel = UserModel.Instance();

        MultipartFormDataContent content;

        SuckessDelegate suckess;
        ErorDelegate error;

        string j_resul = null;

        private string host = "https://xn--80aealq7apged.su/api/v1/";

        static internal ServerController Instance()
        {
            if (_instance == null)
            {
                _instance = new ServerController();
                
            }

            return _instance;
        }

        public async Task SendGetRequst(string path, SuckessDelegate s, ErorDelegate e)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(host);



                HttpResponseMessage response = await client.GetAsync(path);
                string responseBody = await response.Content.ReadAsStringAsync();

                Console.WriteLine(responseBody);
                j_resul = responseBody;

                if (response.IsSuccessStatusCode)
                {
                    s();
                }
                else
                {
                    e();
                }
            }
        }

        public async Task sendPostRequest(string path, List<KeyValuePair<string,string>> form, bool isHeader = true)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(host);
                client.Timeout = TimeSpan.FromSeconds(5);

                if(isHeader)
                {
                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                    client.DefaultRequestHeaders.Add("Authorization", usermodel.user.token_type + " " + usermodel.user.access_token);
                }

                var content = new FormUrlEncodedContent(form);

                var result = await client.PostAsync(path, content);

                string resultContent = await result.Content.ReadAsStringAsync();
                j_resul = resultContent;
                Console.WriteLine("Content seted");

                if (result.IsSuccessStatusCode)
                {
                    
                    if (suckess != null) this.suckess();
                }
                else
                {
                    if (error != null) this.error();
                }
            }
        }

        public async Task sendPostRequest(string path, SuckessDelegate s, ErorDelegate e,  bool isHeader = true)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(host);
                client.Timeout = TimeSpan.FromSeconds(5);

                if (isHeader)
                {
                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                    client.DefaultRequestHeaders.Add("Authorization", usermodel.user.token_type + " " + usermodel.user.access_token);
                }

                var result = await client.PostAsync(path, content);

                if (result.IsSuccessStatusCode)
                {
                    j_resul = result.ToString();
                    s();
                }
                else
                {
                    e();
                }
            }
        }

        public void InitializaContent()
        {
            content = new MultipartFormDataContent();
        }

        public void AddStringData(string key, string value)
        {
            content.Add(new StringContent(value), key);
        }

        public void AddStringData(string key, byte[] imagedata, string path)
        {
            HttpContent stringContent = new ByteArrayContent(imagedata);
            content.Add(stringContent, key, path);
        }

        public void AddStringData(string key, List<byte[]> images)
        {
            var array = images.ToArray();
            var jsonArray = JsonConvert.SerializeObject(array);

            content.Add(new StringContent(jsonArray), key);
        }


        public string returnJsonResult()
        {
            return j_resul;
        }


        public void setsucksessdelegate(SuckessDelegate delegat)
        {
            this.suckess = delegat;
        }

        public void seterrordelegate(ErorDelegate delegat)
        {
            this.error = delegat;
        }
    }
}
