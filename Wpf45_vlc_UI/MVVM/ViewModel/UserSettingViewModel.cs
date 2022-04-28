using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Wpf45_vlc_UI.Core;
using Wpf45_vlc_UI.DAO;
using Wpf45_vlc_UI.MVVM.Model;
using Wpf45_vlc_UI.MVVM.View;

namespace Wpf45_vlc_UI.MVVM.ViewModel
{
    
    class UserSettingViewModel : ObservableObject
    {
        #region khai báo biến toàn cục
        public UserSettingView userSettingView { get; set; }
        public MainRelayCommand EscCommand { get; set; }
        public MainRelayCommand ButCloseCommand { get;  set; }
        public MainRelayCommand ButMinizeCommand { get;  set; }
        public MainRelayCommand SelectedManagerItemCommand { get; set; }
        public MainRelayCommand DeleteSelectedItemCommand { get; set; }
        public MainRelayCommand ClearUserCamCommand { get; set; }
        public MainRelayCommand CancelCommand { get; set; }
        public MainRelayCommand ConfirmCommand { get; set; }

        private ObservableCollection<CamSettingModel> listOfUserCams;
        public ObservableCollection<CamSettingModel> ListOfUserCams
        {
            get { return listOfUserCams; }
            set
            {
                listOfUserCams = value;
                OnPropertyChanged();
            }
        }
        private ObservableCollection<CamSettingModel> listOfManagerCams;
        public ObservableCollection<CamSettingModel> ListOfManagerCams
        {
            get { return listOfManagerCams; }
            set
            {
                listOfManagerCams = value;
                OnPropertyChanged();
            }
        }
        private CamSettingModel _selectedManagerCam;

        public CamSettingModel SelectedManagerCam
        {
            get { return _selectedManagerCam; }
            set { _selectedManagerCam = value; }
        }
        private CamSettingModel _selectedUserCam;

        public CamSettingModel SelectedUserCam
        {
            get { return _selectedUserCam; }
            set { _selectedUserCam = value; }
        }
        private bool _isCamChecked;

        public bool IsCamChecked
        {
            get { return _isCamChecked; }
            set { _isCamChecked = value;
                OnPropertyChanged();
            }
        }


        private int _managerUId;
        public int ManagerUId
        {
            get { return _managerUId; }
            set
            {
                _managerUId = value;
            }
        }
        private int _userUId;
        public int UserUId
        {
            get { return _userUId; }
            set
            {
                _userUId = value;
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
        #endregion 

        public UserSettingViewModel()
        {
            #region set vew và datacontext
            userSettingView = new UserSettingView();
            userSettingView.DataContext = this;
            #endregion

            ListOfManagerCams = new ObservableCollection<CamSettingModel>();
            ListOfUserCams = new ObservableCollection<CamSettingModel>();

            #region các command
            EscCommand = new MainRelayCommand(o =>
            {
                
            });
            //command đóng cửa sổ
            ButCloseCommand = new MainRelayCommand(o =>
            {
                userSettingView.Close();
            });
            //command thu nhỏ ứng dụng
            ButMinizeCommand = new MainRelayCommand(o =>
            {
                userSettingView.WindowState = WindowState.Minimized;
            });
            //command cấp quyền cam đc chọn cho user 
            SelectedManagerItemCommand = new MainRelayCommand(o =>
            {
                string camurl = SelectedManagerCam.CamUrl.ToString();
                string camname = SelectedManagerCam.CamName.ToString();
                int camid = SelectedManagerCam.CamID;
                bool iscamchecked = SelectedManagerCam.IsCamChecked;

                if (iscamchecked)//nếu check thì add vào list
                {
                    CamSettingModel Cam = new CamSettingModel(camid, camname, camurl, UserUId);
                    ListOfUserCams.Add(Cam);
                    
                }
                else//nếu bỏ check thì xóa khỏi list
                {
                    object p = SelectedUserCam;
                    
                    foreach (var item in ListOfUserCams)
                    {
                        if(item.CamName == camname)
                        {
                            p = item;
                        }
                    }
                    ListOfUserCams.Remove(p as CamSettingModel);//xóa item đc chọn khỏi list

                }

                

            });
            //command xóa cam đc cấp cho user
            DeleteSelectedItemCommand = new MainRelayCommand(o =>
            {

                object p = SelectedUserCam;
                ListOfUserCams.Remove(p as CamSettingModel);//xóa item đc chọn khỏi list

            });
            //command xóa toàn bộ cam đc cấp cho user
            ClearUserCamCommand = new MainRelayCommand(o =>
            {

                ListOfUserCams.Clear();//xóa item đc chọn khỏi list

            });
            //command hủy thay đổi
            CancelCommand = new MainRelayCommand(o =>
            {
                userSettingView.Visibility = Visibility.Hidden;

            });
            //comand lưu thay đổi
            ConfirmCommand = new MainRelayCommand(o =>
            {
                if (!CameraDAO.Instance.DeleteALLCameraInfo(UserUId))
                {
                    return;
                }

                foreach (var item in ListOfUserCams) //với mỗi item trong list sẽ đc add vào database
                {
                    if (string.IsNullOrEmpty(item.CamName))//kiểm tra nếu tên cam mà null thì return
                    {
                        return;
                    }
                    if (CameraDAO.Instance.SaveUserCameraInfo(item.CamUrl, item.CamName, UserUId))//lưu cam
                    {

                    }
                    else
                    {
                        MessageBox.Show("Lưu thất bại!!! cho cam " + item.CamName);
                        ListOfUserCams = CameraDAO.Instance.GetCameraInforUid(UserUId);//tải lại list cam
                        return;
                    }
                }
                userSettingView.Visibility = Visibility.Hidden;//đóng cửa sổ 

            });
            #endregion

            userSettingView.Show();
        }
    }
}
