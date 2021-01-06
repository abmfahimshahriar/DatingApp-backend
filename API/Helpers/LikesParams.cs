namespace API.Helpers
{
    public class LikesParams: PagiantionParams
    {
        public int UserId { get; set; }
        public string Predicate { get; set; }
    }
}