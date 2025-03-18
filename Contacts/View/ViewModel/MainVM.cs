using System.ComponentModel;
using System.Windows.Input;
using View.Model;
using View.Model.Services;

namespace View.ViewModel
{
    public class MainVM : INotifyPropertyChanged
    {
        private string _name;
        private string _phoneNumber;
        private string _email;
        private Contact _contact = new Contact();
        private ContactSerializer _contactSerializer = new ContactSerializer();

        public event PropertyChangedEventHandler PropertyChanged;

        public string Name
        {
            get => _name;
            set
            {
                if (_name != value)
                {
                    _name = value;
                    OnPropertyChanged(nameof(Name));
                }
            }
        }

        public string PhoneNumber
        {
            get => _phoneNumber;
            set
            {
                if (_phoneNumber != value)
                {
                    _phoneNumber = value;
                    OnPropertyChanged(nameof(PhoneNumber));
                }
            }
        }

        public string Email
        {
            get => _email;
            set
            {
                if (_email != value)
                {
                    _email = value;
                    OnPropertyChanged(nameof(Email));
                }
            }
        }

        public ICommand SaveCommand { get; }
        public ICommand LoadCommand { get; }

        public MainVM()
        {
            SaveCommand = new SaveCommand(_contactSerializer, _contact, UpdateContactFromProperties);
            LoadCommand = new LoadCommand(_contactSerializer, SetContactFromLoadedData);
        }

        /// <summary>
        /// Обновляет объект _contact из текущих данных ViewModel
        /// </summary>
        private void UpdateContactFromProperties()
        {
            _contact.Name = Name;
            _contact.PhoneNumber = PhoneNumber;
            _contact.Email = Email;
        }

        /// <summary>
        /// Устанавливает данные в ViewModel из загруженного контакта
        /// </summary>
        private void SetContactFromLoadedData(Contact loadedContact)
        {
            if (loadedContact != null)
            {
                Name = loadedContact.Name;
                PhoneNumber = loadedContact.PhoneNumber;
                Email = loadedContact.Email;
            }
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}