using System;

namespace View.Model
{
    /// <summary>
    /// Представляет контакт с именем, номером телефона и электронной почтой.
    /// </summary>
    public class Contact
    {
        private string _name;
        private string _phoneNumber;
        private string _email;

        /// <summary>
        /// Получает или задаёт имя контакта.
        /// </summary>
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        /// <summary>
        /// Получает или задаёт номер телефона контакта.
        /// </summary>
        public string PhoneNumber
        {
            get { return _phoneNumber; }
            set { _phoneNumber = value; }
        }

        /// <summary>
        /// Получает или задаёт электронную почту контакта.
        /// </summary>
        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }
    }
}
