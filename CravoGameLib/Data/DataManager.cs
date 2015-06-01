using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CravoGameLib.Data
{
    public class DataManager
    {
        Dictionary<string, object> CachedData;
        Dictionary<Type, DataProcessor> DataProcessors;
        string RootPath;
        GraphicsDevice GraphicsDevice;

        public DataManager(string rootPath, GraphicsDevice device)
        {
            this.GraphicsDevice = device;

            RootPath = rootPath;
            if ( !RootPath.EndsWith("\\") )
            {
                RootPath += "\\";
            }


            CachedData = new Dictionary<string, object>();

            InitialiseDataProcessors();
        }

        public T Load<T>(string filename)
        {
            object result = null;
            filename = RootPath + filename;

            bool cached = CachedData.TryGetValue(filename, out result);

            if ( !cached )
            {
                DataProcessor processor = GetProcessor(typeof(T));
                filename = processor.GetSupportedFilename(filename);

                using (StreamReader reader = new StreamReader(filename))
                {
                    result = processor.ProcessData(new DataReader(this.GraphicsDevice, reader.BaseStream));
                }
            }

            return (T)result;
        }

        private DataProcessor GetProcessor(Type type)
        {
            DataProcessor result = null;
            if ( !DataProcessors.TryGetValue(type, out result) )
            {
                throw new NotImplementedException("No data processor for objects of type " + type + " could be found.");
            }

            return result;
        }

        private void InitialiseDataProcessors()
        {
            //todo: Factory create the list by finding all the DataProcessor types in the assembly
            // For now I'll just hard-code them here

            DataProcessors = new Dictionary<Type, DataProcessor>();

            DataProcessors.Add(typeof(Texture2D), new Processor.Texture2DProcessor());
        }
    }
}
