using System;
using System.Collections.Generic;
using System.Linq;

using Hacknet;

using Pathfinder.Executable;

using PrincessRTFM.Hacknet.Lib.Extensions;

namespace PrincessRTFM.Hacknet.Foxnet.Commands;

internal class GetAllPrograms: CommandBase {
	public override string[] Aliases { get; } = ["xmas"];
	public override string Description { get; } = "Gives you a copy of EVERY executable in the game";
	public override string[] Arguments { get; } = [];

	public override void Execute(OS os, string cmd, string[] args) {
		Folder bin = os.thisComputer.files.root.searchForFolder("bin");
		HashSet<string> progs = new(bin.files.Select(f => f.data));
		List<FileEntry> files = [];
		foreach (int exeNum in PortExploits.exeNums) {
			string exeName = PortExploits.cracks[exeNum];
			string magic = PortExploits.crackExeData[exeNum];

			if (!progs.Contains(magic))
				files.Add(new(magic, exeName));
		}
		try {
			foreach (ExecutableManager.CustomExeInfo ce in ExecutableManager.AllCustomExes) {
				if (!progs.Contains(ce.ExeData))
					files.Add(new(ce.ExeData, ce.ExeType.FullName + ".exe"));
			}
		}
		catch (Exception e) {
			os.Print(Foxnet.MESSAGE_PREFIX, $"Failed to reflect into custom executable manager");
			os.Print(Foxnet.MESSAGE_PREFIX, $"{e.GetType().Name}:\n{e.Message}");
			os.Print(Foxnet.MESSAGE_PREFIX, "Cannot provide custom executables");
		}
		bin.files.AddRange(files);
		os.Print(Foxnet.MESSAGE_PREFIX, $"Added {files.Count} program{(files.Count == 1 ? "" : "s")} to your /bin folder.");
		if (files.Count > 0 && cmd == "xmas")
			os.Print(Foxnet.MESSAGE_PREFIX, "Ho ho ho, ya naughty fuck.");
	}
}
