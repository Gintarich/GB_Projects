﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GBCore
{
    public class Member
    {
        private int _startPointIndex;
        private int _endPointIndex;
        public Member(int startIndex, int endIndex)
        {
            _startPointIndex = startIndex;
            _endPointIndex = endIndex;
        }

    }
}
