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

namespace Lab_6_7
{
    /// <summary>
    /// Логика взаимодействия для AddPartWindow.xaml
    /// </summary>
    public partial class AddPartWindow : Window
    {
        private Command addCommand;
        private Command closeCommand;
        public ICommand AddCommand
        {
            get
            {
                return addCommand ??
                  (addCommand = new Command(obj =>
                  {
                      try
                      {
                          Part device = new Part();
                          device.Name = TextBox_Name.Text;
                          device.Description = TextBox_Description.Text;
                          device.Quantity = Convert.ToInt32(TextBox_Quantity.Text);
                          //device.Purhased = Convert.ToInt32(TextBox_Purchased.Text);
                          device.Price = Convert.ToInt32(TextBox_Price.Text);
                          device.ImagePath = TextBox_ImagePath.Text;
                          //if (device.HaveEmptyFields()) throw new Exception();

                          using (DataBase db = new DataBase())
                          {
                              db.AddPart(device);
                              this.Close();
                          }
                      }
                      catch
                      {
                          MessageBox.Show("Введены некорретные данные!");
                      }
                  }));
            }
        }
        public ICommand CloseCommand
        {
            get
            {
                return closeCommand ??
                  (closeCommand = new Command(obj =>
                  {
                      this.Close();
                  }));
            }
        }
        public AddPartWindow()
        {
            InitializeComponent();
        }
    }
}
