namespace VisionPlatform.Domain.Entities
{
    public class VersionTask
    {
        public long Id { get; set; }

        public long VersionId { get; set; }

        public long ClienteId { get; set; }

        public long AreaId { get; set; }

        public string AzureTaskUrl { get; set; }

        public long AzureTaskId { get; set; }

        public string Titulo { get; set; }

        public string Tipo { get; set; }

        public string StatusPlanejamento { get; set; }

        public long? QaUserId { get; set; }

        public bool MergeRealizado { get; set; }

        public DateTime? DataMerge { get; set; }

        public long? QuemFezMerge { get; set; }

        public bool PossuiScript { get; set; }

        public string? ScriptDescricao { get; set; }

        public bool PossuiTagVersao { get; set; }

        public string? NomeTagGerada { get; set; }

        public int? OrdemExibicao { get; set; }

        public DateTime DataCriacao { get; set; } = DateTime.UtcNow;

        public ReleaseVersion Version { get; set; }

        public Cliente Cliente { get; set; }

        public Area Area { get; set; }

        public User QaUser { get; set; }

        public User? UsuarioMerge { get; set; }

        public ICollection<TestEvidence> Evidencias { get; set; }
    }
}
