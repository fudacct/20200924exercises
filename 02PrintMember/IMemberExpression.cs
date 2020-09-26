using System;

namespace _02PrintMember
{
    internal interface IMemberExpression<T>
    {
        IMemberExpression<T> PrintMember(Func<T, object> p);

        
    }
}