import React, { useState } from "react";
import RestaurantList from "./components/RestaurantList";
import "./index.css";
import logo from "./Assets/just-eat-logo.png"; // Importing logo
import { fetchRestaurantsByPostcode } from "./services/api";


function App() {
  const [postcode, setPostcode] = useState("");
  const [restaurants, setRestaurants] = useState([]);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState(null);

  // Validate postcode format (optional)
  const isValidPostcode = (postcode) => {
    const regex = /^[A-Z0-9]{2,4}\s?[A-Z0-9]{3}$/i;
    return regex.test(postcode.trim());
  };

  // Handle search for restaurants
  const handleSearch = async () => {
    setRestaurants([]);
    setError(null);

    if (!postcode.trim()) {
      setError("⚠️ Please enter a valid postcode.");
      return;
    }

    if (!isValidPostcode(postcode)) {
      setError("❌ Invalid postcode format. Please check and try again.");
      return;
    }

    setLoading(true);

    try {
      const data = await fetchRestaurantsByPostcode(postcode);

      if (data.length === 0) {
        setError("⚠️ No restaurants found for this postcode.");
      } else {
        setRestaurants(data);
      }
    } catch (error) {
      setError("❌ Error fetching restaurant data. Please try again later.");
    } finally {
      setLoading(false);
    }
  };


  return (
    <div className="App">
      {/* Header Section */}
      <header className="header">
        <img src={logo} alt="Just Eat Logo" className="logo" />
        <h1>
          Order food and more <br />
          <span className="subheading">
            Restaurants and grocery stores delivering near you
          </span>
        </h1>
      </header>

      {/* Search Section */}
      <div className="search-section">
        <input
          type="text"
          id="postcode" 
          name="postcode"
          placeholder="Enter Postcode (e.g., EC4M 7RF)"
          value={postcode}
          onChange={(e) => setPostcode(e.target.value)}
        />
        <button onClick={handleSearch} disabled={loading}>
          {loading ? "Searching..." : "Search"}
        </button>
      </div>

      {/* Display Error or Loading */}
      {error && <p className="error-message">{error}</p>}
      {loading && <p className="loading-message">🔎 Searching for restaurants...</p>}

      {/* Restaurant List Component */}
      {!loading && <RestaurantList restaurants={restaurants} />}
    </div>
  );
}

export default App;
