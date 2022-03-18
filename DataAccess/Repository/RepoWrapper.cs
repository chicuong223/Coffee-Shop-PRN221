using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataObject.Models;
using DataAccess.RepositoryInterface;

namespace DataAccess.Repository
{
    public class RepoWrapper : IRepoWrapper
    {
        private IBaseRepository<BillDetail> _billDetails ;
        private IBaseRepository<Bill> _bills;
        private IBaseRepository<Category> _categories;
        private IBaseRepository<Notification> _notifications;
        private IBaseRepository<NotificationDetail> _notificationDetails;
        private IBaseRepository<Product> _products;
        private IBaseRepository<Staff> _staffs;
        private IBaseRepository<Supplier> _suppliers;
        private IBaseRepository<Supply> _supplies;
        private IBaseRepository<Voucher> _vouchers;

        public IBaseRepository<BillDetail> BillDetails
        {
            get
            {
                if(_billDetails == null)
                {
                    _billDetails = new BillDetailRepository();
                }
                return _billDetails;
            }
        }

        public IBaseRepository<Bill> Bills
        {
            get
            {
                if (_bills == null)
                {
                    _bills = new BillRepository();
                }
                return _bills;
            }
        }

        public IBaseRepository<Category> Categories
        {
            get
            {
                if (_categories == null)
                {
                    _categories = new CategoryRepository();
                }
                return _categories;
            }
        }

        public IBaseRepository<Notification> Notifications
        {
            get
            {
                if (_notifications == null)
                {
                    _notifications = new NotificationRepository();
                }
                return _notifications;
            }
        }

        public IBaseRepository<NotificationDetail> NotificationDetails
        {
            get
            {
                if (_notificationDetails == null)
                {
                    _notificationDetails = new NotificationDetailRepository();
                }
                return _notificationDetails;
            }
        }

        public IBaseRepository<Product> Products
        {
            get
            {
                if (_products == null)
                {
                    _products = new ProductRepository();
                }
                return _products;
            }
        }

        public IBaseRepository<Staff> Staffs
        {
            get
            {
                if (_staffs == null)
                {
                    _staffs = new StaffRepository();
                }
                return _staffs;
            }
        }

        public IBaseRepository<Supplier> Suppliers
        {
            get
            {
                if (_suppliers == null)
                {
                    _suppliers = new SupplierRepository();
                }
                return _suppliers;
            }
        }

        public IBaseRepository<Supply> Supplies
        {
            get
            {
                if (_supplies == null)
                {
                    _supplies = new SupplyRepository();
                }
                return _supplies;
            }
        }

        public IBaseRepository<Voucher> Vouchers => throw new NotImplementedException();
    }
}
