using System;
using System.Windows.Input;
using View.Model;
using View.Model.Services;

namespace View.ViewModel
{
    public class LoadCommand : ICommand
    {
        private readonly ContactSerializer _contactSerializer;
        private readonly Action<Contact> _setContact;

        public LoadCommand(ContactSerializer contactSerializer, Action<Contact> setContact)
        {
            _contactSerializer = contactSerializer ?? throw new ArgumentNullException(nameof(contactSerializer));
            _setContact = setContact ?? throw new ArgumentNullException(nameof(setContact));
        }

        public bool CanExecute(object parameter) => true;

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            Contact loadContact = _contactSerializer.LoadFromJson();
            if (loadContact != null)
            {
                _setContact(loadContact);
                Console.WriteLine("Контакт успешно загружен.");
            }
            else
            {
                Console.WriteLine("Не удалось загрузить контакт.");
            }
        }
    }
}
