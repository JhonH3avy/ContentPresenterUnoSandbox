using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;
using Windows.UI.Core;
using Windows.UI.Xaml.Controls;
#if WINDOWS_UWP
using UnoPlatformSandbox.Annotations;
#endif

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace UnoPlatformSandbox.Shared
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PageToNav2 : Page, INotifyPropertyChanged
    {

        private Timer timer;

        public PageToNav2()
        {
            this.InitializeComponent();
            DataContext = this;
            IsBusy = false;
            timer = new Timer(state =>
            {
                Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => IsBusy = !IsBusy);
            }, null, new TimeSpan(0, 0, 5), new TimeSpan(0, 0, 5));
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
