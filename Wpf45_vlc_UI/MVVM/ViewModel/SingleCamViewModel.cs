using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Wpf45_vlc_UI.Core;
using Wpf45_vlc_UI.MVVM.Model;
using Wpf45_vlc_UI.MVVM.View;

namespace Wpf45_vlc_UI.MVVM.ViewModel
{
    class SingleCamViewModel : ObservableObject
    {
        public MainRelayCommand WatchCamCommand { get; set; }
        public SingleCamView singleCamView { get; set; }
        public MainWindow mainWindow { get; set; }

        private string vlcLibraryPath;
        public SingleCamViewModel()
        {
            singleCamView = new SingleCamView();
            singleCamView.DataContext = this;

            var currentAssembly = Assembly.GetEntryAssembly();
            var currentDirectory = new FileInfo(currentAssembly.Location).DirectoryName;
            // Default installation path of VideoLAN.LibVLC.Windows
            vlcLibraryPath = Path.Combine(currentDirectory, "libvlc", IntPtr.Size == 4 ? "win-x86" : "win-x64");


            WatchCamCommand = new MainRelayCommand(o =>
            {

                //if (string.IsNullOrEmpty(Camlink))
                //{
                //    return;
                //}

                //Vlc.DotNet.Wpf.VlcControl vlcControl = new Vlc.DotNet.Wpf.VlcControl(); //tạo control mới

                //var libDirectory = new DirectoryInfo(vlcLibraryPath);

                //Grid.SetRow(vlcControl, 0); //set vị trí hàng
                //Grid.SetColumn(vlcControl, 0);//set vị trí cột

                //vlcControl?.Dispose();
                //// add vlcControl vào ControlContainer

                //vlcControl.SourceProvider.CreatePlayer(libDirectory); //tạo player
                //vlcControl.SourceProvider.MediaPlayer.Play(new Uri(Camlink));//thêm đường dẫn

                //singleCamView.ControlContainer.Content = vlcControl;


            });

        }

    }
}
