namespace TreeListApp
{
    // TODO: перенести в проект, где идет обращение к базе (проект Data)
    public class TreeListDto
    {
        public int Id { get; set; }

        public int? ParentId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}
