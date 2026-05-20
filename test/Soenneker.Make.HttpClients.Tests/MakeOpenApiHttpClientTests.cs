using Soenneker.Make.HttpClients.Abstract;
using Soenneker.Tests.HostedUnit;

namespace Soenneker.Make.HttpClients.Tests;

[ClassDataSource<Host>(Shared = SharedType.PerTestSession)]
public sealed class MakeOpenApiHttpClientTests : HostedUnitTest
{
    private readonly IMakeOpenApiHttpClient _httpclient;

    public MakeOpenApiHttpClientTests(Host host) : base(host)
    {
        _httpclient = Resolve<IMakeOpenApiHttpClient>(true);
    }

    [Test]
    public void Default()
    {

    }
}
