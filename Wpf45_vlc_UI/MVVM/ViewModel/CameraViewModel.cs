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
    class CameraViewModel : ObservableObject
    {
        #region khai báo toàn cục cho CameraViewModel
        public CameraVIew cameraView { get; set; }//gọi view
        public PreviewCamViewModel PreviewCamVM { get; set; }
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

        private CamSettingModel _selectedCam;
        public CamSettingModel SelectedCam//cam được chọn
        {
            get { return _selectedCam; }
            set
            {
                _selectedCam = value;
                OnPropertyChanged();
            }
        }
        private bool _isControllerVisible;
        public bool IsControllerVisible
        {
            get { return _isControllerVisible; }
            set
            {
                _isControllerVisible = value;
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
            set { _failMessageContent = value;
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

        private ObservableCollection<CamSettingModel> listOfCams;
        public ObservableCollection<CamSettingModel> ListOfCams
        {
            get { return listOfCams; }
            set { listOfCams = value;
                OnPropertyChanged();
            }
        }// list các cam đc hiểm thị
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
        private int _uId;
        public int UId
        {
            get { return _uId; }
            set
            {
                _uId = value;
            }
        }
        private int _camQuantity;

        public int CamQuantity
        {
            get { return _camQuantity; }
            set
            {
                _camQuantity = value;
                OnPropertyChanged();
            }
        }
        

        private string _avatarSrc;

        public string AvatarSrc
        {
            get { return _avatarSrc; }
            set
            {
                _avatarSrc = value;
                OnPropertyChanged();
            }
        } //đường dẫn của avatar

        private int _currentCamQuantity;

        public int CurrentCamQuantity
        {
            get
            {
                _currentCamQuantity = ListOfCams.Count();
                return _currentCamQuantity;
            }
            set
            {
                _currentCamQuantity = value;
                OnPropertyChanged();
            }
        }

        DispatcherTimer _timer;
        TimeSpan _time;
        #endregion

        public CameraViewModel()
        {
            cameraView = new CameraVIew();
            cameraView.DataContext = this;

            ListOfCams = new ObservableCollection<CamSettingModel>();
            Autocompleteboxlist = new ObservableCollection<string>();

            IsSuccessMessageVisible = false;
            IsFailMessageVisible = false;
            FailMessageContent = "";
            SuccessMessageContent = "";

            #region các command
            //command thêm cam mới
            AddNewCommand = new MainRelayCommand(o =>
            {
                string camurl = "";
                string camname = "";

                if (ListOfCams.Count() > CamQuantity - 1)
                {
                    FailMessage("User Quantity Reach Limited !!");
                    return;
                }
                CamSettingModel cam = new CamSettingModel(0,camname,camurl,UId);
                ListOfCams.Add(cam);
                CurrentCamQuantity = ListOfCams.Count();
            });
            //command lưu thông tin cam
            SaveSelectedItemCommand = new MainRelayCommand(o =>
            {
                string camurl = SelectedCam.CamUrl.ToString();
                string camname = SelectedCam.CamName.ToString();
                int uid = SelectedCam.UId;
                int camid = SelectedCam.CamID;

                if (string.IsNullOrEmpty(camurl) || string.IsNullOrEmpty(camname))
                {
                    FailMessage("Please enter camera infomation !!");
                    return;
                }
                Uri outUri;
                if (Uri.TryCreate(camurl, UriKind.Absolute, out outUri))
                {
                    //use the uri here
                    if (CameraDAO.Instance.SaveCameralInfo(camid, camurl, camname, uid))
                    {
                        //MessageBox.Show("Lưu cam thành công!");
                        ListOfCams = CameraDAO.Instance.GetCameraInfor(userName);
                        CurrentCamQuantity = ListOfCams.Count();
                        SuccessMessage("Your camera have been saved successfully ✓");
                    }
                    else
                    {
                        MessageBox.Show("Lưu cam thất bại!!!");
                    }
                }
                else
                {
                    FailMessage("Camera Url is not a valid URL  !!");
                }

                
            });
            //command hủy thay đổi 
            CancelSelectedItemCommand = new MainRelayCommand(o =>
            {
                ListOfCams = CameraDAO.Instance.GetCameraInfor(userName);
                CurrentCamQuantity = ListOfCams.Count();
            });
            //command xóa cam
            DeleteSelectedItemCommand = new MainRelayCommand(o =>
            {
                

                string camurl = SelectedCam.CamUrl.ToString();
                string camname = SelectedCam.CamName.ToString();
                int uid = SelectedCam.UId;
                int camid = SelectedCam.CamID;

                //nếu là cam chưa lưu thì thực hiện xóa luôn, k cần thông báo
                if (camid == 0)
                {
                    ListOfCams = CameraDAO.Instance.GetCameraInfor(userName);
                    CurrentCamQuantity = ListOfCams.Count();
                    return;
                }

                System.Windows.Forms.DialogResult result = CustomMessageBoxView.Show("Are Your sure want to delete " + camname, CustomMessageBoxView.cMessageBoxTitle.Confirm, CustomMessageBoxView.cMessageBoxButton.Yes, CustomMessageBoxView.cMessageBoxButton.No);

                if (result == System.Windows.Forms.DialogResult.Yes)
                {
                    if (CameraDAO.Instance.DeleteCameraInfo(camid))
                    {
                        ListOfCams = CameraDAO.Instance.GetCameraInfor(userName);
                        CurrentCamQuantity = ListOfCams.Count();
                        SuccessMessage("Camera deleted successfully ✓");
                    }
                    else
                    {
                        CustomMessageBoxView.Show("Xóa cam thất bại!!!" + camname, CustomMessageBoxView.cMessageBoxTitle.Error, CustomMessageBoxView.cMessageBoxButton.Ok, CustomMessageBoxView.cMessageBoxButton.Cancel);
                    }
                }
                else
                {
                    return;
                }

                
            });
            //command tìm kiếm
            FilterNameCommand = new RelayCommand<AutoCompleteBox>((p) => true, p =>
            {
                string filerstring = "";

                filerstring = p.Text;

                ListOfCams = CameraDAO.Instance.GetFilterCamsInfor(UId, filerstring, filerstring);

            });
            PreviewSelectedItemCommand = new MainRelayCommand(o =>
            {
                string camurl = SelectedCam.CamUrl.ToString();
                if (string.IsNullOrEmpty(camurl))
                {
                    FailMessage("Please enter camera infomation !!");
                    return;
                }
                else
                {
                    Uri outUri;
                    if (Uri.TryCreate(camurl, UriKind.Absolute, out outUri))
                    {
                        //use the uri here
                        PreviewCamVM = new PreviewCamViewModel(camurl);
                    }
                    else
                    {
                        FailMessage("Camera Url is not a valid URL  !!");
                    }
                }

                
            });
            #endregion
        }
        #region Khai báo các Command
        public ICommand FilterNameCommand { get; set; }
        public MainRelayCommand AddNewCommand { get; set; }
        public MainRelayCommand SaveSelectedItemCommand { get; set; }
        public MainRelayCommand DeleteSelectedItemCommand { get; set; }
        public MainRelayCommand CancelSelectedItemCommand { get; set; }
        public MainRelayCommand PreviewSelectedItemCommand { get; set; }
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
