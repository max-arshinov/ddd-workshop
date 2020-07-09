using System;

namespace DddWorkshop.Areas.Core.Domain.State
{
    public interface IHasStatus<out T> 
        where T : Enum
    {
        T Status { get; }
    }
}