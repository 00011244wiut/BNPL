namespace Domain.Constants;

public enum UserState
{
    NotComplete = 0,
    PhoneNumberConfirmed,
    VerificationCompleted,
    CompleteProfile
}

// 0 - Not Complete: The user has started the sign-up process but hasn't confirmed their phone number.
// 1 - Phone Number Confirmed: The user has confirmed their phone number but hasn't completed the verification process.
// 2 - Verification Completed: The user has passed KYC but hasn't completed their profile or added a card.
// 3 - Complete Profile: The user has completed all required steps, including profile information, KYC verification,
// card addition, and scoring process verification. They are now fully onboarded and can be redirected to the dashboard.