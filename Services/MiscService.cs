using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csv_column_matcher.Services
{
    public static class MiscService
    {
        public static bool IsValidFile(string path)
        {
            return !string.IsNullOrEmpty(path) && System.IO.File.Exists(path);
        }

        public static bool IsValidDirectory(string path)
        {
            return !string.IsNullOrEmpty(path) && System.IO.Directory.Exists(path);
        }
    }
}
