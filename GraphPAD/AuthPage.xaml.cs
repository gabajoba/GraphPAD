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
            string login = textboxLogin.Text.Trim().ToLower(); //ToLower() - Перевод всех символов строки в нижний регистр
            string password = passwordboxPassword.Password.Trim(); //Trim() - Удаление лишних символов
            bool loginCorrect = false;
            bool passCorrect = false;
            //Логика авторизации
            if (login.Length < 5)
            {
                textboxLogin.ToolTip = "Логин слишком короткий.\n(Минимальная длина - 5 символов)"; //ToolTip - Выдаёт подсказку при наведении курсора мыши на объект
                textboxLogin.BorderBrush = Brushes.Red;
            } 
            else if(!login.Contains("@") || (!login.Contains(".")))
            {
                textboxLogin.ToolTip = "Введены некорректные данные.\n(Возможно отсутствует символ \"@\" или символ \".\")";
                textboxLogin.BorderBrush = Brushes.Red;
            } 
            else //Логин верен
            {
                textboxLogin.ToolTip = login;
                textboxLogin.BorderBrush = Brushes.Gray;
                loginCorrect = true;
            }
            if (password.Length < 8)
            {
                passwordboxPassword.ToolTip = "Пароль слишком короткий.\nМинимальная длина пароля - 8 символов.";
                passwordboxPassword.BorderBrush = Brushes.Red;
            }
            else if (password != "qwertyuiop")
            {
                passwordboxPassword.ToolTip = "Неверный пароль.";
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
                MessageBox.Show("Здравствуйте, " + textboxLogin.Text, "Успешный вход", MessageBoxButton.OK);
                MainPage mainPage = new MainPage();
                this.Visibility = Visibility.Hidden;
                mainPage.Show();
            }
        }
        private void OpenMainPageWithoutAuth(object sender, RoutedEventArgs e) //Открытие главного окна без входа в аккаунт ("Гостевой Профиль")
        {
            MainPage mainPage = new MainPage();
            this.Visibility = Visibility.Hidden;
            mainPage.Show();
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
    }
}
