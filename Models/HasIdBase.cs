using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace DotNext.DddWorkshop.Models
{
    public abstract class HasIdBase: HasIdBase<int>
    {}
    
    public abstract class HasIdBase<TKey>
        where TKey: IEquatable<TKey>
    {
        [Key, Required, HiddenInput]
        public virtual TKey Id { get; set; }
    }
}