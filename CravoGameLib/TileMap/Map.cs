using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using CravoGameLib.Extensions;
using System.IO;
using Microsoft.Xna.Framework.Graphics;

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
        private List<Tileset> Tilesets { get; set; }
        private List<Layer> Layers { get; set; }
        private SpriteBatch SpriteBatch;

        public Map()
        {
            InitDefaults();
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
            Tilesets = new List<Tileset>();
            Layers = new List<Layer>();
        }

        public void Load(string file, GraphicsDevice device)
        {
            InitDefaults();
            SpriteBatch = new SpriteBatch(device);

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

                foreach (XmlElement tilesetElement in mapElement.GetElementsByTagName("tileset"))
                {
                    AddTileset(tilesetElement, Path.GetDirectoryName(file), device);
                }

                foreach(XmlElement layerElement in mapElement.GetElementsByTagName("layer"))
                {
                    Layer layer = new Layer();
                    layer.FromXml(layerElement);

                    Layers.Add(layer);
                }
            }
        }

        private void AddTileset(XmlElement element, string filePath, GraphicsDevice device)
        {
            if ( element.HasAttribute("source") )
            {
                string source = element.GetAttribute("source");
                XmlDocument doc = new XmlDocument();
                doc.Load(filePath + "\\" + source);
                foreach(XmlElement tilesetElement in doc.GetElementsByTagName("tileset"))
                {
                    element = tilesetElement;
                }
            }

            Tileset tileset = new Tileset();
            tileset.FromXml(element, filePath, device);

            Tilesets.Add(tileset);
        }

        private Tileset GetTilesetForTileGID(int gid)
        {
            int tilesetIndex = 0;
            while (tilesetIndex < Tilesets.Count && Tilesets[tilesetIndex].FirstGID < gid)
            {
                tilesetIndex++;
            }
            tilesetIndex--;

            return Tilesets[tilesetIndex];
        }

        public void Draw()
        {
            SpriteBatch.Begin();
            foreach (Layer layer in Layers)
            {
                if (layer.Visible == Layer.Visibility.Visible)
                {
                    for (int y = 0; y < layer.Height; ++y)
                    {
                        for (int x = 0; x < layer.Width; ++x)
                        {
                            int tileGID = layer.TileGID[x, y];

                            //todo: precompute tile rects
                            Tileset tileset = GetTilesetForTileGID(tileGID);
                            int tileLocalID = tileGID - tileset.FirstGID;
                            int currentID = 0;
                            int tx = tileset.Margin;
                            int ty = tileset.Margin;
                            while (currentID < tileLocalID - 1)
                            {
                                tx += TileWidth + tileset.Spacing;

                                if (tx > tileset.Texture.Width - TileWidth)
                                {
                                    ty += TileHeight + tileset.Spacing;
                                    tx = tileset.Margin;
                                }

                                ++currentID;
                            }

                            Rectangle tileRect = new Rectangle(tx, ty, TileWidth, TileHeight);

                            SpriteBatch.Draw(tileset.Texture, new Rectangle(x * TileWidth, y * TileHeight, TileWidth, TileHeight), tileRect, new Color(1, 1, 1, layer.Opacity));
                        }
                    }
                }
            }
            SpriteBatch.End();
        }
    }
}
