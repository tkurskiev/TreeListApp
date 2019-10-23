using System;

namespace TreeListApp.Exceptions
{
    public class DbException : Exception
    {
        public DbException(Exception exception) : base(exception.Message, exception) 
        {
        }
    }
}
