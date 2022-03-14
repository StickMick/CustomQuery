namespace CustomQuery.Services;

public interface ISessionService
{
    public int GetUserId();
}

public class SessionService : ISessionService
{
    public int GetUserId()
    {
        return 1;
    }
}