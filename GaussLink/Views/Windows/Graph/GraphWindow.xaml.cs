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
    public partial class GraphWindow : Window
    {
  
        ExcitationEnergy en;
        List<float> nmValues;
        List<float> evValues;
        List<float> cmValues;
        List<float> yValues;
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

        public GraphWindow(ExcitationEnergy gt)
        {
            InitializeComponent();
            this.gt = gt;
            this.Loaded += GraphTabView_Loaded;
            Messenger.Default.Register<ThemeChangedMessage>(this,ThemeChanged);

            plotView.SizeChanged += PlotView_SizeChanged;
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

        private void PlotView_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            screenWidth = (float)plotView.ActualWidth;
            xMargin = 40;
            screenHeight = (float)plotView.ActualHeight;
            yMargin = 40;
            plotHeight = screenHeight - 2 * yMargin;
            plotWidth = screenWidth - 2 * xMargin;
            plotView.Children.Clear();
            if (en != null)
            {
                switch (currentUnit)
                {
                    case Unit.nm: Draw(nmValues,"nm"); break;
                    case Unit.cm: Draw(cmValues,"cm-1"); break;
                    case Unit.eV: Draw(evValues,"eV"); break;
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
                nmValues.Add(en.ExcitedStates[i].WaveLength);
                evValues.Add(en.ExcitedStates[i].ExcitationEnergy);
                yValues.Add(en.ExcitedStates[i].OscillatorStrength);
                if (yValues[i] > yMax)
                {
                    yMax = yValues[i];
                }

            }
            cmValues = Convert(evValues, evToCm);
            yMax = (float)RoundUpToNearest(yMax);

        }

        private void GraphTabView_Loaded(object sender, RoutedEventArgs e)
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

            Label exEn = new Label();
            exEn.Content = "Excitation Energy (" + text + ")";
            exEn.RenderTransform = new TranslateTransform(plotWidth , plotHeight / 2);
            exEn.LayoutTransform = new RotateTransform(90);
            Label osc = new Label();
            osc.Content = "Oscillator Strength";
            osc.RenderTransform = new TranslateTransform((plotWidth + xMargin)/2, 20);
            plotView.Children.Add(exEn);
            plotView.Children.Add(osc);

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
                float x = OldToNewRangeConverter(xIntervalUnit * i, xMax, yMax, plotWidth, xMargin);
                //float y = OldToNewRangeConverter(50*(i+1), yOldMax, yOldMin, plotHeight, 0);
                Point p1 = new Point(x, plotHeight);
                Point p2 = new Point(x, yMargin);
                l.Stroke = new SolidColorBrush(Colors.Gray);
                l.Points.Add(p1);
                l.Points.Add(p2);
                l.StrokeDashArray = new DoubleCollection() { 2 };
                Label b = new Label();
                b.Content = (i * xIntervalUnit).ToString();
                b.RenderTransform = new TranslateTransform(x - 15, plotHeight + yMargin / 2);
                plotView.Children.Add(b);
                if(i>0) plotView.Children.Add(l);
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
                b.Content = (i  * yIntervalUnit).ToString();
                b.RenderTransform = new TranslateTransform(0, y-10);
                plotView.Children.Add(b);
                if(i>0) plotView.Children.Add(l);
            }

            DrawLines(values);

            Polyline plotLine = new Polyline();
            plotLine.Stroke = plotBrush;

   
            for (int i = 0; i < points.Length - 2; i++)
            {
                float x = OldToNewRangeConverter((float)points[i].X, xMax, xMin, plotWidth, xMargin);
                float y = OldToNewRangeConverter((float)points[i].Y, yMax, yMin, plotHeight, yMargin);
                plotLine.Points.Add(new Point(x, plotHeight+yMargin - y));
            }

            plotView.Children.Add(plotLine);
        }
    
        List<float> Convert(List<float> values, double factor)
        {
            List<float> v = new List<float>();
            for (int i = 0; i < values.Count; i++)
            {
                v.Add((float)(values[i]*factor));
            }
            return v;
        }

        void DrawLines(List<float> values)
        {
            int i = 0;
            foreach (float v in values)
            {
                Polyline p = new Polyline();
                float x = OldToNewRangeConverter(v, xMax, xMin, plotWidth,xMargin);
                float y = OldToNewRangeConverter(yValues[i], yMax, yMin, plotHeight, yMargin);
                Point p1 = new Point(x,plotHeight+yMargin-y);
                Point p2 = new Point(x, plotHeight);
                p.Stroke = new SolidColorBrush(Colors.Blue);
                p.Points.Add(p1);
                p.Points.Add(p2);
                plotView.Children.Add(p);
                i++;
            }
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
            yIntervalUnit = yMax /10;
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
                    return x/2;
                else return x;
            }
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
                    Draw(nmValues,"nm");
                    break;
                case "cm^-1":
                    currentUnit = Unit.cm;
                    Draw(cmValues,"cm-1");
                    break;
                case "eV":
                    currentUnit = Unit.eV;
                    Draw(evValues,"eV");
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
                        case Unit.nm: Draw(nmValues,"nm"); break;
                        case Unit.cm: Draw(cmValues,"cm-1"); break;
                        case Unit.eV: Draw(evValues,"eV"); break;
                    }
                }
            }
        }
    }
}

