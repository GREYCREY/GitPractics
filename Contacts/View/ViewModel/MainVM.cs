using System.ComponentModel;
using System.Windows.Input;
using View.Model;
using View.Model.Services;

namespace View.ViewModel
{
    /// <summary>
    /// Основная ViewModel для работы с данными контакта.
    /// </summary>
    public class MainVM : INotifyPropertyChanged
    {
        private string _name;
        private string _phoneNumber;
        private string _email;
        private Contact _contact = new Contact();
        private ContactSerializer _contactSerializer = new ContactSerializer();

        /// <summary>
        /// Событие изменения свойств.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Получает или задаёт имя контакта.
        /// </summary>
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

        /// <summary>
        /// Получает или задаёт номер телефона контакта.
        /// </summary>
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

        /// <summary>
        /// Получает или задаёт электронную почту контакта.
        /// </summary>
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

        /// <summary>
        /// Команда для сохранения данных контакта.
        /// </summary>
        public ICommand SaveCommand { get; }

        /// <summary>
        /// Команда для загрузки данных контакта.
        /// </summary>
        public ICommand LoadCommand { get; }

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="MainVM"/> и создаёт команды.
        /// </summary>
        public MainVM()
        {
            SaveCommand = new SaveCommand(_contactSerializer, _contact, UpdateContactFromProperties);
            LoadCommand = new LoadCommand(_contactSerializer, SetContactFromLoadedData);
        }

        /// <summary>
        /// Обновляет объект _contact из текущих данных ViewModel.
        /// </summary>
        private void UpdateContactFromProperties()
        {
            _contact.Name = Name;
            _contact.PhoneNumber = PhoneNumber;
            _contact.Email = Email;
        }

        /// <summary>
        /// Устанавливает данные в ViewModel из загруженного контакта.
        /// </summary>
        /// <param name="loadedContact">Загруженный контакт, содержащий данные для обновления ViewModel.</param>
        private void SetContactFromLoadedData(Contact loadedContact)
        {
            if (loadedContact != null)
            {
                Name = loadedContact.Name;
                PhoneNumber = loadedContact.PhoneNumber;
                Email = loadedContact.Email;
            }
        }

        /// <summary>
        /// Вспомогательный метод для вызова события изменения свойства.
        /// </summary>
        /// <param name="propertyName">Имя изменённого свойства.</param>
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
