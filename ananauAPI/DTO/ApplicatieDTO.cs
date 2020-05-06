using System;
namespace ananauAPI.DTO
{
    public class ApplicatieDTO
    {
        public string Id { get; set; }
        public String Achternaam { get; set; }
        public String Voornaam { get; set; }
        public String Email { get; set; }
        public String Straatnaam { get; set; }
        public String Huisnummer { get; set; }
        public String Bus { get; set; }
        public String Gemeente { get; set; }
        public String Postcode { get; set; }
        public DateTime GeboorteDatum { get; set; }
        public String GeboortePlaats { get; set; }
        public String Nationaliteit { get; set; }
        public String PaspoortNummer { get; set; }
        public String TelefoonNummer { get; set; }
        public String TelefoonnummerNood { get; set; }
        public String VoornaamNood { get; set; }
        public String AchternaamNood { get; set; }
        public String EmailNood { get; set; }
        public String RelatieNood { get; set; }
        public String Allergie { get; set; }
        public String MedischeAandoening { get; set; }
        public String Opleiding { get; set; }
        public String ErvaringVrijwillger { get; set; }
        public String SpaansNiveau { get; set; }
        public String TakenVrijwilliger { get; set; }
        public String VerwachtingenVrijwilliger { get; set; }
        public String Voorstellen { get; set; }
        public int HuidigeStap { get; set; }
        public DateTime PeriodeStageVan { get; set; }
        public DateTime PeriodeStageTot { get; set; }
        public DateTime PeriodeVerblijfVan { get; set; }
        public DateTime PeriodeVerblijfTot { get; set; }
        public String AantalWekenSpaans { get; set; }
        public Boolean PeriodeBevestigd { get; set; }

        public ApplicatieDTO()
        {
        }

    }
}
