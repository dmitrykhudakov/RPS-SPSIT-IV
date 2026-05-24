/// Класс, описывающий луч в трёхмерном пространстве.
/// Хранит точку начала и нормализованный вектор направления.
/// Направление вычисляется автоматически из горизонтального и вертикального углов.
using System;

namespace Lab1
{
	public class Ray3D
	{
		public Point3D Origin { get; }
		public Point3D Direction { get; } /// Нормализованный вектор направления луча.
		public double HorizontalAngle { get; }
		public double VerticalAngle { get; }

		public Ray3D(double x, double y, double z, double horizontalDeg, double verticalDeg)
		/// Инициализирует луч по координатам начала и углам направления.
		/// Углы преобразуются из сферической системы координат в декартову.
		{
			Origin = new Point3D(x, y, z);
			HorizontalAngle = horizontalDeg;
			VerticalAngle = verticalDeg;

			double hRad = horizontalDeg * Config.DegreesToRadians;
			double vRad = verticalDeg * Config.DegreesToRadians;

			double dx = Math.Cos(vRad) * Math.Cos(hRad);
			double dy = Math.Cos(vRad) * Math.Sin(hRad);
			double dz = Math.Sin(vRad);

			Direction = new Point3D(dx, dy, dz).Normalize();
		}
		/// Возвращает точку на луче, находящуюся на заданном расстоянии от начала.
		public Point3D GetPointAt(double distance) => Origin + Direction * distance;
	}
}