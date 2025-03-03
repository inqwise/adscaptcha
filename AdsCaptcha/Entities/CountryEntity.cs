using System;
using Inqwise.AdsCaptcha.Common;
using System.Data;
namespace Inqwise.AdsCaptcha.Entities
{
    public class CountryEntity : ICountry
    {
        public CountryEntity()
        {

        }

        public CountryEntity(IDataReader reader)
        {
            Id = reader.ReadInt("Country_Id");
            try
            {
                Name = reader.ReadString("Country_Name");
                Prefix = reader.ReadString("Country_Prefix");
                IsDeleted = reader.ReadBool("Is_Deleted");
                IsAccessible = reader.ReadBool("Is_Accessible");
            }
            catch (Exception ex)
            {
                throw new Exception("Unable to parse the Country #" + Id, ex);
            }
        }

        public int Id
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public bool IsDeleted { get; set; }

        public bool IsAccessible { get; set; }

        public string Prefix { get; set; }
    }
}