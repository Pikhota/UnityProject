using System;
using System.Collections.Generic;

namespace Assets.Scripts.Models
{
    [Serializable]
    public class Config
    {
        public string url;
        public List<int> games = new List<int>();
    }
}