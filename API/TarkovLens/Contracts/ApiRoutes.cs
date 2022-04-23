using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TarkovLens.Enums;

namespace TarkovLens.Contracts
{
    public static class ApiRoutes
    {
        private static readonly string ApiBaseUrl = "";

        public static class Items
        {
            private static readonly string ItemsControllerUrl = string.Concat(ApiBaseUrl, "item");

            public static string Get() => $"{ItemsControllerUrl}";
            public static string Get(string id) => $"{ItemsControllerUrl}/{id}";
            public static string BsgId(string bsgId) => $"{ItemsControllerUrl}/bsgid/{bsgId}";
            public static string Search(string name) => $"{ItemsControllerUrl}/search?name={name}";
            public static string Kind() => $"{ItemsControllerUrl}/kind";
            public static string Kind(KindOfItem kind, string name = null, string caliber = null) => $"{ItemsControllerUrl}/kind/{kind}?name={name}&caliber={caliber}";
        }

        public static class Characters
        {
            private static readonly string CharactersControllerUrl = string.Concat(ApiBaseUrl, "character");

            public static string Get() => $"{CharactersControllerUrl}";
            public static string Get(string id) => $"{CharactersControllerUrl}/{id}";
            public static string Type(CharacterType type) => $"{CharactersControllerUrl}/type/{type}";
            public static string Combatants() => $"{CharactersControllerUrl}/type/combatant";
        }
    }
}
