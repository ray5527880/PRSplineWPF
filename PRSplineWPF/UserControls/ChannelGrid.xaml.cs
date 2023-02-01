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

namespace PRSplineWPF.UserControls
{
    /// <summary>
    /// ChannelGrid.xaml 的互動邏輯
    /// </summary>
    public partial class ChannelGrid : UserControl
    {
        public string[] listName;
        public ChannelGrid()
        {
            InitializeComponent();
        }
        public void updatabutton()
        {
            listboxname.Items.Clear();
            foreach(var items in listName)
            {
                addButton(items);
            }
        }
        private void addButton(string buttonName)
        {
            var _listboxitem = new ListBoxItem();

            var _button = new Button
            {
                Content = buttonName,
                Width = 90,
                Height = 30
            };
            _listboxitem.Content = _button;
            listboxname.Items.Add(_listboxitem);
        }

        
    }
}
