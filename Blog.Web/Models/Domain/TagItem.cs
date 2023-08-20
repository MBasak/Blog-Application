namespace Blog.Web.Models.Domain
{
    public class TagItem
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string DisplayName { get; set; }
        public ICollection<BlogPost> BlogPosts { get; set; }
    }
}
