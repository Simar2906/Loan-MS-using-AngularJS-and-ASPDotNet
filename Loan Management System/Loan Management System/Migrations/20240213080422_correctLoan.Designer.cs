﻿// <auto-generated />
using System;
using Loan_Management_System.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Loan_Management_System.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240213080422_correctLoan")]
    partial class correctLoan
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Loan_Management_System.DTOs.AppliedLoan", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("AppliedAmount")
                        .HasColumnType("integer");

                    b.Property<float>("AppliedRate")
                        .HasColumnType("real");

                    b.Property<DateTime>("DateApplied")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("LoanId")
                        .HasColumnType("integer");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<int>("TermLength")
                        .HasColumnType("integer");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("LoanId");

                    b.HasIndex("UserId");

                    b.ToTable("AppliedLoans");
                });

            modelBuilder.Entity("Loan_Management_System.DTOs.Loan", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("InterestRates")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("LoanAmount")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Logo")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("MinCreditScore")
                        .HasColumnType("integer");

                    b.Property<decimal>("ProcessingFee")
                        .HasColumnType("numeric");

                    b.Property<decimal>("TermLength")
                        .HasColumnType("numeric");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Loans");
                });

            modelBuilder.Entity("Loan_Management_System.DTOs.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Designation")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Employer")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal>("Salary")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<string>("UserPic")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Loan_Management_System.DTOs.AppliedLoan", b =>
                {
                    b.HasOne("Loan_Management_System.DTOs.Loan", "Loan")
                        .WithMany("AppliedLoans")
                        .HasForeignKey("LoanId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Loan_Management_System.DTOs.User", "User")
                        .WithMany("AppliedLoans")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Loan");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Loan_Management_System.DTOs.Loan", b =>
                {
                    b.Navigation("AppliedLoans");
                });

            modelBuilder.Entity("Loan_Management_System.DTOs.User", b =>
                {
                    b.Navigation("AppliedLoans");
                });
#pragma warning restore 612, 618
        }
    }
}