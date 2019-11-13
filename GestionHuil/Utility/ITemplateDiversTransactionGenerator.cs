using GestionHuil.Models;

namespace GestionHuil.Utility
{
    public interface ITemplateDiversTransactionGenerator
    {
        string GetHTMLString(DiversTransaction diversTransaction);
        FileDto GetUsersAsPdfAsync(DiversTransaction diversTransaction);
    }
}