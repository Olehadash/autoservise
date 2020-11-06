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
    public bool is_Login { get; set; }
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
        private CachPreferens cache = CachPreferens.GetInstance;

        public User user;
        static internal UserModel Instance()
        {
            if (_instance == null)
            {
                _instance = new UserModel();

            }

            return _instance;
        }

        public void SeveData()
        {
            cache.SetKey("user_id", user.id.ToString());
            cache.SetKey("user_name", user.name);
            cache.SetKey("user_password", user.password);
            cache.SetKey("user_email", user.email);
            cache.SetKey("user_phone", user.phone.ToString());
            cache.SetKey("user_avatar", user.avatar.ToString());
            cache.SetKey("user_logo", user.logo.ToString());
            cache.SetKey("user_organization_name", user.organization_name.ToString());
            cache.SetKey("user_about", user.about.ToString());
            cache.SetKey("user_category_id", user.category_id.ToString());
            cache.SetKey("user_city_id", user.city_id.ToString());
            cache.SetKey("user_access_token", user.access_token.ToString());
            cache.SetKey("user_user_type", user.user_type.ToString());
            cache.SetKey("user_is_banned", user.is_banned.ToString());
            cache.SetKey("user_token_type", user.token_type.ToString());
            cache.SetKey("user_has_category", user.has_category.ToString());
            cache.SetKey("user_has_city", user.has_city.ToString());
            cache.SetKey("user_has_order", user.has_order.ToString());
            cache.SetKey("user_is_active", user.is_active.ToString());
            cache.SetKey("user_plan_date", user.plan_date.ToString());
            cache.SetKey("user_is_verified", user.is_verified.ToString());
            cache.SetKey("user_expires_at", user.expires_at.ToString());
    }

        public void LoadData()
        {
            if (!cache.HasKey("user_id"))
                return;
            user.id = cache.GetInt("user_id");
            user.name = cache.GetString("user_name");
            user.password = cache.GetString("user_password");
            user.email = cache.GetString("user_email");
            user.phone = cache.GetString("user_phone");
            user.avatar = cache.GetString("user_avatar");
            user.logo = cache.GetString("user_logo");
            user.organization_name = cache.GetString("user_organization_name");
            user.about = cache.GetString("user_about");
            user.category_id = cache.GetInt("user_category_id");
            user.city_id = cache.GetInt("user_city_id");
            user.access_token = cache.GetString("user_access_token");
            user.user_type = cache.GetString("user_user_type");
            user.is_banned = cache.GetBool("user_is_banned");
            user.token_type = cache.GetString("user_token_type");
            user.has_category = cache.GetBool("user_has_category");
            user.has_city = cache.GetBool("user_has_city");
            user.has_order = cache.GetBool("user_has_order");
            user.is_active = cache.GetBool("user_is_active");
            user.plan_date = cache.GetString("user_plan_date");
            user.is_verified = cache.GetBool("user_is_verified");
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
