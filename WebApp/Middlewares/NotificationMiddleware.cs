using DataAccess.RepositoryInterface;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace WebApp.Middlewares
{
    public class NotificationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IRepoWrapper _repo;
        public NotificationMiddleware(RequestDelegate next, IRepoWrapper repo)
        {
            _next = next;
            _repo = repo;
        }
        public async Task Invoke(HttpContext context)
        {
            var notifications = await _repo.Notifications.GetAll(noti => noti.IsSent == false);
            foreach (var notification in notifications)
            {
                if(notification.IsSent == false)
                {
                    var time = DateTime.Now - notification.NotificationDate;
                    var minutes = time.Value.TotalMinutes;
                    if(minutes >= 30)
                    {
                        var details = await _repo.NotificationDetails.GetAll(detail => detail.NotificationId == notification.Id);
                        foreach(var detail in details)
                        {
                            await _repo.NotificationDetails.Delete((detail.NotificationId, detail.ProductId));
                        }
                        await _repo.Notifications.Delete(notification.Id);
                    }
                }
            }
            await _next(context);
        }
    }
}
