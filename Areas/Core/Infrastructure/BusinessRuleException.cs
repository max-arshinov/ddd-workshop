using System;
using System.Collections.Generic;
using Force.Ccc;

namespace DddWorkshop.Areas.Core.Infrastructure
{
    public sealed class BusinessRuleException : Exception, IHasUserFrendlyMessage
    {
        public BusinessRuleException(string message) : base(message)
        {
        }
        
        public BusinessRuleException(string message, Exception innerException) : base(message, innerException)
        {
        }
        
        public BusinessRuleException(string message, object data) : base(message)
        {
            Data["ExceptionData"] = data;
        }
    }
}