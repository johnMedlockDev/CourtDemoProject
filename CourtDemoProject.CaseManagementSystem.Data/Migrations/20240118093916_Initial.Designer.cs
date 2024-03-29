﻿// <auto-generated />
using System;
using CourtDemoProject.CaseManagementSystem.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CourtDemoProject.CaseManagementSystem.Data.Migrations
{
    [DbContext(typeof(CaseManagementSystemDbContext))]
    [Migration("20240118093916_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CourtDemoProject.CaseManagementSystem.Data.Entities.CaseDetailEntity", b =>
                {
                    b.Property<Guid>("CaseDetailId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CaseDetailEntryDateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("CaseEntityCaseId")
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("DocketDetail")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("DocumentUri")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CaseDetailId");

                    b.HasIndex("CaseEntityCaseId");

                    b.ToTable("CaseDetails");
                });

            modelBuilder.Entity("CourtDemoProject.CaseManagementSystem.Data.Entities.CaseEntity", b =>
                {
                    b.Property<string>("CaseId")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("CaseStatus")
                        .HasColumnType("int");

                    b.Property<int>("CaseType")
                        .HasColumnType("int");

                    b.Property<string>("CourtDates")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CourtName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateOnly>("DateOfOffense")
                        .HasColumnType("date");

                    b.Property<int>("Plea")
                        .HasColumnType("int");

                    b.Property<int>("Verdict")
                        .HasColumnType("int");

                    b.HasKey("CaseId");

                    b.ToTable("Cases");
                });

            modelBuilder.Entity("CourtDemoProject.CaseManagementSystem.Data.Entities.CaseParticipantEntity", b =>
                {
                    b.Property<Guid>("CaseParticipantEntityId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CaseEntityCaseId")
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("CaseParticipantFirstName")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.Property<string>("CaseParticipantLastName")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.Property<string>("CaseParticipantMiddleName")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.Property<int>("CaseParticipantType")
                        .HasColumnType("int");

                    b.HasKey("CaseParticipantEntityId");

                    b.HasIndex("CaseEntityCaseId");

                    b.ToTable("CaseParticipants");
                });

            modelBuilder.Entity("CourtDemoProject.CaseManagementSystem.Data.Entities.ChargeEntity", b =>
                {
                    b.Property<Guid>("ChargeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CaseEntityCaseId")
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("ChargeCode")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.Property<string>("ChargeName")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.Property<int>("ChargeType")
                        .HasColumnType("int");

                    b.Property<double>("FineAmount")
                        .HasColumnType("float");

                    b.Property<int>("JudgementType")
                        .HasColumnType("int");

                    b.Property<int>("SentenceLengthIndays")
                        .HasColumnType("int");

                    b.HasKey("ChargeId");

                    b.HasIndex("CaseEntityCaseId");

                    b.ToTable("Charges");
                });

            modelBuilder.Entity("CourtDemoProject.CaseManagementSystem.Data.Entities.CaseDetailEntity", b =>
                {
                    b.HasOne("CourtDemoProject.CaseManagementSystem.Data.Entities.CaseEntity", null)
                        .WithMany("CaseDetails")
                        .HasForeignKey("CaseEntityCaseId");
                });

            modelBuilder.Entity("CourtDemoProject.CaseManagementSystem.Data.Entities.CaseParticipantEntity", b =>
                {
                    b.HasOne("CourtDemoProject.CaseManagementSystem.Data.Entities.CaseEntity", null)
                        .WithMany("CaseParticipants")
                        .HasForeignKey("CaseEntityCaseId");
                });

            modelBuilder.Entity("CourtDemoProject.CaseManagementSystem.Data.Entities.ChargeEntity", b =>
                {
                    b.HasOne("CourtDemoProject.CaseManagementSystem.Data.Entities.CaseEntity", null)
                        .WithMany("Charges")
                        .HasForeignKey("CaseEntityCaseId");
                });

            modelBuilder.Entity("CourtDemoProject.CaseManagementSystem.Data.Entities.CaseEntity", b =>
                {
                    b.Navigation("CaseDetails");

                    b.Navigation("CaseParticipants");

                    b.Navigation("Charges");
                });
#pragma warning restore 612, 618
        }
    }
}
