using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Laundry.BLL;
using Laundry.Model;

namespace Laundry.Web.Controllers
{
    public class TransactionController : ApiController
    {
        private TransactionBs NewTransactionBs = new TransactionBs();

        //Get api/<controller>/5
        public IHttpActionResult GetReceipt(string TransactionNo)
        {
            var result = NewTransactionBs.GetByTransactionNo(TransactionNo);
            if(result==null)
            {
                return NotFound();
            }
            else
            {
                return Ok(result);
            }

        }

        public IHttpActionResult GetReceipt(int id)
        {
            var result = NewTransactionBs.GetById(id);
            if (result == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(result);
            }

        }
    }
}
