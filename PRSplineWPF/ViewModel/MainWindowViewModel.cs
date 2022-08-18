using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PRSplineWPF.Model;

namespace PRSplineWPF.ViewModel
{
    public class MainWindowViewModel:ViewModelBase
    {
        public MainWindowModel models { get; set; }
        public double MainViewHeight { get; set; }
        public double MainViewWidth { get; set; }
        public MainWindowViewModel()
        {
            models= new MainWindowModel();
        }
        public void WindowsSizeChanged()
        {
            //System.Windows.MessageBox.Show(string.Format("{0},{1}", MainViewHeight, MainViewWidth));
        }
    }
}
