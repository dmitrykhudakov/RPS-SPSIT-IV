using System;
/// Вспомогательный класс для взаимодействия с консолью.
/// Отвечает за форматированный вывод, чтение ввода с валидацией и отображение меню.
namespace Lab1
{
	public static class ConsoleHelper
	{
		public static void PrintHeader()
		{
			Console.WriteLine(new string('=', 60));
			Console.WriteLine(Config.AppTitle.ToUpper());
			Console.WriteLine($"Автор: {Config.AppAuthor}");
			Console.WriteLine($"Задача: {Config.AppTask}");
			Console.WriteLine("Результат: координаты точки пересечения или сообщение об отсутствии");
			Console.WriteLine(new string('=', 60) + "\n");
		}

		public static double ReadDouble(string prompt)
		{
			while (true)
			{
				Console.Write(prompt);
				string input = Console.ReadLine();
				var (ok, value) = Validator.TryParseDouble(input);

				if (ok) return value;
				Console.WriteLine("Некорректное число. Попробуйте снова.");
			}
		}

		public static int ReadMenuChoice(string[] options)
		{
			for (int i = 0; i < options.Length; i++)
				Console.WriteLine($"{i + 1} - {options[i]}");

			while (true)
			{
				Console.Write("\nВаш выбор: ");
				string input = Console.ReadLine();

				if (int.TryParse(input, out int choice) && choice >= 1 && choice <= options.Length)
					return choice;

				Console.WriteLine("Такой пункт не существует. Попробуйте снова.");
			}
		}

		public static void PrintResult(Point3D? hitPoint)
		{
			Console.WriteLine();
			if (hitPoint.HasValue)
			{
				Console.WriteLine("Пересечение найдено!");
				Console.WriteLine($"Точка: ({hitPoint.Value.X:F3}, {hitPoint.Value.Y:F3}, {hitPoint.Value.Z:F3})");
			}
			else
			{
				Console.WriteLine("Пересечение не найдено.");
			}
			Console.WriteLine();
		}

		public static void PrintData(Triangle3D tri, Ray3D ray)
		{
			Console.WriteLine($"\n Треугольник:");
			Console.WriteLine($"  V1: ({tri.V1.X:F2}, {tri.V1.Y:F2}, {tri.V1.Z:F2})");
			Console.WriteLine($"  V2: ({tri.V2.X:F2}, {tri.V2.Y:F2}, {tri.V2.Z:F2})");
			Console.WriteLine($"  V3: ({tri.V3.X:F2}, {tri.V3.Y:F2}, {tri.V3.Z:F2})");
			Console.WriteLine($"\n Луч: начало ({ray.Origin.X:F2}, {ray.Origin.Y:F2}, {ray.Origin.Z:F2})");
			Console.WriteLine($"    углы: H={ray.HorizontalAngle:F1}°, V={ray.VerticalAngle:F1}°");
		}
	}
}