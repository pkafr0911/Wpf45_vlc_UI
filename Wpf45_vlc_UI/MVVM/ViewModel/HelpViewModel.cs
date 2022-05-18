using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wpf45_vlc_UI.MVVM.View;

namespace Wpf45_vlc_UI.MVVM.ViewModel
{
    class HelpViewModel
    {
        public HelpView helpView { get; set; }
        public HelpViewModel()
        {
            helpView = new HelpView();
            helpView.DataContext = this;
        }
    }
}
