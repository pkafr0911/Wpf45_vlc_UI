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
    class BuyPackageViewModel : ObservableObject
    {
        public BuyPackageView buyPackageView { get; set; }
        public MainRelayCommand PurchaseCommand { get; set; }
        public MainRelayCommand AddFundsCommand { get; set; }

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
        private PackageModel _packageModel;

        public PackageModel SelectedPackage
        {
            get { return _packageModel; }
            set { _packageModel = value;
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
            set { _camQuantity = value;
                OnPropertyChanged();
            }
        }

        private int _userQuantity;

        public int UserQuantity
        {
            get { return _userQuantity; }
            set { _userQuantity = value;
                OnPropertyChanged();
            }
        }
        private int _wallet;

        public int Wallet
        {
            get { return _wallet; }
            set { _wallet = value;
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


        public BuyPackageViewModel()
        {
            buyPackageView = new BuyPackageView();
            buyPackageView.DataContext = this;

            PackageList = new ObservableCollection<PackageModel>();

            PackageList.Add(new PackageModel() { PackageName = "Basic Package", Price = 20000, CamQuantity = 3, UserQuantity = 2 });
            PackageList.Add(new PackageModel() { PackageName = "Home Package", Price = 50000, CamQuantity = 5, UserQuantity = 3 });
            PackageList.Add(new PackageModel() { PackageName = "Store Package", Price = 80000, CamQuantity = 10, UserQuantity = 8 });
            PackageList.Add(new PackageModel() { PackageName = "Apartment Package", Price = 100000, CamQuantity = 20, UserQuantity = 10 });
            PackageList.Add(new PackageModel() { PackageName = "Comany Package", Price = 120000, CamQuantity = 30, UserQuantity = 15 });


            PurchaseCommand = new MainRelayCommand(o =>
            {
                try
                {
                    int total = SelectedPackage.Total;
                    int camquantity = SelectedPackage.CamQuantity;
                    int userquantity = SelectedPackage.UserQuantity;

                    if (UId == 0) 
                    {
                        return;
                    }
                    if (Wallet - total < 0)
                    {
                        MessageBox.Show("mua thất bại!!!");
                        return;
                    }

                    if (AccountDAO.Instance.SaveAccountInfo(UId, userName, PassWord, Roles, UserGroupID, CamQuantity + camquantity, UserQuantity + userquantity, Wallet - total))
                    {
                        MessageBox.Show("mua thanh cong");
                        UpdateAcountInfor();
                    }
                    else
                    {
                        MessageBox.Show("mua thất bại!!!");
                    }
                }
                catch(NullReferenceException ex)
                {
                    MessageBox.Show(""+ex);
                }
                

            });
            AddFundsCommand = new MainRelayCommand(o =>
            {
                System.Diagnostics.Process.Start("https://sandbox.vnpayment.vn/tryitnow/Home/CreateOrder");

            });
        }
        public void UpdateAcountInfor()
        {
            #region Get Account Infor
            //lấy thông tin của account
            AccountInfor = AccountDAO.Instance.GetAccountInfor(userName);
            foreach (var s in AccountInfor)
            {
                UserQuantity = s.UserQuantity;
                CamQuantity = s.CamQuantity;
                Wallet = s.Wallet;
            }
            #endregion
        }

    }
}
