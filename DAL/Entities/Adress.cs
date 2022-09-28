namespace DAL.Entities
{
    public class Adress
    {
        public Guid Id { get; set; }
        public DateTime CreateDate { get; set; }
        public string Country { get; set; }
        public string AdressText { get; set; }
        public string Description { get; set; }

        public Client Client { get; set; }
        public Guid ClientId { get; set; }
        public Adress()
        {
            CreateDate = DateTime.Now;
        }
    }
}