using System;
using System.Collections.Generic;
using System.IO;
using NFirmware;
using NDataflashEditor.Managers;
using NLog;
using System.Xml.Serialization;
using NDataflashEditor.Core;
using NDataflash.Definition;

namespace NDataflashEditor.Storages
{
	internal class DataFlashDefinitionsStorage : IFileStorage<DataflashDefinition>
	{
		private static readonly ILogger s_logger = LogManager.GetCurrentClassLogger();

		#region Implementation of IStorage
		public void Initialize()
		{
			var initEx = Safe.Execute(() => Paths.EnsureDirectoryExists(Paths.DataflashDefinitionsDirectory));
			if (initEx == null) return;

			s_logger.Warn(initEx, "An error occured during creating definitions directory '{0}'.", Paths.DataflashDefinitionsDirectory);
		}
		#endregion

		#region Implementation of IFileStorage<out FirmwareDefinition>
		public DataflashDefinition TryLoad(string filePath)
		{
			if (string.IsNullOrEmpty(filePath)) throw new ArgumentNullException("filePath");

			try
			{
				using (var fs = File.OpenRead(filePath))
				{
                    var serializer = new XmlSerializer(typeof(DataflashDefinition));
                    return (DataflashDefinition)serializer.Deserialize(fs);
				}
			}
			catch (Exception ex)
			{
				s_logger.Warn(ex, "An error occured during reading definition file '{0}'.", filePath);
				return null;
			}
		}

		public IEnumerable<DataflashDefinition> LoadAll()
		{
			var result = new List<DataflashDefinition>();
			var files = Directory.GetFiles(Paths.DataflashDefinitionsDirectory, Consts.DataflashDefinitionFileExtension, SearchOption.AllDirectories);
			foreach (var filePath in files)
			{
				var definition = TryLoad(filePath);
				if (definition == null) continue;

				definition.FileName = Path.GetFileName(filePath);
				definition.Sha = GitHubApi.GetGitSha(filePath);
				result.Add(definition);
			}
			return result;
		}
		#endregion

	}
}
