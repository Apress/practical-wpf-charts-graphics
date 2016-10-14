using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace Geometries
{
    public class CubeGeometryWithNormals
    {
        // Define private fields:
        private double length = 1.0;
        private double width = 1.0;
        private double height = 1.0;
        private bool isNormal = false;
        private Point3D center = new Point3D();

        // Define public properties:
        public double Length
        {
            get { return length; }
            set { length = value; }
        }

        public double Width
        {
            get { return width; }
            set { width = value; }
        }

        public double Height
        {
            get { return height; }
            set { height = value; }
        }

        public Point3D Center
        {
            get { return center; }
            set { center = value; }
        }

        // Get-only property generates MeshGeometry3D object:
        public MeshGeometry3D Mesh3D
        {
            get { return GetMesh3D(); }
        }

        private MeshGeometry3D GetMesh3D()
        {
            MeshGeometry3D mesh = new MeshGeometry3D();
            Point3D[] pts = new Point3D[8];

            double hl = 0.5 * Length;
            double hw = 0.5 * Width;
            double hh = 0.5 * Height;

            pts[0] = new Point3D(hl, hh, hw);
            pts[1] = new Point3D(hl, hh, -hw);
            pts[2] = new Point3D(-hl, hh, -hw);
            pts[3] = new Point3D(-hl, hh, hw);
            pts[4] = new Point3D(-hl, -hh, hw);
            pts[5] = new Point3D(-hl, -hh, -hw);
            pts[6] = new Point3D(hl, -hh, -hw);
            pts[7] = new Point3D(hl, -hh, hw);

            for (int i = 0; i < 8; i++)
            {
                pts[i] += (Vector3D)Center;
            }

            // Top surface (0-3):
            for (int i = 0; i < 4; i++)
                mesh.Positions.Add(pts[i]);
            
            mesh.TriangleIndices.Add(0);
            mesh.TriangleIndices.Add(1);
            mesh.TriangleIndices.Add(2);
            mesh.Normals.Add(GetNormal(mesh.Positions[0], mesh.Positions[1], mesh.Positions[2]));
            mesh.Normals.Add(GetNormal(mesh.Positions[0], mesh.Positions[1], mesh.Positions[2]));
            mesh.Normals.Add(GetNormal(mesh.Positions[0], mesh.Positions[1], mesh.Positions[2]));

            mesh.TriangleIndices.Add(2);
            mesh.TriangleIndices.Add(3);
            mesh.TriangleIndices.Add(0);
            mesh.Normals.Add(GetNormal(mesh.Positions[2], mesh.Positions[3], mesh.Positions[0]));
            mesh.Normals.Add(GetNormal(mesh.Positions[2], mesh.Positions[3], mesh.Positions[0]));
            mesh.Normals.Add(GetNormal(mesh.Positions[2], mesh.Positions[3], mesh.Positions[0]));


            //Bottom surface (4-7):
            for (int i = 4; i < 8; i++)
                mesh.Positions.Add(pts[i]);

            mesh.TriangleIndices.Add(4);
            mesh.TriangleIndices.Add(5);
            mesh.TriangleIndices.Add(6);
            mesh.Normals.Add(GetNormal(mesh.Positions[4], mesh.Positions[5], mesh.Positions[6]));
            mesh.Normals.Add(GetNormal(mesh.Positions[4], mesh.Positions[5], mesh.Positions[6]));
            mesh.Normals.Add(GetNormal(mesh.Positions[4], mesh.Positions[5], mesh.Positions[6]));

            mesh.TriangleIndices.Add(6);
            mesh.TriangleIndices.Add(7);
            mesh.TriangleIndices.Add(4);
            mesh.Normals.Add(GetNormal(mesh.Positions[6], mesh.Positions[7], mesh.Positions[4]));
            mesh.Normals.Add(GetNormal(mesh.Positions[6], mesh.Positions[7], mesh.Positions[4]));
            mesh.Normals.Add(GetNormal(mesh.Positions[6], mesh.Positions[7], mesh.Positions[4]));

            // Front surface (8-11):
            mesh.Positions.Add(pts[0]);
            mesh.Positions.Add(pts[3]);
            mesh.Positions.Add(pts[4]);
            mesh.Positions.Add(pts[7]);

            mesh.TriangleIndices.Add(8);
            mesh.TriangleIndices.Add(9);
            mesh.TriangleIndices.Add(10);
            mesh.Normals.Add(GetNormal(mesh.Positions[8], mesh.Positions[9], mesh.Positions[10]));
            mesh.Normals.Add(GetNormal(mesh.Positions[8], mesh.Positions[9], mesh.Positions[10]));
            mesh.Normals.Add(GetNormal(mesh.Positions[8], mesh.Positions[9], mesh.Positions[10]));

            mesh.TriangleIndices.Add(10);
            mesh.TriangleIndices.Add(11);
            mesh.TriangleIndices.Add(8);
            mesh.Normals.Add(GetNormal(mesh.Positions[10], mesh.Positions[11], mesh.Positions[8]));
            mesh.Normals.Add(GetNormal(mesh.Positions[10], mesh.Positions[11], mesh.Positions[8]));
            mesh.Normals.Add(GetNormal(mesh.Positions[10], mesh.Positions[11], mesh.Positions[8]));

            // Back surface (12-15):
            mesh.Positions.Add(pts[1]);
            mesh.Positions.Add(pts[2]);
            mesh.Positions.Add(pts[5]);
            mesh.Positions.Add(pts[6]);

            mesh.TriangleIndices.Add(12);
            mesh.TriangleIndices.Add(15);
            mesh.TriangleIndices.Add(14);
            mesh.Normals.Add(GetNormal(mesh.Positions[12], mesh.Positions[15], mesh.Positions[14]));
            mesh.Normals.Add(GetNormal(mesh.Positions[12], mesh.Positions[15], mesh.Positions[14]));
            mesh.Normals.Add(GetNormal(mesh.Positions[12], mesh.Positions[15], mesh.Positions[14]));

            mesh.TriangleIndices.Add(14);
            mesh.TriangleIndices.Add(13);
            mesh.TriangleIndices.Add(12);
            mesh.Normals.Add(GetNormal(mesh.Positions[14], mesh.Positions[13], mesh.Positions[12]));
            mesh.Normals.Add(GetNormal(mesh.Positions[14], mesh.Positions[13], mesh.Positions[12]));
            mesh.Normals.Add(GetNormal(mesh.Positions[14], mesh.Positions[13], mesh.Positions[12]));

            // Left surface (16-19):
            mesh.Positions.Add(pts[2]);
            mesh.Positions.Add(pts[3]);
            mesh.Positions.Add(pts[4]);
            mesh.Positions.Add(pts[5]);

            mesh.TriangleIndices.Add(16);
            mesh.TriangleIndices.Add(19);
            mesh.TriangleIndices.Add(18);
            mesh.Normals.Add(GetNormal(mesh.Positions[16], mesh.Positions[19], mesh.Positions[18]));
            mesh.Normals.Add(GetNormal(mesh.Positions[16], mesh.Positions[19], mesh.Positions[18]));
            mesh.Normals.Add(GetNormal(mesh.Positions[16], mesh.Positions[19], mesh.Positions[18]));

            mesh.TriangleIndices.Add(18);
            mesh.TriangleIndices.Add(17);
            mesh.TriangleIndices.Add(16);
            mesh.Normals.Add(GetNormal(mesh.Positions[18], mesh.Positions[17], mesh.Positions[16]));
            mesh.Normals.Add(GetNormal(mesh.Positions[18], mesh.Positions[17], mesh.Positions[16]));
            mesh.Normals.Add(GetNormal(mesh.Positions[18], mesh.Positions[17], mesh.Positions[16]));

            // Right surface (20-23):
            mesh.Positions.Add(pts[0]);
            mesh.Positions.Add(pts[1]);
            mesh.Positions.Add(pts[6]);
            mesh.Positions.Add(pts[7]);

            mesh.TriangleIndices.Add(20);
            mesh.TriangleIndices.Add(23);
            mesh.TriangleIndices.Add(22);
            mesh.Normals.Add(GetNormal(mesh.Positions[20], mesh.Positions[23], mesh.Positions[22]));
            mesh.Normals.Add(GetNormal(mesh.Positions[20], mesh.Positions[23], mesh.Positions[22]));
            mesh.Normals.Add(GetNormal(mesh.Positions[20], mesh.Positions[23], mesh.Positions[22]));

            mesh.TriangleIndices.Add(22);
            mesh.TriangleIndices.Add(21);
            mesh.TriangleIndices.Add(20);
            mesh.Normals.Add(GetNormal(mesh.Positions[22], mesh.Positions[21], mesh.Positions[20]));
            mesh.Normals.Add(GetNormal(mesh.Positions[22], mesh.Positions[21], mesh.Positions[20]));
            mesh.Normals.Add(GetNormal(mesh.Positions[22], mesh.Positions[21], mesh.Positions[20]));
            
            mesh.Freeze();
            return mesh;
        }

        private Vector3D GetNormal(Point3D p0, Point3D p1, Point3D p2)
        {
            if (isNormal == true)
            {
                Vector3D v1 = new Vector3D(p1.X - p0.X, p1.Y - p0.Y, p1.Z - p0.Z);
                Vector3D v2 = new Vector3D(p2.X - p1.X, p2.Y - p1.Y, p2.Z - p1.Z);
                return Vector3D.CrossProduct(v1, v2);
            }
            else
                return new Vector3D();
        }
    }
}

