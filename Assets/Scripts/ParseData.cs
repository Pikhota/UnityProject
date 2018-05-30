using Assets.Scripts.Models;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using UnityEngine;

namespace Assets.Scripts
{
    public static class ParseData
    {
        public static Data ParseXmlFileToObject(string xmlFileName)
        {
            TextAsset xmlFile = Resources.Load<TextAsset>(xmlFileName);
            string xmlData = xmlFile.text;
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(new StringReader(xmlData));
            string path = "/";
            XmlNode xmlDataNode = xmlDoc.SelectSingleNode(path);
            XmlSerializer serializer = new XmlSerializer(typeof(Data));
            return (Data)serializer.Deserialize(new XmlNodeReader(xmlDataNode));
        }
    }
}
