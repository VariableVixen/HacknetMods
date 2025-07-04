using Hacknet;

using VariableVixen.Hacknet.Lib.Extensions;

namespace VariableVixen.Hacknet.Foxnet.Commands;

internal class SetTraceTime: CommandBase {

	public override string Description { get; } = "Set the passive trace time for the connected computer.";
	public override string[] Arguments { get; } = ["seconds"];

	public override void Execute(OS os, string cmd, string[] args) {
		if (os.connectedComp is null) {
			os.Print(Foxnet.MESSAGE_PREFIX, "You are not connected to a computer");
			return;
		}
		if (!float.TryParse(args[0], out float time)) {
			os.Print(Foxnet.MESSAGE_PREFIX, "Invalid argument: must pass trace time in seconds as a float");
			return;
		}
		if (time < 0) {
			os.Print(Foxnet.MESSAGE_PREFIX, "Invalid argument: trace time cannot be negative");
			return;
		}

		os.connectedComp.traceTime = time;
		if (time > 0)
			os.Print(Foxnet.MESSAGE_PREFIX, $"Set trace time of {os.connectedComp.ip} to {time} seconds");
		else
			os.Print(Foxnet.MESSAGE_PREFIX, $"Cleared trace on {os.connectedComp.ip}");
	}
}
