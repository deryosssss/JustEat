import { render, screen } from "@testing-library/react";
import RestaurantList from "./RestaurantList";

test("displays restaurant details", () => {
  const restaurants = [
    {
      name: "Pizza Place",
      cuisines: ["Pizza", "Italian"],
      rating: 4.5,
      address: "123 Pizza Street",
    },
  ];

  render(<RestaurantList restaurants={restaurants} />);

  expect(screen.getByText("Pizza Place")).toBeInTheDocument();
  expect(screen.getByText(/Pizza, Italian/)).toBeInTheDocument();
  expect(screen.getByText(/⭐ 4.5/)).toBeInTheDocument();
});
