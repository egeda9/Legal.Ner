namespace Legal.Ner.DTO
{
    public class TreeEdge
    {
        public string Id { get; set; }
        public string FromNode { get; set; }
        public string ToNode { get; set; }
        public bool Head { get; set; }
    }
}
