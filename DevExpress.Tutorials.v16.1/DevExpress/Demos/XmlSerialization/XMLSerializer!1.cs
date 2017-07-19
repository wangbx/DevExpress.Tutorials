namespace DevExpress.Demos.XmlSerialization
{
    using System;
    using System.IO;
    using System.IO.IsolatedStorage;
    using System.Reflection;
    using System.Runtime.Serialization.Formatters.Binary;
    using System.Xml.Serialization;

    public static class XMLSerializer<T> where T: class
    {
        private static FileStream CreateFileStream(IsolatedStorageFile isolatedStorageFolder, string path)
        {
            if (isolatedStorageFolder != null)
            {
                return new IsolatedStorageFileStream(path, FileMode.OpenOrCreate, isolatedStorageFolder);
            }
            return new FileStream(path, FileMode.OpenOrCreate);
        }

        private static TextReader CreateTextReader(IsolatedStorageFile isolatedStorageFolder, string path)
        {
            if (isolatedStorageFolder != null)
            {
                return new StreamReader(new IsolatedStorageFileStream(path, FileMode.Open, isolatedStorageFolder));
            }
            return new StreamReader(path);
        }

        private static TextWriter CreateTextWriter(IsolatedStorageFile isolatedStorageFolder, string path)
        {
            if (isolatedStorageFolder != null)
            {
                return new StreamWriter(new IsolatedStorageFileStream(path, FileMode.OpenOrCreate, isolatedStorageFolder));
            }
            return new StreamWriter(path);
        }

        private static XmlSerializer CreateXmlSerializer(Type[] extraTypes)
        {
            if (extraTypes != null)
            {
                return new XmlSerializer(typeof(T), extraTypes);
            }
            return new XmlSerializer(typeof(T));
        }

        public static T Load(string path)
        {
            return XMLSerializer<T>.LoadFromXmlFormat(null, path, null);
        }

        public static T Load(string path, SerializeFormat serializedFormat)
        {
            T local = default(T);
            switch (serializedFormat)
            {
                case SerializeFormat.Binary:
                    return XMLSerializer<T>.LoadFromBinaryFormat(path, null);
            }
            return XMLSerializer<T>.LoadFromXmlFormat(null, path, null);
        }

        public static T Load(string path, Type[] extraTypes)
        {
            return XMLSerializer<T>.LoadFromXmlFormat(extraTypes, path, null);
        }

        public static T Load(string fileName, IsolatedStorageFile isolatedStorageDirectory)
        {
            return XMLSerializer<T>.LoadFromXmlFormat(null, fileName, isolatedStorageDirectory);
        }

        public static T Load(string fileName, IsolatedStorageFile isolatedStorageDirectory, SerializeFormat serializedFormat)
        {
            T local = default(T);
            switch (serializedFormat)
            {
                case SerializeFormat.Binary:
                    return XMLSerializer<T>.LoadFromBinaryFormat(fileName, isolatedStorageDirectory);
            }
            return XMLSerializer<T>.LoadFromXmlFormat(null, fileName, isolatedStorageDirectory);
        }

        public static T Load(string fileName, IsolatedStorageFile isolatedStorageDirectory, Type[] extraTypes)
        {
            return XMLSerializer<T>.LoadFromXmlFormat(null, fileName, isolatedStorageDirectory);
        }

        private static T LoadFromBinaryFormat(string path, IsolatedStorageFile isolatedStorageFolder)
        {
            T local = default(T);
            using (FileStream stream = XMLSerializer<T>.CreateFileStream(isolatedStorageFolder, path))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                return (formatter.Deserialize(stream) as T);
            }
        }

        public static T LoadFromXml(TextReader textReader, Type[] extraTypes)
        {
            T local = default(T);
            return (XMLSerializer<T>.CreateXmlSerializer(extraTypes).Deserialize(textReader) as T);
        }

        private static T LoadFromXmlFormat(Type[] extraTypes, string path, IsolatedStorageFile isolatedStorageFolder)
        {
            T local = default(T);
            using (TextReader reader = XMLSerializer<T>.CreateTextReader(isolatedStorageFolder, path))
            {
                return (XMLSerializer<T>.CreateXmlSerializer(extraTypes).Deserialize(reader) as T);
            }
        }

        public static T LoadXmlFromResources(Assembly assembly, string path, Type[] extraTypes)
        {
            T local = default(T);
            using (Stream stream = assembly.GetManifestResourceStream(path))
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    local = XMLSerializer<T>.LoadFromXml(reader, extraTypes);
                }
            }
            return local;
        }

        public static void Save(T serializableObject, string path)
        {
            XMLSerializer<T>.SaveToXmlFormat(serializableObject, null, path, null);
        }

        public static void Save(T serializableObject, string path, SerializeFormat serializedFormat)
        {
            switch (serializedFormat)
            {
                case SerializeFormat.Binary:
                    XMLSerializer<T>.SaveToBinaryFormat(serializableObject, path, null);
                    return;
            }
            XMLSerializer<T>.SaveToXmlFormat(serializableObject, null, path, null);
        }

        public static void Save(T serializableObject, string path, Type[] extraTypes)
        {
            XMLSerializer<T>.SaveToXmlFormat(serializableObject, extraTypes, path, null);
        }

        public static void Save(T serializableObject, string fileName, IsolatedStorageFile isolatedStorageDirectory)
        {
            XMLSerializer<T>.SaveToXmlFormat(serializableObject, null, fileName, isolatedStorageDirectory);
        }

        public static void Save(T serializableObject, string fileName, IsolatedStorageFile isolatedStorageDirectory, SerializeFormat serializedFormat)
        {
            switch (serializedFormat)
            {
                case SerializeFormat.Binary:
                    XMLSerializer<T>.SaveToBinaryFormat(serializableObject, fileName, isolatedStorageDirectory);
                    return;
            }
            XMLSerializer<T>.SaveToXmlFormat(serializableObject, null, fileName, isolatedStorageDirectory);
        }

        public static void Save(T serializableObject, string fileName, IsolatedStorageFile isolatedStorageDirectory, Type[] extraTypes)
        {
            XMLSerializer<T>.SaveToXmlFormat(serializableObject, null, fileName, isolatedStorageDirectory);
        }

        private static void SaveToBinaryFormat(T serializableObject, string path, IsolatedStorageFile isolatedStorageFolder)
        {
            using (FileStream stream = XMLSerializer<T>.CreateFileStream(isolatedStorageFolder, path))
            {
                new BinaryFormatter().Serialize(stream, serializableObject);
            }
        }

        private static void SaveToXmlFormat(T serializableObject, Type[] extraTypes, string path, IsolatedStorageFile isolatedStorageFolder)
        {
            using (TextWriter writer = XMLSerializer<T>.CreateTextWriter(isolatedStorageFolder, path))
            {
                XMLSerializer<T>.CreateXmlSerializer(extraTypes).Serialize(writer, serializableObject);
            }
        }
    }
}

