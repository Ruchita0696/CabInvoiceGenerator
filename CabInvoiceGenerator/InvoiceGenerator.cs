using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CabInvoiceGenerator
{
    public class InvoiceGenerator
    {
        //Variable
        RideType objRideType;
        private RideRepository objRideRepository;
        //Constants
        private readonly double MINIMUM_COST_PER_KM;
        private readonly double COST_PER_TIME;
        private readonly double MINIMUM_FARE;

        ///Constructor to Create RideRepository instance.
        public InvoiceGenerator(RideType rideType)
        {
            this.objRideType = rideType;
            this.objRideRepository = new RideRepository();
            try
            {
                //If Ride type is Premium then RAtes set for premimum else for normal
                if (objRideType.Equals(RideType.PREMIUM))
                {
                    this.MINIMUM_COST_PER_KM = 15;
                    this.COST_PER_TIME = 2;
                    this.MINIMUM_FARE = 20;
                }
                else if (objRideType.Equals(RideType.NORMAL))
                {
                    this.MINIMUM_COST_PER_KM = 10;
                    this.COST_PER_TIME = 1;
                    this.MINIMUM_FARE = 5;
                }
            }
            catch (CabInvoiceException)
            {
                throw new CabInvoiceException(CabInvoiceException.ExceptionType.INVALID_RIDE_TYPE, "Invalid Ride Type");
            }
        }
        public double CalculateFare(double distance, int time)
        {
            double totalFare = 0;
            try
            {
                totalFare = distance * MINIMUM_COST_PER_KM + time * COST_PER_TIME;
            }
            catch (CabInvoiceException)
            {
                if (objRideType.Equals(null))
                {
                    throw new CabInvoiceException(CabInvoiceException.ExceptionType.INVALID_RIDE_TYPE, "Invalid Ride Type");
                }
                if (distance <= 0)
                {
                    throw new CabInvoiceException(CabInvoiceException.ExceptionType.INVALID_DISTANCE, "Invalid Distance");
                }
                if (time < 0)
                {
                    throw new CabInvoiceException(CabInvoiceException.ExceptionType.INVALID_TIME, "Invalid Time");
                }
            }
            return Math.Max(totalFare, MINIMUM_FARE);
        }
        public InvoiceSummary CalculateFare(Ride[] objrides)
        {
            double totalFare = 0;
            try
            {
                foreach (Ride ride in objrides)
                {
                    totalFare += this.CalculateFare(ride.distance, ride.time);
                }
            }
            catch (CabInvoiceException)
            {
                if (objrides == null)
                {
                    throw new CabInvoiceException(CabInvoiceException.ExceptionType.NULL_RIDES, "Ride Are Null");
                }
            }
            return new InvoiceSummary(objrides.Length, totalFare);
        }
        public void AddRides(string userId, Ride[] objrides)
        {
            try
            {
                objRideRepository.AddRide(userId, objrides);
            }
            catch (CabInvoiceException)
            {
                if (objrides != null)
                {
                    throw new CabInvoiceException(CabInvoiceException.ExceptionType.NULL_RIDES, "Rides Are Null");
                }
            }
        }
        public InvoiceSummary GetInvoiceSummary(string userId)
        {
            try
            {
                return this.CalculateFare(objRideRepository.GetRides(userId));
            }
            catch (CabInvoiceException)
            {
                throw new CabInvoiceException(CabInvoiceException.ExceptionType.INVALID_USER_ID, "Invalid User ID");
            }
        }
    }
}

            