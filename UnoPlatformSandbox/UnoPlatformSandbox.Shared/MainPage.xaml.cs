using System;
using System.ComponentModel;
using System.Reflection;
using System.Runtime.CompilerServices;
using Windows.UI.Core;
using Windows.UI.Xaml.Controls;
using System.Threading;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;
using UnoPlatformSandbox.Shared;
#if WINDOWS_UWP
using UnoPlatformSandbox.Annotations;
#endif


// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace UnoPlatformSandbox
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page, INotifyPropertyChanged
    {
        private Timer timer;
        private string lastItem;

        public MainPage()
        {
            InitializeComponent();
            DataContext = this;
            IsBusy = false;
            timer = new Timer(state =>
            {
                Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => IsBusy = !IsBusy);
            }, null, new TimeSpan(0, 0, 5), new TimeSpan(0, 0, 5));
        }

        private string GetPageClassName(string navItemContent)
        {
            switch (navItemContent)
            {
                case "Page 1": return nameof(PageToNav1);
                case "Page 2": return nameof(PageToNav2);
                default: return string.Empty;
            }
        }

        private void NavigationView_OnItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            if (args.IsSettingsInvoked) return;
            var item = args.InvokedItem as string;
            if (item == null || item == lastItem)
                return;
            var clickedView = GetPageClassName(item);
            if (!NavigateToView(clickedView)) return;
            lastItem = item;
        }

        private bool NavigateToView(string clickedView)
        {
            var view = Assembly.GetExecutingAssembly()
                .GetType($"UnoPlatformSandbox.Shared.{clickedView}");

            if (string.IsNullOrWhiteSpace(clickedView) || view == null)
            {
                return false;
            }

            ContentFrame.Navigate(view, null, new EntranceNavigationTransitionInfo());
            return true;
        }

        private void ContentFrame_OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            if (ContentFrame.CanGoBack)
                ContentFrame.GoBack();
        }

        private bool _isBusy;

        public bool IsBusy
        {
            get => _isBusy;
            set
            {
                _isBusy = value;
                OnPropertyChanged();
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

#if WINDOWS_UWP
        [NotifyPropertyChangedInvocator]
#endif
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
