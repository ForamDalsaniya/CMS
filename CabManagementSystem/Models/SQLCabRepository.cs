namespace CabManagementSystem.Models
{
    public class SQLCabRepository : ICabRepository
    {
        private readonly AppDbContext context;

        public SQLCabRepository(AppDbContext context)
        {
            this.context = context;
        }
        public Cab Add(Cab cab)
        {
            context.cab.Add(cab);
            context.SaveChanges();
            return cab;
        }

        public Cab Delete(int id)
        {
            Cab cab = context.cab.Find(id);
            if(cab != null)
            {
                context.cab.Remove(cab);
                context.SaveChanges();
            }
            return cab;
        }

        public IEnumerable<Cab> GetAllCab()
        {
            return context.cab;
        }
        public Cab GetCab(int CarId)
        {
            return context.cab.Find(CarId);
        }
        public Cab Update(Cab cabChanges)
        {
            var cab = context.cab.Attach(cabChanges);
            cab.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return cabChanges;
        }
    }
}
