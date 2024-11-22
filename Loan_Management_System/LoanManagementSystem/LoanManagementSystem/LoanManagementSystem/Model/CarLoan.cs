namespace LoanManagementSystem.Model
{
    public class CarLoan : Loan
    {
        private string carModel;
        private decimal carValue;

        public CarLoan() : base()
        {
            carModel = "Unknown";
            carValue = 0.0m;
        }

        public CarLoan(int loanID, int customerID, decimal principalAmount, float interestRate, int loanTerm, string loanStatus, string carModel, decimal carValue)
            : base(customerID, principalAmount, interestRate, loanTerm, "CarLoan", loanStatus)
        {
            this.carModel = carModel;
            this.carValue = carValue;
        }

        public string CarModel
        {
            get { return carModel; }
            set { carModel = value; }
        }

        public decimal CarValue
        {
            get { return carValue; }
            set { carValue = value; }
        }

        public void PrintCarLoanInfo()
        {
            PrintLoanInfo();
            Console.WriteLine($"Car Model: {carModel}");
            Console.WriteLine($"Car Value: {carValue}");
        }
    }
}
