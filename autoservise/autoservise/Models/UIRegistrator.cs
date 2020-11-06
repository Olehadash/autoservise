using autoservise.MainUI;
using System;
using System.Collections.Generic;
using System.Text;

namespace autoservise.Models
{
    class UIRegistrator
    {
        private static UIRegistrator _instace = new UIRegistrator();

        public List<arrowbut> buttons = new List<arrowbut>();
        public Dictionary<InputTextViewerType, InputTextViewModel> inputTextView = new Dictionary<InputTextViewerType, InputTextViewModel>();
        public Dictionary<CustomUIViewerType, CustomUIViewModel> UIView = new Dictionary<CustomUIViewerType, CustomUIViewModel>();

        static internal UIRegistrator GetInstance
        {
            get
            {
                return _instace;
            }
        }

        public UIRegistrator()
        {
            buttons.Add(new arrowbut("logico.png", "Войти как клиент ", "Чтобы найти специалиста "));
            buttons.Add(new arrowbut("logico2.png", "Войти как специалист ", "Чтобы предложить свои услуги"));
            buttons.Add(new arrowbut("logico3.png", "У меня уже есть аккаунт ", ""));

            inputTextView.Add(InputTextViewerType.UserName, new InputTextViewModel("Укажите имя *", "Имя", "username.png", false));
            inputTextView.Add(InputTextViewerType.Phone, new InputTextViewModel("Укажите номер телефона *", "+777", "Phone.png", false));
            inputTextView.Add(InputTextViewerType.Email, new InputTextViewModel("Укажите свой E-mail *", "E-mail", "Email.png", false));
            inputTextView.Add(InputTextViewerType.Password, new InputTextViewModel("Напишите пароль *", "Пароль", "keyimg.png", true));
            inputTextView.Add(InputTextViewerType.OrganizationName, new InputTextViewModel("Название организации", "Организация", "username.png", false));
            inputTextView.Add(InputTextViewerType.ConfirmMail, new InputTextViewModel("", "Код подтверждения", "", false));
            inputTextView.Add(InputTextViewerType.Desctiption, new InputTextViewModel("Добавить описание", "Описание", "", false));
            inputTextView.Add(InputTextViewerType.OrederDescripton, new InputTextViewModel("", "Напишите,какую работу нужно выполнить", "", false));
            inputTextView.Add(InputTextViewerType.Adress, new InputTextViewModel("Адрес", "Адрес", "", false));
            inputTextView.Add(InputTextViewerType.Budget, new InputTextViewModel("Укажите свой бюджет", "0", "", false));/**/

            UIView.Add(CustomUIViewerType.City, new CustomUIViewModel("Выбрать город *", "Выбрать город", "map.png", UIType.PICKER));
            UIView.Add(CustomUIViewerType.OrderCityPicker, new CustomUIViewModel("Город", "Город", "", UIType.PICKER));
            UIView.Add(CustomUIViewerType.OrderDatePicker, new CustomUIViewModel("День", "", "", UIType.DATEPICK));
            UIView.Add(CustomUIViewerType.OrderTimePicker, new CustomUIViewModel("Время", "", "", UIType.TIMEPICK));
            UIView.Add(CustomUIViewerType.Category, new CustomUIViewModel("Выбрать раздел *", "Выбрать раздел", "", UIType.PICKER));
            UIView.Add(CustomUIViewerType.GalleryOpen, new CustomUIViewModel("Добавить фото ", "Добавить фото", "photoadd.png", UIType.BUTTON));
            UIView.Add(CustomUIViewerType.SetAdress, new CustomUIViewModel("Добавить адрес", "Не обязательное поле для заполнения", "map.png", UIType.BUTTON));
            UIView.Add(CustomUIViewerType.SetDate, new CustomUIViewModel("Указать дату и время", "Не обязательное поле для заполнения", "timedata.png", UIType.BUTTON));
            UIView.Add(CustomUIViewerType.SetBudget, new CustomUIViewModel("Указать бюджет ", "Не обязательное поле для заполнения", "money.png", UIType.BUTTON));
            UIView.Add(CustomUIViewerType.GallaryOpenSecond, new CustomUIViewModel("Загрузить фотографии", "Не обязательное поле для заполнения", "photoadd.png", UIType.BUTTON));
        }
    }
}
