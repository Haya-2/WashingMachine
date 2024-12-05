using System.Text.Json.Serialization;

namespace WashinApi.Models
{
    public class Machine
    {
        public int Id { get; set; }
        public int Id_Building { get; set; }
        public int? UserId { get; set; }
        public bool IsWorking { get; set; }

        [JsonIgnore]
        public Building Building { get; set; }

    }
}
