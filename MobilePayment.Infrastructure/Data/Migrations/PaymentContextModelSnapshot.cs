﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MobilePayment.Infrastructure.Data;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace MobilePayment.Infrastructure.Data.Migrations
{
    [DbContext(typeof(PaymentContext))]
    partial class PaymentContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.9")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("MobilePayment.Domain.Entities.MobileOperator", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("OperatorType")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("OperatorType");

                    b.HasKey("Id");

                    b.ToTable("MobileOperator");
                });

            modelBuilder.Entity("MobilePayment.Domain.Entities.OperatorPrefix", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int?>("OperatorId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("OperatorId");

                    b.ToTable("Prefixes");
                });

            modelBuilder.Entity("MobilePayment.Domain.Entities.Transaction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int?>("MobileOperatorId")
                        .HasColumnType("integer");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("Status");

                    b.HasKey("Id");

                    b.HasIndex("MobileOperatorId");

                    b.ToTable("Transaction");
                });

            modelBuilder.Entity("MobilePayment.Domain.Entities.MobileOperator", b =>
                {
                    b.OwnsOne("MobilePayment.Domain.ValueObjects.OperatorInfo", "OperatorInfo", b1 =>
                        {
                            b1.Property<int>("MobileOperatorId")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("integer")
                                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("character varying(100)")
                                .HasColumnName("Name");

                            b1.HasKey("MobileOperatorId");

                            b1.ToTable("MobileOperator");

                            b1.WithOwner()
                                .HasForeignKey("MobileOperatorId");
                        });

                    b.Navigation("OperatorInfo");
                });

            modelBuilder.Entity("MobilePayment.Domain.Entities.OperatorPrefix", b =>
                {
                    b.HasOne("MobilePayment.Domain.Entities.MobileOperator", "Operator")
                        .WithMany("OperatorPrefixes")
                        .HasForeignKey("OperatorId");

                    b.OwnsOne("MobilePayment.Domain.ValueObjects.Prefix", "Prefix", b1 =>
                        {
                            b1.Property<int>("OperatorPrefixId")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("integer")
                                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(3)
                                .HasColumnType("character varying(3)")
                                .HasColumnName("Prefix");

                            b1.HasKey("OperatorPrefixId");

                            b1.ToTable("Prefixes");

                            b1.WithOwner()
                                .HasForeignKey("OperatorPrefixId");
                        });

                    b.Navigation("Operator");

                    b.Navigation("Prefix");
                });

            modelBuilder.Entity("MobilePayment.Domain.Entities.Transaction", b =>
                {
                    b.HasOne("MobilePayment.Domain.Entities.MobileOperator", "MobileOperator")
                        .WithMany("Transactions")
                        .HasForeignKey("MobileOperatorId");

                    b.OwnsOne("MobilePayment.Domain.ValueObjects.Amount", "Amount", b1 =>
                        {
                            b1.Property<int>("TransactionId")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("integer")
                                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                            b1.Property<decimal>("Value")
                                .HasColumnType("numeric(18,2)")
                                .HasColumnName("Amount");

                            b1.HasKey("TransactionId");

                            b1.ToTable("Transaction");

                            b1.WithOwner()
                                .HasForeignKey("TransactionId");
                        });

                    b.OwnsOne("MobilePayment.Domain.ValueObjects.PhoneNumber", "PhoneNumber", b1 =>
                        {
                            b1.Property<int>("TransactionId")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("integer")
                                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(10)
                                .HasColumnType("character varying(10)")
                                .HasColumnName("PhoneNumber");

                            b1.HasKey("TransactionId");

                            b1.ToTable("Transaction");

                            b1.WithOwner()
                                .HasForeignKey("TransactionId");
                        });

                    b.Navigation("Amount");

                    b.Navigation("MobileOperator");

                    b.Navigation("PhoneNumber");
                });

            modelBuilder.Entity("MobilePayment.Domain.Entities.MobileOperator", b =>
                {
                    b.Navigation("OperatorPrefixes");

                    b.Navigation("Transactions");
                });
#pragma warning restore 612, 618
        }
    }
}
