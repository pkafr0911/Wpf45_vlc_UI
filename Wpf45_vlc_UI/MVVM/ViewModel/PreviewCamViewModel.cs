using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Wpf45_vlc_UI.Core;
using Wpf45_vlc_UI.MVVM.View;

namespace Wpf45_vlc_UI.MVVM.ViewModel
{
    class PreviewCamViewModel
    {
        string vlcLibraryPath;
        public PreviewCamView previewCamView { get; set; }
        public MainRelayCommand ButCloseCommand { get; set; }
        public MainRelayCommand ButMinizeCommand { get; set; }
        public PreviewCamViewModel(string link)
        {
            #region set view và datacontext
            previewCamView = new PreviewCamView();
            previewCamView.DataContext = this;
            #endregion 

            #region setup vi tri chay cam
            var currentAssembly = Assembly.GetEntryAssembly();
            var currentDirectory = new FileInfo(currentAssembly.Location).DirectoryName;
            // Default installation path of VideoLAN.LibVLC.Windows
            vlcLibraryPath = Path.Combine(currentDirectory, "libvlc", IntPtr.Size == 4 ? "win-x86" : "win-x64");
            #endregion 

            AddPreView(link);

            //command đóng cửa sổ
            ButCloseCommand = new MainRelayCommand(o =>
            {
                previewCamView.Close();
            });
            //command thu nhỏ ứng dụng
            ButMinizeCommand = new MainRelayCommand(o =>
            {
                previewCamView.WindowState = WindowState.Minimized;
            });

            previewCamView.Show();
        }

        private void AddPreView(string link)
        {

            Vlc.DotNet.Wpf.VlcControl vlcControl = new Vlc.DotNet.Wpf.VlcControl(); //tạo control mới

            var libDirectory = new DirectoryInfo(vlcLibraryPath);
            Grid.SetRow(vlcControl, 0); //set vị trí hàng
            Grid.SetColumn(vlcControl, 0);//set vị trí cột

            vlcControl.SourceProvider.CreatePlayer(libDirectory); //tạo player
            vlcControl.SourceProvider.MediaPlayer.Play(new Uri(link));//thêm đường dẫn
            previewCamView.ThePreViewGrid.Children.Add(vlcControl); //thêm control vừa đc tạo vào grid

        }

    }
}
