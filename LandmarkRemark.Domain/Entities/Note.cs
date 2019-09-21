using NetTopologySuite.Geometries;

namespace LandmarkRemark.Domain.Entities
{
    public class Note
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public Point Location { get; set; }  
        
        public User User { get; set; }
      
    }
}