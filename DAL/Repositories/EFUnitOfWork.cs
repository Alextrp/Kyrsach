using DAL.Entities;
using DAL.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private readonly AppContext _context;
        private CargoRepository _cargoRepository;
        private CargoTypeRepository _cargoTypeRepository;
        private OrderRepository _orderRepository;
        private OrderStatusRepository _orderStatusRepository;
        private PaymentRepository _paymentRepository;
        private ReviewRepository _reviewRepository;
        private SessionRepository _sessionRepository;
        private TrackingRepository _trackingRepository;
        private UserRepository _userRepository;
        private VehicleRepository _vehicleRepository;
        private VehicleTypeRepository _vehicleTypeRepository;

        public EFUnitOfWork(AppContext context)
        {
            _context = context;
        }

        public ICargoRepository Cargos
        {
            get
            {
                if (_cargoRepository == null)
                    _cargoRepository = new CargoRepository(_context);
                return _cargoRepository;
            }
        }

        public ICargoTypeRepository CargoTypes
        {
            get
            {
                if (_cargoTypeRepository == null)
                    _cargoTypeRepository = new CargoTypeRepository(_context);
                return _cargoTypeRepository;
            }
        }

        public IOrderRepository Orders
        {
            get
            {
                if (_orderRepository == null)
                    _orderRepository = new OrderRepository(_context);
                return _orderRepository;
            }
        }

        public IOrderStatusRepository OrderStatus
        {
            get
            {
                if (_orderStatusRepository == null)
                    _orderStatusRepository = new OrderStatusRepository(_context);
                return _orderStatusRepository;
            }
        }

        public IPaymentRepository Payments
        {
            get
            {
                if (_paymentRepository == null)
                    _paymentRepository = new PaymentRepository(_context);
                return _paymentRepository;
            }
        }

        public IReviewRepository Reviews
        {
            get
            {
                if (_reviewRepository == null)
                    _reviewRepository = new ReviewRepository(_context);
                return _reviewRepository;
            }
        }

        public ISessionRepository Sessions
        {
            get
            {
                if (_sessionRepository == null)
                    _sessionRepository = new SessionRepository(_context);
                return _sessionRepository;
            }
        }

        public ITrackingRepository Trackings
        {
            get
            {
                if (_trackingRepository == null)
                    _trackingRepository = new TrackingRepository(_context);
                return _trackingRepository;
            }
        }

        public IUserRepository Users
        {
            get
            {
                if (_userRepository == null)
                    _userRepository = new UserRepository(_context);
                return _userRepository;
            }
        }

        public IVehicleRepository Vehicles
        {
            get
            {
                if (_vehicleRepository == null)
                    _vehicleRepository = new VehicleRepository(_context);
                return _vehicleRepository;
            }
        }

        public IVehicleTypeRepository VehicleTypes
        {
            get
            {
                if (_vehicleTypeRepository == null)
                    _vehicleTypeRepository = new VehicleTypeRepository(_context);
                return _vehicleTypeRepository;
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
                disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }

}
