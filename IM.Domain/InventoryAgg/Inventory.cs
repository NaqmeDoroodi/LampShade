using System.Collections.Generic;
using System.Linq;
using Framework.Domain;

namespace IM.Domain.InventoryAgg
{
    public class Inventory : BaseEntity
    {
        public long ProductId { get; private set; }
        public double UnitePrice { get; private set; }
        public bool IsInStock { get; private set; }
        public List<InventoryOperation> Operations { get; private set; }


        public Inventory(long productId, double unitePrice)
        {
            ProductId = productId;
            UnitePrice = unitePrice;
            IsInStock = false;
        }
        public void Edit(long productId, double unitePrice)
        {
            ProductId = productId;
            UnitePrice = unitePrice;
        }

        public long CalcCurrentCnt()
        {
            var incAmount = Operations.Where(x => x.Operation).Sum(x => x.Count);
            var decAmount = Operations.Where(x => !x.Operation).Sum(x => x.Count);

            return incAmount - decAmount;
        }

        public void Increase(long count, string desc, long operatorId)
        {
            var currentCnt = CalcCurrentCnt() + count;
            var newOperation = new InventoryOperation(true, count, currentCnt, desc, operatorId, 0, Id);
            Operations.Add(newOperation);
            IsInStock = currentCnt > 0;
        }

        public void Reduce(long count, string desc, long operatorId, long orderId)
        {
            var currentCnt = CalcCurrentCnt() - count;
            var newOperation = new InventoryOperation(false, count, currentCnt, desc, operatorId, orderId, Id);
            Operations.Add(newOperation);
            IsInStock = currentCnt > 0;
        }

    }
}