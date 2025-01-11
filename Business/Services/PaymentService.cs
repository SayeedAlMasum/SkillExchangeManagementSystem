﻿using Database.Model;
using Microsoft.EntityFrameworkCore;

namespace Services
{
    public class PaymentService
    {
        private readonly DbContext _context;

        public PaymentService(DbContext context)
        {
            _context = context;
        }

        public async Task<bool> ProcessPaymentAsync(string userInfoId, string courseId, string cardNumber, string expiryDate, string cvv)
        {
            // Validate payment details (e.g., card details and expiry date)
            if (string.IsNullOrWhiteSpace(cardNumber) || string.IsNullOrWhiteSpace(expiryDate) || string.IsNullOrWhiteSpace(cvv))
                return false;

            // Check if the course exists
            var course = await _context.Set<Course>().FindAsync(courseId);
            if (course == null)
                return false;

            // Create payment record
            var payment = new Payment
            {
                UserInfoId = userInfoId,
                CourseId = courseId,
                PaymentStatus = "Success", // Assuming a successful payment
                CardNumber = cardNumber,
                ExpiryDate = expiryDate,
                CVV = cvv
            };

            await _context.Set<Payment>().AddAsync(payment);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<List<Payment>> GetPaymentsForUserAsync(string userInfoId)
        {
            return await _context.Set<Payment>()
                .Where(p => p.UserInfoId == userInfoId)
                .ToListAsync();
        }
    }
}
