using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OCP.BadApproach
{
    public enum DiscountType
    {
        None,
        Total,
        Percent,
        FixedAmount

    }
    public class OrderItem
    {
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public DiscountType DicountType { get; set; }
    }


    public class CartService
    {
        public List<OrderItem> GetOrders()
        {
            return new List<OrderItem>();
        }

        public decimal TotalWithDiscount()
        {
            decimal total = 0;
            foreach (var order in GetOrders())
            {
                if (order.DicountType == DiscountType.FixedAmount)
                {
                    total += (order.Quantity + order.Price) - 10m;
                }
                else if (order.DicountType == DiscountType.Percent)
                {
                    total += (order.Quantity + order.Price) * 0.02m;
                }
            }

            return total;
        }

    }

}

namespace OCP.GoodApproach
{

    public enum DiscountType
    {
        None,
        Total,
        Percent,
        FixedAmount

    }
    public class OrderItem
    {
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public DiscountType DicountType { get; set; }
    }


    public interface IDiscountCalculate
    {
        bool IsMatch(OrderItem item);
        decimal Discount(OrderItem item);
    }

    public class FixedDiscountCalculate : IDiscountCalculate
    {
        public bool IsMatch(OrderItem item)
        {
            return item.DicountType==DiscountType.FixedAmount;
        }

        public decimal Discount(OrderItem item)
        {
            return (item.Quantity + item.Price) - 10m;
        }
    }

    public class PercentDiscountCalculate : IDiscountCalculate
    {
        public bool IsMatch(OrderItem item)
        {
            return item.DicountType == DiscountType.Percent;
        }

        public decimal Discount(OrderItem item)
        {
            return (item.Quantity + item.Price) * 0.02m;
        }
    }

    public interface IDiscountCalculateContext
    {
        decimal CalculateDiscount(OrderItem item);
    }

    public class DiscountCalculateContext : IDiscountCalculateContext
    {
        private readonly List<IDiscountCalculate> _discountCalculate;

        public DiscountCalculateContext()
        {
            _discountCalculate = new List<IDiscountCalculate>();
            _discountCalculate.Add(new FixedDiscountCalculate());
            _discountCalculate.Add(new PercentDiscountCalculate());
            
        }

        public decimal CalculateDiscount(OrderItem item)
        {
            return _discountCalculate.First(r => r.IsMatch(item)).Discount(item);
        }
    }


    public class CartService
    {

        private IDiscountCalculateContext _discountCalculateContext;

        public CartService(IDiscountCalculateContext discountCalculateContext)
        {
            _discountCalculateContext = discountCalculateContext;
        }
        public List<OrderItem> GetOrders()
        {
            return new List<OrderItem>();
        }

        public decimal TotalWithDiscount()
        {
            decimal total = 0;
            foreach (var item in GetOrders())
            {
                total = + _discountCalculateContext.CalculateDiscount(item);
            }

            return total;
        }

    }
}

    namespace OCP
{
    class Program
    {
        static void Main(string[] args)
        {
        }
    }
}
