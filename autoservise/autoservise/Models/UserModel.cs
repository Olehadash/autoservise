using autoservise.Controllers;
using autoservise.Models.Static;
using System;
using System.Collections.Generic;
using System.Text;

struct User
{
    public int id { get; set; }
    public string name { get; set; }
    public string password { get; set; }
    public string email { get; set; }
    public string phone { get; set; }
    public string avatar { get; set; }
    public string logo { get; set; }
    public string organization_name { get; set; }
    public string about { get; set; }
    public int category_id { get; set; }
    public int city_id { get; set; }
    public string access_token { get; set; }
    public string user_type { get; set; }
    public bool is_banned { get; set; }
    public string token_type { get; set; }
    public bool has_category { get; set; }
    public bool has_city { get; set; }
    public bool has_order { get; set; }
    public bool is_active { get; set; }
    public string plan_date { get; set; }
    public bool is_verified { get; set; }
    public string expires_at { get; set; }
    public string code { get; set; }
}

namespace autoservise.Models
{
    public enum UserType
    {
        NONE     = 0,
        CASTOMER = 1,
        EXECUTOR = 2

    }

    class UserModel
    {

        private static UserModel _instance = null;

        public User user;
        static internal UserModel Instance()
        {
            if (_instance == null)
            {
                _instance = new UserModel();

            }

            return _instance;
        }

        public void setUserType(UserType type)
        {
            switch (type)
            {
                case UserType.NONE:
                    user.user_type = "";
                    break;
                case UserType.CASTOMER:
                    user.user_type = "customer";
                    break;
                case UserType.EXECUTOR:
                    user.user_type = "executor";
                    break;
            }
        }

        public UserType getType()
        {
            switch (user.user_type)
            {
                case "":
                    return UserType.NONE;
                    break;
                case "customer":
                    return UserType.CASTOMER;
                    break;
                case "executor":
                    return UserType.EXECUTOR;
                    break;
                default:
                    return UserType.NONE;
                    break;

            }
        }

        public PageType getPrelog()
        {
            switch (user.user_type)
            {
                case "":
                    return PageType.Authorization;
                    break;
                case "customer":
                    return PageType.CreateCustomer;
                    break;
                case "executor":
                    return PageType.CreateExecutor;
                    break;
                default:
                    return PageType.None;
                    break;

            }
        }
    }
}
