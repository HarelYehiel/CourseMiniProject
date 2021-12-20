﻿using BO;
using System;
using System.Collections.Generic;
using System.Linq;


namespace BlApi
{
    public partial class BL : IBL
    {
        BO.EnumBO.Situations FunParcelSituation(DO.Parcel p)
        {
            if (p.Delivered != null) return BO.EnumBO.Situations.provided;
            else if (p.PickedUp != null) return BO.EnumBO.Situations.collected;
            else if (p.Scheduled != null) return EnumBO.Situations.associated;
            return BO.EnumBO.Situations.created;

        }

        // Return list of entity_BO ('entity' to list).
        public IEnumerable<StationToTheList> GetListOfBaseStations()
        {
            List<BO.StationToTheList> stations = GetAllStaionsBy(p => true).ToList();

            if (stations.Count == 0)
                throw new MyExeption_BO("Exception from function 'Displays_a_list_of_base_stations'", MyExeption_BO.An_empty_list);

            return stations;
        }
        public IEnumerable<DroneToList> GetTheListOfDrones()
        {
            return ListDroneToList;
        }
        public IEnumerable<CustomerToList> GetListOfCustomers()
        {

            List<BO.CustomerToList> customersToList = GetAllCustomersBy(p => true).ToList();

            if (customersToList.Count == 0)
                throw new MyExeption_BO("Exception from function 'Displays_a_list_of_customers'", MyExeption_BO.An_empty_list);

            return customersToList;

        }
        public IEnumerable<ParcelToList> DisplaysTheListOfParcels()
        {
            List<BO.ParcelToList> customersToList = GetAllParcelsBy(p => true).ToList();

            if (customersToList.Count == 0)
                throw new MyExeption_BO("Exception from function 'Displays_the_list_of_Parcels'", MyExeption_BO.An_empty_list);

            return customersToList;
        }


        // Filter functions of list with entity_DO and return list with entity_BO (after the filter).
        public IEnumerable<ParcelToList> GetAllParcelsBy(System.Predicate<DO.Parcel> filter)
        {
            List<ParcelToList> parcelsToLists = new List<ParcelToList>();

            parcelsToLists.AddRange(accessIdal.GetListOfParcels() // Get IEnumerable of all parcels.
                .ToList() // Comvert from IEnumerable to list.
                .FindAll(filter) // Filter the list by the filter.
                .ConvertAll(convertParcelDoToParcelBo));// Convert parcel_do to parcel_bo

            return parcelsToLists;
        }
        public IEnumerable<StationToTheList> GetAllStaionsBy(System.Predicate<DO.Station> filter)
        {
            List<StationToTheList> StationsToTheList = new List<StationToTheList>();

            StationsToTheList.AddRange(accessIdal.GetListOfStations() // Get IEnumerable of all stations.
                .ToList() // Comvert from IEnumerable to list.
                .FindAll(filter) // Filter the list by the 'filter'.
                .ConvertAll(convertStaionDoToStaionBo));// convert station_do to station_bo


            return StationsToTheList;
        }
        public IEnumerable<StationToTheList> GetAllStaionsBy22(System.Predicate<BO.station> filter)
        {
            List<StationToTheList> StationsToTheList = new List<StationToTheList>();
            return StationsToTheList;


        }

        public IEnumerable<CustomerToList> GetAllCustomersBy(System.Predicate<DO.Customer> filter)
        {
            List<CustomerToList> customersToList = new List<CustomerToList>();

            customersToList.AddRange(accessIdal.DisplaysListOfCustmers() // Get IEnumerable of all customers.
                     .ToList() // Comvert from IEnumerable to list.
                .FindAll(filter) // Filter the list by the 'filter'.
                .ConvertAll(convertCustomerDoToCustomerBo));// convert customer_do to customer_bo

            return customersToList;
        }
        public IEnumerable<DroneToList> GetAllDronesBy(System.Predicate<BO.DroneToList> filter)
        {
            try
            {
                List<DroneToList> DronesToList = new List<DroneToList>();

                DronesToList.AddRange(ListDroneToList // List of all drones in BO.
                    .FindAll(filter)); // Filter the list by the 'filter'.

                return DronesToList;
            }
            catch (Exception)
            {
                throw new BO.MyExeption_BO("No_object_by_this_filter");
            }
        }


        //  Convert functions from entity_DO to entity_BO.
        ParcelToList convertParcelDoToParcelBo(DO.Parcel item)
        {
            try
            {
                ParcelToList ParcelToListBO = new ParcelToList();

                ParcelToListBO = new ParcelToList();
                ParcelToListBO.uniqueID = item.Id;
                ParcelToListBO.nameTarget = accessIdal.GetCustomer(item.TargetId).name;
                ParcelToListBO.namrSender = accessIdal.GetCustomer(item.SenderId).name;
                ParcelToListBO.priority = (BO.EnumBO.Priorities)(int)item.priority;
                ParcelToListBO.weight = (BO.EnumBO.WeightCategories)(int)item.weight;
                ParcelToListBO.parcelsituation = FunParcelSituation(item);

                return ParcelToListBO;

            }
            catch (Exception e)
            {

                throw new BO.MyExeption_BO("Exception from function 'convertParcelDoToParcelBo'", e);
            }


        }
        StationToTheList convertStaionDoToStaionBo(DO.Station staion)
        {
            StationToTheList stationForTheList = new StationToTheList();

            stationForTheList.uniqueID = staion.id;
            stationForTheList.name = staion.name;
            stationForTheList.availableChargingStations = staion.ChargeSlots;

            stationForTheList.unAvailableChargingStations = accessIdal.GetListOfStations()
                .ToList() // Comvert from IEnumerable to list.
               .FindAll(droneCarge_DO => droneCarge_DO.id == staion.id) // Return list with all the droneCarge_DO == staion.id 
                .Count(); // Return count of item in the list.


            return stationForTheList;
        }
        CustomerToList convertCustomerDoToCustomerBo(DO.Customer customer)
        {
            CustomerToList customerToList = new CustomerToList();

            customerToList.uniqueID = customer.Id;
            customerToList.name = customer.name;
            customerToList.phone = customer.phone;


            // Run on the list parcel and find the parcels the related him (the customer).
            accessIdal.GetListOfParcels().ToList().ForEach(delegate (DO.Parcel parcel)
            {

                // packages sent and delivered
                if (parcel.SenderId == customerToList.uniqueID && parcel.Delivered != null)
                    customerToList.packagesSentAndDelivered++;
                // packages sent and not delivered
                if (parcel.SenderId == customerToList.uniqueID && parcel.PickedUp != null && parcel.Delivered == null)
                    customerToList.packagesSentAndNotDelivered++;
                // packages he received
                if (parcel.TargetId == customerToList.uniqueID && parcel.Delivered != null)
                    customerToList.packagesHeReceived++;
                // packages on the way to the customer
                if (parcel.TargetId == customerToList.uniqueID && parcel.PickedUp != null && parcel.Delivered == null)
                    customerToList.packagesOnTheWayToTheCustomer++;

            });

            return customerToList;
        }
    }
}