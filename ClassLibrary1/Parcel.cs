﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAL.DalObject;

namespace IDAL
{
    namespace DO
    {
        public struct Parcel
        {
            public int Id{get; set;}
            public int SenderId{ get; set;}
            public int TargetId{get; set;}
            public int DroneId{get; set;}
            public Enum.Priorities Priority { get; set; }
            public Enum.WeightCategories Weight { get; set; }
            public DateTime Requested { get; set; }            public DateTime Scheduled { get; set; }            public DateTime PickedUp { get; set; }            public DateTime Delivered { get; set; }
            public int Selected_drone { get; set; }
            public override string ToString()
            {
                return $"Parcel ID: {Id}, sender: {SenderId}, target: {TargetId}, drone ID: {DroneId}, requested: {Requested}, scheduled: {Scheduled} ";
            }
            public enum Priorities { }
        }    }
}
