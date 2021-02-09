using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace GaussLink.Object3D
{
    public class Sphere:Shape
    {

        public Sphere(Point3D Center, double Radius, int Rings,int Segments)
        {
            CreateSphere(Geometry, Center, Radius, Rings, Segments);
        }

        private void CreateSphere(MeshGeometry3D mesh, Point3D center, double radius, int num_phi, int num_theta)
        {
            double phi0, theta0;
            double dphi = Math.PI / num_phi;
            double dtheta = 2 * Math.PI / num_theta;

            phi0 = 0;
            double y0 = radius * Math.Cos(phi0);
            double r0 = radius * Math.Sin(phi0);
            for (int i = 0; i < num_phi; i++)
            {
                double phi1 = phi0 + dphi;
                double y1 = radius * Math.Cos(phi1);
                double r1 = radius * Math.Sin(phi1);

                // Point ptAB has phi value A and theta value B.
                // For example, pt01 has phi = phi0 and theta = theta1.
                // Find the points with theta = theta0.
                theta0 = 0;
                Point3D pt00 = new Point3D(
                    center.X + r0 * Math.Cos(theta0),
                    center.Y + y0,
                    center.Z + r0 * Math.Sin(theta0));
                Point3D pt10 = new Point3D(
                    center.X + r1 * Math.Cos(theta0),
                    center.Y + y1,
                    center.Z + r1 * Math.Sin(theta0));
                for (int j = 0; j < num_theta; j++)
                {
                    // Find the points with theta = theta1.
                    double theta1 = theta0 + dtheta;
                    Point3D pt01 = new Point3D(
                        center.X + r0 * Math.Cos(theta1),
                        center.Y + y0,
                        center.Z + r0 * Math.Sin(theta1));
                    Point3D pt11 = new Point3D(
                        center.X + r1 * Math.Cos(theta1),
                        center.Y + y1,
                        center.Z + r1 * Math.Sin(theta1));

                    // Create the triangles.
                    AddTriangle(mesh, pt00, pt11, pt10);
                    AddTriangle(mesh, pt00, pt01, pt11);

                    // Move to the next value of theta.
                    theta0 = theta1;
                    pt00 = pt01;
                    pt10 = pt11;
                }

                // Move to the next value of phi.
                phi0 = phi1;
                y0 = y1;
                r0 = r1;
            }
        }

       

    }
}
