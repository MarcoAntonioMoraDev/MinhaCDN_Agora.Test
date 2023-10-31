namespace CandidateTesting.MarcoAntonioMoraPalacios.Domain.Entities
{
    public class AgoraInputs
    {
        public decimal Version { get; set; }
        public DateTime Date { get; set; }
        public List<AgoraInput> Inputs { get; set; } = new List<AgoraInput>();


    }
}
