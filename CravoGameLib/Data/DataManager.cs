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
    /// The DataManager class allows you to load data for various different types.
    /// 
    /// It serves as a replacement for the MonoGame content manager since their content
    /// pipeline only supports 64-bit and I wanted something that would work on 32-bit
    /// systems too.
    /// 
    /// It maintains a cache of loaded objects, so if you request an object more than once
    /// it will be returned instantly from the cache rather than re-loading it.
    /// 
    /// </summary>
    public class DataManager
    {
        Dictionary<string, object> CachedData;
        Dictionary<Type, DataProcessor> DataProcessors;
        string RootPath;
        GraphicsDevice GraphicsDevice;

        /// <summary>
        /// Create a new DataManager for loading data.  It's perfectly fine to have
        /// multiple data managers if needed
        /// </summary>
        /// <param name="rootPath">The path below which all your data lives.</param>
        /// <param name="device">The MonoGame GraphicsDevice (since it may be required by certain content processors)</param>
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

        /// <summary>
        /// Loads an object from data
        /// </summary>
        /// <typeparam name="T">The type of the object.  Throws an exception if no processor is available for that type</typeparam>
        /// <param name="filename">The filename of the file to load, relative to the root path passed to the constructor.
        /// Do not specify an extension - the relevant data processor will sort that out for you.</param>
        /// <returns>The loded object</returns>
        public T Load<T>(string filename)
        {
            object result = null;
            filename = RootPath + filename;

            // First check the cache and return from there if it exists
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

        /// <summary>
        /// Get the data processor for the given type
        /// </summary>
        /// <param name="type">The type a processor is required for.</param>
        /// <returns>The relevant data processor.  Throws an exception if one couldn't be found.</returns>
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

            AddDataProcessor(typeof(Texture2D), new Processor.Texture2DProcessor());
        }

        /// <summary>
        /// Add a new data processor
        /// </summary>
        /// <param name="type">The type of data your processor handles.  Will throw exception if this type of processor exists already</param>
        /// <param name="processor">The processor.  It'll be passed data of your type when encountered.</param>
        public void AddDataProcessor(Type type, DataProcessor processor)
        {
            DataProcessors.Add(type, processor);
        }
    }
}
