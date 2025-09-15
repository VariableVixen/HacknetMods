using BepInEx;
using BepInEx.Hacknet;

namespace VariableVixen.Hacknet.Lib;

[BepInPlugin(GUID, NAME, VERSION)]
[BepInDependency(Pathfinder.PathfinderAPIPlugin.ModGUID)]
internal class FakeMod: HacknetPlugin {
	public const string
		GUID = $"PrincessRTFM.{NAME}",
		NAME = "LibsuneHacknet",
		VERSION = "3.0.0";

	public override bool Load() => true;
}
