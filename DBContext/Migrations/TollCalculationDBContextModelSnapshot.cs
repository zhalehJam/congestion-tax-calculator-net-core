﻿// <auto-generated />
using System;
using DBContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DBContext.Migrations
{
    [DbContext(typeof(TollCalculationDBContext))]
    partial class TollCalculationDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("DBContext.Models.SpecialTimeTollFee", b =>
                {
                    b.Property<TimeSpan>("FromTime")
                        .HasColumnType("time");

                    b.Property<TimeSpan>("ToTime")
                        .HasColumnType("time");

                    b.Property<int>("TollFee")
                        .HasColumnType("int");

                    b.ToTable("SpecialTimeTollFees");
                });

            modelBuilder.Entity("DBContext.Models.GetyearDayType", b =>
                {
                    b.Property<DateTime>("DateOfYear")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsfreeDate")
                        .HasColumnType("bit");

                    b.HasKey("DateOfYear");

                    b.ToTable("GetyearDayTypes", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}
