namespace Blog.Web.Models.Domain
{
    public class BlogPost
    {
        public Guid Id { get; set; }
        public string Heading { get; set; }
        public string PageTitle { get; set; }


        public string Content { get; set; }
        public string ShortDescription { get; set; }

        public string FeaturedImageUrl { get; set; }

        public string Author { get; set; }

        public DateTime PublishedDate { get; set; }

        public string UriHandle { get; set; }

        public bool Visible { get; set; }

        public ICollection<TagItem> Tags { get; set; }
    }
}
