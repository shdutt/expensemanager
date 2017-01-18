using System.Collections.Generic;
using test.controllers;
using test.Entities;

namespace test.Repositories
{
    public interface IExpenseReportRepository
    {
        IEnumerable<Expense> GetTransactionReports();

        IEnumerable<users> getusers();

        Expense GetTransactionReportsWithoutTransaction(int expenseReportId);

        ExpenseReportDto GetTransactionReports(int expenseReportId, bool includeTransaction);

        IEnumerable<Transaction> GetTransactionsReport(int expenseReportId);

        Transaction GetTransactionForExpenseReport(int expenseReportId, int transactionId);

        bool ExpenseReportExists(int expenseReportId);

        void AddTransactionToExpenseReport(Transaction transaction);

        void AddExpenseReport(Expense expenseReport);

        void DeleteExpenseReport(Expense expenseReport);

        void DeleteTransactionForExpenseReport(Transaction transaction);

        bool Save();
    }
}



