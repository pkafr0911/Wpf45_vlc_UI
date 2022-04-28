using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wpf45_vlc_UI.MVVM.Model
{
    public class AccountModel
    {
        public AccountModel(int id, string username, string password, int roles, int usergroupid, int camquantity, int userquantity,int wallet)
        {
            this._UID = id;
            this.UserName = username;
            this.PassWord = password;
            this.Roles = roles;
            this.UserGroupID = usergroupid;
            this.CamQuantity = camquantity;
            this.UserQuantity = userquantity;
            this.Wallet = wallet;

        }

        public AccountModel(DataRow row)
        {
            this._UID = (int)row["UID"];
            this.UserName = (string)row["DB_UserName"];
            this.PassWord = (string)row["DB_PassWord"];
            this.Roles = (int)row["DB_Roles"];
            this.UserGroupID = (int)row["DB_UserGroupID"];
            this.CamQuantity = (int)row["DB_CamQuantity"];
            this.UserQuantity = (int)row["DB_UserQuantity"];
            this.Wallet = (int)row["DB_Wallet"];
        }

        private int _uID;

        public int _UID
        {
            get { return _uID; }
            set { _uID = value; }
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


        private ObservableCollection<string> _comboBoxListRoels;
        public ObservableCollection<string> ComboBoxListRoels
        {
            get {
                if (Roles == 3)
                {
                    SelectedRoles = "User";
                }

                return setComboBoxListRoels(); }
            set
            {
                
                _comboBoxListRoels = value;
            }
        }

        public ObservableCollection<string> setComboBoxListRoels()//set item cho combobox của manager
        {
            _comboBoxListRoels = new ObservableCollection<string>();
            _comboBoxListRoels.Add("User");
            return _comboBoxListRoels;
        }

        private string _selectedRoles;

        public string SelectedRoles
        {
            get { return _selectedRoles; }
            set { _selectedRoles = value; }
        }


        private ObservableCollection<string> _comboBoxListRoelsAdmin;
        public ObservableCollection<string> ComboBoxListRoelsAdmin
        {
            get
            {
                if (Roles == 1)
                {
                    SelectedRoles = "Admin";
                }
                if (Roles == 2)
                {
                    SelectedRoles = "Manager";
                }
                if (Roles == 3)
                {
                    SelectedRoles = "User";
                }

                return setComboBoxListRoelsAdmin();
            }
            set
            {

                _comboBoxListRoelsAdmin = value;
            }
        }

        public ObservableCollection<string> setComboBoxListRoelsAdmin()//set item cho combobox của admin
        {
            _comboBoxListRoelsAdmin = new ObservableCollection<string>();
            _comboBoxListRoelsAdmin.Add("Admin");
            _comboBoxListRoelsAdmin.Add("Manager");
            _comboBoxListRoelsAdmin.Add("User");
            return _comboBoxListRoelsAdmin;
        }

    }
}
