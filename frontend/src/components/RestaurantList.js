// src/components/RestaurantList.js
import React from "react";
import "./RestaurantList.css";

function RestaurantList({ restaurants }) {
  if (restaurants.length === 0) {
    return null;
  }

  return (
    <div className="results">
      <h2> Top 10 Restaurants Near You</h2>
      <ul>
        {restaurants.map((r, index) => (
          <li key={index} className="restaurant-card">
            <strong>{r.name || "Unknown Restaurant"}</strong>
            <p>
              <strong>Cuisines:</strong> {r.cuisines || "N/A"}
            </p>
            <p>
              <strong>Rating:</strong> ⭐ {r.rating ? r.rating.toFixed(1) : "N/A"}
            </p>
            <p>
              <strong>Address:</strong> {r.address || "Address not available"}
            </p>
          </li>
        ))}
      </ul>
    </div>
  );
}

export default RestaurantList;


