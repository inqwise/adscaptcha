using System;

namespace Inqwise.AdsCaptcha.SystemFramework
{
    /// <summary>
    /// Summary description for Randomizer
    /// </summary>
    public sealed class Randomizer
    {
        static Randomizer _randomizer;
        static object _sunc = new object();
        public Random Random { get; private set; }

        private Randomizer()
        {
            Random = new Random();
        }

        public static Randomizer Instance
        {
            get
            {
                if (_randomizer == null)
                {
                    lock (_sunc)
                    {
                        if (_randomizer == null)
                        {
                            _randomizer = new Randomizer();
                        }
                    }
                }
                return _randomizer;
            }
        }
    }
}
