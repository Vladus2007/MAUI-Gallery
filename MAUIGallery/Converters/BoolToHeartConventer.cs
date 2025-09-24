using System.Globalization;

namespace Gallery.Converters
{
    public class BoolToHeartIconConverter : IValueConverter
    {
        // Пути к иконкам. Добавь эти файлы в папку Resources/Images/
        private const string FilledHeart = "heart_filled.png";
        private const string OutlineHeart = "heart_outline.png";

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Если значение true - возвращаем заполненное сердечко, иначе - контур.
            return (bool)value ? FilledHeart : OutlineHeart;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            
            throw new NotImplementedException();
        }
    }
}