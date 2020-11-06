using autoservise.Controllers;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace autoservise.Models
{
    public struct Order
    {
        public int id { get; set; }
        public int executor_id { get; set; }
        public int category_id { get; set; }
        public int status { get; set; }
        public string description { get; set; }
        public string adres { get; set; }
        public string time { get; set; }
        public List<byte[]> images { get; set; }
        public bool show_contacts { get; set; }
        public int price { get; set; }
        public bool deal_price { get; set; }
        public int city_id { get; set; }
        public string category_name { get; set; }
        public string customer_name { get; set; }
        public int applications_count { get; set; }
        public string city { get; set; }
        public bool has_executor { get; set; }
        public bool is_new { get; set; }
        public bool is_ended { get; set; }
        public bool answered { get; set; }
        public bool is_you { get; set; }
        public int customer_id { get; set; }
    }
    class OrderModel
    {
        private static OrderModel _instance = new OrderModel();

        private ServerController server = ServerController.GetInstance;

        public Order order = new Order();

        List<Order> orders = new List<Order>();

        SuckessDelegate suckess;

        static internal OrderModel GetInstance
        {
            get
            {
                return _instance;
            }
        }

        public async Task SendData()
        {
            server.InitializeContent();
            server.AddStringData("description", this.order.description);
            server.AddStringData("address",string.IsNullOrEmpty(this.order.adres) ? "" : this.order.adres);
            server.AddStringData("time", string.IsNullOrEmpty(this.order.time) ? "" : this.order.time);
            server.AddStringData("show_contacts", this.order.show_contacts ? "True" : "False");
            server.AddStringData("price", string.IsNullOrEmpty(this.order.price.ToString()) ? "" : this.order.price.ToString());
            server.AddStringData("deal_price", this.order.deal_price ? "True" : "False");
            server.AddStringData("category_id", string.IsNullOrEmpty(this.order.category_id.ToString()) ? "" : this.order.category_id.ToString());

            if (order.images != null && order.images.Count>0)
            {
                server.AddStringData("images", order.images);
            }


            await server.sendPostRequest("orders/store", true);
        }

        public List<Order> getListOrders()
        {
            return orders;
        }

        public async Task GetAllOrders()
        {
            await server.SendGetRequst("categories/1/show", false, true);
            if(server.ServerResult)
            {
                JObject json = JObject.Parse(server.returnJsonResult());
                Console.WriteLine(server.returnJsonResult());
                int max = json["data"]["orders"].Count();
                int i = 0;
                orders.Clear();
                for (i = 0; i < max; i++)
                {
                    Order order = new Order();
                    order.id = (int) (json["data"]["orders"][i]["id"]);
                    order.executor_id = (int)(json["data"]["orders"][i]["executor_id"]);
                    order.category_id = (int)(json["data"]["orders"][i]["category_id"]);
                    order.status = (int)(json["data"]["orders"][i]["status"]);
                    order.description = json["data"]["orders"][i]["description"].ToString();
                    order.adres = json["data"]["orders"][i]["address"].ToString();
                    order.time = json["data"]["orders"][i]["time"].ToString();
                    order.city_id = (int)(json["data"]["orders"][i]["city_id"]);
                    order.price = (int)(json["data"]["orders"][i]["price"]);
                    order.customer_id = (int)(json["data"]["orders"][i]["customer_id"]);
                    order.category_name = json["data"]["orders"][i]["category_name"].ToString();
                    order.customer_name = json["data"]["orders"][i]["customer_name"].ToString();
                    order.applications_count = (int)(json["data"]["orders"][i]["applications_count"]);
                    order.city = json["data"]["orders"][i]["city"].ToString();
                    order.is_new = (bool)(json["data"]["orders"][i]["is_new"]);
                    order.is_ended = (bool)(json["data"]["orders"][i]["is_ended"]);
                    order.answered = (bool)(json["data"]["orders"][i]["answered"]);
                    order.is_you = (bool)(json["data"]["orders"][i]["is_you"]);

                    orders.Add(order);
                }
            }
            
        }

        public void setImageData(List<byte[]> images)
        {
            order.images = images;
        }

        void logindata()
        {
            suckess();
        }
    }
}
