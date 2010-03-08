using System.Text;
using GeometryConverter.DAL.Bases;
using GeometryConverter.DAL.Collections;

namespace GeometryConverter.BLL
{
    //todo: eliminate
    public static class ElementParser
    {
        private const string OBST = "&OBST XB=<%X1%>,<%X2%>,<%Y1%>,<%Y2%>,<%Z1%>,<%Z2%>, SURF_ID='<%SURF_ID%>' /";

        /// <summary>
        /// Provides FDS obstacle from Element Collection
        /// </summary>
        /// <param name="collection">Element Collection</param>
        /// <returns>FDS String</returns>
        public static string ToFdsString(this ElementCollection collection)
        {
            StringBuilder output = new StringBuilder();

            foreach (Element element in collection.Elements)
            {
                output.AppendFormat("{0}\n", element.ToFdsString());
            }
            return output.ToString();
        }

        /// <summary>
        /// Provides FDS obstacle from Element
        /// </summary>
        /// <param name="element">Element</param>
        /// <returns>FDS String</returns>
        public static string ToFdsString(this Element element)
        {
            StringBuilder result = new StringBuilder(OBST);
            result.Replace("<%X1%>", element.X1.ToString(".#"));
            result.Replace("<%X2%>", element.X2.ToString(".#"));
            result.Replace("<%Y1%>", element.Y1.ToString(".#"));
            result.Replace("<%Y2%>", element.Y2.ToString(".#"));
            result.Replace("<%Z1%>", element.Z1.ToString(".#"));
            result.Replace("<%Z2%>", element.Z2.ToString(".#"));
            result.Replace("<%SURF_ID%>", "MATL_ID");
            return result.ToString();
        }
    }
}
