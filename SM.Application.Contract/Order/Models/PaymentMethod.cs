using System.Collections.Generic;
using System.Linq;

namespace SM.Application.Contract.Order.Models
{
    public class PaymentMethod
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Desc { get; private set; }


        private PaymentMethod(int id, string name, string desc)
        {
            Id = id;
            Name = name;
            Desc = desc;
        }


        public static List<PaymentMethod> GetList()
        {
            return new List<PaymentMethod>
            {
                new PaymentMethod(1,"پرداخت اینترنتی","با انتخاب این روش پرداخت شما به صفحه پرداخت اینترنتی وصل شده و پس از پرداخت موفق، سفارش شما ثبت و ارسال میشود."),
                new PaymentMethod(2,"پرداخت نقدی","در این روش شما میتوانید کالای مورد نظر خود را سفارش داده و پس از ارسال، پرداخت را در منزل انجام دهید."),
            };
        }


        public static PaymentMethod GetMethodBy(int id)
        {
            return GetList().FirstOrDefault(x => x.Id == id);
        }
    }
}
