namespace Seif.Models.SeifData
{
    public class Secure
    {
         
    }

    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

    }

    public class Role
    {
        public int Id { get; set; }
        public string Title { get; set; }
    }
}