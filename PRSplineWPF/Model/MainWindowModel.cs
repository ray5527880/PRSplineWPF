using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRSplineWPF.Model
{
    public class MainWindowModel
    {
        public MainWindowModel()
        {

        }
        private string _Location = "Location：";
        private string _Device = "Device：";
        private string _StartData = "開始日期：";
        private string _TriggerData = "觸發日期：";
        private string _StartTime = "開始時間：";
        private string _TriggerTime = "觸發時間：";
        #region General Information
        public string Locastion { set { _Location = "Location：" + value; } get { return _Location; } }
        public string Device { set { _Device = "Device：" + value; } get { return _Device; } }
        public string StartData { set { _StartData = "開始日期：" + value; } get { return _StartData; } }
        public string TriggerData { set { _TriggerData = "觸發日期：" + value; } get { return _TriggerData; } }
        public string StartTime { set { _StartTime = "開始時間：" + value; } get { return _StartTime; } }
        public string TriggerTime { set { _TriggerTime = "觸發時間：" + value; } get { return _TriggerTime; } }
        #endregion
    }
}
