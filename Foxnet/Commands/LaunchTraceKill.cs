using System.Collections.Generic;
using System.Linq;

using Hacknet;

using Pathfinder.Executable;

using VariableVixen.Hacknet.Lib.Extensions;

namespace VariableVixen.Hacknet.Foxnet.Commands;

internal class LaunchTraceKill: CommandBase {
	public const int TRACEKILL_RAM_COST = 600;

	public override string[] Aliases { get; } = ["tkill", "tk"];
	public override string Description { get; } = "Instantly launches TraceKill.exe, closing open programs if you need more RAM";
	public override string[] Arguments { get; } = [];

	public override void Execute(OS os, string cmd, string[] args) {
		const string name = "TraceKill.exe";
		string magic = Foxnet.GetMagicFromExeName(name);

		if (!string.IsNullOrEmpty(magic)) {
			int free = os.ramAvaliable;
			IEnumerable<ExeModule> liveExes = os.exes.Where(e => e.ramCost > 0);
			if (free < TRACEKILL_RAM_COST) { // PASS ONE - remove notes
				IEnumerable<ExeModule> filtered = liveExes.Where(e => e is NotesExe);
				foreach (ExeModule exe in filtered) {
					int ram = exe.ramCost;
					if (exe.Kill())
						free += ram;
					if (free >= TRACEKILL_RAM_COST)
						break;
				}
			}
			if (free < TRACEKILL_RAM_COST) { // PASS TWO - remove theme changers
				IEnumerable<ExeModule> filtered = liveExes.Where(e => e is ThemeChangerExe);
				foreach (ExeModule exe in filtered) {
					int ram = exe.ramCost;
					if (exe.Kill())
						free += ram;
					if (free >= TRACEKILL_RAM_COST)
						break;
				}
			}
			if (free < TRACEKILL_RAM_COST) { // PASS THREE - remove netmap organisers
				IEnumerable<ExeModule> filtered = liveExes.Where(e => e is NetmapOrganizerExe);
				foreach (ExeModule exe in filtered) {
					int ram = exe.ramCost;
					if (exe.Kill())
						free += ram;
					if (free >= TRACEKILL_RAM_COST)
						break;
				}
			}
			if (free < TRACEKILL_RAM_COST) { // PASS FOUR - remove music changers
				IEnumerable<ExeModule> filtered = liveExes.Where(e => e is TuneswapExe);
				foreach (ExeModule exe in filtered) {
					int ram = exe.ramCost;
					if (exe.Kill())
						free += ram;
					if (free >= TRACEKILL_RAM_COST)
						break;
				}
			}
			if (free < TRACEKILL_RAM_COST) { // PASS FIVE - remove shells
				IEnumerable<ExeModule> filtered = liveExes.Where(e => e is ShellExe);
				foreach (ExeModule exe in filtered) {
					int ram = exe.ramCost;
					if (exe.Kill())
						free += ram;
					if (free >= TRACEKILL_RAM_COST)
						break;
				}
			}
			if (free < TRACEKILL_RAM_COST) { // PASS SIX - remove everything that's left
				foreach (ExeModule exe in liveExes) {
					int ram = exe.ramCost;
					if (exe.Kill())
						free += ram;
					if (free > TRACEKILL_RAM_COST)
						break;
				}
			}
			os.ramAvaliable = free;
			os.launchExecutable(name, magic, 12); // target port isn't even used in the method, wtf
		}
		else {
			os.Print(Foxnet.MESSAGE_PREFIX, $"Foxnet error: cannot find internal data for {name}");
		}
	}
}
