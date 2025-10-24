using System;

namespace CommonLib.Exceptions
{
    public sealed class GameVersionNotSupportedException : Exception
    {
        public GameVersionNotSupportedException()
            : base("This game version is not supported! Try update mod or contact author")
        {
        }
    }
}
