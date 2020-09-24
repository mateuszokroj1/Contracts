using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace Contracts
{
    public static class Activator
    {
        public static T CreateInstance<T>()
            where T : class, new()
        {
            ConstructorInfo constructor = typeof(T).GetConstructor(Type.EmptyTypes);
            NewExpression newExpression = Expression.New(constructor);
            LambdaExpression lambdaExpression = Expression.Lambda<Action<T>>(newExpression);

            Delegate @delegate = lambdaExpression.Compile();
            return @delegate.DynamicInvoke() as T;
        }

        public static TException CreateInstanceOfException<TException>(string message)
            where TException : Exception 
        {
            ConstructorInfo constructor = typeof(TException).GetConstructor(new Type[] { typeof(string) });
            NewExpression newExpression = Expression.New(constructor);
            LambdaExpression lambdaExpression = Expression.Lambda<Func<string, TException>>(newExpression);

            Delegate @delegate = lambdaExpression.Compile();
            return @delegate.DynamicInvoke(message) as TException;
        }
    }
}
