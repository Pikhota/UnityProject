using System.Xml.Serialization;

namespace Assets.Scripts.Models
{ 
    [XmlRoot(ElementName="supergame")]
    public class SuperGame
    {
        [XmlAttribute(AttributeName="price")]
		public int Price { get; set; }
		[XmlAttribute(AttributeName="max")]
		public int MaxPlayers { get; set; }
		[XmlAttribute(AttributeName="current")]
		public int AmountPlayers { get; set; }
    }
}
