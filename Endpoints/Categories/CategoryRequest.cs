namespace ProjectAPI.Endpoints.Categories;

    public class CategoryRequest
    {
        public string Name { get; set; }
        public bool Active { get; set; }
    public string Email { get; internal set; }
    public string Password { get; internal set; }
}

