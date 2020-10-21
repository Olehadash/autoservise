using System;
using System.Collections.Generic;
using System.Text;

namespace autoservise.Models
{
    class CreateUserModel
    {
        private static CreateUserModel _instance = null;

        public bool _isConsumer = true;

        static internal CreateUserModel Instance()
        {
            if (_instance == null)
            {
                _instance = new CreateUserModel();

            }

            return _instance;
        }
    }
}
