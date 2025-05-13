using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Model.Services
{
        /// <summary>
        /// Предоставляет методы для сериализации и десериализации.
        /// коллекции контактов.
        /// </summary>
        public class ContactSerializer
        {
            /// <summary>
            /// Путь к файлу, в котором хранятся контакты.
            /// </summary>
            private string _filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Contacts", "contacts.json");

            /// <summary>
            /// Инициализирует новый экземпляр класса <see cref="ContactSerializer"/>.
            /// Создаёт директорию, если она не существует.
            /// </summary>
            public ContactSerializer()
            {
                string directoryPath = Path.GetDirectoryName(_filePath);
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }
            }

            /// <summary>
            /// Сохраняет коллекцию контактов.
            /// </summary>
            /// <param name="contacts">Коллекция контактов для сохранения.</param>
            public void SaveContacts(ObservableCollection<Contact> contacts)
            {
                string json = JsonConvert.SerializeObject(contacts);
                File.WriteAllText(_filePath, json);
            }

            /// <summary>
            /// Хагружает коллекцию контактов из файла.
            /// </summary>
            /// <returns>Коллекцию контактов, загруженная из файла.
            /// Возвращает новую пустую коллекцию, если файл не существует.</returns>
            public ObservableCollection<Contact> LoadContacts()
            {
                if (File.Exists(_filePath))
                {
                    string json = File.ReadAllText(_filePath);
                    return JsonConvert.DeserializeObject<ObservableCollection<Contact>>(json);
                }

                return new ObservableCollection<Contact>();
            }

        }
    
}
