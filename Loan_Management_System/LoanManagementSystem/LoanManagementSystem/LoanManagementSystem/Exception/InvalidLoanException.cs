using System;


namespace LoanManagementSystem.Exceptions
{
    public class InvalidLoanException : Exception
    {
        public InvalidLoanException(string message) : base(message) { }
        
    }
}
