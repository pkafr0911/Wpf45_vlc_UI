using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml.Serialization;
using Wpf45_vlc_UI.Constructer;
using Wpf45_vlc_UI.DAO;
using Wpf45_vlc_UI.MVVM.Model;

namespace Wpf45_vlc_UI
{
    /// <summary>
    /// Interaction logic for Startup.xaml
    /// </summary>
    public partial class Startup : Window
    {
        public Startup()
        {
            InitializeComponent();
            string username = "";
            string password = "";
            UserModel userModel = new UserModel();
            XmlSerializer serializer =
            new XmlSerializer(typeof(UserModel));

            try
            {
                
                using (FileStream fs = new FileStream("Account_Login.xml", FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {

                    TextReader reader = new StreamReader(fs);

                    // Declare an object variable of the type to be deserialized.
                    UserModel user = (UserModel)serializer.Deserialize(reader);
                    Security security = new Security();
                    username = user.UserName;
                    password = user.PassWord;
                    username = security.Decrypt(username);//giải mã tên đăng nhập đc lưu
                    password = security.Decrypt(password);//giải mã password nhập đc lưu



                    if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))// nếu k có username/password thì thực hiện đăng nhập
                    {
                        MVVM.ViewModel.LoginViewModel lvm = new MVVM.ViewModel.LoginViewModel();
                    }
                    else //nếu có rồi thì vào thăng chương trình
                    {

                        MVVM.ViewModel.MainViewModel mvm = new MVVM.ViewModel.MainViewModel();
                        mvm.userName = username;
                        mvm.ListOfCams = CameraDAO.Instance.GetCameraInfor(username);
                    }

                }


                this.Close();

            }
            catch(InvalidOperationException ex)
            {
                MessageBox.Show("Phiên đăng nhập thất bại!!\nInvalidOperation Exception issue: " + ex.Message);
                userModel.save();
                MVVM.ViewModel.LoginViewModel lvm = new MVVM.ViewModel.LoginViewModel();// thực hiện đăng nhập lại nếu có lỗi
                this.Close();
            }
            catch (FileNotFoundException)
            {
                userModel.save();
                MVVM.ViewModel.LoginViewModel lvm = new MVVM.ViewModel.LoginViewModel();
                this.Close();
            }

        }
    }
}
