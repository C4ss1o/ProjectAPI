using Flunt.Notifications;
using System.ComponentModel.DataAnnotations;

namespace ProjectAPI.Domain.Products
{
    public abstract class Entity : Notifiable<Notification>
    {
        [Key]
        public Guid Id { get; private set; } = Guid.NewGuid();

        public string CreateBy { get; set; }
        public DateTime CreateOn { get; set; } = DateTime.UtcNow;
        public string EditeBy { get; set; }
        public DateTime EditeOn { get; set; } = DateTime.UtcNow;
    }
}
