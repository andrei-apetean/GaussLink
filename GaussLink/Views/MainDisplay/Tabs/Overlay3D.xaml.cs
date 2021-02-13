using GaussLink.Data.Object3D;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace GaussLink.Views.MainDisplay.Tabs
{
    /// <summary>
    /// Interaction logic for Overlay3D.xaml
    /// </summary>
    public partial class Overlay3D : UserControl
    {
        ModelVisual3D modelVisual3D;
        ModelVisual3D lightVisual3D;
        public Model3DGroup axisGroup;
        Model3DGroup lightGroup;
        Viewport3D viewport3D;
        PerspectiveCamera camera;
        DirectionalLight directionalLight = new DirectionalLight
        {
            Color = Colors.White,
            Direction = new Vector3D(-0.5, -1, 0)
        };
        AmbientLight ambientLight = new AmbientLight
        {
            Color = Colors.Gray
        };
        public Overlay3D()
        {
            InitializeComponent();
            this.Loaded += Overlay3D_Loaded;
        }

        private void Overlay3D_Loaded(object sender, RoutedEventArgs e)
        {
            modelVisual3D = new ModelVisual3D();
            lightVisual3D = new ModelVisual3D();
            axisGroup = new Model3DGroup();
            lightGroup = new Model3DGroup();
            viewport3D = new Viewport3D();
            camera = new PerspectiveCamera();

            camera.Position = new Point3D(15, 0, 0);
            camera.LookDirection = new Vector3D(-1, 0, 0);
            camera.FieldOfView = 60;
            viewport3D.Camera = camera;
            lightGroup.Children.Add(ambientLight);
            lightGroup.Children.Add(directionalLight);

            axisGroup = Init(0.25f, 0.5f);

            lightVisual3D.Content = lightGroup;

            modelVisual3D.Content = axisGroup;
            viewport3D.Children.Add(modelVisual3D);
            viewport3D.Children.Add(lightVisual3D);
            this.Content = viewport3D;
        }

        public Model3DGroup Init(float cylinderRadius, float coneRadius)
        {
            Model3DGroup m = new Model3DGroup();
            GeometryModel3D x = new GeometryModel3D();
            DiffuseMaterial r = new DiffuseMaterial(new SolidColorBrush(Colors.Red));
            Cone c = new Cone(new Point3D(5, 0, 0), new Vector3D(1, 0, 0), coneRadius, 12);
            x.Geometry = c.Geometry;
            x.Material = r;
            m.Children.Add(x);
            x = new GeometryModel3D();
            Cylinder cn = new Cylinder(new Point3D(0, 0, 0), new Vector3D(5, 0, 0), cylinderRadius, 12);
            x.Geometry = cn.Geometry;
            x.Material = r;
            m.Children.Add(x);

            DiffuseMaterial gr = new DiffuseMaterial(new SolidColorBrush(Colors.Green));
            GeometryModel3D y = new GeometryModel3D();
            c = new Cone(new Point3D(0, 5, 0), new Vector3D(0, 1, 0), coneRadius, 12);
            y.Geometry = c.Geometry;
            y.Material = gr;
            m.Children.Add(y);
            cn = new Cylinder(new Point3D(0, 0, 0), new Vector3D(0, 5, 0), cylinderRadius, 12);
            y = new GeometryModel3D();
            y.Geometry = cn.Geometry;
            y.Material = gr;
            m.Children.Add(y);

            DiffuseMaterial b = new DiffuseMaterial(new SolidColorBrush(Colors.Blue));
            GeometryModel3D z = new GeometryModel3D();
            c = new Cone(new Point3D(0, 0, 5), new Vector3D(0, 0, 1), coneRadius, 12);
            z.Geometry = c.Geometry;
            z.Material = b;
            m.Children.Add(z);
            cn = new Cylinder(new Point3D(0, 0, 0), new Vector3D(0, 0, 5), cylinderRadius, 12);
            z = new GeometryModel3D();
            z.Geometry = cn.Geometry;
            z.Material = b;
            m.Children.Add(z);


            return m;
        }
    }
}
