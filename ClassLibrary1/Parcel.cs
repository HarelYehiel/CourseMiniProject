﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAL
{
    namespace DO
    {
        public struct Parcel
        {
            public Parcel(int id, int y, int t, int r) : this()
            {
                
            }
            public int Id{get; set;}
            public int SenderId{ get; set;}
            public int TargetId{get; set;}
            public int DroneId{get; set;}
        }    }
}
