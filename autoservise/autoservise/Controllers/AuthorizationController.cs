using autoservise.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

public delegate void Switchdelegate();

namespace autoservise.Controllers
{
    class AuthorizationController
    {
        private static AuthorizationController _instance = null;

        ServerController server = ServerController.GetInstance;
        UserModel usermodel = UserModel.Instance();

        Switchdelegate success;

        static internal AuthorizationController Instance()
        {
            if (_instance == null)
            {
                _instance = new AuthorizationController();

            }

            return _instance;
        }


        public async void login (Switchdelegate success, ErorDelegate error)
        {
            List<KeyValuePair<string, string>> form = new List<KeyValuePair<string, string>>();


            form.Add(new KeyValuePair<string, string>("email", usermodel.user.email));
            form.Add(new KeyValuePair<string, string>("password", usermodel.user.password));

            this.success = success;

            server.setsucksessdelegate(logindata);
            server.seterrordelegate(error);

            await server.sendPostRequest("auth/login", form, false);
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
            this.success();

        }
    }
}
