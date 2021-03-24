using GaussLink.Models;
using GaussLink.ViewModels.MainDisplay.Tabs;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace GaussLink.Views.MainDisplay.Tabs
{
    /// <summary>
    /// Interaction logic for GraphTab.xaml
    /// </summary>
    public partial class GraphTabView : UserControl
    {
        ExcitationEnergy en;
       
        float xMargin, yMargin;
        float plotHeight, plotWidth;
        float screenWidth, screenHeight;
        float xOldMax,xNewMax,xOldMin,xNewMin,yOldMax,yNewMax,yOldMin,yNewMin;
        float g = 1;

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
                {                }
                if(value == null)
                {
                    return;
                }
                g = (float)value;
                //t.Text = g.ToString();
                if(en!=null)
                {
                    Draw();
                }
            }
        }

        int points;
        public GraphTabView()
        {
            InitializeComponent();
            this.Loaded += GraphTabView_Loaded;
            plotView.SizeChanged += PlotView_SizeChanged;
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


                Draw();
            }
        }

        private void GraphTabView_Loaded(object sender, RoutedEventArgs e)
        {
            GraphTab t = (GraphTab)this.DataContext;
            if (t != null)
            {
                en = t.ExcitationEnergy;
                screenWidth = (float)plotView.ActualWidth;
                xMargin = 50;
                screenHeight = (float)plotView.ActualHeight;
                yMargin = 50;
                plotHeight = screenHeight - yMargin;
                plotWidth = screenWidth - xMargin;
                yNewMax = 0;
                yNewMin = 0;

         

                foreach (ExcitedState state in en.ExcitedStates)
                {
                    if(state.OscillatorStrength > yNewMax)
                    {
                        yNewMax = state.OscillatorStrength;
                    }
                }
                yNewMax += yNewMax / 2;
                Draw();

            }
        }

        void Draw()
        {
            plotView.Children.Clear();

            Polyline xAxis = new Polyline();
            xAxis.Stroke = new SolidColorBrush(Colors.Red);
            xAxis.Points.Add(new Point(xMargin, plotHeight));
            xAxis.Points.Add(new Point(plotWidth, plotHeight));
            plotView.Children.Add(xAxis);


            Polyline plotLine = new Polyline();
            plotLine.Stroke = new SolidColorBrush(Colors.White);
            Point[] points = CalculateGraphPoints(en);
            float xValMax = (float)points[points.Length - 2].X;
            float xValMin = (float)points[0].X;
            float yValueMax = 0;
           
            foreach(ExcitedState s in en.ExcitedStates)
            {
                if(s.OscillatorStrength >yValueMax)
                {
                    yValueMax = s.OscillatorStrength;
                }
            }
            for (int i = 0; i < points.Length-2; i++)
            {
                float x = OldToNewRangeConverter((float)points[i].X, xOldMax, xOldMin, plotWidth, xMargin);
                float y = OldToNewRangeConverter((float)points[i].Y, yOldMax, yOldMin, plotHeight, 0);
                plotLine.Points.Add(new Point(x,plotHeight-y));
            }

            Label min = new Label();
            Label max = new Label();
            min.Content = xOldMin.ToString();
            max.Content = xOldMax.ToString();

            min.RenderTransform = new TranslateTransform(xMargin, screenHeight  - yMargin);
            max.RenderTransform = new TranslateTransform(xMargin + plotWidth, screenHeight - yMargin);
            plotView.Children.Add(min);
            plotView.Children.Add(max);
            plotView.Children.Add(plotLine);
        }

        float CalculateAverage(List<float> averages)
        {
            float y = 0;
            for (int i = 0; i < averages.Count; i++)
            {
                y += averages[i];
            }
            return y / averages.Count;
        }

        float L(float E, float x, float amp, float g)
        {
            //x este eps[i];
            return amp * g * g / ((E - x) * (E - x) + g * g);
        }

        Point[] CalculateGraphPoints(ExcitationEnergy e)
        {
            int npoints = 500;
            Point[] points = new Point[npoints];
            float y = 0;
            float[] ints = new float[npoints];
            float Emin = e.ExcitedStates[e.ExcitedStates.Count-1].WaveLength - e.ExcitedStates[e.ExcitedStates.Count-1].WaveLength / 2;
            if(Emin<100)
            {
            Emin = (float)Math.Floor(Emin / 10) * 10;
            }else
            {
                Emin = (float)Math.Floor(Emin / 50) * 50;
            }
            xOldMin = Emin;
            float Emax = e.ExcitedStates[0].WaveLength + e.ExcitedStates[e.ExcitedStates.Count-1].WaveLength / 2;
            double range =  Math.Ceiling(Emax / 50) * 50;
            Emax = (float)range;
            xOldMax = Emax;
            yOldMax = 0;
            yOldMin = 0;
            var dE = (Emax - Emin) / (npoints - 1);
            for (int i = 0; i < e.ExcitedStates.Count; i++)
            {
                var amp = e.ExcitedStates[i].OscillatorStrength;
                var x = e.ExcitedStates[i].WaveLength;
                var E = Emin;
                int j = 0;
                do
                {
                    y = L(E, x,amp,g);
                    ints[j] += y;
                    points[j] = new Point(E, ints[j]);

                    if (yOldMax < ints[j])
                    {
                        yOldMax = ints[j];
                    }
                    else if (yOldMin > ints[j])
                    {
                        yOldMin = ints[j];
                    }

                    j++;

                    E = E + dE;
                } while (E <= Emax);
            }
        

            return points;
        }

        Point ScreenToGraphCoordinates(Point p, float plotWidth, float plotHeight, float xMargin,float yMargin, float xValMax, float xValMin,float yValueMax)
        {

            var x = (p.X * plotWidth)/(xValMax - xValMin) ;
            var y = (p.Y * plotHeight) / yValueMax;
            return new Point(x+xMargin, plotHeight-y+yMargin);
        }

        float OldToNewRangeConverter(float Value, float OldMax, float OldMin, float NewMax, float NewMin)
        {
            float OldRange = OldMax - OldMin;
            float NewRange = NewMax - NewMin;
             return (((Value - OldMin) * NewRange) / OldRange) + NewMin;
        }

    }
}
