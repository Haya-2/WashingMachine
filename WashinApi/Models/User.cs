namespace WashinApi.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public bool IsManager { get; set; }
        public int Id_Building { get; set; }

        public Building Building { get; set; }

    }
}
