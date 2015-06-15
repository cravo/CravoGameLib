using CravoGameLib.Extensions;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace CravoGameLib.TileMap
{
    public class Layer
    {
        public enum Visibility
        {
            Visible,
            Hidden,
        }

        public string Name { get; private set; }
        public int Width { get; private set; }
        public int Height { get; private set; }
        public float Opacity { get; set; }
        public Visibility Visible { get; set; } 
        public int[,] TileGID;

        public Layer()
        {
            InitDefaults();
        }

        private void InitDefaults()
        {
            Name = "No Name";
            Width = 0;
            Height = 0;
            Opacity = 1;
            Visible = Visibility.Visible;
        }

        public void FromXml(XmlElement element)
        {
            InitDefaults();

            Name = element.TryGetString("name", Name);
            Width = element.TryGetInt("width", Width);
            Height = element.TryGetInt("height", Height);
            Opacity = element.TryGetFloat("opacity", Opacity);

            int vis = element.TryGetInt("visible", 1);
            if ( vis == 1 )
            {
                Visible = Visibility.Visible;
            }
            else
            {
                Visible = Visibility.Hidden;
            }

            foreach (XmlElement dataElement in element.GetElementsByTagName("data"))
            {
                string encoding = dataElement.GetAttribute("encoding");
                if (encoding != "csv")
                {
                    throw new NotImplementedException("Only csv encoding is currently supported");
                }
                else
                {
                    string [] csvdata = dataElement.InnerText.Trim().Split("\n".ToCharArray());
                    TileGID = new int[Width, Height];
                    for (int y = 0; y < Height;++y)
                    {
                        string[] tiles = csvdata[y].Split(",".ToCharArray());
                        for(int x = 0; x < Width; ++x)
                        {
                            TileGID[x, y] = Convert.ToInt32(tiles[x]);
                        }
                    }
                }
            }
        }
    }
}