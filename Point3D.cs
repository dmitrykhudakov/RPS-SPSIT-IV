/// Неизменяемая структура, представляющая точку или вектор в трёхмерном пространстве.
/// Объединяет функциональность классов Point и Vector для упрощения векторной алгебры.
using System;

namespace Lab1
{
	public readonly record struct Point3D(double X, double Y, double Z)
	{
		public double Length => Math.Sqrt(X * X + Y * Y + Z * Z);
		/// Вычисляет евклидову длину вектора (норму).
		/// Формула: √(x² + y² + z²)
		public Point3D Normalize() /// Возвращает нормализованный вектор (единичной длины).
		{
			double len = Length;
			return len > Config.Epsilon ? new Point3D(X / len, Y / len, Z / len) : this;
		}

		public static double Dot(Point3D a, Point3D b) =>  /// Скалярное произведение двух векторов.
			a.X * b.X + a.Y * b.Y + a.Z * b.Z;

		public static Point3D Cross(Point3D a, Point3D b) => new(  /// Векторное произведение двух векторов.
			a.Y * b.Z - a.Z * b.Y,
			a.Z * b.X - a.X * b.Z,
			a.X * b.Y - a.Y * b.X);

		public static Point3D operator -(Point3D a, Point3D b) =>
			new(a.X - b.X, a.Y - b.Y, a.Z - b.Z);

		public static Point3D operator *(Point3D v, double scalar) => /// Умножение вектора на скаляр.
			new(v.X * scalar, v.Y * scalar, v.Z * scalar);

		public static Point3D operator +(Point3D point, Point3D vector) =>
			new(point.X + vector.X, point.Y + vector.Y, point.Z + vector.Z);
	}
}