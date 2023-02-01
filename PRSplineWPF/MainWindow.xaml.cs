using Microsoft.Win32;
using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using PRSplineWPF.Model;
using PRSplineWPF.Scripts;
using BF_FW;
using ScottPlot;

namespace PRSplineWPF
{
    public enum ADType
    {
        Analoge = 0,
        Digital = 1
    };
    /// <summary>
    /// MainWindow.xaml 的互動邏輯
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindowModel models { get; set; }
        public List<chartdata> ScottPlotChart;
        public int selectindex = 0;
        public RalayPRData LocalRalayPRData;
        public MainWindow()
        {
            InitializeComponent();
            tLocation.Content = "Location：";
            tDevice.Content = "Device：";
            tStartData.Content = "開始日期：";
            tTriggerData.Content = "觸發日期：";
            tStartTime.Content = "開始時間：";
            tTriggerTime.Content = "觸發時間：";
            var _EditXml = new EditXml();
            _EditXml.GetXmlData();
        }

        private void UpdataIntroduction()
        {
            tLocation.Content = "Location：" + models.datas[selectindex - 1].Parsers.Schema.StationName;
            tDevice.Content = "Device：" + models.datas[selectindex - 1].Parsers.Schema.DeviceID;
            tStartData.Content = "開始日期：" + models.datas[selectindex - 1].Parsers.Schema.StartTime.Value.ToString("yyyy/MM/dd");
            tTriggerData.Content = "觸發日期：" + models.datas[selectindex - 1].Parsers.Schema.TriggerTime.Value.ToString("yyyy/MM/dd");
            tStartTime.Content = "開始時間：" + models.datas[selectindex - 1].Parsers.Schema.StartTime.Value.ToString("HH:mm:ss.fff");
            tTriggerTime.Content = "觸發時間：" + models.datas[selectindex - 1].Parsers.Schema.TriggerTime.Value.ToString("HH:mm:ss.fff");

        }

        private void FileList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void onFileOpen(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "DR-files(*.zip;*.cfg)|*.zip;*.cfg";
            if (openFileDialog.ShowDialog() == true)
            {
                var _items = new RalayPRData();

                OpenFiles _openFiles = new OpenFiles();

                _openFiles.OpenFile(openFileDialog.FileName, openFileDialog.SafeFileName, ref _items);

                models = new MainWindowModel();
                models.datas = new List<RalayPRData>();
                models.Filelist = new List<string>();
               

              
                foreach (var _names in _items.AnalogNames)
                {
                    addListItem(_names, ADType.Analoge);
                }
                foreach (var _names in _items.DigitalNames)
                {
                    addListItem(_names, ADType.Digital);
                }
                models.datas.Add(_items);
                selectindex = models.datas.Count;
                models.Filelist.Add(openFileDialog.SafeFileName);
                
                var _times = new List<double>();

             

                wpfPlot1.Plot.XAxis.Label("Time(ms)");
                DwpfPlot.Plot.XAxis.Label("Time(ms)");

                UpPlotView();

                UpdataIntroduction();
                LocalRalayPRData = _items;
            }
        }

        private void addChartData(charttype _type, List<double[]> _datas, string[] _AnalogNames, string[] _DigitalName)
        {
            var _times = new List<double>();
            foreach (var items in _datas)
            {
                _times.Add(items[1]);
            }
            for (int i = 2; i < _datas[0].Length; i++)
            {
                var _chartdatas = new List<double>();
                foreach (var items in _datas)
                {
                    _chartdatas.Add(items[i]);
                }
                //   if (i<)
                if (i < models.datas[selectindex - 1].Parsers.Schema.TotalAnalogChannels + 2)
                {
                    NewChartdata(ADType.Analoge, _times.ToArray(), _chartdatas.ToArray(), _type, _AnalogNames[i - 2]);
                }
                else if (i < models.datas[selectindex - 1].Parsers.Schema.TotalAnalogChannels + 2 + models.datas[selectindex - 1].Parsers.Schema.TotalDigitalChannels)
                {
                    NewChartdata(ADType.Digital, _times.ToArray(), _chartdatas.ToArray(), _type, _DigitalName[i-2- models.datas[selectindex - 1].Parsers.Schema.TotalAnalogChannels]);
                }
                else
                {
                    NewChartdata(ADType.Analoge, _times.ToArray(), _chartdatas.ToArray(), _type, _AnalogNames[i  - 2 - models.datas[selectindex - 1].Parsers.Schema.TotalDigitalChannels]);
                }
            }
        }
        private void NewChartdata(ADType adtype, double[] XValue, double[] YValue, charttype charttypes, string signalName)
        {
            var _ScottPlotData = new chartdata()
            {
                signalName = signalName,
                types = charttypes,
                SignalPlotXY = adtype == ADType.Analoge ? wpfPlot1.Plot.AddSignalXY(XValue, YValue) : DwpfPlot.Plot.AddSignalXY(XValue, YValue)
            };
            _ScottPlotData.SignalPlotXY.Label = signalName;
            ScottPlotChart.Add(_ScottPlotData);
        }
        private void addListItem(string buttonName, ADType types)
        {
            var _button = new Button
            {
                Content = buttonName,
                //Width = 90,
                // Height = 30
            };
            
            _button.Click += ListBoxItem_Selected;
            switch (types)
            {
                case ADType.Analoge:
                    ACGrid.Children.Add(_button);
                    break;
                case ADType.Digital:
                    DCGrid.Children.Add(_button);
                    break;
            }

            //ACGrid.ContextMenu.Add(_listboxitem);
        }
        private void ListBoxItem_Selected(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < ScottPlotChart.Count; i++)
            {
                if (((Button)sender).Content.ToString() == ScottPlotChart[i].signalName)
                {
                    ScottPlotChart[i].SignalPlotXY.IsVisible = !ScottPlotChart[i].SignalPlotXY.IsVisible;

                    UpPlotView();

                }
            }

        }

        private void OpenDownloadWindows(object sender, RoutedEventArgs e)
        {
            var _newWindows = new DownloadWindow();
            _newWindows.Show();
        }
        private void OpenSetupWindows(object sender, RoutedEventArgs e)
        {
            var _newWindows = new SetupWindow();
            _newWindows.Show();
        }
        public void UpPlotView()
        {
            wpfPlot1.Plot.Legend(location: Alignment.UpperRight);
            wpfPlot1.Refresh();
            DwpfPlot.Plot.Legend(location: Alignment.UpperRight);
            DwpfPlot.Refresh();
        }

        private void PSCbx_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ScottPlotChart = new List<chartdata>();
            addChartData(charttype.Primary, LocalRalayPRData.PrimaryData, LocalRalayPRData.AnalogNames, LocalRalayPRData.DigitalNames);
            foreach (var items in ScottPlotChart)
            {
                items.SignalPlotXY.IsVisible = false;
            }
        }
    }
}
