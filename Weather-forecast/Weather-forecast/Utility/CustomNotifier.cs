using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ToastNotifications;
using ToastNotifications.Lifetime;
using ToastNotifications.Messages;
using ToastNotifications.Position;

namespace Weather_forecast.Utility
{
    public class CustomNotifier
    {
        public static int x { get; set; }
        public static int y { get; set; }

        public CustomNotifier(int xOffSet, int yOffSet)
        {
            x = xOffSet;
            y = yOffSet;
        }

        public Notifier notifier = new Notifier(cfg =>
        {    
            cfg.PositionProvider = new WindowPositionProvider(
                parentWindow: Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive),
                corner: Corner.TopRight,
                offsetX: x,
                offsetY: y);

            cfg.LifetimeSupervisor = new TimeAndCountBasedLifetimeSupervisor(
                notificationLifetime: TimeSpan.FromSeconds(3),
                maximumNotificationCount: MaximumNotificationCount.FromCount(2));

            cfg.Dispatcher = Application.Current.Dispatcher;
        });
    }
}
