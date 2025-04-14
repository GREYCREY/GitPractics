using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace View.Model
{
    public class Contact : INotifyPropertyChanged
    {
        private string _name;
        private string _phoneNumber;
        private string _email;
        public event PropertyChangedEventHandler PropertyChanged;

        public string Name { get { return _name; } set { _name = value; } }
        public string PhoneNumber { get { return _phoneNumber; } set { _phoneNumber = value; } }
        public string Email { get { return _email; } set { _email = value; } }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }



        

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="Contact"/> с указанными именем,
        /// номером телефона и электронной почтой.
        /// </summary>
        /// <param name="name">Имя контакта.</param>
        /// <param name="number">Номер телефона контакта.</param>
        /// <param name="email">Электронная почта контакта.</param>
        public Contact(string name, string number, string email)
        {
            Name = name;
            PhoneNumber = number;
            Email = email;
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="Contact"/> без параметров.
        /// </summary>
        public Contact() { }


    }
}
