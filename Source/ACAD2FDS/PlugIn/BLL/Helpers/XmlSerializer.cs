namespace Fds2AcadPlugin.BLL.Helpers
{
    using System;
    using System.IO;
    using System.Xml.Serialization;

    public class XmlSerializer<T> where T : class
    {
        private static XmlSerializer Serializer
        {
            get
            {
                try
                {
                    if (serializer == null)
                    {
                        lock (typeof(XmlSerializer<T>))
                        {
                            if (serializer == null)
                                serializer = new XmlSerializer(typeof(T));
                        }
                    }

                    return serializer;
                }
                catch (Exception)
                {
                    return serializer;
                }
            }
        }

        private static XmlSerializer serializer;

        public static void Serialize(T item, string filepath)
        {
            using (var fstream = new FileStream(filepath, FileMode.Create))
            {
                Serializer.Serialize(fstream, item);
            }
        }

        public static T Deserialize(string filepath)
        {
            try
            {
                object item;

                using (var fstream = new FileStream(filepath, FileMode.Open))
                {
                    item = Serializer.Deserialize(fstream);
                }

                return (T)item;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}