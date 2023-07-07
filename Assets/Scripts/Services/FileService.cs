using System.IO;
using System.Xml;
using System.Threading.Tasks;
using UnityEngine;

namespace Services
{
	internal static class FileService
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="fileName">the string contaning the path to the file from the StreamingAssets folder (must contain the extension too)</param>
		/// <returns></returns>
		public static XmlDocument LoadXml(string fileName)
		{
			string path = Path.Combine(Application.streamingAssetsPath, fileName);
			XmlDocument xmlDocument = new XmlDocument();
			if (!File.Exists(path))
			{
				Debug.LogWarning($"ERROR: The file {fileName} does not exist");
				// There is no file at the designated path
				// TODO How do we handle that case ?
				return null;
			}
			else
			{
				// Try to load the localization file
				xmlDocument.Load(path);
				return xmlDocument;
			}
		}

		public async static Task<XmlDocument> LoadXmlAsync(string fileName)
		{
			string path = Path.Combine(Application.streamingAssetsPath, fileName);
			XmlDocument xmlDocument = new XmlDocument();
			if (!File.Exists(path))
			{
				Debug.LogWarning($"ERROR: The file {fileName} does not exist");
				// There is no file at the designated path
				// TODO How do we handle that case ?
				return null;
			}
			else
			{
				// Try to load the localization file
				await Task.Run(() => xmlDocument.Load(path));
				return xmlDocument;
			}
		}
	}
}
