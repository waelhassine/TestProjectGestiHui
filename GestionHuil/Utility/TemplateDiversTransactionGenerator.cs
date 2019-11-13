using DinkToPdf;
using DinkToPdf.Contracts;
using GestionHuil.Data;
using GestionHuil.Models;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace GestionHuil.Utility
{
    public class TemplateDiversTransactionGenerator : ITemplateDiversTransactionGenerator
    {
        private readonly IConverter _converter;
        GlobalSettings globalSettings;

        ObjectSettings ObjectSettingsDivers;
        StringBuilder StringBuilderDivers;
        HtmlToPdfDocument HtmlToPdfDocumentDivers;


        string htmlStockage;


        public TemplateDiversTransactionGenerator(IConverter converter)
        {
            _converter = converter;
            globalSettings = new GlobalSettings();

            ObjectSettingsDivers = new ObjectSettings();
            StringBuilderDivers = new StringBuilder();
            HtmlToPdfDocumentDivers = new HtmlToPdfDocument();

            globalSettings.ColorMode = ColorMode.Color;
            globalSettings.Orientation = Orientation.Landscape;
            globalSettings.PaperSize = PaperKind.A5;
            globalSettings.Margins = new MarginSettings { Top = 10 };
        }
        public FileDto GetUsersAsPdfAsync(DiversTransaction diversTransaction)
        {
            htmlStockage = GetHTMLString(diversTransaction);


            ObjectSettingsDivers.PagesCount = true;
            ObjectSettingsDivers.HtmlContent = htmlStockage;
            ObjectSettingsDivers.WebSettings.DefaultEncoding = "utf-8";
            ObjectSettingsDivers.WebSettings.UserStyleSheet = Path.Combine(Directory.GetCurrentDirectory(), "Assets", "Style.css");
            ObjectSettingsDivers.WebSettings.LoadImages = true;


            HtmlToPdfDocumentDivers = new HtmlToPdfDocument()
            {
                GlobalSettings = globalSettings,
                Objects = { ObjectSettingsDivers }
            };
            return new FileDto("UserList.pdf", _converter.Convert(HtmlToPdfDocumentDivers));

        }
        public string GetHTMLString(DiversTransaction diversTransactiona)
        {
            // var sb = new StringBuilder();
            StringBuilderDivers.Clear();
            StringBuilderDivers.Append(@"
                        <html>
<head>
	<title>Achat huile PDF</title>
</ head >
<body >
 <img src='http://localhost:59948/Assets/entetea.png' class='logoa'/>
  <p><br/><br/><br/><br/><br/><br/></p>
	<section>
		<div class='container'>
			<div class='details clearfix'>
				<div class='client left'>
					<p> Client :</p>");
            StringBuilderDivers.AppendFormat(@"
                    <p class='name'>{0}</p>
                    <p>Nom : {1}</p>
					<p>Gsm :{2}</p>
                     <p>Tel : {3}</p>
					
				</div>
				<div class='data right'>
					<div class='title'>Avance N° 00 {4}</div>
					<div class='date'>
						Date :{5}<br>
					</div>
				</div></div>", diversTransactiona.Client.Nom, diversTransactiona.Client.Gsm, diversTransactiona.Client.Tel, diversTransactiona.Client.Ville, diversTransactiona.Id, diversTransactiona.Date);
            StringBuilderDivers.Append(@"
			<table border = '0' cellspacing='0' cellpadding='0'>
				<thead>
					<tr>
						<th class='desc'>Description</th>
						<th class='qty'>Type de paiement</th>
						<th class='total'>Montant</th>
					</tr>
				</thead>
            
                <tbody>");
            StringBuilderDivers.AppendFormat(@"
                    <tr>    
						<td class='desc'><h3> {0}</h3></td>
						<td class='qty'>{1} L</td>
                        <td class='total'> --- </td>
					</tr>", diversTransactiona.TypeTransaction, diversTransactiona.TypeDePaiement);
            StringBuilderDivers.Append(@"</tbody></table><p><br/><br/><br/>");
            StringBuilderDivers.AppendFormat(@"
                   <div class='no-break'>
                 <table class='grand-total'>
					<tbody><br/><br/>

						<tr>
							<td class='desc'></td>
							<td class='unit' colspan='2'>TOTAL Montant:</td>
							<td class='total'>{0} .000 Dt</td>
						</tr>
					</tbody>
				</table>
			</div>", diversTransactiona.Montant);
            StringBuilderDivers.Append(@" </div>
	</section>


</body>

</html>");
            return StringBuilderDivers.ToString();
        }
 

    }
}

