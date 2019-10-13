using System;
using System.Collections.Generic;
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

namespace ChatClientWpf
{
    /// <summary>
    /// Interaction logic for RegistrationWindow.xaml
    /// </summary>
    public partial class RegistrationWindow : Window
    {
        Client c = new Client();

        public RegistrationWindow()
        {
            InitializeComponent();
        }

        private void RegisttrationButton_Click(object sender, RoutedEventArgs e)
        {
            string login = LoginBox.Text;
            string email = EmailBox.Text;
            string password = PasswordBox.Text;
            string image = "";
        }
    }
}
