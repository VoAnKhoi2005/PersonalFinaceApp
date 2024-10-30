﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PersonalFinanceApp.Database;

#nullable disable

namespace PersonalFinanceApp.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20241030030714_AddSoftDeleteExpense")]
    partial class AddSoftDeleteExpense
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.10");

            modelBuilder.Entity("PersonalFinanceApp.Model.Category", b =>
                {
                    b.Property<int>("CategoryID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("ExBMonth")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ExBYear")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Resources")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<int>("UserID")
                        .HasColumnType("INTEGER");

                    b.HasKey("CategoryID");

                    b.HasIndex("ExBMonth", "ExBYear", "UserID");

                    b.ToTable("CATEGORY");
                });

            modelBuilder.Entity("PersonalFinanceApp.Model.Expense", b =>
                {
                    b.Property<int>("ExpenseID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<long>("Amount")
                        .HasColumnType("INTEGER");

                    b.Property<int>("CategoryID")
                        .HasColumnType("INTEGER");

                    b.Property<DateOnly>("Date")
                        .HasColumnType("TEXT");

                    b.Property<bool>("Deleted")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<int>("ExBMonth")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ExBYear")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<bool>("Recurring")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Resources")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("TimeAdded")
                        .HasColumnType("TEXT");

                    b.Property<int>("UserID")
                        .HasColumnType("INTEGER");

                    b.HasKey("ExpenseID");

                    b.HasIndex("CategoryID");

                    b.HasIndex("Date");

                    b.HasIndex("TimeAdded")
                        .IsUnique();

                    b.HasIndex("ExBMonth", "ExBYear", "UserID");

                    b.ToTable("EXPENSE", t =>
                        {
                            t.HasCheckConstraint("CK_Amount", "[Amount] > 0");

                            t.HasCheckConstraint("CK_Deleted", "[Deleted] IN (0, 1)");

                            t.HasCheckConstraint("CK_Recurring", "[Recurring] IN (0, 1)");
                        });
                });

            modelBuilder.Entity("PersonalFinanceApp.Model.ExpensesBook", b =>
                {
                    b.Property<int>("Month")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Year")
                        .HasColumnType("INTEGER");

                    b.Property<int>("UserID")
                        .HasColumnType("INTEGER");

                    b.Property<long>("Budget")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Resources")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<long>("Spending")
                        .HasColumnType("INTEGER");

                    b.HasKey("Month", "Year", "UserID");

                    b.HasIndex("UserID");

                    b.ToTable("EXPENSESBOOK", t =>
                        {
                            t.HasCheckConstraint("CK_Budget", "[Budget] >= 0");

                            t.HasCheckConstraint("CK_Month", "[Month] >= 1 AND [Month] <= 12");

                            t.HasCheckConstraint("CK_Spending", "[Spending] >= 0");

                            t.HasCheckConstraint("CK_Year", "[Year] >= 0");
                        });
                });

            modelBuilder.Entity("PersonalFinanceApp.Model.Goal", b =>
                {
                    b.Property<int>("GoalID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<long>("GoalAmount")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Resources")
                        .HasColumnType("TEXT");

                    b.Property<long>("Target")
                        .HasColumnType("INTEGER");

                    b.Property<int>("UserID")
                        .HasColumnType("INTEGER");

                    b.HasKey("GoalID");

                    b.HasIndex("UserID");

                    b.ToTable("GOAL");
                });

            modelBuilder.Entity("PersonalFinanceApp.Model.GoalHistory", b =>
                {
                    b.Property<int>("GoalID")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("TimeAdded")
                        .HasColumnType("TEXT");

                    b.Property<long>("Amount")
                        .HasColumnType("INTEGER");

                    b.HasKey("GoalID", "TimeAdded");

                    b.ToTable("GOALHISTORY");
                });

            modelBuilder.Entity("PersonalFinanceApp.Model.RecurringDetail", b =>
                {
                    b.Property<int>("ExpenseID")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Frequency")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Interval")
                        .HasColumnType("INTEGER");

                    b.Property<DateOnly>("StarDate")
                        .HasColumnType("TEXT");

                    b.HasKey("ExpenseID");

                    b.ToTable("RECURRINGDETAIL", t =>
                        {
                            t.HasCheckConstraint("CK_Frequency", "[Frequency] IN ('Daily', 'Weekly', 'Monthly', 'Yearly')");
                        });
                });

            modelBuilder.Entity("PersonalFinanceApp.Model.User", b =>
                {
                    b.Property<int>("UserID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<long>("DefaultBudget")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("TEXT");

                    b.Property<string>("Resources")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<long>("Saving")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("TEXT");

                    b.HasKey("UserID");

                    b.HasIndex("Username")
                        .IsUnique();

                    b.ToTable("USER", t =>
                        {
                            t.HasCheckConstraint("CK_DefaultBudget", "[DefaultBudget] >= 0");

                            t.HasCheckConstraint("CK_Saving", "[Saving] >= 0");
                        });
                });

            modelBuilder.Entity("PersonalFinanceApp.Model.Category", b =>
                {
                    b.HasOne("PersonalFinanceApp.Model.ExpensesBook", "ExpensesBook")
                        .WithMany("Categories")
                        .HasForeignKey("ExBMonth", "ExBYear", "UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ExpensesBook");
                });

            modelBuilder.Entity("PersonalFinanceApp.Model.Expense", b =>
                {
                    b.HasOne("PersonalFinanceApp.Model.Category", "Category")
                        .WithMany("Expenses")
                        .HasForeignKey("CategoryID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PersonalFinanceApp.Model.ExpensesBook", "ExpensesBook")
                        .WithMany("Expenses")
                        .HasForeignKey("ExBMonth", "ExBYear", "UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("ExpensesBook");
                });

            modelBuilder.Entity("PersonalFinanceApp.Model.ExpensesBook", b =>
                {
                    b.HasOne("PersonalFinanceApp.Model.User", "User")
                        .WithMany("ExpensesBooks")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("PersonalFinanceApp.Model.Goal", b =>
                {
                    b.HasOne("PersonalFinanceApp.Model.User", "User")
                        .WithMany("Goals")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("PersonalFinanceApp.Model.GoalHistory", b =>
                {
                    b.HasOne("PersonalFinanceApp.Model.Goal", "Goal")
                        .WithMany("GoalHistories")
                        .HasForeignKey("GoalID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Goal");
                });

            modelBuilder.Entity("PersonalFinanceApp.Model.RecurringDetail", b =>
                {
                    b.HasOne("PersonalFinanceApp.Model.Expense", "Expense")
                        .WithOne("RecurringDetail")
                        .HasForeignKey("PersonalFinanceApp.Model.RecurringDetail", "ExpenseID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Expense");
                });

            modelBuilder.Entity("PersonalFinanceApp.Model.Category", b =>
                {
                    b.Navigation("Expenses");
                });

            modelBuilder.Entity("PersonalFinanceApp.Model.Expense", b =>
                {
                    b.Navigation("RecurringDetail")
                        .IsRequired();
                });

            modelBuilder.Entity("PersonalFinanceApp.Model.ExpensesBook", b =>
                {
                    b.Navigation("Categories");

                    b.Navigation("Expenses");
                });

            modelBuilder.Entity("PersonalFinanceApp.Model.Goal", b =>
                {
                    b.Navigation("GoalHistories");
                });

            modelBuilder.Entity("PersonalFinanceApp.Model.User", b =>
                {
                    b.Navigation("ExpensesBooks");

                    b.Navigation("Goals");
                });
#pragma warning restore 612, 618
        }
    }
}
