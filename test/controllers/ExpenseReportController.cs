using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using test.Repositories;
using test.Entities;
using Microsoft.AspNetCore.JsonPatch;
using test.controllers;
using AutoMapper;

namespace test.Controllers
{
    [Route("api/ExpenseReports")]
    public class ExpenseReportController : Controller
    {
        private IExpenseReportRepository _myRepository;

        public ExpenseReportController(IExpenseReportRepository myRepository)
        {
            _myRepository = myRepository;
        }

        [Route("login")]
        [HttpGet()]
        public ActionResult getUser()
        {
            var users = _myRepository.getusers();
            //var result = Mapper.Map<IEnumerable<usersDto>>(users);
            return Ok(users);
        }
        
        [HttpGet()]
        public ActionResult GetExpenseReports()
        {
            var expenseReports = _myRepository.GetTransactionReports();

            var result = Mapper.Map<IEnumerable<ExpenseReportDto>>(expenseReports);

            return Ok(result);
        }


        [HttpGet("{id}", Name = "GetExpenseReport")]
        public ActionResult GetExpenseReport(int id)
        {
            var expenseReport = _myRepository.GetTransactionReports(id, false);

            if (expenseReport == null)
                return NotFound();

            var result = Mapper.Map<ExpenseReportDto>(expenseReport);

            return Ok(result);
        }

        [HttpPost]
        public ActionResult CreateExpenseReport([FromBody]ExpenseReportCreationDto expenseReport)
        {
            if (expenseReport == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest();

            var finalExpenseReport = Mapper.Map<Expense>(expenseReport);

            _myRepository.AddExpenseReport(finalExpenseReport);

            if (!_myRepository.Save())
                return StatusCode(500, "Error Occured while processing request");

            return CreatedAtRoute("GetExpenseReport", new { id = finalExpenseReport.id }, finalExpenseReport);
        }

        [HttpPatch("{id}")]
        public ActionResult UpdateExpenseReport(int id, [FromBody] JsonPatchDocument<ExpenseReportUpdateDto> patchDoc)
        {
            if (patchDoc == null)
                return BadRequest();

            var expenseReport = _myRepository.GetTransactionReportsWithoutTransaction(id);

            if (expenseReport == null)
                return NotFound();

            var expenseReportToPatch = Mapper.Map<ExpenseReportUpdateDto>(expenseReport);

            patchDoc.ApplyTo(expenseReportToPatch, ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            TryValidateModel(expenseReportToPatch);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            Mapper.Map(expenseReportToPatch, expenseReport);

            if (!_myRepository.Save())
                return StatusCode(500, "A Problem occured Processing your Request");

            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteExpenseReport(int id)
        {
            var expenseReport = _myRepository.GetTransactionReportsWithoutTransaction(id);
            var TransactionReport = _myRepository.GetTransactionReports(id, true);
            var temp = TransactionReport.Transaction.Count;
            if (temp != 0)
                return StatusCode(500, "This Expense is being refered by Transactions,cannot delete");


            if (expenseReport == null)
                return NotFound();

            _myRepository.DeleteExpenseReport(expenseReport);

            if (!_myRepository.Save())
                return StatusCode(500, "A Problem occured Processing your Request");

            return NoContent();
        }
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] ExpenseReportCreationDto expenseReport)
        {
            if (expenseReport == null)
            {
                return BadRequest();
            }
            var expense = _myRepository.GetTransactionReportsWithoutTransaction(id);
            

            if (expense == null)
            {
                return NotFound();
            }
            Mapper.Map(expenseReport, expense);
            if(!_myRepository.Save())
            {
                return BadRequest();
            }
            return NoContent();
        }




    }
}
