using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Логика взаимодействия для CartContril.xaml
    /// </summary>
    public partial class CartContril : UserControl
    {
        public static ObservableCollection<Part> Parts = new ObservableCollection<Part>();
        private Command buyCommand;
        public ICommand BuyCommand
        {
            get
            {
                return buyCommand ??
                    (buyCommand = new Command(obj =>
                    {
                        foreach(var i in Parts)
                        {
                            if (i.Quantity > 0)
                            {
                                using (DataBase db = new DataBase())
                                {
                                    i.Quantity--;
                                    db.Update(i.Id, i);
                                    MessageBox.Show("Спасибо за покупку");
                                }
                            }
                            else
                                MessageBox.Show($"Покупка отменена, товар {i.Name} отсутствует на складе");
                        }
                        Parts.Clear();
                    }));
            }
        }
        public CartContril()
        {
            InitializeComponent();
            partList.ItemsSource = Parts;
            //Text_ItemCount.Text = Parts.Count.ToString();
            //Text_OverallPrice.Text = (Parts.Count == 0 ? "0" : Parts.Sum(d => d.Price).ToString()) + "$";
        }
    }
}
