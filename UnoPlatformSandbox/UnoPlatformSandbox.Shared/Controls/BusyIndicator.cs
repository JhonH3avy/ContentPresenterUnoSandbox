using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;

namespace UnoPlatformSandbox.Shared.Controls
{
    public class BusyIndicator : ContentControl
    {
        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            //if (!(GetTemplateChild("root") is Grid rootGrid)) return;
            //rootGrid.DataContext = this;
            //var std = rootGrid.Children[0] as ContentPresenter;
            //std.Content = Content;
        }

        public BusyIndicator()
        {
            DefaultStyleKey = typeof(BusyIndicator);
        }

        public bool IsBusy
        {
            get => (bool)GetValue(IsBusyProperty);
            set
            {
                if (IsBusyProperty != null) SetValue(IsBusyProperty, value);
            }
        }

        public static DependencyProperty IsBusyProperty = DependencyProperty.Register("IsBusy", typeof(bool), typeof(BusyIndicator), new PropertyMetadata(true, null));

    }
}
