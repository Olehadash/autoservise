using autoservise.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace autoservise.Controllers
{
    class MainPageController
    {
        private static MainPageController _instace = new MainPageController();
        DataModel data = DataModel.GetInstance;

        public List<Categories>[] categoties = new List<Categories>[5];
        int[] numbers = new int[4] { 0, 1, 4, 18 };
        

        static internal MainPageController GetInstance
        {
            get
            {
                return _instace;
            }
        }

        public bool isMulticotegoty(int id)
        {
            for(int i =0; i<numbers.Length;i++)
            {
                if (id == numbers[i])
                    return true;
            }
            return false;
        }

        public MainPageController()
        {
            
        }

        public int GetIndex(int id)
        {
            for (int i = 0; i <numbers.Length; i++)
            {
                if (numbers[i] == id)
                    return i;
            }
            return -1;
        }

        public void GetData()
        {
            int i = 0, j = 0;

            for (i = 0; i < numbers.Length; i++)
            {
                categoties[i] = new List<Categories>();
            }

                for (j = 0; j < data.categories.Count; j++)
            {
                for (i = 0; i < numbers.Length; i++)
                {
                    
                    if (data.categories[j].parent_id == numbers[i])
                    {
                        categoties[i].Add(data.categories[j]);
                    }
                }
            }
        }

        public List<Categories> GetSubCategory(int index)
        {
            return categoties[numbers[index]];
        }

        
    }
}
