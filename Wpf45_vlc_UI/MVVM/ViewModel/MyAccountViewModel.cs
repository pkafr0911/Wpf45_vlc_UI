using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using System.Xml.Serialization;
using Wpf45_vlc_UI.Constructer;
using Wpf45_vlc_UI.Core;
using Wpf45_vlc_UI.DAO;
using Wpf45_vlc_UI.MVVM.Model;
using Wpf45_vlc_UI.MVVM.View;

namespace Wpf45_vlc_UI.MVVM.ViewModel
{
    class MyAccountViewModel : ObservableObject
    {
        #region khai báo toàn cục
        private string  strImagePath = "";

        public MyAccountView myAccountView { get; set; }
        public SettingViewModel settingVM { get; set; }
        public MainRelayCommand EditUserCommand { get; set; }
        public MainRelayCommand EditUserAvatarCommand { get; set; }        
        public MainRelayCommand ChangePasswordCommand { get; set; }
        public MainRelayCommand CancelEditCommand { get; set; }
        private string _avatarSrc;

        public string AvatarSrc
        {
            get { return _avatarSrc; }  
            set { _avatarSrc = value;
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

        private string _currentPassWord;

        public string CurrentPassWord
        {
            get { return _currentPassWord; }
            set
            {
                _currentPassWord = value;
                OnPropertyChanged();
            }
        }
        private int _uId;
        public int UId
        {
            get { return _uId; }
            set
            {
                _uId = value;
            }
        }
        private int _roles;
        public int Roles
        {
            get { return _roles; }
            set
            {
                _roles = value;
            }
        }
        private int _userGroupID;
        public int UserGroupID
        {
            get { return _userGroupID; }
            set
            {
                _userGroupID = value;
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
        private bool _isAllowEiditReadOnly;

        public bool IsAllowEiditReadOnly
        {
            get { return _isAllowEiditReadOnly; }
            set
            {
                _isAllowEiditReadOnly = value;
                OnPropertyChanged();
            }
        }

        private bool _isSaveButtonVisible;

        public bool IsSaveButtonVisible
        {
            get { return _isSaveButtonVisible; }
            set
            {
                _isSaveButtonVisible = value;
                OnPropertyChanged();
            }
        }
        private bool _isCancelButtonVisible;

        public bool IsCancelButtonVisible
        {
            get { return _isCancelButtonVisible; }
            set
            {
                _isCancelButtonVisible = value;
                OnPropertyChanged();
            }
        }

        private string _isAllowEiditBackGround;

        public string IsAllowEiditBackGround
        {
            get { return _isAllowEiditBackGround; }
            set
            {
                _isAllowEiditBackGround = value;
                OnPropertyChanged();
            }
        }
        private bool _isSuccessMessageVisible;
        public bool IsSuccessMessageVisible
        {
            get { return _isSuccessMessageVisible; }
            set
            {
                _isSuccessMessageVisible = value;
                OnPropertyChanged();
            }
        }

        private bool _isFailMessageVisible;
        public bool IsFailMessageVisible
        {
            get { return _isFailMessageVisible; }
            set
            {
                _isFailMessageVisible = value;
                OnPropertyChanged();
            }
        }
        private string _failMessageContent;

        public string FailMessageContent
        {
            get { return _failMessageContent; }
            set
            {
                _failMessageContent = value;
                OnPropertyChanged();
            }
        }
        private string _successMessageContent;

        public string SuccessMessageContent
        {
            get { return _successMessageContent; }
            set
            {
                _successMessageContent = value;
                OnPropertyChanged();
            }
        }
        DispatcherTimer _timer;
        TimeSpan _time;
        #endregion 
        public MyAccountViewModel()
        {
            myAccountView = new MyAccountView();
            myAccountView.DataContext = this;

            IsSaveButtonVisible = false;
            IsAllowEiditReadOnly = true;
            IsAllowEiditBackGround = "Transparent";
            IsCancelButtonVisible = false;


            DirectoryInfo dir_info = new DirectoryInfo(System.IO.Path.Combine(Environment.CurrentDirectory, "Avatar"));


            EditUserAvatarCommand = new MainRelayCommand(o =>
            {
                OpenFileDialog op = new OpenFileDialog();
                op.Title = "Select a picture";
                op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
                  "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
                  "Portable Network Graphic (*.png)|*.png";
                op.ShowDialog();
                if (op.FileName == null)
                {
                    return;
                }


                strImagePath = ConfigurationManager.AppSettings["ImagePath"];

                int count = 0;
                foreach (FileInfo file_info in dir_info.GetFiles())
                {
                    if ((file_info.Extension.ToLower() == ".jpg") ||
                        (file_info.Extension.ToLower() == ".png"))
                    {
                        count++;
                    }

                }

                var fileNameToSave = count + 1;
                var imagePath = System.IO.Path.Combine(strImagePath + "/" + fileNameToSave + ".jpg");
                AvatarSrc = op.FileName;

                try
                {
                    File.Copy(op.FileName, imagePath);
                }
                catch (IOException ex)
                {
                    MessageBox.Show("ảnh đang đc sử dụng\n " + ex.Message);
                }
                catch (ArgumentException)
                {
                    foreach (FileInfo file_info in dir_info.GetFiles())
                    {
                        if ((file_info.Extension.ToLower() == ".jpg") ||
                            (file_info.Extension.ToLower() == ".png"))
                        {
                            AvatarSrc = file_info.FullName;
                            //Images.Add(new BitmapImage(new Uri(file_info.FullName)));
                        }

                    }
                    return;
                }

                foreach (FileInfo file_info in dir_info.GetFiles())
                {
                    if ((file_info.Extension.ToLower() == ".jpg") ||
                        (file_info.Extension.ToLower() == ".png"))
                    {
                        AvatarSrc = file_info.FullName;
                        //Images.Add(new BitmapImage(new Uri(file_info.FullName)));
                    }

                }
            });

            EditUserCommand = new MainRelayCommand(o =>
            {
                AllowEidit();
            });


            SaveProfileCommand = new RelayCommand<UIElementCollection>((p) => true, p =>
            {
                #region update user
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

                        case "tbusername":
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
                        case "pbpassword":
                            password = b.Password;
                            break;
                    }
                }
                //kiểm tra chuỗn có null hay rỗng không
                if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                {
                    FailMessage("Enter Username and Password");
                    NotAllowEidit();
                    return;
                };
                if (password == PassWord)
                {
                    if (AccountDAO.Instance.SaveAccountInfo(UId, username, password, Roles, UserGroupID, CamQuantity, UserQuantity, Wallet))
                    {
                        SuccessMessage("Account have been saved successfully ✓ \n Login Again to continue ^^");
                        MessageBox.Show("Account have been saved successfully ✓ \n Login Again to continue ^^");
                        logOut();
                    }
                    else
                    {
                        MessageBox.Show("Lưu User thất bại!!!");
                    }
                }
                else
                {
                    FailMessage("Wrong Password!!!");
                }

                //thực hiện lưu

                #endregion               
                NotAllowEidit();
            });

            CancelEditCommand = new MainRelayCommand(o =>
            {
                NotAllowEidit();
            });

            ChangePasswordCommand = new MainRelayCommand(o =>
            {

                ChangePasswordViewModel changePasswordView = new ChangePasswordViewModel(UId, userName, PassWord, Roles, UserGroupID, CamQuantity, UserQuantity, Wallet);
            });

        }


        #region icommand
        public ICommand SaveProfileCommand { get; set; }
        #endregion
        #region function
        //
        public void AllowEidit()
        {
            IsCancelButtonVisible = true;
            IsSaveButtonVisible = true;
            IsAllowEiditReadOnly = false;
            IsAllowEiditBackGround = "#4f4f4f";
        }
        public void NotAllowEidit()
        {
            IsCancelButtonVisible = false;
            IsSaveButtonVisible = false;
            IsAllowEiditReadOnly = true;
            IsAllowEiditBackGround = "Transparent";
        }
        public void SuccessMessage(string content)
        {
            SuccessMessageContent = content;
            IsSuccessMessageVisible = true;

            #region messagetimecounter
            _time = TimeSpan.FromSeconds(1);

            _timer = new DispatcherTimer(new TimeSpan(0, 0, 1), DispatcherPriority.Normal, delegate
            {
                if (_time == TimeSpan.Zero)
                {
                    IsSuccessMessageVisible = false;
                    SuccessMessageContent = "";
                    _timer.Stop();
                }
                _time = _time.Add(TimeSpan.FromSeconds(-1));
            }, Application.Current.Dispatcher);

            _timer.Start();
            #endregion
        }
        public void FailMessage(string content)
        {
            FailMessageContent = content;
            IsFailMessageVisible = true;
            #region messagetimecounter
            _time = TimeSpan.FromSeconds(1);

            _timer = new DispatcherTimer(new TimeSpan(0, 0, 1), DispatcherPriority.Normal, delegate
            {
                if (_time == TimeSpan.Zero)
                {
                    IsFailMessageVisible = false;
                    FailMessageContent = "";
                    _timer.Stop();
                }
                _time = _time.Add(TimeSpan.FromSeconds(-1));
            }, Application.Current.Dispatcher);

            _timer.Start();
            #endregion
        }

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
        #endregion







    }
}
