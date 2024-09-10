using Hacknet;

using PrincessRTFM.Hacknet.Lib.Extensions;

namespace PrincessRTFM.Hacknet.Foxnet.Commands;

internal class LoseAdminAccess: CommandBase {
	public override string Description { get; } = "Lose admin access on a computer you have it on";
	public override string[] Arguments { get; } = [];

	public override void Execute(OS os, string cmd, string[] args) {
		if (os.connectedComp is not null) {
			if (os.connectedComp.ip == os.thisComputer.ip) {
				os.Print(Foxnet.MESSAGE_PREFIX, "Cannot lose admin on your own computer");
			}
			else if (os.connectedComp.PlayerHasAdminPermissions()) {
				os.connectedComp.adminIP = os.connectedComp.ip;
				os.Print(Foxnet.MESSAGE_PREFIX, "Admin access lost");
			}
			else {
				os.Print(Foxnet.MESSAGE_PREFIX, "You aren't an admin");
			}
		}
	}
}
