namespace Models.General
{
    public class SpecialtyUpdateModel
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public bool? Dirty { get; set; }
        public bool? Delete { get; set; }
        public bool? NewRecord { get; set; }
    }
}
