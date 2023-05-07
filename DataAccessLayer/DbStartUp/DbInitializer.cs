namespace DataAccessLayer.DbStartUp;

public static class DbInitializer
{
    public static void Initialize(CalCalcContext context)
    {
        context.Database.EnsureCreated();
    }
}
