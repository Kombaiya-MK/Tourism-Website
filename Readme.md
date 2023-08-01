# Kanini Tourism Website (Japan) 

## Sprint 0 - Requirement Analysis
## --------------------------------------------------------------------------
## Introduction

Kanini Tourism Website aims to provide a user-friendly platform for travelers to explore and plan their trips to various destinations in Japan. Inspired by the UI design of Visit Saudi, the website caters to three types of users: Admin, Travel Agents, and regular Users. The website will be built as a set of Micro Services, each responsible for specific functionalities.

## Users

1. Admin
2. Travel Agent
3. User

## Modules (Micro Services)

### 1. User Management (Admin)

#### Functionalities:
- Login: Admin can log in using valid credentials to access the Admin dashboard.
- Registration: Admin can register new users, both Travel Agents and regular Users, by providing necessary information (It affects only user entity).
- Forgot Password: Admin can initiate a password reset process for any user who has forgotten their password.
- Change Password: Admin can change their own password or reset the password of other users.
- Approve Travel Agent: Admin can approve registration requests from Travel Agents after reviewing their details.
- View Users: Admin can view a list of all registered users and their details.
- Update User Details: Admin can edit and update user information (e.g., name, email, contact details).
- Add Travel Agency: User can add his agency based on the service.
- Add Personal Details : (User details) it will add the user details in the userdetails entity.

### 2. Bus, Train, Airplane, Hotel, Locations

#### Functionalities:
- CRUD Operations: Admin can manage details for bus, train, airplane, hotel, and location services.
- Search and Filter: Users can search for services based on various criteria (e.g., destination, date, price range).
- Availability Check: Users can check the availability of seats/rooms/flights for specific dates.
- Sort Services: Users can sort services based on criteria like price, ratings, etc.
- Detailed Service Information: Users can view comprehensive information about each service, including amenities, photos, and reviews.
- Seat Allocation (Bus): Users can allocate seats while booking bus tickets.
- Room Allocation (Hotel): Users can allocate rooms while booking hotels.

### 3. Booking, Travel Information, Feedback, Contact, Bill Generation

#### Functionalities:
- CRUD Operations: Users and Travel Agents can manage bookings, travel information, feedback, contact/tickets, and bill generation.
- Booking Confirmation: Users receive a confirmation email with booking details after successful booking.
- Booking Cancellation: Users can cancel their bookings within a specified time frame, and refunds (if applicable) are automatically processed.
- Booking Modification: Users can modify their bookings, subject to availability and fare differences.
- Travel Alerts: The system can send travel alerts and reminders to users for their upcoming trips.
- Travel Itinerary: Users can access and download a comprehensive travel itinerary containing all their bookings and relevant information.
- Real-Time Updates: Travel Agents receive real-time updates about their clients' bookings and travel plans.
- Feedback Analysis: The system analyzes feedback received from users and Travel Agents to identify trends and areas for improvement.
- Support Ticket Escalation: The support team can escalate tickets to higher authorities for prompt resolution.
- Invoice Generation: The system generates detailed invoices for users' bookings, displaying a breakdown of charges.

### Additional Modules:

### 4. Payment Gateway Integration

#### Functionalities:
- Secure Payment Processing: The system integrates with a reliable payment gateway to ensure secure and hassle-free transactions.
- Payment Status: Users can view the status of their payments, and the Admin can track payment records.

### 5. Travel Packages (Admin)

#### Functionalities:
- Create and Manage Packages: The Admin can create and manage attractive travel packages, combining various services (e.g., flights, hotels, tours).
- Package Customization: Users can customize packages based on their preferences and budget.
- Package Recommendations: The system can recommend personalized travel packages to users based on their travel history and preferences.

### 6. User Reviews and Ratings

#### Functionalities:
- Users can submit reviews and ratings for services they have availed.
- Display Average Ratings: The system displays average ratings for each service based on user feedback.
- Sort by Ratings: Users can sort services based on their ratings and reviews.

### 7. Weather Forecast Integration

#### Functionalities:
- Users can access real-time weather forecasts for their travel destinations to plan their trips better.

## Conclusion

This document provides a comprehensive requirement analysis for the Kanini Tourism Website. The website's development team should use this information as a foundation for planning, designing, and implementing the various modules and functionalities. As the project progresses, additional features may emerge, and it's essential to engage in continuous discussions and iterations to meet the users' needs effectively. By delivering an intuitive and feature-rich platform, the Kanini Tourism Website will enhance travelers' experiences while exploring the beauty and culture of Japan.


## Sprint 1 - Features needs to be implemented

## Modules
- User Management
- Locations(Gallery)
- Tour Packages
- Hotels and Rooms
- Bookings
- Feedbacks 


## Features 
- Interactive Maps
- Weather on Location
- tour Packages with hotel and without hotel
- Bill generation
- Contact page

