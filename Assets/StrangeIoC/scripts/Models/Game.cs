﻿using System.Collections.Generic;
using System.Xml.Serialization;

namespace Assets.Scripts.Models
{
    [XmlRoot(ElementName = "game")]
    public class Game
    {
        [XmlElement(ElementName = "supergame")]
        public List<SuperGame> Supergame { get; set; }
        [XmlElement(ElementName = "room")]
        public List<Room> Room { get; set; }
        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }
        [XmlAttribute(AttributeName = "players")]
        public int Players { get; set; }
        [XmlAttribute(AttributeName = "gameId")]
        public int GameId { get; set; }
    }
}