using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace XaccountBook.Droid
{
    public class LocalNotificationAndroid : Service, ILocalNotification
    {
        public override IBinder OnBind(Intent intent)
        {
            return null;
        }

        public void PushNotification(string title, string context)
        {
            Notification.Builder builder = new Notification.Builder(this)
                .SetContentTitle(title)
                .SetContentText(context)
                .SetSmallIcon(Resource.Drawable.icon);

            Notification notification = builder.Build();

            NotificationManager notificationManager = GetSystemService(Context.NotificationService) as NotificationManager;

            const int notificationId = 0;
            notificationManager.Notify(notificationId, notification);
        }
    }
}