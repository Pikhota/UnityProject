using System.Xml.Serialization;

namespace Assets.Scripts.Models
{
    [XmlRoot(ElementName = "room")]
    public class Room
    {
        [XmlAttribute(AttributeName = "players")]
        public int Players { get; set; }
        [XmlAttribute(AttributeName = "maxPlayers")]
        public int MaxPlayers { get; set; }
        [XmlAttribute(AttributeName = "price")]
        public int Price { get; set; }
        [XmlText]
        public string Text { get; set; }
    }
}
