using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Lab1.ViewModels;
using System.Diagnostics;
using Windows.Storage.Pickers;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Media.Imaging;
using Lab1.Models;
using Windows.UI.ViewManagement;

// https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x804 上介绍了“空白页”项模板

namespace Lab1
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        ListItemView ViewModel { get; set; }
        private List<Grid> m_renderedGrids = new List<Grid>();
        private static string _state = "Min", fromstate = "Min";
        public MainPage()
        {
            this.InitializeComponent();
            this.DataContextChanged += (s, e) => { ViewModel = DataContext as ListItemView; };
            listview.ItemClick += goToNewPage;
            this.Loaded += ScreenSize_Loaded;
            this.SizeChanged += (s, e) =>
            {
                if (_state.Equals("Min"))
                {
                    if (e.NewSize.Width > 800)
                    {
                        fromstate = "Min800";
                        _state = "Min800";
                        newpage.Visibility = Visibility.Visible;
                        AddAppBarButton.Click -= add_click;

                    }
                    else if (e.NewSize.Width > 600)
                    {
                        fromstate = "Min600";
                        _state = "Min600";
                    }
                    else
                    {
                        fromstate = "Min000";
                        _state = "Min000";
                    }
                }
                var state = "Min000";
                if (e.NewSize.Width > 800)
                {
                    fromstate = _state;
                    _state = "Min800";
                    state = "Min800";
                }
                else if (e.NewSize.Width > 600)
                {
                    fromstate = _state;
                    _state = "Min600";
                }
                else
                {
                    fromstate = _state;
                    _state = "Min000";
                }
                if (fromstate.Equals(_state)) return;
                if (_state.Equals("Min800"))
                {
                    newpage.Visibility = Visibility.Visible;
                    AddAppBarButton.Click -= add_click;
                    listview.ItemClick -= goToNewPage;
                }
                else if (_state.Equals("Min600"))
                {
                    if (fromstate.Equals("Min800"))
                    {
                        listview.ItemClick += goToNewPage;
                        AddAppBarButton.Click += add_click;
                        newpage.Visibility = Visibility.Collapsed;
                    }
                    else
                    {
                        foreach (var grid in m_renderedGrids)
                        {
                            Image thisimage = (Image)GetChildren(grid).First(x => x.Name == "thisimage");
                            thisimage.Visibility = Visibility.Visible;
                        }
                    }
                } else
                {
                    foreach (var grid in m_renderedGrids)
                    {
                        Image thisimage = (Image)GetChildren(grid).First(x => x.Name == "thisimage");
                        thisimage.Visibility = Visibility.Collapsed;
                    }
                }
                VisualStateManager.GoToState(this, state, true);
            };

            
        }

        private void ScreenSize_Loaded(object sender, RoutedEventArgs e)
        {
            // Window.Current.Bounds - 当前窗口的大小（单位是有效像素，没有特别说明就都是有效像素）
            //     注：窗口大小不包括标题栏，标题栏属于系统级 UI

            ApplicationView applicationView = ApplicationView.GetForCurrentView();

            // SetPreferredMinSize(Size minSize) - 指定窗口允许的最小尺寸（最小：192×48，最大：500×500）
            applicationView.SetPreferredMinSize(new Size(200, 200));

            // PreferredLaunchViewSize - 窗口启动时的初始尺寸
            // 若要使 PreferredLaunchViewSize 设置有效，需要将 ApplicationView.PreferredLaunchWindowingMode 设置为 ApplicationViewWindowingMode.PreferredLaunchViewSize
            ApplicationView.PreferredLaunchViewSize = new Size(600, 650);
            ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.PreferredLaunchViewSize;

            /*
             * ApplicationView.PreferredLaunchWindowingMode - 窗口的启动模式
             *     Auto - 系统自动调整
             *     PreferredLaunchViewSize - 由 ApplicationView.PreferredLaunchViewSize 决定
             *     FullScreen - 全屏启动
             */
        }

        private List<FrameworkElement> GetChildren(DependencyObject parent)
        {
            List<FrameworkElement> controls = new List<FrameworkElement>();

            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); ++i)
            {
                var child = VisualTreeHelper.GetChild(parent, i);
                if (child is FrameworkElement)
                {
                    controls.Add(child as FrameworkElement);
                }
                controls.AddRange(GetChildren(child));
            }

            return controls;
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            // Add the Grids inside DataTemplate into a List. 
            m_renderedGrids.Add(sender as Grid);
            Image thisimage = (Image)GetChildren(sender as Grid).First(x => x.Name == "thisimage");
            if (_state.Equals("Min000"))
                thisimage.Visibility = Visibility.Collapsed;
            else
                thisimage.Visibility = Visibility.Visible;
        }

        private async void imageClickAsync(object sender, RoutedEventArgs e)
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
                    cancelClick(sender,e);
                }
                else
                {
                    ListItem item = listview.SelectedItem as ListItem;
                    ViewModel.UpdateListItem(item.id,_title, _detail, _date, image.Source);
                    text = "更新成功";
                }
                var msgDialog = new Windows.UI.Popups.MessageDialog(text) { Title = "Success" };
                await msgDialog.ShowAsync();

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

        private void add_click(object sender, RoutedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;
            rootFrame.Navigate(typeof(NewPage),ViewModel);
        }

        private void ListItemClick(object sender, ItemClickEventArgs e)
        {
            ListItem item = e.ClickedItem as ListItem;

            ViewModel.selectedItem = item;
            image.Source = item.imagesource;
            title.Text = item.title;
            detail.Text = item.description;
            datepicker.Date = item.date;
            create.Content = "Update";
        }

        private void goToNewPage(object sender, ItemClickEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;
            rootFrame.Navigate(typeof(NewPage),ViewModel);
        }

        private void selectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ViewModel.selectedItem = listview.SelectedItem as ListItem;
        }

        private void delete_click(object sender, RoutedEventArgs e)
        {
            if (create.Content.ToString().Equals("Create") || ViewModel.selectedItem == null) return;
            ViewModel.RemoveListItem(ViewModel.selectedItem.id);
            cancelClick(sender,e);
                
        }

    }
}
