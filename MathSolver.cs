/// Класс, содержащий математические алгоритмы решения задачи.
/// Реализует алгоритм Мёллера–Трумбора для поиска пересечения луча и треугольника.
namespace Lab1
{
	public static class MathSolver
	{
		public static bool FindIntersection(Ray3D ray, Triangle3D triangle, out Point3D hitPoint)    /// Определяет, пересекает ли луч треугольник, и вычисляет координаты точки пересечения.
		{
			hitPoint = default;

			var edge1 = triangle.V2 - triangle.V1;
			var edge2 = triangle.V3 - triangle.V1;

			var pVec = Point3D.Cross(ray.Direction, edge2);
			double det = Point3D.Dot(edge1, pVec);

			if (Math.Abs(det) < Config.Epsilon)
				return false;

			double invDet = 1.0 / det;
			var tVec = ray.Origin - triangle.V1;

			double u = invDet * Point3D.Dot(tVec, pVec);
			if (u < 0.0 || u > 1.0)
				return false;

			var qVec = Point3D.Cross(tVec, edge1);
			double v = invDet * Point3D.Dot(ray.Direction, qVec);
			if (v < 0.0 || u + v > 1.0)
				return false;

			double t = invDet * Point3D.Dot(edge2, qVec);

			if (t > Config.Epsilon)
			{
				hitPoint = ray.GetPointAt(t);
				return true;
			}

			return false;
		}
	}
}