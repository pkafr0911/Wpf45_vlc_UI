using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wpf45_vlc_UI.Core;
using Wpf45_vlc_UI.MVVM.Model;
using Wpf45_vlc_UI.MVVM.View;

namespace Wpf45_vlc_UI.MVVM.ViewModel
{
    class AboutUsViewModel : ObservableObject
    {
        public AboutUsView aboutUsView { get; set; }
        private ObservableCollection<PackageModel> packageList;
        public ObservableCollection<PackageModel> PackageList
        {
            get { return packageList; }
            set
            {
                packageList = value;
                OnPropertyChanged();
            }
        }
        public AboutUsViewModel()
        {
            aboutUsView = new AboutUsView();
            aboutUsView.DataContext = this;


            PackageList = new ObservableCollection<PackageModel>();

            PackageList.Add(new PackageModel() { PackageName = "Basic Package", Price = 20000, CamQuantity = 3, UserQuantity = 2 });
            PackageList.Add(new PackageModel() { PackageName = "Home Package", Price = 50000, CamQuantity = 5, UserQuantity = 3 });
            PackageList.Add(new PackageModel() { PackageName = "Store Package", Price = 80000, CamQuantity = 10, UserQuantity = 8 });
            PackageList.Add(new PackageModel() { PackageName = "Apartment Package", Price = 100000, CamQuantity = 20, UserQuantity = 10 });
            PackageList.Add(new PackageModel() { PackageName = "Comany Package", Price = 120000, CamQuantity = 30, UserQuantity = 15 });
        }
    }
}
