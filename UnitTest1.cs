using NUnit.Framework;
using Lab1;

namespace Lab1.Tests
{
	/// Модульные тесты для проверки геометрических вычислений и валидации данных.
	[TestFixture]
	public class GeometryTests
	{
		[Test]
		public void IsValidTriangle_ValidTriangle_ReturnsTrue()
		{
			var tri = new Triangle3D(new Point3D(0, 0, 0), new Point3D(0, 5, 0), new Point3D(5, 0, 0));

			// В NUnit: Assert.True() или Assert.That(..., Is.True)
			Assert.True(Validator.IsValidTriangle(tri));
		}

		[Test]
		public void IsValidTriangle_DegenerateTriangle_ReturnsFalse()
		{
			// Точки лежат на одной прямой → вырожденный треугольник
			var tri = new Triangle3D(new Point3D(0, 0, 0), new Point3D(1, 1, 1), new Point3D(2, 2, 2));

			Assert.IsFalse(Validator.IsValidTriangle(tri));
		}

		[Test]
		public void FindIntersection_DirectHit_ReturnsTrueAndCorrectPoint()
		{
			var tri = new Triangle3D(new Point3D(-1, -1, 0), new Point3D(1, -1, 0), new Point3D(0, 1, 0));
			var ray = new Ray3D(0, 0, -5, 0, 90);

			bool intersected = MathSolver.FindIntersection(ray, tri, out Point3D hit);

			Assert.True(intersected);
			Assert.That(hit.X, Is.EqualTo(0).Within(0.001));
			Assert.That(hit.Y, Is.EqualTo(0).Within(0.001));
			Assert.That(hit.Z, Is.EqualTo(0).Within(0.001));
		}

		[Test]
		public void FindIntersection_ParallelRay_ReturnsFalse()
		{
			var tri = new Triangle3D(new Point3D(0, 10, 0), new Point3D(0, 51, 0), new Point3D(15, 50, 70));
			var ray = new Ray3D(0, 0, 0, 0, 0);

			bool intersected = MathSolver.FindIntersection(ray, tri, out _);

			Assert.False(intersected);
		}

		[Test]
		public void FindIntersection_RayStartsOnPlane_ReturnsFalse()
		{
			var tri = new Triangle3D(new Point3D(0, 0, 0), new Point3D(0, 5, 0), new Point3D(5, 0, 0));
			var ray = new Ray3D(0, 0, 0, 0, 90);

			bool intersected = MathSolver.FindIntersection(ray, tri, out _);

			Assert.False(intersected);
		}
	}
}