using System;
using System.Linq.Expressions;

namespace DddWorkshop.Infrastructure
{
    public abstract class Specs<T>
    {
        protected static Spec<T> Spec(Expression<Func<T, bool>> expr) => new Spec<T>(expr);
    }
}