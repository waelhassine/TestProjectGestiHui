using DinkToPdf;
using DinkToPdf.Contracts;
using GestionHuil.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionHuil.Utility
{
    public class TemplateStockageOlivesGeneratorcs : ITemplateStockageOlivesGeneratorcs
    {
        private readonly IConverter _converter;
        GlobalSettings globalSettings;

        ObjectSettings ObjectSettingsStockage;
        StringBuilder StringBuilderStockage;
        HtmlToPdfDocument HtmlToPdfDocumentStockage;

        string htmlStockage;

        public TemplateStockageOlivesGeneratorcs(IConverter converter)
        {
            _converter = converter;
            globalSettings = new GlobalSettings();
            ObjectSettingsStockage = new ObjectSettings();
            StringBuilderStockage = new StringBuilder();
            HtmlToPdfDocumentStockage = new HtmlToPdfDocument();

            globalSettings.ColorMode = ColorMode.Color;
            globalSettings.Orientation = Orientation.Landscape;
            globalSettings.PaperSize = PaperKind.A5;
            globalSettings.Margins = new MarginSettings { Top = 10 };
        }
        public FileDto GetUsersAsPdfAsync(StockageOlive stockageOlive)
        {
            htmlStockage = GetHTMLString(stockageOlive);
            HtmlToPdfDocumentStockage = null;

            ObjectSettingsStockage.PagesCount = true;
            ObjectSettingsStockage.HtmlContent = htmlStockage;
            ObjectSettingsStockage.WebSettings.DefaultEncoding = "utf-8";
            ObjectSettingsStockage.WebSettings.UserStyleSheet = Path.Combine(Directory.GetCurrentDirectory(), "Assets", "Style.css");
            ObjectSettingsStockage.WebSettings.LoadImages = true;

            HtmlToPdfDocumentStockage = new HtmlToPdfDocument()
            {
                GlobalSettings = globalSettings,
                Objects = { ObjectSettingsStockage }
            };
            return new FileDto("UserList.pdf", _converter.Convert(HtmlToPdfDocumentStockage));

        }
        public string GetHTMLString(StockageOlive stockageOlivea)
        {
            StringBuilderStockage.Clear();
            //var sb = new StringBuilder();
            StringBuilderStockage.Append(@"
                        <html>
<head>
	<title>Trituration huile PDF</title>
</ head >
<body >
 <img src='http://localhost:59948/Assets/entetea.png' class='logoa'/>
   <p><br/><br/><br/><br/><br/><br/></p>
	<section>
		<div class='container'>
			<div class='details clearfix'>
				<div class='client left'>
					<p> Client :</p>");
            StringBuilderStockage.AppendFormat(@"
                    <p class='name'>{0}</p>
                    <p>Nom : {1}</p>
					<p>Gsm :{2}</p>
                     <p>Tel : {3}</p>
					
				</div>
				<div class='data right'>
					<div class='title'>Bon Réception N° 00{4}</div>
					<div class='date'>
						Date réception:{5}<br>
					</div>
				</div></div>", stockageOlivea.Client.Nom, stockageOlivea.Client.Gsm, stockageOlivea.Client.Tel, stockageOlivea.Client.Ville, stockageOlivea.Id, stockageOlivea.Date);
            StringBuilderStockage.Append(@"
			
			<table border = '0' cellspacing='0' cellpadding='0'>
				<thead>
					<tr>
						<th class='desc'>Description</th>
						<th class='qty'>Qte</th>
                        <th class='unit'>Chauffeur</th>
						<th class='total'>Vehicule</th>
					</tr>
				</thead>
				<tbody>
					<tr>");
            StringBuilderStockage.AppendFormat(@"
						<td class='desc'><h3>Stockage Olive : {0}</h3></td>
						<td class='qty'>{1} Kg</td>
						<td class='unit'>{2}</td>
						<td class='total'>{3}</td>
					</tr>
					
				</tbody>
			</table>
 <p><br/><br/><br/>
			<div class='no-break'>
				<table class='grand-total'>
					<tbody>
						<tr>
							<td class='desc'></td>
							<td class='unit' colspan='2'>QUANTITÉ TOTALE:</td>
							<td class='total'>{4} Kg</td>
						</tr>
        ", stockageOlivea.Variete.Name, stockageOlivea.Poids, stockageOlivea.Chauffeur, stockageOlivea.Vehicule, stockageOlivea.Poids);
            StringBuilderStockage.Append(@"

                    </tbody>
				</table>
			</div>
		</div>
	</section>

</body>

</html>");
            return StringBuilderStockage.ToString();
        }
    }
}
