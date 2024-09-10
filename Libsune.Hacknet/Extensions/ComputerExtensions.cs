using System;

using Hacknet;

using Pathfinder.Util;

namespace PrincessRTFM.Hacknet.Lib.Extensions;

public static class ComputerExtensions {
	public const int MIN_PORT_COUNT_FOR_INVIOLABILITY = 100;

	/// <summary>
	/// Get the number of ports that must be opened to run PortHack on a computer, with sane bounding.
	/// </summary>
	/// <remarks>
	/// This function performs simple normalisation:
	/// the port count will be lower-bounded to <c>0</c>, and if it is greater than or equal to <c><see cref="MIN_PORT_COUNT_FOR_INVIOLABILITY"/></c> then <c><see cref="int.MaxValue"/></c> will be used instead.
	/// </remarks>
	/// <param name="computer">The target <see cref="Computer"/>.</param>
	/// <exception cref="ArgumentNullException">If the target computer is <c>null</c>.</exception>
	/// <returns>The sane number of ports needed for PortHack, or <see cref="int.MaxValue"/> if the computer is inviolable</returns>
	public static int GetPortsNeededForCrack(this Computer computer) {
		computer.ThrowNull(nameof(computer));
		int real = computer.GetRealPortsNeededForCrack();
		return real < 0
			? 0
			: real >= MIN_PORT_COUNT_FOR_INVIOLABILITY
			? int.MaxValue
			: real;
	}

	/// <summary>
	/// Set the number of ports that must be opened to run PortHack on a computer, with sane bounding.
	/// </summary>
	/// <inheritdoc cref="GetPortsNeededForCrack(Computer)" path="/remarks" />
	/// <inheritdoc cref="GetPortsNeededForCrack(Computer)" path="/param" />
	/// <param name="ports">The number of ports that must be opened for PortHack.</param>
	/// <inheritdoc cref="GetPortsNeededForCrack(Computer)" path="/exception" />
	public static void SetPortsNeededForCrack(this Computer computer, int ports) {
		computer.ThrowNull(nameof(computer));
		int real = ports < 0
			? 0
			: ports > MIN_PORT_COUNT_FOR_INVIOLABILITY
			? int.MaxValue
			: ports;
		computer.SetRealPortsNeededForCrack(real);
	}

	/// <summary>
	/// Get the "real" number of ports needed to crack a computer.
	/// </summary>
	/// <remarks>
	/// This function performs only minimal bounding: if the <paramref name="computer"/> is not null, the adjusted (<c>value + 1</c>) port count is lower-bounded at <c>0</c>.
	/// </remarks>
	/// <inheritdoc cref="GetPortsNeededForCrack(Computer)" path="/param" />
	/// <returns><c>-1</c> if the computer is null, else the actual number of ports needed to run PortHack.</returns>
	public static int GetRealPortsNeededForCrack(this Computer computer) {
		if (computer is null)
			return -1;
		int real = computer.portsNeededForCrack + 1;
		return real < 0
			? 0
			: real;
	}

	/// <summary>
	/// Set the actual number of ports needed to crack a computer.
	/// </summary>
	/// <inheritdoc cref="GetRealPortsNeededForCrack(Computer)" path="/remarks" />
	/// <inheritdoc cref="GetPortsNeededForCrack(Computer)" path="/param" />
	/// <param name="ports">The number of ports needed to crack.</param>
	public static void SetRealPortsNeededForCrack(this Computer comp, int ports) {
		if (comp is null)
			return;
		comp.portsNeededForCrack = ports < 0 ? -1 : ports - 1;
	}
}
