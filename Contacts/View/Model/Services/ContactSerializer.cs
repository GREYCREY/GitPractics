using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace View.Model.Servicies
{
    /// <summary>
    /// Класс для сериализации и десериализации контактов.
    /// </summary>
    public class ContactSerializer
    {
         

        /// <summary>
        /// Путь к файлу для сохранения и загрузки контактов.
        /// </summary>
        public string FilePath { get; set; }

        
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="ContactSerializer"/>.
        /// Устанавливает стандартный путь для сохранения файлов.
        /// </summary>
        public ContactSerializer()
        {
            FilePath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                "Contacts",
                "contacts.json"
            );

            Directory.CreateDirectory(Path.GetDirectoryName(FilePath));
        }

        

       

        /// <summary>
        /// Сохраняет контакты в файл в формате JSON.
        /// </summary>
        /// <param name="contacts">Список контактов для сохранения.</param>
        public void SaveContacts(IEnumerable<Contact> contacts)
        {
            try
            {
                string json = JsonConvert.SerializeObject(contacts, Formatting.Indented);
                File.WriteAllText(FilePath, json);
                Console.WriteLine("Контакты успешно сохранены.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при сохранении контактов: {ex.Message}");
            }
        }

        /// <summary>
        /// Загружает контакты из файла.
        /// </summary>
        /// <returns>Список контактов.</returns>
        public List<Contact> LoadContacts()
        {
            try
            {
                if (!File.Exists(FilePath))
                {
                    Console.WriteLine("Файл не найден.");
                    return new List<Contact>();
                }

                string json = File.ReadAllText(FilePath);
                List<Contact> contacts = JsonConvert.DeserializeObject<List<Contact>>(json);
                Console.WriteLine("Контакты успешно загружены.");
                return contacts;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при загрузке контактов: {ex.Message}");
                return new List<Contact>();
            }
        }

        
    }
}