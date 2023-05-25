namespace lardota2.Contracts.User
{
    public class ReviewUserRequest
    {
        public int Userid { get; set; }

        public int? Rating { get; set; }

        public string? Comment { get; set; }
    }
}
