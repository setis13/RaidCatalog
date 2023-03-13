using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaidCatalog.Logic.Extensions {
    public static class StringExtension {
        public static string OnlyLetters(this string str) {
            if (str == null) return null;
            return new String(str.Where(e => Char.IsLetter(e)).ToArray());
        }
    }
}
