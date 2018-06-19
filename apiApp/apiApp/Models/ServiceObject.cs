namespace apiApp.Models
{
    public class ServiceObject
    {
        public Metadata Metadata { get; set; }
        public Spec Spec { get; set; }
        public Status Status { get; set; }
    }
}