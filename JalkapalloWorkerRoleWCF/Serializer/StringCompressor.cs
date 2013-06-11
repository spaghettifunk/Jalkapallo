using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.IO;
using System.IO.Compression;

namespace JalkapalloWorkerRoleWCF
{
	public static class StringCompressor
	{
		// Compresses request
		public static string Compress(string text)
		{
			byte[] gZipBuffer;
			byte[] buffer = Encoding.UTF8.GetBytes(text);

			using (MemoryStream ms = new MemoryStream())
			{
				using (GZipStream gZipStream = new GZipStream(ms, CompressionMode.Compress, true))
				{
					gZipStream.Write(buffer, 0, buffer.Length);
				}

				ms.Position = 0;

				byte[] compressedData = new byte[ms.Length];
				ms.Read(compressedData, 0, compressedData.Length);

				gZipBuffer = new byte[compressedData.Length + 4];
				Buffer.BlockCopy(compressedData, 0, gZipBuffer, 4, compressedData.Length);
				Buffer.BlockCopy(BitConverter.GetBytes(buffer.Length), 0, gZipBuffer, 0, 4);
			}
			return Convert.ToBase64String(gZipBuffer);
		}

		// Decompresses answer
		public static string Decompress(string compressedText)
		{
			byte[] buffer;
			byte[] gZipBuffer = Convert.FromBase64String(compressedText);
			using (MemoryStream ms = new MemoryStream())
			{
				int dataLength = BitConverter.ToInt32(gZipBuffer, 0);
				ms.Write(gZipBuffer, 4, gZipBuffer.Length - 4);

				buffer = new byte[dataLength];

				ms.Position = 0;
				using (GZipStream gZipStream = new GZipStream(ms, CompressionMode.Decompress))
				{
					gZipStream.Read(buffer, 0, buffer.Length);
				}
			}
			return Encoding.UTF8.GetString(buffer);
		}
	}
}