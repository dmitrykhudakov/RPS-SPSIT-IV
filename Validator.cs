using System.Globalization;
/// Статический класс для валидации входных данных и проверки геометрических условий.
/// Отделяет логику проверок от пользовательского интерфейса и математических расчётов.
namespace Lab1
{
	public static class Validator
	{
		public static bool IsValidTriangle(Triangle3D triangle)
		{
			double a = (triangle.V2 - triangle.V1).Length;
			double b = (triangle.V3 - triangle.V2).Length;
			double c = (triangle.V1 - triangle.V3).Length;

			return (a + b > c + Config.Epsilon) &&
				   (b + c > a + Config.Epsilon) &&
				   (c + a > b + Config.Epsilon);
		}

		public static (bool Success, double Value) TryParseDouble(string input)  /// Безопасно парсит строку в число типа double.
		{
			if (double.TryParse(input, CultureInfo.InvariantCulture, out double result))
			{
				return (true, result);
			}
			return (false, 0);
		}
	}
}