# JustEat
Overview
This project fetches restaurant data from the JustEat API based on a postcode and displays relevant details, including:

1) Restaurant Name
2) Cuisines
3) Rating (as a number)
4) Address (street, city, postcode)

This project is a backend service built using ASP.NET Core. The goal is to provide a simplified interface to fetch and return essential restaurant data in a user-friendly format.

## Prerequisites
To run this project, you need the following installed on your machine:
.NET 6.0 SDK or later
A text editor or IDE (e.g., Visual Studio, Visual Studio Code)

## How to Use
Use the endpoint GET /api/restaurant/get-restaurants?postcode={postcode} to get a list of restaurants for a given postcode.
Replace {postcode} with the actual postcode you want to search for.

The API will return a JSON response containing the name, cuisines, rating, and address for each restaurant.

Example Request
GET http://localhost:5020/api/restaurant/get-restaurants?postcode=EN35XU

## Assumptions
The JustEat API returns data in a specific structure. This solution assumes that the response will always contain valid restaurant data with the name, cuisines, rating, and address fields.

The country information is omitted in the address as it is not included in the response from the JustEat API.

If no restaurants are found for a given postcode, an empty array will be returned.

This is a basic integration of the JustEat API. Error handling is minimal and doesn't include retry logic or complex failure handling in case of connection issues.

Improvements and Future Work
Error Handling: Implement retry logic or better error handling in case of network issues or API downtime.

Pagination: The JustEat API may return many results. Adding pagination would be a useful improvement to limit the number of restaurants returned at once.

Caching: Implement caching for frequently queried postcodes to reduce the load on the JustEat API.