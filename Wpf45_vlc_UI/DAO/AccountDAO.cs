using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wpf45_vlc_UI.MVVM.Model;

namespace Wpf45_vlc_UI.DAO
{
    class AccountDAO
    {
        // khai báo class instance
        private static AccountDAO instance;

        public static AccountDAO Instance
        {
            get { if (instance == null) instance = new AccountDAO(); return instance; }
            private set { instance = value; }
        }

        private AccountDAO() { }
        
        //hàm đăng nhập
        public bool Login(string userName, string passWord)
        {

            string query = "USP_Login @userName , @passWord ";
            DataTable result = DataProvider.Instance.ExecuteQuery(query, new object[] { userName, passWord });
            return result.Rows.Count > 0;//nếu có giá trị phù hợp trong csdl thì trả về true
        }

        //hàm load thông tin account theo username
        public ObservableCollection<AccountModel> GetAccountInfor(string username) //lấy thông tin tài khoản theo username
        {
            ObservableCollection<AccountModel> listUserModel = new ObservableCollection<AccountModel>();

            DataTable data = DataProvider.Instance.ExecuteQuery("USP_LoadAccount @Username =" + username);

            foreach (DataRow item in data.Rows)
            {
                AccountModel info = new AccountModel(item); //từ cái item khi nhấn button đưa vô
                listUserModel.Add(info);
            }

            return listUserModel;
        }

        public bool CheckExistedUserName(string username)
        {

            DataTable result = DataProvider.Instance.ExecuteQuery("USP_LoadAccount @Username =" + username);
            return result.Rows.Count > 0;//nếu có giá trị phù hợp trong csdl thì trả về true
        }

        //hàm load thông tin những account có quyền dưới và bằng account đăng nhập
        public ObservableCollection<AccountModel> GetLowerAccountInfor(int roles , int usergroupid) //lấy thông tin tài khoản theo roles và user groupid
        {
            ObservableCollection<AccountModel> listUserModel = new ObservableCollection<AccountModel>();

            DataTable data = DataProvider.Instance.ExecuteQuery("USP_GetLowerAccountInfor @Roles = "+ roles + ", @UserGroupID =" + usergroupid);//roles 1 thì hiện thông tin của tất cả account/ roles > 1 hiện theo user group id

            foreach (DataRow item in data.Rows)
            {
                AccountModel info = new AccountModel(item); 
                listUserModel.Add(info);
            }
            return listUserModel;
        }

        //hàm tìm hiếm -  filter
        public ObservableCollection<AccountModel> GetFilterAccountInfor(int roles, int usergroupid, string username, string groupid)  //lấy thông tin tài khoản theo roles, user groupid, username
        {
            ObservableCollection<AccountModel> listUserModel = new ObservableCollection<AccountModel>();

            DataTable data = DataProvider.Instance.ExecuteQuery("USP_UserFilter @Roles = " + roles + ", @UserGroupID ="+ usergroupid + ", @UserName ='"+ username + "', @GroupID = '"+ groupid + "'");

            foreach (DataRow item in data.Rows)
            {
                AccountModel info = new AccountModel(item); //từ cái item khi nhấn button đưa vô
                listUserModel.Add(info);
            }

            return listUserModel;
        }

        //lưu thông tin các tài khoản
        public bool SaveAccountInfo(int uid, string usernamme, string password, int roles, int usergroupid, int camquantity, int userquantity, int wallet)
        {

            string query = string.Format("USP_SaveUserInfor @Uid = {0}, @UserName ='{1}', @PassWord ='{2}', @Roles = {3}, @UserGroupID = {4}, @CamQuantity = {5}, @UserQuantity = {6},  @Wallet = {7} ", uid, usernamme, password, roles, usergroupid, camquantity, userquantity, wallet);
            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result != 0;
        }

        //xóa tài khoản
        public bool DeleteAccountInfo(int uid)
        {

            string query = string.Format("UPS_DeleteAccountInfo @UId = {0}", uid);
            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result != 0;
        }
    }
}
