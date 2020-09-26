using System;

namespace _02PrintMember
{
    internal class MemberExpression<T> : IMemberExpression<T>
    {
       
        T _value;

        public MemberExpression(T t)
        {
            this._value = t;
        }

        public IMemberExpression<T> PrintMember(Func<T, object> p)
        {
            Console.WriteLine(p.Invoke(_value).ToString());
            return this;
        }


    }
}