using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Jalkapallo_Shared
{
    [DataContract]
    public class News
    {
        #region Properties

        [DataMember]
        public string TitleNews { get; set; }

        [DataMember]
        public string TextNews { get; set; }

        [DataMember]
        public string BodyNews { get; set; }

        #endregion

        public News() { }
    }
}
