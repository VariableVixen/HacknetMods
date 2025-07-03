using System;

using Hacknet;

using Pathfinder.Util;

namespace VariableVixen.Hacknet.Lib.Extensions;

public static class FilesystemExtensions {

	/// <summary>
	/// Searches for a folder with the given name inside the target folder, creates one if it doesn't exist, and then returns the requested <see cref="Folder" />.
	/// </summary>
	/// <param name="folder">The target folder to search.</param>
	/// <param name="name">The name of the folder being looked for.</param>
	/// <returns>A <see cref="Folder"/> with the given name, within the target folder.</returns>
	/// <exception cref="ArgumentNullException">If the target folder is <c>null</c>.</exception>
	public static Folder GetOrCreateFolder(this Folder folder, string name) {
		folder.ThrowNull(nameof(folder), "Cannot get or create entry in null folder");

		Folder found = folder.searchForFolder(name);
		if (found is null) {
			found = new(name);
			folder.folders.Add(found);
		}
		return found;
	}

	/// <summary>
	/// Searches for a file with the given name inside the target folder, creates an empty one if it doesn't exist, and then returns the requested <see cref="FileEntry"/>.
	/// </summary>
	/// <param name="folder">The target folder to search.</param>
	/// <param name="name">The name of the file being looked for.</param>
	/// <returns>A <see cref="FileEntry"/> with the given name, within the target folder. If it did not previously exist, the <see cref="FileEntry.data"/> will be empty.</returns>
	/// <inheritdoc cref="GetOrCreateFolder(Folder, string)" path="/exception" />
	public static FileEntry GetOrCreateFile(this Folder folder, string name) {
		folder.ThrowNull(nameof(folder), "Cannot get or create entry in null folder");

		FileEntry found = folder.searchForFile(name);
		if (found is null) {
			found = new(string.Empty, name);
			folder.files.Add(found);
		}
		return found;
	}
}
