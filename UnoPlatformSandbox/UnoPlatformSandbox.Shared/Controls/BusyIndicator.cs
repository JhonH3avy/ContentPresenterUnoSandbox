using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Microsoft.Toolkit.Uwp.UI.Extensions;

namespace UnoPlatformSandbox.Shared.Controls
{
    public class BusyIndicator : ContentControl
    {
        DoubleAnimation rotationAnimation;
        Storyboard storyboard;

        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            CraeteStoryboard();
            if (IsBusy)
            {
                StartBusyStoryboard();
            }
            else
            {
                StopBusyStoryboard();
            }
        }

        private void CraeteStoryboard()
        {
            var rootGrid = GetTemplateChild("root") as Grid;
            if (rootGrid == null) return;
            rootGrid.DataContext = this;
            var visualGrid = rootGrid.FindChildByName("visual") as Grid;
            visualGrid.RenderTransform = new RotateTransform();
            storyboard = new Storyboard();
            rotationAnimation = new DoubleAnimation();
            rotationAnimation.From = 0;
            rotationAnimation.To = 360;
            rotationAnimation.Duration = new Duration(TimeSpan.FromSeconds(5));
            rotationAnimation.RepeatBehavior = RepeatBehavior.Forever;
            Storyboard.SetTarget(rotationAnimation, visualGrid);
            Storyboard.SetTargetProperty(rotationAnimation, "(Grid.RenderTransform).(RotateTransform.Angle)");
            storyboard.Children.Add(rotationAnimation);
            //Storyboard.SetTargetProperty(rotationAnimation, "Angle");
            //Storyboard.SetTargetName(rotationAnimation, "angleTransform");
        }

        public void StartBusyStoryboard()
        {
            //Console.WriteLine("Starting BusyIndicator animation");
            //var rootGrid = GetTemplateChild("root") as Grid;
            //if (rootGrid == null)
            //{
            //    Console.WriteLine("Cannot find root grid");
            //    return;
            //}
            //Console.WriteLine("Found root grid");
            //rootGrid.DataContext = this;
            //var std = rootGrid.Resources["anim"] as Storyboard;
            //Console.WriteLine($"There are {rootGrid.Resources.Count} resources in rootGrid");
            //if (std == null)
            //{
            //    Console.WriteLine("Cannot find storyboard");
            //    return;
            //}
            //Console.WriteLine("Found storyboard");
            //std.Begin();
            storyboard.Begin();
        }

        public void StopBusyStoryboard()
        {
            //Console.WriteLine("Stopping BusyIndicator animation");
            //var rootGrid = GetTemplateChild("root") as Grid;
            //if (rootGrid == null)
            //{
            //    Console.WriteLine("Cannot find root grid");
            //    return;
            //}
            //Console.WriteLine("Found root grid");
            //rootGrid.DataContext = this;
            //var std = rootGrid.Resources["anim"] as Storyboard;
            //Console.WriteLine($"There are {rootGrid.Resources.Count} resources in rootGrid");
            //if (std == null)
            //{
            //    Console.WriteLine("Cannot find storyboard");
            //    return;
            //}
            //Console.WriteLine("Found storyboard");
            //std.Stop();
            storyboard.Stop();
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

        public static DependencyProperty IsBusyProperty = DependencyProperty.Register("IsBusy", typeof(bool), typeof(BusyIndicator), new PropertyMetadata(true, IsBusyPropertyChanged));

        private static void IsBusyPropertyChanged(DependencyObject dependencyobject, DependencyPropertyChangedEventArgs args)
        {
            var busyIndicator = dependencyobject as BusyIndicator;
            if ((bool)args.NewValue)
            {
                busyIndicator.StartBusyStoryboard();
            }
            else
            {
                busyIndicator.StopBusyStoryboard();
            }
        }
    }
}
