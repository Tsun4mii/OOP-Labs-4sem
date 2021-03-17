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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Lab_6_7
{
    /// <summary>
    /// Логика взаимодействия для MainControl.xaml
    /// </summary>
    public partial class MainControl : UserControl
    {
        private List<Part> Parts { get; set; }
        public MainControl()
        {
            InitializeComponent();
            using (DataBase db = new DataBase())
            {
                Parts = db.GetParts(); 
            }
            partList.ItemsSource = Parts;
        }
        private void ListBoxItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Part device = (Part)partList.SelectedItem;
            CartContril.Parts.Add(device);
        }
    }
}
