using System;
using ananauAPI.DTO;

namespace ananauAPI.Models
{
    public class Applicatie
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
        public String ReispaspoortNaam { get; set; }
        public String attestNaam { get; set; }
        public String diplomaNaam { get; set; }
        public Boolean PeriodeBevestigd { get; set; }
        public String WelkeWeg { get; set; }
        public String VragenAanOns { get; set; }
        public String Motivatie { get; set; }
        public Applicatie()
        {
            HuidigeStap = 1;
        }

        public void UpdateDezeApplicatie(ApplicatieDTO applicatieDTO) {
            Achternaam = applicatieDTO.Achternaam;
            Voornaam = applicatieDTO.Voornaam;
            Straatnaam = applicatieDTO.Straatnaam;
            Email = applicatieDTO.Email;
            Huisnummer = applicatieDTO.Huisnummer;
            Bus = applicatieDTO.Bus;
            Gemeente = applicatieDTO.Gemeente;
            Postcode = applicatieDTO.Postcode;
            GeboorteDatum = applicatieDTO.GeboorteDatum;
            GeboortePlaats = applicatieDTO.GeboortePlaats;
            Nationaliteit = applicatieDTO.Nationaliteit;
            PaspoortNummer = applicatieDTO.PaspoortNummer;
            TelefoonNummer = applicatieDTO.TelefoonNummer;
            TelefoonnummerNood = applicatieDTO.TelefoonnummerNood;
            VoornaamNood = applicatieDTO.VoornaamNood;
            AchternaamNood = applicatieDTO.AchternaamNood;
            EmailNood = applicatieDTO.EmailNood;
            RelatieNood = applicatieDTO.RelatieNood;
            Allergie = applicatieDTO.Allergie;
            MedischeAandoening = applicatieDTO.MedischeAandoening;
            Opleiding = applicatieDTO.Opleiding;
            ErvaringVrijwillger = applicatieDTO.ErvaringVrijwillger;
            SpaansNiveau = applicatieDTO.SpaansNiveau;
            TakenVrijwilliger = applicatieDTO.TakenVrijwilliger;
            VerwachtingenVrijwilliger = applicatieDTO.VerwachtingenVrijwilliger;
            Voorstellen = applicatieDTO.Voorstellen;
            HuidigeStap = applicatieDTO.HuidigeStap == 0 ? 1 : applicatieDTO.HuidigeStap;
            PeriodeStageVan = applicatieDTO.PeriodeStageVan;
            PeriodeStageTot = applicatieDTO.PeriodeStageTot;
            PeriodeVerblijfVan = applicatieDTO.PeriodeVerblijfVan;
            PeriodeVerblijfTot = applicatieDTO.PeriodeVerblijfTot;
            AantalWekenSpaans = applicatieDTO.AantalWekenSpaans;
            PeriodeBevestigd = applicatieDTO.PeriodeBevestigd;
            WelkeWeg = applicatieDTO.WelkeWeg;
            VragenAanOns = applicatieDTO.VragenAanOns;
            Motivatie = applicatieDTO.Motivatie;
        }


        public Applicatie(ApplicatieDTO applicatieDTO) :this()
        {
            this.PeriodeBevestigd = false;
            this.UpdateDezeApplicatie(applicatieDTO);
        }
    }
}
