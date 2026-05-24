/// Глобальные константы приложения.
using System;

namespace Lab1
{
	public static class Config
	{
		public const double Epsilon = 1e-6;  /// Допустимая погрешность вычислений. Используется для сравнения double и защиты от деления на ноль.
		public const double DegreesToRadians = Math.PI / 180.0; /// Коэффициент перевода градусов в радианы (π / 180).

		public const double RayCoordMin = -100.0; /// Минимальное значение координаты для генерации случайных данных луча.
		public const double RayCoordMax = 100.0;  /// Максимальное значение координаты для генерации случайных данных луча.
		public const double AngleMin = 0.0;
		public const double AngleMax = 360.0;

		public const double TriangleRangeSmall = 1.0; /// Диапазон разброса координат для малых треугольников при случайной генерации.
		public const double TriangleRangeMedium = 2.0;  /// Диапазон разброса координат для средних треугольников при случайной генерации.
		public const double TriangleRangeLarge = 3.0; /// Диапазон разброса координат для больших треугольников при случайной генерации.

		public const string AppTitle = "Программа для определения координат точки пересечения луча и треугольника";
		public const string AppAuthor = "Худяков Дмитрий, группа 444";
		public const string AppTask = "Найти точку пересечения луча и треугольника в трехмерном пространстве";

		public const string FileExtension = ".txt";
		public const char Separator = ';';
	}
}