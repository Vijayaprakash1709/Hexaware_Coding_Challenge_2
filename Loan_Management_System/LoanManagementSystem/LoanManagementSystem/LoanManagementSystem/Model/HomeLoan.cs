namespace LoanManagementSystem.Model
{
    public class HomeLoan : Loan
    {
        private string propertyAddress;
        private decimal propertyValue;

        public HomeLoan() : base()
        {
            propertyAddress = "Unknown";
            propertyValue = 0.0m;
        }

        public HomeLoan(int loanID, int customerID, decimal principalAmount, float interestRate, int loanTerm, string loanStatus, string propertyAddress, decimal propertyValue)
            : base(customerID, principalAmount, interestRate, loanTerm, "HomeLoan", loanStatus)
        {
            this.propertyAddress = propertyAddress;
            this.propertyValue = propertyValue;
        }

        public string PropertyAddress
        {
            get { return propertyAddress; }
            set { propertyAddress = value; }
        }

        public decimal PropertyValue
        {
            get { return propertyValue; }
            set { propertyValue = value; }
        }

        public void PrintHomeLoanInfo()
        {
            PrintLoanInfo();
            Console.WriteLine($"Property Address: {propertyAddress}");
            Console.WriteLine($"Property Value: {propertyValue}");
        }
    }
}
