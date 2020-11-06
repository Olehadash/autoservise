using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace autoservise.Controllers
{
    public enum PageType
    {
        None,
        CreateCustomer,
        CreateExecutor,
        Authorization,
        CityPick,
        ConfirmMail,
        ForgetPassword,
        Categories,
        ResetPassword,
        ExecutorInfo,
        CategoryFirstPanel,
        CategorySecondPanel,
        CategoryTreedPanel,
        CategoryFourPanel,
        PublishOrder,
        AddAdressOrder,
        AddDateOrder,
        AddBudgetOrder,
        Gallery,
        MyOrderList,
        OrderMessanger,
        MainPage


    }
    public struct PageOperationDelegates
    {
        public Func<Task> open;
        public Func<Task> close;
    }
    class BreadScribe
    {
        private static BreadScribe _instace = new BreadScribe();

        List<PageType> stek = new List<PageType>();
        Dictionary<PageType, PageOperationDelegates> pageOperation = new Dictionary<PageType, PageOperationDelegates>();

        static internal BreadScribe GetInstance
        {
            get
            {
                return _instace;
            }
        }

        public void Cancel()
        {
            stek.RemoveAt(stek.Count - 1);
        }

        public void SetFirstPage(PageType type)
        {
            stek.Clear();
            stek.Add(type);
        }
        public async Task NextPage(PageType type)
        {
            if (stek.Count < 1)
                return;
            stek.Add(type);
            await pageOperation[stek[stek.Count - 2]].close();
            await pageOperation[type].open();
            
        }

        public async Task PreviousPage()
        {
            if (stek.Count < 2)
                return;
            await pageOperation[stek[stek.Count - 1]].close();
            await pageOperation[stek[stek.Count - 2]].open();
            stek.RemoveAt(stek.Count-1);

        }
        public async Task GoToPage(int index)
        {
            if (stek.Count < 2)
                return;
            await pageOperation[stek[stek.Count - 1]].close();
            await pageOperation[stek[index]].open();

            for(int i = stek.Count - 1; i> index; i--)
                stek.RemoveAt(i);

        }

        public void RegistratePage(PageType type, Func<Task> open, Func<Task> close)
        {
            if (pageOperation.ContainsKey(type))
                return;
            PageOperationDelegates pageOperationDelegates = new PageOperationDelegates();
            pageOperationDelegates.open = open;
            pageOperationDelegates.close = close;
            pageOperation.Add(type, pageOperationDelegates);
        }

        public PageType GetPageType(int index)
        {
            return stek[index];
        }

        public PageType GetFirstPageType()
        {
            return stek[0];
        }
        
        public PageType GetLastElement()
        {
            return stek[stek.Count-1];
        }
    }
}
