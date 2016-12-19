using System;
using System.Drawing;
using System.IO;
using System.Reflection;
using JetBrains.Annotations;

namespace NDataflashEditor.Core
{
	internal static class Paths
	{
        private const string DataflashDefinitionsDirectoryName = "DataflashDefinitions";      

		static Paths()
		{
			var assemblyLocation = Assembly.GetExecutingAssembly().Location;
			ApplicationIcon = Icon.ExtractAssociatedIcon(assemblyLocation);
			ApplicationDirectory = Directory.GetParent(assemblyLocation).FullName;


            DataflashDefinitionsDirectory = Path.Combine(ApplicationDirectory, DataflashDefinitionsDirectoryName);
		}

		public static string ApplicationDirectory { get; private set; }

		public static Icon ApplicationIcon { get; private set; }

		public static string SettingsFile { get; private set; }

		public static string DefinitionsFile { get; private set; }

		public static string DefinitionsDirectory { get; private set; }

        public static string DataflashDefinitionsDirectory { get; private set; }

        public static string PatchDirectory { get; private set; }

		public static string ResourcePackDirectory { get; private set; }

		public static void EnsureDirectoryExists([NotNull] string directoryPath)
		{
			if (string.IsNullOrEmpty(directoryPath)) throw new ArgumentNullException("directoryPath");
			if (Directory.Exists(directoryPath)) return;

			Directory.CreateDirectory(directoryPath);
		}

		[CanBeNull]
		public static string ValidateInputArgs([CanBeNull] string[] args)
		{
			if (args == null || args.Length != 1) return null;

			var filePath = args[0];
			return File.Exists(filePath) ? filePath : null;
		}
	}
}
