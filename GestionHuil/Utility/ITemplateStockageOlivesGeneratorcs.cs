using GestionHuil.Models;

namespace GestionHuil.Utility
{
    public interface ITemplateStockageOlivesGeneratorcs
    {
        string GetHTMLString(StockageOlive stockageOlivea);
        FileDto GetUsersAsPdfAsync(StockageOlive stockageOlive);
    }
}