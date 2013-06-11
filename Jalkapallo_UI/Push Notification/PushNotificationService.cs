using Microsoft.Phone.Notification;
using System.Text;
using System;
using System.Windows.Threading;
using System.Windows;
using Microsoft.Phone.Info;

using Jalkapallo_UI;
using System.Diagnostics;

namespace Jalkapallo_Shared
{
    public class PushNotificationService
    {
		private string phoneId = null;

        public PushNotificationService() 
        {
            App.ServiceClient.SetPushNotificationChannelCompleted += new EventHandler<System.ComponentModel.AsyncCompletedEventArgs>(ServiceClient_SetPushNotificationChannelCompleted);
        }

        public void SetPushNotification()
        {
			byte[] deviceId = (byte[])DeviceExtendedProperties.GetValue("DeviceUniqueId");
			phoneId = Convert.ToBase64String(deviceId);

            /// Holds the push channel that is created or found.
            HttpNotificationChannel pushChannel;

            // The name of our push channel.
            string channelName = "JalkapalloPushNotificationChannel";

            // Try to find the push channel.
            pushChannel = HttpNotificationChannel.Find(channelName);

            // If the channel was not found, then create a new connection to the push service.
            if (pushChannel == null)
            {
                pushChannel = new HttpNotificationChannel(channelName);

                // Register for all the events before attempting to open the channel.
                pushChannel.ChannelUriUpdated += new EventHandler<NotificationChannelUriEventArgs>(PushChannel_ChannelUriUpdated);
                pushChannel.ErrorOccurred += new EventHandler<NotificationChannelErrorEventArgs>(PushChannel_ErrorOccurred);

                // Register for this notification only if you need to receive the notifications while your application is running.
                pushChannel.ShellToastNotificationReceived += new EventHandler<NotificationEventArgs>(PushChannel_ShellToastNotificationReceived);
                pushChannel.Open();

                // Bind this new channel for toast events.
                pushChannel.BindToShellToast();
            }
            else
            {
                // The channel was already open, so just register for all the events.
                pushChannel.ChannelUriUpdated += new EventHandler<NotificationChannelUriEventArgs>(PushChannel_ChannelUriUpdated);
                pushChannel.ErrorOccurred += new EventHandler<NotificationChannelErrorEventArgs>(PushChannel_ErrorOccurred);

                // Register for this notification only if you need to receive the notifications while your application is running.
                pushChannel.ShellToastNotificationReceived += new EventHandler<NotificationEventArgs>(PushChannel_ShellToastNotificationReceived);

                // Normally, the URI would be passed back to your web service at this point.
                App.ServiceClient.SetPushNotificationChannelAsync(phoneId, pushChannel.ChannelUri.ToString());
            }
        }

        void PushChannel_ChannelUriUpdated(object sender, NotificationChannelUriEventArgs e)
        {
			App.ServiceClient.SetPushNotificationChannelAsync(phoneId, e.ChannelUri.ToString());
        }

        void PushChannel_ErrorOccurred(object sender, NotificationChannelErrorEventArgs e)
        {
        }

        void ServiceClient_SetPushNotificationChannelCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            Debug.WriteLine("**** Set Push Completed - ", e.UserState + " ******");
        }

        // Stampa la Push notification che è arrivata (crea sostanzialmente la piccola barra in alto!)
        void PushChannel_ShellToastNotificationReceived(object sender, NotificationEventArgs e)
        {
            StringBuilder message = new StringBuilder();
            string relativeUri = string.Empty;

            message.AppendFormat("Received Toast {0}:\n", DateTime.Now.ToShortTimeString());

            // Parse out the information that was part of the message.
            foreach (string key in e.Collection.Keys)
            {
                message.AppendFormat("{0}: {1}\n", key, e.Collection[key]);

                if (string.Compare(
                    key,
                    "wp:Param",
                    System.Globalization.CultureInfo.InvariantCulture,
                    System.Globalization.CompareOptions.IgnoreCase) == 0)
                {
                    relativeUri = e.Collection[key];
                }
            }
        }
    }
}

