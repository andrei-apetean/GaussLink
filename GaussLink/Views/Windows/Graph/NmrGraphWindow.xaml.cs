using GalaSoft.MvvmLight.Messaging;
using GaussLink.Data.Messages;
using GaussLink.Models;
using GaussLink.ViewModels.Themes;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace GaussLink.Views.Windows.Graph
{
    /// <summary>
    /// Interaction logic for NmrGraphWindow.xaml
    /// </summary>
    public partial class NmrGraphWindow : Window
    {
        private NMR nmr;
        private SolidColorBrush plotBrush = new SolidColorBrush(Colors.White);
        private int  npoints = 500;
        List<float> xValues = new List<float>();
        List<Tuple<int, int>> pointIndices;
        float xMargin, yMargin;
        float xIntervalUnit = 50;
        float yIntervalUnit = 0.05f;
        float plotHeight, plotWidth;
        float screenWidth, screenHeight;
        float xMax, yMax, xMin, yMin;
        float g = 0.8f;
        bool canFire = false;


        public NmrGraphWindow(NMR nmr)
        {
            this.nmr = nmr;
            InitializeComponent();
            this.Loaded += NmrGraphView_Loaded;
            Messenger.Default.Register<ThemeChangedMessage>(this, ThemeChanged);
            Uri iconUri = new Uri("pack://application:,,,/UI/Images/appIconWhite.png", UriKind.RelativeOrAbsolute);
            this.Icon = BitmapFrame.Create(iconUri);
            plotView.SizeChanged += PlotView_SizeChanged;
        }

        private void PlotView_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (xValues.Count > 0)
            {
                screenWidth = (float)plotView.ActualWidth;
                xMargin = 50f;
                screenHeight = (float)plotView.ActualHeight;
                yMargin = 50;
                plotHeight = screenHeight - yMargin;
                plotWidth = screenWidth - xMargin;
                plotView.Children.Clear();
                xLabels.Children.Clear();
                plot.Children.Clear();
                plotView.Children.Clear();
                Draw(xValues);
            }
            
        }

        private void ThemeChanged(ThemeChangedMessage obj)
        {
            switch (obj.ThemeType)
            {
                case ThemeType.Dark:
                case ThemeType.ColourfulDark:
                    plotBrush = new SolidColorBrush(Colors.White);
                    break;
                case ThemeType.Light:

                case ThemeType.ColourfulLight:
                    plotBrush = new SolidColorBrush(Colors.Black);
                    break;
            }

            if (xValues.Count>0)
            {
                Draw(xValues);
            }
        }

        private void NmrGraphView_Loaded(object sender, RoutedEventArgs e)
        {
            if(nmr!=null)
            {
                screenWidth = (float)plotView.ActualWidth;
                xMargin = 50;
                screenHeight = (float)plotView.ActualHeight;
                yMargin = 50;
                plotHeight = screenHeight - yMargin;
                plotWidth = screenWidth - xMargin;
                sFactor.Text = g.ToString(CultureInfo.InvariantCulture);
                foreach (MagneticShieldTensor item in nmr.ShieldingTensors)
                {
                    xValues.Add(item.Isotropic);
                }
                Draw(xValues);
                canFire = true;
            }
        }
        void Draw(List<float> values)
        {
            plotView.Children.Clear();
            plot.Children.Clear();
            xLabels.Children.Clear();
            lines.Children.Clear();
            cpoints.Children.Clear();

            Label deg = new Label
            {
                Content = "Degeneracy",
                RenderTransform = new TranslateTransform(plotWidth, plotHeight / 2),
                LayoutTransform = new RotateTransform(90)
            };
            Label shield = new Label
            {
                Content = "Shielding (ppm)",
                RenderTransform = new TranslateTransform((plotWidth + xMargin) / 2, 20)
            };
            plotView.Children.Add(deg);
            plotView.Children.Add(shield);

            Point[] points = CalculateGraphPoints(values);
            Polyline xAxis = new Polyline
            {
                Stroke = new SolidColorBrush(Colors.Red)
            };
            xAxis.Points.Add(new Point(xMargin, plotHeight));
            xAxis.Points.Add(new Point(plotWidth, plotHeight));
            plotView.Children.Add(xAxis);

            Polyline yAxis = new Polyline
            {
                Stroke = new SolidColorBrush(Colors.Green)
            };
            yAxis.Points.Add(new Point(xMargin, yMargin));
            yAxis.Points.Add(new Point(xMargin, plotHeight));
            plotView.Children.Add(yAxis);
            for (int i = 0; i <= (xMax / xIntervalUnit); i++)
            {
                Polyline l = new Polyline();
                float x = OldToNewRangeConverter(i * xIntervalUnit, xMax, xMin, plotWidth, xMargin);
                Point p1 = new Point(x, plotHeight);
                Point p2 = new Point(x, yMargin);
                l.Stroke = new SolidColorBrush(Colors.Gray);
                l.Points.Add(p1);
                l.Points.Add(p2);
                l.StrokeDashArray = new DoubleCollection() { 2 };
                Label b = new Label
                {
                    Content = (i * xIntervalUnit).ToString(),
                    RenderTransform = new TranslateTransform(x - 15, plotHeight + yMargin / 2)
                };
                xLabels.Children.Add(b);
                plot.Children.Add(l);

            }
            for (int i = 0; i <= (yMax / yIntervalUnit)+1; i++)
            {
                Polyline l = new Polyline();
                float y = OldToNewRangeConverter(yIntervalUnit * i, yMax, yMin, yMargin, plotHeight);
                Point p1 = new Point(xMargin, y);
                Point p2 = new Point(plotWidth, y);
                l.Stroke = new SolidColorBrush(Colors.Gray);
                l.Points.Add(p1);
                l.Points.Add(p2);
                l.StrokeDashArray = new DoubleCollection() { 2 };
                Label b = new Label
                {
                    Content = (i * yIntervalUnit).ToString(),
                    RenderTransform = new TranslateTransform(0, y - 10)
                };
                plotView.Children.Add(b);
                if (i > 0) plotView.Children.Add(l);
            }


            Polyline plotLine = new Polyline
            {
                Stroke = plotBrush
            };


            for (int i = 0; i < points.Length - 2; i++)
            {
                float x = OldToNewRangeConverter((float)points[i].X, xMax, xMin, plotWidth, xMargin);
                float y = OldToNewRangeConverter((float)points[i].Y, yMax, yMin, plotHeight, yMargin);
                    plotLine.Points.Add(new Point(x, plotHeight + yMargin - y));
            }
            DrawLines(values);

            plot.Children.Add(plotLine);

        }

        Point[] CalculateGraphPoints(List<float> xValues)
        {
            Point[] points = new Point[npoints];
            float y = 0;
            float min = xValues.Any() ? xValues.Min() : 0;
            float max = xValues.Any() ? xValues.Max() : 0;
            float[] ints = new float[npoints];

            float Emin = min - min / 2;
            Emin = 0;
            xMin = 0;

            float Emax = (float)RoundUpToNearest(max);

            xMax = Emax;
            yMax = 2;
            yMin = 0;
            xIntervalUnit = xMax / 10;
            yIntervalUnit = yMax / 10;
            var dE = (Emax - Emin) / (npoints - 1);
            for (int i = 0; i < nmr.ShieldingTensors.Count; i++)
            {
                var amp = 1;
                var x = xValues[i];
                var E = Emin;
                int j = 0;
                do
                {
                    y = L(E, x, amp, g);
                    ints[j] += y;
                    points[j] = new Point(E, ints[j]);



                    j++;

                    E += dE;
                } while (E <= Emax);
            }
            return points;

        }


        void DrawLines(List<float> values)
        {
            pointIndices = new List<Tuple<int, int>>();
            int i = 0;
            foreach (float v in values)
            {
                Polyline p = new Polyline();
                Rectangle r = new Rectangle
                {
                    Fill = new SolidColorBrush(Colors.Green),
                    Width = 10,
                    Height = 10
                };
                r.MouseEnter += R_MouseEnter;
                r.MouseLeave += R_MouseLeave;
                float x = OldToNewRangeConverter(v, xMax, xMin, plotWidth, xMargin);
                float y = OldToNewRangeConverter(1, yMax, yMin, plotHeight, yMargin);
                r.RenderTransform = new TranslateTransform(x - 5, plotHeight + yMargin - y - 5);
                Point p1 = new Point(x, plotHeight + yMargin - y);
                Point p2 = new Point(x, plotHeight);
                p.Stroke = new SolidColorBrush(Colors.Blue);
                p.Points.Add(p1);
                p.Points.Add(p2);

                cpoints.Children.Add(r);
                lines.Children.Add(p);
                pointIndices.Add(new Tuple<int, int>(cpoints.Children.IndexOf(r), i));
                i++;
            }
        }
        float L(float E, float x, float amp, float g)
        {
            //x este eps[i];
            return amp * g * g / ((E - x) * (E - x) + g * g);
        }

        private void R_MouseLeave(object sender, MouseEventArgs e)
        {
            pointVal.Text = "Hover over points to see their values";
        }

        private void R_MouseEnter(object sender, MouseEventArgs e)
        {
            Rectangle r = sender as Rectangle;
            int i = cpoints.Children.IndexOf(r);
            int p = pointIndices.Single(x => x.Item1 == i).Item2;
            float value = xValues[p];
                    
            pointVal.Text = "(" + value.ToString(CultureInfo.InvariantCulture) + " , " + 1+ ")";
        }
      
        private void Sfactor_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!canFire) return;

            TextBox t = sender as TextBox;
            if (string.IsNullOrEmpty(t.Text) || t.Text.EndsWith(".") )
            {
                return;

            }
            else
            {
                float? value = null;
                try
                {
                    value = float.Parse(t.Text, CultureInfo.InvariantCulture);
                }
                catch (Exception)
                { }
                if (value == null)
                {
                    return;
                }
                g = (float)value;
                //t.Text = g.ToString();
                if (nmr != null)
                {
                    Draw(xValues);
                }
            }
        }

        float OldToNewRangeConverter(float Value, float OldMax, float OldMin, float NewMax, float NewMin)
        {
            float OldRange = OldMax - OldMin;
            float NewRange = NewMax - NewMin;
            return (((Value - OldMin) * NewRange) / OldRange) + NewMin;
        }


        double DecimalRoundUp(double passednumber)
        {
            if (passednumber == 0) return passednumber;
            else
            {
                return Math.Ceiling(passednumber * 10) / 10;
            }
        }

        double RoundUpToNearest(double passednumber)
        {
            if (passednumber == 0)
            {
                return passednumber;
            }
            else
            {
                double x = Math.Pow(10, Math.Ceiling(Math.Log10(passednumber)));
                if (x / 2 > passednumber)
                    return x / 2;
                if (x * 0.8f > passednumber)
                    return x * 0.8f;
                else return x;
            }
        }

        private void Inverse_Checked(object sender, RoutedEventArgs e)
        {

            foreach (Label item in xLabels.Children)
            {
                item.LayoutTransform = new ScaleTransform(-1, 1, 0.5, 0.5);
            }
            plot.LayoutTransform = new ScaleTransform(-1, 1);
            cpoints.LayoutTransform = new ScaleTransform(-1, 1);
            lines.LayoutTransform = new ScaleTransform(-1, 1);

            xLabels.LayoutTransform = new ScaleTransform(-1, 1);

        }
        private void Points_Checked(object sender, RoutedEventArgs e)
        {
            cpoints.Visibility = Visibility.Visible;

        }
        private void Lines_Checked(object sender, RoutedEventArgs e)
        {
            lines.Visibility = Visibility.Visible;

        }
        private void Points_Unchecked(object sender, RoutedEventArgs e)
        {
            cpoints.Visibility = Visibility.Hidden;

        }
        private void Lines_Unchecked(object sender, RoutedEventArgs e)
        {
            lines.Visibility = Visibility.Hidden;

        }

        private void Inverse_Unchecked(object sender, RoutedEventArgs e)
        {

            foreach (Label item in xLabels.Children)
            {
                item.LayoutTransform = new ScaleTransform(1, 1);
            }
            xLabels.LayoutTransform = new ScaleTransform(1, 1);
            plot.LayoutTransform = new ScaleTransform(1, 1);
            cpoints.LayoutTransform = new ScaleTransform(1, 1);
            lines.LayoutTransform = new ScaleTransform(1, 1);


        }



    }
}
