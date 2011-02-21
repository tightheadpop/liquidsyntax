using System.IO;
using System.Linq;

namespace LiquidSyntax {
    public static class FileSystemExtensions {
        public static DirectoryInfo GetSubdirectory(this DirectoryInfo directoryInfo, string name) {
            return directoryInfo.GetDirectories(name).FirstOrDefault();
        }
    }
}