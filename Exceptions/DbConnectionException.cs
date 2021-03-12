using System;

namespace Exceptions
{
    public class DbConnectionException : Exception
    {
        public DbConnectionException(string msg) : base(msg) {
        }
        public DbConnectionException(string msg, Exception ex) : base(msg, ex) {
        }
    }
}
