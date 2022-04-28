using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Wpf45_vlc_UI.Core;
using Wpf45_vlc_UI.DAO;
using Wpf45_vlc_UI.MVVM.View;

namespace Wpf45_vlc_UI.MVVM.ViewModel
{
    class SignInViewModel : ObservableObject
    {
        public SignInView signInView { get; set; }
        private string _userNameContent;

        public string UserNameContent
        {
            get { return _userNameContent; }
            set
            {
                _userNameContent = value;
                OnPropertyChanged();
            }
        }
        private string _passwordContent;

        public string PasswordContent
        {
            get { return _passwordContent; }
            set
            {
                _passwordContent = value;
                OnPropertyChanged();
            }
        }
        private string _comfirmPasswordContent;
        public string ComfirmPasswordContent
        {
            get { return _comfirmPasswordContent; }
            set
            {
                _comfirmPasswordContent = value;
                OnPropertyChanged();
            }
        }
        public SignInViewModel()
        {
            signInView = new SignInView();
            signInView.DataContext = this;

            UserNameContent = "";
            PasswordContent = "";
            ComfirmPasswordContent = "";

            CancelCommand = new MainRelayCommand(o =>
            {
                System.Windows.Forms.DialogResult result = CustomMessageBoxView.Show("Are Your sure want to quit Sign in ?" , CustomMessageBoxView.cMessageBoxTitle.Confirm, CustomMessageBoxView.cMessageBoxButton.Yes, CustomMessageBoxView.cMessageBoxButton.No);

                if (result == System.Windows.Forms.DialogResult.Yes)
                {
                    signInView.Visibility = Visibility.Hidden;
                }
                    
            });
            #region Done command
            DoneCommand = new RelayCommand<UIElementCollection>((p) => true, p =>
            {
                #region update user
                string username = "";
                string password = "";
                string comfirmnewpassword = "";
                //lấy username từ textbox
                //lấy password từ textbox
                foreach (var item in p)
                {
                    TextBox a = item as TextBox;
                    if (a == null)
                    {
                        continue;
                    }
                    switch (a.Name)
                    {
                        case "tbusername":
                            username = a.Text;
                            break;
                    }
                }

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
                        case "tbcomfirmnewpassword":
                            comfirmnewpassword = b.Password;
                            break;
                    }
                }
                //kiểm tra chuỗn có null hay rỗng không
                if (string.IsNullOrEmpty(username))
                {
                    UserNameContent = " - Enter UserName";
                    return;
                };
                if (string.IsNullOrEmpty(password))
                {
                    UserNameContent = "";
                    PasswordContent = " - Enter Password";
                    return;
                };
                if (string.IsNullOrEmpty(comfirmnewpassword))
                {
                    PasswordContent = "";
                    ComfirmPasswordContent = " - Enter Password";
                    return;
                };
                if (comfirmnewpassword == password)
                {
                    ComfirmPasswordContent = "";
                    //if (AccountDAO.Instance.SaveAccountInfo(uid, username, newpassword, roles, usergroupid, camquantity, userquantity, wallet))
                    if (!AccountDAO.Instance.CheckExistedUserName(username))
                    {
                        if (AccountDAO.Instance.SaveAccountInfo(0, username, password, 2, 0, 2, 0, 0))
                        {
                            CustomMessageBoxView.Show("Account have been saved successfully ✓ \n Login to continue ^^", CustomMessageBoxView.cMessageBoxTitle.Infor, CustomMessageBoxView.cMessageBoxButton.Ok, CustomMessageBoxView.cMessageBoxButton.Cancel);
                            signInView.Close();
                        }
                        else
                        {
                            CustomMessageBoxView.Show("Fail to save user!!!", CustomMessageBoxView.cMessageBoxTitle.Error, CustomMessageBoxView.cMessageBoxButton.Ok, CustomMessageBoxView.cMessageBoxButton.Cancel);
                        }
                    }
                    else
                    {
                        UserNameContent = " - Existed UserName";
                    }
                }
                else
                {
                    UserNameContent = "";
                    ComfirmPasswordContent = "- Passwords do not match!";
                }

                //thực hiện lưu

                #endregion               
            });
            #endregion
            signInView.ShowDialog();
        }

        public ICommand DoneCommand { get; set; }
        public MainRelayCommand CancelCommand { get; set; }
    }
}
