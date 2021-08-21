using Microsoft.EntityFrameworkCore;
using PessengerApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PessengerApp.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Pessenger>().HasData(
                new Pessenger { Status= Status.Online, Country= Country.Turkey, Id = 403101, Name = "Kerem", Surname = "Tunç", Gender = Gender.Man, DocumentType = DocumentType.Pasaport, DocumentNo = "PS01203", IssueDate = new DateTime(2018, 3, 21) },
                new Pessenger { Status = Status.Online, Country = Country.Turkey, Id = 403102, Name = "Emine", Surname = "Özdemir", Gender = Gender.Woman, DocumentType = DocumentType.Pasaport, DocumentNo = "PS01415", IssueDate = new DateTime(2015, 9, 27) },
                new Pessenger { Status = Status.Online, Country = Country.USA, Id = 403103, Name = "Yasin", Surname = "Şahin", Gender = Gender.Man, DocumentType = DocumentType.TravelDocument, DocumentNo = "TD03156", IssueDate = new DateTime(2021, 6, 13) },
                new Pessenger { Status = Status.Online, Country = Country.USA, Id = 403104, Name = "Erva", Surname = "Tunç", Gender = Gender.Woman, DocumentType = DocumentType.Visa, DocumentNo = "VS202113", IssueDate = new DateTime(2021, 7, 16) },
                new Pessenger { Status = Status.Offline, Country = Country.Turkey, Id = 403105, Name = "Ayşe", Surname = "Uzun", Gender = Gender.Woman, DocumentType = DocumentType.Visa, DocumentNo = "VS202147", IssueDate = new DateTime(2021, 2, 28) }
            );
        }
    }
}
