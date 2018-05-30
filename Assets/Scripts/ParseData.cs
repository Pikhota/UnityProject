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
            //Load a file.xml from the Resources folder
            TextAsset xmlFile = Resources.Load<TextAsset>(xmlFileName);
            //Getting data in xml format 
            string xmlData = xmlFile.text;
            //create a XmlDocument
            XmlDocument xmlDoc = new XmlDocument();
            //Load data in the XmlDocument
            xmlDoc.Load(new StringReader(xmlData));
            //Set a path to a node from we start to get data (in current case we get all data from our file.xml)
            string path = "/";
            //Get root node
            XmlNode xmlDataNode = xmlDoc.SelectSingleNode(path);
            //Initializes a new instance of the XmlSerializer which can Deserialize of Data type
            XmlSerializer serializer = new XmlSerializer(typeof(Data));
            //deserilize and return data
            return (Data)serializer.Deserialize(new XmlNodeReader(xmlDataNode));
        }
    }
}
