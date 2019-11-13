using GestionHuil.Models;

namespace GestionHuil.Utility
{
    public interface ITemplateGeneratorAll
    {
        string GetHTMLStringAchat(Achat achatsa , float s);
        string GetHTMLStringTrituration(Trituration triturationsa , float s);
        string GetHTMLStringVenteHuile(VenteHuile venteHuile, float s);
        FileDto GetUsersAsPdfAsync(VenteHuile venteHuile, float s);
        FileDto GetUsersAsPdfAsync(Achat achat, float s);
        FileDto GetUsersAsPdfAsync(Trituration trituration , float s);
        FileDto GetUsersAsPdfAsync(Grignon grignon, float s);
        string GetHTMLStringVenteHuile(Grignon grignon, float s);
    }
}