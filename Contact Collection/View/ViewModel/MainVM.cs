using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Model.Services;
using Model;
using System.ComponentModel;
using System.Windows.Input;

namespace ViewModel
{
    /// <summary>
    /// ViewModel для главного окна приложения.
    /// </summary>
    public partial class MainVM : ObservableObject
    {
        #region Поля

        [ObservableProperty]
        /// <summary>
        /// Коллекция контактов.
        /// </summary>
        private ObservableCollection<Contact> _contacts;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(EditContactCommand))]
        [NotifyCanExecuteChangedFor(nameof(RemoveContactCommand))]
        [NotifyCanExecuteChangedFor(nameof(ApplyContactCommand))]
        /// <summary>
        /// Выбранный контакт из списка.
        /// </summary>
        private Contact _selectedContact;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(EditContactCommand))]
        [NotifyCanExecuteChangedFor(nameof(RemoveContactCommand))]
        [NotifyCanExecuteChangedFor(nameof(ApplyContactCommand))]
        [NotifyPropertyChangedFor(nameof(IsReadOnly))]
        /// <summary>
        /// Флаг, указывающий, находится ли приложение в режиме редактирования.
        /// </summary>
        private bool _isEditMode = false;

        /// <summary>
        /// Флаг, указывающий, был ли создан новый контакт.
        /// </summary>
        private bool _isNewContact = false;

        /// <summary>
        /// Сервис для сериализации и десериализации контактов.
        /// </summary>
        private ContactSerializer _serializer = new ContactSerializer();

        /// <summary>
        /// Индекс выбранного контакта.
        /// </summary>
        private int _selectedIndex;

        #endregion

        #region Свойства

        /// <summary>
        /// Получает значение, указывающее, находится ли приложение в режиме только для чтения.
        /// </summary>
        public bool IsReadOnly => !IsEditMode;

        #endregion

        #region Конструктор

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="MainVM"/>.
        /// Загружает контакты из файла и инициализирует команды.
        /// </summary>
        public MainVM()
        {
            _serializer = new ContactSerializer();
            Contacts = new ObservableCollection<Contact>(_serializer.LoadContacts());
        }

        #endregion

        #region Обработчики команд

        [RelayCommand]
        /// <summary>
        /// Обработчик команды AddCommand.
        /// Создаёт новый контакт и подготавливает ViewModel к редактированию.
        /// </summary>
        /// <param name="parameter">Параметр команды (не используется).</param>
        private void AddContact(object parameter)
        {
            SelectedContact = null;
            var newContact = new Contact();
            SelectedContact = newContact;
            _isNewContact = true;
            IsEditMode = true;
            SelectedContact.PropertyChanged += SelectedContanctChanged;
        }

        [RelayCommand(CanExecute = nameof(CanEditOrRemoveContact))]
        /// <summary>
        /// Обработчик команды EditCommand.
        /// Переводит приложение в режим редактирования выбранного контакта.
        /// </summary>
        /// <param name="parameter">Параметр команды (не используется).</param>
        public void EditContact(object parameter)
        {
            if (IsEditMode)
            {
                IsEditMode = false;
                return;
            }

            var clonedContact = new Contact()
            {
                Email = SelectedContact.Email,
                PhoneNumber = SelectedContact.PhoneNumber,
                Name = SelectedContact.Name
            };
            _selectedIndex = Contacts.IndexOf(SelectedContact);
            var newIndex = _selectedIndex;
            SelectedContact = clonedContact;
            SelectedContact.PropertyChanged += SelectedContanctChanged;
            IsEditMode = true;
            _selectedIndex = newIndex;
        }

        private void SelectedContanctChanged(object? sender, PropertyChangedEventArgs e)
        {
            ApplyContactCommand.NotifyCanExecuteChanged();
        }

        [RelayCommand(CanExecute = nameof(CanEditOrRemoveContact))]
        /// <summary>
        /// Обработчик команды RemoveCommand.
        /// Удаляет выбранный контакт из коллекции.
        /// </summary>
        /// <param name="parameter">Параметр команды (не используется).</param>
        private void RemoveContact(object parameter)
        {
            if (SelectedContact != null)
            {
                _selectedIndex = Contacts.IndexOf(SelectedContact);
                Contacts.Remove(SelectedContact);

                if (Contacts.Count > 0)
                {
                    if (_selectedIndex < Contacts.Count)
                    {
                        if (_selectedIndex > -1)
                        {
                            SelectedContact = Contacts[_selectedIndex];
                        }
                    }
                    else
                    {
                        SelectedContact = Contacts[Contacts.Count - 1];
                    }
                }
                else
                {
                    SelectedContact = null;
                }

                IsEditMode = false;
                SaveContacts();
            }
        }

        [RelayCommand(CanExecute = nameof(CanApplyContact))]
        /// <summary>
        /// Обработчик команды ApplyCommand.
        /// Применяет изменения к контакту и сохраняет их в файл.
        /// </summary>
        /// <param name="parameter">Параметр команды (не используется).</param>
        private void ApplyContact(object parameter)
        {
            SelectedContact.PropertyChanged -= SelectedContanctChanged;
            if (_isNewContact)
            {
                Contacts.Add(SelectedContact);
            }
            else
            {
                Contacts[_selectedIndex] = SelectedContact;
                _selectedIndex = -1;
            }

            IsEditMode = false;
            _isNewContact = false;
            SaveContacts();
        }

        #endregion

        #region Вспомогательные методы

        /// <summary>
        /// Определяет, может ли быть выполнена команда EditCommand или RemoveCommand.
        /// </summary>
        /// <param name="parameter">Параметр команды (не используется).</param>
        /// <returns><c>true</c>, если команда может быть выполнена, иначе <c>false</c>.</returns>
        private bool CanEditOrRemoveContact(object parameter)
        {
            return SelectedContact != null && !IsEditMode;
        }

        /// <summary>
        /// Определяет, может ли быть выполнена команда ApplyCommand.
        /// </summary>
        /// <param name="parameter">Параметр команды (не используется).</param>
        /// <returns><c>true</c>, если команда может быть выполнена, иначе <c>false</c>.</returns>
        private bool CanApplyContact(object parameter)
        {
            return SelectedContact != null && IsEditMode && !SelectedContact.HasErrors;
        }

        /// <summary>
        /// Сохраняет коллекцию контактов в файл.
        /// </summary>
        private void SaveContacts()
        {
            _serializer.SaveContacts(Contacts);
        }

        partial void OnSelectedContactChanging(Contact oldValue, Contact newValue)
        {
            if (oldValue != null)
            {
                oldValue.PropertyChanged -= SelectedContanctChanged;
            }

            IsEditMode = false;
        }

        partial void OnSelectedContactChanged(Contact value)
        {
            EditContactCommand.NotifyCanExecuteChanged();
            RemoveContactCommand.NotifyCanExecuteChanged();
            ApplyContactCommand.NotifyCanExecuteChanged();
        }

        #endregion
    }
}