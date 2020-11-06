using autoservise.Controllers;
using autoservise.MainUI;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace autoservise.Models.Static
{
    
    class AuthorizationPageModel
    {
        
        
        private static AuthorizationPageModel _instace = new AuthorizationPageModel();
        UserModel usermodel = UserModel.Instance();
        ServerController server = ServerController.GetInstance;
        CachPreferens cache = CachPreferens.GetInstance;

        static internal AuthorizationPageModel GetInstance
        {
            get
            {
                return _instace;
            }
        }
        public AuthorizationPageModel()
        {
            
        }

        public void Autorize()
        {

        }

        public async Task CreateUser()
        {
            List<KeyValuePair<string, string>> form = new List<KeyValuePair<string, string>>();

            form.Add(new KeyValuePair<string, string>("email", usermodel.user.email));
            form.Add(new KeyValuePair<string, string>("password", usermodel.user.password));
            if (usermodel.getType() == UserType.CASTOMER)
                form.Add(new KeyValuePair<string, string>("name", usermodel.user.name));
            else if(usermodel.getType() == UserType.EXECUTOR)
                form.Add(new KeyValuePair<string, string>("name", usermodel.user.organization_name));

            form.Add(new KeyValuePair<string, string>("type", usermodel.user.user_type));
            form.Add(new KeyValuePair<string, string>("phone", usermodel.user.phone));

            await server.sendPostRequest("auth/register", form, false);
        }

        public async Task ResetPassword()
        {
            List<KeyValuePair<string, string>> form = new List<KeyValuePair<string, string>>();

            form.Add(new KeyValuePair<string, string>("email", usermodel.user.email));
            form.Add(new KeyValuePair<string, string>("code", usermodel.user.code));
            form.Add(new KeyValuePair<string, string>("password", usermodel.user.password));

            await server.sendPostRequest("auth/verify", form, false);
        }

        public async Task login()
        {
            List<KeyValuePair<string, string>> form = new List<KeyValuePair<string, string>>();

            Console.WriteLine("email " + usermodel.user.email);
            Console.WriteLine("password " + usermodel.user.password);

            form.Add(new KeyValuePair<string, string>("email", usermodel.user.email));
            form.Add(new KeyValuePair<string, string>("password", usermodel.user.password));

            await server.sendPostRequest("auth/login", form, false);
            if (server.ServerResult)
                logindata();
        }

        public async void logindata()
        {
            var jsonString = server.returnJsonResult();
            var json = JObject.Parse(jsonString);
            Console.WriteLine(json["data"]["name"]);
            usermodel.user.id = (int)json["data"]["id"];
            usermodel.user.name = json["data"]["name"].ToString();
            usermodel.user.email = json["data"]["email"].ToString();
            usermodel.user.phone = json["data"]["phone"].ToString();
            usermodel.user.avatar = json["data"]["avatar"].ToString();
            usermodel.user.logo = json["data"]["logo"].ToString();
            usermodel.user.organization_name = json["data"]["organization_name"].ToString();
            usermodel.user.about = json["data"]["about"].ToString();
            usermodel.user.category_id = (int)json["data"]["category_id"];
            usermodel.user.city_id = (int)json["data"]["category_id"];
            usermodel.user.access_token = json["data"]["access_token"].ToString();
            usermodel.user.user_type = json["data"]["user_type"].ToString();
            usermodel.user.is_banned = (bool)json["data"]["is_banned"];
            usermodel.user.token_type = json["data"]["token_type"].ToString();
            usermodel.user.has_category = (bool)json["data"]["has_category"];
            usermodel.user.has_city = (bool)json["data"]["has_city"];
            usermodel.user.has_order = (bool)json["data"]["has_order"];
            usermodel.user.is_active = (bool)json["data"]["is_active"];
            usermodel.user.plan_date = json["data"]["plan_date"].ToString();
            usermodel.user.is_verified = (bool)json["data"]["is_verified"];
            usermodel.user.expires_at = json["data"]["expires_at"].ToString();
            GotoMainFormPage();
            usermodel.SeveData();

        }

        public async Task GetCode()
        {
            List<KeyValuePair<string, string>> form = new List<KeyValuePair<string, string>>();

            form.Add(new KeyValuePair<string, string>("email", usermodel.user.email));

            await server.sendPostRequest("auth/reset", form, false);
            if (!server.ServerResult)
                ErrorMessage();
        }

        public async Task GetCodeAgain()
        {
            List<KeyValuePair<string, string>> form = new List<KeyValuePair<string, string>>();

            form.Add(new KeyValuePair<string, string>("email", usermodel.user.email));

            await server.sendPostRequest("auth/verify/resend", form, false);
            if (!server.ServerResult)
                ErrorMessage();
        }

        public async Task SendCode()
        {
            List<KeyValuePair<string, string>> form = new List<KeyValuePair<string, string>>();

            form.Add(new KeyValuePair<string, string>("email", usermodel.user.email));
            form.Add(new KeyValuePair<string, string>("code", usermodel.user.code));

            await server.sendPostRequest("auth/verify", form, false);
        }

        void GotoMainFormPage()
        {
            //Go to Main Form Page
        }

        public async Task ErrorMessage()
        {
             
        }

        

    }
}
