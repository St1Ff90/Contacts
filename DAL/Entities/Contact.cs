namespace DAL.Entities
{
    public class Contact
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Phone { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public DateTime BirthDayDate { get; set; }

        public Client? Client { get; set; }
        public Guid ClientId { get; set; }

        public Contact()
        {
            CreatedDate = DateTime.Now;
        }
    }
}