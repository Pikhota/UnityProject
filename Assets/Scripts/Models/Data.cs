using System.Collections.Generic;
using System.Xml.Serialization;

namespace Assets.Scripts.Models
{
    [XmlRoot(ElementName = "data")]
    public class Data
    {
        [XmlElement(ElementName = "roomlist")]
        public List<RoomList> Roomlist { get; set; }
    }
}
