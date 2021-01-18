using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TarkovLens.Enums;

namespace TarkovLens.Contracts
{
    public static class ApiRoutes
    {
        private static readonly string _baseUrl = "";

        public static class Items
        {
            private static readonly string _itemsControllerUrl = string.Concat(_baseUrl, "item");

            public static string Get(string id) => $"{_itemsControllerUrl}/{id}";
            public static string Search(string name) => $"{_itemsControllerUrl}/search?name={name}";
            public static string Kind(KindOfItem kind, string name = null, string caliber = null) => $"{_itemsControllerUrl}/kind/{kind}?name={name}&caliber={caliber}";
        }
    }
}
