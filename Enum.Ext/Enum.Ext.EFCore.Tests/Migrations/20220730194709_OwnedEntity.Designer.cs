﻿// <auto-generated />
using Enum.Ext.EFCore.Tests.DbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Enum.Ext.EFCore.Tests.Migrations
{
    [DbContext(typeof(TestDbContext))]
    [Migration("20220730194709_OwnedEntity")]
    partial class OwnedEntity
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.0");

            modelBuilder.Entity("Enum.Ext.EFCore.Tests.DbContext.Entities.EntityWithOwnedProperty", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("EntitiesWithOwnedProperties");
                });

            modelBuilder.Entity("Enum.Ext.EFCore.Tests.DbContext.Entities.SomeEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("Weekday")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("SomeEntities");
                });

            modelBuilder.Entity("Enum.Ext.EFCore.Tests.DbContext.Entities.EntityWithOwnedProperty", b =>
                {
                    b.OwnsOne("Enum.Ext.EFCore.Tests.DbContext.Entities.OwnedStuff", "OwnedStuff", b1 =>
                        {
                            b1.Property<int>("EntityWithOwnedPropertyId")
                                .HasColumnType("INTEGER");

                            b1.Property<int?>("Weekday")
                                .HasColumnType("INTEGER");

                            b1.HasKey("EntityWithOwnedPropertyId");

                            b1.ToTable("EntitiesWithOwnedProperties");

                            b1.WithOwner()
                                .HasForeignKey("EntityWithOwnedPropertyId");
                        });

                    b.Navigation("OwnedStuff")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
