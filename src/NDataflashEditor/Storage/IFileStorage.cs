using System.Collections.Generic;
using JetBrains.Annotations;

namespace NDataflashEditor.Storages
{
	internal interface IStorage
	{
		void Initialize();
	}

	internal interface IFileStorage<out T> : IStorage
	{
		[CanBeNull]
		T TryLoad([NotNull] string filePath);

		[NotNull, ItemNotNull]
		IEnumerable<T> LoadAll();
	}
}
