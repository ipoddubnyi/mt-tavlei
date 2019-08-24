using System;

namespace MT.Tavlei.Core.Common
{
    public class TavleiRuntimeException : Exception
    {
        public TavleiRuntimeException(string message) :
            base(message)
        {
        }
    }
}
