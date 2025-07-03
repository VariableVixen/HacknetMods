using Hacknet;

using VariableVixen.Hacknet.Lib.Extensions;

namespace VariableVixen.Hacknet.Foxnet.Commands;

internal class SetPortsForCrack: CommandBase {
	public override string Description { get; } = "Changes the number of ports needed for PortHack";
	public override string[] Arguments { get; } = ["ports"];

	public override void Execute(OS os, string cmd, string[] args) {
		if (os.connectedComp is not Computer c) {
			os.Print(Foxnet.MESSAGE_PREFIX, "You are not connected to a target node!");
			return;
		}
		if (!int.TryParse(args[0], out int ports)) {
			os.Print(Foxnet.MESSAGE_PREFIX, "Invalid port count - must be an integer!");
			return;
		}

		int old = c.GetPortsNeededForCrack();
		c.SetPortsNeededForCrack(ports > 0 ? ports : int.MaxValue);
		ports = c.GetPortsNeededForCrack();

		string descOld = old == int.MaxValue
			? "INVIOLABLE"
			: old.ToString();
		string descNew = ports == int.MaxValue
			? "INVIOLABLE"
			: ports.ToString();

		os.Print(Foxnet.MESSAGE_PREFIX, $"Changed minimum port count from {descOld} to {descNew}");
	}
}
