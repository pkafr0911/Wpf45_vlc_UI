using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Xml.Serialization;
using Wpf45_vlc_UI.Constructer;
using Wpf45_vlc_UI.Core;
using Wpf45_vlc_UI.DAO;
using Wpf45_vlc_UI.MVVM.Model;
using Wpf45_vlc_UI.MVVM.View;

namespace Wpf45_vlc_UI.MVVM.ViewModel
{
    class LoginViewModel
    {
        #region khai báo toàn cục
        public View.LoginView loginview { get; set; }
        public MainWindow mainWindow { get; set; }

        #endregion

        public LoginViewModel()
        {
            loginview = new View.LoginView();
            loginview.DataContext = this;

            #region các command

            //command đóng ứng dụng
            ButCloseCommand = new MainRelayCommand(o =>
            {
                Application.Current.Shutdown();
            });
            //command thu nhỏ ứng dụng
            ButMinizeCommand = new MainRelayCommand(o =>
            {
                loginview.WindowState = WindowState.Minimized;
            });
            //command đăng nhập
            LoginCommand = new RelayCommand<UIElementCollection>((p) => true, p =>
            {
                string username = "";
                string password = "";
                //lấy username từ textbox
                foreach (var item in p)
                {
                    TextBox a = item as TextBox;
                    if (a == null)
                    {
                        continue;
                    }
                    switch (a.Name)
                    {

                        case "tbuser":
                            username = a.Text;
                            break;
                    }
                }
                //lấy password từ textbox
                foreach (var item in p)
                {
                    PasswordBox b = item as PasswordBox;
                    if (b == null)
                    {
                        continue;
                    }
                    switch (b.Name)
                    {
                        case "tbpassword":
                            password = b.Password;
                            break;
                    }
                }
                //kiểm tra chuỗn có null hay rỗng không
                if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                {
                    return;
                }
                #region test login
                //if (username == "admin" && password == "admin")
                //{
                //    MVVM.ViewModel.MainViewModel mvm = new MVVM.ViewModel.MainViewModel();

                //    loginview.Close();
                //}
                //else
                //{
                //    MessageBox.Show("nhap sai!!", "SAIII");
                //}
                #endregion

                //thực hiện đăng nhập
                if (Login(username, password))
                {
                    #region Ghi nhớ cho lần đăng nhập sau
                    UserModel userModel = new UserModel();
                    Security security = new Security();
                    userModel.UserName = security.Encrypt(username);//mã hóa tên đăng nhập đc lưu
                    userModel.PassWord = security.Encrypt(password);//mã hóa password đc lưu

                    XmlSerializer x = new XmlSerializer(typeof(UserModel));
                    TextWriter writer = new StreamWriter("Account_Login.xml");
                    x.Serialize(writer, userModel);//lưu thông tin theo usermodel 
                    writer.Close();
                    #endregion

                    MVVM.ViewModel.MainViewModel mvm = new MVVM.ViewModel.MainViewModel();
                    mvm.userName = username;
                    mvm.ListOfCams = CameraDAO.Instance.GetCameraInfor(username);

                    loginview.Close();//đóng cửa sổ login
                }
                else
                {
                    CustomMessageBoxView.Show("Wrong username or password", CustomMessageBoxView.cMessageBoxTitle.Error, CustomMessageBoxView.cMessageBoxButton.Ok, CustomMessageBoxView.cMessageBoxButton.Cancel);

                }

            });

            SignInCommand = new MainRelayCommand(o =>
            {
                SignInViewModel sivm = new SignInViewModel();
            });

            #endregion

            loginview.Show();
        }
        #region Command xử lý
        public ICommand LoginCommand { get; set; }
        public MainRelayCommand ButCloseCommand { get; set; }
        public MainRelayCommand ButMinizeCommand { get; set; }
        public MainRelayCommand SignInCommand { get; set; }
        #endregion

        #region function
        //hàm check đăng nhập
        bool Login(string userName, string passWord)
        {
            return DAO.AccountDAO.Instance.Login(userName, passWord);
        }
        #endregion
    }
}
