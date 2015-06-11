using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using CravoGameLib.Extensions;

namespace CravoGameLib.TileMap
{
    public class Map
    {
        public enum OrientationType
        {
            Orthogonal,
            Isometric,
            Staggered,
        }

        public enum RenderOrderType
        {
            RightDown,
            RightUp,
            LeftDown,
            LeftUp,
        }

        private string Version { get; set; }
        public OrientationType Orientation { get; private set; }
        public int WidthInTiles { get; private set; }
        public int HeightInTiles { get; private set; }
        public int TileWidth { get; private set; }
        public int TileHeight { get; private set; }
        public Color BackgroundColour { get; set; }
        private int NextObjectID { get; set; }

        public Map()
        {
            InitDefaults();
        }

        public Map(string file)
        {
            InitDefaults();

            Load(file);
        }

        private void InitDefaults()
        {
            Version = "1.0";
            Orientation = OrientationType.Orthogonal;
            WidthInTiles = 1;
            HeightInTiles = 1;
            TileWidth = 1;
            TileHeight = 1;
            BackgroundColour = Color.Black;
            NextObjectID = 1;
        }

        public void Load(string file)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(file);

            foreach (XmlElement mapElement in doc.GetElementsByTagName("map"))
            {
                Version = mapElement.TryGetString("version", Version);

                // Parse orientation from string to enum, ensuring it's Uppercase first letter, lowercase the rest
                string orientationString = mapElement.TryGetString("orientation", Enum.GetName(typeof(OrientationType), Orientation));
                orientationString.ToLower();
                orientationString = char.ToUpper(orientationString[0]) + orientationString.Substring(1);
                Orientation = (OrientationType)Enum.Parse(typeof(OrientationType), orientationString);

                WidthInTiles = mapElement.TryGetInt("width", WidthInTiles);
                HeightInTiles = mapElement.TryGetInt("height", HeightInTiles);
                TileWidth = mapElement.TryGetInt("tilewidth", TileWidth);
                TileHeight = mapElement.TryGetInt("tileheight", TileHeight);

                //todo: Background Colour, leave unsupported for now.
                //todo: RenderOrder, leave unsupported for now.
            }
        }
    }
}
