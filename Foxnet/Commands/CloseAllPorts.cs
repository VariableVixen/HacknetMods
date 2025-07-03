using Hacknet;

using Pathfinder.Port;

using VariableVixen.Hacknet.Lib.Extensions;

namespace VariableVixen.Hacknet.Foxnet.Commands;

internal class CloseAllPorts: CommandBase {
	public override string[] Aliases { get; } = ["lock"];
	public override string Description { get; } = "Instantly closes all ports";
	public override string[] Arguments { get; } = [];

	public override void Execute(OS os, string cmd, string[] args) {
		if (os.connectedComp is not null) {
			Computer c = os.connectedComp;
			bool hadPorts = false;
			string source = os.thisComputer.ip;

			foreach (PortState port in c.GetAllPortStates()) {
				if (!port.Cracked)
					continue;

				hadPorts = true;
				port.SetCracked(false, source);
				os.Print(Foxnet.MESSAGE_PREFIX, $"Closed {port.DisplayName} port ({port.Record.Protocol}, {port.PortNumber})");
			}

			if (!hadPorts)
				os.Print(Foxnet.MESSAGE_PREFIX, "No ports to close");
		}
	}
}
