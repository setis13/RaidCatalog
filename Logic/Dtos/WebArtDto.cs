namespace RaidCatalog.Logic.Dtos {
    public class WebArtDto {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ArtId { get; set; }
        public int? HeroId { get; set; }
        public byte? Level { get; set; }
        public short Order { get; set; }
        public string Comment { get; set; }
        public bool Ascend { get; set; }
        public bool Low { get; set; }
        public byte? Tag { get; set; }
    }
}
