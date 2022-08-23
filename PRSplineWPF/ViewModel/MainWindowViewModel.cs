using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Win32;
using PRSplineWPF.Model;
using PRSplineWPF.Scripts;

namespace PRSplineWPF.ViewModel
{
    public class MainWindowViewModel:ViewModelBase
    {
        private int selectindex = 0;
        public MainWindowModel models { get; set; }
        public double MainViewHeight { get; set; }
        public double MainViewWidth { get; set; }

        public ICommand buttondown { get; }
        public ICommand btn_XZoomIn { get; }
        public ICommand btn_XZoomOut { get; }
        public ICommand btn_YZoomIn { get; }
        public ICommand btn_YZoomOut { get; }
        public ICommand btn_ReZoom { get; }
        public ICommand btn_OpenFile { get; }
        public ICommand btn_DowloadFile { get; }
        public ICommand btn_Setup { get; }
        public ICommand btn_VS { get; }
        public ICommand btn_Screenshot { get; }
        public ICommand btn_ReomveBlack { get; }
        public ICommand btn_Extremum { get; }
        public ICommand btnAButtonCheck { get; }

        public MainWindowViewModel()
        {
            
            models = new MainWindowModel();
            models.datas = new List<RalayPRData>();
            btn_OpenFile = new RelayCommand<object>(() => OpenFile());
            btnAButtonCheck = new RelayCommand<object>(() => OnAButtonCheck());
        }
        public void WindowsSizeChanged()
        {
            //System.Windows.MessageBox.Show(string.Format("{0},{1}", MainViewHeight, MainViewWidth));
        }
        public void OnAButtonCheck()
        {

        }
        private void OpenFile()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "DR-files(*.zip;*.cfg)|*.zip;*.cfg";
            if (openFileDialog.ShowDialog() == true)
            {
                var _items = new RalayPRData();
                
                OpenFiles _openFiles = new OpenFiles();
                
                _openFiles.OpenFile(openFileDialog.FileName, openFileDialog.SafeFileName,ref _items);

                models.datas.Add(_items);
                selectindex = models.datas.Count;

                UpdataIntroduction();

                // System.Windows.MessageBox.Show(openFileDialog.SafeFileName);
                //System.Windows.MessageBox.Show(openFileDialog.FileName);
                //_mainWindowMode = new MainWindowModel();
                //_mainWindowMode.OpenFile(openFileDialog.FileName, openFileDialog.SafeFileName);
            }
            //txtEditor.Text = File.ReadAllText(openFileDialog.FileName);
        }
        private void UpdataIntroduction()
        {
            models.Locastion = models.datas[selectindex - 1].Parsers.Schema.StationName;
            models.Device = models.datas[selectindex - 1].Parsers.Schema.DeviceID;
            models.StartData = models.datas[selectindex - 1].Parsers.Schema.StartTime.Value.ToString("yyyy/MM/dd");
            models.TriggerData = models.datas[selectindex - 1].Parsers.Schema.TriggerTime.Value.ToString("yyyy/MM/dd");
            models.StartTime = models.datas[selectindex - 1].Parsers.Schema.StartTime.Value.ToString("HH:mm:ss.fff");
            models.TriggerTime = models.datas[selectindex - 1].Parsers.Schema.TriggerTime.Value.ToString("HH:mm:ss.fff");

        }


    }
}
