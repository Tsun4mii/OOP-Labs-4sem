using Lab_6_7.Commands;
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
    /// Логика взаимодействия для SearchControl.xaml
    /// </summary>
    public partial class SearchControl : UserControl
    {
        public ObservableCollection<Part> parts = new ObservableCollection<Part>();
        public SearchControl()
        {
            InitializeComponent();
            using (DataBase db = new DataBase())
            {
                parts = new ObservableCollection<Part>(db.GetParts());
                deviceGrid.ItemsSource = parts;
            }
        }
        public List<Part> deletedParts = new List<Part>();

        public static UndoManager undoCommandManager = new UndoManager();

        private UndoCommand deleteCommand;
        private UndoCommand addCommand;
        private Command saveCommand;
        private Command undoCommand;
        private Command redoCommand;
        private Command applyCommand;
        private Command updateCommand;
        
        public ICommand AddCommand
        {
            get
            {
                return addCommand ??
                      (addCommand = new UndoCommand(
                      obj =>
                      {
                          AddPartWindow addDeviceWindow = new AddPartWindow();
                          addDeviceWindow.ShowDialog();
                          using (DataBase db = new DataBase())
                          {
                              var res = new ObservableCollection<Part>(db.GetParts());

                              if (parts.Count != res.Count)
                              {
                                  parts = res;
                                  deviceGrid.ItemsSource = parts;
                                  return parts.Last();
                              }
                          }
                          return null;
                      },
                      obj =>
                      {
                          Part selected = obj as Part;
                          if (selected != null)
                          {
                              parts.Remove(selected);
                              deletedParts.Add(selected);
                              SaveCommand.Execute(null);
                          }
                      }));
            }

        }
        public ICommand SaveCommand
        {
            get
            {
                return saveCommand ??
                  (saveCommand = new Command(obj =>
                  {
                      using (DataBase db = new DataBase())
                      {
                          deletedParts.ForEach(device => db.Delete(device));

                          var old = db.GetParts();
                          if (old.Count == parts.Count)
                          {
                              for (int i = 0; i < parts.Count; i++)
                              {
                                  if (!old[i].Equals(parts[i]))
                                  {
                                      db.Update(parts[i].Id, parts[i]);
                                  }
                              }
                          }
                      }
                      deletedParts.Clear();
                  }));
            }
        }
        public ICommand DeleteCommand
        {
            get
            {
                return deleteCommand ??
                  (deleteCommand = new UndoCommand(obj =>
                  {
                      Part selected = obj as Part ?? deviceGrid.SelectedItem as Part;
                      if (selected != null)
                      {
                          parts.Remove(selected);
                          deletedParts.Add(selected);
                      }
                      return selected;
                  },
                  obj =>
                  {
                      Part selected = obj as Part;
                      if (selected != null)
                      {
                          parts.Add(selected);
                          deletedParts.Remove(selected);
                      }
                  }
                ));
            }
        }
        public ICommand ApplyCommand
        {
            get
            {
                return applyCommand ??
                  (applyCommand = new Command(obj =>
                  {
                      List<Part> result = new List<Part>(parts);
                      if (name.Text != null && name.Text != "")
                          result = new List<Part>(result.Where(d => d.Name.Contains(name.Text)));
                      if (UpPrice.Text != " " && BottomPrice.Text != " ")
                      {
                          int top = Convert.ToInt32(UpPrice.Text);  //Костль, переделать
                          int botom = Convert.ToInt32(BottomPrice.Text);
                          if (botom != null && top != null)
                          {
                              if (botom > top) top = botom;
                              result = new List<Part>(result.Where(d => d.Price <= top && d.Price >= botom));
                          }
                      }
                      deviceGrid.ItemsSource = result;
                  }));
            }
        }
        public ICommand UpdateCommand
        {
            get
            {
                return updateCommand ??
                  (updateCommand = new Command(obj =>
                  {
                      using (DataBase db = new DataBase())
                      {
                          parts = new ObservableCollection<Part>(db.GetParts());
                          deviceGrid.ItemsSource = parts;
                      }
                      deletedParts.Clear();
                  }));
            }
        }
        public ICommand UndoCommand
        {
            get
            {
                return undoCommand ??
                  (undoCommand = new Command(obj =>
                  {
                      undoCommandManager.Undo();
                  }));
            }
        }

        public ICommand RedoCommand
        {
            get
            {
                return redoCommand ??
                  (redoCommand = new Command(obj =>
                  {
                      undoCommandManager.Redo();
                  }));
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Lab_8 lab = new Lab_8();
            lab.ShowDialog();
        }
    }
}
