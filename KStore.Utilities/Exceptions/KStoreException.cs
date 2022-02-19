using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KStore.Utilities.Exceptions
{
    public class KStoreException : Exception
    {
        public KStoreException()
        {
        }
        public KStoreException(string message) : base(message)
        {

        }
        public KStoreException(string message, Exception inner):base(message, inner)
        {

        }
    }
}
