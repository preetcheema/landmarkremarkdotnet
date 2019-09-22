using System;
using System.Collections.Generic;
using System.Net.Mime;
using LandmarkRemark.BusinessLogic.Tests.Infrastructure;
using LandmarkRemark.Common;
using LandmarkRemark.Domain.Entities;
using LandmarkRemark.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using NetTopologySuite.Geometries;

namespace LandmarkRemark.BusinessLogic.Tests
{
    /// <summary>
    /// Base class for all tests, create unique context.
    /// I could also used IOC to inject our in memory context and use _mediatr.Send(some command) instead of doing someCOmmandHandler.Send(LandmarkContext)
    /// But as we have very few dependencies, I am choosing to not use IOC in tests
    /// </summary>
    public  class TestBaseFixture : IDisposable
    {
        public LandmarkRemarkContext LandmarkContext { get; set; }
        public ITimeProvider DateTimeProvider { get; set; }


        public TestBaseFixture()
        {
            var options = new DbContextOptionsBuilder<LandmarkRemarkContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            LandmarkContext = new LandmarkRemarkContext(options);
            DateTimeProvider = new FakeDatetimeProvider();

            CreateDataForTests(LandmarkContext);
        }


        private void CreateDataForTests(LandmarkRemarkContext context)
        {
            var users = new[]
            {
                new User
                {
                    FirstName = "Steve", LastName = "Smith", UserName = "steve.smith", UserNotes = new List<Note>
                    {
                        new Note {Text = "Great place to visit", Location = new Point(145.1257112, -37.9131133), AddedOn = DateTimeProvider.Now()}
                    }
                },
                new User
                {
                    FirstName = "Tom", LastName = "Cruise", UserName = "tom.cruise", UserNotes = new List<Note>
                    {
                        new Note
                        {
                            Text = "Great place to visit", Location = new Point(153.0272569, -27.4640302), AddedOn = DateTimeProvider.Now()
                        },
                        new Note {Text = "Great food", Location = new Point(152.9986068, -27.4522202), AddedOn = DateTimeProvider.Now()}
                    }
                },

                new User {FirstName = "Peter", LastName = "Mccormack", UserName = "peter.mccormack"}
            };

            foreach (var user in users)
            {
                (byte[] passwordHash, byte[] passwordSalt) = CreatePasswordHash($"{user.FirstName}{user.LastName}");
                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;
            }

            context.Users.AddRange(users);
            context.SaveChanges();
        }


        private (byte[]passwordHash, byte[]passwordSalt) CreatePasswordHash(string password)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                var passwordSalt = hmac.Key;
                var passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return (passwordHash, passwordSalt);
            }
        }

        public void Dispose()
        {
            LandmarkContext.Database.EnsureDeleted();

            LandmarkContext.Dispose();
        }
    }

  
}