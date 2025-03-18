using Newtonsoft.Json;
using System;
using System.IO;

namespace View.Model.Services
{
    public class ContactSerializer
    {
        private string _filePath;

        public string FilePath
        {
            get => _filePath;
            set => _filePath = value;
        }

        public ContactSerializer()
        {
            // Получаем путь к папке "Документы" текущего пользователя
            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string contactsFolder = Path.Combine(documentsPath, "Contacts");

            // Создаем папку, если её нет
            if (!Directory.Exists(contactsFolder))
            {
                Directory.CreateDirectory(contactsFolder);
            }

            // Формируем путь к JSON-файлу
            _filePath = Path.Combine(contactsFolder, "contacts.json");
        }

        public void SaveToJson(Contact obj)
        {
            try
            {
                if (obj == null)
                {
                    Console.WriteLine("Ошибка: передан null-объект.");
                    return;
                }

                string json = JsonConvert.SerializeObject(obj, Formatting.Indented);

                File.WriteAllText(FilePath, json);

                Console.WriteLine($" Объект успешно сохранён в файл: {FilePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($" Ошибка при сохранении: {ex.Message}");
            }
        }

        public Contact LoadFromJson()
        {
            try
            {
                if (!File.Exists(FilePath))
                {
                    Console.WriteLine(" Файл не найден.");
                    return null;
                }

                string json = File.ReadAllText(FilePath);

                Contact obj = JsonConvert.DeserializeObject<Contact>(json);

                Console.WriteLine($" Объект успешно загружен из файла: {FilePath}");
                return obj;
            }
            catch (Exception ex)
            {
                Console.WriteLine($" Ошибка при загрузке: {ex.Message}");
                return null;
            }
        }
    }
}
