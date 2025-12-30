namespace ProniaA.Models.Base
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsDelited { get; set; }
    }
}
