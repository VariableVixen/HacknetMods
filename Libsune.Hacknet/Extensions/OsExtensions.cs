using System;
using System.Collections.Generic;
using System.Linq;

using Hacknet;

using Pathfinder.Util;

namespace PrincessRTFM.Hacknet.Lib.Extensions;

public static class OsExtensions {

	/// <summary>
	/// Writes a series of lines to the player's terminal.
	/// </summary>
	/// <exception cref="ArgumentNullException">If the OS object is <c>null</c>.</exception>
	/// <param name="os">The Hacknet <see cref="OS"/>.</param>
	/// <param name="lines">The series of lines to print.</param>
	public static void Write(this OS os, IEnumerable<string> lines) {
		os.ThrowNull(nameof(os));
		foreach (string line in lines)
			os.write(line);
	}

	/// <inheritdoc cref="Write(OS, IEnumerable{string})" />
	public static void Write(this OS os, params string[] lines) => os.Write((IEnumerable<string>)lines);

	/// <summary>
	/// Writes a series of lines to the player's terminal, prefixed with the given string and a space.
	/// </summary>
	/// <param name="prefix">The string to prepend to each line. An additional space will be added.</param>
	/// <inheritdoc cref="Write(OS, IEnumerable{string})" />
	public static void Print(this OS os, string prefix, IEnumerable<string> lines) => os.Write(lines.Select(s => $"{prefix} {s}".Trim()));

	/// <inheritdoc cref="Print(OS, string, IEnumerable{string})" />
	public static void Print(this OS os, string prefix, params string[] lines) => os.Print(prefix, (IEnumerable<string>)lines);

}
