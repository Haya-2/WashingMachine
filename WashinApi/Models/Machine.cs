namespace WashinApi.Models
{
    public class Machine
    {
        public int Id { get; set; }
        public int Id_Building { get; set; }
        public int? userId { get; set; }
        public bool IsWorking { get; set; }

        public Building Building { get; set; }

    }
}
