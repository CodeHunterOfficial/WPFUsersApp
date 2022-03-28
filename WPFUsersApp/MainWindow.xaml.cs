using System;
using System.Linq;
using System.Windows;

namespace WPFUsersApp
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ShowMethod()
        {
            using (myUsersContext db = new myUsersContext())
            {
                // получаем объекты из бд и выводим на консоль
                var users = db.Users.ToList();
                UsersList.DataContext = users;               
            }
        }

        // Добавление
        private void AddUser()
        {
            using (myUsersContext db = new myUsersContext())
            {
                User user1 = new User { Name = "Косогова Анна", Age = 18 };
                User user2 = new User { Name = "Матвеева Маргарита", Age = 18 };
                // Добавление
                db.Users.Add(user1);
                db.Users.Add(user2);
                db.SaveChanges();
            }

        }
        private void AddUser(User Adduser)
        {
            using (myUsersContext db = new myUsersContext())
            {
                // Добавление
                db.Users.Add(Adduser);
                db.SaveChanges();
            }
        }

        // Редактирование
        private void UpdateUser(User updateUser)
        {
            using (myUsersContext db = new myUsersContext())
            {
                // получаем первый объект
                User user = db.Users.Where(p => p.Id == updateUser.Id).FirstOrDefault();
                if (user != null)
                {
                    user.Name = updateUser.Name;
                    user.Age = updateUser.Age;
                    //обновляем объект
                    db.Users.Update(user);
                    db.SaveChanges();
                }
            }
        }

        // Удаление
        private void DeleteUser(int id)
        {
            using (myUsersContext db = new myUsersContext())
            {
                // получаем первый объект
                User user = db.Users.Where(p => p.Id == id).FirstOrDefault();
                if (user != null)
                {
                    //удаляем объект
                    db.Users.Remove(user);
                    db.SaveChanges();
                }

            }
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ShowMethod();
        }       
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (tbName.Text.Length < 1)
            {
                MessageBox.Show("Name Error");
                return;
            }
            if (tbAge.Text.Length < 1)
            {
                MessageBox.Show("Age Error");
                return;
            }
            string name = tbName.Text;

            int age = Convert.ToInt32(tbAge.Text);
            User Adduser = new User { Name = name, Age = age };
            AddUser(Adduser);
            ShowMethod();
        }
        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            tbName.Text = "";
            tbAge.Text = "";
        }
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (UsersList.SelectedItems.Count > 0)
            {
                int index = UsersList.SelectedIndex;
                var deleteuser = (User)UsersList.Items[index];

                MessageBoxResult result = MessageBox.Show("Действительно удалить запись в БД?", "даление контракта", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    DeleteUser(deleteuser.Id);
                    ShowMethod();
                }
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (UsersList.SelectedItems.Count > 0)
            {

                if (tbName.Text.Length < 1)
                {
                    MessageBox.Show("Name Error");
                    return;
                }
                if (tbAge.Text.Length < 1)
                {
                    MessageBox.Show("Age Error");
                    return;
                }
                int index = UsersList.SelectedIndex;
                var updateuser = (User)UsersList.Items[index];

                string name = tbName.Text;
                int age = Convert.ToInt32(tbAge.Text);
                updateuser.Name = name;
                updateuser.Age = age;

                UpdateUser(updateuser);
                ShowMethod();
            }
        }
    }
}