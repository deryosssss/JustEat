import React from "react";

const RestaurantList = ({ restaurants, error }) => {
  return (
    <div className="restaurant-list">
      {error && <p className="error">{error}</p>}

      {restaurants.length > 0 ? (
        <div>
          <h2>Restaurants Found:</h2>
          <ul>
            {restaurants.map((restaurant, index) => (
              <li key={index} className="restaurant-card">
                <h3>{restaurant.name}</h3>
                <p><strong>Cuisines:</strong> {restaurant.cuisines}</p>
                <p><strong>Rating:</strong> ⭐ {restaurant.rating}</p>
                <p><strong>Address:</strong> {restaurant.address}</p>
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


