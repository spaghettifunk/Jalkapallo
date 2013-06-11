using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

using WebRoleWCFConnector.WebRoleServiceReference;

namespace WebRoleWCFConnector
{
	public class WBConnector
	{
		public static WebRoleServiceClient WebRoleServiceClientObject { get; private set; }
		
		public WBConnector()
		{
            WebRoleServiceClientObject = new WebRoleServiceClient("BasicHttpBinding_IWebRoleService");
		}
	}
}
