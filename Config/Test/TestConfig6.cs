#if DEBUG

namespace CommonLib.Config
{
    [Config("test6.json", Version = 1)]
    public class TestConfig6
    {
        [WaypointName]
        public string WaypointName { get; set; } = "circle";

        [HexColor]
        public string HexColor { get; set; } = "#121314";
    }
}
#endif
