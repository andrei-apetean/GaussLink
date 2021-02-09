using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace GaussLink.Object3D
{
    public class Cube
    {
        public MeshGeometry3D Geometry = new MeshGeometry3D();



        public Cube()
        {
            Geometry.Positions.Add(new Point3D(1, -1, -1));
            Geometry.Positions.Add(new Point3D(1, -1, 1));
            Geometry.Positions.Add(new Point3D(-1, -1, 1));
            Geometry.Positions.Add(new Point3D(-1, -1, -1));
            Geometry.Positions.Add(new Point3D(1, 1, -1));
            Geometry.Positions.Add(new Point3D(1, 1, 1));
            Geometry.Positions.Add(new Point3D(-1, 1, 1));
            Geometry.Positions.Add(new Point3D(-1, 1, -1));


            Geometry.TriangleIndices.Add(1);
            Geometry.TriangleIndices.Add(3);
            Geometry.TriangleIndices.Add(0);
            Geometry.TriangleIndices.Add(7);
            Geometry.TriangleIndices.Add(5);
            Geometry.TriangleIndices.Add(4);
            Geometry.TriangleIndices.Add(4);
            Geometry.TriangleIndices.Add(1);
            Geometry.TriangleIndices.Add(0);
            Geometry.TriangleIndices.Add(5);
            Geometry.TriangleIndices.Add(2);
            Geometry.TriangleIndices.Add(1);
            Geometry.TriangleIndices.Add(2);
            Geometry.TriangleIndices.Add(7);
            Geometry.TriangleIndices.Add(3);
            Geometry.TriangleIndices.Add(0);
            Geometry.TriangleIndices.Add(7);
            Geometry.TriangleIndices.Add(4);
            Geometry.TriangleIndices.Add(1);
            Geometry.TriangleIndices.Add(2);
            Geometry.TriangleIndices.Add(3);
            Geometry.TriangleIndices.Add(7);
            Geometry.TriangleIndices.Add(6);
            Geometry.TriangleIndices.Add(5);
            Geometry.TriangleIndices.Add(4);
            Geometry.TriangleIndices.Add(5);
            Geometry.TriangleIndices.Add(1);
            Geometry.TriangleIndices.Add(5);
            Geometry.TriangleIndices.Add(6);
            Geometry.TriangleIndices.Add(2);
            Geometry.TriangleIndices.Add(2);
            Geometry.TriangleIndices.Add(6);
            Geometry.TriangleIndices.Add(7);
            Geometry.TriangleIndices.Add(0);
            Geometry.TriangleIndices.Add(3);
            Geometry.TriangleIndices.Add(7);
        }
    }
}
