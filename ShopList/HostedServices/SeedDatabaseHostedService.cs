using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Threading;
using ShopList.Models.Constants;
using ShopList.Models.Database.Entities;

namespace ShopList.HostedServices
{
    public class SeedDatabaseHostedService : IHostedService, IDisposable
    {
        private Timer _timer;
        private IServiceScopeFactory Services { get; }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(async _ => await SeedDatabase(),
                null, TimeSpan.Zero, TimeSpan.FromDays(7));

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return null;
        }

        public void Dispose()
        {
        }


        public SeedDatabaseHostedService(IServiceScopeFactory services)
        {
            Services = services;
        }

        private async Task SeedDatabase()
        {
            using var scope = Services.CreateScope();
            await SeedRoles(scope);
        }

        private async Task SeedRoles(IServiceScope scope)
        {
            using var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<RoleEntity>>();
            List<string> roleList = new()
            {
                UserRoles.administrator,
                UserRoles.moderator,
                UserRoles.normal
            };
            foreach(var role in roleList)
            {
                if(!await roleManager.RoleExistsAsync(role))
                {
                    var result=await roleManager.CreateAsync(new RoleEntity
                    {
                        Name = role
                    });

                }
            }

        }


    }
}
