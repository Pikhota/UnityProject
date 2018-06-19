using System;
using System.Collections.Generic;

namespace Assets.Models
{
    [Serializable]
    public class Config
    {
        public string url;
        public List<int> games = new List<int>();
    }
}