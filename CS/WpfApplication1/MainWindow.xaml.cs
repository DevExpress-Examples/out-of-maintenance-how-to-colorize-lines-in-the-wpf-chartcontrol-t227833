using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using DevExpress.Xpf.Charts;

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            UpdateDataSource();
            InitializeChartControl();
        }


        DataTable CreateData(int rows)
        {
            Random r = new Random();

            DataTable dt = new DataTable();
            dt.Columns.Add("Ser", typeof(int));
            dt.Columns.Add("Arg", typeof(int));
            dt.Columns.Add("Value", typeof(int));
            for (int i = 0; i < rows; i++)
            {
                dt.Rows.Add(0, i, r.Next(100));
                dt.Rows.Add(1, i, r.Next(100));
            }
            return dt;
        }


        private void InitializeChartControl()
        {
            chartControl1.Diagram.Series.Clear();
            chartControl1.Diagram.SeriesDataMember = "Ser";
            chartControl1.Diagram.SeriesTemplate.ValueDataMember = "Value";
            chartControl1.Diagram.SeriesTemplate.ArgumentDataMember = "Arg";
        }

        private void UpdateDataSource()
        {
            chartControl1.DataSource = CreateData(10);
        }

        private void Canvas_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            DrawOverChart();
        }
        private void DrawOverChart()
        {
            Canvas.Children.Clear();
            XYDiagram2D diagram = ((XYDiagram2D)chartControl1.Diagram);

            foreach (Series s in chartControl1.Diagram.Series)
            {
                for (int i = 0; i < s.Points.Count; i++)
                {
                    if (i < s.Points.Count - 1)
                    {
                        Point screenPoint1 = diagram.DiagramToPoint(s.Points[i].NumericalArgument, s.Points[i].Value).Point;
                        Point screenPoint2 = diagram.DiagramToPoint(s.Points[i + 1].NumericalArgument, s.Points[i + 1].Value).Point;

                        if (IsValidPoints(screenPoint1, screenPoint2))
                            Canvas.Children.Add(CreateLine(screenPoint1,
                                                        screenPoint2, GetBrush(s.Points[i], s)));
                    }
                }
            }
        }

        private SolidColorBrush GetBrush(SeriesPoint point1, Series series)
        {
            switch (series.DisplayName)
            {
                case "0":
                    if (point1.NumericalArgument < 5)
                        return Brushes.Red;
                    else
                        return Brushes.Pink;
                case "1":
                    if (point1.NumericalArgument < 5)
                        return Brushes.Blue;
                    else
                        return Brushes.Aqua;
            }
            return Brushes.Black;
        }

        private bool IsValidPoints(Point screenPoint1, Point screenPoint2)
        {
            return !double.IsInfinity(screenPoint1.X) && !double.IsInfinity(screenPoint1.Y) && !double.IsInfinity(screenPoint2.X) && !double.IsInfinity(screenPoint2.Y);
        }
        
        private Line CreateLine(Point screenPoint1, Point screenPoint2, SolidColorBrush brush)
        {
            var line = new Line()
            {
                X1 = screenPoint1.X,
                X2 = screenPoint2.X,
                Y1 = screenPoint1.Y,
                Y2 = screenPoint2.Y,
                Stroke = brush,
                StrokeThickness = 3
            };
            return line;
        }
    }
}
