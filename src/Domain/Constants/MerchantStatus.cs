namespace Domain.Constants;

public enum MerchantStatus
{
    NotComplete = 0,
    PhoneNumberConfirmed,
    LegalDataObtained,
    Complete,
}

// 0 - Not Complete: The merchant has started the sign-up process but hasn't confirmed their phone number.
// 1 - Phone Number Confirmed: The merchant has confirmed their phone number but hasn't completed the legal data process.
// 2 - Legal Data Obtained: The merchant has completed the legal data process but hasn't uploaded their bank information and documents.
// 3 - Complete: The merchant has completed all required steps, including legal data, bank information, and document uploads.