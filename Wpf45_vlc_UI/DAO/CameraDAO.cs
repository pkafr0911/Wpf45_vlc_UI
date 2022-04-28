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
    public class CameraDAO
    {
        // khai báo class instance
        private static CameraDAO instance;

        public static CameraDAO Instance
        {
            get { if (instance == null) instance = new CameraDAO(); return CameraDAO.instance; }
            private set { CameraDAO.instance = value; }
        }

        private CameraDAO() { }
        //lấy thông tin camera của tài khoản theo username
        public ObservableCollection<CamSettingModel> GetCameraInfor(string username) //username của account
        {
            ObservableCollection<CamSettingModel> listOfCams = new ObservableCollection<CamSettingModel>();

            DataTable data = DataProvider.Instance.ExecuteQuery("USP_LoadCam @Username = " + username);

            foreach (DataRow item in data.Rows)
            {
                CamSettingModel cam = new CamSettingModel(item); //từ cái item khi nhấn button đưa vô
                listOfCams.Add(cam);
            }

            return listOfCams;
        }
        //lấy thông tin camera của tài khoản theo uid
        public ObservableCollection<CamSettingModel> GetCameraInforUid(int uid) //uid của account
        {
            ObservableCollection<CamSettingModel> listOfCams = new ObservableCollection<CamSettingModel>();

            DataTable data = DataProvider.Instance.ExecuteQuery("USP_LoadCamUid @Uid = " + uid);

            foreach (DataRow item in data.Rows)
            {
                CamSettingModel cam = new CamSettingModel(item); //từ cái item khi nhấn button đưa vô
                listOfCams.Add(cam);
            }

            return listOfCams;
        }
        //hàm tìm kiếm - filter
        public ObservableCollection<CamSettingModel> GetFilterCamsInfor(int uid, string camname, string camurl) //id của Cam
        {
            ObservableCollection<CamSettingModel> listOfCams = new ObservableCollection<CamSettingModel>();

            DataTable data = DataProvider.Instance.ExecuteQuery("USP_CamsFilter "+ uid+",'"+ camname + "','"+ camurl+"'");

            foreach (DataRow item in data.Rows)
            {
                CamSettingModel cam = new CamSettingModel(item); //từ cái item khi nhấn button đưa vô
                listOfCams.Add(cam);
            }

            return listOfCams;
        }
        //lưu thông tin cam của account
        public bool SaveCameralInfo( int camid, string camurl, string camname, int uid)
        {

            string query = string.Format("USP_SaveCameralInfo @CamId = {0}, @CamUrl ='{1}', @UId ={2}, @CamName ='{3}'", camid, camurl, uid, camname);
            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result != 0;
        }
        //lưu thông tin cam đc cấp cho các user
        public bool SaveUserCameraInfo(string camurl, string camname, int uid)
        {

            string query = string.Format("USP_SaveUserCameralInfo @CamUrl ='{0}', @UId ={1}, @CamName ='{2}'", camurl, uid, camname);
            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result != 0;
        }
        //xóa cam đc chọn
        public bool DeleteCameraInfo(int camid)
        {

            string query = string.Format("UPS_DeleteCameraInfo @CamId = {0}", camid);
            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result != 0;
        }

        //xóa tất cả cam
        public bool DeleteALLCameraInfo(int uid)
        {

            string query = string.Format("UPS_DeleteALLCameraInfo @UId = {0}", uid);
            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result != 0;
        }
        //kiểm tra cam đã có trong csdl hay chưa
        public bool CheckExistingCams(int camid,int uid)
        {
            // string query = string.Format("INSERT INTO BillInfo([idBill], [idService], [intCount], [DateService]) Values ( {0} , {1} , {2} , GETDATE() )", idBill, idService, intCount );

            string query = string.Format("UPS_CheckExistingCams @CamId ={0}, @UId = {1}",camid, uid);
            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result != 0;
        }
    }
}
