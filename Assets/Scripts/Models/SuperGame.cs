using System.Xml.Serialization;

namespace Assets.Scripts.Models
{ 
    [XmlRoot(ElementName="supergame")]
    public class SuperGame
    {
        [XmlAttribute(AttributeName="price")]
		public string Price { get; set; }
		[XmlAttribute(AttributeName="max")]
		public string MaxPlayers { get; set; }
		[XmlAttribute(AttributeName="current")]
		public string AmountPlayers { get; set; }
    }
}
