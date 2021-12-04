namespace account_ms.Dtos
{
    public class CreateClientDto
    {
        public string fName { get; set;}
        public string sName { get; set; }
        public string sureName { get; set; }
        public int active { get; set; }
        public string email { get; set; }
        public long telNumber { get; set; }
        public string password { get; set; }
        public string image {get; set;}
    }
}
