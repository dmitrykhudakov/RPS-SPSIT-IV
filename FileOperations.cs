using System;
using System.Globalization;
using System.IO;

namespace Lab1
{
	public static class FileOperations
	/// Класс для операций с текстовыми файлами.
	/// Формат данных: одна строка, 14 чисел, разделённых символом ';'.
	/// Порядок: V1(x;y;z), V2(x;y;z), V3(x;y;z), Origin(x;y;z), HorizontalAngle, VerticalAngle.
	{
		/// Сохранение данных в .txt файл.
		public static bool Save(string path, Triangle3D tri, Ray3D ray)
		{
			path = path.Trim().Trim('"');

			if (!path.EndsWith(Config.FileExtension, StringComparison.OrdinalIgnoreCase))
				path += Config.FileExtension;

			//  Проверяем, существует ли файл
			if (File.Exists(path))
			{
				Console.Write($"Файл \"{path}\" уже существует. Перезаписать? (1 - Да, 2 - Нет): ");
				string choice = Console.ReadLine();

				// Если введено anything кроме "1", отменяем сохранение
				if (choice?.Trim() != "1")
				{
					Console.WriteLine("Сохранение отменено.");
					return false;
				}
			}

			try
			{
				string content = $"{tri.V1.X:F6}{Config.Separator}{tri.V1.Y:F6}{Config.Separator}{tri.V1.Z:F6}{Config.Separator}" +
								 $"{tri.V2.X:F6}{Config.Separator}{tri.V2.Y:F6}{Config.Separator}{tri.V2.Z:F6}{Config.Separator}" +
								 $"{tri.V3.X:F6}{Config.Separator}{tri.V3.Y:F6}{Config.Separator}{tri.V3.Z:F6}{Config.Separator}" +
								 $"{ray.Origin.X:F6}{Config.Separator}{ray.Origin.Y:F6}{Config.Separator}{ray.Origin.Z:F6}{Config.Separator}" +
								 $"{ray.HorizontalAngle:F2}{Config.Separator}{ray.VerticalAngle:F2}";

				File.WriteAllText(path, content);
				return true;
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Ошибка сохранения: {ex.Message}");
				return false;
			}
		}
		public static (bool Success, Triangle3D Triangle, Ray3D Ray) Load(string path)
		{
			path = path.Trim().Trim('"');

			if (!path.EndsWith(Config.FileExtension, StringComparison.OrdinalIgnoreCase))
				path += Config.FileExtension;

			try
			{
				if (!File.Exists(path))
				{
					return (false, null, null);
				}

				string content = File.ReadAllText(path);
				string[] parts = content.Split(Config.Separator);

				if (parts.Length < 14)
				{
					return (false, null, null);
				}

				double Parse(string s)
				{
					s = s.Replace(',', '.').Trim();
					return double.TryParse(s, NumberStyles.Float, CultureInfo.InvariantCulture, out double val) ? val : 0;
				}

				var tri = new Triangle3D(
					new Point3D(Parse(parts[0]), Parse(parts[1]), Parse(parts[2])),
					new Point3D(Parse(parts[3]), Parse(parts[4]), Parse(parts[5])),
					new Point3D(Parse(parts[6]), Parse(parts[7]), Parse(parts[8]))
				);

				var ray = new Ray3D(
					Parse(parts[9]), Parse(parts[10]), Parse(parts[11]),
					Parse(parts[12]), Parse(parts[13])
				);

				if (!Validator.IsValidTriangle(tri))
				{
					return (false, null, null);
				}

				return (true, tri, ray);
			}
			catch (Exception)
			{
				return (false, null, null);
			}
		}
	}
}