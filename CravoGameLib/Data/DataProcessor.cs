using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CravoGameLib.Data
{
    /// <summary>
    /// DataProcessor base class.  In general you should derive from DataProcessor<T> not this class
    /// </summary>
    public abstract class DataProcessor
    {
        public Type DataType { get; private set; }

        protected DataProcessor(Type type)
        {
            DataType = type;
        }

        protected internal abstract object ProcessData(DataReader reader);

        public abstract string GetSupportedFilename(string filename);

        protected internal string GetSupportedFilename(string filename, string [] supportedExtensions)
        {
            string result = filename;

            foreach ( string extension in supportedExtensions )
            {
                string fullPath = filename + "." + extension;
                if (File.Exists(fullPath))
                {
                    result = fullPath;
                }
            }

            return result;
        }
    }

    /// <summary>
    /// DataProcessor base class templated on type.  Derive all data processors from this.
    /// </summary>
    /// <typeparam name="T">The type the data processor creates</typeparam>
    public abstract class DataProcessor<T> : DataProcessor
    {
        protected DataProcessor() : base(typeof(T))
        {

        }

        protected internal override object ProcessData(DataReader reader)
        {
            return this.ProcessData(reader, default(T));
        }

        protected internal abstract T ProcessData(DataReader reader, T instance);
    }
}
