using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using CravoGameLib.Extensions;
using Microsoft.Xna.Framework.Graphics;
using System.IO;

namespace CravoGameLib.TileMap
{
    public class Tileset
    {
        public int FirstGID { get; private set; }
        public string Name { get; private set; }
        public int TileWidth { get; private set; }
        public int TileHeight { get; private set; }
        public int Spacing { get; private set; }
        public int Margin { get; private set; }
        public Point TileOffset { get; private set; }
        public Texture2D Texture { get; private set; }

        public Tileset()
        {
            InitDefaults();   
        }
        
        private void InitDefaults()
        {
            FirstGID = 0;
            Name = "No Name";
            TileWidth = 0;
            TileHeight = 0;
            Spacing = 0;
            Margin = 0;
            TileOffset = new Point(0, 0);
        }

        public void FromXml(XmlElement element, string filePath, GraphicsDevice device)
        {
            InitDefaults();

            Name = element.TryGetString("name", Name);
            TileWidth = element.TryGetInt("tilewidth", TileWidth);
            TileHeight = element.TryGetInt("tileheight", TileHeight);
            Spacing = element.TryGetInt("spacing", Spacing);
            Margin = element.TryGetInt("margin", Margin);

            foreach(XmlElement tileOffsetElement in element.GetElementsByTagName("tileoffset"))
            {
                int x = tileOffsetElement.TryGetInt("x", TileOffset.X);
                int y = tileOffsetElement.TryGetInt("y", TileOffset.Y);

                TileOffset = new Point(x, y);
            }

            foreach(XmlElement imageElement in element.GetElementsByTagName("image"))
            {
                string filename = imageElement.GetAttribute("source");

                using (FileStream stream = new FileStream(filePath + "\\" + filename, FileMode.Open))
                {
                    Texture = Texture2D.FromStream(device, stream);
                }
            }
        }
    }
}
