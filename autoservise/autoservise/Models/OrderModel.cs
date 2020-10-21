using autoservise.Controllers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text;

namespace autoservise.Models
{
    struct Order
    {
        public string description { get; set; }
        public string adres { get; set; }
        public string time { get; set; }
        public List<byte[]> images { get; set; }
        public bool show_contacts { get; set; }
        public int price { get; set; }
        public bool deal_price { get; set; }
        public int category_id { get; set; }
    }
    class OrderModel
    {
        private static OrderModel _instance = new OrderModel();

        private ServerController server = ServerController.Instance();

        public Order oreder = new Order();

        SuckessDelegate suckess;

        static internal OrderModel GetInstance
        {
            get
            {
                return _instance;
            }
        }

        public async void SendData(SuckessDelegate s, ErorDelegate e)
        {
            server.InitializaContent();
            server.AddStringData("description", this.oreder.description);
            server.AddStringData("address", this.oreder.adres);
            server.AddStringData("time", this.oreder.time);
            server.AddStringData("show_contacts", this.oreder.show_contacts ? "True" : "False");
            server.AddStringData("price", this.oreder.price.ToString());
            server.AddStringData("deal_price", this.oreder.deal_price ? "True" : "False");
            server.AddStringData("category_id", this.oreder.category_id.ToString());

            if (oreder.images.Count>0)
            {
                server.AddStringData("images", oreder.images);
            }

            this.suckess = s;

            await server.sendPostRequest("orders/store", logindata, e);
        }

        public void setImageData(List<byte[]> images)
        {
            oreder.images = images;
        }

        void logindata()
        {
            suckess();
        }
    }
}
