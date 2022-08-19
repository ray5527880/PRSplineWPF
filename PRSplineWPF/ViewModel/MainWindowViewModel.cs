using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Win32;
using PRSplineWPF.Model;

namespace PRSplineWPF.ViewModel
{
    public class MainWindowViewModel:ViewModelBase
    {
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

        public MainWindowViewModel()
        {
            models= new MainWindowModel();
        }
        public void WindowsSizeChanged()
        {
            //System.Windows.MessageBox.Show(string.Format("{0},{1}", MainViewHeight, MainViewWidth));
        }
        private void OpenFile()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "DR-files(*.zip;*.cfg)|*.zip;*.cfg";
            if (openFileDialog.ShowDialog() == true)
            {
                // System.Windows.MessageBox.Show(openFileDialog.SafeFileName);
                //System.Windows.MessageBox.Show(openFileDialog.FileName);
                //_mainWindowMode = new MainWindowModel();
                //_mainWindowMode.OpenFile(openFileDialog.FileName, openFileDialog.SafeFileName);
            }
            //txtEditor.Text = File.ReadAllText(openFileDialog.FileName);
        }
    }
}
