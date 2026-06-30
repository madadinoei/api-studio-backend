namespace ApiStudio.Application.Collection.Dtos;

public class CollectionDto
{
    public Guid Id { get; set; }
    public string? Name { get; set; }

    public string? Description { get; set; }
}

public class CollectionTreeDto
{
    public Guid Id { get; set; }
    public string? Name { get; set; }

    public string? Description { get; set; }
    public List<TreeNodeDto> Nodes { get; set; }
}




public class TreeNodeDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }

    public string Type { get; set; }

    public string? Method { get; set; }

    public string? RequestId { get; set; }

    public bool Expanded { get; set; }

    public List<TreeNodeDto> Children { get; set; }
}
public enum TreeNodeType
{
    Folder,
    Request
}