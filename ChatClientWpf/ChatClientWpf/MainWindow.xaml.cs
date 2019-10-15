using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace ChatClientWpf
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Client c = new Client();
        IMReceivedEventHandler receivedHandler;
        string UserNickname;
		//string UserPassword; //maybe should be in connect-function?
        public MainWindow()
        {
            InitializeComponent();
            receivedHandler = new IMReceivedEventHandler(im_MessageReceived);

            c.MessageReceived += receivedHandler;
        }

        void im_MessageReceived(object sender, IMReceivedEventArgs e)
        {
            if (e.From == UserNickname)
                Dispatcher.Invoke(() => ListBoxMess.Items.Add(String.Format("{0}[{2}] {1}\r\n", e.From, e.Message, DateTime.Now)));
            //    Dispatcher.BeginInvoke(DispatcherPriority.Input, new ThreadStart(() =>
            //    {

            //    }));


            //    this.BeginInvoke(new MethodInvoker(delegate
            //    {

            //    }));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string mess, to;
            mess = TextBoxMess.Text;
            to = TextBoxTo.Text;
            c.SendMessage(to, mess);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            UserNickname = UserName.Text;
            c.connect(UserNickname); //maybe here should be UserPassword, given above
        }
    }
}
