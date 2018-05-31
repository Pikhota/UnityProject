using System.Collections.Generic;
using System.Xml.Serialization;

namespace Assets.Scripts.Models
{
    [XmlRoot(ElementName = "roomlist")]
    public class RoomList
    {
        [XmlElement(ElementName = "game")]
        public List<Game> Games { get; set; }
        [XmlAttribute(AttributeName = "type")]
        public string Type { get; set; }
    }
}
