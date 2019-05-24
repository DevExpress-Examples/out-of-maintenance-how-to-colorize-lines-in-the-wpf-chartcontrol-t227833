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
    }
}
