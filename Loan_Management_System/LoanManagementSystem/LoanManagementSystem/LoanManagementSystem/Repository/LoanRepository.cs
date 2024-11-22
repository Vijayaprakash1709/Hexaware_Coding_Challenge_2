
using System.Data.SqlClient;
using LoanManagementSystem.Exceptions;
using LoanManagementSystem.Model;
using LoanManagementSystem.Util;

namespace LoanManagementSystem.Repository
{
    public class LoanRepository : ILoanRepository
    {
        public void ApplyLoan(Loan loan)
        {
            try
            {
                using (SqlConnection connection = DBConnUtil.GetConnection())
                {
                    connection.Open();
                    string query = "INSERT INTO Loan (CustomerID, PrincipalAmount, InterestRate, LoanTerm, LoanType, LoanStatus) " +
                                   "VALUES (@CustomerID, @PrincipalAmount, @InterestRate, @LoanTerm, @LoanType, 'Pending')";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@CustomerID", loan.CustomerID);
                        command.Parameters.AddWithValue("@PrincipalAmount", loan.PrincipalAmount);
                        command.Parameters.AddWithValue("@InterestRate", loan.InterestRate);
                        command.Parameters.AddWithValue("@LoanTerm", loan.LoanTerm);
                        command.Parameters.AddWithValue("@LoanType", loan.LoanType);

                        Console.Write("Confirm loan application? (Yes/No): ");
                        string confirmation = Console.ReadLine();
                        if (confirmation?.ToLower() == "yes")
                        {
                            command.ExecuteNonQuery();
                            
                        }
                        else
                        {
                            Console.WriteLine("Loan application canceled.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error applying loan: {ex.Message}");
                Console.ResetColor();
                throw;
            }
        }

        public decimal CalculateInterest(int loanId)
        {
            try
            {
                using (SqlConnection connection = DBConnUtil.GetConnection())
                {
                    connection.Open();
                    string query = "SELECT PrincipalAmount, InterestRate, LoanTerm FROM Loan WHERE LoanID = @LoanID";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@LoanID", loanId);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (!reader.Read())
                            {
                                throw new InvalidLoanException("Loan not found.");
                            }

                            decimal principal = reader.GetDecimal(0);
                            float interestRate = (float)reader.GetDouble(1);
                            int loanTerm = reader.GetInt32(2);

                            return (principal * (decimal)interestRate * loanTerm) / 12;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error calculating interest: {ex.Message}");
                Console.ResetColor();
                throw;
            }
        }

        public decimal CalculateInterest(decimal principal, float interestRate, int loanTenure)
        {
            try
            {
                return (principal * (decimal)interestRate * loanTenure) / 12;
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error calculating interest with parameters: {ex.Message}");
                Console.ResetColor();
                throw;
            }
        }

        public void LoanStatus(int loanId)
        {
            try
            {
                using (SqlConnection connection = DBConnUtil.GetConnection())
                {
                    connection.Open();

                    string creditScoreQuery = @"
                SELECT c.CreditScore 
                FROM Loan l
                INNER JOIN Customer c ON l.CustomerID = c.CustomerID
                WHERE l.LoanID = @LoanID";
                    int creditScore;

                    using (SqlCommand creditScoreCommand = new SqlCommand(creditScoreQuery, connection))
                    {
                        creditScoreCommand.Parameters.AddWithValue("@LoanID", loanId);
                        object result = creditScoreCommand.ExecuteScalar();

                        if (result == null)
                        {
                            throw new InvalidLoanException("Loan not found or customer information is missing.");
                        }

                        creditScore = Convert.ToInt32(result);
                    }

                    string loanStatus = creditScore > 650 ? "Approved" : "Rejected";

                    if (creditScore > 650)
                    {
                        string updateStatusQuery = "UPDATE Loan SET LoanStatus = @LoanStatus WHERE LoanID = @LoanID";
                        using (SqlCommand updateCommand = new SqlCommand(updateStatusQuery, connection))
                        {
                            updateCommand.Parameters.AddWithValue("@LoanStatus", loanStatus);
                            updateCommand.Parameters.AddWithValue("@LoanID", loanId);
                            updateCommand.ExecuteNonQuery();
                        }
                    }

                    Console.ForegroundColor = loanStatus == "Approved" ? ConsoleColor.Green : ConsoleColor.Red;
                    Console.WriteLine($"Loan Status: {loanStatus} ");
                    Console.ResetColor();
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error processing loan status: {ex.Message}");
                Console.ResetColor();
                throw;
            }
        }


        public decimal CalculateEMI(int loanId)
        {
            try
            {
                using (SqlConnection connection = DBConnUtil.GetConnection())
                {
                    connection.Open();
                    string query = "SELECT PrincipalAmount, InterestRate, LoanTerm FROM Loan WHERE LoanID = @LoanID";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@LoanID", loanId);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (!reader.Read())
                            {
                                throw new InvalidLoanException("Loan not found.");
                            }

                            decimal principal = reader.GetDecimal(0);
                            float annualRate = (float)reader.GetDouble(1);
                            int loanTerm = reader.GetInt32(2);

                            float monthlyRate = annualRate / 12 / 100;
                            int tenureMonths = loanTerm * 12;

                            if (monthlyRate == 0)
                            {
                                return principal / tenureMonths;
                            }

                            decimal numerator = principal * (decimal)(monthlyRate * Math.Pow(1 + monthlyRate, tenureMonths));
                            decimal denominator = (decimal)(Math.Pow(1 + monthlyRate, tenureMonths) - 1);

                            return numerator / denominator;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error calculating EMI: {ex.Message}");
                Console.ResetColor();
                throw;
            }
        }

        public decimal CalculateEMI(decimal principal, float annualRate, int tenureMonths)
        {
            try
            {
                float monthlyRate = annualRate / 12 / 100;

                decimal numerator = principal * (decimal)(monthlyRate * Math.Pow(1 + monthlyRate, tenureMonths));
                decimal denominator = (decimal)(Math.Pow(1 + monthlyRate, tenureMonths) - 1);

                return numerator / denominator;
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error calculating EMI with parameters: {ex.Message}");
                Console.ResetColor();
                throw;
            }
        }

        public void LoanRepayment(int loanId, decimal amount)
        {
            try
            {
                decimal emi = CalculateEMI(loanId);
                if (amount < emi)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Payment rejected. Amount is less than the EMI.");
                    Console.ResetColor();
                    return;
                }

                int emiCount = (int)(amount / emi);

                using (SqlConnection connection = DBConnUtil.GetConnection())
                {
                    connection.Open();
                    string query = "UPDATE Loan SET LoanTerm = LoanTerm - @EMICount WHERE LoanID = @LoanID";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@EMICount", emiCount);
                        command.Parameters.AddWithValue("@LoanID", loanId);
                        command.ExecuteNonQuery();
                    }
                }

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Successfully paid {emiCount} EMIs.");
                Console.ResetColor();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error processing repayment: {ex.Message}");
                Console.ResetColor();
                throw;
            }
        }

        public List<Loan> GetAllLoans()
        {
            try
            {
                List<Loan> loans = new List<Loan>();
                using (SqlConnection connection = DBConnUtil.GetConnection())
                {
                    connection.Open();
                    string query = "SELECT * FROM Loan";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Loan loan = new Loan
                            {
                                LoanID = reader.GetInt32(0),
                                CustomerID = reader.GetInt32(1),
                                PrincipalAmount = reader.GetDecimal(2),
                                InterestRate = (float)reader.GetDouble(3),
                                LoanTerm = reader.GetInt32(4),
                                LoanType = reader.GetString(5),
                                LoanStatus = reader.GetString(6)
                            };
                            loans.Add(loan);
                        }
                    }
                }
                return loans;
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error fetching all loans: {ex.Message}");
                Console.ResetColor();
                throw;
            }
        }
        public Loan GetLoanById(int loanId)
        {
            try
            {
                using (SqlConnection connection = DBConnUtil.GetConnection())
                {
                    connection.Open();
                    string query = "SELECT * FROM Loan WHERE LoanID = @LoanID";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@LoanID", loanId);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (!reader.Read())
                            {
                                throw new InvalidLoanException("Loan not found.");
                            }

                            return new Loan
                            {
                                LoanID = reader.GetInt32(0),
                                CustomerID = reader.GetInt32(1),
                                PrincipalAmount = reader.GetDecimal(2),
                                InterestRate = (float)reader.GetDouble(3), 
                                LoanTerm = reader.GetInt32(4),
                                LoanType = reader.GetString(5),
                                LoanStatus = reader.GetString(6)
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching loan by ID: {ex.Message}");
                throw;
            }
        }
    }
}
