using System;
using System.Collections.Generic;
using System.Text;

namespace autoservise.Models
{
    class Tutorial
    {
        public string picture;
        public string title;
        public string description;

        public Tutorial(string picture, string title, string description)
        {
            this.picture = picture;
            this.title = title;
            this.description = description;
        }

    }
    class TutorialModel
    {
        public List<Tutorial> tutorials = new List<Tutorial>();
        public TutorialModel()
        {
            tutorials.Add(new Tutorial("tutorialpic3.png", "Создайте заявку", "За 3 минуты создайте заявку. Её увидят автосервисы вашего города и ответят в приложении."));
            tutorials.Add(new Tutorial("tutorialpic.png", "Получите ответы", "Сравните цены, отзывы и выберите тот автосервис. который вам понравился"));
            tutorials.Add(new Tutorial("tutorialpic4.png", "Чат с автосервисом", "Обсудите детали с представителем мастерской, назначьте удобное для вас время и езжайте на ремонт"));
            tutorials.Add(new Tutorial("tutorialpic2.png", "Ремонт завершён", "Вы узнаете о завершении ремонта в мобильном приложении. Останется только забрать автомобиль."));
        }
    }
}
