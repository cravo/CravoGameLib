using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CravoGameLib.Data
{
    /// <summary>
    /// Data to read is passed to the DataProcessor via this class.
    /// It encapsulates the data stream plus any other information the processors
    /// may need in order to process their data.
    /// </summary>
    public class DataReader
    {
        public GraphicsDevice GraphicsDevice { get; private set; }
        public Stream Stream { get; private set; }

        public DataReader(GraphicsDevice device, Stream stream)
        {
            this.GraphicsDevice = device;
            this.Stream = stream;
        }
    }
}
