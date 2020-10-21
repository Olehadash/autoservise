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
}

namespace autoservise.Models
{

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
    }
}
