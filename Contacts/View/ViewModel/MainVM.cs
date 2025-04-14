using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using View.Model;
using View.Model.Servicies;


namespace View.ViewModel
{
    public class MainVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private ObservableCollection<Contact> _contacts;
        private Contact _selectedContact;
        private Contact _clonedContact;
        private string _name;
        private string _phoneNumber;
        private string _email;
        private Visibility _applyButtonVisibility = Visibility.Collapsed;
        private Contact _contact = new Contact();
        private ContactSerializer _contactSerializer = new ContactSerializer();
        
        
        private bool _isContactReadOnly = true;
        private bool _isDataChanged;
        private bool _isAddingNewContact;
        private bool _isEditingContact;
        private int _indexBeforeEditing;


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

        public ObservableCollection<Contact> Contacts
        {
            get => _contacts;
            set
            {
                _contacts = value;
                OnPropertyChanged();
            }
        }

        public bool IsContactSelected => SelectedContact != null;

        public Contact SelectedContact
        {
            get => _selectedContact;
            set
            {
                if (_selectedContact != value)
                {
                    if (_isAddingNewContact && _selectedContact != null && !Contacts.Contains(_selectedContact))
                    {
                        _selectedContact = null;
                        _isAddingNewContact = false;
                    }

                    _selectedContact = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(IsContactSelected));
                    OnPropertyChanged(nameof(ApplyButtonVisibility));

                    if (_selectedContact != null)
                    {
                        IsContactReadOnly = true;
                    }
                }
            }
        }

        public Visibility ApplyButtonVisibility
        {
            get { return _applyButtonVisibility; }
            set
            {
                if (_applyButtonVisibility != value)
                {
                    _applyButtonVisibility = value;
                    OnPropertyChanged(nameof(ApplyButtonVisibility));
                }
            }
        }

        public bool IsAddingNewContact
        {
            get => _isAddingNewContact;
            set
            {
                if (_isAddingNewContact != value)
                {
                    _isAddingNewContact = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool IsDataChanged
        {
            get => _isDataChanged;
            set
            {
                if (_isDataChanged != value)
                {
                    _isDataChanged = value;
                    OnPropertyChanged(nameof(IsDataChanged));
                    ApplyButtonVisibility = _isDataChanged ? Visibility.Visible : Visibility.Collapsed;
                }
            }
        }

        public bool IsContactReadOnly
        {
            get => _isContactReadOnly;
            set
            {
                if(_isContactReadOnly != value)
                {
                    _isContactReadOnly = value;
                    OnPropertyChanged();
                }
            }
        }

        public ICommand AddCommand { get; }
        public ICommand EditCommand { get; }

        public ICommand RemoveCommand { get; }

        public ICommand ApplyCommand { get; }

        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        
        private void UpdateContact(Contact contact)
        {
            IsDataChanged = true;
            IsContactReadOnly = false;
        }

        private void AddContact(object parametr)
        {
            IsContactReadOnly = false;
            IsAddingNewContact = true;
            var newContact = new Contact();
            SelectedContact = newContact;
            UpdateContact(newContact);
        }
        
        private void EditContact(object parametr)
        {
            if(SelectedContact != null)
            {
                IsContactReadOnly = false;
                _isEditingContact = true;
                _indexBeforeEditing = Contacts.IndexOf(SelectedContact);

                _clonedContact = new Contact
                {
                    Name = SelectedContact.Name,
                    PhoneNumber = SelectedContact.PhoneNumber,
                    Email = SelectedContact.Email,
                };

                SelectedContact = _clonedContact;
                UpdateContact(_clonedContact);
            }
        }

        private void RemoveContact(object parametr)
        {
            if(SelectedContact != null)
            {
                int selectedIndex = Contacts.IndexOf(SelectedContact);
                Contacts.RemoveAt(selectedIndex);
                _contactSerializer.SaveContacts(Contacts);
                
                if(Contacts.Count > 0)
                {
                    if (selectedIndex < Contacts.Count)
                    {
                        SelectedContact = Contacts[selectedIndex];
                    }

                    else
                    {
                        SelectedContact = Contacts[Contacts.Count - 1];
                    }

                }
            }

            else
            {
                SelectedContact = null;
            }
        }

        private void ApplyChanges(object parametr)
        {
            if(SelectedContact != null)
            {
                if (_isAddingNewContact)
                {
                    Contacts.Add(SelectedContact);
                    IsAddingNewContact = false;
                }

                if (_isEditingContact)
                {
                    Contacts[_indexBeforeEditing] = SelectedContact;
                    _isAddingNewContact = false;
                }
            }

            _contactSerializer.SaveContacts(Contacts);
            IsDataChanged = false;
            IsContactReadOnly = true;
        }

        public void DeselectContact()
        {
            SelectedContact = null;
        }
        private bool CanEditOrRemoveContact(object parametr)
        {
            return SelectedContact != null && Contacts.Count > 0;
        }
        public MainVM()
        {
            _contactSerializer = new ContactSerializer();
            Contacts = new ObservableCollection<Contact>(_contactSerializer.LoadContacts());
            AddCommand = new RelayCommand(AddContact);
            EditCommand = new RelayCommand(EditContact, CanEditOrRemoveContact);
            RemoveCommand = new RelayCommand(RemoveContact, CanEditOrRemoveContact);
            ApplyCommand = new RelayCommand(ApplyChanges);
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
    }
}