using BigApp.Base;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace BigApp.Pages
{
    /// <summary>
    /// Логика взаимодействия для LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private void CreateCapthca_Click(object sender, RoutedEventArgs e)
        {
            TxbCaptcha.Text = Captcha();
        }


        /// <summary>
        /// Генератор случайных значений
        /// </summary>
        /// <returns></returns>
        string Captcha()
        {
            string Aplhabet = "0123456789QWERTYUIOPASDFGHJKLMNBVCXZ";
            string capch = "";
            Random rand = new Random();

            for (int i = 0; i < 8; i++)
            {
                capch += Aplhabet[rand.Next(Aplhabet.Length)].ToString();
            }

            return capch;
        }


        private void ShowPassword_Click(object sender, RoutedEventArgs e)
        {
            if (TxbPassword.Visibility == Visibility.Collapsed)
            {
                TxbPassword.Text = Psb.Password;
                Psb.Visibility = Visibility.Collapsed;
                TxbPassword.Visibility = Visibility.Visible;
            } else
            {
                Psb.Visibility = Visibility.Visible;
                TxbPassword.Visibility = Visibility.Collapsed;
            }
        }

        private void ToNextPage_Click(object sender, RoutedEventArgs e)
        {
            if (TxbCaptcha.Text == TxtCaptcha.Text)
            {

                var CurrentUser = BaseConnect.db.User.FirstOrDefault(x => x.Login == TxbLogin.Text && Psb.Password == x.Password);

                if (CurrentUser != null)
                {
                    NavigationService.Navigate(new HelloPage(CurrentUser));
                }
                else
                {
                    PanelCaptcha.Visibility = Visibility.Visible;
                    CreateCapthca_Click(null, null);
                }
            } else
            {

            }
        }
    }
}
