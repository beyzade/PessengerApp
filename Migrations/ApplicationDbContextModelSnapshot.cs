// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PessengerApp.Data;

namespace PessengerApp.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.18")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("PessengerApp.Models.Pessenger", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Country")
                        .HasColumnType("int");

                    b.Property<string>("DocumentNo")
                        .IsRequired()
                        .HasColumnType("nvarchar(20)")
                        .HasMaxLength(20);

                    b.Property<int>("DocumentType")
                        .HasColumnType("int");

                    b.Property<int>("Gender")
                        .HasColumnType("int");

                    b.Property<DateTime>("IssueDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.ToTable("Pessengers");

                    b.HasData(
                        new
                        {
                            Id = 403101,
                            Country = 2,
                            DocumentNo = "PS01203",
                            DocumentType = 0,
                            Gender = 0,
                            IssueDate = new DateTime(2018, 3, 21, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Kerem",
                            Status = 0,
                            Surname = "Tunç"
                        },
                        new
                        {
                            Id = 403102,
                            Country = 2,
                            DocumentNo = "PS01415",
                            DocumentType = 0,
                            Gender = 1,
                            IssueDate = new DateTime(2015, 9, 27, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Emine",
                            Status = 0,
                            Surname = "Özdemir"
                        },
                        new
                        {
                            Id = 403103,
                            Country = 1,
                            DocumentNo = "TD03156",
                            DocumentType = 2,
                            Gender = 0,
                            IssueDate = new DateTime(2021, 6, 13, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Yasin",
                            Status = 0,
                            Surname = "Şahin"
                        },
                        new
                        {
                            Id = 403104,
                            Country = 1,
                            DocumentNo = "VS202113",
                            DocumentType = 1,
                            Gender = 1,
                            IssueDate = new DateTime(2021, 7, 16, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Erva",
                            Status = 0,
                            Surname = "Tunç"
                        },
                        new
                        {
                            Id = 403105,
                            Country = 2,
                            DocumentNo = "VS202147",
                            DocumentType = 1,
                            Gender = 1,
                            IssueDate = new DateTime(2021, 2, 28, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Ayşe",
                            Status = 1,
                            Surname = "Uzun"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
