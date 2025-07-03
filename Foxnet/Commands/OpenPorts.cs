using System.Collections.Generic;
using System.Linq;

using Hacknet;

using Pathfinder.Port;

using VariableVixen.Hacknet.Lib.Extensions;

namespace VariableVixen.Hacknet.Foxnet.Commands;

internal class OpenPorts: CommandBase {
	public override string[] Aliases { get; } = ["open"];
	public override string Description { get; } = "Opens the listed port(s), if active";
	public override string[] Arguments { get; } = ["ports..."];

	public override void Execute(OS os, string cmd, string[] args) {
		if (args.Length < 1) {
			os.Print(Foxnet.MESSAGE_PREFIX, $"Usage: {cmd} [<port> [<port2>...]]");
			return;
		}
		if (os.connectedComp is not null) {
			Computer c = os.connectedComp;
			string me = os.thisComputer.ip;

			HashSet<int> ports = new(args
				.SelectMany(s => s.Split(','))
				.Select(s => s.Trim())
				.Where(s => int.TryParse(s, out _))
				.Select(int.Parse)
				.ToArray()
			);

			foreach (PortState port in c.GetAllPortStates()) {
				if (ports.Contains(port.PortNumber) && !port.Cracked) {
					port.SetCracked(true, me);
					os.Print(Foxnet.MESSAGE_PREFIX, $"Opened port {port.PortNumber} ({port.DisplayName})");
				}
			}
		}
	}
}
