using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TarkovLens.Helpers.ExtensionMethods
{
    public static class BooleanExtensions
    {
        public static bool IsNull<T>(this T thing)
        {
            return thing == null;
        }

        public static bool IsNotNull<T>(this T thing)
        {
            return thing != null;
        }
    }
}
