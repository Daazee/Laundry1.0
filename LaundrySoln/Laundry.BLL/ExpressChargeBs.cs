using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Laundry.DAL;
using Laundry.Model;

namespace Laundry.BLL
{
   public class ExpressChargeBs
    {
        private ExpressChargeDA NewExpressChargeDA = new ExpressChargeDA();

        public IEnumerable<ExpressCharge> ListAll()
        {
            return NewExpressChargeDA.ListAll();
        }

        public ExpressCharge GetById(string id)
        {
            return NewExpressChargeDA.GetById(id);
        }
        public void Insert(ExpressCharge ExpressChargeBsObj)
        {
            NewExpressChargeDA.Insert(ExpressChargeBsObj);
        }

        public void Update(ExpressCharge ExpressChargeBsObj)
        {
            var ExpressChargeExist = NewExpressChargeDA.GetById(ExpressChargeBsObj.ExpressCharge_Id);
            ExpressChargeExist.ExpressAmount= ExpressChargeBsObj.ExpressAmount;
            ExpressChargeExist.ExpressFlag = ExpressChargeBsObj.ExpressFlag;
            ExpressChargeExist.ExpressUserId = ExpressChargeBsObj.ExpressUserId;
            NewExpressChargeDA.Update(ExpressChargeExist);
        }
    }
}
