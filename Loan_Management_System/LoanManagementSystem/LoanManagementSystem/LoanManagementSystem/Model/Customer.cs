namespace LoanManagementSystem.Model
{
    public class Customer
    {
        private int customerID;
        private string name;
        private string emailAddress;
        private string phoneNumber;
        private string address;
        private int creditScore;

        public int CustomerID
        {
            get { return customerID; }
            set { customerID = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string EmailAddress
        {
            get { return emailAddress; }
            set { emailAddress = value; }
        }

        public string PhoneNumber
        {
            get { return phoneNumber; }
            set { phoneNumber = value; }
        }

        public string Address
        {
            get { return address; }
            set { address = value; }
        }

        public int CreditScore
        {
            get { return creditScore; }
            set { creditScore = value; }
        }
    }
}
