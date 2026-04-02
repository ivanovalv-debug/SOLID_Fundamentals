namespace SOLID_Fundamentals
{
    using System;

    public abstract class Account
    {
        public decimal Balance { get; protected set; }

        public virtual void Deposit(decimal amount)
        {
            if (amount <= 0)
                throw new ArgumentException("Amount must be positive", nameof(amount));
            Balance += amount;
        }

        public virtual bool CanWithdraw(decimal amount, out string reason)
        {
            if (amount <= 0)
            {
                reason = "Amount must be positive";
                return false;
            }
            if (amount > Balance)
            {
                reason = "Insufficient funds";
                return false;
            }
            reason = string.Empty;
            return true;
        }

        public virtual void Withdraw(decimal amount)
        {
            if (!CanWithdraw(amount, out var reason))
                throw new InvalidOperationException(reason);
            Balance -= amount;
        }

        public virtual decimal CalculateInterest()
        {
            return Balance * 0.01m;
        }
    }

    public class SavingsAccount : Account
    {
        public decimal MinimumBalance { get; } = 100m;

        public override bool CanWithdraw(decimal amount, out string reason)
        {
            if (!base.CanWithdraw(amount, out reason))
                return false;

            if (Balance - amount < MinimumBalance)
            {
                reason = "Cannot go below minimum balance";
                return false;
            }
            return true;
        }

        public override decimal CalculateInterest()
        {
            return Balance * 0.01m;
        }
    }

    public class CheckingAccount : Account
    {
        public decimal OverdraftLimit { get; } = 500m;

        public override bool CanWithdraw(decimal amount, out string reason)
        {
            if (amount <= 0)
            {
                reason = "Amount must be positive";
                return false;
            }
            if (Balance - amount < -OverdraftLimit)
            {
                reason = "Overdraft limit exceeded";
                return false;
            }
            reason = string.Empty;
            return true;
        }

        public override decimal CalculateInterest()
        {
            return 0;
        }
    }

    public class FixedDepositAccount : Account
    {
        public DateTime MaturityDate { get; }

        public FixedDepositAccount(DateTime maturityDate)
        {
            MaturityDate = maturityDate;
        }

        public override bool CanWithdraw(decimal amount, out string reason)
        {
            if (amount <= 0)
            {
                reason = "Amount must be positive";
                return false;
            }
            if (DateTime.Now < MaturityDate)
            {
                reason = "Cannot withdraw before maturity date";
                return false;
            }
            if (amount > Balance)
            {
                reason = "Insufficient funds";
                return false;
            }
            reason = string.Empty;
            return true;
        }

        public override decimal CalculateInterest()
        {
            return Balance * 0.05m;
        }
    }

    public class BankService
    {
        public bool ProcessWithdrawal(Account account, decimal amount, out string message)
        {
            if (account.CanWithdraw(amount, out var reason))
            {
                account.Withdraw(amount);
                message = $"Successfully withdrew {amount:C}";
                return true;
            }
            message = $"Withdrawal failed: {reason}";
            return false;
        }

        public bool Transfer(Account from, Account to, decimal amount, out string message)
        {
            if (from.CanWithdraw(amount, out var reason))
            {
                from.Withdraw(amount);
                to.Deposit(amount);
                message = $"Transferred {amount:C}";
                return true;
            }
            message = $"Transfer failed: {reason}";
            return false;
        }
    }
}