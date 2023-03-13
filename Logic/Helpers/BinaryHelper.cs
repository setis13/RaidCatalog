using System.IO;

namespace RaidCatalog.Logic.Helpers {
    public static class BinaryHelper {
        /// <summary>
        ///     Stream to bytes </summary>
        public static byte[] ReadFully(Stream input) {
            byte[] buffer = new byte[16 * 1024];
            using (var ms = new MemoryStream()) {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0) {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }
        public static async Task<byte[]> ReadFullyAsync(Stream input) {
            byte[] buffer = new byte[16 * 1024];
            using (var ms = new MemoryStream()) {
                int read;
                while ((read = await input.ReadAsync(buffer, 0, buffer.Length)) > 0) {
                    await ms.WriteAsync(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }
    }
}
