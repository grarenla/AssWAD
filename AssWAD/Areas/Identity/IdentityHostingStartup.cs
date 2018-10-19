﻿using System;
using AssWAD.Areas.Identity.Data;
using AssWAD.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(AssWAD.Areas.Identity.IdentityHostingStartup))]
namespace AssWAD.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<AssWADContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("AssWADContextConnection")));

            });
        }
    }
}