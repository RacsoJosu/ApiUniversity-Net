namespace ApiRestfull.Models
{
    public class UserToken
    {
        public string Token { get; set; }

        public int Id { get; set; }
        

        public Roles Role { get; set; }

        public string UserName { get; set; }

        public TimeSpan Validity { get; set; }

        public string RefreshToken { get; set; }

        public string EmailId { get; set; }

        public Guid GuidId { get; set; }

        public DateTime ExpiredTime { get; set;}


    }
}
