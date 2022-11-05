namespace CashMachineLogic.CashMachine
{
    /// <summary>
    /// Class contains logic to withdraw amount from 
    /// a cash machine and to insert banknotes to a cash machine.  
    /// </summary>
    public class CashMachine : ICashMachine
    {
        Dictionary<int, int> _allowedBanknotes = new Dictionary<int, int>()
        {
            {100, 0},
            {50, 0},
            {20, 0},
            {10, 0},
            {5, 0},
        };

        /// <summary>
        /// Inserts banknotes into cash machine.
        /// </summary>
        /// <param name="cashToInsert">Cash to insert.</param>
        /// <exception cref="InvalidOperationException">Thrown, when inserted banknotes do not match the correct format.</exception>
        public void Insert(int[] cashToInsert)
        {
            if (cashToInsert.Length == 0)
            {
                throw new InvalidOperationException("Cash machine cannot work without banknotes.");
            }

            for (int currentBanknote = 0; currentBanknote < cashToInsert.Length; currentBanknote++)
            {
                if (cashToInsert.Length > 10)
                {
                    throw new InvalidOperationException("Cash machine cannot work work with more than 10 banknotes.");
                }

                if (!_allowedBanknotes.ContainsKey(cashToInsert[currentBanknote]))
                {
                    throw new InvalidOperationException("Banknote is not available for this cash machine! Please, insert only 5, 10, 20, 50, 100 nominals.");
                }

                foreach (var banknote in _allowedBanknotes)
                {
                    if (cashToInsert[currentBanknote] == banknote.Key)
                    {
                        _allowedBanknotes[cashToInsert[currentBanknote]]++;
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// Gets amount from a cash machine.
        /// </summary>
        /// <param name="amount">Amount to get.</param>
        /// <returns>
        /// Amount received.
        /// Returns 0 if requested amount cannot be found in the cash machine. 
        /// </returns>
        public int Withdraw(int amount)
        {
            int banknotesAmount = _allowedBanknotes.Sum(availableBanknote => availableBanknote.Key * availableBanknote.Value);

            if (amount <= banknotesAmount)
            {
                List<int> cashPile = new List<int>();
                int banknoteIndex = 0;
                int amountToWithdraw = 0;

                while (!(amountToWithdraw == 0 && banknoteIndex >= _allowedBanknotes.Count))
                {
                    while (banknoteIndex < _allowedBanknotes.Count)
                    {
                        while (_allowedBanknotes.ElementAt(banknoteIndex).Value > 0 &&
                               _allowedBanknotes.ElementAt(banknoteIndex).Key + amountToWithdraw <= amount)
                        {
                            int currentBanknote = _allowedBanknotes.ElementAt(banknoteIndex).Key;
                            cashPile.Add(currentBanknote);
                            _allowedBanknotes[currentBanknote]--;
                            amountToWithdraw = cashPile.Sum(cash => cash);

                            if (amountToWithdraw == amount)
                            {
                                return amountToWithdraw;
                            }
                        }

                        banknoteIndex++;
                    }

                    if (cashPile.Count != 0)
                    {
                        int cashPileIndex = cashPile.Count - 1;
                        _allowedBanknotes[cashPile.ElementAt(cashPileIndex)]++;
                        amountToWithdraw -= cashPile[cashPileIndex];
                        banknoteIndex = GetIndexOfKey(_allowedBanknotes, cashPile[cashPileIndex]) + 1;
                        cashPile.RemoveAt(cashPileIndex);
                    }
                }
            }

            return 0;
        }

        /// <summary>
        /// Calculates index of dictionary by key.
        /// </summary>
        /// <param name="dictionary">Dictionary.</param>
        /// <param name="keyToFind">Key to find.</param>
        /// <returns>Found index by the key.</returns>
        private int GetIndexOfKey(Dictionary<int, int> dictionary, int keyToFind)
        {
            int index = -1;
            foreach (var value in dictionary.Keys)
            {
                index++;
                if (keyToFind == value)
                    return index;
            }

            return -1;
        }
    }
}