namespace GestionHuil.Models
{
    public class Achat
    {
        public int Id { get; set; }
        public int QteAchete { get; set; }
        public float Prix_unitaire { get; set; }
        public float MontantAchat { get; set; }
        public string Note { get; set; }
        public Trituration Trituration { get; set;}
        public int TriturationId { get; set; }
    }
}
