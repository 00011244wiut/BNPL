![Backend Banner](https://telegra.ph/file/4f47f6b3f04e4d8b66628.png)
# Backend Repository

This repository contains the backend services for our application.

You can read and learn about related report with primary and secondary research about the BNPL industry in Uzbekistan [here](https://docs.google.com/document/d/1c5u_VMlW8WvlbIM9NAgjzpHo6rCcVXOi/edit?usp=sharing&ouid=107772990378977160799&rtpof=true&sd=true).


# Sign-Up and BNPL Service API

This repository contains the backend code for the Sign-Up and BNPL (Buy Now Pay Later) Service API. The API documentation is provided using OpenAPI 3.0.0 and is available [here](https://bnpl-qmob.onrender.com/swagger/index.html).

## Technologies Used
- Backend Framework: .NET Core 3.1
- Architecture: Clean Architecture

## Endpoints

### User Sign Up and Sign In
- `POST /otp/request`: Request OTP for phone number verification.
- `POST /otp/verify`: Verify received OTP.
- `POST /user/profile`: Submit user name and surname after OTP verification.
- `POST /user/kyc`: Submit KYC documents for verification.
- `POST /user/card`: Submit card information.
- `POST /user/scoring`: Initiate scoring process for the user.

### Product Order Process
- `POST /order/create`: Create an order.
- `GET /user/getUserInfo`: Fetch user information.
- `POST /order/confirm`: Confirm order.
- `GET /user/purchases`: Fetch all purchases.
- `GET /user/info`: Fetch user info.
- `GET /user/limit`: Fetch available limit.
- `GET /purchase/details/{purchaseId}`: Fetch purchase details.
- `GET /purchase/payments/{purchaseId}`: Fetch payment history by purchaseId.

### Merchant API
- `POST /merchant/otp/request`: Request OTP for phone verification.
- `POST /merchant/otp/verify`: Verify OTP.
- `POST /merchant/info/submit`: Submit company information.
- `POST /merchant/bank/submit`: Submit bank information.
- `POST /merchant/documents/upload`: Upload verification documents.
- `GET /merchant/info`: Fetch merchant info.
- `GET /merchant/taxpayer/info`: Fetch merchant info based on taxPayerId.

### Dashboard
- `POST /product/create`: Create a new product.
- `GET /product/details/{productId}`: Get product details.
- `PUT /product/update/{productId}`: Update product details.
- `GET /sales/by-merchant/{merchantId}`: Get all sales by merchant.

## Authentication
This API uses JWT (JSON Web Tokens) for authentication. You need to include a valid JWT token in the Authorization header of your requests.

## Getting Started
To set up the project locally, follow these steps:

1. Clone this repository.
2. Make sure you have .NET Core 3.1 installed on your machine.
3. Navigate to the project directory and run `dotnet restore` to restore the dependencies.
4. Update the `appsettings.json` file with your database connection string and other configurations.
5. Run `dotnet build` to build the project.
6. Run `dotnet run` to start the server locally.

---
**Copyright © 2024**  
*A project undertaken as part of BSc (Hons) Business Information Systems Degree, Westminster International University in Tashkent.*

