using Lab1.Models;
using Lab1.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace Lab1
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class NewPage : Page
    {
        public ListItemView ViewModel = null;

        public NewPage()
        {
            this.InitializeComponent();
        }

        private async void imageClick(object sender, RoutedEventArgs e)
        {
            //文件选择器  
            FileOpenPicker openPicker = new FileOpenPicker();
            //选择视图模式  
            openPicker.ViewMode = PickerViewMode.Thumbnail;
            //openPicker.ViewMode = PickerViewMode.List;  
            //初始位置  
            openPicker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
            //添加文件类型  
            openPicker.FileTypeFilter.Add(".jpg");
            openPicker.FileTypeFilter.Add(".jpeg");
            openPicker.FileTypeFilter.Add(".png");

            StorageFile file = await openPicker.PickSingleFileAsync();
            if (file != null)
            {
                using (IRandomAccessStream stream = await file.OpenAsync(FileAccessMode.Read))
                {
                    var srcImage = new BitmapImage();
                    await srcImage.SetSourceAsync(stream);
                    image.Source = srcImage;
                }
            }
        }

        private async void createClick(object sender, RoutedEventArgs e)
        {
            string content = create.Content.ToString();
            if (datepicker.Date <= DateTime.Today)
            {
                var msgDialog = new Windows.UI.Popups.MessageDialog("请选择正确的日期") { Title = "DateError" };
                await msgDialog.ShowAsync();
            }
            else if (title.Text == "" || detail.Text == "")
            {
                var msgDialog = new Windows.UI.Popups.MessageDialog("请输入文本内容") { Title = "TextError" };
                await msgDialog.ShowAsync();
            }
            else
            {
                string _title = title.Text;
                string _detail = detail.Text;
                DateTimeOffset _date = datepicker.Date;
                string text = "";
                if (content.Equals("Create"))
                {
                    ViewModel.AddListItem(_title, _detail, _date, image.Source);
                    text = "添加成功";
                }
                else
                {
                    ViewModel.UpdateListItem(ViewModel.selectedItem.id, _title, _detail, _date, image.Source);
                    text = "更新成功";
                }
                var msgDialog = new Windows.UI.Popups.MessageDialog(text) { Title = "Success" };
                await msgDialog.ShowAsync();

                Frame rootFrame = Window.Current.Content as Frame;
                if (rootFrame == null)
                    return;

                // Navigate back if possible, and if the event has not 
                // already been handled .
                while (rootFrame.CanGoBack)
                {
                    rootFrame.GoBack();
                }

                
            }
        }

        private void cancelClick(object sender, RoutedEventArgs e)
        {
            title.Text = "";
            detail.Text = "";
            datepicker.Date = DateTime.Today;
            image.Source = null;
            create.Content = "Create";
        }

        private void delete_click(object sender, RoutedEventArgs e)
        {
            if (create.Content.ToString().Equals("Create")) return;
            ViewModel.RemoveListItem(ViewModel.selectedItem.id);
            Frame rootFrame = Window.Current.Content as Frame;
            if (rootFrame == null)
                return;

            // Navigate back if possible, and if the event has not 
            // already been handled .
            while (rootFrame.CanGoBack)
            {
                rootFrame.GoBack();
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            //这个e.Parameter是获取传递过来的参数，其实大家应该再次之前判断这个参数是否为null的，我偷懒了
            ViewModel = e.Parameter as ListItemView;
            if (ViewModel.selectedItem == null) return;
            ListItem item = ViewModel.selectedItem;
            title.Text = item.title;
            detail.Text = item.description;
            image.Source = item.imagesource;
            datepicker.Date = item.date;
            create.Content = "Update";
        }
    }
}
