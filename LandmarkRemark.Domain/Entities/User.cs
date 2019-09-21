using System.Collections.Generic;

namespace LandmarkRemark.Domain.Entities
{
    public class User
    {
        public User()
        {
            UserNotes=new List<Note>(); //could be hashset depending on if duplicate notes allowed , in that case, makes sense to override equality
        }
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        
        public ICollection<Note>UserNotes { get; set; }
    }
}