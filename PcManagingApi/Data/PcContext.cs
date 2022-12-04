using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using PcManagingApi.Models;

namespace PcManagingApi.Data;

public class PcContext : DbContext
{
    public PcContext(DbContextOptions<PcContext> opts) : base(opts)
    {}

    public DbSet<Pc> Pcs { get; set; }
}
