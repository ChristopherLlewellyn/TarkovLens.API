using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TarkovLens.Helpers.ExtensionMethods
{
    public static class RavenExtensions
    {
        public static string AddCollectionName(this string id, string collectionName)
        {
            if (id.IsNullOrEmpty())
            {
                return null;
            }
            else if (id.ToLower().StartsWith(collectionName.ToLower()))
            {
                return id;
            }
            return string.Format("{0}/{1}", collectionName, id);
        }

        public static List<string> AddCollectionName(this IEnumerable<string> list, string collectionName)
        {
            var returnList = new List<string>();
            foreach (var item in list)
            {
                returnList.Add(item.AddCollectionName(collectionName));
            }

            return returnList;
        }

        public static string StripCollectionName(this string id)
        {
            if (id.IsNullOrEmpty()) return string.Empty;
            if (!id.Contains("/"))
            {
                return id;
            }
            return id.Substring(id.IndexOf("/") + 1);
        }

        public static List<string> StripCollectionName(this IEnumerable<string> list)
        {
            var returnList = new List<string>();
            foreach (var item in list)
            {
                returnList.Add(item.StripCollectionName());
            }

            return returnList;
        }
    }
}
