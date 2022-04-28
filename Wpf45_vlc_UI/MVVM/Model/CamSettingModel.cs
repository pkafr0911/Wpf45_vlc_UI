using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wpf45_vlc_UI.DAO;

namespace Wpf45_vlc_UI.MVVM.Model
{
    public class CamSettingModel
    {
        public CamSettingModel(int camid, string camname, string camurl, int uid)
        {
            this.CamID = camid;
            this.CamName = camname;
            this.CamUrl = camurl;
            this.UId = uid;

        }

        public CamSettingModel(DataRow row)
        {
            this.CamID = (int)row["CamID"];
            this.CamUrl = row["DB_CamURL"].ToString(); ;
            this.UId = (int)row["UID"];
            this.CamName = row["DB_CamName"].ToString(); ;
            
           

        }

        private int _camID;

        public int CamID
        {
            get { return _camID; }
            set { _camID = value; }
        }

        private string _camName;

        public string CamName
        {
            get { return _camName; }
            set { _camName = value; }
        }

        private string _camUrl;

        public string CamUrl
        {
            get { return _camUrl; }
            set { _camUrl = value; }
        }

        private int _uId;

        public int UId
        {
            get { return _uId; }
            set { _uId = value; }
        }

        private bool _isCamChecked;

        public bool IsCamChecked
        {
            get { return _isCamChecked; }
            set { _isCamChecked = value; }
        }

    }
}
