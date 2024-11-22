using System;
using LoanManagementSystem.Model;
using LoanManagementSystem.Repository;

namespace LoanManagementSystem.Service
{
    public class LoanService
    {
        private readonly ILoanRepository _loanRepository;

        public LoanService()
        {
            _loanRepository = new LoanRepository();
        }

        public void ApplyLoan()
        {
            try
            {
                Console.Clear();
                Console.WriteLine("=== Apply for a Loan ===");
                Console.WriteLine();

                Console.Write("Enter Customer ID: ");
                int customerId = int.Parse(Console.ReadLine());

                Console.Write("Enter Loan Principal Amount (e.g. 50000.75): ");
                decimal principalAmount = decimal.Parse(Console.ReadLine());

                Console.Write("Enter Interest Rate (in percentage, e.g. 7.5): ");
                float interestRate = float.Parse(Console.ReadLine());

                Console.Write("Enter Loan Term (in years, e.g. 10): ");
                int loanTerm = int.Parse(Console.ReadLine());

                string loanType;
                while (true)
                {
                    Console.Write("Enter Loan Type (CarLoan / HomeLoan): ");
                    loanType = Console.ReadLine();

                    if (loanType.Equals("CarLoan", StringComparison.OrdinalIgnoreCase) ||
                        loanType.Equals("HomeLoan", StringComparison.OrdinalIgnoreCase))
                    {
                        loanType = char.ToUpper(loanType[0]) + loanType.Substring(1).ToLower();
                        break;
                    }

                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid loan type. Please enter 'CarLoan' or 'HomeLoan'.");
                    Console.ResetColor();
                }

                Loan loan = new Loan
                {
                    CustomerID = customerId,
                    PrincipalAmount = principalAmount,
                    InterestRate = interestRate,
                    LoanTerm = loanTerm,
                    LoanType = loanType
                };

                _loanRepository.ApplyLoan(loan);

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\nLoan application has been successfully submitted.");
                Console.ResetColor();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\nError: {ex.Message}");
                Console.ResetColor();
            }
        }


        public void ShowLoanInterest()
        {
            try
            {
                Console.Clear();
                Console.WriteLine("=== Show Loan Interest ===");
                Console.WriteLine();

                Console.Write("Enter Loan ID to calculate interest: ");
                int loanId = int.Parse(Console.ReadLine());

                decimal interest = _loanRepository.CalculateInterest(loanId);
                Console.WriteLine($"\nThe calculated interest for Loan ID {loanId} is: {interest}");
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\nError: {ex.Message}");
                Console.ResetColor();
            }
        }

        public void ShowLoanEMI()
        {
            try
            {
                Console.Clear();
                Console.WriteLine("=== Show Loan EMI ===");
                Console.WriteLine();

                Console.Write("Enter Loan ID to calculate EMI: ");
                int loanId = int.Parse(Console.ReadLine());

                decimal emi = _loanRepository.CalculateEMI(loanId);
                Console.WriteLine($"\nThe calculated EMI for Loan ID {loanId} is: {emi}");
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\nError: {ex.Message}");
                Console.ResetColor();
            }
        }

        public void ShowLoanStatus()
        {
            try
            {
                Console.Clear();
                Console.WriteLine("=== Show Loan Status ===");
                Console.WriteLine();

                Console.Write("Enter Loan ID to check status: ");
                int loanId = int.Parse(Console.ReadLine());

                _loanRepository.LoanStatus(loanId);
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\nError: {ex.Message}");
                Console.ResetColor();
            }
        }

        public void MakeLoanRepayment()
        {
            try
            {
                Console.Clear();
                Console.WriteLine("=== Make Loan Repayment ===");
                Console.WriteLine();

                Console.Write("Enter Loan ID for repayment: ");
                int loanId = int.Parse(Console.ReadLine());

                Console.Write("Enter amount to pay (e.g. 5000.75): ");
                decimal amount = decimal.Parse(Console.ReadLine());

                _loanRepository.LoanRepayment(loanId, amount);

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\nSuccessfully made a repayment of {amount} for Loan ID {loanId}.");
                Console.ResetColor();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\nError: {ex.Message}");
                Console.ResetColor();
            }
        }

        public void ShowAllLoans()
        {
            try
            {
                Console.Clear();
                Console.WriteLine("=== All Loans Information ===");
                Console.WriteLine();

                var loans = _loanRepository.GetAllLoans();
                if (loans.Count == 0)
                {
                    Console.WriteLine("No loans found.");
                    return;
                }

                foreach (var loan in loans)
                {
                    Console.WriteLine($"Loan ID: {loan.LoanID}");
                    Console.WriteLine($"Customer ID: {loan.CustomerID}");
                    Console.WriteLine($"Principal: {loan.PrincipalAmount}");
                    Console.WriteLine($"Interest Rate: {loan.InterestRate}%");
                    Console.WriteLine($"Term: {loan.LoanTerm} years");
                    Console.WriteLine($"Type: {loan.LoanType}");
                    Console.WriteLine($"Status: {loan.LoanStatus}");
                    Console.WriteLine("-------------------------------------------");
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\nError in showAllLoans(): {ex.Message}");
                Console.ResetColor();
            }
        }

        public void ShowLoanDetailsById()
        {
            try
            {
                Console.Clear();
                Console.WriteLine("=== Show Loan Details by ID ===");
                Console.WriteLine();

                Console.Write("Enter Loan ID to get details: ");
                int loanId = int.Parse(Console.ReadLine());

                Loan loan = _loanRepository.GetLoanById(loanId);
                Console.WriteLine($"\nLoan ID: {loan.LoanID}");
                Console.WriteLine($"Customer ID: {loan.CustomerID}");
                Console.WriteLine($"Principal: {loan.PrincipalAmount}");
                Console.WriteLine($"Interest Rate: {loan.InterestRate}%");
                Console.WriteLine($"Term: {loan.LoanTerm} years");
                Console.WriteLine($"Type: {loan.LoanType}");
                Console.WriteLine($"Status: {loan.LoanStatus}");
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\nError: {ex.Message}");
                Console.ResetColor();
            }
        }
    }
}
