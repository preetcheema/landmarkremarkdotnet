using System;
using System.Collections.Generic;
using System.Linq;
using LandmarkRemark.Domain.Entities;
using NetTopologySuite.Geometries;

namespace LandmarkRemark.Persistence
{
    public class LandmarkRemarkSeedDataCreator
    {
        public void CreateData(LandmarkRemarkContext context)
        {
            context.Database.EnsureCreated();
            if (context.Users.Any())
            {
                return;
            }

            SeedUsers(context);
        }

        private void SeedUsers(LandmarkRemarkContext context)
        {
            var users = new[]
            {
                new User
                {
                    FirstName = "Steve", LastName = "Smith", UserName = "steve.smith",CreatedOn=DateTime.Now, UserNotes = new List<Note>
                    {
                        new Note {Text = "Great place to visit", Location = new Point(145.1257112, -37.9131133), AddedOn=DateTime.Now}
                    },
                },
                new User
                {
                    FirstName = "Tom", LastName = "Cruise", UserName = "tom.cruise",CreatedOn=DateTime.Now, UserNotes = new List<Note>
                    {
                        new Note
                        {
                            Text = "Great place to visit", Location = new Point(153.0272569, -27.4640302), AddedOn=DateTime.Now
                        },
                        new Note {Text = "Great food", Location = new Point(152.9986068, -27.4522202),AddedOn=DateTime.Now}
                    }
                },

                new User {FirstName = "Peter", LastName = "Mccormack",CreatedOn=DateTime.Now,UserName = "peter.mccormack"}
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
    }
}