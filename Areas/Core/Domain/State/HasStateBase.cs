using System;
using Force.Ddd;

namespace DddWorkshop.Areas.Core.Domain.State
{
    public abstract class HasStateBase<TStatus, TState>: 
        EntityBase<int>,
        IHasStatus<TStatus>, 
        IHasState<TState>
        where TStatus: Enum
    {
        private TStatus _status;
        
        private TState _state;
        
        public TStatus Status
        {
            get => _status;
            protected set
            {
                _status = value;
                _state = GetState(_status);
            }
        }

        public abstract TState GetState(TStatus status);

        public TState State => _state ??= GetState(Status);
        
        protected T As<T>()
            where T: TState
            => (T)State;

        protected T To<T>(TStatus status)
            where T : TState
        {
            Status = status;
            return (T)State;
        }

        public static explicit operator TState(HasStateBase<TStatus, TState> hasStatus)
        {
            return hasStatus.State;
        }
    }
}