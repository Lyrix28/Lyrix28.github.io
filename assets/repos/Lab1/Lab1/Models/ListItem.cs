using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media;

namespace Lab1.Models
{
    public class ListItem : INotifyPropertyChanged
    {
        public string id;
        private string _title;
        public string title { get { return _title; } set { _title = value; OnPropertyChanged(); } }
        public string description { get; set; }
        private bool _completed;
        public bool completed { get { return _completed;  } set { _completed = value; OnPropertyChanged(); } }
        public DateTimeOffset date { get; set; }
        private ImageSource _imagesource;
        public ImageSource imagesource { get { return _imagesource; } set { _imagesource = value; OnPropertyChanged(); } }

        public ListItem(string title, string description, DateTimeOffset date, ImageSource imagesource)
        {
            this.id = Guid.NewGuid().ToString();
            this._title = title;
            this.description = description;
            this.date = date;
            this._imagesource = imagesource;
            this._completed = false;
        }
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName]string propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
