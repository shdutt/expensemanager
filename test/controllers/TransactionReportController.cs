using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using test.Entities;
using test.Repositories;
using test.controllers;
using Microsoft.WindowsAzure.Storage;

namespace test.controllers
{
    [Route("api/Expense")]
    public class TransactionReportController : Controller
    {

        private IExpenseReportRepository _myRepository;
        private object concatedResult;

        public TransactionReportController(IExpenseReportRepository myRepository)
        {
            _myRepository = myRepository;
        }

        /*[HttpGet]
        public ActionResult GetTransactionReports()
        {
           var transactionReports = _myRepository.GetTransactionReports();

            var result = Mapper.Map<IEnumerable<TransactionReportDto>>(transactionReports);

            return Ok(result);
        }*/


        [HttpGet("{id}/Transaction")]
        public ActionResult GetTransactionsForExpenseReport(int id)
        {
            var TransactionReport = _myRepository.GetTransactionsReport(id);

            if (TransactionReport == null)
                return NotFound();

            return Ok(TransactionReport);
        }
        [HttpGet("{id}/Transaction/{tid}")]
        public ActionResult GetTransactionForExpenseReport(int id, int tid)
        {
            List<TransactionReportCreationDto> ConcatedResult = new List<TransactionReportCreationDto>();
            var TransactionReport = _myRepository.GetTransactionForExpenseReport(id, tid);
            if (TransactionReport == null)
                return NotFound();


            return Ok(TransactionReport);
        }
        [HttpDelete("{id}/Transaction/{tid}")]
        public ActionResult DeleteExpenseReport(int id, int tid)
        {
            var Trans = _myRepository.GetTransactionForExpenseReport(id, tid);

            if (Trans == null)
                return NotFound();

            _myRepository.DeleteTransactionForExpenseReport(Trans);

            if (!_myRepository.Save())
                return StatusCode(500, "A Problem occured Processing your Request");

            return NoContent();
        }

        [HttpPost]
        public ActionResult CreateTransactionForExpense([FromBody]TransactionReportCreationDto TransactionReport)
        {
            if (TransactionReport == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest();

            var finalTransactionReport = Mapper.Map<Transaction>(TransactionReport);

            _myRepository.AddTransactionToExpenseReport(finalTransactionReport);
            if (!_myRepository.Save())
                return StatusCode(500, "Error Occured while processing request");

            TransactionUri t = new TransactionUri();

            finalTransactionReport.Uri = t.GetUriForAttachment(finalTransactionReport.Uri, finalTransactionReport.ExId, finalTransactionReport.tid);

            //_myRepository.AddTransactionToExpenseReport(finalTransactionReport);

            if (!_myRepository.Save())
                return StatusCode(500, "Error Occured while processing request");

            return CreatedAtRoute("GetExpenseReport", new { id = finalTransactionReport.tid }, finalTransactionReport);
        }
        [HttpPut("{id}/Transaction/{tid}")]
        public IActionResult Update(int id, int tid, [FromBody] TransactionReportCreationDto transReport)
        {
            if (transReport == null)
            {
                return BadRequest();
            }
            var UpdatedTransaction = _myRepository.GetTransactionForExpenseReport(id, tid);

            Mapper.Map(transReport, UpdatedTransaction);

            if (!_myRepository.Save())
            {
                return BadRequest();
            }
            return NoContent();
        }
    }
}
