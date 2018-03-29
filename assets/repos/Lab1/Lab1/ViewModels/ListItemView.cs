using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Lab1.Models;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace Lab1.ViewModels
{
    public class ListItemView : INotifyPropertyChanged
    {
        public ListItem selectedItem;
        private static ObservableCollection<ListItem> allItems = new ObservableCollection<ListItem>();
        public ObservableCollection<ListItem> AllItems { get { return allItems; } }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName]string propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public ListItemView()
        {
            this.selectedItem = null;
        }

        public void AddListItem(string title, string description, DateTimeOffset date, ImageSource imagesource)
        {
            allItems.Add(new ListItem(title, description, date, imagesource));
        }
        public void RemoveListItem(string id)
        {
            //code
            foreach(ListItem item in allItems)
            {
                if (item.id.Equals(id))
                {
                    allItems.Remove(item);
                    break;
                }
            }
            this.selectedItem = null;
        }
        public void UpdateListItem(string id, string title, string description, DateTimeOffset date, ImageSource imagesource)
        {
            //code
            foreach (ListItem item in allItems)
            {
                if (item.id.Equals(id))
                {
                    item.title = title;
                    item.description = description;
                    item.date = date;
                    item.imagesource = imagesource;
                    break;
                }
            }
            this.selectedItem = null;
        }
    }
}
