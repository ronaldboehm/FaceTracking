using SmoothTrack;
using System;
using System.Collections.Generic;
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

namespace DemoApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly SmoothTrackListener _listener;
        public MainWindow()
        {
            InitializeComponent();

            _listener = new SmoothTrackListener(4242);
        }

        private HeadTrackingData _lastData;
        private HeadTrackingData _offset;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _listener.Start(data =>
            {
                _lastData = data;

                var effective = data - _offset;

                this.Dispatcher.Invoke(() =>
                {
                    sldX.Value = - effective.X;
                    sldY.Value = + effective.Y;
                    sldZ.Value = - effective.Z;
                    sldYaw.Value   = + effective.Yaw;
                    sldPitch.Value = + effective.Pitch;
                    sldRoll.Value  = - effective.Roll;
                });
            });
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            _listener.Stop();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _offset = _lastData;
        }
    }
}
