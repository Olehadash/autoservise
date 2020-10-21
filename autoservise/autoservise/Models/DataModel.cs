using autoservise.Controllers;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace autoservise.Models
{

    struct Cities
    {
        public int id;
        public string name;
    }

    struct Categories
    {
        public int id;
        public string name;
        public string description;
        public int parent_id;
    }

    class DataModel
    {
        private static DataModel _instance = new DataModel();

        public List<Cities> cities = new List<Cities>();
        public List<Categories> categories = new List<Categories>();

        public bool is_data_seted = false;

        public ServerController server = ServerController.Instance();

        SuckessDelegate suckess;

        static internal DataModel GetInstance
        {
            get
            {
                return _instance;
            }
        }

        public async void GetData(SuckessDelegate delegat_s, ErorDelegate error)
        {
            if (is_data_seted)
            {
                delegat_s();
                return;
            }
            suckess = delegat_s;

            await server.SendGetRequst("data", LoadData, error);
        }

        public void LoadData()
        {
            string jsonstring = server.returnJsonResult();
            if (String.IsNullOrEmpty(jsonstring))
            {
                return;
            }
            var json = JObject.Parse(jsonstring);
            int cityMax = json["data"]["cities"].Count();
            int cotegoriesMax = json["data"]["categories"].Count();

            for(int i = 0; i<cityMax; i++)
            {
                Cities city = new Cities();
                city.id = (int)json["data"]["cities"][i]["id"];
                city.name = json["data"]["cities"][i]["id"].ToString();

                cities.Add(city);
            }

            for(int i = 0; i<cotegoriesMax; i++)
            {
                Categories categoty = new Categories();
                
                categoty.id = (int)json["data"]["categories"][i]["id"];
                categoty.name = json["data"]["categories"][i]["name"].ToString();
                categoty.description = json["data"]["categories"][i]["description"].ToString();
                categoty.parent_id = (int)json["data"]["categories"][i]["parent_id"];

                categories.Add(categoty);
            }

            is_data_seted = true;
            suckess();
        }


    }
}
