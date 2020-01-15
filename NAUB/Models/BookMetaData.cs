using System.Web.Mvc;

namespace NAUB.Models
{
    public class BookMetaData
    {
        public int Id { get; set; }

        [Remote("IsIsbnAvailable","Books", AdditionalFields = "Id", HttpMethod = "POST", ErrorMessage = "A book with this ISBN already exists...")]
        public string Isbn { get; set; }
    }
}