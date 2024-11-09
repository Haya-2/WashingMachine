using System.ComponentModel.DataAnnotations.Schema;

namespace WashinApi.Models
{
    public class Building
    {
        public int Id { get; set; }
        public string Name { get; set; }
        

        public List<User> Queue { get; set; } = new List<User>();

    }
}
