using Newtonsoft.Json;
using System;
using System.IO;

namespace View.Model.Services
{
    /// <summary>
    /// Сервис для сериализации и десериализации контактов в JSON файл.
    /// </summary>
    public class ContactSerializer
    {
        private string _filePath;

        /// <summary>
        /// Получает или задаёт путь к файлу JSON для сохранения и загрузки контактов.
        /// </summary>
        public string FilePath
        {
            get => _filePath;
            set => _filePath = value;
        }

        /// <summary>
        /// Конструктор класса. Формирует путь к файлу контактов в папке "Документы".
        /// </summary>
        public ContactSerializer()
        {
            // Получаем путь к папке "Документы" текущего пользователя
            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string contactsFolder = Path.Combine(documentsPath, "Contacts");

            // Создаём папку, если её нет
            if (!Directory.Exists(contactsFolder))
            {
                Directory.CreateDirectory(contactsFolder);
            }

            // Формируем путь к JSON-файлу
            _filePath = Path.Combine(contactsFolder, "contacts.json");
        }

        /// <summary>
        /// Сохраняет объект типа <see cref="Contact"/> в JSON файл.
        /// </summary>
        /// <param name="obj">Объект контакта для сохранения.</param>
        public void SaveToJson(Contact obj)
        {
            try
            {
                if (obj == null)
                {
                    Console.WriteLine("Ошибка: передан null-объект.");
                    return;
                }

                // Сериализация объекта в JSON строку
                string json = JsonConvert.SerializeObject(obj, Formatting.Indented);

                // Запись JSON в файл
                File.WriteAllText(FilePath, json);

                Console.WriteLine($"Объект успешно сохранён в файл: {FilePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при сохранении: {ex.Message}");
            }
        }

        /// <summary>
        /// Загружает объект типа <see cref="Contact"/> из JSON файла.
        /// </summary>
        /// <returns>Загруженный объект контакта, или null в случае ошибки.</returns>
        public Contact LoadFromJson()
        {
            try
            {
                if (!File.Exists(FilePath))
                {
                    Console.WriteLine("Файл не найден.");
                    return null;
                }

                // Чтение JSON строки из файла
                string json = File.ReadAllText(FilePath);

                // Десериализация JSON в объект
                Contact obj = JsonConvert.DeserializeObject<Contact>(json);

                Console.WriteLine($"Объект успешно загружен из файла: {FilePath}");
                return obj;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при загрузке: {ex.Message}");
                return null;
            }
        }
    }
}
