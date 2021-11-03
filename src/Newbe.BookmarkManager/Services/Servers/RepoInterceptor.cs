


using Castle.DynamicProxy;
using Newbe.BookmarkManager.Services.Servers;

public class RepoInterceptor:IRepoInterceptor
{

    private readonly BkManagerClient _bkManagerClient;

    public RepoInterceptor(BkManagerClient bkManagerClient)
    {
        _bkManagerClient = bkManagerClient;
    }

    public void Intercept(IInvocation invocation)
    {
        var method = invocation.Method;
        
    }
}