using System;
using Microsoft.AspNetCore.Mvc;

namespace DotNext.DddWorkshop.Models
{
    public abstract class EntityBase<T>
        where T: IEquatable<T>
    {
        [HiddenInput]
        public virtual T Id { get; protected set; }
        protected virtual object Actual => this;

        public override bool Equals(object obj)
        {
            var other = obj as EntityBase<T>;

            if (other is null)
                return false;

            if (ReferenceEquals(this, other))
                return true;

            if (Actual.GetType() != other.Actual.GetType())
                return false;

            if (Id?.Equals(default) != false || other.Id?.Equals(default) != false)
                return false;

            return Id.Equals(other.Id);
        }

        public static bool operator ==(EntityBase<T> a, EntityBase<T> b)
        {
            if (a is null && b is null)
                return true;

            if (a is null || b is null)
                return false;

            return a.Equals(b);
        }

        public static bool operator !=(EntityBase<T> a, EntityBase<T> b)
        {
            return !(a == b);
        }

        public override int GetHashCode()
        {
            return (Actual.GetType().ToString() + Id).GetHashCode();
        }
    }
}