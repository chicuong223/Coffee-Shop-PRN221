using DataAccess.RepositoryInterface;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Middlewares
{
    public class BillMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IRepoWrapper _repo;

        public BillMiddleware(RequestDelegate next, IRepoWrapper repo)
        {
            _next = next;
            _repo = repo;
        }

        public async Task Invoke(HttpContext context)
        {
            var bills = (await _repo.Bills.GetAll(b => b.Status.Value == false)).ToList();
            if(bills.Count() >  0)
			{
                foreach (var bill in bills)
                {
                    var time = DateTime.Now - bill.BillDate;
                    var minutes = time.Value.TotalMinutes;
                    if (minutes >= 30)
                    {
                        var billDetails = await _repo.BillDetails.GetAll(b => b.BillId == bill.Id);
                        foreach (var detail in billDetails)
                        {
                            await _repo.BillDetails.Delete((detail.BillId, detail.ProductId));
                        }
                        await _repo.Bills.Delete(bill.Id);
                    }
                }
            }
            await _next(context);
        }
    }
}
