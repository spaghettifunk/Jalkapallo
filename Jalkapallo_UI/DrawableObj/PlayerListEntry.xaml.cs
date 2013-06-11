using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Navigation;

using Jalkapallo_Shared;

namespace Jalkapallo_UI.DrawableObj
{
	public partial class PlayerListEntry : UserControl
	{
		private Player _associatedPlayer;
		public Player AssociatedPlayer
		{
			get { return _associatedPlayer; }
			set { _associatedPlayer = value; }
		}

		public PlayerListEntry()
		{
			InitializeComponent();
		}
	}
}
