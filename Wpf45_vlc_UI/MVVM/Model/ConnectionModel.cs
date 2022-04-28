using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Wpf45_vlc_UI.MVVM.Model
{
    public class ConnectionModel
    {
        private string _connection;

        public string Connection
        {
            get { return _connection; }
            set { _connection = value; }
        }

        //hàm tạo file connection mới nếu file xml bị xóa
        public void save()
        {
            ConnectionModel connectionModel = new ConnectionModel();
            connectionModel.Connection = " ";

            using (FileStream stream = new FileStream("Connection.xml", FileMode.Create))
            {
                XmlSerializer xml = new XmlSerializer(typeof(ConnectionModel));
                xml.Serialize(stream, this);
                stream.Close();
            }
            XmlSerializer x = new XmlSerializer(typeof(ConnectionModel));
            TextWriter writer = new StreamWriter("Connection.xml");
            x.Serialize(writer, connectionModel);
            writer.Close();
        }
    }
}
