using System;

namespace MyVinted.Core.Application.Results
{
    public abstract record PaymentResult
    {
        public DateTime DateCreated => DateTime.Now;
    }
}