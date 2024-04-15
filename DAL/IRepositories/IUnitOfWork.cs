using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.IRepositories
{
    public interface IUnitOfWork: IDisposable
    {
        ICargoRepository Cargos { get; }
        ICargoTypeRepository CargoTypes { get; }
        IOrderRepository Orders { get; }
        IOrderStatusRepository OrderStatus { get; }
        IPaymentRepository Payments { get; }
        IReviewRepository Reviews { get; }
        ISessionRepository Sessions { get; }
        ITrackingRepository Trackings { get; }
        IUserRepository Users { get; }
        IVehicleRepository Vehicles { get; }
        IVehicleTypeRepository VehicleTypes { get; }

        void Save();

    }
}
