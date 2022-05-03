using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Wpf45_vlc_UI.MVVM.View
{
    /// <summary>
    /// Interaction logic for CustomMessageBoxView.xaml
    /// </summary>
    public partial class CustomMessageBoxView : Window
    {
        public CustomMessageBoxView()
        {
            InitializeComponent();
        }
        static CustomMessageBoxView cMessageBox;
        static DialogResult result = System.Windows.Forms.DialogResult.No;
        public enum cMessageBoxButton
        {
            Ok,
            No,
            Yes,
            Cancel,
            Confirm
        }
        public enum cMessageBoxTitle
        {
            Error,
            Infor,
            Warning,
            Confirm,
            Success
        }
        public static DialogResult Show(string massage, cMessageBoxTitle title, cMessageBoxButton butOk, cMessageBoxButton butCancel)
        {
            cMessageBox = new CustomMessageBoxView();
            cMessageBox.messageboxcontent.Text = massage;
            cMessageBox.butok.Content = cMessageBox.GetMessageButton(butOk);
            cMessageBox.butcancel.Content = cMessageBox.GetMessageButton(butCancel);
            cMessageBox.messagetitle.Content = cMessageBox.GetTitle(title);

            
            switch (title)
            {
                case cMessageBoxTitle.Error:
                    cMessageBox.iconmessagebox.Source = new BitmapImage(new Uri(@"/Image/error-icon.png", UriKind.Relative));
                    cMessageBox.butcancel.Visibility = Visibility.Collapsed;
                    cMessageBox.butok.SetValue(Grid.ColumnSpanProperty, 2);
                    break;
                    break;
                case cMessageBoxTitle.Infor:
                    cMessageBox.iconmessagebox.Source = new BitmapImage(new Uri(@"/Image/infor-icon.png", UriKind.Relative));
                    cMessageBox.butcancel.Visibility = Visibility.Collapsed;
                    cMessageBox.butok.SetValue(Grid.ColumnSpanProperty, 2);
                    break;
                case cMessageBoxTitle.Confirm:
                    cMessageBox.iconmessagebox.Source = new BitmapImage(new Uri(@"/Image/question-mark-icon.png", UriKind.Relative));
                    break;
                case cMessageBoxTitle.Warning:
                    cMessageBox.iconmessagebox.Source = new BitmapImage(new Uri(@"/Image/warning-icon.png", UriKind.Relative));
                    cMessageBox.butcancel.Visibility = Visibility.Collapsed;
                    cMessageBox.butok.SetValue(Grid.ColumnSpanProperty, 2);
                    break;
                case cMessageBoxTitle.Success:
                    cMessageBox.iconmessagebox.Source = new BitmapImage(new Uri(@"/Image/success-icon.png", UriKind.Relative));
                    cMessageBox.butcancel.Visibility = Visibility.Collapsed;
                    cMessageBox.butok.SetValue(Grid.ColumnSpanProperty, 2);
                    break;
            }
            cMessageBox.ShowDialog();
            return result;
        }
        public string GetTitle(cMessageBoxTitle value)
        {
            return Enum.GetName(typeof(cMessageBoxTitle), value);
        }

        //for button
        public string GetMessageButton(cMessageBoxButton value)
        {
            return Enum.GetName(typeof(cMessageBoxButton), value);
        }

        private void butok_Click(object sender, RoutedEventArgs e)
        {
            result = System.Windows.Forms.DialogResult.Yes;
            cMessageBox.Close();
        }

        private void butcancel_Click(object sender, RoutedEventArgs e)
        {
            result = System.Windows.Forms.DialogResult.No;
            cMessageBox.Close();
        }
    }
}
