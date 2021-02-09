using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace GaussLink.Object3D
{
    public class Shape
    {
        public MeshGeometry3D Geometry = new MeshGeometry3D();
        public void AddTriangle(MeshGeometry3D mesh, Point3D point1, Point3D point2, Point3D point3)
        {
            // Create the points.
            int index1 = mesh.Positions.Count;
            mesh.Positions.Add(point1);
            mesh.Positions.Add(point2);
            mesh.Positions.Add(point3);

            // Create the triangle.
            mesh.TriangleIndices.Add(index1++);
            mesh.TriangleIndices.Add(index1++);
            mesh.TriangleIndices.Add(index1);
        }
    }
}
