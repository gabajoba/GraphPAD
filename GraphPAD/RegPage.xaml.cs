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
using System.Windows.Shapes;

namespace GraphPAD
{
    public partial class RegPage : Window
    {
        public RegPage()
        {
            InitializeComponent();
            Closing += OnClosing; //Делегат для отлова закрытия окна
        }
        private void OpenAuthPage(object sender, RoutedEventArgs e)
        {
            AuthPage authPage = new AuthPage();
            this.Visibility = Visibility.Hidden; //Скрывает текущее окно
            authPage.Show();
        }
        private void RegButton_Clicked(object sender, RoutedEventArgs e)
        {
            string name = textboxName.Text.Trim(); //Trim() - Удаление лишних символов
            string Email = textboxEmail.Text.Trim().ToLower(); //ToLower() - Перевод всех символов строки в нижний регистр
            string password1 = passwordbox_1.Password.Trim();
            string password2 = passwordbox_2.Password.Trim();
            bool nameCorrect = false;
            bool emailCorrect = false;
            bool ispassEqual = false;
            //Логика регистрации
            if (name.Length < 3)
            {
                textboxName.ToolTip = "Имя слишком короткое.\n(Минимальная длина - 3 символа)";
                textboxName.BorderBrush = Brushes.Red;
            }
            else //Имя верно
            {
                textboxName.ToolTip = name;
                textboxName.BorderBrush = Brushes.Gray;
                nameCorrect = true;
            }
            if (Email.Length < 5)
            {
                textboxEmail.ToolTip = "Логин слишком короткий.\n(Минимальная длина - 5 символов)";
                textboxEmail.BorderBrush = Brushes.Red;
            }
            else if (!Email.Contains("@") || (!Email.Contains(".")))
            {
                textboxEmail.ToolTip = "Введены некорректные данные.\n(Возможно отсутствует символ \"@\" или символ \".\")";
                textboxEmail.BorderBrush = Brushes.Red;
            }
            else //Почта верна
            {
                textboxEmail.ToolTip = Email;
                textboxEmail.BorderBrush = Brushes.Gray;
                emailCorrect = true;
            }
            if (password1 != password2 || (password1 == "" && password2 == ""))
            {
                passwordbox_1.ToolTip = "Пароли не совпадают.";
                passwordbox_1.BorderBrush = Brushes.Red;
                passwordbox_2.ToolTip = "Пароли не совпадают.";
                passwordbox_2.BorderBrush = Brushes.Red;
            }
            else if (password1.Length < 8 || password2.Length < 8)
            {
                passwordbox_1.ToolTip = "Пароль слишком короткий.\nМинимальная длина пароля - 8 символов.";
                passwordbox_1.BorderBrush = Brushes.Red;
                passwordbox_2.ToolTip = "Пароль слишком короткий.\nМинимальная длина пароля - 8 символов.";
                passwordbox_2.BorderBrush = Brushes.Red;
            }
            else //Пароли совпадают
            {
                passwordbox_1.ToolTip = "Пароли совпадают";
                passwordbox_1.BorderBrush = Brushes.Gray;
                passwordbox_2.ToolTip = "Пароли совпадают";
                passwordbox_2.BorderBrush = Brushes.Gray;
                ispassEqual = true;
            }
            if (nameCorrect == true && emailCorrect == true && ispassEqual == true)
            {
                if (MessageBox.Show(this, "Создать аккаунт с этими данными?\n\nИмя - " + textboxName.Text + "\nEmail - " + textboxEmail.Text, "Подтверждение", MessageBoxButton.YesNo) != MessageBoxResult.Yes)
                {
                    //pass
                }
                else
                {
                    MessageBox.Show("Регистрация прошла успешно", "Сообщение", MessageBoxButton.OK, MessageBoxImage.Information);
                    MainPage mainPage = new MainPage();
                    this.Visibility = Visibility.Hidden;
                    mainPage.Show();
                }
            }
        }
        private void OnClosing(object sender, CancelEventArgs cancelEventArgs) //Подтверждения выхода из программы
        {
            if (MessageBox.Show(this, "Вы действительно хотите выйти ? ", "Подтверждение", MessageBoxButton.YesNo) != MessageBoxResult.Yes)
            {
                cancelEventArgs.Cancel = true;
            }
            else
            {
                Process.GetCurrentProcess().Kill();  //Полное выключение программы
            }

        }
    }
}
