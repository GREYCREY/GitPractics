using Newtonsoft.Json;
using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace View.Model.Services
{
     public class  ContactSerializer
    {
        private  string _filePath = "Моидокументы\\Contacts\\contacts.json";
        public   string FilePath
        {
            get { return _filePath; }
            set { _filePath = value; }
        }
        public  void SaveToJson<Contact>(Contact obj)
        {
            try
            {
                
                if (string.IsNullOrEmpty(FilePath))
                {
                    Console.WriteLine("Путь не задан.");
                    return;
                }

                // Сериализация объекта в JSON с форматированием
                string json = JsonConvert.SerializeObject(obj, Formatting.Indented);

                // Запись JSON в файл по пути, заданному в свойстве
                File.WriteAllText(FilePath, json);

                Console.WriteLine($"Объект успешно сохранён в файл: {FilePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при сохранении: {ex.Message}");
            }
        }
        public  Contact LoadFromJson<Contact>()
        {
            try
            {
                // Проверка, существует ли файл
                if (!File.Exists(FilePath))
                {
                    Console.WriteLine("Файл не найден.");
                    return default;
                }

                // Чтение содержимого файла
                string json = File.ReadAllText(FilePath);

                // Десериализация JSON в объект
                Contact obj = JsonConvert.DeserializeObject<Contact>(json);

                Console.WriteLine($"Объект успешно загружен из файла: {FilePath}");
                return obj;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при загрузке: {ex.Message}");
                return default;
            }
        }
    }

}
