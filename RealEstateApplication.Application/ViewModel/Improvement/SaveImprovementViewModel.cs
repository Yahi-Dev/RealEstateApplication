﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateApplication.Application.ViewModel.Improvement
{
    public class SaveImprovementViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public bool HasError { get; set; }
        public string? Error { get; set; }

    }
}
