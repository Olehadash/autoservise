using autoservise.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace autoservise.Controllers
{
    class CreateUserController
    {
        private static CreateUserController _instance = null;
        UserModel usermodel = UserModel.Instance();
        ServerController server = ServerController.Instance();
        CreateUserModel gum = CreateUserModel.Instance();

        static internal CreateUserController Instance()
        {
            if (_instance == null)
            {
                _instance = new CreateUserController();

            }

            return _instance;
        }

        public async void CreateUser(SuckessDelegate success, ErorDelegate error)
        {
            List<KeyValuePair<string, string>> form = new List<KeyValuePair<string, string>>();


            form.Add(new KeyValuePair<string, string>("email", usermodel.user.email));
            form.Add(new KeyValuePair<string, string>("password", usermodel.user.password));
            if(gum._isConsumer)
                form.Add(new KeyValuePair<string, string>("name", usermodel.user.name));
            else
                form.Add(new KeyValuePair<string, string>("name", usermodel.user.organization_name));

            form.Add(new KeyValuePair<string, string>("type", usermodel.user.user_type));
            form.Add(new KeyValuePair<string, string>("phone", usermodel.user.phone));

            server.setsucksessdelegate(success);
            server.seterrordelegate(error);

            await server.sendPostRequest("auth/register", form, false);
        }
    }
}
