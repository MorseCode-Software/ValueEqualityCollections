using TUnit.Core;
using TUnit.Mocks;

namespace MorseCode.Collections.ValueEquality.UnitTests;

public class GlobalSetup
{
    [Before(HookType.TestDiscovery)]
    public static void Configure(BeforeTestDiscoveryContext context)
    {
        context.Settings.Mocks.DefaultMode = MockBehavior.Strict;
    }
}