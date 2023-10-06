namespace Spg.Mongo.DomainModel.Model 
{ 
    public class Product 
    {
        public int Id { set; get; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime CreationDate { get; set; }
    } 
} 
