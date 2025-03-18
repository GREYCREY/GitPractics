using System;
using System.Windows.Input;
using View.Model;
using View.Model.Services;

namespace View.ViewModel
{
    /// <summary>
    /// Команда для загрузки контакта из JSON файла.
    /// </summary>
    public class LoadCommand : ICommand
    {
        private readonly ContactSerializer _contactSerializer;
        private readonly Action<Contact> _setContact;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="LoadCommand"/>.
        /// </summary>
        /// <param name="contactSerializer">Сервис для работы с файлами контактов.</param>
        /// <param name="setContact">Действие для установки загруженного контакта.</param>
        /// <exception cref="ArgumentNullException">Если переданные параметры равны null.</exception>
        public LoadCommand(ContactSerializer contactSerializer, Action<Contact> setContact)
        {
            _contactSerializer = contactSerializer ?? throw new ArgumentNullException(nameof(contactSerializer));
            _setContact = setContact ?? throw new ArgumentNullException(nameof(setContact));
        }

        /// <summary>
        /// Определяет, может ли команда быть выполнена.
        /// </summary>
        /// <param name="parameter">Параметры команды.</param>
        /// <returns>Возвращает true, если команда может быть выполнена.</returns>
        public bool CanExecute(object parameter) => true;

        /// <summary>
        /// Событие, уведомляющее об изменении возможности выполнения команды.
        /// </summary>
        public event EventHandler CanExecuteChanged;

        /// <summary>
        /// Выполняет команду загрузки контакта.
        /// </summary>
        /// <param name="parameter">Параметры команды.</param>
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
