using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms.DataVisualization.Charting;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Day2_2
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            // CmbChartType.ItemsSource = Enum.GetNames(typeof(SeriesChartType)).ToList();
            // CmbChartType.SelectedIndex = 0;

            //CmbPalette.ItemsSource = Enum.GetNames(typeof(ChartColorPalette)).ToList();
            //CmbPalette.SelectedIndex = 0;
            FillBuildingChart();

        }

        private void FillBuildingChart()
        {
            var data = GetData();

            var buildingTypes = data.GroupBy(x => x.TerritoryType);

            foreach (var buildingType in buildingTypes)
            {
                var series = MyChart.Series.Add(buildingType.Key.ToString());

                var seriesData = buildingType.GroupBy(x => x.ReportingYear).Select(x=> new {Year = x.Key, FinalResult = x.Max(z => z.ActualIndicators)});


                var  xValue = seriesData.Select(x => x.Year).ToArray();
                var yValue = seriesData.Select(x => x.FinalResult).ToArray();
                series.Points.DataBindXY(xValue, yValue);
            }

            var seriesPlannedAll = MyChart.Series.Add("Planning");
            seriesPlannedAll.ChartType = SeriesChartType.Spline;
            seriesPlannedAll.Color = System.Drawing.Color.Green;

            var seriesDataAll = buildingTypes.FirstOrDefault(x => x.Key == TerritoryType.All);

            var seriesFinalDataAll = seriesDataAll.GroupBy(x => x.ReportingYear).Select(x => new {Year =x.Key, FinalResult = x.Max(z=> z.PlannedIndicators)});


            var xValuePlanned = seriesFinalDataAll.Select(x => x.Year).ToArray();
            var yValuePlanned = seriesFinalDataAll.Select(x => x.FinalResult).ToArray();

            seriesPlannedAll.Points.DataBindXY(xValuePlanned, yValuePlanned);
        }

        private void FillTestChart(SeriesChartType type, ChartColorPalette palette)
        {



            var firstSeries = MyChart.Series["FirstSeries"];
            firstSeries.ChartType = type;
            firstSeries.Palette = palette;
            //firstSeries.Color = System.Drawing.Color.Red;
            //var firstSeries2 = MyChart.Series.First();

            var axisXData = new DateTime[]{
                new DateTime(2022, 3, 8),
                new DateTime(2022, 3, 9),
                new DateTime(2022, 3, 10),
                new DateTime(2022, 3, 11),
                new DateTime(2022, 3, 12)
            };

            var axisYDataCold = new int[] { -25, -20, -16, -19, -13 };
            var axisYDataWarm = new int[] { -12, -8, -10, -11, -5 };

            firstSeries.Points.DataBindXY(axisXData, axisYDataCold);


            var secondSeries = MyChart.Series.Add("SecondSeries");
            secondSeries.ChartType = SeriesChartType.Spline;
            secondSeries.Color = System.Drawing.Color.Red;
            secondSeries.ChartArea = "SecondArea";
            secondSeries.Legend = "SecondLegend";
            secondSeries.Points.DataBindXY(axisXData, axisYDataWarm);


        }

        private List<BuldingData> GetData()
        {
            WebClient webClient = new WebClient { Encoding= Encoding.UTF8};
            string url = "https://hakta.pro/data.json";
            var response = webClient.DownloadString(url);
            var data = JsonConvert.DeserializeObject<List<BuldingData>>(response);
            return data;

        }

        private void CmbChartType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            /*
            if (CmbChartType.SelectedIndex >= 0 && CmbPalette.SelectedIndex >= 0)
            {
                var selectedType = (SeriesChartType)Enum.Parse(typeof(SeriesChartType), CmbChartType.SelectedValue.ToString());
                var selectedPallete = (ChartColorPalette)Enum.Parse(typeof(ChartColorPalette), CmbPalette.SelectedValue.ToString()); 
                FillTestChart(selectedType, selectedPallete);
            }
            */



           
        }
    }
}
