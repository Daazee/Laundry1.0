using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Laundry.DAL;
using Laundry.Model;

namespace Laundry.BLL
{
  public  class CodesBs
    {
        private CodesDA NewCodesDA = new CodesDA();

        public IEnumerable<Codes> ListAll()
        {
            return NewCodesDA.ListAll();
        }

        public Codes GetById(int id)
        {
            return NewCodesDA.GetById(id);
        }

        public IEnumerable<Codes> GetByCodeType(string code_type)
        {
            return NewCodesDA.GetByCodeType(code_type);
        }

        public Codes GetByCodeValue(string code_value)
        {
            return NewCodesDA.GetByCodeValue(code_value);
        }

        public Codes GetByCodeDesc(string code_desc)
        {
            return NewCodesDA.GetByCodeDesc(code_desc);
        }
        public void Insert(Codes CodesBsObj)
        {
            NewCodesDA.Insert(CodesBsObj);
        }

        public void Update(Codes CodesBsObj)
        {
            var CodesExist = NewCodesDA.GetById(CodesBsObj.Codes_Id);
            CodesExist.Codes_Desc = CodesBsObj.Codes_Desc;
            CodesExist.Codes_Val = CodesBsObj.Codes_Val;
            CodesExist.Codes_Flag = CodesBsObj.Codes_Flag;
            NewCodesDA.Update(CodesExist);
        }
    }
}
