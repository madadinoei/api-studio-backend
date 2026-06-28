namespace ApiStudio.Api.Models.Requests.Collections
{
    public class CreateCollectionRequest
    {
        public Guid WorkspaceId { get; set; }
        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }
    }
}
