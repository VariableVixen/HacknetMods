using BepInEx;
using BepInEx.Hacknet;

namespace VariableVixen.Hacknet.Lib;

[BepInPlugin(GUID, NAME, VERSION)]
internal class FakeMod: HacknetPlugin {
	public const string
		GUID = $"PrincessRTFM.{NAME}",
		NAME = "LibsuneHacknet",
		VERSION = "3.0.1";

	public override bool Load() => true;
}
