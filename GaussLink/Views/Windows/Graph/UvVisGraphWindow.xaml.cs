using GalaSoft.MvvmLight.Messaging;
using GaussLink.Data.Messages;
using GaussLink.Models;
using GaussLink.ViewModels.MainDisplay.Tabs;
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
using System.Windows.Threading;

namespace GaussLink.Views.Windows.Graph
{
    /// <summary>
    /// Interaction logic for GraphWindow.xaml
    /// </summary>
    public partial class UvVisGraphWindow : Window
    {

        ExcitationEnergy en;
        List<float> nmValues;
        List<float> evValues;
        List<float> cmValues;
        List<float> yValues;
        List<Tuple<int,int>> pointIndices;
        //Canvas xLabels = new Canvas();
        //Canvas plot=new Canvas();
        //Canvas rest= new Canvas();
        float xMargin, yMargin;
        int nrOfStates;
        Unit currentUnit = Unit.nm;
        float xIntervalUnit = 50;
        float yIntervalUnit = 0.05f;
        float plotHeight, plotWidth;
        float screenWidth, screenHeight;
        float xMax, yMax, xMin, yMin;
        float g = 1;
        SolidColorBrush plotBrush = new SolidColorBrush(Colors.White);
        const double evToCm = 8065.543937;



        int npoints = 500;
        private ExcitationEnergy gt;

        public UvVisGraphWindow(ExcitationEnergy gt)
        {
            InitializeComponent();
            this.gt = gt;
            this.Loaded += UvVisGraphView_Loaded;
            Messenger.Default.Register<ThemeChangedMessage>(this, ThemeChanged);
            Uri iconUri = new Uri("pack://application:,,,/UI/Images/appIconWhite.png", UriKind.RelativeOrAbsolute);
            this.Icon = BitmapFrame.Create(iconUri);
            plotView.SizeChanged += plotView_SizeChanged;
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

            if (en != null)
            {
                switch (currentUnit)
                {
                    case Unit.nm: Draw(nmValues, "nm"); break;
                    case Unit.cm: Draw(cmValues, "cm-1"); break;
                    case Unit.eV: Draw(evValues, "eV"); break;
                }
            }
        }

        private void plotView_SizeChanged(object sender, SizeChangedEventArgs e)
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
            if (en != null)
            {
                switch (currentUnit)
                {
                    case Unit.nm: Draw(nmValues, "nm"); break;
                    case Unit.cm: Draw(cmValues, "cm-1"); break;
                    case Unit.eV: Draw(evValues, "eV"); break;
                }
            }
        }

        private void ExtractValues()
        {
            yMax = 0;
            xMax = 0;
            nrOfStates = en.ExcitedStates.Count;
            nmValues = new List<float>();
            evValues = new List<float>();
            cmValues = new List<float>();
            yValues = new List<float>();
            for (int i = 0; i < nrOfStates; i++)
            {
                nmValues.Add(en.ExcitedStates[i].NmEnergy);
                evValues.Add(en.ExcitedStates[i].EvEnergy);
                yValues.Add(en.ExcitedStates[i].OscillatorStrength);
                if (yValues[i] > yMax)
                {
                    yMax = yValues[i];
                }

            }
            cmValues = Convert(evValues, evToCm);
            yMax = (float)RoundUpToNearest(yMax);

        }

        private void UvVisGraphView_Loaded(object sender, RoutedEventArgs e)
        {
            if (gt != null)
            {
                en = gt;
                screenWidth = (float)plotView.ActualWidth;
                xMargin = 50;
                screenHeight = (float)plotView.ActualHeight;
                yMargin = 50;
                plotHeight = screenHeight - yMargin;
                plotWidth = screenWidth - xMargin;

                ExtractValues();

                Draw(nmValues, "nm");

            }
        }

        void Draw(List<float> values, string text)
        {
            plotView.Children.Clear();
            plot.Children.Clear();
            xLabels.Children.Clear();
            lines.Children.Clear();
            cpoints.Children.Clear();

            Label osc = new Label();
            osc.Content = "Oscillator Strength";
            osc.RenderTransform = new TranslateTransform(plotWidth, plotHeight / 2);
            osc.LayoutTransform = new RotateTransform(90);
            Label exE = new Label();
            exE.Content = "Excitation Energy (" + text + ")";
            exE.RenderTransform = new TranslateTransform((plotWidth + xMargin) / 2, 20);
            plotView.Children.Add(osc);
            plotView.Children.Add(exE);

            Point[] points = CalculateGraphPoints(values);
            Polyline xAxis = new Polyline();
            xAxis.Stroke = new SolidColorBrush(Colors.Red);
            xAxis.Points.Add(new Point(xMargin, plotHeight));
            xAxis.Points.Add(new Point(plotWidth, plotHeight));
            plotView.Children.Add(xAxis);

            Polyline yAxis = new Polyline();
            yAxis.Stroke = new SolidColorBrush(Colors.Green);
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
                Label b = new Label();
                b.Content =  (i * xIntervalUnit).ToString();
                b.RenderTransform = new TranslateTransform(x - 15, plotHeight + yMargin / 2);
                xLabels.Children.Add(b);
                plot.Children.Add(l);

            }
            for (int i = 0; i <= (yMax / yIntervalUnit); i++)
            {
                Polyline l = new Polyline();
                float y = OldToNewRangeConverter(yIntervalUnit * i, yMax, yMin, yMargin, plotHeight);
                Point p1 = new Point(xMargin, y);
                Point p2 = new Point(plotWidth, y);
                l.Stroke = new SolidColorBrush(Colors.Gray);
                l.Points.Add(p1);
                l.Points.Add(p2);
                l.StrokeDashArray = new DoubleCollection() { 2 };
                Label b = new Label();
                b.Content = (i * yIntervalUnit).ToString();
                b.RenderTransform = new TranslateTransform(0, y - 10);
                plotView.Children.Add(b);
                if (i > 0) plotView.Children.Add(l);
            }


            Polyline plotLine = new Polyline();
            plotLine.Stroke = plotBrush;


            for (int i = 0; i < points.Length - 2; i++)
            {
                float x = OldToNewRangeConverter((float)points[i].X, xMax, xMin, plotWidth, xMargin);
                float y = OldToNewRangeConverter((float)points[i].Y, yMax, yMin, plotHeight, yMargin);
                plotLine.Points.Add(new Point(x, plotHeight + yMargin - y));
            }
            DrawLines(values);

            plot.Children.Add(plotLine);
        
        }

        List<float> Convert(List<float> values, double factor)
        {
            List<float> v = new List<float>();
            for (int i = 0; i < values.Count; i++)
            {
                v.Add((float)(values[i] * factor));
            }
            return v;
        }

        void DrawLines(List<float> values)
        {
            pointIndices = new List<Tuple<int,int>>();
            int i = 0;
            foreach (float v in values)
            {
                Polyline p = new Polyline();
                Rectangle r = new Rectangle();
                r.Fill = new SolidColorBrush(Colors.Green);
                r.Width = 10;
                r.Height = 10;
                r.MouseEnter += R_MouseEnter;
                r.MouseLeave += R_MouseLeave;
                float x = OldToNewRangeConverter(v, xMax, xMin, plotWidth, xMargin);
                float y = OldToNewRangeConverter(yValues[i], yMax, yMin, plotHeight, yMargin);
                r.RenderTransform = new TranslateTransform(x - 5, plotHeight + yMargin - y - 5);
                Point p1 = new Point(x, plotHeight + yMargin - y);
                Point p2 = new Point(x, plotHeight);
                p.Stroke = new SolidColorBrush(Colors.Blue);
                p.Points.Add(p1);
                p.Points.Add(p2);

                lines.Children.Add(p);
                cpoints.Children.Add(r);
                pointIndices.Add(new Tuple<int, int>(cpoints.Children.IndexOf(r), i));
                i++;
            }
        }

        private void R_MouseLeave(object sender, MouseEventArgs e)
        {
            pointVal.Text= "Hover over points to see their values";
        }

        private void R_MouseEnter(object sender, MouseEventArgs e)
        {
            Rectangle r = sender as Rectangle;
            int i = cpoints.Children.IndexOf(r);
            int p = pointIndices.Single(x => x.Item1 == i).Item2;
            float value =0;
            switch (currentUnit)
            {
                case Unit.cm:
                    value =  cmValues[p];
                    break;
                case Unit.nm:
                    value = nmValues[p];
                    break;
                case Unit.eV:
                    value = evValues[p];
                    break;
            }
            pointVal.Text = "(" + value.ToString(CultureInfo.InvariantCulture) + " , " + yValues[p].ToString(CultureInfo.InvariantCulture) + ")";
        }

        float L(float E, float x, float amp, float g)
        {
            //x este eps[i];
            return amp * g * g / ((E - x) * (E - x) + g * g);
        }


        Point[] CalculateGraphPoints(List<float> xValues)
        {
            Point[] points = new Point[npoints];
            float y = 0;
            float min = xValues.Any() ? xValues.Min() : 0;
            float max = xValues.Any() ? xValues.Max() : 0;
            float[] ints = new float[npoints];

            float Emin = min - min / 2;
            Emin = (float)RoundDownToNearest(min);
            xMin = Emin;
            yMin = 0;

            float Emax = (float)RoundUpToNearest(max);

            xMax = Emax;
            yMax = yValues.Any() ? yValues.Max() : 0;
            yMin = yValues.Any() ? yValues.Min() : 0;
            yMax = (float)DecimalRoundUp(yMax);
            xIntervalUnit = xMax / 10;
            yIntervalUnit = yMax / 10;
            var dE = (Emax - Emin) / (npoints - 1);
            for (int i = 0; i < nrOfStates; i++)
            {
                var amp = yValues[i];
                var x = xValues[i];
                var E = Emin;
                int j = 0;
                do
                {
                    y = L(E, x, amp, g);
                    ints[j] += y;
                    points[j] = new Point(E, ints[j]);



                    j++;

                    E = E + dE;
                } while (E <= Emax);
            }
            return points;

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
                else return x;
            }
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
        private void Inverse_Checked(object sender, RoutedEventArgs e)
        {

            foreach (Label item in xLabels.Children)
            {
                item.LayoutTransform = new ScaleTransform(-1, 1, 0.5, 0.5);
            }
            plot.LayoutTransform = new ScaleTransform(-1, 1);
            xLabels.LayoutTransform = new ScaleTransform(-1, 1);
            cpoints.LayoutTransform = new ScaleTransform(-1, 1);
            lines.LayoutTransform = new ScaleTransform(-1, 1);
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

        double RoundDownToNearest(double passednumber)
        {

            if (passednumber == 0)
            {
                return passednumber;
            }
            else
            {
                return Math.Pow(10, Math.Floor(Math.Log10(passednumber)));
            }
        }

        private bool handle = true;
        private void ComboBox_DropDownClosed(object sender, EventArgs e)
        {
            ComboBox cmb = sender as ComboBox;
            if (handle) Handle(cmb);
            handle = true;
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cmb = sender as ComboBox;
            handle = !cmb.IsDropDownOpen;
            Handle(cmb);
        }

        private void Handle(ComboBox cmbSelect)
        {
            var item = cmbSelect.SelectedItem.ToString().Split(new string[] { ": " }, StringSplitOptions.None).Last();
            switch (item)
            {
                case "nm":
                    currentUnit = Unit.nm;
                    Draw(nmValues, "nm");
                    break;
                case "cm^-1":
                    currentUnit = Unit.cm;
                    Draw(cmValues, "cm-1");
                    break;
                case "eV":
                    currentUnit = Unit.eV;
                    Draw(evValues, "eV");
                    break;
            }

        }



        private void SmoothFactorTxt_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox t = sender as TextBox;
            if (string.IsNullOrEmpty(t.Text) || t.Text.EndsWith("."))
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
                if (en != null)
                {
                    switch (currentUnit)
                    {
                        case Unit.nm: Draw(nmValues, "nm"); break;
                        case Unit.cm: Draw(cmValues, "cm-1"); break;
                        case Unit.eV: Draw(evValues, "eV"); break;
                    }
                }
            }
        }
    }
}
