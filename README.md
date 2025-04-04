# Just Eat Coding Assignment

Here is my attempt at the JustEat coding assessment: <br />
This project fetches restaurant data from the JustEat API based on a postcode and displays relevant details, including:

1) Restaurant Name
2) Cuisines
3) Rating (as a number)
4) Address (street, city, postcode)


## Prerequisites
To run this project, you need the following installed on your machine:
- .NET 6.0 SDK or later
- A text editor or IDE (e.g., Visual Studio, Visual Studio Code)
- Node.js and npm (for the frontend)

## How to build, compile and run
1) clone repository:
```
git clone https://github.com/deryosssss/JustEat.git 
```
```
cd JustEat  
```
#### backend
2) Navigate to the backend folder and restore dependencies: <br />
```
cd backend 
```
```
dotnet restore 
```
3) Build and run the application: <br />
```
dotnet run
```
The API will be available at https://localhost:5020/api/restaurant/get-restaurants?postcode={postcode} <br /> 
{postcode} = any valid uk postcode such as:<br />
http://localhost:5020/api/restaurant/get-restaurants?postcode=EC1A1BB

#### frontend 
4) Navigate to the frontend folder using a secondary terminal if needed (do remember to change directory to the just eat project) <br />
```
cd frontend
```
5) Install dependencies: <br />
```
npm install
```
6) Start the frontend: <br />
```
npm start 
```
7) Open http://localhost:3000 in your browser to use the application.

## Meeting the requirement
1) The API fetches restaurant data by sending a postcode to JustEat API.
2) From the received data, only the following fields are displayed: Name, Cuisines, Rating, and Address.
3) The list is limited to the first 10 restaurants returned.
4) The data is displayed in a clean, user-friendly UI using React.
5) The completed solution is hosted on GitHub with a visible commit history.

## Assumptions
- API Response Consistency: Assumes that the API response format will remain unchanged.
- This is a basic integration of the JustEat API. Error handling is minimal and doesn't include retry logic or complex failure handling in case of connection issues.
- No User Authentication: No authentication is required, as this is a simple demo application.
- Basic UI Implementation: The frontend focuses on displaying data rather than advanced features like user authentication or filters.
- Assumed that address includes firstLine, city and then postalCode
- The assumed rating value is a double, as per the format shown in the API response. This allows for decimal values to represent the rating.

## Improvements and Future Work
 - Improve Error Handling: Implement better exception handling with retry logic.
 - Add Caching: Reduce API calls by caching restaurant data for a short period.
 - Implement a favorites feature for users to save restaurants.
 - Dark mode toggle for better UI experience.
 - Unit Tests: Add tests for RestaurantService to ensure reliability.


## Additional features
- Mobile-Friendly Design – Fully responsive UI for different screen sizes.
- Smooth Animations & Transitions – Enhances user experience.
- Theme colors and logo matching Just Eat (orange & white) - HEX codes and logo derived from the website 
- Postcode Validation & Error Handling – Prevents invalid searches
- Modular Code Structure – Ensures easy maintenance and scalability.