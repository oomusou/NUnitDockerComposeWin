﻿// <auto-generated />
using ClassLib;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace ClassLib.Migrations
{
    [DbContext(typeof(CrmDbContext))]
    partial class CrmDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.1.1-rtm-30846")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("ClassLib.Customer", b =>
                {
                    b.Property<int>("CustomerId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Age");

                    b.Property<string>("Name");

                    b.HasKey("CustomerId");

                    b.ToTable("Customers");

                    b.HasData(
                        new { CustomerId = 1, Age = 18, Name = "Sam" },
                        new { CustomerId = 2, Age = 19, Name = "Kevin" },
                        new { CustomerId = 3, Age = 20, Name = "John" }
                    );
                });
#pragma warning restore 612, 618
        }
    }
}