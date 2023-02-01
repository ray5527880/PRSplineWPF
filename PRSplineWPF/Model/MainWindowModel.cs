using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GSF.COMTRADE;
using PRSplineWPF.ViewModel;

namespace PRSplineWPF.Model
{
    public enum charttype
    {
        Primary = 1, Secondary = 2, PerUnit = 3
    }
    public struct chartdata
    {
        public ScottPlot.Plottable.SignalPlotXY SignalPlotXY;
        public string signalName;
        public bool IsView;
        public charttype types;

    }
    public struct RalayPRData
    {
        public Parser Parsers;
        public List<double> timeData;
        public List<double[]> PrimaryData;
        public List<double[]> SecondaryData;
        public List<double[]> PerUnitData;
        public string[] AnalogNames;
        public string[] DigitalNames;
        public bool[] AnalogIsView;
        public bool[] DigitalIsView;
    }
    public class MainWindowModel : ViewModelBase
    {
        public MainWindowModel()
        {

        }
      
        public List<RalayPRData> datas { set; get; }
        public List<string> Filelist { set; get; }
     
    }
}
