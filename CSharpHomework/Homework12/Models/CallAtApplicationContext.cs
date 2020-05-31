using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Models
{
    public class CallAtApplicationContext:DbContext
    {
        public CallAtApplicationContext(DbContextOptions<CallAtApplicationContext> options)
            : base(options)
        {
            this.Database.EnsureCreated(); //自动建库建表
        }

        public DbSet<CallAtApplicationItem> TodoItems { get; set; }
    }
}
