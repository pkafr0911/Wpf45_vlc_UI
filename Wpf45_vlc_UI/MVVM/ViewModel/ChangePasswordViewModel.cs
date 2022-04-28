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
using Wpf45_vlc_UI.Core;
using Wpf45_vlc_UI.DAO;
using Wpf45_vlc_UI.MVVM.Model;
using Wpf45_vlc_UI.MVVM.View;

namespace Wpf45_vlc_UI.MVVM.ViewModel
{
     class ChangePasswordViewModel : ObservableObject
    {
        
        ChangePasswordView changePasswordView { get; set; }
        
        private int _uId;
        public int UId
        {
            get { return _uId; }
            set
            {
                _uId = value;
            }
        }
        private string _UserName;

        public string userName
        {
            get { return _UserName; }
            set
            {
                _UserName = value;
                OnPropertyChanged();
            }
        }

        private string _passWord;

        public string PassWord
        {
            get { return _passWord; }
            set
            {
                _passWord = value;
                OnPropertyChanged();
            }
        }
        private int _camQuantity;

        public int CamQuantity
        {
            get { return _camQuantity; }
            set { _camQuantity = value; }
        }

        private int _userQuantity;

        public int UserQuantity
        {
            get { return _userQuantity; }
            set { _userQuantity = value; }
        }
        private int _wallet;

        public int Wallet
        {
            get { return _wallet; }
            set { _wallet = value; }
        }
        private string _curentPasswordContent;

        public string CurentPasswordContent
        {
            get { return _curentPasswordContent; }
            set { _curentPasswordContent = value;
                OnPropertyChanged();
            }
        }
        private string _newPasswordContent;

        public string NewPasswordContent
        {
            get { return _newPasswordContent; }
            set { _newPasswordContent = value;
                OnPropertyChanged();
            }
        }
        private string _comfirmPasswordContent;

        public string ComfirmPasswordContent
        {
            get { return _comfirmPasswordContent; }
            set { _comfirmPasswordContent = value;
                OnPropertyChanged();
            }
        }
        public ChangePasswordViewModel(int uid,string  username, string password,int roles,int  usergroupid,int camquantity, int userquantity, int wallet)
        {
            #region set view và datacontext
            changePasswordView = new ChangePasswordView();
            changePasswordView.DataContext = this;
            #endregion 

            CurentPasswordContent = "";
            NewPasswordContent = "";
            ComfirmPasswordContent = "";

            //command đóng cửa sổ
            ButCloseCommand = new MainRelayCommand(o =>
            {
                changePasswordView.Close();
            });
            //command thu nhỏ ứng dụng
            ButMinizeCommand = new MainRelayCommand(o =>
            {
                changePasswordView.WindowState = WindowState.Minimized;
            });
            CancelCommand = new MainRelayCommand(o =>
            {
                changePasswordView.Visibility = Visibility.Hidden;
            });
            DoneCommand = new RelayCommand<UIElementCollection>((p) => true, p =>
            {
                #region update user
                string newpassword = "";
                string currentpassword = "";
                string comfirmnewpassword = "";
                //lấy username từ textbox
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
                        case "tbcurrentpassword":
                            currentpassword = b.Password;
                            break;
                        case "tbnewpassword":
                            newpassword = b.Password;
                            break;
                        case "tbcomfirmnewpassword":
                            comfirmnewpassword = b.Password;
                            break;
                    }
                }
                //kiểm tra chuỗn có null hay rỗng không
                if (string.IsNullOrEmpty(currentpassword))
                {
                    CurentPasswordContent = " - Enter Password";
                    return;
                };
                if (string.IsNullOrEmpty(newpassword))
                {
                    CurentPasswordContent = "";
                    NewPasswordContent = " - Enter Password";
                    return;
                };
                if (string.IsNullOrEmpty(comfirmnewpassword))
                {
                    NewPasswordContent = "";
                    ComfirmPasswordContent = " - Enter Password";
                    return;
                };
                if (currentpassword == password)
                {
                    CurentPasswordContent = "";
                    if (comfirmnewpassword == newpassword)
                    {
                        if (AccountDAO.Instance.SaveAccountInfo(uid, username, newpassword, roles, usergroupid, camquantity, userquantity, wallet))
                        {
                            logOut();
                            MessageBox.Show("Account have been saved successfully ✓ \n Login Again to continue ^^");
                        }
                        else
                        {
                            MessageBox.Show("Lưu User thất bại!!!");
                        }
                    }else
                    {
                        ComfirmPasswordContent = "Passwords do not match!";
                    }
                }
                else
                {
                    CurentPasswordContent = " - Wrong password";
                }

                //thực hiện lưu

                #endregion               
            });

            changePasswordView.Show();
        }

        public ICommand DoneCommand { get; set; }
        public MainRelayCommand ButCloseCommand { get; set; }
        public MainRelayCommand ButMinizeCommand { get; set; }
        public MainRelayCommand CancelCommand { get; set; }

        private void logOut()
        {
            UserModel userModel = new UserModel();

            #region xóa thông tin tài khoản đang đăng nhập
            XmlSerializer x = new XmlSerializer(typeof(UserModel));
            TextWriter writer = new StreamWriter("Account_Login.xml");
            x.Serialize(writer, userModel);
            writer.Close();
            #endregion

            MVVM.ViewModel.LoginViewModel lvm = new MVVM.ViewModel.LoginViewModel();//mở cửa sổ login
            System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);
            Application.Current.Shutdown();

            //Application.Current.Shutdown();
        }
    }
}
