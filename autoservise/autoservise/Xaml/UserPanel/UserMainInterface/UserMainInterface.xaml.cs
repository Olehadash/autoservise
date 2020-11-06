using autoservise.Controllers;
using autoservise.MainUI;
using autoservise.Models;
using autoservise.Models.Static;
using autoservise.Xaml.UserPanel.UserMainInterface.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace autoservise.Xaml.UserPanel.UserMainInterface
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserMainInterface : ContentPage
    {
        AuthorizationPageModel auth = AuthorizationPageModel.GetInstance;
        MainPageController pagemode = MainPageController.GetInstance;
        AnimationController animation = AnimationController.GetInstance;
        BreadScribe breadscribe = BreadScribe.GetInstance;
        DataModel data = DataModel.GetInstance;
        UserModel usermodel = UserModel.Instance();
        ServerController server = ServerController.GetInstance;
        OrderModel order = OrderModel.GetInstance;

        StackLayout backButton;
        StackLayout content;
        StackLayout backcontent;
        Button bottomButton;
        ScrollView scroll;
        ToolBarUI toolbar;
        PageType pagetype;

        int buildCategorySwitcher = 0;

        public UserMainInterface()
        {
            InitializeComponent();
            
            
            breadscribe.SetFirstPage(PageType.CategoryFirstPanel);
            breadscribe.RegistratePage(PageType.CategoryFirstPanel, Show, Hide);
            breadscribe.RegistratePage(PageType.CategorySecondPanel, Show, Hide);
            breadscribe.RegistratePage(PageType.CategoryTreedPanel, Show, Hide);
            breadscribe.RegistratePage(PageType.CategoryFourPanel, Show, Hide);
            breadscribe.RegistratePage(PageType.PublishOrder, AddOrderShow, Hide);
            breadscribe.RegistratePage(PageType.AddAdressOrder, AddOrderAdressShow, Hide);
            breadscribe.RegistratePage(PageType.AddDateOrder, AddOrderDate, Hide);
            breadscribe.RegistratePage(PageType.AddBudgetOrder, AddOrderBudget, Hide);
            breadscribe.RegistratePage(PageType.MyOrderList, MyOrdersList, Hide);
            breadscribe.RegistratePage(PageType.OrderMessanger, OrderMessageShow, Hide);


            backButton = (StackLayout)this.FindByName("BackButton");
            content = (StackLayout)this.FindByName("Content");
            backcontent = (StackLayout)this.FindByName("BottomContent");
            bottomButton = (Button)this.FindByName("BottomButton");
            scroll = (ScrollView)this.FindByName("Scroll");
            toolbar = new ToolBarUI();
            toolbar.IsVisible = false;
            bottomButton.IsVisible = false;

            backcontent.Children.Add(toolbar);
            backButton.IsVisible = false;

            Build();


        }

        /*
         * Build First Page
         * */
        public async void Build()
        {
            await auth.login();
            if(server.ServerResult)
            {
                usermodel.SeveData();
                Console.WriteLine("data GetData");
                await data.GetData();
                pagemode.GetData();
                if (usermodel.user.user_type == "customer")
                {
                    pagetype = PageType.MyOrderList;
                    await MyOrdersList();
                    //await ShowTask();
                }
            }
            else
            {
                Error();
            }
        }

        /*
         * Set Order Category
         * Show - Hide - ButtonAction
         * */
        async Task Show()
        {
            backButton.IsVisible = !(pagetype == PageType.CategoryFirstPanel);

            if(pagetype == PageType.CategoryFirstPanel)
            {
                scroll.Margin = new Thickness(10, 100, 10, 10);
            }
            else
            {
                scroll.Margin = new Thickness(10, 10, 10, 10);
            }
            
            int i = 0;
            List<Categories> line = pagemode.categoties[pagemode.GetIndex(buildCategorySwitcher)];
            for (i = 0; i< line.Count; i++)
            {
                CatigogiesView view = new CatigogiesView();
                view.SetData(line[i]);
                view.SetDelegate(ContentButtonAction);
                view.Opacity = 0;
                content.Children.Add(view);
                animation.OpacityIn(view);
                await Task.Delay(50);
            }
            
        }

        async void ContentButtonAction(int id)
        {
            Console.WriteLine(id);
            if(pagemode.isMulticotegoty(id))
            {
                buildCategorySwitcher = id;
                switch(id)
                {
                    case 1:
                        pagetype = PageType.CategorySecondPanel;
                        break;
                    case 4:
                        pagetype = PageType.CategoryTreedPanel;
                        break;
                    case 18:
                        pagetype = PageType.CategoryTreedPanel;
                        break;
                }
                await breadscribe.NextPage(pagetype);
            }
            else
            {
                order.order.category_id = id;
                await breadscribe.NextPage(PageType.PublishOrder);
            }
        }

        async Task Hide()
        {
            for (int i = 0; i < content.Children.Count; i++)
            {
                animation.OpacityOut(content.Children[i]);
                await Task.Delay(50);
            }
            if(bottomButton.IsVisible)
            {
                await animation.OpacityIn(bottomButton);
            }
            content.Children.Clear();
        }

        /*
         * Add Order Page
         * */

        async Task AddOrderShow()
        {
            Console.WriteLine("PageType.PublishOrder");
            pagetype = PageType.PublishOrder;
            backButton.IsVisible = true;
            scroll.Margin = new Thickness(10, 10, 10, 10);

            bottomButton.IsVisible = true;
            bottomButton.IsEnabled = false;
            bottomButton.Text = "Опубликовать";

            var input= new InputTextViewer();
            input.SetData(InputTextViewerType.OrederDescripton);
            input.SetDelegate(SetBottomButtonActive);
            input.Opacity = 0;
            content.Children.Add(input);

            CutomButtonUI button = new CutomButtonUI();
            button.SetData(CustomUIViewerType.SetAdress);
            button.Opacity = 0;
            content.Children.Add(button);

            button = new CutomButtonUI();
            button.SetData(CustomUIViewerType.SetDate);
            button.Opacity = 0;
            content.Children.Add(button);

            button = new CutomButtonUI();
            button.SetData(CustomUIViewerType.SetBudget);
            button.Opacity = 0;
            content.Children.Add(button);

            button = new CutomButtonUI();
            button.SetData(CustomUIViewerType.GallaryOpenSecond);
            button.Opacity = 0;
            content.Children.Add(button);

            SwitcherUI switcher = new SwitcherUI();
            switcher.SetData(CustomUIViewerType.OrderSwitcher);
            switcher.Opacity = 0;
            content.Children.Add(button);

            

            await ShowTask();

        }

        async Task ShowTask()
        {
            for (int i = 0; i < content.Children.Count; i++)
            {
                animation.OpacityIn(content.Children[i]);
                await Task.Delay(50);
            }
            if (bottomButton.IsVisible)
            {
                await animation.OpacityIn(bottomButton);
            }
        }

        /*
         * Add Adress
         * */

        async Task AddOrderAdressShow()
        {
            pagetype = PageType.AddAdressOrder;
            backButton.IsVisible = true;
            scroll.Margin = new Thickness(10, 10, 10, 10);

            bottomButton.IsVisible = true;
            bottomButton.IsEnabled = true;
            bottomButton.Text = "Сохранить";
            bottomButton.Opacity = 0;

            Label title = new Label();
            title.Text = "Адрес";
            title.FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label));
            title.TextColor = Color.FromHex("#66C7F4");
            title.Margin = new Thickness(10, 0, 10, 10);
            title.Opacity = 0;
            content.Children.Add(title);

            CustomElementUI element = new CustomElementUI();
            element.SetData(CustomUIViewerType.OrderCityPicker);
            element.Opacity = 1;
            content.Children.Add(element);

            InputTextViewer input = new InputTextViewer();
            input.SetData(InputTextViewerType.Adress);
            input.Opacity = 0;
            content.Children.Add(input);

            await ShowTask();
        }

        void SetBottomButtonActive(InputTextViewer input)
        {
            if(!string.IsNullOrEmpty(input.GetInfo()))
                bottomButton.IsEnabled = true;
            else
                bottomButton.IsEnabled = false;
        }

        /*
         * Add Order Date
         * */

        async Task AddOrderDate()
        {
            pagetype = PageType.AddDateOrder;
            backButton.IsVisible = true;
            scroll.Margin = new Thickness(10, 10, 10, 10);

            bottomButton.IsVisible = true;
            bottomButton.IsEnabled = true;
            bottomButton.Text = "Сохранить";

            Label title = new Label();
            title.Text = "Время и дата";
            title.FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label));
            title.TextColor = Color.FromHex("#66C7F4");
            title.Margin = new Thickness(10, 10, 10, 10);
            title.Opacity = 0;
            content.Children.Add(title);

            CustomElementUI element = new CustomElementUI();
            element.SetData(CustomUIViewerType.OrderDatePicker);
            element.Opacity = 0;
            content.Children.Add(element);

            element = new CustomElementUI();
            element.SetData(CustomUIViewerType.OrderTimePicker);
            element.Opacity = 0;
            content.Children.Add(element);

            await ShowTask();
        }

        /*
         * Add Order Time
         **/

        async Task AddOrderBudget()
        {
            pagetype = PageType.AddBudgetOrder;
            backButton.IsVisible = true;
            scroll.Margin = new Thickness(10, 10, 10, 10);

            bottomButton.IsVisible = true;
            bottomButton.IsEnabled = true;
            bottomButton.Text = "Сохранить";

            Label title = new Label();
            title.Text = "Указать бюджет";
            title.FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label));
            title.TextColor = Color.FromHex("#66C7F4");
            title.Margin = new Thickness(10, 10, 10, 10);
            content.Children.Add(title);

            InputTextViewer input = new InputTextViewer();
            input.SetData(InputTextViewerType.Budget);
            content.Children.Add(input);

            ToggleUI toggle = new ToggleUI();
            content.Children.Add(toggle);

        }

        /*
         * My Orders List
         * MyOrdersList -> Show() : Hide()
         * */

        async Task MyOrdersList()
        {
            breadscribe.SetFirstPage(PageType.MyOrderList);
            pagetype = PageType.MyOrderList;
            backButton.IsVisible = true;
            scroll.Margin = new Thickness(10, 10, 10, 10);
            await order.GetAllOrders();
            if (server.ServerResult)
            {
                bottomButton.IsVisible = false;
                bottomButton.IsEnabled = false;

                toolbar.IsVisible = true;
                toolbar.Opacity = 0;

                Label title = new Label();
                title.Text = "Мои заказы";
                title.FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label));
                title.TextColor = Color.FromHex("#66C7F4");
                title.Margin = new Thickness(10, 10, 10, 10);
                title.Opacity = 0;
                content.Children.Add(title);

                ButtonUI button = new ButtonUI();
                button.SetCaption("Новый заказ");
                button.Opacity = 0;
                content.Children.Add(button);

                List<Order> orders = order.getListOrders();

                for(int i = 0; i<orders.Count; i++)
                {
                    OrderDescriptionUI orderDiscription = new OrderDescriptionUI();
                    orderDiscription.SetData(orders[i]);
                    orderDiscription.Opacity = 0;
                    content.Children.Add(orderDiscription);
                }

                await ShowTask();

            }else
            {
                Error();
            }


        }

        /*
         *  Order Message Open
         *  
         * */

        async Task OrderMessageShow()
        {
            pagetype = PageType.OrderMessanger;
            OrderViewForm orderForm = new OrderViewForm();
        }

        /*
         * BackButton Press
         * 
         * */
        private async void ImageButton_Clicked(object sender, EventArgs e)
        {
            if (pagetype == PageType.CategorySecondPanel || pagetype == PageType.CategoryTreedPanel || pagetype == PageType.CategoryFourPanel || pagetype == PageType.PublishOrder)
            {
                buildCategorySwitcher = 0;
            }
            await breadscribe.PreviousPage();
        }

        /*
         * Bootom Button Click Action
         * */
        private async void BottomButton_Clicked(object sender, EventArgs e)
        {
            if(pagetype == PageType.AddAdressOrder && pagetype ==PageType.AddBudgetOrder && pagetype == PageType.AddDateOrder)
            {
                await breadscribe.PreviousPage();
            } 
            else if(pagetype == PageType.PublishOrder)
            {
                await order.SendData();
                if(server.ServerResult)
                {
                    await breadscribe.NextPage(PageType.MyOrderList);
                }
                else
                {
                    Error();
                }
            }
        }

        /*
         * Error Control Methods
         */

        private void Error()
        {
            breadscribe.Cancel();
            ErrorPane("Error in connect!", "Error in connection. Try again!");


        }

        private async void ErrorPane(string Title, string message)
        {
            await DisplayAlert(Title, message, "OK");
        }

        
    }
}