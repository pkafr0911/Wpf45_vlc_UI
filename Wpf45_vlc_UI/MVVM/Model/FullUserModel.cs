using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wpf45_vlc_UI.MVVM.Model
{
    public class FullUserModel
    {
        private int _uId;

        public int UID
        {
            get { return _uId; }
            set { _uId = value; }
        }

        private string _userName;

        public string UserName
        {
            get { return _userName; }
            set { _userName = value; }
        }
        private string _passWord;

        public string PassWord
        {
            get { return _passWord; }
            set { _passWord = value; }
        }
        private int _roles;

        public int Roles
        {
            get { return _roles; }
            set { _roles = value; }
        }
        private int _userGroupID;

        public int UserGroupID
        {
            get { return _userGroupID; }
            set { _userGroupID = value; }
        }
        private ObservableCollection<int> _comboBoxListRoels;
        public ObservableCollection<int> ComboBoxListRoels
        {
            get { return _comboBoxListRoels; }
            set
            {
                _comboBoxListRoels = value;
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

    }
}
