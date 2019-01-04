using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows;
using Notifications.Wpf;
using OSVersionHelper;
using Windows.UI.Notifications;

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ClickMe_Click(object sender, RoutedEventArgs e)
        {
            if(WindowsVersionHelper.HasPackageIdentity)
            {
                ShowToastUsingWindowsRuntime();
            }
            else
            {
                ShowToastDownlevel();
            }
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void ShowToastUsingWindowsRuntime()
        {
            // Get a toast XML template
            var toastXml = ToastNotificationManager.GetTemplateContent(ToastTemplateType.ToastImageAndText03);

            // Fill in the text elements
            var stringElements = toastXml.GetElementsByTagName("text");
            for (var i = 0; i < stringElements.Length; i++)
            {
                stringElements[i].AppendChild(toastXml.CreateTextNode("Line " + i));
            }

            // Specify the absolute path to an image
            var imagePath = "file:///" +  Path.Combine(Path.GetDirectoryName(typeof(MainWindow).Assembly.Location), "toastImageAndText.png");
            var imageElements = toastXml.GetElementsByTagName("image");
            imageElements[0].Attributes.GetNamedItem("src").NodeValue = imagePath;
            
            var toast = new ToastNotification(toastXml);

            ToastNotificationManager.CreateToastNotifier().Show(toast);
        }

        private void ShowToastDownlevel()
        {
            var notificationManager = new NotificationManager();
            notificationManager.Show(new NotificationContent
            {
                Title = "The Title",
                Message = "The message",
                Type = NotificationType.Information
            });
        }
    }
}
