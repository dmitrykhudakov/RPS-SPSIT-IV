namespace Lab1
/// Класс, описывающий треугольник в трёхмерном пространстве через три вершины.
/// Используется для проверки валидности геометрии и вычисления пересечений.
{
	public class Triangle3D
	{
		public Point3D V1 { get; }
		public Point3D V2 { get; }
		public Point3D V3 { get; }

		public Triangle3D(Point3D v1, Point3D v2, Point3D v3)
		{
			V1 = v1;
			V2 = v2;
			V3 = v3;
		}
	}
}