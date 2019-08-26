using System;

namespace MT.Tavlei.Core.Common
{
    public class TavleiException : Exception
    {
        public TavleiException(string message) :
            base(message)
        {
        }
    }
}
