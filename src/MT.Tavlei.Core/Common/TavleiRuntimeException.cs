using System;

namespace MT.Tavlei.Core.Common
{
    public class TavleiRuntimeException : TavleiException
    {
        public TavleiRuntimeException(string message) :
            base(message)
        {
        }
    }
}
