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
using System.Windows.Shapes;
using BF_FW;

namespace PRSplineWPF
{
    /// <summary>
    /// SetupWindow.xaml 的互動邏輯
    /// </summary>
    public partial class SetupWindow : Window
    {
        public string ConnectionAlarms;
        public int m_nCount = 0;
        public int m_nIndex = 0;
        public int selectNo = -1;
        public SetupWindow()
        {
            InitializeComponent();
            ViewDG.IsReadOnly = true;
            ViewDG.Columns.Clear();
            ConnectionAlarms = "server=" + EditXml.DBPath + "; database=" + EditXml.DBName + ";uid=" + EditXml.DBUser + ";pwd=" + EditXml.DBPwd;
            UpdataView();
            //ViewDG.
        }
        private void OpenSetupWindows(object sender, RoutedEventArgs e)
        {
            var _newWindows = new SetupWindow();
            _newWindows.Show();
        }
        private void UpdataView()
        {
            ViewDG.Columns.Clear();
            int count = 0;
            m_nCount = EditXml.mFTPData.Count;
            foreach (var item in EditXml.mFTPData)
            {
                string[] arr = new string[]
                {
                    item.strName,item.strIP,item.strUser,item.strPwd,item.BaseValue.ToString(),count.ToString(),item.strPathName
                };
              //  ViewDG.Columns.Add(arr);
                //DGVSetup.Rows.Add(arr);
                count++;
            }
           // DGVSetup.Rows[0].Cells[0].Selected = false;
            //DGVSetup.Rows[m_nCount].Selected = true;
            //DGVSetup.Rows[m_nCount].Cells[0].Value = "新增...";
            m_nIndex = m_nCount;
           // DGVSetup.Refresh();
        }
    }
}
