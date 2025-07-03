using Hacknet;

using Pathfinder.Port;

using VariableVixen.Hacknet.Lib.Extensions;

namespace VariableVixen.Hacknet.Foxnet.Commands;

internal class ResetSecurity: CommandBase {
	public override string Description { get; } = "Instantly closes all ports and resets firewall/proxy";
	public override string[] Arguments { get; } = [];

	public override void Execute(OS os, string cmd, string[] args) {
		Computer c = args.Length > 0
			? Programs.getComputer(os, args[0])
			: os.connectedComp;

		if (c is not null) {
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

			if (c.firewall is not null) {
				c.firewall.solved = false;
				os.Print(Foxnet.MESSAGE_PREFIX, $"Firewall locked ({c.firewall.solution ?? "<no solution?>"})");
			}
			else {
				os.Print(Foxnet.MESSAGE_PREFIX, "No firewall present");
			}

			if (c.hasProxy) {
				c.proxyActive = true;
				os.Print(Foxnet.MESSAGE_PREFIX, "Proxy enabled");
			}
			else {
				os.Print(Foxnet.MESSAGE_PREFIX, "No proxy present");
			}

			Foxnet.PrintRandomSnark(os);
		}
		else {
			os.Print(Foxnet.MESSAGE_PREFIX, "Target computer not found");
		}
	}
}
