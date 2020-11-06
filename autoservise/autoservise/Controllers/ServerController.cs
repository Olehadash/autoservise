using autoservise.Models;
using autoservise.Xaml;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using Rg.Plugins.Popup.Services;
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
        private static ServerController _instance = new ServerController();

        LoadingPopupPage loading = new LoadingPopupPage();

        UserModel usermodel = UserModel.Instance();

        MultipartFormDataContent content;

        SuckessDelegate suckess;
        ErorDelegate error;

        public bool ServerResult = false;

        string j_resul = null;
        bool isPreloader = true;

        private string host = "https://xn--80aealq7apged.su/api/v1/";

        static internal ServerController GetInstance
        {
            get {
                return _instance;
            }
        }

        public void SwitchOfpreloader()
        {
            isPreloader = false;
        }

        public async Task SendGetRequst(string path, bool IsOnlyBackGround = false, bool isHeader = false)
        {
            using (var client = new HttpClient())
            {
                Console.WriteLine("Send Get Request");
                client.BaseAddress = new Uri(host);

                if(!IsOnlyBackGround)
                    await PopupNavigation.PushAsync(loading);
                HttpResponseMessage response = await client.GetAsync(path);

                if (isHeader)
                {
                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                    client.DefaultRequestHeaders.Add("Authorization", usermodel.user.token_type + " " + usermodel.user.access_token);
                    Console.WriteLine("Header Seted");
                }

                string responseBody = await response.Content.ReadAsStringAsync();

                Console.WriteLine("Result: "+ responseBody);
                j_resul = responseBody;
                if (!IsOnlyBackGround)
                    PopupNavigation.RemovePageAsync(loading);
                ServerResult = response.IsSuccessStatusCode;
            }
        }

        public async Task sendPostRequest(string path, List<KeyValuePair<string,string>> form, bool isHeader = true)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(host);
                client.Timeout = TimeSpan.FromSeconds(5);
                Console.WriteLine("start send postRequest");
                if(isHeader)
                {
                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                    client.DefaultRequestHeaders.Add("Authorization", usermodel.user.token_type + " " + usermodel.user.access_token);
                    Console.WriteLine("Header Seted");
                }

                FormUrlEncodedContent content = new FormUrlEncodedContent(form);
                Console.WriteLine("Form Seted");
                var result = await client.PostAsync(path, content);
                Console.WriteLine("REquestGeted");
                if (result.IsSuccessStatusCode)
                {
                    Console.WriteLine(result);
                    if (isPreloader)
                        await PopupNavigation.PushAsync(loading);
                    string resultContent = await result.Content.ReadAsStringAsync();

                    j_resul = resultContent;
                    Console.WriteLine("Content seted");
                    if (isPreloader)
                        await PopupNavigation.RemovePageAsync(loading);
                    else
                        isPreloader = true;
                    ServerResult = result.IsSuccessStatusCode;
                }
            }
        }

        public async Task sendPostRequest(string path,  bool isHeader = true)
        {
            using (var client = new HttpClient())
            {
                ServerResult = false;
                client.BaseAddress = new Uri(host);
                client.Timeout = TimeSpan.FromSeconds(5);

                if (isHeader)
                {
                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                    client.DefaultRequestHeaders.Add("Authorization", usermodel.user.token_type + " " + usermodel.user.access_token);
                }

                var result = await client.PostAsync(path, content);

                j_resul = result.ToString();
                ServerResult = result.IsSuccessStatusCode;
            }
        }

        public void InitializeContent()
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
