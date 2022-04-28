using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wpf45_vlc_UI.MVVM.Model
{
    public class PackageModel
    {
        private string _packageName;

        public string PackageName
        {
            get { return _packageName; }
            set { _packageName = value; }
        }

        private int _price;

        public int Price
        {
            get { return _price; }
            set { _price = value; }
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
        private int _vAT;

        public int VAT
        {
            get {
                _vAT = (int)(Price * 0.1);
                return _vAT; }
            set { _vAT = value; }
        }

        private int _total;

        public int Total
        {
            get
            {
                _total = (int)(Price + VAT);
                return _total;
            }
            set { _total = value; }
        }



    }
}
