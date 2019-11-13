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
    public class TemplateGeneratorAll : ITemplateGeneratorAll
    {
        private readonly IConverter _converter;
        GlobalSettings globalSettings;

        ObjectSettings ObjectSettingsTrituration;
        StringBuilder StringBuilderTrituration;
        HtmlToPdfDocument HtmlToPdfDocumentTrituration;
        
        ObjectSettings ObjectSettingsAchat;
        StringBuilder StringBuilderAchat;
        HtmlToPdfDocument HtmlToPdfDocumentAchat;

        ObjectSettings ObjectSettingsVente;
        StringBuilder StringBuilderVente;
        HtmlToPdfDocument HtmlToPdfDocumentVente;

        ObjectSettings ObjectSettingsGrignon;
        StringBuilder StringBuilderGrignon;
        HtmlToPdfDocument HtmlToPdfDocumentGrignon;

        float saTrituration = 0;
        float saAchat = 0;
        string htmlTrituration = "";
        string htmlAchat = "";
        string htmlVente = "";
        string htmlGrignon = "";

        public TemplateGeneratorAll(IConverter converter)
        {
            _converter = converter;
            globalSettings = new GlobalSettings();

            ObjectSettingsTrituration = new ObjectSettings();
            StringBuilderTrituration = new StringBuilder();
            HtmlToPdfDocumentTrituration = new HtmlToPdfDocument();

            ObjectSettingsAchat = new ObjectSettings();
            StringBuilderAchat = new StringBuilder();
            HtmlToPdfDocumentAchat = new HtmlToPdfDocument();

            ObjectSettingsVente = new ObjectSettings();
            StringBuilderVente = new StringBuilder();
            HtmlToPdfDocumentVente = new HtmlToPdfDocument();

            ObjectSettingsGrignon = new ObjectSettings();
            StringBuilderGrignon = new StringBuilder();
            HtmlToPdfDocumentGrignon = new HtmlToPdfDocument();

            globalSettings.ColorMode = ColorMode.Color;
            globalSettings.Orientation = Orientation.Landscape;
            globalSettings.PaperSize = PaperKind.A5;
            globalSettings.Margins = new MarginSettings { Top = 10 };
        }
        public FileDto GetUsersAsPdfAsync(Trituration trituration , float s)
        {
            // trita = trituration;
            saTrituration = s;
            HtmlToPdfDocumentTrituration = null;
            htmlTrituration = GetHTMLStringTrituration(trituration, saTrituration);

            ObjectSettingsTrituration.PagesCount = true;
            ObjectSettingsTrituration.HtmlContent = htmlTrituration;
            ObjectSettingsTrituration.WebSettings.DefaultEncoding = "utf-8";
            ObjectSettingsTrituration.WebSettings.UserStyleSheet = Path.Combine(Directory.GetCurrentDirectory(), "Assets", "Style.css");
            ObjectSettingsTrituration.WebSettings.LoadImages = true;

            HtmlToPdfDocumentTrituration = new HtmlToPdfDocument()
            {
                GlobalSettings = globalSettings,
                Objects = { ObjectSettingsTrituration }
            };
            return new FileDto("UserList.pdf", _converter.Convert(HtmlToPdfDocumentTrituration));

        }
        public string GetHTMLStringTrituration(Trituration triturationsa , float s)
        {
            StringBuilderTrituration.Clear();
            StringBuilderTrituration.Append(@"
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
            StringBuilderTrituration.AppendFormat(@"
                    <p class='name'>{0}</p>
                    <p>Nom : {1}</p>
					<p>Gsm :{2}</p>
                     <p>Tel : {3}</p>
					
				</div>
				<div class='data right'>
					<div><h3>FACTURE TRITURATION N° 00 {4}</h3></div>
					<div class='date'>
						Date de Trituration:{5}<br>
					</div>
				</div></div>", triturationsa.Client.Nom, triturationsa.Client.Gsm, triturationsa.Client.Tel, triturationsa.Client.Ville, triturationsa.Id, triturationsa.Date);
            StringBuilderTrituration.Append(@"
			
			<table border = '0' cellspacing='0' cellpadding='0'>
				<thead>
					<tr>
						<th class='desc'>Description</th>
						<th class='qty'>Huile Obtenu</th>
                        <th class='unit'>Poids</th>
						<th class='unit'>Prix Unitaire</th>
						<th class='total'>Montant</th>
					</tr>
				</thead>
				<tbody>
					<tr>");
            StringBuilderTrituration.AppendFormat(@"
						<td class='desc'><h3>Type de variete : {0} | Rendement : {1}</h3></td>
						<td class='qty'>{2} L</td>
						<td class='unit'>{3} Kg</td>
                        <td class='unit'>{4} Kg</td>
						<td class='total'>{6} DT</td>
					</tr>
					
				</tbody>
			</table>
 <p><br/><br/><br/>
			<div class='no-break'>
				<table class='grand-total'>
					<tbody>
						<tr>
							<td class='desc'></td>
							<td class='unit' colspan='2'>RESTE A PAYER:</td>
							<td class='total'>{5} DT</td>
						</tr>
                        <tr>
							<td class='desc'></td>
							<td class='unit' colspan='2'>MONTANT TOTAL:</td>
							<td class='total'>{6} DT</td>
						</tr>
        ", triturationsa.Variete.Name, triturationsa.Rendement, triturationsa.HuileObtenu, triturationsa.Poids, triturationsa.PrixUnitaire, triturationsa.Montant - s, triturationsa.Montant);
            StringBuilderTrituration.Append(@"

                    </tbody>
				</table>
			</div>
		</div>
	</section>


</body>

</html>");
            return StringBuilderTrituration.ToString();
        }
        public FileDto GetUsersAsPdfAsync(Achat achat, float s)
        {
            saAchat = s;
            HtmlToPdfDocumentAchat = null;
            htmlAchat = GetHTMLStringAchat(achat, saAchat);

            ObjectSettingsAchat.PagesCount = true;
            ObjectSettingsAchat.HtmlContent = htmlAchat;
            ObjectSettingsAchat.WebSettings.DefaultEncoding = "utf-8";
            ObjectSettingsAchat.WebSettings.UserStyleSheet = Path.Combine(Directory.GetCurrentDirectory(), "Assets", "Style.css");
            ObjectSettingsAchat.WebSettings.LoadImages = true;
            HtmlToPdfDocumentAchat = new HtmlToPdfDocument()
            {
                GlobalSettings = globalSettings,
                Objects = { ObjectSettingsAchat }
            };
            return new FileDto("UserList.pdf", _converter.Convert(HtmlToPdfDocumentAchat));

        }
        public string GetHTMLStringAchat(Achat achatsa, float s)
        {
            StringBuilderAchat.Clear();
            //var achtasa = achatsa;
            // var sb = new StringBuilder();
            StringBuilderAchat.Append(@"
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
            StringBuilderAchat.AppendFormat(@"
                    <p class='name'>{0}</p>
                    <p>Nom : {1}</p>
					<p>Gsm :{2}</p>
                     <p>Tel : {3}</p>
					
				</div>
				<div class='data right'>
					<div class='title'>FACTURE ACHAT N° 00 {4}</div>
					<div class='date'>
						Date :{5}<br>
					</div>
				</div></div>", achatsa.Trituration.Client.Nom, achatsa.Trituration.Client.Gsm, achatsa.Trituration.Client.Tel, achatsa.Trituration.Client.Ville, achatsa.Id, achatsa.Trituration.Date);
            StringBuilderAchat.Append(@"
			<table border = '0' cellspacing='0' cellpadding='0'>
				<thead>
					<tr>
						<th class='desc'>Description</th>
						<th class='qty'>Qte</th>
                        <th class='unit'>Poids</th>
						<th class='unit'>Prix Unitaire</th>
						<th class='total'>Montant</th>
					</tr>
				</thead>
            
                <tbody>");
            StringBuilderAchat.AppendFormat(@"
                    <tr>    
						<td class='desc'><h3>Numero Trituration : {0}  Type huile : {1}</h3></td>
						<td class='qty'>{2} L</td>
                        <td class='unit'>{3}Kg</td>
                        <td class='unit'>{4}00</td>
                        <td class='total'> --- </td>
					</tr>", achatsa.Trituration.Id, achatsa.Trituration.Variete.Name, achatsa.Trituration.HuileObtenu, achatsa.Trituration.Poids, achatsa.Trituration.PrixUnitaire);
            StringBuilderAchat.AppendFormat(@"
                    <tr>    
						<td class='desc'><h3>Achat {0}</h3></td>
						<td class='qty'>{1}L</td>
                        <td class='unit'></td>
                        <td class='unit'>{2}00</td>
                        <td class='total'>{3} DT</td>
					</tr>", achatsa.Note, achatsa.QteAchete, achatsa.Prix_unitaire, achatsa.Prix_unitaire * achatsa.QteAchete);
            StringBuilderAchat.Append(@"</tbody></table><p><br/><br/><br/>");
            StringBuilderAchat.AppendFormat(@"
                   <div class='no-break'>
                 <table class='grand-total'>
					<tbody><br/><br/>
<tr>
							<td class='desc'></td>
							<td class='unit' colspan='2'>RESTE A PAYER:</td>
							<td class='total'>{0} DT</td>
						</tr>
						<tr>
							<td class='desc'></td>
							<td class='unit' colspan='2'>TOTAL Montant:</td>
							<td class='total'>{1}  DT</td>
						</tr>
					</tbody>
				</table>
			</div>", achatsa.MontantAchat - s, achatsa.Prix_unitaire * achatsa.QteAchete);
            StringBuilderAchat.Append(@" </div>
	</section>
</body>

</html>");
            return StringBuilderAchat.ToString();
        }
        public FileDto GetUsersAsPdfAsync(VenteHuile venteHuile, float s)
        {
            //var tritu = venteHuile;
            HtmlToPdfDocumentVente = null;
            htmlVente = GetHTMLStringVenteHuile(venteHuile, s);

            ObjectSettingsVente.PagesCount = true;
            ObjectSettingsVente.HtmlContent = htmlVente;
            ObjectSettingsVente.WebSettings.DefaultEncoding = "utf-8";
            ObjectSettingsVente.WebSettings.UserStyleSheet = Path.Combine(Directory.GetCurrentDirectory(), "Assets", "Style.css");
            ObjectSettingsVente.WebSettings.LoadImages = true;
            HtmlToPdfDocumentVente = new HtmlToPdfDocument()
            {
                GlobalSettings = globalSettings,
                Objects = { ObjectSettingsVente }
            };
            return new FileDto("UserList.pdf", _converter.Convert(HtmlToPdfDocumentVente));

        }
        public string GetHTMLStringVenteHuile(VenteHuile venteHuile, float s)
        {
            StringBuilderVente.Clear();
            //var sb = new StringBuilder();
            StringBuilderVente.Append(@"
                        <html>
<head>
	<title>Vente huile PDF</title>
</ head >
<body >
 <img src='http://localhost:59948/Assets/entetea.png' class='logoa'/>
   <p><br/><br/><br/><br/><br/><br/></p>
	<section>
		<div class='container'>
			<div class='details clearfix'>
				<div class='client left'>
					<p> Client :</p>");
            StringBuilderVente.AppendFormat(@"
                    <p class='name'>{0}</p>
                    <p>Nom : {1}</p>
					<p>Gsm :{2}</p>
                     <p>Tel : {3}</p>
					
				</div>
				<div class='data right'>
					<div><h3>FACTURE VENTE N° 00 {4}</h3></div>
					<div class='date'>
						Date de Vente:{5}<br>
					</div>
				</div></div>", venteHuile.Client.Nom, venteHuile.Client.Gsm, venteHuile.Client.Tel, venteHuile.Client.Ville, venteHuile.Id, venteHuile.Date);
            StringBuilderVente.Append(@"
			
			<table border = '0' cellspacing='0' cellpadding='0'>
				<thead>
					<tr>
						<th class='desc'>Description</th>
						<th class='qty'>Huile</th>
						<th class='unit'>Prix Unitaire</th>
						<th class='total'>Montant</th>
					</tr>
				</thead>
				<tbody>
					<tr>");
            StringBuilderVente.AppendFormat(@"
						<td class='desc'><h3>Type de variete : {0}    || Nombre Bidon : {1}</h3></td>
						<td class='qty'>{2} L</td>
						<td class='unit'>{3} DT</td>
						<td class='total'>{4}  DT</td>
					</tr>
					
				</tbody>
			</table>
 <p><br/><br/><br/>
			<div class='no-break'>
				<table class='grand-total'>
					<tbody>
<tr>
							<td class='desc'></td>
							<td class='unit' colspan='2'>RESTE A PAYER:</td>
							<td class='total'>{5}   DT</td>
						</tr>
						<tr>
							<td class='desc'></td>
							<td class='unit' colspan='2'>MONTANT TOTAL:</td>
							<td class='total'>{6} DT</td>
						</tr>
        ", venteHuile.Variete.Name, venteHuile.Qte_bidon, venteHuile.Qte_Vente, venteHuile.Prix_Unitaire, venteHuile.MontantVente,venteHuile.MontantVente - s, venteHuile.MontantVente);
            StringBuilderVente.Append(@"

                    </tbody>
				</table>
			</div>
		</div>
	</section>

</body>

</html>");
            return StringBuilderVente.ToString();
        }
        public FileDto GetUsersAsPdfAsync(Grignon grignon, float s)
        {
            //var tritu = grignon;
            //var sa = s;
            HtmlToPdfDocumentGrignon = null;
            htmlGrignon = GetHTMLStringVenteHuile(grignon, s);

            ObjectSettingsGrignon.PagesCount = true;
            ObjectSettingsGrignon.HtmlContent = htmlGrignon;
            ObjectSettingsGrignon.WebSettings.DefaultEncoding = "utf-8";
            ObjectSettingsGrignon.WebSettings.UserStyleSheet = Path.Combine(Directory.GetCurrentDirectory(), "Assets", "Style.css");
            ObjectSettingsGrignon.WebSettings.LoadImages = true;

            HtmlToPdfDocumentGrignon = new HtmlToPdfDocument()
            {
                GlobalSettings = globalSettings,
                Objects = { ObjectSettingsGrignon }
            };
            return new FileDto("UserList.pdf", _converter.Convert(HtmlToPdfDocumentGrignon));

        }
        public string GetHTMLStringVenteHuile(Grignon grignon, float s)
        {
            StringBuilderGrignon.Clear();
            StringBuilderGrignon.Append(@"
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
            StringBuilderGrignon.AppendFormat(@"
                    <p class='name'>{0}</p>
                    <p>Nom : {1}</p>
					<p>Gsm :{2}</p>
                     <p>Tel : {3}</p>
					
				</div>
				<div class='data right'>
					<div><h3>FACTURE GRIGNONS N° 00 {4}</h3></div>
					<div class='date'>
						Date de Trituration:{5}<br>
					</div>
				</div></div>", grignon.Client.Nom, grignon.Client.Gsm, grignon.Client.Tel, grignon.Client.Ville, grignon.Id, grignon.Date);
            StringBuilderGrignon.Append(@"
			
			<table border = '0' cellspacing='0' cellpadding='0'>
				<thead>
					<tr>
						<th class='desc'>Description</th>
						<th class='qty'>Huile</th>
						<th class='unit'>Prix Unitaire</th>
						<th class='total'>Montant</th>
					</tr>
				</thead>
				<tbody>
					<tr>");
            StringBuilderGrignon.AppendFormat(@"
						<td class='desc'><h3>Grignons </h3></td>
						<td class='qty'>{0} Kg</td>
						<td class='unit'>{1} </td>
						<td class='total'>{2} DT</td>
					</tr>
					
				</tbody>
			</table>
 <p><br/><br/><br/>
			<div class='no-break'>
				<table class='grand-total'>
					<tbody>
<tr>
							<td class='desc'></td>
							<td class='unit' colspan='2'>RESTE A PAYER:</td>
							<td class='total'>{2} DT</td>
						</tr>
						<tr>
							<td class='desc'></td>
							<td class='unit' colspan='2'>MONTANT TOTAL:</td>
							<td class='total'>{3} DT</td>
						</tr>
        ", grignon.Poids, grignon.Prix_unitaire, grignon.MontantAchat,grignon.MontantAchat - s, grignon.MontantAchat);
            StringBuilderGrignon.Append(@"

                    </tbody>
				</table>
			</div>
		</div>
	</section>

	<footer>
		<div class='container'>
			<div class='notice'>
				
			</div>
			<div class='end'>Invoice was created on a computer and is valid without the signature and seal.</div>
		</div>
	</footer>

</body>

</html>");
            return StringBuilderGrignon.ToString();
        }
    }

}
