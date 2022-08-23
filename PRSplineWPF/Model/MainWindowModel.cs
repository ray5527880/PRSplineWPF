using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GSF.COMTRADE;
using PRSplineWPF.ViewModel;

namespace PRSplineWPF.Model
{
    public struct RalayPRData
    {
        public Parser Parsers;
        public List<double[]> PrimaryData;
        public List<double[]> SecondaryData;
        public List<double[]> PerUnitData;
        public string[] AnalogNames;
        public string[] DigitalNames;
    }
    public class MainWindowModel : ViewModelBase
    {
        public MainWindowModel()
        {

        }
      
        public List<RalayPRData> datas { set; get; }
        private string _Location = "Location：";
        private string _Device = "Device：";
        private string _StartData = "開始日期：";
        private string _TriggerData = "觸發日期：";
        private string _StartTime = "開始時間：";
        private string _TriggerTime = "觸發時間：";
        #region General Information
        public string Locastion { set { _Location = "Location：" + value; OnPropertyChanged(); } get { return _Location; } }
        public string Device { set { _Device = "Device：" + value; OnPropertyChanged(); } get { return _Device; } }
        public string StartData { set { _StartData = "開始日期：" + value; OnPropertyChanged(); } get { return _StartData; } }
        public string TriggerData { set { _TriggerData = "觸發日期：" + value; OnPropertyChanged(); } get { return _TriggerData; } }
        public string StartTime { set { _StartTime = "開始時間：" + value; OnPropertyChanged(); } get { return _StartTime; } }
        public string TriggerTime { set { _TriggerTime = "觸發時間：" + value; OnPropertyChanged(); } get { return _TriggerTime; } }
        #endregion
    }
}
