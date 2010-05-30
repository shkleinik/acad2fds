namespace Fds2AcadPlugin.BLL.Helpers
{
    using MaterialManager.BLL;

    /// <summary>
    /// Work aroud of serializations limitations of IDictionary interface.
    /// </summary>
    public class Entry
    {
        public string Key;
        public Surface Value;

        public Entry()
        {
        }

        public Entry(string key, Surface value)
        {
            Key = key;
            Value = value;
        }
    }
}