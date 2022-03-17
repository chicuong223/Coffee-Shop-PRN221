using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.RepositoryInterface
{
    public interface IRepoWrapper
    {
        IBaseRepository<BillDetail> BillDetails { get; }
        IBaseRepository<Bill> Bills { get; }
        IBaseRepository<Category> Categories { get; }
        IBaseRepository<Notification> Notifications { get; }
        IBaseRepository<NotificationDetail> NotificationDetails { get; }
        IBaseRepository<Product> Products { get; }
        IBaseRepository<Staff> Staffs { get; }
        IBaseRepository<Supplier> Suppliers { get; }
        IBaseRepository<Supply> Supplies { get; }
        IBaseRepository<Voucher> Vouchers { get; }
    }
}
