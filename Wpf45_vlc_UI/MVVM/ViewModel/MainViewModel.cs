using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Wpf45_vlc_UI.Core;
using Wpf45_vlc_UI.DAO;
using Wpf45_vlc_UI.MVVM.Model;
using Wpf45_vlc_UI.MVVM.View;

namespace Wpf45_vlc_UI.MVVM.ViewModel
{
    class MainViewModel : ObservableObject
    {
        #region khai báo toàn cục
        int col = 0, row = 0; //khai báo vị trí cột và hàng của control
        int ColGrid = -1, RowGrid = 0;//khai báosố lượng tất cả cột và hàng hiện tại đc chia
        int CountControl = 0;
        private string vlcLibraryPath;
        private ObservableCollection<CamSettingModel> listOfCams;
        public ObservableCollection<CamSettingModel> ListOfCams
        {
            get { return listOfCams; }
            set { listOfCams = value;
                OnPropertyChanged();
            }
        }
        private ObservableCollection<CamSettingModel> showingCamList;
        public ObservableCollection<CamSettingModel> ShowingCamList
        {
            get { return showingCamList; }
            set
            {
                showingCamList = value;
                OnPropertyChanged();
            }
        }
        public AllCamViewModel AllCamVM { get; set; }
        public SingleCamViewModel SingleCamVM { get; set; }
        public SettingViewModel SettingCamVM { get; set; }
        public MainWindow mainWindow { get; set; }
        private CamSettingModel _selectedCam;
        public CamSettingModel SelectedCam
        {
            get { return _selectedCam; }
            set
            {
                _selectedCam = value;
                OnPropertyChanged();
            }
        }
        private CamSettingModel _selectedShowingCam;
        public CamSettingModel SelectedShowingCam
        {
            get { return _selectedShowingCam; }
            set
            {
                _selectedShowingCam = value;
                OnPropertyChanged();
            }
        }
        private string _camLink;

        public string Camlink
        {
            get { return _camLink; }
            set
            {
                _camLink = value;
                OnPropertyChanged();
            }
        }
        private object _currentView;
        public object CurrentView
        {
            get { return _currentView; }
            set 
            {    _currentView = value;
                OnPropertyChanged();
            }
        }

        private object _currentView_setting;
        public object CurrentView_Setting
        {
            get { return _currentView_setting; }
            set
            {
                _currentView_setting = value;
                OnPropertyChanged();
            }
        }
        private string  _UserName;

        public string userName
        {
            get { return _UserName; }
            set { _UserName = value;
                OnPropertyChanged();
            }
        }
        private string _loadingContent;

        public string LoadingContent
        {
            get { return _loadingContent; }
            set { _loadingContent = value;
                OnPropertyChanged();
            }
        }
        private string _settingButtonContent;

        public string SettingButtonContent
        {
            get { return _settingButtonContent; }
            set { _settingButtonContent = value;
                OnPropertyChanged();
            }
        }
        public string _Username { get; set; }
        private bool _isControllerVisible;

        public bool IsControllerVisible
        {
            get { return _isControllerVisible; }
            set { _isControllerVisible = value;
                OnPropertyChanged();
            }
        }

        private string _isSettingImageURL;

        public string IsSettingImageURL
        {
            get { return _isSettingImageURL; }
            set { _isSettingImageURL = value;
                OnPropertyChanged();
            }
        }
        private bool _isFullScreenVisible;

        public bool IsFullScreenVisible
        {
            get { return _isFullScreenVisible; }
            set
            {
                _isFullScreenVisible = value;
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
        }
        #endregion

        public MainViewModel()
        {
            mainWindow = new MainWindow();
            mainWindow.DataContext = this; //set datacontext cho mainwindow
            IsFullScreenVisible = false;//mặc định ở chế độ window

            ListOfCams = new ObservableCollection<CamSettingModel>();
            ShowingCamList = new ObservableCollection<CamSettingModel>();



            #region khai báo view cho CurrentView
            AllCamVM = new AllCamViewModel();//gọi viewmodel của allcam
            SingleCamVM = new SingleCamViewModel();//gọi viewmodel của singlecam
            CurrentView = AllCamVM;//đặt mặc định 
            #endregion

            #region khai báo view cho CurrentView_Setting
            SettingCamVM = new SettingViewModel();//gọi viewmodel của settingcam
            CurrentView_Setting = SettingCamVM;//đặt view model cho view setting   
            IsControllerVisible = false;// mặt định là ẩn đi
            IsSettingImageURL = "Image/65-654632_setting-icon-clipart-png-download.png";//mặt định ảnh button cài đật
            SettingButtonContent = "Setting";
            #endregion

            #region setup vi tri chay cam
            var currentAssembly = Assembly.GetEntryAssembly();
            var currentDirectory = new FileInfo(currentAssembly.Location).DirectoryName;
            // Default installation path of VideoLAN.LibVLC.Windows
            vlcLibraryPath = Path.Combine(currentDirectory, "libvlc", IntPtr.Size == 4 ? "win-x86" : "win-x64");
            #endregion
            #region chay lan dau
            AddPreView("rtsp://ta12.vddns.vn:554/av0_0");
            ClearPreView();
            #endregion

           
            #region các command

            //command chuyển sang tab hiện tất cả cam
            AllCamCommand = new MainRelayCommand(o =>
            {
                CurrentView = AllCamVM;
                Clear();
                ShowControl();                
            });
            //command chuyển sang tab replay
            SingleCamCommand = new MainRelayCommand(o =>
            {
                CurrentView = SingleCamVM;
            });
            //command chuyển bật tắt tab setting
            CurrentViewSettingCommand = new MainRelayCommand(o =>
            {
                if (IsControllerVisible == false)
                {
                    ShowSetting();
                }
                else if (IsControllerVisible == true)
                {
                    if (string.IsNullOrEmpty(userName))
                    {
                        return;
                    }
                    HideSetting(userName);
                }
            });
            //command đóng ứng dụng
            ButCloseCommand = new MainRelayCommand(o =>
            {
                Application.Current.Shutdown();
            });
            //command thu nhỏ ứng dụng
            ButMinizeCommand = new MainRelayCommand(o =>
            {
                mainWindow.WindowState = WindowState.Minimized;
            });
            //full sceeen
            ButfullscreenCommand = new MainRelayCommand(o =>
            {
                fullScreanMode();
                mainWindow.WindowState = WindowState.Maximized;
            });
            //not full sceeen
            ButNormalScreenCommand = new MainRelayCommand(o =>
            {
                NormalScreanMode();
                mainWindow.WindowState = WindowState.Normal;
            });
            //command xóa phần tử đc chọn trong list
            DeleteSelectedItemCommand = new MainRelayCommand(o =>
            {

                object p = SelectedShowingCam;
                ShowingCamList.Remove(p as CamSettingModel);//xóa item đc chọn khỏi list
                if (CurrentView == SingleCamVM)
                {
                    return;
                }
                Clear();
                ShowControl();
            });
            ClearCommand = new MainRelayCommand(o =>
            {
                ShowingCamList.Clear();
                Clear();
            });
            #region old command
            //command thêm phần tử vào list
            //AddCommand = new RelayCommand<TextBox>((p) => true, p =>
            //{
            //    string camlink = "";


            //    camlink = p.Text;

            //    if (string.IsNullOrEmpty(camlink))
            //    {
            //        return;
            //    }
            //    CamModel cam = new CamModel()
            //    {
            //        CamLink = camlink
            //    };
            //    listOfCams.Add(cam);
            //    //ShowControl();
            //    //ShowCam(camlink);


            //});
            
            //command xem cam đc chọn
            //PlayCommand = new RelayCommand<TextBox>((p) => true, p =>
            //{
            //    string camlink = "";
            //     string camname = "";


            //    camlink = p.Text;

            //    if (string.IsNullOrEmpty(camlink))
            //    {
            //        return;
            //    }
            //    CamModel cam = new CamModel()
            //    {
            //        CamLink = camlink
            //    };
            //    ShowCam(camlink,camname);


            //});
            ////command phóng to/chỉ xem cam đc chọn
            //ZoomOutCommand = new RelayCommand<TextBox>((p) => true, p =>
            //{
            //    string camlink = "";
            //    string camname = "";

            //    camlink = p.Text;

            //    if (string.IsNullOrEmpty(camlink))
            //    {
            //        return;
            //    }
            //    CamModel cam = new CamModel()
            //    {
            //        CamLink = camlink
            //    };
            //    Clear();
            //    ShowCam(camlink, camname);


            //});
            #endregion
            //command thêm cam đc chọn
            SelectedItemCommand = new MainRelayCommand(o =>
            {
                ClearPreView();
                string camlink = SelectedCam.CamUrl.ToString();
                string camname = SelectedCam.CamName.ToString();
                int uid = SelectedCam.UId;
                int camid = SelectedCam.CamID;

                if (string.IsNullOrEmpty(camlink))
                {
                    return;
                }
                CamModel cam = new CamModel()
                {
                    CamLink = camlink
                };
                if (CurrentView == AllCamVM)
                {
                    try
                    {
                        ShowCam(camlink, camname);
                        CamSettingModel Cam = new CamSettingModel(camid, camname, camlink, uid);
                        ShowingCamList.Add(Cam);
                    }
                    catch (UriFormatException ex)
                    {
                        MessageBox.Show("Định dạng Uri Của Camera SAi !!\nInvalidOperation Exception issue: " + ex.Message);
                        return;
                    }
                    
                }
                else if (CurrentView == SingleCamVM)
                {
                    try
                    {
                        Clear();
                        ShowCam(camlink, camname);
                    }
                    catch (UriFormatException ex)
                    {
                        MessageBox.Show("Định dạng Uri Của Camera SAi !!\nInvalidOperation Exception issue: " + ex.Message);
                        return;
                    }
                    

                }
                
            });
            //command xem preview
            PopupPreViewCommand = new MainRelayCommand(o =>
            {
                string camlink = SelectedCam.CamUrl.ToString();

                if (string.IsNullOrEmpty(camlink))
                {
                    return;
                }
                CamModel cam = new CamModel()
                {
                    CamLink = camlink
                };
                AddPreView(camlink);
            });

            AsyncCommand = new AsyncRelayCommand(Execute, CanExecute);
            #endregion



            mainWindow.Show();//show mainwindow

        }
        #region Khai báo các Command
        
        public ICommand AddCommand { get; set; }
        public ICommand PlayCommand { get; set; }
        public ICommand ZoomOutCommand { get; set; }
        public MainRelayCommand AllCamCommand { get; set; }
        public MainRelayCommand SingleCamCommand { get; set; }
        public MainRelayCommand CurrentViewSettingCommand { get; set; }
        public MainRelayCommand ButCloseCommand { get; set; }
        public MainRelayCommand ButMinizeCommand { get; set; }
        public MainRelayCommand ButfullscreenCommand { get; set; }
        public MainRelayCommand ButNormalScreenCommand { get; set; }
        public MainRelayCommand SelectedItemCommand { get; set; }
        public MainRelayCommand DeleteSelectedItemCommand { get; set; }
        public MainRelayCommand ClearCommand { get; set; }
        public MainRelayCommand PopupPreViewCommand { get; set; }
        #endregion
        #region function

        //thêm cột grid
        private void AddColGrid()
        {

            ColumnDefinition colDef = new ColumnDefinition();

            colDef.Width = new GridLength(100, GridUnitType.Star);
            mainWindow.TheGrid.ColumnDefinitions.Add(colDef);


        }
        //thêm hàng grid
        private void AddRowGrid()
        {

            RowDefinition rowDef = new RowDefinition();

            rowDef.Height = new GridLength(100, GridUnitType.Star);
            mainWindow.TheGrid.RowDefinitions.Add(rowDef);

        }
        //thêm control vlc
        private void AddControl(int Row, int Col, string link, string camname)
        {
            try
            {
                
                Vlc.DotNet.Wpf.VlcControl vlcControl = new Vlc.DotNet.Wpf.VlcControl(); //tạo control mới

                var libDirectory = new DirectoryInfo(vlcLibraryPath);


                Grid.SetRow(vlcControl, Row); //set vị trí hàng
                Grid.SetColumn(vlcControl, Col);//set vị trí cột

                vlcControl.SourceProvider.CreatePlayer(libDirectory); //tạo player
                vlcControl.SourceProvider.MediaPlayer.Play(new Uri(link));//thêm đường dẫn
                vlcControl.Margin = new Thickness(5);// căn lề
                mainWindow.TheGrid.Children.Add(vlcControl); //thêm control vừa đc tạo vào grid

                TextBlock textBlock = new TextBlock();
                Grid.SetRow(textBlock, Row); //set vị trí hàng
                Grid.SetColumn(textBlock, Col);//set vị trí cột
                textBlock.FontSize = 20;
                textBlock.Text = camname;
                textBlock.Foreground = Brushes.White;
                textBlock.VerticalAlignment = VerticalAlignment.Top;
                textBlock.Margin = new Thickness(7);// căn lề
                mainWindow.TheGrid.Children.Add(textBlock);
                CountControl++;
            }
            catch(UriFormatException ex)
            {
                MessageBox.Show("Định dạng Uri Của Camera SAi !!\nInvalidOperation Exception issue: " + ex.Message);
                return;
            }

        }
        private void AddPreView(string link)
        {

            Vlc.DotNet.Wpf.VlcControl vlcControl = new Vlc.DotNet.Wpf.VlcControl(); //tạo control mới

            var libDirectory = new DirectoryInfo(vlcLibraryPath);
            Grid.SetRow(vlcControl, 0); //set vị trí hàng
            Grid.SetColumn(vlcControl, 0);//set vị trí cột

            vlcControl.SourceProvider.CreatePlayer(libDirectory); //tạo player
            vlcControl.SourceProvider.MediaPlayer.Play(new Uri(link));//thêm đường dẫn
            vlcControl.Margin = new Thickness(5);// căn lề
            mainWindow.ThePreViewGrid.Children.Add(vlcControl); //thêm control vừa đc tạo vào grid

        }
        //hiển thị tất cả cam có trong list ra
        private void ShowControl()
        {
            foreach (var c in ShowingCamList)
            {


                if (ColGrid == RowGrid)
                {
                    if (col == 999 && row == 999)
                    {
                        AddColGrid();
                        col = ColGrid + 1;
                        row = 0;
                    }
                    AddControl(row, col, c.CamUrl, c.CamName);
                    row++;
                    if (row - 1 == RowGrid)
                    {
                        col = 999;
                        row = 999;
                        ColGrid++;
                    }
                }
                else if (ColGrid == RowGrid + 1)
                {

                    if (col == 999 && row == 999)
                    {
                        AddRowGrid();
                        col = 0;
                        row = RowGrid + 1;

                    }
                    AddControl(row, col, c.CamUrl, c.CamName);
                    col++;
                    if (col - 1 == ColGrid)
                    {

                        RowGrid++;
                        col = 999;
                        row = 999;
                    }
                }
                else if (ColGrid == -1 && RowGrid == 0)
                {
                    AddControl(row, col, c.CamUrl, c.CamName);
                    ColGrid++;
                    col = 999;
                    row = 999;
                }
            }

        }
        //hiển thị từng control có link cam đc chọn
        private void ShowCam(string link, string camname)
        {
            if (ColGrid == RowGrid)
            {
                if (col == 999 && row == 999)
                {
                    AddColGrid();
                    col = ColGrid + 1;
                    row = 0;
                }
                AddControl(row, col, link, camname);
                row++;
                if (row - 1 == RowGrid)
                {
                    col = 999;
                    row = 999;
                    ColGrid++;
                }
            }
            else if (ColGrid == RowGrid + 1)
            {

                if (col == 999 && row == 999)
                {
                    AddRowGrid();
                    col = 0;
                    row = RowGrid + 1;

                }
                AddControl(row, col, link, camname);
                col++;
                if (col - 1 == ColGrid)
                {

                    RowGrid++;
                    col = 999;
                    row = 999;
                }
            }
            else if (ColGrid == -1 && RowGrid == 0)
            {
                AddControl(row, col, link, camname);
                ColGrid++;
                col = 999;
                row = 999;
            }

        }
        //xóa hàng, cột, các phần tử có trong grid
        private void Clear()
        {
            #region check overload
            if (CountControl > 25)
            {
                System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);
                Application.Current.Shutdown();
            }
            #endregion
            mainWindow.TheGrid.RowDefinitions.Clear(); //xóa hàng
            mainWindow.TheGrid.ColumnDefinitions.Clear(); //xóa cột
            mainWindow.TheGrid.Children.Clear(); // xóa các phần tử
            AddColGrid(); //thêm 1 grid có giá trị mặc định
            AddRowGrid();
            ColGrid = -1; //reset lại giá trị cột và hàng
            RowGrid = 0;
            col = 0;
            row = 0;
        }
        //xóa khung preview
        private void ClearPreView()
        {
            mainWindow.ThePreViewGrid.RowDefinitions.Clear(); //xóa hàng
            mainWindow.ThePreViewGrid.ColumnDefinitions.Clear(); //xóa cột
            mainWindow.ThePreViewGrid.Children.Clear(); // xóa các phần tử
            //thêm 1 grid Definition
            ColumnDefinition colDef = new ColumnDefinition();
            colDef.Width = new GridLength(100, GridUnitType.Star);
            mainWindow.ThePreViewGrid.ColumnDefinitions.Add(colDef);

            RowDefinition rowDef = new RowDefinition();
            rowDef.Height = new GridLength(100, GridUnitType.Star);
            mainWindow.ThePreViewGrid.RowDefinitions.Add(rowDef);

        }
        //hiện cài đặt
        public void ShowSetting()
        {
            SettingCamVM.mainWindow = mainWindow;
            IsControllerVisible = true;
            IsSettingImageURL = "Image/icon-home-full.png";
            SettingButtonContent = "Home";
        }
        //ẩn cài đặt
        public void HideSetting(string username)
        {
            ListOfCams = CameraDAO.Instance.GetCameraInfor(username);
            IsControllerVisible = false;
            IsSettingImageURL = "Image/65-654632_setting-icon-clipart-png-download.png";
            SettingButtonContent = "Setting";
        }

        private void fullScreanMode()
        {
            IsFullScreenVisible = true;
            Grid.SetRow(mainWindow.cameragrid, 0); //set vị trí hàng
            Grid.SetColumn(mainWindow.cameragrid, 0);//set vị trí cột
            Grid.SetRowSpan(mainWindow.cameragrid, 2);
            Grid.SetColumnSpan(mainWindow.cameragrid, 2);

        }

        private void NormalScreanMode()
        {
            IsFullScreenVisible = false;
            Grid.SetRow(mainWindow.cameragrid, 1); //set vị trí hàng
            Grid.SetColumn(mainWindow.cameragrid, 1);//set vị trí cột
            Grid.SetRowSpan(mainWindow.cameragrid, 1);
            Grid.SetColumnSpan(mainWindow.cameragrid, 1);

        }


        #endregion

        #region test async
        private bool asyncCommandWorking;
        public AsyncRelayCommand AsyncCommand { get; }
        private Task Execute(object obj)
        {
            return Task.Run(() =>
            {
                // do some work..
                LoadingContent = "......................";

            });
        }
        private bool CanExecute(object obj)
        {
            AsyncCommandWorking = AsyncCommand.IsWorking;
            // process other can execute logic.
            // return the result of CanExecute or not
            return AsyncCommandWorking;
        }


        public bool AsyncCommandWorking
        {
            get => asyncCommandWorking;
            private set
            {
                asyncCommandWorking = value;
                OnPropertyChanged();
            }
        }

        private async Task AddPlayerAsync(string link)
        {
            await Task.Run(async () =>
            {
                Vlc.DotNet.Wpf.VlcControl vlcControl = new Vlc.DotNet.Wpf.VlcControl(); //tạo control mới

                var libDirectory = new DirectoryInfo(vlcLibraryPath);
                Grid.SetRow(vlcControl, 0); //set vị trí hàng
                Grid.SetColumn(vlcControl, 0);//set vị trí cột

                vlcControl.SourceProvider.CreatePlayer(libDirectory); //tạo player
                vlcControl.SourceProvider.MediaPlayer.Play(new Uri(link));//thêm đường dẫn
                vlcControl.Margin = new Thickness(5);// căn lề
                

                var uiUpdatedTask = new TaskCompletionSource<bool>();

                await uiUpdatedTask.Task.ConfigureAwait(false);

                lock (mainWindow)
                {
                    // Add to a list
                    mainWindow.ThePreViewGrid.Children.Add(vlcControl); //thêm control vừa đc tạo vào grid
                }

            });
        }

        #endregion
    }
}
