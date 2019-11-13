namespace GestionHuil.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string NomPrenom { get; set; }
        public string Email { get; set; }
        public string Tel { get; set; }
        public string Photo { get; set; }
        public string Status { get; set; }
        public string Fonction { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
    }
}
