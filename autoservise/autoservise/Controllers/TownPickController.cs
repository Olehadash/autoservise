using autoservise.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace autoservise.Controllers
{
    class TownPickController
    {
        private static TownPickController _instance = new TownPickController();

        DataModel dataModel = DataModel.GetInstance;

        

        static internal TownPickController GetInstance
        {
            get
            {
                return _instance;
            }
        }

        public async void GetData(SuckessDelegate delegat_s, ErorDelegate error)
        {
            //dataModel.GetData();
        }

    }
}
