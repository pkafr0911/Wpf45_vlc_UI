using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Serialization;
using Wpf45_vlc_UI.Constructer;
using Wpf45_vlc_UI.Core;
using Wpf45_vlc_UI.DAO;
using Wpf45_vlc_UI.MVVM.Model;
using Wpf45_vlc_UI.MVVM.View;

namespace Wpf45_vlc_UI.MVVM.ViewModel
{    
    class SettingViewModel : ObservableObject
    {
        #region khai báo toàn cục
        //khai báo các viewmodel,view
        public SettingView settingView { get; set; }
        public MainRelayCommand LogOutCommand { get; set; }
        public MainWindow mainWindow { get; set; }
        public LoginViewModel loginViewModel { get; set; }
        public MyAccountViewModel MyAccountVM { get; set; }
        public CameraViewModel cameraVM { get; set; }
        public UserManagerViewModel UserManagerVM { get; set; }
        public UserManager_AdminViewModel UserManagerAdminVM { get; set; }
        public BuyPackageViewModel BuyPackeVM { get; set; }
        public MainRelayCommand MyAccountCommand { get; set; }
        public MainRelayCommand CameraCommand { get; set; }
        public MainRelayCommand UserManagerCommand { get; set; }
        public MainRelayCommand BuyPacketCommand { get; set; }

        private object _currentView_In_Setting;

        public object CurrentView_In_Setting
        {
            get { return _currentView_In_Setting; }
            set { _currentView_In_Setting = value;
                OnPropertyChanged();
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
        private ObservableCollection<AccountModel> _accountInfor;
        public ObservableCollection<AccountModel> AccountInfor
        {
            get { return _accountInfor; }
            set
            {
                _accountInfor = value;
            }
        }

        private ObservableCollection<AccountModel> _userInfor;
        public ObservableCollection<AccountModel> UserInfor
        {
            get { return _userInfor; }
            set
            {
                _userInfor = value;
            }
        }

        private bool _isManagerVisibility;

        public bool IsManagerVisibility
        {
            get { return _isManagerVisibility; }
            set
            {
                _isManagerVisibility = value;
                OnPropertyChanged();
            }
        }
        #endregion 

        public SettingViewModel()
        {
            #region set view và datacontext

            settingView = new SettingView();
            settingView.DataContext = this;
            #endregion

            #region gọi các viewmodel
            cameraVM = new CameraViewModel();//gọi viewmodel của CameraViewModel
            MyAccountVM = new MyAccountViewModel();//gọi viewmodel của MyAccountViewModel
            UserManagerVM = new UserManagerViewModel();
            UserManagerAdminVM = new UserManager_AdminViewModel();
            BuyPackeVM = new BuyPackageViewModel();
            CurrentView_In_Setting = MyAccountVM;//đặt mặc định
            #endregion 
            //lấy thông itn account đăng nhập
            string username, password;

            UserModel userModel = new UserModel();
            XmlSerializer serializer = new XmlSerializer(typeof(UserModel));
            using (FileStream fs = new FileStream("Account_Login.xml", FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                TextReader reader = new StreamReader(fs);

                // Declare an object variable of the type to be deserialized.
                UserModel user = (UserModel)serializer.Deserialize(reader);
                Security security = new Security();
                username = user.UserName;
                password = user.PassWord;
                username = security.Decrypt(username);
                password = security.Decrypt(password);

                MyAccountVM.userName = username;
                MyAccountVM.PassWord = password;
                fs.Close();

            }
            IsManagerVisibility = false;//set quền mặc định của user k có quền chỉnh cam cà user 

            #region Get Account Infor
            //lấy thông tin của account
            AccountInfor = AccountDAO.Instance.GetAccountInfor(username);
            FullUserModel fullUserModel = new FullUserModel();
            foreach (var s in AccountInfor)
            {
                fullUserModel.UID = s._UID;
                fullUserModel.UserName = s.UserName;
                fullUserModel.PassWord = s.PassWord;
                fullUserModel.Roles = s.Roles;
                fullUserModel.UserGroupID = s.UserGroupID;
                fullUserModel.UserQuantity = s.UserQuantity;
                fullUserModel.CamQuantity = s.CamQuantity;
                fullUserModel.Wallet = s.Wallet;
            }
            #endregion
            //nếu roles nhỏ hơn 3 sẽ có quền điêu manager
            if (fullUserModel.Roles < 3)
            {
                IsManagerVisibility = true;
            }
            //load avatar
            DirectoryInfo dir_info = new DirectoryInfo(System.IO.Path.Combine(Environment.CurrentDirectory, "Avatar"));
            foreach (FileInfo file_info in dir_info.GetFiles())
            {
                if ((file_info.Extension.ToLower() == ".jpg") ||
                    (file_info.Extension.ToLower() == ".png"))
                {
                    MyAccountVM.AvatarSrc = file_info.FullName;
                }

            }
            MyAccountVM.UId = fullUserModel.UID;
            MyAccountVM.userName = username;
            MyAccountVM.PassWord = password;
            MyAccountVM.UserGroupID = fullUserModel.UserGroupID;
            MyAccountVM.Roles = fullUserModel.Roles;
            MyAccountVM.UserQuantity = fullUserModel.UserQuantity;
            MyAccountVM.CamQuantity = fullUserModel.CamQuantity;
            MyAccountVM.Wallet = fullUserModel.Wallet;

           //command chuyển tab sang myaccount
            MyAccountCommand = new MainRelayCommand(o =>
            {
                AccountInfor = AccountDAO.Instance.GetAccountInfor(username);
                foreach (var s in AccountInfor)
                {
                    fullUserModel.UID = s._UID;
                    fullUserModel.UserName = s.UserName;
                    fullUserModel.PassWord = s.PassWord;
                    fullUserModel.Roles = s.Roles;
                    fullUserModel.UserGroupID = s.UserGroupID;
                    fullUserModel.UserQuantity = s.UserQuantity;
                    fullUserModel.CamQuantity = s.CamQuantity;
                    fullUserModel.Wallet = s.Wallet;
                }
                CurrentView_In_Setting = MyAccountVM;
                MyAccountVM.UId = fullUserModel.UID;
                MyAccountVM.userName = username;
                MyAccountVM.PassWord = password;
                MyAccountVM.UserGroupID = fullUserModel.UserGroupID;
                MyAccountVM.Roles = fullUserModel.Roles;
                MyAccountVM.UserQuantity = fullUserModel.UserQuantity;
                MyAccountVM.CamQuantity = fullUserModel.CamQuantity;
                MyAccountVM.Wallet = fullUserModel.Wallet;
                foreach (FileInfo file_info in dir_info.GetFiles())
                {
                    if ((file_info.Extension.ToLower() == ".jpg") ||
                        (file_info.Extension.ToLower() == ".png"))
                    {
                        MyAccountVM.AvatarSrc = file_info.FullName;
                    }

                }
            });
            //command chuyển tab sang camera
            CameraCommand = new MainRelayCommand(o =>
            {
                CurrentView_In_Setting = cameraVM;
                cameraVM.userName = username;
                cameraVM.ListOfCams = CameraDAO.Instance.GetCameraInfor(username);
                cameraVM.UId = fullUserModel.UID;
                cameraVM.CamQuantity = fullUserModel.CamQuantity;
                //load thông tin cam
                cameraVM.Autocompleteboxlist.Clear();
                foreach (var item in cameraVM.ListOfCams)
                {
                    cameraVM.Autocompleteboxlist.Add(item.CamName);
                    cameraVM.Autocompleteboxlist.Add(item.CamUrl);
                }

                foreach (FileInfo file_info in dir_info.GetFiles())
                {
                    if ((file_info.Extension.ToLower() == ".jpg") ||
                        (file_info.Extension.ToLower() == ".png"))
                    {
                        cameraVM.AvatarSrc = file_info.FullName;
                        //Images.Add(new BitmapImage(new Uri(file_info.FullName)));
                    }

                }


            });
            //command chuyển tab sang usermanager
            UserManagerCommand = new MainRelayCommand(o =>
            {
                if(fullUserModel.Roles == 1)//kiểm tra roles tài khoản đăng nhập là admin hay manager 
                {
                    //chuyền dữ liệu vào UserManagerAdminVM
                    CurrentView_In_Setting = UserManagerAdminVM;
                    UserManagerAdminVM.userName = username;
                    UserManagerAdminVM.PassWord = password;
                    UserManagerAdminVM.ListOfUser = AccountDAO.Instance.GetLowerAccountInfor(fullUserModel.Roles, fullUserModel.UserGroupID);
                    UserManagerAdminVM.UserGroupID = fullUserModel.UserGroupID;
                    UserManagerAdminVM.Roles = fullUserModel.Roles;
                    UserManagerAdminVM.UId = fullUserModel.UID;
                    UserManagerAdminVM.UserQuantity = fullUserModel.UserQuantity;
                    UserManagerAdminVM.CamQuantity = fullUserModel.CamQuantity;
                    UserManagerAdminVM.Autocompleteboxlist.Clear();
                    UserManagerAdminVM.IsListViewVisible = false;
                    foreach (var item in UserManagerAdminVM.ListOfUser)
                    {
                        UserManagerAdminVM.Autocompleteboxlist.Add(item.UserName);
                        UserManagerAdminVM.Autocompleteboxlist.Add(item.UserGroupID.ToString());
                    }
                    foreach (FileInfo file_info in dir_info.GetFiles())
                    {
                        if ((file_info.Extension.ToLower() == ".jpg") ||
                            (file_info.Extension.ToLower() == ".png"))
                        {
                            UserManagerAdminVM.AvatarSrc = file_info.FullName;
                            //Images.Add(new BitmapImage(new Uri(file_info.FullName)));
                        }

                    }


                }
                else
                {
                    //chuyền dữ liệu vào UserManagerVM
                    CurrentView_In_Setting = UserManagerVM;
                    UserManagerVM.userName = username;
                    UserManagerVM.ListOfUser = AccountDAO.Instance.GetLowerAccountInfor(fullUserModel.Roles, fullUserModel.UserGroupID);
                    UserManagerVM.UserGroupID = fullUserModel.UserGroupID;
                    UserManagerVM.Roles = fullUserModel.Roles;
                    UserManagerVM.UId = fullUserModel.UID;
                    UserManagerVM.UserQuantity = fullUserModel.UserQuantity;
                    UserManagerVM.CamQuantity = fullUserModel.CamQuantity;

                    UserManagerVM.Autocompleteboxlist.Clear();
                    foreach (var item in UserManagerVM.ListOfUser)
                    {
                        UserManagerVM.Autocompleteboxlist.Add(item.UserName);
                    }
                    foreach (FileInfo file_info in dir_info.GetFiles())
                    {
                        if ((file_info.Extension.ToLower() == ".jpg") ||
                            (file_info.Extension.ToLower() == ".png"))
                        {
                            UserManagerVM.AvatarSrc = file_info.FullName;
                        }

                    }

                }

            });
            //
            BuyPacketCommand = new MainRelayCommand(o =>
            {
                #region Get Account Infor
                //lấy thông tin của account
                AccountInfor = AccountDAO.Instance.GetAccountInfor(username);
                foreach (var s in AccountInfor)
                {
                    fullUserModel.UID = s._UID;
                    fullUserModel.UserName = s.UserName;
                    fullUserModel.PassWord = s.PassWord;
                    fullUserModel.Roles = s.Roles;
                    fullUserModel.UserGroupID = s.UserGroupID;
                    fullUserModel.UserQuantity = s.UserQuantity;
                    fullUserModel.CamQuantity = s.CamQuantity;
                    fullUserModel.Wallet = s.Wallet;
                }
                #endregion
                CurrentView_In_Setting = BuyPackeVM;
                BuyPackeVM.UId = fullUserModel.UID;
                BuyPackeVM.userName = username;
                BuyPackeVM.PassWord = password;
                BuyPackeVM.UserGroupID = fullUserModel.UserGroupID;
                BuyPackeVM.Roles = fullUserModel.Roles;
                BuyPackeVM.UserQuantity = fullUserModel.UserQuantity;
                BuyPackeVM.CamQuantity = fullUserModel.CamQuantity;
                BuyPackeVM.Wallet = fullUserModel.Wallet;
                foreach (FileInfo file_info in dir_info.GetFiles())
                {
                    if ((file_info.Extension.ToLower() == ".jpg") ||
                        (file_info.Extension.ToLower() == ".png"))
                    {
                        BuyPackeVM.AvatarSrc = file_info.FullName;
                    }

                }

            });


            //đăng xuất
            LogOutCommand = new MainRelayCommand(o =>
            {
                logOut();
            });



        }
        #region function
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
            mainWindow.Close();//đóng mainwindow
            
            //Application.Current.Shutdown();
        }
        #endregion
    }
}
