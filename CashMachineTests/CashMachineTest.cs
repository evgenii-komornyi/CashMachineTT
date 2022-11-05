using CashMachineLogic.CashMachine;

namespace CashMachineTests
{
    public class CashMachineTest
    {
        [Fact]
        public void InsertCash_WithMoreThanTenBanknotes_ShouldThrownInvalidOperationException()
        {
            CashMachine cashMachine = new CashMachine();
            List<int> cashToInsert = new List<int>() { 100, 100, 100, 50, 50, 20, 20, 20, 10, 5, 5 };

            var exception = Assert.Throws<InvalidOperationException>(() => cashMachine.Insert(cashToInsert.ToArray()));

            Assert.Equal("Cash machine cannot work work with more than 10 banknotes.", exception.Message);
        }

        [Fact]
        public void InsertCash_BanknotesThatAreNotAvailables_ShouldThrownInvalidOperationException()
        {
            CashMachine cashMachine = new CashMachine();
            List<int> cashToInsert = new List<int>() { 100, 100, 100, 50, 50, 20, 20, 20, 10, 8 };

            var exception = Assert.Throws<InvalidOperationException>(() => cashMachine.Insert(cashToInsert.ToArray()));

            Assert.Equal("Banknote is not available for this cash machine! Please, insert only 5, 10, 20, 50, 100 nominals.", exception.Message);
        }

        [Fact]
        public void InsertCash_WithoutBanknotes_ShouldThrownInvalidOperationException()
        {
            CashMachine cashMachine = new CashMachine();
            List<int> cashToInsert = new List<int>();

            var exception = Assert.Throws<InvalidOperationException>(() => cashMachine.Insert(cashToInsert.ToArray()));

            Assert.Equal("Cash machine cannot work without banknotes.", exception.Message);
        }

        [Fact]
        public void WithdrawCash_InputAmountTwoHundredTen_ShouldReturnTwoHandredTen()
        {
            CashMachine cashMachine = new CashMachine();
            List<int> cashToInsert = new List<int>() { 100, 100, 50, 50, 20, 20, 20, 20, 20, 5 };

            cashMachine.Insert(cashToInsert.ToArray());

            int expectedAmount = 210;

            Assert.Equal(cashMachine.Withdraw(210), expectedAmount);
        }

        [Fact]
        public void WithdrawCash_InputAmountHundredTwentyFive_ShouldReturnZero()
        {
            CashMachine cashMachine = new CashMachine();
            List<int> cashToInsert = new List<int>() { 100, 50, 50, 20, 20, 20 };

            cashMachine.Insert(cashToInsert.ToArray());

            int expectedAmount = 0;

            Assert.Equal(cashMachine.Withdraw(125), expectedAmount);
        }

        [Fact]
        public void WithdrawCash_InputAmountMoreThanCashMachineHas_ShouldReturnZero()
        {
            CashMachine cashMachine = new CashMachine();
            List<int> cashToInsert = new List<int>() { 100, 50, 50, 20, 20, 20 };

            cashMachine.Insert(cashToInsert.ToArray());

            int expectedAmount = 0;

            Assert.Equal(cashMachine.Withdraw(5000), expectedAmount);
        }
    }
}