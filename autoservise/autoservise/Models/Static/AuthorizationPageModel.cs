using autoservise.Controllers;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace autoservise.Models.Static
{

    

    public enum InputTextViewerType
    {
        UserName,
        Password,
        Phone,
        CustomerName,
        OrganizationName,
        Email,
        ConfirmMail
    }
    public class InputTextViewModel
    {
        public string title;
        public string placeholder;
        public string source;
        public bool isPassword = false;

        public InputTextViewModel(string title, string placeholder, string source, bool isPassword)
        {
            this.title = title;
            this.placeholder = placeholder;
            this.source = source;
            this.isPassword = isPassword;
        }
    }

    class arrowbut
    {
        public string source { get; set; }
        public string title { get; set; }
        public string subtitle { get; set; }

        public arrowbut(string source, string title, string subtitle)
        {
            this.source = source;
            this.title = title;
            this.subtitle = subtitle;
        }
    }
    class AuthorizationPageModel
    {
        
        public List<arrowbut> buttons = new List<arrowbut>();
        public Dictionary<InputTextViewerType, InputTextViewModel> inputTextView = new Dictionary<InputTextViewerType, InputTextViewModel>();
        private static AuthorizationPageModel _instace = new AuthorizationPageModel();
        UserModel usermodel = UserModel.Instance();
        ServerController server = ServerController.GetInstance;

        static internal AuthorizationPageModel GetInstance
        {
            get
            {
                return _instace;
            }
        }
        public AuthorizationPageModel()
        {
            buttons.Add(new arrowbut("logico.png", "Войти как клиент ", "Чтобы найти специалиста "));
            buttons.Add(new arrowbut("logico2.png", "Войти как специалист ", "Чтобы предложить свои услуги"));
            buttons.Add(new arrowbut("logico3.png", "У меня уже есть аккаунт ", ""));

            inputTextView.Add (InputTextViewerType.UserName, new InputTextViewModel("Укажите имя *", "Имя", "username.png", false));
            inputTextView.Add(InputTextViewerType.Phone, new InputTextViewModel("Укажите номер телефона *", "+777", "Phone.png", false));
            inputTextView.Add(InputTextViewerType.Email, new InputTextViewModel("Укажите свой E-mail *", "mail", "Email.png", false));
            inputTextView.Add(InputTextViewerType.Password, new InputTextViewModel("Напишите пароль *", "Пароль", "keyimg.png", true));
            inputTextView.Add(InputTextViewerType.OrganizationName, new InputTextViewModel("Название организации", "Организация", "username.png", false));
            inputTextView.Add(InputTextViewerType.ConfirmMail, new InputTextViewModel("", "Код подтверждения", "", false));
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

            server.setsucksessdelegate(GotoMainFormPage);

            await server.sendPostRequest("auth/register", form, false);
        }

        public async void login()
        {
            List<KeyValuePair<string, string>> form = new List<KeyValuePair<string, string>>();


            form.Add(new KeyValuePair<string, string>("email", usermodel.user.email));
            form.Add(new KeyValuePair<string, string>("password", usermodel.user.password));

            await server.sendPostRequest("auth/login", form, false);
            if (server.ServerResult)
                logindata();
            else
                ErrorMessage();
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

        }

        public async Task GetCode()
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
