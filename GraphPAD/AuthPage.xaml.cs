using GraphPAD.Data.JSON;
using GraphPAD.Data.User;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GraphPAD
{
    public partial class AuthPage : Window
    {
        public AuthPage()
        {
            InitializeComponent();
            Closing += OnClosing; //Делегат для отлова закрытия окна
        }
        private void OpenRegPage(object sender, RoutedEventArgs e)
        {
            RegPage regPage = new RegPage();
            this.Visibility = Visibility.Hidden; //Скрывает текущее окно
            regPage.Show();
        }
        private void AuthButton_Clicked(object sender, RoutedEventArgs e)
        {
            string _Email = textboxLogin.Text.Trim().ToLower(); //ToLower() - Перевод всех символов строки в нижний регистр
            string _password = passwordboxPassword.Password.Trim(); //Trim() - Удаление лишних символов
            bool loginCorrect = false;
            bool passCorrect = false;
            //Логика авторизации
            if (_Email.Length < 5)
            {
                textboxLogin.ToolTip = "Логин слишком короткий.\n(Минимальная длина - 5 символов)"; //ToolTip - Выдаёт подсказку при наведении курсора мыши на объект
                textboxLogin.BorderBrush = Brushes.Red;
            }
            else if (!_Email.Contains("@") || (!_Email.Contains(".")))
            {
                textboxLogin.ToolTip = "Введены некорректные данные.\n(Возможно отсутствует символ \"@\" или символ \".\")";
                textboxLogin.BorderBrush = Brushes.Red;
            }
            else //Логин верен
            {
                textboxLogin.ToolTip = _Email;
                textboxLogin.BorderBrush = Brushes.Gray;
                loginCorrect = true;
            }
            if (_password.Length < 8)
            {
                passwordboxPassword.ToolTip = "Пароль слишком короткий.\nМинимальная длина пароля - 8 символов.";
                passwordboxPassword.BorderBrush = Brushes.Red;
            }
            else //Пароль верен
            {
                passwordboxPassword.ToolTip = "";
                passwordboxPassword.BorderBrush = Brushes.Gray;
                passCorrect = true;
            }
            if (loginCorrect && passCorrect)
            {
                try
                {
                    var client = new RestClient("http://testingwebrtc.herokuapp.com/login");
                    client.Timeout = -1;
                    var request = new RestRequest(Method.POST);
                    request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                    request.AddParameter("email", _Email.ToString());
                    request.AddParameter("password", _password.ToString());
                    IRestResponse response = client.Execute(request);

                    string responseData = response.Content.ToString();

                    JSONauth tempUser = JsonConvert.DeserializeObject<JSONauth>(responseData);

                    UserInfo.Email = tempUser.Data.Email;
                    UserInfo.Message = tempUser.Message;
                    UserInfo.Role = tempUser.Data.Role;
                    UserInfo.Token = tempUser.Token;

                    MessageBox.Show("Здравствуйте, " + textboxLogin.Text, "Успешный вход", MessageBoxButton.OK);
                    MainPage mainPage = new MainPage();
                    this.Visibility = Visibility.Hidden;
                    mainPage.Show();

                }
                catch
                {
                    passwordboxPassword.ToolTip = "Неверный пароль.";
                    passwordboxPassword.BorderBrush = Brushes.Red;
                    //MessageBox.Show("Введен неверный логин или пароль", "Ошибка", MessageBoxButton.OK);
                }
            }
        }
        private void OpenMainPageGuest(object sender, RoutedEventArgs e) //Открытие главного окна без входа в аккаунт ("Гостевой Профиль")
        {
            //NameEnterPage nameEnterPage = new NameEnterPage();
            //nameEnterPage.ShowDialog(); //ShowDialog открывает окно поверх, блокируя основное
            NameEnterPage nameEnterPage = new NameEnterPage();
            this.Visibility = Visibility.Hidden;
            nameEnterPage.Show();
        }
        private void OnClosing(object sender, CancelEventArgs cancelEventArgs) //Подтверждения выхода из программы
        {
            if (MessageBox.Show(this, "Вы действительно хотите выйти ? ", "Подтверждение", MessageBoxButton.YesNo) != MessageBoxResult.Yes)
            {
                cancelEventArgs.Cancel = true;
            }
            else
            {
                Process.GetCurrentProcess().Kill(); //Полное выключение программы
            }

        }
        private void checkboxRemember_Click(object sender, RoutedEventArgs e)
        {
            if (checkboxRemember.IsChecked == true) //Запоминает информацию для последующего входа в аккаунт без необходимости вводить данные
            {
                MessageBox.Show("checked", "bruh");
            }
            else
            {
                MessageBox.Show("unchecked", "bruh");
            }
        }
    }
}
