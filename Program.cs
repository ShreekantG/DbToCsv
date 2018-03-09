using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;
using PhoneNumbers;

namespace ConsoleApp2
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var test = new m5dbEntities())
            {
                Console.WriteLine("Processing Started... Please don't close the window");

                using (var textWriter = File.CreateText(@"C:\Temp\NewCsv.csv"))
                using (var csv = new CsvWriter(textWriter))
                {
                    var resultQuery = test.Database
                        .SqlQuery<UspBossUserMigrationToUC_Result>("exec UspBossUserMigrationToUC").ToList();

                    foreach (UspBossUserMigrationToUC_Result result in resultQuery)
                    {
                        csv.WriteField(result.uuid,true);
                        csv.WriteField("\t");
                        csv.WriteField(result.username);
                        csv.WriteField("\t");
                        csv.WriteField(result.first_name);
                        csv.WriteField("\t");
                        csv.WriteField(result.last_name);
                        csv.WriteField("\t");
                        csv.WriteField(result.status);
                        csv.WriteField("\t");
                        csv.WriteField(result.salutation);
                        csv.WriteField("\t");
                        csv.WriteField(result.title);
                        csv.WriteField("\t");
                        csv.WriteField(result.department);
                        csv.WriteField("\t");
                        csv.WriteField(ToE164(result.tn, (Country.Values)result.CountryId));
                        csv.WriteField("\t");
                        csv.WriteField(result.extension);
                        csv.WriteField("\t");
                        csv.WriteField(result.tenant_id);
                        csv.WriteField("\t");
                        csv.WriteField(result.boss_profile_id);
                        csv.WriteField("\t");
                        csv.WriteField(result.location_id);
                        csv.WriteField("\t");
                        csv.WriteField(result.date_created);
                        csv.WriteField("\t");
                        csv.WriteField(result.date_modified);
                        csv.WriteField("\t");
                        csv.WriteField(result.business);
                        csv.WriteField("\t");
                        csv.WriteField(result.home);
                        csv.WriteField("\t");
                        csv.WriteField(result.alt_1);
                        csv.WriteField("\t");
                        csv.WriteField(result.alt_2);
                        csv.WriteField("\t");
                        csv.WriteField(result.fax);
                        csv.WriteField("\t");
                        csv.WriteField(result.businessemail);
                        csv.WriteField("\t");
                        csv.WriteField(result.personal);
                        csv.WriteField("\t");
                        csv.WriteField(result.altemail_1);
                        csv.WriteField("\t");
                        csv.WriteField(result.altemail_2);
                        csv.WriteField("\t");
                        csv.WriteField(result.address_type);
                        csv.WriteField("\t");
                        csv.WriteField(result.address);
                        csv.WriteField("\t");
                        csv.WriteField(result.address_2);
                        csv.WriteField("\t");
                        csv.WriteField(result.address_3);
                        csv.WriteField("\t");
                        csv.WriteField(result.address_4);
                        csv.WriteField("\t");
                        csv.WriteField(result.address_5);
                        csv.WriteField("\t");
                        csv.WriteField(result.address_6);
                        csv.WriteField("\t");
                        csv.WriteField(result.address_7);
                        csv.WriteField("\t");
                        csv.WriteField(result.address_8);
                        csv.WriteField("\t");
                        csv.WriteField(result.city);
                        csv.WriteField("\t");
                        csv.WriteField(result.state);
                        csv.WriteField("\t");
                        csv.WriteField(result.country);
                        csv.WriteField("\t");
                        csv.WriteField(result.zip_code);
                        csv.WriteField("\t");
                        csv.WriteField(result.latitude);
                        csv.WriteField("\t");
                        csv.WriteField(result.longitude);
                        csv.WriteField("\t");
                        csv.WriteField(result.sub_premises);
                        csv.WriteField("\t");
                        csv.WriteField(result.dependent_locality);
                        csv.WriteField("\t");
                        csv.WriteField(result.name_on_door);
                        csv.NextRecord();
                    }
                }
                Console.WriteLine("Data is retrieved under below path - NewCsv.csv");
                Console.Write("Processing Completed... Press enter to exit");
                Console.Read();
            }
        }
        public static string ToE164(string tn, Country.Values country)
        {
            PhoneNumber number = null;

            number = country == Country.Values.Unknown ? Parse(tn) : Parse(tn, country);

            return number == null ? tn : PhoneNumberUtil.GetInstance().Format(number, PhoneNumberFormat.E164);
        }
        public static PhoneNumber Parse(string tn)
        {
            if (tn.StartsWith("+"))
                return PhoneNumberUtil.GetInstance().Parse(tn, null);

            var strippedTn = StripFormatting(tn);
            if (strippedTn.StartsWith("1") && strippedTn.Length == 11)
                return PhoneNumberUtil.GetInstance().Parse(tn, CountryAlpha2.Values.US.ToString());

            return null;
        }
        public static PhoneNumber Parse(string tn, Country.Values country)
        {
            var countryAlpha2 = Country.CountryToAlpha2(country);

            return (countryAlpha2 != null && tn != null) ? PhoneNumberUtil.GetInstance().Parse(tn, countryAlpha2.ToString()) : null;
        }
        public static string StripFormatting(string phoneNumber, bool retainLeadingPlusSign = false)
        {
            if (string.IsNullOrEmpty(phoneNumber))
                return string.Empty;

            StringBuilder sb = new StringBuilder();
            foreach (char c in phoneNumber)
            {
                if (char.IsDigit(c))
                    sb.Append(c);
            }

            if (retainLeadingPlusSign && phoneNumber.StartsWith("+") && sb.Length > 0)
            {
                sb.Insert(0, "+");
            }

            return sb.ToString();
        }
    }

}
