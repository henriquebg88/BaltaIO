namespace DataAccessDapper.Models{
    public class Category
    {
        public Guid id { get; set; }
        public string Title { get; set; }
        public string url { get; set; }
        public string summary { get; set; }
        public int order { get; set; }
        public string description { get; set; }
        public bool featured { get; set; }
    }
}