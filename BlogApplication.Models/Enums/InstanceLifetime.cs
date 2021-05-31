namespace BlogApplication.Models.Enums
{
    public enum InstanceLifetime
    {
        PerDependency,
        PerLifeTimeScope,
        PerRequest,
        Singleton
    }
}