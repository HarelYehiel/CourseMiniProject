﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBL.BO;

namespace IBL
{
    
    public interface IBL
    {
        public void InsertOptions();

        /* In function Insert_options*/
        public void AddingBaseStation(int ID, string name, double Latitude, double Longitude, int numSlots);
        public void AddingDrone(int ID, string model, int maxWeight, int staId);
        public void AbsorptionNewCustomer(int ID, string nameCu, string phoneNumber, double Latitude, double Longitude);
        public void ReceiptOfPackageForDelivery(int parcelID, int senderName, int targetName, int maxWeight, int prioerity);

        /* Until here */

        public void UpdateOptions();

        /* In function Update_options*/
        public void UpdateDroneData(int ID, string newModel);
        public void UpdateStationData(int ID, string name, int numSlots);
        public void UpdateCustomerData(int ID, string custName, string phoneNumber);
        public void SendingDroneToCharging(int ID);
        public void ReleaseDroneFromCharging(int ID,int min);
        public void AssignPackageToDrone(int droneId);
        public void CollectionOfPackageByDrone(int droneId);
        public void DeliveryOfPackageByDrone(int droneId);
        /* Until here */

        public void EntityDisplayOptions();

        /* In function Entity_display_options*/
        public BO.station getBaseStation(int ID);
        public BO.Drone GetDrone(int ID);
        public BO.Customer GetCustomer(int ID);
        public BO.Parcel GetParcel(int ID);
        /* Until here */

        public void ListViewOptions();

        /* In function List_view_options*/
        public IEnumerable<BO.StationToTheList> GetListOfBaseStations();
        public IEnumerable<BO.DroneToList> GetTheListOfDrones();
        public IEnumerable<BO.CustomerToList> GetListOfCustomers();
        public IEnumerable<BO.ParcelToList> DisplaysTheListOfParcels();
        
        public IEnumerable<BO.DroneToList> GetAllDronesBy(System.Predicate<BO.DroneToList> filter);
        public IEnumerable<StationToTheList> GetAllStaionsBy(System.Predicate<IDAL.DO.Station> filter);
        public IEnumerable<CustomerToList> GetAllCustomersBy(System.Predicate<IDAL.DO.Customer> filter);
        public IEnumerable<ParcelToList> GetAllParcelsBy(System.Predicate<IDAL.DO.Parcel> filter);

        /* Until here */

    }
}
