using System.Xml.Serialization;

namespace Assets.Scripts.Models
{
	public class GameState
	{
		public string SelectedRoom;
	}
	
	[XmlRoot(ElementName = "data")]
	public class Data
	{
		[XmlArray("settings")]
		[XmlArrayItem("param")]
		public SettingsParam[] Settings;
		
		[XmlArray("roomlist")]
		[XmlArrayItem("game")]
		public Game[] Games;
	}

	[XmlType("param")]
	public class SettingsParam
	{
		[XmlAttribute(AttributeName = "name")]
		public string Name;
		
		[XmlText]
		public string Value;
	}
	
	[XmlType("game")]
	public class Game
	{
		[XmlAttribute(AttributeName = "name")]
		public string Name;
		
		[XmlAttribute(AttributeName = "players")]
		public int Players;
		
		[XmlAttribute(AttributeName = "gameId")]
		public int GameId;

		[XmlElement("supergame", typeof(SuperGame))]
		[XmlElement("room", typeof(Room))]
		public GameItem[] GameItems;
	}

	
	[XmlInclude(typeof(SuperGame)), 
	 XmlInclude(typeof(Room))]
	public abstract class GameItem
	{
		
	}

	[XmlType("supergame")]
	public class SuperGame : GameItem
	{
		[XmlAttribute(AttributeName = "price")]
		public int Price;
		
		[XmlAttribute(AttributeName = "max")]
		public int Max;
		
		[XmlAttribute(AttributeName = "current")]
		public int Current;
	}

	[XmlType("room")]
	public class Room : GameItem
	{		
		[XmlAttribute(AttributeName = "players")]
		public int Players;
		
		[XmlAttribute(AttributeName = "max_players")]
		public int MaxPlayers;
		
		[XmlAttribute(AttributeName = "price")]
		public int Price;
		
		[XmlText]
		public string Name;
	}
}