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
    /// Interaction logic for LoginView.xaml
    /// </summary>
    public partial class LoginView : Window
    {
        public LoginView()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void cbhidepassword_Checked(object sender, RoutedEventArgs e)
        {
                tbpassword.PasswordChar = '*';
        }

        private void cbhidepassword_Unchecked(object sender, RoutedEventArgs e)
        {
                tbpassword.PasswordChar = '\0';
        }
    }
}
