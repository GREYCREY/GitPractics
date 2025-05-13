using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows;


namespace ContactCollection.Converters
{
    /// <summary>
    /// Конвертер для преобразования значения типа <see cref="bool"/> в <see cref="Visibility"/>.
    /// Используется для управления видимостью элементов интерфейса на основе логических значений.
    /// </summary>
    public class BoolToVisibilityConverter : IValueConverter
    {
        /// <summary>
        /// Преобразует значение типа <see cref="bool"/> в <see cref="Visibility"/>.
        /// Если значение true, возвращает <see cref="Visibility.Visible"/>, иначе <see cref="Visibility.Hidden"/>.
        /// </summary>
        /// <param name="value">Входное значение (ожидается <see cref="bool"/>).</param>
        /// <param name="targetType">Тип данных, в который выполняется преобразование (игнорируется).</param>
        /// <param name="parameter">Дополнительный параметр (не используется).</param>
        /// <param name="culture">Объект <see cref="CultureInfo"/> (не используется).</param>
        /// <returns>Возвращает <see cref="Visibility.Visible"/>, если <paramref name="value"/> равно true, иначе <see cref="Visibility.Hidden"/>.</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool boolValue = (bool)value;
            return boolValue ? Visibility.Visible : Visibility.Hidden;
        }

        /// <summary>
        /// Преобразует значение <see cref="Visibility"/> обратно в <see cref="bool"/>.
        /// Если значение равно <see cref="Visibility.Visible"/>, возвращает true, иначе false.
        /// </summary>
        /// <param name="value">Входное значение (ожидается <see cref="Visibility"/>).</param>
        /// <param name="targetType">Тип данных, в который выполняется преобразование (игнорируется).</param>
        /// <param name="parameter">Дополнительный параметр (не используется).</param>
        /// <param name="culture">Объект <see cref="CultureInfo"/> (не используется).</param>
        /// <returns>Возвращает true, если <paramref name="value"/> равно <see cref="Visibility.Visible"/>, иначе false.</returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value != null && (Visibility)value == Visibility.Visible;
        }
    }
}
