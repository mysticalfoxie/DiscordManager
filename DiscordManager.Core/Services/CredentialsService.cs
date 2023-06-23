using DCM.Core.Interfaces;

namespace DCM.Core.Services;

public class CredentialsService : ICredentialsService
{
    public string LoginToken { get; set; }
}