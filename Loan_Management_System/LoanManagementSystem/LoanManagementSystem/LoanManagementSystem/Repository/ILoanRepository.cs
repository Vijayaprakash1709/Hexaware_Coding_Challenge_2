using LoanManagementSystem.Model;

namespace LoanManagementSystem.Repository
{
    public interface ILoanRepository
    {
        void ApplyLoan(Loan loan);
        decimal CalculateInterest(int loanId);
        decimal CalculateInterest(decimal principal, float interestRate, int loanTenure);
        void LoanStatus(int loanId);
        decimal CalculateEMI(int loanId);
        decimal CalculateEMI(decimal principal, float annualRate, int tenureMonths);
        void LoanRepayment(int loanId, decimal amount);
        List<Loan> GetAllLoans();
        Loan GetLoanById(int loanId);
    }
}
