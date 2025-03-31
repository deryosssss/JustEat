import React, { useState } from "react";
import RestaurantList from "./components/RestaurantList";
import "./index.css";
import logo from "./Assets/just-eat-logo.png"; // Importing logo

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
      <header className="header">
        <img src={logo} alt="Just Eat Logo" className="logo" />
        <h1>Order food and more <br />
        Restaurants and grocery stores delivering near you</h1>
      </header>

      <div className="search-section">
        <input
          type="text"
          placeholder="Enter Postcode"
          value={postcode}
          onChange={(e) => setPostcode(e.target.value)}
        />
        <button onClick={handleSearch}>Search</button>
      </div>

      <RestaurantList restaurants={restaurants} error={error} />
    </div>
  );
}

export default App;