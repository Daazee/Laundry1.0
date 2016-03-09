using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Laundry.DAL;
using Laundry.Model;

namespace Laundry.BLL
{
    public class ClothingBs
    {
        private ClothingDA NewClothingDA = new ClothingDA();

        public IEnumerable<Clothing> ListAll()
        {
            return NewClothingDA.ListAll();
        }

        public Clothing GetById(string id)
        {
            return NewClothingDA.GetById(id);
        }
        public void Insert(Clothing ClothingBsObj)
        {
            NewClothingDA.Insert(ClothingBsObj);
        }

        public void Update(Clothing ClothingBsObj)
        {
            var ClothingExist = NewClothingDA.GetById(ClothingBsObj.ClothId);
            ClothingExist.ClothDesc = ClothingBsObj.ClothDesc;
            ClothingExist.Amount = ClothingBsObj.Amount;
            ClothingExist.Flag = ClothingBsObj.Flag;
            NewClothingDA.Update(ClothingExist);
        }

    }
}
