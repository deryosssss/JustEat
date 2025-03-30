import React from "react";

const RestaurantList = ({ restaurants, error }) => {
  return (
    <div className="restaurant-list">
      {error && <p>{error}</p>}

      {restaurants.length > 0 ? (
        <div>
          <h2>Restaurants Found:</h2>
          <ul>
            {restaurants.map((restaurant, index) => (
              <li key={index}>
                <h3>{restaurant.name}</h3>
                <p>Cuisines: {restaurant.cuisines}</p>
                <p>Rating: {restaurant.rating}</p>
                <p>Address: {restaurant.address}</p>
              </li>
            ))}
          </ul>
        </div>
      ) : (
        <p>No restaurants found for this postcode.</p>
      )}
    </div>
  );
};

export default RestaurantList;

