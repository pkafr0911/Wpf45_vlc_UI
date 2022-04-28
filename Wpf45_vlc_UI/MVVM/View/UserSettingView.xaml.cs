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

namespace Wpf45_vlc_UI.MVVM.View
{
    /// <summary>
    /// Interaction logic for UserSettingView.xaml
    /// </summary>
    public partial class UserSettingView : Window
    {
        public UserSettingView()
        {
            InitializeComponent();
        }
        protected override void OnDeactivated(EventArgs e)
        {
            base.OnDeactivated(e);
            Close();
        }
    }
}
