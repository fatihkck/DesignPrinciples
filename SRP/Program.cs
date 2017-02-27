#region Bad Approach
namespace SRP.BadApproach
{


    public class OrderService
    {
        public void Create()
        {
            //Create

            NotifyCustomer();

            ErpService erpService = new ErpService();
            erpService.Save();
        }

        public void NotifyCustomer()
        {
            EmalService emailService = new EmalService();
            emailService.Send();
        }
    }

    public class EmalService
    {
        public void Send()
        {
            
        }
    }

    public class ErpService
    {
        public void Save()
        {

        }
    }


}
#endregion


#region Bad Approach
namespace SRP.GoodApproach
{
    public class OrderService
    {
        public virtual void Create()
        {
            //Create

        }

    }

    public class OrderOnlineService:OrderService
    {
        IEmalService _emailService;
        IErpService _erpService;
        public OrderOnlineService(
            IEmalService emailService,
            IErpService erpService
            )
        {
            _emailService = emailService;
            _erpService = erpService;
        }

        public override void Create()
        {
            _emailService.Send();
            _erpService.Save();
            base.Create();
        }
    }

    public interface IEmalService
    {
        void Send();
    }

    public interface IErpService
    {
        void Save();
    }


}
#endregion

namespace SRP
{


    class Program
    {
        static void Main(string[] args)
        {
        }
    }
}
