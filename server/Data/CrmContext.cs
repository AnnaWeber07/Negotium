using System.Reflection;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;

using InDrivoHRM.Models.Crm;

namespace InDrivoHRM.Data
{
  public partial class CrmContext : Microsoft.EntityFrameworkCore.DbContext
  {
    public CrmContext(DbContextOptions<CrmContext> options):base(options)
    {
    }

    public CrmContext()
    {
    }

    partial void OnModelBuilding(ModelBuilder builder);

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<InDrivoHRM.Models.Crm.Opportunity>()
              .HasOne(i => i.Contact)
              .WithMany(i => i.Opportunities)
              .HasForeignKey(i => i.ContactId)
              .HasPrincipalKey(i => i.Id);
        builder.Entity<InDrivoHRM.Models.Crm.Opportunity>()
              .HasOne(i => i.OpportunityStatus)
              .WithMany(i => i.Opportunities)
              .HasForeignKey(i => i.StatusId)
              .HasPrincipalKey(i => i.Id);
        builder.Entity<InDrivoHRM.Models.Crm.Task>()
              .HasOne(i => i.Opportunity)
              .WithMany(i => i.Tasks)
              .HasForeignKey(i => i.OpportunityId)
              .HasPrincipalKey(i => i.Id);
        builder.Entity<InDrivoHRM.Models.Crm.Task>()
              .HasOne(i => i.TaskType)
              .WithMany(i => i.Tasks)
              .HasForeignKey(i => i.TypeId)
              .HasPrincipalKey(i => i.Id);
        builder.Entity<InDrivoHRM.Models.Crm.Task>()
              .HasOne(i => i.TaskStatus)
              .WithMany(i => i.Tasks)
              .HasForeignKey(i => i.StatusId)
              .HasPrincipalKey(i => i.Id);


        builder.Entity<InDrivoHRM.Models.Crm.Opportunity>()
              .Property(p => p.CloseDate)
              .HasColumnType("datetime");

        builder.Entity<InDrivoHRM.Models.Crm.Task>()
              .Property(p => p.DueDate)
              .HasColumnType("datetime");

        builder.Entity<InDrivoHRM.Models.Crm.Contact>()
              .Property(p => p.Id)
              .HasPrecision(10, 0);

        builder.Entity<InDrivoHRM.Models.Crm.Opportunity>()
              .Property(p => p.Id)
              .HasPrecision(10, 0);

        builder.Entity<InDrivoHRM.Models.Crm.Opportunity>()
              .Property(p => p.Amount)
              .HasPrecision(19, 4);

        builder.Entity<InDrivoHRM.Models.Crm.Opportunity>()
              .Property(p => p.ContactId)
              .HasPrecision(10, 0);

        builder.Entity<InDrivoHRM.Models.Crm.Opportunity>()
              .Property(p => p.StatusId)
              .HasPrecision(10, 0);

        builder.Entity<InDrivoHRM.Models.Crm.OpportunityStatus>()
              .Property(p => p.Id)
              .HasPrecision(10, 0);

        builder.Entity<InDrivoHRM.Models.Crm.Task>()
              .Property(p => p.Id)
              .HasPrecision(10, 0);

        builder.Entity<InDrivoHRM.Models.Crm.Task>()
              .Property(p => p.OpportunityId)
              .HasPrecision(10, 0);

        builder.Entity<InDrivoHRM.Models.Crm.Task>()
              .Property(p => p.TypeId)
              .HasPrecision(10, 0);

        builder.Entity<InDrivoHRM.Models.Crm.Task>()
              .Property(p => p.StatusId)
              .HasPrecision(10, 0);

        builder.Entity<InDrivoHRM.Models.Crm.TaskStatus>()
              .Property(p => p.Id)
              .HasPrecision(10, 0);

        builder.Entity<InDrivoHRM.Models.Crm.TaskType>()
              .Property(p => p.Id)
              .HasPrecision(10, 0);
        this.OnModelBuilding(builder);
    }


    public DbSet<InDrivoHRM.Models.Crm.Contact> Contacts
    {
      get;
      set;
    }

    public DbSet<InDrivoHRM.Models.Crm.Opportunity> Opportunities
    {
      get;
      set;
    }

    public DbSet<InDrivoHRM.Models.Crm.OpportunityStatus> OpportunityStatuses
    {
      get;
      set;
    }

    public DbSet<InDrivoHRM.Models.Crm.Task> Tasks
    {
      get;
      set;
    }

    public DbSet<InDrivoHRM.Models.Crm.TaskStatus> TaskStatuses
    {
      get;
      set;
    }

    public DbSet<InDrivoHRM.Models.Crm.TaskType> TaskTypes
    {
      get;
      set;
    }
  }
}
