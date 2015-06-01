using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CravoGameLib.Data.Processor
{
    /// <summary>
    /// Data processor for 2D textures
    /// </summary>
    class Texture2DProcessor : DataProcessor<Texture2D>
    {
        private string[] SupportedExtensions =
        {
            "png",
            "jpg",
            "bmp"
        };

        public override string GetSupportedFilename(string filename)
        {
            return GetSupportedFilename(filename, SupportedExtensions);
        }

        protected internal override Texture2D ProcessData(DataReader reader, Texture2D instance)
        {
            Texture2D texture = Texture2D.FromStream(reader.GraphicsDevice, reader.Stream);

            return texture;
        }
    }
}
