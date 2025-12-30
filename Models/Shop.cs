using ProniaA.Models.Base;

namespace ProniaA.Models
{
    public class Shop: BaseEntity
    {
        public string Image {  get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
    }
}
