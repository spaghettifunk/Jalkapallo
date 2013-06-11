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
using Jalkapallo_Shared;

namespace Jalkapallo_UI.DrawableObj
{
    public partial class MatchListEntry : UserControl
    {
        private Match _associatedMatch;
        public Match AssociatedMatch
        {
            get { return _associatedMatch; }
            set { _associatedMatch = value; }
        }

        public MatchListEntry()
        {
            InitializeComponent();
        }
    }
}
