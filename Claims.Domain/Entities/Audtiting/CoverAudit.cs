using System.Security.Claims;

namespace Claims.Domain.Entities.Audtiting;

public class CoverAudit : Audit
{
    public Guid CoverId { get; set; }

    public override string ToString()
    {
        return $"CoverId: {CoverId}, HttpRequestType: {HttpRequestType}";
    }
}
