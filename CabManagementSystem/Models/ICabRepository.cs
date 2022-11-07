namespace CabManagementSystem.Models
{
    public interface ICabRepository
    {
        Cab Add(Cab cab);
        Cab Update(Cab cab);
        Cab Delete(int id);

        IEnumerable<Cab> GetAllCab();
        Cab GetCab(int CarId);
    }
}
