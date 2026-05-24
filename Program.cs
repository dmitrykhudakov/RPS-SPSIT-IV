using System;

namespace Lab1
{
	internal class Program
	{
		static void Main()
		/// Главный метод программы. Инициализирует цикл работы, обрабатывает выбор пользователя
		/// и координирует взаимодействие между модулями ConsoleHelper, FileOperations, Validator и MathSolver.
		{
			ConsoleHelper.PrintHeader();
			bool keepRunning = true;

			while (keepRunning)
			{
				string[] menu = {
					"Ввести данные вручную",
					"Сгенерировать случайные данные",
					"Загрузить из файла",
					"Выйти"
				};

				int choice = ConsoleHelper.ReadMenuChoice(menu);
				Triangle3D triangle = null;
				Ray3D ray = null;

				switch (choice)
				{
					case 1:
						triangle = InputTriangleManual();
						ray = InputRayManual();
						break;
					case 2:
						triangle = GenerateRandomTriangle();
						ray = GenerateRandomRay();
						ConsoleHelper.PrintData(triangle, ray);
						break;
					case 3:
						Console.Write("\nПуть к файлу: ");
						string path = Console.ReadLine();
						var (ok, tri, r) = FileOperations.Load(path);

						if (ok && tri != null && r != null && Validator.IsValidTriangle(tri))
						{
							triangle = tri;
							ray = r;
							Console.WriteLine("Данные загружены.");
							ConsoleHelper.PrintData(triangle, ray);
						}
						else
						{
							Console.WriteLine("Ошибка загрузки или невалидный треугольник.");
							continue;
						}
						break;
					case 4:
						keepRunning = false;
						continue;
				}

				bool found = MathSolver.FindIntersection(ray, triangle, out Point3D hit);
				ConsoleHelper.PrintResult(found ? hit : null);

				if (AskSave())
				{
					Console.Write("Имя файла: ");
					string savePath = Console.ReadLine();
					// Проверяем результат сохранения
					bool success = FileOperations.Save(savePath, triangle, ray);
					if (success)
					{
						Console.WriteLine($"💾 Данные успешно сохранены в {savePath}{Config.FileExtension}");
					}
				}

				Console.WriteLine(new string('-', 40) + "\n");
			}
			Console.WriteLine("Программа завершена!");
		}

		static Triangle3D InputTriangleManual()
		{
			Console.WriteLine("\nВвод вершин треугольника:");

			Point3D InputVertex(string label)
			{
				Console.WriteLine($"\n{label}:");
				return new Point3D(
					ConsoleHelper.ReadDouble("  X = "),
					ConsoleHelper.ReadDouble("  Y = "),
					ConsoleHelper.ReadDouble("  Z = "));
			}

			while (true)
			{
				var tri = new Triangle3D(
					InputVertex("Вершина 1"),
					InputVertex("Вершина 2"),
					InputVertex("Вершина 3"));

				if (Validator.IsValidTriangle(tri))
					return tri;

				Console.WriteLine("Треугольник не существует. Введите снова.");
			}
		}

		static Ray3D InputRayManual()
		{
			Console.WriteLine("\nВвод луча:");
			return new Ray3D(
				ConsoleHelper.ReadDouble("Начало луча, X = "),
				ConsoleHelper.ReadDouble("Y = "),
				ConsoleHelper.ReadDouble("Z = "),
				ConsoleHelper.ReadDouble("Горизонтальный угол (градусы) = "),
				ConsoleHelper.ReadDouble("Вертикальный угол (градусы) = "));
		}

		static Triangle3D GenerateRandomTriangle()
		{
			Random rnd = Random.Shared;
			Triangle3D tri;

			do
			{
				tri = new Triangle3D(
					new Point3D(rnd.NextDouble() * 6 - 3, rnd.NextDouble() * 6 - 3, rnd.NextDouble() * 6 - 3),
					new Point3D(rnd.NextDouble() * 4 - 2, rnd.NextDouble() * 4 - 2, rnd.NextDouble() * 4 - 2),
					new Point3D(rnd.NextDouble() * 2 - 1, rnd.NextDouble() * 2 - 1, rnd.NextDouble() * 2 - 1));
			}
			while (!Validator.IsValidTriangle(tri));

			return tri;
		}

		static Ray3D GenerateRandomRay()
		{
			Random rnd = Random.Shared;
			return new Ray3D(
				rnd.NextDouble() * 200 - 100,
				rnd.NextDouble() * 200 - 100,
				rnd.NextDouble() * 200 - 100,
				rnd.NextDouble() * 360,
				rnd.NextDouble() * 360);
		}

		static bool AskSave()
		{
			Console.Write("Сохранить данные? (1 - Да, 2 - Нет): ");
			return Console.ReadLine()?.Trim() == "1";
		}
	}
}