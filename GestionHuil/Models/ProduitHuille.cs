namespace GestionHuil.Models
{
    public class ProduitHuille
    {
        public int Id { get; set; }
        public int Qte_En_Stock { get; set; }
        public float Prix_unitaire { get; set; }
        public Variete Variete { get; set; }
        public int VarieteId { get; set; }

    }
}
