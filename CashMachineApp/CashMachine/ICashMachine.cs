namespace CashMachineLogic.CashMachine
{
    /// <summary>
    /// Class contains logic to withdraw amount from 
    /// a cash machine and to insert banknotes to a cash machine.  
    /// </summary>
    public interface ICashMachine
    {
        /// <summary>
        /// Gets amount from a cash machine.
        /// </summary>
        /// <param name="amount">Amount to get.</param>
        /// <returns>Amount received.</returns>
        int Withdraw(int amount);

        /// <summary>
        /// Inserts banknotes into cash machine.
        /// </summary>
        /// <param name="cash">Cash to insert.</param>
        void Insert(int[] cash);
    }
}