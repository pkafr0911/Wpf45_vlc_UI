using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Serialization;
using Wpf45_vlc_UI.MVVM.Model;
using Wpf45_vlc_UI.MVVM.View;

namespace Wpf45_vlc_UI.DAO
{
    public class DataProvider
    {
        private static DataProvider instance;

        public static DataProvider Instance
        {
            get
            {
                if (instance == null) instance = new DataProvider();
                return DataProvider.instance;
            }
            private set { DataProvider.instance = value; } 
        }
        private DataProvider() { }
        string connectionSTR = "Data Source=DESKTOP-49EHB4Q,1433;Initial Catalog=QuanLyCamera;User ID=admin2;Password=admin"; //connection string mặc định

        public DataTable ExecuteQuery(string query, object[] parameter = null) //Truyền Parameter
        {
            if (ConnectionString() != " ")// nếu connection string khác rỗng thì thì gán vào strong hiện tại
            {
                connectionSTR = ConnectionString();
            }
           
            DataTable data = new DataTable();

            using (SqlConnection connection = new SqlConnection(connectionSTR))
            {
                try
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand(query, connection);

                    if (parameter != null)
                    {
                        string[] listPara = query.Split(' '); //Split theo khoảng trắng
                        int i = 0;
                        foreach (string item in listPara)
                        {
                            if (item.Contains('@')) //dấu @ chứa parameter, để add n parameter
                            {
                                command.Parameters.AddWithValue(item, parameter[i]);
                                i++;
                            }
                        }
                    }

                    SqlDataAdapter adapter = new SqlDataAdapter(command);

                    adapter.Fill(data);

                    connection.Close();
                }
                catch (SqlException ex) // This will catch all SQL exceptions
                {
                    CustomMessageBoxView.Show("Execute exception issue: " + ex.Message, CustomMessageBoxView.cMessageBoxTitle.Error, CustomMessageBoxView.cMessageBoxButton.Ok, CustomMessageBoxView.cMessageBoxButton.Cancel);
                    UserModel userModel = new UserModel();
                    userModel.save();
                    Application.Current.Shutdown();
                }
                catch (InvalidOperationException ex) // This will catch SqlConnection Exception
                {
                    CustomMessageBoxView.Show("Connection Exception issue: " + ex.Message, CustomMessageBoxView.cMessageBoxTitle.Error, CustomMessageBoxView.cMessageBoxButton.Ok, CustomMessageBoxView.cMessageBoxButton.Cancel);
                    Application.Current.Shutdown();
                }
                catch (Exception ex) // This will catch every Exception
                {
                    CustomMessageBoxView.Show("Exception issue: " + ex.Message, CustomMessageBoxView.cMessageBoxTitle.Error, CustomMessageBoxView.cMessageBoxButton.Ok, CustomMessageBoxView.cMessageBoxButton.Cancel);
                    Application.Current.Shutdown();
                    //Will catch all Exception and write the message of the Exception
                }
                finally // don't forget to close your connection when exception occurs.
                {

                    connection.Close();

                }
                
            }

            return data;
        }

        public int ExecuteNonQuery(string query, object[] parameter = null)
        {
            if (ConnectionString() != " ")
            {
                connectionSTR = ConnectionString();
            }
            int data = 0;

            using (SqlConnection connection = new SqlConnection(connectionSTR))
            {
                connection.Open();


                SqlCommand command = new SqlCommand(query, connection);

                if (parameter != null)
                {
                    string[] listPara = query.Split(' ');
                    int i = 0;
                    foreach (string item in listPara)
                    {
                        if (item.Contains('@'))
                        {
                            command.Parameters.AddWithValue(item, parameter[i]);
                            i++;
                        }
                    }
                }

                data = command.ExecuteNonQuery();

                connection.Close();
            }

            return data;
        }

        //hàm mở và đọc file xml được lưu thông tin connectionstring
        public string ConnectionString()
        {
            string connectionstring = "";
            ConnectionModel connectionModel = new ConnectionModel();
            XmlSerializer serializer =
            new XmlSerializer(typeof(ConnectionModel));

            try
            {

                using (FileStream fs = new FileStream("Connection.xml", FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {

                    TextReader reader = new StreamReader(fs);

                    // Declare an object variable of the type to be deserialized.
                    ConnectionModel connection = (ConnectionModel)serializer.Deserialize(reader);
                    connectionstring = connection.Connection;

                }
                return connectionstring;



            }
            catch (InvalidOperationException ex)
            {
                CustomMessageBoxView.Show("Hãy sửa lại connection string trong file Connection.xml !!\nInvalidOperation Exception issue: " + ex.Message, CustomMessageBoxView.cMessageBoxTitle.Error, CustomMessageBoxView.cMessageBoxButton.Ok, CustomMessageBoxView.cMessageBoxButton.Cancel);
                connectionModel.save();
                return connectionstring;

            }
            catch (FileNotFoundException ex)
            {
                CustomMessageBoxView.Show("Hãy sửa lại connection string trong file Connection.xml !!\nInvalidOperation Exception issue: " + ex.Message, CustomMessageBoxView.cMessageBoxTitle.Error, CustomMessageBoxView.cMessageBoxButton.Ok, CustomMessageBoxView.cMessageBoxButton.Cancel);
                connectionModel.save();
                return connectionstring;
            }
            

        }

    }
}
