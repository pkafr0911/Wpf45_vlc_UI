using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Wpf45_vlc_UI.MVVM.Model
{
    public class UserModel
    {
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

        public void save()
        {
            using (FileStream stream = new FileStream("Account_Login.xml", FileMode.Create))
            {
                XmlSerializer xml = new XmlSerializer(typeof(UserModel));
                xml.Serialize(stream, this);
            }

        }
    }
}
