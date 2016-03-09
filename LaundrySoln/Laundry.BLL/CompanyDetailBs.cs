using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Laundry.Model;
using Laundry.DAL;

namespace Laundry.BLL
{
  public  class CompanyDetailBs
    {
        private CompanyDetailDA NewCompanyDetailDA = new CompanyDetailDA();

        public IEnumerable<CompanyDetail> ListAll()
        {
            return NewCompanyDetailDA.ListAll();
        }

        public CompanyDetail GetById(int id)
        {
            return NewCompanyDetailDA.GetById(id);
        }
        
              public IEnumerable<CompanyDetail> GetByIdList(int id)
        {
            return NewCompanyDetailDA.GetByIdList(id);
        }

        public CompanyDetail GetByCompCode(string code_type)
        {
            return NewCompanyDetailDA.GetByCompCode(code_type);
        }
        public void Insert(CompanyDetail CompanyDetailObj)
        {
            NewCompanyDetailDA.Insert(CompanyDetailObj);
        }

        public void Update(CompanyDetail CompanyDetailObj)
        {
            var CompanyDetailExist= NewCompanyDetailDA.GetById(CompanyDetailObj.Company_Id);
            CompanyDetailExist.Company_Name = CompanyDetailObj.Company_Name;
            CompanyDetailExist.Company_ShortName = CompanyDetailObj.Company_ShortName;
            CompanyDetailExist.Company_Address = CompanyDetailObj.Company_Address;
            CompanyDetailExist.Company_Phone1 = CompanyDetailObj.Company_Phone1;
            CompanyDetailExist.Company_Phone2 = CompanyDetailObj.Company_Phone2;
            CompanyDetailExist.Company_Email = CompanyDetailObj.Company_Email;
            CompanyDetailExist.Company_UserId = CompanyDetailObj.Company_UserId;
            //No update to be performed on login details. will solve dt at forgot login details section
            CompanyDetailExist.Company_Flag = CompanyDetailObj.Company_Flag;
            NewCompanyDetailDA.Update(CompanyDetailExist);
        }

        public string VerifyCompanyEmail(string email)
        {
            return NewCompanyDetailDA.VerifyCompanyEmail(email);
        }

        public string DisplayCompanyShortName()
        {
            CompanyDetailBs NewCompanyDetailBs = new CompanyDetailBs();
            var result = NewCompanyDetailBs.ListAll();
            string CompanyShortName = "";
            if (result != null)
            {
                foreach (var item in result)
                {
                    CompanyShortName = item.Company_ShortName;
                }
                if (CompanyShortName != null)
                {
                    return CompanyShortName;
                }
            }
            return CompanyShortName;
        }

        public string DisplayCompanyName()
        {
            CompanyDetailBs NewCompanyDetailBs = new CompanyDetailBs();
            var result = NewCompanyDetailBs.ListAll();
            string CompanyName = "";
            if (result != null)
            {
                foreach (var item in result)
                {
                    CompanyName = item.Company_Name.ToUpper();
                }
                if (CompanyName != null)
                {
                    return CompanyName;
                }
            }
            return CompanyName;
        }
    }
}

