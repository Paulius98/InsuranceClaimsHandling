using Claims.Domain.Enums;

namespace Claims.Domain.Entities.Audtiting;

public abstract class Audit
{ 
    public int Id { get; set; }
    public DateTimeOffset Created { get; set; }
    public HttpRequestMethodType HttpRequestType { get; set; }
}
