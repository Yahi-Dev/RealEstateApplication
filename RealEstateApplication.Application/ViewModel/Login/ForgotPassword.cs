﻿
namespace RealEstateApplication.Application.ViewModel.Login
{
    public class ForgotPasswordViewModel
    {
        public string? Email { get; set; }
        public bool HasError { get; set; }
        public string? Error { get; set; }
    }
}
