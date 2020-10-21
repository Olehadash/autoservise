using autoservise.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace autoservise.Controllers
{
    class ConfirmMailController
    {
        private static ConfirmMailController _instance = null;
        UserModel usermodel = UserModel.Instance();
        ServerController server = ServerController.Instance();


        static internal ConfirmMailController Instance()
        {
            if (_instance == null)
            {
                _instance = new ConfirmMailController();

            }

            return _instance;
        }

        public async void GetCode(SuckessDelegate success, ErorDelegate error)
        {
            List<KeyValuePair<string, string>> form = new List<KeyValuePair<string, string>>();

            form.Add(new KeyValuePair<string, string>("email", usermodel.user.email));

            server.setsucksessdelegate(success);
            server.seterrordelegate(error);

            await server.sendPostRequest("auth/verify/resend", form, false);
        }

        public async void SendCode(string code, SuckessDelegate success, ErorDelegate error)
        {
            List<KeyValuePair<string, string>> form = new List<KeyValuePair<string, string>>();

            form.Add(new KeyValuePair<string, string>("email", usermodel.user.email));
            form.Add(new KeyValuePair<string, string>("code", code));

            server.setsucksessdelegate(success);
            server.seterrordelegate(error);

            await server.sendPostRequest("auth/verify", form, false);
        }
    }
}
