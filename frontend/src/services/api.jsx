const API_BASE = process.env.REACT_APP_API_URL;

export const fetchRestaurantsByPostcode = async (postcode) => {
  const url = `${API_BASE}/api/restaurant/get-restaurants?postcode=${postcode.trim()}`;

  try {
    const response = await fetch(url);
    const raw = await response.text(); // ⬅️ get raw response as string

    if (!response.ok) {
      throw new Error("Bad response from API");
    }
    
    // Only try to parse JSON if response is OK
    const data = JSON.parse(raw);
    return data;
    
  } catch (error) {
    console.error("❌ Fetch error:", error); // 👈 log the actual error
    throw error;
  }
};
