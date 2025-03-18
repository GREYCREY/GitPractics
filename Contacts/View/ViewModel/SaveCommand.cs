using System;
using System.Windows.Input;
using View.Model;
using View.Model.Services;

namespace View.ViewModel
{
    /// <summary>
    /// Команда для сохранения контакта в JSON файл.
    /// </summary>
    public class SaveCommand : ICommand
    {
        private readonly ContactSerializer _contactSerializer;
        private readonly Contact _contact;
        private readonly Action _updateContact;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="SaveCommand"/>.
        /// </summary>
        /// <param name="contactSerializer">Сервис для работы с файлами контактов.</param>
        /// <param name="contact">Объект контакта для сохранения.</param>
        /// <param name="updateContact">Действие для обновления данных в объекте контакта.</param>
        /// <exception cref="ArgumentNullException">Если один из параметров равен null.</exception>
        public SaveCommand(ContactSerializer contactSerializer, Contact contact, Action updateContact)
        {
            _contactSerializer = contactSerializer ?? throw new ArgumentNullException(nameof(contactSerializer));
            _contact = contact ?? throw new ArgumentNullException(nameof(contact));
            _updateContact = updateContact ?? throw new ArgumentNullException(nameof(updateContact));
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
        /// Выполняет команду сохранения контакта в JSON файл.
        /// </summary>
        /// <param name="parameter">Параметры команды.</param>
        public void Execute(object parameter)
        {
            // Обновление данных в объекте контакта
            _updateContact();

            // Сохранение объекта контакта в JSON файл
            _contactSerializer.SaveToJson(_contact);
        }
    }
}
