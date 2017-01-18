using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using test.controllers;
using test.Entities;

namespace test.Repositories
{
    public class ExpenseReportRepository : IExpenseReportRepository
    {
        private ExpenseInfo _myContext;

        public ExpenseReportRepository(ExpenseInfo myContext)
        {
            _myContext = myContext;
        }
        public IEnumerable<users> getusers()
        {
            return _myContext.users.OrderBy(c => c.username).ToList();

        }

        public IEnumerable<Expense> GetTransactionReports()
        {
            return _myContext.Expense.OrderBy(c => c.name).ToList();
        }

        public Expense GetTransactionReportsWithoutTransaction(int expenseReportId)
        {
            return _myContext.Expense.Where(c => c.id == expenseReportId).FirstOrDefault();
        }

        public ExpenseReportDto GetTransactionReports(int expenseReportId, bool includeTransaction)
        {
            var x = (Mapper.Map<ExpenseReportDto>(_myContext.Expense.Where(c => c.id == expenseReportId).FirstOrDefault()));
            var expenseReportWithTransacions = Mapper.Map<ExpenseReportDto>(x);
            if (!includeTransaction)
                return expenseReportWithTransacions;
            else
            {
                var TransactionsList = GetTransactionsReport(expenseReportId);
                foreach (Transaction t in TransactionsList)
                {
                    expenseReportWithTransacions.Transaction.Add(Mapper.Map<TransactionReportDto>(t));
                }
                return expenseReportWithTransacions;
            }

        }

        public Transaction GetTransactionForExpenseReport(int expenseReportId, int transactionId)
        {
            return _myContext.Transaction.Where(c => c.tid == transactionId && c.ExId == expenseReportId).FirstOrDefault();
        }

        public IEnumerable<Transaction> GetTransactionsReport(int expenseReportId)
        {
            return _myContext.Transaction.Where(c => c.ExId == expenseReportId).ToList();
        }

        public bool ExpenseReportExists(int expenseReportId)
        {
            return _myContext.Expense.Any(c => c.id == expenseReportId);
        }

        public void AddTransactionToExpenseReport(Transaction transaction)
        {
            _myContext.Transaction.Add(transaction);
        }

        public void AddExpenseReport(Expense expenseReport)
        {
            _myContext.Expense.Add(expenseReport);
        }

        public bool Save()
        {
            try { return (_myContext.SaveChanges() >= 0); }
            catch(Exception e) { return false; }
        }

        public void DeleteExpenseReport(Expense expenseReport)
        {
            _myContext.Expense.Remove(expenseReport);
        }

        public void DeleteTransactionForExpenseReport(Transaction transaction)
        {
            _myContext.Transaction.Remove(transaction);
        }
    }
}