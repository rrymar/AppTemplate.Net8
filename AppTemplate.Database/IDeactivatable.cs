namespace Core.Database
{
    public interface IDeactivatable
    {
        bool IsActive { get; set; }
    }
}