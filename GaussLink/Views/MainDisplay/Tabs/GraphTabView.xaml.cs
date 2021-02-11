using GaussLink.Models;
using GaussLink.ViewModels.MainDisplay.Tabs;
using System;
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
        double xValueSpace;
        double yValueSpace;
        double xMargin;
        double yMargin;
        double plotHeight;
        double plotWidth;
        double screenWidth;
        double screenHeight;
        double xUpperLimit=0;
        double yUpperLimit = 0;
        int points;
        public GraphTabView()
        {
            InitializeComponent();
            this.Loaded += GraphTabView_Loaded;
            this.SizeChanged += GraphTabView_SizeChanged;
        }

        private void GraphTabView_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            screenWidth = this.ActualWidth;
            xMargin = 40;
            screenHeight = this.ActualHeight;
            yMargin = 40;
            plotHeight = screenHeight - 2 * yMargin;
            plotWidth = screenWidth - 2 * xMargin;
            plotView.Children.Clear();
            if(en!=null)
            {
                Init();
            }
        }

        private void GraphTabView_Loaded(object sender, RoutedEventArgs e)
        {
            GraphTab t = (GraphTab)this.DataContext;
            if(t != null)
            {
                screenWidth = this.ActualWidth;
                xMargin = 50;
                screenHeight = this.ActualHeight;
                yMargin = 100;
                plotHeight = screenHeight - yMargin;
                plotWidth = screenWidth -  xMargin;

                en = t.ExcitationEnergy;
                Init();

            }
        }

        void Init()
        {
            points = en.ExcitedStates.Count;
            foreach (ExcitedState e in en.ExcitedStates)
            {
                if (e.ExcitationEnergy > xUpperLimit)
                {
                    xUpperLimit = e.WaveLength;
                }
            }
            foreach (ExcitedState e in en.ExcitedStates)
            {
                if (e.OscillatorStrength > yUpperLimit)
                {
                    yUpperLimit = e.OscillatorStrength;
                }
            }
             xValueSpace = Math.Ceiling(xUpperLimit / 100) * 100;
             yValueSpace = Math.Round(yUpperLimit * 2, MidpointRounding.AwayFromZero) / 2;

            int xTickCount = (int)xUpperLimit / 10;
            var tickWidth = (plotWidth-xMargin) / xTickCount;
            Polyline xAxis = new Polyline();
            xAxis.Stroke = new SolidColorBrush(Colors.Red);
            xAxis.StrokeThickness = 2;
            xAxis.Points.Add(new Point(xMargin, yMargin-5));
            xAxis.Points.Add(new Point(plotWidth, yMargin-5));
            plotView.Children.Add(xAxis);
            for (int i = 0; i < xTickCount; i++)
            {
                Polyline p = new Polyline();
                p.Stroke = new SolidColorBrush(Colors.Red);
                p.StrokeThickness = 2;
                Point x = new Point(xMargin + i * tickWidth, yMargin-5);
                Point y = new Point(xMargin + i * tickWidth, yMargin - 10);
                if(i%10==0)
                {
                    y.Y -= 5;
                }
                p.Points.Add(x);
                p.Points.Add(y);

                plotView.Children.Add(p);
            }

            Polyline plotLine = new Polyline();
            plotLine.Stroke = new SolidColorBrush(Colors.White);


            foreach (ExcitedState e in en.ExcitedStates)
            {
                Polyline p = new Polyline();
                p.Stroke = new SolidColorBrush(Colors.White);
                p.StrokeThickness = 2;
                var x = ((e.WaveLength * plotWidth) / xValueSpace)+xMargin;
                var y = ((e.OscillatorStrength * plotHeight) / yValueSpace)+yMargin;
                plotLine.Points.Add(new Point(x, y + 5));
                Point k = new Point(x, y);
                Point l = new Point(x,yMargin);
                p.Points.Add(k);
                p.Points.Add(l);
                plotView.Children.Add(p);
            }
            plotView.Children.Add(plotLine);
        }
    


    }
}
