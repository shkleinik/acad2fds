using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace MaterialManager.BLL
{
    public static class MaterialSerializer
    {
        public static void SerializeMaterials(string path, List<Surface> materials)
        {
            Stream writer = new FileStream(path, FileMode.Create);

            try
            {
                var serializer = new XmlSerializer(typeof(List<Surface>));
                serializer.Serialize(writer, materials);
            }
            catch
            {
            }
            finally
            {
                writer.Close();
            }
        }

        public static List<Surface> DeserializeMaterials(string path)
        {
            Stream reader = null;
            try
            {
                reader = new FileStream(path, FileMode.Open, FileAccess.Read);
                var serializer = new XmlSerializer(typeof(List<Surface>));
                return (List<Surface>)serializer.Deserialize(reader);
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
            }
        }
    }
}