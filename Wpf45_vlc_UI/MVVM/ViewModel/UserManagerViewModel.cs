using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using Wpf45_vlc_UI.Core;
using Wpf45_vlc_UI.DAO;
using Wpf45_vlc_UI.MVVM.Model;
using Wpf45_vlc_UI.MVVM.View;

namespace Wpf45_vlc_UI.MVVM.ViewModel
{
    class UserManagerViewModel : ObservableObject
    {
        #region khai báo toàn cục
        public UserManagerView userManagerView { get; set; }
        private string _avatarSrc;
        public string AvatarSrc
        {
            get { return _avatarSrc; }
            set
            {
                _avatarSrc = value;
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
        private AccountModel _selectedUser;
        public AccountModel SelectedUser
        {
            get { return _selectedUser; }
            set
            {
                _selectedUser = value;
                OnPropertyChanged();
            }
        }
        private object _currentView_In_User_Setting;

        public object CurrentView_In_User_Setting
        {
            get { return _currentView_In_User_Setting; }
            set { _currentView_In_User_Setting = value; }
        }

        private ObservableCollection<AccountModel> listOfUser;
        public ObservableCollection<AccountModel> ListOfUser
        {
            get { return listOfUser; }
            set
            {
                listOfUser = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<string> _comboBoxListRoels;
        public ObservableCollection<string> ComboBoxListRoels
        {
            get { return _comboBoxListRoels; }
            set
            {
                _comboBoxListRoels = value;
                OnPropertyChanged();
            }
        }
        private ObservableCollection<string> _autocompleteboxlist;
        public ObservableCollection<string> Autocompleteboxlist
        {
            get { return _autocompleteboxlist; }
            set
            {
                _autocompleteboxlist = value;
                OnPropertyChanged();
            }
        }

        public MainRelayCommand AddNewCommand { get; private set; }
        public MainRelayCommand SaveSelectedItemCommand { get; set; }
        public MainRelayCommand CancelSelectedItemCommand { get; set; }
        public MainRelayCommand DeleteSelectedItemCommand { get; set; }
        public MainRelayCommand SettingSelectedItemCommand { get; set; }

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
        private int _currentUserQuantity;

        public int CurrentUserQuantity
        {
            get {
                _currentUserQuantity = ListOfUser.Count();
                return _currentUserQuantity; }
            set {
                _currentUserQuantity = value;
                OnPropertyChanged();
            }
        }

        private int _wallet;

        public int Wallet
        {
            get { return _wallet; }
            set { _wallet = value; }
        }

        DispatcherTimer _timer;
        TimeSpan _time;
        #endregion
        public UserManagerViewModel()
        {
            #region set view và datacontext
            userManagerView = new UserManagerView();
            userManagerView.DataContext = this;
            #endregion

            ListOfUser = new ObservableCollection<AccountModel>();
            Autocompleteboxlist = new ObservableCollection<string>();


            #region các command
            //command thêm account
            AddNewCommand = new MainRelayCommand(o =>
            {
                string username = "";
                string password = "";

                if (ListOfUser.Count() > UserQuantity - 1)
                {
                    FailMessage("User Quantity Reach Limited !!");
                    return;
                }
                AccountModel cam = new AccountModel(0, username, password, Roles + 1, UserGroupID,0,0,0);
                ListOfUser.Add(cam);
                CurrentUserQuantity = ListOfUser.Count();
            });
            //command lưu thông tin sửa đổi
            SaveSelectedItemCommand = new MainRelayCommand(o =>
            {
                string username = SelectedUser.UserName.ToString();
                string password = SelectedUser.PassWord.ToString();
                int uid = SelectedUser._UID;
                int roles = 3;
                int usergroupid = SelectedUser.UserGroupID;
                int camquantity = SelectedUser.CamQuantity;
                int userquantity = SelectedUser.UserQuantity;
                int wallet = SelectedUser.Wallet;

                //xử lý roles đc chọn tại view

                if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                {
                    FailMessage("Please enter Username and Password infomation !!");
                    return;
                }

                if (ListOfUser.Count() > UserQuantity)
                {
                    FailMessage("User Quantity Reach Limited !!");
                    return;
                }
                
                if (!AccountDAO.Instance.CheckExistedUserName(username))//kiểm tra username tồn tại chưa
                {
                    //thực hiện lưu
                    if (AccountDAO.Instance.SaveAccountInfo(uid, username, password, roles, usergroupid, camquantity, userquantity, wallet))
                    {
                        SuccessMessage("User have been saved successfully ✓");
                        foreach (var item in ListOfUser)
                        {
                            Autocompleteboxlist.Add(item.UserName);
                        }
                        ListOfUser = AccountDAO.Instance.GetLowerAccountInfor(Roles, UserGroupID);
                        CurrentUserQuantity = ListOfUser.Count();
                    }
                    else
                    {
                        MessageBox.Show("Lưu User thất bại!!!");
                    }
                }
                else
                {
                    FailMessage("Existed UserName");
                    return;
                }
            });
            //command hủy thay đổi
            CancelSelectedItemCommand = new MainRelayCommand(o =>
            {
                ListOfUser = AccountDAO.Instance.GetLowerAccountInfor(Roles, UserGroupID);
                CurrentUserQuantity = ListOfUser.Count();
            });
            //command xóa account
            DeleteSelectedItemCommand = new MainRelayCommand(o =>
            {
                string username = SelectedUser.UserName.ToString();
                string password = SelectedUser.PassWord.ToString();
                int uid = SelectedUser._UID;
                int roles = SelectedUser.Roles;
                int usergroupid = SelectedUser.UserGroupID;
                int camquantity = SelectedUser.CamQuantity;
                int userquantity = SelectedUser.UserQuantity;


                System.Windows.Forms.DialogResult result = CustomMessageBoxView.Show("Are Your sure want to delete " + username, CustomMessageBoxView.cMessageBoxTitle.Confirm, CustomMessageBoxView.cMessageBoxButton.Yes, CustomMessageBoxView.cMessageBoxButton.No);

                if (result == System.Windows.Forms.DialogResult.Yes)
                {
                    if (uid == 0)
                    {
                        ListOfUser = AccountDAO.Instance.GetLowerAccountInfor(Roles, UserGroupID);
                        CurrentUserQuantity = ListOfUser.Count();
                        return;
                    }
                    if (AccountDAO.Instance.DeleteAccountInfo(uid))
                    {
                        foreach (var item in ListOfUser)
                        {
                            Autocompleteboxlist.Add(item.UserName);
                        }
                        ListOfUser = AccountDAO.Instance.GetLowerAccountInfor(Roles, UserGroupID);
                        CurrentUserQuantity = ListOfUser.Count();
                        SuccessMessage("User deleted successfully ✓");

                    }
                    else
                    {
                        CustomMessageBoxView.Show("Xóa user thất bại!!!", CustomMessageBoxView.cMessageBoxTitle.Error, CustomMessageBoxView.cMessageBoxButton.Ok, CustomMessageBoxView.cMessageBoxButton.Cancel);
                    }
                }

                    
            });
            //command chuyển tab quản lý account đc chọn 
            SettingSelectedItemCommand = new MainRelayCommand(o =>
            {
                string username = SelectedUser.UserName.ToString();
                string password = SelectedUser.PassWord.ToString();
                int uid = SelectedUser._UID;
                int roles = SelectedUser.Roles;
                int usergroupid = SelectedUser.UserGroupID;

                UserSettingViewModel usvm = new UserSettingViewModel();
                usvm.ManagerUId = UId;
                usvm.UserUId = uid;
                usvm.userName = username;
                usvm.ListOfUserCams = CameraDAO.Instance.GetCameraInforUid(uid);
                usvm.ListOfManagerCams = CameraDAO.Instance.GetCameraInforUid(UId);


            });
            //command tìm kiếm - filter
            FilterNameCommand = new RelayCommand<AutoCompleteBox>((p) => true, p =>
            {
                string filerstring = "";

                filerstring = p.Text;

                ListOfUser = AccountDAO.Instance.GetFilterAccountInfor(Roles, UserGroupID, filerstring, filerstring);

            });
            #endregion
        }
        #region icommand
        public ICommand FilterNameCommand { get; set; }
        #endregion
        #region function
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
        #endregion
    }
}
