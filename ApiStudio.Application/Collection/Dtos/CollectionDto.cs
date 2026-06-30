namespace ApiStudio.Application.Collection.Dtos;

public class CollectionDto
{
    public string? Name { get; set; }

    public string? Description { get; set; }
    public List<string>? Nodes { get; set; }
}