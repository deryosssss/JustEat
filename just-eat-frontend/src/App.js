import React, { useState } from "react";
import RestaurantList from "./components/RestaurantList";

function App() {
  const [postcode, setPostcode] = useState("");
  const [restaurants, setRestaurants] = useState([]);
  const [error, setError] = useState(null);

  const handleSearch = async () => {
    if (!postcode) return;

    try {
      const response = await fetch(
        `http://localhost:5020/api/restaurant/get-restaurants?postcode=${postcode}`
      );

      if (!response.ok) {
        throw new Error("Error fetching restaurant data.");
      }

      const data = await response.json();
      setRestaurants(data);
      setError(null);
    } catch (error) {
      setError("Error fetching restaurant data.");
      setRestaurants([]);
    }
  };

  return (
    <div className="App">
      <h1>Just Eat Restaurant Finder</h1>
      <input
        type="text"
        placeholder="Enter Postcode"
        value={postcode}
        onChange={(e) => setPostcode(e.target.value)}
      />
      <button onClick={handleSearch}>Search</button> 
      <RestaurantList restaurants={restaurants} error={error} />
    </div>
  );
}

export default App;
