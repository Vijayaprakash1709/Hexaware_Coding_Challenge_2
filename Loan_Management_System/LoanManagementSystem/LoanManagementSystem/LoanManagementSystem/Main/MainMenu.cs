using LoanManagementSystem.Service;

namespace LoanManagementSystem.Main
{
    public class MainMenu
    {
            public static void LoanManagement() {
           
            LoanService loanService = new LoanService();

            bool exit = false;

            while (!exit)
            {
                Console.Clear();
                Console.WriteLine("\n\t--------------Loan Management System-----------------");
                Console.WriteLine("\n\n    1. Apply for Loan");
                Console.WriteLine("    2. Calculate Loan Interest");
                Console.WriteLine("    3. Calculate EMI");
                Console.WriteLine("    4. Check Loan Status");
                Console.WriteLine("    5. Make Loan Repayment");
                Console.WriteLine("    6. Show All Loans");
                Console.WriteLine("    7. Get Loan Details by ID");
                Console.WriteLine("    8. Exit");
                Console.Write("\n Select an option: ");

                try
                {
                    int choice =int.Parse(Console.ReadLine());
                    switch (choice)
                    {
                        case 1:
                            loanService.ApplyLoan();
                            break;
                        case 2:
                            loanService.ShowLoanInterest();
                            break;
                        case 3:
                            loanService.ShowLoanEMI();
                            break;
                        case 4:
                            loanService.ShowLoanStatus();
                            break;
                        case 5:
                            loanService.MakeLoanRepayment();
                            break;
                        case 6:
                            loanService.ShowAllLoans();
                            break;
                        case 7:
                            loanService.ShowLoanDetailsById();
                            break;
                        case 8:
                            exit = true;
                            return;
                        default:
                            Console.WriteLine("Invalid choice. Please try again.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }

                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
            }
        }
    
    }
}
