using Hacknet;

using VariableVixen.Hacknet.Lib.Extensions;

namespace VariableVixen.Hacknet.Foxnet.Commands;

internal class ResetProxy: CommandBase {
	public override string[] Aliases { get; } = ["unsnap", "blip"];
	public override string Description { get; } = "Instantly resets the proxy the connected system";
	public override string[] Arguments { get; } = [];

	public override void Execute(OS os, string cmd, string[] args) {
		if (os.connectedComp is not null) {
			Computer c = os.connectedComp;

			if (c.hasProxy) {
				c.proxyOverloadTicks = c.startingOverloadTicks;
				c.proxyActive = true;
				os.Print(Foxnet.MESSAGE_PREFIX, "Proxy enabled");
			}
			else {
				os.Print(Foxnet.MESSAGE_PREFIX, "No proxy present");
			}
		}
	}
}
