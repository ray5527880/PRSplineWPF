using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using System.ComponentModel;

namespace PRSplineWPF.ViewModel
{
    public   class ViewModelBase: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string PropertyName = null)
        {
            if (PropertyName != null)
            {
                PropertyChanged(this,
                    new PropertyChangedEventArgs(PropertyName));
            }
        }

    }
}
