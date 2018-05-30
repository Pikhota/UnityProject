using System.Xml.Serialization;

namespace Assets.Scripts.Models
{
    [XmlRoot(ElementName = "room")]
    public class Room
    {
        [XmlAttribute(AttributeName = "players")]
        public string Players { get; set; }
        [XmlAttribute(AttributeName = "maxPlayers")]
        public string MaxPlayers { get; set; }
        [XmlAttribute(AttributeName = "price")]
        public string Price { get; set; }
        [XmlText]
        public string Text { get; set; }
    }
}
