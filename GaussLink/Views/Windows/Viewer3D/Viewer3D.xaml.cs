using GaussLink.Data.Object3D;
using GaussLink.Data.PeriodicTable;
using GaussLink.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;

namespace GaussLink.Views.Windows
{
    /// <summary>
    /// Interaction logic for Viewer3D.xaml
    /// </summary>
    public partial class Viewer3D : Window
    {
        public Viewer3D(Molecule3D molecule3D)
        {
            this.m = molecule3D;
            InitializeComponent();
            Uri iconUri = new Uri("pack://application:,,,/UI/Images/appIconWhite.png", UriKind.RelativeOrAbsolute);
            this.Icon = BitmapFrame.Create(iconUri);
            this.MouseWheel += new MouseWheelEventHandler(MouseScrollEvent);
            this.Loaded += new RoutedEventHandler(Window_Loaded);
            this.MouseLeftButtonDown += new MouseButtonEventHandler(MouseButtonDownEvent);
            this.MouseLeftButtonUp += new MouseButtonEventHandler(MouseButtonUpEvent);
            this.MouseMove += new MouseEventHandler(MouseMovingEvent);
            CompositionTarget.Rendering += Animate;
        }
        #region UI vars
        ModelVisual3D modelVisual3D;
        ModelVisual3D lightVisual3D;
        ModelVisual3D axisVisual3D;
        Model3DGroup atomGroup;
        Model3DGroup axisGroup;
        Model3DGroup lightGroup;
        Viewport3D viewport3D;
        PerspectiveCamera camera;
        string check = "✓";
        bool panelTglOn = true;
        bool showTgizmo = true;
        bool showAxis = false;
        byte c;
        #endregion

        #region 3D vars
        int measureCount = 0;
        List<Entity3D> obj3D = new List<Entity3D>();
        public Molecule3D m = new Molecule3D();
        List<GeometryModel3D> selection = new List<GeometryModel3D>();
        DiffuseMaterial selectionMaterial = new DiffuseMaterial(new SolidColorBrush(Colors.Blue));
        PeriodicTable periodicTable;
        Point3D center = new Point3D();
        Sphere sphere;
        Cylinder cylinder;
        DirectionalLight directionalLight = new DirectionalLight
        {
            Color = Colors.White,
            Direction = new Vector3D(-0.5, -1, 0)
        };
        AmbientLight ambientLight = new AmbientLight
        {
            Color = Colors.Gray
        };
        #endregion


        #region trackball vars
        bool dragAction = false;
        readonly int rotationSpeed = 1;
        Point initMousePos = new Point(0, 0);
        Point curMousePos = new Point(0, 0);
        #endregion

        #region Anim vars

        bool isPlaying = false;
        bool canPlay = true;
        bool forward = true;
        int selectedMode = 1;
        int modeCount = 0;

        double animationSpeed = 1f;

        double passedTime = 0;
        double deltaTime = 0;

        Stopwatch stopwatch = new Stopwatch();

        TimeSpan prevTime;
        TimeSpan curTime;
        TimeSpan delta;

        TranslateTransform3D translate;
        Transform3DGroup tg;
        private DiffuseMaterial bondMat;
        private int atomCount;

        #endregion






        #region Events

        #region UI events
        private void DecrementBtn_Click(object sender, RoutedEventArgs e)
        {
            canPlay = false;
            ResetVibrationTransform(m);

            selectedMode--;
            if (selectedMode <= 0)
            {
                selectedMode = 1;
            }
            vibeModeTB.Text = selectedMode.ToString();
            canPlay = true;

        }

        private void IncrementBtn_Click(object sender, RoutedEventArgs e)
        {
            canPlay = false;
            ResetVibrationTransform(m);

            selectedMode++;
            if (selectedMode > modeCount)
            {
                selectedMode = modeCount;
            }
            vibeModeTB.Text = selectedMode.ToString();
            canPlay = true;
        }

        private void VibeModeTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            canPlay = false;
            ResetVibrationTransform(m);
            if (string.IsNullOrEmpty(vibeModeTB.Text))
            {
                selectedMode = 1;

            }
            else
            {
                int i = int.Parse(vibeModeTB.Text);
                if (1 <= i)
                {
                    selectedMode = i;

                }
                if (i >= modeCount)
                {
                    selectedMode = modeCount;
                }
            }
            vibeModeTB.Text = selectedMode.ToString();
            canPlay = true;

        }

        private void OnPlayButtonClick(object sender, RoutedEventArgs e)
        {
            isPlaying = !isPlaying;
            if (isPlaying)
            {
                animButton.Content = "Playing...";
            }
            else
            {
                animButton.Content = "Play Animation";
                ResetVibrationTransform(m);
            }
        }

        private void AnimSpeedSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            animationSpeed = (float)e.NewValue;
            animSpeedLbl.Content = "Animation Speed: " + animationSpeed.ToString();
        }

        private void PanelToggleBtn_Click(object sender, RoutedEventArgs e)
        {
            panelTglOn = !panelTglOn;
            if (panelTglOn)
            {
                parentGrid.ColumnDefinitions[0].Width = new GridLength(4, GridUnitType.Star);
                parentGrid.ColumnDefinitions[1].Width = new GridLength(5);
                parentGrid.ColumnDefinitions[2].Width = new GridLength(1, GridUnitType.Star);
                tglText.Text = "❯";
            }
            else
            {
                parentGrid.ColumnDefinitions[2].Width = new GridLength(0, GridUnitType.Star);
                parentGrid.ColumnDefinitions[1].Width = new GridLength(0);
                tglText.Text = "❮";
            }

        }

        private void BgSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Color color = Color.FromRgb((byte)e.NewValue, (byte)e.NewValue, (byte)e.NewValue);
            viewPanel.Background = new SolidColorBrush(color);
        }

        private void GizmoCheck_Click(object sender, RoutedEventArgs e)
        {
            showTgizmo = !showTgizmo;
            if (showTgizmo)
            {
                overlay3D.Visibility = Visibility.Visible;
                gizmoChkText.Text = check;
            }
            else
            {
                overlay3D.Visibility = Visibility.Hidden;
                gizmoChkText.Text = string.Empty;
            }
        }

        private void AxisCheck_Click(object sender, RoutedEventArgs e)
        {
            showAxis = !showAxis;
            if (showAxis)
            {
                axisChkText.Text = check;
                var g = overlay3D.Init(0.025f, 0.05f);
                axisGroup.Children = g.Children;
            }
            else
            {
                axisChkText.Text = string.Empty;
                axisGroup.Children = null;
            }
        }


        #endregion

        #region Trackball Events
        void MouseScrollEvent(object sender, MouseWheelEventArgs e)
        {

            camera.Position = new Point3D((camera.Position.X + (camera.LookDirection.X * e.Delta / 360D)),
                (camera.Position.Y + (camera.LookDirection.Y * e.Delta / 360D)),
                (camera.Position.Z + (camera.LookDirection.Z * e.Delta / 360D)));

        }
        private void MouseButtonDownEvent(object sender, MouseButtonEventArgs e)
        {
            dragAction = true;
            initMousePos = e.GetPosition(this);

            // Perform the hit test.
            HitTestResult result = VisualTreeHelper.HitTest(this, initMousePos);

            if (!(result is RayMeshGeometry3DHitTestResult mesh_result))
            {
                int i = 0;
                foreach (GeometryModel3D item in selection)
                {
                    foreach (GeometryModel3D g in atomGroup.Children)
                    {
                        if (g == item)
                        {
                            g.Material = GetMaterial(m.MoleculeOrientation.Atoms[i].AtomicNumber);
                        }
                        i++;
                    }
                    i = 0;
                }
                selection.Clear();
                measureCount = 0;
            }
            else
            {
                // Display more detail about the hit.
                GeometryModel3D mesh = mesh_result.ModelHit as GeometryModel3D;
                if (atomGroup.Children.IndexOf(mesh) > m.MoleculeOrientation.Atoms.Count-1) return;
                foreach (GeometryModel3D g in atomGroup.Children)
                {
                    if (g == mesh)
                    {
                        if (selection.Contains(g))
                        {
                            selection.Remove(g);
                            selection.TrimExcess();
                            measureCount--;
                            g.Material = GetMaterial(m.MoleculeOrientation.Atoms[atomGroup.Children.IndexOf(g)].AtomicNumber);
                            break;
                        }

                        if (measureCount < 2)
                        {

                            measureCount++;
                            g.Material = selectionMaterial;
                            selection.Add(g);
                        }
                        else
                        {
                            int i = 0;
                            foreach (GeometryModel3D model in atomGroup.Children)
                            {
                                if (selection.Count > 0)
                                {
                                    if (model == selection[0])
                                    {
                                        model.Material = GetMaterial(m.MoleculeOrientation.Atoms[i].AtomicNumber);
                                        selection.Remove(model);
                                        selection.TrimExcess();
                                        measureCount--;
                                    }
                                    i++;
                                }
                            }
                            selection.Add(g);
                            measureCount++;
                            g.Material = selectionMaterial;
                        }

                    }
                }
            }
            switch (selection.Count)
            {
                case 0:
                    atm1.Content = "No Selection";
                    atm2.Content = "No Selection";
                    dst.Content = "N/A";
                    break;
                case 1:
                    int i = atomGroup.Children.IndexOf(selection[0]);
                    int x = obj3D[i].AtomID;
                    int n = m.MoleculeOrientation.Atoms[x-1].AtomicNumber;
                    string name = periodicTable.GetAtomName(n);
                    atm1.Content = "Atom " + x.ToString() + ": " + name + "-" + n;
                    atm2.Content = "No selection";
                    dst.Content = "N/A";
                    break;
                case 2:
                    int i1 = atomGroup.Children.IndexOf(selection[0]);
                    int x1 = obj3D[i1].AtomID;
                    int n1 = m.MoleculeOrientation.Atoms[x1-1].AtomicNumber;
                    string name1 = periodicTable.GetAtomName(n1);
                    atm1.Content = "Atom " + x1.ToString() + ": " + name1 + "-" + n1;

                    int i2 = atomGroup.Children.IndexOf(selection[1]);
                    int x2 = obj3D[i2].AtomID;
                    int n2 = m.MoleculeOrientation.Atoms[x2-1].AtomicNumber;
                    string name2 = periodicTable.GetAtomName(n2);
                    atm2.Content = "Atom " + x2.ToString() + ": " + name2 + "-" + n2;
                    float distance = (float)Math.Sqrt((obj3D[i2].Position.X - obj3D[i1].Position.X) * (obj3D[i2].Position.X - obj3D[i1].Position.X)
                        + (obj3D[i2].Position.Y - obj3D[i1].Position.Y) * (obj3D[i2].Position.Y - obj3D[i1].Position.Y)
                        + (obj3D[i2].Position.Z - obj3D[i1].Position.Z) * (obj3D[i2].Position.Z - obj3D[i1].Position.Z));

                    dst.Content = "Distance: " + distance.ToString();
                    break;
            }

        }

        private void MouseButtonUpEvent(object sender, MouseButtonEventArgs e)
        {
            dragAction = false;

        }

        void MouseMovingEvent(object sender, MouseEventArgs e)
        {
            if (dragAction)
            {
                if (Keyboard.IsKeyDown(Key.LeftShift))
                {
                    curMousePos = e.GetPosition(this);
                    Transform3DGroup tGroup = new Transform3DGroup();
                    tGroup.Children.Add(camera.Transform);
                    tGroup.Children.Add(new TranslateTransform3D(0, (curMousePos.Y - initMousePos.Y) * 0.01, 0));
                    tGroup.Children.Add(new TranslateTransform3D(0, 0, (curMousePos.X - initMousePos.X) * 0.01));

                    camera.Transform = tGroup;
                    initMousePos = e.GetPosition(this);
                    return;
                }

                curMousePos = e.GetPosition(this);
                Transform3DGroup transform3 = new Transform3DGroup();
                transform3.Children.Add(atomGroup.Transform);
                transform3.Children.Add(new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(0, 1, 0), (initMousePos.X - curMousePos.X) * rotationSpeed)));
                transform3.Children.Add(new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(0, 0, 1), (initMousePos.Y - curMousePos.Y) * rotationSpeed)));


                atomGroup.Transform = transform3;
                axisGroup.Transform = transform3;
                overlay3D.axisGroup.Transform = transform3;
                initMousePos = curMousePos;
            }
        }
        #endregion

        #endregion

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            periodicTable = new PeriodicTable();
            InitializeViewport();

            if (m.VibrationModes == null)
            {
                Model3DGroup g = CreateAtom(m);
                atomGroup.Children = g.Children;

                animationExpander.IsExpanded = false;
                animationExpander.IsEnabled = false;
                if (Application.Current.FindResource("ControlDisabledBackground") is SolidColorBrush solidColorBrush) animationExpander.Background = solidColorBrush;

            }
            else
            {
                modeCount = m.VibrationModes.Count;
                vmLbl.Content = "Vibration Modes: " + modeCount.ToString();
                Model3DGroup g = CreateAtom(m);
                atomGroup.Children = g.Children;

                animSpeedSlider.IsEnabled = true;
                animButton.IsEnabled = true;
                animSpeedSlider.ValueChanged += AnimSpeedSlider_ValueChanged;
                vibeModeTB.Text = "1";
                vibeModeTB.TextChanged += VibeModeTB_TextChanged;
                incrementBtn.Click += IncrementBtn_Click;
                decrementBtn.Click += DecrementBtn_Click;
                animButton.Click += OnPlayButtonClick;

            }

            animGrid.Children.Clear();
            animGrid.Visibility = Visibility.Hidden;
            parentGrid.Visibility = Visibility.Visible;
        }



        private void Animate(object sender, EventArgs e)
        {

            stopwatch.Start();
            prevTime = stopwatch.Elapsed;

            if (isPlaying && canPlay)
            {
                //t.Children.Clear();
                delta = prevTime - curTime;
                deltaTime = delta.TotalSeconds;
                passedTime += deltaTime;
                double interval = animationSpeed > 0 ? 1 / animationSpeed : 0;

                if (passedTime >= interval)
                {
                    forward = !forward;
                    passedTime = 0;
                }
                if (!forward)
                {
                    deltaTime *= -1;
                }

                CalculateVibrationDelta(m, deltaTime);

            }


            curTime = prevTime;

        }




        private Model3DGroup CreateAtom(Molecule3D molecule3D)
        {
            Model3DGroup Group = new Model3DGroup();

            foreach (Atom atom in molecule3D.MoleculeOrientation.Atoms)
            {
                center = new Point3D(atom.AtomCoordinates.X, atom.AtomCoordinates.Y, atom.AtomCoordinates.Z);
                sphere = new Sphere(center, 0.4, 6, 10);
                GeometryModel3D m = new GeometryModel3D
                {
                    Geometry = sphere.Geometry,
                    Material = GetMaterial(atom.AtomicNumber)
                };
                Group.Children.Add(m);
                obj3D.Add(new Entity3D(atom.CenterNumber, new Vector3D(center.X, center.Y, center.Z), Entity3DType.Atom));
                atomCount++;
            }
            foreach (Bond bond in molecule3D.MoleculeBond.Bonds)
            {
                Atom atom1 = molecule3D.MoleculeOrientation.Atoms[bond.X - 1];
                Atom atom2 = molecule3D.MoleculeOrientation.Atoms[bond.Y - 1];
                Vector3D direction = new Vector3D(
                    atom2.AtomCoordinates.X - atom1.AtomCoordinates.X,
                    atom2.AtomCoordinates.Y - atom1.AtomCoordinates.Y,
                    atom2.AtomCoordinates.Z - atom1.AtomCoordinates.Z);
                Point3D endpoint = new Point3D(atom1.AtomCoordinates.X, atom1.AtomCoordinates.Y, atom1.AtomCoordinates.Z);
                cylinder = new Cylinder(endpoint, direction, 0.05, 6);
                GeometryModel3D model3D = new GeometryModel3D
                {
                    Material = bondMat,
                    Geometry = cylinder.Geometry
                };
                Group.Children.Add(model3D);
                obj3D.Add(new Entity3D((atom1.CenterNumber, atom2.CenterNumber), new Vector3D(endpoint.X, endpoint.Y, endpoint.Z), direction, Entity3DType.Bond));
            }

            return Group;
        }


        private void CalculateVibrationDelta(Molecule3D m, double deltaTime)
        {
            tg = new Transform3DGroup();
            double x;
            double y;
            double z;

            foreach (var e in obj3D)
            {
                if (e.Type == Entity3DType.Atom)
                {
                    var d = m.VibrationModes[selectedMode - 1].AtomVibrations[e.AtomID - 1];
                    x = (d.Delta.X * deltaTime) * animationSpeed;
                    y = (d.Delta.Y * deltaTime) * animationSpeed;
                    z = (d.Delta.Z * deltaTime) * animationSpeed;
                    translate = new TranslateTransform3D(x, y, z);
                    e.Offset += new Vector3D(x, y, z);

                    Model3D m3 = atomGroup.Children[d.Atom - 1];
                    tg.Children.Add(atomGroup.Children[d.Atom - 1].Transform);
                    tg.Children.Add(translate);
                    atomGroup.Children[d.Atom - 1].Transform = tg;

                    tg = new Transform3DGroup();
                }
                else
                {
                    int i = obj3D.IndexOf(e);

                    Vector3D a1 = obj3D[e.BondID.Item1 - 1].Position;
                    Vector3D a2 = obj3D[e.BondID.Item2 - 1].Position;

                    Vector3D o1 = obj3D[e.BondID.Item1 - 1].Offset;
                    Vector3D o2 = obj3D[e.BondID.Item2 - 1].Offset;

                    Vector3D direction = (a2 + o2) - (a1 + o1);

                    Point3D endpoint = new Point3D(a1.X + o1.X, a1.Y + o1.Y, a1.Z + o1.Z);
                    cylinder = new Cylinder(endpoint, direction, 0.05, 6);

                    GeometryModel3D model3D = new GeometryModel3D
                    {
                        Material = bondMat,
                        Geometry = cylinder.Geometry
                    };
                    atomGroup.Children[i] = model3D;
                }
            }
        }


        void ResetVibrationTransform(Molecule3D molecule3D)
        {
            TranslateTransform3D t = new TranslateTransform3D(0, 0, 0);
            int i = 0;
            Vector3D zero = new Vector3D(0, 0, 0);
            foreach (Entity3D e in obj3D)
            {
                if (e.Type == Entity3DType.Atom)
                {
                    atomGroup.Children[i].Transform = t;
                    e.Offset = zero;
                }
                else
                {
                    Vector3D a1 = obj3D[e.BondID.Item1 - 1].Position;
                    Vector3D a2 = obj3D[e.BondID.Item2 - 1].Position;

                    Vector3D direction = a2 - a1;

                    Point3D endpoint = new Point3D(a1.X, a1.Y, a1.Z);
                    cylinder = new Cylinder(endpoint, direction, 0.05, 6);

                    GeometryModel3D model3D = new GeometryModel3D
                    {
                        Material = bondMat,
                        Geometry = cylinder.Geometry
                    };
                    atomGroup.Children[i] = model3D;
                }
                i++;
            }
        }



  

        DiffuseMaterial GetMaterial(int index)
        {
            (byte, byte, byte) RGB = periodicTable.GetAtomColor(index);
            return new DiffuseMaterial(new SolidColorBrush(Color.FromRgb(RGB.Item1, RGB.Item2, RGB.Item3)));
        }

        void InitializeViewport()
        {
            stopwatch = new Stopwatch();
            periodicTable = new PeriodicTable();
            modelVisual3D = new ModelVisual3D();
            lightVisual3D = new ModelVisual3D();
            axisVisual3D = new ModelVisual3D();
            axisGroup = new Model3DGroup();
            atomGroup = new Model3DGroup();
            lightGroup = new Model3DGroup();
            viewport3D = new Viewport3D();
            camera = new PerspectiveCamera();

            SolidColorBrush solidColorBrush = Application.Current.FindResource("BackgroundColour") as SolidColorBrush;
            if (solidColorBrush == null) solidColorBrush.Color = Colors.Black;
            c = solidColorBrush.Color.R;

            bondMat = GetMaterial(0);

            viewPanel.Background = solidColorBrush;
            camera.Position = new Point3D(15, 0, 0);
            camera.LookDirection = new Vector3D(-1, 0, 0);
            camera.FieldOfView = 60;
            viewport3D.Camera = camera;
            lightGroup.Children.Add(ambientLight);
            lightGroup.Children.Add(directionalLight);


            lightVisual3D.Content = lightGroup;
            axisVisual3D.Content = axisGroup;
            modelVisual3D.Content = atomGroup;
            viewport3D.Children.Add(modelVisual3D);
            viewport3D.Children.Add(axisVisual3D);
            viewport3D.Children.Add(lightVisual3D);


            viewPanel.Children.Add(viewport3D);
            bgSlider.ValueChanged += BgSlider_ValueChanged;
            bgSlider.Value = c;
        }

        
    }
}
