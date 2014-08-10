using System.Web.SessionState;
using Microsoft.Office365.OAuth;

namespace CRMMetadata
{
    public class OAuth2RedirectHandler : OAuth2RedirectHandler<FixedSessionCache>, IRequiresSessionState
    {
    }
}