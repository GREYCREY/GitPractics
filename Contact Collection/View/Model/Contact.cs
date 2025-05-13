using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Text.RegularExpressions;

namespace Model
{
    /// <summary>
    /// Представляет контакт с именем, номером телефона и электронной почтой.
    /// Реализует валидацию с помощью интерфейса <see cref="IDataErrorInfo"/>.
    /// </summary>
    public partial class Contact : ObservableObject, IDataErrorInfo
    {
        /// <summary>
        /// Имя контакта.
        /// </summary>
        [ObservableProperty]
        private string _name;

        /// <summary>
        /// Номер телефона контакта.
        /// </summary>
        [ObservableProperty]
        private string _phoneNumber;

        /// <summary>
        /// Электронная почта контакта.
        /// </summary>
        [ObservableProperty]
        private string _email;

        /// <summary>
        /// Возвращает значение, указывающее, содержит ли контакт ошибки валидации.
        /// </summary>
        public bool HasErrors =>
            !string.IsNullOrEmpty(this[nameof(Name)]) ||
            !string.IsNullOrEmpty(this[nameof(PhoneNumber)]) ||
            !string.IsNullOrEmpty(this[nameof(Email)]);

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="Contact"/>.
        /// </summary>
        public Contact() { }

        #region IDataErrorInfo

        /// <summary>
        /// Возвращает сообщение об ошибке для указанного свойства.
        /// </summary>
        /// <param name="columnName">Имя проверяемого свойства.</param>
        /// <returns>Сообщение об ошибке или <c>null</c>, если ошибок нет.</returns>
        public string this[string columnName]
        {
            get
            {
                string error = null;

                switch (columnName)
                {
                    case nameof(Name):
                        if (string.IsNullOrWhiteSpace(Name))
                            error = "Имя не может быть пустым.";
                        else if (Name.Length > 100)
                            error = "Имя не должно превышать 100 символов.";
                        break;

                    case nameof(PhoneNumber):
                        if (string.IsNullOrWhiteSpace(PhoneNumber))
                            error = "Номер телефона не может быть пустым.";
                        else if (PhoneNumber.Length > 100)
                            error = "Номер телефона не должен превышать 100 символов.";
                        else if (!Regex.IsMatch(PhoneNumber, @"^\+[0-9]\s?\(?\d{3}\)?\s?\d{3}[-\s]?\d{2}[-\s]?\d{2}$"))
                            error = "Номер телефона должен содержать только цифры или символы +-().";
                        break;

                    case nameof(Email):
                        if (string.IsNullOrWhiteSpace(Email))
                            error = "Адрес почты не может быть пустым.";
                        else if (Email.Length > 100)
                            error = "Адрес почты не должен превышать 100 символов.";
                        else if (!Regex.IsMatch(Email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                            error = "Неправильный формат адреса почты.";
                        break;
                }

                return error;
            }
        }

        /// <summary>
        /// Не используется. Всегда возвращает <c>null</c>.
        /// </summary>
        public string Error => null;

        #endregion
    }
}
