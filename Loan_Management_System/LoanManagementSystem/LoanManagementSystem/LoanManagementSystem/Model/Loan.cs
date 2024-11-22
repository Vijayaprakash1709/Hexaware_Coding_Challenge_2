namespace LoanManagementSystem.Model
{
    public class Loan
    {
        private int loanID;
        private int customerID;
        private decimal principalAmount;
        private float interestRate;
        private int loanTerm;
        private string loanType;
        private string loanStatus;

        public Loan()
        {
            customerID = 0;
            principalAmount = 0.0m;
            interestRate = 0.0f;
            loanTerm = 0;
            loanType = "Unknown";
            loanStatus = "Pending";
        }

        public Loan(int customerID, decimal principalAmount, float interestRate, int loanTerm, string loanType, string loanStatus)
        {
            this.customerID = customerID;
            this.principalAmount = principalAmount;
            this.interestRate = interestRate;
            this.loanTerm = loanTerm;
            this.loanType = loanType;
            this.loanStatus = loanStatus;
        }

        public int LoanID
        {
            get { return loanID; }
            set { loanID = value; }
        }

        public int CustomerID
        {
            get { return customerID; }
            set { customerID = value; }
        }

        public decimal PrincipalAmount
        {
            get { return principalAmount; }
            set { principalAmount = value; }
        }

        public float InterestRate
        {
            get { return interestRate; }
            set { interestRate = value; }
        }

        public int LoanTerm
        {
            get { return loanTerm; }
            set { loanTerm = value; }
        }

        public string LoanType
        {
            get { return loanType; }
            set { loanType = value; }
        }

        public string LoanStatus
        {
            get { return loanStatus; }
            set { loanStatus = value; }
        }

        public void PrintLoanInfo()
        {
            Console.WriteLine($"Loan ID: {loanID}");
            Console.WriteLine($"Customer ID: {customerID}");
            Console.WriteLine($"Principal Amount: {principalAmount}");
            Console.WriteLine($"Interest Rate: {interestRate}%");
            Console.WriteLine($"Loan Term: {loanTerm} years");
            Console.WriteLine($"Loan Type: {loanType}");
            Console.WriteLine($"Loan Status: {loanStatus}");
        }
    }
}
