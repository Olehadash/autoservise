using autoservise.Controllers;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
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
        CachPreferens cache = CachPreferens.GetInstance;

        public bool is_data_seted = false;

        public ServerController server = ServerController.GetInstance;

        ErorDelegate errordelegate;

        static internal DataModel GetInstance
        {
            get
            {
                return _instance;
            }
        }

        public async void GetData( ErorDelegate error)
        {
            if (is_data_seted)
            {
                return;
            }
            errordelegate = error;

            await server.SendGetRequst("data", true);
            if (server.ServerResult)
                LoadData(server.returnJsonResult());
            else
                Error();
        }

        public void Error()
        {
            if(cache.HasKey("data"))
            {
                LoadData(cache.GetString("data"));
            }
        }

        public void SaweData(string jsonstring)
        {
            cache.SetKey("data", jsonstring);
        }

        public void LoadData(string jsonstring)
        {
            
            SaweData(jsonstring);
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
                city.name = json["data"]["cities"][i]["name"].ToString();

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
            
        }


    }
}
