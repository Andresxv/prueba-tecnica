using ApiPruebaBackend.Models;
using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Options;
namespace ApiPruebaBackend.Context
{
    public partial class TicketContext : DbContext
    {
        public TicketContext(DbContextOptions<TicketContext> options) : base(options){}
        public DbSet<Tickets> Tickets { get; set; }
    }
}
